using Entities.Common;
using Entities.PatientCore;
using System.Collections.Generic;

namespace Interface.PatientCore
{
    public interface IPatientService
    {
        Patient GetPatient(int Id);
        Patient GetPatient(string CardSerialNumber);
        PatientVisit GetPatientLastVisit(int patientId);
        List<PatientVisit> GetAllPatientVisits(int patientId);

        List<PatientAlert> GetPatientAlerts(int moduleId, int patientId);

        ResultKeyValue ExecutePatientAlert(PatientAlert alert, int patientId);
    }
}
