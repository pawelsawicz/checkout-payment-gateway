using System;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Models;
using API.Services;
using EventFlow;
using EventFlow.Extensions;
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

        //private readonly ICommandBus _commandBus;

        //private readonly IQueryProcessor _queryProcessor;

//        public PaymentsController(ICommandBus commandBus, IQueryProcessor queryProcessor)
//        {
//            _commandBus = commandBus;
//            _queryProcessor = queryProcessor;
//        }

        // GET api/payments
        [HttpPost]
        [Consumes(PaymentRequest.MediaType)]
        public async Task<IActionResult> Post([FromBody]PaymentRequest request)
        {
            Logger.Information($"Entering {nameof(PaymentsController)} - {nameof(Post)}");

            using (var resolver = EventFlowOptions.New
                .AddEvents(typeof(PaymentSucceeded), typeof(PaymentFailed))
                .AddCommandHandlers(typeof(PayCommandHandler))
                .UseInMemoryReadStoreFor<PaymentInformation>()
                .RegisterServices(registration =>
                    registration.Register<IAcquiringBankService, FakeAcquiringBankServiceWithSuccessfulResponse>())
                .CreateResolver()
            )
            {

                var paymentId = PaymentId.New;
                var _commandBus = resolver.Resolve<ICommandBus>();
                var _queryProcessor = resolver.Resolve<IQueryProcessor>();
                var command = new PayCommand(paymentId, ToBankPaymentRequest(request));

                try
                {
                    await _commandBus.PublishAsync(command, CancellationToken.None);
                }
                catch (Exception ex)
                {

                }

                var paymentInformation = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<PaymentInformation>(paymentId),
                    CancellationToken.None);

                Logger.Information($"Exiting {nameof(PaymentsController)} - {nameof(Post)}");
                
                return await Task.FromResult(Ok(paymentInformation));
            }
        }
        
        // GET api/payments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string paymentId)
        {
            Logger.Information($"Entering {nameof(PaymentsController)} - {nameof(Get)}");

            using (var resolver = EventFlowOptions.New
                .AddEvents(typeof(PaymentSucceeded), typeof(PaymentFailed))
                .AddCommandHandlers(typeof(PayCommandHandler))
                .UseInMemoryReadStoreFor<PaymentInformation>()
                .RegisterServices(registration =>
                    registration.Register<IAcquiringBankService, FakeAcquiringBankServiceWithSuccessfulResponse>())
                .CreateResolver()
            )
            {
                var _queryProcessor = resolver.Resolve<IQueryProcessor>();
                
                try
                {
                    var view = await _queryProcessor.ProcessAsync(new ReadModelByIdQuery<PaymentInformation>(paymentId),
                        CancellationToken.None);
                    return await Task.FromResult(Ok(view));
                }
                catch (Exception ex)
                {

                }
            }

            Logger.Information($"Exiting {nameof(PaymentsController)} - {nameof(Post)}");

            return BadRequest();
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