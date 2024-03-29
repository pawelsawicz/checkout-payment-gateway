using System;
using System.Threading;
using System.Threading.Tasks;
using API.Domain;
using API.Domain.Commands;
using API.Models;
using API.Services;
using EventFlow;
using EventFlow.Configuration;
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

        public PaymentsController(IRootResolver rootResolver)
        {
            _commandBus = rootResolver.Resolve<ICommandBus>();
            _queryProcessor = rootResolver.Resolve<IQueryProcessor>();
        }
        
        [HttpPost]
        [Consumes(PaymentRequest.MediaType)]
        public async Task<IActionResult> Post([FromBody] PaymentRequest request)
        {
            Logger.Information($"Entering {nameof(PaymentsController)} - {nameof(Post)}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var paymentId = PaymentId.New;
            var command = new PayCommand(paymentId, ToBankPaymentRequest(request));

            try
            {
                await _commandBus.PublishAsync(command, CancellationToken.None);
            }
            catch (Exception ex)
            {
                Logger.Error("There was unexpected error while handling your request", ex);
                return StatusCode(500);
            }

            var paymentInformation = await _queryProcessor.ProcessAsync(
                new ReadModelByIdQuery<PaymentInformationReadModel>(paymentId),
                CancellationToken.None);

            Logger.Information($"Exiting {nameof(PaymentsController)} - {nameof(Post)}");

            return Created(paymentInformation.Links.self_href, paymentInformation);

        }
        
        [HttpGet("{paymentId}")]
        public async Task<IActionResult> Get(string paymentId)
        {
            Logger.Information($"Entering {nameof(PaymentsController)} - {nameof(Get)} with {paymentId}");

            try
            {
                var paymentInformation = await _queryProcessor.ProcessAsync(
                    new ReadModelByIdQuery<PaymentInformationReadModel>(paymentId),
                    CancellationToken.None);

                if (paymentInformation == null)
                {
                    return NotFound();
                }
                
                return Ok(paymentInformation);
            }
            catch (Exception ex)
            {
                Logger.Error("There was unexpected error while handling your request", ex);
                return StatusCode(500);
            }
        }

        private AcquiringBankPaymentRequest ToBankPaymentRequest(PaymentRequest paymentRequest) =>
            new AcquiringBankPaymentRequest(
                paymentRequest.CardNumber,
                paymentRequest.ExpiryMonth,
                paymentRequest.ExpiryDate,
                paymentRequest.Name,
                paymentRequest.Amount,
                paymentRequest.CurrencyCode,
                paymentRequest.Cvv);
    }
}