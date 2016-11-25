using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.Billing;
using Entities.Billing;
using IQCare.Billing.Logic;

namespace IQCare.Web.Billing
{
    /// <summary>
    /// Handles payment for Cash, Deposits, and NHIF
    /// </summary>
    public partial class PayBillCashAndDeposit : System.Web.UI.UserControl, IPayment
    {
        #region variables
        /// <summary>
        /// The show deposit
        /// </summary>
        public string showDeposit = "none";
        /// <summary>
        /// The show tendered amount
        /// </summary>
        public string showTenderedAmount = "";
        /// <summary>
        /// The valid
        /// </summary>
        private bool valid = true;

        #endregion

        #region properties
        /// <summary>
        /// Gets the _ items to pay.
        /// </summary>
        /// <value>
        /// The _ items to pay.
        /// </value>
        List<BillItem> _ItemsToPay
        {
            get
            {
                return (List<BillItem>)ItemForPay.DynamicInvoke();
            }
        }
        /// <summary>
        /// //checks whether inputed text is valid if any is invalid it will always return false
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is valid entry; otherwise, <c>false</c>.
        /// </value>
        private bool isValidEntry
        {
            get
            {
                return valid;
            }
            set
            {
                valid = valid == false ? false : value;
            }
        }
        /// <summary>
        /// Gets or sets the payment method identifier.
        /// </summary>
        /// <value>
        /// The payment method identifier.
        /// </value>
        int PaymentMethodId
        {
            get
            {
                return int.Parse(this.HPayMethodID.Value);
            }

            set
            {
                this.HPayMethodID.Value = value.ToString();

            }
        }
        /// <summary>
        /// Gets or sets the name of the payment method.
        /// </summary>
        /// <value>
        /// The name of the payment method.
        /// </value>
        string PaymentMethodName
        {
            get
            {
                return this.HPayMethodName.Value;
            }

            set
            {
                this.HPayMethodName.Value = value.ToString();

            }
        }
        /// <summary>
        /// Gets the net amount due.
        /// </summary>
        /// <value>
        /// The net amount due.
        /// </value>
        Double NetAmountToPay
        {
            get
            {
                if (this.SelectedDiscountPlan != null)
                    return this.AmountToPay * (1.0D - SelectedDiscountPlan.Rate);
                else
                    return this.AmountToPay;
            }
        }
        #endregion

        #region IPayment

