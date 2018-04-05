using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class MotherDetailsRepository : BaseRepository<MOTHERDETAILS>, IMotherDetailsRepository
    {
        private readonly PsmartContext _context;
        public MotherDetailsRepository():this(new PsmartContext())
        {
        }

        public MotherDetailsRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}