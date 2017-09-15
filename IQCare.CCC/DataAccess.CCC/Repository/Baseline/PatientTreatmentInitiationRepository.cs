using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.Context;
using Entities.CCC.Baseline;
using Interface.CCC;

namespace DataAccess.CCC.Repository.Baseline
{
    public class PatientTreatmentInitiationRepository:BaseRepository<PatientTreatmentInitiation>,IPatientTreatmentInitiationRepository
    {
        private readonly GreencardContext _context;


        public PatientTreatmentInitiationRepository() : this(new GreencardContext())
        {
        }

        public PatientTreatmentInitiationRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
