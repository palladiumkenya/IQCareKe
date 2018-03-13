using System.Threading.Tasks;
using IQCare.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IQCare.Common.Infrastructure
{
    public interface ICommonUnitOfWork
    {
        void Save();
        Task SaveAsync();
        DbContext Context { get; }
        IRepository<T> Repository<T>() where T : class;
    }
}