using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IInitialEval
    {
        DataTable GetPatient_No_Of_IE(int patientid);
        //DataSet GetPatient_No_Of_VisitDate(int patientid, DateTime visitdate, int visittype);
        DataSet GetAllDropDowns();
        DataSet SaveInitialEvaluation(Hashtable ht, int none, int notDocumented, int AssoCondnone, int AssoCondnotDocumented, DataSet theDS_IE, ArrayList AssessmentAL, int VisitIE, string AssessmentDescription1, string AssessmentDescription2, int intflag, int DataQualityFlag, DataTable theCustomFieldData, string ClinicalNotes);
        DataSet GetPatientInitialEvaluation(int patientid);
        DataSet GetCurrentDate();
        //DataSet GetInitialEvaluationVisitDate(int patientid);
        DataSet GetInitialEvaluationUpdate(int visitpk, int patientid, int locationID);
        int Update_DataQuality(int patientid, int visitpk, int dataquality, int locationid);
        DataSet GetClinicalDate(int patientID, int visittype);
        DataSet GetARTStatus(int patientID);
        DataSet GetPregnantStatus(int patientID, string VisitDate);
        DataSet GetAppointment(int patientID, int locationID, DateTime AppDate, int AppReason);
        
    }
}
