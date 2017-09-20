using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Interoperability;
using DataAccess.Context;
using Entities.CCC.Interoperability;

namespace DataAccess.CCC.Repository.Interoperability
{
    public class PatientMessageRepository:BaseRepository<PatientMessage>,IPatientMessageRepository
    {
        private readonly LookupContext _context;

        public PatientMessageRepository() : this(new LookupContext())
        {

        }

        public PatientMessageRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
