using BilBixen.Scripts.Helper_Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BilBixen.Pages
{
    public partial class SellYourCar : Page
    {
        private readonly SearchByLicensePlate _PLATE_SEARCH = new SearchByLicensePlate();
        MySQL_Helper SQL = new MySQL_Helper();


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

        private bool usedPlateSearch;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["CAR_AD_ID"] = GenerateId();

                #region Fill Dropdowns

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

                #endregion
            }
        }

        protected async void LoadData(object sender, EventArgs e)
        {
            try
            {
                #region Data From Plate search

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
                    Custom_Make.Text = _MAKE;
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
                    Custom_Model.Text = _MODEL;
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

                #endregion

                usedPlateSearch = true;
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

        void SetModelSelectAfterPlateSearch()
        {
            MODEL_LABEL_TEXT.Enabled = true;

            int count = MODEL_LABEL_TEXT.Items.Count;

            for (int i = 1; i < count; i++)
            {
                MODEL_LABEL_TEXT.Items.RemoveAt(1);
            }

            FillModelDropdownFromDatabase(int.Parse(MAKE_LABEL_TEXT.SelectedValue));
        }

        protected void MakeSelect_OnChange(object sender, EventArgs e)
        {
            MODEL_LABEL_TEXT.Enabled = true;

            int count = MODEL_LABEL_TEXT.Items.Count;

            for (int i = 1; i < count; i++)
            {
                MODEL_LABEL_TEXT.Items.RemoveAt(1);
            }

            if (usedPlateSearch)
            {
                Custom_Model.Text = "";
            }
            MODEL_LABEL_TEXT.SelectedValue = "0";

            FillModelDropdownFromDatabase(int.Parse(MAKE_LABEL_TEXT.SelectedValue));
        }

        protected void SubmitAdbtn_OnClick(object sender, EventArgs e)
        {
            try
            {
                UploadImages();

                if (MAKE_LABEL_TEXT.SelectedValue == "0")
                {
                    AddNewMakeToDatabase();
                }
                if (MODEL_LABEL_TEXT.SelectedValue == "0")
                {
                    AddNewModelToDatabase();
                }

                int brandId = GetBrandIdFromDatabase(MAKE_LABEL_TEXT.SelectedItem.Text);

                int modelId = GetModelIdFromDatabase(MODEL_LABEL_TEXT.SelectedItem.Text);

                string query = "Insert into `cars` " +
                    "(car_BRAND, car_MODEL, car_KM, car_ENGINE, car_FUEL," +
                    " car_DOORS, car_FIRST_REGISTRATION, car_PRICE, car_CATEGORY," +
                    " car_AD_PAGE_ID, car_PLATE, car_MODELYEAR, car_SALES_USER_ID) " +
                    "values " +
                    $"({brandId}, {modelId}, {KM_LABEL_TEXT.Value}, '{VARIANT_LABEL_TEXT.Value}'," +
                    $"{FUEL_TYPE_LABEL_TEXT.Value}, {DOORS_LABEL_TEXT.Value}," +
                    $"{FIRST_REGISTRATION_LABEL_TEXT.Value}, {PRICE_LABEL_TEXT.Value}," +
                    $"{CAR_TYPE_LABEL_TEXT.Value}, '{ViewState["CAR_AD_ID"]}'," +
                    $"{PLATEORVIN_LABEL_TEXT.Value}, {MODEL_YEAR_LABEL_TEXT.Value});";

                SQL.SetDataToDatabase(query);
                
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
                    var ext = Path.GetExtension(fileDocument.FileName);

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

                    //if (fileCount == 0)
                    //{
                    //    ADID_LABEL.Text = "<0";
                    //    for (var i = 0; i < fileCount; i++)
                    //    {
                    //        fileDocument.PostedFile.SaveAs(Path.Combine(savePath, "/IMG" + "_" + i + ext));
                    //    }
                    //    ADID_LABEL.Text = "<0 Complete";
                    //}
                    //else
                    //{
                    //    ADID_LABEL.Text = ">0";
                    //    for (var i = 0; i < fileCount; i++)
                    //    {
                    //        fileDocument.PostedFile.SaveAs(Path.Combine(savePath, "/IMG" + "_" + i + ext));
                    //    }
                    //    ADID_LABEL.Text = ">0 Complete";
                    //}

                    //infoLabel.Text = "Images uploaded successfully.";
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

        void AddNewMakeToDatabase()
        {
            string query = "Insert into `brands` (brand_NAME) " +
                    $"values ('{MAKE_LABEL_TEXT.SelectedItem.Text}');";

            SQL.SetDataToDatabase(query);
        }

        void AddNewModelToDatabase()
        {
            int makeID = GetBrandIdFromDatabase(MAKE_LABEL_TEXT.SelectedItem.Text);

            string query = "Insert into `models` (model_NAME, model_BRAND, model_CATEGORY) " +
                    $"values ('{MODEL_LABEL_TEXT.SelectedItem.Text}', '{makeID}', '{CAR_TYPE_LABEL_TEXT.Value}');";

            SQL.SetDataToDatabase(query);
        }

        int GetBrandIdFromDatabase(string brandName)
        {
            int result = 0;

            string query = "Select * from bilbixen.brands " +
                $"where brand_NAME = '{brandName}';";

            var rows = SQL.GetDataFromDatabase(query);

            int i = 0;

            foreach (DataRow row in rows)
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

        int GetModelIdFromDatabase(string modelName)
        {
            int result = 0;

            string query = "Select * from bilbixen.models " +
                $"where model_NAME = '{modelName}';";

            var rows = SQL.GetDataFromDatabase(query);

            int i = 0;

            foreach (DataRow row in rows)
            {
                if (rows[i]["model_NAME"].ToString() == MAKE_LABEL_TEXT.SelectedItem.Text)
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

                MAKE_LABEL_TEXT.Items.Add(item);
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

                MODEL_LABEL_TEXT.Items.Add(item);
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

                FUEL_TYPE_LABEL_TEXT.Items.Add(item);
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

                CAR_TYPE_LABEL_TEXT.Items.Add(item);
            }
        }

        #endregion
    }
}