using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using WebApi.ClientCert.NetCore.Authentication;
using WebApi.ClientCert.NetCore.ClientCertMiddleware.LibOwin;

using AppFunc =
System.Func<System.Collections.Generic.IDictionary<string, object>,
System.Threading.Tasks.Task>;

namespace WebApi.ClientCert.NetCore.ClientCertMiddleware
{
    public class ClientCertValidatorMiddleware
    {
        public static AppFunc Middleware(AppFunc next, IClientCertificateValidator clientCertificateValidator)
        {
            return async env =>
            {
                var owinContext = new OwinContext(env);

                string _owinClientCertKey = "ssl.ClientCertificate";
                var clientCert = owinContext.Get<X509Certificate2>(_owinClientCertKey);
                var result = clientCertificateValidator.Validate(clientCert);
                if (result.CertificateValid)
                  await next(env);
                else {
                    owinContext.Response.StatusCode = 401;
                    await Task.FromResult(0);
                }                    
            };
        }
    }
}
