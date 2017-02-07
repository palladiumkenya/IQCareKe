using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Entities.Common;

namespace Interface.CCC
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
