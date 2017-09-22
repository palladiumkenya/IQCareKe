using System;
using Application.Presentation;
using Entities.CCC.Triage;
using Interface.CCC.Triage;

namespace IQCare.CCC.UILogic.Triage
{
   public class PatientAdverseEventOutcomeManager
    {
        private readonly IPatientAdverseEventOutcomeManager _patientAdverseEvent  = (IPatientAdverseEventOutcomeManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Triage.BPatientAdverseEventOutcome, BusinessProcess.CCC");


        public int GetPatientAdverseEventOutcomeStatus(int id, int patientId)
        {
            try
            {
                return _patientAdverseEvent.GetPatientAdverseEventOutcomeStatus(id, patientId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int AddPatientAdverseEventOutcome(PatientAdverseEventsOutcome eventsOutcome)
        {
            try
            {
                var adverseEvent = new PatientAdverseEventsOutcome()
                {
                    PatientId = eventsOutcome.PatientId,
                    PatientMasterVisitId = eventsOutcome.PatientMasterVisitId,
                    OutcomeId = eventsOutcome.OutcomeId,
                    ActionTakenId = eventsOutcome.ActionTakenId
                };
                return _patientAdverseEvent.AddPatientAdverseEventOutcome(adverseEvent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int EditPatientAdverseEventOutcome(PatientAdverseEventsOutcome eventsOutcome)
        {
            try
            {
                var adverseEvent=new PatientAdverseEventsOutcome()
                {
                    Id = eventsOutcome.Id,
                    OutcomeId = eventsOutcome.OutcomeId,
                    ActionTakenId = eventsOutcome.ActionTakenId
                };
              return  _patientAdverseEvent.EditPatientAdverseEventOutcome(adverseEvent);    
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public int DeletePatientAdverseEventOutcome(int id)
        {
            try
            {
                return _patientAdverseEvent.DeletePatientAdverseEventOutcome(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
