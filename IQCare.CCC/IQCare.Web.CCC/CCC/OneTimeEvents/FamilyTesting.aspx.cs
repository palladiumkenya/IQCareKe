using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;

namespace IQCare.Web.CCC.OneTimeEvents
{
    public partial class FamilyTesting : System.Web.UI.Page
    {
        public int PatientId;
        public int PatientMasterVisitId;
        protected void Page_Load(object sender, EventArgs e)
        {
            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
            this.GetSessionDetails();

            List<LookupItemView> relationship = mgr.GetLookItemByGroup("Relationship");
            if (relationship != null && relationship.Count > 0)
            {
                Relationship.Items.Add(new ListItem("select", "0"));
                relationshipMod.Items.Add(new ListItem("select", "0"));
                foreach (var k in relationship)
                {
                    Relationship.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                    relationshipMod.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
            }

            List<LookupItemView> sex = mgr.GetLookItemByGroup("Gender");
            if (sex != null && sex.Count > 0)
            {
                Sex.Items.Add(new ListItem("select", "0"));
                sexMod.Items.Add(new ListItem("select", "0"));
                foreach (var k in sex)
                {
                    Sex.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                    sexMod.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
            }

            List<LookupItemView> baseline = mgr.GetLookItemByGroup("BaseLineHivStatus");
            if (baseline != null && baseline.Count > 0)
            {
                BaselineHIVStatus.Items.Add(new ListItem("select", "0"));
                bHivStatusMod.Items.Add(new ListItem("select", "0"));
                foreach (var k in baseline)
                {
                    BaselineHIVStatus.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                    bHivStatusMod.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
            }

            List<LookupItemView> hivtesting = mgr.GetLookItemByGroup("HivTestingResult");
            if (hivtesting != null && hivtesting.Count > 0)
            {
                hivtestingresult.Items.Add(new ListItem("select", "0"));
                testingStatusMod.Items.Add(new ListItem("select", "0"));
                foreach (var k in hivtesting)
                {
                    hivtestingresult.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                    testingStatusMod.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
            }
        }

        private void GetSessionDetails()
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);
            PatientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);
        }
    }
}