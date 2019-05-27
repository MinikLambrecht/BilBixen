using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.UI;
using BilBixen.Scripts.Helper_Classes;

namespace BilBixen.Pages
{
    public partial class CarAdPage : Page
    {
        private readonly MySqlHelper _SQL = new MySqlHelper();

        protected string _FIRSTREGISTRATION;
        protected string _COUPLING;
        protected string _TOTALWEIGHT;
        protected string _DOORS;
        protected string _MAKE;
        protected string _MODEL;
        protected string _MODELYEAR;
        protected string _COLOR;
        protected string _FUELTYPE;
        protected string _PLATE;

        protected string _KM;
        protected string _ENGINE;
        protected string _PRICE;

        protected int commentAmount;
        private string[] _COMMENT_USERNAMES;
        private string[] _COMMENT_TEXTS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;
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

        private void GetImagesFromFolderAndInsertToPage()
        {
            var folderUri = HttpContext.Current.Server.MapPath($"~/Images/{GetLastUrlExtensionAndId()}/");

            var d = new DirectoryInfo(folderUri);

            var files = d.GetFiles("*");

            CreateImageMenuItems(files);
        }

        private void CreateImageMenuItems(FileInfo[] files)
        {
            if (files == null) throw new ArgumentNullException(nameof(files));
            var i = 0;

            foreach (var unused in files)
            {
                if (i == 0)
                {
                    ImageMenu.InnerHtml += $"<!-- Slide {i + 1} --><div class=\"item active\"><img src = \"/Images/{GetLastUrlExtensionAndId()}/{files[i].Name}\" class=\"ImageMenuImage\"/></div>";
                }
                else
                {
                    ImageMenu.InnerHtml += $"<!-- Slide {i + 1} --><div class=\"item\"><img src = \"/Images/{GetLastUrlExtensionAndId()}/{files[i].Name}\" class=\"ImageMenuImage\"/></div>";
                }

                i++;
            }
        }

        private static string GetLastUrlExtensionAndId()
        {
            var result = HttpContext.Current.Request.Url.AbsoluteUri.Split('/');

            return result[result.Length - 1];
        }

        private void GetCarDataFromDatabase()
        {

            var query = "Select * from bilbixen.all_cars " +
                $"where car_AD_PAGE_ID = {GetLastUrlExtensionAndId()};";

            var rows = MySqlHelper.GetDataFromDatabase(query);

            var i = 0;

            foreach (var unused in rows)
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

        private void GetCommentsFromDatabase()
        {

            var query = "Select * from bilbixen.comments " +
                $"where comment_STATUS_ID = 2 and Comment_AD_ID = {GetLastUrlExtensionAndId()};";

            var rows = MySqlHelper.GetDataFromDatabase(query);

            var i = 0;

            var usernames = new List<string>();
            var text = new List<string>();

            foreach (var unused in rows)
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

            _COMMENT_USERNAMES = usernames.ToArray();
            _COMMENT_TEXTS = text.ToArray();
        }

        private void CreateComments()
        {
            try
            {
                if (_COMMENT_USERNAMES.Length == _COMMENT_TEXTS.Length)
                {

                    commentAmount = _COMMENT_USERNAMES.Length;
                    var amountOfComments = _COMMENT_USERNAMES.Length;

                    for (var i = 0; i < amountOfComments; i++)
                    {
                        Comments.InnerHtml += $"<div class=\"CommentBox\" style=\"\"> <p class=\"CommentUsername\">{_COMMENT_USERNAMES[i]}</p> <p class=\"CommentText\">{_COMMENT_TEXTS[i]}</p> </div>";
                    }
                }
                else
                {
                    Debug.WriteLine("CommentUsernames Length: " + _COMMENT_USERNAMES.Length);
                    Debug.WriteLine("CommentTexts Length: " + _COMMENT_TEXTS.Length);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
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

        private void PostComment()
        {
            var name = User.Identity.IsAuthenticated ? User.Identity.Name : "Anonymous";

            var text = CommentTextArea.Value;

            var query = "Insert into `comments` (Comment_TEXT, Comment_USER, Comment_STATUS_ID, Comment_AD_ID) " +
                $"values ('{text}', '{name}', 1, '{GetLastUrlExtensionAndId()}');";

            _SQL.SetDataToDatabase(query);
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
