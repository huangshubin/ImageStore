using ImageStoreWeb.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ImageStoreWeb.Externsions
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
                string name = AppHelper.UnquoteToken(file.Headers.ContentDisposition.Name) ?? String.Empty;
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

        public static JsonStatusResult CreateJsonResult<T>(this HttpRequestMessage request, HttpStatusCode statusCode, T value)
        {
            return new JsonStatusResult(request, statusCode, value);
        }
        public static JsonStatusResult CreateJsonResult<T>(this HttpRequestMessage request, T value)
        {
            return new JsonStatusResult(request, HttpStatusCode.OK, value);
        }
        public static int? Identifier(this ClaimsIdentity user)
        {
            var strId = user.FindByType(ClaimTypes.NameIdentifier);

            var id = strId.ToInt(-1);
            if (id == -1) return null;

            return id;
        }
        public static string FindByType(this ClaimsIdentity user, string claimType)
        {
            bool exist = false;
            exist = user.HasClaim(x => x.Type == claimType);
            if (!exist) return null;


            var value = user.FindFirst(x => x.Type == claimType).Value;
            return value;
        }

        public static int ToInt(this string text, int alternavie = 0)
        {
            int rel;
            if (!int.TryParse(text, out rel))
                rel = alternavie;

            return rel;
        }

    }

    public static class ExternalClaimTypes
    {
        public const string IsActive = "imagewebpid_isactive";
        public const string AuthToken = "auth_token";
    }
}