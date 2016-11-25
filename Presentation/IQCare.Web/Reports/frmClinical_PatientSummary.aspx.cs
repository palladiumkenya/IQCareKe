using System;
using System.Data;
using Application.Presentation;
using CrystalDecisions.CrystalReports.Engine;
using Interface.Clinical;
namespace IQCare.Web.Reports
{
    public partial class frmClinical_PatientSummary : System.Web.UI.Page
    {
        private ReportDocument rptDocument;
        private string theReportSource = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            setReport();
        }

        private void setReport()
        {
            rptDocument = new ReportDocument();
            IQCareUtils theUtil = new IQCareUtils();
            IPatientHome ReportDetails = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome,BusinessProcess.Clinical");
            DataSet theDS = (DataSet)ReportDetails.GetPatientSummaryInformation(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["TechnicalAreaId"]));

            Session["dsPatientClinicalsummary"] = theDS;
            // theDS.WriteXmlSchema(Server.MapPath("..\\XMLFiles\\PatientClinicalSummary.xml"));
            ReportDetails = null;

            theReportSource = "rptPatientClinicalSummary.rpt";
            rptDocument.Load(Server.MapPath(theReportSource));
            rptDocument.SetDataSource(theDS);
            crViewer.ReportSource = rptDocument;
            crViewer.EnableDatabaseLogonPrompt = false;
            crViewer.EnableParameterPrompt = false;

            crViewer.HasToggleGroupTreeButton = false;
            crViewer.DataBind();

            //crViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
        }
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            this.UnLoadReport();
        }
        void UnLoadReport()
        {
            try
            {
                rptDocument.Dispose();
                this.rptDocument = null;
            }
            catch { }
        }
    }
}