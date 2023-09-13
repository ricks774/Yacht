using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Text;
using Org.BouncyCastle.Crypto;
using MimeKit;

namespace Yacht
{
    public partial class dealers : System.Web.UI.Page
    {
        private YachtDBHelper yachtHelper = new YachtDBHelper();

        protected void Page_Load(object sender, EventArgs e)
        {
            GetCountryId(); //取得國家 id
            LoadLeftMenu();
            LoadDealerList();
        }

        private void GetCountryId()
        {
            // 取得網址傳值的 id 內容
            string urlIDStr = Request.QueryString["id"];

            // 取得短網址參數內容的國家名稱
            if (Page.RouteData.Values.Count > 0)
            {
                // 取得短網址參數內容的國家名稱
                string urlCountryStr = Page.RouteData.Values["shortUrl"].ToString();
                string sql = "select id from DealersCountry where CountrySort = @urlCountryStr";
                SqlParameter[] parameters =
                {
                    new SqlParameter("@urlCountryStr", SqlDbType.NVarChar) { Value = urlCountryStr }
                };
                var reader = yachtHelper.SearchData(sql, parameters);
                if (reader.Read())
                {
                    urlIDStr = reader["Id"].ToString();
                }
                yachtHelper.CloseConnection();
            }

            // 如無網址傳值則設為第一筆國家名稱 id
            if (string.IsNullOrEmpty(urlIDStr))
            {
                string sql = "select Top 1 id from DealersCountry";
                var reader = yachtHelper.SearchData(sql);
                if (reader.Read())
                {
                    urlIDStr = reader["Id"].ToString();
                }
                yachtHelper.CloseConnection();
            }

            // 將 id 儲存到 Session
            Session["DealersCountryId"] = urlIDStr;
        }

        private void LoadLeftMenu()
        {
            // 反覆變更字串的值建議用 StringBuilder 效能較好
            StringBuilder leftMenuHtml = new StringBuilder();

            // 取得國家分類
            string sql = "select * from DealersCountry";
            var reader = yachtHelper.SearchData(sql);
            while (reader.Read())
            {
                string idStr = reader["Id"].ToString();
                string countryStr = reader["CountrySort"].ToString();
                // StringBuilder 用 Append 來加入字串內容
                leftMenuHtml.Append($"<li><a href='dealers.aspx?id={idStr}'>{countryStr}</a></li>");
            }
            yachtHelper.CloseConnection();

            // 渲染畫面
            LeftMenu.Text = leftMenuHtml.ToString();
        }

        private void LoadDealerList()
        {
            //取得 Session 儲存 id，Session 物件需轉回字串
            string countryIdStr = Session["DealersCountryId"].ToString();
            string sql = "select CountrySort from DealersCountry where Id = @countryIdStr";
            SqlParameter[] parameters =
            {
                new SqlParameter("@countryIdStr", SqlDbType.NVarChar) { Value = countryIdStr }
            };
            var reader = yachtHelper.SearchData(sql, parameters);
            if (reader.Read())
            {
                string countryStr = reader["CountrySort"].ToString();
                LabLink.InnerText = countryStr;
                LitTitle.InnerText = countryStr;
            }
            yachtHelper.CloseConnection();

            // 依 countryId 取得經銷商資料
            StringBuilder dealerListHtml = new StringBuilder();
            string dealersSQL = "select di.Id, da.AreaSort as Area, di.DealerImgPath, di.Name, di.Contact, di.Address, di.Tel, di.Fax, di.Email, di.Link from DealersInfo di left join DealersArea da on da.Id = di.AreaId where di.CountryId = @GetcountryIdStr";
            SqlParameter[] areaParameters =
            {
                new SqlParameter("@GetcountryIdStr", SqlDbType.NVarChar) { Value = countryIdStr }
            };
            var dealersReader = yachtHelper.SearchData(dealersSQL, areaParameters);
            while (dealersReader.Read())
            {
                string idStr = dealersReader["Id"].ToString();
                string areaStr = dealersReader["Area"].ToString();
                string imgPathStr = dealersReader["DealerImgPath"].ToString();
                string nameStr = dealersReader["Name"].ToString();
                string contactStr = dealersReader["Contact"].ToString();
                string addressStr = dealersReader["Address"].ToString();
                string telStr = dealersReader["Tel"].ToString();
                string faxStr = dealersReader["Fax"].ToString();
                string emailStr = dealersReader["Email"].ToString();
                string linkStr = dealersReader["Link"].ToString();
                dealerListHtml.Append("<li><div class='list02'><ul><li class='list02li'><div>" +
                $"<p><img id='Image{idStr}' src='{imgPathStr}' style='border-width:0px;' Width='209px' /> </p></div></li>" +
                $"<li class='list02li02'> <span>{areaStr}</span><br />");

                if (!string.IsNullOrEmpty(nameStr))
                {
                    dealerListHtml.Append($"{nameStr}<br />");
                }
                if (!string.IsNullOrEmpty(contactStr))
                {
                    dealerListHtml.Append($"Contact：{contactStr}<br />");
                }
                if (!string.IsNullOrEmpty(addressStr))
                {
                    dealerListHtml.Append($"Address：{addressStr}<br />");
                }
                if (!string.IsNullOrEmpty(telStr))
                {
                    dealerListHtml.Append($"TEL：{telStr}<br />");
                }
                if (!string.IsNullOrEmpty(faxStr))
                {
                    dealerListHtml.Append($"FAX：{faxStr}<br />");
                }
                if (!string.IsNullOrEmpty(emailStr))
                {
                    dealerListHtml.Append($"E-Mail：{emailStr}<br />");
                }
                if (!string.IsNullOrEmpty(linkStr))
                {
                    dealerListHtml.Append($"<a href='{linkStr}' target='_blank'>{linkStr}</a>");
                }
            }
            yachtHelper.CloseConnection();

            // 渲染畫面
            DealersList.Text = dealerListHtml.ToString();
        }
    }
}