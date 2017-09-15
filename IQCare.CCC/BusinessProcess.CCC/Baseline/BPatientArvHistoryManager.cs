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
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientArvHistoryRepository.Add(patientArtUseHistory);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
        }

        public int UpdatePatientArvHistory(PatientArvHistory patientArtUseHistory)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientArvHistoryRepository.Update(patientArtUseHistory);
                Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return Result;
            }
           
        }

        public int DeletePatientArvHistory(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var partArtHistory = unitOfWork.PatientArvHistoryRepository.GetById(id);
                unitOfWork.PatientArvHistoryRepository.Remove(partArtHistory);
                 Result = unitOfWork.Complete();
                unitOfWork.Dispose();
                    return Result;
            }
        }

        public List<PatientArvHistory> GetPatientArvHistory(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patientArtHistory =
                        unitOfWork.PatientArvHistoryRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                            .OrderByDescending(x => x.Id)
                            .Distinct()
                            .ToList();
                unitOfWork.Dispose();
                return patientArtHistory;
            }

        }
    }
}
