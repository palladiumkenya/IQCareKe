using System;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
   public class LookupMasterRepository:BaseRepository<LookupMaster>,ILookupMasterRepository
   {
       private readonly LookupContext _context;

        public LookupMasterRepository() : this(new LookupContext())
        {
        }

        public LookupMasterRepository(LookupContext context) : base(context)
       {
            _context = context;
        }
    }
}
