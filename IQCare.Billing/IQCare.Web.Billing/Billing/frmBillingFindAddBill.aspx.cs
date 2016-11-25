using System;
using System.Data;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.Billing;
using Application.Common;
using IQCare.Web.UILogic;

namespace IQCare.Web.Billing
{
    public partial class FindAddBill : System.Web.UI.Page
    {
        //AuthenticationManager Authentication = new AuthenticationManager();
        //int LocationID;
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Master.ExecutePatientLevel = false;

            if (CurrentSession.Current==null || !CurrentSession.Current.HasCurrentModuleRight())
            {
                CurrentSession.Logout();
                string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                //Response.Redirect(theUrl);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Redirect(theUrl, true);
            }
            CurrentSession.Current.ResetCurrentPatient();
            Init_Page();
        }
        protected void btn_close_Click(object sender, EventArgs e)
        {
            string theUrl = "./Home.aspx";
            Response.Redirect(theUrl, false);
        }
        /// <summary>
        /// Init_s the page.
        /// </summary>
        private void Init_Page()
        {


            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Billing";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            Session["PatientId"] = 0;
            Session["TechnicalAreaId"] = null;
            Session["TechnicalAreaName"] = null;

            divsearch.Visible = false;
            ViewState["Facility"] = null;
            // LocationID = Convert.ToInt32(Session["AppLocationId"]);

            /// loadOpenBills();
            /// {
            if (!IsPostBack)
            {
                this.PopulateData();
            }

        }
        /// <summary>
        /// Gets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        int LocationId
        {
            get
            {
                return CurrentSession.Current.Facility.Id;
            }
        }
        
        /// <summary>
        /// Populates the data.
        /// </summary>
        void PopulateData()
        {
            IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
            // int? _patientID = null;
            DateTime? _dateFrom = null;
            DateTime? _dateTo = null;
            if (ckbTodaysBills.Checked)
            {
                _dateFrom = DateTime.Today;
                _dateTo = DateTime.Today.AddDays(1).AddMilliseconds(-1);
            }
            DataTable dt = BillingManager.GetPatientWithUnpaidItems(this.LocationId, _dateFrom, _dateTo);
            dt.DefaultView.Sort = "BillDate Desc";
            //if (ckbTodaysBills.Checked)
            //{
            //    DataView dv = dt.DefaultView;
            //    dv.RowFilter = "BillDate = '" + DateTime.Today + "'";
            //    DataTable theDT = dv.ToTable();
            //    grdPatienBill.DataSource = theDT;
            //}
            //else
            //{
                grdPatienBill.DataSource = dt.DefaultView;
                
           // }
            grdPatienBill.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// Handles the SelectedIndexChanged event of the grdPatienBill control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void grdPatienBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            string theUrl = string.Empty;
            //DataTable theDT = (DataTable)grdPatienBill.DataSource;
            //  DataRow theDR = theDT.Rows[grdPatienBill.SelectedIndex];
            int patientId = int.Parse(grdPatienBill.SelectedDataKey.Values["PatientID"].ToString());
            CurrentSession.Current.SetCurrentPatient(patientId);
            // patientID = Int32.Parse(theDR.ItemArray[0].ToString());
            base.Session["PatientId"] = patientId;
            theUrl = "./frmBilling_ClientBill.aspx";

            Response.Redirect(theUrl, false);


        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the rbtlst_findBill control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void rbtlst_findBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtlst_findBill.SelectedValue == "Patient")
            {
                string theUrl;
                theUrl = string.Format("{0}?FormName={1}&mnuClicked={2}", "..//billing//frmFindPatient.aspx", "FindAddBill", "FindAddBill");
                //string.Format("{0}?PatientId={1}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString());
                Response.Redirect(theUrl, false);

            }

            else
            {
                //this.LoadOpenBills();
                this.PopulateData();
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the grdPatienBill control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdPatienBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdPatienBill, "Select$" + e.Row.RowIndex);
            }
        }

        /// <summary>
        /// Handles the DataBound event of the grdPatienBill control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void grdPatienBill_DataBound(object sender, EventArgs e)
        {
              GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                for (int i = 0; i < grdPatienBill.Columns.Count; i++)
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    TextBox txtSearch = new TextBox();
                    txtSearch.Attributes["placeholder"] = grdPatienBill.Columns[i].HeaderText;
                    txtSearch.CssClass = "search_textbox";
                    cell.Controls.Add(txtSearch);
                    row.Controls.Add(cell);
                }
                try { grdPatienBill.HeaderRow.Parent.Controls.AddAt(1, row); }
                catch { }
                if (grdPatienBill.Rows.Count < 5)
                {
                    row.Style.Clear();
                    row.Style.Add("display", "none");
                }
        }

        protected void ckbTodaysBills_CheckedChanged(object sender, EventArgs e)
        {
            PopulateData();
        }

        protected void lnkSummaryReport_Click(object sender, EventArgs e)
        {
            IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
            DataSet theDS=BillingManager.getCashiersTransactionSummary(Convert.ToInt32(base.Session["AppUserId"]));
            DataTable theDt=theDS.Tables[0];
            DataTable refundsDt = theDS.Tables[1];
            Decimal refunds = 0;
            if (refundsDt.Rows.Count > 0)
                refunds = (Decimal)refundsDt.Rows[0][0];

            Decimal transactions=0;
            Decimal collections=0;
            Decimal cashcollections=0;

            if (theDt.Rows.Count > 0)
            {
                 transactions = Convert.ToDecimal(theDt.Compute("Sum(TotalTransactions)", ""));
                 collections = Convert.ToDecimal(theDt.Compute("Sum(Total)", ""));
                 cashcollections = Convert.ToDecimal(theDt.Compute("Sum(Total)", "PaymentName= 'Cash' OR PaymentName='Deposit'"));
            }

            Decimal cashInHand = cashcollections - refunds;
            lblTotalTransactions.Text = transactions.ToString();
            lblTotalCollections.Text = collections.ToString();
            lblRefunds.Text = refunds.ToString();
            lblCash.Text = cashInHand.ToString();

            gridTransaction.DataSource = theDt;
            gridTransaction.DataBind();
            transactionsSummaryPopup.Show();
        }


    }
}
