using API.Services;
using EventFlow.Aggregates;

namespace API.Domain
{
    public class PaymentFailed : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public PaymentFailed(BankPaymentResponse bankPaymentResponse)
        {
            BankPaymentResponse = bankPaymentResponse;
        }

        public BankPaymentResponse BankPaymentResponse { get; }
    }
}