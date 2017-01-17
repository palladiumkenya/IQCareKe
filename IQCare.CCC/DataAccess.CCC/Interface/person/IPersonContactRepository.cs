using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.Common;

namespace DataAccess.CCC.Interface.person
{
    public interface IPersonContactRepository:IRepository<PersonContact>
    {
       List<PersonContact> GetLatespersonContact(int persoId);
       List<PersonContact> GetAllPersonContact(int personId);
        
    }
}
