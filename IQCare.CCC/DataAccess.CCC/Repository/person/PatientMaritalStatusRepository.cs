using DataAccess.CCC.Interface.person;
using DataAccess.Context;
using Entities.PatientCore;

namespace DataAccess.CCC.Repository.person
{
    public class PatientMaritalStatusRepository :BaseRepository<PatientMaritalStatus>,IPatientMaritalStatusRepository
    {
        private readonly PersonContext _context;

        public PatientMaritalStatusRepository() : this(new PersonContext())
        {

        }

        public PatientMaritalStatusRepository(PersonContext context) : base(context)
        {
            _context = context;
        }

    }
}
