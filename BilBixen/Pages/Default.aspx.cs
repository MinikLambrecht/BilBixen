using BilBixen.Scripts.Helper_Classes;
using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace BilBixen
{
    public partial class Default : Page
    {
        Random rand = new Random();
        MySQL_Helper SQL = new MySQL_Helper();
        ImageFileControll file = new ImageFileControll();

        public string _MAKE;
        public string _MODEL;
        public string _ENGINE;
        public string _KM;
        public string _PRICE;

        public string _AD_ID;

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

        int GetIndexOfDatabaseOutputDataScores(DataRow[] rows)
        {
            float highestScore = 0;
            int indexNumberOfHighestScore = 0;

            int i = 0;

            int km;
            int price;
            int firstRegYear;

            foreach(DataRow row in rows)
            {
                if (!string.IsNullOrWhiteSpace(row["car_KM"].ToString()))
                {
                    km = int.Parse(row["car_KM"].ToString());
                }
                else
                {
                    km = 100;
                }

                price = int.Parse(row["car_PRICE"].ToString());

                string year = row["car_FIRST_REGISTRATION"].ToString().Split('/')[2].Split(' ')[0];

                firstRegYear = int.Parse(year);

                //Devide KM with price times the first year of registration (Higher score is better)
                float score = (((km / price) * firstRegYear));

                if(score > highestScore)
                {
                    highestScore = score;
                    indexNumberOfHighestScore = i;
                }

                i++;
            }

            return indexNumberOfHighestScore;
        }

        void GetPassengerCarsDataFromDatabase()
        {

            string query = "Select * from bilbixen.all_cars " +
                $"where car_CATEGORY = 'Passenger' and car_STATUS = 'For Sale';";

            var rows = SQL.GetDataFromDatabase(query);

            int i = GetIndexOfDatabaseOutputDataScores(rows);

            _MAKE = rows[i]["car_BRAND"].ToString();
            _MODEL = rows[i]["car_MODEL"].ToString();
            _KM = rows[i]["car_KM"].ToString();
            _ENGINE = rows[i]["car_ENGINE"].ToString();
            _PRICE = rows[i]["car_PRICE"].ToString();
            _AD_ID = rows[i]["car_AD_PAGE_ID"].ToString();

            FileInfo[] files = file.GetImagesFromFolder(_AD_ID);

            i = 0;

            PassengerCars.InnerHtml += "" +
                $"<img class=\"ItemCardImage\" src=\"/Images/{_AD_ID}/{files[i].Name}\">" +
                $"<br />" +
                "<div class=\"caption ContentBlock ItemCardDesc\">" +
                $"<h3>{_MAKE}, {_MODEL}</h3>" +
                $"<h4>{_ENGINE}</h4>" +
                $"<p>KM: {_KM}</p>" +
                $"<p>{_PRICE},- DKK</p>" +
                "<p>" +
                $"<a href = \"/AD/{_AD_ID}\" class=\"btn btn-primary\" role=\"button\">View Car</a>" +
                "</p>" +
                "</div>";

        }

        void GetWreckedCarsDataFromDatabase()
        {

            string query = "Select * from bilbixen.all_cars " +
                $"where car_CATEGORY = 'Wreck' and car_STATUS = 'For Sale';";

            var rows = SQL.GetDataFromDatabase(query);

            int i = GetIndexOfDatabaseOutputDataScores(rows);

            _MAKE = rows[i]["car_BRAND"].ToString();
            _MODEL = rows[i]["car_MODEL"].ToString();
            _KM = rows[i]["car_KM"].ToString();
            _ENGINE = rows[i]["car_ENGINE"].ToString();
            _PRICE = rows[i]["car_PRICE"].ToString();
            _AD_ID = rows[i]["car_AD_PAGE_ID"].ToString();

            FileInfo[] files = file.GetImagesFromFolder(_AD_ID);

            i = 0;

            WreckedCars.InnerHtml += "" +
            $"<img class=\"ItemCardImage\" src=\"/Images/{_AD_ID}/{files[i].Name}\">" +
            $"<br />" +
            "<div class=\"caption ContentBlock ItemCardDesc\">" +
            $"<h3>{_MAKE}, {_MODEL}</h3>" +
            $"<h4>{_ENGINE}</h4>" +
            $"<p>KM: {_KM}</p>" +
            $"<p>{_PRICE},- DKK</p>" +
            "<p>" +
            $"<a href = \"/AD/{_AD_ID}\" class=\"btn btn-primary\" role=\"button\">View Car</a>" +
            "</p>" +
            "</div>";

        }

        void GetVanCarsDataFromDatabase()
        {

            string query = "Select * from bilbixen.all_cars " +
                $"where car_CATEGORY = 'Van' and car_STATUS = 'For Sale';";

            var rows = SQL.GetDataFromDatabase(query);

            int i = GetIndexOfDatabaseOutputDataScores(rows);

            _MAKE = rows[i]["car_BRAND"].ToString();
            _MODEL = rows[i]["car_MODEL"].ToString();
            _KM = rows[i]["car_KM"].ToString();
            _ENGINE = rows[i]["car_ENGINE"].ToString();
            _PRICE = rows[i]["car_PRICE"].ToString();
            _AD_ID = rows[i]["car_AD_PAGE_ID"].ToString();

            FileInfo[] files = file.GetImagesFromFolder(_AD_ID);

            i = 0;

            IndustrialCars.InnerHtml += "" +
            $"<img class=\"ItemCardImage\" src=\"/Images/{_AD_ID}/{files[i].Name}\">" +
            $"<br />" +
            "<div class=\"caption ContentBlock ItemCardDesc\">" +
            $"<h3>{_MAKE}, {_MODEL}</h3>" +
            $"<h4>{_ENGINE}</h4>" +
            $"<p>KM: {_KM}</p>" +
            $"<p>{_PRICE},- DKK</p>" +
            "<p>" +
            $"<a href = \"/AD/{_AD_ID}\" class=\"btn btn-primary\" role=\"button\">View Car</a>" +
            "</p>" +
            "</div>";

        }
    }
}