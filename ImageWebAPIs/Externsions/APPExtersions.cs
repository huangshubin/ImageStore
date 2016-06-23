using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace ImageWebAPIs.Externsions
{
    public static class APPExtersions
    {
        public static async Task<IDictionary<string, object>> GetMultipartFormsSync(this HttpRequestMessage request)
        {

            string root = HttpContext.Current.Server.MapPath("~/App_Data/Images");
            Directory.CreateDirectory(root);
            var provider = new MultipartFormDataStreamProvider(root);


            IDictionary<string, object> formsData = new Dictionary<string, object>();


            await request.Content.ReadAsMultipartAsync(provider);
            foreach (MultipartFileData file in provider.FileData)
            {
                string name = UnquoteToken(file.Headers.ContentDisposition.Name) ?? String.Empty;
                formsData.Add(name, file);
            }
            foreach (var key in provider.FormData.AllKeys)
            {
                foreach (var val in provider.FormData.GetValues(key))
                {
                    formsData.Add(key, val);
                }
            }
            return formsData;
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
    }

    public static class ExternalClaimTypes
    {
        public const string IsActive = "ImageWebAPI_IsActive";
    }
}