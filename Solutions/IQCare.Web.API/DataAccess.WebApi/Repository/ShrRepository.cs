using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.PSmart;
using Entities.PSmart;

namespace DataAccess.WebApi.Repository
{
    public class ShrRepository:BaseRepository<SHR>,IShrRepository
    {
        private readonly PsmartContext _context;

        public ShrRepository() : this(new PsmartContext())
        {

        }

        public ShrRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
    public class PSmartLogRepository : BaseRepository<TransactionLog>, IPSmartTransactionLogRepository
    {
        private readonly PsmartContext _context;

        public PSmartLogRepository() : this(new PsmartContext())
        {

        }

        public PSmartLogRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}