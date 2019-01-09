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
    public class BPatientWhoStageManager : ProcessBase, IPatientWhoStageManager
    {
        public int addPatientWhoStage(PatientWhoStage patientWhoStage)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientWhoStageRepository.Add(patientWhoStage);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patientWhoStage.Id;
            }
        }

        public PatientWhoStage GetPatientWhoStage(int patientId, int patientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var whoStage = unitOfWork.PatientWhoStageRepository.FindBy(
                    x => x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId).FirstOrDefault();
                unitOfWork.Dispose();
                return whoStage;
            }
        }

        public PatientWhoStage GetWhoStageById(int entityId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var whoStage = unitOfWork.PatientWhoStageRepository.GetById(entityId);
                unitOfWork.Dispose();
                return whoStage;
            }
        }

        public int UpdatePatientWhoStage(PatientWhoStage patientWhoStage)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientWhoStageRepository.Update(patientWhoStage);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patientWhoStage.Id;
            }
        }

        public List<PatientWhoStage> GetWhoStageListByPatient(int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var whoStage = unitOfWork.PatientWhoStageRepository.FindBy(x=>x.PatientId==patientId).OrderByDescending(x=>x.PatientMasterVisitId).ToList();
                unitOfWork.Dispose();
                return whoStage;
            }
        }
    }
}
