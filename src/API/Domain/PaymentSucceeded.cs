using EventFlow.Aggregates;

namespace API.Domain
{
    public class PaymentSucceeded : AggregateEvent<PaymentAggregate, PaymentId>
    {
    }
}