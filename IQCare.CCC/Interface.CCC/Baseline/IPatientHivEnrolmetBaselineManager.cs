using System.Collections.Generic;
using Entities.CCC.Baseline;

namespace Interface.CCC.Baseline
{
    public interface IPatientHivEnrolmetBaselineManager
    {
        int AddPatientHivEnrollment(PatientHivEnrollmentBaseline patientHivEnrollmentBaseline);
        int UpdatePatientHivEnrollment(PatientHivEnrollmentBaseline patientHivEnrollmentBaseline);
        int DeletePatientHivEnrollment(int id);
        List<PatientHivEnrollmentBaseline> GetPatientHivEnrollmentBaselines(int patientId);
    }
}
