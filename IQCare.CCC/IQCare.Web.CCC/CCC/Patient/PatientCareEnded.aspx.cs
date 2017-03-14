using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.Web.CCC.Patient
{
    public partial class PatientCareEnded : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ILookupManager mgr =
                    (ILookupManager)
                    ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
                List<LookupItemView> vw = mgr.GetLookItemByGroup("CareEnded");
                if (vw != null && vw.Count > 0)
                {
                    Reason.Items.Add(new ListItem("select", "0"));

                    foreach (var item in vw)
                    {
                        Reason.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                    }
                }
            }
        }
    }
}