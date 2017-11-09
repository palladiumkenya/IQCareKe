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
    public class InteropPlacerTypeRepository : BaseRepository<InteropPlacerType>, IInteropPlacerTypesRepository
    {
        private readonly GreencardContext _context;

        public InteropPlacerTypeRepository() : this(new GreencardContext())
        {

        }

        public InteropPlacerTypeRepository(GreencardContext context) : base(context)
        {
            _context = context;
        }
    }
}
