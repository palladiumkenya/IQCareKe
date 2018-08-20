using Entities.Records.Enrollment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records.Enrollment
{
   public interface IIdentifierManager
    {
        List<Identifier> GetIdentifiersById(int identifierId);
        List<Identifier> GetAllIdentifiers();
        Identifier GetIdentifierByCode(string code);
        List<Identifier> GetMultipleIdentifierByCode(string code);
    }
}
