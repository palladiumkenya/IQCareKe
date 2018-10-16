using IQCare.Lab.Infrastructure.Interface;
using IQCare.SharedKernel.Infrastructure;

namespace IQCare.Lab.Infrastructure
{
    public class LabRepository<TEntity> : GenericRepository<TEntity>, ILabRepository<TEntity> where TEntity : class
    {
        public LabRepository(LabDbContext dbContext) : base(dbContext)
        {
        }
    }
}