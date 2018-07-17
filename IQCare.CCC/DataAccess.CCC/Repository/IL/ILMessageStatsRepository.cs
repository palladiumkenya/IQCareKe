using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.IL;
using DataAccess.Context;
using Entities.CCC.IL;

namespace DataAccess.CCC.Repository.IL
{
    public class ILMessageStatsRepository : BaseRepository<ILMessageStats>, iILMessageStatsRepository
    {
        private readonly LookupContext _context;

        public ILMessageStatsRepository(LookupContext context) : base(context)
        {
            _context = context;
        }

        protected ILMessageStatsRepository() : this(new LookupContext())
        {

        }
    }
}