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
    public partial class PatientConsent : System.Web.UI.Page
    {
        public int PatientId;
        public int PatientMasterVisitId;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetSessionDetails();

            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

            List<LookupItemView> statuses = mgr.GetLookItemByGroup("ConsentType");
            if (statuses != null && statuses.Count > 0)
            {
                ConsentType.Items.Add(new ListItem("select", "0"));
                foreach (var k in statuses)
                {
                    ConsentType.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
                ConsentType.SelectedIndex = 1;
            }
        }
        private void GetSessionDetails()
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);
        }
    }
}