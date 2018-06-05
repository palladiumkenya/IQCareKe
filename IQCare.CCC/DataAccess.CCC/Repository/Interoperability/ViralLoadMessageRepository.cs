using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Interoperability;
using DataAccess.Context;
using Entities.CCC.Interoperability;

namespace DataAccess.CCC.Repository.Interoperability
{
    public class ViralLoadMessageRepository : BaseRepository<ViralLoadMessage>, IViralLoadMessageRepository
    {
        private readonly LookupContext _context;

        public ViralLoadMessageRepository() : this(new LookupContext())
        {

        }

        public ViralLoadMessageRepository(LookupContext context) : base(context)
        {
            _context = context;
        }
    }
}
