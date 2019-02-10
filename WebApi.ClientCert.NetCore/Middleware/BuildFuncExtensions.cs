using WebApi.ClientCert.NetCore.Authentication;
using WebApi.ClientCert.NetCore.ClientCertMiddleware;
using BuildFunc = System.Action<System.Func<System.Func<System.Collections.Generic.IDictionary<string, object>,
                    System.Threading.Tasks.Task>, System.Func<System.Collections.Generic.IDictionary<string, object>,
                    System.Threading.Tasks.Task>>>;

namespace WebApi.ClientCert.NetCore.Middleware
{
    public static class BuildFuncExtensions
    {
        public static BuildFunc UseClientCertValidation(this BuildFunc buildFunc, IClientCertificateValidator clientCertificateValidator)
        {
            buildFunc(next => ClientCertValidatorMiddleware.Middleware(next, clientCertificateValidator));
            return buildFunc;
        }
    }
}
