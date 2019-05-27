using BilBixen.Scripts.Helper_Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BilBixen.Pages
{
    public partial class SellYourCar : Page
    {
        private readonly SearchByLicensePlate _PLATE_SEARCH = new SearchByLicensePlate();
        private readonly MySqlHelper _SQL = new MySqlHelper();


        private string _LICENSE_PLATE;
        private string _CAR_TYPE;
        private string _FIRST_REGISTRATION;
        private string _VIN_NUMBER;
        private string _OWN_WEIGHT;
        private string _TOTAL_WEIGHT;
        private string _SEATS;
        private string _DOORS;
        private string _MAKE;
        private string _MODEL;
        private string _VARIANT;
        private string _MODEL_TYPE;
        private string _MODEL_YEAR;
        private string _ENGINE_POWER;
        private string _FUEL_TYPE;

        private string _PLATE_INPUT;

        private bool _USED_PLATE_SEARCH;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
            ViewState["CAR_AD_ID"] = GenerateId();

            try
            {
                FillCarTypeDropdownFromDatabase();

                FillFuelDropdownFromDatabase();

                FillMakeDropdownFromDatabase();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected async void LoadData(object sender, EventArgs e)
        {
            try
            {
                _PLATE_INPUT = PLATEORVIN_LABEL_TEXT.Value;
                var data = await _PLATE_SEARCH.GetInfo(_PLATE_INPUT);

                _LICENSE_PLATE = data[0];
                _CAR_TYPE = data[3];
                _FIRST_REGISTRATION = data[5].Split('+')[0];
                _VIN_NUMBER = data[6];
                _OWN_WEIGHT = data[7];
                _TOTAL_WEIGHT = data[8];
                _SEATS = data[11];
                _DOORS = data[13];
                _MAKE = data[14];
                _MODEL = data[15];
                _VARIANT = data[16];
                _MODEL_TYPE = data[17];
                _MODEL_YEAR = data[18];
                _ENGINE_POWER = data[23];
                _FUEL_TYPE = data[24];

                LICENSE_PLATE_TEXT.Value = _LICENSE_PLATE;
                CAR_TYPE_LABEL_TEXT.Value = _CAR_TYPE;
                FIRST_REGISTRATION_LABEL_TEXT.Value = _FIRST_REGISTRATION;
                VIN_NUMBER_LABEL_TEXT.Value = _VIN_NUMBER;
                OWN_WEIGHT_LABEL_TEXT.Value = _OWN_WEIGHT;
                TOTAL_WEIGHT_LABEL_TEXT.Value = _TOTAL_WEIGHT;
                SEATS_LABEL_TEXT.Value = _SEATS;
                DOORS_LABEL_TEXT.Value = _DOORS;

                //Brand
                bool exists = false;
                int i = 0;
                foreach (ListItem item in MAKE_LABEL_TEXT.Items)
                {
                    if (item.Text.ToUpper() == _MAKE.ToUpper())
                    {
                        exists = true;
                    }
                    else
                    {
                        i++;
                    }
                }

                if (!exists)
                {
                    MODEL_LABEL_TEXT.Items[0].Text = _MAKE;
                    MAKE_LABEL_TEXT.SelectedValue = "0";
                }
                else
                {
                    MAKE_LABEL_TEXT.SelectedIndex = i;
                }

                SetModelSelectAfterPlateSearch();


                //Model
                exists = false;
                i = 0;
                foreach (ListItem item in MODEL_LABEL_TEXT.Items)
                {
                    if (item.Text.ToUpper() == _MODEL.ToUpper())
                    {
                        exists = true;
                    }
                    else
                    {
                        i++;
                    }
                }

                if (!exists)
                {
                    MODEL_LABEL_TEXT.Items[0].Text = _MODEL;
                    MODEL_LABEL_TEXT.SelectedValue = "0";
                }
                else
                {
                    MODEL_LABEL_TEXT.SelectedIndex = i;
                }

                VARIANT_LABEL_TEXT.Value = _VARIANT;
                MODEL_TYPE_LABEL_TEXT.Value = _MODEL_TYPE;
                MODEL_YEAR_LABEL_TEXT.Value = _MODEL_YEAR;
                ENGINE_POWER_LABEL_TEXT.Value = _ENGINE_POWER;
                FUEL_TYPE_LABEL_TEXT.Value = _FUEL_TYPE;

                _USED_PLATE_SEARCH = true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private static int GenerateId()
        {
            var rand = new Random();

            var h = DateTime.Now.ToString("HH");
            var m = DateTime.Now.ToString("mm");
            var s = DateTime.Now.ToString("ss");

            var p1 = rand.Next(0, 100).ToString();
            var p2 = rand.Next(0, 100).ToString();

            var uniqueId = h + m + s + p1 + p2;

            return Convert.ToInt32(uniqueId);
        }

        private void SetModelSelectAfterPlateSearch()
        {
            MODEL_LABEL_TEXT.Enabled = true;

            var count = MODEL_LABEL_TEXT.Items.Count;

            for (var i = 1; i < count; i++)
            {
                MODEL_LABEL_TEXT.Items.RemoveAt(1);
            }

            FillModelDropdownFromDatabase(int.Parse(MAKE_LABEL_TEXT.SelectedValue));
        }

        protected void MakeSelect_OnChange(object sender, EventArgs e)
        {
            MODEL_LABEL_TEXT.Enabled = true;

            var count = MODEL_LABEL_TEXT.Items.Count;

            for (var i = 1; i < count; i++)
            {
                MODEL_LABEL_TEXT.Items.RemoveAt(1);
            }

            if (_USED_PLATE_SEARCH)
            {
                MODEL_LABEL_TEXT.Items[0].Text = "";
            }
            MODEL_LABEL_TEXT.SelectedValue = "0";

            FillModelDropdownFromDatabase(int.Parse(MAKE_LABEL_TEXT.SelectedValue));
        }

        protected void SubmitAD_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(MODEL_LABEL_TEXT.SelectedItem.Text) &&
                    string.IsNullOrWhiteSpace(MAKE_LABEL_TEXT.SelectedItem.Text)) return;
                if (MAKE_LABEL_TEXT.SelectedValue == "0")
                {
                    AddNewMakeToDatabase();
                }
                if (MODEL_LABEL_TEXT.SelectedValue == "0")
                {
                    AddNewModelToDatabase();
                }

                var brandId = GetBrandIdFromDatabase(MAKE_LABEL_TEXT.SelectedItem.Text);

                var modelId = GetModelIdFromDatabase(MODEL_LABEL_TEXT.SelectedItem.Text);

                var currentUser = Membership.GetUser(User.Identity.Name);

                var userId = currentUser != null ? int.Parse(currentUser.ProviderUserKey?.ToString() ?? throw new InvalidOperationException()) : 0;

                if (MODEL_YEAR_LABEL_TEXT.Value == "0" || MODEL_YEAR_LABEL_TEXT.Value == null)
                {
                    MODEL_YEAR_LABEL_TEXT.Value = FIRST_REGISTRATION_LABEL_TEXT.Value.Split('-')[0];
                }

                string query = "Insert into `cars` " +
                               "(car_BRAND, car_MODEL, car_KM, car_ENGINE, car_FUEL," +
                               " car_DOORS, car_FIRST_REGISTRATION, car_PRICE, car_CATEGORY," +
                               " car_AD_PAGE_ID, car_PLATE, car_MODELYEAR, car_SALES_USER_ID) " +
                               "values " +
                               $"({brandId}, {modelId}, {KM_LABEL_TEXT.Value}, '{VARIANT_LABEL_TEXT.Value}'," +
                               $"{FUEL_TYPE_LABEL_TEXT.Value}, {DOORS_LABEL_TEXT.Value}," +
                               $"'{FIRST_REGISTRATION_LABEL_TEXT.Value}', {PRICE_LABEL_TEXT.Value}," +
                               $"{CAR_TYPE_LABEL_TEXT.Value}, '{ViewState["CAR_AD_ID"]}'," +
                               $"'{LICENSE_PLATE_TEXT.Value}', {MODEL_YEAR_LABEL_TEXT.Value}, {userId});";

                _SQL.SetDataToDatabase(query);

                UploadImages();

                Response.RedirectToRoute("Home_Page");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        void UploadImages()
        {
            if (!fileDocument.HasFiles)
            {
                infoLabel.Text = "No files selected!";
                return;
            }

            try
            {
                var docFileLength = fileDocument.FileBytes.Length;
                var fileName = fileDocument.PostedFile.FileName;

                var savePath = Server.MapPath("~/Images/") + ViewState["CAR_AD_ID"];

                if (docFileLength > 4096000)
                {
                    infoLabel.Text = "Image exceeds the size limit of 4 MB.";
                    return;
                }

                //ADID_LABEL.Text = "File size not exceeded!";

                if (fileName != string.Empty)
                {
                    var ext = Path.GetExtension(fileDocument.FileName)?.ToLower();

                    if (!((ext == ".jpeg") | (ext == ".jpg") | (ext == ".png")))
                    {
                        return;
                    }

                    if (!Directory.Exists(savePath))
                    {
                        Directory.CreateDirectory(savePath);
                        //ADID_LABEL.Text = "Dir created!";
                    }

                    string[] files = Directory.GetFiles(savePath, "*.*", SearchOption.TopDirectoryOnly);
                    List<string> fileNames = new List<string>();
                    List<int> highestNumber = new List<int>();

                    if (files.Length > 0)
                    {
                        foreach (string str in files)
                        {
                            fileNames.Add(Path.GetFileNameWithoutExtension(str));
                        }

                        foreach (var name in fileNames)
                        {
                            var fileNumber = name.Substring(name.LastIndexOf('_') + 1);
                            highestNumber.Add(int.Parse(fileNumber));

                            int value = highestNumber.Max();

                            int newFileNumber = value + 1;

                            fileDocument.PostedFile.SaveAs(Path.Combine(savePath, "IMG" + "_" + newFileNumber + ext));
                        }
                    }
                    else
                    {
                        fileDocument.PostedFile.SaveAs(Path.Combine(savePath, "IMG" + "_" + "0" + ext));
                    }
                }
                else
                {
                    infoLabel.Text = "File not found.";
                }
            }
            catch (Exception ex)
            {
                infoLabel.Text = ex.Message;
            }
        }

        private void AddNewMakeToDatabase()
        {
            var query = "Insert into `brands` (brand_NAME) " +
                    $"values ('{MAKE_LABEL_TEXT.SelectedItem.Text}');";

            _SQL.SetDataToDatabase(query);
        }

        private void AddNewModelToDatabase()
        {
            var makeId = GetBrandIdFromDatabase(MAKE_LABEL_TEXT.SelectedItem.Text);

            var query = "Insert into `models` (model_NAME, model_BRAND, model_CATEGORY) " +
                    $"values ('{MODEL_LABEL_TEXT.SelectedItem.Text}', '{makeId}', '{CAR_TYPE_LABEL_TEXT.Value}');";

            _SQL.SetDataToDatabase(query);
        }

        private int GetBrandIdFromDatabase(string brandName)
        {
            var result = 0;

            var query = "Select * from bilbixen.brands " +
                $"where brand_NAME = '{brandName}';";

            var rows = MySqlHelper.GetDataFromDatabase(query);

            var i = 0;

            foreach (var unused in rows)
            {
                if (rows[i]["brand_NAME"].ToString() == MAKE_LABEL_TEXT.SelectedItem.Text)
                {
                    result = int.Parse(rows[i]["brand_ID"].ToString());
                }
                else
                {
                    i++;
                }
            }

            return result;
        }

        private int GetModelIdFromDatabase(string modelName)
        {
            var result = 0;

            var query = "Select * from bilbixen.models " +
                $"where model_NAME = '{modelName}';";

            var rows = MySqlHelper.GetDataFromDatabase(query);

            var i = 0;

            foreach (var unused in rows)
            {
                if (rows[i]["model_NAME"].ToString() == MODEL_LABEL_TEXT.SelectedItem.Text)
                {
                    result = int.Parse(rows[i]["model_ID"].ToString());
                }
                else
                {
                    i++;
                }
            }

            return result;
        }

        private void FillMakeDropdownFromDatabase()
        {
            const string query = "Select * from bilbixen.brands";

            var rows = MySqlHelper.GetDataFromDatabase(query);

            foreach (var row in rows)
            {
                var item = new ListItem {Value = row["brand_ID"].ToString(), Text = row["brand_NAME"].ToString()};

                MAKE_LABEL_TEXT.Items.Add(item);
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


                MODEL_LABEL_TEXT.Items.Add(item);
            }
        }

        private void FillFuelDropdownFromDatabase()
        {
            const string query = "Select * from bilbixen.fuels";

            var rows = MySqlHelper.GetDataFromDatabase(query);

            foreach (var row in rows)
            {
                var item = new ListItem {Value = row["fuel_ID"].ToString(), Text = row["fuel_TYPE"].ToString()};

                FUEL_TYPE_LABEL_TEXT.Items.Add(item);
            }
        }

        private void FillCarTypeDropdownFromDatabase()
        {
            const string query = "Select * from bilbixen.categories";

            var rows = MySqlHelper.GetDataFromDatabase(query);

            foreach (var row in rows)
            {
                var item = new ListItem {Value = row["category_ID"].ToString(), Text = row["category_NAME"].ToString()};

                CAR_TYPE_LABEL_TEXT.Items.Add(item);
            }
        }
    }
}