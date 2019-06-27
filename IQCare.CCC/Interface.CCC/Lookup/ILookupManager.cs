using Entities.CCC.Lookup;
using Entities.CCC.Visit;
using System.Collections.Generic;
using System.Data;

namespace Interface.CCC.Lookup
{
    public interface ILookupManager
    {
        List<LookupItemView> GetGenderOptions();
        List<LookupItemView> GetLookItemByGroup(string groupname);
        List<LookupItemView> GetLookItemByGroup(string groupname, string anotherGroupname);
        List<LookupCounty> GetLookupCounties();
        List<LookupCounty> GetLookupSubcounty(string county);
        List<LookupCounty> GetLookupWards(string subcounty);
        List<LookupItemView> GetLookUpItemViewByMasterName(string masterName);
        List<LookupItemView> GetLookUpItemViewByMasterId(int id);
        int GetLookUpMasterId(string masterName);
        string GetLookupMasterNameByMasterIdDisplayName(int itemId, string displayName);
        string GetLookupItemNameByMasterNameItemId(int itemId, string masterName);
        List<LookupLabs> GetLookupLabs();
        List<LookupPreviousLabs> GetLookupPreviousLabs(int patientId);
        List<LookupPreviousLabs> GetExtruderCompleteLabs(int patientId);
        List<LookupPreviousLabs> GetExtruderPendingLabs(int patientId);
        List<LookupPreviousLabs> GetLookupVllabs(int patientId);
        List<LookupPreviousLabs> GetLookupPendingVllabs(int patientId);
        List<LookupPreviousLabs> GetLookupPendingLabs(int patientId);
        List<LookupTestParameter> GetTestParameter(int labTestId);
        LookupFacility GetFacility();
        LookupFacility GetFacility(string mflCode);
        PatientLookup GetPatientById(int patientId);
        LookupItemView GetPatientGender(int genderId);
        string GetLookupNameFromId(int id);
        List<LookupItemView> GetItemIdByGroupAndItemName(string groupName, string itemName);
        List<LookupItemView> GetItemIdByGroupAndDisplayName(string groupName, string displayName);
        int GetRegimenCategory(int regimenId);
        LookupLabs GetLabTestId(string labType);
        string GetCountyByCountyId(int countyId);
        string GetCountyNameBySubCountyId(int subCountyId);
        string GetWardNameByWardId(int wardId);
        List<PatientLabTracker> GetVlPendingCount(int facilityId);
        List<LookupFacilityViralLoad> GetFacilityVLSuppressed(int facilityId);
        List<LookupFacilityViralLoad> GetFacilityVLUnSuppressed(int facilityId);
        List<PatientLabTracker> GetVlCompleteCount(int facilityId);

        PatientRegimenLookup GetCurentPatientRegimen(int patientId);
        List<PatientRegimenLookup> GetPatientRegimenList(int patientId);
        LookupPatientAdherence GetPatientAdherence(int patientId);
        List<LookupFacilityStatistics> GetLookupFacilityStatistics();
        string GetRegimenCategoryByRegimenName(string regimenNaame);
        List<LookupItemView> GetRegimenCategoryListByRegimenName(string regimenName);
        string GetLookUpMasterNameFromId(int masterId);
        string GetLookupItemId(string lookupItemName);
        LookupCounty GetCountyDetailsByWardName(string wardName);

        DataTable GetCouncellingTopics();
        DataTable GetCouncellingTypes();
        DataTable GetLnkCouncellingTypeTopic();
    }
}
