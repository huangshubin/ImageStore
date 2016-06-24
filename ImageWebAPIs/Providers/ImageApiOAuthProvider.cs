using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using ImageWebAPIs.Infrastructure;
using System.Data.Entity;
using ImageWebAPIs.Models;
using ImageWebAPIs.Externsions;

namespace ImageWebAPIs.Providers
{
    public class ImageApiOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var allowedOrigin = "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var dbContext = context.OwinContext.Get<AppDbContext>();

            var user = await dbContext.Clients.FirstOrDefaultAsync(x => x.UserName == context.UserName && x.Password == context.Password);

            if (user == null)
            {

                context.SetError("invalid_grant", "user name or password is not incorrect.");
                return;
            }


            ClaimsIdentity oAuthIdentity = GenerateIdentity(user);

            var ticket = new AuthenticationTicket(oAuthIdentity, null);

            context.Validated(ticket);

        }
        private ClaimsIdentity GenerateIdentity(Client user)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
            claims.Add(new Claim(ExternalClaimTypes.IsActive, user.Active.ToString(), ClaimValueTypes.String));

            return new ClaimsIdentity(claims, "ImageWebAPIOAuth");
        }
    }
}