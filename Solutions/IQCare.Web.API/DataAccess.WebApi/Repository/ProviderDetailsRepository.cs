using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class ProviderDetailsRepository : BaseRepository<PROVIDERDETAILS>, IProviderDetailsRepository
    {
        private readonly PsmartContext _context;
        public ProviderDetailsRepository():this(new PsmartContext())
        {
        }

        public ProviderDetailsRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}