using System.Threading;
using System.Threading.Tasks;
using EventFlow.Commands;

namespace API.Tests
{
    public class PayCommandHandler : CommandHandler<PaymentAggregate, PaymentId, PayCommand>
    {
        public override Task ExecuteAsync(
            PaymentAggregate aggregate,
            PayCommand command,
            CancellationToken cancellationToken)
        {
            aggregate.SetPaymentSuccessful();
            return Task.FromResult(0);
        }
    }
}