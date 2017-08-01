using Application.Presentation;
using Entities.CCC.Enrollment;
using Interface.CCC.Enrollment;
using System.Collections.Generic;

namespace IQCare.CCC.UILogic.Enrollment
{
    public class PatientArtDistributionManager
    {
        private IPatientArtDistributionManager _artDistributionManager = (IPatientArtDistributionManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Enrollment.BPatientArtDistribution, BusinessProcess.CCC");

        public int AddPatientArtDistribution(PatientArtDistribution p)
        {
            var patientArvDistribution = new PatientArtDistribution()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                ArtRefillModel = p.ArtRefillModel,
                Cough = p.Cough,
                Diarrhea = p.Diarrhea,
                FamilyPlanning = p.FamilyPlanning,
                FamilyPlanningMethod = p.FamilyPlanningMethod,
                Fatigue = p.Fatigue,
                Fever = p.Fever,
                MissedArvDoses = p.MissedArvDoses,
                GenitalSore = p.GenitalSore,
                MissedArvDosesCount = p.MissedArvDosesCount,
                Nausea = p.Nausea,
                NewMedication = p.NewMedication,
                NewMedicationText = p.NewMedicationText,
                OtherSymptom = p.OtherSymptom,
                PregnancyStatus = p.PregnancyStatus,
                Rash = p.Rash,
                ReferedToClinic = p.ReferedToClinic,
                ReferedToClinicDate = p.ReferedToClinicDate,
            };
            return _artDistributionManager.AddPatientArtDistribution(patientArvDistribution);
        }

        public PatientArtDistribution GetPatientArtDistribution(int id)
        {
            var distribution = _artDistributionManager.GetPatientArtDistribution(id);
            return distribution;
        }

        public void DeletePatientArtDistribution(int id)
        {
            _artDistributionManager.DeletePatientArtDistribution(id);
        }

        public int UpdatePatientArtDistribution(PatientArtDistribution p)
        {
            var patientArvDistribution = new PatientArtDistribution()
            {
                PatientId = p.PatientId,
                PatientMasterVisitId = p.PatientMasterVisitId,
                ArtRefillModel = p.ArtRefillModel,
                Cough = p.Cough,
                Diarrhea = p.Diarrhea,
                FamilyPlanning = p.FamilyPlanning,
                FamilyPlanningMethod = p.FamilyPlanningMethod,
                Fatigue = p.Fatigue,
                Fever = p.Fever,
                MissedArvDoses = p.MissedArvDoses,
                GenitalSore = p.GenitalSore,
                MissedArvDosesCount = p.MissedArvDosesCount,
                Nausea = p.Nausea,
                NewMedication = p.NewMedication,
                NewMedicationText = p.NewMedicationText,
                OtherSymptom = p.OtherSymptom,
                PregnancyStatus = p.PregnancyStatus,
                Rash = p.Rash,
                ReferedToClinic = p.ReferedToClinic,
                ReferedToClinicDate = p.ReferedToClinicDate,
            };
            return _artDistributionManager.UpdatePatientArtDistribution(patientArvDistribution);
        }

        public List<PatientArtDistribution> GetByPatientId(int patientId)
        {
            var distribution = _artDistributionManager.GetByPatientId(patientId);
            return distribution;
        }
    }
}