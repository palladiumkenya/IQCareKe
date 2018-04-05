using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class PatientPhysicalAddressRepository : BaseRepository<PHYSICALADDRESS>, IPhysicalAddressRepository
    {
        private readonly PsmartContext _context;

        public PatientPhysicalAddressRepository():this(new PsmartContext())
        {
        }

        public PatientPhysicalAddressRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}