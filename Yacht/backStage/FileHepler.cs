using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Yacht.backStage
{
    public class FileHepler
    {
        public int UploadCheck(string extension, int fileSize)
        {
            int ae = AllowedExtextsion(extension);
            int fz = FileSize(fileSize);
            if (ae == 0 && fz == 0) return 0;
            else return -1;
        }

        // ! 判斷是否為允許上傳的檔案附檔名
        public int AllowedExtextsion(string extension)
        {
            List<string> allowedExtextsion = new List<string> { ".jpg", ".bmp", ".png" };
            if (allowedExtextsion.IndexOf(extension) == -1) return -1;
            else return 0;
        }

        // ! 限制檔案大小，限制為 5MB
        public int FileSize(int fileSize)
        {
            if (fileSize > 5120000) return -1;
            else return 0;
        }

        #region "檢查檔案上傳格式是否符合"

        public string FileCheck(FileUpload fileUpload)
        {
            string fileName = fileUpload.FileName;
            string extension = Path.GetExtension(fileName).ToLowerInvariant();

            // 取得檔案大小
            int fileSize = fileUpload.PostedFile.ContentLength;

            // 判斷是否為允許上傳的檔案附檔名
            int ae = AllowedExtextsion(extension);
            int fz = FileSize(fileSize);

            if (ae == -1 || fz == -1)
            {
                return "NO";
            }
            else return "OK";
        }

        #endregion "檢查檔案上傳格式是否符合"

        public string FileUpload(FileUpload fileUpload, string sql, SqlParameter[] parameters, int newDealerId, string newDealerName, string thumbnailDir)
        {
            YachtDBHelper yachtHelper = new YachtDBHelper();

            string fileName = fileUpload.FileName;  // 取得上傳檔案的檔名
            string extension = Path.GetExtension(fileName).ToLowerInvariant();  // 取得副檔名

            // === 自訂定義檔案名稱 ===
            fileName = $"{newDealerId}_{newDealerName}{extension}";

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            if (Directory.Exists(thumbnailDir) == false) Directory.CreateDirectory(thumbnailDir);

            // 把路徑寫到資料庫內
            //string virtualPath = $"/images/Dealers/{fileName}";
            //string sql = $"update DealersInfo set DealerImgPath = @UpdateImgDealerPath where Id = @UpdateImgDealerId";
            //SqlParameter[] parameters =
            //{
            //    new SqlParameter("@UpdateImgDealerPath", SqlDbType.NVarChar) { Value = virtualPath },
            //    new SqlParameter("@UpdateImgDealerId", SqlDbType.Int) { Value = newDealerId }
            //};
            yachtHelper.ExecuteSQL(sql, parameters);

            // 檔案上傳
            string coverFilePath = Path.Combine(thumbnailDir, fileName);
            fileUpload.SaveAs(coverFilePath);

            // 判斷檔案是否上傳成功
            if (File.Exists(coverFilePath)) return "OK";
            else return "FALL";
        }

        #region "多檔案上傳"

        public string FileUploadMultiple(HttpPostedFile httpPostedFile, string sql, SqlParameter[] parameters, int dataId, string savefileName, string savePath)
        {
            YachtDBHelper yachtHelper = new YachtDBHelper();

            string fileName = httpPostedFile.FileName;  // 取得上傳檔案的檔名
            string extension = Path.GetExtension(fileName).ToLowerInvariant();  // 取得副檔名

            // === 自訂定義檔案名稱 ===
            fileName = $"{dataId}_{savefileName}{extension}";

            // 檢查 Server 上該資料夾是否存在，不存在就自動建立
            if (Directory.Exists(savePath) == false) Directory.CreateDirectory(savePath);
            yachtHelper.ExecuteSQL(sql, parameters);

            // 檔案上傳
            string coverFilePath = Path.Combine(savePath, fileName);
            httpPostedFile.SaveAs(coverFilePath);

            // 判斷檔案是否上傳成功
            if (File.Exists(coverFilePath)) return "OK";
            else return "FALL";
        }

        #endregion "多檔案上傳"

        #region "檔案上傳(不執行SQL指令)"

        public string FileUpload(HttpPostedFile httpPostedFile, FileUpload fileUpload, int dataId, string fileName, string savePath)
        {
            if (FileCheck(fileUpload) == "OK")
            {
                // === 自訂定義檔案名稱 ===
                fileName = $"{dataId}_{fileName}";

                // 檢查 Server 上該資料夾是否存在，不存在就自動建立
                if (Directory.Exists(savePath) == false) Directory.CreateDirectory(savePath);

                // 檔案上傳
                string coverFilePath = Path.Combine(savePath, fileName);
                fileUpload.SaveAs(coverFilePath);

                // 判斷檔案是否上傳成功
                if (File.Exists(coverFilePath)) return "OK";
                else return "FALL";
            }
            else
            {
                return "FALL";
            }
        }

        #endregion "檔案上傳(不執行SQL指令)"

        public string GetFileName(int bindId)
        {
            YachtDBHelper yachtHelper = new YachtDBHelper();

            string getName = $"SELECT RIGHT(DealerImgPath, CHARINDEX('/', REVERSE(DealerImgPath)) - 1) AS FileName FROM DealersInfo WHERE Id = @Id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", SqlDbType.NVarChar) { Value = bindId }
            };
            var sqlName = yachtHelper.SearchData(getName, parameters);
            var fileName = "";
            if (sqlName.Read()) fileName = sqlName["FileName"].ToString();   // 取得檔名
            yachtHelper.CloseConnection();
            return fileName;
        }
    }
}