﻿using System;
using System.Security.Cryptography.X509Certificates;

namespace SearchForRootCertBySubjectForCurrentUser
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
                var findResult = certificatesInStore.Find(X509FindType.FindBySubjectName, "localhosttestclientcert", true);

                foreach (X509Certificate2 cert in findResult)
                {
                    Console.WriteLine(cert.GetExpirationDateString());
                    Console.WriteLine(cert.GetExpirationDateString());
                    Console.WriteLine(cert.Issuer);
                    Console.WriteLine(cert.GetEffectiveDateString());
                    Console.WriteLine(cert.GetNameInfo(X509NameType.SimpleName, true));
                    Console.WriteLine(cert.HasPrivateKey);
                    Console.WriteLine(cert.SubjectName.Name);
                    Console.WriteLine("-----------------------------------");
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
