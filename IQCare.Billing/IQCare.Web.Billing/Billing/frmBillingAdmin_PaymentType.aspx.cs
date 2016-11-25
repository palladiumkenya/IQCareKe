using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Entities.Billing;
using Interface.Billing;
using IQCare.Web.UILogic;

namespace IQCare.Web.Billing
{
    public partial class ManagePaymentType : System.Web.UI.Page
    {
     
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!CurrentSession.Current.HasFeaturePermission(ApplicationAccess.BillingFeature.Configuration))
                {
                    CurrentSession.Logout();
                    string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                    //Response.Redirect(theUrl);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
                if (Page.IsPostBack != true)
                {
                    CurrentSession.Current.ResetCurrentPatient();
                    lblHeader.Text = "Payment Type";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Billing Administration >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Payment Type";
                    this.PopulatePaymentType();
                }
                btnCancel.OnClientClick = "javascript:window.location.href='./Home.aspx';return false;";
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanging event of the grdCustom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSelectEventArgs"/> instance containing the event data.</param>
        protected void gridPaymentType_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = gridPaymentType.PageIndex;
            int thePageSize = gridPaymentType.PageSize;

            GridViewRow gridRow = gridPaymentType.Rows[e.NewSelectedIndex];
            gridPaymentType.SelectedIndex = e.NewSelectedIndex;
            errorLabel.Text = "";

            //this.SerialNumber = gridRow.Cells[0].Text.Trim();
            Label lblStatus = gridRow.FindControl("labelStatus") as Label;
            string stStatus = lblStatus.Text.Trim().ToUpper();
            rblStatus.SelectedIndex = stStatus == "ACTIVE" ? 0 : 1;
            this.CurrentPayMethodId = this.gridPaymentType.SelectedDataKey["ID"].ToString();
            this.buttonSubmit.Text = "Update";
            this.buttonSubmit.CommandName = "UPDATE";

            Label lblHandler = gridRow.FindControl("labelHandler") as Label;
            Label lblDesc = gridRow.FindControl("labelDescription") as Label;
            if (lblHandler != null)
            {
                this.CurrentPlugin = lblHandler.Text.Trim();
            }

            this.CurrentPayMethodName = this.gridPaymentType.SelectedDataKey["Name"].ToString();
            if (lblDesc != null)
            {
                textDescription.Text = lblDesc.Text;
                    //PaymentConfigHelper.PayElementDescription(this.CurrentPayMethodName);
            }
         //   this.prevTypeCode.Value = this.textPaymentTypeCode.Text = PaymentConfigHelper.PayElementCode(this.CurrentPayMethodName);

            this.paymentTypePopup.Show();
        }

        /// <summary>
        /// Handles the Sorting event of the grdCustom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void gridPaymentType_Sorting(object sender, GridViewSortEventArgs e)
        {
            //Init_Form(); 
            GridView theGD = (GridView)sender;
            IQCareUtils SortManager = new IQCareUtils();
            DataView theDV;
            if (ViewState["gridSortDirection"].ToString() == "Asc")
            {
                theDV = SortManager.GridSort((DataTable)this.ViewState["gridSource"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
                ViewState["gridSortDirection"] = "Desc";
            }
            else
            {
                theDV = SortManager.GridSort((DataTable)this.ViewState["gridSource"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
                ViewState["gridSortDirection"] = "Asc";
            }

            theGD.Columns.Clear();
            theGD.DataSource = theDV;

        }

        /// <summary>
        /// Handles the RowDataBound event of the grdCustom control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridPaymentType_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PaymentMethod rowItem = ((PaymentMethod)e.Row.DataItem);
                if (rowItem.Locked)
                {

                    e.Row.Attributes.Add("onmouseover", "this.style.cursor='help';");
                    string theScript = "alert('This is a system generated method and cannot be modified.');return false;";
                    e.Row.Attributes.Add("onclick", theScript);

                }
                else
                {
                    e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';");
                    e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                    // Page.ClientScript.GetPostBackClientHyperlink(grdPatienBill, "Select$" + e.Row.RowIndex);
                    e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackClientHyperlink(gridPaymentType, "Select$" + e.Row.RowIndex.ToString()));
                }

            }
        }
        /// <summary>
        /// Clears the controls.
        /// </summary>
        void ClearControls()
        {
            this.CurrentPlugin = "";
            this.CurrentPayMethodId = "-1";
            errorLabel.Text = this.textDescription.Text = this.textPaymentTypeCode.Text = this.textPaymentTypeName.Text = "";
            this.prevPluginName.Value = this.prevPaymentName.Value = this.prevTypeCode.Value = "";
            rblStatus.SelectedIndex = 0;
            this.buttonSubmit.Text = "Save";
            this.buttonSubmit.CommandName = "NEW";
            
        }
        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            this.ClearControls();
            this.paymentTypePopup.Show();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl = "frmAdmin_PMTCT_CustomItems.aspx";
            Response.Redirect(theUrl);
        }
        #region "User Functions"

        


