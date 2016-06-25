using ImageWebAPIs.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;
using ImageWebAPIs.Externsions;
namespace ImageWebAPIs
{
    public class OAuthService
    {

        public static void Register(IAppBuilder app)
        {
            ConfigureOAuthTokenGeneration(app);
            ConfigureOAuthTokenConsumption(app);

        }
        private static void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {

            var tokenExpireTime = ConfigurationManager.AppSettings["OauthTokenExpireTime"]?.ToInt(1440);
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {

                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/login"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(tokenExpireTime ?? 1440),
                Provider = new ImageApiAuthorizateProvider(),
                AccessTokenProvider = new ImageApiAccessTokenProvider(app)
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }

        private static void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            app.UseOAuthBearerAuthentication(
                new OAuthBearerAuthenticationOptions
                {
                    Provider = new ImageApiAuthenticateProvider(),
                    AccessTokenProvider = new ImageApiAccessTokenProvider(app)
                });
        }



    }
}