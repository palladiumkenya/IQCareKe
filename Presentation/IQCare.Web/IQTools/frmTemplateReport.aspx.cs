using System;
using System.Web;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.IQToolsReports;
using System.Data;
using IQCare.Web.UILogic;

namespace IQCare.Web.IQTools
{
    public partial class frmTemplateReport : System.Web.UI.Page
    {
        /// <summary>
        /// The no data flag
        /// </summary>
       // bool noData = false;
        /// <summary>
        /// The error flag
        /// </summary>
        bool isError = false;
        /// <summary>
        /// The reports
        /// </summary>
        IReportIQTools theReports = (IReportIQTools)ObjectFactory.CreateInstance("BusinessProcess.IQReports.IQToolsReport, BusinessProcess.IQReports");
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            //Ajax.Utility.RegisterTypeForAjax(typeof(Reports_frmIQToolsReports));
            Session["PatientId"] = 0;
            //(Master.FindControl("lblRoot") as Label).Text = "Reports >>";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "QueryBuilder Reports";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "IQTools Reports";
            if (!IsPostBack)
            {
                DateTime today = DateTime.Today;
                DateTime endOfMonth = new DateTime(today.Year, today.Month, 1).AddMonths(1).AddDays(-1);
                ceEndDate.SelectedDate = endOfMonth;
                textDateTo.Text = endOfMonth.ToString("dd-MMM-yyyy");

                DateTime startOfMonth = new DateTime(today.Year, today.Month, 1);
                ceStartDate.SelectedDate = startOfMonth;
                textDateFrom.Text = startOfMonth.ToString("dd-MMM-yyyy");
                this.PopulateData();


            }
        }

        /// <summary>
        /// Populates the data.
        /// </summary>
        private void PopulateData()
        {

            DataTable reports = theReports.GetReports();
            gridResult.DataSource = reports;
            gridResult.DataBind();

        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            divError.Visible = isError;

        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            //this.rptCategory.ItemCommand += new RepeaterCommandEventHandler(rptCategory_ItemCommand);
            //this.gridResult.
            this.gridResult.DataBinding += new EventHandler(gridResult_DataBinding);
            this.gridResult.DataBound += new EventHandler(gridResult_DataBound);
            //this.Form.Enctype = "multipart/form-data";
            this.btnActionOK.Click += new EventHandler(btnActionOK_Click);

        }


        /// <summary>
        /// Handles the DataBound event of the gridResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void gridResult_DataBound(object sender, EventArgs e)
        {
            //foreach (BoundColumn column in this.gridResult.Columns.OfType<BoundColumn>())
            //{

            //    column.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
            //}


        }
        /// <summary>
        /// Handles the DataBinding event of the gridResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void gridResult_DataBinding(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The executable.</param>
        void showErrorMessage(ref Exception ex)
        {
            lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team";
            this.isError = this.divError.Visible = true;
            //Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
            //logger.LogError(ex);
            SystemSetting.LogError(ex);

        }

        /// <summary>
        /// Handles the Click event of the XSLButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void XSLButton_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the cmdUpload control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.Exception">
        /// No file has been delivered to the server.
        /// or
        /// Filesize is zero.
        /// or
        /// No filename has been specified.
        /// or
        /// Wrong file format has been specified.
        /// </exception>
        protected void cmdUpload_Click(object sender, EventArgs e)
        {
            HttpPostedFile hFile = null;
            try
            {
                if (ctrlFileUpload.PostedFile == null) throw new Exception("No file has been delivered to the server.");

                hFile = ctrlFileUpload.PostedFile;
                string pFileName;
                string pFileNameNoExtension;
                string pFileExt;
                byte[] fileData;


                int pFileLength = hFile.ContentLength;
                string pContentType = hFile.ContentType;

                if (pFileLength == 0) throw new Exception("Filesize is zero.");
                if (hFile.FileName == String.Empty) throw new Exception("No filename has been specified.");

                pFileName = System.IO.Path.GetFileName(hFile.FileName);
                pFileNameNoExtension = System.IO.Path.GetFileNameWithoutExtension(hFile.FileName);
                pFileExt = System.IO.Path.GetExtension(pFileName).Replace(".", "").ToUpper();
                if (pFileExt != "XSL") throw new Exception("Wrong file format has been specified.");
                fileData = new byte[pFileLength];


                hFile.InputStream.Read(fileData, 0, pFileLength);
                int reportID = Convert.ToInt16(HReport_ID.Value);

                theReports.UpdateReportXsl(reportID, fileData, pFileNameNoExtension, pFileExt, pContentType, pFileLength);
                this.PopulateData();
                divData.Visible = false;
                HReport_ID.Value = "";
            }

            catch (Exception Err)
            {
                lblError.Text = Err.Message;
                isError = true;
            }

        }

        /// <summary>
        /// Handles the Click event of the cmdTFConfirmDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void cmdTFConfirmDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (HReport_ID.Value == String.Empty)
                {
                    throw new Exception("Template was not found.");
                }

