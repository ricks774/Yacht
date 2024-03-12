using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Configuration;

namespace Yacht
{
    public partial class yachts : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetGuid();
                LoadLeftMenu();
            }
            string getId = Request.QueryString["id"];
            if (getId != null)
            {
                LoadYachtsImg();
            }
        }

        #region "LoadYachtsImg() 讀取遊艇照片"

        private void LoadYachtsImg()
        {
            string getId = Request.QueryString["id"].ToString();
            string sql = "select * from YachtsAlbum where YachtModelGuid = @YachtModelGuid and IsLayout = 0";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtModelGuid", SqlDbType.NVarChar) { Value = getId }
            };
            SqlDataReader reader = yachtHelper.SearchData(sql, parameters);
            if (reader.Read())
            {
            }
            Rp_Img.DataSource = reader;
            Rp_Img.DataBind();
            yachtHelper.CloseConnection();
        }

        #endregion "LoadYachtsImg() 讀取遊艇照片"

        #region "GetGuid() 取得網址傳值的對應型號Guid"

        private void GetGuid()
        {
            string guidStr = Request.QueryString["id"];
            string sql = "select Top 1 Guid from YachtsManager";
            SqlDataReader reader = yachtHelper.SearchData(sql);
            if (reader.Read())
            {
                // 如果沒有傳值就用第一筆遊艇型號的 Guid
                if (String.IsNullOrEmpty(guidStr))
                {
                    guidStr = reader["Guid"].ToString().Trim();
                }
            }
            yachtHelper.CloseConnection();
            Session["Guid"] = guidStr;
        }

        #endregion "GetGuid() 取得網址傳值的對應型號Guid"

        #region "讀取左側遊艇型號清單"

        private void LoadLeftMenu()
        {
            StringBuilder leftMenuHtml = new StringBuilder();

            //取得遊艇型號資料
            string sql = "select CONCAT(YachtModelName, ' ', YachtModelNo) AS YachtModel,IsNewDesign, IsNewBuilding,Guid from YachtsManager";
            SqlDataReader reader = yachtHelper.SearchData(sql);
            // 反覆變更字串的值建議用 StringBuilder 效能較好

            while (reader.Read())
            {
                string yachtModelStr = reader["yachtModel"].ToString();
                string isNewDesignStr = reader["isNewDesign"].ToString();
                string isNewBuildingStr = reader["isNewBuilding"].ToString();
                string guidStr = reader["guid"].ToString();
                string isNewStr = "";
                //依是否為新建或新設計加入標註
                if (isNewDesignStr.Equals("True"))
                {
                    isNewStr = "(New Design)";
                }
                else if (isNewBuildingStr.Equals("True"))
                {
                    isNewStr = "(New Building)";
                }
                // StringBuilder 用 Append 來加入字串內容
                leftMenuHtml.Append($"<li><a href='yachts.aspx?id={guidStr}'>{yachtModelStr} {isNewStr}</a></li>");
            }
            yachtHelper.CloseConnection();

            // 渲染畫面
            Lr_LeftMenuHtml.Text = leftMenuHtml.ToString();
        }

        #endregion "讀取左側遊艇型號清單"
    }
}