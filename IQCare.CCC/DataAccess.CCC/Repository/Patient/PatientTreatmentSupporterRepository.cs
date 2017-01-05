
using DataAccess.Context;
using Entities.PatientCore;

namespace DataAccess.CCC.Repository.Patient
{
    public class PatientTreatmentSupporterRepository :BaseRepository<PatientTreatmentSupporter>,IPatientTreatmentSupporterRepository
    {
        private readonly GreencardContext _context;

        public PatientTreatmentSupporterRepository() : this(new GreencardContext())
        {

        }

        public PatientTreatmentSupporterRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
