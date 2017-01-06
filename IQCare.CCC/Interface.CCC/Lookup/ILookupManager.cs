using Entities.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.CCC.Lookup
{
   public interface ILookupManager
   {
       List<LookupItemView> GetGenderOptions();
       List<LookupItemView> GetLookItemByGroup(string groupname);
   }
}
