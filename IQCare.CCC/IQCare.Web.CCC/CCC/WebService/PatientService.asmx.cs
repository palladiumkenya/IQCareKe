using Application.Presentation;
using Entities.CCC.Appointment;
using Entities.CCC.Lookup;
using Entities.CCC.Triage;
using Interface.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using Entities.CCC.Baseline;
using Interface.CCC.Visit;
using IQCare.CCC.UILogic;
using IQCare.CCC.UILogic.Baseline;

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
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod]
        public string AddPatientAppointment(int patientId, int patientMasterVisitId, DateTime appointmentDate, string description, int reasonId, int serviceAreaId, int statusId, int differentiatedCareId)
        {
            PatientAppointment patientAppointment = new PatientAppointment()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                AppointmentDate = appointmentDate,
                Description = description,
                DifferentiatedCareId = differentiatedCareId,
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
                Msg = e.Message;
            }
            return Msg;
        }

        [WebMethod]
        public string AddPatientFamilyTesting(int patientId, int patientMasterVisitId, string firstName, string middleName, string lastName, int sex, DateTime dob, int relationshipId, int baselineHivStatusId, DateTime baselineHivStatusDate, int hivTestingresultId, DateTime hivTestingresultDate, bool cccreferal, string cccReferalNumber)
        {
            PatientFamilyTesting patientAppointment = new PatientFamilyTesting()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                FirstName = firstName,
                MiddleName = middleName,
                LastName = lastName,
                Sex = sex,
                DateOfBirth = dob,
                RelationshipId = relationshipId,
                BaseLineHivStatusId = baselineHivStatusId,
                BaselineHivStatusDate = baselineHivStatusDate,
                HivTestingResultsDate = hivTestingresultDate,
                HivTestingResultsId = hivTestingresultId,
                CccReferal = cccreferal,
                CccReferaalNumber = cccReferalNumber
            };
            try
            {
                var testing = new PatientFamilyTestingManager();
                Result = testing.AddPatientFamilyTestings(patientAppointment);
                if (Result > 0)
                {
                    Msg = "Patient family testing Added Successfully!";
                }
            }
            catch (Exception e)
            {
                Msg = e.Message;
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
                Msg = e.Message;
            }
            return appointmentsDisplay;
        }

        [WebMethod]
        public List<PatientFamilyDisplay> GetFamilyTestings(string patientId)
        {
            List<PatientFamilyDisplay> familyDisplays = new List<PatientFamilyDisplay>();
            List<PatientFamilyTesting> familytestings = new List<PatientFamilyTesting>();
            try
            {
                var patientFamily = new PatientFamilyTestingManager();
                int id = Convert.ToInt32(patientId);
                familytestings = patientFamily.GetPatienFamilyList(id);
                foreach (var member in familytestings)
                {
                    PatientFamilyDisplay familyDisplay = MapMembers(member);
                    familyDisplays.Add(familyDisplay);
                }

            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return familyDisplays;
        }

        [WebMethod]
        public int GetPatientAppointmentCount(DateTime date)
        {
            int count = 0;
            List<PatientAppointment> appointments = new List<PatientAppointment>();
            try
            {
                var appointment = new PatientAppointmentManager();
                appointments = appointment.GetByDate(date);
                count = appointments.Count;
            }
            catch (Exception e)
            {
                Msg = e.Message;
            }
            return count;
        }

        private PatientFamilyDisplay MapMembers(PatientFamilyTesting member)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            string relationship = "";
            string baselineHivStatus = "";
            string hivStatus = "";
            List<LookupItemView> relationships = mgr.GetLookItemByGroup("Relationship");
            var s = relationships.FirstOrDefault(n => n.ItemId == member.RelationshipId);
            if (s != null)
            {
                relationship = s.ItemDisplayName;
            }
            List<LookupItemView> reasons = mgr.GetLookItemByGroup("BaseLineHivStatus");
            var r = reasons.FirstOrDefault(n => n.ItemId == member.BaseLineHivStatusId);
            if (r != null)
            {
                baselineHivStatus = r.ItemDisplayName;
            }
            List<LookupItemView> areas = mgr.GetLookItemByGroup("HivTestingResult");
            var sa = areas.FirstOrDefault(n => n.ItemId == member.HivTestingResultsId);
            if (sa != null)
            {
                hivStatus = sa.ItemDisplayName;
            }

            PatientFamilyDisplay familyMemberDisplay = new PatientFamilyDisplay()
            {
                Name = member.FirstName + ' ' + member.MiddleName + ' ' + member.LastName,
                Relationship = relationship,
                BaseLineHivStatus = baselineHivStatus,
                BaseLineHivStatusDate = member.BaselineHivStatusDate,
                HivStatusResult = hivStatus,
                HivStatusResultDate = member.HivTestingResultsDate,
                CccReferal = member.CccReferal.ToString()
            };
            return familyMemberDisplay;
        }

        private PatientAppointmentDisplay Mapappointments(PatientAppointment a)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            string status = "";
            string reason = "";
            string serviceArea = "";
            string differentiatedCare = "";
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
            List<LookupItemView> care = mgr.GetLookItemByGroup("DifferentiatedCare");
            var dc = care.FirstOrDefault(n => n.ItemId == a.DifferentiatedCareId);
            if (dc != null)
            {
                differentiatedCare = dc.ItemDisplayName;
            }
            PatientAppointmentDisplay appointment = new PatientAppointmentDisplay()
            {
                ServiceArea = serviceArea,
                Reason = reason,
                AppointmentDate = a.AppointmentDate,
                Description = a.Description,
                Status = status,
                DifferentiatedCare = differentiatedCare
            };

            return appointment;
        }
    }

    public class PatientAppointmentDisplay
    {
        public string ServiceArea { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
        public string DifferentiatedCare { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }

    public class PatientFamilyDisplay
    {
        public string Name { get; set; }
        public string Relationship { get; set; }
        public string BaseLineHivStatus { get; set; }
        public DateTime ? BaseLineHivStatusDate { get; set; }
        public string HivStatusResult { get; set; }
        public DateTime ? HivStatusResultDate { get; set; }
        public string CccReferal { get; set; }
    }
}