using System;
using System.Web.UI;
using BilBixen.Scripts.Helper_Classes;

namespace BilBixen.Pages
{
    public partial class CarAdPage : Page
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


        protected void Page_Load(object sender, EventArgs e)
        {
            CreateComments();
        }

        void CreateComments()
        {
            int amountOfComments = 5;
            string username = "TestName", commentText = "TestText is only to be used in tests";

            for (var i = 0; i < amountOfComments; i++)
            {
                Comments.InnerHtml += "" +
                    "<div class=\"CommentBox\" style=\"\"> " +
                    $"<p class=\"CommentUsername\">{username}</p> " +
                    $"<p class=\"CommentText\">{commentText}</p> " +
                    "</div>";
            }
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
