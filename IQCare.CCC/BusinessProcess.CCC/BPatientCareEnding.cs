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
            _unitOfWork.PatientCareEndingRepository.Add(patientCareEnding);
            _unitOfWork.Complete();
            return patientCareEnding.Id;
        }

        public List<PatientCareEnding> GetPatientCareEndings(int patientId)
        {
            return _unitOfWork.PatientCareEndingRepository.FindBy(x => x.PatientId == patientId && !x.Active).ToList();
        }

        public int UpdatePatientCareEnding(PatientCareEnding patientCareEnding)
        {
            var careEnding =
                _unitOfWork.PatientCareEndingRepository.FindBy(x => x.PatientId == patientCareEnding.PatientId & !x.DeleteFlag & !x.Active)
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

        public int DeletePatientCareEnding(int id)
        {
            var careEnding = _unitOfWork.PatientCareEndingRepository.GetById(id);
            _unitOfWork.PatientCareEndingRepository.Remove(careEnding);
            return _unitOfWork.Complete();
        }

        public string PatientCareEndingStatus(int patientId)
        {

           var careendingStatus = new PatientCareEnding();
            var lookupManager = new BLookupManager();
           var exitReason =
                _unitOfWork.PatientCareEndingRepository.FindBy(x => x.PatientId == patientId & !x.DeleteFlag & !x.Active)
                    .FirstOrDefault();
            if (exitReason != null)
            {
                careendingStatus.ExitReason =  exitReason.ExitReason;
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
    }
}
