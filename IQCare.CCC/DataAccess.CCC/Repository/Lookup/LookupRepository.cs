using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DataAccess.CCC.Context;
using DataAccess.CCC.Interface.Lookup;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Repository.Lookup
{
    public class LookupRepository :BaseRepository<Entities.CCC.Lookup.LookupItemView>,ILookupRepository
    {
        private readonly LookupContext _context;

        public LookupRepository() : this(new LookupContext())
        {
        }

        public LookupRepository(LookupContext context) : base(context)
       {
            _context = context;
        }

        public void GetDropdownValue(DropDownList ddl,string listGroup)
        {
            ILookupRepository x=new LookupRepository();
            List<Entities.CCC.Lookup.LookupItemView> myList = x.FindBy(g => g.MasterName == listGroup.ToString()).ToList();
            ddl.DataSource = myList;
            ddl.DataTextField = "ItemName";
            ddl.DataValueField = "ItemId";
            ddl.DataBind();
        }

        public List<LookupItemView> GetLookupItemViews(string listGroup)
        {
            ILookupRepository x = new LookupRepository();
            var myList = x.FindBy(g => g.MasterName == listGroup.ToString());
           return myList.OrderBy(l => l.OrdRank).ToList();
          //  return myList;
        }
    }
}
