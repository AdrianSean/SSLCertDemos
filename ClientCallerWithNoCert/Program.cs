using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;

namespace ClientCallerWithNoCert
{
    class Program
    {
        static void Main(string[] args)
        {
            X509Certificate2 clientCert = GetClientCertificate();
            var requestHandler = new HttpClientHandler(); // webrequesthandler equiv in .net core
            requestHandler.ClientCertificates.Add(clientCert);

            var client = new HttpClient(requestHandler)
            {
                BaseAddress = new Uri("https://mylocalsite.local/")
            };

            var response = client.GetAsync("customer").Result;
            string responseContent = response.Content.ReadAsStringAsync().Result;
            string responseStatusCode = response.StatusCode.ToString();
            Console.WriteLine(responseContent);
            Console.WriteLine(responseStatusCode);
            Console.ReadKey();
        }



        static X509Certificate2 GetClientCertificate()
        {
            var userCaStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                userCaStore.Open(OpenFlags.ReadOnly);
                var certificatesInStore = userCaStore.Certificates;
                var findResult = certificatesInStore.Find(X509FindType.FindBySubjectName, "localhosttestclientcert", true);
                X509Certificate2 clientCertificate = null;
                if (findResult.Count == 1)
                {
                    clientCertificate = findResult[0];
                }
                else
                {
                    throw new Exception("Unable to locate the correct client certificate.");
                }
                return clientCertificate;
            }
            catch
            {
                throw;
            }
            finally
            {
                userCaStore.Close();
            }
        }
    }
}
