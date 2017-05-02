using System;
using System.Collections.Generic;
using Entities.CCC.Lookup;
using Entities.CCC.Visit;

namespace Interface.CCC.Lookup
{
    public interface IPatientLookupmanager
    {
        List<PatientLookup> GetPatientSearchPayload();
        PatientLookup GetPatientDetailsLookup(int id);
        List<PatientLookup> GetPatientByPersonId(int personId);
        List<PatientLookup> GetPatientSearchPayloadWithParameter(string patientId, string fname, string mname, string lname, DateTime doB, int sex, int facility,int start,int length);
        int GetTotalpatientCount();
        PatientLookup GetGenderID(int patientId);
       int GetPatientTypeId(int PatientId);       

    }
}
 