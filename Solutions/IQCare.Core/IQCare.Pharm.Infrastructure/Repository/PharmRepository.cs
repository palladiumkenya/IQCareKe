using IQCare.SharedKernel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Pharm.Infrastructure.Repository
{
    public class PharmRepository<TEntity>: GenericRepository<TEntity>,IPharmRepository<TEntity> where TEntity: class
    {

        public PharmRepository(PharmDbContext pharmDbContext) : base(pharmDbContext)
        {

        }
    }
}
