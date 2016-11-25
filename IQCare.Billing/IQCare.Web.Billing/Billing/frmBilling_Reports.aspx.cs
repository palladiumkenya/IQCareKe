using System;
using System.Collections;
using System.Data;
using System.IO;

using Application.Presentation;
using CrystalDecisions.CrystalReports.Engine;
using Interface.Billing;
using IQCare.Web.UILogic;
using Application.Common;

namespace IQCare.Web.Billing
{
    public partial class ViewReports : System.Web.UI.Page
    {
        ReportDocument rptDocument;
   
        /// <summary>
        /// Uns the load report.
        /// </summary>
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
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!CurrentSession.Current.HasFeaturePermission(ApplicationAccess.BillingFeature.Report))
            {
                CurrentSession.Logout();
                string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                //Response.Redirect(theUrl);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Redirect(theUrl, true);
            }
            init_page();
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
        /// Init_pages this instance.
        /// </summary>
        private void init_page()
        {
            IBilling BManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
            this.rptDocument = new ReportDocument();

            DateTime fromDate;
            DateTime.TryParse(Request.QueryString["sDt"], out fromDate);
            DateTime toDate;
            DateTime.TryParse(Request.QueryString["eDt"], out toDate);
            DataSet theDataSet;
            if (Request.QueryString["RptCd"] == "Invoice")
            {
                CurrentSession session = CurrentSession.Current;

                if (session == null)
                {
                    string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                    //Response.Redirect(theUrl);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
                theDataSet = BManager.GetInvoice(Convert.ToInt32(Request.QueryString["BillRefCode"]), Convert.ToInt32(Session["AppLocationId"]), session.CurrentPatient.Id);
                theDataSet.Tables[0].TableName = "Command_3";
                theDataSet.Tables[1].TableName = "Command_2";
                theDataSet.Tables[2].TableName = "Command_1";
                theDataSet.Tables[3].TableName = "Command";

                rptDocument.Load(MapPath("..\\Billing\\Reports\\rptInvoice.rpt"));


                rptDocument.SetDataSource(theDataSet);


                rptDocument.SetParameterValue("Currency", "KES");
                rptDocument.SetParameterValue("userName", Session["AppUserName"]);
                String facilityName = (String)theDataSet.Tables[1].Rows[0]["FacilityName"];

                //GblIQCare gl = new GblIQCare();
                
                rptDocument.SetParameterValue("FacilityName", facilityName);
                String facilityLogo = (String)theDataSet.Tables[1].Rows[0]["FacilityLogo"];
                rptDocument.SetParameterValue("PicturePath", GblIQCare.GetPath() + facilityLogo);


            }
            else
            {
                Hashtable ht = (Hashtable)Session["BREPORTS_XRFS"];
                string ReportQuery = ht["ReportQuery"].ToString();
                string TableNames = ht["TableNames"].ToString();
                bool hasPatientData = ht["PatientData"].ToString() == "TRUE";
                theDataSet = BManager.GetBillingReport(fromDate, toDate, Convert.ToInt32(Session["AppLocationId"]), ReportQuery, hasPatientData);
               rptDocument.Load(MapPath(String.Format("..\\Billing\\Reports\\{0}.rpt", Request.QueryString["RptNm"])));
                try
                {
                    string[] parts = TableNames.Split(',');
                    for (int i = 0; i < parts.Length; i++)
                    {
                        theDataSet.Tables[i].TableName = parts[i];
                    }
                }
                catch { }
                // theDataSet.Tables[0].TableName = "collectionsummary";
                //  theDataSet.Tables[1].TableName = "Facility";

                rptDocument.SetDataSource(theDataSet);

                rptDocument.SetParameterValue("StartDate", fromDate);
                rptDocument.SetParameterValue("EndDate", toDate);
                rptDocument.SetParameterValue("currentuser", Session["AppUserName"]);
                String facilityName = (String)theDataSet.Tables[1].Rows[0]["Name"];

                rptDocument.SetParameterValue("FacilityName", facilityName);

                String facilityLogo = (String)theDataSet.Tables[1].Rows[0]["logo"];
                rptDocument.SetParameterValue("PicturePath", GblIQCare.GetPath() + facilityLogo);
                //rptDocument.FileName = Request.QueryString["RptNm"];

            }
            



            Session["rptDoc"] = rptDocument;

            billingRptViewer.EnableDatabaseLogonPrompt = false;
            billingRptViewer.EnableParameterPrompt = false;
            billingRptViewer.ReportSource = rptDocument;
            billingRptViewer.HasToggleGroupTreeButton = false;
            billingRptViewer.DataBind();
            

        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            string theUrl = "~/Billing/frmBillingReportPage.aspx";


            Response.Redirect(theUrl, false);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            ReportDocument rptDocument = (ReportDocument)Session["rptDoc"];

            try
            {
                rptDocument.PrintToPrinter(1, false, 0, 0);
                string theUrl = "~/Billing/frmBillingReportPage.aspx";


                Response.Redirect(theUrl, false);
            }
            catch // (Exception ex)
            {
               // MessageBox.Show("Please configure printer first" + ex.Message, "Printing Error");
            }

            // rptDocument.PrintOptions.PrinterName = "doPDF v7";

        }
        private byte[] getLogo(String facilityLogo)
        {

            // define the filestream object to read the image
            FileStream fs = default(FileStream);
            // define te binary reader to read the bytes of image
            BinaryReader br = default(BinaryReader);
            // check the existance of image
            if (File.Exists(GblIQCare.GetPath() + facilityLogo))
            {
                // open image in file stream
                fs = new FileStream(GblIQCare.GetPath() + facilityLogo, FileMode.Open);

                // initialise the binary reader from file streamobject
                br = new BinaryReader(fs);
                // define the byte array of filelength
                byte[] imgbyte = new byte[fs.Length + 1];
                // read the bytes from the binary reader
                imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));

                br.Close();
                // close the binary reader
                fs.Close();
                // close the file stream
                return imgbyte;
            }
            return null;
        }

        /// <summary>
        /// Handles the Unload event of the billingRptViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void billingRptViewer_Unload(object sender, EventArgs e)
        {
            this.UnLoadReport();
        }
    }
}