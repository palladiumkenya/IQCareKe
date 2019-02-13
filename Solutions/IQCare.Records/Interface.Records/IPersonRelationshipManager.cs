using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records
{
    public interface IPersonRelationshipManager
    {
        int AddPersonRelationship(PersonRelationship personRelationship);
        int UpdatePersonRelationship(PersonRelationship personRelationship);
        int DeletePersonRelationship(int id);
        List<PersonRelationship> GetAllPersonRelationship(int patientId);
        bool PersonLinkedToPatient(int personId, int patientId);
    }
}
