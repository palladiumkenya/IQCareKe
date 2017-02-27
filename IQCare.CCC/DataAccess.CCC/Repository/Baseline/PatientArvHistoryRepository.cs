using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.Context;
using Entities.CCC.Baseline;

namespace DataAccess.CCC.Repository.Baseline
{
    public class PatientArvHistoryRepository:BaseRepository<PatientArvHistory>,IPatientArvHistoryRepository
   {

       private readonly GreencardContext _context;

        public PatientArvHistoryRepository():this(new GreencardContext() )
       {
           
       }

       public PatientArvHistoryRepository(GreencardContext context) : base(context)
       {
           _context = context;
       }
   }
}