        #region "Subscriber Properties"
        /// <summary>
        /// Gets or sets the amount due.
        /// </summary>
        /// <value>
        /// The amount due.
        /// </value>
        public Double AmountDue
        {
            get
            {
                return Double.Parse(this.HDAmountDue.Value);
            }
            set
            {
                this.HDAmountDue.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the amount to pay.
        /// </summary>
        /// <value>
        /// The amount to pay.
        /// </value>
        public Double AmountToPay
        {
            get
            {
                return Double.Parse(this.HDAmountToPay.Value);
            }
            set
            {
                this.HDAmountToPay.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the bill amount.
        /// </summary>
        /// <value>
        /// The bill amount.
        /// </value>
        public Double BillAmount
        {
            get
            {
                return Double.Parse(this.HBillAmount.Value);
            }
            set
            {
                this.HBillAmount.Value = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the bill identifier.
        /// </summary>
        /// <value>
        /// The bill identifier.
        /// </value>
        public int BillId
        {
            get
            {
                return Convert.ToInt32(HBillID.Value);
            }
            set
            {
                this.HBillID.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the bill location identifier.
        /// </summary>
        /// <value>
        /// The bill location identifier.
        /// </value>
        public int BillLocationId
        {
            get
            {
                return Convert.ToInt32(HLocationID.Value);
            }
            set
            {
                HLocationID.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the item for pay.
        /// </summary>
        /// <value>
        /// The item for pay.
        /// </value>
        public Delegate ItemForPay
        {
            get;
            set;
        }
       
        /// <summary>
        /// Gets or sets the payment mode.
        /// </summary>
        /// <value>
        /// The payment mode.
        /// </value>
        public PaymentMethod PaymentMode
        {
            get
            {
                return new PaymentMethod() { Id = this.PaymentMethodId, Name = this.PaymentMethodName };

            }
            set
            {
                this.PaymentMethodId = value.Id.Value;
                this.PaymentMethodName = value.Name;
            }
        }
        /// <summary>
        /// Gets or sets the selected discount plan.
        /// </summary>
        /// <value>
        /// The selected discount plan.
        /// </value>
        public DiscountPlan SelectedDiscountPlan
        {
            get
            {
                if (Session["DiscountPlan"] !=null)
                    return (DiscountPlan)Session["DiscountPlan"];
                return null;
            }
            set
            {
                base.Session["DiscountPlan"] = value;
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance can pay partial.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can pay partial; otherwise, <c>false</c>.
        /// </value>
        public bool HasTransaction
        {
            get
            {
                return HTran.Value == "TRUE";
            }
            set
            {
                HTran.Value = value.ToString().ToUpper();
            }
        }
        /// <summary>
        /// Gets or sets the bill pay option.
        /// </summary>
        /// <value>
        /// The bill pay option.
        /// </value>
        public BillPaymentOptions BillPayOption
        {
            get
            {
                return (BillPaymentOptions)Convert.ToInt32(this.HPayMode.Value);
            }
            set
            {
                this.HPayMode.Value = Convert.ToInt32(value).ToString();
            }
        }
        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public int PatientId
        {
            get
            {
                return Convert.ToInt32(HPatientID.Value);
            }
            set
            {
                this.HPatientID.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>        
        public int UserId
        {
            get
            {
                return Convert.ToInt32(HUserID.Value);
            }
            set
            {
                this.HUserID.Value = value.ToString();
            }
        }

        #endregion

        #region Subscriber events       
        /// <summary>
        /// Occurs when [cancel compute].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when compute balance is canceled.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler CancelCompute;
        /// <summary>
        /// Occurs when [pay complete].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when finish payment events completes.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler PayComplete;
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

        /// <summary>
        /// Occurs when [execute payment].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised to execute payment.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler ExecutePayment;

        #endregion

        #region Compute | Fetch | Clear Data
        /// <summary>
        /// Clears the controls.
        /// </summary>
        public void Clear()
        {
            base.Session.Remove("paymentInformation");
            btnFinish.Enabled = false;
            this.labelAmountDue.InnerText = AmountDue.ToString();
            lblChange.InnerText = lblPaid.InnerText = "";
            //lblAmountToPay.Text = textAmountToPay.Text = AmountDue.ToString();
            lblAmountToPay.Text = textAmountToPay.Text = NetAmountToPay.ToString();
            textReferenceNo.Text = "";
            //ddlPaymentMode.SelectedIndex = -1;
            textTenderedAmount.Text = "0.0";
            panelCompute.Visible = true;
            panelDeposit.Visible = false;
            panelTenderedAmount.Visible = true;
            panelFinish.Visible = false;
            base.Session.Remove("ItemsToPay");
        }

        /// <summary>
        /// Rebinds this instance.
        /// </summary>
        public void Rebind()
        {

            // this.PopulatePaymentMode();
            // tblCompute.Visible = true;
            panelCompute.Visible = true;
            //tblFinish.Visible = false;
            panelFinish.Visible = false;
            this.Populate();

        }
        /// <summary>
        /// Validates the specified bill payment information.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Validate()
        {
            if (this.PaymentMethodName.ToLower().Trim() == "deposit")
            {
                IBilling bMrg = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling,BusinessProcess.Billing");
                DataTable dtDeposit = bMrg.GetPatientDeposit(this.PatientId, this.BillLocationId);
                if (dtDeposit.Rows.Count > 0 && Convert.ToDouble(dtDeposit.Rows[0]["AvailableAmount"]) > 0.0D)
                {
                    Double availableDeposit = Convert.ToDouble(dtDeposit.Rows[0]["AvailableAmount"]);
                    this.ValidateDeposit(availableDeposit);
                    labelAmountTendered.InnerText = "Available Deposit";
                }
                else
                {
                    panelDeposit.Visible = false;
                    NotifyAction("The client has no available deposit", "Patient Deposits", true);
                    buttonCompute.Visible = btnFinish.Visible = false;
                }
            }
            else if (this.PaymentMethodName.ToLower().Trim() == "cash")
            {

                this.ValidateCash();
            }
            else //if (this.PaymentMethodName.ToLower().Trim() == "nhif")
            {
                this.ValidateOthers();
            }
        }

        /// <summary>
        /// Computes this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Compute()
        {
            valid = true;

            string strPaymentMode = this.PaymentMethodName;
            if (strPaymentMode == "Cash")
                isValidEntry = validateTextBox(textTenderedAmount);

            //isValidEntry = validateDropDownList(ddlPaymentMode);

            if (strPaymentMode != "Cash" && strPaymentMode != "Deposit")
                isValidEntry = validateTextBox(textReferenceNo);
            if (!isValidEntry)
            {

                return;
            }
            //  DataTable itemsToPay = this.initializeItemsToPay();
            Double _amountToPay = 0;
            Double _amountTendered = 0;
            Double _discount = 0;

            _amountToPay = Double.Parse(textAmountToPay.Text);
            if (this.SelectedDiscountPlan != null)
            {
                _discount = _amountToPay / (1.0D - this.SelectedDiscountPlan.Rate) - _amountToPay;
            }

            if (strPaymentMode == "Cash")
                _amountTendered = Double.Parse(textTenderedAmount.Text);

            BillPaymentInfo payObject = new BillPaymentInfo()
            {
                BillId = this.BillId,
                LocationId = this.BillLocationId,
                //AmountPayable = _amountToPay,
                Amount = _amountToPay+_discount, //already discounted
                TenderedAmount = _amountTendered,
                ReferenceNumber = textReferenceNo.Text.Trim(),
                Deposit = (this.PaymentMethodName == "Deposit"),
                PaymentMode = new PaymentMethod() { Id = this.PaymentMethodId, Name = this.PaymentMethodName },
                ChosenDiscountPlan = this.SelectedDiscountPlan,
                ItemsToPay = null
            };

            base.Session["paymentInformation"] = payObject;

            if (this.PaymentMethodName == "Deposit")
            {
                this.computePaidAmount(Convert.ToDouble(labelAvailableDeposit.Text), _amountToPay);
                //theDR.SetField("IsDeposit", true);
            }
            else if (this.PaymentMethodName == "Cash")
                this.computePaidAmount(_amountTendered, _amountToPay);
            else //if (this.PaymentMethodName == "NHIF")
            {
                this.computePaidAmount(_amountToPay, _amountToPay);
            }
        }
        #endregion
       
        #endregion

        #region "Wired Events"
        /// <summary>
        /// Called when [cancel compute].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnCancelCompute(object sender, CommandEventArgs e)
        {
            if (this.CancelCompute != null)
            {
                this.CancelCompute(sender, e);
            }
        }
        /// <summary>
        /// Called when [pay execute].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnPayExecute(object sender, CommandEventArgs e)
        {
            if (this.ExecutePayment != null)
            {
                this.ExecutePayment(sender, e);
            }
        }
        /// <summary>
        /// Called when [pay complete].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnPayComplete(object sender, CommandEventArgs e)
        {
            if (this.PayComplete != null)
            {
                this.PayComplete(sender, e);
            }
        }
        /// <summary>
        /// Called when [notify required].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnNotifyRequired(object sender, CommandEventArgs e)
        {
            if (this.NotifyCommand != null)
            {
                this.NotifyCommand(sender, e);
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
        #endregion

        #region event handlers
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

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
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {

            panelDeposit.Visible = this.PaymentMethodName == "Deposit";
            panelTenderedAmount.Visible = CompareValidator1.Enabled = this.PaymentMethodName == "Cash";

        }
        /// <summary>
        /// Handles the Click event of the buttonCompute control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonCompute_Click(object sender, EventArgs e)
        {

            this.Compute();
        }
        /// <summary>
        /// Handles the Click event of the btnFinish control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnFinish_Click(object sender, EventArgs e)
        {
            try
            {
                BillPaymentInfo paymentInfo = (BillPaymentInfo)Session["paymentInformation"];

              //  IBilling BManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
                //  DataTable _items = (DataTable)base.Session["ItemsToPay"];
                //  paymentInfo.Rows[0]["PrintReceipt"] = ckbPrintReciept.Checked;

                paymentInfo.PrintReceipt = ckbPrintReciept.Checked;
                Session["paymentInformation"] = paymentInfo;

                this.OnPayExecute(this, new CommandEventArgs("Execute", paymentInfo));

            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Handles the Click event of the btnOkAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOkAction_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            this.Clear();
        }
        /// <summary>
        /// Handles the Click event of the buttonStepBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonStepBack_Click(object sender, EventArgs e)
        {
            this.Clear();
            this.OnCancelCompute(this, new CommandEventArgs("CancelCompute", BillId));
        } 
        #endregion

        #region private methods
        /// <summary>
        /// Initializes the pay payment object.
        /// </summary>
        /// <returns></returns>
        private DataTable initializePayPaymentObject()
        {

            DataTable theDT = new DataTable();
            theDT.Columns.Add("BillID", typeof(Int32));
            theDT.Columns.Add("LocationID", typeof(Int32));
            theDT.Columns["LocationID"].DefaultValue = this.BillLocationId;
            theDT.Columns.Add("PaymentType", typeof(Int32));
            theDT.Columns.Add("PaymentName", typeof(String));
            theDT.Columns.Add("RefNo", typeof(String));
            theDT.Columns.Add("Amount", typeof(Double));
            theDT.Columns["Amount"].DefaultValue = 0;
            theDT.Columns.Add("TenderedAmount", typeof(Double));
            theDT.Columns["TenderedAmount"].DefaultValue = 0;
            theDT.Columns.Add("IsDeposit", typeof(Boolean));
            theDT.Columns["IsDeposit"].DefaultValue = false;
            theDT.Columns.Add("PrintReceipt", typeof(Boolean));
            theDT.Columns["PrintReceipt"].DefaultValue = true;
            return theDT;
        }
        /// <summary>
        /// Computes the paid amount.
        /// </summary>
        /// <param name="tenderedAmount">The total paid.</param>
        private void computePaidAmount(Double tenderedAmount, Double amountToPay)
        {

            lblPaid.InnerText = tenderedAmount.ToString("F");
            labelPaymentType.InnerText = this.PaymentMethodName;
            labelAmountTopay.InnerText = amountToPay.ToString("F");

            List<BillItem> itemsList = this._ItemsToPay;

            Double _totalToPay = amountToPay;
            Double _discount = 0.0D;


            BillPaymentInfo paymentInfo = (BillPaymentInfo)Session["paymentInformation"];
            if (this.SelectedDiscountPlan != null)
            {
                _discount = paymentInfo.CalculatedDiscount * paymentInfo.Amount;
            }
            labelDiscountGiven.InnerText = _discount.ToString("F");
            if (itemsList != null && itemsList.Count > 0)
            {
                _totalToPay = itemsList.Sum(i => i.Amount) - _discount;
            }

            paymentInfo.ItemsToPay = itemsList;

            //if (!this.HasTransaction && _totalToPay == amountToPay)
            //{
            //    paymentInfo.ItemsToPay = itemsList;
            //}
            //else
            //{
            //    if (this.BillPayOption == BillPaymentOptions.SelectItem)
            //    {
            //        paymentInfo.ItemsToPay = itemsList;
            //    }
            //}
            if (this.AllowPartialPayment) _totalToPay = amountToPay;

            Double _discountedToPay = paymentInfo.AmountPayable;

            //if (this.SelectedDiscountPlan != null)
            //{
            //    _discountedToPay =  ((1.0M - this.SelectedDiscountPlan.Rate) * _totalToPay);
            //}
            Session["paymentInformation"] = paymentInfo;
            //tendered less amount than the amount to be paid
            //if (tenderedAmount < amountToPay || _totalToPay != amountToPay)
            if (tenderedAmount < _discountedToPay || _discountedToPay != amountToPay)
            {
                textTenderedAmount.BorderColor = System.Drawing.Color.Red;
                lblChange.InnerText = "0";
                btnFinish.Enabled = false;
                return;
            }
            else
            {
               // Decimal changeDue = tenderedAmount - amountToPay;
                Double changeDue = tenderedAmount - _discountedToPay;
                if (this.PaymentMethodName != "Cash")//confirm that payment type is cash. change can only be given to cash payments
                    lblChange.InnerText = "0";
                else
                    lblChange.InnerText = (Math.Abs(changeDue)).ToString("F");
                //tblFinish.Visible = true;
                panelFinish.Visible = true;
                btnFinish.Enabled = true;
                // tblCompute.Visible = false;
                panelCompute.Visible = false;
                //amount of bill pending after this transaction
                //Decimal amountAfterThisTransaction = this.AmountDue - amountToPay;
                Double amountAfterThisTransaction = this.AmountDue - paymentInfo.Amount;
                labelAmountDue.InnerText = amountAfterThisTransaction.ToString("F");

            }
        }

        /// <summary>
        /// Validates the text box.
        /// </summary>
        /// <param name="inputbox">The inputbox.</param>
        /// <returns></returns>
        private bool validateTextBox(TextBox inputbox)
        {
            if (inputbox.Text.Trim() == "" || inputbox.Text.Trim() == "0")
            {
                inputbox.BorderColor = System.Drawing.Color.Red;
                valid = false;
                return false;
            }
            inputbox.BorderColor = System.Drawing.Color.White;
            valid = true;
            return true;

        }
        /// <summary>
        /// Validates the drop down list.
        /// </summary>
        /// <param name="inputbox">The inputbox.</param>
        /// <returns></returns>
        private bool validateDropDownList(DropDownList inputbox)
        {
            if (inputbox.SelectedItem.Text.Trim() == "Select")
            {
                inputbox.BorderColor = System.Drawing.Color.Red;
                inputbox.BackColor = System.Drawing.Color.Orange;
                valid = false;
                return false;
            }
            inputbox.BorderColor = System.Drawing.Color.White;
            inputbox.BackColor = System.Drawing.Color.White;
            valid = true;
            return true;

        }
        /// <summary>
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        void NotifyAction(string strMessage, string strTitle, bool errorFlag)
        {

            lblNoticeInfo.Text = strMessage;
            lblNotice.Text = strTitle;
            lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
            lblNoticeInfo.Font.Bold = true;
            this.notifyPopupExtender.Show();
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Message", strMessage));
            list.Add(new KeyValuePair<string, string>("Title", strTitle));
            list.Add(new KeyValuePair<string, string>("errorFlag", errorFlag.ToString().ToLower()));
            this.OnNotifyRequired(this, new CommandEventArgs("Notify", list));
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
        /// Gets a value indicating whether [allow partial payment].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [allow partial payment]; otherwise, <c>false</c>.
        /// </value>
        bool AllowPartialPayment
        {
            get
            {
              // return (this.BillPayOption == BillPaymentOptions.FullBill || !this.HasTransaction) && (this.AmountDue == this.AmountToPay);
               // return false;
                return true;
            }
        }
        /// <summary>
        /// Populates this instance.
        /// </summary>
        void Populate()
        {

            lblTotalBill.InnerText = this.BillAmount.ToString("F");
            buttonCompute.Visible = btnFinish.Visible = !(this.AmountToPay == 0);
            rgAmountToPay.Enabled = !(this.AmountToPay == 0);
            labelAmountOutstanding.Text = labelAmountDue.InnerText = this.AmountDue.ToString("F");

           // lblAmountToPay.Text = textAmountToPay.Text = this.AmountToPay.ToString();

            lblAmountToPay.Text = textAmountToPay.Text = this.NetAmountToPay.ToString("F");

          //  this.rgAmountToPay.MaximumValue = this.AmountDue.ToString();
            this.rgAmountToPay.MaximumValue = this.NetAmountToPay.ToString("F");

            this.rgAmountToPay.MinimumValue = "0";

          //  this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", this.AmountDue);
            this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", this.NetAmountToPay);

            labelPaymentMode.Text = this.PaymentMethodName;

            labelDiscountedAmountToPay.Text = this.NetAmountToPay.ToString("F");

            if (this.SelectedDiscountPlan != null)
            {
                labelDiscount.Text = string.Format("{0} %", this.SelectedDiscountPlan.Rate * 100);
            }
            else
            {
                labelDiscount.Text = "0% discount";
            }

            if (this.AllowPartialPayment)
            {
                lblAmountToPay.Visible = false;
                rgAmountToPay.Enabled = textAmountToPay.Visible = true;

            }
            else
            {
                lblAmountToPay.Visible = true;
                rgAmountToPay.Enabled = textAmountToPay.Visible = false;
            }

            //this.CashDepositRules();
            this.Validate();
        }
        /// <summary>
        /// Validates the deposit.
        /// </summary>
        /// <param name="availableDeposit">The available deposit.</param>
        /// <returns></returns>
        void ValidateDeposit(Double availableDeposit)
        {
            panelDeposit.Visible = true;
            CompareValidator1.Enabled = false;
            this.textTenderedAmount.Enabled = false;
         
            labelAvailableDeposit.Text = availableDeposit.ToString("F");
            labelAmountTendered.InnerText = "Available Deposit";
            buttonCompute.Text = "Compute";
            if (this.AllowPartialPayment)
            {
                lblAmountToPay.Visible = false;
                textAmountToPay.Visible = true;

                //if (availableDeposit <= this.AmountToPay)
                if (availableDeposit <= this.NetAmountToPay)
                {
                    this.rgAmountToPay.MaximumValue = availableDeposit.ToString("F");
                    this.rgAmountToPay.MinimumValue = "0";
                    this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", availableDeposit);
                    lblAmountToPay.Text = this.textAmountToPay.Text = availableDeposit.ToString("F");
                    this.textAmountToPay.ReadOnly = true;

                }
                else
                {
                   // this.rgAmountToPay.MaximumValue = AmountToPay.ToString();
                    this.rgAmountToPay.MaximumValue = NetAmountToPay.ToString("F");
                    this.rgAmountToPay.MinimumValue = "0";
                    //this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", AmountToPay);
                    this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", NetAmountToPay);
                    //lblAmountToPay.Text = this.textAmountToPay.Text = AmountToPay.ToString();
                    lblAmountToPay.Text = this.textAmountToPay.Text = NetAmountToPay.ToString("F");
                    this.textAmountToPay.ReadOnly = false;

                }

            }
            else
            {
                /*
                    ///cannot pay partial. availableDeposit must be greate or equal to amounttopay                
                ///
                */
                //lblAmountToPay.Text = this.textAmountToPay.Text = AmountToPay.ToString();
                //this.rgAmountToPay.MaximumValue = AmountToPay.ToString();

                lblAmountToPay.Text = this.textAmountToPay.Text = NetAmountToPay.ToString("F");
                this.rgAmountToPay.MaximumValue = NetAmountToPay.ToString();
                this.rgAmountToPay.MinimumValue = "0";
                //this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", AmountToPay);
                this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", NetAmountToPay);
                lblAmountToPay.Visible = true;
                textAmountToPay.Visible = false;
                //if (availableDeposit < this.AmountToPay)
                if (availableDeposit < this.NetAmountToPay)
                {

                    panelDeposit.Visible = false;
                  //  NotifyAction(string.Format("Available deposit of{0} is not enough to cover the amount required of {1}", availableDeposit, this.AmountToPay), "Patient Deposits", true);
                    NotifyAction(string.Format("Available deposit of{0} is not enough to cover the amount required of {1}", availableDeposit, this.NetAmountToPay), "Patient Deposits", true);
                    buttonCompute.Visible = btnFinish.Visible = false;
                }


            }

        }
        /// <summary>
        /// Validates the nhif.
        /// </summary>
        void ValidateOthers()
        {
            panelDeposit.Visible = false;
            CompareValidator1.Enabled = false;
            this.textTenderedAmount.Enabled = false;
            
           // labelAvailableDeposit.Text = availableDeposit.ToString();
           // labelAmountTendered.InnerText = "Available Deposit";
            buttonCompute.Text = "Validate";
            if (this.AllowPartialPayment)
            {
                lblAmountToPay.Visible = false;
                textAmountToPay.Visible = true;               
                //this.rgAmountToPay.MaximumValue = AmountToPay.ToString();
                this.rgAmountToPay.MaximumValue = NetAmountToPay.ToString("F");
                this.rgAmountToPay.MinimumValue = "0";
                //this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", AmountToPay);
                this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", NetAmountToPay);
                //lblAmountToPay.Text = this.textAmountToPay.Text = AmountToPay.ToString();
                lblAmountToPay.Text = this.textAmountToPay.Text = NetAmountToPay.ToString("F");
                this.textAmountToPay.ReadOnly = false;         

            }
            else
            {
                //cannot pay partial. availableDeposit must be greate or equal to amounttopay
              /*  lblAmountToPay.Text = this.textAmountToPay.Text = AmountToPay.ToString();
                this.rgAmountToPay.MaximumValue = AmountToPay.ToString();
                this.rgAmountToPay.MinimumValue = "0";
                this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", AmountToPay);
                lblAmountToPay.Visible = true;
                textAmountToPay.Visible = false;           */
                lblAmountToPay.Text = this.textAmountToPay.Text = NetAmountToPay.ToString("F");
                this.rgAmountToPay.MaximumValue = NetAmountToPay.ToString();
                this.rgAmountToPay.MinimumValue = "0";
                this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", NetAmountToPay);
                lblAmountToPay.Visible = true;
                textAmountToPay.Visible = false;   

            }

        }
        /// <summary>
        /// Validates the cash.
        /// </summary>
        void ValidateCash()
        {
            panelTenderedAmount.Visible = true;
            CompareValidator1.Enabled = true;
            panelDeposit.Visible = false;
            rgAmountToPay.Enabled = true;
            this.textTenderedAmount.Enabled = true;
            this.textAmountToPay.ReadOnly = false;

            labelAmountTendered.InnerText = "Amount Tendered:";
            buttonCompute.Text = "Compute Change";
           // lblAmountToPay.Text = this.textAmountToPay.Text = this.AmountToPay.ToString();
            lblAmountToPay.Text = this.textAmountToPay.Text = this.NetAmountToPay.ToString("F");

            //this.rgAmountToPay.MaximumValue = this.AmountToPay.ToString();
            this.rgAmountToPay.MaximumValue = this.NetAmountToPay.ToString("F");
            this.rgAmountToPay.MinimumValue = "0";
            //this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", this.AmountToPay);
            this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", this.NetAmountToPay);
            if (this.AllowPartialPayment)
            {


                this.textTenderedAmount.Enabled = true;
                this.textAmountToPay.ReadOnly = false;
                this.textAmountToPay.Visible = true;
                lblAmountToPay.Visible = false;
                rgAmountToPay.Enabled = true;
            }
            else
            {
                lblAmountToPay.Visible = true;
                this.textAmountToPay.Visible = false;
               // textAmountToPay.Text = lblAmountToPay.Text = this.AmountToPay.ToString();
                textAmountToPay.Text = lblAmountToPay.Text = this.NetAmountToPay.ToString("F");
                rgAmountToPay.Enabled = false;
            }
        }

       
        #endregion




        public string ClientScript
        {
            get { return ""; }
        }
    }
}