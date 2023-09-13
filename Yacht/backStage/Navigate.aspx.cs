using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Yacht.backStage
{
    public partial class Navigate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 判斷是否有進行帳號登入
            //if (Session["Account"] == null) Response.Redirect("~/backStage/Sign_in.aspx");
            //else
            //{
            //    // 判斷是否為管理者帳號
            //    bool isManager = (bool)Session["MaxPower"];
            //    if (isManager) Response.Redirect("~/backStage/UserBoard.aspx");
        }
    }
}