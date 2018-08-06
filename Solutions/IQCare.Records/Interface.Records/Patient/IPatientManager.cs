using Entities.Records;
using Entities.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records.Patient
{
    public interface IPatientManager
    {
        int AddPatient(Entities.Records.Enrollment.PatientEntity patient);
        int UpdatePatient(Entities.Records.Enrollment.PatientEntity patient, int id);
        int DeletePatient(int id);
        PatientEntity GetPatient(int id);
        List<PatientEntity> CheckPersonEnrolled(int persionId);
        int GetPatientType(int patientId);
        List<PatientRegistrationLookup> GetPatientIdByPersonId(int personId);
        void UpdatePatientType(int PatientId, int PatientType);
    }
}
