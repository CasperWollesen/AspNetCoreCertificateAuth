using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreCertificateAuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            if(Request.HttpContext.Request.Headers.TryGetValue("api_key", out var value))
            {
                foreach (var v in value)
                {
                    Console.WriteLine($"{v}");
                }
            }

            //X509Certificate2 clientCertInRequest = Request.HttpContext.Connection.ClientCertificate;
            //if (!clientCertInRequest.Verify() || !AllowedCerialNumbers(clientCertInRequest.SerialNumber))
            //{
            //    Response.StatusCode = 404;
            //    return null;
            //}

            return new string[] { "value1", "value2", "value3", DateTime.Now.ToString(), DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString(), };
        }

        //https://stackoverflow.com/questions/40014047/add-client-certificate-to-net-core-httpclient
        //private static X509Certificate2? Signer()
        //{
        //    using var cert = X509Certificate2.CreateFromSignedFile(Assembly.GetExecutingAssembly().Location);
        //    if (cert is null)
        //        return null;

        //    return new X509Certificate2(cert);
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
