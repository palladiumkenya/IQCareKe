using DataAccess.Context;
using Entities.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Records.Interface
{
   public interface ILookupCounty:IRepository<LookupCounty>
    {
        List<LookupCounty> GetCounties();
        List<LookupCounty> GetSubCounties(string county);
        List<LookupCounty> GetWardsList(string subcounty);
        List<LookupCounty> FindBy(Func<LookupCounty, bool> p);
        string GetCountyByCountyId(int countyId);
        string GetSubCountyNameBySubCountyId(int subCountyId);
        string GetWardNameByWardId(int wardId);
        LookupCounty GetCountyDetailsByWardName(string wardName);
    }
}
