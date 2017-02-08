using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Enrollment;

namespace Interface.CCC.Visit
{
    public interface IPatientIdentifierManager
    {
        int AddPatientIdentifier(PatientEntityIdentifier patientIdentifier);
        int UpdatePatientIdentifier(PatientEntityIdentifier patientIdentifier);
        int DeletePatientIdentifier(int id);
        List<PatientEntityIdentifier> GetPatientIdentifiers(int patientId);
        List<PatientEntityIdentifier> GetPatientServicePatientIdentifiers(int serviceId, int personId);
    }
}
