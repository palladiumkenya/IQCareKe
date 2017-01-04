using DataAccess.CCC.Interfaces;
using DataAccess.Context;
using Entities.PatientCore;

namespace DataAccess.CCC.Repository
{
   public class PatientContactRepository :BaseRepository<PatientContact>,IPatientContactRepository
    {
        private readonly GreencardContext _context;

        public PatientContactRepository() :this(new GreencardContext())
        {

        }

        public PatientContactRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
