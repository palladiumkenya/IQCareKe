using Common.Data.Repository;
using PatientManagement.Core.Interfaces;
using PatientManagement.Core.Model;


namespace PatientManagement.Data.Repository
{
   public class PatientContactRepository :BaseRepository<PatientContact>,IPatientContactRepository
    {
        private readonly PatientContext _context;

        public PatientContactRepository() :this(new PatientContext())
        {

        }

        public PatientContactRepository(PatientContext context) : base(context)
        {
            _context = context;
        }
    }
}
