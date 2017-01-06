using DataAccess.Base;
using Interface.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Lookup;
using DataAccess.CCC.Repository.Lookup;

namespace BusinessProcess.CCC
{
    public class BLookupManager : ProcessBase, ILookupManager
    {
        public List<LookupItemView> GetGenderOptions()
        {
            LookupRepository repo = new LookupRepository();
           return repo.GetLookupItemViews("Gender");
        }

        public List<LookupItemView> GetLookItemByGroup(string groupname)
        {
            LookupRepository repo = new LookupRepository();
            return repo.GetLookupItemViews(groupname);
        }
    }
}
