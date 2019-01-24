using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using Entities.Common;
namespace DataAccess.Records.Interface
{
   public interface IPersonRepository:IRepository<Person>
    {
    }
}
