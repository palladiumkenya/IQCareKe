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
        // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int result;
        public int AddPatientCareEnding(PatientCareEnding patientCareEnding)
        {
            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _unitOfWork.PatientCareEndingRepository.Add(patientCareEnding);
                _unitOfWork.Complete();
                int Id= patientCareEnding.Id;
                _unitOfWork.Dispose();
                return Id;
            }
        }

        public List<PatientCareEnding> GetPatientCareEndings(int patientId)
        {

            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
               var ptnCareending= _unitOfWork.PatientCareEndingRepository.FindBy(x => x.PatientId == patientId && !x.Active).ToList();
                _unitOfWork.Dispose();
                return ptnCareending;

            }
        }

        public int UpdatePatientCareEnding(PatientCareEnding patientCareEnding)
        {

            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
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
                result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return result;
            }
        }

        public int DeletePatientCareEnding(int id)
        {

            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var careEnding = _unitOfWork.PatientCareEndingRepository.GetById(id);
                _unitOfWork.PatientCareEndingRepository.Remove(careEnding);
                result= _unitOfWork.Complete();
                _unitOfWork.Dispose();
                return result;
            }
        }

        public string PatientCareEndingStatus(int patientId)
        {

            using (UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext()))
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
                _unitOfWork.Dispose();
                return careendingStatus.ToString();
            }
        }
    }
}
