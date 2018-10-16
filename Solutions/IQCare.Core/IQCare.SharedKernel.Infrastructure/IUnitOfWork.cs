using IQCare.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IQCare.SharedKernel.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        void Save();
        Task SaveAsync();
        DbContext Context { get; }
        IRepository<T> Repository<T>() where T : class;
    }
}
