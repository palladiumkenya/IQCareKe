using Common.Data.Repository;
using PatientManagement.Core.Interfaces;

namespace PatientManagement.Data.Repository
{
    public class PatientRepository : BaseRepository<PatientManagement.Core.Model.Patient>, IPatientRepository
    {
        private readonly PatientContext _context;

        public PatientRepository() :this(new PatientContext())
        {

        }

        public PatientRepository(PatientContext context) : base(context)
        {
            _context = context;
        }
    }
}
