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
    public class InteropPlacerValuesRepository : BaseRepository<InteropPlacerValues>,IInteropPlacerValuesRepository
    {
        private readonly GreencardContext _context;

        public InteropPlacerValuesRepository() : this(new GreencardContext())
        {

        }

        public InteropPlacerValuesRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
