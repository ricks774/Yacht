using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace Yacht.backStage
{
    public partial class UserBoard : System.Web.UI.Page
    {
        private DBHelper helper = new DBHelper();
        private Argon2Verify ag2Verify = new Argon2Verify();

        protected void Page_Load(object sender, EventArgs e)
        {
            //// 判斷是否有登入
            //if (Session["Account"] == null) { Response.Redirect("~/backStage/SignIn.aspx"); }
            //else
            //{
            //    // 判斷是否為管理員
            //    bool maxPower = (bool)Session["MaxPower"];
            //    if (maxPower) { ShowData(); }
            //    else { Response.Redirect("~/backStage/Navigate.aspx"); }
            //}
            // 取得驗證票夾帶資訊
            try
            {
                string ticketUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
                string[] ticketUserDataArr = ticketUserData.Split(';');
                bool haveRight = HttpContext.Current.User.Identity.IsAuthenticated;
                if (haveRight)
                {
                    //for (int i = 0; i < ticketUserDataArr.Length; i++)
                    //{
                    //    Response.Write($"<script>alert('{ticketUserDataArr[i]}')</script>");
                    //}
                    if (ticketUserDataArr[0].Equals("True"))
                    {
                        GV_all.Visible = true;
                        // TODO 不知道為什麼會跑出來...
                        //newAccount_btn.Visible = true;
                    }
                    else
                    {
                        GV_all.Visible = false;
                        newAccount_btn.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write($"<script>alert('{ex}')</script>");
            }

            if (!IsPostBack)
            {
                ShowData();
            }
        }

        // ! 顯示資料
        private void ShowData()
        {
            string sql = "select * from YachtsUser";
            var reader = helper.SearchData(sql);
            GV_all.DataSource = reader;
            GV_all.DataBind();
            // TODO 找到 admin 關閉刪除按鈕
            GV_all.Rows[0].Cells[4].Controls.Clear();   // 移除第一行第5個控制項，避免管理者刪掉自己的帳號
            helper.CloseConnection();
        }

        // ! 新增使用者
        protected void Btn_newAccount_Click(object sender, EventArgs e)
        {
            bool haveSameAccount = false;
            string account = input_act.Text;
            string password = input_pwd.Text;
            string name = input_name.Text;

            if (account == "" || password == "")
            {
                Response.Write("<script>alert('帳號或密碼不能空白!!')</script>");
            }
            else
            {
                // 檢查帳號是否重複
                string checkSQL = "select * from YachtsUser where Account = @Account";
                SqlParameter[] checkParameters =
                {
                    new SqlParameter("@Account", SqlDbType.NVarChar) { Value = account }
                };
                var checkReader = helper.SearchData(checkSQL, checkParameters);
                if (checkReader.Read())
                {
                    haveSameAccount = true;
                    //lb_account_check.Visible = true;
                    Response.Write("<script>alert('帳號重複!')</script>");
                }
                helper.CloseConnection();

                if (!haveSameAccount)
                {
                    // Hash 加鹽加密
                    var salt = ag2Verify.CreateSalt();
                    string saltStr = Convert.ToBase64String(salt);  // 將byte改回字串
                    var hash = ag2Verify.HashPassword(password, salt);
                    string hashPassword = Convert.ToBase64String(hash);

                    string insertSQL = "INSERT INTO YachtsUser (Account, Password, Salt, Name) VALUES(@NewAccount, @Password, @Salt, @Name)";
                    SqlParameter[] insertParameters =
                    {
                        new SqlParameter("@NewAccount", SqlDbType.NVarChar) { Value = account },
                        new SqlParameter("@Password", SqlDbType.NVarChar) { Value = hashPassword },
                        new SqlParameter("@Salt", SqlDbType.NVarChar) { Value = saltStr },
                        new SqlParameter("@Name", SqlDbType.NVarChar) { Value = name },
                    };

                    helper.ExecuteSQL(insertSQL, insertParameters);
                    ShowData();

                    // 清除輸入框
                    input_act.Text = "";
                    input_pwd.Text = "";
                    input_name.Text = "";
                }
            }
        }

        // ! GridView取消編輯
        protected void GV_all_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_all.EditIndex = -1;
            ShowData();
        }

        // ! GridView刪除事件
        protected void GV_all_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string getAct = GV_all.DataKeys[e.RowIndex].Value.ToString(); // 取得資料表的帳號

            string delUser = $"delete YachtsUser where Account = @Account";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Account", SqlDbType.NVarChar) { Value = getAct }
            };
            int result = helper.ExecuteSQL(delUser, parameters);

            if (result > 0) Response.Write("<script>alert('成功刪除')</script>");
            else Response.Write("<script>alert('刪除失敗')</script>");

            GV_all.EditIndex = -1;
            ShowData();
        }

        // ! GridView更新事件
        protected void GV_all_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GV_all.Rows[e.RowIndex];  // 取的正在更新的索引
            string getAccount = GV_all.DataKeys[e.RowIndex].Value.ToString(); // 取得資料表的帳號
            TextBox tb_title = row.FindControl("Name") as TextBox;  // 取得目前編輯列的控制項
            string newName = tb_title.Text;
            string sql = $"update YachtsUser set Name = @Name where Account = @Account";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value =  newName},
                new SqlParameter("@Account", SqlDbType.NVarChar) { Value = getAccount }
            };
            helper.ExecuteSQL(sql, parameters);

            GV_all.EditIndex = -1;
            ShowData();
        }

        // ! GridView編輯事件
        protected void GV_all_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_all.EditIndex = e.NewEditIndex;
            ShowData();
        }
    }
}