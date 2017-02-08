using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.Context;
using Entities.CCC.Baseline;

namespace DataAccess.CCC.Repository.Baseline
{
    public class PatientHivEnrollmentBaselineRepository:BaseRepository<PatientHivEnrollmentBaseline>,IPatientHivEnrollmentBaselineRepository
   {
       private readonly GreencardContext _context;

       public PatientHivEnrollmentBaselineRepository():this(new GreencardContext())
       {
           
       }

       public PatientHivEnrollmentBaselineRepository(GreencardContext context) : base(context)
       {
           _context = context;
       }

   }
}
