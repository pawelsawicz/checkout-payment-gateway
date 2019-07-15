using System.Threading;
using System.Threading.Tasks;
using API.Domain.Commands;
using API.Services;
using EventFlow.Commands;

namespace API.Domain
{
    public class PayCommandHandler : CommandHandler<PaymentAggregate, PaymentId, PayCommand>
    {
        private readonly IAcquiringBankService _acquiringBankService;
        
        public PayCommandHandler(IAcquiringBankService acquiringBankService)
        {
            _acquiringBankService = acquiringBankService;
        }
        public override async Task ExecuteAsync(
            PaymentAggregate aggregate,
            PayCommand command,
            CancellationToken cancellationToken)
        {
            var bankPaymentResponse = await _acquiringBankService.ProcessPayment(command.PaymentRequest);
            var paymentStatus = CreatePaymentStatus(command.PaymentRequest, bankPaymentResponse);
            if (bankPaymentResponse.PaymentStatus == "Approved")
            {
                aggregate.SuccessPayment(paymentStatus);
            }
            else
            {
                aggregate.FailPayment(paymentStatus);
            }
        }

        private PaymentStatus CreatePaymentStatus(
            AcquiringBankPaymentRequest request,
            AcquiringBankPaymentResponse response) => new PaymentStatus
        {
            BankIdentifier = response.BankIdentifier,
            PaymentStatusCode = response.PaymentStatus,
            CardNumber = request.CardNumber,
            ExpiryMonth = request.ExpiryMonth,
            ExpiryDate = request.ExpiryDate,
            Name = request.Name,
            Amount = request.Amount,
            Currency = request.CurrencyCode
        };
    }
}