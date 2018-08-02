using System.Threading.Tasks;
using IQCare.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IQCare.PMTCT.Infrastructure
{
    public class PmtctUnitOfWork :IPmtctUnitOfWork
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new System.NotImplementedException();
        }

        public DbContext Context { get; }
        public IRepository<T> Repository<T>() where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}