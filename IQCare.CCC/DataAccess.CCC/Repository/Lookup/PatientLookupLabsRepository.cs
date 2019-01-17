using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CCC.Repository.Lookup
{
    public class PatientLookupLabsRepository : BaseRepository<PatientLab>, IPatientLookupLabsRepository
    {
        private readonly LookupContext _context;

        public PatientLookupLabsRepository() : this(new LookupContext())
        {

        }

        public PatientLookupLabsRepository(LookupContext context) : base(context)
        {
            _context = context;
        }

    }
}