using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IFollowup
    {
        DataTable GetPatient_No_Of_IE(int patientid);
        DataSet GetPatient_No_Of_VisitDate(int patientid, DateTime visitdate, int visittype);
        DataSet GetPatientFollowUpART(int patientid);
        DataSet GetLatestCD4ViralLoad(int patientid, DateTime visitdate);
        DataSet GetLatestHeight(int patientID, DateTime VisitDate);
        DataSet GetFollowUpARTupdate(int patientid, int visitpk, int locationID);
        DataSet GetARTFollowUPVisitDate(int patientid);
        DataSet GetAllDropDownsART(int patientid);
        int Save_Update_FollowUP(int patientID, int VisitID, int LocationID, Hashtable ht, DataSet theDS_ARTFU, int VisitIE, int rdoARVSideEffectsNone, int rdoARVSideEffectsNotDocumented, int rdoOIsAIDsIllnessNone, int rdoOIsAIDsIllnessNotDocumented, int userID, Boolean Save, Boolean Update, DateTime createDate, int DataQualityFlag, DataTable theCustomFieldData);
        DataSet GetClinicalDate(int patientID, int visittype);
        int DeleteARTForms(string FormName, int OrderNo, int PatientId, int userID);
    }
}
