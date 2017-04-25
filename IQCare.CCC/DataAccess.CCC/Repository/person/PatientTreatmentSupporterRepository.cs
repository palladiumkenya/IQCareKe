using DataAccess.CCC.Interface.person;
using DataAccess.Context;
using Entities.PatientCore;

namespace DataAccess.CCC.Repository.person
{
   public class PatientTreatmentSupporterRepository:BaseRepository<PatientTreatmentSupporter>,IPatientTreatmentSupporterRepository
    {
        private readonly PersonContext _context;


        public PatientTreatmentSupporterRepository() : this(new PersonContext())
        {

        }

        public PatientTreatmentSupporterRepository(PersonContext context) : base(context)
        {
            _context = context;
        }
    }
}
