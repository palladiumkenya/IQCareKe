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
    public class IdentifierRepository : BaseRepository<Identifier>, IIdentifierRepository
    {
        private readonly GreencardContext _context;
        public IdentifierRepository():this(new GreencardContext())
        {
            
        }

        public IdentifierRepository(GreencardContext context):base(context)
        {
            _context = context;
        }
    }
}
