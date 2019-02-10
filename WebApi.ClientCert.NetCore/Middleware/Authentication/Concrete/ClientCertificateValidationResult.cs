using System.Collections.Generic;
using System.Linq;

namespace WebApi.ClientCert.NetCore.Authentication
{
    public class ClientCertificateValidationResult
    {
        public ClientCertificateValidationResult(bool certificateValid)
        {
            CertificateValid = certificateValid;
            ValidationExceptions = new List<string>();
        }

        public void AddValidationExceptions(IEnumerable<string> validationExceptions)
        {
            ValidationExceptions.Concat(validationExceptions);
        }

        public void AddValidationException(string validationException)
        {
            ValidationExceptions.Concat(new[] { validationException });
        }

        public IEnumerable<string> ValidationExceptions { get; }

        public bool CertificateValid { get; }
    }
}