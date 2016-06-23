using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageWebAPIs.Infrastructure
{
    public static class AppHelpers
    {
        public static ImageFormat GetImageFormater(string type)
        {
            var imageFormat = ImageFormat.Jpeg;
            switch (type.ToLower())
            {
                case ".png":
                    imageFormat = ImageFormat.Png;
                    break;

                case ".gif":
                    imageFormat = ImageFormat.Gif;
                    break;

                default:
                    imageFormat = ImageFormat.Jpeg;
                    break;
            }

            return imageFormat;
        }

        public static byte[] imageToByteArray(string imgPath,string fileName)
        {
            MemoryStream ms = new MemoryStream();
            var img = System.Drawing.Image.FromFile(imgPath);
            img.Save(ms, GetImageFormater(Path.GetExtension(fileName)));
            return ms.ToArray();
        }
    }
}