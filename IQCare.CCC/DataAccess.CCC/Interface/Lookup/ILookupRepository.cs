using System;
using System.Collections.Generic;
using System.Linq;

using DataAccess.Context;

using Entities.CCC.Lookup;

namespace DataAccess.CCC.Interface.Lookup
{
    public interface ILookupRepository:IRepository<LookupItemView>
    {
        List<LookupItemView> GetLookupItemViews(string listGroup);
        List<LookupItemView> FindBy(Func<LookupItemView, bool> p);
        LookupItemView GetPatientGender(int genderID);

    }
}
