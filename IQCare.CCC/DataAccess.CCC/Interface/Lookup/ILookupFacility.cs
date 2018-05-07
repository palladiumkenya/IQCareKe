using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Interface.Lookup
{
    public interface ILookupFacility : IRepository<LookupFacility>
    {
        LookupFacility GetFacility();
        List<LookupFacility> FindBy(Func<LookupFacility, bool> p);
        // LookupFacility Findby(int facilityId);
        LookupFacility GetFacilityByMflCode(string mflCode);
    }
}
