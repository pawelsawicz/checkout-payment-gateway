using API.Services.FakeAcquiringBankImpls;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace API.Services
{
    public static class AcquiringBankServiceFactory
    {
        private static readonly ILogger Logger = Log.Logger;
        
        public static IAcquiringBankService Create(IConfiguration configuration)
        {
            var useLiveService = configuration.GetValue<bool>("LIVE_ACQUIRING_BANK_SERVICE");
            
            if (useLiveService)
            {
                return new AcquiringBankService();
            }

            Logger.Warning("Creating an instance of FakeAcquiringBankServiceRandomResponse");
            
            return new FakeAcquiringBankServiceRandomResponse();
        }
    }
}