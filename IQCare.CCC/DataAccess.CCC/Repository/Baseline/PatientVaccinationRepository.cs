using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.Context;
using Entities.CCC.Encounter;

namespace DataAccess.CCC.Repository.Baseline
{
    public class PatientVaccinationRepository : BaseRepository<PatientVaccination> , IPatientVaccinationRepository
    {
        private readonly GreencardContext _context;

        public PatientVaccinationRepository()
        {

        }

        public PatientVaccinationRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }

    }
}
