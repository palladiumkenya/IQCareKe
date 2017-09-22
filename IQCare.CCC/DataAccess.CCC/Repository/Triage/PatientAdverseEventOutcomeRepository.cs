using DataAccess.Context;
using Entities.CCC.Triage;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Triage;

namespace DataAccess.CCC.Repository.Triage
{
    public class PatientAdverseEventOutcomeRepository :BaseRepository<PatientAdverseEventsOutcome>,IPatientAdverseEventOutcomeRepository
    {
        private readonly GreencardContext _context;

        public PatientAdverseEventOutcomeRepository() :this(new GreencardContext())
        {

        }

        public PatientAdverseEventOutcomeRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
