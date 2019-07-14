using API.Services;
using EventFlow.Commands;

namespace API.Domain
{
    public class PayCommand : Command<PaymentAggregate, PaymentId>
    {
        public PayCommand(PaymentId aggregateId,
            AcquiringBankPaymentRequest acquiringBankPaymentRequest)
            : base(aggregateId)
        {
            AcquiringBankPaymentRequest = acquiringBankPaymentRequest;
        }
        
        public AcquiringBankPaymentRequest AcquiringBankPaymentRequest { get; }
    }
}