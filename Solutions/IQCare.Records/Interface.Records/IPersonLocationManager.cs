using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records
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
