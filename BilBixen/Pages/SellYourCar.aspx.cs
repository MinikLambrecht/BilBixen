using System;
using System.Web.UI;
using AjaxControlToolkit;
using BilBixen.Scripts.Helper_Classes;



namespace BilBixen.Pages
{
    public partial class SellYourCar : Page
    {
        SearchByLicensePlate plateSearch = new SearchByLicensePlate();

        public string licensePlate;
        public string status;
        public string statusDate;
        public string carType;
        public string use;
        public string firstRegistration;
        public string vinNumber;
        public string ownWeight;
        public string totalWeight;
        public string axels;
        public string pullingAxels;
        public string seats;
        public string coupling;
        public string doors;
        public string make;
        public string model;
        public string variant;
        public string modelType;
        public string modelYear;
        public string color;
        public string chassisType;
        public string engineCyleders;
        public string engineVolume;
        public string enginePower;
        public string fuelType;
        public string regitrationZipcode;

        string plateInput;

        public int AdID;

        protected async void Page_Load(object sender, EventArgs e)
        {
            AdID = GenerateID();

        }

        protected async void LoadData(object sender, EventArgs e)
        {
            plateInput = PlateOrVIN.Value.ToString();
            string[] data = await plateSearch.GetInfo(plateInput);

            licensePlate = data[0];
            status = data[1];
            statusDate = data[2];
            carType = data[3];
            use = data[4];
            firstRegistration = data[5];
            vinNumber = data[6];
            ownWeight = data[7];
            totalWeight = data[8];
            axels = data[9];
            pullingAxels = data[10];
            seats = data[11];
            coupling = data[12];
            doors = data[13];
            make = data[14];
            model = data[15];
            variant = data[16];
            modelType = data[17];
            modelYear = data[18];
            color = data[19];
            chassisType = data[20];
            engineCyleders = data[21];
            engineVolume = data[22];
            enginePower = data[23];
            fuelType = data[24];
            regitrationZipcode = data[25];

            LicensePlatelblText.Text = licensePlate;
            StatuslblText.Text = status;
            StatusDatelblText.Text = statusDate;
            CarTypelblText.Text = carType;
            UselblText.Text = use;
            FirstRegistrationlblText.Text = firstRegistration;
            VINNumberlblText.Text = vinNumber;
            OwnWeightlblText.Text = ownWeight;
            TotalWeightlblText.Text = totalWeight;
            AxelslblText.Text = axels;
            PullingAxelslblText.Text = pullingAxels;
            SeatslblText.Text = seats;
            ClutchlblText.Text = coupling;
            DoorslblText.Text = doors;
            MakelblText.Text = make;
            ModellblText.Text = model;
            VariantlblText.Text = variant;
            ModelTypelblText.Text = modelType;
            ModelYearlblText.Text = modelYear;
            ColorlblText.Text = color;
            ChasisTypelblText.Text = chassisType;
            EngineCylinderslblText.Text = engineCyleders;
            EngineVolumelblText.Text = engineVolume;
            EnginePowerlblText.Text = enginePower;
            FuelTypelblText.Text = fuelType;
            RegistrationZIPlblText.Text = regitrationZipcode;

        }

        public int GenerateID()
        {
            Random rand = new Random();

            string H = DateTime.Now.ToString("HH");
            string M = DateTime.Now.ToString("mm");
            string S = DateTime.Now.ToString("ss");

            string P1 = rand.Next(0, 100).ToString();
            string P2 = rand.Next(0, 100).ToString();

            string UniqueID = H + M + S + P1 + P2;

            return Convert.ToInt32(UniqueID);
        }

        protected void AdPictures_UploadComplete(object sender, AjaxFileUploadEventArgs e)
        {

            try
            {
                //HttpFileCollection hfc = Request.Files;
                //for (int i = 0; i < hfc.Count; i++)
                //{
                //    HttpPostedFile hpf = hfc[i];

                //    string ext = Path.GetExtension(hpf.FileName);


                //    if (hpf.ContentLength > 0)
                //    {
                //        if (Directory.Exists(Server.MapPath("~/Images/") + AdID.ToString()))
                //        {
                //            List.Text = "Dir exsists!";
                //        }
                //        else
                //        {
                //            Directory.CreateDirectory(Server.MapPath("~/Images/") + AdID.ToString());
                //            List.Text = "Dir created!";
                //        }
                //        hpf.SaveAs(Path.Combine(Server.MapPath("~/Images/") + AdID.ToString(), AdID.ToString() + "_" + i.ToString() + ext));
                //    }
                //}
            }
            catch (Exception ex)
            {
                // Log errors
            }
        }
    }
}