using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Enrollment;

namespace Interface.CCC.Enrollment
{
    public interface IIdentifiersManager
    {
        List<Identifier> GetIdentifiersById(int identifierId);
        List<Identifier> GetAllIdentifiers();
        Identifier GetIdentifierByCode(string code);

        Identifier GetIdentifierByName(string name);
    }
}
