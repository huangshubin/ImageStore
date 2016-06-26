using ImageStoreWeb.Externsions;
using ImageStoreWeb.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;

namespace ImageStoreWeb.Repositories
{
    public class ImageRepository : DBRepository
    {

        public async Task<int> SaveSync(IDictionary<string, object> formData, ClaimsIdentity user)
        {
            object temp;
            if (!formData.TryGetValue("image", out temp) || !(temp is MultipartFileData))
                throw new HttpDataException(HttpStatusCode.BadRequest, "can not find the image");

            var fileData = temp as MultipartFileData;

            var imgTempPath = fileData.LocalFileName;
            var orignFileName = AppHelper.UnquoteToken(fileData.Headers.ContentDisposition.FileName);

            if (!formData.TryGetValue("store", out temp))
                temp = "1";

            var strStore = temp as string;

            bool store = strStore == "1" || strStore == "true";
            var savePath = "";
            if (!store)
            {
                var destFileName = $"File_{user.Name}_{DateTime.Now.ToString("yyyyMMddhhmmss")}{Path.GetExtension(orignFileName)}";

                savePath = SaveImageDest(imgTempPath, destFileName);
            }
            var img = new Models.Image()
            {
                Active = false,
                ImageContent = store ? AppHelper.imageToByteArray(imgTempPath, Path.GetExtension(orignFileName)) : null,
                UserId = user.Identifier().Value,
                ImageType = Path.GetExtension(orignFileName),
                ImagePath = store ? null : savePath

            };

            DB.Images.Add(img);
            return await DB.SaveChangesAsync();
        }

        public async Task<IList<int>> FineImagesByUser(int? userId)
        {
            var ids = await (from p in DB.Images
                             where p.UserId == userId
                             select p.Id).ToListAsync();

            return ids;
        }
        public async Task<Tuple<byte[], string>> FineImageById(int id)
        {
            var image = await DB.Images.FindAsync(id);

            var bytes = image.ImageContent;
            if (bytes == null)
            {
                bytes = AppHelper.imageToByteArray(image.ImagePath, image.ImageType);
            }
            return new Tuple<byte[], string>(bytes, image.ImageType);
        }

        private string SaveImageDest(string imgTempPath, string destFileName)
        {
            var path = ConfigurationManager.AppSettings["ImagePath"];
            if (path == null) path = "~/Images";

            if (!Path.IsPathRooted(path))
                path = HttpContext.Current.Server.MapPath(path);

            Directory.CreateDirectory(path);

            var destPath = Path.Combine(path, destFileName);
            File.Copy(imgTempPath, destPath, true);

            return destPath;
        }

    }
}