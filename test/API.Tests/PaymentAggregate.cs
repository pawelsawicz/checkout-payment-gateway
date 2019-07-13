using EventFlow.Aggregates;

namespace API.Tests
{
    public class PaymentAggregate : AggregateRoot<PaymentAggregate, PaymentId>
    {
        public PaymentAggregate(PaymentId id) : base(id)
        {
        }

        public void SetPaymentSuccessful()
        {
            Emit(new PaymentSucceeded());
        }

        public void Apply(PaymentSucceeded paymentSucceeded)
        {
        }
    }
}