using DataAccess.CCC.Context;
using DataAccess.Context;
using Entities.Common;

namespace DataAccess.CCC.Repository.Patient
{
    public class PatientLocationRepository :BaseRepository<PersonLocation>,IPatientLocationRepository
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
