using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.IL;
using DataAccess.Context;
using Entities.CCC.IL;

namespace DataAccess.CCC.Repository.IL
{
    public class ILMessageViewerRepository : BaseRepository<ILMessageViewer>, iILMessageViewerRepository
    {
        private readonly LookupContext _context;

        public ILMessageViewerRepository(LookupContext context) : base(context)
        {
            _context = context;
        }

        protected ILMessageViewerRepository() : this(new LookupContext())
        {

        }
    }
}