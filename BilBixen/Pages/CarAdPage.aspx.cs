﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
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

        public string _KM;
        public string _ENGINE;
        public string _PRICE;

        public int commentAmount;
        string[] commentUsernames;
        string[] commentTexts;


        protected void Page_Load(object sender, EventArgs e)
        {
            Debug.WriteLine(GetLastURLExtentionAndID());

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
                GetCommentsFromDatabase();

                CreateComments();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        string GetLastURLExtentionAndID()
        {
            string[] result = HttpContext.Current.Request.Url.AbsoluteUri.Split('/');

            return result[result.Length - 1];
        }

        void GetCarDataFromDatabase()
        {

            string query = "Select * from bilbixen.cars " +
                $"where car_AD_PAGE_ID = {GetLastURLExtentionAndID()};";

            using (var conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["LocalMySqlServer"].ConnectionString))
            {
                conn.Open();

                var cmd = new MySqlCommand(query, conn);

                var dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                var rows = dt.AsEnumerable().ToArray();

                int i = 0;

                foreach (DataRow row in rows)
                {
                    try
                    {
                        _KM = rows[i]["car_KM"].ToString();
                        _ENGINE = rows[i]["car_ENGINE"].ToString();
                        _FIRSTREGISTRATION = rows[i]["car_FIRST_REGISTRATION"].ToString();
                        _PRICE = rows[i]["car_PRICE"].ToString();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Get Car ERROR: " + ex);
                    }

                    i++;
                }
            }
        }

        void GetCommentsFromDatabase()
        {

            string query = "Select * from bilbixen.comments " +
                $"where comment_STATUS_ID = 2 and Comment_AD_ID = {GetLastURLExtentionAndID()};";

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
                        Debug.WriteLine("Get Comments ERROR: " + ex);
                    }

                    i++;
                }

                commentUsernames = usernames.ToArray();
                commentTexts = text.ToArray();
            }
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
