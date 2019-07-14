using System.Threading.Tasks;

namespace API.Services
{
    public class AcquiringBankService : IAcquiringBankService
    {
        public Task<BankPaymentResponse> ProcessPayment(BankPaymentRequest bankPaymentRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}