using DataAccess.CCC.Interface.person;
using DataAccess.CCC.Repository.Patient;
using DataAccess.Context;
using Entities.PatientCore;

namespace DataAccess.CCC.Repository.person
{
    public class PatientOVCStatusRepository:BaseRepository<PatientOVCStatus>,IPatientOvcStatusRepository
    {
        private readonly PersonContext _context;

        public PatientOVCStatusRepository() : this(new PersonContext())
        {

        }

        public PatientOVCStatusRepository(PersonContext context) : base(context)
        {
            _context = context;
        }
    }
}
