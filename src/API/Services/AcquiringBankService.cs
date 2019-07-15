using System.Threading.Tasks;
using Serilog;

namespace API.Services
{
    public sealed class AcquiringBankService : IAcquiringBankService
    {
        private static readonly ILogger Logger = Log.Logger.ForContext<AcquiringBankService>();
        
        public Task<AcquiringBankPaymentResponse> ProcessPayment(AcquiringBankPaymentRequest acquiringBankPaymentRequest)
        {
            Logger.Error("Entering {ProcessPayment} in {AcquiringBankService}, this service is not live yet!",
                nameof(ProcessPayment),
                nameof(AcquiringBankService));
            
            throw new System.NotImplementedException("Service is not live yet!");
        }
    }
}