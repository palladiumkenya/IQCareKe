using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;
using Entities.CCC.PSmart;

namespace DataAccess.WebApi.Repository
{
    public class MotherDetailsViewRepository : BaseRepository<MotherDetailsView>, IMotherDetailsViewRepository
    {
        private readonly PsmartContext _context;
        public MotherDetailsViewRepository() :this(new PsmartContext())
        {
        }

        public MotherDetailsViewRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}