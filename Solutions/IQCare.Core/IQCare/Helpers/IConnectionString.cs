using System.Threading.Tasks;

namespace IQCare.Helpers
{
    public interface IConnectionString
    {
        Task<string> GetConnectionString();
    }
}