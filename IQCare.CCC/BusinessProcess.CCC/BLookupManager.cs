using DataAccess.Base;
using Interface.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.CCC.Interface.Lookup;
using Entities.CCC.Lookup;
using DataAccess.CCC.Repository.Lookup;

namespace BusinessProcess.CCC
{
    public class BLookupManager : ProcessBase, ILookupManager
    {
        
        public List<LookupItemView> GetGenderOptions()
        {
            LookupRepository repo = new LookupRepository();
           return repo.GetLookupItemViews("Gender");
        }

        public List<LookupItemView> GetLookItemByGroup(string groupname)
        {
            LookupRepository repo = new LookupRepository();
            return repo.GetLookupItemViews(groupname);
        }

        public List<LookupCounty> GetLookupCounties()
        {
           LookupCountyRepository lookupCountyRepository =new  LookupCountyRepository();
            return lookupCountyRepository.GetCounties();
        }

        public List<LookupCounty> GetLookupSubcounty(string county)
        {
            LookupCountyRepository lookupCountyRepository=new LookupCountyRepository();
            return lookupCountyRepository.GetSubCounties(county);
        }

        public List<LookupCounty> GetLookupWards(string subcounty)
        {
            LookupCountyRepository lookupCountyRepository= new LookupCountyRepository();
            return lookupCountyRepository.GetWardsList(subcounty);
        }
    }
}
