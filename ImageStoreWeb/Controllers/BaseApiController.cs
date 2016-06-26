using ImageStoreWeb.Infrastructure;
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
using ImageStoreWeb.Externsions;
namespace ImageStoreWeb.Controllers
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

        public JsonStatusResult StatusMsg(HttpStatusCode code, string msg)
        {

            var myError = new
            {
                message = msg
            };

            return Request.CreateJsonResult(code, myError);
        }

    }
}
