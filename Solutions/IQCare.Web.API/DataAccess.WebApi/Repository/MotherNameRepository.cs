using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class MotherNameRepository : BaseRepository<MOTHERNAME>, IMotherNameRepository
    {
        private readonly PsmartContext _context;
        public MotherNameRepository():this(new PsmartContext())
        {
        }

        public MotherNameRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}