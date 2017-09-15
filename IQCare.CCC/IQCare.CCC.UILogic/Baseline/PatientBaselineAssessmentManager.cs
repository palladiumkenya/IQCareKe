using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientBaselineAssessmentManager
    {
        private readonly IPatientBaselineAssessmentManager _patientBaselineAssessment = (IPatientBaselineAssessmentManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientBaselineAssessmentManager, BusinessProcess.CCC");
        private int _id = 0;
        private int _result = 0;
        
        public int ManagePatientBaselineAssessment(int id,int patientId, int patientMasterVisitId, bool hbvInfected, bool pregnant, bool tbInfected, int whoStage, bool breastfeeding, decimal cd4Count, decimal muac, decimal weight, decimal height,int userId)
        {
            _id = _patientBaselineAssessment.CheckIfPatientBaselineExists(patientId);

            var patientArtInitiationBaselinesInsert= new PatientBaselineAssessment()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    HBVInfected = hbvInfected,
                    Pregnant = pregnant,
                    TBInfected = tbInfected,
                    WHOStage = whoStage,
                    Breastfeeding = breastfeeding,
                   // CD4Count = cd4Count,
                    MUAC = muac,
                    Weight = weight,
                    Height = height,
                    CreatedBy = userId
                };
            if (cd4Count > 0)
            {
                patientArtInitiationBaselinesInsert.CD4Count = cd4Count;}
            _result = (_id > 0)
                ? _patientBaselineAssessment.UpdatePatientBaselineAssessment(patientArtInitiationBaselinesInsert): _patientBaselineAssessment.AddPatientBaselineAssessment(patientArtInitiationBaselinesInsert);
            return _result;
        }

        public int UpdatePatientBaselineAssessment(int id,int patientId, int patientMasterVisitId, bool hbvInfected, bool pregnant, bool tbInfected, int whoStage, bool breastfeeding, decimal cd4Count, decimal muac, decimal weight, decimal height,int userId)
        {
            var patientBaseline = new PatientBaselineAssessment()
            {
                HBVInfected = hbvInfected,
                Pregnant = pregnant,
                TBInfected = tbInfected,
                WHOStage = whoStage,
                Breastfeeding = breastfeeding,
                CD4Count = cd4Count,
                MUAC = muac,
                Weight = weight,
                Height = height,
            };
            return _patientBaselineAssessment.UpdatePatientBaselineAssessment(patientBaseline);
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
