using System.Threading.Tasks;

namespace API.Services
{
    public class FakeBankComponent : IBankComponent
    {
        public Task<bool> ProcessPayment()
        {
            return Task.FromResult(true);
        }
    }
}