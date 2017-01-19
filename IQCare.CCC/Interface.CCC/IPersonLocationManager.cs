using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Entities.Common;

namespace Interface.CCC
{
   public interface IPersonLocationManager
   {
       void AddPersonLocation(PersonLocation location);
       void UpdatePersonLocation(PersonLocation location);
       void DeletePersonLocation(int id);
       List<PersonLocation> GetCurrentPersonLocation(int persoId);
       List<PersonLocation> GetPersonLocationAll(int personId);
   }
}
