using System;
using System.Collections.Generic;
using Entities.CCC.Appointment;

namespace Interface.CCC
{
    public interface IPatientAppointment
    {
        int AddPatientAppointments(PatientAppointment p);
        PatientAppointment GetPatientAppointments(int id);
        List<BlueCardAppointment> GetBluecardPatientAppointmentsBypatientId(int patientId);
        void DeletePatientAppointments(int id);
        int UpdatePatientAppointments(PatientAppointment p);
        List<PatientAppointment> GetByPatientId(int patientId);
        List<PatientAppointment> GetByDate(DateTime date);
        List<PatientAppointment> GetAppointmentId(int PatientId, int PatientMasterVisitId, DateTime date);
        List<PatientAppointment> GetByDateRange(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets the appointment summary by date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        List<AppointmentSummary> GetAppointmentSummaryByDate(DateTime date);
    }
}