using Entities.CCC.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.CCC.Patient
{
    public interface IPatientManager
    {
        int AddPatient(Entities.CCC.Enrollment.PatientEntity patient);
        int UpdatePatient(Entities.CCC.Enrollment.PatientEntity patient, int id);
        int DeletePatient(int id);
        PatientEntity GetPatient(int id);
        List<PatientEntity> CheckPersonEnrolled(int persionId);
        int GetPatientType(int patientId);
        int GetPatientIdByPersonId(int personId);
    }
}
