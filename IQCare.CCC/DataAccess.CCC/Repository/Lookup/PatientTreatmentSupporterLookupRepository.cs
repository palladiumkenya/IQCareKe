using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;
using Entities.PatientCore;

namespace DataAccess.CCC.Repository.Lookup
{
    public class PatientTreatmentSupporterLookupRepository : BaseRepository<PatientTreatmentSupporterLookup>, IPatientTreatmentSupporterLookupRepository
    {
        private readonly LookupContext _context;

        public PatientTreatmentSupporterLookupRepository():this(new LookupContext())
        {
            
        }

        public PatientTreatmentSupporterLookupRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
