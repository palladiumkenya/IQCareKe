using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.Context;
using Entities.CCC.Baseline;

namespace DataAccess.CCC.Repository.Baseline
{
    public class PatientBaselineAssessmentRepository:BaseRepository<PatientBaselineAssessment>,IPatientBaselineAssessmentRepository
    {
        private readonly GreencardContext _context;

        public PatientBaselineAssessmentRepository() : this(new GreencardContext())
        {
            
        }

        public PatientBaselineAssessmentRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

    }
}
