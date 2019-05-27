using System.IO;
using System.Web;

namespace BilBixen.Scripts.Helper_Classes
{
    public class ImageFileController
    {
        public static FileInfo[] GetImagesFromFolder(string adId)
        {
            var folderUri = HttpContext.Current.Server.MapPath($"~/Images/{adId}/");

            var d = new DirectoryInfo(folderUri);

            var files = d.GetFiles("*");

            return files;
        }
    }
}