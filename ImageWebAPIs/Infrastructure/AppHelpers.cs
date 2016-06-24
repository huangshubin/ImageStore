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

        public static byte[] imageToByteArray(string imgPath, string fileName)
        {
            MemoryStream ms = new MemoryStream();
            var img = System.Drawing.Image.FromFile(imgPath);
            img.Save(ms, GetImageFormater(Path.GetExtension(fileName)));
            return ms.ToArray();
        }
        public static string UnquoteToken(string token)
        {
            if (String.IsNullOrWhiteSpace(token))
            {
                return token;
            }

            if (token.StartsWith("\"", StringComparison.Ordinal) && token.EndsWith("\"", StringComparison.Ordinal) && token.Length > 1)
            {
                return token.Substring(1, token.Length - 2);
            }

            return token;
        }

        public static string GetBaseUrl()
        {
            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (appUrl != "/") appUrl += "/";

            var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

            return baseUrl;
        }
    }
}