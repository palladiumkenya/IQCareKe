using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;

namespace IQCare.CCC.UILogic
{
    public class PatientAdherenceAssessmentManager
    {
        private readonly IPatientAdherenceAssessessment _patientAdherenceAssessessment =(IPatientAdherenceAssessessment)  ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientAdherenceAssessment, BusinessProcess.CCC");

        public int AddPatientAdherenceAssessment(int patientId, int patientMasterVisitId, int createdBy,bool feelBetter, bool carelessAboutMedicine, bool feelWorse, bool forgetMedicine)
        {
            try
            {
                PatientAdherenceAssessment adherenceAssessment = new PatientAdherenceAssessment();

                adherenceAssessment.PatientId = patientId;
                adherenceAssessment.PatientMasterVisitId = patientMasterVisitId;

                adherenceAssessment.FeelBetter = feelBetter;
                adherenceAssessment.CarelessAboutMedicine = carelessAboutMedicine;
                adherenceAssessment.FeelWorse = feelWorse;
                adherenceAssessment.ForgetMedicine = forgetMedicine;

                adherenceAssessment.DeleteFlag = false;
                adherenceAssessment.CreatedBy = createdBy;
                adherenceAssessment.CreateDate = DateTime.Now;

                return _patientAdherenceAssessessment.AddPatientAdherenceAssessment(adherenceAssessment);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public PatientAdherenceAssessment GetCurrentAdheranceStatus(int patientId)
        {
            return _patientAdherenceAssessessment.GetPatientCurrentAdheranceStatus(patientId);
        }

        public List<PatientAdherenceAssessment> GetPatientAdheranceHistory(int patientId)
        {
            return _patientAdherenceAssessessment.GetAdherenceAssessmentsList(patientId);
        }

    }
}
