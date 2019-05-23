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

                    UpdateSearch();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

               

            }
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

            UpdateSearch();
        }

        protected void AutoSearch_OnChange(object sender, EventArgs e)
        {
            UpdateSearch();
        }

        protected void UpdateSearch()
        {
            var data = GetSearchResultsFromDatabase();

            GetSearchResultAmount(data);

            CreateCarCards(data);
        }

        void CreateCarCards(DataRow[] carData)
        {
            CarGalleryContainer.InnerHtml = "";

            CarGalleryContainer.InnerHtml += "<div class=\"row\">";

            int i = 0;
            float calc;

            foreach (DataRow row in carData)
            {
                FileInfo[] files = file.GetImagesFromFolder(row["car_AD_PAGE_ID"].ToString());

                calc = i % 3;

                if (calc == 0 && i != 0)
                {
                    CarGalleryContainer.InnerHtml += "</div> " +
                        " <br /> " +
                        "<div class=\"row\">";
                }

                string cardSetup = "" +
                    "<div class=\"col-md-4\">" +
                        "<div class=\"thumbnail ItemCard\" id=\"SearchResults\">" +
                            $"<img class=\"ItemCardImage\" src=\"/Images/{row["car_AD_PAGE_ID"].ToString()}/{files[0].Name}\">" +
                            "<br />" +
                            "<div class=\"caption ContentBlock ItemCardDesc\">" +
                                $"<h3>{row["car_BRAND"].ToString()}, {row["car_MODEL"].ToString()} </h3>" +
                                $"<h4>{row["car_ENGINE"].ToString()}</h4>" +
                                $"<p>KM: {row["car_KM"].ToString()}</p>" +
                                $"<p>{row["car_PRICE"].ToString()},- DKK</p>" +
                                "<p>" +
                                    $"<a href = \"/AD/{row["car_AD_PAGE_ID"].ToString()}\" class=\"btn btn-primary\" role=\"button\">View Car</a>" +
                                "</p>" +
                            "</div>" +
                        "</div>" +
                    "</div>";
                
                CarGalleryContainer.InnerHtml += cardSetup;

                i++;
            }
            CarGalleryContainer.InnerHtml += "</div>";
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
            string query = "SELECT * FROM bilbixen.all_cars " +
                "WHERE car_STATUS = 'For Sale'";

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
                        query += $" AND car_BRAND = '{MAKE_LABEL_DROP.SelectedItem.Text}'";
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
                        query += $" AND car_MODEL = '{MODEL_LABEL_DROP.SelectedItem.Text}'";
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
                        query += $" AND car_FUEL = '{FUEL_LABEL_DROP.SelectedItem.Text}'";
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
                        query += $" AND car_CATEGORY = '{CAR_TYPE_LABEL_DROP.SelectedItem.Text}'";
                        break;
                    }
            }

            #endregion

            #region Other

            //TEXT1 = MIN
            //TEXT2 = MAX

            //KM 
            if (!string.IsNullOrWhiteSpace(KM_LABEL_TEXT1.Text) && string.IsNullOrWhiteSpace(KM_LABEL_TEXT2.Text))
            {
                query += $" AND car_KM >= {KM_LABEL_TEXT1.Text}";                    
            }
            else if (string.IsNullOrWhiteSpace(KM_LABEL_TEXT1.Text) && !string.IsNullOrWhiteSpace(KM_LABEL_TEXT2.Text))
            {
                query += $" AND car_KM <= {KM_LABEL_TEXT2.Text}";
            }
            else if(!string.IsNullOrWhiteSpace(KM_LABEL_TEXT1.Text) && !string.IsNullOrWhiteSpace(KM_LABEL_TEXT2.Text))
            {
                query += $" AND car_KM BETWEEN {KM_LABEL_TEXT1.Text} AND {KM_LABEL_TEXT2.Text}";
            }


            //PRICE
            if (!string.IsNullOrWhiteSpace(PRICE_LABEL_TEXT1.Text) && string.IsNullOrWhiteSpace(PRICE_LABEL_TEXT2.Text))
            {
                query += $" AND car_PRICE >= {PRICE_LABEL_TEXT1.Text}";
            }
            else if (string.IsNullOrWhiteSpace(PRICE_LABEL_TEXT1.Text) && !string.IsNullOrWhiteSpace(PRICE_LABEL_TEXT2.Text))
            {
                query += $" AND car_PRICE <= {PRICE_LABEL_TEXT2.Text}";
            }
            else if (!string.IsNullOrWhiteSpace(PRICE_LABEL_TEXT1.Text) && !string.IsNullOrWhiteSpace(PRICE_LABEL_TEXT2.Text))
            {
                query += $" AND car_PRICE BETWEEN {PRICE_LABEL_TEXT1.Text} AND {PRICE_LABEL_TEXT2.Text}";
            }


            //YEAR
            if (!string.IsNullOrWhiteSpace(YEAR_LABEL_TEXT1.Text) && string.IsNullOrWhiteSpace(YEAR_LABEL_TEXT2.Text))
            {
                query += $" AND car_MODELYEAR >= {YEAR_LABEL_TEXT1.Text}";
            }
            else if (string.IsNullOrWhiteSpace(YEAR_LABEL_TEXT1.Text) && !string.IsNullOrWhiteSpace(YEAR_LABEL_TEXT2.Text))
            {
                query += $" AND car_MODELYEAR <= {YEAR_LABEL_TEXT2.Text}";
            }
            else if (!string.IsNullOrWhiteSpace(YEAR_LABEL_TEXT1.Text) && !string.IsNullOrWhiteSpace(YEAR_LABEL_TEXT2.Text))
            {
                query += $" AND car_MODELYEAR BETWEEN {YEAR_LABEL_TEXT1.Text} AND {YEAR_LABEL_TEXT2.Text}";
            }
            #endregion

            query += ";";

            Debug.WriteLine("Query: " + query);

            return SQL.GetDataFromDatabase(query);
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