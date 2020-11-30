using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Execute...");
            Console.ReadKey();
            
            try
            {
                var selfSigned = new X509Certificate2("sts_dev_cert1.pfx", "1234");
                var handlerSelfSigned = new HttpClientHandler();
                handlerSelfSigned.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                //handlerSelfSigned.ClientCertificates.Add(selfSigned);

                //var client = new HttpClient(handlerSelfSigned);
                var client = new HttpClient(handlerSelfSigned);
                

                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://localhost:5001/api/values"),
                    Method = HttpMethod.Get,
                };
                request.Headers.Add("X-ARR-ClientCert", selfSigned.GetRawCertDataString());

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var data = JsonDocument.Parse(responseContent);

                    Console.WriteLine(data);
                    Console.WriteLine("Succees: " + responseContent);
                }
                else
                {
                    Console.WriteLine("Not allowed: ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed: " + e);
                
            }

            Console.WriteLine("done");
            Console.ReadKey();


            return;
        }
    }
}
