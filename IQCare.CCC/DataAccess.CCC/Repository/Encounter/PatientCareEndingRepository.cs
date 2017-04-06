using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Encounter;
using DataAccess.Context;
using Entities.CCC.Encounter;

namespace DataAccess.CCC.Repository.Encounter
{
    public class PatientCareEndingRepository:BaseRepository<PatientCareEnding>, IPatientCareEndingRepository
    {
        private readonly GreencardContext _context;

        public PatientCareEndingRepository():base(new GreencardContext())
        {
            
        }

        public PatientCareEndingRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
