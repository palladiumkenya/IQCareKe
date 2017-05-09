using System;
using System.Collections.Generic;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.CCC.UILogic
{
    public class PatientTreatmentTrackerManager
    {
        private IPatientTreatmentTrackerManager _patientTreatmentTrackerManager = (IPatientTreatmentTrackerManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.Lookup.BPatientTreatmentTrackerManager, BusinessProcess.CCC");

        public PatientTreamentTrackerLookup GetCurrentPatientRegimen(int patientId)
        {
            try
            {
                return _patientTreatmentTrackerManager.GetCurrentPatientRegimen(patientId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        public List<PatientTreamentTrackerLookup> GetPatientTreatmentSwitchesList(int patientId)
        {
            try
            {
                return GetPatientTreatmentSwitchesList(patientId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        public List<PatientTreamentTrackerLookup> GetPatientTreatmentInterrupList(int patientId)
        {
            try
            {
                return GetPatientTreatmentInterrupList(patientId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<PatientTreamentTrackerLookup> GetPatientTreatmentSubstitutionList(int patientId)
        {
            try
            {
                return GetPatientTreatmentSubstitutionList(patientId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
