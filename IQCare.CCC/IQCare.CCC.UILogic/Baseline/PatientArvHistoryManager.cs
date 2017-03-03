using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace IQCare.CCC.UILogic.Baseline
{
    public class PatientArvHistoryManager
    {
        private readonly IPatientArvHistoryManager _patientArtUseHistoryManager = (IPatientArvHistoryManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Baseline.BPatientArvHistoryManager, BusinessProcess.CCC");

        public int AddPatientArtUseHistory(int id,int patientId,int patientMasterVisitId,string treatmentType,string purpose,string regimen,DateTime dateLastUsed,int userId)
        {
            var patientArtUseHistoryInsert=new PatientArvHistory()
            {
                PatientId   = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                TreatmentType = treatmentType,
                Purpose = purpose,
                Regimen = regimen,
                DateLastUsed = dateLastUsed,
                CreatedBy = userId
            };

            return _patientArtUseHistoryManager.AddPatientArvHistory(patientArtUseHistoryInsert);        
        }

        public int UpdatePatientArtUseHistory(int id,int patientId, int patientMasterVisitId, string treatmentType, string purpose, string regimen, DateTime dateLastUsed,int userId)
        {
            PatientArvHistory patientArtUseHistoryUpdate = new PatientArvHistory()
            {
                Id = id,
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                TreatmentType = treatmentType,
                Purpose = purpose,
                Regimen = regimen,
                DateLastUsed = dateLastUsed,
                CreatedBy = userId
            };

            return _patientArtUseHistoryManager.UpdatePatientArvHistory(patientArtUseHistoryUpdate);
        }

        public int DeletePatientArtUseHistory(int id)
        {
            return _patientArtUseHistoryManager.DeletePatientArvHistory(id);
        }

        public List<PatientArvHistory> GetPatientArtUseHistory(int patientId)
        {
            return _patientArtUseHistoryManager.GetPatientArvHistory(patientId);
        }
    }
}
