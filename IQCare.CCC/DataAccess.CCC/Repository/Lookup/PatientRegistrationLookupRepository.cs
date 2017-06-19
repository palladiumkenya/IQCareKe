using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class PatientRegistrationLookupRepository : BaseRepository<PatientRegistrationLookup> , IPatientRegistrationLookupRepository
    {
        private readonly LookupContext _context;

        public PatientRegistrationLookupRepository():this(new LookupContext())
        {
            
        }

        public PatientRegistrationLookupRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
