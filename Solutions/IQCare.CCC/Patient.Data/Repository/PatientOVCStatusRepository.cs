using Common.Data.Repository;
using PatientManagement.Core.Interfaces;
using PatientManagement.Core.Model;

namespace PatientManagement.Data.Repository
{
    public class PatientOVCStatusRepository :BaseRepository<PatientOVCStatus>,IPatientOVCStatusRepository
    {
        private readonly PatientContext _context;

        public PatientOVCStatusRepository():this(new PatientContext())
        { }

        public PatientOVCStatusRepository(PatientContext context) :base(context)
        {
            _context = context;
        }
    }
}
