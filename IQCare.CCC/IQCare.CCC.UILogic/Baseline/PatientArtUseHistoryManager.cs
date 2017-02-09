using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Presentation;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace IQCare.CCC.UILogic.Baseline
{
   public class PatientArtUseHistoryManager
    {
        private readonly IPatientArtUseHistoryManager _patientArtUseHistoryManager = (IPatientArtUseHistoryManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientArtUseHistoryManager, BusinessProcess.CCC");
        private int _result;

        public int AddPatientArtUseHistory(int patientId,int patientMasterVisitId,string treatmentType,string purpose,string regimen,DateTime dateLastUsed)
        {
            PatientArtUseHistory patientArtUseHistory=new PatientArtUseHistory()
            {
                PatientId   = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                TreatmentType = treatmentType,
                Purpose = purpose,
                Regimen = regimen,
                DateLastUsed = dateLastUsed
            };

            if (GetPatientArtUseHistory(patientId).Count < 1)
            {
                return _result = _patientArtUseHistoryManager.AddPatientArtUseHistory(patientArtUseHistory);
            }
            else
            {
                return _result=0;
            }
        }

        public int UpdatePatientArtUseHistory(int patientId, int patientMasterVisitId, string treatmentType, string purpose, string regimen, DateTime dateLastUsed)
        {
            PatientArtUseHistory patientArtUseHistory = new PatientArtUseHistory()
            {
                PatientId = patientId,
                PatientMasterVisitId = patientMasterVisitId,
                TreatmentType = treatmentType,
                Purpose = purpose,
                Regimen = regimen,
                DateLastUsed = dateLastUsed
            };

            return _result = _patientArtUseHistoryManager.UpdatePatientArtUseHistory(patientArtUseHistory);
        }

        public int DeletePatientArtUseHistory(int id)
        {
            return _result=_patientArtUseHistoryManager.DeletePatientArtUseHistory(id);
        }

        public List<PatientArtUseHistory> GetPatientArtUseHistory(int patientId)
        {
            return _patientArtUseHistoryManager.GetPatientArtUseHistory(patientId);
        }
    }
}
