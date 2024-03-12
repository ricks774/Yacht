using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yacht
{
    public partial class news1 : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNews();
            }
        }

        private void LoadNews()
        {
            string guidStr = Request.QueryString["id"];
            // 如果沒有網址傳址就回到新聞列表
            if (String.IsNullOrEmpty(guidStr)) { Response.Redirect("~/news.aspx"); }

            // 依據 giud 取得新聞資料
            string sql = "select * from News where Guid = @Guid";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Guid", SqlDbType.NVarChar) { Value = guidStr }
            };
            var reader = yachtHelper.SearchData(sql, parameters);
            if (reader.Read())
            {
                // 渲染新聞標題
                NewsTitle.InnerText = reader["HeadLine"].ToString();
                //渲染新聞主文
                ShowNewsContent.Text = HttpUtility.HtmlDecode(reader["NewsContentHtml"].ToString());
            }
        }
    }
}