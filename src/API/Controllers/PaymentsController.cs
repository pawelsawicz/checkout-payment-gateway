using System.Threading;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private static readonly ILogger Logger = Log.Logger.ForContext<PaymentsController>();

        // GET api/payments
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentRequest request)
        {
            Logger.Information($"Entering {nameof(PaymentsController)} - {nameof(Post)}");

            Logger.Information($"With Body - {request}");
            
            Thread.Sleep(50);
            
            Logger.Information($"Exiting {nameof(PaymentsController)} - {nameof(Post)}");
            
            return await Task.FromResult(Ok(20));
        }
        
        // GET api/payments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            Logger.Information($"Entering {nameof(PaymentsController)} - {nameof(Get)}");

            return await Task.FromResult(Ok(id));
        }
    }
}