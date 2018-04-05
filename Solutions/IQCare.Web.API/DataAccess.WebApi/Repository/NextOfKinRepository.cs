using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class NextOfKinRepository : BaseRepository<NEXTOFKIN>, INextOfKinRepository
    {
        private readonly PsmartContext _context;
        public NextOfKinRepository():this(new PsmartContext())
        {
        }

        public NextOfKinRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}