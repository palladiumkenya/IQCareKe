using Entities.CCC.Lookup;
using Entities.CCC.Encounter;
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
        List<LookupPreviousLabs> GetExtruderCompleteLabs(int patientId);
        List<LookupPreviousLabs> GetExtruderPendingLabs(int patientId);
        List<LookupPreviousLabs> GetLookupVllabs(int patientId);
        List<LookupPreviousLabs> GetLookupPendingVllabs(int patientId);
        List<LookupPreviousLabs> GetLookupPendingLabs(int patientId);
        LookupTestParameter GetTestParameter(int LabTestId);
        LookupFacility GetFacility();
        PatientLookup GetPatientPtn_pk(int patientId);
        LookupItemView GetPatientGender(int genderId);        
        string GetLookupNameFromId(int id);
        List<LookupItemView> GetItemIdByGroupAndItemName(string groupName, string itemName);
        int GetRegimenCategory(int regimenId);
        LookupLabs GetLabTestId(string labType);      
        string GetCountyByCountyId(int countyId);
        string GetCountyNameBySubCountyId(int subCountyId);
        string GetWardNameByWardId(int wardId);
        List<PatientLabTracker> GetVlPendingCount(int facilityId);
        List<PatientLabTracker> GetVlCompleteCount(int facilityId);

    }




}
