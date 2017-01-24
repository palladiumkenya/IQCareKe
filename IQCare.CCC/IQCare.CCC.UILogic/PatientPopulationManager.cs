using System.Collections.Generic;
using Application.Presentation;
using Entities.PatientCore;
using Interface.CCC;

namespace IQCare.CCC.UILogic
{
    public class PatientPopulationManager
    {
        private IPatientPopuationManager _mgr = (IPatientPopuationManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.PatientMaritalStatusManager, BusinessProcess.CCC");

        public void AddPatientPopulation(int patientId, int populationTypeId, int populationcategory)
        {
            PatientPopulation patientPopulation=new PatientPopulation()
            {
                PatientId = patientId,
                PopulationTypeId = populationTypeId,
                PopulationCategory = populationTypeId
            };

            _mgr.AddPatientPopulation(patientPopulation);
        }

        public void UpdatePatientPopulation(int populationTypeId, int populationcategory)
        {
            PatientPopulation patientPopulation = new PatientPopulation()
            {
                PopulationTypeId = populationTypeId,
                PopulationCategory = populationTypeId
            };

            _mgr.UpdatePatientPopulation(patientPopulation);
        }

        public void DeletePatientPopulation(int id)
        {
            _mgr.DeletePatientPopulation(id);
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
