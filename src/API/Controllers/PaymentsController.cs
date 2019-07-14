using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Models;
using API.Services;
using EventFlow;
using EventFlow.Queries;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private static readonly ILogger Logger = Log.Logger.ForContext<PaymentsController>();

        private readonly ICommandBus _commandBus;

        private readonly IQueryProcessor _queryProcessor;

        public PaymentsController(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            _commandBus = commandBus;
            _queryProcessor = queryProcessor;
        }

        // GET api/payments
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentRequest request)
        {
            Logger.Information($"Entering {nameof(PaymentsController)} - {nameof(Post)}");

            Logger.Information($"With Body - {request}");

            var paymentId = PaymentId.New;
            
            var command = new PayCommand(paymentId, ToBankPaymentRequest(request));

            await _commandBus.PublishAsync(command, CancellationToken.None);

            var view = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<PaymentInformation>(paymentId),
                CancellationToken.None);
            
            Logger.Information($"Exiting {nameof(PaymentsController)} - {nameof(Post)}");

            return await Task.FromResult(Ok(view));
        }
        
        // GET api/payments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string paymentId)
        {
            Logger.Information($"Entering {nameof(PaymentsController)} - {nameof(Get)}");
            
            var view = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<PaymentInformation>(paymentId),
                CancellationToken.None);
            
            Logger.Information($"Exiting {nameof(PaymentsController)} - {nameof(Post)}");

            return await Task.FromResult(Ok(view));
        }

        private BankPaymentRequest ToBankPaymentRequest(PaymentRequest paymentRequest) =>
            new BankPaymentRequest
            {
                CardNumber = paymentRequest.CardNumber,
                ExpiryDate = paymentRequest.ExpiryDate,
                ExpiryMonth = paymentRequest.ExpiryMonth,
                Amount = paymentRequest.Amount,
                Cvv = paymentRequest.Cvv,
                CurrencyCode = paymentRequest.CurrencyCode
            };
    }
}