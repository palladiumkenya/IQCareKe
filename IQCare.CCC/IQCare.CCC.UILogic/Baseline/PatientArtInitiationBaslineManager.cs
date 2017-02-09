using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientArtInitiationBaslineManager
    {
        private readonly IPatientArtInitiationBaselineManager _mgr = (IPatientArtInitiationBaselineManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientArtInitiationBaselineManager, BusinessProcess.CCC");
        private int _result;

        public int AddArtInitiationbaseline(int patientId, int patientMasterVisitId, bool hbvInfected, bool pregnant,
            bool tbInfected, int whoStage, bool breastfeeding, int cd4Count, decimal viralLoad, DateTime viralLoadDate,
            decimal muac, decimal weight, decimal height, string artCohort, DateTime firstlineStartDate,
            int startRegimen)
        {
            PatientArtInitiationBaseline patientArtInitiationBaselines= new PatientArtInitiationBaseline()
                {
                    PatientId = patientId,
                    PatientMasterVisitId = patientMasterVisitId,
                    HBVInfected = hbvInfected,
                    Pregnant = pregnant,
                    TBInfected = tbInfected,
                    WHOStage = whoStage,
                    Breastfeeding = breastfeeding,
                    CD4Count = cd4Count,
                    ViralLoad = viralLoad,
                    ViralLoadDate = viralLoadDate,
                    MUAC = muac,
                    Weight = weight,
                    Height = height,
                    ARTCohort = artCohort,
                    FirstlineDate = firstlineStartDate,
                    StartRegimen = startRegimen
                };
          return _result=  _mgr.AddArtInitiationBaseline(patientArtInitiationBaselines);
        }

        public int UpdateArtInitiationbaseline(int patientId, int patientMasterVisitId, bool hbvInfected, bool pregnant,
            bool tbInfected, int whoStage, bool breastfeeding, int cd4Count, decimal viralLoad, DateTime viralLoadDate,
            decimal muac, decimal weight, decimal height, string artCohort, DateTime firstlineStartDate,
            int startRegimen)
        {
            PatientArtInitiationBaseline patientArtInitiationBaselines = new PatientArtInitiationBaseline()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                HBVInfected = hbvInfected,
                Pregnant = pregnant,
                TBInfected = tbInfected,
                WHOStage = whoStage,
                Breastfeeding = breastfeeding,
                CD4Count = cd4Count,
                ViralLoad = viralLoad,
                ViralLoadDate = viralLoadDate,
                MUAC = muac,
                Weight = weight,
                Height = height,
                ARTCohort = artCohort,
                FirstlineDate = firstlineStartDate,
                StartRegimen = startRegimen
            };
            return _result = _mgr.UpdateArtInitiationBaseline(patientArtInitiationBaselines);
        }

        public int DeletePatientArtInitiationbasline(int id)
        {
            return _result=_mgr.DeleteArtInitiationBaseline(id);
        }

        public List<PatientArtInitiationBaseline> GetPatientArtInitiationBaselines(int patientId)
        {
            return _mgr.GetPatientArtInitiationBaseline(patientId);
        }

    }
}
