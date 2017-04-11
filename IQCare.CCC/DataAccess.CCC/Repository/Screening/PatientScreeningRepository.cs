using DataAccess.CCC.Context;
using DataAccess.CCC.Interface;
using DataAccess.Context;
using Entities.CCC.Screening;

namespace DataAccess.CCC.Repository.Screening
{
    public class PatientScreeningRepository:BaseRepository<PatientScreening>,IPatientScreeningRepository
    {
        private GreencardContext _context;

        public PatientScreeningRepository():this(new GreencardContext())
        {

        }

        public PatientScreeningRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
