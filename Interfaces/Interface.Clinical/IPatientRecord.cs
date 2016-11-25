using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Interface.Clinical
{
    public interface IPatientRecord
    {

        DataTable SavePatientRecord(Hashtable ht, Array arrIllness, Array arrReferredTo, Array arrAdhReason, Array arrARVReason4, DataTable theCustomFieldData);
        DataSet GetPatientRecord(string Mode, int Ptn_Pk,int LocationID,int VisitId);
        DataSet CheckVisitDate(string Ptn_Pk, int LocationID, DateTime VisitDate, int VisitId);
        DataSet GetCD4TLC(Hashtable htCD4TLC);
        DataSet GetHeightCD4Regimen(string Ptn_Pk, string VisitId, string VisitDate, string LocationID);
        int DeletePatientRecord(string FormName, int OrderNo, int PatientId, int UserID);
    }
}
