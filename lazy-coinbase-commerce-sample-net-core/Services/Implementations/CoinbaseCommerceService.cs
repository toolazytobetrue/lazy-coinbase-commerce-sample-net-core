using System;
using System.Threading.Tasks;
using Coinbase.Commerce;
using Coinbase.Commerce.Models;
using Microsoft.Extensions.Configuration;
using lazy_coinbase_commerce_sample_net_core.Services.Interfaces;

namespace lazy_coinbase_commerce_sample_net_core.Services.Implementations
{
    public class CoinbaseCommerceService : ICoinbaseCommerceService
    {
        IConfiguration _iconfiguration;
        CommerceApi commerceApi;
        public CoinbaseCommerceService(IConfiguration iconfiguration)
        {
            _iconfiguration = iconfiguration;
            string apiKey = _iconfiguration.GetSection("Coinbase").GetSection("API_KEY").Value;
            commerceApi = new CommerceApi(apiKey);
        }

        public async Task<Response<Charge>> CreateCharge()
        {
            var customerId = Guid.NewGuid();

            var charge = new CreateCharge
            {
                Name = "Candy Bar",
                Description = "Sweet Tasting Chocolate",
                PricingType = PricingType.FixedPrice,
                LocalPrice = new Money { Amount = 1.00m, Currency = "USD" },
                Metadata = // Here we associate the customer ID in our DB with the charge.
         {       // You can put any custom info here but keep it minimal.
            {"customerId", customerId}
         },
            };

            return await commerceApi.CreateChargeAsync(charge);
        }
    }
}
