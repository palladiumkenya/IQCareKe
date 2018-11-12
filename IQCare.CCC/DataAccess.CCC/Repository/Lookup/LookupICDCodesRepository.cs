using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
   public  class LookupICDCodesRepository:BaseRepository<ICDCodeList>, ILookupICDCodesRepository
    {
        private readonly LookupContext _context;

    public LookupICDCodesRepository() : this(new LookupContext())
    {

    }
     
     
  public LookupICDCodesRepository(LookupContext context) : base(context)
    {
        _context = context;
    }
}
}
