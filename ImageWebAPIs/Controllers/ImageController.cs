using ImageWebAPIs.Infrastructure;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ImageWebAPIs.Externsions;
using System;
using System.Web;

namespace ImageWebAPIs.Controllers
{

    [RoutePrefix("api/image")]
    public class ImageController : BaseApiController
    {

        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> StoreImage()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    return StatusMsg(HttpStatusCode.UnsupportedMediaType, "use multipart/form-data type to post data");
                }

                var formsData = await Request.GetMultipartFormsSync();
                var fileData = (MultipartFileData)formsData["image"];

                var imgTempPath = fileData.LocalFileName;
                var orignFileName = APPExtersions.UnquoteToken(fileData.Headers.ContentDisposition.FileName);

                var strStore = ((string)formsData["store"]).ToLower();
                bool store = strStore == "1" || strStore == "true";
                var savePath = "";
                if (!store)
                {
                    savePath = SaveImageDest(imgTempPath, orignFileName);
                }

                var userId = CurUserId;


                if (userId == null)
                    return StatusMsg(HttpStatusCode.Unauthorized, "unaothorized");

                var img = new Models.Image()
                {
                    Active = false,
                    ImageContent = store ? AppHelpers.imageToByteArray(imgTempPath, orignFileName) : null,
                    UserId = userId.Value,
                    ImageType = Path.GetExtension(orignFileName),
                    ImagePath = store ? null : savePath

                };

                DbContext.Images.Add(img);
                await DbContext.SaveChangesAsync();
                return StatusMsg(HttpStatusCode.OK, "success");
            }

            catch (Exception ex)
            {
                return StatusMsg(HttpStatusCode.InternalServerError, ex.Message);
            }

        }


        private string SaveImageDest(string imgTempPath, string fileName)
        {
            var path = ConfigurationManager.AppSettings["ImagePath"];
            if (path == null) path = "~/Images";

            if (!Path.IsPathRooted(path))
                path = HttpContext.Current.Server.MapPath(path);

            Directory.CreateDirectory(path);


            var destFileName = $"File_{CurUser.Name}_{DateTime.Now.ToString("yyyyMMddhhmmss")}{Path.GetExtension(fileName)}";
            var destPath = Path.Combine(path, destFileName);


            File.Copy(imgTempPath, destPath, true);

            return destPath;
        }

    }
}
