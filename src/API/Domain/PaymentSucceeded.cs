using API.Services;
using EventFlow.Aggregates;

namespace API.Domain
{
    public class PaymentSucceeded : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public PaymentSucceeded(AcquiringBankPaymentResponse acquiringBankPaymentResponse)
        {
            AcquiringBankPaymentResponse = acquiringBankPaymentResponse;
        }
        
        public AcquiringBankPaymentResponse AcquiringBankPaymentResponse { get; }
    }
}