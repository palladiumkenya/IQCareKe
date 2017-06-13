using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Entities.CCC.Enrollment;

namespace Interface.CCC.Enrollment
{
    public interface IServiceAreaIdentifiers
    {
        List<ServiceAreaIdentifiers> GetIdentifiersByServiceArea(int serviceArea);
    }
}
