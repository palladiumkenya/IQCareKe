using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Encounter;
using Interface.CCC.Encounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Base;

namespace BusinessProcess.CCC
{
    public class BPatientHighRiskManager: ProcessBase, IPatientHighRiskManager
    {
        public PatientHighRisk addPatientHighRisk(PatientHighRisk patienthighrisk)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientHighRiskRepository.Add(patienthighrisk);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patienthighrisk;
            }
        }

        public PatientHighRisk GetPatientHighRisk(int patientId, int patientMasterVisitId, int partnerId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var PatientHighRisk = unitOfWork.PatientHighRiskRepository.FindBy(
                    x => x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId && x.PartnerId==partnerId).FirstOrDefault();
                unitOfWork.Dispose();
                return PatientHighRisk;
            }
        }
        public PatientHighRisk GetPatientHighRisks(int patientId, int patientMasterVisitId,int partnerId,int HighRisk)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var OI = unitOfWork.PatientHighRiskRepository.FindBy(
                    x => x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId && x.PartnerId == partnerId && x.HighRisk == HighRisk && !x.DeleteFlag).FirstOrDefault();
                unitOfWork.Dispose();
                return OI;
            }
        }

        public List<PatientHighRisk> GetPatientHighRisksList(int patientId, int patientMasterVisitId, int partnerId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var HighRisk = unitOfWork.PatientHighRiskRepository.FindBy(
                    x => x.PatientId == patientId && x.PatientMasterVisitId == patientMasterVisitId && x.PartnerId == partnerId  && !x.DeleteFlag).ToList();
                unitOfWork.Dispose();
                return HighRisk;
            }

        }
        public PatientHighRisk GetPatientHighRiskbyId(int entityId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var patienthighrisk = unitOfWork.PatientHighRiskRepository.GetById(entityId);
                unitOfWork.Dispose();
                return patienthighrisk;
            }
        }

        public PatientHighRisk UpdatePatientHighRisk(PatientHighRisk patienthighrisk)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientHighRiskRepository.Update(patienthighrisk);
                unitOfWork.Complete();
                unitOfWork.Dispose();
                return patienthighrisk;
            }
        }
    }
}
