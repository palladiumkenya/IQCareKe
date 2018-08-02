using System;
using System.Collections;
using System.Threading.Tasks;
using IQCare.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IQCare.PMTCT.Infrastructure
{
    public class PmtctUnitOfWork :IPmtctUnitOfWork
    {
        private readonly PmtctDbContext _context;
        private Hashtable repositories;
        // Flag: Has Dispose already been called?
        private bool disposed = false;

        public PmtctUnitOfWork(PmtctDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
                return;
            if (disposing)
            {
                // Free any other managed objects here
                _context.Dispose();
            }

            // Free any unmanaged objects here
            this.disposed = true;
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new System.NotImplementedException();
        }

        public DbContext Context { get { return _context; } }
        public IRepository<T> Repository<T>() where T : class
        {
            throw new System.NotImplementedException();
        }
    }
}