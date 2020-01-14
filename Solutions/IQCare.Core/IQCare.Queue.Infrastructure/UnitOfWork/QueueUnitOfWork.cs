using IQCare.Queue.Infrastructure.Repository;
using IQCare.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.Queue.Infrastructure.UnitOfWork
{
   public class QueueUnitOfWork:IQueueUnitOfWork
    {
        private readonly QueueDbContext _context;
        private Hashtable repositories;
        // Flag: Has Dispose already been called?
        private bool disposed = false;

        public QueueUnitOfWork(QueueDbContext context)
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
            => _context.SaveChanges();

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();

        public DbContext Context { get { return _context; } }

        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories == null)
                repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(QueueRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)repositories[type];
        }

    }
}
