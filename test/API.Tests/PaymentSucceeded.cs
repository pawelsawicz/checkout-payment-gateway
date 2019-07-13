using EventFlow.Aggregates;

namespace API.Tests
{
    public class PaymentSucceeded : AggregateEvent<PaymentAggregate, PaymentId>
    {
    }
}