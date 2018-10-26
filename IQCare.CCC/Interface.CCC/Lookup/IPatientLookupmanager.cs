using Entities.CCC;
using Entities.CCC.Lookup;
using System;
using System.Collections.Generic;

namespace Interface.CCC.Lookup
{
    public interface IPatientLookupmanager
    {
        List<PatientLookup> GetPatientSearchPayload(string patientId, string isEnrolled, string firstName, string middleName, string lastName);
        List<PatientLookup> GetPatientSearchPayload(string isEnrolled);
        PatientLookup GetPatientDetailsLookup(int id);
        PatientLookup GetPatientDetailsLookupBrief(int patientId,int personId);
        PatientLookup GetPatientByPersonId(int personId);
        List<PatientLookup> GetPatientSearchPayloadWithParameter(string patientId, string fname, string mname, string lname, DateTime doB, int sex, int facility,int start,int length);
        int GetTotalpatientCount();
        PatientLookup GetGenderID(int patientId);
        int GetPatientTypeId(int PatientId);
        int GetPatientSexId(int patientId);
        List<PatientLookup> GetPatientListByParams(int patientId, string firstName, string middleName, string lastName, int sex);
        PatientLookup GetPatientByCccNumber(string cccNumber);
        PatientLookup GetPatientByNormalizedCccNumber(string normalizedCccNumber);

        List<PatientRelationshipDTO> GetPatientRelationshipView(int patientId);
        PersonExtLookup GetPersonExtLookups(int personId);
    }
}
 