using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Common;
namespace Interface.Records
{
  public  interface IPersonManager
    {
        int AddPerson(Person p);
        Person GetPerson(int id);
        void DeletePerson(int id);
        int UpdatePerson(Person p, int id);
    }
}
