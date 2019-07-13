using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace API.Tests
{
    public class PaymentInformation : IReadModel, IAmReadModelFor<PaymentAggregate, PaymentId, PaymentSucceeded>
    {
        public void Apply(
            IReadModelContext context,
            IDomainEvent<PaymentAggregate, PaymentId, PaymentSucceeded> domainEvent)
        {
        }
    }
}