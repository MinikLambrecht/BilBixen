using System;
using System.Diagnostics;
using System.Web.Security;
using System.Web.UI;

namespace BilBixen.Pages
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Wiz1_OnCreatedUser(object sender, EventArgs e)
        {
            _ASSIGN_USER_ROLE(Wiz1.UserName, "User");
        }

        private static void _ASSIGN_USER_ROLE(string username, string role)
        {
            try
            {
                Roles.AddUserToRole(username, role);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}