using ImageStoreWeb.Infrastructure;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ImageStoreWeb.Externsions;
using System;
using System.Web;
using ImageStoreWeb.Repositories;
using System.Web.Http.Results;
using System.Collections.Generic;
using Easy.Logger;

namespace ImageStoreWeb.Controllers
{
    [Authorize]
    [RoutePrefix("api/image")]
    public class ImageController : BaseApiController
    {
        private ILogger _logger = Log4NetService.Instance.GetLogger<ImageController>();
        private ImageRepository imgResponsity;
        public ImageController()
        {
            imgResponsity = new ImageRepository();
        }
        [Route("")]
        [HttpPost]
        public async Task<IHttpActionResult> StoreImage()
        {
            try
            {

                if (!Request.Content.IsMimeMultipartContent())
                {

                    return StatusMsg(HttpStatusCode.UnsupportedMediaType, "use multipart/form-data type to post data");
                }

                var formData = await Request.GetMultipartFormsSync();

                await imgResponsity.SaveSync(formData, CurUser);


                return StatusMsg(HttpStatusCode.OK, $"{CurUser.Name} your image was processed");
            }

            catch (HttpDataException hre)
            {
                return StatusMsg(hre.ResponseStatus, hre.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusMsg(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        [Route("list")]
        public async Task<IHttpActionResult> GetImageIds()
        {
            try
            {

                var userId = CurUser.Identifier();

                IList<int> imageIds = await imgResponsity.FineImagesByUser(userId);

                var result = new { images = imageIds };
                return Request.CreateJsonResult(result);

            }

            catch (HttpDataException hre)
            {
                return StatusMsg(hre.ResponseStatus, hre.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusMsg(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetImage(int id)
        {
            try
            {

                var image = await imgResponsity.FineImageById(id);

                var result = new { type = image.Item2, image = image.Item1 };

                return Request.CreateJsonResult(result);

            }

            catch (HttpDataException hre)
            {
                return StatusMsg(hre.ResponseStatus, hre.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return StatusMsg(HttpStatusCode.InternalServerError, ex.Message);
            }

        }
    }
}
