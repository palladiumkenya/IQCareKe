using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using IQCare.CCC.UILogic;

namespace IQCare.Web.CCC.OneTimeEvents
{
    public partial class FamilyTesting : System.Web.UI.Page
    {
        protected string Gender
        {
            get { return Session["Gender"].ToString(); }
        }

        public int PatientId;
        public int PatientMasterVisitId;
        public int UserId;
        public DateTime PatientDateOfBirth;
        public int PatientAge;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
                this.GetSessionDetails();

                List<LookupItemView> relationship = mgr.GetLookItemByGroup("Relationship");
                if (relationship != null && relationship.Count > 0)
                {
                    Relationship.Items.Add(new ListItem("select", ""));
                    relationshipMod.Items.Add(new ListItem("select", ""));
                    foreach (var k in relationship)
                    {
                        Relationship.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                        relationshipMod.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                    }
                }

                List<LookupItemView> sex = mgr.GetLookItemByGroup("Gender");
                if (sex != null && sex.Count > 0)
                {
                    Sex.Items.Add(new ListItem("select", ""));
                    sexMod.Items.Add(new ListItem("select", ""));
                    foreach (var k in sex)
                    {
                        Sex.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                        sexMod.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                    }
                }

                List<LookupItemView> baseline = mgr.GetLookItemByGroup("BaseLineHivStatus");
                if (baseline != null && baseline.Count > 0)
                {
                    BaselineHIVStatus.Items.Add(new ListItem("select", ""));
                    bHivStatusMod.Items.Add(new ListItem("select", ""));
                    foreach (var k in baseline)
                    {
                        BaselineHIVStatus.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                        bHivStatusMod.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                    }
                }

                List<LookupItemView> hivtesting = mgr.GetLookItemByGroup("HivTestingResult");
                if (hivtesting != null && hivtesting.Count > 0)
                {
                    hivtestingresult.Items.Add(new ListItem("select", ""));
                    testingStatusMod.Items.Add(new ListItem("select", ""));
                    foreach (var k in hivtesting)
                    {
                        hivtestingresult.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                        testingStatusMod.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                    }
                }

                /*List<LookupItemView> YesNo = mgr.GetLookItemByGroup("YesNo");
                if (YesNo != null && YesNo.Count > 0)
                {
                    RegisteredInClinic.Items.Add(new ListItem("select", ""));
                    foreach (var item in YesNo)
                    {
                        RegisteredInClinic.Items.Add(new ListItem(item.ItemName, item.ItemId.ToString()));
                    }
                }*/
            }
        }

        private void GetSessionDetails()
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);
            UserId = Convert.ToInt32(HttpContext.Current.Session["AppUserId"]);
            PatientAge = Convert.ToInt32(HttpContext.Current.Session["Age"]);
            PatientDateOfBirth = Convert.ToDateTime(HttpContext.Current.Session["DateOfBirth"]).Date;
        }

        [WebMethod(EnableSession = true)]
        public static void SetEnrollmentSession(int personId)
        {
            HttpContext.Current.Session["PersonId"] = personId;
            LookupLogic logic = new LookupLogic();
            var items = logic.GetItemIdByGroupAndItemName("PatientType", "New");
            if (items.Count > 0)
            {
                HttpContext.Current.Session["PatientType"] = items[0].ItemId;
            }          
        }
    }
}