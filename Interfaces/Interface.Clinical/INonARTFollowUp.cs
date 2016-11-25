using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Data;



namespace Interface.Clinical
{
    public interface INonARTFollowUp
    {
        DataSet GetPatientNonARTFollowUp(int PatientID);
        DataSet GetExistVisitNonARTFollowUp(int PatientID);
        DataSet SaveNonARTFollowUp(int PatientID, int PharmacyID, int LocationID, int VisitID, DataSet theDS, DataTable theDT, Hashtable theHT, DateTime OrderedByDate, DateTime DispensedByDate, Boolean Signature, int EmployeeID, int UserID, Boolean flag, Boolean theHIVAssocDisease, int DataQualityFlag, DataTable theCustomFieldData);
        DataSet GetPatientExsistNonARTFollowUp(int PatientID, int VisitID);
        DataSet GetExistNonARTFollowUpDrugDetails(int PharmacyID);
        DataSet GetNonARTBoundaryValues();
        DataSet GetExistNonARTFollowUpDetails(int PharmacyID, int VisitID,int PatientID);
        DataSet GetExistPharmacyDetail(int PharmacyID);
        int DeleteNonARTForms(string FormName, int OrderNo, int PatientId, int UserID);
        
    }
}
