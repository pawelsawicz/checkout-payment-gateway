using API.Services;
using EventFlow.Commands;

namespace API.Domain.Commands
{
    public class PayCommand : Command<PaymentAggregate, PaymentId>
    {
        public PayCommand(PaymentId aggregateId,
            AcquiringBankPaymentRequest paymentRequest)
            : base(aggregateId)
        {
            PaymentRequest = paymentRequest;
        }
        
        public AcquiringBankPaymentRequest PaymentRequest { get; }
    }
}