        /// <summary>
        /// Populates the type of the payment.
        /// </summary>
        void PopulatePaymentType()
        {
            IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
            List<PaymentMethod> paymentMethods = BillingManager.GetPaymentMethods("");


          
                gridPaymentType.DataSource = paymentMethods;
                gridPaymentType.DataBind();
            


        }

        /// <summary>
        /// Handles the Click event of the buttonSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonSubmit_Click(object sender, EventArgs e)
        {

            string paymentName = textPaymentTypeName.Text;


            if (textPaymentTypeName.Text == "")
            {
                this.NotifyAction("Name of the payment method is required", "Validation", true);
                this.paymentTypePopup.Show();
                return;
            }
            string pluginName = "PayBillCorporate.ascx";// ddlHandler.SelectedItem.Value;

         
            if (buttonSubmit.CommandName == "UPDATE")
            {
                pluginName = CurrentPlugin;
            }
            try
            {


               

                IBilling BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");

                PaymentMethod thisMethod = new PaymentMethod() { 
                    Name = paymentName, 
                    Locked = false, 
                    Active = rblStatus.SelectedValue == "1" , 
                    MethodDescription = textDescription.Text,
                    Credit = true,
                    ControlName=pluginName 
                };
                if (buttonSubmit.CommandName == "UPDATE")
                {
                    thisMethod.Id = Convert.ToInt32(this.CurrentPayMethodId);
                }

                BillingManager.SavePaymentMethod(thisMethod, this.UserId);

                if (buttonSubmit.CommandName == "NEW")
                {
                    this.NotifyAction("New payment method addedd successfully", string.Format("{0} {1} ", "Adding Payment method", paymentName), false);

                }
                else
                {
                    this.NotifyAction("Payment method updated successfully", string.Format("{0} {1} ", "Updating Payment method", paymentName), false);

                }
                this.PopulatePaymentType();
                return;
            }
            catch (Exception ex)
            {
                this.NotifyAction(ex.Message.ToString(), "Error Occured", true);
                return;

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
            IQCareMsgBox.NotifyAction(strMessage,strTitle,errorFlag,this,"");
            //lblNoticeInfo.Text = strMessage;
            //lblNotice.Text = strTitle;
            //lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
            //lblNoticeInfo.Font.Bold = true;
            //imgNotice.ImageUrl = (errorFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
            //this.notifyPopupExtender.Show();
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="BillingAdmin_PaymentType"/> is debug.
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
        void showErrorMessage(ref Exception ex)
        {
            //this.isError = true;
            if (this.Debug)
            {
                lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            }
            else
            {
                lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team";
                this.divError.Visible = true;
                Exception lastError = ex;
                lastError.Data.Add("Domain", "Payment method administration");
                Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                logger.LogError(ex);

            }

        }
        /// <summary>
        /// Gets the current pay method identifier.
        /// </summary>
        /// <value>
        /// The current pay method identifier.
        /// </value>
        string CurrentPayMethodId
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
        /// Gets the name of the current pay method.
        /// </summary>
        /// <value>
        /// The name of the current pay method.
        /// </value>
        string CurrentPayMethodName
        {
            get
            {
                return this.prevPaymentName.Value.Trim();
            }
            set
            {
                this.prevPaymentName.Value = this.textPaymentTypeName.Text = value;
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
        /// Gets the current handler.
        /// </summary>
        /// <value>
        /// The current handler.
        /// </value>
        string CurrentPlugin
        {
            get
            {
                return prevPluginName.Value.Trim();
            }
            set
            {
                prevPluginName.Value = value;
                //ddlHandler.ClearSelection();
                //if (value.ToString() == "-1")
                //{
                //    ddlHandler.SelectedIndex = -1;
                //}
                //else
                //{
                //    ListItem item = ddlHandler.Items.FindByValue(value);
                //    if (item != null)
                //    {
                //        item.Selected = true;
                //    }
                //}
            }
        }

        #endregion

        /// <summary>
        /// Handles the Click event of the btnOkAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOkAction_Click(object sender, EventArgs e)
        {
            //this.notifyPopupExtender.Hide();
        }   
    }
}