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
           // var list = previouslabsrepository.GetAll().GroupBy(x => x.Id).Select(x => x.First()).OrderBy(l => l.TestName);
            //return list.ToList();

           
            var myList = previouslabsrepository.FindBy(x => x.Id == patientId);
            var list = myList.GroupBy(x => x.TestName).Select(x => x.First());
            return list.Distinct().ToList();
        }

       

    }
}
