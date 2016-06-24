using ImageWebAPIs.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
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
                string name = AppHelpers.UnquoteToken(file.Headers.ContentDisposition.Name) ?? String.Empty;
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

        public static int? Identifier(this ClaimsIdentity user)
        {
            bool exist = false;
            exist = user.HasClaim(x => x.Type == ClaimTypes.NameIdentifier);
            if (!exist) return null;

            int id = -1;
            bool success = int.TryParse(user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value, out id);
            if (success) return id;

            return null;
        }

    }

    public static class ExternalClaimTypes
    {
        public const string IsActive = "imagewebpid_isactive";
    }
}