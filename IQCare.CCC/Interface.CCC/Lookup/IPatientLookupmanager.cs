using System;
using System.Collections.Generic;
using Entities.CCC.Lookup;

namespace Interface.CCC.Lookup
{
    public interface IPatientLookupmanager
    {
        List<PatientLookup> GetPatientSearchPayload();
        List<PatientLookup> GetPatientDetailsLookup(int id);
        List<PatientLookup> GetPatientSearchPayloadWithParameter(string patientId, string fname, string mname, string lname, DateTime doB, int sex, int facility,int start,int length);
        int GetTotalpatientCount();
        PatientLookup GetGender(int patientId);
    }
}
 