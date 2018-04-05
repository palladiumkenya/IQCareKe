using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class NokNameRepository : BaseRepository<NOKNAME>, INokNameRepository
    {
        private readonly PsmartContext _context;
        public NokNameRepository():this(new PsmartContext())
        {
        }

        public NokNameRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}