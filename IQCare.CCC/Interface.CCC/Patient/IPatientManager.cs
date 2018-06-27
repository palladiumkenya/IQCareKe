using Entities.CCC.Enrollment;
using Entities.CCC.Lookup;
using System.Collections.Generic;

namespace Interface.CCC.Patient
{
    public interface IPatientManager
    {
        int AddPatient(Entities.CCC.Enrollment.PatientEntity patient);
        int UpdatePatient(Entities.CCC.Enrollment.PatientEntity patient, int id);
        int DeletePatient(int id);
        PatientPersonViewEntity GetPatient(int id);
        PatientPersonViewEntity CheckPersonEnrolled(int personId);
        PatientPersonViewEntity GetPatientEntityByPersonId(int persionId);
        int GetPatientType(int patientId);
        int GetPersonId(int patientId);
        List<PatientRegistrationLookup> GetPatientIdByPersonId(int personId);
        List<PatientRegistrationLookup> GetPatientByPtn_Pk(int ptn_pk);
        void UpdatePatientType(int PatientId, int PatientType);
    }
}
