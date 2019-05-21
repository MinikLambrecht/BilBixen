using BilBixen.Scripts.Helper_Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace BilBixen.Pages
{
    public partial class Tests : Page
    {
        private readonly SearchByLicensePlate _PLATE_SEARCH = new SearchByLicensePlate();

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["CAR_AD_ID"] = GenerateId();
            }
        }

        protected async void LoadData(object sender, EventArgs e)
        {
            #region Data From Plate search

            _PLATE_INPUT = PLATEORVIN_LABEL_TEXT.Value;
            var data = await _PLATE_SEARCH.GetInfo(_PLATE_INPUT);

            _LICENSE_PLATE = data[0];
            _CAR_TYPE = data[3];
            _FIRST_REGISTRATION = data[5];
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
            MAKE_LABEL_TEXT.Value = _MAKE;
            MODEL_LABEL_TEXT.Value = _MODEL;
            VARIANT_LABEL_TEXT.Value = _VARIANT;
            MODEL_TYPE_LABEL_TEXT.Value = _MODEL_TYPE;
            MODEL_YEAR_LABEL_TEXT.Value = _MODEL_YEAR;
            ENGINE_POWER_LABEL_TEXT.Value = _ENGINE_POWER;
            FUEL_TYPE_LABEL_TEXT.Value = _FUEL_TYPE;

            #endregion


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

        void FillDropdownsFromDatabase()
        {
            string query = "Select * from bilbixen.all_cars " +
                $"where car_CATEGORY = 'Passenger' and car_STATUS = 'For Sale';";

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn);

                var dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                var rows = dt.AsEnumerable().ToArray();

                foreach (DataRow row in rows)
                {
                    // row["model_NAME"]
                }
            }
        }

        protected void SubmitAdbtn_OnClick(object sender, EventArgs e)
        {

        }

        protected void BtnUpload_OnClick(object sender, EventArgs e)
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

                ADID_LABEL.Text = "File size not exceeded!";

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
                        ADID_LABEL.Text = "Dir created!";
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
    }
}