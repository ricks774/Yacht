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

namespace Yacht.backStage
{
    public partial class CompanyEdit : System.Web.UI.Page
    {
        private List<ImageNameH> saveNameListH = new List<ImageNameH>();
        private YachtDBHelper yachtHelper = new YachtDBHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            FileBrowser fileBrowser = new FileBrowser();
            fileBrowser.BasePath = "/ckfinder";
            fileBrowser.SetupCKEditor(CKED);
            if (!IsPostBack)
            {
                //LoadImageHList();
            }
        }

        // ! JSON 資料 Horizontal Image
        public class ImageNameH
        {
            public string SaveName { get; set; }
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
            DateTime nowtime = DateTime.Now;
            UploadAboutUsLab.Visible = true;
            UploadAboutUsLab.Text = "*成功更新! - " + nowtime.ToString("g");
        }

        // ! 讀取照片
        //private void LoadImageHList()
        //{
        //    string sql = "select CertificatHorizontalImgJSON from Company where id = 100";
        //    var reader = yachtHelper.SearchData(sql);
        //    if (reader.Read())
        //    {
        //        string loadJSON = reader["CertificatHorizontalImgJSON"].ToString();
        //        // 反序列化 JSON 格式
        //        saveNameListH = JsonConvert.DeserializeObject<List<ImageNameH>>(loadJSON);
        //    }
        //    yachtHelper.CloseConnection();

        //    if (saveNameListH?.Count > 0)
        //    {
        //        // 逐一取出每筆JSON資料
        //        foreach (var item in saveNameListH)
        //        {
        //            // 將 RadioButton選項內容改為圖片格式，值設為檔案名稱
        //            ListItem listItem = new ListItem($"<img src='/images/ImgTemp/{item.SaveName}' alt='thumbnail' class='img-thumbnail' width='230px'/>", item.SaveName);
        //            //加入圖片選項
        //            RadioButtonListH.Items.Add(listItem);
        //        }
        //    }
        //    DelHImageBtn.Visible = false; //刪除鈕有選擇圖片時才顯示
        //}

        //protected void UploadHBtn_Click(object sender, EventArgs e)
        //{
        //    // 有選擇檔案時才執行
        //    if (imageUploadH.HasFile)
        //    {
        //        // 先讀取資料庫原有資料
        //        LoadImageHList();
        //        string savePath = Server.MapPath("~/images/ImgTemp/");

        //        // 添加圖檔資料
        //        // 逐一讀取選擇的圖檔
        //        foreach (HttpPostedFile postedFile in imageUploadH.PostedFiles)
        //        {
        //            // 儲存圖片檔案及圖片名稱
        //            // 檢查專案資料夾內有無同名檔案，有同名就加流水號
        //            DirectoryInfo directoryInfo = new DirectoryInfo(savePath);
        //            string fileName = postedFile.FileName;
        //            string[] fileNameArr = fileName.Split(',');
        //            int count = 0;
        //            foreach (var fileItem in directoryInfo.GetFiles())
        //            {
        //                if (fileItem.Name.Contains(fileNameArr[0]))
        //                {
        //                    count++;
        //                }
        //            }

        //            fileName = fileNameArr[0] + $"{count + 1}" + fileNameArr[1];

        //            // 在圖片名稱前面加入 temp 標示並儲存圖片檔案
        //            postedFile.SaveAs(savePath + "temp" + fileName);
        //            // 新增JSON資料
        //            saveNameListH.Add(new ImageNameH { SaveName = fileName });

        //            // 使用 NetVips 套件進行壓縮圖檔
        //            var img = NetVips.Image.NewFromFile(savePath + "temp" + fileName);
        //            if (img.Width > 214 * 2)
        //            {
        //                // 產生原始圖片一半大小的新圖片
        //                var newImg = img.Resize(0.5);
        //                // 如果新圖片寬度還是過大繼續縮減
        //                while (newImg.Width > 214 * 2)
        //                {
        //                    newImg = newImg.Resize(0.5);
        //                }
        //                // 新圖片儲存為正式圖片
        //                newImg.WriteToFile(savePath + fileName);
        //            }
        //            else
        //            {
        //                postedFile.SaveAs(savePath + fileName);
        //            }
        //            // 刪除原始圖片
        //            File.Delete(savePath + "temp" + fileName);
        //        }

        //        // 更新新增後的圖片名稱JSON存入資料庫
        //        string fileNameJsonStr = JsonConvert.SerializeObject(saveNameListH);
        //        string sql = "update Company set CertificatHorizontalImgJSON = @fileNameJsonStr where id = 100";
        //        SqlParameter[] parameters =
        //        {
        //            new SqlParameter("@fileNameJsonStr", SqlDbType.NVarChar) { Value = fileNameJsonStr }
        //        };
        //        yachtHelper.ExecuteSQL(sql, parameters);

        //        //渲染畫面
        //        RadioButtonListH.Items.Clear();
        //        LoadImageHList();
        //    }
        //}

        //protected void DelHImageBtn_Click(object sender, EventArgs e)
        //{
        //}

        //protected void RadioButtonListH_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //顯示刪除按鈕
        //    DelHImageBtn.Visible = true;
        //}
    }
}