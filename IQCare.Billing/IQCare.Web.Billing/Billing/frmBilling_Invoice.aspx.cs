using System;
using System.Data;
using Application.Common;
using Application.Presentation;
using CrystalDecisions.CrystalReports.Engine;
using Interface.Billing;
using IQCare.Web.UILogic;

namespace IQCare.Web.Billing
{
    /// <summary>
    /// 
    /// </summary>
   
    public partial class Invoice : System.Web.UI.Page
    {
        int PatientId
        {
            get
            {
                return CurrentSession.Current.CurrentPatient.Id;
            }
        }
        /// <summary>
        /// The RPT document
        /// </summary>
        ReportDocument rptDocument;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //AuthenticationManager Authentication = new AuthenticationManager();
                CurrentSession session = CurrentSession.Current;
                
                if (session == null)
                {
                    string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                    //Response.Redirect(theUrl);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
                if (this.PatientId == 0)
                {
                    string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                    //Response.Redirect(theUrl);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
                if ( !session.HasFeaturePermission(ApplicationAccess.BillingFeature.ClientBilling))
                {
                    string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                    //Response.Redirect(theUrl);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
            }
            //if (WebClientPrint.ProcessPrintJob(Request))
            //{
            init_page();
            //}

        }
       
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Unload" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            this.UnLoadReport();
        }
        /// <summary>
        /// Gets or sets the un load report.
        /// </summary>
        /// <value>
        /// The un load report.
        /// </value>
        void UnLoadReport()
        {
            try
            {
                rptDocument.Dispose();
                this.rptDocument = null;
            }
            catch { }
        }


        /// <summary>
        /// Init_pages this instance.
        /// </summary>
        private void init_page()
        {

            IBilling BManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
            this.rptDocument = new ReportDocument();
            DataSet theDataSet;
            theDataSet = BManager.GetInvoice(Convert.ToInt32(Request.QueryString["BillRefCode"]), Convert.ToInt32(Session["AppLocationId"]), this.PatientId);
            theDataSet.Tables[0].TableName = "Command_3";
            theDataSet.Tables[1].TableName = "Command_2";
            theDataSet.Tables[2].TableName = "Command_1";
            theDataSet.Tables[3].TableName = "Command";

            rptDocument.Load(MapPath("..\\Billing\\Reports\\rptInvoice.rpt"));


            rptDocument.SetDataSource(theDataSet);


            rptDocument.SetParameterValue("Currency", "KES");
            rptDocument.SetParameterValue("userName", Session["AppUserName"]);
            String facilityName = (String)theDataSet.Tables[1].Rows[0]["FacilityName"];

            rptDocument.SetParameterValue("FacilityName", facilityName);
            String facilityLogo = (String)theDataSet.Tables[1].Rows[0]["FacilityLogo"];
            rptDocument.SetParameterValue("PicturePath", GblIQCare.GetPath() + facilityLogo);


            billingRptViewer.EnableParameterPrompt = false;
            billingRptViewer.ReportSource = rptDocument;
            billingRptViewer.HasToggleGroupTreeButton = false;
            billingRptViewer.DataBind();
        }

        /// <summary>
        /// Handles the Unload event of the billingRptViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void billingRptViewer_Unload(object sender, EventArgs e)
        {
            this.UnLoadReport();
        }
    }
}