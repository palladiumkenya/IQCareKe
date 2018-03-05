using IQCare.Common.Core.Interfaces.Repositories;
using IQCare.Common.Core.Models;
using IQCare.SharedKernel.Infrastructure.Repository;

namespace IQCare.Common.Infrastructure.Repository
{
    public class LookupItemViewRepository : BaseRepositoryLookup<LookupItemView>, ILookupItemViewRepository
    {
        public LookupItemViewRepository(CommonDbContext context) : base(context)
        {
        }
    }
}