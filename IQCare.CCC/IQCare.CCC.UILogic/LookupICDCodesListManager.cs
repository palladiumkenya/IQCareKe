using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IQCare.CCC.UILogic
{
   public class LookupICDCodesListManager
    {
        private ILookupICDCodesManager mgr =
            (ILookupICDCodesManager)ObjectFactory.CreateInstance(
                "BusinessProcess.CCC.Lookup.BLookupICDCodesManager, BusinessProcess.CCC");

        public List<ICDCodeList> GetICDCodeList()
        {
            try
            {
                return mgr.GetICDCodeList();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

    }
}
