using System;
using System.Threading.Tasks;

namespace API.Services.FakeAcquiringBankImpls
{
    public sealed class FakeAcquiringBankServiceWithSuccessfulResponse : IAcquiringBankService
    {
        public const string ReturnedStatusCode = "Approved";
        
        public Task<AcquiringBankPaymentResponse> ProcessPayment(AcquiringBankPaymentRequest acquiringBankPaymentRequest)
        {
            return Task.FromResult(
                new AcquiringBankPaymentResponse(Guid.NewGuid().ToString(), ReturnedStatusCode));
        }
    }
}