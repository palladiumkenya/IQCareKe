using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientBaslineAssessmentManager
    {
        private readonly IPatientBaselineAssessmentManager _patientBaselineAssessment = (IPatientBaselineAssessmentManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientBaselineAssessmentManager, BusinessProcess.CCC");

        public int AddArtInitiationbaseline(int patientId, int patientMasterVisitId, bool hbvInfected, bool pregnant, bool tbInfected, int whoStage, bool breastfeeding, decimal cd4Count, decimal muac, decimal weight, decimal height,int userId)
        {
            var patientArtInitiationBaselinesInsert= new PatientBaselineAssessment()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    HBVInfected = hbvInfected,
                    Pregnant = pregnant,
                    TBInfected = tbInfected,
                    WHOStage = whoStage,
                    Breastfeeding = breastfeeding,
                    CD4Count = cd4Count,
                    MUAC = muac,
                    Weight = weight,
                    Height = height,
                    CreatedBy = userId
                };
          return   _patientBaselineAssessment.AddPatientBaselineAssessment(patientArtInitiationBaselinesInsert);
        }

        public int UpdateArtInitiationbaseline(int id,int patientId, int patientMasterVisitId, bool hbvInfected, bool pregnant, bool tbInfected, int whoStage, bool breastfeeding, int cd4Count, decimal muac, decimal weight, decimal height,int userId)
        {
            var patientArtInitiationBaselinesUPdate = new PatientBaselineAssessment()
            {
                Id = id,
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                HBVInfected = hbvInfected,
                Pregnant = pregnant,
                TBInfected = tbInfected,
                WHOStage = whoStage,
                Breastfeeding = breastfeeding,
                CD4Count = cd4Count,
                MUAC = muac,
                Weight = weight,
                Height = height,
                CreatedBy = userId
            };
            return _patientBaselineAssessment.UpdatePatientBaselineAssessment(patientArtInitiationBaselinesUPdate);
        }

        public int DeletePatientArtInitiationbasline(int id)
        {
            return _patientBaselineAssessment.DeletePatientBaselineAssessment(id);
        }

        public List<PatientBaselineAssessment> GetPatientArtInitiationBaselines(int patientId)
        {
            return _patientBaselineAssessment.GetPatientBaselineAssessment(patientId);
        }

    }
}
