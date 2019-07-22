using System;
using System.Collections.Generic;
using Entities.CCC.Baseline;

namespace Interface.CCC.Baseline
{
    public interface IPatientHivDiagnosisManager
    {
        int AddPatientHivDiagnosis(PatientHivDiagnosis patientHivDiagnosis);
        int UpdatePatientHivDiagnosis(PatientHivDiagnosis patientHivDiagnosis);
        void UpdateBlueCardBaseline(int? ptn_pk, DateTime dateOfHivDiagnosis, DateTime? artInitiationDate, DateTime? dateOfEnrollment, int locationId, int whostage);
        int DeletePatientHivDiagnosis(int id);
        List<PatientHivDiagnosis> GetPatientHivDiagnosis(int patientId);
        int CheckIfDiagnosisExists(int patientId);
    }
}