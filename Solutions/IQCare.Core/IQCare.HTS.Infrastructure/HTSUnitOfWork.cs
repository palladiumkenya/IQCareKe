using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using IQCare.Common.Infrastructure;
using IQCare.SharedKernel.Interfaces;

namespace IQCare.HTS.Infrastructure
{
    public class HTSUnitOfWork : IHTSUnitOfWork
    {
        private readonly HtsDbContext _context;
        private Hashtable repositories;
        // Flag: Has Dispose already been called?
        private bool disposed = false;

        public HTSUnitOfWork(HtsDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // public implementation of dispose pattern callable by consumers
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if(this.disposed)
                return;
            if (disposing)
            {
                // Free any other managed objects here
                _context.Dispose();
            }

            // Free any unmanaged objects here
            this.disposed = true;
        }


        public DbContext Context { get { return _context; } }

        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories == null)
                repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(HTSRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)repositories[type];
        }

        public void Save()
            => _context.SaveChanges();

        public async Task SaveAsync()
            => await _context.SaveChangesAsync();

    }
}

