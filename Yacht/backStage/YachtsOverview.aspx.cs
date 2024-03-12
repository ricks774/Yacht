using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CKFinder;

namespace Yacht.backStage
{
    public partial class YachtsEdit : System.Web.UI.Page
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
                LoadYachtsModelList();
            }

            string getId = Request.QueryString["Id"];
            if (getId != null)
            {
                if (!IsPostBack)
                {
                    ShowYachtsDetail();
                    LoadOverViewContents();
                    LoadOverViewDimensions();
                    LoadDimensionsFile();
                };
            }
        }

        #region "LoadYachtsModelList() 讀取遊艇型號清單"

        private void LoadYachtsModelList()
        {
            string sql = "select CONCAT(ym.YachtModelName,' ', ym.YachtModelNo) as YachtModel, ym.Id, ys.Id as YachtsId  from YachtsManager ym left join Yachts ys on ym.id = ys.YachtsId";
            GV_Yachts.DataSource = yachtHelper.SearchData(sql);
            GV_Yachts.DataBind();
            yachtHelper.CloseConnection();
        }

        #endregion "LoadYachtsModelList() 讀取遊艇型號清單"

        #region "LoadOverViewContents() 讀取遊艇內容"

        private void LoadOverViewContents()
        {
            string getId = Request.QueryString["id"].ToString();
            string sql = "select OverViewContents from YachtsManager ym left join Yachts ys on ym.id = ys.YachtsId where ys.Id = @YachtsId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtsId", SqlDbType.Int) { Value = getId }
            };
            SqlDataReader reader = yachtHelper.SearchData(sql, parameters);
            if (reader.Read())
            {
                Tbox_OverViewContent.Text = HttpUtility.HtmlDecode(reader["OverViewContents"].ToString());
            }
            yachtHelper.CloseConnection();
        }

        #endregion "LoadOverViewContents() 讀取遊艇內容"

        #region "LoadOverViewDimensions() 讀取遊艇規格"

        private void LoadOverViewDimensions()
        {
            string getId = Request.QueryString["id"].ToString();
            string sql = "select OverViewDimensions from YachtsManager ym left join Yachts ys on ym.id = ys.YachtsId where ys.Id = @YachtsId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtsId", SqlDbType.Int) { Value = getId }
            };
            SqlDataReader reader = yachtHelper.SearchData(sql, parameters);
            if (reader.Read())
            {
                CKED.Text = HttpUtility.HtmlDecode(reader["OverViewDimensions"].ToString());
            }
            yachtHelper.CloseConnection();
        }

        #endregion "LoadOverViewDimensions() 讀取遊艇規格"

        #region "LoadDimensionsFile() 讀取遊艇規格檔案"

        private void LoadDimensionsFile()
        {
            string getId = Request.QueryString["id"].ToString();
            string sql = "SELECT FilePath, RIGHT(FilePath, CHARINDEX('/', REVERSE(FilePath)) - 1) AS FileName FROM Yachts WHERE Id = @YachtsId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtsId", SqlDbType.Int) { Value = getId }
            };
            SqlDataReader reader = yachtHelper.SearchData(sql, parameters);
            if (reader.Read())
            {
                string filePath = reader["FilePath"].ToString();
                string fileName = reader["FileName"].ToString();

                // 設定檔案連結的屬性
                Lr_FileName.NavigateUrl = filePath;
                Lr_FileName.Text = fileName;

                // 設定下載時的檔案名稱
                Lr_FileName.Attributes["download"] = fileName;
            }
            yachtHelper.CloseConnection();
        }

        #endregion "LoadDimensionsFile() 讀取遊艇規格檔案"

        #region "ShowYachtsDetail() 顯示遊艇型號的規格"

        private void ShowYachtsDetail()
        {
            string getId = Request.QueryString["Id"];
            string sql = "select ys.Id, ym.YachtModelName, ym.YachtModelNo, ys.HullLength, ys.LWL, ys.BMAX, ys.StandardDraft, ys.Ballast, ys.Displacement, ys.SailArea, ys.Cutter, ys.DimensionsImgPath, ys.DimensionsTitle, ys.FilePath, ys.VideoPath from YachtsManager ym left join Yachts ys on ym.id = ys.YachtsId where ys.Id = @YachtsManagerId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtsManagerId", SqlDbType.Int) { Value = getId }
            };
            SqlDataReader reader = yachtHelper.SearchData(sql, parameters);
            if (reader.Read())
            {
                CKED.Visible = true;
                Btn_UpdateYachtsDimensions.Visible = true;
                Tbox_OverViewContent.Visible = true;
                Btn_UpdateYachtsContents.Visible = true;
                Lb_UploadTitle.Visible = true;
                Lb_OverViewContents.Visible = true;
                Lb_OverViewDimensions.Visible = true;
                Fi_UploadFile.Visible = true;
                Btn_UploadFile.Visible = true;
                Session["YachtFileName"] = reader["YachtModelName"].ToString() + reader["YachtModelNo"].ToString();
            }
            yachtHelper.CloseConnection();
        }

        #endregion "ShowYachtsDetail() 顯示遊艇型號的規格"

        #region "更新遊艇內容"

        protected void Btn_UpdateYachtsContents_Click(object sender, EventArgs e)
        {
            string getId = Request.QueryString["id"].ToString();
            string contentsHtml = HttpUtility.HtmlEncode(Tbox_OverViewContent.Text);
            string sql = "update Yachts set OverViewContents = @UpdateContents where Id = @UpdateId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UpdateContents", SqlDbType.NVarChar) { Value = contentsHtml},
                new SqlParameter("@UpdateId", SqlDbType.Int) { Value = getId }
            };

            yachtHelper.ExecuteSQL(sql, parameters);
            Response.Redirect($"~/backStage/YachtsOverview.aspx?id={getId}");
        }

        #endregion "更新遊艇內容"

        #region "更新遊艇規格"

        protected void Btn_UpdateYachtsDimensions_Click(object sender, EventArgs e)
        {
            string getId = Request.QueryString["id"].ToString();
            string contentsHtml = HttpUtility.HtmlEncode(CKED.Text);
            string sql = "update Yachts set OverViewDimensions = @UpdateDimensions where Id = @UpdateId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UpdateDimensions", SqlDbType.NVarChar) { Value = contentsHtml},
                new SqlParameter("@UpdateId", SqlDbType.Int) { Value = getId }
            };

            yachtHelper.ExecuteSQL(sql, parameters);
            Response.Redirect($"~/backStage/YachtsOverview.aspx?id={getId}");
        }

        #endregion "更新遊艇規格"

        #region "上傳型錄檔案"

        protected void Btn_UploadFile_Click(object sender, EventArgs e)
        {
            if (Fi_UploadFile.HasFile)
            {
                int getId = int.Parse(Request.QueryString["id"]);    // 取得網址的Id
                string savePath = Server.MapPath("~/File/");    // 設定檔案實際儲存的路徑 (絕對路徑)

                string fileName = Fi_UploadFile.FileName;   // 取得檔案名稱
                string extension = Path.GetExtension(fileName).ToLowerInvariant();  // 取得副檔名

                string getYachtName = Session["YachtFileName"].ToString();
                string saveFileName = $"{getId}_{getYachtName}{extension}";   //  自訂定義檔案名稱
                string sqlPath = $"/File/{saveFileName}";  // 儲存到資料庫的路徑 (相對路徑)

                string sql = $"update Yachts set FilePath = @UploadFilePath where Id = @UploadId";
                SqlParameter[] parameters =
                {
                new SqlParameter("@UploadFilePath", SqlDbType.NVarChar) { Value = sqlPath },
                new SqlParameter("@UploadId", SqlDbType.Int) { Value = getId }
                };
                string result = fileHepler.FileUpload(Fi_UploadFile, sql, parameters, getId, getYachtName, savePath);
                if (result == "OK") { Response.Write($"<script>alert('成功新增')</script>"); }
                else { Response.Write($"<script>alert('新增失敗')</script>"); }
            }
        }

        #endregion "上傳型錄檔案"
    }
}