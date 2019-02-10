using System;
using System.Security.Cryptography.X509Certificates;

namespace RunClientCertValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            var userCaStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            try
            {
                userCaStore.Open(OpenFlags.ReadOnly);
                var certificatesInStore = userCaStore.Certificates;
                var findResult = certificatesInStore.Find(X509FindType.FindBySubjectName, "localhosttestclientcert", true);
                foreach (X509Certificate2 cert in findResult)
                {
                    var chain = new X509Chain();
                    var chainPolicy = new X509ChainPolicy()
                    {
                        RevocationMode = X509RevocationMode.NoCheck,
                        RevocationFlag = X509RevocationFlag.EntireChain
                    };
                    chain.ChainPolicy = chainPolicy;
                    if (!chain.Build(cert))
                    {
                        foreach (X509ChainElement chainElement in chain.ChainElements)
                        {
                            foreach (X509ChainStatus chainStatus in chainElement.ChainElementStatus)
                            {
                                Console.WriteLine(chainStatus.StatusInformation);
                            }
                        }
                    }
                }
            }
            finally
            {
                userCaStore.Close();
            }

            Console.ReadLine();
        }
    }
}
