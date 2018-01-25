using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Triage;
using DataAccess.Context;
using Entities.CCC.Triage;

namespace DataAccess.CCC.Repository.Triage
{
    public class PregnancyOutcomeLookupRepository : BaseRepository<PregnancyOutcomeLookup>, IPregnancyOutcomeLookupRepository
    {
        private readonly LookupContext _context;

        public PregnancyOutcomeLookupRepository() : this(new LookupContext())
        {

        }

        public PregnancyOutcomeLookupRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
