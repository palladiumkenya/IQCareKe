using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.assessment;
using DataAccess.Context;
using Entities.CCC.Assessment;

namespace DataAccess.CCC.Repository.assessment
{
    public class PatientPsychosocialCriteriaRepository:BaseRepository<PatientPsychoscialCriteria>,IPatientPsychosocialCriteriaRepository
    {
        private GreencardContext _context;

        public PatientPsychosocialCriteriaRepository():base(new GreencardContext())
        {

        }

        public PatientPsychosocialCriteriaRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
