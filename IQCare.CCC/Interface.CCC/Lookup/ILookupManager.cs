using Entities.CCC.Lookup;
using Entities.CCC.Visit;
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
        List<LookupItemView> GetLookUpItemViewByMasterId(int id);
        int GetLookUpMasterId(string masterName);
       
        List<LookupLabs> GetLookupLabs();  
        List<LookupPreviousLabs> GetLookupPreviousLabs(int patientId);
        List<LookupPreviousLabs> GetLookupVllabs(int patientId);
        List<LookupPreviousLabs> GetLookupPendingVllabs(int patientId);
        List<LookupPreviousLabs> GetLookupPendingLabs(int patientId);
        string GetLookupNameFromId(int id);
    }

       

   
}
