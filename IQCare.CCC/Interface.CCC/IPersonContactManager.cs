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
         void UpdatePersonContact(PersonContact p,int id);
         void VoidPersonContact(int personId,int id);
         List<PersonContact> GetLatespersonContact(int persoId);
         List<PersonContact> GetAllPersonContact(int personId);
    }
}
