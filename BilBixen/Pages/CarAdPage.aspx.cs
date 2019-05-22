using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using BilBixen.Scripts.Helper_Classes;
using MySql.Data.MySqlClient;

namespace BilBixen.Pages
{
    public partial class CarAdPage : Page
    {
        SearchByLicensePlate plateSearch = new SearchByLicensePlate();
        MySQL_Helper SQL = new MySQL_Helper();

        public string _FIRSTREGISTRATION;
        public string _TOTALWEIGHT;
        public string _COUPLING;
        public string _DOORS;
        public string _MAKE;
        public string _MODEL;
        public string _MODELYEAR;
        public string _COLOR;
        public string _FUELTYPE;
        public string _PLATE;

        public string _KM;
        public string _ENGINE;
        public string _PRICE;

        public int commentAmount;
        string[] commentUsernames;
        string[] commentTexts;

        public string[] ImagesURI;

        #region OnPageLoad

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    GetCarDataFromDatabase();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                try
                {
                    GetImagesFromFolderAndInsertToPage();

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }

                /*
                try
                {
                    await GetInfoFromPlate();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                */

                try
                {
                    GetCommentsFromDatabase();

                    CreateComments();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        void GetImagesFromFolderAndInsertToPage()
        {
            string folderURI = HttpContext.Current.Server.MapPath($"~/Images/{GetLastURLExtentionAndID()}/");

            DirectoryInfo d = new DirectoryInfo(folderURI);

            FileInfo[] files = d.GetFiles("*");

            CreateImageMenuItems(files);
        }

        void CreateImageMenuItems(FileInfo[] files)
        {
            int i = 0;

            foreach (FileInfo uri in files)
            {

                if (i == 0)
                {
                    ImageMenu.InnerHtml += "" +
                        $"<!-- Slide {i + 1} -->" +
                        $"<div class=\"item active\">" +
                        $"<img src = \"/Images/{GetLastURLExtentionAndID()}/{files[i].Name}\" class=\"ImageMenuImage\"/>" +
                        $"</div>";
                }
                else
                {
                    ImageMenu.InnerHtml += "" +
                        $"<!-- Slide {i + 1} -->" +
                        $"<div class=\"item\">" +
                        $"<img src = \"/Images/{GetLastURLExtentionAndID()}/{files[i].Name}\" class=\"ImageMenuImage\"/>" +
                        $"</div>";
                }

                i++;
            }
        }

        async Task GetInfoFromPlate()
        {
            var data = await plateSearch.GetInfo(_PLATE);

            _COUPLING = data[12];
            _DOORS = data[13];
            _MODELYEAR = data[18];
            _COLOR = data[19];
        }

        string GetLastURLExtentionAndID()
        {
            string[] result = HttpContext.Current.Request.Url.AbsoluteUri.Split('/');

            return result[result.Length - 1];
        }

        void GetCarDataFromDatabase()
        {

            string query = "Select * from bilbixen.all_cars " +
                $"where car_AD_PAGE_ID = {GetLastURLExtentionAndID()};";

            var rows = SQL.GetDataFromDatabase(query);

            int i = 0;

            foreach (DataRow row in rows)
            {
                try
                {
                    _MAKE = rows[i]["car_BRAND"].ToString();
                    _MODEL = rows[i]["car_MODEL"].ToString();
                    _KM = rows[i]["car_KM"].ToString();
                    _ENGINE = rows[i]["car_ENGINE"].ToString();
                    _FIRSTREGISTRATION = rows[i]["car_FIRST_REGISTRATION"].ToString();
                    _PRICE = rows[i]["car_PRICE"].ToString();
                    _PLATE = rows[i]["car_PLATE"].ToString();
                    _MODELYEAR = rows[i]["car_MODELYEAR"].ToString();
                    _FUELTYPE = rows[i]["car_FUEL"].ToString();

                    _FIRSTREGISTRATION = _FIRSTREGISTRATION.Split(' ')[0];
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Get Car ERROR: " + ex);
                }

                i++;
            }
        }

        void GetCommentsFromDatabase()
        {

            string query = "Select * from bilbixen.comments " +
                $"where comment_STATUS_ID = 2 and Comment_AD_ID = {GetLastURLExtentionAndID()};";

            var rows = SQL.GetDataFromDatabase(query);

            int i = 0;

            List<string> usernames = new List<string>();
            List<string> text = new List<string>();

            foreach (DataRow row in rows)
            {
                try
                {
                    usernames.Add(rows[i]["Comment_USER"].ToString());
                    text.Add(rows[i]["Comment_TEXT"].ToString());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Get Comments ERROR: " + ex);
                }

                i++;
            }

            commentUsernames = usernames.ToArray();
            commentTexts = text.ToArray();
        }

        void CreateComments()
        {
            try
            {
                if (commentUsernames.Length == commentTexts.Length)
                {

                    commentAmount = commentUsernames.Length;
                    var amountOfComments = commentUsernames.Length;

                    for (var i = 0; i < amountOfComments; i++)
                    {
                        Comments.InnerHtml += "" +
                            "<div class=\"CommentBox\" style=\"\"> " +
                            $"<p class=\"CommentUsername\">{commentUsernames[i]}</p> " +
                            $"<p class=\"CommentText\">{commentTexts[i]}</p> " +
                            "</div>";
                    }
                }
                else
                {
                    Debug.WriteLine("CommentUsernames Lenght: " + commentUsernames.Length);
                    Debug.WriteLine("CommentTexts Lenght: " + commentTexts.Length);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        #endregion

        #region PostComments

        protected void Comment_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Posting comment");

            try
            {
                PostComment();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        void PostComment()
        {
            string name;

            if (User.Identity.IsAuthenticated)
            {
                name = User.Identity.Name;
            }
            else
            {
                name = "Anonymous";
            }

            string text = CommentTextArea.Value;

            string query = "Insert into `comments` (Comment_TEXT, Comment_USER, Comment_STATUS_ID, Comment_AD_ID) " +
                $"values ('{text}', '{name}', 1, '{GetLastURLExtentionAndID()}');";

            SQL.SetDataToDatabase(query);
        }

        #endregion
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
