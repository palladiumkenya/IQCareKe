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
    public partial class frmReportPatient : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Ashok Kr. Gupta
        // Written Date      : 05th Oct 2006
        // Modification Date : 
        // Description       : Patient Report Form
        //
        /// /////////////////////////////////////////////////////////////////

        #region "User Functions"
        private void Init_Page()
        {
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            rdoUpARVPickup.Checked = false;
            rdoMisARVPickup.Checked = false;
            rdoNewPatients.Checked = false;
            rdoPregnantFU.Checked = false;

        }

        private string Get_ReportName()
        {
            string theReportName = string.Empty;

            if (rdoUpARVPickup.Checked == true)
            {
                theReportName = rdoUpARVPickup.Value;

            }
            else if (rdoMisARVPickup.Checked == true)
            {
                theReportName = rdoMisARVPickup.Value;
            }
            else if (rdoNewPatients.Checked == true)
            {
                theReportName = rdoNewPatients.Value;
            }
            else if (rdoPregnantFU.Checked == true)
            {
                theReportName = rdoPregnantFU.Value;
            }
            else
            {
                theReportName = null;
            }

            return theReportName;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
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
            }
        }
        protected void btnView_Click(object sender, EventArgs e)
        {
            string theReportName = string.Empty;
            string theStartDate = string.Empty;
            string theEndDate = string.Empty;

            try
            {
                theReportName = Get_ReportName();
                theStartDate = txtStartDate.Text;
                theEndDate = txtEndDate.Text;

                string theUrl = string.Format("{0}ReportName={1}&StartDate={2}&EndDate={3}", "frmReportViewer.aspx?", theReportName, theStartDate, theEndDate);
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
            Init_Page();

        }

    }
}