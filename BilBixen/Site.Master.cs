using System;
using System.Web.Security;
using System.Web.UI;

namespace BilBixen
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void _LOGOUT_USER(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.RedirectToRoute("Login_Page");
        }
    }
}