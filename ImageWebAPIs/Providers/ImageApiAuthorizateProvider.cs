﻿using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ImageWebAPIs.Models;
using ImageWebAPIs.Externsions;
using ImageWebAPIs.Repositories;

namespace ImageWebAPIs.Providers
{
    public class ImageApiAuthorizateProvider : OAuthAuthorizationServerProvider
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

            var repos = new ClientRepository();

            var user = await repos.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "user name/password is not incorrect. or user is not active");
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
            claims.Add(new Claim(ExternalClaimTypes.IsActive, user.Active.ToString().ToLower(), ClaimValueTypes.String));

            return new ClaimsIdentity(claims, "ImageWebAPIOAuth");
        }
    }
}