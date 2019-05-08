using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace BilBixen.Scripts.Helper_Classes
{
    public class SearchByLicensePlate
    {
        private string token = "jpdjnjobzpt5x7irdmu745h64o22hr6a";

        public string _LICENSEPLATE;
        public string _STATUS;
        public string _STATUSDATE;
        public string _CARTYPE;
        public string _USE;
        public string _FIRSTREGISTRATION;
        public string _VINNUMBER;
        public string _OWNWEIGHT;
        public string _TOTALWEIGHT;
        public string _AXELS;
        public string _PULLINGAXELS;
        public string _SEATS;
        public string _COUPLING;
        public string _DOORS;
        public string _MAKE;
        public string _MODEL;
        public string _VARIANT;
        public string _MODELTYPE;
        public string _MODELYEAR;
        public string _COLOR;
        public string _CHASSISTYPE;
        public string _ENGINECYLINDERS;
        public string _ENGINEVOLUME;
        public string _ENGINEPOWER;
        public string _FUELTYPE;
        public string _REGISTRATIONZIPCODE;

        string[] final = new string[26];

        public string[] GetInfo()
        {
            GetResponseString();

            return final;
        }

        async void GetResponseString()
        {
            string result;

            using (var request = new HttpClient())
            {
                request.DefaultRequestHeaders.Add("X-AUTH-TOKEN", token);
                request.DefaultRequestHeaders.Accept.Clear();
                request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                result = await request.GetStringAsync("https://v1.motorapi.dk/vehicles/CE78197");
            }

            string temp;

            temp = result.Replace("null,", "\"null\",");

            result = temp;

            string[] resultarray = result.Split(new string[] { ": \"" }, StringSplitOptions.None);

            int i = 0;

            List<string> finalCollection = new List<string>();

            foreach (string item in resultarray)
            {
                string[] tempo = item.Split(new string[] { "\"," }, StringSplitOptions.None);

                finalCollection.Add(tempo[0]);

                i++;
            }

            finalCollection.RemoveAt(0);
            finalCollection.RemoveAt(finalCollection.Count);

            _LICENSEPLATE = final[0];
            _STATUS = final[1];
            _STATUSDATE = final[2];
            _CARTYPE = final[3];
            _USE = final[4];
            _FIRSTREGISTRATION = final[5];
            _VINNUMBER = final[6];
            _OWNWEIGHT = final[7];
            _TOTALWEIGHT = final[8];
            _AXELS = final[9];
            _PULLINGAXELS = final[10];
            _SEATS = final[11];
            _COUPLING = final[12];
            _DOORS = final[13];
            _MAKE = final[14];
            _MODEL = final[15];
            _VARIANT = final[16];
            _MODELTYPE = final[17];
            _MODELYEAR = final[18];
            _COLOR = final[19];
            _CHASSISTYPE = final[20];
            _ENGINECYLINDERS = final[21];
            _ENGINEVOLUME = final[22];
            _ENGINEPOWER = final[23];
            _FUELTYPE = final[24];
            _REGISTRATIONZIPCODE = final[25];
        }
    }
}