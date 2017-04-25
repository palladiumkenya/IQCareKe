using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Baseline;
using Interface.CCC.Baseline;

namespace BusinessProcess.CCC.Baseline
{
    public class BPatientArvHistoryManager:ProcessBase,IPatientArvHistoryManager
    {
       // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientArvHistory(PatientArvHistory patientArtUseHistory)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientArvHistoryRepository.Add(patientArtUseHistory);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdatePatientArvHistory(PatientArvHistory patientArtUseHistory)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientArvHistoryRepository.Update(patientArtUseHistory);
                Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return Result;
            }
           
        }

        public int DeletePatientArvHistory(int id)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var partArtHistory = _unitOfWork.PatientArvHistoryRepository.GetById(id);
                _unitOfWork.PatientArvHistoryRepository.Remove(partArtHistory);
                 Result = _unitOfWork.Complete();
                _unitOfWork.Dispose();
                    return Result;
            }
        }

        public List<PatientArvHistory> GetPatientArvHistory(int patientId)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientArtHistory =
                        _unitOfWork.PatientArvHistoryRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                            .OrderByDescending(x => x.Id)
                            .Distinct()
                            .ToList();
                _unitOfWork.Dispose();
                return patientArtHistory;
            }

        }
    }
}
