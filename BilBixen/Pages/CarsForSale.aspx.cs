using BilBixen.Scripts.Helper_Classes;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BilBixen.Pages
{
    public partial class CarsForSale : Page
    {
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
            if (!Page.IsPostBack)
            {
                try
                {
                    //Fill dropdowns
                    FillMakeDropdownFromDatabase();

                    FillCarTypeDropdownFromDatabase();

                    FillFuelDropdownFromDatabase();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

               

            }
        }

        protected void UpdateSearch()
        {
            var data = GetSearchResultsFromDatabase();

            GetSearchResultAmount(data);

            WriteDatabaseResults(data);

            FileInfo[] files = file.GetImagesFromFolder(_AD_ID);

            CreateCarCards(data, files);
        }

        void WriteDatabaseResults(DataRow[] rows)
        {
            int i = 0;

            _MAKE = rows[i]["car_BRAND"].ToString();
            _MODEL = rows[i]["car_MODEL"].ToString();
            _KM = rows[i]["car_KM"].ToString();
            _ENGINE = rows[i]["car_ENGINE"].ToString();
            _PRICE = rows[i]["car_PRICE"].ToString();
            _AD_ID = rows[i]["car_AD_PAGE_ID"].ToString();
        }

        void CreateCarCards(DataRow[] carData, FileInfo[] files)
        {
            int i = 0;

            string cardSetup = "" +
                "<div runat = \"server\" class=\"thumbnail ItemCard\" id=\"SearchResults\">" +
                $"<img class=\"ItemCardImage\" src=\"/Images/{_AD_ID}/{files[i].Name}\">" +
                "<br />" +
                "<div class=\"caption ContentBlock ItemCardDesc\">" +
                $"<h3>{_MAKE}, {_MODEL} </h3>" +
                $"<h4>{_ENGINE}</h4>" +
                $"<p>KM: {_KM}</p>" +
                $"<p>{_PRICE},- DKK</p>" +
                "<p>" +
                $"<a href = \"/AD/{_AD_ID}\" class=\"btn btn-primary\" role=\"button\">View Car</a>" +
                "</p>" +
                "</div>" +
                "</div>";

        }

        void GetSearchResultAmount(DataRow[] cars)
        {
            int i = 0;
            
            foreach(DataRow row in cars)
            {
                i++;
            }

            SearchResultsLabel.Text = $"{i} : Results";
        }

        DataRow[] GetSearchResultsFromDatabase()
        {
            string query = "SELECT * FROM bilbixen.cars " +
                "WHERE car_STATUS = 1";

            #region Dropdowns

            //Make
            switch (MAKE_LABEL_DROP.SelectedValue)
            {
                case "0":
                    {
                        break;
                    }
                default:
                    {
                        query += $", car_BRAND = {MAKE_LABEL_DROP.SelectedValue}";
                        break;
                    }
            }

            //Model
            switch (MODEL_LABEL_DROP.SelectedValue)
            {
                case "0":
                    {
                        break;
                    }
                default:
                    {
                        query += $", car_MODEL = {MODEL_LABEL_DROP.SelectedValue}";
                        break;
                    }
            }

            //FUEL
            switch (FUEL_LABEL_DROP.SelectedValue)
            {
                case "0":
                    {
                        break;
                    }
                default:
                    {
                        query += $", car_FUEL = {FUEL_LABEL_DROP.SelectedValue}";
                        break;
                    }
            }

            //CAR TYPE
            switch (CAR_TYPE_LABEL_DROP.SelectedValue)
            {
                case "0":
                    {
                        break;
                    }
                default:
                    {
                        query += $", car_CATEGORY = {CAR_TYPE_LABEL_DROP.SelectedValue}";
                        break;
                    }
            }

            #endregion

            #region Other

            //TEXT1 = MIN
            //TEXT2 = MAX

            //KM 
            if (!string.IsNullOrWhiteSpace(KM_LABEL_TEXT1.Value) && string.IsNullOrWhiteSpace(KM_LABEL_TEXT2.Value))
            {
                query += $", car_KM >= {KM_LABEL_TEXT1.Value}";                    
            }
            else if (string.IsNullOrWhiteSpace(KM_LABEL_TEXT1.Value) && !string.IsNullOrWhiteSpace(KM_LABEL_TEXT2.Value))
            {
                query += $", car_KM <= {KM_LABEL_TEXT2.Value}";
            }
            else if(!string.IsNullOrWhiteSpace(KM_LABEL_TEXT1.Value) && !string.IsNullOrWhiteSpace(KM_LABEL_TEXT2.Value))
            {
                query += $", car_KM BETWEEN {KM_LABEL_TEXT1.Value} AND {KM_LABEL_TEXT2.Value}";
            }


            //PRICE
            if (!string.IsNullOrWhiteSpace(PRICE_LABEL_TEXT1.Value) && string.IsNullOrWhiteSpace(PRICE_LABEL_TEXT2.Value))
            {
                query += $", car_PRICE >= {PRICE_LABEL_TEXT1.Value}";
            }
            else if (string.IsNullOrWhiteSpace(PRICE_LABEL_TEXT1.Value) && !string.IsNullOrWhiteSpace(PRICE_LABEL_TEXT2.Value))
            {
                query += $", car_PRICE <= {PRICE_LABEL_TEXT2.Value}";
            }
            else if (!string.IsNullOrWhiteSpace(PRICE_LABEL_TEXT1.Value) && !string.IsNullOrWhiteSpace(PRICE_LABEL_TEXT2.Value))
            {
                query += $", car_PRICE BETWEEN {PRICE_LABEL_TEXT1.Value} AND {PRICE_LABEL_TEXT2.Value}";
            }


            //YEAR
            if (!string.IsNullOrWhiteSpace(YEAR_LABEL_TEXT1.Value) && string.IsNullOrWhiteSpace(YEAR_LABEL_TEXT2.Value))
            {
                query += $", car_YEAR >= {YEAR_LABEL_TEXT1.Value}";
            }
            else if (string.IsNullOrWhiteSpace(YEAR_LABEL_TEXT1.Value) && !string.IsNullOrWhiteSpace(YEAR_LABEL_TEXT2.Value))
            {
                query += $", car_YEAR <= {YEAR_LABEL_TEXT2.Value}";
            }
            else if (!string.IsNullOrWhiteSpace(YEAR_LABEL_TEXT1.Value) && !string.IsNullOrWhiteSpace(YEAR_LABEL_TEXT2.Value))
            {
                query += $", car_YEAR BETWEEN {YEAR_LABEL_TEXT1.Value} AND {YEAR_LABEL_TEXT2.Value}";
            }
            #endregion

            query += ";";

            return SQL.GetDataFromDatabase(query);
        }

        protected void MakeSelect_OnChange(object sender, EventArgs e)
        {
            MODEL_LABEL_DROP.Enabled = true;

            int count = MODEL_LABEL_DROP.Items.Count;

            for (int i = 1; i < count; i++)
            {
                MODEL_LABEL_DROP.Items.RemoveAt(1);
            }

            MODEL_LABEL_DROP.SelectedValue = "0";

            FillModelDropdownFromDatabase(int.Parse(MAKE_LABEL_DROP.SelectedValue));
        }

        #region Fill Dropdowns (Code)

        void FillMakeDropdownFromDatabase()
        {
            string query = "Select * from bilbixen.brands";

            var rows = SQL.GetDataFromDatabase(query);

            foreach (DataRow row in rows)
            {
                ListItem item = new ListItem();
                item.Value = row["brand_ID"].ToString();
                item.Text = row["brand_NAME"].ToString();

                MAKE_LABEL_DROP.Items.Add(item);
            }

        }

        void FillModelDropdownFromDatabase(int brandID)
        {
            string query = "Select * from bilbixen.models " +
                $"where model_BRAND = {brandID};";

            var rows = SQL.GetDataFromDatabase(query);

            foreach (DataRow row in rows)
            {
                ListItem item = new ListItem();

                item.Value = row["model_ID"].ToString();
                item.Text = row["model_NAME"].ToString();

                MODEL_LABEL_DROP.Items.Add(item);
            }
        }

        void FillFuelDropdownFromDatabase()
        {
            string query = "Select * from bilbixen.fuels";

            var rows = SQL.GetDataFromDatabase(query);

            foreach (DataRow row in rows)
            {
                ListItem item = new ListItem();
                item.Value = row["fuel_ID"].ToString();
                item.Text = row["fuel_TYPE"].ToString();

                FUEL_LABEL_DROP.Items.Add(item);
            }
        }

        void FillCarTypeDropdownFromDatabase()
        {
            string query = "Select * from bilbixen.categories";

            var rows = SQL.GetDataFromDatabase(query);

            foreach (DataRow row in rows)
            {
                ListItem item = new ListItem();
                item.Value = row["category_ID"].ToString();
                item.Text = row["category_NAME"].ToString();

                CAR_TYPE_LABEL_DROP.Items.Add(item);
            }
        }

        #endregion
    }
}