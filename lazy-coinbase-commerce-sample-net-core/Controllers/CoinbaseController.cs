using System;
using System.IO;
using System.Threading.Tasks;
using Coinbase.Commerce;
using Coinbase.Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lazy_coinbase_commerce_sample_net_core.Controllers
{
    [ApiController]
    [Route("api/webhooks/coinbase")]
    public class CoinbaseController : ControllerBase
    {
        IConfiguration _iconfiguration;
        public CoinbaseController(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            HttpRequestRewindExtensions.EnableBuffering(this.Request);
            var body = this.Request.Body;
            body.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(body);
            var input = await reader.ReadToEndAsync();
            var requestSignature = Request.Headers[HeaderNames.WebhookSignature];
            string webhookSecret = _iconfiguration.GetSection("Coinbase").GetSection("WEBHOOK_SECRET").Value;
            if (WebhookHelper.IsValid(webhookSecret, requestSignature, input))
            {
                Console.WriteLine("Verified");
                // The request is legit and an authentic message from Coinbase.
                // It's safe to deserialize the JSON body. 
                var webhook = JsonConvert.DeserializeObject<Webhook>(input);
                var chargeInfo = webhook.Event.DataAs<Charge>();

                // Remember that customer ID we created back in the first example?
                // Here's were we can extract that information from the callback.
                //var customerId = chargeInfo.Metadata["customerId"].ToObject<string>();

                if (webhook.Event.IsChargeFailed)
                {
                    // The payment failed. Log something.
                    //Database.MarkPaymentFailed(customerId);
                    Console.WriteLine("Charge failed");
                }
                else if (webhook.Event.IsChargeCreated)
                {
                    // The charge was created just now.
                    // Do something with the newly created
                    // event.
                    //Database.MarkPaymentPending(customerId)
                    Console.WriteLine("Charge created");
                }
                else if (webhook.Event.IsChargeConfirmed)
                {
                    Console.WriteLine("Charge confirmed");
                }

                return Ok("Success");
            }
            else
            {
                Console.WriteLine("unverified");
                // Some hackery going on. The Webhook message validation failed.
                // Someone is trying to spoof payment events!
                // Log the requesting IP address and HTTP body.
                return BadRequest("Unverified");
            }
        }
    }
}
