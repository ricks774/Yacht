using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetVips;

namespace Yacht
{
    public partial class index : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBanner();
                LoadNews();
            }
        }

        // ! 讀取Banner
        private void LoadBanner()
        {
            string sql = "select * from YachtsManager ORDER BY id DESC";
            var reader = yachtHelper.SearchData(sql);
            StringBuilder bannerHtml = new StringBuilder();
            while (reader.Read())
            {
                // 遊艇型號字串用空格切割區分文字及數字
                //string[] modelArr = reader["YachtModel"].ToString().Split(' ');
                string modelName = reader["YachtModelName"].ToString();
                string modelNo = reader["YachtModelNo"].ToString();
                string imgNameStr = reader["BannerImgPathJSON"].ToString();

                // 依新設計或新建造來切換顯示標籤
                string isNewDesignStr = reader["IsNewDesign"].ToString();
                string isNewBuildingStr = reader["IsNewBuilding"].ToString();
                string newTagStr = ""; //標籤檔名用
                // value 預設為 0 不顯示標籤
                string displayNewStr = "0";
                //判斷是否顯示對應標籤
                if (isNewDesignStr.Equals("True"))
                {
                    displayNewStr = "1";
                    newTagStr = "images/new02.png";
                }
                else if (isNewBuildingStr.Equals("True"))
                {
                    displayNewStr = "1";
                    newTagStr = "images/new01.png";
                }

                //加入遊艇型號輪播圖 HTML
                bannerHtml.Append($"<li class='info' style='border-radius: 5px;height: 424px;width: 978px;'><a href='' target='_blank'><img src='{imgNameStr}' style='width: 978px;height: 424px;border-radius: 5px;'/></a><div class='wordtitle'>{modelName} <span>{modelNo}</span><br /><p>SPECIFICATION SHEET</p></div><div class='new' style='display: none;overflow: hidden;border-radius:10px;'><img src='{newTagStr}' alt='new' /></div><input type='hidden' value='{displayNewStr}' /></li>");
            }
            yachtHelper.CloseConnection();

            //渲染畫面
            Lt_Banner.Text = bannerHtml.ToString();
            Lt_BannerNum.Text = bannerHtml.ToString(); //不顯示但影響輪播圖片數量計算
        }

        // ! 讀取新聞
        private void LoadNews()
        {
            // 設定首頁3筆新聞的時間範圍
            DateTime nowTime = DateTime.Now;
            string nowDate = nowTime.ToString("yyyy-M-dd");
            int startDate = -1;
            DateTime limitTime = nowTime.AddMonths(startDate);
            string limitDate = limitTime.ToString("yyyy-M-dd");

            // 計算設定的時間範圍內新聞數量
            //string sql = "select COUNT(id) from News where ImgIsTop3 >= @LimitData AND DateTitle <= @NowDate";
            //SqlParameter[] parameters =
            //{
            //    new SqlParameter("@LimitData", SqlDbType.Date) { Value = limitDate },
            //    new SqlParameter("@NowDate", SqlDbType.Date) { Value = nowDate }
            //};
            //int newsNum = yachtHelper.SearchDataCountData(sql, parameters);
            //while (newsNum < 3)
            //{
            //    startDate--;
            //    limitTime = nowTime.AddDays(startDate);
            //    limitDate = limitTime.ToString("yyyy-MM-dd");
            //    SqlParameter[] newParameters =
            //    {
            //        new SqlParameter("@LimitData", SqlDbType.Date) { Value = limitDate },
            //        new SqlParameter("@NowDate", SqlDbType.Date) { Value = nowDate }
            //    };
            //    newsNum = yachtHelper.SearchDataCountData(sql, newParameters);
            //}
            yachtHelper.CloseConnection();

            // 取出時間範圍內首 3 筆新聞，且 TOP 會放在取出項的最前面
            string top3SQL = "select TOP 3 * FROM News WHERE DateTitle <= @NowDate ORDER BY IsTop DESC, DateTitle DESC";
            SqlParameter[] top3Parameters =
            {
                //new SqlParameter("@LimitData", SqlDbType.Date) { Value = limitDate },
                new SqlParameter("@NowDate", SqlDbType.Date) { Value = nowDate }
            };
            var reader = yachtHelper.SearchData(top3SQL, top3Parameters);

            int count = 1; //第幾筆新聞
            while (reader.Read())
            {
                string isTopStr = reader["IsTop"].ToString();
                string guidStr = reader["Guid"].ToString();
                if (count == 1)
                {
                    //渲染第1筆新聞圖卡
                    string newsImg = reader["ThumbnailPath"].ToString();
                    Lt_NewsImg1.Text = $"<img id='thumbnail_Image1' src='{newsImg}' style='border-width: 0px;' />";
                    DateTime dateTimeTitle = DateTime.Parse(reader["dateTitle"].ToString());
                    Lb_NewsDate1.Text = dateTimeTitle.ToString("yyyy/M/d");
                    HL_News1.Text = reader["headline"].ToString();
                    HL_News1.NavigateUrl = $"newsView.aspx?id={guidStr}";
                    if (isTopStr.Equals("True"))
                    {
                        ImgIsTop1.Visible = true;
                    }
                }
                else if (count == 2)
                {
                    //渲染第2筆新聞圖卡
                    string newsImg = reader["thumbnailPath"].ToString();
                    Lt_NewsImg2.Text = $"<img id='thumbnail_Image2' src='{newsImg}' style='border-width: 0px;' />";
                    DateTime dateTimeTitle = DateTime.Parse(reader["dateTitle"].ToString());
                    Lb_NewsDate2.Text = dateTimeTitle.ToString("yyyy/M/d");
                    HL_News2.Text = reader["headline"].ToString();
                    HL_News2.NavigateUrl = $"newsView.aspx?id={guidStr}";
                    if (isTopStr.Equals("True"))
                    {
                        ImgIsTop2.Visible = true;
                    }
                }
                else if (count == 3)
                {
                    //渲染第3筆新聞圖卡
                    string newsImg = reader["thumbnailPath"].ToString();
                    Lt_NewsImg3.Text = $"<img id='thumbnail_Image3' src='{newsImg}' style='border-width: 0px;' />";
                    DateTime dateTimeTitle = DateTime.Parse(reader["dateTitle"].ToString());
                    Lb_NewsDate3.Text = dateTimeTitle.ToString("yyyy/M/d");
                    HL_News3.Text = reader["headline"].ToString();
                    HL_News3.NavigateUrl = $"newsView.aspx?id={guidStr}";
                    if (isTopStr.Equals("True"))
                    {
                        ImgIsTop3.Visible = true;
                    }
                }
                else break; // 超過3筆後停止
                count++;
            }
            yachtHelper.CloseConnection();
        }
    }
}