using System.Linq;
using DataAccess.Base;
using DataAccess.CCC.Context;
using DataAccess.CCC.Repository;
using Entities.CCC.Triage;
using Interface.CCC.Triage;

namespace BusinessProcess.CCC.Triage
{
    public class BPatientAdverseEventOutcome : ProcessBase,IPatientAdverseEventOutcomeManager
    {
        private int _result;

        public int AddPatientAdverseEventOutcome(PatientAdverseEventsOutcome patientAdverseEventsOutcome)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var adverseOutcome=new PatientAdverseEventsOutcome()
                {
                    PatientId = patientAdverseEventsOutcome.PatientId,
                    PatientMasterVisitId = patientAdverseEventsOutcome.PatientMasterVisitId,
                    OutcomeId = patientAdverseEventsOutcome.OutcomeId,
                    ActionTakenId = patientAdverseEventsOutcome.ActionTakenId
                };
                unitOfWork.PatientAdverseEventOutcomeRepository.Add(adverseOutcome);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public int DeletePatientAdverseEventOutcome(int id)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var adverseEvent = unitOfWork.PatientAdverseEventOutcomeRepository.GetById(id);
                unitOfWork.PatientAdverseEventOutcomeRepository.Remove(adverseEvent);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public int EditPatientAdverseEventOutcome(PatientAdverseEventsOutcome patientAdverseEvent)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                var adverseEvent=new PatientAdverseEventsOutcome()
                {
                    OutcomeId = patientAdverseEvent.OutcomeId,
                    ActionTakenId = patientAdverseEvent.ActionTakenId
                };

                unitOfWork.PatientAdverseEventOutcomeRepository .Update(adverseEvent);
                _result = unitOfWork.Complete();
                unitOfWork.Dispose();
                return _result;
            }
        }

        public int GetPatientAdverseEventOutcomeStatus(int id, int patientId)
        {
            using (UnitOfWork unitOfWork = new UnitOfWork(new GreencardContext()))
            {
                _result = unitOfWork.PatientAdverseEventOutcomeRepository
                    .FindBy(x => x.Id == id & x.PatientId == patientId).Count();
                unitOfWork.Dispose();
                return _result;
            }
        }
    }
}
