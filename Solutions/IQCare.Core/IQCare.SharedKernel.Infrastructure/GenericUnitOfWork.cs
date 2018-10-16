using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IQCare.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IQCare.SharedKernel.Infrastructure
{
    public abstract class GenericUnitOfWork : IUnitOfWork
    {
        private BaseContext _context;
        private bool disposed;

        public GenericUnitOfWork(BaseContext context)
        {
            _context = context;
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
            => _context.SaveChanges();

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();

        public DbContext Context { get { return _context; } }

        public abstract IRepository<T> Repository<T>() where T : class;

    }
}
