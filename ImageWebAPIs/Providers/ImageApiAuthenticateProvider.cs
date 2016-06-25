using ImageWebAPIs.Infrastructure;
using ImageWebAPIs.Repositories;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ImageWebAPIs.Providers
{
    public class ImageApiAuthenticateProvider : OAuthBearerAuthenticationProvider
    {
        public ImageApiAuthenticateProvider()
        {
            OnRequestToken = GetRequestToken;

        }

        public async Task GetRequestToken(OAuthRequestTokenContext context)
        {
            TokenRepository reps = new TokenRepository();
            var tokenText = context.Token;
            var tokenEntity = await reps.FindAsync(tokenText);

            if (tokenEntity == null)
            {
                context.Token = null;
                return;
            }
        }
    }
}