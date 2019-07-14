using System;
using System.Threading.Tasks;

namespace API.Services
{
    public sealed class FakeAcquiringBankServiceRandomResponse : IAcquiringBankService
    {
        public Task<BankPaymentResponse> ProcessPayment(BankPaymentRequest bankPaymentRequest)
        {
            var failedResponse = new FakeAcquiringBankServiceWithFailedResponse();
            var successfulResponse = new FakeAcquiringBankServiceWithSuccessfulResponse();
            var randomN = new Random().Next(1, 100);

            return (randomN % 2) == 0
                ? failedResponse.ProcessPayment(bankPaymentRequest)
                : successfulResponse.ProcessPayment(bankPaymentRequest);
        }
    }
}