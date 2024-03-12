using CKEditor.NET;
using CKFinder;
using NetVips;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using static Yacht.backStage.CompanyEdit;

namespace Yacht.backStage
{
    public partial class CompanyEdit : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKED);
            fileBrowser.SetupCKEditor(CKED_certif);
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            string sql = "select * from Company";
            var reader = yachtHelper.SearchData(sql);
            if (reader.Read())
            {
                CKED.Text = HttpUtility.HtmlDecode(reader["AboutUsHtml"].ToString());
                CKED_certif.Text = reader["CertificatContent"].ToString();
            }
            yachtHelper.CloseConnection();
        }

        // ! AboutUs的內容更新
        protected void UploadAboutUsBtn_Click(object sender, EventArgs e)
        {
            //取得 CKEditorControl 的 HTML 內容
            string aboutUsHtmlStr = HttpUtility.HtmlEncode(CKED.Text);
            //更新 About Us 頁面 HTML 資料
            string sql = "UPDATE Company SET AboutUsHtml = @aboutUsHtml WHERE id = 100";
            SqlParameter[] parameters =
            {
                new SqlParameter("@aboutUsHtml", SqlDbType.NVarChar) { Value = aboutUsHtmlStr }
            };
            yachtHelper.ExecuteSQL(sql, parameters);

            // 渲染更新畫面提示
            //DateTime nowtime = DateTime.Now;
            //UploadAboutUsLab.Visible = true;
            //UploadAboutUsLab.Text = "*成功更新! - " + nowtime.ToString("g");
            Response.Redirect("~/backStage/CompanyEdit.aspx");
        }

        protected void UploadCertificatBtn_Click(object sender, EventArgs e)
        {
            //取得 CKEditorControl 的 HTML 內容
            string certificatHtmlStr = HttpUtility.HtmlEncode(CKED_certif.Text);
            // 更新 Certificat 頁文字說明資料
            string sql = "UPDATE Company SET CertificatContent = @certificatContent WHERE id = 100";
            SqlParameter[] parameters =
{
                new SqlParameter("@certificatContent", SqlDbType.NVarChar) { Value = certificatHtmlStr }
            };
            yachtHelper.ExecuteSQL(sql, parameters);

            //渲染畫面提示
            //DateTime nowtime = DateTime.Now;
            //uploadCertificatLab.Visible = true;
            //uploadCertificatLab.Text = "*Upload Success! - " + nowtime.ToString("g");
            Response.Redirect("~/backStage/CompanyEdit.aspx");
        }
    }
}