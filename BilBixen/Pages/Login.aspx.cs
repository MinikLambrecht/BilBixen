using System;
using System.Drawing;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BilBixen.Pages
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Wiz1_OnCreatedUser(object sender, EventArgs e)
        {
            // TODO: Find out why the User role is not found.
            Roles.AddUserToRole(Wiz1.UserName, "User");
        }
    }
}