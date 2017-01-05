
using DataAccess.CCC.Interfaces;
using DataAccess.Context;
using Entities.PatientCore;

namespace DataAccess.CCC.Repository
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        private readonly GreencardContext _context;

        public PatientRepository() :this(new GreencardContext())
        {

        }

        public PatientRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
