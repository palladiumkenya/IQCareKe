using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace Interface.Clinical
{
    public interface IPriorArtHivCare
    {
        DataTable GetPatient_No_Of_IE(int patientid);
        //DataSet GetPatient_No_Of_VisitDate(int patientid, DateTime visitdate, int visittype);
        DataSet GetAllDropDowns();
        DataSet SavePriorArtHivCare(Hashtable ht, int intflag, int DataQualityFlag, DataTable thedtDynamicDrugMedical, DataTable theCustomFieldData);
        
        DataSet SaveInitialEvaluation(Hashtable ht, int none, int notDocumented, int AssoCondnone, int AssoCondnotDocumented, DataSet theDS_IE, ArrayList AssessmentAL, int VisitIE, string AssessmentDescription1, string AssessmentDescription2, int intflag, int DataQualityFlag, DataTable theCustomFieldData, string ClinicalNotes);
        DataSet GetPatientPriorArtHIVCare(int patientid);
        DataSet GetCurrentDate();
        //DataSet GetInitialEvaluationVisitDate(int patientid);
        DataSet GetPriorArtHivCareUpdate(int visitpk, int patientid, int locationID);
        int Update_DataQuality(int patientid, int visitpk, int dataquality, int locationid);
        DataSet GetClinicalDate(int patientID, int visittype);
        DataSet GetARTStatus(int patientID);
        DataSet GetPregnantStatus(int patientID, string VisitDate);
        DataSet GetAppointment(int patientID, int locationID, DateTime AppDate, int AppReason);

        int SaveExposedInfant(int Id, int Ptn_Pk, int ExposedInfantId, string FirstName, string LastName, DateTime DOB, string FeedingPractice3mos,
            string CTX2mos, string HIVTestType, string HIVResult, string FinalStatus, DateTime? DeathDate);

        #region "Kenya Technical Area"
        DataTable Save_Update_ARVHistory(Hashtable theHT, DataTable theDT, DataTable theCustomFieldData);
        DataSet GetARTHistoryData(int PatientId, int VisitId, int LocationId);
        #endregion

        int DeleteHIVCareEncounterForms(string FormName, int OrderNo, int PatientId, int UserID);
        

    }
}
