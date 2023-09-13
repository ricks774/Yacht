using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.Web.Helpers;
using System.Xml.Linq;
using System.IO;

namespace Yacht.backStage
{
    public partial class DealersEdit : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();
        private FileHepler fileHepler = new FileHepler();
        private int id = 0; // 保存 Repeater 指定行操作所在的ID號
        private string getCountryId;
        private string getAreaId;
        private string defaultCountry;
        private string defaultArea;

        public object DataTableReader { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowDealers();
                LoadCountryData();
                LoadAreaData("10");
            }
            string getId = Request.QueryString["Id"];
            if (getId != null)
            {
                if (!IsPostBack) { ShowDetail(); };
            }
        }

        private void ShowDealers()
        {
            string sql = "select * from DealersInfo";
            GV_dealers.DataSource = yachtHelper.SearchData(sql);
            GV_dealers.DataBind();
            yachtHelper.CloseConnection();
        }

        private void ShowDetail()
        {
            string getId = Request.QueryString["Id"];
            string sql = "select di.id, di.Name, dc.CountrySort, da.AreaSort, di.DealerImgPath, di.Contact, di.Address, di.Tel, di.Fax, di.Email, di.Link from DealersInfo di left join DealersCountry dc on dc.Id = di.CountryId left join DealersArea da on da.Id = di.AreaId where di.Id = @Id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Id", SqlDbType.NVarChar) { Value = getId }
            };

            // 設定 Repeater 的資料來源
            rp_dealerDetail.DataSource = yachtHelper.SearchData(sql, parameters);
            rp_dealerDetail.DataBind();

            // 關閉資料庫連接
            yachtHelper.CloseConnection();

            GetCountry();
        }

        // ! <----------------------------------- 編輯供應商事件開始 ----------------------------------->
        private void GetCountry()
        {
            string getId = Request.QueryString["Id"];
            string sql = "select di.id, di.Name, dc.Id as dcID, dc.CountrySort, da.AreaSort, di.Contact, di.Address, di.Tel, di.Fax, di.Email, di.Link from DealersInfo di left join DealersCountry dc on dc.Id = di.CountryId left join DealersArea da on da.Id = di.AreaId where di.Id = @CountryId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@CountryId", SqlDbType.Int) { Value = getId }
            };

            // 執行資料檢索
            var reader = yachtHelper.SearchData(sql, parameters);

            if (reader.Read())
            {
                defaultCountry = reader["CountrySort"].ToString();
                defaultArea = reader["AreaSort"].ToString();
                getCountryId = reader["dcID"].ToString();
            }
            yachtHelper.CloseConnection();

            LoadCountryList();  // 載入國家
            LoadAreaList();     // 載入地區
        }

        // ! Repeater編輯的相關事件
        protected void Rp_dealerDetail_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                id = int.Parse(e.CommandArgument.ToString());  // 讀取資料庫Id
            }
            else if (e.CommandName == "Cancel")  //取消更新命令
            {
                id = -1;
            }
            else if (e.CommandName == "Delete")  //刪除行內容命令
            {
                id = int.Parse(e.CommandArgument.ToString());  // 讀取資料庫Id
                string thumbnailName = fileHepler.GetFileName(id);  // 取得縮圖檔名

                string sql = "delete DealersInfo where Id = @deleteDealerId";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@deleteDealerId", SqlDbType.NVarChar) { Value = id }
                };
                yachtHelper.ExecuteSQL(sql, parameters);

                // 刪除檔案
                string sourcePath = Server.MapPath($"~/images/Dealers/{thumbnailName}");
                if (thumbnailName != "")
                {
                    File.Delete(sourcePath);
                }
                //ShowDealers();
                Response.Redirect("~/backStage/DealersEdit.aspx");
            }
            else if (e.CommandName == "Update") //更新行內容命令
            {
                //獲取更新的資料庫Id
                id = int.Parse(e.CommandArgument.ToString());
                string name = ((TextBox)e.Item.FindControl("tbx_name")).Text.Trim();
                string contact = ((TextBox)e.Item.FindControl("tbx_contact")).Text.Trim();
                string address = ((TextBox)e.Item.FindControl("tbx_address")).Text.Trim();
                string tel = ((TextBox)e.Item.FindControl("tbx_tel")).Text.Trim();
                string fax = ((TextBox)e.Item.FindControl("tbx_fax")).Text.Trim();
                string email = ((TextBox)e.Item.FindControl("tbx_email")).Text.Trim();
                string link = ((TextBox)e.Item.FindControl("tbx_link")).Text.Trim();
                FileUpload thumbnail = (FileUpload)e.Item.FindControl("update_thumbnail");

                string sql = "update DealersInfo set Name = @NewName, CountryId = @NewCountryId, AreaId = @NewAreaId, Contact = @NewContact, Address = @NewAddress, Tel = @NewTel, Fax = @NewFax, Email = @NewEamil, Link = @NewLink  where id = @InfoId";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@NewName", SqlDbType.NVarChar) { Value = name },
                    new SqlParameter("@NewContact", SqlDbType.NVarChar) { Value = contact },
                    new SqlParameter("@NewAddress", SqlDbType.NVarChar) { Value = address },
                    new SqlParameter("@NewTel", SqlDbType.NVarChar) { Value = tel },
                    new SqlParameter("@NewFax", SqlDbType.NVarChar) { Value = fax },
                    new SqlParameter("@NewEamil", SqlDbType.NVarChar) { Value = email },
                    new SqlParameter("@NewLink", SqlDbType.NVarChar) { Value = link },
                    new SqlParameter("@InfoID", SqlDbType.Int) { Value = id },
                    new SqlParameter("@NewCountryId", SqlDbType.Int) { Value = Session["NewCountryId"] },
                    new SqlParameter("@NewAreaId", SqlDbType.Int) { Value = Session["NewAreaId"] }
                };
                int result = yachtHelper.ExecuteSQL(sql, parameters);
                yachtHelper.CloseConnection();

                // 檔案處理
                if (thumbnail.HasFile)
                {
                    string thumbnailName = fileHepler.GetFileName(id);  // 取得縮圖檔名
                    if (thumbnailName != "")
                    {
                        string sourcePath = Server.MapPath($"~/images/Dealers/{thumbnailName}");
                        File.Delete(sourcePath);
                    }

                    UpdateThumbnail(thumbnail, id, name);
                }

                // 關閉編輯模式
                id = -1;
            }
            //重新綁定控件上的內容
            ShowDetail();
        }

        // ! Repeater 進入編輯模式顯示的畫面
        protected void Rp_dealerDetail_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //判斷Repeater控件中的數據是否是綁定的數據源，如果是的話將會驗證是否進行了編輯操作
            //ListItemType 枚舉表示在一個列表控件可以包括，例如 DataGrid、 DataList和 Repeater 控件的不同項目。
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //獲取綁定的數據源，這裏要註意上面使用sqlReader的方法來綁定數據源，所以下面使用的DbDataRecord方法獲取的
                //如果綁定數據源是DataTable類型的使用下面的語句就會報錯.
                System.Data.Common.DbDataRecord record = (System.Data.Common.DbDataRecord)e.Item.DataItem;
                //DataTable類型的數據源驗證方式
                //System.Data.DataRowView record = (DataRowView)e.Item.DataItem;

                //判斷數據源的id是否等於現在的id，如果相等的話證明現點擊了編輯觸發了userRepeat_ItemCommand事件
                if (id == int.Parse(record["id"].ToString()))
                {
                    ((Panel)e.Item.FindControl("plItem")).Visible = false;
                    ((Panel)e.Item.FindControl("plEdit")).Visible = true;
                }
                else
                {
                    ((Panel)e.Item.FindControl("plItem")).Visible = true;
                    ((Panel)e.Item.FindControl("plEdit")).Visible = false;
                }
            }
        }

        // ! 國家下拉選單
        private void LoadCountryList()
        {
            foreach (RepeaterItem item in rp_dealerDetail.Items)
            {
                Panel plEdit = (Panel)item.FindControl("plEdit");
                // 現在可以訪問 plEdit 控制項內的其他控制項
                // 讀取下拉控制項
                DropDownList list_country = plEdit.FindControl("list_country") as DropDownList;
                // 清空DropDownList以確保不會重複添加
                list_country.Items.Clear();

                string sql = "select * from DealersCountry";
                var reader = yachtHelper.SearchData(sql);
                while (reader.Read())
                {
                    string country = reader["CountrySort"].ToString();
                    string countryId = reader["Id"].ToString();

                    //加入國家到下拉選單選項
                    ListItem listItem = new ListItem();
                    listItem.Text = country;
                    listItem.Value = countryId;
                    list_country.Items.Add(listItem);
                }
                yachtHelper.CloseConnection();

                // 設置預設值 (假設要將第一個項目設置為預設值)
                for (int i = 0; i < list_country.Items.Count; i++)
                {
                    ListItem item2 = list_country.Items[i];
                    if (item2.Text == defaultCountry)
                    {
                        list_country.SelectedValue = list_country.Items[i].Value;
                        Session["NewCountryId"] = list_country.Items[i].Value;
                        getCountryId = list_country.Items[i].Value;
                    }
                }
            }
        }

        // ! 取得選擇國家的值
        protected void Country_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rp_dealerDetail.Items)
            {
                Panel plEdit = (Panel)item.FindControl("plEdit");
                DropDownList list_country = plEdit.FindControl("list_country") as DropDownList;
                getCountryId = list_country.SelectedValue;
                Session["NewCountryId"] = list_country.SelectedValue;
                LoadAreaList(getCountryId);
            }
        }

        // ! 地區下拉選單
        private void LoadAreaList(string countryId)
        {
            foreach (RepeaterItem item in rp_dealerDetail.Items)
            {
                Panel plEdit = (Panel)item.FindControl("plEdit");
                // 現在可以訪問 plEdit 控制項內的其他控制項
                // 讀取下拉控制項
                DropDownList list_area = plEdit.FindControl("list_area") as DropDownList;
                // 清空DropDownList以確保不會重複添加
                list_area.Items.Clear();

                string sql = "select * from DealersArea where CountryId = @CountryIdNew";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@CountryIdNew", SqlDbType.Int) { Value = countryId }
                };
                var reader = yachtHelper.SearchData(sql, parameters);
                while (reader.Read())
                {
                    string area = reader["AreaSort"].ToString();
                    string areaId = reader["Id"].ToString();

                    //加入國家到下拉選單選項
                    ListItem listItem = new ListItem();
                    listItem.Text = area;
                    listItem.Value = areaId;
                    list_area.Items.Add(listItem);
                }
                yachtHelper.CloseConnection();
            }
        }

        // ! 地區下拉選單(載入資料庫原本的值)
        private void LoadAreaList()
        {
            foreach (RepeaterItem item in rp_dealerDetail.Items)
            {
                Panel plEdit = (Panel)item.FindControl("plEdit");
                // 現在可以訪問 plEdit 控制項內的其他控制項
                // 讀取下拉控制項
                DropDownList list_area = plEdit.FindControl("list_area") as DropDownList;
                // 清空DropDownList以確保不會重複添加
                list_area.Items.Clear();

                string sql = "select * from DealersArea where CountryId = @CountryIdNew";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@CountryIdNew", SqlDbType.Int) { Value = getCountryId }
                };
                var reader = yachtHelper.SearchData(sql, parameters);
                while (reader.Read())
                {
                    string area = reader["AreaSort"].ToString();
                    string areaId = reader["Id"].ToString();

                    //加入國家到下拉選單選項
                    ListItem listItem = new ListItem();
                    listItem.Text = area;
                    listItem.Value = areaId;
                    list_area.Items.Add(listItem);
                }
                yachtHelper.CloseConnection();

                // 設置預設值 (假設要將第一個項目設置為預設值)
                for (int i = 0; i < list_area.Items.Count; i++)
                {
                    ListItem item2 = list_area.Items[i];
                    if (item2.Text == defaultArea)
                    {
                        list_area.SelectedValue = list_area.Items[i].Value;
                        Session["NewAreaId"] = list_area.Items[i].Value;
                    }
                }
            }
        }

        // ! 取得地區選取的值
        protected void Area_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rp_dealerDetail.Items)
            {
                Panel plEdit = (Panel)item.FindControl("plEdit");
                DropDownList list_area = plEdit.FindControl("list_area") as DropDownList;
                getAreaId = list_area.SelectedValue;
                Session["NewAreaId"] = list_area.SelectedValue;
            }
        }

        // ! <----------------------------------- 編輯供應商事件開始 ----------------------------------->

        // ! <----------------------------------- 新增供應商事件開始 ----------------------------------->
        protected void Btn_AddNewDealer_Click(object sender, EventArgs e)
        {
            if (tbx_addName.Text != "")
            {
                string sql =
                    "insert into DealersInfo (Name, CountryId, AreaId, Contact, Address, Tel, Fax, Email, Link) " +
                    "values (@AddName, @AddCountryId, @AddAreaId, @AddContact, @AddAddress, @AddTel, @AddFax, @AddEmail, @AddLink) SELECT SCOPE_IDENTITY()";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@AddName", SqlDbType.NVarChar) { Value = tbx_addName.Text },
                    new SqlParameter("@AddCountryId", SqlDbType.Int) { Value = dl_addCountry.SelectedValue },
                    new SqlParameter("@AddAreaId", SqlDbType.Int) { Value = dl_addArea.SelectedValue },
                    new SqlParameter("@AddContact", SqlDbType.NVarChar) { Value = tbx_addContact.Text },
                    new SqlParameter("@AddAddress", SqlDbType.NVarChar) { Value = tbx_addAddress.Text },
                    new SqlParameter("@AddTel", SqlDbType.NVarChar) { Value = tbx_addTel.Text },
                    new SqlParameter("@AddFax", SqlDbType.NVarChar) { Value = tbx_addFax.Text },
                    new SqlParameter("@AddEmail", SqlDbType.NVarChar) { Value = tbx_addEmail.Text },
                    new SqlParameter("@AddLink", SqlDbType.NVarChar) { Value = tbx_addLink.Text }
                    //new SqlParameter("@InfoID", SqlDbType.Int) { Value = id },
                };
                int newDealerId = yachtHelper.ExecuteSQLId(sql, parameters);
                yachtHelper.CloseConnection();
                //Response.Write($"<script>alert('{newDealerId}')</script>");

                UploadThumbnail(newDealerId, tbx_addName.Text);
                Response.Redirect("~/backStage/DealersEdit.aspx");
            }
            else
            {
                Response.Write("<script>alert('Name不能為空')</script>");
            }
        }

        private void LoadCountryData()
        {
            string sql = "select * from DealersCountry";
            var reader = yachtHelper.SearchData(sql);
            while (reader.Read())
            {
                string country = reader["CountrySort"].ToString();
                string countryId = reader["Id"].ToString();

                //加入國家到下拉選單選項
                ListItem listItem = new ListItem();
                listItem.Text = country;
                listItem.Value = countryId;
                dl_addCountry.Items.Add(listItem);
            }
            yachtHelper.CloseConnection();
        }

        private void LoadAreaData(string getCountryId)
        {
            // 清空DropDownList以確保不會重複添加
            dl_addArea.Items.Clear();

            string sql = "select * from DealersArea where CountryId = @AddCountryId";
            SqlParameter[] parameters =
            {
                new SqlParameter("@AddCountryId", SqlDbType.Int) { Value = getCountryId }
            };
            var reader = yachtHelper.SearchData(sql, parameters);
            while (reader.Read())
            {
                string area = reader["AreaSort"].ToString();
                string areaId = reader["Id"].ToString();

                //加入國家到下拉選單選項
                ListItem listItem = new ListItem();
                listItem.Text = area;
                listItem.Value = areaId;
                dl_addArea.Items.Add(listItem);
            }
            yachtHelper.CloseConnection();
        }

        protected void Dl_addCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string getAddCountryId = dl_addCountry.SelectedValue;
            LoadAreaData(getAddCountryId);
        }

        // ! <----------------------------------- 新增供應商事件結束 ----------------------------------->

        // ! <----------------------------------- 檔案處理事件開始 ----------------------------------->
        private void UploadThumbnail(int newDealerId, string newDealerName)
        {
            //設定存檔路徑
            string savePath = Server.MapPath("/images/Dealers/");

            // 判斷是否有檔案
            if (upload_thumbnail.HasFile)
            {
                //儲存圖片檔案及圖片名稱
                string result = fileHepler.FileUpload(upload_thumbnail, newDealerId, newDealerName, savePath);
                if (result == "OK") { Response.Write($"<script>alert('成功新增')</script>"); }
                else { Response.Write($"<script>alert('新增失敗')</script>"); }
            }
        }

        private void UpdateThumbnail(FileUpload updateFileUpload, int newDealerId, string newDealerName)
        {
            //設定存檔路徑
            string savePath = Server.MapPath("/images/Dealers/");

            //儲存圖片檔案及圖片名稱
            string result = fileHepler.FileUpload(updateFileUpload, newDealerId, newDealerName, savePath);
        }

        // ! <----------------------------------- 檔案處理事件結束 ----------------------------------->
    }
}