using System;
using System.IdentityModel.Selectors;
using System.Security.Cryptography.X509Certificates;

namespace RunClientCertValidationCompactVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            var chainTrustValidator = X509CertificateValidator.ChainTrust;
            try
            {
                var userCaStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);                
                userCaStore.Open(OpenFlags.ReadOnly);
                var certificatesInStore = userCaStore.Certificates;
                var findResult = certificatesInStore.Find(X509FindType.FindBySubjectName, "localhosttestclientcert", true);


                foreach (X509Certificate2 cert in findResult) {
                    chainTrustValidator.Validate(cert);
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
