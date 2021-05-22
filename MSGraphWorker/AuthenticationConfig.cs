using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.IO;

namespace MSGraphWorkerDemo
{
    public class AuthenticationConfig
    {
        public string Instance { get; set; } = "https://login.microsoftonline.com/{0}";

        public string Tenant { get; set; }

        public string ClientId { get; set; }

        public string Authority
        {
            get
            {
                return String.Format(CultureInfo.InvariantCulture, Instance, Tenant);
            }
        }

        public string ClientSecret { get; set; }

        public string CertificateName { get; set; }

        public static AuthenticationConfig ReadFromJsonFile(string path)
        {
            IConfigurationRoot configuration;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(path);

                configuration = builder.Build();
            return configuration.Get<AuthenticationConfig>();
        }
    }



}
