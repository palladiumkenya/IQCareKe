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
       int AddPersonLocation(PersonLocation location);
       int UpdatePersonLocation(PersonLocation location);
       int DeletePersonLocation(int id);
       List<PersonLocation> GetCurrentPersonLocation(int persoId);
       List<PersonLocation> GetPersonLocationAll(int personId);
   }
}
