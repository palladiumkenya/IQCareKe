using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Triage;
using DataAccess.Context;
using Entities.CCC.Triage;

namespace DataAccess.CCC.Repository.Triage
{
    public class PatientPregnancyRepository:BaseRepository<PatientPreganancy>,IPatientPregnancyRepository
    {
        private GreencardContext _context;

        public PatientPregnancyRepository():this(new GreencardContext())
        {

        }

        public PatientPregnancyRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
