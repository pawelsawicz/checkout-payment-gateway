using API.Services;
using EventFlow.Aggregates;

namespace API.Domain
{
    public class PaymentFailed : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public PaymentFailed(AcquiringBankPaymentResponse acquiringBankPaymentResponse)
        {
            AcquiringBankPaymentResponse = acquiringBankPaymentResponse;
        }

        public AcquiringBankPaymentResponse AcquiringBankPaymentResponse { get; }
    }
}