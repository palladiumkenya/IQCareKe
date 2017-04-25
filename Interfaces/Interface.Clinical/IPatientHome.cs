using System;
using System.Data;
using System.Collections.Generic;
using Entities.FormBuilder;
using Entities.PatientCore;

namespace Interface.Clinical
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPatientHome
    {
        /// <summary>
        /// Gets the patient details.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="SystemId">The system identifier.</param>
        /// <param name="ModuleId">The module identifier.</param>
        /// <returns></returns>
        DataSet GetPatientDetails(int patientId, int systemId,int moduleId);
        /// <summary>
        /// Gets the patient history.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        DataSet GetPatientHistory(int patientId);
        /// <summary>
        /// Gets the patient visit detail.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetPatientVisitDetail(int patientId);
        /// <summary>
        /// Res the activate patient.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="ModId">The mod identifier.</param>
        /// <returns></returns>
        DataSet ReActivatePatient(int patientId, int moduleId);
        /// <summary>
        /// Gets the pharmacy identifier.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="VisitId">The visit identifier.</param>
        /// <returns></returns>
        DataTable GetPharmacyID(int patientId, int locationId, int visitId);
        /// <summary>
        /// Gets the name of the technical areaand form.
        /// </summary>
        /// <param name="ModuleId">The module identifier.</param>
        /// <returns></returns>
        DataSet GetTechnicalAreaandFormName(int moduleId);
        /// <summary>
        /// Gets the technical area indicators.
        /// </summary>
        /// <param name="ModuleId">The module identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        DataSet GetTechnicalAreaIndicators(int moduleId, int patientId);
        /// <summary>
        /// Gets the technical area identifier future.
        /// </summary>
        /// <param name="ModuleId">The module identifier.</param>
        /// <param name="Ptn_Pk">The PTN_ pk.</param>
        /// <returns></returns>
        DataSet GetTechnicalAreaIdentifierFuture(int moduleId, int Ptn_Pk);
        /// <summary>
        /// Gets the patient debit note summary.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetPatientDebitNoteSummary(int patientId);
        /// <summary>
        /// Gets the patient debit note open items.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        DataTable GetPatientDebitNoteOpenItems(int patientId,DateTime start, DateTime end);
        /// <summary>
        /// Gets the patient debit note details.
        /// </summary>
        /// <param name="billId">The bill identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        DataSet GetPatientDebitNoteDetails(int billId,int patientId);
        /// <summary>
        /// Creates the debit note.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        int CreateDebitNote(int patientId,int locationId, int userId, DateTime start, DateTime end);
        /// <summary>
        /// Gets the patient summary information.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="ModuleId">The module identifier.</param>
        /// <returns></returns>
        DataSet GetPatientSummaryInformation(int patientId, int moduleId);
        /// <summary>
        /// Gets the patient wait list.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetPatientWaitList(int patientId);
        /// <summary>
        /// Saves the patient wait list.
        /// </summary>
        /// <param name="WaitingList">The waiting list.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        void SavePatientWaitList(DataTable WaitingList, int moduleId, int userId, int patientId);
        /// <summary>
        /// Saves the patient revisit.
        /// </summary>
        /// <param name="VisitDate">The visit date.</param>
        /// <param name="HoursBetweenRevisit">The hours between revisit.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        void SavePatientRevisit(DateTime visitDate,int hoursBetweenRevisit , int moduleId, int userId, int patientId, int locationId);
        /// <summary>
        /// Gets the patient revisits.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="GetLastOnly">if set to <c>true</c> [get last only].</param>
        /// <returns></returns>
        DataTable GetPatientRevisits(int patientId, int locationId, bool getLastOnly = false);
        /// <summary>
        /// Gets the allowed module forms.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetModuleForms(int moduleId,int locationId);
        /// <summary>
        /// Gets the module forms business rule.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="formId">The form identifier.</param>
        /// <returns></returns>
        List<FormRule> GetModuleFormsBusinessRule(int? moduleId = null, int? featureId=null, int? formId = null);
        /// <summary>
        /// Gets the allowed module report.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="locationId">The location identifier.</param>
        /// <param name="patientId">The patient identifier.</param>
        /// <returns></returns>
        DataTable GetModuleReport(int moduleId,  int locationId);
        Patient GetPatientById(int Id);
       

       
      }
}
