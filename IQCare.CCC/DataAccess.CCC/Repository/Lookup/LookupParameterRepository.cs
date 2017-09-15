using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.CCC.Repository.Lookup
{
   public  class LookupParameterRepository : BaseRepository<LookupTestParameter>, ILookupParameter
    {
        private readonly LookupContext _context;

        public LookupParameterRepository() : this(new LookupContext())
        {
        }

        public LookupParameterRepository(LookupContext context) : base(context)
        {
            _context = context;
        }

       public List<LookupTestParameter> GetTestParameter(int LabTestId)
        {
            ILookupParameter paramlookupRepository = new LookupParameterRepository();
            var paramId = paramlookupRepository.FindBy(x => x.LabTestId == LabTestId).ToList();
            return paramId;

        }
        public List<LookupTestParameter> FindBy(Func<LookupTestParameter, bool> p)
        {
            var results = _context.LookupTestParameter.Where(p);           

            return results.ToList();
        }
    }
}
