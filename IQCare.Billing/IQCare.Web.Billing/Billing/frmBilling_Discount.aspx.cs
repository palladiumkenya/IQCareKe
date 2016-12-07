using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using System.Data;
using Application.Presentation;
using System.Configuration;
using Interface.Billing;
using Entities.Billing;
using IQCare.Web.UILogic;

namespace IQCare.Web.Billing
{
    public partial class Discount : System.Web.UI.Page
    {
        AuthenticationManager Authentication = new AuthenticationManager();
        bool isError;

        /// <summary>
        /// Gets or sets the current discount plan identifier.
        /// </summary>
        /// <value>
        /// The current discount plan identifier.
        /// </value>
        string CurrentDiscountPlanId
        {
            get
            {
                return currentID.Value.Trim();
            }
            set
            {
                this.currentID.Value = value;
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
                return CurrentSession.Current.User.Id;
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
            IQCareMsgBox.NotifyAction(strMessage, strTitle, errorFlag, this, "");
            //lblNoticeInfo.Text = strMessage;
            //lblNotice.Text = strTitle;
            //lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
            //lblNoticeInfo.Font.Bold = true;
            //imgNotice.ImageUrl = (errorFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
            //this.notifyPopupExtender.Show();
        }
        /// <summary>
        /// Clears the controls.
        /// </summary>
        void ClearControls()
        {

         this.textRate.Text =   labelPaymentMethod.Text = errorLabel.Text = this.textDiscountName.Text = this.textEndDate.Text = this.textStartDate.Text = "";
            this.prevDiscountName.Value = this.prevPaymentID.Value = this.prevRate.Value = "";
            rblStatus.SelectedIndex = 0;
            this.buttonSubmit.Text = "Save";
            this.buttonSubmit.CommandName = "NEW";
            ddlPaymentMode.SelectedIndex = -1;
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="frmBilling_Discount"/> is debug.
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
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void showErrorMessage(ref Exception ex)
        {
            this.isError = true;
            if (this.Debug)
            {
                lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            }
            else
            {
                lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team.  ";
                this.isError = this.divError.Visible = true;
                Exception lastError = ex;
                lastError.Data.Add("Domain", "Discounts Plans");
                try
                {
                    Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                    logger.LogError(ex);
                }
                catch
                {

                }
            }
            //notifyPopupExtender.Hide();

        }
        /// <summary>
        /// Validates the text box.
        /// </summary>
        /// <param name="inputbox">The inputbox.</param>
        /// <returns></returns>
        private bool ValidateTextBox(TextBox inputbox)
        {
            if (inputbox.Text.Trim() == "" || inputbox.Text.Trim() == "0")
            {
                inputbox.BorderColor = System.Drawing.Color.Red;

                return false;
            }
            inputbox.BorderColor = System.Drawing.Color.White;

            return true;

        }
        /// <summary>
        /// Validates the drop down list.
        /// </summary>
        /// <param name="inputbox">The inputbox.</param>
        /// <returns></returns>
        private bool ValidateDropDownList(DropDownList inputbox)
        {
            if (inputbox.SelectedValue == "" || inputbox.SelectedIndex == -1)
            {
                inputbox.BorderColor = System.Drawing.Color.Red;
                inputbox.BackColor = System.Drawing.Color.Orange;

                return false;
            }
            inputbox.BorderColor = System.Drawing.Color.White;
            inputbox.BackColor = System.Drawing.Color.White;

            return true;

        }
        /// <summary>
        /// Injects the script.
        /// </summary>
        void InjectScript()
        {
            string scriptPastDates = @" function disable_past_dates(sender,args){
                                                    var senderDate = new Date(sender._selectedDate);senderDate.setHours(0,0,0,0)
                                                    var nowDate =new Date();  nowDate.setHours(0,0,0,0);
                                                    if(senderDate < nowDate){
                                                        alert('You cannot select a day before today'); 
                                                        sender._selectedDate=new Date();sender._textbox.set_Value(sender._selectedDate.format(sender._format));    }}";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "_pastdates", scriptPastDates, true);
        }
        /// <summary>
        /// Populates the discount plans.
        /// </summary>
        void PopulateDiscountPlans()
        {
            try
            {
                IBilling bMGr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
                List<DiscountPlan> _plans = bMGr.GetDiscountPlans(null, null, null, true);
                gridDiscountPlan.DataSource = _plans;
                gridDiscountPlan.DataBind();
            }
            catch (Exception ex)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = ex.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }
        /// <summary>
        /// Populates the payment methods.
        /// </summary>
        void PopulatePaymentMethods(string selectedValue = "")
        {
            try
            {
                if (ddlPaymentMode.Items.Count < 2)
                {
                    IBilling bMGr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
                    List<PaymentMethod> paymentMethods = bMGr.GetPaymentMethods("");
                    foreach (PaymentMethod me in paymentMethods)
                    {

                        ddlPaymentMode.Items.Add(new ListItem(me.Name, me.Id.Value.ToString()));
                    }
                    if (ddlPaymentMode.Items.Count > 1)
                    {
                        ddlPaymentMode.Items.Insert(0, new ListItem("Select...", ""));
                    }
                }
                if (selectedValue != "")
                {
                    ddlPaymentMode.Items.FindByValue(selectedValue).Selected = true;
                }
            }
            catch (Exception ex)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = ex.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bool hasPerm = (CurrentSession.Current.HasFeaturePermission(ApplicationAccess.BillingFeature.Configuration));
                CurrentSession.Current.ResetCurrentPatient();
                if (!hasPerm)
                {
                    CurrentSession.Logout();
                    string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                    //Response.Redirect(theUrl);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
                if (Page.IsPostBack != true)
                {

                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Billing Administration >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Discount Plans";
                    this.PopulateDiscountPlans();
                }
                btnCancel.OnClientClick = "javascript:window.location.href='../frmFacilityHome.aspx';return false;";
                this.InjectScript();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {
            if (buttonSubmit.CommandName == "NEW")
            {
                try
                {
                    bool valid = ValidateTextBox(textDiscountName);

                    if (!valid)
                    {
                        this.NotifyAction("Name of the discount plan is required", "Validation", true);
                        this.discountPopup.Show();
                        return;
                    }
                    string strdiscountName = textDiscountName.Text;
                    valid = ValidateTextBox(textDiscountName);

                    if (!valid)
                    {
                        this.NotifyAction("Discount Rate is required", "Validation", true);
                        this.discountPopup.Show();
                        return;
                    }
                    decimal _rate = decimal.Parse(textRate.Text.Trim()) / 100.0M;
                 /*   valid = ValidateDropDownList(ddlPaymentMode);
                    if (!valid)
                    {

                        this.NotifyAction("Please select the payment method for this discount", "Validation", true);
                        this.discountPopup.Show();
                        return;
                    }

                    string _paymentName = ddlPaymentMode.SelectedItem.Text;
                    int _paymentID = int.Parse(ddlPaymentMode.SelectedValue);
                    */
                    valid = ValidateTextBox(textStartDate);

                    if (!valid)
                    {
                        this.NotifyAction("Discount StartDate is required", "Validation", true);
                        this.discountPopup.Show();
                        return;
                    }

                    DateTime strStartDate = DateTime.Parse(textStartDate.Text.Trim()); ;
                    DateTime? strEndDate = null;
                    if (textEndDate.Text != "" )
                    {
                        valid = ValidateTextBox(textEndDate);

                        if (!valid)
                        {
                            this.NotifyAction("Discount EndDate is required", "Validation", true);
                            this.discountPopup.Show();
                            return;
                        }
                         strEndDate = DateTime.Parse(textEndDate.Text.Trim());
                    }
                    if (rblStatus.SelectedIndex == -1)
                    {
                        this.NotifyAction("Discount Plan Status is required", "Validation", true);
                        this.discountPopup.Show();
                        return;
                    }
                    bool Active = rblStatus.SelectedValue == "1";

                    IBilling bMGr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");

                    DiscountPlan thePlan = new DiscountPlan()
                    {
                        Name = strdiscountName,
                        Active = Active,
                        EndDate = strEndDate,
                        StartDate = strStartDate,
                        Rate = _rate
                        //DiscountedPayMethod = null}new PaymentMethod() { ID = _paymentID, Name = _paymentName }
                      //  DiscountedPayMethod = null
                    };
                    bMGr.AddDiscountPlan(thePlan, this.UserId);

                    this.NotifyAction("New discount plan  addedd successfully", string.Format("{0} {1} ", "Adding discount plan", strdiscountName), false);
                }
                catch (Exception ew)
                {
                    this.NotifyAction(ew.Message, "Error", true);

                }
            }
            else if (buttonSubmit.CommandName == "UPDATE")
            {

            }
        }

        /// <summary>
        /// Handles the Click event of the btnOkAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOkAction_Click(object sender, EventArgs e)
        {
            this.PopulateDiscountPlans();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the gridDiscountPlan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void gridDiscountPlan_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = gridDiscountPlan.PageIndex;
            int thePageSize = gridDiscountPlan.PageSize;

            GridViewRow gridRow = gridDiscountPlan.Rows[e.NewSelectedIndex];
            gridDiscountPlan.SelectedIndex = e.NewSelectedIndex;
            errorLabel.Text = "";

            //getstatus
            Label lblStatus = gridRow.FindControl("labelStatus") as Label;
            string stStatus = lblStatus.Text.Trim().ToUpper();
            rblStatus.SelectedIndex = stStatus == "ACTIVE" ? 0 : 1;
            //GetID
            this.CurrentDiscountPlanId = this.gridDiscountPlan.SelectedDataKey["PlanID"].ToString();
            this.buttonSubmit.Text = "Update";
            this.buttonSubmit.CommandName = "UPDATE";

            //getName
            string _name = gridRow.Cells[0].Text.Trim();
            textDiscountName.Text = _name;
            //getDates
            string startDate = (gridRow.Cells[2].Text.Trim());
            textStartDate.Text = startDate;
            string endDate = (gridRow.Cells[3].Text.Trim());
            textEndDate.Text = endDate;
            //getRate
            string _rate = (gridRow.Cells[1].Text);
            textRate.Text = _rate;
            prevRate.Value = _rate;
            //getpaymethod
            //Label lblPayName = gridRow.FindControl("labelPayMethod") as Label;
            //string _paymentName = lblPayName.Text.Trim();
            //labelPaymentMethod.Text = _paymentName;

            //HiddenField HPayID = gridRow.FindControl("hPayMethodID") as HiddenField;
            //string _paymentID = HPayID.Value;
            //prevPaymentID.Value = _paymentID;
            //this.PopulatePaymentMethods(_paymentID);


            this.discountPopup.Show();
        }

        /// <summary>
        /// Handles the RowDataBound event of the gridDiscountPlan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridDiscountPlan_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';");
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                // Page.ClientScript.GetPostBackClientHyperlink(grdPatienBill, "Select$" + e.Row.RowIndex);
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackClientHyperlink(gridDiscountPlan, "Select$" + e.Row.RowIndex.ToString()));


            }
        }

        /// <summary>
        /// Handles the RowCommand event of the gridDiscountPlan control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridDiscountPlan_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.ClearControls();
            this.PopulatePaymentMethods();
            this.discountPopup.Show();
        }
    }
}