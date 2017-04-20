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
    public class PatientServiceEnrollmentLookupRepository:BaseRepository<PatientServiceEnrollmentLookup>, IPatientServiceEnrollmentLookupRepository
    {
        private readonly LookupContext _context;

        public PatientServiceEnrollmentLookupRepository():this(new LookupContext())
        {

        }

        public PatientServiceEnrollmentLookupRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
