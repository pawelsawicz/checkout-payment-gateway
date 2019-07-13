using System.Threading.Tasks;

namespace API.Services
{
    public interface IBankComponent
    {
        Task<bool> ProcessPayment();
    }
}