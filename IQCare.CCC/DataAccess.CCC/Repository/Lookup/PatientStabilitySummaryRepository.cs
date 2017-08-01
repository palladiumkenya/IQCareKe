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
    public class PatientStabilitySummaryRepository:BaseRepository<PatientStabilitySummary>,IPatientStabilitySummaryRepository
    {
        private readonly LookupContext _context;

        public PatientStabilitySummaryRepository() : this(new LookupContext())
        {

        }

        public PatientStabilitySummaryRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
