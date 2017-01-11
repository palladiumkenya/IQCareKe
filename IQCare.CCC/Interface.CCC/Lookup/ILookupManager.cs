using Entities.CCC.Lookup;
using System.Collections.Generic;

namespace Interface.CCC.Lookup
{
    public interface ILookupManager
   {
       List<LookupItemView> GetGenderOptions();
       List<LookupItemView> GetLookItemByGroup(string groupname);
   }
}
