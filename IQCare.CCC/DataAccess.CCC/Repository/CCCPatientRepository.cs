using DataAccess.Context;

namespace DataAccess.CCC.Repository.Patient
{
    public class CCCPatientRepository : BaseRepository<Entities.PatientCore.Patient>, ICCCPatientRepository
    {
        private readonly GreencardContext _context;

        public CCCPatientRepository() :this(new GreencardContext())
        {

        }
        public CCCPatientRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
        
    }
}
