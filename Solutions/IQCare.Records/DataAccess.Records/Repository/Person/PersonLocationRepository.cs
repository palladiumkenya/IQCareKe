using DataAccess.Context;
using DataAccess.Records.Interface;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Repository
{

    public class PersonLocationRepository : BaseRepository<PersonLocation>, IPersonLocationRepository
    {
        private readonly PersonContext _context;

        public PersonLocationRepository() : this(new PersonContext())
        {

        }

        public PersonLocationRepository(PersonContext context) : base(context)
        {
            _context = context;
        }

    }
}
