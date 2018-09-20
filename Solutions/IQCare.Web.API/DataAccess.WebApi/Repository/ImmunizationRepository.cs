using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.PSmart;

namespace DataAccess.WebApi.Repository
{
    public class ImmunizationRepository : BaseRepository<IMMUNIZATION>, IImmunizationRepository
    {
        private readonly PsmartContext _context;

        public ImmunizationRepository():this(new PsmartContext())
        {
        }

        public ImmunizationRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}