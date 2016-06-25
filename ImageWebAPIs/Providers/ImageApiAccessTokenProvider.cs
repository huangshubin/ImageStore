using ImageWebAPIs.Externsions;
using ImageWebAPIs.Repositories;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ImageWebAPIs.Providers
{
    public class ImageApiAccessTokenProvider : AuthenticationTokenProvider
    {
        private IAppBuilder _app;
        private TicketDataFormat _dataFormat;
        private TokenRepository _tokenRepository;
        public ImageApiAccessTokenProvider(IAppBuilder app)
        {
            _app = app;
            _tokenRepository = new TokenRepository();

            OnCreateAsync = CreateAccessTokenSync;
            OnCreate = (x => { });

            OnReceiveAsync = ReceiveAccessTokenSync;
            OnReceive = (x => { });
        }

        private async Task ReceiveAccessTokenSync(AuthenticationTokenReceiveContext context)
        {
            var ticket = UnProtecedData(context.Token);

            if (ticket.Properties.ExpiresUtc.HasValue &&
                ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow)
                await _tokenRepository.RemoveAsync(context.Token);

            var newClaim = new Claim(ExternalClaimTypes.AuthToken, context.Token, ClaimValueTypes.String);

            ticket.Identity.AddClaim(newClaim);

            context.SetTicket(ticket);
        }

        private async Task CreateAccessTokenSync(AuthenticationTokenCreateContext context)
        {
            var ticket = context.Ticket;
            var token = ProtectedData(context);

            if (!ticket.Identity.Identifier().HasValue)
                return;



            await _tokenRepository.SaveAsync(token,
                ticket.Properties.ExpiresUtc?.UtcDateTime
                , ticket.Properties.IssuedUtc?.UtcDateTime
                , ticket.Identity.Identifier().Value);

            context.SetToken(token);
        }


        private string ProtectedData(AuthenticationTokenCreateContext context)
        {
            if (_dataFormat == null)
                _dataFormat = CreateDataFormat();

            var data = context.Ticket;
            var text = _dataFormat.Protect(data);
            return text;

        }
        private AuthenticationTicket UnProtecedData(string protectedText)
        {
            if (_dataFormat == null)
                _dataFormat = CreateDataFormat();

            var ticket = _dataFormat.Unprotect(protectedText);
            return ticket;

        }
        private TicketDataFormat CreateDataFormat()
        {
            var dataProtecter = _app.CreateDataProtector(
                 typeof(ImageApiAccessTokenProvider).Namespace,
                 "Access_Token", "v1");

            return new TicketDataFormat(dataProtecter);

        }
    }
}