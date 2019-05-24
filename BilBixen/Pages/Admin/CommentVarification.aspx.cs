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
    public partial class CommentVarification : Page
    {
        MySQL_Helper SQL = new MySQL_Helper();

        string commentUsername;
        string commentText;
        int commentID;

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowNextComment(GetCommentsFromDatabase());
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

                try
                {
                    commentID = int.Parse(data[0]["Comment_ID"].ToString());
                    commentUsername = data[0]["Comment_USER"].ToString();
                    commentText = data[0]["Comment_TEXT"].ToString();

                    Debug.WriteLine(commentText);


                    Comment.InnerHtml += "" +
                        "<div class=\"CommentBox\"> " +
                        $"<p class=\"CommentUsername\">{commentUsername}</p> " +
                        $"<p class=\"CommentText\">{commentText}</p> " +
                        "</div>";

                    ID_LABEL.Text = $"Comment ID: {commentID}";
                }
                catch (Exception)
                {
                    Comment.InnerHtml += "" +
                        "<div class=\"CommentBox\"> " +
                        $"<p class=\"CommentUsername\">NoUser</p> " +
                        $"<p class=\"CommentText\">There are no more comments to check</p> " +
                        "</div>";

                    ID_LABEL.Text = $"Comment ID: NULL";
                }
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
                Debug.WriteLine("Rejecting Comment with ID: " + commentID);

                string query = "UPDATE comments " +
                    $"SET Comment_STATUS_ID = 3 " +
                    $"WHERE Comment_ID = {commentID}";

                Debug.WriteLine(query);

                var i = SQL.UpdateDataToDatabase(query);

                Debug.WriteLine(i);

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
                Debug.WriteLine("Approving Comment with ID: " + commentID);

                string query = "UPDATE comments " +
                    $"SET Comment_STATUS_ID = 2 " +
                    $"WHERE Comment_ID = {commentID}";

                Debug.WriteLine(query);

                var i = SQL.UpdateDataToDatabase(query);

                Debug.WriteLine(i);

                UpdateComment();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}