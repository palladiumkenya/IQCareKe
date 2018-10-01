using System;
using System.Collections.Generic;
using DataAccess.Context;
using Entities.CCC.Appointment;

namespace DataAccess.CCC.Interface.Patient
{
    public interface IPatientAppointmentRepository : IRepository<PatientAppointment>
    {
        List<PatientAppointment> GetByPatientId(int patientId);
        List<PatientAppointment> GetByDate(DateTime date);
        List<PatientAppointment> GetAppointmentId(int PatientId, int PatientMasterVisitId, DateTime date);
        List<PatientAppointment> GetByDateRange(DateTime startDate, DateTime endDate);
        List<AppointmentSummary> GetAppointmentSummaryByDate(DateTime date);
    }
}