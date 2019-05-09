using System;
using System.Web;
using System.IO;
using BilBixen.Scripts.Helper_Classes;

namespace BilBixen.Pages
{
    public partial class SellYourCar : System.Web.UI.Page
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


        public int AdID;

        protected void Page_Load(object sender, EventArgs e)
        {
            /* move this into a button function
             
            string[] data = plateSearch.GetInfo();

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
            */
            AdID = GenerateID();
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

        protected void uploadFiles_Click(object sender, EventArgs e)
        {
            try
            {
                HttpFileCollection hfc = Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];

                    string ext = System.IO.Path.GetExtension(hpf.FileName);


                    if (hpf.ContentLength > 0)
                    {
                        if (Directory.Exists(Server.MapPath("~/Images/") + AdID.ToString()))
                        {
                            List.Text = "Dir exsists!";
                        }
                        else
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Images/") + AdID.ToString());
                            List.Text = "Dir created!";
                        }
                        hpf.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Images/") + AdID.ToString(), AdID.ToString() + "_" + i.ToString() + ext));
                    }
                }
            }
            catch (Exception ex)
            {
                // Log errors
            }

            //if (AdPictures.HasFiles)
            //{
            //    foreach (HttpPostedFile file in AdPictures.PostedFiles)
            //    {
            //        int picNumber = 0;
            //        string ext = System.IO.Path.GetExtension(AdPictures.PostedFile.FileName);

            //        file.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Images/"), AdID.ToString() + "_" + picNumber.ToString() + ext));
            //        List.Text += String.Format("{0} <br />", AdPictures.PostedFile.FileName);
            //    }
            //}
        }
    }
}