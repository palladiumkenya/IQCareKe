using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class HivTestRepository : BaseRepository<HIVTEST>, IHivTestRepository
    {
        private readonly PsmartContext _context;
        public HivTestRepository():this(new PsmartContext())
        {
        }

        public HivTestRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}