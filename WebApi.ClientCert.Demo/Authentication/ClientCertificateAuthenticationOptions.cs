using Microsoft.Owin.Security;

namespace WebApi.ClientCert.NetCore.Authentication
{
    public class ClientCertificateAuthenticationOptions : AuthenticationOptions
    {
        public ClientCertificateAuthenticationOptions() : base("X.509")
        { }
    }
}
