using API.Services;
using EventFlow.Aggregates;

namespace API.Domain
{
    public class PaymentSucceeded : AggregateEvent<PaymentAggregate, PaymentId>
    {
        public PaymentSucceeded(BankPaymentResponse bankPaymentResponse)
        {
            BankPaymentResponse = bankPaymentResponse;
        }
        
        public BankPaymentResponse BankPaymentResponse { get; }
    }
}