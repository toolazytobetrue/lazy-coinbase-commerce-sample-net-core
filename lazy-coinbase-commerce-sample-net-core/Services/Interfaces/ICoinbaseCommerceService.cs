using System;
using System.Threading.Tasks;
using Coinbase.Commerce.Models;

namespace lazy_coinbase_commerce_sample_net_core.Services.Interfaces
{
    public interface ICoinbaseCommerceService
    {
        Task<Response<Charge>> CreateCharge();
    }
}
