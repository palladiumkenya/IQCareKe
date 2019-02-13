using System;
using System.Collections.Generic;
using System.Text;
using IQCare.SharedKernel.Infrastructure;

namespace IQCare.AIR.Infrastructure.Repository
{
    public class AirRepository<TEntity> : GenericRepository<TEntity>, IAirRepository<TEntity> where TEntity: class 
    {
        public AirRepository(AirDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
