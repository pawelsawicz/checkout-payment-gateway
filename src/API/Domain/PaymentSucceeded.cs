using EventFlow.Aggregates;

namespace API.Domain
{
    public class PaymentSucceeded : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public PaymentSucceeded(PaymentStatus paymentStatus)
        {
            PaymentStatus = paymentStatus;
        }
        
        public PaymentStatus PaymentStatus { get; }
    }
}