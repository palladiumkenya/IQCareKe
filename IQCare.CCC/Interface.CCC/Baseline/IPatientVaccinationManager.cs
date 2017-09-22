using Entities.CCC.Triage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.CCC.Baseline
{
    public interface IPatientVaccinationManager
    {
        int addPatientVaccination(PatientVaccination patientVaccination);
        int updatePatientVaccination(PatientVaccination patientVaccination);
        int DeletePatientVaccination(int id);
        List<PatientVaccination> GetPatientVaccinations(int patientId);
        List<PatientVaccination> VaccineExists(int patientId, int vaccine, string vaccineStage);
    }
}
