using Application.Presentation;
using Entities.CCC.Appointment;
using Entities.CCC.Lookup;
using Entities.CCC.Triage;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace IQCare.Web.CCC.WebService
{
    /// <summary>
    /// Summary description for PatientService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    [System.Web.Script.Services.ScriptService]
    public class PatientService : System.Web.Services.WebService
    {
        private string Msg { get; set; }
        private int Result { get; set; }

        [WebMethod]
        public string AddpatientVitals(int patientId, int bpSystolic, int bpDiastolic, decimal heartRate, decimal height,
            decimal muac, int patientMasterVisitId, decimal respiratoryRate, decimal spo2, decimal tempreture,
            decimal weight, decimal bmi, decimal headCircumference)
        {
            try
            {
                PatientVital patientVital = new PatientVital()
                {
                    PatientId = patientId,
                    BpSystolic = bpSystolic,
                    Bpdiastolic = bpDiastolic,
                    HeartRate = heartRate,
                    Height = height,
                    Muac = muac,
                    PatientMasterVisitId = patientMasterVisitId,
                    RespiratoryRate = respiratoryRate,
                    SpO2 = spo2,
                    Temperature = tempreture,
                    Weight = weight,
                    BMI = bmi,
                    HeadCircumference = headCircumference
                };
                var vital = new PatientVitalsManager();
                Result = vital.AddPatientVitals(patientVital);
                if (Result > 0)
                {
                    Msg = "Patient Vitals Added Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message + ' ' + e.InnerException;
            }
            return Msg;
        }

        [WebMethod]
        public string AddPatientAppointment(int patientId, int patientMasterVisitId, DateTime appointmentDate, string description, int reasonId, int serviceAreaId, int statusId)
        {
            PatientAppointment patientAppointment = new PatientAppointment()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                AppointmentDate = appointmentDate,
                Description = description,
                ReasonId = reasonId,
                ServiceAreaId = serviceAreaId,
                StatusId = statusId,
            };
            try
            {
                var appointment = new PatientAppointmentManager();
                Result = appointment.AddPatientAppointments(patientAppointment);
                if (Result > 0)
                {
                    Msg = "Patient appointment Added Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message + ' ' + e.InnerException;
            }
            return Msg;
        }

        [WebMethod]
        public List<PatientAppointmentDisplay> GetPatientAppointments(string patientId)
        {
            List<PatientAppointmentDisplay> appointmentsDisplay = new List<PatientAppointmentDisplay>();
            List<PatientAppointment> appointments = new List<PatientAppointment>();
            try
            {
                var patientAppointment = new PatientAppointmentManager();
                int id = Convert.ToInt32(patientId);
                appointments = patientAppointment.GetByPatientId(id);
                foreach (var appointment in appointments)
                {
                    PatientAppointmentDisplay appointmentDisplay = Mapappointments(appointment);
                    appointmentsDisplay.Add(appointmentDisplay);
                }

            }
            catch (Exception e)
            {
                Msg = e.Message + ' ' + e.InnerException;
            }
            return appointmentsDisplay;
        }

        private PatientAppointmentDisplay Mapappointments(PatientAppointment a)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            IPatientLookupmanager patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");
            string status = "";
            string reason = "";
            string serviceArea = "";
            List <LookupItemView> statuses = mgr.GetLookItemByGroup("AppointmentStatus");
            var s = statuses.FirstOrDefault(n => n.ItemId == a.StatusId);
            if (s != null)
            {
                status = s.ItemDisplayName;
            }
            List<LookupItemView> reasons = mgr.GetLookItemByGroup("AppointmentReason");
            var r = reasons.FirstOrDefault(n => n.ItemId == a.ReasonId);
            if (r != null)
            {
                reason = r.ItemDisplayName;
            }
            List<LookupItemView> areas = mgr.GetLookItemByGroup("ServiceArea");
            var sa = areas.FirstOrDefault(n => n.ItemId == a.ServiceAreaId);
            if (sa != null)
            {
                serviceArea = sa.ItemDisplayName;
            }
            PatientAppointmentDisplay appointment = new PatientAppointmentDisplay()
            {
                ServiceArea = serviceArea,
                Reason = reason,
                AppointmentDate = a.AppointmentDate,
                Description = a.Description,
                Status = status,
            };

            return appointment;
        }
    }

    public class PatientAppointmentDisplay
    {
        public string ServiceArea { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }
}