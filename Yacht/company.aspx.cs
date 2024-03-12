using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yacht
{
    public partial class company : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadContent();
            }
        }

        private void LoadContent()
        {
            //從資料庫取資料
            string sqlCountry = "SELECT TOP 1 AboutUsHtml FROM Company";
            var reader = yachtHelper.SearchData(sqlCountry);
            if (reader.Read())
            {
                //渲染畫面
                Literal1.Text = HttpUtility.HtmlDecode(reader["aboutUsHtml"].ToString());
            }
            yachtHelper.CloseConnection();
        }
    }
}