using System.Collections.Generic;
using Entities.CCC.IL;

namespace Interface.CCC.Interoperability
{
    public interface IIlMessengerManager
    {
       IEnumerable<IlMessengerLog>  GetIlMessengerLog(string options);
    }
}