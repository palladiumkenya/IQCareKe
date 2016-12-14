using Common.Data;
using Common.Data.Repository;
using PatientManagement.Core.Interfaces;
using PatientManagement.Core.Model;

namespace PatientManagement.Data.Repository
{
    public class PatientLocationRepository :BaseRepository<PatientLocation>,IPatientLocationRepository
    {
        private readonly PatientContext _context;

        public PatientLocationRepository() : this(new PatientContext())
        {

        }

        public PatientLocationRepository(PatientContext context) : base(context)
        {
            _context = context;
        }
    }
}
