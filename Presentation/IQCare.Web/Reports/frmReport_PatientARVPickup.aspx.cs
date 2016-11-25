using System;
using System.Data;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Reports;
using IQCare.Web.UILogic;

namespace IQCare.Web.Reports
{

    public partial class PatientARVPickup : System.Web.UI.Page
    {
        IReports ReportDetails;

        #region "User Functions"
        private void Init_Page()
        {
            //IPatientHome PatientManager;
            //PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            //DataTable theDT = PatientManager.GetPatientVisitDetail(Convert.ToInt32(Request.QueryString["PatientId"]));
            if (CurrentSession.Current.CurrentPatient.Status.ToString() == "0")
            {
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "0";
            }
            else
            {
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "1";
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            chkAll.Visible = false;


            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Patient Reports >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "ARV Pick-up";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "ARV Pick-up";

            txtStartDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            txtStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtEndDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            txtEndDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");

            if (!IsPostBack)
            {
                Init_Page();
            }

            AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientARVPickup, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                btnSubmit.Enabled = false;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string thePatientId1 = Request.QueryString["PatientId"];

            Response.Redirect("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + thePatientId1);

        }

        protected void btnSubmitClick(object sender, EventArgs e)
        {
            CurrentSession session = CurrentSession.Current;
            string theReportName = "PatientARVPickup";
            string theStartDate = string.Empty;
            string theEndDate = string.Empty;
            string thePatientId = session.CurrentPatient.Id.ToString();
            string theSatelliteId = session.Facility.SatelliteId.ToString();
            string theCountryID = session.Facility.CountryId.ToString();
            string thePosId = session.Facility.MasterIndex.ToString();
            int LocationID;
            if (chkAll.Checked == true)
            {
                LocationID = 0;
            }
            else
            {
                LocationID = Convert.ToInt32(Session["AppLocationId"]);
            }
            int thePatientId2 = 0;

            if (FieldValidation() == false)
            {
                return;
            }

            IQCareUtils theUtil = new IQCareUtils();

            theStartDate = theUtil.MakeDate(txtStartDate.Text);
            theEndDate = theUtil.MakeDate(txtEndDate.Text);

            thePatientId2 = Convert.ToInt32(Request.QueryString["PatientId"]);


            ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");

            DataTable dtDrugARVPickup = new DataTable();
            if (chkAll.Checked == false)
            {
                dtDrugARVPickup = (DataTable)ReportDetails.GetDrugARVPickup(thePatientId2, Convert.ToDateTime(theStartDate), Convert.ToDateTime(theEndDate), theSatelliteId, theCountryID, thePosId, LocationID).Tables[0];
            }


            Session["dtDrugARVPickup"] = dtDrugARVPickup;
            ReportDetails = null;
            dtDrugARVPickup.WriteXmlSchema(Server.MapPath("..\\XMLFiles\\SinglePatientARVPickUP.xml"));

            if ((dtDrugARVPickup != null) && (dtDrugARVPickup.Rows.Count != 0))
            {

                string theUrl = string.Format("{0}ReportName={1}&StartDate={2}&EndDate={3}&PatientId={4}&SatelliteID={5}&CountryID={6}&PosID={7}", "frmReportViewer.aspx?", theReportName, theStartDate, theEndDate, thePatientId, theSatelliteId, theCountryID, thePosId);
                Response.Redirect(theUrl);
            }
            else
            {
                IQCareMsgBox.Show("NoData", this);
                txtStartDate.Focus();
            }

        }


        private Boolean FieldValidation()
        {

            if (txtStartDate.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Ordered from date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtStartDate.Focus();
                return false;
            }

            if (txtEndDate.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Ordered to date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtEndDate.Focus();
                return false;
            }

            if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
            {
                IQCareMsgBox.Show("StartEndDate", this);
                txtEndDate.Focus();
                return false;
            }

            return true;
        }
    }
}