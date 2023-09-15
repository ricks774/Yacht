using Microsoft.AspNet.FriendlyUrls;
using Microsoft.AspNet.FriendlyUrls.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Yacht
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // 設定不顯示附檔名
            var routes = RouteTable.Routes;
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            //routes.EnableFriendlyUrls(settings);

            //修改避免 CKFinder 上傳功能錯誤
            routes.EnableFriendlyUrls(settings, new Microsoft.AspNet.FriendlyUrls.Resolvers.IFriendlyUrlResolver[] { new MyWebFormsFriendlyUrlResolver() });

            // 執行短網址路由方法
            //RegisterRouters(RouteTable.Routes);
        }

        public class MyWebFormsFriendlyUrlResolver : Microsoft.AspNet.FriendlyUrls.Resolvers.WebFormsFriendlyUrlResolver
        {
            public override string ConvertToFriendlyUrl(string path)
            {
                //字串為 ckfinder 固定內容
                if (!string.IsNullOrEmpty(path) && path.ToLower().Contains("/ckfinder/core/connector/aspx/connector.aspx"))
                {
                    return path;
                }
                return base.ConvertToFriendlyUrl(path);
            }
        }

        private void RegisterRouters(RouteCollection routes)
        {
            // MapPageRoute("自訂路由名稱", "替換後的網址區塊", "原本實際執行的網頁位置")
            // {shortUrl} 為短網址名稱，可以視為之後要用來抓取的參數
            routes.MapPageRoute("shortUrlRoute", "ShowList/{shortUrl}", "~/Tayanahtml/dealers.aspx");
            //可以建立多個規則
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}