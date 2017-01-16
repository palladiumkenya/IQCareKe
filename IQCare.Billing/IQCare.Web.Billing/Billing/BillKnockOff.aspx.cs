using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using IQCare.Billing.Logic;
using IQCare.Web.UILogic;
using Entities.Billing;
using System.Data;
using System.Collections.Specialized;
using AjaxControlToolkit;
namespace IQCare.Web.Billing
{
    public partial class BillKnockOff : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TabContainer1.ActiveTabIndex = 0;
            }
            else return;
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            btnCancel.OnClientClick = "javascript:window.location='Home.aspx';return false;";
        }
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            this.GetContextTabData(TabContainer1.ActiveTabIndex);
        }

        private void GetContextTabData(int tabIndex)
        {
            CurrentSession session = CurrentSession.Current;
            if (tabIndex == 0)
            {
                txtDetails.Text = txtAmount.Text = txtDate.Text = txtReference.Text = txtDate.Text = "";
                ddlVoucherType.SelectedIndex = 0;
                return;
            }
            if (tabIndex == 1)
            {
               
                txtKOFrom.Text = DateTime.Now.AddMonths(-1).Date.ToString("dd-MMM-yyyy");
                txtKOTo.Text = DateTime.Today.ToString("dd-MMM-yyyy");
                KnockOffServices KOServices = new KnockOffServices();
                List<PaymentVoucher> voucher = KOServices.GetOpenVoucher(session);
                ddlKOVoucher.SelectedIndex = ddlPTKO.SelectedIndex = -1;
                ddlKOVoucher.Items.Clear();
                ddlPTKO.Items.Clear();
                ddlKOVoucher.DataSource = voucher;
                ddlKOVoucher.DataValueField = "Id";
                ddlKOVoucher.DataTextField = "DisplayName";
                ddlKOVoucher.DataBind();
                ddlKOVoucher.Items.Insert(0, new ListItem("Select..", ""));
                //voucher.Where(v => v.AmountAvailable > 0.0D).ToList().ForEach(pv =>
                //{
                //    ListItem item = new ListItem(string.Format("{0} {1} {2} ({3})", pv.VoucherType, pv.ReferenceId, pv.VoucherDate.ToString("dd-MMM-yyyy"), pv.AmountAvailable), pv.Id.ToString());
                //    item.Attributes.Add("amt", pv.AmountUsed.ToString());
                //    ddlKOVoucher.Items.Add(item);
                //});
              
                PaymentServices pyService = new PaymentServices();
                List<PaymentMethod> payMethods = pyService.GetCreditPaymentMethod(session);


                ddlPTKO.DataValueField = "Id";
                ddlPTKO.DataTextField = "Name";
                ddlPTKO.DataSource = payMethods;
                ddlPTKO.DataBind();
                ddlPTKO.Items.Insert(0, new ListItem("Select..", ""));

                gridKO.DataBind();
               this.DataBind();
                return;
            }
            if (tabIndex == 2)
            {

            }
        }








        protected void FindHistory(object sender, EventArgs e)
        {


        }
        protected void FindKnockOff(object sender, EventArgs e)
        {
            
            try
            {            
                string vs = Convert.ToString(Request.Form[ddlKOVoucher.UniqueID]);
                if (ddlKOVoucher.SelectedValue == "") throw new Exception("Select the voucher to knock off against");
                if (txtKOFrom.Text == "" || txtKOTo.Text == "") throw new Exception("Specify the date range");
                if (ddlPTKO.SelectedValue == "") throw new Exception("Select the payment method");

                KnockOffServices KOServices = new KnockOffServices();
                DateRange range = new DateRange(Convert.ToDateTime(txtKOFrom.Text.Trim()), Convert.ToDateTime(txtKOTo.Text.Trim()));
                CurrentSession session = CurrentSession.Current;
             DataTable dt =   KOServices.GetCreditTransactions(session, range, int.Parse(ddlPTKO.SelectedValue), "", null);
             gridKO.DataSource = dt;
             gridKO.DataBind();
            }
            catch (Exception ex)
            {
                IQCareMsgBox.NotifyAction(ex.Message, "Error Finding Transcations", true, this, "");
            }
        }

        protected void gridHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gridHistory_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gridKO_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
              DataRowView rowView = (DataRowView)e.Row.DataItem;
              double amount = Convert.ToDouble(rowView["Amount"]);
              double setteledAmount = Convert.ToDouble(rowView["AmountSettled"]);
              RangeValidator rg = e.Row.FindControl("rgKOAmount") as RangeValidator;
              TextBox txtAmt = e.Row.FindControl("txtKOAmt") as TextBox;
              if (txtAmt != null)
              {
                  txtAmt.Text =  (amount - setteledAmount).ToString();
              }
              if (rg != null)
              {
                  rg.MinimumValue = "0";
                  rg.MaximumValue = (amount - setteledAmount).ToString();
                  rg.ErrorMessage = string.Format("The value should be between 0.00 and  {0}", (amount - setteledAmount));
              }
            }
        }

        protected void gridKO_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void KnockOffTransaction(object sender, EventArgs e)
        {
            List<Entities.Billing.KnockOff> transactions = new List<Entities.Billing.KnockOff>();
            int voucherId = Convert.ToInt32(ddlKOVoucher.SelectedValue);
           
            CurrentSession session = CurrentSession.Current;
            foreach (GridViewRow gridRow in this.gridKO.Rows)
            {
                if (gridRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = gridRow.FindControl("chkBxItem") as CheckBox;
                    if (chk != null && chk.Checked && chk.Enabled)
                    {
                        int transactionId = int.Parse(gridKO.DataKeys[gridRow.RowIndex].Values["TransactionId"].ToString());
                        int paymentTypeId = int.Parse(gridKO.DataKeys[gridRow.RowIndex].Values["PaymentTypeId"].ToString());
                        Label labelTranTotal = gridRow.FindControl("labeltranTotal") as Label;
                        
                        TextBox textBox = gridRow.FindControl("txtKOAmt") as TextBox;
                        RangeValidator rgKOAmount = gridRow.FindControl("rgKOAmount") as RangeValidator;

                        if (textBox != null && textBox.Text != "" && Convert.ToDouble(textBox.Text.Trim())> 0.0D && rgKOAmount.IsValid)
                        {
                            double KOAmt = Convert.ToDouble(textBox.Text.Trim());
                            KnockOff knockOff = new KnockOff()
                            {
                                Id = 0,
                                TransactionId = transactionId,                           
                                UserId = session.User.Id,
                                PaymentTypeId = paymentTypeId,
                                KnockOffAmount = KOAmt,
                                TransactionAmount = Convert.ToDouble(labelTranTotal.Text.Trim()),
                                DeleteFlag = false,
                                Description = ""

                            };
                            transactions.Add(knockOff);
                        }
                    }
                };

              
            }
            KnockOffServices servicesKO = new KnockOffServices();
            KnockOffServices.ResponseCode responseCode = servicesKO.KnockOffTransaction(session, voucherId, transactions);
            
            if (responseCode == KnockOffServices.ResponseCode.Ok)
            {
                IQCareMsgBox.NotifyAction("Knock off for the selected* transaction succeeded", "Knock Operation Success", false, this, "");
            }
            else if (responseCode == KnockOffServices.ResponseCode.BadRequest)
            {
                IQCareMsgBox.NotifyAction("Knock off for the selected* transaction failed", "Bad Request", true, this, "");
            }
            this.GetContextTabData(TabContainer1.ActiveTabIndex);

        }

        /// <summary>
        /// Saves the voucher.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void SaveVoucher(object sender, EventArgs e)
        {
            try
            {
                DateTime valDate = Convert.ToDateTime(txtDate.Text);
                Double valAmount = Convert.ToDouble(txtAmount.Text);
                string valType = ddlVoucherType.SelectedValue;
                string valReference = txtReference.Text;
                string description = txtDetails.Text;

                KnockOffServices KOServices = new KnockOffServices();
                KnockOffServices.ResponseCode responseCode = KOServices.SaveVoucher(CurrentSession.Current, valDate, valAmount, valType, valReference, description);
                if (responseCode == KnockOffServices.ResponseCode.Ok)
                {
                    txtDetails.Text = txtAmount.Text = txtDate.Text = txtReference.Text = txtDate.Text = "";
                    ddlVoucherType.SelectedIndex = 0;
                    IQCareMsgBox.NotifyAction("Voucher details saved successfully", "Saving Voucher", false, this, "");

                }

            }
            catch (Exception ex)
            {
                IQCareMsgBox.NotifyAction(ex.Message, "Error Saving Voucher", true, this, "");
            }

        }

        protected void CancelSaveVoucher(object sender, EventArgs e)
        {
            string theUrl = string.Format("{0}", "~/Billing/Home.aspx");
            //Response.Redirect(theUrl);
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect(theUrl, true);

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

       
    }
}