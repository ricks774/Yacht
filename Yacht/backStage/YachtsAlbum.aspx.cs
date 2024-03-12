using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yacht.backStage
{
    public partial class YachtsAlbum : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();
        private FileHepler fileHepler = new FileHepler();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadYachtsModelList();
            }
            string getId = Request.QueryString["Id"];
            if (getId != null)
            {
                if (!IsPostBack)
                {
                    Fi_UploadLayout.Visible = true;
                    Btn_UploadLayout.Visible = true;
                    Fi_UploadImg.Visible = true;
                    Btn_UploadImg.Visible = true;
                    GetGuid();
                    LoadYachtsLayout();
                    LoadYachtsImg();
                }
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

        #region "GetGuid() 取得網址傳值的對應型號Guid"

        private void GetGuid()
        {
            string getId = Request.QueryString["id"];
            string sql = "select id, Guid from YachtsManager where Id = @YachtId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtId", SqlDbType.Int) { Value = getId }
            };
            SqlDataReader reader = yachtHelper.SearchData(sql, parameters);
            if (reader.Read())
            {
                Session["Guid"] = reader["Guid"];
            }
            yachtHelper.CloseConnection();
        }

        #endregion "GetGuid() 取得網址傳值的對應型號Guid"

        #region "LoadYachtsLayout() 讀取layout圖片"

        private void LoadYachtsLayout()
        {
            string getId = Request.QueryString["id"].ToString();
            string sql = "select * from YachtsAlbum where YachtId = @YachtId and IsLayout = 1";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtId", SqlDbType.Int) { Value = getId }
            };
            SqlDataReader reader = yachtHelper.SearchData(sql, parameters);
            GV_Layout.DataSource = reader;
            GV_Layout.DataBind();
            yachtHelper.CloseConnection();
        }

        #endregion "LoadYachtsLayout() 讀取layout圖片"

        #region "LoadYachtsImg() 讀取遊艇圖片"

        private void LoadYachtsImg()
        {
            string getId = Request.QueryString["id"].ToString();
            string sql = "select * from YachtsAlbum where YachtId = @YachtId and IsLayout = 0";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtId", SqlDbType.Int) { Value = getId }
            };
            GV_Img.DataSource = yachtHelper.SearchData(sql, parameters);
            GV_Img.DataBind();
            yachtHelper.CloseConnection();
        }

        #endregion "LoadYachtsImg() 讀取遊艇圖片"

        #region "上傳遊艇設計圖"

        protected void Btn_UploadLayout_Click(object sender, EventArgs e)
        {
            string result = "";
            if (Fi_UploadLayout.HasFile)
            {
                string getId = Request.QueryString["id"];
                for (int i = 0; i < Fi_UploadLayout.PostedFiles.Count; i++)
                {
                    string fileName = Fi_UploadLayout.PostedFiles[i].FileName;   // 取得檔案名稱

                    string sql = $"insert into YachtsAlbum (YachtId, IsLayout) values (@YachtId{i}, @IsLayout{i}) SELECT SCOPE_IDENTITY()";
                    SqlParameter[] parameters =
                    {
                        new SqlParameter($"@YachtId{i}", SqlDbType.Int) { Value = getId },
                        new SqlParameter($"@IsLayout{i}", SqlDbType.Bit) { Value = 1 },
                    };
                    int getNewId = yachtHelper.ExecuteSQLId(sql, parameters);

                    // 更新圖檔路徑並儲存檔案
                    string saveSQLName = $"{getNewId}_{getId}_{fileName}";   //  儲存到資料庫的檔案名稱
                    string sqlPath = $"/images/Yachts/Layout/{saveSQLName}";  // 儲存到資料庫的路徑 (相對路徑)

                    string filePath = Server.MapPath("~/images/Yachts/Layout/");    // 設定檔案實際儲存的路徑 (絕對路徑)
                    string saveFileName = $"{getId}_{Path.GetFileNameWithoutExtension(fileName)}";  // 實際儲存的檔案路徑
                    string getGuid = Session["Guid"].ToString();

                    string updatePathSQL = $"update YachtsAlbum set ImgPath = @ImgPath, YachtModelGuid = @YachtModelGuid where Id = @UploadId";
                    SqlParameter[] updatePathParameters =
                    {
                        new SqlParameter("@ImgPath", SqlDbType.NVarChar) { Value = sqlPath },
                        new SqlParameter("@YachtModelGuid", SqlDbType.NVarChar) { Value = getGuid },
                        new SqlParameter("@UploadId", SqlDbType.Int) { Value = getNewId }
                    };
                    result = fileHepler.FileUploadMultiple(Fi_UploadLayout.PostedFiles[i], updatePathSQL, updatePathParameters, getNewId, saveFileName, filePath);
                }
            }
            if (result == "OK") { Response.Write($"<script>alert('成功新增')</script>"); }
            else { Response.Write($"<script>alert('新增失敗')</script>"); }
            LoadYachtsLayout();
        }

        #endregion "上傳遊艇設計圖"

        #region "上傳遊艇照片"

        protected void Btn_UploadImg_Click(object sender, EventArgs e)
        {
            string result = "";
            if (Fi_UploadImg.HasFile)
            {
                string getId = Request.QueryString["id"];
                for (int i = 0; i < Fi_UploadImg.PostedFiles.Count; i++)
                {
                    string fileName = Fi_UploadImg.PostedFiles[i].FileName;   // 取得檔案名稱

                    string sql = $"insert into YachtsAlbum (YachtId) values (@YachtId{i}) SELECT SCOPE_IDENTITY()";
                    SqlParameter[] parameters =
                    {
                        new SqlParameter($"@YachtId{i}", SqlDbType.Int) { Value = getId },
                    };
                    int getNewId = yachtHelper.ExecuteSQLId(sql, parameters);

                    // 更新圖檔路徑並儲存檔案
                    string saveSQLName = $"{getNewId}_{getId}_{fileName}";   //  儲存到資料庫的檔案名稱
                    string sqlPath = $"/images/Yachts/Image/{saveSQLName}";  // 儲存到資料庫的路徑 (相對路徑)

                    string filePath = Server.MapPath("~/images/Yachts/Image/");    // 設定檔案實際儲存的路徑 (絕對路徑)
                    string saveFileName = $"{getId}_{Path.GetFileNameWithoutExtension(fileName)}";  // 實際儲存的檔案路徑
                    string getGuid = Session["Guid"].ToString();

                    string updatePathSQL = $"update YachtsAlbum set ImgPath = @ImgPath, YachtModelGuid = @YachtModelGuid where Id = @UploadId";
                    SqlParameter[] updatePathParameters =
                    {
                        new SqlParameter("@ImgPath", SqlDbType.NVarChar) { Value = sqlPath },
                        new SqlParameter("@YachtModelGuid", SqlDbType.NVarChar) { Value = getGuid },
                        new SqlParameter("@UploadId", SqlDbType.Int) { Value = getNewId }
                    };
                    result = fileHepler.FileUploadMultiple(Fi_UploadImg.PostedFiles[i], updatePathSQL, updatePathParameters, getNewId, saveFileName, filePath);
                }
            }
            if (result == "OK") { Response.Write($"<script>alert('成功新增')</script>"); }
            else { Response.Write($"<script>alert('新增失敗')</script>"); }
            LoadYachtsImg();
        }

        #endregion "上傳遊艇照片"

        #region "Layout的圖片刪除事件"

        protected void GV_Layout_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bindId = Convert.ToInt32(GV_Layout.DataKeys[e.RowIndex].Value); // 取得資料表的 ID
            string delLayoutSQL = $"delete YachtsAlbum where Id = @YachtsModelId and IsLayout = 1";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtsModelId", SqlDbType.Int) { Value = bindId }
            };
            int result = yachtHelper.ExecuteSQL(delLayoutSQL, parameters);
            if (result > 0) Response.Write("<script>alert('成功刪除')</script>");
            else Response.Write("<script>alert('刪除失敗')</script>");

            GV_Layout.EditIndex = -1;
            LoadYachtsLayout();
        }

        #endregion "Layout的圖片刪除事件"

        #region "Img的圖片刪除事件"

        protected void GV_Img_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bindId = Convert.ToInt32(GV_Img.DataKeys[e.RowIndex].Value); // 取得資料表的 ID
            string delLayoutSQL = $"delete YachtsAlbum where Id = @YachtsModelId and IsLayout = 0";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtsModelId", SqlDbType.Int) { Value = bindId }
            };
            int result = yachtHelper.ExecuteSQL(delLayoutSQL, parameters);
            if (result > 0) Response.Write("<script>alert('成功刪除')</script>");
            else Response.Write("<script>alert('刪除失敗')</script>");

            GV_Img.EditIndex = -1;
            LoadYachtsImg();
        }

        #endregion "Img的圖片刪除事件"
    }
}