using System.Threading.Tasks;

namespace API.Services
{
    public interface IAcquiringBankService
    {
        Task<AcquiringBankPaymentResponse> ProcessPayment(AcquiringBankPaymentRequest acquiringBankPaymentRequest);
    }
}