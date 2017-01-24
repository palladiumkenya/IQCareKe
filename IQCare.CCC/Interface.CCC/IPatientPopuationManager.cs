using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.PatientCore;

namespace Interface.CCC
{
    public interface IPatientPopuationManager
    {
        void AddPatientPopulation(PatientPopulation patientPopulation);
        void UpdatePatientPopulation(PatientPopulation patientPopulation);
        void DeletePatientPopulation(int id);
        List<PatientPopulation> GetAllPatientPopulations(int patientId);
        List<PatientPopulation> GetCurrentPatientPopulations(int patientId);

    }
}
