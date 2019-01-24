using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Common;
using DataAccess.Context;
namespace DataAccess.Records.Interface
{
    public interface IPersonContactRepository:IRepository<PersonContact>
    {
        List<PersonContact> GetCurrentPersonContact(int personId);
        List<PersonContact> GetAllPersonContact(int personId);
    }
}
