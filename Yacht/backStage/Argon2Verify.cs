using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace Yacht.backStage
{
    public class Argon2Verify
    {
        private readonly DBHelper helper = new DBHelper();

        // ! 產生salt功能
        public byte[] CreateSalt()
        {
            var buffer = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(buffer);
            return buffer;
        }

        // ! Hash處理加鹽的密碼功能
        public byte[] HashPassword(string password, byte[] salt)
        {
            // 建立Argon2id實例，並使用UTF - 8編碼將輸入的密碼轉換為位元組陣列
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            // 設定的值會影響計算時間，驗證時也要使用相同的設定
            argon2.Salt = salt;  // 設定了Argon2計算中使用的鹽（Salt），是一個隨機值
            argon2.DegreeOfParallelism = 8;  // 計算密碼散列時同時使用的核心數，用8個核心並行處理
            argon2.Iterations = 4;  //  設定了計算的迭代次數
            argon2.MemorySize = 1024 * 1024;    // 記憶體的使用量，設定為1 GB
            return argon2.GetBytes(16);
        }

        // ! 驗證加密密碼
        // ! 驗證
        private bool VerifyHash(string password, byte[] salt, byte[] hash)
        {
            var newHash = HashPassword(password, salt);
            return hash.SequenceEqual(newHash); // 比較兩個位元組數組（hash 和 newHash）是否相等
        }

        // ! 設定驗證票
        private void SetAuthenTicket(string userData, string userId)
        {
            // 宣告一個驗證票 要引入 using System.Web.Security
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userId, DateTime.Now, DateTime.Now.AddHours(3), false, userData);
            // 加密驗證票
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            // 建立 Cookie
            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            // 將 Cookie 寫入回應
            HttpContext.Current.Response.Cookies.Add(authenticationCookie);
        }

        // ! 登入驗證
        public void Argon2_Login(string account, string password)
        {
            SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["UserDbConnection"].ConnectionString);
            helper.ConnectDB();
            string sql = "SELECT * from YachtsUser WHERE Account = @Account";
            SqlCommand sqlcommand = new SqlCommand(sql, connection);
            sqlcommand.Parameters.AddWithValue("@Account", account);
            //sqlcommand.Parameters.AddWithValue("@Password", password);

            // 資料庫用 Adapter 執行指令
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlcommand);
            // 建立一個空的 Table
            DataTable dataTable = new DataTable();
            // 將資料放入 Table 內
            dataAdapter.Fill(dataTable);

            // SQL有找到資料時執行
            if (dataTable.Rows.Count > 0)
            {
                byte[] hash = Convert.FromBase64String(dataTable.Rows[0]["Password"].ToString());
                byte[] salt = Convert.FromBase64String(dataTable.Rows[0]["Salt"].ToString());

                // 驗證密碼
                bool success = VerifyHash(password, salt, hash);

                if (success)
                {
                    // 宣告驗證票要夾帶的資料(用 ; 區隔)
                    string userData =
                        dataTable.Rows[0]["MaxPower"].ToString() + ";" +
                        dataTable.Rows[0]["Account"].ToString() + ";" +
                        dataTable.Rows[0]["Name"].ToString() + ";" +
                        dataTable.Rows[0]["Email"].ToString();
                    // 設定驗證票(夾帶資料、Cookie命名)
                    SetAuthenTicket(userData, account);

                    // 更新最後登入時間
                    DateTime now = DateTime.Now;
                    string loginDate = now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    string loginSQL = "UPDATE YachtsUser SET LastLoginDate = @LoginDate WHERE Account = @Account";
                    SqlParameter[] parameters =
                    {
                            new SqlParameter("@LoginDate", SqlDbType.DateTime) { Value = loginDate },
                            new SqlParameter("@Account", SqlDbType.NVarChar) { Value = account }
                        };
                    helper.ExecuteSQL(loginSQL, parameters);

                    HttpContext.Current.Response.Write("<script>alert('OK')</script>");
                    // 導頁至權限分流頁面
                    HttpContext.Current.Response.Redirect("~/backStage/CheckAccount.ashx");
                    //HttpContext.Current.Response.Redirect("~/backStage/test.ashx");
                }
                else
                {
                    // 資料庫找不到資料時 表示密碼有錯誤
                    HttpContext.Current.Response.Write("<script>alert('密碼輸入錯誤!')</script>");
                    connection.Close();
                    return;
                }
            }
            else
            {
                //資料庫裡找不到相同資料時，表示帳號有誤!
                HttpContext.Current.Response.Write("<script>alert('帳號輸入錯誤!')</script>");
                connection.Close();
                return;
            }
            connection.Close();
        }
    }
}