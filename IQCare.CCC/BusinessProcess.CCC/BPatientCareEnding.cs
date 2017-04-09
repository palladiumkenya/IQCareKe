using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;

namespace BusinessProcess.CCC
{
    public class BPatientCareEnding : ProcessBase, IPatientCareEnding
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        public int AddPatientCareEnding(PatientCareEnding patientCareEnding)
        {
            try
            {
                _unitOfWork.PatientCareEndingRepository.Add(patientCareEnding);
                _unitOfWork.Complete();
                return patientCareEnding.Id;
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

        public List<PatientCareEnding> GetPatientCareEndings(int patientId)
        {
            try
            {
                return
                    _unitOfWork.PatientCareEndingRepository.FindBy(x => x.PatientId == patientId && !x.Active).ToList();
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

        public int UpdatePatientCareEnding(PatientCareEnding patientCareEnding)
        {
            try
            {
                var careEnding =
                    _unitOfWork.PatientCareEndingRepository.FindBy(
                            x => x.PatientId == patientCareEnding.PatientId & !x.DeleteFlag & !x.Active)
                        .FirstOrDefault();
                if (careEnding != null)
                {
                    careEnding.ExitReason = patientCareEnding.ExitReason;
                    careEnding.ExitDate = patientCareEnding.ExitDate;
                    careEnding.TransferOutFacility = patientCareEnding.TransferOutFacility;
                    careEnding.DateOfDeath = patientCareEnding.DateOfDeath;
                    careEnding.CareEndingNotes = patientCareEnding.CareEndingNotes;
                }
                _unitOfWork.PatientCareEndingRepository.Update(careEnding);
                return _unitOfWork.Complete();
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

        public int DeletePatientCareEnding(int id)
        {
            try
            {
                var careEnding = _unitOfWork.PatientCareEndingRepository.GetById(id);
                _unitOfWork.PatientCareEndingRepository.Remove(careEnding);
                return _unitOfWork.Complete();
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

        public string PatientCareEndingStatus(int patientId)
        {
            try
            {
                var careendingStatus = new PatientCareEnding();
                var lookupManager = new BLookupManager();
                var exitReason =
                    _unitOfWork.PatientCareEndingRepository.FindBy(
                            x => x.PatientId == patientId & !x.DeleteFlag & !x.Active)
                        .FirstOrDefault();
                if (exitReason != null)
                {
                    careendingStatus.ExitReason = exitReason.ExitReason;
                    careendingStatus.ExitDate = exitReason.ExitDate;
                    careendingStatus.TransferOutFacility = exitReason.TransferOutFacility;
                    careendingStatus.DateOfDeath = exitReason.DateOfDeath;
                    careendingStatus.CareEndingNotes = exitReason.CareEndingNotes;
                }
                else
                {
                    careendingStatus.ExitReason = 0;
                }

                return careendingStatus.ToString();
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
