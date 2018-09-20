using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.IL;
using DataAccess.Context;
using Entities.CCC.IL;

namespace DataAccess.CCC.Repository.IL
{
   
    public class IlMessengerRepository: BaseRepository<IlMessengerLog>,IIlMessengerRepository
    {
        public readonly LookupContext _context;

        public IlMessengerRepository():this(new LookupContext())
        {
        }

        public IlMessengerRepository(LookupContext context): base(context)
        {
            _context = context;
        }
    }
}