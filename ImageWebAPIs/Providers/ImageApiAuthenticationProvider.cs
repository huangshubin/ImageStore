using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ImageWebAPIs.Providers
{
    public class ImageWebApiAuthenticationProvider : OAuthBearerAuthenticationProvider
    {
        public ImageWebApiAuthenticationProvider()
        {
            OnRequestToken = GetRequestToken;

        }

        public async Task GetRequestToken(OAuthRequestTokenContext context)
        {

         //   context.Token = null;
            return;
        }
    }
}