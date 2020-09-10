using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace lazy_coinbase_commerce_sample_net_core.Controllers
{
    [ApiController]
    [Route("")]
    public class IndexController : ControllerBase
    {
        private readonly ILogger<IndexController> _logger;

        public IndexController(ILogger<IndexController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello");
        }
    }
}
