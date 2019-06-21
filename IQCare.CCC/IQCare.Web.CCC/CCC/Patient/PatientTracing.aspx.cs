using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using System.Data;
using System.Globalization;
using Entities.CCC.Encounter;
using Entities.CCC.Screening;
using IQCare.CCC.UILogic.Encounter;
using IQCare.CCC.UILogic.Screening;
using Application.Presentation;


namespace IQCare.Web.CCC.Patient
{
    public partial class PatientTracing : System.Web.UI.Page
    {
        public int PatientId;
        protected void Page_Load(object sender, EventArgs e)
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            //tracing method
            List<LookupItemView> tracingmethods = mgr.GetLookItemByGroup("TracingMethod");
            if (tracingmethods != null && tracingmethods.Count > 0)
            {
                tracingmethod.Items.Add(new ListItem("select", "0"));
                foreach (var k in tracingmethods)
                {
                    tracingmethod.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
                tracingmethod.SelectedIndex = 0;
            }

            //tracing outcome
            List<LookupItemView> tracingOutcomes = mgr.GetLookItemByGroup("TracingOutcome");
            if (tracingOutcomes != null && tracingOutcomes.Count > 0)
            {
                tracingoutcome.Items.Add(new ListItem("select", "0"));
                foreach (var k in tracingOutcomes)
                {
                    tracingoutcome.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
                tracingoutcome.SelectedIndex = 0;
            }
        }
    }
}