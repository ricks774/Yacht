using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Yacht.backStage
{
    /// <summary>
    /// CheckAccount 的摘要描述
    /// </summary>
    public class CheckAccount : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string ticketUserData = ((FormsIdentity)(HttpContext.Current.User.Identity)).Ticket.UserData;
            string[] ticketUserDataArr = ticketUserData.Split(';');
            bool haveRight = HttpContext.Current.User.Identity.IsAuthenticated;

            // 依照管理權限導頁面
            if (haveRight)
            {
                if (ticketUserDataArr[0].Equals("True")) { context.Response.Redirect("~/backStage/UserBoard.aspx"); }
                else { context.Response.Redirect("~/backStage/Navigate.aspx"); }
            }
            else
            {
                context.Response.Redirect("~/backStage/SignIn.aspx");
            }
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