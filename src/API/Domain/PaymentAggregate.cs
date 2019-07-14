using API.Services;
using EventFlow.Aggregates;
using EventFlow.Exceptions;

namespace API.Domain
{
    public class PaymentAggregate : AggregateRoot<PaymentAggregate, PaymentId>
    {

        private AcquiringBankPaymentResponse _acquiringBankPaymentResponse;
        
        public PaymentAggregate(PaymentId id) : base(id)
        {
        }

        public void SetPaymentSuccessful(AcquiringBankPaymentResponse acquiringBankPaymentResponse)
        {
            if (_acquiringBankPaymentResponse == null)
            {
                _acquiringBankPaymentResponse = acquiringBankPaymentResponse;
                Emit(new PaymentSucceeded(acquiringBankPaymentResponse));
            }
            else
            {
                throw DomainError.With("Payment has been already processed");
            }
        }

        public void SetPaymentFailed(AcquiringBankPaymentResponse acquiringBankPaymentResponse)
        {
            if (_acquiringBankPaymentResponse == null)
            {
                _acquiringBankPaymentResponse = acquiringBankPaymentResponse;
                Emit(new PaymentFailed(acquiringBankPaymentResponse));
            }
            else
            {
                throw DomainError.With("Payment has been already processed");
            }
        }

        public void Apply(PaymentSucceeded paymentSucceeded)
        {
            _acquiringBankPaymentResponse = paymentSucceeded.AcquiringBankPaymentResponse;
        }

        public void Apply(PaymentFailed paymentFailed)
        {
            _acquiringBankPaymentResponse = paymentFailed.AcquiringBankPaymentResponse;
        }
    }
}