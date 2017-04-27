using IQCare.CCC.UILogic;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.CCC.Lookup;
using Interface.CCC.Lookup;
using IQCare.Web.Laboratory;

namespace IQCare.Web.CCC.Encounter
{
    public partial class PatientEncounter : System.Web.UI.Page
    {
        public int PatientId;       
        public int PatientMasterVisitId;
        public int UserId;     
        public int patientId;              
        public string Msg { get; set; }
        public int Result { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
          
            PatientId = Convert.ToInt32(HttpContext.Current.Session["patientId"]);
            PatientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);
            UserId = Convert.ToInt32(HttpContext.Current.Session["AppUserId"]);           
           

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