using Application.Presentation;
using Entities.CCC.Visit;
using Interface.CCC.Visit;
using Interface.Scheduler;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace IQCare.Web.CCC.Appointment
{
    public partial class ScheduleAppointment : System.Web.UI.Page
    {
        public int PatientId;
        public int PatientMasterVisitId;
        private IPatientMasterVisitManager _visitManager = (IPatientMasterVisitManager)ObjectFactory.CreateInstance("BusinessProcess.CCC.visit.BPatientmasterVisit, BusinessProcess.CCC");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PopulateDropDown();
                this.GetSessionDetails();
            }
        }

        private void GetSessionDetails()
        {
            PatientId = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);
            PatientMasterVisitId = Convert.ToInt32(HttpContext.Current.Session["PatientMasterVisitId"]);
            if (PatientMasterVisitId == 0)
            {
                PatientMasterVisit visit = new PatientMasterVisit()
                {
                    PatientId = PatientId,
                    Start = DateTime.Now,
                    Active = true,
                };
                PatientMasterVisitId = _visitManager.AddPatientmasterVisit(visit);
            }
        }

        private void PopulateDropDown()
        {
            //*******Get the patient details on the basis of Patient Enrollment Id and show the details.*******//
            var formManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
            var theDtSet = formManager.GetAppointmentStatus();

            BindFunctions appBind = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            appBind.BindCombo(status, theDtSet.Tables[0], "Name", "Id", "Id");
            ListItem item = status.Items.FindByText("Pending");
            if (item != null)
            {
                item.Selected = true;
            }
            else
            {
                status.ClearSelection();
            }

            DataSet theDtSetPurpose = formManager.GetAppointmentReasons(0);

            DataView theDv = new DataView(theDtSetPurpose.Tables[0]);
            theDv.RowFilter = "DeleteFlag=0";
            DataTable TheDT = (DataTable)theUtils.CreateTableFromDataView(theDv);
            appBind.BindCombo(Reason, TheDT, "Name", "Id");
            theDv.Dispose();
            TheDT.Clear();

            appBind = new BindFunctions();
            DataTable dt = (((DataTable)Session["AppModule"]).DefaultView).ToTable(true, "ModuleName", "ModuleId");
            dt.DefaultView.RowFilter = "ModuleName Not In ('PM/SCM')";
            appBind.BindCombo(ServiceArea, dt, "ModuleName", "ModuleId", "ModuleName");

            ServiceArea.ClearSelection();
        }
    }
}