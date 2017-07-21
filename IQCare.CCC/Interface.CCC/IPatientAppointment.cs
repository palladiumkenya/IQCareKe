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
        List<PatientAppointment> GetByDateRange(DateTime startDate, DateTime endDate);
    }
}