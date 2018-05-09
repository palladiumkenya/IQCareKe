using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records
{
   public interface IPersonContactManager
    {

        int AddPersonContact(PersonContact p);
        int UpdatePersonContact(PersonContact p);
        int DeletePersonContact(int id);
        List<PersonContact> GetCurrentPersonContacts(int persoId);
        List<PersonContact> GetAllPersonContact(int personId);
    }
}
