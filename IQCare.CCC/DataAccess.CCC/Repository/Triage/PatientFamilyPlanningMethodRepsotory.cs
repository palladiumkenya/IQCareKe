using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Triage;
using DataAccess.Context;
using Entities.CCC.Triage;

namespace DataAccess.CCC.Repository.Triage
{

    public class PatientFamilyPlanningMethodRepsotory : BaseRepository<PatientFamilyPlanningMethod>,IPatientFamilyPlanningMethodRepository
    {
        private readonly GreencardContext _context;

        public PatientFamilyPlanningMethodRepsotory() :this(new GreencardContext())
        {

        }

        public PatientFamilyPlanningMethodRepsotory(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
