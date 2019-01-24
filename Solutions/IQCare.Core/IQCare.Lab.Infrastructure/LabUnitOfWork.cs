using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using IQCare.Lab.Infrastructure.Interface;
using IQCare.SharedKernel.Infrastructure;
using IQCare.SharedKernel.Interfaces;

namespace IQCare.Lab.Infrastructure
{
    public class LabUnitOfWork : GenericUnitOfWork, ILabUnitOfWork
    {
        private Hashtable repositories;
        private readonly LabDbContext labDbContext;

        public LabUnitOfWork(LabDbContext context) : base(context)
        {
            labDbContext = context;
        }

        public override IRepository<T> Repository<T>()
        {
            if (repositories == null)
                repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(LabRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), labDbContext);
                repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)repositories[type];
        }
    }
}
