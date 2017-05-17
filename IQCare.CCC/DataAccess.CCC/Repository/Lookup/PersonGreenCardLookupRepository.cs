using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.CCC.Repository.Lookup
{
    public class PersonGreenCardLookupRepository : BaseRepository<PersonGreenCardLookup>, IPersonGreenCardLookupRepository
    {
        private readonly GreencardContext _context;

        public PersonGreenCardLookupRepository():this(new GreencardContext())
        {

        }

        public PersonGreenCardLookupRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
