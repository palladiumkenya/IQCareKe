using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;
using DataAccess.CCC.Context;

namespace DataAccess.CCC.Repository.Lookup
{
    public class PersonLookUpRepository : BaseRepository<PersonLookUp>, IPersonLookUpRepository
    {
        private readonly LookupContext _context;

        public PersonLookUpRepository():this(new LookupContext())
        {

        }

        public PersonLookUpRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
       
    }
}
