using DataAccess.Context;
using Entities.PatientCore;

namespace DataAccess.CCC.Repository.Patient
{
    public class PatientLocationRepository :BaseRepository<PatientLocation>,IPatientLocationRepository
    {
        private readonly GreencardContext _context;

        public PatientLocationRepository() : this(new GreencardContext())
        {

        }

        public PatientLocationRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
