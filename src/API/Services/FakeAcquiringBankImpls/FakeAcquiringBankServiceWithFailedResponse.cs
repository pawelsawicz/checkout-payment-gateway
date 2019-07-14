using System;
using System.Threading.Tasks;

namespace API.Services.FakeAcquiringBankImpls
{
    public class FakeAcquiringBankServiceWithFailedResponse : IAcquiringBankService
    {
        public Task<BankPaymentResponse> ProcessPayment(BankPaymentRequest bankPaymentRequest)
        {
            return Task.FromResult(new BankPaymentResponse
            {
                BankIdentifier = Guid.NewGuid().ToString(),
                PaymentStatus = "Failed"
            });
        }
    }
}