using API.Domain.Events;
using EventFlow.Aggregates;
using EventFlow.Exceptions;

namespace API.Domain
{
    public class PaymentAggregate : AggregateRoot<PaymentAggregate, PaymentId>
    {

        private PaymentStatus _paymentStatus;
        
        public PaymentAggregate(PaymentId id) : base(id)
        {
        }

        public void SuccessPayment(PaymentStatus paymentStatus)
        {
            if (_paymentStatus == null)
            {
                _paymentStatus = paymentStatus;
                Emit(new PaymentSucceeded(paymentStatus));
            }
            else
            {
                throw DomainError.With("Payment has been already processed");
            }
        }

        public void FailPayment(PaymentStatus paymentStatus)
        {
            if (_paymentStatus == null)
            {
                _paymentStatus = paymentStatus;
                Emit(new PaymentFailed(paymentStatus));
            }
            else
            {
                throw DomainError.With("Payment has been already processed");
            }
        }

        public void Apply(PaymentSucceeded paymentSucceeded)
        {
            _paymentStatus = paymentSucceeded.PaymentStatus;
        }

        public void Apply(PaymentFailed paymentFailed)
        {
            _paymentStatus = paymentFailed.PaymentStatus;
        }
    }
}