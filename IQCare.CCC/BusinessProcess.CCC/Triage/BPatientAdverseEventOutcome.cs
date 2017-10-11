using System;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Triage;
using Interface.CCC.Triage;
using System.Linq;
using System.Collections.Generic;

namespace BusinessProcess.CCC.Triage
{
    public class BPatientAdverseEventOutcome : ProcessBase, IPatientAdverseEventOutcomeManager
    {
        int result;

        public int CheckIfPatientAdverseEventOutcomeExists(int patientId, int adverseEventId, int patientMasterVisitId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
               result= unitOfWork.PatientAdverseEventOutcomeRepository.FindBy(x => x.PatientId == patientId && x.AdverseEventId==adverseEventId && x.PatientMasterVisitid==patientMasterVisitId).Count();
                unitOfWork.Dispose();
                return result;
            }
        }

        public int DeletePatientAdverseEventOutcome(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var outcome = unitOfWork.PatientAdverseEventOutcomeRepository.GetById(id);
                unitOfWork.PatientAdverseEventOutcomeRepository.Remove(outcome);
                result= unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }
        }

        public List<PatientAdverseEventOutcome> GetAdverseEventOutcome(int adverseId,int patientMasterVisitId, int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var outcome =
                    unitOfWork.PatientAdverseEventOutcomeRepository.FindBy(x => x.AdverseEventId == adverseId && x.PatientMasterVisitid==patientMasterVisitId && x.PatientId==patientId).ToList();
                unitOfWork.Dispose();
                return outcome;
            }
        }

        public int SavePatientAdverseEventOutcome(PatientAdverseEventOutcome patientAdverseEventOutcome)
        {
            var outcome = new PatientAdverseEventOutcome()
            {
                PatientId = patientAdverseEventOutcome.PatientId,
                PatientMasterVisitid = patientAdverseEventOutcome.PatientMasterVisitid,
                AdverseEventId = patientAdverseEventOutcome.AdverseEventId,
                OutcomeId = patientAdverseEventOutcome.OutcomeId,
                OutcomeDate = patientAdverseEventOutcome.OutcomeDate,
                UserId = patientAdverseEventOutcome.UserId
            };
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientAdverseEventOutcomeRepository.Add(outcome);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;
            }

        }

        public int UpdatePatientAdverseEventOutcome(PatientAdverseEventOutcome patientAdverseEventOutcome)
        {
            var outcome = new PatientAdverseEventOutcome()
            {
                //Id=patientAdverseEventOutcome.Id,
                OutcomeId=patientAdverseEventOutcome.OutcomeId,
            };
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                unitOfWork.PatientAdverseEventOutcomeRepository.Update(outcome);
                result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return result;

            }
        }

    }
}
