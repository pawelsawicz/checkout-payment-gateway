using EventFlow.Aggregates;
using EventFlow.Exceptions;

namespace API.Tests
{
    public class PaymentAggregate : AggregateRoot<PaymentAggregate, PaymentId>
    {

        private string _bankIdentifier;
        
        public PaymentAggregate(PaymentId id) : base(id)
        {
        }

        public void SetPaymentSuccessful(string bankIdentifier)
        {
            if (string.IsNullOrEmpty(_bankIdentifier))
            {
                _bankIdentifier = bankIdentifier;
                Emit(new PaymentSucceeded());
            }
            else
            {
                throw DomainError.With("Payment has been already processed");
            }
        }

        public void SetPaymentFailed(string bankIdentifier)
        {
            if (string.IsNullOrEmpty(_bankIdentifier))
            {
                _bankIdentifier = bankIdentifier;
                Emit(new PaymentFailed());
            }
            else
            {
                throw DomainError.With("Payment has been already processed");
            }
        }

        public void Apply(PaymentSucceeded paymentSucceeded)
        {
        }

        public void Apply(PaymentFailed paymentFailed)
        {
        }
    }
}