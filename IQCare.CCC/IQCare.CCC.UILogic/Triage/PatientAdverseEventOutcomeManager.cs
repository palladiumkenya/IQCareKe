using Application.Presentation;
using Entities.CCC.Triage;
using Interface.CCC.Triage;
using System;

namespace IQCare.CCC.UILogic.Triage
{
    public class PatientAdverseEventOutcomeManager
    {
        private IPatientAdverseEventOutcomeManager _patientAdverseEventOutcomeManager = (IPatientAdverseEventOutcomeManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Triage.BPatientAdverseEventOutcome, BusinessProcess.CCC");
        private int result;
        private string jsonMessage;

        public int CheckIfPatientAdverseEventOutcomeExists(int patientId, int adverseEventId)
        {
            try
            {
                result = _patientAdverseEventOutcomeManager.CheckIfPatientAdverseEventOutcomeExists(patientId, adverseEventId);
                return result;
            }catch(Exception e)
            {
                throw new Exception(e.Message.ToString());
                //you can also log the problem here
            }
        }
        public string SavePatientAdverseEventOutcome(PatientAdverseEventOutcome patientAdverseEventOutcome)
        {
            try
            {
                result = _patientAdverseEventOutcomeManager.SavePatientAdverseEventOutcome(patientAdverseEventOutcome);
                if (result > 0)
                {
                    jsonMessage = "Adverse Event Outcome Saved!";
                }
            }
            catch (Exception e)
            {
                jsonMessage = e.Message.ToString();
                //you can also log the problem here
            }
            return jsonMessage;
        }
        public string UpdatePatientAdverseEventOutcome(PatientAdverseEventOutcome patientAdverseEventOutcome)
        {
            try
            {
                result = _patientAdverseEventOutcomeManager.UpdatePatientAdverseEventOutcome(patientAdverseEventOutcome);
                if (result > 0)
                {
                    jsonMessage = "Adverse Event Outcome updated!";
                }
            }
            catch (Exception e)
            {
                jsonMessage = e.Message.ToString();
            }
            return jsonMessage;
        }
        public string DeletePatientAdverseEventOutcome(int id)
        {
            try
            {
                result = _patientAdverseEventOutcomeManager.DeletePatientAdverseEventOutcome(id);
                if (result > 0)
                {
                    jsonMessage = "Adverse Event Outcome Deleted";
                }
            }
            catch (Exception e)
            {
                jsonMessage = e.Message.ToString();
            }

            return jsonMessage;
        }

    }
}
