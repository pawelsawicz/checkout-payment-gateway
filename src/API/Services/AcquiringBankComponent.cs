using System.Threading.Tasks;

namespace API.Services
{
    public class AcquiringBankComponent : IBankComponent
    {
        public Task<BankPaymentResponse> ProcessPayment()
        {
            throw new System.NotImplementedException();
        }
    }
}