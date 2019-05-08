using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BilBixen.Scripts.Helper_Classes;

namespace BilBixen.Pages
{
    public partial class CarAdPage : System.Web.UI.Page
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


        protected void Page_Load(object sender, EventArgs e)
        {
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
        }
    }
}