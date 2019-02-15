using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using IQCare.AIR.Infrastructure.Repository;
using IQCare.SharedKernel.Infrastructure;
using IQCare.SharedKernel.Interfaces;

namespace IQCare.AIR.Infrastructure.UnitOfWork
{
    public class AirUnitOfWork : GenericUnitOfWork, IAirUnitOfWork
    {
        private AirDbContext dbContext;
        private Hashtable repositories;

        public AirUnitOfWork(AirDbContext _dbContext) : base(_dbContext)
        {
            dbContext = _dbContext;
        }
        public override IRepository<T> Repository<T>()
        {
            if (repositories == null)
                repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(AirRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), dbContext);
                repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)repositories[type];
        }
    }
}
