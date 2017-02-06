using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Baseline;
using DataAccess.Context;
using Entities.CCC.Baseline;

namespace DataAccess.CCC.Repository.Baseline
{
    public class PatientTransferInRepository:BaseRepository<PatientTransferIn>,IPatientTransferInRepository
  {
      private readonly GreencardContext _context;

      public PatientTransferInRepository():this(new GreencardContext())
       {

        }

      public PatientTransferInRepository(GreencardContext context) : base(context)
       {
            _context = context;
        }


    }
}
