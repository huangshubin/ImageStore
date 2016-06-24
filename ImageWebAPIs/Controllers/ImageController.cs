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
using ImageWebAPIs.Repositories;

namespace ImageWebAPIs.Controllers
{
    [Authorize]
    [RoutePrefix("api/image")]
    public class ImageController : BaseApiController
    {

        [Route("")]
        [HttpPost]
        public async Task<HttpResponseMessage> StoreImage()
        {
            try
            {
              
                var userId = CurUser.Identifier();
                
                if (userId == null)
                    return StatusMsg(HttpStatusCode.Unauthorized, "unaothorized");

                if (!Request.Content.IsMimeMultipartContent())
                {
                    return StatusMsg(HttpStatusCode.UnsupportedMediaType, "use multipart/form-data type to post data");
                }

                var formData = await Request.GetMultipartFormsSync();
                var imgResponsity = new ImageRepository(CurUser);
                await imgResponsity.SaveSync(formData);


                return StatusMsg(HttpStatusCode.OK, "success");
            }

            catch(HttpDataException hre)
            {
                return StatusMsg(hre.ResponseStatus, hre.Message);
            }
            catch (Exception ex)
            {
                return StatusMsg(HttpStatusCode.InternalServerError, ex.Message);
            }

        }




    }
}
