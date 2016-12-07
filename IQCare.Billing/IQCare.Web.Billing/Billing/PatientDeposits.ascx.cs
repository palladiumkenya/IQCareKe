using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.Billing;
using Interface.Billing;

namespace IQCare.Web.Billing
{
    public partial class PatientDeposits : System.Web.UI.UserControl
    {
        #region "Subscriber Properties"
        // <summary>
        /// <summary>
        /// Gets or sets a value indicating whether this instance can refund.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can refund; otherwise, <c>false</c>.
        /// </value>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Whether the logged on user can refund deposit")]
        [System.ComponentModel.Bindable(true)]
        public bool CanRefund
        {
            private get
            {
                if (this.HCanRefund.Value == "")
                    this.HCanRefund.Value = "FALSE";
                return bool.Parse(this.HCanRefund.Value);

            }
            set
            {
                this.HCanRefund.Value = value.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance can receive.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can receive; otherwise, <c>false</c>.
        /// </value>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Whether the logged on user can receive new deposit")]
        [System.ComponentModel.Bindable(true)]
        public bool CanReceive
        {
            private get
            {
                if (this.HCanReceive.Value == "")
                    this.HCanReceive.Value = "FALSE";
                return bool.Parse(this.HCanReceive.Value);

            }
            set
            {
                this.HCanReceive.Value = value.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Patient ID")]
        [System.ComponentModel.Bindable(true)]
        public int PatientId
        {
            private get
            {
                if (this.HPatientID.Value == "")
                    this.HPatientID.Value = "0";
                return int.Parse(this.HPatientID.Value);

            }
            set
            {
                this.HPatientID.Value = value.ToString().ToUpper();
            }
        }
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Print JS Method Name")]
        [System.ComponentModel.Bindable(true)]
        public string PrintReceiptJSMethod
        {
            private get
            {
                if (this.HPrintMethodName.Value == "")
                    this.HPrintMethodName.Value = "";
                return (this.HPrintMethodName.Value);

            }
            set
            {
                this.HPrintMethodName.Value = value.ToString().Trim();
            }
        }
        /// <summary>
        /// Gets or sets the print receipt URL.
        /// </summary>
        /// <value>
        /// The print receipt URL.
        /// </value>
        /// 
        [System.ComponentModel.DefaultValue("")]
        [System.ComponentModel.Category("Behaviour")]
        [System.ComponentModel.Description("Print JS Method Name")]
        [System.ComponentModel.Bindable(true)]
        public string PrintReceiptURL
        {
            private get
            {
                if (this.HPrintURL.Value == "")
                    this.HPrintURL.Value = "";
                return (this.HPrintURL.Value);

            }
            set
            {
                this.HPrintURL.Value = value.ToString().Trim();
            }
        }
        /// <summary>
        /// Gets or sets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        public int LocationId
        {
            private get
            {
                if (this.HLocationID.Value == "")
                    this.HLocationID.Value = "0";
                return int.Parse(this.HLocationID.Value);

            }
            set
            {
                this.HLocationID.Value = value.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int UserId
        {
            get
            {
                return Convert.ToInt32(base.Session["AppUserId"]);
            }
        }
        #endregion

        #region Subscriber events

        /// <summary>
        /// Occurs when [refund command].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when a refund is made.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler RefundCommand;
        /// <summary>
        /// Occurs when [deposit command].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when a deposit is made.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler DepositCommand;

        /// <summary>
        /// Occurs when [notify command].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when a notifcation need to be sent.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler NotifyCommand;
        /// <summary>
        /// Occurs when [error occurred].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when an error occurs.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler ErrorOccurred;
        #endregion

        /// <summary>
        /// Called when [deposit command].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnDepositCommand(object sender, CommandEventArgs e)
        {
            if (this.DepositCommand != null)
            {
                this.DepositCommand(sender, e);
            }
        }
        /// <summary>
        /// Called when [refund command].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnRefundCommand(object sender, CommandEventArgs e)
        {
            if (this.RefundCommand != null)
            {
                this.RefundCommand(sender, e);
            }
        }
        /// <summary>
        /// Called when [error occured].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnErrorOccured(object sender, CommandEventArgs e)
        {
            if (this.ErrorOccurred != null)
            {
                this.ErrorOccurred(sender, e);
            }
        }

        /// <summary>
        /// Called when [notify command].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnNotifyCommand(object sender, CommandEventArgs e)
        {
            if (this.NotifyCommand != null)
            {
                this.NotifyCommand(sender, e);
            }
        }
        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void showErrorMessage(ref Exception ex)
        {

            this.OnErrorOccured(this, new CommandEventArgs("Error", ex));
        }

        /// <summary>
        /// Gets the billing manager.
        /// </summary>
        /// <value>
        /// The billing manager.
        /// </value>
        IBilling BillingManager
        {
            get
            {

                return (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");

            }
        }
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        /// <summary>
        /// Binds a data source to the invoked server control and all its child controls with an option to raise the <see cref="E:System.Web.UI.Control.DataBinding" /> event.
        /// </summary>
        /// <param name="raiseOnDataBinding">true if the <see cref="E:System.Web.UI.Control.DataBinding" /> event is raised; otherwise, false.</param>
        protected override void DataBind(bool raiseOnDataBinding)
        {
            base.DataBind(raiseOnDataBinding);
        }

        /// <summary>
        /// Handles the Prender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Prender(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btnNewDeposit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnNewDeposit_Click(object sender, EventArgs e)
        {
            try
            {
                decimal amount = 0.0M;
                amount = Convert.ToDecimal(textAmount.Text.Trim());
                if (amount > 0.0M)
                {
                    //DataTable dt
                      Receipt receipt  = this.BillingManager.ExecuteDepositTransaction(this.PatientId, this.LocationId, this.UserId, amount, DepositTransactionType.MakeDeposit);

                      string transactionID = receipt.TransactionId.ToString();
                        //dt.Rows[0]["TransactionID"].ToString();
                      string tranRef = receipt.ReceiptNumber;
                        //dt.Rows[0]["TransactionReference"].ToString();

                    var list = new List<KeyValuePair<string, string>>();
                    list.Add(new KeyValuePair<string, string>("TransactionID", transactionID));

                    list.Add(new KeyValuePair<string, string>("TransactionReference", tranRef));
                   // base.Session["TransactionReceipt"] = receipt;

                    string theUrl = string.Format("{0}?ReceiptTrxCode={1}&RePrint=false&TranXORType=9", this.PrintReceiptURL, tranRef);
                    string urlParam = String.Format("{0}('{1}')",this.PrintReceiptJSMethod,theUrl);

                  //  this.btnOkAction.OnClientClick = urlParam;// string.Format("javascript:{0};return true;", urlParam);

                   // ScriptManager.RegisterStartupScript(Page, Page.GetType(),"PRINTRECEIPT",urlParam,true);
                    textAmount.Text = "";
                    
                    this.OnDepositCommand(btnNewDeposit, new CommandEventArgs("NewDeposit", list));

                   this.Rebind();  
                    this.NotifyAction("Successful. Deposit received of " + textAmount.Text.Trim(), "Making Deposit", false,urlParam);
                }
                else
                {
                    this.NotifyAction("Cannot make a deposit of " + textAmount.Text.Trim() + " amount!", "Error Making Deposit", true);
                }

            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
   
        /// <summary>
        /// Handles the Click event of the btnRefundYes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnRefundYes_Click(object sender, EventArgs e)
        {
            try
            {
                decimal amount = 0.0M;
                string strAmount = labelTotalAvailable.Text.Trim().Replace(",", "");
                amount = Convert.ToDecimal(strAmount);
                if (amount > 0.0M)
                {
                    //DataTable dt = 
                      Receipt receipt =  this.BillingManager.ExecuteDepositTransaction(this.PatientId, this.LocationId, this.UserId, amount, DepositTransactionType.ReturnDeposit);

                      string transactionID = receipt.TransactionId.ToString();
                        //dt.Rows[0]["TransactionID"].ToString();
                      string tranRef = receipt.ReceiptNumber;
                        //dt.Rows[0]["TransactionReference"].ToString();
                     /// base.Session["TransactionReceipt"] = receipt;
                    var list = new List<KeyValuePair<string, string>>();
                    list.Add(new KeyValuePair<string, string>("TransactionID", transactionID));

                    list.Add(new KeyValuePair<string, string>("TransactionReference", tranRef));

                    string theUrl = string.Format("{0}?ReceiptTrxCode={1}&RePrint=false&TranXORType=13", this.PrintReceiptURL, tranRef);
                    string urlParam = String.Format("{0}('{1}')", this.PrintReceiptJSMethod, theUrl);

                    this.NotifyAction("Successful. Deposit refunded amounting to " + amount.ToString(), "Refunding Deposit", false,urlParam);
                }
                else
                {
                    this.NotifyAction("Cannot refund  deposit of " + amount.ToString(), "Error Refunding Deposit", true);
                }
                this.Rebind();
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Handles the Click event of the buttonViewTransactions control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonViewTransactions_Click(object sender, EventArgs e)
        {
            DataTable dt = this.BillingManager.GetPatientDepositTransactions(this.PatientId, this.LocationId);
            gridTransaction.DataSource = dt;
            gridTransaction.DataBind();
            depositDetailsPopup.Show();
        }
        /// <summary>
        /// Handles the RowDataBound event of the gridDeposit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridDeposit_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }       
        /// <summary>
        /// Handles the RowDataBound event of the transactionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void transactionGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
            }
        }

        /// <summary>
        /// Handles the RowCommand event of the transactionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void transactionGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int transactionID;
            int index;
            if (e.CommandName == "Print")
            {
                GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                GridView transactionGrid = (GridView)gvr.NamingContainer;
                index = gvr.RowIndex;
                transactionID = int.Parse(transactionGrid.DataKeys[index].Values["transactionid"].ToString());
                return;
            }
        }
        /// <summary>
        /// Shows the hide refund.
        /// </summary>
        /// <returns></returns>
        protected string ShowHideRefund()
        {
            decimal amount = 0.0M;
            decimal.TryParse(labelTotalAvailable.Text, out amount);
            return this.CanRefund && Convert.ToDecimal(amount) > 0.0M ? "" : "none";
        }
        /// <summary>
        /// Formats the amount.
        /// </summary>
        /// <param name="transationType">Type of the transation.</param>
        /// <returns></returns>
        protected string FormatAmount(object transationType)
        {
            if (transationType.ToString().ToLower() == "debit") return "left";
            else return "right";
        }
        /// <summary>
        /// Formats the amount display.
        /// </summary>
        /// <param name="transationType">Type of the transation.</param>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        protected string FormatAmountDisplay(object transationType, object amount)
        {
            if (transationType.ToString().ToLower() == "debit") 
                return "("+amount.ToString()+")";
            else 
                return amount.ToString();
        }
        /// <summary>
        /// Populates this instance.
        /// </summary>
        void Populate()
        {
            try
            {
               
                DataTable dDeposit = new DataTable("PatientDepositSummary");
                dDeposit = BillingManager.GetPatientDeposit(this.PatientId, this.LocationId);
                if (dDeposit.Rows.Count > 0)
                {
                    labelDepositDate.Text = DataBinder.Eval(dDeposit.DefaultView[0], "DepositDate", "{0:dd-MMM-yyyy HH:mm}");
                    labelDepositedAmount.Text = DataBinder.Eval(dDeposit.DefaultView[0], "Amount", "{0:N}");
                    labelReceivedBy.Text = DataBinder.Eval(dDeposit.DefaultView[0], "ReceivedBy").ToString();
                    labelTotalAvailable.Text = DataBinder.Eval(dDeposit.DefaultView[0], "AvailableAmount", "{0:N}");
                }
                //this.gridDeposit.DataSource = dDeposit;
                //this.gridDeposit.DataBind();
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Rebinds this instance.
        /// </summary>
        public void Rebind()
        {

            this.Populate(); 
            DataBind(true);
        }
        /// <summary>
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        void NotifyAction(string strMessage, string strTitle, bool errorFlag,string onOkScript="")
        {

            lblNoticeInfo.Text = strMessage;
            lblNotice.Text = strTitle;
            lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
            lblNoticeInfo.Font.Bold = true;
            btnOkAction.OnClientClick = "";
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Message", strMessage));
            list.Add(new KeyValuePair<string, string>("Title", strTitle));
            list.Add(new KeyValuePair<string, string>("errorFlag", errorFlag.ToString().ToLower()));
            if (onOkScript != "")
            {
                list.Add(new KeyValuePair<string, string>("OkScript", onOkScript));
                btnOkAction.OnClientClick = onOkScript;
            }
            this.notifyPopupExtender.Show();
            this.OnNotifyCommand(this, new CommandEventArgs("Notify", list));
        }
    }
}