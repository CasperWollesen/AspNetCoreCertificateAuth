using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreCertificateAuthApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // System.Diagnostics.Debugger.Launch();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
            => WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureKestrel(options =>
            {
                //options.Listen(IPAddress.Any, 80);
                //options.Listen(IPAddress.Any, 44380, listenOptions =>
                //{
                //    listenOptions.UseHttps(Path.Combine(@"C:\Development\GitHub\AspNetCoreCertificateAuth\AspNetCoreCertificateAuthApiSelfSigned\", "sts_dev_cert.pfx"), "1234");
                //});

                //var cert = new X509Certificate2(Path.Combine(@"C:\Development\GitHub\AspNetCoreCertificateAuth\AspNetCoreCertificateAuthApiSelfSigned\", "sts_dev_cert.pfx"), "1234");
                //options.ConfigureHttpsDefaults(o =>
                //{

                //    o.ServerCertificate = cert;
                //    o.ClientCertificateMode = ClientCertificateMode.RequireCertificate;
                //});
            })
            .Build();
    }
}
