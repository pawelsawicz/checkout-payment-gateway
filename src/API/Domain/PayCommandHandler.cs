using System.Threading;
using System.Threading.Tasks;
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
            var bankPaymentResponse = await _acquiringBankService.ProcessPayment();
            if (bankPaymentResponse.PaymentStatus == "Approved")
            {
                aggregate.SetPaymentSuccessful(bankPaymentResponse);
            }
            else
            {
                aggregate.SetPaymentFailed(bankPaymentResponse);
            }
        }
    }
}