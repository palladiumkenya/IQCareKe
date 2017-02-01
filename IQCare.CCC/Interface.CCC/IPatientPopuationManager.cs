using System.Collections.Generic;
using Entities.PatientCore;

namespace Interface.CCC
{
    public interface IPatientPopuationManager
    {
        int AddPatientPopulation(PatientPopulation patientPopulation);
        int UpdatePatientPopulation(PatientPopulation patientPopulation);
        int DeletePatientPopulation(int id);
        List<PatientPopulation> GetAllPatientPopulations(int patientId);
        List<PatientPopulation> GetCurrentPatientPopulations(int patientId);

    }
}
