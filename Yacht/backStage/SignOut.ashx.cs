using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Yacht.backStage
{
    /// <summary>
    /// SignOut 的摘要描述
    /// </summary>
    public class SignOut : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            //清除所有的 Session
            if (context.Session != null)
            {
                context.Session.Abandon();      // 結束 Session 會將 Session 標記為結束
                context.Session.RemoveAll();    // 清除 Session 中所有存儲的資料
            }

            // 建立一個同名的 Cookie 來覆蓋原本的 Cookie
            HttpCookie authenticationCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            authenticationCookie.Expires = DateTime.Now.AddYears(-1);
            context.Response.Cookies.Add(authenticationCookie);

            // 執行登出
            FormsAuthentication.SignOut();

            // 轉向到你登出後要到的頁面
            context.Response.Redirect("~/backStage/SignIn.aspx", true);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}