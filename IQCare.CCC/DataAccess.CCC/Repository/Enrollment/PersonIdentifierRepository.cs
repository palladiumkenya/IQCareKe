using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.enrollment;
using DataAccess.Context;
using Entities.CCC.Enrollment;

namespace DataAccess.CCC.Repository.Enrollment
{
    public class PersonIdentifierRepository : BaseRepository<PersonIdentifier>, IPersonIdentifierRepository
    {
        private readonly GreencardContext _context;

        public PersonIdentifierRepository():this(new GreencardContext())
        {

        }

        public PersonIdentifierRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
