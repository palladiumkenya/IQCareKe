using DataAccess.CCC.Interfaces;
using DataAccess.Context;
using Entities.PatientCore;

namespace DataAccess.CCC.Repository
{
    public class PatientEnrollmentRepository :BaseRepository<PatientEnrollment>,IPatientEnrollmentRepository
    {
        private readonly GreencardContext _context;
        public PatientEnrollmentRepository() : this(new GreencardContext())
        {

        }

        public PatientEnrollmentRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
