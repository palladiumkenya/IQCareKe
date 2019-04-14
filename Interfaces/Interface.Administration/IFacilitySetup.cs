using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
 
namespace Interface.Administration
{
    public interface IFacilitySetup
    {
        DataSet GetFacilityList(int SystemId, int FeatureId, int ModuleId);
        DataSet GetSystemBasedLabels(int SystemId, int FeatureId, int ModuleId);
        DataSet GetFacility();
        int SaveNewFacility(string FacilityName, string CountryID, string PosID, string SatelliteID, string NationalID, int ProvinceId, int DistrictId, string image, int currency, int AppGracePeriod, string dateformat, DateTime PepFarStartDate, int SystemId, int thePreferred, int Paperless, int UserID,int Frequency,int Queue, DataTable dtModule, Hashtable ht);
        int UpdateFacility(int FacilityId, string FacilityName, string CountryID, string PosID, string SatelliteID, string NationalID, int ProvinceId, int DistrictId, string image, int currency, int AppGracePeriod, string dateformat, DateTime PepFarStartDate, int Status, int SystemId, int thePreferred, int Paperless, int UserID,int Frequency,int Queue, DataTable dtModule, Hashtable ht);
        int SaveBackupSetup(string theDrive, DateTime theTime);
        DataTable GetBackupSetup();
        DataSet GetModuleName();
    }
}
