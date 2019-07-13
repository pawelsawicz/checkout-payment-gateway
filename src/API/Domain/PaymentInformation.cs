using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace API.Domain
{
    public class PaymentInformation : IReadModel,
        IAmReadModelFor<PaymentAggregate, PaymentId, PaymentSucceeded>,
        IAmReadModelFor<PaymentAggregate, PaymentId, PaymentFailed>
    {
        public void Apply(
            IReadModelContext context,
            IDomainEvent<PaymentAggregate, PaymentId, PaymentSucceeded> domainEvent)
        {
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<PaymentAggregate, PaymentId, PaymentFailed> domainEvent)
        {
        }
    }
}