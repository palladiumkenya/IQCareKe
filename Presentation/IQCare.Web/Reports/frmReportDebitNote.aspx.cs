using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Interface.Clinical;
namespace IQCare.Web.Reports
{
    public partial class frmReportDebitNote : System.Web.UI.Page
    {
        string theCurrency = string.Empty;
        public string DateFmt
        {
            get { return Session["AppDateFormat"].ToString(); }
        }

        private void Init_Page()
        {
            TxtFromDate.Text = "";
            TxtToDate.Text = "";
            btnSubmit.Enabled = false;  //only enabled when the open transaction are shown.

            DataTable table = GetOpenItems(Convert.ToInt32(Session["PatientId"]), DateTime.Now.AddYears(-100), DateTime.Now);
            if (table.Rows.Count > 0)
            {
                DateTime start = Convert.ToDateTime(table.Rows[0]["TransactionDate"].ToString());
                DateTime end = Convert.ToDateTime(table.Rows[table.Rows.Count - 1]["TransactionDate"].ToString());

                LabelRange.Text = string.Format("Open transactions exist from {0} to {1}", start.ToString(DateFmt), end.ToString(DateFmt));
                TxtFromDate.Text = start.ToString(DateFmt);
                TxtToDate.Text = end.ToString(DateFmt);
            }
            else
            {
                LabelRange.Text = "no open transactions exist for this patient.";
            }

            SetSecurity();

            if (!CanSubmit)
            {
                btnSubmit.Enabled = false;

            }

        }

        private void SetSecurity()
        {
            AuthenticationManager Authentiaction = new AuthenticationManager();
            CanSubmit = Authentiaction.HasFunctionRight(ApplicationAccess.DebitNote, FunctionAccess.View,
                                                        (DataTable)Session["UserRight"]);
        }

        protected bool CanSubmit
        {
            get { return Convert.ToBoolean(ViewState["CanSubmit"].ToString()); }
            set { ViewState["CanSubmit"] = value; }
        }

        private DataTable GetOpenItems(int id, DateTime start, DateTime end)
        {
            IPatientHome PatientManager;
            PatientManager =
                (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            DataTable dataTable = PatientManager.GetPatientDebitNoteOpenItems(id, start, end);
            AddFormattedDateColumn(dataTable);
            return dataTable;
        }

        private void AddFormattedDateColumn(DataTable dataTable)
        {
            dataTable.Columns.Add("TransactionDateFmt", typeof(string));
            foreach (DataRow row in dataTable.Rows)
            {
                row["TransactionDateFmt"] = Convert.ToDateTime(row["TransactionDate"]).ToString(this.DateFmt);
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            ClientScript.GetPostBackEventReference(this, string.Empty);


            if (!IsPostBack)
            {

                Init_Page();
            }

            getCurrency();
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Debit Note";

            TxtFromDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            TxtFromDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            TxtToDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            TxtToDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime start = Convert.ToDateTime(TxtFromDate.Text);
                DateTime end = Convert.ToDateTime(TxtToDate.Text);
                int patientId = Convert.ToInt32(Session["PatientId"]);
                int locationId = Convert.ToInt32(Session["LocationId"]);
                int userId = Convert.ToInt32(Session["AppUserID"]);

                IPatientHome PatientManager;
                PatientManager =
                    (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                int billId = PatientManager.CreateDebitNote(patientId, locationId, userId, start, end);
                PrintDebitNote(billId);
                string theScript;
                theScript = "<script language='javascript' id='PDFPopup'>\n";
                theScript += "fnOpenWin('../ExcelFiles/DebitNote.pdf');\n";
                theScript += "window.location.href='../ClinicalForms/frmPatient_Home.aspx';\n";
                theScript += "</script>\n";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "PDFPopup", theScript);

            }

            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
        }


        private void PrintDebitNote(int billid)
        {
            //theReportHeading = "Upcoming ARV Pickup Report";
            string theReportSource = string.Empty;
            theReportSource = "rptPatientDebitNote.rpt";
            IPatientHome PatientManager;
            PatientManager =
                (IPatientHome)
                ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataSet dataTable = PatientManager.GetPatientDebitNoteDetails(billid, Convert.ToInt32(Session["PatientId"]));
            dataTable.WriteXmlSchema(Server.MapPath("..\\XMLFiles\\PatientDebitNote.xml"));
            ReportDocument rptDocument = new ReportDocument();
            rptDocument.Load(Server.MapPath(theReportSource));
            rptDocument.SetDataSource(dataTable);
            rptDocument.SetParameterValue("BillId", billid.ToString());
            rptDocument.SetParameterValue("Currency", theCurrency.ToString());
            rptDocument.SetParameterValue("FacilityName", Session["AppLocation"].ToString());
            rptDocument.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("..\\ExcelFiles\\DebitNote.pdf"));
        }

        private void getCurrency()
        {
            DataTable dtCurrency = new DataTable();
            System.Data.DataSet theDS = new System.Data.DataSet();
            theDS.ReadXml(Server.MapPath("..\\XMLFiles\\Currency.xml"));
            DataView theCurrDV = new DataView(theDS.Tables[0]);
            theCurrDV.RowFilter = "Id=" + Convert.ToInt32(Session["AppCurrency"]);
            string thestringCurrency = theCurrDV[0]["Name"].ToString();
            theCurrency = thestringCurrency.Substring(thestringCurrency.LastIndexOf("(") + 1, 3);

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ClinicalForms/frmPatient_Home.aspx");
        }


        protected void ButtonShow_Click(object sender, EventArgs e)
        {

            DateTime start = new DateTime();
            DateTime end = new DateTime();

            if (DateTime.TryParse(TxtFromDate.Text, out start)
                && DateTime.TryParse(TxtToDate.Text, out end))
            {
                int id = Convert.ToInt32(Session["PatientId"]);

                DataTable table = GetOpenItems(id, start, end);
                if (table.Rows.Count > 0)
                {
                    btnSubmit.Enabled = CanSubmit;
                    GridViewTran.DataSource = table;
                    GridViewTran.DataBind();
                }
                else
                {
                    btnSubmit.Enabled = false;
                    GridViewTran.DataSource = null;
                    GridViewTran.DataBind();
                }
            }
            else
            {

            }
        }
    }
}