                theReports.UpdateReportXsl(int.Parse(HReport_ID.Value), null, "", "", "", 0);

                //parForceRender = true;
                //HDataMode.Value = "LIST";
                this.PopulateData();
            }
            catch (Exception Err)
            {
                lblError.Text = Err.Message;
                isError = true;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the gridResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void gridResult_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the RowCommand event of the gridResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                divData.Visible = true;

                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = (gridResult.Rows[index]);
                gridResult.SelectedIndex = index;

                HReport_ID.Value = gridResult.SelectedDataKey.Value.ToString();
                int reportId = Convert.ToInt16(HReport_ID.Value);

                txtreportName.Text = row.Cells[1].Text;
                pnlFileUpload.Visible = true;
                bool hasTemplate = row.Cells[2].Text.Trim().ToLower().Equals("yes");

                if (hasTemplate)
                    DocumentTemplateLink.Text = gridResult.SelectedDataKey.Values[1].ToString();

                DocumentTemplateLink.Visible = hasTemplate;
                lblNoTemplateFile.Visible = !hasTemplate;
                cmdTFileDelete.Visible = hasTemplate;
                panelAllowPrint.Visible = hasTemplate;

            }
            else if (e.CommandName == "RUN")
            {
                int index = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = (gridResult.Rows[index]);
                gridResult.SelectedIndex = index;

                HReport_ID.Value = gridResult.SelectedDataKey.Value.ToString();
                int reportId = Convert.ToInt16(HReport_ID.Value);
                textDateTo.Text = textDateFrom.Text = "";
                textCD4.Text = hCutOffCD4.Value;
                this.parameterPopup.Show();
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the gridResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                bool hasTemplate = row["HasTemplate"].ToString().Trim().ToLower().Equals("yes");
                if (!hasTemplate)
                {
                    Button button = e.Row.FindControl("buttonRun") as Button;
                    if (button != null)
                        button.Enabled = false;
                }
            }
        }
        /// <summary>
        /// Handles the Click event of the btnActionOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void btnActionOK_Click(object sender, EventArgs e)
        {
            divData.Visible = false;
            if (textDateFrom.Text == "")
            {
                lblError.Text = "Start Date is required";
                this.isError = this.divError.Visible = true;
                this.isError = true;
            }
            if (textDateTo.Text == "")
            {
                lblError.Text = "End Date is required";
                this.isError = this.divError.Visible = true;

            }
            if (isError) parameterPopup.Hide();

            DateTime dateFrom = Convert.ToDateTime(textDateFrom.Text);
            DateTime dateTo = Convert.ToDateTime(textDateTo.Text);

            int reportId = Convert.ToInt16(HReport_ID.Value);

            base.Session["PRINT_REPORT_ID"] = reportId;
            base.Session["REPOR_DATE_RANGE_FROM"] = dateFrom;
            base.Session["REPOR_DATE_RANGE_TO"] = dateTo;

            int cd4Baseline = int.Parse(hCutOffCD4.Value);
            int cd4CutOff = (textCD4.Text == "") ? cd4Baseline : int.Parse(textCD4.Text);

            base.Session["REPORT_CD4_CUTOFF"] = cd4CutOff;
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "RunReport", "PrintReport();", true);
        }

        /// <summary>
        /// Handles the Click event of the btrRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btrRefresh_Click(object sender, EventArgs e)
        {
            theReports.ImportReportsFromIQTools(false);
            this.PopulateData();
        }

        /// <summary>
        /// Handles the Click event of the DocumentTemplateLink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void DocumentTemplateLink_Click(object sender, EventArgs e)
        {
            theReports.GetReportbyID(int.Parse(HReport_ID.Value));
            string strTemplate = theReports.ReportXslTemplate;
            byte[] toBytes = System.Text.Encoding.UTF8.GetBytes(strTemplate);
            IQWebUtils Util = new IQWebUtils();
            Util.ExportDocument(toBytes, theReports.ReportTemplateContentType, theReports.ReportTemplateFileName, Response);
        }

        /// <summary>
        /// Handles the Click event of the buttonReportXML control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonReportXML_Click(object sender, EventArgs e)
        {
            //  theReports.GetReportbyID(int.Parse(HReport_ID.Value));
            DateTime dateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime dateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);
            int cd4Baseline = int.Parse(hCutOffCD4.Value);
            // int cd4CutOff = (textCD4.Text == "") ? cd4Baseline : int.Parse(textCD4.Text);

            //DataTable theDT = theQueries.ExecQuery(queryID, dateFrom, dateTo, cd4CutOff);
            theReports.RunReport(int.Parse(HReport_ID.Value), dateFrom, dateTo, cd4Baseline);
            string strTemplate = theReports.GetReportData();
            byte[] toBytes = System.Text.Encoding.UTF8.GetBytes(strTemplate);
            IQWebUtils Util = new IQWebUtils();
            Util.ExportDocument(toBytes, "text/xml", theReports.ReportName + ".xml", Response);
        }

        protected void buttonClose_Click(object sender, EventArgs e)
        {
            divData.Visible = false;
        }
    }
}