using System.Security.Cryptography.X509Certificates;

namespace WebApi.ClientCert.NetCore.Authentication
{
    public interface IClientCertificateValidator
    {
        ClientCertificateValidationResult Validate(X509Certificate2 certificate);
    }
}