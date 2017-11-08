using System.Collections.Generic;
using Entities.CCC.Enrollment;

namespace Interface.CCC.Enrollment
{
    public interface IPersonIdentifierManager
    {
        int AddPersonIdentifier(PersonIdentifier personIdentifier);
        List<PersonIdentifier> GetPersonIdenfiers(int personId, int identifierId);
    }
}
