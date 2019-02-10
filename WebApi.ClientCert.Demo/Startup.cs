using Owin;
using WebApi.ClientCert.Demo.Authentication;

namespace WebApi.ClientCert.Demo
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseClientCertificateAuthentication(new DefaultClientCertificateValidator());
        }
    }
}