using API.Services;
using EventFlow.Aggregates;
using EventFlow.Exceptions;

namespace API.Domain
{
    public class PaymentAggregate : AggregateRoot<PaymentAggregate, PaymentId>
    {

        private BankPaymentResponse _bankPaymentResponse;
        
        public PaymentAggregate(PaymentId id) : base(id)
        {
        }

        public void SetPaymentSuccessful(BankPaymentResponse bankPaymentResponse)
        {
            if (_bankPaymentResponse == null)
            {
                _bankPaymentResponse = bankPaymentResponse;
                Emit(new PaymentSucceeded(bankPaymentResponse));
            }
            else
            {
                throw DomainError.With("Payment has been already processed");
            }
        }

        public void SetPaymentFailed(BankPaymentResponse bankPaymentResponse)
        {
            if (_bankPaymentResponse == null)
            {
                _bankPaymentResponse = bankPaymentResponse;
                Emit(new PaymentFailed(bankPaymentResponse));
            }
            else
            {
                throw DomainError.With("Payment has been already processed");
            }
        }

        public void Apply(PaymentSucceeded paymentSucceeded)
        {
            _bankPaymentResponse = paymentSucceeded.BankPaymentResponse;
        }

        public void Apply(PaymentFailed paymentFailed)
        {
            _bankPaymentResponse = paymentFailed.BankPaymentResponse;
        }
    }
}