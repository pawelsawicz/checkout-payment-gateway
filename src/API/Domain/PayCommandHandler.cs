using System;
using System.Threading;
using System.Threading.Tasks;
using API.Services;
using EventFlow.Commands;

namespace API.Domain
{
    public class PayCommandHandler : CommandHandler<PaymentAggregate, PaymentId, PayCommand>
    {
        private IBankComponent _bankComponent;
        
        public PayCommandHandler(IBankComponent bankComponent)
        {
            _bankComponent = bankComponent;
        }
        public override async Task ExecuteAsync(
            PaymentAggregate aggregate,
            PayCommand command,
            CancellationToken cancellationToken)
        {

            var bankResult = Guid.NewGuid();
            
            
            if (await _bankComponent.ProcessPayment())
            {
                aggregate.SetPaymentSuccessful(bankResult.ToString());
            }
            else
            {
                aggregate.SetPaymentFailed(bankResult.ToString());
            }
        }
    }
}