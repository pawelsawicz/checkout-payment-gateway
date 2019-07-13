using System.Threading.Tasks;

namespace API.Tests
{
    public interface IBankComponent
    {
        Task<bool> ProcessPayment();
    }
}