using System;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            

            try
            {
                
                Console.WriteLine("Web Server Testing: ");

                var port = GetPortNumber();
                var ssl = GetSsl();

                var @continue = true;

                while (@continue)
                {
                    await TestConnection("localhost", port, ssl);
                    Console.WriteLine("Continue? - Press x for exit");
                    var exit = Console.ReadKey();
                    if (exit.Key == ConsoleKey.X)
                    {
                        @continue = false;
                        
                    }
                }
                 
                Console.WriteLine("Exit - press any key to close application.");
                Console.ReadKey();



            }
            catch (Exception e)
            {
                throw new ApplicationException($"Exception {e}");
            }

            Console.WriteLine("Done");
            Console.ReadKey();

        }

        private static bool GetSsl()
        {
            var ssl = true;

            Console.WriteLine($"Press x for use none-ssl");
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.X)
            {
                return false;
            }

            return ssl;
        }

        private static int GetPortNumber()
        {
            var port = 5004; // 44381; // Kestrel 5001 - iss 44379
            Console.WriteLine($"Continue with port: {port} - or enter new one: ");
            var inputPort = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(inputPort) && int.TryParse(inputPort, out int newPort))
            {
                if (newPort > 0)
                {
                    port = newPort;
                }
            }

            return port;
        }

        private static async Task<bool> TestConnection(string hostname, int port, bool ssl)
        {
            try
            {
                var cert = new X509Certificate2(Path.Combine(@"C:\Development\GitHub\AspNetCoreCertificateAuth\AspNetCoreCertificateAuthApiSelfSigned\", "sts_dev_cert.pfx"), "1234");

                string url = $"https://{hostname}:{port}/api/values";

                Console.WriteLine($"URL: {url} SSL: {ssl}");

                var clientHandler = new HttpClientHandler();

                if (ssl)
                {
                    clientHandler.ClientCertificates.Add(cert);
                    // clientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                    clientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
                }

                var client = new HttpClient(clientHandler);

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(url),
                    Method = HttpMethod.Get,
                };

                request.Headers.Add("api_key", new []{"1234", "9876"});

                if (ssl)
                {
                    request.Headers.Add("X-ARR-ClientCert", cert.GetRawCertDataString());
                }

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // var data = JsonDocument.Parse(responseContent);
                    Console.WriteLine($"Success - StatusCode: {response.StatusCode}. Data: " + responseContent);
                }
                else
                {
                    Console.WriteLine($"Failed - StatusCode: {response.StatusCode}");
                }
                
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);   //  + " - " + e
            }

            return false;
        }

    }
}
