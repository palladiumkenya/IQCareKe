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
        List<PatientEntityIdentifier> GetPatientEntityIdentifiers(int patientId, int patientEnrollmentId, int identifierTypeId);
        List<PatientEntityIdentifier> CheckIfIdentifierNumberIsUsed(string identifierValue, int identifierTypeId);
        List<PatientEntityIdentifier> GetPatientEntityIdentifiersByPatientId(int patientId, int identifierTypeId);
        List<PatientEntityIdentifier> GetEntityIdentifiersByPatientIdEnrollmentId(int patientId,
            int patientEnrollmentId);
        List<PatientEntityIdentifier> GetAllPatientEntityIdentifiers(int patientId);
        PatientEntityIdentifier GetPatientByCardSerialNumber(string cardSerialNumber);
    }
}
