using API.Services;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace API.Domain
{
    public class PaymentInformation : IReadModel,
        IAmReadModelFor<PaymentAggregate, PaymentId, PaymentSucceeded>,
        IAmReadModelFor<PaymentAggregate, PaymentId, PaymentFailed>
    {
        public PaymentId paymentId { get; private set; }
        
        public BankPaymentResponse bankPaymentResponse { get; private set; }
        
        public void Apply(
            IReadModelContext context,
            IDomainEvent<PaymentAggregate, PaymentId, PaymentSucceeded> domainEvent)
        {
            paymentId = domainEvent.AggregateIdentity;
            bankPaymentResponse = domainEvent.AggregateEvent.BankPaymentResponse;
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<PaymentAggregate, PaymentId, PaymentFailed> domainEvent)
        {
            paymentId = domainEvent.AggregateIdentity;
            bankPaymentResponse = domainEvent.AggregateEvent.BankPaymentResponse;
        }
    }
}