using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Triage;
using DataAccess.Context;
using Entities.CCC.Triage;

namespace DataAccess.CCC.Repository.Triage
{


    public class PatientFamilyPlanningRepository :BaseRepository<PatientFamilyPlanning>,IPatientFamilyPlanningRepository
    {
        private readonly GreencardContext _context;

        public PatientFamilyPlanningRepository() :this(new GreencardContext())
        {

        }

        public PatientFamilyPlanningRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

    }
}
