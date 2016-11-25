using Application.Presentation;
using Interface.Reports;
using IQCare.Web.UILogic;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
namespace IQCare.Web.Reports
{
    public partial class frmQueryBuilderReports : System.Web.UI.Page
    {
        /// <summary>
        /// The no data flag
        /// </summary>
        bool noData = false;
        /// <summary>
        /// The error flag
        /// </summary>
        bool isError = false;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = 0;
            //(Master.FindControl("lblRoot") as Label).Text = "Reports >>";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "QueryBuilder Reports";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Reports";
            if (!IsPostBack)
            {

                //IReports theQBuilderReports = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
                //theQBuilderReports.RefreshCustomTables(Convert.ToString(Session["AppLocation"]));

                //  DataTable theDT = theQBuilderReports.GetReportsCategory();
                DataSet dataTest = new DataSet("QBReportList");
                dataTest.ReadXml(Server.MapPath("~\\XMLFiles\\QueryBuilderReports.con"));
                dataTest.AcceptChanges();
                var categoryList = dataTest.Tables["QueryBuilderReports"].AsEnumerable()
                     .Select(row =>
                         new
                         {
                             CategoryID = row["CategoryID"].ToString(),
                             CategoryName = row["CategoryName"].ToString()
                         }
                         ).Distinct().OrderBy(x => x.CategoryID);//.ToList();
                ddlCategory.Items.Clear();
                ddlCategory.Items.Add(new ListItem("Select...", "XX"));
                foreach (var cat in categoryList)
                {
                    ddlCategory.Items.Add(new ListItem(cat.CategoryName, cat.CategoryID));
                };


            }
        }
        /// <summary>
        /// Gets the reports by category unique identifier.
        /// </summary>
        /// <param name="categoryID">The category unique identifier.</param>
        void GetReportsByCategoryID(string categoryID)
        {
            DataSet dataTest = new DataSet("QBReportList");
            dataTest.ReadXml(Server.MapPath("~\\XMLFiles\\QueryBuilderReports.con"));
            dataTest.AcceptChanges();
            var reportList = dataTest.Tables["QueryBuilderReports"].AsEnumerable()
                .Where(c => c["CategoryID"].ToString() == categoryID)
                 .Select(row =>
                     new
                     {
                         ReportID = row["ReportID"].ToString(),
                         Description = row["ReportDescription"].ToString()
                     }
                     ).Distinct().OrderBy(x => x.Description);//.ToList();
            ddlReport.Items.Clear();
            ddlReport.Items.Add(new ListItem("Select...", "XX"));
            foreach (var rep in reportList)
            {
                ddlReport.Items.Add(new ListItem(rep.Description, rep.ReportID));
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
            this.btnActionOK.Click += new EventHandler(btnActionOK_Click);

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
        /// Handles the DataBinding event of the gridResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void gridResult_DataBinding(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// Handles the ItemCommand event of the rptCategory control.
        /// </summary>
        /// <param name="source">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterCommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void rptCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                int categoryID = int.Parse(e.CommandArgument.ToString());
                if (categoryID == 0)
                {
                    IQCareMsgBox.Show("QueryBuilder", this);
                    return;
                }
                IReports theQBuilderReports = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
                DataTable theDT = theQBuilderReports.GetCustomReports(categoryID);
                //BindFunctions theBind = new BindFunctions();
                ddlReport.DataValueField = "ReportQuery";
                ddlReport.DataTextField = "ReportName";
                ddlReport.DataSource = theDT;
                ddlReport.DataBind();
                ddlReport.Items.Insert(0, new ListItem("Select...", "0"));
                //  theBind.BindCombo(ddlCategory, theDT, "ReportName", "ReportQuery");
            }
            catch (Exception ex)
            {
                IQCareMsgBox.Show(ex.Message, this);
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlCategory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (ddlCategory.SelectedValue == "0")
            {
                dyanamicRadiobutton.Visible = false;
                IQCareMsgBox.Show("QueryBuilder", this);
            }
            else
            {
                AddDynamicRadioButton();
                dyanamicRadiobutton.Visible = true;

            }*/

            if (ddlCategory.SelectedValue == "XX") return;

            try
            {
                int categoryID = int.Parse(ddlCategory.SelectedValue);
                if (categoryID == 0)
                {
                    IQCareMsgBox.Show("QueryBuilder", this);
                    return;
                }
                this.GetReportsByCategoryID(categoryID.ToString());
                //IReports theQBuilderReports = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
                //DataTable theDT = theQBuilderReports.GetCustomReports(categoryID);
                ////BindFunctions theBind = new BindFunctions();
                //ddlReport.DataValueField = "ReportID";
                //ddlReport.DataTextField = "ReportDescription";
                //ddlReport.DataSource = theDT;
                //ddlReport.DataBind();
                //ddlReport.Items.Insert(0, new ListItem("Select...", "XX"));
            }
            catch (Exception ex)
            {

                this.showErrorMessage(ref ex);
            }




        }
        /// <summary>
        /// Adds the dynamic RadioButton.
        /// </summary>
        private void AddDynamicRadioButton()
        {
            //  IReports theQBuilderReports = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            //  DataTable theDT = theQBuilderReports.GetCustomReports(Convert.ToInt32(ddlCategory.SelectedValue));
            //  rdButtonList.Items.Clear();
            //  foreach (DataRow theDR in theDT.Rows)
            //{
            //    rdButtonList.Items.Add(new ListItem(theDR["ReportName"].ToString(), theDR["ReportQuery"].ToString()));

            //} 
        }
        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //if(rdButtonList.SelectedItem !=null)
            //{

            //             string theQuery = rdButtonList.SelectedItem.Value.ToString();
            //             IReports theQBuilderReports = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            //             DataTable theDT = theQBuilderReports.ReturnQueryResult(theQuery).Tables[0];
            //             if (theDT.Rows.Count > 0)
            //             {
            //                 IQWebUtils theUtils = new IQWebUtils();
            //                 string ReportName1 = rdButtonList.SelectedItem.Text;
            //                 string thePath = Server.MapPath("..\\ExcelFiles\\" + ReportName1 + ".xls");
            //                 string theTemplatePath = Server.MapPath("..\\ExcelFiles\\IQCareTemplate.xls");
            //                 theDT.TableName = ReportName1;
            //                 theUtils.ExporttoExcel(theDT, Response);

            //             }
            //}
            // else
            //{
            //    IQCareMsgBox.Show("SelectRadioButton", this);
            //}


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
                Util.ExporttoExcel((DataTable)base.Session["QbReport"], Response);
            }
            catch (Exception ex)
            {

                this.showErrorMessage(ref ex);
            }
            //Response.Redirect("~/frmFacilityHome.aspx");
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlReport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReport.SelectedValue == "0") return;

            string queryToExecute = ddlReport.SelectedValue;
            IReports theQBuilderReports = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            DataTable theDT = theQBuilderReports.ReturnQueryResult(queryToExecute).Tables[0];
            gridResult.DataSource = theDT;
            gridResult.DataBind();
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

        void GenerateReport(string queryToExecute, string tableName)
        {
            IReports theQBuilderReports = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");

            DataTable theDT = theQBuilderReports.ReturnQueryResult(queryToExecute).Tables[0];
            theDT.TableName = tableName;

            if (theDT.Rows.Count == 0)
            {
                this.noData = this.pnlNoData.Visible = true;
                base.Session.Remove("QBReport");
                gridResult.DataSource = theDT;
                gridResult.DataBind();
            }
            else
            {
                gridResult.DataSource = theDT;
                gridResult.DataBind();
                base.Session["QbReport"] = theDT;
                this.pnlNoData.Visible = false;
            }
        }

        SqlDbType GetSqlDBTypeFromstring(string paramType)
        {
            SqlDbType dbtype;
            switch (paramType.ToLower())
            {
                case "nvarchar":
                case "varchar":
                case "string":
                    dbtype = SqlDbType.VarChar;
                    break;
                case "int":
                case "int32":
                case "int64":
                case "int16":
                    dbtype = SqlDbType.Int;
                    break;
                case "datetime":
                    dbtype = SqlDbType.DateTime;
                    break;
                case "decimal":
                case "numeric":
                case "float":
                    dbtype = SqlDbType.Decimal;
                    break;
                default:
                    dbtype = SqlDbType.VarChar;
                    break;

            }
            return dbtype;
        }
        /// <summary>
        /// Handles the Click event of the btnActionOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void btnActionOK_Click(object sender, EventArgs e)
        {

            int l = gridParameter.Rows.Count;
            // SqlParameter[] p = new SqlParameter[l];

            //System.Collections.Hashtable paramTable = new System.Collections.Hashtable();
            //int i =0;
            System.Text.StringBuilder sbParams = new System.Text.StringBuilder("<parameters>");

            foreach (GridViewRow row in gridParameter.Rows)
            {
                sbParams.Append("<parameter>");
                TextBox t = row.FindControl("paramValue") as TextBox;
                string paramValue = t.Text.Trim();
                sbParams.Append("<value>" + paramValue + "</value>");
                string paramName = row.Cells[0].Text;
                sbParams.Append("<name>" + paramName + "</name>");
                string paramType = row.Cells[1].Text;
                sbParams.Append("<type>" + paramType + "</type>");
                sbParams.Append("</parameter>");
                
            }
            sbParams.Append("</parameters>");
            IReports theQBuilderReports = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");

            string queryToExecute = queryString.Value.Trim();


            DataTable theDT = theQBuilderReports.ReturnQueryResult(queryString.Value, sbParams.ToString()).Tables[0];
            theDT.TableName = thetableName.Value;

            if (theDT.Rows.Count == 0)
            {
                this.noData = this.pnlNoData.Visible = true;
                base.Session.Remove("QBReport");
                gridResult.DataSource = theDT;
                gridResult.DataBind();
            }
            else
            {
                gridResult.DataSource = theDT;
                gridResult.DataBind();
                base.Session["QbReport"] = theDT;
                this.pnlNoData.Visible = false;
            }
            thetableName.Value = queryString.Value = "";
        }

        /// <summary>
        /// Handles the Click event of the btnGenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ddlReport.SelectedValue == "XX") return;

            //string queryToExecute = ddlReport.SelectedValue;
            string queryToExecute;

            try
            {

                IReports theQBuilderReports = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
                DataTable dtQuery = theQBuilderReports.GetQueryBuilderReportQuery(ddlReport.SelectedValue);
                if (dtQuery.Rows.Count == 0)
                {
                    base.Session.Remove("QBReport");
                    return;
                };
                DataRow dataRow = dtQuery.Rows[0];
                string tableName = dataRow["ReportName"].ToString().Trim().Replace(" ", string.Empty);
                queryToExecute = dataRow["ReportQuery"].ToString().Trim();

                bool hasParameters = int.Parse(dataRow["HasParameters"].ToString()) > 0;

                if (hasParameters)
                {
                    DataTable dtParams = theQBuilderReports.GetQueryBuilderReportParameters(ddlReport.SelectedValue);
                    gridParameter.DataSource = dtParams;
                    gridParameter.DataBind();
                    queryString.Value = queryToExecute;
                    thetableName.Value = tableName;
                    parameterPopup.Show();
                    theQBuilderReports = null;
                    return;
                }

                this.GenerateReport(queryToExecute, tableName);
                theQBuilderReports = null;
                return;

                //DataTable theDT = theQBuilderReports.ReturnQueryResult(queryToExecute).Tables[0];
                //theDT.TableName = tableName;

                //if (theDT.Rows.Count == 0)
                //{
                //    this.noData = this.pnlNoData.Visible = true;
                //    base.Session.Remove("QBReport");
                //    gridResult.DataSource = theDT;
                //    gridResult.DataBind();
                //}
                //else
                //{
                //    gridResult.DataSource = theDT;
                //    gridResult.DataBind();
                //    base.Session["QbReport"] = theDT;
                //    this.pnlNoData.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }

        }
    }





}