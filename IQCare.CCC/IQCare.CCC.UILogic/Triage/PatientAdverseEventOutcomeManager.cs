using Application.Presentation;
using Entities.CCC.Triage;
using Interface.CCC.Triage;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IQCare.CCC.UILogic.Triage
{
    public class PatientAdverseEventOutcomeManager
    {
        private IPatientAdverseEventOutcomeManager _patientAdverseEventOutcomeManager = (IPatientAdverseEventOutcomeManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Triage.BPatientAdverseEventOutcome, BusinessProcess.CCC");
        private int result;
        private string jsonMessage;

        public int CheckIfPatientAdverseEventOutcomeExists(int patientId, int adverseEventId,int patientmasterVisitId)
        {
            try
            {
                result = _patientAdverseEventOutcomeManager.CheckIfPatientAdverseEventOutcomeExists(patientId, adverseEventId, patientmasterVisitId);
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


        public List<PatientAdverseEventOutcome> GetAdverseEventOutcome(int adverseId,int patientMasterVisitId,int patientId)
        {
            try
            {
                return _patientAdverseEventOutcomeManager.GetAdverseEventOutcome(adverseId,patientId);
            }
            catch (Exception  e)
            {
                throw new Exception("Error getting adverse Event Outcome"+e.InnerException);
            }
        }
    }
}
