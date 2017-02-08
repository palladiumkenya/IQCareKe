using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Appointment;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{
    public class PatientAppointmentManager
    {
        private IPatientAppointment _appointment = (IPatientAppointment)ObjectFactory.CreateInstance("BusinessProcess.CCC.PatientAppointment, BusinessProcess.CCC");
        public int AddPatientAppointments(PatientAppointment p)
        {
            PatientAppointment appointment = new PatientAppointment()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                AppointmentDate = p.AppointmentDate,
                Description = p.Description,
                ReasonId = p.ReasonId,
                ServiceAreaId = p.ServiceAreaId,
                StatusId = p.StatusId,
                StatusDate = DateTime.Now,
                
            };
            return _appointment.AddPatientAppointments(appointment);
        }

        public PatientAppointment GetPatientAppointments(int id)
        {
            var appointment = _appointment.GetPatientAppointments(id);
            return appointment;
        }

        public void DeletePatientAppointments(int id)
        {
            _appointment.DeletePatientAppointments(id);
        }

        public int UpdatePatientAppointments(PatientAppointment p)
        {
            PatientAppointment appointment = new PatientAppointment()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                AppointmentDate = p.AppointmentDate,
                Description = p.Description,
                ReasonId = p.ReasonId,
                ServiceAreaId = p.ServiceAreaId,
                StatusId = p.StatusId,
                StatusDate = DateTime.Now,

            };
            return _appointment.UpdatePatientAppointments(appointment);
        }

        public List<PatientAppointment> GetByPatientId(int patientId)
        {
            var appointment = _appointment.GetByPatientId(patientId);
            return appointment;
        }

        public List<PatientAppointment> GetByDate(DateTime date)
        {
            var appointment = _appointment.GetByDate(date);
            return appointment;
        }

        public List<PatientAppointment> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var appointment = _appointment.GetByDateRange(startDate, endDate);
            return appointment;
        }
    }
}
