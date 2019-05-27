using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.UI;
using BilBixen.Scripts.Helper_Classes;

namespace BilBixen.Pages.Admin
{
    public partial class CommentVerification : Page
    {
        private readonly MySqlHelper _SQL = new MySqlHelper();

        private string _COMMENT_USERNAME;
        private string _COMMENT_TEXT;
        private int _COMMENT_ID;

        protected void Page_Load(object sender, EventArgs e)
        {
            ShowNextComment(GetCommentsFromDatabase());
        }

        private DataRow[] GetCommentsFromDatabase()
        {

            const string query = "Select * from bilbixen.comments where comment_STATUS_ID = 1;";

            var rows = MySqlHelper.GetDataFromDatabase(query);

            return rows;
        }

        private void ShowNextComment(IReadOnlyList<DataRow> data)
        {
            try
            {
                Comment.InnerHtml = "";

                try
                {
                    _COMMENT_ID = int.Parse(data[0]["Comment_ID"].ToString());
                    _COMMENT_USERNAME = data[0]["Comment_USER"].ToString();
                    _COMMENT_TEXT = data[0]["Comment_TEXT"].ToString();

                    Debug.WriteLine(_COMMENT_TEXT);


                    Comment.InnerHtml += $"<div class=\"CommentBox\"> <p class=\"CommentUsername\">{_COMMENT_USERNAME}</p> <p class=\"CommentText\">{_COMMENT_TEXT}</p> </div>";

                    ID_LABEL.Text = $"Comment ID: {_COMMENT_ID}";
                }
                catch (Exception)
                {
                    Comment.InnerHtml += "<div class=\"CommentBox\"> <p class=\"CommentUsername\">NoUser</p> <p class=\"CommentText\">There are no more comments to check</p> </div>";

                    ID_LABEL.Text = "Comment ID: NULL";
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
                Debug.WriteLine("Rejecting Comment with ID: " + _COMMENT_ID);

                var query = $"UPDATE comments SET Comment_STATUS_ID = 3 WHERE Comment_ID = {_COMMENT_ID}";

                Debug.WriteLine(query);

                int i;
                _SQL.UpdateDataToDatabase(query);

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
                Debug.WriteLine("Approving Comment with ID: " + _COMMENT_ID);

                var query = $"UPDATE comments SET Comment_STATUS_ID = 2 WHERE Comment_ID = {_COMMENT_ID}";

                Debug.WriteLine(query);

                int i;
                _SQL.UpdateDataToDatabase(query);

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