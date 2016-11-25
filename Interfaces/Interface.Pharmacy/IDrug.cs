using System;
using System.Collections;
using System.Data;

namespace Interface.Pharmacy
{
    public interface IDrug
    {
        DataSet GetPharmacyMasters(int PatientID);
        DataSet GetExistPharmacyDetail(int PatientID);
        DataSet GetPatientRecordformStatus(int PatientID);
        DataSet GetExistPharmacy_CTC_Detail(int PharmacyID);
        DataSet GetGenericID_CTC_Detail(int RegimenID);
        DataSet Get_TBRegimen_Detail(int RegimenID);
        DataSet GetPharmacyList(int PatientID);
        int UpdateExistDrug(int patientID, int LocationID, int PharmacyId, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken);
        int UpdateExistDrug_CTC(int patientID,int PharmacyId, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData);
        int SaveDrugOrder(int patientID, int LocationID, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken);
        int SaveDrugOrder_CTC(int patientID, int LocationID, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData);
        int DeleteDrugForms(string FormName, int OrderNo, int PatientId, int UserID);
        int SaveUpdateDrugOrder(int patientID, int LocationID, int PharmacyId, int RegimenLine, string PharmacyNotes, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData, int PeriodTaken, int flag, int SCMFlag);
        int SaveUpdateDrugOrder_CTC(int patientID, int LocationID,int PharmacyId, int OrderedBy, DateTime OrderedByDate, int DispensedBy, DateTime DispensedByDate, int HoldMedicine, int Signature, int EmployeeID, int OrderType, int UserID, DataTable DrugTable, DataSet Master, int ProgID, int ProviderID, DataTable theCustomFieldData,int flag);
        DataSet GetExistPharmacyForm(int PatientID, DateTime OrderedByDate);
        DataSet GetExistPharmacyFormDespensedbydate(int PatientID, DateTime DispensedByDate);
        DataSet GetARVStatus(int patientid, DateTime DispensedBy);
        DataTable GetStrengthForFixedDrug(int Drug_pk);
        DataTable GetFrequencyForFixedDrug(int Drug_pk);
        DataTable GetEmployeeDetails();
        DataSet GetNonARTDate(int PatientId);
        DataTable CheckARTStopStatus(int PatientId);
        DataTable ReturnDatatableQuery(string theQuery);
        DataSet GetPharmacyPrescriptionDetails(int PharmacyID, int PatientId, int IQCareFlag);
        //#############
        //John Macharia - Start
        //26-Jul-2012
        //
        DataSet GetPharmacyNotes(int PatientID);
        //John Macharia - End

        DataTable FindDrugByName(string SearchText, bool CheckQuantity = false, int? ExcludeDrugType = null);
    }
}
