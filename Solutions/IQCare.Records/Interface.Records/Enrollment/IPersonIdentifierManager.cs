using Entities.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records.Enrollment
{
    public interface IPersonIdentifierManager
    {
        int AddPersonIdentifier(PersonIdentifier personIdentifier);
        List<PersonIdentifier> GetPersonIdenfiers(int personId, int identifierId);

        PersonIdentifier GetCurrentPersonIdentifier(int personId, int identifierId);
       int UpdatePersondentifier(PersonIdentifier personIdentifier);
       int  DeletePersonIdentifier(int id);


       
    }
}
