using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using MySql.Web.Security;
using System.Web.UI;
using Microsoft.Ajax.Utilities;
using MySql.Data.MySqlClient;

namespace BilBixen.Pages
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckDbConnection();
        }

        protected void Wiz1_OnCreatedUser(object sender, EventArgs e)
        {
            var roles = new MySQLRoleProvider();

            var usernameList = new string[1];
            usernameList[0] = Wiz1.UserName;

            var roleList = new string[1];
            roleList[0] = "User";

            // TODO: Find out why the User role is not found.
            roles.AddUsersToRoles(usernameList, roleList);
        }

        private static void CheckDbConnection()
        {
            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                conn.Open();

                Debug.WriteLine("Connection is " + conn.State + ".");
                Debug.WriteLineIf(conn.State == ConnectionState.Open, "Server is running MySql version " + conn.ServerVersion + ".");
            }
        }
    }
}