using System;
using EventFlow.Aggregates;
using EventFlow.ReadStores;

namespace API.Domain
{
    public class PaymentInformation : IReadModel,
        IAmReadModelFor<PaymentAggregate, PaymentId, PaymentSucceeded>,
        IAmReadModelFor<PaymentAggregate, PaymentId, PaymentFailed>
    {
        public PaymentId PaymentId { get; private set; }
        
        public PaymentStatus PaymentStatus { get; private set; }
        
        public Links Links { get; private set; }
        
        public void Apply(
            IReadModelContext context,
            IDomainEvent<PaymentAggregate, PaymentId, PaymentSucceeded> domainEvent)
        {
            PaymentId = domainEvent.AggregateIdentity;
            PaymentStatus = domainEvent.AggregateEvent.PaymentStatus;
            Links = ToLinks(PaymentId);
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<PaymentAggregate, PaymentId, PaymentFailed> domainEvent)
        {
            PaymentId = domainEvent.AggregateIdentity;
            PaymentStatus = domainEvent.AggregateEvent.PaymentStatus;
            Links = ToLinks(PaymentId);
        }

        private Links ToLinks(PaymentId paymentId)
        {
            return new Links
            {
                self_href = new Uri($"http://localhost:5000/api/payments/{paymentId.Value}").ToString()
            };
        }
    }
}