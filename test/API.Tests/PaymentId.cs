using EventFlow.Core;

namespace API.Tests
{
    public class PaymentId : Identity<PaymentId>
    {
        public PaymentId(string value) : base(value)
        {
        }
    }
}