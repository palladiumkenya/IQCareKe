using DataAccess.Context;
using DataAccess.Records.Context;
using DataAccess.Records.Interface;
using Entities.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Repository.Lookup
{
    public class PatientRegistrationLookupRepository : BaseRepository<PatientRegistrationLookup>, IPatientRegistrationLookupRepository
    {
        private readonly LookupContext _context;

        public PatientRegistrationLookupRepository() : this(new LookupContext())
        {

        }

        public PatientRegistrationLookupRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
