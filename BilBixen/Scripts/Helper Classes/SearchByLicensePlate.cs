using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;

/*
 
    GetInfo string array setup (Final Output):

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

namespace BilBixen.Scripts.Helper_Classes
{
    public class SearchByLicensePlate
    {
        private string token = "jpdjnjobzpt5x7irdmu745h64o22hr6a";

        public string _LICENSEPLATE; // 0
        public string _STATUS; // 1
        public string _STATUSDATE; // 2
        public string _CARTYPE; // 3
        public string _USE; // 4
        public string _FIRSTREGISTRATION; // 5
        public string _VINNUMBER; // 6
        public string _OWNWEIGHT; // 7
        public string _TOTALWEIGHT; // 8
        public string _AXELS; // 9
        public string _PULLINGAXELS; // 10
        public string _SEATS; // 11
        public string _COUPLING; // 12
        public string _DOORS; // 13
        public string _MAKE; // 14
        public string _MODEL; // 15
        public string _VARIANT; // 16
        public string _MODELTYPE; // 17
        public string _MODELYEAR; // 18
        public string _COLOR; // 19
        public string _CHASSISTYPE; // 20
        public string _ENGINECYLINDERS; // 21
        public string _ENGINEVOLUME; // 22
        public string _ENGINEPOWER; // 23
        public string _FUELTYPE; // 24
        public string _REGISTRATIONZIPCODE; // 25

        string[] final;

        public async Task<string[]> GetInfo(string plate)
        {
            await GetResponseString(plate);

            //Debug.WriteLine(String.Join(",", final));

            return final;
        }

        async Task GetResponseString(string Lplate)
        {
            string result;

            using (var request = new HttpClient())
            {
                request.DefaultRequestHeaders.Add("X-AUTH-TOKEN", token);
                request.DefaultRequestHeaders.Accept.Clear();
                request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                result = await request.GetStringAsync("https://v1.motorapi.dk/vehicles/" + Lplate.ToUpper());
            }

            string temp;

            temp = result.Replace("null,", "\"null\",");

            result = temp;

            temp = result.Replace("false,", "\"false\",");

            result = temp;

            temp = result.Replace("true,", "\"true\",");

            result = temp;

            string[] resultarray = result.Split(new string[] { ": \"" }, StringSplitOptions.None);

            //Debug.WriteLine(String.Join(", ", resultarray));

            int i = 0;

            List<string> finalCollection = new List<string>();

            foreach (string item in resultarray)
            {
                string[] tempo = item.Split(new string[] { "\"," }, StringSplitOptions.None);

                finalCollection.Add(tempo[0]);

                i++;
            }

            //Debug.WriteLine($"Before------------------------------ {Environment.NewLine} {String.Join(",", finalCollection)} {Environment.NewLine + Environment.NewLine} ");

            finalCollection.RemoveAt(0);
            finalCollection.RemoveAt(finalCollection.Count - 1);

            //Debug.WriteLine($"After------------------------------ {Environment.NewLine} {String.Join(",", finalCollection)} {Environment.NewLine + Environment.NewLine} ");


            final = finalCollection.ToArray();

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