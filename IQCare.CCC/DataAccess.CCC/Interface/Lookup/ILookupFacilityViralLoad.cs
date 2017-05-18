using DataAccess.Context;
using Entities.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.CCC.Interface.Lookup
{
   public interface ILookupFacilityViralLoad : IRepository<LookupFacilityViralLoad>
    {
        List<LookupFacilityViralLoad> GetFacilityVLSuppressed(int facilityId);
        List<LookupFacilityViralLoad> GetFacilityVLUnSuppressed(int facilityId);
    }
}
