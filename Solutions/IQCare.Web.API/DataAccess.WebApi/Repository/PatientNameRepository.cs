using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class PatientNameRepository : BaseRepository<PATIENTNAME>, IPatientNameRepository
    {
        private readonly PsmartContext _context;

        public PatientNameRepository():this(new PsmartContext())
        {

        }

        public PatientNameRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}