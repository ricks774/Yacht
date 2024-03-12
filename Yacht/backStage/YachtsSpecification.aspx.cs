using CKFinder;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yacht.backStage
{
    public partial class YachtsSpecification : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();

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
                    LoadDetail();
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

        #region "LoadDetail() 讀取遊艇規格"

        private void LoadDetail()
        {
            string getId = Request.QueryString["id"].ToString();
            string sql = "select ys.Id, ys.VideoPath, ys.DetailSpecification from YachtsManager ym left join Yachts ys on ym.id = ys.YachtsId where ys.Id = @YachtsId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@YachtsId", SqlDbType.Int) { Value = getId }
            };
            SqlDataReader reader = yachtHelper.SearchData(sql, parameters);
            if (reader.Read())
            {
                CKED.Text = HttpUtility.HtmlDecode(reader["DetailSpecification"].ToString());
                Lb_OverViewDimensions.Visible = true;
                CKED.Visible = true;
                Btn_UpdateDetail.Visible = true;
            }
            yachtHelper.CloseConnection();
        }

        #endregion "LoadDetail() 讀取遊艇規格"

        #region "更新詳細規格"

        protected void Btn_UpdateDetail_Click(object sender, EventArgs e)
        {
            string getId = Request.QueryString["id"].ToString();
            string contentsHtml = CKED.Text;
            string sql = "update Yachts set DetailSpecification = @UpdateDetail where Id = @UpdateId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@UpdateDetail", SqlDbType.NVarChar) { Value = contentsHtml},
                new SqlParameter("@UpdateId", SqlDbType.Int) { Value = getId }
            };

            yachtHelper.ExecuteSQL(sql, parameters);
            Response.Redirect($"~/backStage/YachtsSpecification.aspx?id={getId}");
        }

        #endregion "更新詳細規格"
    }
}