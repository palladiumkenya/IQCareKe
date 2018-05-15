using DataAccess.Context;
using Entities.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Interface
{
    public interface ILookupFacility : IRepository<LookupFacility>
    {
        LookupFacility GetFacility();
        List<LookupFacility> FindBy(Func<LookupFacility, bool> p);
        // LookupFacility Findby(int facilityId);
    }
}
