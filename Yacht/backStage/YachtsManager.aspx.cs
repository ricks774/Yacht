using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Yacht.backStage
{
    public partial class Yahts_Edit : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();
        private FileHepler fileHepler = new FileHepler();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadYachtsList();
                LoadYachtsModel();
                LoadYachtsBanner();
            }
        }

        // ! 下拉選單讀取遊艇的型號
        private void LoadYachtsList()
        {
            string sql = "select  CONCAT(YachtModelName, ' ', YachtModelNo) AS YachtModel , id from YachtsManager";
            Dl_bannerList.DataSource = yachtHelper.SearchData(sql);
            Dl_bannerList.DataBind();
            yachtHelper.CloseConnection();
        }

        // ! 讀取遊艇型號清單
        private void LoadYachtsModel()
        {
            string sql = "select * from YachtsManager";
            GV_all.DataSource = yachtHelper.SearchData(sql);
            GV_all.DataBind();
            yachtHelper.CloseConnection();
        }

        // ! 讀取遊艇型號縮圖
        private void LoadYachtsBanner()
        {
            if (Dl_bannerList.SelectedIndex != -1)
            {
                string sql = "select BannerImgPathJSON from YachtsManager where Id = @YachtsModelId";
                SqlParameter[] parameters =
                {
                new SqlParameter("@YachtsModelId", SqlDbType.Int) { Value = Dl_bannerList.SelectedValue }
            };
                var reader = yachtHelper.SearchData(sql, parameters);
                if (reader.Read())
                {
                    YachtsImg.ImageUrl = reader["BannerImgPathJSON"].ToString();
                }
            }
        }

        // ! 新增遊艇型號
        protected void Btn_AddYachtModel_Click(object sender, EventArgs e)
        {
            // 插入空格區隔文字跟數字 (頁面細項標題會用到)
            //string yachtModelStr = TBox_AddYachtModel.Text + " " + TBox_AddYachtLength.Text;
            // 產生 GUID 隨機碼 + 時間2位秒數 (加強避免重複)
            DateTime nowTime = DateTime.Now;
            string nowSec = nowTime.ToString("ff");
            string guidStr = Guid.NewGuid().ToString().Trim() + nowSec;
            // 取得勾選項目
            string isNewDesign = CBox_NewDesign.Checked.ToString();
            string isNewBuilding = CBox_NewBuilding.Checked.ToString();

            string sql = "insert into YachtsManager( YachtModelName, IsNewDesign, IsNewBuilding, Guid, YachtModelNo)" +
                "VALUES (@YachtModelName, @IsNewDesign, @IsNewBuilding, @Guid, @YachtModelNo)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtModelName", SqlDbType.NVarChar) { Value = TBox_AddYachtModel.Text },
                new SqlParameter("@IsNewDesign", SqlDbType.Bit) { Value = isNewDesign },
                new SqlParameter("@IsNewBuilding", SqlDbType.Bit) { Value = isNewBuilding },
                new SqlParameter("@Guid", SqlDbType.NVarChar) { Value = guidStr },
                new SqlParameter("@YachtModelNo", SqlDbType.NVarChar) { Value = TBox_AddYachtLength.Text }
            };
            yachtHelper.ExecuteSQL(sql, parameters);

            //畫面渲染
            TBox_AddYachtModel.Text = "";
            TBox_AddYachtLength.Text = "";
            CBox_NewDesign.Checked = false;
            CBox_NewBuilding.Checked = false;
            //Dl_bannerList.SelectedItem.Text = yachtModelStr; //設定下拉選單選取項為新增項
            Response.Redirect("~/backStage/YachtsManager.aspx");
        }

        // ! 遊艇型號下拉清單變更時的事件
        protected void Dl_bannerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadYachtsBanner();
        }

        protected void Btn_BannerImgUpload_Click(object sender, EventArgs e)
        {
            if (BannerImg_FileUpload.HasFile)
            {
                string fileName = BannerImg_FileUpload.FileName;  // 取得上傳檔案的檔名
                string extension = Path.GetExtension(fileName).ToLowerInvariant();  // 取得副檔名
                int yachtsModelId = Convert.ToInt32(Dl_bannerList.SelectedValue); // 取得選取的遊艇型號Id
                string yachtsModelName = Dl_bannerList.SelectedItem.Text; // 取得選取的遊艇型號名稱
                // === 自訂定義檔案名稱 ===
                fileName = $"{yachtsModelId}_{yachtsModelName}{extension}";

                //設定存檔路徑
                string savePath = Server.MapPath("/images/Yachts/Banner/");
                string sqlPath = $"/images/Yachts/Banner/{fileName}";
                string sql = $"update YachtsManager set BannerImgPathJSON = @UpdateYachtsImgPath where Id = @UpdateYachtsImgId";
                SqlParameter[] parameters =
                {
                new SqlParameter("@UpdateYachtsImgPath", SqlDbType.NVarChar) { Value = sqlPath },
                new SqlParameter("@UpdateYachtsImgId", SqlDbType.Int) { Value = yachtsModelId}
                };
                string resilt = fileHepler.FileUpload(BannerImg_FileUpload, sql, parameters, yachtsModelId, yachtsModelName, savePath);
                Response.Write($"<script>alert('{resilt}')</script>");
            }
            LoadYachtsBanner();
        }

        #region "<============================== 遊艇型號編輯事件開始 ==============================>"

        protected void GV_all_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_all.EditIndex = e.NewEditIndex;
            LoadYachtsList();
            LoadYachtsModel();
            LoadYachtsBanner();
        }

        protected void GV_all_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_all.EditIndex = -1;
            LoadYachtsList();
            LoadYachtsModel();
            LoadYachtsBanner();
        }

        protected void GV_all_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int bindId = Convert.ToInt32(GV_all.DataKeys[e.RowIndex].Value); // 取得資料表的 ID
            GridViewRow row = GV_all.Rows[e.RowIndex];  // 取的正在更新的索引
            // 取得目前編輯列的控制項
            string newModelName = (row.FindControl("Ed_YachtsModelName") as TextBox)?.Text;
            string newModelNo = (row.FindControl("Ed_YachtsModelNo") as TextBox)?.Text;
            string sql = "update YachtsManager SET YachtModelName = @UpdateYachtModelName, YachtModelNo = @UpdateYachtModelNo WHERE Id = @UpdateId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UpdateYachtModelName", SqlDbType.NVarChar) { Value = newModelName },
                new SqlParameter("@UpdateYachtModelNo", SqlDbType.NVarChar) { Value = newModelNo },
                new SqlParameter("@UpdateId", SqlDbType.Int) { Value = bindId }
            };
            yachtHelper.ExecuteSQL(sql, parameters);

            GV_all.EditIndex = -1;
            LoadYachtsList();
            LoadYachtsModel();
            LoadYachtsBanner();
        }

        protected void GV_all_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bindId = Convert.ToInt32(GV_all.DataKeys[e.RowIndex].Value); // 取得資料表的 ID
            string delUser = $"delete YachtsManager where Id = @YachtsModelId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtsModelId", SqlDbType.Int) { Value = bindId }
            };
            int result = yachtHelper.ExecuteSQL(delUser, parameters);
            if (result > 0) Response.Write("<script>alert('成功刪除')</script>");
            else Response.Write("<script>alert('刪除失敗')</script>");

            GV_all.EditIndex = -1;
            LoadYachtsList();
            LoadYachtsModel();
            LoadYachtsBanner();
        }

        #endregion "<============================== 遊艇型號編輯事件開始 ==============================>"
    }
}