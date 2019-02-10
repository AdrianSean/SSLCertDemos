using Owin;
using WebApi.ClientCert.NetCore.Authentication;

namespace WebApi.ClientCert.Demo.Authentication
{
    public static class ClientCertificateAuthenticationExtensions
    {
        public static void UseClientCertificateAuthentication(this IAppBuilder appBuilder, IClientCertificateValidator clientCertificateValidator)
        {
            appBuilder.Use<ClientCertificateAuthMiddleware>(new ClientCertificateAuthenticationOptions(), clientCertificateValidator);
        }
    }
}