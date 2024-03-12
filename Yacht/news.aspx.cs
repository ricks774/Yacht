using NetVips;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yacht
{
    public partial class news : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadNewsList();
            }
        }

        private void LoadNewsList()
        {
            // 取得目前的時間
            DateTime nowTime = DateTime.Now;
            string nowDate = nowTime.ToString("yyyy-M-dd");

            int page = 1;   // 預設為第一頁
            // 判斷網址後有無參數
            if (!String.IsNullOrEmpty(Request.QueryString["page"]))
            {
                page = Convert.ToInt32(Request.QueryString["page"]);
            }
            // 設定頁面參數屬性
            // 設定控制項參數: 一頁幾筆資料
            Pagination.limit = 5;
            // 設定控制項參數: 作用頁面完整網頁名稱
            Pagination.targetPage = "news.aspx";

            // 建立計算分頁資料顯示邏輯 (每一頁是從第幾筆開始到第幾筆結束)
            // 計算每個分頁的第幾筆到第幾筆
            var floor = (page - 1) * Pagination.limit + 1; //每頁的第一筆
            var ceiling = page * Pagination.limit; //每頁的最末筆

            // 建立計算資料筆數的 SQL 語法
            // 算出我們要秀的資料數
            string sql = $"SELECT COUNT(id) FROM News WHERE DateTitle <= '{nowDate}'";
            SqlParameter[] parameters =
            {
                new SqlParameter("@NowDate", SqlDbType.Date) { Value =  nowDate}
            };
            int count = yachtHelper.SearchDataCountData(sql, parameters);
            yachtHelper.CloseConnection();

            // 將取得的資料筆數設定給頁面參數屬性
            // 設定控制項參數: 總共幾筆資料
            Pagination.totalItems = count;

            // 使用 ShowPageControls() 渲染至網頁
            // 渲染分頁控制項
            Pagination.ShowPageControls();

            // 將原始資料表的 SQL 語法使用 CTE 暫存表改寫，並使用 ROW_NUMBER() 函式製作資料項流水號 rowindex
            // SQL 用 CTE 暫存表 + ROW_NUMBER 去生出我的流水號 rowindex 後以流水號為條件來查詢暫存表
            // 排序先用 isTop 後用 dateTitle 產生 TOP News 置頂效果
            string tempSQL = $"WITH temp AS (SELECT ROW_NUMBER() OVER (ORDER BY isTop DESC, dateTitle DESC) AS rowindex, * FROM News ) SELECT * FROM temp WHERE rowindex >= {floor} AND rowindex <= {ceiling} AND DateTitle <= @NowDate";
            SqlParameter[] tempParameters =
            {
                new SqlParameter("@NowDate", SqlDbType.Date) { Value = nowDate }
            };
            var reader = yachtHelper.SearchData(tempSQL, tempParameters);

            StringBuilder newListHtml = new StringBuilder();

            while (reader.Read())
            {
                string idStr = reader["id"].ToString();
                DateTime dateTimeTitle = DateTime.Parse(reader["dateTitle"].ToString());
                string dateTitleStr = dateTimeTitle.ToString("yyyy/M/d");
                string headlineStr = reader["headline"].ToString();
                string summaryStr = reader["summary"].ToString();
                string thumbnailPathStr = reader["thumbnailPath"].ToString();
                string guidStr = reader["guid"].ToString();
                string isTopStr = reader["isTop"].ToString();
                string displayStr = "none";
                if (isTopStr.Equals("True"))
                {
                    displayStr = "inline-block";
                }
                newListHtml.Append($"<li><div class='list01'><ul><li><div class='news01'>" +
                    $"<img src='images/new_top01.png' alt='&quot;&quot;' style='display: {displayStr};position: absolute;z-index: 5;'/>" +
                    $"<div class='news02p1' style='margin: 0px;border-width: 0px;padding: 0px;' ><p>" +
                    $"<img id='thumbnail_Image{idStr}' src='{thumbnailPathStr}' style='border-width: 0px;position: absolute;z-index: 1;' width='161px' height='121px' />" +
                    $"</p></div></li><li><span>{dateTitleStr}</span><br />" +
                    $"<a href='newsView.aspx?id={guidStr}'>{headlineStr} </a></li><br />" +
                    $"<li>{summaryStr} </li></ul></div></li>");
            }
            yachtHelper.CloseConnection();

            //渲染新聞列表
            newsList.Text = newListHtml.ToString();
        }
    }
}