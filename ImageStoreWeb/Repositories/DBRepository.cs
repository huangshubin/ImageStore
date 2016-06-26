using ImageStoreWeb.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageStoreWeb.Repositories
{
    public abstract class DBRepository
    {

        protected AppDbContext DB { get { return HttpContext.Current.GetOwinContext().Get<AppDbContext>(); } }

    }
}