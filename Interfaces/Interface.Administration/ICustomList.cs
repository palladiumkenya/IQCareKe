using System;
using System.Data;
using System.Collections;

namespace Interface.Administration
{
    public interface ICustomList
    {
        DataTable GetCustomListUpdateFlag(string TableName, int ID, int SystemId);
        DataTable GetCustomListUpdatePriortorize(string TableName, int CategoryID, string SRno, int SystemId);        
        DataTable GetCustomList(string TableName, int Category, int SystemId);
        DataTable GetCustomMasterDetails(string TableName, int Id, int SystemId);
        //int SaveCustomMasterRecord(string TableName, string Name, int Sequence, int Category, int UserId, int SystemId);
        //int SaveCustomMasterRecord_Code(string TableName, string Name, string Code, int Sequence, int Category, int UserId, int SystemId);
        DataTable SaveCustomMasterRecord(string TableName, string ListName, string Name, string Code, string Stage, int Sequence, int Category, int UserId, int SystemId, int CountryID, int ModuleId, string multiplier);
        int UpdateCustomMasterRecord(string TableName, int Id, string Name, string Code, string Stage, int Sequence, int Category, int Status, int UserId, int SystemId, int CountryID, int ModuleId, string multiplier);
        //int UpdateCustomMasterRecord1(string TableName, int Id, string Name, int Sequence, int Category, int Status, int UserId, int SystemId);
        //int UpdateCustomMasterRecord2(string TableName, int Id, string Name, string Code, int Sequence, int Category, int Status, int UserId, int SystemId);
        //int UpdateCustomMasterRecord_Code(string TableName, int Id, string Name, string Code, int Sequence, int Category, int Status, int UserId, int SystemId);

        DataTable GetCustomFieldList(Int32 SystemId, Int32 FacilityId);
         DataTable GetCustomFieldListPMTCT();
        //DataTable GetAidsDefEvents(string TableName,string Name, string Code, int flag, int ID);
        //DataTable GetDecode(string TableName, string Name,int flag, int ID);
        int SaveUpdateCustomMasterLinkRecord(string TableName, int CodeId1,int CodeId2,int UserId);
        int SaveUpdateVillageChairperson(int RegionID,int DistrictID,int WardID,int VillageId, string CPersonName, int UserId, int flag);
        DataTable GetVillageChairperson(int RegionID, int DistrictID, int WardID, int villageId);
        DataTable GetCustomMasterLinkRecord(string TableName, Int32 CodeId);
        int DeleteCustomMasterLinkRecord(string TableName, Int32 CodeId);
        int DeleteCustomMasterLinkRecordParticular(string TableName, Int32 CodeId);
        DataTable GetCustomMasterNonSelectedRecord(string TableName);
        DataSet GetDistric(int RegionID, int SystemID);
        DataSet GetWard(int DistricID, int SystemID);
        DataSet GetVillage(int Ward, int SystemID);
        DataSet GetLPTFPatientTransfer(int SystemID);
        DataSet GetLPTFPatientTransferID(int LPTFId);
        DataTable SaveLPTF(String LPTFID, String LPTFName, String Answer, String Status, String theUserID, String SystemID, String Flag);
        DataSet GetICDList();
        DataTable SaveICDCodeRecord(Hashtable theHT, ArrayList theAL);
        int UpdateICDCodeRecord(string Id, Hashtable theHT, ArrayList theAL);
        DataSet GetICDData(int Id, int DiseaseFlag);
    }
}
