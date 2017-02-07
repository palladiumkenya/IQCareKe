using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace IQCare.CCC.UILogic
{
    public class LookupLogic
    {
        public static string GeSubCountyListJson(string county)
        {
            string jsonObject = "[]";
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupCounty> lookupCounties = lookupManager.GetLookupSubcounty(county);

            if (lookupCounties != null && lookupCounties.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupCounties);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }

        public static string GetLookupWardListJson(string subcounty)
        {
            string jsonObject;
            ILookupManager lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
           List<LookupCounty> lookupwardsList = lookupManager.GetLookupWards(subcounty);

            if (lookupwardsList != null && lookupwardsList.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupwardsList);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }

        public void populateDDL(DropDownList ddl, string groupName)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            List<LookupItemView> vw = mgr.GetLookItemByGroup(groupName);
            ddl.Items.Add(new ListItem("Select", "0"));
            if (vw != null && vw.Count > 0)
            {
                foreach (var item in vw)
                {
                    ddl.Items.Add(new ListItem(item.ItemDisplayName, item.ItemId.ToString()));
                }
            }
        }

        public static string GetLookupItemByName(string itemName)
        {
            string jsonObject = "[]";
            ILookupManager lookupManager =
                (ILookupManager) ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager,BusinessProcess.CCC");
            List<LookupItemView> lookupItem = lookupManager.GetLookItemByGroup(itemName);
            if (lookupItem !=null && lookupItem.Count > 0)
            {
                jsonObject = new JavaScriptSerializer().Serialize(lookupItem);
            }
            else
            {
                jsonObject = "[]";
            }
            return jsonObject;
        }  
    }
}
