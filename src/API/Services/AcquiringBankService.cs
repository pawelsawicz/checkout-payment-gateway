using System.Threading.Tasks;

namespace API.Services
{
    public sealed class AcquiringBankService : IAcquiringBankService
    {
        public Task<AcquiringBankPaymentResponse> ProcessPayment(AcquiringBankPaymentRequest acquiringBankPaymentRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}