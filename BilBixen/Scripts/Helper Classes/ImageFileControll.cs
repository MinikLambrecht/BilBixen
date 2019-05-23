using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BilBixen.Scripts.Helper_Classes
{
    public class ImageFileControll
    {
        public FileInfo[] GetImagesFromFolder(string adID)
        {
            string folderURI = HttpContext.Current.Server.MapPath($"~/Images/{adID}/");

            DirectoryInfo d = new DirectoryInfo(folderURI);

            FileInfo[] files = d.GetFiles("*");

            return files;
        }
    }
}