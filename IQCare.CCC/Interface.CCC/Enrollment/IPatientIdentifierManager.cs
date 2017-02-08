using Entities.CCC.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.CCC.Enrollment
{
    public interface IPatientIdentifierManager
    {
        int AddPatientIdentifier(PatientEntityIdentifier patientIdentifier);
        int UpdatePatientIdentifier(PatientEntityIdentifier patientIdentifier);
        int DeletePatientIdentifier(int id);
    }
}
