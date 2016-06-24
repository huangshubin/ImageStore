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
    public class AuthController : BaseApiController
    {

        [Route("api/logout")]
        public async Task<HttpResponseMessage> OauthLogout()
        {
            try
            {


                return StatusMsg(HttpStatusCode.OK, "success");
            }

            catch (HttpDataException hre)
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
