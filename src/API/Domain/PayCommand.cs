using API.Services;
using EventFlow.Commands;

namespace API.Domain
{
    public class PayCommand : Command<PaymentAggregate, PaymentId>
    {
        public PayCommand(PaymentId aggregateId,
            BankPaymentRequest bankPaymentRequest)
            : base(aggregateId)
        {
            BankPaymentRequest = bankPaymentRequest;
        }
        
        public BankPaymentRequest BankPaymentRequest { get; }
    }
}