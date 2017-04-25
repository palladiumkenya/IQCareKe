using System;
using System.Data;
using Entities.Administration;
using System.Collections.Generic;

namespace Interface.Scheduler
{
    /// <summary>
    ///
    /// </summary>
    public interface IAppointment
    {
        /// <summary>
        /// Checks the appointment existance.
        /// </summary>
        /// <param name="PatientId">The patient identifier.</param>
        /// <param name="LocationId">The location identifier.</param>
        /// <param name="AppDate">The application date.</param>
        /// <param name="ReasonId">The reason identifier.</param>
        /// <param name="visitId">The visit identifier.</param>
        /// <returns></returns>
        DataTable CheckAppointmentExistance(int patientPk, int locationId, DateTime appDate, int reasonId, int visitId = 0, int? moduleId = null);

        /// <summary>
        /// Deletes the patient appointment details.
        /// </summary>
        /// <param name="AppointmentId">The appointment identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <returns></returns>
        int DeletePatientAppointmentDetails(int appointmentId, int userId);

        /// <summary>
        /// Gets the appointment grid.
        /// </summary>
        /// <param name="LocationID">The location identifier.</param>
        /// <param name="AppStatus">The application status.</param>
        /// <param name="FromDate">From date.</param>
        /// <param name="ToDate">To date.</param>
        /// <param name="AppointmentReason">The appointment reason.</param>
        /// <returns></returns>
        DataTable GetAppointmentList(int locationId, int? patientPk = null, int? moduleId = null, int? visitId = null, int? appStatus = null, DateTime? fromDate = null, DateTime? toDate = null, int? appointmentReason = null);

       // List<Appointment> GetAppointments(int LocationId, int? PatientId = null, int? ModuleId = null, int? VisitId = null, int? AppStatus = null, DateTime? FromDate = null, DateTime? ToDate = null, int? AppointmentReason = null);
        /// <summary>
        /// Gets the appointment status.
        /// </summary>
        /// <returns></returns>
        DataSet GetAppointmentStatus();
        /// <summary>
        /// Gets the patientppointment details.
        /// </summary>
        /// <param name="PatientId">The patient identifier.</param>
        /// <param name="LocationId">The location identifier.</param>
        /// <param name="VisitId">The visit identifier.</param>
        /// <returns></returns>
        DataSet GetPatientppointmentDetails(int patientPk, int locationId, int visitId);

        /// <summary>
        /// Saves the appointment.
        /// </summary>
        /// <param name="PatientId">The patient identifier.</param>
        /// <param name="LocationId">The location identifier.</param>
        /// <param name="AppDate">The application date.</param>
        /// <param name="AppReasonId">The application reason identifier.</param>
        /// <param name="AppProviderId">The application provider identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="CreateDate">The create date.</param>
        /// <param name="ModuleID">The module identifier.</param>
        /// <param name="AppNote">The application note.</param>
        /// <returns></returns>
      //  int SaveAppointment(int patientPk, int locationId, DateTime appDate, int appReasonId, int appProviderId, int userId, DateTime createDate, int? moduleId = null, string AppNote = "");

        int SaveAppointment(Appointment appointment);
        int UpdatePatientppointment(Appointment appointment);

        /// <summary>
        /// Updates the patientppointment details.
        /// </summary>
        /// <param name="AppointmentId">The appointment identifier.</param>
        /// <param name="AppDate">The application date.</param>
        /// <param name="AppReasonId">The application reason identifier.</param>
        /// <param name="UserId">The user identifier.</param>
        /// <param name="AppProviderId">The application provider identifier.</param>
        /// <param name="Updationdate">The updationdate.</param>
        /// <returns></returns>
        int UpdatePatientppointmentDetails(int appointmentId, DateTime appDate, int appReasonId, int userId, int appProviderId, DateTime Updationdate, int? ServiceAreaId = null, string AppNote = "");
        #region "Modified13June07(1)"

        /// <summary>
        /// Gets the appointment reasons.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        DataSet GetAppointmentReasons(int Id);

        /// <summary>
        /// Gets the employees.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        DataSet GetEmployees(int Id);

        #endregion "Modified13June07(1)"
    }
}