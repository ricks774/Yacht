using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;

namespace Yacht.backStage
{
    public partial class DealersManager : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowCountryData();
                LoadArealList();
                ShowAreaData();
            }
        }

        private void ShowCountryData()
        {
            string sql = "select * from DealersCountry";
            GV_all.DataSource = yachtHelper.SearchData(sql);
            GV_all.DataBind();
            yachtHelper.CloseConnection();
        }

        private void ShowAreaData()
        {
            string selectArea = area_list.SelectedValue;
            string sql = $"select da.Id, da.AreaSort, da.CreateDate from DealersCountry dc left join DealersArea da on da.CountryId = dc.Id where dc.id ='{selectArea}'";
            GV_area.DataSource = yachtHelper.SearchData(sql);
            GV_area.DataBind();
            yachtHelper.CloseConnection();
        }

        private void ShowAreaData(string area)
        {
            string sql = $"select da.Id, da.AreaSort, da.CreateDate from DealersCountry dc left join DealersArea da on da.CountryId = dc.Id where dc.id = '{area}'";
            GV_area.DataSource = yachtHelper.SearchData(sql);
            GV_area.DataBind();
            yachtHelper.CloseConnection();
        }

        protected void GV_all_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_all.EditIndex = -1;
            ShowCountryData();
        }

        protected void GV_all_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bindId = Convert.ToInt32(GV_all.DataKeys[e.RowIndex].Value); // 取得資料表的 ID
            string sql = "delete DealersCountry where Id = @Id";
            SqlParameter[] parameters ={
                new SqlParameter("@Id", SqlDbType.Int) { Value = bindId }
            };
            yachtHelper.ExecuteSQL(sql, parameters);
            GV_all.EditIndex = -1;
            ShowCountryData();
        }

        protected void GV_all_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int bindId = Convert.ToInt32(GV_all.DataKeys[e.RowIndex].Value); // 取得資料表的 ID
            GridViewRow row = GV_all.Rows[e.RowIndex];  // 取的正在更新的索引
            TextBox textBox = row.FindControl("input_country") as TextBox;
            string updateCountry = textBox.Text;
            string sql = $"update DealersCountry set CountrySort = @Name where Id = @Id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value =  updateCountry},
                new SqlParameter("@Id", SqlDbType.Int) { Value = bindId }
            };
            yachtHelper.ExecuteSQL(sql, parameters);

            GV_all.EditIndex = -1;
            ShowCountryData();
        }

        protected void GV_all_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_all.EditIndex = e.NewEditIndex;
            ShowCountryData();
        }

        protected void Btn_AddNewCountry_Click(object sender, EventArgs e)
        {
            string sql = "insert into DealersCountry (CountrySort) values (@CountrySort)";
            string newCountry = input_newCountry.Text;
            SqlParameter[] parameters =
            {
                new SqlParameter("@CountrySort", SqlDbType.NVarChar) { Value = newCountry }
            };
            int result = yachtHelper.ExecuteSQL(sql, parameters);
            if (result > 0) { Response.Write("<script>alert('成功新增')</script>"); }
            else { Response.Write("<script>alert('新增失敗')</script>"); }
            ShowCountryData();
        }

        // ! <====================================== 地區按鈕事件開始 ======================================>
        protected void Btn_AddNewArea_Click(object sender, EventArgs e)
        {
            string sql = "insert into DealersArea (AreaSort, CountryId) values (@AreaSort, @CountryId)";
            string selectCountryId = area_list.SelectedValue;
            string newArea = input_area.Text;
            SqlParameter[] parameters =
            {
                new SqlParameter("@AreaSort", SqlDbType.NVarChar) { Value = newArea },
                new SqlParameter("@CountryId", SqlDbType.Int) { Value = selectCountryId }
            };
            int result = yachtHelper.ExecuteSQL(sql, parameters);
            if (result > 0) { Response.Write("<script>alert('成功新增')</script>"); }
            else { Response.Write("<script>alert('新增失敗')</script>"); }
            ShowAreaData();
        }

        protected void GV_area_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GV_area.EditIndex = -1;
            ShowAreaData();
        }

        protected void GV_area_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int bindId = Convert.ToInt32(GV_area.DataKeys[e.RowIndex].Value); // 取得資料表的 ID
            string sql = "delete DealersArea where Id = @Id";
            SqlParameter[] parameters ={
                new SqlParameter("@Id", SqlDbType.Int) { Value = bindId }
            };
            yachtHelper.ExecuteSQL(sql, parameters);
            GV_area.EditIndex = -1;
            ShowAreaData();
        }

        protected void GV_area_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GV_area.EditIndex = e.NewEditIndex;
            ShowAreaData();
        }

        protected void GV_area_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int bindId = Convert.ToInt32(GV_area.DataKeys[e.RowIndex].Value); // 取得資料表的 ID
            GridViewRow row = GV_area.Rows[e.RowIndex];  // 取的正在更新的索引
            TextBox textBox = row.FindControl("update_area") as TextBox;
            string updateArea = textBox.Text;
            string sql = $"update DealersArea set AreaSort = @Name where Id = @Id";
            SqlParameter[] parameters =
            {
                new SqlParameter("@Name", SqlDbType.NVarChar) { Value =  updateArea},
                new SqlParameter("@Id", SqlDbType.Int) { Value = bindId }
            };
            yachtHelper.ExecuteSQL(sql, parameters);

            GV_area.EditIndex = -1;
            ShowAreaData();
        }

        // ! <====================================== 地區按鈕事件結束 ======================================>

        // ! 地區下拉選單
        private void LoadArealList()
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
                area_list.Items.Add(listItem);
            }
            yachtHelper.CloseConnection();
        }

        protected void area_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectArea = area_list.SelectedValue;
            ShowAreaData(selectArea);
        }
    }
}