using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entity.WebApi.PSmart;

namespace DataAccess.WebApi.Repository
{
    public class SmartCardPatientListRepository:BaseRepository<PsmartEligibleList>,ISmartCardPatientListRepository
    {
        private readonly PsmartContext _context;

        public SmartCardPatientListRepository() : this(new PsmartContext())
        {

        }

        public SmartCardPatientListRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}