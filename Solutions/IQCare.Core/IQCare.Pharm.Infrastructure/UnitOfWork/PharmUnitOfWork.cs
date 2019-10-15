using System;
using System.Collections.Generic;
using System.Text;
using IQCare.SharedKernel.Infrastructure;
using IQCare.SharedKernel.Interfaces;
using IQCare.Pharm.Infrastructure.Repository;
using System.Collections;

namespace IQCare.Pharm.Infrastructure.UnitOfWork
{
    public class PharmUnitOfWork : GenericUnitOfWork, IPharmUnitOfWork
    {
        Hashtable repositories;
        PharmDbContext pharmDbContext;


        public PharmUnitOfWork(PharmDbContext _pharmDbContext) : base(_pharmDbContext)
        {
            pharmDbContext = _pharmDbContext;
        }

        public override IRepository<T> Repository<T>()
        {
            if (repositories == null)
                repositories = new Hashtable();
            var type = typeof(T).Name;


            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(PharmRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), pharmDbContext);
                repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)repositories[type];
        }
    }
}

