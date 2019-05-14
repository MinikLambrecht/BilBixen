using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
        AXLES; // 9
        PULLINGAXLES; // 10
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
        private const string Token = "jpdjnjobzpt5x7irdmu745h64o22hr6a";

        //private string _LICENSEPLATE; // 0
        //private string _STATUS; // 1
        //private string _STATUSDATE; // 2
        //private string _CARTYPE; // 3
        //private string _USE; // 4
        //private string _FIRSTREGISTRATION; // 5
        //private string _VINNUMBER; // 6
        //private string _OWNWEIGHT; // 7
        //private string _TOTALWEIGHT; // 8
        //private string _AXLES; // 9
        //private string _PULLINGAXLES; // 10
        //private string _SEATS; // 11
        //private string _COUPLING; // 12
        //private string _DOORS; // 13
        //private string _MAKE; // 14
        //private string _MODEL; // 15
        //private string _VARIANT; // 16
        //private string _MODELTYPE; // 17
        //private string _MODELYEAR; // 18
        //private string _COLOR; // 19
        //private string _CHASSISTYPE; // 20
        //private string _ENGINECYLINDERS; // 21
        //private string _ENGINEVOLUME; // 22
        //private string _ENGINEPOWER; // 23
        //private string _FUELTYPE; // 24
        //private string _REGISTRATIONZIPCODE; // 25

        private string[] _FINAL;

        /// <summary>Gets the information.</summary>
        /// <param name="plate">License plate.</param>
        /// <returns>Returns relevant information about the car associated with the license plate.</returns>
        public async Task<string[]> GetInfo(string plate)
        {
            await GetResponseString(plate);

            //Debug.WriteLine(String.Join(",", final));

            return _FINAL;
        }

        private async Task GetResponseString(string lplate)
        {
            string result;

            using (var request = new HttpClient())
            {
                request.DefaultRequestHeaders.Add("X-AUTH-TOKEN", Token);
                request.DefaultRequestHeaders.Accept.Clear();
                request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                result = await request.GetStringAsync("https://v1.motorapi.dk/vehicles/" + lplate.ToUpper());
            }

            var temp = result.Replace("null,", "\"null\",");

            result = temp;

            temp = result.Replace("false,", "\"false\",");

            result = temp;

            temp = result.Replace("true,", "\"true\",");

            result = temp;

            var resultarray = result.Split(new[] { ": \"" }, StringSplitOptions.None);

            //Debug.WriteLine(String.Join(", ", resultarray));

            var finalCollection = resultarray.Select(item => item.Split(new[] { "\"," }, StringSplitOptions.None)).Select(tempo => tempo[0]).ToList();

            //Debug.WriteLine($"Before------------------------------ {Environment.NewLine} {String.Join(",", finalCollection)} {Environment.NewLine + Environment.NewLine} ");

            finalCollection.RemoveAt(0);
            finalCollection.RemoveAt(finalCollection.Count - 1);

            //Debug.WriteLine($"After------------------------------ {Environment.NewLine} {String.Join(",", finalCollection)} {Environment.NewLine + Environment.NewLine} ");

            _FINAL = finalCollection.ToArray();

            //_LICENSEPLATE = _FINAL[0];
            //_STATUS = _FINAL[1];
            //_STATUSDATE = _FINAL[2];
            //_CARTYPE = _FINAL[3];
            //_USE = _FINAL[4];
            //_FIRSTREGISTRATION = _FINAL[5];
            //_VINNUMBER = _FINAL[6];
            //_OWNWEIGHT = _FINAL[7];
            //_TOTALWEIGHT = _FINAL[8];
            //_AXLES = _FINAL[9];
            //_PULLINGAXLES = _FINAL[10];
            //_SEATS = _FINAL[11];
            //_COUPLING = _FINAL[12];
            //_DOORS = _FINAL[13];
            //_MAKE = _FINAL[14];
            //_MODEL = _FINAL[15];
            //_VARIANT = _FINAL[16];
            //_MODELTYPE = _FINAL[17];
            //_MODELYEAR = _FINAL[18];
            //_COLOR = _FINAL[19];
            //_CHASSISTYPE = _FINAL[20];
            //_ENGINECYLINDERS = _FINAL[21];
            //_ENGINEVOLUME = _FINAL[22];
            //_ENGINEPOWER = _FINAL[23];
            //_FUELTYPE = _FINAL[24];
            //_REGISTRATIONZIPCODE = _FINAL[25];
        }
    }
}