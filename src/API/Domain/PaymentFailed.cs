using EventFlow.Aggregates;

namespace API.Domain
{
    public class PaymentFailed : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public PaymentFailed(PaymentStatus paymentStatus)
        {
            PaymentStatus = paymentStatus;
        }

        public PaymentStatus PaymentStatus { get; }
    }
}