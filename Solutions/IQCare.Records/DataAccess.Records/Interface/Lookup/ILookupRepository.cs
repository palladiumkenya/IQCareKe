using DataAccess.Context;
using Entities.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Interface
{
    public interface ILookupRepository : IRepository<LookupItemView>
    {
        List<LookupItemView> GetLookupItemViews(string listGroup);
        List<LookupItemView> FindBy(Func<LookupItemView, bool> p);
        LookupItemView GetPatientGender(int genderID);

    }
}
