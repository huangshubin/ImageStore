using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClient.Infrastructure
{
    class ImageWebAPIs
    {
        private static readonly string BaseUrl = ConfigurationManager.AppSettings["BasePathOfApi"];

        public static readonly string Login = $"{BaseUrl}/api/login";
        public static readonly string Logout = $"{BaseUrl}/api/logout";
        public static readonly string SendImage = $"{BaseUrl}/api/image";
        public static readonly string GetImageList = $"{BaseUrl}/api/image/list";
        public static readonly string GetImage = $"{BaseUrl}/api/image/{{0}}";
    }
}
