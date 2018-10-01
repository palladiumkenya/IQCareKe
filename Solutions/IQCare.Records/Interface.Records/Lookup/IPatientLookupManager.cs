using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Records;
using DataAccess.Context;

namespace Interface.Records
{
    public interface IPatientLookupmanager
    {
        List<PatientLookup> GetPatientSearchPayload(string patientId, string firstName, string middleName, string lastName);
        List<PatientLookup> GetPatientSearchPayload();
        PatientLookup GetPatientDetailsLookup(int id);
        List<PatientLookup> GetPatientByPersonId(int personId);
        List<PatientLookup> GetPatientSearchPayloadWithParameter(string patientId, string fname, string mname, string lname, DateTime doB, int sex, int facility, int start, int length);
        int GetTotalpatientCount();
        PatientLookup GetGenderID(int patientId);
        int GetPatientTypeId(int PatientId);
        int GetPatientSexId(int patientId);
        List<PatientLookup> GetPatientListByParams(int patientId, string firstName, string middleName, string lastName, int sex);
        PatientLookup GetPatientByCccNumber(string cccNumber);
    }
}
