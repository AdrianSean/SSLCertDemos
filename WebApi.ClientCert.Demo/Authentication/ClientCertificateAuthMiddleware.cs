using Microsoft.Owin;
using Microsoft.Owin.Security.Infrastructure;
using System;
using WebApi.ClientCert.NetCore.Authentication;

namespace WebApi.ClientCert.Demo.Authentication
{
    public class ClientCertificateAuthMiddleware : AuthenticationMiddleware<ClientCertificateAuthenticationOptions>
    {
        private readonly IClientCertificateValidator _clientCertificateValidator;

        public ClientCertificateAuthMiddleware(OwinMiddleware nextMiddleware, ClientCertificateAuthenticationOptions authOptions,
            IClientCertificateValidator clientCertificateValidator)
            : base(nextMiddleware, authOptions)
        {
            _clientCertificateValidator = clientCertificateValidator ?? throw new ArgumentNullException("ClientCertificateValidator");
        }

        protected override AuthenticationHandler<ClientCertificateAuthenticationOptions> CreateHandler()
        {
            return new ClientCertificateAuthenticationHandler(_clientCertificateValidator);
        }
    }
}