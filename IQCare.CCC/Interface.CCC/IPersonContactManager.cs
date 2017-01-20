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
         void AddPersonContact(PersonContact p);
         void UpdatePersonContact(PersonContact p);
         void DeletePersonContact(int id);
         List<PersonContact> GetCurrentPersonContacts(int persoId);
         List<PersonContact> GetAllPersonContact(int personId);
    }
}
