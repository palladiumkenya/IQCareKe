using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using Entities.Lab;

namespace Interface.Laboratory
{
    
    public interface ILabFunctions
    {
        int DeleteLabForms(string FormName, int OrderNo, int PatientId, int UserID);

        DataTable FindLabByName(string SearchText, int? IncludeDepartment = null, int? ExcludeDepartment = null);

        DataTable GetBmiValue(int PatientID, int LocationID, int VisitID, int statusHW);

        DataTable GetEmployeeDetails();

        DataTable GetLaborderdate(int PatientID, int LocationID, int LabId);

        DataSet GetLabValues();

        /// <summary>
        /// Gets the ordered labs.
        /// </summary>
        /// <param name="labId">The lab identifier.</param>
        /// <returns></returns>
        DataSet GetOrderedLabs(int labId);

        DataSet GetPatientInfo(string patientid);

        DataSet GetPatientLab(String labId);

        DataSet GetPatientLabOrder(string patientId);

        DataSet GetPatientLabTestID(String labId);

        DataSet GetPatientRecordformStatus(int patientId);

        DataTable ReturnLabQuery(string theQuery);

        //DataTable GetTestParameters(int labTestId);
       
        //DataTable GetResultSelectList(int testParameterId);
        //DataTable GetTestParameterResultUnit(int testParameterId);

        /// <summary>
        /// Saves the dynamic lab results.
        /// </summary>
        /// <param name="labID">The lab identifier.</paramd
        /// <param name="userID">The user identifier.</param>
        /// <param name="reportedByName">Name of the reported by.</param>
        /// <param name="reportedByDate">The reported by date.</param>
        /// <param name="labResults">The lab results.</param>
        void SaveDynamicLabResults(int labID, int userID, int reportedByName, DateTime reportedByDate, DataTable labResults);

        DataSet SaveLabOrder(int ptnPk, int locationId, DataTable labTests, int userId, int orderedByName, DateTime orderedByDate,
            DateTime preClinicLabDate, int? labOrderId = null, int? moduleId = null, bool isRadiology = false, string clinicalOrderNotes = "");

        DataSet SaveLabOrderTests(int ptnPK, int locationId, DataTable parameterId, int userId, int orderedByName, string orderedByDate, string labId, int flagExist, string preClinicLabDate);

        DataSet SaveLabOrderTests(int ptnPK, int locationId, DataTable parameterID, int userId, int orderedByName, string orderedByDate, string labID, int flagExist, string preClinicLabDate,
            int? moduleID = null, bool isRadiology = false,
           string clinicalOrderNotes = "");

        DataTable SaveNewLabOrder(Hashtable ht, DataTable dt, string strCustomField, string paperless, DataTable theCustomFieldData);
    }
}