using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Enrollment;

namespace Interface.CCC.Visit
{
    public interface IPatientIdentifierManager
    {
        int AddPatientIdentifier(PatientIdentifier patientIdentifier);
        int UpdatePatientIdentifier(PatientIdentifier patientIdentifier);
        int DeletePatientIdentifier(int id);
        List<PatientIdentifier> GetPatientIdentifiers(int patientId);
        List<PatientIdentifier> GetPatientServicePatientIdentifiers(int serviceId, int personId);
    }
}
