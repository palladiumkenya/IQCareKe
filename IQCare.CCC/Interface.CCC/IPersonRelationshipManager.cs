using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.Common;

namespace Interface.CCC
{
    public interface IPersonRelationshipManager
    {
        void AddPersonRelationship(PersonRelationship personRelationship);
        void UpdatePersonRelationship(PersonRelationship personRelationship);
        void DeletePersonRelationship(int id);
        List<PersonRelationship> GetAllPersonRelationship(int personId);

    }
}
