using System.Collections.Generic;
using Application.Presentation;
using Entities.PatientCore;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{
    public class PatientPopulationManager
    {
        private IPatientPopuationManager _mgr = (IPatientPopuationManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientPopulationManager, BusinessProcess.CCC");
        private int _result;
        public int AddPatientPopulation(int personId, string populationtypeId, int populationCategory, int userId)
        {
            PatientPopulation patientPopulation=new PatientPopulation()
            {
                PersonId = personId,
                PopulationType = populationtypeId,
                PopulationCategory = populationCategory,
                CreatedBy = userId,
                Active = true
            };

           return _result= _mgr.AddPatientPopulation(patientPopulation);
        }

        public int UpdatePatientPopulation(PatientPopulation patientPopulation)
        {
            return _mgr.UpdatePatientPopulation(patientPopulation);
        }

        public void ResetPatientPopulation(int personId)
        {
            var list = _mgr.GetAllPatientPopulations(personId);
            foreach (var itemPopulation in list)
            {
                itemPopulation.DeleteFlag = true;
                _mgr.UpdatePatientPopulation(itemPopulation);
            }
        }

        public int DeletePatientPopulation(int id)
        {
            return _result = _mgr.DeletePatientPopulation(id);
        }

        public List<PatientPopulation> GetAllPatientPopulations(int patientId)
        {
            var myList = _mgr.GetAllPatientPopulations(patientId);
            return myList;
        }

        public List<PatientPopulation> GetCurrentPatientPopulations(int patientId)
        {
            var myList = _mgr.GetCurrentPatientPopulations(patientId);
            return myList;
        }

    }
}
