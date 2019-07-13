using System;
using System.Threading.Tasks;

namespace API.Services
{
    public class FakeBankComponent : IBankComponent
    {
        public Task<BankPaymentResponse> ProcessPayment()
        {
            var fakeResponse = new BankPaymentResponse
            {
                BankIdentifier = Guid.NewGuid().ToString(),
                PaymentStatus = "Approved"
            };
            return Task.FromResult(fakeResponse);
        }
    }
}