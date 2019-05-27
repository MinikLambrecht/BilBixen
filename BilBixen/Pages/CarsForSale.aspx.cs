using BilBixen.Scripts.Helper_Classes;
using System;
using System.Data;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BilBixen.Pages
{
    public partial class CarsForSale : Page
    {
        private readonly MySqlHelper _SQL = new MySqlHelper();
        private readonly ImageFileController _FILE = new ImageFileController();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
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

        protected void MakeSelect_OnChange(object sender, EventArgs e)
        {
            MODEL_LABEL_DROP.Enabled = true;

            var count = MODEL_LABEL_DROP.Items.Count;

            for (var i = 1; i < count; i++)
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

        private void UpdateSearch()
        {
            var data = GetSearchResultsFromDatabase();

            GetSearchResultAmount(data);

            CreateCarCards(data);
        }

        private void CreateCarCards(DataRow[] carData)
        {
            if (carData == null) throw new ArgumentNullException(nameof(carData));
            CarGalleryContainer.InnerHtml = "";

            CarGalleryContainer.InnerHtml += "<div class=\"row\">";

            var i = 0;

            foreach (var row in carData)
            {
                var files = ImageFileController.GetImagesFromFolder(row["car_AD_PAGE_ID"].ToString());

                float calc = i % 3;

                if (Math.Abs(Math.Abs(calc)) < 0 && i != 0)
                {
                    CarGalleryContainer.InnerHtml += "</div> " +
                        " <br /> " +
                        "<div class=\"row\">";
                }

                var cardSetup = "" +
                    "<div class=\"col-md-4\">" +
                        "<div class=\"thumbnail ItemCard\" id=\"SearchResults\">" +
                            $"<img class=\"ItemCardImage\" src=\"/Images/{row["car_AD_PAGE_ID"]}/{files[0].Name}\">" +
                            "<br />" +
                            "<div class=\"caption ContentBlock ItemCardDesc\">" +
                                $"<h3>{row["car_BRAND"]}, {row["car_MODEL"]} </h3>" +
                                $"<h4>{row["car_ENGINE"]}</h4>" +
                                $"<p>KM: {row["car_KM"]}</p>" +
                                $"<p>{row["car_PRICE"]},- DKK</p>" +
                                "<p>" +
                                    $"<a href = \"/AD/{row["car_AD_PAGE_ID"]}\" class=\"btn btn-primary\" role=\"button\">View Car</a>" +
                                "</p>" +
                            "</div>" +
                        "</div>" +
                    "</div>";
                
                CarGalleryContainer.InnerHtml += cardSetup;

                i++;
            }
            CarGalleryContainer.InnerHtml += "</div>";
        }

        private void GetSearchResultAmount(DataRow[] cars)
        {
            if (cars == null) throw new ArgumentNullException(nameof(cars));
            var i = cars.Length;

            SearchResultsLabel.Text = $"{i} : Results";
        }

        private DataRow[] GetSearchResultsFromDatabase()
        {
            var query = "SELECT * FROM bilbixen.all_cars " +
                "WHERE car_STATUS = 'For Sale'";
            
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

            query += ";";

            Debug.WriteLine("Query: " + query);

            return MySqlHelper.GetDataFromDatabase(query);
        }

        private void FillMakeDropdownFromDatabase()
        {
            var query = "Select * from bilbixen.brands";

            var rows = MySqlHelper.GetDataFromDatabase(query);

            foreach (var row in rows)
            {
                var item = new ListItem {Value = row["brand_ID"].ToString(), Text = row["brand_NAME"].ToString()};

                MAKE_LABEL_DROP.Items.Add(item);
            }

        }

        private void FillModelDropdownFromDatabase(int brandId)
        {
            var query = "Select * from bilbixen.models " +
                $"where model_BRAND = {brandId};";

            var rows = MySqlHelper.GetDataFromDatabase(query);

            foreach (var row in rows)
            {
                var item = new ListItem {Value = row["model_ID"].ToString(), Text = row["model_NAME"].ToString()};

                MODEL_LABEL_DROP.Items.Add(item);
            }
        }

        private void FillFuelDropdownFromDatabase()
        {
            var query = "Select * from bilbixen.fuels";

            var rows = MySqlHelper.GetDataFromDatabase(query);

            foreach (var row in rows)
            {
                var item = new ListItem {Value = row["fuel_ID"].ToString(), Text = row["fuel_TYPE"].ToString()};

                FUEL_LABEL_DROP.Items.Add(item);
            }
        }

        private void FillCarTypeDropdownFromDatabase()
        {
            var query = "Select * from bilbixen.categories";

            var rows = MySqlHelper.GetDataFromDatabase(query);

            foreach (var row in rows)
            {
                var item = new ListItem {Value = row["category_ID"].ToString(), Text = row["category_NAME"].ToString()};

                CAR_TYPE_LABEL_DROP.Items.Add(item);
            }
        }
    }
}