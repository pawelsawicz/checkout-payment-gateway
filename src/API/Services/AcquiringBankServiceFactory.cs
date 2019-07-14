using Microsoft.Extensions.Configuration;

namespace API.Services
{
    public static class AcquiringBankServiceFactory
    {
        public static IAcquiringBankService Create(IConfiguration configuration)
        {
            var useLiveService = configuration.GetValue<bool>("LIVE_ACQUIRING_BANK_SERVICE");
            if (useLiveService)
            {
                return new AcquiringBankService();
            }

            return new FakeAcquiringBankServiceRandomResponse();
        }
    }
}