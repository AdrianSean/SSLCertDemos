using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using WebApi.ClientCert.NetCore.Authentication;

namespace WebApi.ClientCert.Demo.Authentication
{
    public class DefaultClientCertificateValidator : IClientCertificateValidator
    {
        public ClientCertificateValidationResult Validate(X509Certificate2 certificate)
        {
            bool isValid = false;
            List<string> exceptions = new List<string>();
            try
            {
                X509Chain chain = new X509Chain();
                X509ChainPolicy chainPolicy = new X509ChainPolicy()
                {
                    RevocationMode = X509RevocationMode.NoCheck,
                    RevocationFlag = X509RevocationFlag.EntireChain
                };
                chain.ChainPolicy = chainPolicy;
                if (!chain.Build(certificate))
                {
                    foreach (X509ChainElement chainElement in chain.ChainElements)
                    {
                        foreach (X509ChainStatus chainStatus in chainElement.ChainElementStatus)
                        {
                            exceptions.Add(chainStatus.StatusInformation);
                        }
                    }
                }
                else
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
                exceptions.Add(ex.Message);
            }
            ClientCertificateValidationResult res = new ClientCertificateValidationResult(isValid);
            res.AddValidationExceptions(exceptions);
            return res;
        }
    }
}