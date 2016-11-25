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
using Interface.Reports;
using Application.Common;
using Application.Presentation;
namespace IQCare.Web.Reports
{
    public partial class NigeriaMonthlyNACAReport : System.Web.UI.Page
    {
        private void Init_Page()
        {

        }

        private void Page_Load()
        {

            if (Page.IsPostBack != true)
            {
                //(Master.FindControl("lblRoot") as Label).Text = "Reports >>";
                ////(Master.FindControl("lblMark") as Label).Text = "»";
                //(Master.FindControl("lblMark") as Label).Visible = false;
                //(Master.FindControl("lblheader") as Label).Text = "Donor Reports »Nigeria-Monthly NACA Report ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Donor Reports »Nigeria-Monthly NACA Report";

                txtDateOrderedFrom.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'4')");
                txtDateOrderedFrom.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'4')");
                txtDateOrderedTo.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'4')");
                txtDateOrderedTo.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'4')");

            }



        }


        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            if (txtDateOrderedFrom.Value == "")
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Date Ordered From";
                IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                return;
            }
            if (txtDateOrderedTo.Value == "")
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Date Ordered To";
                IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                return;
            }



            DateTime theDateOrderedFrom = Convert.ToDateTime("01-" + txtDateOrderedFrom.Value);
            DateTime theDateOrderedTo = Convert.ToDateTime("01-" + txtDateOrderedTo.Value);

            if (theDateOrderedFrom > theDateOrderedTo)
            {
                IQCareMsgBox.Show("StartEndDate", this);
                txtDateOrderedTo.Focus();
                return;

            }
            if (theDateOrderedFrom > Convert.ToDateTime(Application["AppCurrentDate"]))
            {

                IQCareMsgBox.Show("OrderedFromDate", this);
                txtDateOrderedTo.Focus();
                return;
            }

            if (theDateOrderedTo > Convert.ToDateTime(Application["AppCurrentDate"]))
            {
                IQCareMsgBox.Show("OrderedToDate", this);
                txtDateOrderedTo.Focus();
                return;
            }
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            DataTable dtMonthlyNACAReportData = ReportDetails.GetMonthlyNACAReportData(theDateOrderedFrom, theDateOrderedTo, Convert.ToInt32(Session["AppLocationId"]));
            dtMonthlyNACAReportData.WriteXmlSchema(Server.MapPath("..\\XMLFiles\\Nigeria-Monthly NACA Report (IQCare data).xml"));
            ReportDetails = null;
            Session["dtMonthlyNACAReportData"] = dtMonthlyNACAReportData;
            string theReportName = "Nigeria-Monthly NACA Report (IQCare data)";
            string theUrl = string.Format("{0}ReportName={1}&StartDate={2}&EndDate={3}", "frmReportViewerARV.aspx?", theReportName, theDateOrderedFrom, theDateOrderedTo);
            Response.Redirect(theUrl);
            //IQWebUtils theUtl = new IQWebUtils();
            //theUtl.ExporttoExcel(dtMonthlyNACAReportData, Response);

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("frmReportDonorJump.aspx");

        }
    }

}