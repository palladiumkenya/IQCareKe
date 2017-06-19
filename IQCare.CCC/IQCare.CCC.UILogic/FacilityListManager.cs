using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.CCC.UILogic
{
    public class FacilityListManager
    {
        private IFacilityList mgr =
            (IFacilityList) ObjectFactory.CreateInstance(
                "BusinessProcess.CCC.Lookup.BFacilityListManager, BusinessProcess.CCC");

        public List<FacilityList> GetFacilitiesList()
        {
            try
            {
                return mgr.GetFacilitiesList();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
