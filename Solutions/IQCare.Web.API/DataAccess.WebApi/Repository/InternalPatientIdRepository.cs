using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.PSmart;

namespace DataAccess.WebApi.Repository
{
    public class InternalPatientIdRepository : BaseRepository<INTERNALPATIENTID>, IInternalPatientIdRepository
    {
        private readonly PsmartContext _context;
        public InternalPatientIdRepository():this(new PsmartContext())
        {
        }

        public InternalPatientIdRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}