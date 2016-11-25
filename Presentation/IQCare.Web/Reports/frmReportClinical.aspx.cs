using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Application.Common;
using Application.Presentation;
namespace IQCare.Web.Reports
{
    public partial class frmReportClinical : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Ashok Kr. Gupta
        // Written Date      : 10th Oct 2006
        // Modification Date : 
        // Description       : Clinical Report Form
        //
        /// /////////////////////////////////////////////////////////////////

        #region "User Functions"
        private void Init_Page()
        {
            rdoARVAdherence.Checked = false;
        }

        //private string Get_ReportName()
        //{
        //    string theReportName = string.Empty;

        //    if (rdoARVAdherence.Checked == true)
        //    {
        //        theReportName = rdoARVAdherence.Value;

        //    }
        //    else if (rdoMisARVAppointment.Checked == true)
        //    {
        //        theReportName = rdoMisARVAppointment.Value;   
        //    }
        //    else
        //    {
        //        theReportName = null;
        //    }

        //    return theReportName;


        //}
        private Boolean FieldValidation()
        {
            if ((rdoARVAdherence.Checked == false) && (rdoMisARVAppointment.Checked == false))
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Please, select report.";
                IQCareMsgBox.Show("ReportErrorMessage", theBuilder, this);
                rdoARVAdherence.Focus();
                return false;
            }

            if (rdoARVAdherence.Checked == true)
            {

                if ((rdoAllClients.Checked == false) && (rdoSelectClient.Checked == false))
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Please, select All Patients/Select Patient.";
                    IQCareMsgBox.Show("ReportErrorMessage", theBuilder, this);
                    rdoAllClients.Focus();
                    return false;
                }
            }
            if (rdoMisARVAppointment.Checked == true)
            {

                if ((rdoOrdered.Checked == false) && (rdoDispensed.Checked == false))
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Please, select Ordered/Dispensed date.";
                    IQCareMsgBox.Show("ReportErrorMessage", theBuilder, this);

                    rdoOrdered.Focus();
                    return false;
                }
                else if ((rdoOrdered.Checked == true) || (rdoDispensed.Checked == true))
                {
                    if (txtStartDate.Text == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Ordered/Dispensed date";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtStartDate.Focus();
                        return false;
                    }
                }
            }




            return true;
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //(Master.FindControl("lblRoot") as Label).Text = "Facility Report >>";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "ARV Pick up Report" ;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Facility Report >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "ARV Pick up Report";

            //RTyagi. 20-Feb-07.
            AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientARVPickup, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                btnView.Enabled = false;
                btnReset.Enabled = false;

            }
            if (Page.IsPostBack != true)
            {
                Init_Page();
                txtStartDate.Attributes.Add("readonly", "true");
            }
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            string theReportName = string.Empty;
            string theUrl = string.Empty;

            if (FieldValidation() == false)
            {
                return;
            }


            try
            {
                //theReportName = Get_ReportName();
                if (rdoARVAdherence.Checked == true)
                {
                    theReportName = rdoARVAdherence.Value;
                    if (rdoAllClients.Checked == true)
                    {
                        theUrl = string.Format("{0}ReportName={1}&PatientId={2}", "frmReportViewer.aspx?", theReportName, "0");

                    }
                    else if (rdoSelectClient.Checked == true)
                    {
                        theUrl = string.Format("{0}ReportName={1}", "../frmFindAddPatient.aspx?", theReportName);

                    }

                }
                else if (rdoMisARVAppointment.Checked == true)
                {
                    string theDType = string.Empty;
                    string theStartDate = string.Empty;

                    if (txtStartDate.Text != "")
                    {
                        theStartDate = txtStartDate.Text;
                    }

                    theReportName = rdoMisARVAppointment.Value;
                    if (rdoOrdered.Checked == true)
                    {
                        theDType = rdoOrdered.Value;
                        theUrl = string.Format("{0}ReportName={1}&DType={2}&StartDate={3}", "frmReportViewerARV.aspx?", theReportName, theDType, theStartDate);

                    }
                    else if (rdoDispensed.Checked == true)
                    {
                        theDType = rdoDispensed.Value;

                        theUrl = string.Format("{0}ReportName={1}&DType={2}&StartDate={3}", "frmReportViewerARV.aspx?", theReportName, theDType, theStartDate);

                    }

                }



                Response.Redirect(theUrl);

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {

            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {


        }

    }
}