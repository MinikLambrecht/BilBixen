using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BilBixen.Scripts.Helper_Classes;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BilBixen.Pages
{
    public partial class CarAdPage : System.Web.UI.Page
    {
        public string _FIRSTREGISTRATION;
        public string _TOTALWEIGHT;
        public string _COUPLING;
        public string _DOORS;
        public string _MAKE;
        public string _MODEL;
        public string _MODELYEAR;
        public string _COLOR;
        public string _FUELTYPE;

        SearchByLicensePlate platesearch = new SearchByLicensePlate();

        string[] data;


        protected async void Page_Load(object sender, EventArgs e)
        {
            data = await platesearch.GetInfo("CB39909");

            // CE78197
            // CB39909
            int i = 0;

            foreach (string str in data)
            {
                //Debug.WriteLine("DATA " + i + " " + str);

                i++;
            }


            _FIRSTREGISTRATION = data[5];
            _TOTALWEIGHT = data[8];
            _COUPLING = data[12];
            _MAKE = data[14];
            _MODEL = data[15];
            _MODELYEAR = data[18];
            _COLOR = data[19];
            _FUELTYPE = data[24];
        }
    }
}

/*
 
    GetInfo string array setup:

        LICENSEPLATE; // 0
        STATUS; // 1
        STATUSDATE; // 2
        CARTYPE; // 3
        USE; // 4
        FIRSTREGISTRATION; // 5
        VINNUMBER; // 6
        OWNWEIGHT; // 7
        TOTALWEIGHT; // 8
        AXELS; // 9
        PULLINGAXELS; // 10
        SEATS; // 11
        COUPLING; // 12
        DOORS; // 13
        MAKE; // 14
        MODEL; // 15
        VARIANT; // 16
        MODELTYPE; // 17
        MODELYEAR; // 18
        COLOR; // 19
        CHASSISTYPE; // 20
        ENGINECYLINDERS; // 21
        ENGINEVOLUME; // 22
        ENGINEPOWER; // 23
        FUELTYPE; // 24
        REGISTRATIONZIPCODE; // 25

     */
