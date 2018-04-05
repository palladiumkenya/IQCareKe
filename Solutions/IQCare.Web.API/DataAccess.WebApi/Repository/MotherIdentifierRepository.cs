using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class MotherIdentifierRepository : BaseRepository<MOTHERIDENTIFIER>, IMotherIdentifierRepository
    {
        private readonly PsmartContext _context;
        public MotherIdentifierRepository():this(new PsmartContext())
        {
        }

        public MotherIdentifierRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}