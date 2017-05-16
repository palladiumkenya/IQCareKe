using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class PatientLookupAdhereenceRepository :BaseRepository<LookupPatientAdherence>,ILookupPatientAdherenceRepository
    {
        private readonly LookupContext _context;

        public PatientLookupAdhereenceRepository() : this(new LookupContext())
        {

        }

        public PatientLookupAdhereenceRepository(LookupContext context) : base(context)
       {
            _context = context;
        }

        public LookupPatientAdherence GetPatientAdherenceStatus(int patientId)
        {
            PatientLookupAdhereenceRepository patientAdheranceLookup = new PatientLookupAdhereenceRepository();
           var adherence= patientAdheranceLookup.FindBy(x => x.PatientId == patientId).OrderByDescending(x=>x.Id).FirstOrDefault();
           return adherence;
        }
    }
}
