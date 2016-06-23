using ImageWebAPIs.AuthEntityFramework;
using ImageWebAPIs.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageWebAPIs.Infrastructure
{
    public class ClientManager :UserManager<Client,int>
    {
        public ClientManager(IUserStore<Client,int> store)
                : base(store)
            {
        }

        public static ClientManager Create(IdentityFactoryOptions<ClientManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<AppDbContext>();
            var clientManager = new ClientManager(new UserStore<Client>(appDbContext));

            clientManager.UserValidator = new UserValidator<Client,int>(clientManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };


            clientManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true,
            };

           // appUserManager.EmailService =;

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                clientManager.UserTokenProvider = new DataProtectorTokenProvider<Client, int>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    TokenLifespan = TimeSpan.FromHours(6)
                };
            }

            return clientManager;
        }
    }

}