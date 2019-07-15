using System;
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

        private const string Approved = "Approved";

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
            if (string.Equals(bankPaymentResponse.PaymentStatus, Approved, StringComparison.InvariantCultureIgnoreCase))
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
            AcquiringBankPaymentResponse response) =>
            new PaymentStatus(
                response.BankIdentifier,
                response.PaymentStatus,
                request.CardNumber,
                request.ExpiryMonth,
                request.ExpiryDate,
                request.Name,
                request.Amount,
                request.CurrencyCode);
    }
}