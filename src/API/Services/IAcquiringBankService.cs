using System.Threading.Tasks;

namespace API.Services
{
    public interface IAcquiringBankService
    {
        Task<BankPaymentResponse> ProcessPayment();
    }
}