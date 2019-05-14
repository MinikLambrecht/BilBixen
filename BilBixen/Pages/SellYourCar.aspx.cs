using BilBixen.Scripts.Helper_Classes;
using System;
using System.IO;
using System.Web.UI;

namespace BilBixen.Pages
{
    public partial class SellYourCar : Page
    {
        private readonly SearchByLicensePlate _PLATE_SEARCH = new SearchByLicensePlate();

        private string _LICENSE_PLATE;
        private string _STATUS;
        private string _STATUS_DATE;
        private string _CAR_TYPE;
        private string _USE;
        private string _FIRST_REGISTRATION;
        private string _VIN_NUMBER;
        private string _OWN_WEIGHT;
        private string _TOTAL_WEIGHT;
        private string _AXLES;
        private string _PULLING_AXLES;
        private string _SEATS;
        private string _COUPLING;
        private string _DOORS;
        private string _MAKE;
        private string _MODEL;
        private string _VARIANT;
        private string _MODEL_TYPE;
        private string _MODEL_YEAR;
        private string _COLOR;
        private string _CHASSIS_TYPE;
        private string _ENGINE_CYLINDERS;
        private string _ENGINE_VOLUME;
        private string _ENGINE_POWER;
        private string _FUEL_TYPE;
        private string _REGISTRATION_ZIPCODE;

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
            _PLATE_INPUT = PLATEORVIN_LABEL_TEXT.Value;
            var data = await _PLATE_SEARCH.GetInfo(_PLATE_INPUT);

            _LICENSE_PLATE = data[0];
            _STATUS = data[1];
            _STATUS_DATE = data[2];
            _CAR_TYPE = data[3];
            _USE = data[4];
            _FIRST_REGISTRATION = data[5];
            _VIN_NUMBER = data[6];
            _OWN_WEIGHT = data[7];
            _TOTAL_WEIGHT = data[8];
            _AXLES = data[9];
            _PULLING_AXLES = data[10];
            _SEATS = data[11];
            _COUPLING = data[12];
            _DOORS = data[13];
            _MAKE = data[14];
            _MODEL = data[15];
            _VARIANT = data[16];
            _MODEL_TYPE = data[17];
            _MODEL_YEAR = data[18];
            _COLOR = data[19];
            _CHASSIS_TYPE = data[20];
            _ENGINE_CYLINDERS = data[21];
            _ENGINE_VOLUME = data[22];
            _ENGINE_POWER = data[23];
            _FUEL_TYPE = data[24];
            _REGISTRATION_ZIPCODE = data[25];

            LICENSE_PLATE_TEXT.Text = _LICENSE_PLATE;
            STATUS_LABEL_TEXT.Text = _STATUS;
            STATUS_DATE_LABEL_TEXT.Text = _STATUS_DATE;
            CAR_TYPE_LABEL_TEXT.Text = _CAR_TYPE;
            USE_LABEL_TEXT.Text = _USE;
            FIRST_REGISTRATION_LABEL_TEXT.Text = _FIRST_REGISTRATION;
            VIN_NUMBER_LABEL_TEXT.Text = _VIN_NUMBER;
            OWN_WEIGHT_LABEL_TEXT.Text = _OWN_WEIGHT;
            TOTAL_WEIGHT_LABEL_TEXT.Text = _TOTAL_WEIGHT;
            AXLES_LABEL_TEXT.Text = _AXLES;
            PULLING_AXLES_LABEL_TEXT.Text = _PULLING_AXLES;
            SEATS_LABEL_TEXT.Text = _SEATS;
            CLUTCH_LABEL_TEXT.Text = _COUPLING;
            DOORS_LABEL_TEXT.Text = _DOORS;
            MAKE_LABEL_TEXT.Text = _MAKE;
            MODEL_LABEL_TEXT.Text = _MODEL;
            VARIANT_LABEL_TEXT.Text = _VARIANT;
            MODEL_TYPE_LABEL_TEXT.Text = _MODEL_TYPE;
            MODEL_YEAR_LABEL_TEXT.Text = _MODEL_YEAR;
            COLOR_LABEL_TEXT.Text = _COLOR;
            CHASSIS_LABEL_TEXT.Text = _CHASSIS_TYPE;
            ENGINE_CYLINDERS_LABEL_TEXT.Text = _ENGINE_CYLINDERS;
            ENGINE_VOLUME_LABEL_TEXT.Text = _ENGINE_VOLUME;
            ENGINE_POWER_LABEL_TEXT.Text = _ENGINE_POWER;
            FUEL_TYPE_LABEL_TEXT.Text = _FUEL_TYPE;
            REGISTRATIONZIPCODE_LABEL_TEXT.Text = _REGISTRATION_ZIPCODE;
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

                    var fileCount = Directory.GetFiles(savePath, "*.*", SearchOption.TopDirectoryOnly).Length;

                    ADID_LABEL.Text = fileCount.ToString();

                    for (var i = 0; i < fileCount; i++)
                    {
                        fileDocument.PostedFile.SaveAs(Path.Combine(savePath, "IMG" + "_" + i + ext));
                    }

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