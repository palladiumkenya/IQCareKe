using Entities.CCC.Lookup;
using System.Collections.Generic;

namespace Interface.CCC.Lookup
{
    public interface ILookupManager
   {
       List<LookupItemView> GetGenderOptions();
       List<LookupItemView> GetLookItemByGroup(string groupname);

       List<LookupCounty> GetLookupCounties();
       List<LookupCounty> GetLookupSubcounty(string county);
       List<LookupCounty> GetLookupWards(string subcounty);
        List<LookupItemView> GetLookUpItemViewByMasterName(string masterName);
        int GetLookUpMasterId(string masterName);
   }
}
