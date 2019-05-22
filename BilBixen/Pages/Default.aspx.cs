using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using MySql.Data.MySqlClient;
using MySql.Web.Security;

namespace BilBixen.Pages
{
    public partial class Default : Page
    {
        public Random Rand { get; } = new Random();

        private string _MAKE;
        private string _MODEL;
        private string _ENGINE;
        private string _KM;
        private string _PRICE;

        private string _AD_ID;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                GetPassengerCarsDataFromDatabase();

                GetWreckedCarsDataFromDatabase();

                GetVanCarsDataFromDatabase();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        // Bug: Role provider connection is actively refused by pc
        protected void TestButton_OnClick(object sender, EventArgs e)
        {
            var roleManager = new MySQLRoleProvider();
            var roles = roleManager.GetAllRoles();

            foreach (var role in roles)
            {
                Debug.WriteLineIf(!string.IsNullOrWhiteSpace(role), role);
            }
        }

        private static int GetIndexOfDatabaseOutputDataScores(IEnumerable<DataRow> rows)
        {
            float highestScore = 0;
            var indexNumberOfHighestScore = 0;

            var i = 0;

            foreach(var row in rows)
            {
                var km = !string.IsNullOrWhiteSpace(row["car_KM"].ToString()) ? int.Parse(row["car_KM"].ToString()) : 100;

                var price = int.Parse(row["car_PRICE"].ToString());

                var year = row["car_FIRST_REGISTRATION"].ToString().Split('/')[2].Split(' ')[0];

                var firstRegYear = int.Parse(year);

                //Divide KM with price times the first year of registration (Higher score is better)
                float score = (km / price) * firstRegYear;

                if(score > highestScore)
                {
                    highestScore = score;
                    indexNumberOfHighestScore = i;
                }

                i++;
            }

            return indexNumberOfHighestScore;
        }

        private void GetPassengerCarsDataFromDatabase()
        {
            const string query = "Select * from bilbixen.all_cars where car_CATEGORY = 'Passenger' and car_STATUS = 'For Sale';";

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn);

                var dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                var rows = dt.AsEnumerable().ToArray();

                var i = GetIndexOfDatabaseOutputDataScores(rows);

                _MAKE = rows[i]["car_BRAND"].ToString();
                _MODEL = rows[i]["car_MODEL"].ToString();
                _KM = rows[i]["car_KM"].ToString();
                _ENGINE = rows[i]["car_ENGINE"].ToString();
                _PRICE = rows[i]["car_PRICE"].ToString();
                _AD_ID = rows[i]["car_AD_PAGE_ID"].ToString();

                var files = GetImagesFromFolder(_AD_ID);

                i = 0;

                PassengerCars.InnerHtml +=
                    $"<img class=\"ItemCardImage\" src=\"/Images/{_AD_ID}/{files[i].Name}\"><br /><div class=\"caption ContentBlock ItemCardDesc\"><h3>{_MAKE}, {_MODEL}</h3><h4>{_ENGINE}</h4><p>KM: {_KM}</p><p>{_PRICE},- DKK</p><p><a href = \"/AD/{_AD_ID}\" class=\"btn btn-primary\" role=\"button\">View Car</a></p></div>";
            }
        }

        void GetWreckedCarsDataFromDatabase()
        {

            var query = "Select * from bilbixen.all_cars where car_CATEGORY = 'Wreck' and car_STATUS = 'For Sale';";

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn);

                var dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                var rows = dt.AsEnumerable().ToArray();

                var i = GetIndexOfDatabaseOutputDataScores(rows);

                _MAKE = rows[i]["car_BRAND"].ToString();
                _MODEL = rows[i]["car_MODEL"].ToString();
                _KM = rows[i]["car_KM"].ToString();
                _ENGINE = rows[i]["car_ENGINE"].ToString();
                _PRICE = rows[i]["car_PRICE"].ToString();
                _AD_ID = rows[i]["car_AD_PAGE_ID"].ToString();

                var files = GetImagesFromFolder(_AD_ID);

                i = 0;

                WreckedCars.InnerHtml +=
                    $"<img class=\"ItemCardImage\" src=\"/Images/{_AD_ID}/{files[i].Name}\"><br /><div class=\"caption ContentBlock ItemCardDesc\"><h3>{_MAKE}, {_MODEL}</h3><h4>{_ENGINE}</h4><p>KM: {_KM}</p><p>{_PRICE},- DKK</p><p><a href = \"/AD/{_AD_ID}\" class=\"btn btn-primary\" role=\"button\">View Car</a></p></div>";
            }
        }

        void GetVanCarsDataFromDatabase()
        {

            var query = "Select * from bilbixen.all_cars where car_CATEGORY = 'Van' and car_STATUS = 'For Sale';";

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn);

                var dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                var rows = dt.AsEnumerable().ToArray();

                var i = GetIndexOfDatabaseOutputDataScores(rows);

                _MAKE = rows[i]["car_BRAND"].ToString();
                _MODEL = rows[i]["car_MODEL"].ToString();
                _KM = rows[i]["car_KM"].ToString();
                _ENGINE = rows[i]["car_ENGINE"].ToString();
                _PRICE = rows[i]["car_PRICE"].ToString();
                _AD_ID = rows[i]["car_AD_PAGE_ID"].ToString();

                FileInfo[] files = GetImagesFromFolder(_AD_ID);

                i = 0;

                IndustrialCars.InnerHtml +=
                    $"<img class=\"ItemCardImage\" src=\"/Images/{_AD_ID}/{files[i].Name}\"><br /><div class=\"caption ContentBlock ItemCardDesc\"><h3>{_MAKE}, {_MODEL}</h3><h4>{_ENGINE}</h4><p>KM: {_KM}</p><p>{_PRICE},- DKK</p><p><a href = \"/AD/{_AD_ID}\" class=\"btn btn-primary\" role=\"button\">View Car</a></p></div>";
            }
        }

        private static FileInfo[] GetImagesFromFolder(string adId)
        {
            var folderUri = HttpContext.Current.Server.MapPath($"~/Images/{adId}/");

            var d = new DirectoryInfo(folderUri);

            var files = d.GetFiles("*");

            return files;
        }
    }
}