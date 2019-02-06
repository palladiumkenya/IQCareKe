using System.Threading.Tasks;

namespace IQCare.SharedKernel.Infrastructure.Helpers
{
    public interface IConnectionString
    {
        Task<string> GetConnectionString(string iqcareUri);
    }
}