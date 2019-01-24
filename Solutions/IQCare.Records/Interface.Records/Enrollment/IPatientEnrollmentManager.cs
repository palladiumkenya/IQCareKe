using Entities.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records
{
    public interface IPatientEnrollmentManager
    {
        int AddPatientEnrollment(PatientEntityEnrollment patientEnrollment);
        int UpdatePatientEnrollment(PatientEntityEnrollment patientEnrollment);
        int DeletePatientEnrollment(int id);
        DateTime GetPatientEnrollmentDate(int patientId);
        List<PatientEntityEnrollment> GetPatientEnrollmentByPatientId(int patientId);
        PatientEntityEnrollment GetPatientEntityEnrollment(int id);
        List<PatientEntityEnrollment> GetPatientByPatientIdCareEnded(int patientId);
    }
}
