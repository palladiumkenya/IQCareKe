using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Interoperability;
using DataAccess.Context;
using Entities.CCC.Interoperability;

namespace DataAccess.CCC.Repository.Interoperability
{
    public class PatientVitalsMessageRepository:BaseRepository<PatientVitalsMessage>, IPatientVitalsMessageRepository
    {
        private readonly LookupContext _context;

        public PatientVitalsMessageRepository() : this(new LookupContext())
        {

        }

        public PatientVitalsMessageRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
