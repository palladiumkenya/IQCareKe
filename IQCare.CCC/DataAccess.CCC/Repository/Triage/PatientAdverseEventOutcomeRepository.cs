using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Triage;
using DataAccess.Context;
using Entities.CCC.Triage;

namespace DataAccess.CCC.Repository.Triage
{
    public class PatientAdverseEventOutcomeRepository :BaseRepository<PatientAdverseEventOutcome>,IPatientAdverseEventOutcomeRepository
    {
        private GreencardContext _context;

        public PatientAdverseEventOutcomeRepository():this(new GreencardContext())
        {

        }

        public PatientAdverseEventOutcomeRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
