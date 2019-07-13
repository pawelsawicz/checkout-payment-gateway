using EventFlow.Aggregates;

namespace API.Domain
{
    public class PaymentFailed : AggregateEvent<PaymentAggregate, PaymentId>
    {
        
    }
}