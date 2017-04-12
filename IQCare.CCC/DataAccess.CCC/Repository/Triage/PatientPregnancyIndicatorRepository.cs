using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Triage;
using DataAccess.Context;
using Entities.CCC.Triage;

namespace DataAccess.CCC.Repository.Triage
{
    public class PatientPregnancyIndicatorRepository:BaseRepository<PatientPregnancyIndicator>,IPatientPregnancyIndicatorRepository
    {
        private GreencardContext _context;

        public PatientPregnancyIndicatorRepository() : this(new GreencardContext())
        {

        }

        public PatientPregnancyIndicatorRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
