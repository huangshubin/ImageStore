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
        [HttpPost]
        public async Task<HttpResponseMessage> Logout()
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
                return StatusMsg(HttpStatusCode.InternalServerError, ex.Message);
            }

        }




    }
}
