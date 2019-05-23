using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Web.Security;
using System.Web.UI;
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
            Roles.AddUserToRole(Wiz1.UserName, "User");
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