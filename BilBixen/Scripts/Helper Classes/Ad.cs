using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace BilBixen.Scripts.Helper_Classes
{
    public class Ad
    {
        private string[] _IMGURL = new string[10];

        private string _TITLE;
        private string _DESCRIPTION;

        private string _BRAND;
        private string _MODEL;
        private string _ENGINE;
        private string _KM;
        private string _DOORS;
        private DateTime _FIRSTREGISTRATION;
        private string _CATEGORY;
        private string _FUEL;
        private string _STATUS;
        private int _PRICE;

        public Ad(string title, string desc, string brand, string model, string engine, string km, string doors, DateTime firstreg, string category, string fuel, string status, int price)
        {
            _TITLE = title;
            _DESCRIPTION = desc;

            _BRAND = brand;
            _MODEL = model;
            _ENGINE = engine;
            _KM = km;
            _DOORS = doors;
            _FIRSTREGISTRATION = firstreg;
            _CATEGORY = category;
            _FUEL = fuel;
            _STATUS = status;
            _PRICE = price;
        }

        public void AddAd(string title, string desc, string brand, string model, string engine, string km, string doors, DateTime firstreg, string category, string fuel, string status, int price)
        {

        }

        public void RemoveAd(int id)
        {

        }

        public void EditAd(int id)
        {

        }

        private void CarSqlCall(string brand, string model, string engine, string km, string doors, DateTime firstreg, string category, string fuel, string status)
        {
            const string query = "";

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                var cmd = new MySqlCommand(query, conn);
            }
        }

        private void AdSqlCall(string title, string desc, string brand, string model, string engine, string km, string doors, DateTime firstreg, string category, string fuel, string status, int price)
        {
            const string query = "";

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                var cmd = new MySqlCommand(query, conn);
            }
        }
    }
}