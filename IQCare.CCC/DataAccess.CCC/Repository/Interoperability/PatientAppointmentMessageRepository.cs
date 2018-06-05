using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Interoperability;
using DataAccess.Context;
using Entities.CCC.Interoperability;

namespace DataAccess.CCC.Repository.Interoperability
{
    public class PatientAppointmentMessageRepository : BaseRepository<PatientAppointmentMessage>,IPatientAppointmentMessageRepository
    {
        private readonly LookupContext _context;
        public PatientAppointmentMessageRepository() : this(new LookupContext())
        {
            
        }

        public PatientAppointmentMessageRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
