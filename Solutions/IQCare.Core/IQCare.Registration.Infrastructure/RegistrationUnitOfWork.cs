using IQCare.Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IQCare.SharedKernel.Interfaces;

namespace IQCare.Registration.Infrastructure
{
    public class RegistrationUnitOfWork : IRegistrationUnitOfWork
    {
        private readonly RegistrationContext _context;
        private Hashtable repositories;

        public RegistrationUnitOfWork(RegistrationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Dispose()
        {
            _context.Dispose();
        }


        public DbContext Context { get { return _context; } }

        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories == null)
                repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(RegistrationRepository<>);
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
