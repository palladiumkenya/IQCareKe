using DataAccess.Context;
using DataAccess.Records.Context;
using DataAccess.Records.Interface;
using Entities.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Repository
{
    public class PersonContactLookUpRepository : BaseRepository<PersonContactLookUp>, IPersonContactLookUpRepository
    {
        private readonly LookupContext _context;

        public PersonContactLookUpRepository() : this(new LookupContext())
        {

        }

        public PersonContactLookUpRepository(LookupContext context) : base(context)
        {
            _context = context;
        }

    }
}
