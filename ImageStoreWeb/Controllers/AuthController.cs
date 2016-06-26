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
using Easy.Logger;

namespace ImageStoreWeb.Controllers
{
    [Authorize]
    public class AuthController : BaseApiController
    {
        private ILogger _logger = Log4NetService.Instance.GetLogger<ImageController>();
        [Route("api/logout")]
        [HttpGet]
        public async Task<JsonStatusResult> Logout()
        {
            try
            {
                var userId = CurUser.Identifier();
                if (userId == null)
                    return StatusMsg(HttpStatusCode.Unauthorized, "unaothorized");

                var token = CurUser.FindByType(ExternalClaimTypes.AuthToken);

                if (token != null)
                {
                    TokenRepository reps = new TokenRepository();
                    await reps.RemoveUserTokenAsync(userId.Value, token);
                }

                return StatusMsg(HttpStatusCode.OK, "success");
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
