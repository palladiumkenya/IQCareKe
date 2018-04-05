using DataAccess.Context;
using DataAccess.WebApi.Interface;
using Entities.CCC.psmart;

namespace DataAccess.WebApi.Repository
{
    public class PatientIdentificationRepository : BaseRepository<PATIENTIDENTIFICATION>, IPatientIdentificationRepository
    {
        private readonly PsmartContext _context;

        public PatientIdentificationRepository() :this(new PsmartContext())
        {
        }

        public PatientIdentificationRepository(PsmartContext context) : base(context)
        {
            _context = context;
        }
    }
}