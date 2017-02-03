using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.CCC.Patient
{
    public interface IPatientManager
    {
        int AddPatient(Entities.CCC.Enrollment.PatientEntity patient);
        int UpdatePatient(Entities.CCC.Enrollment.PatientEntity patient);
        int DeletePatient(int id);
        Entities.CCC.Enrollment.PatientEntity GetPatient(int id);
    }
}
