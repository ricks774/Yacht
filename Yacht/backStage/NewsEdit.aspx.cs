using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using CKFinder;

namespace Yacht.backStage
{
    public partial class NewEdit : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();
        private FileHepler fileHepler = new FileHepler();

        protected void Page_Load(object sender, EventArgs e)
        {
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKED);

            if (!IsPostBack)
            {
                Calendar1.SelectedDate = Calendar1.TodaysDate;  // 預設選取當天日期
                LoadDayNewsHeadLine();
            }
        }

        // ! 讀取新聞資料
        private void LoadDayNewsHeadLine()
        {
            // 依照選取日期取得資料庫新聞的資料
            string sql = "select * from News where DateTitle = @DateTitle order by Id Asc";
            SqlParameter[] parameters =
            {
                new SqlParameter("@DateTitle", SqlDbType.Date) { Value = Calendar1.SelectedDate.ToString("yyyy-M-dd") }
            };
            var reader = yachtHelper.SearchData(sql, parameters);
            while (reader.Read())
            {
                string headLineStr = reader["HeadLine"].ToString();
                string isTopStr = reader["IsTop"].ToString();
                string headListId = reader["Id"].ToString();

                // 渲染畫面
                LabIsTop.Visible = false;
                if (isTopStr.Equals("True"))
                {
                    LabIsTop.Visible = true;
                }
                ListItem listItem = new ListItem();
                listItem.Text = headLineStr;
                listItem.Value = headListId;
                headlineRadioBtnList.Items.Add(listItem);
            }
            yachtHelper.CloseConnection();

            // 預設選取新增新聞項目
            int RadioBtnCount = headlineRadioBtnList.Items.Count;
            if (RadioBtnCount > 0)
            {
                headlineRadioBtnList.Items[RadioBtnCount - 1].Selected = true;
                deleteNewsBtn.Visible = true;
                string newsId = headlineRadioBtnList.SelectedValue;
                LoadNewsCover(newsId);
                LoadSummary();
                LoadNewSContent(newsId);
            }
        }

        // ! 讀取新聞縮圖
        private void LoadNewsCover(string newsId)
        {
            Show_Cover.Visible = false;
            if (headlineRadioBtnList.SelectedIndex != -1)
            {
                string sql = "select * from News where Id = @Id";
                SqlParameter[] parameters =
                {
                new SqlParameter("@Id", SqlDbType.Int) { Value = newsId },
            };
                var reader = yachtHelper.SearchData(sql, parameters);
                if (reader.Read())
                {
                    string coverPath = reader["ThumbnailPath"].ToString();
                    // 判斷是否有縮圖路徑，有才顯示 沒有就隱藏
                    if (coverPath != "")
                    {
                        Show_Cover.Visible = true;
                        Show_Cover.Text = $"<img src='{coverPath}' style='border-width:0px;' Width='210px' />";
                    }
                    else { Show_Cover.Visible = false; }
                }
                yachtHelper.CloseConnection();
            }
        }

        // ! 刪除新聞按鈕事件
        protected void DeleteNewsBtn_Click(object sender, EventArgs e)
        {
            // 隱藏刪除按鈕
            deleteNewsBtn.Visible = false;

            // 取得選取項目內容
            string selHeadLineStr = headlineRadioBtnList.SelectedItem.Text;
            // 取得日曆選取的日期
            string selNewsDate = Calendar1.SelectedDate.ToString("yyyy-M-dd");

            string sql = "delete from News where DateTitle = @SelNewsDate AND HeadLine = @SelHeadLineStr";
            SqlParameter[] parameters =
            {
                new SqlParameter("@SelNewsDate", SqlDbType.Date) { Value = selNewsDate },
                new SqlParameter("@SelHeadLineStr", SqlDbType.NVarChar) { Value = selHeadLineStr },
            };
            yachtHelper.ExecuteSQL(sql, parameters);

            // 渲染畫面
            headlineRadioBtnList.Items.Clear();
            LoadDayNewsHeadLine();
        }

        // ! 選擇新聞時的事件
        protected void HeadlineRadioBtnList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 依照日期的新聞標題選取項目判斷是不是焦點新聞
            string sql = "select IsTop from News where DateTitle = @DateTitle AND HeadLine = @HeadLine";
            SqlParameter[] parameters =
            {
                new SqlParameter("@DateTitle", SqlDbType.Date) { Value = Calendar1.SelectedDate.ToString("yyyy-M-dd") },
                new SqlParameter("@HeadLine", SqlDbType.NVarChar) { Value = headlineRadioBtnList.SelectedItem.Text},
            };
            var reader = yachtHelper.SearchData(sql, parameters);
            if (reader.Read())
            {
                string isTopStr = reader["IsTop"].ToString();
                // 渲染畫面
                LabIsTop.Visible = false;
                if (isTopStr.Equals("True")) { LabIsTop.Visible = true; }
            }
            yachtHelper.CloseConnection();
            LoadNewsCover(headlineRadioBtnList.SelectedValue);
            LoadSummary();
            LoadNewSContent(headlineRadioBtnList.SelectedValue);
        }

        // ! 日曆日期選取改變時的事件
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            deleteNewsBtn.Visible = false;
            LabIsTop.Visible = false;
            headlineRadioBtnList.Items.Clear();
            LoadDayNewsHeadLine();
            LoadNewsCover(headlineRadioBtnList.SelectedValue);
            LoadSummary();
            LoadNewSContent(headlineRadioBtnList.SelectedValue);
        }

        // ! 日曆日期渲染畫面時的事件
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            // 取得新聞日期
            string sql = "select DateTitle from News";
            var reader = yachtHelper.SearchData(sql);
            while (reader.Read())
            {
                // 轉換成 DateTime
                DateTime newsTime = DateTime.Parse(reader["DateTitle"].ToString());
                // 顯示出有新聞的日期
                if (e.Day.Date.Date == newsTime && e.Day.Date.Date != Calendar1.SelectedDate)
                {
                    // 渲染畫面
                    e.Cell.Font.Underline = true;   // 字型底線
                    e.Cell.Font.Bold = true;    // 字型粗體
                    e.Cell.ForeColor = Color.DodgerBlue;    // 顏色
                }
            }
            yachtHelper.CloseConnection();
        }

        // ! 新增新聞事件
        protected void AdHeadlineBtn_Click(object sender, EventArgs e)
        {
            // 產生GUID隨機碼 + 時間兩位秒數(加強避免重複)
            DateTime nowTime = DateTime.Now;
            string nowSec = nowTime.ToString("ff");
            string guid = Guid.NewGuid().ToString().Trim() + nowSec;

            // 取得日曆選取的日期
            string selNewsDate = Calendar1.SelectedDate.ToString("yyyy-M-dd");
            // 取得是否勾選
            string isTop = CBoxIsTop.Checked.ToString();    // 得到 True 或 False

            // 將資料存到資料庫
            string sql = "insert into News (DateTitle, HeadLine, Guid, Istop) values (@SetNewsDate, @HeadLine, @Guid, @IsTop)";
            SqlParameter[] parameters =
                {
                    new SqlParameter("@SetNewsDate", SqlDbType.Date) { Value = selNewsDate},
                    new SqlParameter("@HeadLine", SqlDbType.NVarChar) { Value = headlineTbox.Text},
                    new SqlParameter("@Guid", SqlDbType.NVarChar) { Value = guid},
                    new SqlParameter("@IsTop", SqlDbType.Bit) { Value = isTop}
                };
            yachtHelper.ExecuteSQL(sql, parameters);

            // 渲染畫面
            headlineRadioBtnList.Items.Clear();
            LoadDayNewsHeadLine();

            // 清空輸入欄位
            headlineTbox.Text = "";
        }

        // ! <========================== 新聞縮圖開始 ==========================>
        // ! 上傳新聞縮圖事件
        protected void Btn_CoverUpload_Click(object sender, EventArgs e)
        {
            // 判斷是否有選擇新聞
            if (headlineRadioBtnList.SelectedIndex != -1)
            {
                int headListId = Convert.ToInt32(headlineRadioBtnList.SelectedValue);
                string headTitle = headlineRadioBtnList.SelectedItem.Text;
                UploadCover(headListId, headTitle);
            }
            else
            {
                Response.Write($"<script>alert('沒有選擇新聞!')</script>");
            }
        }

        // ! 上傳新聞縮圖事件
        private void UploadCover(int Id, string headTitle)
        {
            //設定存檔路徑
            string savePath = Server.MapPath("/images/News/");

            // 判斷是否有檔案
            if (Fi_coverUpload.HasFile)
            {
                //儲存圖片檔案及圖片名稱
                string fileName = Fi_coverUpload.FileName;
                string extension = Path.GetExtension(fileName).ToLowerInvariant();  // 取得副檔名
                // === 自訂定義檔案名稱 ===
                fileName = $"{Id}_{headTitle}{extension}";
                string path = $"/images/News/{fileName}";
                string sql = $"update News set ThumbnailPath = @UpdateImgPath where Id = @UpdateImgId";
                SqlParameter[] parameters =
                {
                new SqlParameter("@UpdateImgPath", SqlDbType.NVarChar) { Value = path },
                new SqlParameter("@UpdateImgId", SqlDbType.Int) { Value = Id }
                };
                string result = fileHepler.FileUpload(Fi_coverUpload, sql, parameters, Id, headTitle, savePath);
                if (result == "OK") { Response.Write($"<script>alert('成功新增')</script>"); }
                else { Response.Write($"<script>alert('新增失敗')</script>"); }
            }
            LoadNewsCover(headlineRadioBtnList.SelectedValue);
        }

        // ! <========================== 新聞縮圖結束 ==========================>

        // ! <========================== 新聞概括開始 ==========================>
        // ! 讀取新聞概括事件
        private void LoadSummary()
        {
            if (headlineRadioBtnList.SelectedIndex != -1)
            {
                // 取得新聞標題
                string selHeadLineStr = headlineRadioBtnList.SelectedItem.Text;
                // 取得新聞日期
                string selNewsDate = Calendar1.SelectedDate.ToString("yyyy-M-dd");
                string sql = "select Summary from News where DateTitle = @SelNewsDate AND HeadLine = @SelHeadLineStr";
                SqlParameter[] parameters =
                {
                new SqlParameter("@SelNewsDate", SqlDbType.Date) { Value = selNewsDate },
                new SqlParameter("@SelHeadLineStr", SqlDbType.NVarChar) { Value = selHeadLineStr },
            };
                var reader = yachtHelper.SearchData(sql, parameters);
                if (reader.Read()) { Summary_tbx.Text = HttpUtility.HtmlDecode(reader["Summary"].ToString()); }
                else { Summary_tbx.Text = ""; }
                yachtHelper.CloseConnection();

                // 渲染畫面
                lb_UploadSummary.Visible = false;
            }
        }

        // ! 更新概括事件
        protected void Btn_SummaryUpdate_Click(object sender, EventArgs e)
        {
            if (headlineRadioBtnList.SelectedIndex != -1)
            {
                // 取得新聞標題
                string selHeadLineStr = headlineRadioBtnList.SelectedItem.Text;
                // 取得新聞日期
                string selNewsDate = Calendar1.SelectedDate.ToString("yyyy-M-dd");
                // 使用html編碼寫入
                string newsSummaryHtmlStr = HttpUtility.HtmlEncode(Summary_tbx.Text);

                string sql = "update News set Summary = @NewSummary where DateTitle = @SelNewsDate AND HeadLine = @SelHeadLineStr";
                SqlParameter[] parameters =
                {
                new SqlParameter("@SelNewsDate", SqlDbType.Date) { Value = selNewsDate },
                new SqlParameter("@SelHeadLineStr", SqlDbType.NVarChar) { Value = selHeadLineStr },
                new SqlParameter("@NewSummary", SqlDbType.NVarChar) { Value = newsSummaryHtmlStr }
                };
                yachtHelper.ExecuteSQL(sql, parameters);

                // 渲染畫面
                DateTime nowTime = DateTime.Now;
                lb_UploadSummary.Visible = true;
                lb_UploadSummary.Text = $"*成功更新! - {nowTime.ToString("g")}";
            }
            else
            {
                Response.Write($"<script>alert('沒有選擇新聞!')</script>");
            }
        }

        // ! <========================== 新聞概括結束 ==========================>

        // ! <========================== 新聞內容開始 ==========================>
        // ! 讀取新聞內容
        private void LoadNewSContent(string newsId)
        {
            // 取得新聞標題
            //string selHeadLineStr = headlineRadioBtnList.SelectedItem.Text;
            //// 取得新聞日期
            //string selNewsDate = Calendar1.SelectedDate.ToString("yyyy-M-dd");
            if (headlineRadioBtnList.SelectedIndex != -1)
            {
                string sql = "select NewsContentHtml from News where id = @NewsId";
                SqlParameter[] parameters =
                {
                new SqlParameter("@NewsId", SqlDbType.Int) { Value = newsId }
            };
                var reader = yachtHelper.SearchData(sql, parameters);
                if (reader.Read())
                {
                    CKED.Text = HttpUtility.HtmlDecode(reader["NewsContentHtml"].ToString());
                }
                yachtHelper.CloseConnection();
            }
        }

        // ! 更新新聞事件
        protected void Btn_UpdateNewContent_Click(object sender, EventArgs e)
        {
            //取得 CKEditorControl 的 HTML 內容
            string newsContentHtmlStr = HttpUtility.HtmlEncode(CKED.Text);
            // 判斷目前是否有選擇新聞列表
            if (headlineRadioBtnList.SelectedIndex != -1)
            {
                // 更新 CKED 的內容
                string sql = "UPDATE News SET NewsContentHtml = @NewsContentHtml where Id = @NewsId";
                SqlParameter[] parameters =
                {
                new SqlParameter("@NewsContentHtml", SqlDbType.NVarChar) { Value = newsContentHtmlStr },
                new SqlParameter("@NewsId", SqlDbType.Int) { Value = headlineRadioBtnList.SelectedValue },
                };
                yachtHelper.ExecuteSQL(sql, parameters);

                //渲染畫面提示
                DateTime nowtime = DateTime.Now;
                lb_UpdateNewContent.Visible = true;
                lb_UpdateNewContent.Text = "*成功更新! - " + nowtime.ToString("g");
            }
            else
            {
                Response.Write($"<script>alert('沒有選擇新聞!')</script>");
            }
        }

        // ! <========================== 新聞內容結束 ==========================>
    }
}