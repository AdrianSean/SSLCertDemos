using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace WebApi.ClientCert.NetCore.Authentication
{
    public class ClientCertificateAuthenticationHandler : AuthenticationHandler<ClientCertificateAuthenticationOptions>
    {
        private readonly IClientCertificateValidator _clientCertificateValidator;
        private readonly string _owinClientCertKey = "ssl.ClientCertificate";

        public ClientCertificateAuthenticationHandler(IClientCertificateValidator clientCertificateValidator)
        {
            _clientCertificateValidator = clientCertificateValidator ?? throw new ArgumentNullException("ClientCertificateValidator");
        }

        protected override async Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            ClientCertificateValidationResult validationResult = await Task.Run(() => ValidateCertificate(Request.Environment));
            if (validationResult.CertificateValid)
            {
                AuthenticationProperties authProperties = new AuthenticationProperties
                {
                    IssuedUtc = DateTime.UtcNow,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1),
                    AllowRefresh = true,
                    IsPersistent = true
                };
                IList<Claim> claimCollection = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Andras")
                , new Claim(ClaimTypes.Country, "Sweden")
                , new Claim(ClaimTypes.Gender, "M")
                , new Claim(ClaimTypes.Surname, "Nemes")
                , new Claim(ClaimTypes.Email, "hello@me.com")
                , new Claim(ClaimTypes.Role, "IT")
                , new Claim("HasValidClientCertificate", "true")
            };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimCollection, "X.509");
                AuthenticationTicket ticket = new AuthenticationTicket(claimsIdentity, authProperties);
                return ticket;
            }
            return await Task.FromResult<AuthenticationTicket>(null);
        }

        private ClientCertificateValidationResult ValidateCertificate(IDictionary<string, object> owinEnvironment)
        {
            if (owinEnvironment.ContainsKey(_owinClientCertKey))
            {
                X509Certificate2 clientCert = Context.Get<X509Certificate2>(_owinClientCertKey);
                return _clientCertificateValidator.Validate(clientCert);
            }

            ClientCertificateValidationResult invalid = new ClientCertificateValidationResult(false);
            invalid.AddValidationException("There's no client certificate attached to the request.");
            return invalid;
        }
    }
}
