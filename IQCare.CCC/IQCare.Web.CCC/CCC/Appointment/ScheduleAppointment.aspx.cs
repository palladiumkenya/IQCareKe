using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.Scheduler;
using IQCare.Web.UILogic;

namespace IQCare.Web.CCC.Appointment
{
    public partial class ScheduleAppointment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PopulateDropDown();
                //CurrentSession session = CurrentSession.Current;
                //Entities.PatientCore.Patient patient = session.CurrentPatient;
                //txtpatientId = patient.Id;
                //txtpatientMasterVisitId = session.

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