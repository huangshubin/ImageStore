using ImageWebAPIs.Externsions;
using ImageWebAPIs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Net;

namespace ImageWebAPIs.Repositories
{
    public class ImageRepository
    {
        private ClaimsIdentity _curUser;
        public AppDbContext DbContext
        {
            get { return HttpContext.Current.GetOwinContext().Get<AppDbContext>(); }
        }
        public ImageRepository(ClaimsIdentity user)
        {
            _curUser = user;
        }

        public async Task<int> SaveSync(IDictionary<string, object> formData)
        {
            object temp;
            if (!formData.TryGetValue("image", out temp) || !(temp is MultipartFileData))
                throw new HttpDataException(HttpStatusCode.BadRequest, "can not find the image");

            var fileData = temp as MultipartFileData;

            var imgTempPath = fileData.LocalFileName;
            var orignFileName = AppHelpers.UnquoteToken(fileData.Headers.ContentDisposition.FileName);

            if (!formData.TryGetValue("store", out temp))
                temp = "1";

            var strStore = temp as string;

            bool store = strStore == "1" || strStore == "true";
            var savePath = "";
            if (!store)
            {
                savePath = SaveImageDest(imgTempPath, orignFileName);
            }
            var img = new Models.Image()
            {
                Active = false,
                ImageContent = store ? AppHelpers.imageToByteArray(imgTempPath, orignFileName) : null,
                UserId = _curUser.Identifier().Value,
                ImageType = Path.GetExtension(orignFileName),
                ImagePath = store ? null : savePath

            };

            DbContext.Images.Add(img);
            return await DbContext.SaveChangesAsync();
        }
        private string SaveImageDest(string imgTempPath, string fileName)
        {
            var path = ConfigurationManager.AppSettings["ImagePath"];
            if (path == null) path = "~/Images";

            if (!Path.IsPathRooted(path))
                path = HttpContext.Current.Server.MapPath(path);

            Directory.CreateDirectory(path);


            var destFileName = $"File_{_curUser.Name}_{DateTime.Now.ToString("yyyyMMddhhmmss")}{Path.GetExtension(fileName)}";
            var destPath = Path.Combine(path, destFileName);


            File.Copy(imgTempPath, destPath, true);

            return destPath;
        }

    }
}