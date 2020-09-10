using System;
using System.Threading.Tasks;
using lazy_coinbase_commerce_sample_net_core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace lazy_coinbase_commerce_sample_net_core.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        ICoinbaseCommerceService _coinbaseCommerceService;
        public OrderController(ILogger<OrderController> logger, ICoinbaseCommerceService coinbaseCommerceService)
        {
            _logger = logger;
            _coinbaseCommerceService = coinbaseCommerceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _coinbaseCommerceService.CreateCharge();
            if (response.HasError())
            {
                return BadRequest(400);
            }
            else
            {
                return Ok(response.Data.HostedUrl);
            }
        }
    }
}