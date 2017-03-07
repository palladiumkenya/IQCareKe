using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Enrollment;

namespace Interface.CCC.Enrollment
{
    public interface IPatientEnrollmentManager
    {
        int AddPatientEnrollment(PatientEntityEnrollment patientEnrollment);
        int UpdatePatientEnrollment(PatientEntityEnrollment patientEnrollment);
        int DeletePatientEnrollment(int id);
        List<PatientEntityEnrollment> GetPatientEnrollmentByPatientId(int patientId);
    }
}
