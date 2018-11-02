using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Common;

namespace Interface.CCC
{
    public interface IPersonRelationshipManager
    {
        int AddPersonRelationship(PersonRelationship personRelationship);
        int UpdatePersonRelationship(PersonRelationship personRelationship);
        int DeletePersonRelationship(int id);
        List<PersonRelationship> GetAllPersonRelationship(int patientId);
        bool PersonLinkedToPatient(int personId, int patientId);

        PersonRelationship GetSpecificRelationship(int personId, int patientId);
    }
}
