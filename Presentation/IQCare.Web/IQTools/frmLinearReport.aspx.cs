using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using System.Xml;
using Application.Presentation;
using Interface.IQToolsReports;
using IQCare.Web.UILogic;

namespace IQCare.Web.IQTools
{
    public partial class frmLinearReport : System.Web.UI.Page
    {
        /// <summary>
        /// The no data flag
        /// </summary>
        bool noData = false;
        /// <summary>
        /// The error flag
        /// </summary>
        bool isError = false;
        IReportIQQuery theQueries = (IReportIQQuery)ObjectFactory.CreateInstance("BusinessProcess.IQReports.IQToolsQueries, BusinessProcess.IQReports");
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
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

                if (NeedRefresh())
                {
                    this.RegenerateQueryFiles();
                }
                this.PopulateData();
            }
        }
        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.pnlNoData.Visible = this.noData;
            this.divError.Visible = this.isError;
            btnExport.Visible = (this.gridResult.Rows.Count > 0);
            this.isError = false;

        }
        /// <summary>
        /// Needs the refresh.
        /// </summary>
        /// <returns></returns>
        bool NeedRefresh()
        {
            string filePath = ConfigurationManager.AppSettings.Get("IQToolsQueryFilePath");
            string fullfilePath = Server.MapPath("~\\" + filePath).ToString();
            System.IO.FileInfo theFile = new System.IO.FileInfo(fullfilePath);
            int refreshHours = 24;
            int.TryParse(ConfigurationManager.AppSettings.Get("IQToolsFileRefreshIntervalHours"), out refreshHours);
            if (theFile != null)
            {
                DateTime expectedNextWrite = theFile.LastWriteTime.Date.AddHours(refreshHours);
                if (expectedNextWrite.Date <= DateTime.Now.Date)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.gridResult.DataBound += new EventHandler(gridResult_DataBound);

        }
        /// <summary>
        /// Gets the query document.
        /// </summary>
        /// <value>
        /// The query document.
        /// </value>
        XmlDocument QueryDocument
        {
            get
            {

                string filePath = ConfigurationManager.AppSettings.Get("IQToolsQueryFilePath");
                string fullfilePath = Server.MapPath("~\\" + filePath).ToString();
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.Load(fullfilePath);
                return document;
            }
        }
        /// <summary>
        /// Populates the data.
        /// </summary>
        private void PopulateData()
        {

            System.Xml.XmlDocument document = this.QueryDocument;
            XmlElement documentElement = document.DocumentElement;
            XmlNodeList nodeList = documentElement.SelectNodes("//Root/Category/SubCategory");

            ddlReport.Items.Clear();
            ddlReport.Items.Add(new ListItem("Select...", "XX"));

            ddlCategory.Items.Clear();
            ddlCategory.Items.Add(new ListItem("Select...", "XX"));
            foreach (XmlNode node in nodeList)
            {
                string categoryName = node.Attributes["Name"].Value.ToString();
                ddlCategory.Items.Add(new ListItem(categoryName, categoryName));
            }

        }
        /// <summary>
        /// Handles the DataBound event of the gridResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void gridResult_DataBound(object sender, EventArgs e)
        {
            foreach (BoundColumn column in this.gridResult.Columns)
            {
                column.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;
            }


        }
        /// <summary>
        /// Regenerates the query files.
        /// </summary>
        void RegenerateQueryFiles()
        {
            try
            { //IQTools Clinical queries

                // IReportIQQuery theQueries = (IReportIQQuery)ObjectFactory.CreateInstance("BusinessProcess.IQReports.IQToolsQueries, BusinessProcess.IQReports");
                string queryRep = theQueries.GetFullQueryDocument("Clinical");

                string filePath = ConfigurationManager.AppSettings.Get("IQToolsQueryFilePath");
                string fullfilePath = Server.MapPath("~\\" + filePath).ToString();
                System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                xmlDoc.LoadXml(queryRep);
                xmlDoc.Save(fullfilePath);
            }
            catch (Exception ex)
            {
                lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team  :" + ex.Message;
                this.isError = this.divError.Visible = true;
                SystemSetting.LogError(ex);
                //Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                //logger.LogError(ex);
            }
        }
        /// <summary>
        /// Handles the Click event of the btnGenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnGenerate_Click(object sender, EventArgs e)
        {

            if (ddlReport.SelectedValue == "XX") return;
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
            if (isError) return;
            try
            {
                DateTime dateFrom = Convert.ToDateTime(textDateFrom.Text);
                DateTime dateTo = Convert.ToDateTime(textDateTo.Text);
                int queryID = int.Parse(ddlReport.SelectedValue);
                int cd4Baseline = int.Parse(hCutOffCD4.Value);
                int cd4CutOff = (textCD4.Text == "") ? cd4Baseline : int.Parse(textCD4.Text);

                DataTable theDT = theQueries.ExecQuery(queryID, dateFrom, dateTo, cd4CutOff);


                if (theDT.Rows.Count == 0)
                {
                    this.noData = this.pnlNoData.Visible = true;
                    base.Session.Remove("IQToolsQbReport");
                    gridResult.DataSource = theDT;
                    gridResult.DataBind();
                }
                else
                {
                    gridResult.DataSource = theDT;
                    gridResult.DataBind();
                    base.Session["IQToolsQbReport"] = theDT;
                    this.pnlNoData.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team  :" + ex.Message;
                this.isError = this.divError.Visible = true;
                SystemSetting.LogError(ex);
                //Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                //logger.LogError(ex);
            }

        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlCategory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = ddlCategory.SelectedValue;
            base.Session.Remove("IQToolsQbReport");
            gridResult.DataSource = "";
            gridResult.DataBind();
            if (category != "XX")
            {
                System.Xml.XmlDocument document = this.QueryDocument;
                XmlElement documentElement = document.DocumentElement;
                XmlNodeList queryList = documentElement.SelectNodes("//Root/Category/SubCategory[@Name ='" + category + "']/Query");
                ddlReport.Items.Clear();
                ddlReport.Items.Add(new ListItem("Select...", "XX"));

                foreach (XmlNode query in queryList)
                {
                    string queryID = query["Query_ID"].InnerText;
                    string queryDescription = query["Description"].InnerText;
                    ddlReport.Items.Add(new ListItem(queryDescription, queryID));
                }
            }

        }

        /// <summary>
        /// Handles the Click event of the btnExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                IQWebUtils Util = new IQWebUtils();
                Util.ExporttoExcel((DataTable)base.Session["IQToolsQbReport"], Response);
            }
            catch (Exception ex)
            {

                this.showErrorMessage(ref ex);
            }
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
        /// Handles the Click event of the btrRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btrRefresh_Click(object sender, EventArgs e)
        {
            this.RegenerateQueryFiles();
        }
    }
}