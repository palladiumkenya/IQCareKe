using System;
using System.Collections.Generic;
using System.Text;
using IQCare.SharedKernel.Infrastructure;
using IQCare.SharedKernel.Interfaces;
using IQCare.Prep.Infrastructure.Repository;
using System.Collections;

namespace IQCare.Prep.Infrastructure.UnitOfWork
{
    public class PrepUnitOfWork : GenericUnitOfWork, IPrepUnitOfWork
    {
        Hashtable repositories;
        PrepDbContext prepDbContext;

        public PrepUnitOfWork(PrepDbContext _prepDbContext) : base(_prepDbContext)
        {
            prepDbContext = _prepDbContext;
        }

        public override IRepository<T> Repository<T>()
        {

            if (repositories == null)
                repositories = new Hashtable();

            var type = typeof(T).Name;


            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(PrepRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), prepDbContext);
                repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)repositories[type];
        }
    }
}