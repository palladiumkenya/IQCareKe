using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.enrollment;
using DataAccess.Context;
using Entities.CCC.Enrollment;

namespace DataAccess.CCC.Repository.Enrollment
{
    public class PatientEnrollmentRepository:BaseRepository<PatientEntityEnrollment>,IPatientEnrollmentRepository
    {
        private readonly GreencardContext _context;

        public PatientEnrollmentRepository() : this(new GreencardContext())
        {
            
        }

        public PatientEnrollmentRepository(GreencardContext context) : base(context)
        {
            _context =context;
        }
    }
}
