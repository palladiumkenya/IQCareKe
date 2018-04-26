using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.PSmart;

namespace DataAccess.WebApi.Repository
{
    public class PatientAddressRepository : BaseRepository<PATIENTADDRESS>, IPatientAddressRepository
    {
        private readonly PsmartContext _context;
        public PatientAddressRepository():this(new PsmartContext())
        {
        }

        public PatientAddressRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}