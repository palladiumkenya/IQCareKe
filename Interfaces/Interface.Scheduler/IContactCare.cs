using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;



namespace Interface.Scheduler
{
    public interface IContactCare
    {
        DataSet GetDropDowns();
        DataSet GetProgramStatus(int Patient_ID);
        DataSet GetCareEndDate(int ptn_pk, string ProgName);
        //DataSet GetLastActualContact(int Patient_ID);
        DataSet GetFieldsforID(int Patient_ID, int LocationId, int SystemId, int ModuleId, int FeatureId);
        //DataSet GetFieldsforID(int Patient_ID, int LocationId, int SystemId, int FeatureId, int ModuleId);
        DataSet GetContactListforID(int Patient_ID, int LocationId);
        DataSet GetFieldsforEdit(int Patient_ID, int LocationId, int CareEndedID, int TrackingID, DataTable theCustomFieldData);
        //DataSet SaveContactCare(int ptn_pk, int LocationId, int ARTended, DateTime ARTenddate, int ARTendreason, int careended, int exitreason, int dropreason, DateTime dateofdeath, int deathreason, string deathreasondescription, int employeeid, DateTime careendeddate, DateTime DateLastContact, int UserID, int Status, DateTime MissedAppDate, int DataQuality, int LPTFTransfer, int LostFollowreason, string Stop_Lostreason_Other);
        //int UpdateContactCare(int ptn_pk, int LocationId, int ARTended, DateTime ARTenddate, int ARTendreason, int careended, int exitreason, int dropreason, string dropreasonother, DateTime dateofdeath, int deathreason, string deathreasondescription, int employeeid, DateTime careendeddate, DateTime DateLastContact, int UserID, int Status, int TrackingID, int CareEndedID, DateTime MissedAppDate, int DataQuality);
        //DataSet GetFieldsforID(int Patient_ID, int LocationId, int SystemId, int FeatureId, int ModuleId);
        DataTable GetCareEndedNos(int Patient_ID, DateTime LastcontactDate, int flagcontactdate);
        DataTable GetCareDetails(int CEndedId);
       // public DataSet GetFieldsforID(int Patient_ID, int LocationId, int SystemId, int FeatureId);
        DataSet SaveContactCare(Hashtable ht, int DataQuality, DataTable theCustomFieldData);
        DataTable  UpdateContactCare(Hashtable ht, int DataQuality, int CareEndedID, int TrackingID, DataTable theCustomFieldData);
        DataSet PatientPrevProgram(int Ptn_Pk);
        DataSet CheckModuleTrackingStatus(Int32 thePtn_Pk, Int32 theModuleId);
    }
}
