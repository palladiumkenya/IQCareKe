using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.enrollment;
using DataAccess.Context;
using Entities.CCC.Enrollment;

namespace DataAccess.CCC.Repository.Enrollment
{
    public class PatientReEnrollmentRepository : BaseRepository<PatientReEnrollment> , IPatientReEnrollmentRepository
    {
        private readonly GreencardContext _context;

        public PatientReEnrollmentRepository():this(new GreencardContext())
        {

        }

        public PatientReEnrollmentRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
