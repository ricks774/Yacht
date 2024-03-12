using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yacht
{
    public partial class Yachts1 : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetGuid();
                LoadLeftMenu();
                LoadYachtsImg();
                LoadOverView();
            }
            string getId = Request.QueryString["id"];
            if (getId != null)
            {
            }
        }

        #region "LoadYachtsImg() 讀取遊艇照片"

        private void LoadYachtsImg()
        {
            string firstGuid = Session["Guid"].ToString();
            if (Request.QueryString["id"] != null)
            {
                string getGuid = Request.QueryString["id"].ToString();
                string sql = "select ya.Id, ya.YachtId, ya.ImgPath, ya.IsLayout, ya.IsTop, ym.Guid, CONCAT(ym.YachtModelName, ' ', ym.YachtModelNo) AS YachtModel from YachtsAlbum ya left join YachtsManager ym on ya.YachtId = ym.Id" +
                    " where ya.YachtModelGuid = @YachtModelGuid and IsLayout = 0";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@YachtModelGuid", SqlDbType.NVarChar) { Value = getGuid }
                };
                SqlDataReader reader = yachtHelper.SearchData(sql, parameters);
                if (reader.Read())
                {
                    Lb_YachtsTitle.InnerText = reader["YachtModel"].ToString();
                    Lb_RighTopLink.InnerText = reader["YachtModel"].ToString();
                }
                Rp_Img.DataSource = reader;
                Rp_Img.DataBind();
            }
            else
            {
                string sql = "select ya.Id, ya.YachtId, ya.ImgPath, ya.IsLayout, ya.IsTop, ym.Guid, CONCAT(ym.YachtModelName, ' ', ym.YachtModelNo) AS YachtModel from YachtsAlbum ya left join YachtsManager ym on ya.YachtId = ym.Id" +
                    " where ya.YachtModelGuid = @YachtModelGuid and IsLayout = 0";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@YachtModelGuid", SqlDbType.NVarChar) { Value = firstGuid }
                };
                SqlDataReader reader = yachtHelper.SearchData(sql, parameters);
                if (reader.Read())
                {
                    Lb_YachtsTitle.InnerText = reader["YachtModel"].ToString();
                    Lb_RighTopLink.InnerText = reader["YachtModel"].ToString();
                }
                Rp_Img.DataSource = reader;
                Rp_Img.DataBind();
            }
            yachtHelper.CloseConnection();
        }

        #endregion "LoadYachtsImg() 讀取遊艇照片"

        #region "LoadOverView() 讀取遊行型號資料"

        private void LoadOverView()
        {
            string getGuid = Session["Guid"].ToString();

            // 設定切換標籤時讓 Guid 的值能夠導到其他頁面
            Lr_Interior.Text = $"<li><a class='menu_yli01' href='Yachts1.aspx?id={getGuid}'></a></li>";
            Lr_Layout.Text = $"<li><a class='menu_yli02' href='Yachts_2.aspx?id={getGuid}'></a></li>";
            Lr_Specification.Text = $"<li><a class='menu_yli03' href='Yachts_3.aspx?id={getGuid}'></a></li>";
            //Lr_Video.Text = $"<li><a class='menu_yli04' href='Yachts_2.aspx?id={getGuid}'></a></li>";

            string sql = "select ym.Guid, ym.YachtModelNo, ys.OverViewContents, ys.OverViewDimensions, SUBSTRING(ys.FilePath, CHARINDEX('_', ys.FilePath) + 1, LEN(ys.FilePath) - CHARINDEX('_', ys.FilePath)) AS FileName, ys.FilePath from YachtsManager ym left join Yachts ys on ys.YachtsId = ym.Id " +
                "where ym.Guid = @YachtGuid";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtGuid", SqlDbType.NVarChar) { Value = getGuid }
            };
            SqlDataReader reader = yachtHelper.SearchData(sql, parameters);
            if (reader.Read())
            {
                Lr_OverViewContent.Text = HttpUtility.HtmlDecode(reader["OverViewContents"].ToString());
                Lr_OverViewDimension.Text = HttpUtility.HtmlDecode(reader["OverViewDimensions"].ToString());
                Hl_DownloadFile.Text = reader["FileName"].ToString();
                h4_DimensionTitle.InnerText = reader["YachtModelNo"].ToString() + " DIMENSION";
                // 沒有附檔案就不顯示下載區塊
                if (reader["FilePath"].ToString() != "")
                {
                    Div_downloads.Visible = true;
                    Hl_DownloadFile.NavigateUrl = reader["FilePath"].ToString();
                }
            }
        }

        #endregion "LoadOverView() 讀取遊行型號資料"

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
                    //Lr_Interior.Text = $"<li><a class='menu_yli01' href='Yachts1.aspx?id={guidStr}'></a></li>";
                    //Lr_Layout.Text = $"<li><a class='menu_yli02' href='Yachts_2.aspx?id={guidStr}'></a></li>";
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
                leftMenuHtml.Append($"<li><a href='Yachts1.aspx?id={guidStr}'>{yachtModelStr} {isNewStr}</a></li>");
            }
            yachtHelper.CloseConnection();

            // 渲染畫面
            Lr_LeftMenuHtml.Text = leftMenuHtml.ToString();
        }

        #endregion "讀取左側遊艇型號清單"
    }
}