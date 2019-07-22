using Application.Presentation;
using Entities.CCC.Appointment;
using Interface.CCC;
using System;
using System.Collections.Generic;
using IQCare.Events;
using IQCare.Web.UILogic;

namespace IQCare.CCC.UILogic
{
    public class PatientAppointmentManager
    {
        private IPatientAppointment _appointment = (IPatientAppointment)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientAppointment, BusinessProcess.CCC");

        public int AddPatientAppointments(PatientAppointment p, bool sendEvent = true)
        {
            int returnVal = 0;

            try
            {
                if (p.CreatedBy == 0) { p.CreatedBy = SessionManager.UserId; }
            }
            catch { }
            PatientAppointment appointment = new PatientAppointment()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                AppointmentDate = p.AppointmentDate,
                Description = p.Description,
                ReasonId = p.ReasonId,
                DifferentiatedCareId = p.DifferentiatedCareId,
                ServiceAreaId = p.ServiceAreaId,
                StatusId = p.StatusId,
                StatusDate = DateTime.Now,
                CreatedBy = p.CreatedBy
            };

            var sameDayAppointmentExists = _appointment.GetAppointmentByType(p.PatientId, p.PatientMasterVisitId, p.AppointmentDate,
                p.ReasonId);
            if (sameDayAppointmentExists.Count > 0)
            {
                sameDayAppointmentExists[0].Description = p.Description;
                sameDayAppointmentExists[0].DifferentiatedCareId = p.DifferentiatedCareId;
                sameDayAppointmentExists[0].ServiceAreaId = p.ServiceAreaId;
                sameDayAppointmentExists[0].StatusId = p.StatusId;

                _appointment.UpdatePatientAppointments(sameDayAppointmentExists[0]);
            }
            else
            {
                returnVal = _appointment.AddPatientAppointments(appointment);
            }
            
            if (returnVal > 0 && sendEvent)
            {
                PatientLookupManager patientLookup = new PatientLookupManager();
                var patient = patientLookup.GetPatientDetailSummary(p.PatientId);
                MessageEventArgs args = new MessageEventArgs()
                {
                    FacilityId = patient.FacilityId,
                    EntityId = returnVal,
                    PatientId = appointment.PatientId,
                    MessageType = MessageType.AppointmentScheduling,
                    EventOccurred = "Patient Appointment Scheduled"
                };

                Publisher.RaiseEventAsync(this, args).ConfigureAwait(false);
            }
            return returnVal;
        }

        public PatientAppointment GetPatientAppointment(int id)
        {
            var appointment = _appointment.GetPatientAppointments(id);
            return appointment;
        }

        public List<BlueCardAppointment> GetBluecardAppointmentsByPatientId(int patientId)
        {
            var appointments = _appointment.GetBluecardPatientAppointmentsBypatientId(patientId);
            return appointments;
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
                DifferentiatedCareId = p.DifferentiatedCareId,
                ReasonId = p.ReasonId,
                ServiceAreaId = p.ServiceAreaId,
                StatusId = p.StatusId,
                //StatusDate = DateTime.Now,
                Id = p.Id
            };
            return _appointment.UpdatePatientAppointments(appointment);
        }

        public List<PatientAppointment> GetByPatientId(int patientId)
        {
            var appointment = _appointment.GetByPatientId(patientId);
            return appointment;
        }

        public int GetCountByPatientId(int patientId)
        {
            var appointment = _appointment.GetByPatientId(patientId);
            var bluecardappointment = _appointment.GetBluecardPatientAppointmentsBypatientId(patientId);
            return appointment.Count + bluecardappointment.Count;

        }

        public List<PatientAppointment> GetByDate(DateTime date)
        {
            var appointment = _appointment.GetByDate(date);
            return appointment;
        }

        public List<PatientAppointment> GetAppointmentId(int PatientId, int PatientMasterVisitId, DateTime date)
        {
            var appointment = _appointment.GetAppointmentId(PatientId, PatientMasterVisitId, date);
            return appointment;
        }

        public List<AppointmentSummary>GetAppointmentSummaryByDate(DateTime date)
        {
            var _appointments = _appointment.GetAppointmentSummaryByDate(date);
            return _appointments;
        }
        public List<PatientAppointment> GetByDateRange(DateTime startDate, DateTime endDate)
        {
            var appointment = _appointment.GetByDateRange(startDate, endDate);
            return appointment;
        }
    }
}