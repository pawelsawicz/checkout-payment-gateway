using System;
using System.Threading.Tasks;

namespace API.Services
{
    public class FakeAcquiringBankServiceWithSuccessfulResponse : IAcquiringBankService
    {
        public Task<BankPaymentResponse> ProcessPayment()
        {
            return Task.FromResult(new BankPaymentResponse
            {
                BankIdentifier = Guid.NewGuid().ToString(),
                PaymentStatus = "Approved"
            });
        }
    }
}