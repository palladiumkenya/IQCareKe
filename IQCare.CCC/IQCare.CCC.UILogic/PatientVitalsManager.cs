using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Triage;
using Interface.CCC;
using IQCare.Events;

namespace IQCare.CCC.UILogic
{
    public class PatientVitalsManager 
    {
        IPatientVitals _vitals = (IPatientVitals)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientVitals, BusinessProcess.CCC");
        public int AddPatientVitals(PatientVital p, int facilityId)
        {
            PatientVital patientVital = new PatientVital()
            {
                PatientId = p.PatientId,
                BpSystolic = p.BpSystolic,
                Bpdiastolic = p.Bpdiastolic,
                HeartRate = p.HeartRate,
                Height = p.Height,
                Muac = p.Muac,
                PatientMasterVisitId = p.PatientMasterVisitId,
                RespiratoryRate = p.RespiratoryRate,
                SpO2 = p.SpO2,
                Temperature = p.Temperature,
                Weight = p.Weight,
                BMI = p.BMI,
                HeadCircumference = p.HeadCircumference,
                BMIZ = p.BMIZ,
                WeightForHeight = p.WeightForHeight,
                WeightForAge = p.WeightForAge,
                VisitDate = p.VisitDate,
                CreatedBy = p.CreatedBy,
                AgeforZ = p.AgeforZ,
                NursesComments = p.NursesComments
            };
            int vitalsId = _vitals.AddPatientVitals(patientVital);
            if (vitalsId > 1)
            {
                MessageEventArgs args = new MessageEventArgs()
                {
                    PatientId = p.PatientId,
                    PatientMasterVisitId = p.PatientMasterVisitId,
                    EntityId = vitalsId,
                    MessageType = MessageType.ObservationResult,
                    EventOccurred = "Observation Result Unsolicited = ",
                    FacilityId = facilityId,
                    ObservationType = ObservationType.Vitals
                };

                Publisher.RaiseEventAsync(this, args).ConfigureAwait(false);
            }
            return vitalsId;
        }

        public PatientVital GetPatientVitals(int id)
        {
            var patientVitals = _vitals.GetPatientVitals(id);
            return patientVitals;
        }

        public void DeletePatientVitals(int id)
        {
            _vitals.DeletePatientVitals(id);
        }

        public int UpdatePatientVitals(PatientVital p)
        {
            PatientVital patientVital = new PatientVital()
            {
                PatientId = p.PatientId,
                BpSystolic = p.BpSystolic,
                Bpdiastolic = p.Bpdiastolic,
                HeartRate = p.HeartRate,
                Height = p.Height,
                Muac = p.Muac,
                PatientMasterVisitId = p.PatientMasterVisitId,
                RespiratoryRate = p.RespiratoryRate,
                SpO2 = p.SpO2,
                Temperature = p.Temperature,
                Weight = p.Weight,
                BMI = p.BMI,
                HeadCircumference = p.HeadCircumference,
                VisitDate = p.VisitDate
            };
            return _vitals.UpdatePatientVitals(patientVital);
        }

        public PatientVital GetByPatientId(int patientId)
        {
            var patientVitals = _vitals.GetByPatientId(patientId);
            return patientVitals;
        }

        public PatientVital GetByPatientVisitId(int patientVisitId)
        {
            var patientVitals = _vitals.GetByPatientVisitId(patientVisitId);
            return patientVitals;
        }

        public PatientVital GetPatientVitalsByMasterVisitId(int patientId, int patientMasterVisitId)
        {
            try
            {
                return _vitals.GetPatientVitalsByMasterVisitId(patientId, patientMasterVisitId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }           
        }

        public PatientVital GetPatientVitalsBaseline(int patientId)
        {
            try
            {
                var ptnVitals = _vitals.GetPatientVitalsBaseline(patientId);
                return ptnVitals;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
              
        }
    }
}