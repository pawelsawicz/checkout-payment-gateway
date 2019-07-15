using System;
using System.Threading.Tasks;

namespace API.Services.FakeAcquiringBankImpls
{
    public sealed class FakeAcquiringBankServiceWithFailedResponse : IAcquiringBankService
    {
        public const string ReturnedStatusCode = "Failed";
        
        public Task<AcquiringBankPaymentResponse> ProcessPayment(AcquiringBankPaymentRequest acquiringBankPaymentRequest)
        {
            return Task.FromResult(
                new AcquiringBankPaymentResponse(Guid.NewGuid().ToString(), ReturnedStatusCode));
        }
    }
}