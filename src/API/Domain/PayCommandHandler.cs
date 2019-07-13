using System.Threading;
using System.Threading.Tasks;
using API.Services;
using EventFlow.Commands;

namespace API.Domain
{
    public class PayCommandHandler : CommandHandler<PaymentAggregate, PaymentId, PayCommand>
    {
        private readonly IBankComponent _bankComponent;
        
        public PayCommandHandler(IBankComponent bankComponent)
        {
            _bankComponent = bankComponent;
        }
        public override async Task ExecuteAsync(
            PaymentAggregate aggregate,
            PayCommand command,
            CancellationToken cancellationToken)
        {
            var bankPaymentResponse = await _bankComponent.ProcessPayment();
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