using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entity.WebApi.PSmart;

namespace DataAccess.WebApi.Repository
{
    public class PSmartAuthRepository:BaseRepository<PsmartAuthUser>,IPSmartAuthRepository
    {
        private readonly PsmartContext _context;

        public PSmartAuthRepository() : this(new PsmartContext())
        {

        }

        public PSmartAuthRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}