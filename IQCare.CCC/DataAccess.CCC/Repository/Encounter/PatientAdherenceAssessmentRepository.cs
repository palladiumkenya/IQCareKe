using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Triage;

namespace DataAccess.CCC.Repository.Encounter
{
    public class PatientAdherenceAssessmentRepository:BaseRepository<PatientAdherenceAssessment>, IPatientAdherenceAssessmentRepository
    {
        private readonly GreencardContext _context;

        public PatientAdherenceAssessmentRepository() : this(new GreencardContext())
        {
        }

        public PatientAdherenceAssessmentRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
