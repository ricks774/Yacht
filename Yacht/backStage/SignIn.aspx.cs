using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Yacht;
using Yacht.backStage;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using System.Web.Security;

namespace Dashboard
{
    public partial class Sign_in : System.Web.UI.Page
    {
        private readonly Yacht.backStage.DBHelper helper = new Yacht.backStage.DBHelper();
        private readonly Argon2Verify ag2Verify = new Argon2Verify();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Btn_login(object sender, EventArgs e)
        {
            string account = acc_input.Text;
            string password = pwd_input.Text;
            ag2Verify.Argon2_Login(account, password);

            //string act = acc_input.Text;
            //string pwd = pwd_input.Text;
            //var ac = helper.Login(act, pwd);

            //if (ac.Read())
            //{
            //    Session["Account"] = ac["Account"];
            //    Session["Name"] = ac["Name"];
            //    Session["MAxPower"] = ac["MaxPower"];
            //    helper.CloseConnection();

            //    // 更新最後登入時間
            //    DateTime now = DateTime.Now;
            //    string loginDate = now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            //    string sql = "UPDATE UserInfo SET LastLoginDate = @LoginDate WHERE Account = @Account";
            //    SqlParameter[] parameters =
            //    {
            //        new SqlParameter("@LoginDate", SqlDbType.DateTime) { Value = loginDate },
            //        new SqlParameter("@Account", SqlDbType.NVarChar) { Value = act }
            //    };
            //    helper.ExecuteSQL(sql, parameters);

            //    Response.Redirect("~/backStage/Navigate.aspx");
            //}
            //else Response.Write("<script>alert('帳號或密碼輸入錯誤!')</script>");
        }
    }
}