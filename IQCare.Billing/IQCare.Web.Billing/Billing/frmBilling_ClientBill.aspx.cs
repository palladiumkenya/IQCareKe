using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Application.Common;
using Application.Presentation;
using Entities.Billing;
using Interface.Administration;
using Interface.Clinical;
using Interface.Billing;
using Entities.Clinical;
using IQCare.Web.UILogic;
using Entities.PatientCore;


namespace IQCare.Web.Billing
{
    public partial class ClientBill : System.Web.UI.Page
    {
        /// <summary>
        /// The display flag
        /// </summary>
        public string displayFlag = "";
        /// <summary>
        /// The is error
        /// </summary>
        bool isError = false;

        //PayMode defaultPayMode = PayMode.FullBill;
        bool useDefaultPaymode = true;
        //AuthenticationManager Authentication = new AuthenticationManager();
        /// <summary>
        /// Gets a value indicating whether this instance can write off.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can write off; otherwise, <c>false</c>.
        /// </value>
        bool CanWriteOff
        {
            get
            {
                return (CurrentSession.Current.HasFeaturePermission(ApplicationAccess.BillingFeature.WriteOffDebt));
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance can do client bill.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can do client bill; otherwise, <c>false</c>.
        /// </value>
        bool CanDoClientBill
        {
            get
            {
                return (CurrentSession.Current.HasFeaturePermission(ApplicationAccess.BillingFeature.ClientBilling));
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance can receive payment.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can receive payment; otherwise, <c>false</c>.
        /// </value>
        bool CanReceivePayment
        {
            get
            {
                return (CurrentSession.Current.HasFeaturePermission(ApplicationAccess.BillingFeature.ReceivePayment));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        enum PayMode
        {
            FullBill = 1,
            SelectItem = 2
        }
        /// <summary>
        /// Gets the item manager.
        /// </summary>
        /// <value>
        /// The item manager.
        /// </value>
        IItemMaster itemManager
        {
            get
            {
                return (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
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
               // this.Master.ExecutePatientLevel = false;

                if (!CurrentSession.Current.HasCurrentModuleRight())
                {
                    string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                    //Response.Redirect(theUrl);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
              //  if (!Authentication.HasFeatureRight(ApplicationAccess.Billing, (DataTable)Session["UserRight"]))
                if(!this.CanDoClientBill)
                {
                    string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                    //Response.Redirect(theUrl);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
               
                if (this.PatientId==0)
                {
                    string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                    //Response.Redirect(theUrl);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
                Master.PageScriptManager.RegisterAsyncPostBackControl(TabContainer1);
                if (IsPostBack) return;
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Billing >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Curent Bill";
                //this.SetConsumableItemTypeID();
                // (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
                this.Init_page();
                this.BindDropdown();
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }

        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            CurrentSession session = CurrentSession.Current;
            string xc = base.Session["PatientId"].ToString();
            Master.PageScriptManager.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(PageScriptManager_AsyncPostBackError);
            this.ReverseTransaction.ErrorOccurred += new CommandEventHandler(ReverseTransaction_ErrorOccurred);
            this.ReverseTransaction.IsApprovalMode = false;
            this.ReverseTransaction.NotifyCommand += new CommandEventHandler(ReverseTransaction_NotifyCommand);
            session.SetCurrentPatient(Convert.ToInt32(base.Session["PatientId"].ToString()));
            this.ReverseTransaction.PatientId = session.CurrentPatient.Id;
            this.ReverseTransaction.LocationId = session.Facility.Id;

            this.ReverseTransaction.CanRefund = true;
            this.ReverseTransaction.RowCommand += new CommandEventHandler(ReverseTransaction_RowCommand);

            this.PDControl.PatientId = session.CurrentPatient.Id;
            this.PDControl.LocationId = session.Facility.Id; 
            this.PDControl.ErrorOccurred += new CommandEventHandler(ReverseTransaction_ErrorOccurred);
            this.buttonHidden.Click += new EventHandler(buttonHidden_Click);

        }
        /// <summary>
        /// Handles the AsyncPostBackError event of the PageScriptManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AsyncPostBackErrorEventArgs"/> instance containing the event data.</param>
        void PageScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            string message = e.Exception.Message;
            Master.PageScriptManager.AsyncPostBackErrorMessage = message;
        }
        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.SetStyle();
            divError.Visible = isError;
        }


        /// <summary>
        /// Init_pages this instance.
        /// </summary>
        private void Init_page()
        {
            //if (Request.QueryString["PatientId"] != null)
            //    Session["PatientId"] = Request.QueryString["PatientId"];
            TabContainer1.ActiveTabIndex = 0;
            try
            {
                this.PopulateUnBilledItems();
                this.BindUnBilledGrid();

                //this.PopulatePatientDetails();
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
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
        /// Handles the Click event of the btn_close control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btn_close_Click(object sender, EventArgs e)
        {
            string theUrl = "./frmBillingFindAddBill.aspx";
            Response.Redirect(theUrl, false);
        }

        /// <summary>
        /// Handles the Click event of the btn_print control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btn_print_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btn_saveBill control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btn_saveBill_Click(object sender, EventArgs e)
        {

            saveBillInformation();
            this.Init_page();
        }

        /// <summary>
        /// Saves the bill information.
        /// </summary>
        private void saveBillInformation()
        {
            try
            {
                IBilling BillingManager;
                BillingManager = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
                int result = BillingManager.SavePatientPayableItems(this.UnBilledItems, this.UserId);
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Handles the RowCommand event of the grdUnBilledItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void grdUnBilledItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddItem"))
            {
                try
                {
                    TextBox txtNewDescription = (TextBox)grdUnBilledItems.FooterRow.FindControl("txtNewDescription");
                    TextBox txtNewQuantity = (TextBox)grdUnBilledItems.FooterRow.FindControl("txtNewQuantity");
                    DropDownList ddlNewPaymentMode = (DropDownList)grdUnBilledItems.FooterRow.FindControl("ddlNewPaymentMode");

                    Label lblNewUnitPrice = (Label)grdUnBilledItems.FooterRow.FindControl("lblNewUnitPrice");
                    //TextBox txtNewitemId = (TextBox)grdCurrentBill.FooterRow.FindControl("txtNewitemId");
                    // TextBox txtNewItemType = (TextBox)grdCurrentBill.FooterRow.FindControl("txtNewItemType");

                    valid = true;
                    isValidEntry = validateTextBox(txtNewDescription);
                    isValidEntry = validateTextBox(txtNewQuantity);
                    int moduleID = 0;
                    string costCenterName = "";
                    GridViewRow gridRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    string selectedItemType = HItemTypeName.Value;
                    //if (HItemTypeName.Value == "Consumables" || HItemTypeName.Value == "Billables" || HItemTypeName.Value=="Visit Type")
                    //{

                        DropDownList ddl = gridRow.FindControl("ddlItemCostCenter") as DropDownList;
                        if (ddl != null)
                        {
                            if (selectedItemType == "Consumables" || selectedItemType == "Billables" || selectedItemType == "Visit Type" || selectedItemType == "VisitType")
                            {
                                if (validateDropDown(ddl))
                                {
                                    int.TryParse(ddl.SelectedValue, out moduleID);
                                    isValidEntry = true;
                                    costCenterName = ddl.SelectedItem.Text;
                                }
                                else isValidEntry = false;
                            }
                            else
                            {
                                if(ddl.SelectedIndex >0 && ddl.SelectedValue!= "")
                                {
                                    if (ddl.SelectedValue == "0")
                                    {
                                        costCenterName = this.ItemCostCenter(selectedItemType, Convert.ToInt32(HItemID.Value));                                        
                                    }
                                    else
                                    {
                                        int.TryParse(ddl.SelectedValue, out moduleID);
                                        costCenterName = ddl.SelectedItem.Text;
                                    }
                                    //if (moduleID > 1000) moduleID = 0;
                                }
                                else
                                {
                                    costCenterName = this.ItemCostCenter(selectedItemType, Convert.ToInt32(HItemID.Value));
                                }
                            }
                        }
                        
                    if (!isValidEntry)
                    {

                        return;
                    }
                    DataTable theDT = this.UnBilledItems;
                    DataRow theDR = theDT.NewRow();
                    theDR.SetField("BillID", DBNull.Value);
                    theDR.SetField("BillItemID", DBNull.Value);
                    theDR.SetField("PatientID", this.PatientId);
                    theDR.SetField("LocationID", Convert.ToInt32(Session["AppLocationId"]));
                    theDR.SetField("BillItemDate", DateTime.Now);
                    theDR.SetField("ItemName", txtNewDescription.Text);
                    theDR.SetField("ItemId", HItemID.Value);
                    theDR.SetField("itemType", HItemTypeID.Value);
                    theDR.SetField("ItemTypeName", HItemTypeName.Value);
                    theDR.SetField("Quantity", Int32.Parse(txtNewQuantity.Text));
                    theDR.SetField("sellingPrice", Decimal.Parse(lblNewUnitPrice.Text));
                    theDR.SetField("PaymentStatus", 0);
                    theDR.SetField("Amount", Decimal.Parse(lblNewUnitPrice.Text) * Int32.Parse(txtNewQuantity.Text));
                    theDR.SetField("PayItem", true);
                    theDR.SetField("RowStatus", "Added");
                    theDR.SetField("Persisted", false);
                    theDR.SetField("serviceStatus", 0);
                    theDR.SetField("ModuleID", moduleID);
                    theDR.SetField("CostCenterName", costCenterName);
                    theDR.SetField("ItemSourceReferenceID", DBNull.Value);
                    theDT.Rows.Add(theDR);

                    DataRow placeHolderRow = theDT.AsEnumerable().FirstOrDefault(row => row["ItemId"].ToString() == "");
                    if (placeHolderRow != null)
                        theDT.Rows.Remove(placeHolderRow);
                    theDT.AcceptChanges();
                    // Session["billingInformation"] = theDT;
                    this.UnBilledItems = theDT;
                    this.BindUnBilledGrid();
                }
                catch (Exception ex)
                {
                    this.showErrorMessage(ref ex);
                }
                // fillBillGrids(false);
            }


        }
        private bool valid = true;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid entry.
        /// checks whether inputed text is valid if any is invalid it will always return false
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
        /// Validates the text box.
        /// </summary>
        /// <param name="inputbox">The inputbox.</param>
        /// <returns></returns>
        private bool validateTextBox(TextBox inputbox)
        {
            if (inputbox.Text.Trim() == "" || inputbox.Text.Trim() == "0")
            {
                inputbox.BorderColor = System.Drawing.Color.Red;
                return false;
            }
            inputbox.BorderColor = System.Drawing.Color.White;
            return true;

        }
        private bool validateDropDown(DropDownList selectList)
        {
            if (selectList.SelectedIndex == -1 || selectList.SelectedValue.Trim() == "0")
            {
                selectList.BorderColor = System.Drawing.Color.Red;
                return false;
            }
            selectList.BorderColor = System.Drawing.Color.White;
            return true;

        }
        /// <summary>
        /// Handles the RowEditing event of the grdCurrentBill control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewEditEventArgs"/> instance containing the event data.</param>
        protected void grdUnBilledItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdUnBilledItems.EditIndex = e.NewEditIndex;

            // fillBillGrids(false);
            this.BindUnBilledGrid();
        }

        /// <summary>
        /// Handles the RowCancelingEdit event of the grdUnBilledItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCancelEditEventArgs"/> instance containing the event data.</param>
        protected void grdUnBilledItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdUnBilledItems.EditIndex = -1;

            //fillBillGrids(false);
            this.BindUnBilledGrid();
        }

        /// <summary>
        /// Handles the RowUpdating event of the grdUnBilledItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewUpdateEventArgs"/> instance containing the event data.</param>
        protected void grdUnBilledItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                Label lblUpdateDate = (Label)grdUnBilledItems.Rows[e.RowIndex].FindControl("lblEditDate");
                TextBox txtUpdateDescription = (TextBox)grdUnBilledItems.Rows[e.RowIndex].FindControl("txtEditDescription");
                TextBox txtUpdateQuantity = (TextBox)grdUnBilledItems.Rows[e.RowIndex].FindControl("txtEditQuantity");
                DropDownList ddlUpdatePaymentMode = (DropDownList)grdUnBilledItems.Rows[e.RowIndex].FindControl("ddlEditPaymentMode");
                TextBox txtEdititemId = (TextBox)grdUnBilledItems.Rows[e.RowIndex].FindControl("txtEdititemId");
                Label lblUpdateUnitPrice = (Label)grdUnBilledItems.Rows[e.RowIndex].FindControl("lblEditUnitPrice");


                DataTable theDT = this.UnBilledItems;

                DataRow rowEdit = theDT.Rows[e.RowIndex];

                /*   if (txtUpdateDescription.Text.Trim() == "" || txtUpdateQuantity.Text.Trim() == "" || txtUpdateQuantity.Text.Trim() == "0")
                       return;*/

                //rest valid first
                valid = true;
                isValidEntry = validateTextBox(txtUpdateQuantity);
                isValidEntry = validateTextBox(txtUpdateDescription);
                int moduleID = 0;
                string costCenterName = "";
                GridViewRow gridRow = grdUnBilledItems.Rows[e.RowIndex];
                string  oldDepartment, oldItemID, oldItemTypeName, oldDeptID, oldItemTypeID;
                int oldQTY;
                //oldName = e.OldValues["ItemName"].ToString();
                //oldDepartment = e.OldValues["CostCenterName"].ToString();
                //oldDeptID = e.OldValues["ModuleID"].ToString();
                //oldItemType = e.OldValues["ItemTypeName"].ToString();
                //oldItemID = e.Keys["ItemID"].ToString();
                //oldItemTypeID = e.Keys["ItemType"].ToString();
                //oldQTY = int.Parse(e.OldValues["Quantity"].ToString());
                //  e.r
                oldItemTypeName = rowEdit["ItemTypeName"].ToString();
                oldItemTypeID = rowEdit["ItemType"].ToString();
                oldDeptID = rowEdit["ModuleId"].ToString();
                costCenterName = oldDepartment = rowEdit["CostCenterName"].ToString();
                oldItemID = rowEdit["ItemID"].ToString();
                oldQTY = Convert.ToInt32(rowEdit["Quantity"]);
                if (oldItemTypeName == "Consumables" || oldItemTypeName == "Billables" || oldItemTypeName == "Visit Type")
                {

                    DropDownList ddl = gridRow.FindControl("ddlItemCostCenter") as DropDownList;
                    if (ddl != null && ddl.SelectedIndex != -1 && ddl.SelectedValue != "")
                    {
                        isValidEntry = validateDropDown(ddl);
                        if (validateDropDown(ddl))
                        {
                            int.TryParse(ddl.SelectedValue, out moduleID);
                            isValidEntry = true;
                            costCenterName = ddl.SelectedItem.Text;
                        }
                        else isValidEntry = false;
                    }

                }
                else
                {
                    Label lbl = gridRow.FindControl("lblCostCenter") as Label;
                    if (lbl != null)
                    {
                        costCenterName = lbl.Text;
                    }
                }



                if (!isValidEntry)
                {
                    e.Cancel = true;

                    return;
                }

                bool itemChanged = (HItemID.Value != "" && (HItemID.Value != oldItemID && HItemTypeID.Value != oldItemTypeID));

                string newItemID = itemChanged ? HItemID.Value : oldItemID;
                string newItemTypeID = itemChanged ? HItemTypeID.Value : oldItemTypeID;
                string newItemTypeName = itemChanged ? HItemTypeName.Value : oldItemTypeName;

                bool departmentChanged = oldDepartment != costCenterName;
                bool QtyChanged = oldQTY != Int32.Parse(txtUpdateQuantity.Text);

                if (!QtyChanged && !departmentChanged && !itemChanged)
                {
                    e.Cancel = true;
                    grdUnBilledItems.EditIndex = -1;
                  //  return;
                }
                else
                {
                    if (Convert.ToBoolean(rowEdit["Persisted"]))
                    {
                        //rowEdit["BillItemDate"] = DateTime.Now.ToString();//lblUpdateDate.Text;
                        rowEdit["ItemName"] = txtUpdateDescription.Text;
                        rowEdit["Quantity"] = Int32.Parse(txtUpdateQuantity.Text);
                        //theDT.Rows[e.RowIndex]["PaymentName"] = ddlUpdatePaymentMode.SelectedItem.Text;
                        //theDT.Rows[e.RowIndex]["PaymentType"] = ddlUpdatePaymentMode.SelectedValue;
                        rowEdit["sellingPrice"] = Decimal.Parse(lblUpdateUnitPrice.Text);
                        rowEdit["Amount"] = Decimal.Parse(rowEdit["sellingPrice"].ToString()) * Decimal.Parse(rowEdit["Quantity"].ToString());
                        rowEdit["ItemID"] = newItemID;
                        rowEdit["ItemType"] = newItemTypeID;
                        rowEdit["ItemTypeName"] = newItemTypeName;
                        rowEdit["ModuleID"] = moduleID;
                        rowEdit["CostCenterName"] = costCenterName;
                        rowEdit.SetField("RowStatus", "Updated");
                        rowEdit.AcceptChanges();
                    }
                    else
                    {
                        theDT.Rows.RemoveAt(e.RowIndex);

                        DataRow theDR = theDT.NewRow();
                        //theDR.SetField("BillID", Session["BillID"]);
                        theDR.SetField("BillID", DBNull.Value);
                        theDR.SetField("BillItemID", DBNull.Value);
                        theDR.SetField("PatientID", this.PatientId);
                        theDR.SetField("BillItemDate", DateTime.Now);
                        theDR.SetField("ItemName", txtUpdateDescription.Text);
                        theDR.SetField("ItemId", newItemID);
                        theDR.SetField("itemType", newItemTypeID);
                        theDR.SetField("ItemTypeName", newItemTypeName);
                        theDR.SetField("Quantity", Int32.Parse(txtUpdateQuantity.Text));
                        theDR.SetField("sellingPrice", Decimal.Parse(lblUpdateUnitPrice.Text));
                        theDR.SetField("PaymentStatus", 0);
                        theDR.SetField("Amount", Decimal.Parse(lblUpdateUnitPrice.Text) * Int32.Parse(txtUpdateQuantity.Text));
                        theDR.SetField("PayItem", true);
                        theDR.SetField("RowStatus", "Added");
                        theDR.SetField("Persisted", false);
                        theDR.SetField("serviceStatus", 0);
                        theDR.SetField("ModuleID", moduleID);
                        theDR.SetField("CostCenterName", costCenterName);
                        theDR.SetField("ItemSourceReferenceID", DBNull.Value);
                        theDT.Rows.Add(theDR);
                    }

                    theDT.AcceptChanges();
                    //Session["billingInformation"] = theDT;

                    this.UnBilledItems = theDT;
                }
                grdUnBilledItems.EditIndex = -1;
                this.BindUnBilledGrid();
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
            // fillBillGrids(false);
        }

        /// <summary>
        /// Handles the RowDeleting event of the grdUnBilledItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void grdUnBilledItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                //DataTable theDT = (DataTable)Session["billingInformation"];
                DataTable theDT = this.UnBilledItems;

                DataRow rowDelete = theDT.Rows[e.RowIndex];

                if (Convert.ToBoolean(rowDelete["Persisted"]))
                {
                    rowDelete["RowStatus"] = "Deleted";
                    rowDelete.AcceptChanges();
                }
                else
                {
                    theDT.Rows.RemoveAt(e.RowIndex);
                }
                this.BindUnBilledGrid();
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }

        }

        /// <summary>
        /// Handles the RowDataBound event of the grdUnBilledItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdUnBilledItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRowView rowView = (DataRowView)e.Row.DataItem;

                    // Retrieve the serviceStatus value for the current row.
                    //if service/item has been given then it becomes uneditable
                    bool serviceReceived = rowView["ServiceStatus"].ToString().Equals("1");
                    //if (serviceStatus == "1")
                    //{
                        Button btnEdit = (Button)e.Row.FindControl("buttonEdit");
                       if(btnEdit !=null) btnEdit.Visible = !serviceReceived;
                        //disable the delete button too

                        Button btnDelete = (Button)e.Row.FindControl("buttonDelete");
                        if (btnDelete != null) btnDelete.Visible = !serviceReceived;

                        Label labelServiceStatus = (Label)e.Row.FindControl("lblServiceStatus");
                        if (labelServiceStatus != null)
                        {
                            labelServiceStatus.Text = serviceReceived ? "Yes" : "No";
                        }
                     
                    //}
                    Boolean persisted = Convert.ToBoolean(rowView["Persisted"]);
                    CheckBox chkBx = e.Row.FindControl("chkBxItem") as CheckBox;
                    if (chkBx != null) chkBx.Enabled = persisted;

                    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                    {
                        string thisItemModule = (rowView["ModuleID"].ToString());
                        string costCenterName = rowView["CostCenterName"].ToString();
                        Label lblCostCenter = (Label)e.Row.FindControl("lblCostCenter");
                        if (thisItemModule != "" && thisItemModule != "0")
                        {
                            DropDownList ddl = e.Row.FindControl("ddlItemCostCenter") as DropDownList;
                            ddl.Items.Clear();
                            ddlCostCenter.ClearSelection();
                            if (ddl != null)
                            {
                                ListItem[] arr = new ListItem[ddlCostCenter.Items.Count];
                                ddlCostCenter.Items.CopyTo(arr, 0);
                                ddl.Items.AddRange(arr);
                                ddl.ClearSelection();
                                //if (ddl.SelectedIndex == -1)
                                //{
                                ListItem item = ddl.Items.FindByText(costCenterName);
                                if (item != null)
                                {
                                    ddl.SelectedIndex = ddl.Items.IndexOf(item);
                                    //  item.Selected = true;
                                }
                                //}
                            }
                            ddl.Visible = true;
                            lblCostCenter.Visible = false;
                        }
                        else
                        {
                            lblCostCenter.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Sets the consumable item type identifier. Consumables are excluded from the search functionality
        /// </summary>
        //void SetConsumableItemTypeID()
        //{

        //    //  IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling,BusinessProcess.Billing");
        //    if (this.Authentication.HasFeatureRight(ApplicationAccess.Consumables, (DataTable)Session["UserRight"]) == false)
        //    {
        //        IItemMaster bMgr = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
        //        Session["ConsumableTypeID"] = bMgr.GetItemTypeIDByName("Consumables").ToString();
        //    }
        //    else
        //    {
        //        Session["ConsumableTypeID"] = null;
        //    }
        //}
        /// <summary>
        /// Items the cost center.
        /// </summary>
        /// <returns></returns>
        string ItemCostCenter(string itemTypeName, int ItemId=0)
        {
            switch (itemTypeName)
            {
                case "Pharmaceuticals":
                    return "Pharmacy";
                case "Ward Admission":
                    return "Inpatient";
                case "Lab Tests":
                    return "Laboratory";
                case "Clinical Services" :
                    return   GetClinicalItemCostCenter(ItemId);
                default:
                    return "";
            }
        }
         string GetClinicalItemCostCenter(int itemId)
         {
             try
             {
                 IServiceManager mGr = (IServiceManager)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BServicesManager, BusinessProcess.Clinical");
                 Service srv = mGr.GetServiceById(itemId);
                 if (null != srv)
                 {
                     return srv.ServiceArea;
                 }
                 return "";
             }
             catch
             {
                 return "";
             }
         }
        /// <summary>
        /// Hides the edit.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        protected string HideEdit(object str)
        {
            if (str != null)
            {
                string cc = this.ItemCostCenter(str.ToString());
                return cc == "" ? "none" : "";
            }
            return "";
        }
        /// <summary>
        /// Shows the edit.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        protected string ShowEdit(object str)
        {
            if (str != null)
            {
                string cc = this.ItemCostCenter(str.ToString());
                return cc == "" ? "" : "none";
            }
            return "none";
        }
        /// <summary>
        /// Handles the textChanged event of the txtNewDescription control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void txtNewDescription_textChanged(object sender, EventArgs e)
        {
            int ItemId, itemType;
            string itemTypeName = "";
            decimal itemPrice;
            try
            {
                if (hdCustID.Value != "")
                {
                    String[] itemCodes = hdCustID.Value.Split(';');
                    if (itemCodes.Length == 4)
                    {
                        ItemId = Convert.ToInt32(itemCodes[0]);
                        itemType = Convert.ToInt32(itemCodes[1]);
                        itemTypeName = itemCodes[3].ToString();
                        itemPrice = Convert.ToDecimal(itemCodes[2]);
                        //IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling,BusinessProcess.Billing");
                      //  itemPrice = bMgr.GetItemPrice(ItemId, itemType, DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")));
                        if (itemPrice == 0.00M)
                        {
                            ((TextBox)sender).Text = "";
                            hdCustID.Value = "";
                        }
                        HItemTypeName.Value = itemTypeName;
                        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
                        Label lblNewUnitPrice = (Label)row.FindControl("lblNewUnitPrice");
                        Label lblNewAmountPrice = (Label)row.FindControl("lblNewAmountPrice");
                        TextBox txtNewQuantity = (TextBox)row.FindControl("txtNewQuantity");
                        lblNewUnitPrice.Text = itemPrice.ToString();
                        HItemID.Value = ItemId.ToString();
                        HItemTypeID.Value = itemType.ToString();
                        Label lblCostCenter = (Label)row.FindControl("lblCostCenter");
                        string defItem = this.ItemCostCenter(itemTypeName,ItemId);
                        //if (lblCostCenter.Text == "")
                        //{
                            DropDownList ddl = row.FindControl("ddlItemCostCenter") as DropDownList;
                            if (ddl != null)
                            {
                                ListItem[] arr = new ListItem[ddlCostCenter.Items.Count];
                                ddlCostCenter.Items.CopyTo(arr, 0);
                                ddl.Items.AddRange(arr);

                            }
                            ddl.Visible = true;
                            lblCostCenter.Visible = false;
                            ddl.ClearSelection();
                            ListItem li = ddl.Items.FindByText(defItem);
                            if (li != null) li.Selected = true;
                        //}
                        //else
                        //{
                        //    lblCostCenter.Visible = true;
                        //}
                    }

                }
                else
                {
                    ((TextBox)sender).Text = "";
                    hdCustID.Value = "";
                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }

        }
        /// <summary>
        /// Handles the textChanged event of the txtEditDescription control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void txtEditDescription_textChanged(object sender, EventArgs e)
        {
            int ItemId, itemType;
            decimal itemPrice;
            string itemTypeName = "";
            try
            {
                if (hdCustID.Value != "")
                {
                    String[] itemCodes = hdCustID.Value.Split(';');
                    if (itemCodes.Length == 4)
                    {
                        ItemId = Convert.ToInt32(itemCodes[0]);
                        itemType = Convert.ToInt32(itemCodes[1]);
                        itemPrice = Convert.ToDecimal(itemCodes[2]);
                        itemTypeName = HItemTypeName.Value = itemCodes[3].ToString();
                        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
                        Label lblNewUnitPrice = (Label)row.FindControl("lblEditUnitPrice");
                        lblNewUnitPrice.Text = itemPrice.ToString();
                        HItemID.Value = ItemId.ToString();
                        HItemTypeID.Value = itemType.ToString();
                        Label lblCostCenter = (Label)row.FindControl("lblCostCenter");
                        lblCostCenter.Text = this.ItemCostCenter(itemTypeName,ItemId);
                        //if (lblCostCenter.Text == "")
                        //{
                            DropDownList ddl = row.FindControl("ddlItemCostCenter") as DropDownList;
                            if (ddl != null)
                            {
                                ListItem[] arr = new ListItem[ddlCostCenter.Items.Count];
                                ddlCostCenter.Items.CopyTo(arr, 0);
                                ddl.Items.AddRange(arr);
                            }
                            ddl.Visible = true;
                            lblCostCenter.Visible = false;
                        //}
                        //else {
                            
                        //    lblCostCenter.Visible = true;
                        //}
                    }
                }
                else
                {
                    ((TextBox)sender).Text = "";
                    hdCustID.Value = "";
                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Searches the items.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> SearchItems(string prefixText, int count)
        {
            List<string> ar = new List<string>();
            IItemMaster _iMGR = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
            //filling data from database
         //   int? consumableItemTypeID = null;
            //if (System.Web.HttpContext.Current.Session["ConsumableTypeID"] != null)
            //{
            //    consumableItemTypeID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ConsumableTypeID"].ToString());
            //}
            // int.TryParse(System.Web.HttpContext.Current.Session["ConsumableTypeID"].ToString(), out consumableItemTypeID);
            DataTable dataTable = _iMGR.FindItems(prefixText, null, null, DateTime.Now, true);
            string custItem = string.Empty;
            foreach (DataRow theRow in dataTable.Rows)//.Select("ItemTypeName <> 'Consumables'"))
            {
                custItem = AutoCompleteExtender.CreateAutoCompleteItem(theRow[1].ToString(), String.Format("{0};{1};{2};{3}", theRow["ItemID"], theRow["ItemTypeID"], theRow["SellingPrice"], theRow["ItemTypeName"]));
                //theRow[0], theRow[2], theRow[3]));

                ar.Add(custItem);
            }

            return ar;


        }

        /// <summary>
        /// Handles the TextChanged event of the txtNewQuantity control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void txtNewQuantity_TextChanged(object sender, EventArgs e)
        {
            int value;
            Int32.TryParse(((TextBox)sender).Text, out value);
            if (value < 1) value = 1;

        }

        /// <summary>
        /// Gets or sets the un billed items.
        /// </summary>
        /// <value>
        /// The un billed items.
        /// </value>
        DataTable UnBilledItems
        {
            get
            {
                if (base.Session["PatientUnbilledItems"] == null)
                    return new DataTable();
                else
                {
                    return (DataTable)base.Session["PatientUnbilledItems"];
                }
            }
            set
            {
                base.Session["PatientUnbilledItems"] = value;
            }
        }
        /// <summary>
        /// Populates the un billed items.
        /// </summary>
        void PopulateUnBilledItems()
        {
            IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
            DataTable dtUnbilledItems = bMgr.GetPatientUnBilledItems(this.PatientId, this.LocationId);
            DataColumn newCol = new DataColumn("RowStatus", typeof(string));
            newCol.DefaultValue = "UnModified";
            dtUnbilledItems.Columns.Add(newCol);
            DataColumn persistCol = new DataColumn("Persisted", typeof(bool));
            persistCol.DefaultValue = false;
            dtUnbilledItems.Columns.Add(persistCol);
            dtUnbilledItems.AcceptChanges();
            foreach (DataRow row in dtUnbilledItems.Rows)
            {
                row["Persisted"] = true;
            }
            dtUnbilledItems.AcceptChanges();
            this.UnBilledItems = dtUnbilledItems;


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
        /// Populates the pending bills.
        /// </summary>
        void PopulateOpenBills()
        {

            IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
            DataTable dtOpenBills = bMgr.GetPatientBillByStatus(this.PatientId, this.LocationId, BillStatus.Open);
            this.gridPendingBills.DataSource = dtOpenBills;
            this.gridPendingBills.DataBind();
        }
        /// <summary>
        /// Populates the settled bills.
        /// </summary>
        void PopulateClosedBills()
        {
        //    frmBilling_ClientBill bill = new frmBilling_ClientBill();
            IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
            DataTable dtClosedBills = bMgr.GetPatientBillByStatus(this.PatientId, this.LocationId, BillStatus.Closed);
            this.gridClosedBills.DataSource = dtClosedBills;
            this.gridClosedBills.DataBind();
        }

        /// <summary>
        /// Gets the bill transactions.
        /// </summary>
        /// <param name="billID">The bill identifier.</param>
        /// <returns></returns>
        DataTable GetBillTransactions(int billID)
        {
            IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
            DataTable billTransactions = bMgr.GetBillTransactions(billID);
            return billTransactions;
        }
        /// <summary>
        /// Populates the items by bill identifier.
        /// </summary>
        /// <param name="billID">The bill identifier.</param>
        /// <returns></returns>
        DataTable GetItemsByBillID(int billID)
        {
            IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
            DataTable billItems = bMgr.GetBillItems(billID);
            return billItems;
        }
        /// <summary>
        /// Binds the unbilled grid.
        /// </summary>
        void BindUnBilledGrid()
        {
            //frmBilling_ClientBill bill = new frmBilling_ClientBill();
            try
            {
                DataTable theMainDT = this.UnBilledItems;
                DataView dv = theMainDT.DefaultView;
                dv.RowFilter = "RowStatus <> 'Deleted'";
                DataTable theDT = dv.ToTable();
                if (theDT.Rows.Count == 0)
                {
                    DataRow theDR = theDT.NewRow();
                    theDR.SetField(0, 0);
                    theDT.Rows.Add(theDR);
                    this.grdUnBilledItems.DataSource = theDT;
                    this.grdUnBilledItems.DataBind();
                    this.grdUnBilledItems.Rows[0].Visible = false;
                }
                else
                {
                    this.grdUnBilledItems.DataSource = theDT;
                    this.grdUnBilledItems.DataBind();
                }
                this.lbl_total.Text = "Total: " + theDT.Compute("Sum(amount)", "");
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Populates the patient detais.
        /// </summary>
        /*void PopulatePatientDetails()
        {
            try
            {
              //  IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");

              //  DataTable theDT = ptnMgr.GetPatientRecord(Convert.ToInt32(Session["PatientId"]));
              
                if (CurrentSession.Current.CurrentPatient != null)
                {
                    Patient p = CurrentSession.Current.CurrentPatient;
                    lblname.Text = p.FullName;
                    //String.Format("{0} {1} {2}", theDT.Rows[0]["Firstname"], theDT.Rows[0]["Middlename"], theDT.Rows[0]["Lastname"]);
                    lblsex.Text = p.Sex;// (theDT.Rows[0]["sex"].ToString() == "16") ? "Male" : "Female";
                    lbldob.Text = p.DateOfBirth.ToString("dd-MMM-yyyy");// Convert.ToDateTime(theDT.Rows[0]["dob"]).ToString("dd-MMM-yyyy");
                    lblFacilityID.Text = p.UniqueFacilityId;// theDT.Rows[0]["PatientFacilityID"].ToString();
                    //lblIQno.Text = theDT.Rows[0]["IQNumber"].ToString();
                }
                //ptnMgr = null;
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }*/
        /// <summary>
        /// Binds the dropdown.
        /// </summary>
        void BindDropdown()
        {
            //if (base.Session["TechnicalAreaId"] == null)
            //{
            BindFunctions BindManager = new BindFunctions();
            IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet DSModules = ptnMgr.GetModuleNames(this.LocationId);

            DataTable theMainDT = new DataTable();
            theMainDT = DSModules.Tables[0];
            
            //DataRow dr = theMainDT.NewRow();
            //dr["FacilityID"] = this.LocationId;
            //dr["ModuleID"] = "0";
            //dr["ModuleName"] = "Laboratory";

            //DataRow drPha = theMainDT.NewRow();
            //drPha["FacilityID"] = this.LocationId;
            //drPha["ModuleID"] = "0";
            //drPha["ModuleName"] = "Pharmacy";
            //theMainDT.Rows.Add(dr);
            //theMainDT.Rows.Add(drPha);
            
            DataView dv = theMainDT.DefaultView;
            dv.RowFilter = "ModuleName Not In ('PM/SCM','Billing','Ward Admission')";
            DataTable theDT = dv.ToTable();
            if (theDT.Rows.Count > 0)
            {
                BindManager.BindCombo(ddlCostCenter, theDT, "ModuleName", "ModuleID");
                ptnMgr = null;
            }
            ///}
        }
        /// <summary>
        /// Cancels the bill.
        /// </summary>
        /// <param name="billId">The bill identifier.</param>
        void CancelBill(int billID)
        {
            IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
            bMgr.CancelBill(billID, this.UserId);
            bMgr = null;
        }

        /// <summary>
        /// Gets the context tab data.
        /// </summary>
        /// <param name="TabIndex">Index of the tab.</param>
        void GetContextTabData(int TabIndex) //This is Page 
        {
            //  string text = "text" + TabIndex.ToString();

            if (TabIndex == 0)
            {
                this.PopulateUnBilledItems();
                this.BindUnBilledGrid();

            }
            else if (TabIndex == 1)
            {
                this.PopulateOpenBills();
            }
            else if (TabIndex == 2)
            {
                this.PopulateClosedBills();
            }
            else if (TabIndex == 3)
            {
                // this.PopulatePatientReversals();
                this.ReverseTransaction.Rebind();
            }
            else if (TabIndex == 4)
            {
                // this.PopulatePatientReversals();
                this.PDControl.Rebind();
            }
            //return text;
        }
        /// <summary>
        /// Initializes the items bill table.
        /// </summary>
        /// <returns></returns>
        private DataTable initializeItemsToBill()
        {

            DataTable theDT = new DataTable();
            theDT.Columns.Add("BillItemID", typeof(Int32));
            theDT.Columns.Add("PatientID", typeof(Int32));
            theDT.Columns.Add("BillItemDate", typeof(DateTime));
            theDT.Columns.Add("Amount", typeof(Decimal));
            theDT.Columns["Amount"].DefaultValue = 0;
            theDT.Columns.Add("PayItem", typeof(Boolean));
            theDT.Columns.Add("PaymentStatus", typeof(Int32));
            return theDT;
        }
        /// <summary>
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        /// <param name="onOkScript">The on ok script.</param>
        void NotifyAction(string strMessage, string strTitle, bool errorFlag, string onOkScript = "")
        {
            IQCareMsgBox.NotifyAction(strMessage, strTitle, errorFlag, this, onOkScript);
            //lblNoticeInfo.Text = strMessage;
            //lblNotice.Text = strTitle;
            //lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
            //lblNoticeInfo.Font.Bold = true;
            //imgNotice.ImageUrl = (errorFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
            //btnOkAction.OnClientClick = "";
            //if (onOkScript != "")
            //{
            //    btnOkAction.OnClientClick = onOkScript;
            //}
            //this.notifyPopupExtender.Show();
        }
        /// <summary>
        /// Handles the Click event of the GenerateBill control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void GenerateBill_Click(object sender, EventArgs e)
        {
            //this.saveBillInformation();
            //this.Init_page();
            double _total = 0;
            int itemChecked = 0;
            DataTable itemsToBill = this.initializeItemsToBill();
            DataTable theDT = this.UnBilledItems;
            foreach (GridViewRow gridRow in this.grdUnBilledItems.Rows)
            {
                if (gridRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chk = gridRow.FindControl("chkBxItem") as CheckBox;
                    if (chk != null && chk.Checked && chk.Enabled)
                    {
                        int billItemId = int.Parse(grdUnBilledItems.DataKeys[gridRow.RowIndex].Values["billItemID"].ToString());

                        DataRow thisRow = theDT.AsEnumerable().FirstOrDefault(r => r["billItemID"].ToString() == billItemId.ToString());
                        if (thisRow != null)
                        {
                            int patientID = Convert.ToInt32(thisRow["PatientID"]);
                            double amount = double.Parse(thisRow["Amount"].ToString());
                            DateTime billItemDate = DateTime.Parse(thisRow["BillItemDate"].ToString());
                            itemsToBill.Rows.Add(new object[]
                               {
                                billItemId,
                                patientID,
                                billItemDate,
                                amount,
                                true,
                                0
                               });
                            itemsToBill.AcceptChanges();
                            _total += amount;
                            itemChecked++;
                        }
                    }
                }
            }
            if (itemChecked > 0)
            {
                try
                {
                    IBilling objBilling = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling,BusinessProcess.Billing");
                    string InvoiceNumber = objBilling.GenerateBill(itemsToBill, this.PatientId, this.LocationId, this.UserId);
                    this.UnBilledItems = null;
                    this.Init_page();
                    // this.NotifyAction("Bill Generated :Reference " + InvoiceNumber + " Successfully .. Click :Open Bill to view the bill details", string.Format("Bill Generated {0} ", InvoiceNumber), false);
                }
                catch (Exception ex)
                {
                    this.NotifyAction("Attempt to generate bill(invoice) failed  " + ex.Message + " Successfully ..", string.Format("Bill/Invoice Generated {0} ", "Failure"), true);
                }
            }
            else
            {
                this.NotifyAction("Attempt to generate bill(invoice) failed. Ensure you have checked some items  " , string.Format("Bill/Invoice Generated {0} ", "Failure"),true);
            }

        }

        /// <summary>
        /// Handles the PreRender event of the buttonGenerateBill control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonGenerateBill_PreRender(object sender, EventArgs e)
        {
            //this.displayFlag = this.UnBilledItems.Rows.Count > 0 ? "" : "none";
            //this.buttonGenerateBill.Attributes.Add("style", "margin-right:5px; font-weight:bold;display:" + displayFlag);
        }

        /// <summary>
        /// Handles the Click event of the btnOkAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOkAction_Click(object sender, EventArgs e)
        {
            this.UnBilledItems = null;
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

                }
                catch (Exception ex)
                {
                    this.NotifyAction("Request to Reverse Receipt Number " + labelReceipt.Text + "  Failed ..<br />" + ex.Message,
                        string.Format("Reverse {0} ", labelReceipt.Text), true);
                }

            };

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
        /// Gets a value indicating whether this <see cref="frmBilling_ReverseBill"/> is debug.
        /// </summary>
        /// <value>
        ///   <c>true</c> if debug; otherwise, <c>false</c>.
        /// </value>
        bool Debug
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("DEBUG").ToLower().Equals("true");
            }
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
                lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team.  " + ex.Message;
                this.isError = this.divError.Visible = true;
                Exception lastError = ex;
                lastError.Data.Add("Domain", "Patient Consumeable Issueance Form");
                try
                {
                    Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                    logger.LogError(ex);
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// Handles the ActiveTabChanged event of the TabContainer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
        {
            this.GetContextTabData(TabContainer1.ActiveTabIndex);
        }

        #region PendingBills

        /// <summary>
        /// Shows the pay direct.
        /// </summary>
        /// <param name="billStatus">The bill status.</param>
        /// <param name="hasTransaction">The has transaction.</param>
        /// <returns></returns>
        protected string ShowPay(object billStatus, object hasTransaction)
        {
            string display = "none";
            if (!this.CanReceivePayment) return display;
            string status = billStatus.ToString().ToLower().Trim();
            bool hasTran = hasTransaction.ToString().ToLower().Trim() == "true";
            if (status == "open")
            {
                display = "";
                return display;
            }

            return display;
        }

        /// <summary>
        /// Shows the cancel.
        /// </summary>
        /// <param name="billStatus">The bill status.</param>
        /// <param name="hasTransaction">The has transaction.</param>
        /// <returns></returns>
        protected string ShowCancel(object billStatus, object hasTransaction)
        {
            string display = "none";
            string status = billStatus.ToString().ToLower().Trim();
            bool hasTran = hasTransaction.ToString().ToLower().Trim() == "true";
            if (status == "voided" || hasTran)
                display = "none";
            else display = "";
            return display;
        }
        /// <summary>
        /// Shows the write off.
        /// </summary>
        /// <param name="billStatus">The bill status.</param>
        /// <param name="hasTransaction">The has transaction.</param>
        /// <returns></returns>
        protected string ShowWriteOff(object billStatus, object hasTransaction)
        {
            string display = "none";
            if (!this.CanWriteOff) return display;

            string status = billStatus.ToString().ToLower().Trim();
            bool hasTran = hasTransaction.ToString().ToLower().Trim() == "true";
            if (status == "voided" || status == "closed")
                display = "none";
            else display = "";
            return display;
        }

        /// <summary>
        /// Handles the RowDataBound event of the gridPendingBills control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridPendingBills_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRow row = ((DataRowView)e.Row.DataItem).Row;
                    string billStatus = row["BillStatus"].ToString();
                    string _billID = row["billid"].ToString();
                    bool _hasTransaction = Convert.ToBoolean(row["HasTransaction"]);
                    PayMode _payMode = (PayMode)Convert.ToInt32(row["PayMode"].ToString());
                    Button buttonPay = e.Row.FindControl("buttonPay") as Button;
                    Button buttonPayDirect = e.Row.FindControl("buttonPayRedirect") as Button;
                    if (billStatus != "Open")
                    {
                        // LinkButton button = e.Row.Cells[5].Controls.OfType<LinkButton>().DefaultIfEmpty(null).FirstOrDefault();
                        if (buttonPay != null)
                            buttonPay.Enabled = false;
                        buttonPayDirect.Visible = false;
                    }
                    if (_hasTransaction || this.useDefaultPaymode)
                    {
                        if (buttonPay != null)
                        {
                            buttonPay.Enabled = false;
                            //buttonPay.Attributes.Add("display", "none");
                            buttonPay.Visible = false;
                            ModalPopupExtender mpeBillPay = e.Row.FindControl("mpeBillPay") as ModalPopupExtender;
                            mpeBillPay.Enabled = false;
                            ConfirmButtonExtender cbeBillPay = e.Row.FindControl("cbeBillPay") as ConfirmButtonExtender;
                            cbeBillPay.Enabled = false;
                        }
                        if (buttonPayDirect != null)
                        {
                            buttonPayDirect.Visible = true;
                            // if (useDefaultPaymode) _payMode = defaultPayMode;
                            buttonPayDirect.CommandName = "PayBill";// _payMode == PayMode.SelectItem ? "PaySelectItem" : "PayFullBill";
                        }

                    }
                    Button print = e.Row.FindControl("buttonInvoice") as Button;
                    if (print != null)
                    {
                        Application.Common.Utility objUtil = new Application.Common.Utility();
                        string urlParam = String.Format("openReportPage('./frmBilling_Invoice.aspx?BillRefCode={0}&RptCd=Invoice');return false;", _billID);

                        print.OnClientClick = urlParam;
                    }
                }

            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Handles the RowCommand event of the gridPendingBills control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridPendingBills_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int _billID;
            int index;
            try
            {
                if (e.CommandName == "Expand")
                {

                    index = Int32.Parse(e.CommandArgument.ToString());
                    gridPendingBills.SelectedIndex = index;
                    _billID = int.Parse(gridPendingBills.SelectedDataKey.Values["billID"].ToString());
                    bool _hasTransaction = Boolean.Parse(gridPendingBills.SelectedDataKey.Values["HasTransaction"].ToString());
                    GridViewRow row = (gridPendingBills.Rows[index]);

                    GridView nestedGrid = row.FindControl("gridBillItemList") as GridView;

                    Panel containerDiv = row.FindControl("ContainerDiv") as Panel;


                    ImageButton expandButton = row.FindControl("ExpandGridButton") as ImageButton;
                    if (expandButton != null && expandButton.ImageUrl.IndexOf("minus") != -1)
                    {
                        expandButton.ImageUrl = expandButton.ImageUrl.Replace("minus", "plus");

                        pendingBillsOpenITem.Value = "";

                        containerDiv.Style.Add(HtmlTextWriterStyle.Display, "none");

                    }
                    else if (expandButton != null && expandButton.ImageUrl.IndexOf("plus") != -1)
                    {
                        expandButton.ImageUrl = expandButton.ImageUrl.Replace("plus", "minus");
                        DataTable dt = this.GetItemsByBillID(_billID);
                        nestedGrid.DataSource = dt;
                        nestedGrid.DataBind();
                        DataTable dtTrans = this.GetBillTransactions(_billID);

                        containerDiv.Style.Add(HtmlTextWriterStyle.Display, "inline");
                        pendingBillsOpenITem.Value = containerDiv.ClientID;
                    }
                    return;
                }
                if (e.CommandName == "CancelBill")
                {
                    index = Int32.Parse(e.CommandArgument.ToString());
                    gridPendingBills.SelectedIndex = index;
                    _billID = int.Parse(gridPendingBills.SelectedDataKey.Values["billID"].ToString());
                    this.CancelBill(_billID);
                    this.PopulateOpenBills();

                    return;
                }
                if (e.CommandName == "PayBill")
                {
                    index = Int32.Parse(e.CommandArgument.ToString());
                    //  GridViewRow row = (gridPendingBills.Rows[index]);
                    //RadioButtonList payOption = row.FindControl("radPayOption") as RadioButtonList;
                    gridPendingBills.SelectedIndex = index;
                    _billID = int.Parse(gridPendingBills.SelectedDataKey.Values["billID"].ToString());
                    base.Session["BillID"] = _billID;
                    // radPayOption
                    string theUrl = "./frmBilling_PayBill.aspx";
                    //if (payOption.SelectedValue == "ByItems")
                    //{
                    //    theUrl = "./frmBilling_PayBillByItems.aspx";
                    //}

                    Response.Redirect(theUrl, true);

                    //TODO go to paybill page
                    return;
                }
                if (e.CommandName == "WriteOffBill")
                {
                    index = int.Parse(e.CommandArgument.ToString());
                    gridPendingBills.SelectedIndex = index;
                    _billID = int.Parse(gridPendingBills.SelectedDataKey.Values["billID"].ToString());
                    int patientId = int.Parse(gridPendingBills.SelectedDataKey.Values["PatientID"].ToString());
                    GridViewRow row = (gridPendingBills.Rows[index]);
                    decimal outstanding = decimal.Parse(row.Cells[4].Text.Trim());
                    IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
                    List<PaymentMethod> pm = bMgr.GetPaymentMethods("WriteOff");
                    if (pm != null && pm.Count > 0)
                    {
                        PaymentMethod writeOff = pm[0];
                        BillPaymentInfo paymentInfo = new BillPaymentInfo()
                        {
                            Amount = outstanding,
                            //AmountPayable = outstanding,
                            
                            BillId = _billID,
                            LocationId = this.LocationId,
                            Deposit = false,
                            PaymentMode = writeOff,
                            PrintReceipt = false,
                            TenderedAmount = 0.0M,
                            ReferenceNumber = ""
                        };
                        bMgr.SavePatientBillPayments(patientId, paymentInfo, this.UserId);
                        this.PopulateOpenBills();
                        this.NotifyAction("The outstanding amount has successfully been written off", "Write Off", false, "");
                        return;
                    }
                }
                //if (e.CommandName == "PaySelectItem" || e.CommandName == "PayFullBill")
                //{
                //    index = Int32.Parse(e.CommandArgument.ToString());
                //    GridViewRow row = (gridPendingBills.Rows[index]);
                //    RadioButtonList payOption = row.FindControl("radPayOption") as RadioButtonList;
                //    gridPendingBills.SelectedIndex = index;
                //    _billID = int.Parse(gridPendingBills.SelectedDataKey.Values["billID"].ToString());
                //    base.Session["BillID"] = _billID;
                //    string theUrl = (e.CommandName == "PayFullBill" ? "./frmBilling_PayBill.aspx" : "./frmBilling_PayBillByItems.aspx");
                //    Response.Redirect(theUrl, true);
                //    return;
                //}
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Handles the RowCreated event of the gridPendingBills control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridPendingBills_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView nestedGrid = e.Row.FindControl("gridBillItemList") as GridView;
                nestedGrid.RowCommand += new GridViewCommandEventHandler(nestedGrid_RowCommand);
                nestedGrid.RowDataBound += new GridViewRowEventHandler(nestedGrid_RowDataBound);

            }
        }
        /// <summary>
        /// Handles the RowDataBound event of the nestedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void nestedGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                Button removeButton = e.Row.FindControl("buttonRemove") as Button;
                if (removeButton != null)
                {
                    HiddenField HDTran = (HiddenField)e.Row.Parent.Parent.Parent.FindControl("HdnTransaction");
                    bool hasTransaction = bool.Parse(HDTran.Value.ToString());
                    string item = e.Row.Cells[1].Text;
                    removeButton.Attributes["onclick"] = "if(!confirm('Do you want to remove " + item + " from this bill?')){ return false; };";
                    removeButton.Enabled = !hasTransaction;
                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Handles the RowCommand event of the nestedGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void nestedGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("RemoveItem"))
                {
                    int index = int.Parse(e.CommandArgument.ToString());
                    GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    GridView myGrid = (GridView)sender;
                    int _billID = int.Parse(myGrid.DataKeys[row.RowIndex].Values["BillID"].ToString());
                    int billItemID = int.Parse(myGrid.DataKeys[row.RowIndex].Values["billItemID"].ToString());
                    IBilling objBilling = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling,BusinessProcess.Billing");
                    objBilling.RemoveItemFromBill(billItemID, _billID, this.UserId);
                    DataTable dt = this.GetItemsByBillID(_billID);
                    myGrid.DataSource = dt;
                    myGrid.DataBind();
                    this.PopulateOpenBills();
                    //myGrid.Style.Add(HtmlTextWriterStyle.Display, "inline");
                    // pendingBillsOpenITem.Value = containerDiv.ClientID;

                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        #endregion

        #region ClosedBills

        /// <summary>
        /// Handles the RowDataBound event of the gridBills control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridClosedBills_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRow row = ((DataRowView)e.Row.DataItem).Row;
                    string billStatus = row["BillStatus"].ToString();
                    string _billID = row["BillID"].ToString();
                    //if (billStatus != "Paid")
                    //{
                    //    LinkButton button = e.Row.Cells[5].Controls.OfType<LinkButton>().DefaultIfEmpty(null).FirstOrDefault();
                    //    if (button != null)
                    //        button.Enabled = false;
                    //}
                    Button print = e.Row.FindControl("invoicePrint") as Button;
                    if (print != null)
                    {

                        string urlParam = String.Format("openReportPage('./frmBilling_Invoice.aspx?BillRefCode={0}&RptCd=Invoice');return false;", _billID);

                        print.OnClientClick = urlParam;
                    }
                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Handles the RowCommand event of the gridBills control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridClosedBills_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int billID;
            //int _transactionID;
            int index;
            try
            {
                if (e.CommandName == "Expand")
                {

                    index = Int32.Parse(e.CommandArgument.ToString());
                    gridClosedBills.SelectedIndex = index;
                    billID = int.Parse(gridClosedBills.SelectedDataKey.Values["billID"].ToString());
                    GridViewRow row = (gridClosedBills.Rows[index]);

                    GridView nestedGrid = row.FindControl("gridBillTransaction") as GridView;

                    Panel containerDiv = row.FindControl("ContainerDiv") as Panel;

                    ImageButton expandButton = row.FindControl("ExpandGridButton") as ImageButton;
                    if (expandButton != null && expandButton.ImageUrl.IndexOf("minus") != -1)
                    {
                        expandButton.ImageUrl = expandButton.ImageUrl.Replace("minus", "plus");

                        OpenedDivsHiddenField.Value = "";

                        containerDiv.Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                    else if (expandButton != null && expandButton.ImageUrl.IndexOf("plus") != -1)
                    {
                        expandButton.ImageUrl = expandButton.ImageUrl.Replace("plus", "minus");
                        DataTable dt = this.GetBillTransactions(billID);
                        nestedGrid.DataSource = dt;
                        nestedGrid.DataBind();
                        containerDiv.Style.Add(HtmlTextWriterStyle.Display, "inline");
                        OpenedDivsHiddenField.Value = containerDiv.ClientID;
                    }
                    return;
                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Handles the RowCreated event of the gridClosedBills control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridClosedBills_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView transactionGrid = e.Row.FindControl("gridBillTransaction") as GridView;
                if (transactionGrid != null)
                {
                    transactionGrid.RowCommand += new GridViewCommandEventHandler(transactionGrid_RowCommand);
                    transactionGrid.RowDataBound += new GridViewRowEventHandler(transactionGrid_RowDataBound);
                }
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the transactionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void transactionGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DataRow row = ((DataRowView)e.Row.DataItem).Row;
                    string transactionStatus = row["TransactionStatus"].ToString();
                    bool IsReversible = Convert.ToBoolean(row["Reversible"]);
                    string _transactionID = row["TransactionID"].ToString();
                    string _transactionRef = row["ReceiptNumber"].ToString();
                    Button printButton = e.Row.FindControl("receiptPrint") as Button;
                    if (transactionStatus != "Paid" || !IsReversible)
                    {
                        Button reverseButton = e.Row.FindControl("buttonReverse") as Button;
                        if (reverseButton != null)
                            reverseButton.Enabled = false;
                        if (printButton != null)
                            printButton.Enabled = false;
                    }
                    if (printButton != null)
                    {
                        Application.Common.Utility objUtil = new Application.Common.Utility();
                        string urlParam = String.Format("openReportPage('./frmBilling_Reciept.aspx?ReceiptTrxCode={0}&RePrint=true');return false;", _transactionRef);
                        printButton.OnClientClick = urlParam;
                    }
                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
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
        /// Handles the RowCommand event of the transactionGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void transactionGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int _transactionID;
            int index;

            //int index = Int32.Parse(e.CommandArgument.ToString());
            //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            //GridView myGrid = (GridView)sender;
            try
            {
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
                    this.PopulateClosedBills();
                    return;
                }
                if (e.CommandName == "PrintReceipt")
                {

                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        #endregion

        #region Reversals | Refund
        /// <summary>
        /// Handles the RowCommand event of the ReverseTransaction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void ReverseTransaction_RowCommand(object sender, CommandEventArgs e)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Handles the NotifyCommand event of the ReverseTransaction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void ReverseTransaction_NotifyCommand(object sender, CommandEventArgs e)
        {
            //List<KeyValuePair<string, string>> param = e.CommandArgument as List<KeyValuePair<string, string>>;
            //string strMessage = param.Find(l => l.Key == "Message").Value;
            //string strTitle = param.Find(l => l.Key == "Message").Value;
            //bool errorFlag = param.Find(l => l.Key == "errorFlag").Value.Equals("true");
            //this.NotifyAction(strMessage, strTitle, errorFlag);
        }

        /// <summary>
        /// Handles the ErrorOccurred event of the ReverseTransaction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void ReverseTransaction_ErrorOccurred(object sender, CommandEventArgs e)
        {
            // throw new NotImplementedException();
            this.buttonHidden_Click(sender, null);
            Exception ex = e.CommandArgument as Exception;
            this.showErrorMessage(ref ex);
        }
        #endregion

        #region "Deposit"
        /// <summary>
        /// Handles the Click event of the buttonHidden control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void buttonHidden_Click(object sender, EventArgs e)
        {

        }
        #endregion

        protected void btnPatientInterimBill_Click(object sender, EventArgs e)
        {
            IBilling objBilling = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling,BusinessProcess.Billing");
            DataTable outstandingDT= objBilling.getOutstandingBill(PatientId);
            Session["CBillingReport"] = outstandingDT;
            

          //  lblUserName.Text = "Printed by: " + Session["AppUserName"];
          //  lblFacilityName.Text = Session["AppLocation"].ToString();
            Session["ReportName"] = "Outstanding Bill(Interim Bill) - " + CurrentSession.Current.CurrentPatient.FullName;
            Session["ReportParameters"] = "As at " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt");
           // string theUrl = "./frmCustomReportPrint.aspx";

           // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "InterimBill", "printInterimBill();", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "InterimBill", "printInterimBill();", true);

           // Response.Redirect(theUrl, true);
        }
    }

}
