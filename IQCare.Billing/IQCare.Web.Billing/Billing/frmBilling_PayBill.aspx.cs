using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Entities.Billing;
using Interface.Billing;
using IQCare.Billing.Logic;
using IQCare.Web.UILogic;

namespace IQCare.Web.Billing
{
    /// <summary>
    /// Payment Page. Use the plugin for the payment mode
    /// </summary>
    public partial class PayBill : System.Web.UI.Page
    {

        /// <summary>
        /// The is error
        /// </summary>
        bool isError = false;
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        delegate List<BillItem> ItemsToPay();
        /// <summary>
        /// The pay bill control
        /// </summary>
        IPayment payBillControl;
        /// <summary>
        /// 
        /// </summary>
        Control _oldControl = null;
        /// <summary>
        /// The authentication
        /// </summary>
        AuthenticationManager Authentication = new AuthenticationManager();
        #region Properties
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
                return CurrentSession.Current.User.Id;
            }
        }

        /// <summary>
        /// Gets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        int PatientId
        {
            get
            {
                return CurrentSession.Current.CurrentPatient.Id;
                    //Convert.ToInt32(base.Session["PatientId"]);
            }
        }
        /// <summary>
        /// Gets the bill location identifier.
        /// </summary>
        /// <value>
        /// The bill location identifier.
        /// </value>
        int BillLocationId
        {
            get
            {
                return CurrentSession.Current.Facility.Id;
                    //Convert.ToInt32(HLocationID.Value);
            }
        }
        /// <summary>
        /// Gets the bill identifier.
        /// </summary>
        /// <value>
        /// The bill identifier.
        /// </value>
        int BillId
        {
            get
            {
                return Convert.ToInt32(base.Session["BillID"]);
            }
        }
        /// <summary>
        /// Gets or sets the amount due.
        /// </summary>
        /// <value>
        /// The amount due.
        /// </value>
        decimal AmountDue
        {
            get
            {
                decimal _amt = 0.0M;
                decimal.TryParse(this.HDAmountDue.Value, out _amt);
                return _amt;
            }
            set
            {
                this.HDAmountDue.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the bill amount.
        /// </summary>
        /// <value>
        /// The bill amount.
        /// </value>
        decimal BillAmount
        {
            get
            {
                decimal _amt = 0.0M;
                decimal.TryParse(this.HDBillAmount.Value, out _amt);
                return _amt;

            }
            set
            {
                this.HDBillAmount.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the bill number.
        /// </summary>
        /// <value>
        /// The bill number.
        /// </value>
        string BillNumber
        {
            get
            {
                return this.HDBillNumber.Value;
            }
            set
            {
                this.HDBillNumber.Value = value.ToString();
            }
        }
        /// <summary>
        /// Gets or sets the bill pay option.
        /// </summary>
        /// <value>
        /// The bill pay option.
        /// </value>
        BillPaymentOptions BillPayOption
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
        /// Gets the default payment controller.
        /// </summary>
        /// <value>
        /// The default payment controller.
        /// </value>
        string DefaultPaymentHandler
        {
            get
            {

                return "CORPORATE";
            }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="frmBilling_ReverseBill"/> is debug.
        /// </summary>
        /// <value>
        ///   <c>true</c> if debug; otherwise, <c>false</c>.
        /// </value>
        bool Debug
        {
            get
            {
                bool _debug = true;
                bool.TryParse(ConfigurationManager.AppSettings.Get("DEBUG").ToLower(), out _debug);
                return _debug;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has transaction.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has transaction; otherwise, <c>false</c>.
        /// </value>
        bool HasTransaction
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
        #endregion

        #region PaymentPlugin
        /// <summary>
        /// Adds the pay control.
        /// </summary>
        private void AddPayControl(Hashtable ht)
        {
            //  
            string controlName = ht["ControlName"].ToString();
            Control payControl = base.LoadControl(controlName);
            payBillControl = payControl as IPayment;
            if (phPayMethod.HasControls())
            {
                Control oldControl = phPayMethod.Controls.OfType<Control>().DefaultIfEmpty(null).FirstOrDefault();
                if (oldControl != null)
                {
                    oldControl.Dispose();
                    phPayMethod.Controls.Remove(oldControl);
                }
            }

            if (payBillControl != null)
            {
                DiscountPlan plan = (DiscountPlan)ht["DiscountPlan"];

                _oldControl = payControl;
                payBillControl.AmountDue = this.AmountDue;
                payBillControl.BillAmount = this.BillAmount;
                payBillControl.BillLocationId = Convert.ToInt32(HLocationID.Value);
                payBillControl.PatientId = this.PatientId;
                payBillControl.UserId = this.UserId;
                payBillControl.BillId = this.BillId;
                payBillControl.PaymentMode = (PaymentMethod)ht["PaymentMode"];
                payBillControl.AmountToPay = (decimal)ht["AmountToPay"];
                payBillControl.BillPayOption = this.BillPayOption;
                payBillControl.HasTransaction = this.HasTransaction;
                if (plan != null)
                    payBillControl.SelectedDiscountPlan = plan;              
                

                ItemsToPay itemsToPay = new ItemsToPay(initializeItemsToPay);
                payBillControl.ItemForPay = itemsToPay;

                //phPayMethod.Controls.Clear();
                phPayMethod.Controls.Add(payControl);
               
                payBillControl.Rebind();
                this.DisplayMode("PAY");

                Session["PayControl"] = ht;
                payBillControl.ExecutePayment += new CommandEventHandler(payBillControl_ExecutePayment);
                payBillControl.ErrorOccurred += new CommandEventHandler(payBillControl_ErrorOccurred);
                payBillControl.NotifyCommand += new CommandEventHandler(payBillControl_NotifyCommand);
                payBillControl.PayComplete += new CommandEventHandler(payBillControl_PayComplete);
                payBillControl.CancelCompute += new CommandEventHandler(payBillControl_CancelCompute);
            }
        }

        /// <summary>
        /// Handles the CancelCompute event of the payBillControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void payBillControl_CancelCompute(object sender, CommandEventArgs e)
        {
            ((IPayment)sender).Clear();
            this.DisplayMode("ITEMS");
            //this.PopulateBillTransactions();        
            //  this.phPayMethod.Controls.Clear();
            phPayMethod.Controls.Remove((Control)sender);
            Session.Remove("PayControl");
            UpdatePanel1.Update();
            upBill.Update();
            this.Init_page();
           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Selector", "ToggleCheckUncheckAllOptionAsNeeded();", true);
        }

        /// <summary>
        /// Handles the NotifyCommand event of the payBillControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void payBillControl_NotifyCommand(object sender, CommandEventArgs e)
        {
            List<KeyValuePair<string, string>> param = e.CommandArgument as List<KeyValuePair<string, string>>;
            string strMessage = param.Find(l => l.Key == "Message").Value;
            string strTitle = param.Find(l => l.Key == "Message").Value;
            bool errorFlag = param.Find(l => l.Key == "errorFlag").Value.Equals("true");
            this.NotifyAction(strMessage, strTitle, errorFlag);
        }

        /// <summary>
        /// Handles the ErrorOccurred event of the payBillControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void payBillControl_ErrorOccurred(object sender, CommandEventArgs e)
        {
            Exception ex = e.CommandArgument as Exception;
            this.showErrorMessage(ref ex);
        }
        /// <summary>
        /// Handles the PayComplete event of the payBillControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void payBillControl_PayComplete(object sender, CommandEventArgs e)
        {

        }
        /// <summary>
        /// Handles the Compute event of the payBillControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void payBillControl_Compute(object sender, CommandEventArgs e)
        {

        }
        /// <summary>
        /// Handles the ExecutePayment event of the payBillControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        void payBillControl_ExecutePayment(object sender, CommandEventArgs e)
        {
            // Hashtable payObject = e.CommandArgument as Hashtable;

            BillPaymentInfo paymentInfo = (BillPaymentInfo)e.CommandArgument;
            List<BillItem> items = (List<BillItem>)paymentInfo.ItemsToPay;//["PayItems"];
            try
            {
                IBilling BManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
                
                //DataTable table
                  Receipt receipt  = BManager.SavePatientBillPayments(this.PatientId, paymentInfo, this.UserId);

                //BManager.SavePatientBillPayments(this.PatientID, paymentInfo, null, this.UserID);
                bool printReceipt = paymentInfo.PrintReceipt;// Convert.ToBoolean(paymentInfo.Rows[0]["PrintReceipt"]);
                // Session["transactionID"] = transactionID;
                //Reciept print
                string theUrl;
                base.Session.Remove("ItemsToPay");
                base.Session.Remove("paymentInformation");
                base.Session.Remove("DiscountPlan");
                base.Session.Remove("totalbill");
               // DataRow row = table.Rows[0];
                string transactionID = receipt.TransactionId.ToString();// row["TransactionID"].ToString();
                string transactionRef = receipt.ReceiptNumber;// row["TransactionReference"].ToString();
                ((IPayment)sender).Clear();

                if (printReceipt)
                {
                  //  base.Session["TransactionReceipt"] = receipt;
                    theUrl = string.Format("./frmBilling_Reciept.aspx?ReceiptTrxCode={0}&RePrint=false", transactionRef);
                    string theOrdScript;
                    theOrdScript = "<script language='javascript' id='PrintReciept'>\n";
                    theOrdScript += "window.open('" + theUrl + "','Receipt','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes');\n";
                    //  theOrdScript += "window.location.href = './frmBilling_ClientBill.aspx';\n";
                    theOrdScript += "</script>\n";
                    //  Page.ClientScript.RegisterStartupScript(this.GetType(), "PrintReciept", theOrdScript);
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "PrintReceipt", theOrdScript, false);

                }
                //
                this.DisplayMode("ITEMS");
               this.PopulateBillTransactions();
                UpdatePanel1.Update();
                upBill.Update();
                Session.Remove("PayControl");
                this.Init_page();
            }
            catch (Exception ex)
            {
                ((IPayment)sender).Clear();
                this.showErrorMessage(ref ex);
            }
        }
        #endregion

        #region Page Events

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (CurrentSession.Current == null || !CurrentSession.Current.HasFeaturePermission(ApplicationAccess.BillingFeature.ReceivePayment))
            {
                string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                CurrentSession.Logout();
                //Response.Redirect(theUrl);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Redirect(theUrl, true);
            }
            if (!IsPostBack)
            {
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Billing >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Pay Bill";
              //  (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
                Session.Remove("PayControl");
                Init_page();
            }
            if (Session["PayControl"] != null)
            {
                Hashtable ht = (Hashtable)Session["PayControl"];
                this.AddPayControl(ht);
            }
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        /* protected override void OnInit(EventArgs e)
         {
             base.OnInit(e);
            
         }
         protected override void LoadViewState(object savedState)
         {
             base.LoadViewState(savedState);
           // if(_oldControl !=null) phPayMethod.Controls.Remove(_oldControl);

         }*/
        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            divError.Visible = this.isError;
            panelPayment.Visible = HDisplay.Value == "PAY";
            panelSummarry.Visible = HDisplay.Value == "ITEMS";
            this.SetStyle();
        }
        #endregion

        #region Populate Methods
        /// <summary>
        /// Populates the payment mode.
        /// </summary>
        void PopulatePaymentMode()
        {
            //BindFunctions BindManager = new BindFunctions();
            //IQCareUtils theUtils = new IQCareUtils();
            //using (DataSet theDSXML = new DataSet())
            //{
            //    /// DataTable theDT;
                // theDSXML.ReadXml(MapPath("~\\XMLFiles\\AllMasters.con"));

                IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling,BusinessProcess.Billing");
                DataTable dtDeposit = bMgr.GetPatientDeposit(this.PatientId, this.BillLocationId);
                //string rowFilter = "DeleteFlag=0 and CodeID=212 and Name <> 'WriteOff'";
                //if (dtDeposit.Rows.Count == 0 || Convert.ToDecimal(dtDeposit.Rows[0]["AvailableAmount"]) == 0.0M)
                //    rowFilter = "DeleteFlag=0 and CodeID=212 and Name <> 'Deposit'";


                List<PaymentMethod> _paymentMethods = bMgr.GetPaymentMethods("");
                //if (_paymentMethods != null)
                //{
                //    var c = _paymentMethods.Where(_p => _p.Active == true && _p.Name != "WriteOff");
                //}
                // DataView theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                // theDV.RowFilter = rowFilter;
                if (_paymentMethods != null && _paymentMethods.Count > 0)
                {
                    //  DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    bool showdeposit = true;
                    if (dtDeposit.Rows.Count == 0 || Convert.ToDouble(dtDeposit.Rows[0]["AvailableAmount"]) == 0.0D)
                    {
                        showdeposit = false;
                        //_paymentMethods.DefaultIfEmpty(null).FirstOrDefault(p=> p.Name =="Deposit")                      
                    }
                    _paymentMethods.RemoveAll(p => p.Active == false || p.Name == "WriteOff" || (showdeposit == false && p.Name == "Deposit"));

                   // List<string> PayMethods = PaymentConfigHelper.PayMethods();

                    ddlPaymentMode.Items.Clear();
                    ddlPaymentMode.Items.Add(new ListItem("Select..", ""));
                    foreach (PaymentMethod method in _paymentMethods)
                    {
                        string name = method.Name;
                        //if (PayMethods.Exists(x => x == name))
                        //{
                            ddlPaymentMode.Items.Add(new ListItem(method.Name, method.Id.ToString()+"|"+method.ControlName));
                        //}
                    }
                    //foreach (DataRow row in theDT.Rows)
                    //{
                    //    string name = row["Name"].ToString();

                    //    if (PayMethods.Exists(x => x == name))
                    //    {
                    //        ddlPaymentMode.Items.Add(new ListItem(row["Name"].ToString(), row["ID"].ToString()));
                    //    }
                    //}
                    // theDV.Dispose();
                    // theDT.Clear();
                }
                bMgr = null;
            //}
        }

        /// <summary>
        /// Populates the patient details.
        /// </summary>
       /* void PopulatePatientDetails()
        {
            IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataTable theDT = ptnMgr.GetPatientRecord(Convert.ToInt32(Session["PatientId"]));
            //Session["ClientInfo"] = theDS;
            if (theDT.Rows.Count > 0)
            {
                lblname.Text = String.Format("{0} {1} {2}", theDT.Rows[0]["Firstname"], theDT.Rows[0]["Middlename"], theDT.Rows[0]["Lastname"]);
                lblsex.Text = (theDT.Rows[0]["sex"].ToString() == "16") ? "Male" : "Female";
                lbldob.Text = theDT.Rows[0]["dob"].ToString();
                lblFacilityID.Text = theDT.Rows[0]["PatientFacilityID"].ToString();
                lblIQno.Text = theDT.Rows[0]["IQNumber"].ToString();
                lblAge.Text = string.Format("{0} year {1} months", theDT.Rows[0]["Age"], theDT.Rows[0]["Age1"]);
            }
            ptnMgr = null;
        }*/
        /// <summary>
        /// Populates the bill transactions.
        /// </summary>
        void PopulateBillTransactions()
        {
            IBilling BManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
            DataTable dtTransaction = BManager.GetBillTransactions(BillId);
            gridBillTransaction.DataSource = dtTransaction;
            gridBillTransaction.DataBind();
        }
        /// <summary>
        /// Populates the bill items.
        /// </summary>
        void PopulateBillItems()
        {
            IBilling BManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
            DataTable dtItems = BManager.GetBillItems(this.BillId);            
            grdPayItems.Columns[4].FooterText = "Total: " + this.BillAmount.ToString("N");
            grdPayItems.DataSource = dtItems;
            grdPayItems.DataBind();
        }

        #endregion

        #region Init Methods
        /// <summary>
        /// Init_pages this instance.
        /// </summary>
        private void Init_page()
        {
            try
            {
                IBilling BManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
                DataTable dtBill = BManager.GetBillDetails(this.BillId);
                // lblTotalBill.InnerText = dtBill.Rows[0]["BillAmount"].ToString();
                this.AmountDue = Convert.ToDecimal(dtBill.Rows[0]["UnpaidAmount"].ToString());

                bool hasTransaction = Convert.ToBoolean(dtBill.Rows[0]["HasTransaction"]);
                this.HasTransaction = hasTransaction;
                this.BillPayOption = BillPaymentOptions.FullBill;// (BillPaymentOptions)Convert.ToInt32(dtBill.Rows[0]["PayMode"].ToString());

                labelBillNumber.Text = this.BillNumber = dtBill.Rows[0]["BillNumber"].ToString();
                HLocationID.Value = dtBill.Rows[0]["LocationID"].ToString();
                if (this.AmountDue == 0)
                {
                    //string theUrl = "./frmBillingFindAddBill.aspx";
                    //Response.Redirect(theUrl, false);
                }
                buttonProceed.Enabled = this.AmountDue > 0;
                this.BillAmount = Convert.ToDecimal(dtBill.Rows[0]["BillAmount"].ToString());
                // labelAmountOutstanding.Text = labelAmountDue.InnerText = textAmountToPay.Text = this.AmountDue.ToString();
                labelAmountDue.Text = this.AmountDue.ToString();
                // this.rgAmountToPay.MaximumValue = this.AmountDue.ToString();
                //  this.rgAmountToPay.MinimumValue = "0";
                // this.rgAmountToPay.ErrorMessage = string.Format("The value should be between 0 and {0}", this.AmountDue);
                this.PopulateBillItems();
                if (hasTransaction)
                {
                    this.PopulateBillTransactions();
                }
                this.PopulatePaymentMode();

                //this.PopulatePatientDetails();
                HDisplay.Value = "ITEMS";
                panelPayment.Visible = false;


            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }

        }


        /// <summary>
        /// Gets the discount plans.
        /// </summary>
        /// <returns></returns>
        [System.Web.Services.WebMethod(EnableSession = true)]
        static List<DiscountPlan> GetDiscountPlans(int payMethodID)
        {
            List<DiscountPlan> plans = new List<DiscountPlan>();
            IBilling bMGr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling,BusinessProcess.Billing");
            plans = bMGr.GetDiscountPlans(null, payMethodID, DateTime.Today, true);

            return plans;
            
        }
        /// <summary>
        /// Gets the formatted discount plans.
        /// </summary>
        /// <param name="payMethodID">The pay method identifier.</param>
        /// <returns></returns>
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<KeyValuePair<string, string>> GetFormattedDiscountPlans(string selectedpayMethod)
        {
            int payMethodID = int.Parse(selectedpayMethod.Split('|')[0]);
            List<DiscountPlan> plans = GetDiscountPlans(payMethodID);
            var g = new KeyValuePair<string, string>();
            List<KeyValuePair<string, string>> L = new List<KeyValuePair<string, string>>();
            foreach (DiscountPlan p in plans)
            {
                g = new KeyValuePair<string, string>(
                    string.Format("{0}:{1}:{2}:{3}", 
                    p.PlanId, 
                    p.Rate.ToString(), 
                    p.StartDate.ToString("dd-MMM-yyyy"),
                    p.EndDate.HasValue? p.EndDate.Value.ToString("dd-MMM-yyyy") : "00-000-0000"),
                    p.Name);
                L.Add(g);
            };

            return L;
        }
        /// <summary>
        /// Initializes the items to pay.
        /// </summary>
        /// <returns></returns>
        private List<BillItem> initializeItemsToPay()
        {

            List<BillItem> BillItemsToPay = new List<BillItem>();
            //if (this.BillPayOption == BillPaymentOptions.SelectItem || !this.HasTransaction)
            //{
                foreach (GridViewRow gridRow in this.grdPayItems.Rows)
                {
                    if (gridRow.RowType == DataControlRowType.DataRow)
                    {
                       // CheckBox chk = gridRow.FindControl("chkBxItem") as CheckBox;

                       // if (chk != null && chk.Checked)
                       // {
                            int billItemId = int.Parse(grdPayItems.DataKeys[gridRow.RowIndex].Values["billitemid"].ToString());
                            int itemId = int.Parse(grdPayItems.DataKeys[gridRow.RowIndex].Values["ItemId"].ToString());
                            decimal amount = decimal.Parse(grdPayItems.DataKeys[gridRow.RowIndex].Values["amount"].ToString());
                            DateTime billItemDate = DateTime.Parse(grdPayItems.DataKeys[gridRow.RowIndex].Values["billitemdate"].ToString());
                            string itemName = gridRow.Cells[1].Text.Trim();
                            BillItemsToPay.Add(new BillItem() { BillItemId = billItemId, Amount = amount, ItemId = itemId, ItemName = itemName });
                       // }
                    }
              //  };
                };
            //return theDT;
            return BillItemsToPay;
        }
        /// <summary>
        /// Initializes the pay payment object.
        /// </summary>
        /// <returns></returns>
        private DataTable initializePayPaymentObject()
        {

            DataTable theDT = new DataTable();
            theDT.Columns.Add("BillID", typeof(int));
            theDT.Columns.Add("LocationID", typeof(int));
            theDT.Columns["LocationID"].DefaultValue = this.BillLocationId;
            theDT.Columns.Add("PaymentType", typeof(int));
            theDT.Columns.Add("PaymentName", typeof(string));
            theDT.Columns.Add("RefNo", typeof(string));
            theDT.Columns.Add("Amount", typeof(decimal));
            theDT.Columns["Amount"].DefaultValue = 0;
            theDT.Columns.Add("TenderedAmount", typeof(decimal));
            theDT.Columns["TenderedAmount"].DefaultValue = 0;
            theDT.Columns.Add("IsDeposit", typeof(bool));
            theDT.Columns["IsDeposit"].DefaultValue = false;

            return theDT;
        }

        #endregion

        #region Format Render Display
        /// <summary>
        /// Displays the mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        void DisplayMode(string mode)
        {
            if (mode == "PAY")
            {
                HDisplay.Value = "PAY";
                panelSummarry.Visible = false;
                panelPayment.Visible = true;
            }
            else
            {
                HDisplay.Value = "ITEMS";
                panelSummarry.Visible = true;
                panelPayment.Visible = false;
            }
        }
        /// Sets the style.
        /// </summary>
        void SetStyle()
        {
            HtmlGenericControl facilityBanner = (Master.FindControl("facilityBanner") as HtmlGenericControl);
            if (facilityBanner != null) facilityBanner.Style.Add("display", "inline");

            HtmlGenericControl patientBanner = (Master.FindControl("patientBanner") as HtmlGenericControl);
            if (patientBanner != null) patientBanner.Style.Add("display", "none");
            HtmlGenericControl username1 = (Master.FindControl("username1") as HtmlGenericControl);
            if (username1 != null)
                username1.Attributes["class"] = "usernameLevel1"; //Style.Add("display", "inline");
            HtmlGenericControl currentdate1 = (Master.FindControl("currentdate1") as HtmlGenericControl);
            if (currentdate1 != null) currentdate1.Attributes["class"] = "currentdateLevel1"; //Style.Add("display", "inline");
            HtmlGenericControl facilityName = (Master.FindControl("facilityName") as HtmlGenericControl);
            if (facilityName != null) facilityName.Attributes["class"] = "facilityLevel1"; //Style.Add("display", "inline");
            //userNameLevel2.Style.Add("display", "none");
            //currentDateLevel2.Style.Add("display", "none");
            HtmlGenericControl imageFlipLevel2 = (Master.FindControl("imageFlipLevel2") as HtmlGenericControl);
            if (imageFlipLevel2 != null) imageFlipLevel2.Style.Add("display", "none");
            //facilityLevel2.Style.Add("display", "none");
            HtmlGenericControl level2Navigation = (Master.FindControl("level2Navigation") as HtmlGenericControl);
            if (level2Navigation != null) level2Navigation.Style.Add("display", "none");
        }
        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void showErrorMessage(ref Exception ex)
        {
            this.isError = true;
            if (this.Debug)
            {
                lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            }
            else
            {
                lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team";
                this.isError = this.divError.Visible = true;
                Exception lastError = ex;


                lastError.Data.Add("Domain", "Pay Bill Page");
                Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                logger.LogError(ex);

            }

        }
        /// <summary>
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        void NotifyAction(string strMessage, string strTitle, bool errorFlag)
        {
            string redirectUrl = "window.location.href='./frmBilling_PayBill.aspx';";
            IQCareMsgBox.NotifyAction(strMessage, strTitle, errorFlag,this,redirectUrl);
            //lblNoticeInfo.Text = strMessage;
            //lblNotice.Text = strTitle;
            //lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
            //lblNoticeInfo.Font.Bold = true;
            //imgNotice.ImageUrl = (errorFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
            //this.notifyPopupExtender.Show();
        }
        /// <summary>
        /// Gets the allow select.
        /// </summary>
        /// <value>
        /// The allow select.
        /// </value>
        protected string AllowSelect
        {
            get
            {
                //if (HDisplay.Value == "PAY")
                //    return "none";
                //if (!this.HasTransaction)
                //    return "";
                //if (this.HasTransaction && this.BillPayOption == BillPaymentOptions.FullBill)
                //    return "none";

                //return "";
                return "none";
            }
        }

        #endregion

        #region Event Handlers
        //void GetCheckedItemsTotal()
        //{
        //    decimal _total = 0;
        //    int selectedCount = 0;

        //    foreach (GridViewRow gridRow in this.grdPayItems.Rows)
        //    {
        //        if (gridRow.RowType == DataControlRowType.DataRow)
        //        {
        //            CheckBox chk = gridRow.FindControl("chkBxItem") as CheckBox;

        //            if (chk != null && chk.Checked && chk.Enabled)
        //            {
        //                decimal amount = decimal.Parse(grdPayItems.DataKeys[gridRow.RowIndex].Values["amount"].ToString());
        //                _total += amount;
        //                selectedCount += 1;
        //            }
        //        }
        //    }
        //    lblTotal.Text = _total.ToString();
        //}
        /// <summary>
        /// Handles the CheckedChanged event of the chkBxItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void chkBxItem_CheckedChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the CheckedChanged event of the chkBxHeader control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void chkBxHeader_CheckedChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the Click event of the btn_close control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btn_close_Click(object sender, EventArgs e)
        {
            base.Session.Remove("ItemsToPay");
            base.Session.Remove("paymentInformation");
            base.Session.Remove("totalbill");
            base.Session.Remove("transactionID");
            base.Session.Remove("BillID");
            string theUrl = "./frmBilling_ClientBill.aspx";
            Response.Redirect(theUrl, false);
        }

        /// <summary>
        /// Handles the RowDataBound event of the gridBillTransaction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridBillTransaction_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow row = ((DataRowView)e.Row.DataItem).Row;
                string tranStatus = row["TransactionStatus"].ToString();

                string _transactionID = row["TransactionID"].ToString();

                Button printButton = e.Row.FindControl("receiptPrint") as Button;
                if (printButton != null)
                {
                    if (tranStatus == "Paid")
                    {
                        string urlParam = string.Format("openReportPage('./frmBilling_Reciept.aspx?ReceiptTrxCode={0}&RePrint=false');return false;", _transactionID);

                        printButton.OnClientClick = urlParam;
                    }
                    else
                    {
                        printButton.Enabled = false;
                    }
                }

                if (tranStatus != "Paid")
                {
                    Button reverseButton = e.Row.FindControl("buttonReverse") as Button;

                    if (reverseButton != null)
                        reverseButton.Enabled = false;


                    if (printButton != null)
                        printButton.Enabled = false;
                }
            }
        }
        /// <summary>
        /// Determines whether the specified payment status is reversible.
        /// </summary>
        /// <param name="paymentStatus">The payment status.</param>
        /// <param name="reversible">The reversible.</param>
        /// <returns></returns>
        protected string IsReversible(object paymentStatus, object reversible)
        {
            if (paymentStatus.ToString() != "Paid" || !Convert.ToBoolean(reversible))
                return "none";
            else return "";
        }
        /// <summary>
        /// Handles the RowCommand event of the gridBillTransaction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridBillTransaction_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int _transactionID;
            int index;

            if (e.CommandName == "Reverse")
            {

                //index = Convert.ToInt32(e.CommandArgument);
                //  GridViewRow gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                GridViewRow gvr = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                GridView transactionGrid = (GridView)gvr.NamingContainer;
                index = gvr.RowIndex;
                labelReceipt.Text = gvr.Cells[0].Text;
                //GridViewRow row = (transactionGrid.Rows[index]);
                transactionGrid.SelectedIndex = index;
                _transactionID = int.Parse(transactionGrid.SelectedDataKey.Values["TransactionID"].ToString());
                HTransactionID.Value = _transactionID.ToString();
                this.mpeReverse.Show();
                return;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnOkAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOkAction_Click(object sender, EventArgs e)
        {
            this.Init_page();
        }
        /// <summary>
        /// Handles the Click event of the btnActionOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void RequestReversal(object sender, EventArgs e)
        {
            if (textReason.Text.Trim() != "")
            {
                try
                {
                    IBilling bMrg = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling,BusinessProcess.Billing");
                    List<int> ItemToReverse = new List<int>();
                    bMrg.RequestTransactionReversal(Convert.ToInt32(HTransactionID.Value), this.UserId, textReason.Text.Trim(), DateTime.Now, ItemToReverse);
                    mpeReverse.Hide();
                    this.NotifyAction("Request to Reverse Receipt Number " + labelReceipt.Text + " Successfully ..", string.Format("Reverse {0} ", labelReceipt.Text), false);
                    this.PopulateBillTransactions();
                }
                catch (Exception ex)
                {
                    this.showErrorMessage(ref ex);
                    this.NotifyAction("Request to Reverse Receipt Number " + labelReceipt.Text + " Failed ..<br />" + ex.Message, string.Format("Failed Reverse {0} ", labelReceipt.Text), true);
                }

            };

        }
        /// <summary>
        /// Handles the Click event of the buttonReversalOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonReversalOK_Click(object sender, EventArgs e)
        {
            this.PopulateBillTransactions();
        }
        /// <summary>
        /// Handles the RowDataBound event of the grdPayItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdPayItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRow dataRow = ((DataRowView)e.Row.DataItem).Row;
                GridViewRow gridRow = e.Row;
                bool paid = (dataRow["PaymentStatus"].ToString() == "1");
                ((CheckBox)gridRow.FindControl("chkBxItem")).Checked = !paid;
                ((CheckBox)gridRow.FindControl("chkBxItem")).Enabled = !paid;
            }
        }
      
        /// <summary>
        /// Buttons the proceed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonProceed_Click(object sender, EventArgs e)
        {
            string selectedMethod = ddlPaymentMode.SelectedValue;
            string[] method = selectedMethod.Split('|');
            string paymethodName = ddlPaymentMode.SelectedItem.Text;

            //List<IQCare.CustomReportConfig.PayMethodElement> lx =
            //       IQCare.CustomReportConfig.PaymentMethodConfig.PayMethodAvailable;
            //PayMethodElement selectedElement = IQCare.CustomReportConfig.PaymentMethodConfig.GetPayMethodConfigElement(paymethod);

            string controlName = method[1].ToString();
            //PaymentConfigHelper.GetHandlerNameByPayMethod(paymethod);
            //if (string.IsNullOrEmpty(selectedHandler))
            //{
            //    selectedHandler = this.DefaultPaymentHandler;
            //    //return;
            //}
            //string controlName = PaymentConfigHelper.HandlerControlName(selectedHandler);
            if (string.IsNullOrEmpty(controlName)) {
               return;
            }
                ;
            controlName = ("~/billing/" + controlName);

            PaymentMethod payMode = new PaymentMethod() { Id = Convert.ToInt32(method[0]), Name = paymethodName, ControlName = controlName };

            DiscountPlan plan = null;
            string g = ddlDiscountPlan.SelectedValue;
            string SelectedPlan = HDiscountPlan.Value;
            if (SelectedPlan != "")
            {
                ListItem item = ddlDiscountPlan.SelectedItem;
                string[] parts = SelectedPlan.Split(':');

                decimal rate = decimal.Parse(parts[1]);
                int planId = int.Parse(parts[0]);
                DateTime dtStart = DateTime.Parse(parts[2]);
                DateTime? dtEnd =null;
                if (parts[3] != "00-000-0000")
                {
                    dtEnd = DateTime.Parse(parts[3]);
                }

                plan = new DiscountPlan();
                plan.PlanId = planId;
                plan.Name = HDiscountPlanText.Value;
                plan.Rate = rate;
                plan.StartDate = dtStart;
                plan.EndDate = dtEnd;
                //plan.DiscountedPayMethod = payMode;
                plan.Active = true;

                //payBillControl.SelectedDiscountPlan = plan;
            }


            decimal SelectedAmountToPay = 0.0M;
            foreach (GridViewRow gridRow in this.grdPayItems.Rows)
            {
                if (gridRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = gridRow.FindControl("chkBxItem") as CheckBox;
                    decimal amount = decimal.Parse(grdPayItems.DataKeys[gridRow.RowIndex].Values["amount"].ToString());
                    if (chk != null && chk.Checked)
                    {

                        SelectedAmountToPay += amount;
                    }
                }
            };
            //May132015
            if (SelectedAmountToPay == 0 || SelectedAmountToPay > this.AmountDue) SelectedAmountToPay = this.AmountDue;

            Hashtable ht = new Hashtable()
                {
                    {"ControlName",controlName},
                    {"AmountDue",this.AmountDue},
                    {"BillAmount", this.BillAmount},
                    {"BillLocationId",Convert.ToInt32(HLocationID.Value)},
                    {"PatientID",this.PatientId},
                    {"UserID",this.UserId},
                    {"BillID",this.BillId},
                    {"PaymentMode",payMode} ,//new PaymentMethod() { ID = Convert.ToInt32(ddlPaymentMode.SelectedValue), Name = paymethod }},
                    {"DiscountPlan", plan},
                    {"AmountToPay",SelectedAmountToPay}

                };

            Session["PayControl"] = ht;
            this.AddPayControl(ht);

          //  ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Highlightor", "highlight();", true);
        }
        /// <summary>
        /// Handles the PreRender event of the grdPayItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void grdPayItems_PreRender(object sender, EventArgs e)
        {

        }
        #endregion

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlPaymentMode control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // this.PopulateDiscountPlans();
        }

        protected void ddlDiscountPlan_SelectedIndexChanged(object sender, Telerik.Web.UI.RadComboBoxSelectedIndexChangedEventArgs e)
        {

        }

        /// <summary>
        /// Handles the ItemDataBound event of the ddlDiscountPlan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Telerik.Web.UI.RadComboBoxItemEventArgs"/> instance containing the event data.</param>
        protected void ddlDiscountPlan_ItemDataBound(object sender, Telerik.Web.UI.RadComboBoxItemEventArgs e)
        {
            try
            {
                e.Item.Attributes.Clear();
                DiscountPlan plan = e.Item.DataItem as DiscountPlan;
                e.Item.Text = plan.Name;
                e.Item.Attributes.Add("rate", plan.Rate.ToString("{0:N}"));
                e.Item.Attributes.Add("startdate", plan.StartDate.ToString("dd-MMM-yyyy"));
                e.Item.Attributes.Add("enddate", plan.EndDate.HasValue ? plan.EndDate.Value.ToString("dd-MMM-yyyy") : "00-000-0000");
                //plan.EndDate.ToString("dd-MMM-yyyy"));
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }



    }
}
