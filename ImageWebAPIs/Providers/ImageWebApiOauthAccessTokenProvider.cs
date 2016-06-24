using ImageWebAPIs.Infrastructure;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Thinktecture.IdentityModel.Tokens;

namespace ImageWebAPIs.Providers
{
    public class ImageWebApiOauthAccessTokenProvider : AuthenticationTokenProvider
    {
        public ImageWebApiOauthAccessTokenProvider()
        {
            OnCreateAsync = CreateAccessTokenSync;
            OnCreate = CreateAccessToken;
        }
        private async Task CreateAccessTokenSync(AuthenticationTokenCreateContext context)
        {

            var token = ProtectedData(context);
            context.SetToken(token);
        }
        private void CreateAccessToken(AuthenticationTokenCreateContext context)
        {
            var token = ProtectedData(context);
            context.SetToken(token);
            return;
        }

        private string ProtectedData(AuthenticationTokenCreateContext context)
        {
            string audienceId = ConfigurationManager.AppSettings["AudienceId"];

            string symmetricKeyAsBase64 = ConfigurationManager.AppSettings["AudienceSecret"];

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            var signingKey = new HmacSigningCredentials(keyByteArray);

            var data = context.Ticket;
            var issued = data.Properties.IssuedUtc;

            var expires = data.Properties.ExpiresUtc;

            var token = new JwtSecurityToken(AppHelpers.GetBaseUrl(), audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new JwtSecurityTokenHandler();

            var secToken = handler.WriteToken(token);


            return secToken;

        }
    }
}