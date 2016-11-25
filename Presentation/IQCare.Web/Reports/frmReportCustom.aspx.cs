using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Reports;
using Interface.Security;

namespace IQCare.Web.Reports
{
    public partial class frmReportCustom : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = 0;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Custom Reports";

            this.btnExportReport.Attributes.Add("onclick", "javascript:return CheckReport('" + this.ddTitle.ClientID + "')");
            this.btnRun.Attributes.Add("onclick", "javascript:return CheckReport('" + this.ddTitle.ClientID + "')");
            this.btnEdit.Attributes.Add("onclick", "javascript:return CheckReport('" + this.ddTitle.ClientID + "')");
            IQCareMsgBox.ShowConfirm("CustomRepRefresh", btnRefresh);

            //RTyagi..20-Feb-07.
            if (Page.IsPostBack != true)
            {
                AuthenticationManager Authentiaction = new AuthenticationManager();
                if (Authentiaction.HasFunctionRight(ApplicationAccess.CustomReports, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    btnEdit.Enabled = false;
                    btnRun.Enabled = false;
                    btnExportReport.Enabled = false;
                    btnEditImport.Enabled = false;
                }
               

                DataSet dsCategory;
                IReports CustomReport;
                CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                dsCategory = CustomReport.GetAllCategory();
                BindFunctions theBind = new BindFunctions();
                theBind.BindCombo(ddCategory, dsCategory.Tables[0], "CategoryName", "CategoryId");
                //ddCategory.Items.Insert(0, new ListItem("Select Category", ""));
                ddTitle.Items.Insert(0, new ListItem("Select Title", ""));

                //////
                string str = string.Empty;
                if (Request.QueryString["r"] != null)
                {
                    str = Request.QueryString["r"].ToString();
                }
                if (str != "no")
                {

                    string thescript = "<script language = 'javascript' defer ='defer' id = 'refreshCache'>\n";
                    thescript += "document.getElementById('" + btnRefresh.ClientID + "').click();\n";
                    thescript += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "refreshCache", thescript);
                }
                //return;
            }

        }


        //protected void lnkNewReport_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("frmReportCustomNew.aspx", true);
        //}

        protected void btnExportReport_Click(object sender, EventArgs e)
        {
            if (this.ddTitle.SelectedValue.ToString() != "")
            {
                DataSet dsExistingReport;
                IReports CustomReport;
                CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                dsExistingReport = CustomReport.GetCustomReportData(Convert.ToInt32(this.ddTitle.SelectedValue));
                Stream stream = new MemoryStream();
                dsExistingReport.WriteXml(stream, XmlWriteMode.WriteSchema);
                byte[] Buffer;

                Buffer = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(Buffer, 0, (int)stream.Length);
                stream.Close();

                Response.Clear();

                Response.ContentType = "application/xml";
                Response.AddHeader("content-disposition", "attachment; filename=Report.xml");
                Response.BinaryWrite(Buffer);

                Response.End();

               
            }
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.ddTitle.SelectedValue != "")
            {
                Response.Redirect("frmReportCustomNew.aspx?ReportId=" + this.ddTitle.SelectedValue.ToString(), true);
            }
        }

        protected void ddCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddCategory.SelectedIndex == 0)
            {
                ddTitle.Items.Clear();
                ddTitle.Items.Insert(0, new ListItem("Select Title", ""));
            }
            else if ((ddCategory.SelectedValue != "") && (ddCategory.SelectedIndex != 0))
            {
                DataSet dsReports;
                IReports CustomReport;
                CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                dsReports = CustomReport.GetReportList(Convert.ToInt32(ddCategory.SelectedValue));
                ddTitle.DataSource = dsReports.Tables[0];
                ddTitle.DataTextField = "ReportName";
                ddTitle.DataValueField = "ReportId";
                ddTitle.DataBind();
                ddTitle.Items.Insert(0, new ListItem("Select Title", ""));
            }
        }

        protected void btnEditImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (inptReport.HasFile)
                {
                   
                    DataSet theDS = new DataSet();
                    theDS.ReadXml(inptReport.PostedFile.InputStream);

                    //theDS.ReadXml(@"C:\" + inptReport.FileName);()
                    Session["CustomReportDS"] = theDS;
                    Response.Redirect("frmReportCustomNew.aspx?Import=Yes", true);
                   
                }
            }
            catch (Exception err)
            {
                //29Jul08
                //IQCareMsgBox.Show("InvalidFile", this);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private DataSet CreateDataSet()
        {
            DataSet dsCustomReports = new DataSet();

            DataTable dtMstReport = new DataTable("dtMstReport");
            DataTable dtlReportFields = new DataTable("dtlReportFields");
            DataTable dtlReportFilter = new DataTable("dtlReportFilter");

            //============= adding columns to MstReport DataTable ================
            dtMstReport.Columns.Add(new DataColumn("ColumnNo", typeof(int)));
            dtMstReport.Columns.Add(new DataColumn("CategoryId", typeof(int)));
            dtMstReport.Columns.Add(new DataColumn("CategoryName", typeof(string)));
            dtMstReport.Columns.Add(new DataColumn("ReportName", typeof(string)));
            dtMstReport.Columns.Add(new DataColumn("Description", typeof(string)));
            dtMstReport.Columns.Add(new DataColumn("Condition", typeof(string)));
            dtMstReport.Columns.Add(new DataColumn("ReportId", typeof(int)));
            dtMstReport.Columns.Add(new DataColumn("RptType", typeof(string)));

            //============= adding columns to Report's Field DataTable ================
            dtlReportFields.Columns.Add(new DataColumn("GroupId", typeof(int)));
            dtlReportFields.Columns.Add(new DataColumn("FieldId", typeof(int)));
            dtlReportFields.Columns.Add(new DataColumn("FieldLabel", typeof(string)));
            dtlReportFields.Columns.Add(new DataColumn("AggregateFunction", typeof(string)));
            dtlReportFields.Columns.Add(new DataColumn("IsDisplay", typeof(bool)));
            dtlReportFields.Columns.Add(new DataColumn("Sequence", typeof(int)));
            dtlReportFields.Columns.Add(new DataColumn("Sort", typeof(string)));
            dtlReportFields.Columns.Add(new DataColumn("ViewName", typeof(string)));

            //============= adding columns to Report's Filter DataTable ================
            dtlReportFilter.Columns.Add(new DataColumn("LinkFieldId", typeof(int)));
            dtlReportFilter.Columns.Add(new DataColumn("Operator", typeof(string)));
            dtlReportFilter.Columns.Add(new DataColumn("FilterValue", typeof(string)));
            dtlReportFilter.Columns.Add(new DataColumn("AndOr", typeof(string)));
            dtlReportFilter.Columns.Add(new DataColumn("Sequence", typeof(int)));

            dtlReportFilter.Columns.Add(new DataColumn("Operator1", typeof(string)));
            dtlReportFilter.Columns.Add(new DataColumn("FilterValue1", typeof(string)));
            dtlReportFilter.Columns.Add(new DataColumn("PanelId", typeof(int)));
            dtlReportFilter.Columns.Add(new DataColumn("AndOr1", typeof(string)));


            dsCustomReports.Tables.Add(dtMstReport);
            dsCustomReports.Tables.Add(dtlReportFields);
            dsCustomReports.Tables.Add(dtlReportFilter);

            return dsCustomReports;
        }

        protected void btnRun_Click(object sender, EventArgs e)
        {
            if (this.ddTitle.SelectedValue != "")
            {
                IReports CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                DataSet DSCustomReport = CustomReport.GetCustomReport(Convert.ToInt32(ddTitle.SelectedValue));
                string theRptName = "";
                if (Convert.ToInt32(DSCustomReport.Tables[4].Rows[0][0]) > 0 && Convert.ToInt32(DSCustomReport.Tables[4].Rows[0][0]) < 8)
                {
                    theRptName = "rptPotrait";
                }
                else
                {
                    theRptName = "rptLandscape";
                }
                Session.Add("ReportData", DSCustomReport);
                Response.Redirect("frmReportViewer.aspx?ReportId=" + ddTitle.SelectedValue.ToString() + "&rptType=" + theRptName);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("../frmFacilityHome.aspx", true);
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            IIQCareSystem theRptTables = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem,BusinessProcess.Security");
            theRptTables.RefreshReportingTables(1);

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmReportCustomNew.aspx", true);
        }
    }
}