using IQCare.SharedKernel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Prep.Infrastructure.Repository
{
   public class PrepRepository<TEntity> : GenericRepository<TEntity>,IPrepRepository<TEntity> where TEntity:class
    {
        public PrepRepository(PrepDbContext prepDbContext): base(prepDbContext)
        {

        }
    }
}
