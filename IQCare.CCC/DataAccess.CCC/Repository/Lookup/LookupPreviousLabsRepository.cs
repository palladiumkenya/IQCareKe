using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
   public class LookupPreviousLabsRepository : BaseRepository<LookupPreviousLabs>, ILookupPreviousLabs
    {
        private readonly LookupContext _context;

        public LookupPreviousLabsRepository() : this(new LookupContext())
        {
        }

        public LookupPreviousLabsRepository(LookupContext context) : base(context)
       {
            _context = context;
        }

        public List<LookupPreviousLabs> FindBy(Func<LookupPreviousLabs, bool> p)
        {
            var results = _context.LookupPreviousLaboratories.Where(p);
            //  .Where(p).ToList<LookupCounty>();

            return results.ToList();
        }

        public List<LookupPreviousLabs> GetPreviousLabs(int patientId)
        {
            ILookupPreviousLabs previouslabsrepository = new LookupPreviousLabsRepository();
            var list = previouslabsrepository.GetAll().GroupBy(x => x.Ptn_Pk).Select(x => x.First()).OrderBy(l => l.TestName);
            return list.ToList();
        }

        /*  public List<LookupPreviousLabs> GetPreviousLabs()
          {
             ILookupPreviousLabs lookuplabsPrevious = new LookupPreviousLabsRepository();
              var myList = lookuplabsPrevious.FindBy(s => s.Ptn_Pk == p_id);
              var list = myList.GroupBy(x => x.Ptn_Pk).Select(x => x.First());
              return list.ToList();
          }

      */

    }
}
