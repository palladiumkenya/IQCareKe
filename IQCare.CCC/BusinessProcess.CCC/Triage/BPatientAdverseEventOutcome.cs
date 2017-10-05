using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Triage;
using Interface.CCC.Triage;
using System.Linq;

namespace BusinessProcess.CCC.Tb
{
    public class BPatientAdverseEventOutcome : ProcessBase, IPatientAdverseEventOutcomeManager
    {
        int result;

        public int CheckIfPatientAdverseEventOutcomeExists(int patientId, int adverseEventId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
               result= unitOfWork.PatientAdverseEventOutcomeRepository.FindBy(x => x.PatientId == patientId && x.PatientAdverseEventId==adverseEventId).Count();
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

        public int SavePatientAdverseEventOutcome(PatientAdverseEventOutcome patientAdverseEventOutcome)
        {
            var outcome = new PatientAdverseEventOutcome()
            {
                PatientId = patientAdverseEventOutcome.PatientId,
                PatientMasterVisitid = patientAdverseEventOutcome.PatientMasterVisitid,
                PatientAdverseEventId = patientAdverseEventOutcome.PatientAdverseEventId,
                OutcomeId = patientAdverseEventOutcome.OutcomeId,
                ActionTakenId = patientAdverseEventOutcome.ActionTakenId
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
                ActionTakenId=patientAdverseEventOutcome.ActionTakenId
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
