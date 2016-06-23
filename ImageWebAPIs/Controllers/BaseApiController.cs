using ImageWebAPIs.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;

namespace ImageWebAPIs.Controllers
{
    public class BaseApiController : ApiController
    {
        public AppDbContext DbContext
        {
            get { return HttpContext.Current.GetOwinContext().Get<AppDbContext>(); }
        }

        public ClaimsIdentity CurUser
        {
            get
            {
                return User.Identity as ClaimsIdentity;
            }
        }
        public int? CurUserId
        {
            get
            {
                bool exist = false;
                exist = CurUser.HasClaim(x => x.Type == ClaimTypes.NameIdentifier);
                if (!exist) return null;

                int id = -1;
                bool success = int.TryParse(CurUser.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value, out id);
                if (success) return id;

                return null;
            }
        }

        public HttpResponseMessage StatusMsg(HttpStatusCode code, string message)
        {

            var myError = new
            {
                msg = message
            };
            return Request.CreateResponse(code, myError);
        }

    }
}
