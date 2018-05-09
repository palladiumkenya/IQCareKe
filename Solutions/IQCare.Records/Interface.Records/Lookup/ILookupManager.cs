using Entities.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface.Records
{
   public  interface ILookupManager
    {

        List<LookupItemView> GetGenderOptions();
        List<LookupItemView> GetLookItemByGroup(string groupname);
        List<LookupItemView> GetLookItemByGroup(string groupname, string anotherGroupname);
        List<LookupCounty> GetLookupCounties();
        List<LookupCounty> GetLookupSubcounty(string county);
        List<LookupCounty> GetLookupWards(string subcounty);
        string GetCountyByCountyId(int countyId);
        string GetCountyNameBySubCountyId(int subCountyId);
        string GetWardNameByWardId(int wardId);

        string GetLookupItemId(string lookupItemName);
        LookupCounty GetCountyDetailsByWardName(string wardName);

        List<LookupItemView> GetItemIdByGroupAndItemName(string groupName, string itemName);



    }
}
