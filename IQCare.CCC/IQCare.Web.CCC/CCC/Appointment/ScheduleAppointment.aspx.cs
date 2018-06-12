using Application.Presentation;
using Entities.CCC.Lookup;
using Entities.CCC.Visit;
using Interface.CCC.Lookup;
using Interface.CCC.Visit;
using IQCare.Web.UILogic;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Entities.CCC.Appointment;
using Interface.CCC;
using IQCare.CCC.UILogic;
using System.Data;

namespace IQCare.Web.CCC.Appointment
{
    public partial class ScheduleAppointment : System.Web.UI.Page
    {
        public int PatientId;
        public int PatientMasterVisitId;
        public int UserId;
        public int AppointmentId;
        public string UpdateAppointmentDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.GetSessionDetails();

            IQCareMsgBox.Show("Yes", AppointmentDate);

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

            if (Request.QueryString["appointmentid"] != null)
            {
                AutoFillAppointments(Convert.ToInt32(Request.QueryString["appointmentid"]));
                AppointmentId = Convert.ToInt32(Request.QueryString["appointmentid"]);
            }

        }

        private void GetSessionDetails()
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientPK"]);
            PatientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);
            UserId = SessionManager.UserId;
        }

        private void AutoFillAppointments(int AppointmentId)
        {
            PatientAppointmentManager appointmentmgr = new PatientAppointmentManager();
            PatientAppointment pa = appointmentmgr.GetPatientAppointment(AppointmentId);
            AppointmentDate.Text = pa.AppointmentDate.ToString("dd-MMM-yyy");
            //AppointmentDate.Text = "";
            ServiceArea.SelectedValue = pa.ServiceAreaId.ToString();
            Reason.SelectedValue = pa.ReasonId.ToString();
            DifferentiatedCare.SelectedValue = pa.DifferentiatedCareId.ToString();
            description.Text = pa.Description.ToString();
            status.SelectedValue = pa.StatusId.ToString();
            UpdateAppointmentDate = pa.AppointmentDate.ToString("dd-MMM-yyy");
        }
    }
}