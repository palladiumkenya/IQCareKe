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
       
        List<LookupLabs> GetLookupLabs();  
        List<LookupPreviousLabs> GetLookupPreviousLabs(int patientId);
       string GetLookupNameFromId(int id);

   }
}
