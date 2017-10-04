using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Encounter;

namespace Interface.CCC.Encounter
{
    public interface IPatientCareEnding
    {
        int AddPatientCareEnding(PatientCareEnding patientCareEnding);
        int UpdatePatientCareEnding(PatientCareEnding patientCareEnding);
        int DeletePatientCareEnding(int id);
        List<PatientCareEnding> GetPatientCareEndings(int patientId);
        string PatientCareEndingStatus(int patientId);
        int ResetPatientCareEnding(PatientCareEnding patientCareEnding);
    }
}
