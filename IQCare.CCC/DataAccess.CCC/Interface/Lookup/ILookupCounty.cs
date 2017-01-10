using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess.Context;
using Entities.CCC.Lookup;

namespace DataAccess.CCC.Interface.Lookup
{
   public interface ILookupCounty :IRepository<LookupCounty>
   {
       List<LookupCounty> GetCounties();
       List<LookupCounty> GetSubCounties(string county);
       List<LookupCounty> GetWardsList(string subcounty);
   }
}
