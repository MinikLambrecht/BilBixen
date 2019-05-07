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

        string _LICENSEPLATE;
        string _STATUS;
        string _STATUSDATE;
        string _CARTYPE;
        string _USE;
        string _FIRSTREGISTRATION;
        string _VINNUMBER;
        string _OWNWEIGHT;
        string _TOTALWEIGHT;
        string _AXELS;
        string _PULLINGAXELS;
        string _SEATS;
        string _COUPLING;
        string _DOORS;
        string _MAKE;
        string _MODEL;
        string _VARIANT;
        string _MODELTYPE;
        string _MODELYEAR;
        string _COLOR;
        string _CHASSISTYPE;
        string _ENGINECYLINDERS;
        string _ENGINEVOLUME;
        string _ENGINEPOWER;
        string _FUELTYPE;
        string _REGISTRATIONZIPCODE;


        public void SetInfo()
        {
            Task.Run(async () => await GetResponseString());
        }

        async Task GetResponseString()
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

            _LICENSEPLATE = finalCollection[0];
            _STATUS = finalCollection[1];
            _STATUSDATE = finalCollection[2];
            _CARTYPE = finalCollection[3];
            _USE = finalCollection[4];
            _FIRSTREGISTRATION = finalCollection[5];
            _VINNUMBER = finalCollection[6];
            _OWNWEIGHT = finalCollection[7];
            _TOTALWEIGHT = finalCollection[8];
            _AXELS = finalCollection[9];
            _PULLINGAXELS = finalCollection[10];
            _SEATS = finalCollection[11];
            _COUPLING = finalCollection[12];
            _DOORS = finalCollection[13];
            _MAKE = finalCollection[14];
            _MODEL = finalCollection[15];
            _COLOR = finalCollection[16];
            _CHASSISTYPE = finalCollection[17];
            _ENGINECYLINDERS = finalCollection[18];
            _ENGINEVOLUME = finalCollection[19];
            _ENGINEPOWER = finalCollection[20];
            _FUELTYPE = finalCollection[21];
            _REGISTRATIONZIPCODE = finalCollection[22];
        }
    }
}