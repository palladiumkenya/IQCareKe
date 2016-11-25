using System;
using System.Configuration;
using System.Data;
using System.IO;
using Application.Presentation;
using CrystalDecisions.CrystalReports.Engine;
using Interface.Billing;
using Entities.Billing;
using CrystalDecisions.Shared;

namespace IQCare.Web.Billing
{
    public partial class Reciept : System.Web.UI.Page
    {
        ReportDocument rptDocument;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
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
        /// Gets a value indicating whether [print on thermal].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [print on thermal]; otherwise, <c>false</c>.
        /// </value>
        bool PrintOnThermal
        {
            get
            {
                string printOnthermal = ConfigurationManager.AppSettings.Get("ReceiptOnThermal").ToLower();
                if (printOnthermal != null)
                    return printOnthermal.Equals("true");
                else return false;
            }
        }

        /// <summary>
        /// Init_pages this instance.
        /// </summary>
        private void init_page()
        {

            //ReceiptType defaultType = ReceiptType.BillPayment;
            string reportfile = "rptBillingRecieptThermal.rpt";
            if (Request.QueryString.Count > 0)
            {

                if (this.Request.QueryString["ReceiptTrxCode"] != null)
                {
                    string receiptNumber = this.Request.QueryString["ReceiptTrxCode"].ToString();
                    IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
                    Receipt receipt = bMgr.GetReceipt(receiptNumber);
                    switch (receipt.ReceiptType)
                    {
                        case ReceiptType.BillPayment:
                            reportfile = "rptBillingRecieptThermal.rpt";
                            break;
                        case ReceiptType.BillPaymentReversal:
                            reportfile = "ReversalReceipt.rpt";
                            break;
                        case ReceiptType.DepositRefund:
                        case ReceiptType.NewDeposit:
                            reportfile = "DepositReceipt.rpt";
                            break;

                    }
                    DataSet receiptData = new DataSet("Receipt");
                    using (System.IO.TextReader txR = new System.IO.StringReader(receipt.ReceiptData))
                    {

                        receiptData.ReadXml(txR);
                        txR.Close();
                    }
                    //    receiptData.WriteXml(Server.MapPath(string.Format("~\\Billing\\{0}.xml",receipt.ReceiptNumber)));
                    this.rptDocument = new ReportDocument();


                    DataTable theDT = receiptData.Tables["Transaction"];

                    rptDocument.Load(MapPath(string.Format("~\\Billing\\Reports\\{0}", reportfile)));
                    String facilityName = (String)theDT.Rows[0]["FacilityName"];

                    String dupl;
                    if (Request.QueryString["reprint"] != null && Request.QueryString["RePrint"] == "true")
                        dupl = "Copy";
                    else
                        dupl = "";
                    // dupl = (receipt.PrintCount > 1) ? "DUPLICATE" : "";

                    rptDocument.SetDataSource(receiptData);
                    rptDocument.SetParameterValue("FacilityName", facilityName);
                    rptDocument.SetParameterValue("Currency", "KES");
                    rptDocument.SetParameterValue("DuplicateReceipt", dupl);
                    if (!this.PrintOnThermal)
                    {
                        String facilityLogo = (String)theDT.Rows[0]["FacilityLogo"];
                        string f = GblIQCare.GetPath() + facilityLogo;
                        string p = Server.MapPath("~/Images/" + facilityLogo);
                        rptDocument.SetParameterValue("PicturePath", p);
                    }

                    //byte[] pdfContent = null;
                    //using (MemoryStream ms = (MemoryStream)rptDocument.ExportToStream(ExportFormatType.PortableDocFormat))
                    //{
                    //    pdfContent = ms.ToArray();
                    //}
                    //create a temp file name for our PDF report...
                    //string fileName = Session["ReceiptNumber"].ToString() + ".pdf";

                    //Create a PrintFile object with the pdf report
                    // PrintFile file = new PrintFile(pdfContent, fileName);

                    billingRptViewer.EnableParameterPrompt = false;
                    billingRptViewer.ReportSource = rptDocument;
                    billingRptViewer.DataBind();
                }
                this.divError.Visible = false;
            }


        }
        /// <summary>
        /// Gets the logo.
        /// </summary>
        /// <param name="facilityLogo">The facility logo.</param>
        /// <returns></returns>
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
