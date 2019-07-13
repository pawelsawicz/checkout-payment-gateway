using System.Threading.Tasks;

namespace API.Tests
{
    public class FakeBankComponent : IBankComponent
    {
        public Task<bool> ProcessPayment()
        {
            return Task.FromResult(true);
        }
    }
}