using System;
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
        
        public AcquiringBankPaymentResponse AcquiringBankPaymentResponse { get; private set; }
        
        public Links Links { get; private set; }
        
        public void Apply(
            IReadModelContext context,
            IDomainEvent<PaymentAggregate, PaymentId, PaymentSucceeded> domainEvent)
        {
            paymentId = domainEvent.AggregateIdentity;
            AcquiringBankPaymentResponse = domainEvent.AggregateEvent.AcquiringBankPaymentResponse;
            Links = ToLinks(paymentId);
        }

        public void Apply(
            IReadModelContext context,
            IDomainEvent<PaymentAggregate, PaymentId, PaymentFailed> domainEvent)
        {
            paymentId = domainEvent.AggregateIdentity;
            AcquiringBankPaymentResponse = domainEvent.AggregateEvent.AcquiringBankPaymentResponse;
            Links = ToLinks(paymentId);
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