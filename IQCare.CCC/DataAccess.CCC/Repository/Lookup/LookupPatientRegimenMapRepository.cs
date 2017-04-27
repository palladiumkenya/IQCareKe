using System.Collections.Generic;
using System.Linq;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;
using DataAccess.CCC.Context;

namespace DataAccess.CCC.Repository.Lookup
{
    public class LookupPatientRegimenMapRepository : BaseRepository<PatientRegimenLookup>, ILookupPatientRegimenMap
    {
        private readonly LookupContext _context;

        public LookupPatientRegimenMapRepository() : this(new LookupContext())
        {
        }

        public LookupPatientRegimenMapRepository(LookupContext context) : base(context)
        {
            _context = context;
        }

        public PatientRegimenLookup GetPatientCurrentRegimen(int patientId)
        {
            LookupPatientRegimenMapRepository patientRegimenMapLookup = new LookupPatientRegimenMapRepository();
            var regimen =
                patientRegimenMapLookup.FindBy(x => x.Id == patientId).OrderByDescending(x => x.Id).FirstOrDefault();
            return regimen;

        }

       public List<PatientRegimenLookup> GetPatientRegimenList(int patientId)
        {
            LookupPatientRegimenMapRepository patientRegimenMapLookup = new LookupPatientRegimenMapRepository();
            var regimenList = patientRegimenMapLookup.FindBy(x => x.Id == patientId).OrderBy(x => x.Id).ToList();
            return regimenList;
        }
    }
}
