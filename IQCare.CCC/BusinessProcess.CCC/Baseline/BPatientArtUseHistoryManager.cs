using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientArtUseHistoryManager:ProcessBase,IPatientArtUseHistoryManager
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientArtUseHistory(PatientArtUseHistory patientArtUseHistory)
        {
            _unitOfWork.PatientArvHistoryRepository.Add(patientArtUseHistory);
            return Result = _unitOfWork.Complete();
        }

        public int UpdatePatientArtUseHistory(PatientArtUseHistory patientArtUseHistory)
        {
            _unitOfWork.PatientArvHistoryRepository.Update(patientArtUseHistory);
            return Result=_unitOfWork.Complete();
        }

        public int DeletePatientArtUseHistory(int id)
        {
            var partArtHistory= _unitOfWork.PatientArvHistoryRepository.GetById(id);
            _unitOfWork.PatientArvHistoryRepository.Remove(partArtHistory);
            return Result=_unitOfWork.Complete();
        }

        public List<PatientArtUseHistory> GetPatientArtUseHistory(int patientId)
        {
            var patientArtHistory =
                _unitOfWork.PatientArvHistoryRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                    .OrderByDescending(x => x.Id)
                    .Distinct()
                    .ToList();
            return patientArtHistory;
        }
    }
}
