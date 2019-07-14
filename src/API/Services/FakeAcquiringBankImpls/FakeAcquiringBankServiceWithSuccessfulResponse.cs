using System;
using System.Threading.Tasks;

namespace API.Services.FakeAcquiringBankImpls
{
    public class FakeAcquiringBankServiceWithSuccessfulResponse : IAcquiringBankService
    {
        public Task<AcquiringBankPaymentResponse> ProcessPayment(AcquiringBankPaymentRequest acquiringBankPaymentRequest)
        {
            return Task.FromResult(new AcquiringBankPaymentResponse
            {
                BankIdentifier = Guid.NewGuid().ToString(),
                PaymentStatus = "Approved"
            });
        }
    }
}