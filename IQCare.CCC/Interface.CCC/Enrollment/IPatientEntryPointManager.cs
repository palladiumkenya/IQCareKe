using Entities.CCC.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.CCC.Enrollment
{
    public interface IPatientEntryPointManager
    {
        int AddPatientEntryPoint(PatientEntryPoint patientEntryPoint);
        int UpdatePatientEntryPoint(PatientEntryPoint patientEntryPoint);
        int DeletePatientEntryPoint(int id);
    }
}
