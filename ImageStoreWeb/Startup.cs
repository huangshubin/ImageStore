using ImageStoreWeb.Infrastructure;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(ImageStoreWeb.Startup))]
namespace ImageStoreWeb
{

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(AppDbContext.Create);

            OAuthService.Register(app);


        }
    }
}