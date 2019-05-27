using System.Configuration;
using System.Data;
using System.Linq;
using MySql.Data.MySqlClient;

namespace BilBixen.Scripts.Helper_Classes
{
    public class MySqlHelper
    {
        public static DataRow[] GetDataFromDatabase(string query) //Returns rows in table
        {
            using (var conn =
                new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn);

                var dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                var rows = dt.AsEnumerable().ToArray();

                return rows;
            }
        }

        public void SetDataToDatabase(string query) //Adds new data
        {
            using (var conn =
                new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn);

                cmd.ExecuteReader();
            }
        }

        public int UpdateDataToDatabase(string query) //Returns rows updated
        {
            using (var conn =
                new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn);

                var rowsUpdated = cmd.ExecuteNonQuery();

                return rowsUpdated;
            }
        }
    }
}