using System;
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
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        internal int Result;

        public int AddPatientArvHistory(PatientArvHistory patientArtUseHistory)
        {
            try
            {
                _unitOfWork.PatientArvHistoryRepository.Add(patientArtUseHistory);
                return Result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
         
        }

        public int UpdatePatientArvHistory(PatientArvHistory patientArtUseHistory)
        {
            try
            {
                _unitOfWork.PatientArvHistoryRepository.Update(patientArtUseHistory);
                return Result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
           
        }

        public int DeletePatientArvHistory(int id)
        {
            try
            {
                var partArtHistory = _unitOfWork.PatientArvHistoryRepository.GetById(id);
                _unitOfWork.PatientArvHistoryRepository.Remove(partArtHistory);
                return Result = _unitOfWork.Complete();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }

        public List<PatientArvHistory> GetPatientArvHistory(int patientId)
        {
            try
            {
                var patientArtHistory =
                    _unitOfWork.PatientArvHistoryRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag)
                        .OrderByDescending(x => x.Id)
                        .Distinct()
                        .ToList();
                return patientArtHistory;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }

        }
    }
}
