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
        // private readonly UnitOfWork _unitOfWork = new UnitOfWork(new GreencardContext());
        private int result;
        public int AddPatientCareEnding(PatientCareEnding patientCareEnding)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientCareEndingRepository.Add(patientCareEnding);
                unitOfWork.Complete();
                int Id= patientCareEnding.Id;
                unitOfWork.Dispose();
                return Id;
            }
        }
        public List<PatientCareEnding> GetPatientCareEndingsByVisitId(int patientId,int patientmastervisitid)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var ptnCareending = unitOfWork.PatientCareEndingRepository.FindBy(x => x.PatientId == patientId &&x.PatientMasterVisitId==patientmastervisitid).OrderByDescending(x=>x.Id).ToList();
                unitOfWork.Dispose();
                return ptnCareending;

            }

        }

        public List<PatientCareEnding> GetPatientCareEndings(int patientId)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
               var ptnCareending= unitOfWork.PatientCareEndingRepository.FindBy(x => x.PatientId == patientId && !x.Active).ToList();
                unitOfWork.Dispose();
                return ptnCareending;

            }
        }

        public int UpdatePatientCareEnding(PatientCareEnding patientCareEnding)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var careEnding =
                   unitOfWork.PatientCareEndingRepository.FindBy(
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
                unitOfWork.PatientCareEndingRepository.Update(careEnding);
                result= unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }

        public int DeletePatientCareEnding(int id)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var careEnding = unitOfWork.PatientCareEndingRepository.GetById(id);
                unitOfWork.PatientCareEndingRepository.Remove(careEnding);
                result= unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }

        public string PatientCareEndingStatus(int patientId)
        {

            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var careendingStatus = new PatientCareEnding();
                var lookupManager = new BLookupManager();
                var exitReason =
                    unitOfWork.PatientCareEndingRepository.FindBy(
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
                unitOfWork.Dispose();
                return careendingStatus.ToString();
            }
        }

        public int ResetPatientCareEnding(PatientCareEnding patientCareEnding)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientCareEndingRepository.Update(patientCareEnding);
                unitOfWork.Complete();
                int Id = patientCareEnding.Id;
                unitOfWork.Dispose();
                return Id;
            }
        }
    }
}
