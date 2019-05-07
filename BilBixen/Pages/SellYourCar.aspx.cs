using System;
using System.Web;
using System.IO;

namespace BilBixen.Pages
{
    public partial class SellYourCar : System.Web.UI.Page
    {
        public int AdID;

        protected void Page_Load(object sender, EventArgs e)
        {
            AdID = GenerateID();
        }

        public int GenerateID()
        {
            Random rand = new Random();

            string H = DateTime.Now.ToString("HH");
            string M = DateTime.Now.ToString("mm");
            string S = DateTime.Now.ToString("ss");

            string P1 = rand.Next(0, 100).ToString();
            string P2 = rand.Next(0, 100).ToString();

            string UniqueID = H + M + S + P1 + P2;

            return Convert.ToInt32(UniqueID);
        }

        protected void uploadFiles_Click(object sender, EventArgs e)
        {
            try
            {
                HttpFileCollection hfc = Request.Files;
                for (int i = 0; i < hfc.Count; i++)
                {
                    HttpPostedFile hpf = hfc[i];

                    string ext = System.IO.Path.GetExtension(hpf.FileName);


                    if (hpf.ContentLength > 0)
                    {
                        if (Directory.Exists(Server.MapPath("~/Images/") + AdID.ToString()))
                        {
                            List.Text = "Dir exsists!";
                        }
                        else
                        {
                            Directory.CreateDirectory(Server.MapPath("~/Images/") + AdID.ToString());
                            List.Text = "Dir created!";
                        }
                        hpf.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Images/") + AdID.ToString(), AdID.ToString() + "_" + i.ToString() + ext));
                    }
                }
            }
            catch (Exception ex)
            {
                // Log errors
            }

            //if (AdPictures.HasFiles)
            //{
            //    foreach (HttpPostedFile file in AdPictures.PostedFiles)
            //    {
            //        int picNumber = 0;
            //        string ext = System.IO.Path.GetExtension(AdPictures.PostedFile.FileName);

            //        file.SaveAs(System.IO.Path.Combine(Server.MapPath("~/Images/"), AdID.ToString() + "_" + picNumber.ToString() + ext));
            //        List.Text += String.Format("{0} <br />", AdPictures.PostedFile.FileName);
            //    }
            //}
        }
    }
}