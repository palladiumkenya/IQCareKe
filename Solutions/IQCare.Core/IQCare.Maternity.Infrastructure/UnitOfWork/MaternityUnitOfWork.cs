using IQCare.Maternity.Infrastructure.Repository;
using IQCare.SharedKernel.Infrastructure;
using IQCare.SharedKernel.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.UnitOfWork
{
    public class MaternityUnitOfWork : GenericUnitOfWork, IMaternityUnitOfWork
    {
        Hashtable repositories;
        MaternityDbContext maternityDbContext;
        public MaternityUnitOfWork(MaternityDbContext _maternityDbContext) : base(_maternityDbContext)
        {
            maternityDbContext = _maternityDbContext;
        }
        public override IRepository<T> Repository<T>()
        {
            if (repositories == null)
                repositories = new Hashtable();

            var type = typeof(T).Name;

            if (!repositories.ContainsKey(type))
            {
                var repositoryType = typeof(MaternityRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), maternityDbContext);
                repositories.Add(type, repositoryInstance);
            }

            return (IRepository<T>)repositories[type];
        }
    }
}
