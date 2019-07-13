using EventFlow.Core;

namespace API.Domain
{
    public class PaymentId : Identity<PaymentId>
    {
        public PaymentId(string value) : base(value)
        {
        }
    }
}