using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Entities.Pharmacy;


namespace Interface.Pharmacy
{
    public interface IPharmacyRepo
    {
        List<Prescription> GetAll(Entities.Common.IFilter orderFilters);
    }
  
    public interface IPediatric
    {
        DataSet GetPediatricFields(int patientId);
        DataSet GetExistPaediatricDetails(int patientId);
        int ModifyPrescription(int patientId, int pharmacyId, int locationId, int regimenLine,
           string PharmacyNotes, DataTable theDT, DataSet theDrgMst, int orderedBy, DateTime orderedByDate,
           int dispensedBy, DateTime dispensedByDate, int signature, int employeedId, int orderType, int visitType, int userId,
           decimal height, decimal weight, int FDC, int progId, int providerId, DataTable theCustomFieldData, int periodTaken,
           int flag, int sCMFlag, DateTime appntDate, int appntReason, string editReason, int? moduleId = null);
        int SaveUpdatePaediatricDetail(int patientId, int pharmacyId, int locationId, int regimenLine,
            string PharmacyNotes, DataTable theDT, DataSet theDrgMst, int orderedBy, DateTime orderedByDate,
            int dispensedBy, DateTime dispensedByDate, int signature, int employeedId, int orderType, int visitType, int userId,
            decimal height, decimal weight, int FDC, int progId, int providerId, DataTable theCustomFieldData, int periodTaken,
            int flag, int sCMFlag, DateTime appntDate, int appntReason, int? moduleId = null);

       

        int UpdatePaediatricDetail(int patientId, int locationId, int PharmacyID, DataTable theDT,
            DataSet theDrgMst, int OrderedBy, int DispensedBy, int Signature, int EmployeeID, int OrderType, int UserID,
            decimal Height, decimal Weight, int FDC, int ProgID, int ProviderID, DateTime OrderedByDate, DateTime ReportedByDate,
            DataTable theCustomFieldData, int PeriodTaken);
        int DeletePediatricForms(string FormName, int OrderNo, int patientId, int UserID);
        DataSet GetExistPharmacyForm(int patientId, DateTime OrderedByDate);
        DataSet GetExistPharmacyFormDespensedbydate(int patientId, DateTime DispensedByDate);
        DataSet GetPatientRecordformStatus(int patientId);

        /// <summary>
        /// Gets the patient pharmacy orders.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="CutOffDate">The cut off date.</param>
        /// <param name="ShowMostRecent">if set to <c>true</c> [show most recent].</param>
        /// <param name="DrugType">Type of the drug. If 0, show all orders, else show orders with a particular drug type</param>
        /// <returns></returns>
        DataTable GetPatientPharmacyOrders(int patientId, DateTime CutOffDate, bool ShowMostRecent = true, int DrugType = 0);


    }
}
