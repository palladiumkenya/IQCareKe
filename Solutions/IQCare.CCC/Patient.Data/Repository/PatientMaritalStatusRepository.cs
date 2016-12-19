using Common.Data.Repository;
using PatientManagement.Core.Interfaces;
using PatientManagement.Core.Model;

namespace PatientManagement.Data.Repository
{
    public class PatientMaritalStatusRepository :BaseRepository<PatientMaritalStatus>,IPatientMaritalStatusRepository
    {
        private readonly PatientContext _context;

        public PatientMaritalStatusRepository() :this(new PatientContext())
        {
            
        }

        public PatientMaritalStatusRepository(PatientContext context) : base(context)
        {
            _context = context;
        }
    }
}
