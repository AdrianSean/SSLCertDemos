using System;
using System.Security.Cryptography.X509Certificates;

namespace FindAllClientCertsFromStoreForCurrentUser
{
    class Program
    {
        static void Main(string[] args)
        {
            var userCaStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);

            try
            {
                userCaStore.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certificatesInStore = userCaStore.Certificates;

                foreach (X509Certificate2 cert in certificatesInStore)
                {
                    Console.WriteLine(cert.GetExpirationDateString());
                    Console.WriteLine(cert.Issuer);
                    Console.WriteLine(cert.GetEffectiveDateString());
                    Console.WriteLine(cert.GetNameInfo(X509NameType.SimpleName, true));
                    Console.WriteLine(cert.HasPrivateKey);
                    Console.WriteLine(cert.SubjectName.Name);
                    Console.WriteLine("-----------------------------------");

                    Console.ReadLine();
                }
            }
            finally
            {
                userCaStore.Close();
            }
        }
    }
}
