using IQCare.SharedKernel.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQCare.Maternity.Infrastructure.Repository
{
    public class MaternityRepository<TEntity> : GenericRepository<TEntity>, IMaternityRepository<TEntity> where TEntity : class
    {
        public MaternityRepository(MaternityDbContext maternityDbContext) : base(maternityDbContext)
        {

        }
    }
}
