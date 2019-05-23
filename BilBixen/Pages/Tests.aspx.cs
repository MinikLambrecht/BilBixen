using BilBixen.Scripts.Helper_Classes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BilBixen.Pages
{
    public partial class Tests : Page
    {
        MySQL_Helper SQL = new MySQL_Helper();

        string commentUsername;
        string commentText;
        int commentID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ShowNextComment(GetCommentsFromDatabase());
            }
        }

        DataRow[] GetCommentsFromDatabase()
        {

            string query = "Select * from bilbixen.comments " +
                $"where comment_STATUS_ID = 1;";

            var rows = SQL.GetDataFromDatabase(query);

            return rows;
        }

        void ShowNextComment(DataRow[] data)
        {
            try
            {
                Comment.InnerHtml = "";

                commentID = int.Parse(data[0]["Comment_ID"].ToString());
                commentUsername = data[0]["Comment_USER"].ToString();
                commentText = data[0]["Comment_TEXT"].ToString();

                Comment.InnerHtml += "" +
                    "<div class=\"CommentBox\" style=\"\"> " +
                    $"<p class=\"CommentUsername\">{commentUsername}</p> " +
                    $"<p class=\"CommentText\">{commentText}</p> " +
                    "</div>";

                ID_LABEL.Text = $"Comment ID: {commentID}";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        void UpdateComment()
        {
            ShowNextComment(GetCommentsFromDatabase());
        }

        protected void RejectComment_OnClick(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE bilbixen.comments " +
                    $"SET Comment_STATUS_ID = 3 " +
                    $"WHERE Comment_ID = {commentID};";

                SQL.SetDataToDatabase(query);

                UpdateComment();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        protected void ApproveComment_OnClick(object sender, EventArgs e)
        {
            try
            {
                string query = "UPDATE bilbixen.comments " +
                    $"SET Comment_STATUS_ID = 2 " +
                    $"WHERE Comment_ID = {commentID};";

                SQL.SetDataToDatabase(query);

                UpdateComment();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}