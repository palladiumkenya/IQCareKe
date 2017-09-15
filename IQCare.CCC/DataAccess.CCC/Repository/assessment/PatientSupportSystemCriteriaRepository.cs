using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.assessment;
using DataAccess.Context;
using Entities.CCC.Assessment;

namespace DataAccess.CCC.Repository.assessment
{
    public class PatientSupportSystemCriteriaRepository:BaseRepository<PatientSupportSystemCriteria>,IPatientSupportSystemCriteriaRepository
    {
        private GreencardContext _context;

        public PatientSupportSystemCriteriaRepository():base(new GreencardContext())
        {

        }

        public PatientSupportSystemCriteriaRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
