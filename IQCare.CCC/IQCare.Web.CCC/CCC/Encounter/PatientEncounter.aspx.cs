using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;


namespace IQCare.Web.CCC.Encounter
{
    public partial class PatientEncounter : System.Web.UI.Page
    {
        public int PatientId;
        public int visitId = 0;
        public int PatientMasterVisitId;
        public int UserId;

        private readonly ILookupManager _lookupManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");
        private readonly IPatientLookupmanager _patientLookupmanager = (IPatientLookupmanager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BPatientLookupManager, BusinessProcess.CCC");
        private readonly ILookupManager _lookupItemManager = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

        protected void Page_Load(object sender, EventArgs e)
        {

            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);
            PatientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);
            //UserId = Convert.ToInt32(HttpContext.Current.Session["UserId"]);

            if (!IsPostBack)
            {
                LookupLogic lookUp = new LookupLogic();
                lookUp.populateDDL(orderReason, "LabOrderReason");

            }

            ILookupManager mgr = (ILookupManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.BLookupManager, BusinessProcess.CCC");

            List<LookupItemView> statuses = mgr.GetLookItemByGroup("AppointmentStatus");
            if (statuses != null && statuses.Count > 0)
            {
                status.Items.Add(new ListItem("select", "0"));
                foreach (var k in statuses)
                {
                    status.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
                status.SelectedIndex = 1;
                status.Enabled = false;
            }

            List<LookupItemView> areas = mgr.GetLookItemByGroup("ServiceArea");
            if (areas != null && areas.Count > 0)
            {
                ServiceArea.Items.Add(new ListItem("select", "0"));
                foreach (var k in areas)
                {
                    ServiceArea.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
            }

            List<LookupItemView> reasons = mgr.GetLookItemByGroup("AppointmentReason");
            if (reasons != null && reasons.Count > 0)
            {
                Reason.Items.Add(new ListItem("select", "0"));
                foreach (var k in reasons)
                {
                    Reason.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
            }

            List<LookupItemView> care = mgr.GetLookItemByGroup("DifferentiatedCare");
            if (care != null && care.Count > 0)
            {
                DifferentiatedCare.Items.Add(new ListItem("select", "0"));
                foreach (var k in care)
                {
                    DifferentiatedCare.Items.Add(new ListItem(k.ItemDisplayName, k.ItemId.ToString()));
                }
            }

        }

    }

}