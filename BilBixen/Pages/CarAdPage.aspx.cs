using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using BilBixen.Scripts.Helper_Classes;
using MySql.Data.MySqlClient;

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

        string[] commentUsernames;
        string[] commentTexts;


        protected void Page_Load(object sender, EventArgs e)
        {
            GetCommentsFromDatabase();

            CreateComments();
        }

        void GetCommentsFromDatabase()
        {
            const string query = "Select * from bilbixen.comments " +
                "where comment_STATUS_ID = 2 and Comment_AD_ID = 1;";

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn);

                List<string> usernames = new List<string>();
                List<string> text = new List<string>();

                var dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                var rows = dt.AsEnumerable().ToArray();

                int i = 0;

                foreach (DataRow row in rows)
                {
                    try
                    {
                        usernames.Add(rows[i]["Comment_USER"].ToString());
                        text.Add(rows[i]["Comment_TEXT"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }

                    i++;
                }

                commentUsernames = usernames.ToArray();
                commentTexts = text.ToArray();
            }
        }

        void CreateComments()
        {
            if (commentUsernames.Length == commentTexts.Length)
            {
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
