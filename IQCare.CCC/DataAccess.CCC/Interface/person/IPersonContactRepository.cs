using System.Collections.Generic;
using DataAccess.Context;
using Entities.Common;

namespace DataAccess.CCC.Interface.person
{
    public interface IPersonContactRepository:IRepository<PersonContact>
    {
       List<PersonContact> GetCurrentPersonContact(int persoId);
       List<PersonContact> GetAllPersonContact(int personId);
    }
}
