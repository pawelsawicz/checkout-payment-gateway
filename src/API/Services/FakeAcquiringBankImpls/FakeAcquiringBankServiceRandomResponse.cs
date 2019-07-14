using System;
using System.Threading.Tasks;

namespace API.Services.FakeAcquiringBankImpls
{
    public sealed class FakeAcquiringBankServiceRandomResponse : IAcquiringBankService
    {
        public Task<AcquiringBankPaymentResponse> ProcessPayment(AcquiringBankPaymentRequest acquiringBankPaymentRequest)
        {
            var failedResponse = new FakeAcquiringBankServiceWithFailedResponse();
            var successfulResponse = new FakeAcquiringBankServiceWithSuccessfulResponse();
            var randomN = new Random().Next(1, 100);

            return (randomN % 2) == 0
                ? failedResponse.ProcessPayment(acquiringBankPaymentRequest)
                : successfulResponse.ProcessPayment(acquiringBankPaymentRequest);
        }
    }
}