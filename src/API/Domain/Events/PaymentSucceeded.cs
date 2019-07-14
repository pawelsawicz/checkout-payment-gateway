using EventFlow.Aggregates;

namespace API.Domain.Events
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