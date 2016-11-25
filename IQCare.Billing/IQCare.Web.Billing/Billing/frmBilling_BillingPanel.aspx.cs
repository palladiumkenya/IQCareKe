using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Linq;
using IQCare.Web.UILogic;

namespace IQCare.Web.Billing
{
    public partial class frmBilling_BillingPanel : System.Web.UI.Page
    {
        /// <summary>
        /// The is error
        /// </summary>
        bool isError = false;

        /// <summary>
        /// The authentication
        /// </summary>
        AuthenticationManager Authentication = new AuthenticationManager();

        /// <summary>
        /// The session_ key
        /// </summary>
        private readonly string Session_Key = "TY!$#%CONSTXYW";
        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        int UserID
        {
            get
            {
                return Convert.ToInt32(base.Session["AppUserId"]);
            }
        }
        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName
        {
            get
            {
                return base.Session["AppUserName"].ToString();
            }
        }
        /// <summary>
        /// Gets or sets the patient items.
        /// </summary>
        /// <value>
        /// The patient items.
        /// </value>
        List<IssuableItem> PatientItems
        {
            get
            {
                if (base.Session[Session_Key] == null)
                    return new List<IssuableItem>();
                else
                {
                    return (List<IssuableItem>)base.Session[Session_Key];
                }
            }
            set
            {
                base.Session[Session_Key] = value;
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
        /// 
        /// </summary>
        enum FormRenderMode
        {
            Clinical = 1,
            Billing = 2

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
                bool debug = false;
                bool.TryParse(ConfigurationManager.AppSettings.Get("DEBUG").ToLower(), out debug);
                return debug;
            }
        }
        /// <summary>
        /// Gets or sets the selected date.
        /// </summary>
        /// <value>
        /// The selected date.
        /// </value>
        DateTime SelectedDate
        {
            get
            {
                return DateTime.Parse(this.HSelectedDate.Value);
            }
            set
            {
                HSelectedDate.Value = value.ToString("dd-MMM-yyyy");
                Session["SelectedDate"] = value.ToString("dd-MMM-yyyy");
            }
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
        /// Sets the consumable item type identifier. Consumables are excluded from the search functionality
        /// </summary>
        void SetConsumableItemTypeID()
        {

            //  IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling,BusinessProcess.SCM");
            //if (this.Authentication.HasFeatureRight(ApplicationAccess.Consumables, (DataTable)Session["UserRight"]) == false)
            //{
            IItemMaster bMgr = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
            Session["ConsumableTypeID"] = bMgr.GetItemTypeIDByName("Consumables").ToString();
            //}
            //else
            //{
            //    Session["ConsumableTypeID"] = null;
            //}
        }
        /// <summary>
        /// Binds the dropdown.
        /// </summary>
        void BindDropdown()
        {
           
            BindFunctions BindManager = new BindFunctions();
            IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet DSModules = ptnMgr.GetModuleNames(this.LocationId);

            DataTable theDT = new DataTable();
            theDT = DSModules.Tables[0];
            
            if (CurrentMode == FormRenderMode.Clinical)
            {
                theDT.DefaultView.RowFilter = string.Format("ModuleID ={0}", (base.Session["TechnicalAreaId"].ToString()));


            }
            theDT = theDT.DefaultView.ToTable();
            if (theDT.Rows.Count > 0)
            {
                BindManager.BindCombo(ddlCostCenter, theDT, "ModuleName", "ModuleID");
                ptnMgr = null;
            }
           
        }

        /*void PopulatePatientDetails()
        {
            DataTable theDT;
           
            IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            theDT = ptnMgr.GetPatientRecord(Convert.ToInt32(Session["PatientId"]));
            ptnMgr = null;

            if (theDT.Rows.Count > 0)
            {
                lblname.Text = String.Format("{0} {1} {2}", theDT.Rows[0]["Firstname"], theDT.Rows[0]["Middlename"], theDT.Rows[0]["Lastname"]);
                //   lblsex.Text = theDT.Rows[0]["sex"].ToString();
                lblsex.Text = (theDT.Rows[0]["sex"].ToString() == "16") ? "Male" : "Female";
                lbldob.Text = theDT.Rows[0]["Age"].ToString() + " years";
                lblFacilityID.Text = theDT.Rows[0]["PatientFacilityID"].ToString();
            }
        }*/
        /// <summary>
        /// Binds the grid.
        /// </summary>
        void BindGrid()
        {
            try
            {
                this.PatientItems = (List<IssuableItem>)Session[this.Session_Key];
                List<IssuableItem> itemsToBind = new List<IssuableItem>();
                var activeItems = this.PatientItems.Where(it => it.Active == true);
                if (this.PatientItems.Count == 0 || activeItems.Count() == 0)
                {

                    itemsToBind.Add(new IssuableItem()
                    {
                        IssueDate = SelectedDate,
                        IssuedById = this.UserID,
                        PatientId = this.PatientId,
                        LocationId = this.LocationId
                    });
                    gridItems.DataSource = itemsToBind;
                    gridItems.DataBind();
                    gridItems.Rows[0].Visible = false;
                }
                else
                {
                    gridItems.DataSource = activeItems;
                    gridItems.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Populates the items.
        /// </summary>
        /// <param name="issueDate">The issue date.</param>
        /// <param name="parForce">if set to <c>true</c> [par force].</param>
        void PopulateItems(DateTime issueDate, bool parForce = false)
        {
            try
            {
                //if (parForce || base.Session[this.Session_Key] == null)
                //{
                IBilling bmGr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling,BusinessProcess.Billing");
                //this.PatientItems = _consumablemManager.GetPatientConsumableByDate(this.PatientID, issueDate);
                int? itemTypeID = null;
                if (CurrentMode == FormRenderMode.Clinical) itemTypeID = Convert.ToInt32(Session["ConsumableTypeID"]);
                this.PatientItems = bmGr.GetPatientsItemsIssuedByUserID(this.PatientId, this.LocationId, this.UserID, issueDate, itemTypeID);

                if (parForce) this.BindGrid();

            }
            catch (Exception ex)
            {
                isError = true;
                showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Gets the search patient URL.
        /// </summary>
        /// <value>
        /// The search patient URL.
        /// </value>
        string BillingSearchPatientURL
        {
            get
            {
                return string.Format("{0}?FormName={1}&mnuClicked={2}", "~/Billing/frmFindPatient.aspx", "QuickPanel", "QuickPanel");
            }
        }
        /// <summary>
        /// Gets the clinical search patient URL.
        /// </summary>
        /// <value>
        /// The clinical search patient URL.
        /// </value>
        string ClinicalSearchPatientURL
        {
            get
            {
                return string.Format("{0}?FormName={1}&mnuClicked={2}", "..//frmFindAddPatient.aspx", "Consumables", "Consumables");
                //  return string.Format("{0}?FormName={1}&mnuClicked={2}", "~/Billing/frmFindPatient.aspx", "QuickPanel", "QuickPanel");
            }
        }
        FormRenderMode CurrentMode
        {
            get
            {
                if (this.Session["FormMode"] == null)
                {
                    this.Session["FormMode"] = FormRenderMode.Clinical;
                }
                return (FormRenderMode)base.Session["FormMode"];
            }
            set
            {
                Session["FormMode"] = value;
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
                if (IsPostBack) return;
                string theUrl = "";
                if (this.Request.QueryString["mode"] == "billing")
                {
                    if (!Authentication.HasFeatureRight(ApplicationAccess.BillingFeature.QuickPanel, (DataTable)Session["UserRight"]))
                    {
                        System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                        Response.Redirect(string.Format("{0}", "./frmLogin.aspx"), true);
                    }
                    CurrentMode = FormRenderMode.Billing;
                    base.Session["TechnicalAreaId"] = null;
                    theUrl = this.BillingSearchPatientURL;
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Billing >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Quick Panel";
                }
                else
                {
                    CurrentMode = FormRenderMode.Clinical;
                    theUrl = this.ClinicalSearchPatientURL;

                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Quick Panel";
                    //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Patient Consumables";
                }

                if (CurrentSession.Current.CurrentPatient == null)
                {
                    CurrentMode = FormRenderMode.Billing;
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(this.BillingSearchPatientURL, true);
                }
                this.SelectedDate = this.calendar1.SelectedDate = DateTime.Today;
                this.BindDropdown();
                this.SetConsumableItemTypeID();
                this.PopulateItems(DateTime.Today, true);

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
            Master.PageScriptManager.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(PageScriptManager_AsyncPostBackError);

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
        /// Gets a value indicating whether [show footer].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show footer]; otherwise, <c>false</c>.
        /// </value>
        bool ShowFooter
        {
            get
            {
                bool showFooter = false;
                if (this.CurrentMode == FormRenderMode.Billing)
                {
                    showFooter = true;
                }
                else if (this.CurrentMode == FormRenderMode.Clinical)
                {
                    try
                    {
                        int techID = Convert.ToInt32(base.Session["TechnicalAreaId"].ToString());
                        showFooter = true;
                    }
                    catch { showFooter = false; }
                }
                return showFooter;
            }
        }
        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {

            divError.Visible = isError;

           // buttonClose.Visible = panelPatient.Visible = (this.CurrentMode == FormRenderMode.Billing);
            buttonClose.Visible =  (this.CurrentMode == FormRenderMode.Billing);
            
            try { gridItems.FooterRow.Visible = this.ShowFooter; }
            catch { }// this.ShowFooter;// (this.CurrentMode == FormRenderMode.Billing) || ((this.CurrentMode == FormRenderMode.Clinical) && techID != null);

            if ((this.CurrentMode == FormRenderMode.Billing))
            {
                this.SetStyle();
               // this.PopulatePatientDetails();
            }

            this.btnSaveItems.Enabled = this.PatientItems.Count(it => it.Active == false || it.ItemIssuanceId.HasValue == false) > 0;
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.TemplateControl.Error" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnError(EventArgs e)
        {
            // base.OnError(e);
            Exception lastError = base.Server.GetLastError();
            this.showErrorMessage(ref lastError);
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
        /// Items the cost center.
        /// </summary>
        /// <returns></returns>
        string ItemCostCenter(string itemTypeName)
        {
            switch (itemTypeName)
            {
                case "Pharmaceuticals":
                    return "Pharmacy";
                case "Ward Admission":
                    return "Inpatient";
                case "Lab Tests":
                    return "Laboratory";
                default:
                    return "";
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
            //int? consumableItemTypeID = null;
            //if (System.Web.HttpContext.Current.Session["ConsumableTypeID"] != null)
            //{
            //    consumableItemTypeID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ConsumableTypeID"].ToString());
            //}
            int? itemTypeID = null;
            //if ((FormRenderMode)System.Web.HttpContext.Current.Session["FormMode"] == FormRenderMode.Clinical)
               // itemTypeID = Convert.ToInt32(System.Web.HttpContext.Current.Session["ConsumableTypeID"]);

            DateTime dt = DateTime.Parse(System.Web.HttpContext.Current.Session["SelectedDate"].ToString());
            // int.TryParse(System.Web.HttpContext.Current.Session["ConsumableTypeID"].ToString(), out consumableItemTypeID);
            DataTable dataTable = new DataTable("Items");
            if ((FormRenderMode)System.Web.HttpContext.Current.Session["FormMode"] == FormRenderMode.Clinical)
            {
                dataTable = _iMGR.ClinicalFindItems(prefixText);
            }
            else
            {
                dataTable = _iMGR.FindItems(prefixText, itemTypeID, null, dt, false);
            }
            string custItem = string.Empty;
            foreach (DataRow theRow in dataTable.Rows)//.Select("ItemTypeName <> 'Consumables'"))
            {
                custItem = AutoCompleteExtender.CreateAutoCompleteItem(theRow[1].ToString(), String.Format("{0};{1};{2};{3}", theRow["ItemID"], theRow["ItemTypeID"], theRow["SellingPrice"], theRow["ItemTypeName"]));

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
        /// Handles the textChanged event of the txtNewDescription control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void txtNewDescription_textChanged(object sender, EventArgs e)
        {
            int ItemId, itemType;
            string itemTypeName = "";
            double itemPrice;
            try
            {
                if (HItemName.Value != "")
                {
                    String[] itemCodes = HItemName.Value.Split(';');
                    if (itemCodes.Length == 4)
                    {
                        ItemId = Convert.ToInt32(itemCodes[0]);
                        itemType = Convert.ToInt32(itemCodes[1]);
                        itemTypeName = itemCodes[3].ToString();
                        itemPrice = Convert.ToDouble(itemCodes[2]);
                        IBilling bMgr = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling,BusinessProcess.Billing");
                        itemPrice = bMgr.GetItemPrice(ItemId, itemType, DateTime.Parse(this.SelectedDate.ToString()));
                        //if (itemPrice == 0.00M)
                        //{
                        //    ((TextBox)sender).Text = "";
                        //    HItemName.Value = "";
                        //}
                        HItemTypeName.Value = itemTypeName;
                        GridViewRow row = (GridViewRow)((TextBox)sender).NamingContainer;
                        Label lblNewUnitPrice = (Label)row.FindControl("lblNewUnitPrice");
                        Label lblNewAmountPrice = (Label)row.FindControl("lblNewAmountPrice");
                        TextBox txtNewQuantity = (TextBox)row.FindControl("txtNewQuantity");
                        lblNewAmountPrice.Text = lblNewUnitPrice.Text = itemPrice.ToString();
                        HItemID.Value = ItemId.ToString();
                        HItemTypeID.Value = itemType.ToString();
                        Label lblCostCenter = (Label)row.FindControl("lblCostCenter");
                        lblCostCenter.Text = this.ItemCostCenter(itemTypeName);
                        if (lblCostCenter.Text == "")
                        {
                            DropDownList ddl = row.FindControl("ddlItemCostCenter") as DropDownList;
                            if (ddl != null)
                            {
                                ListItem[] arr = new ListItem[ddlCostCenter.Items.Count];
                                ddlCostCenter.Items.CopyTo(arr, 0);
                                ddl.Items.AddRange(arr);
                                if (ddl.Items.Count == 2)
                                {
                                    ddl.SelectedIndex = 1;
                                }

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
                else
                {
                    ((TextBox)sender).Text = "";
                    HItemName.Value = "";
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
                if (HItemName.Value != "")
                {
                    String[] itemCodes = HItemName.Value.Split(';');
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
                        lblCostCenter.Text = this.ItemCostCenter(itemTypeName);
                        if (lblCostCenter.Text == "")
                        {
                            DropDownList ddl = row.FindControl("ddlItemCostCenter") as DropDownList;
                            if (ddl != null)
                            {
                                ListItem[] arr = new ListItem[ddlCostCenter.Items.Count];
                                ddlCostCenter.Items.CopyTo(arr, 0);
                                ddl.Items.AddRange(arr);
                                ddl.Visible = true;
                                lblCostCenter.Visible = false;
                                if (ddl.Items.Count == 2)
                                {
                                    ddl.SelectedIndex = 1;
                                }
                            }
                        }
                        else
                        {
                            lblCostCenter.Visible = true;
                        }

                    }
                }
                else
                {
                    ((TextBox)sender).Text = "";
                    HItemName.Value = "";
                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the gridItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridItems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    IssuableItem issuedItem = (IssuableItem)e.Row.DataItem;
                    // Retrieve the serviceStatus value for the current row.
                    //if service/item has been given then it becomes uneditable
                    bool ServiceReceived = issuedItem.Received;
                    bool CanBeBilled = issuedItem.CanBeBilled;
                    bool persisted = issuedItem.ItemIssuanceId.HasValue;
                    bool Billed = issuedItem.Billed;
                    bool sentToBill = issuedItem.BillItemId.HasValue;
                    //bool canEdit = issuedItem.ControlFlag != "Persisted";
                    bool canEdit = !persisted || !sentToBill;

                    bool canDelete = (!issuedItem.Billed);


                    Button btnEdit = (Button)e.Row.FindControl("buttonEdit");
                    // btnEdit.Visible = canEdit;
                    btnEdit.Visible = false;
                    //disable the delete button too
                    Button btnDelete = (Button)e.Row.FindControl("buttonDelete");
                    btnDelete.Visible = canDelete;
                    //}
                    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                    {
                        string thisItemModule = (issuedItem.ModuleId.ToString());
                        string costCenterName = issuedItem.CostCenter;
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
                                ListItem item = ddl.Items.FindByValue(thisItemModule);
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
                        if (sentToBill)
                        {
                            TextBox textItemName = (TextBox)e.Row.FindControl("txtEditDescription");
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
        /// The valid
        /// </summary>
        private bool valid = true;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is valid entry.
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
        /// <summary>
        /// Validates the drop down.
        /// </summary>
        /// <param name="selectList">The select list.</param>
        /// <returns></returns>
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
        /// Handles the RowCommand event of the gridItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("AddItem"))
            {
                try
                {
                    TextBox txtNewDescription = (TextBox)gridItems.FooterRow.FindControl("txtNewDescription");
                    TextBox txtNewQuantity = (TextBox)gridItems.FooterRow.FindControl("txtNewQuantity");
                    DropDownList ddlNewPaymentMode = (DropDownList)gridItems.FooterRow.FindControl("ddlNewPaymentMode");

                    Label lblNewUnitPrice = (Label)gridItems.FooterRow.FindControl("lblNewUnitPrice");
                    //TextBox txtNewitemId = (TextBox)grdCurrentBill.FooterRow.FindControl("txtNewitemId");
                    // TextBox txtNewItemType = (TextBox)grdCurrentBill.FooterRow.FindControl("txtNewItemType");

                    valid = true;
                    isValidEntry = validateTextBox(txtNewDescription);
                    isValidEntry = validateTextBox(txtNewQuantity);
                    int moduleID = 0;
                    string costCenterName = "";
                    GridViewRow gridRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                    if (HItemTypeName.Value == "Consumables" || HItemTypeName.Value == "Billables" || HItemTypeName.Value == "Visit Type")
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

                        return;
                    }
                    // List<IssuableItem> theItems = this.PatientItems;
                    IssuableItem newItem = new IssuableItem()
                    {
                        PatientId = this.PatientId,
                        LocationId = this.LocationId,
                        IssueDate = this.SelectedDate,
                        ItemId = int.Parse(HItemID.Value),
                        ItemName = txtNewDescription.Text,
                        ItemTypeId = int.Parse(HItemTypeID.Value),
                        IssuedById = this.UserID,
                        IssuedQuantity = Int32.Parse(txtNewQuantity.Text),
                        SellingPrice = Double.Parse(lblNewUnitPrice.Text),
                        Paid = false,
                        Received = true,
                        Active = true,
                        ModuleId = moduleID,
                        CostCenter = costCenterName,
                        IssuedByName = this.UserName
                    };
                    this.PatientItems.Add(newItem);
                    this.BindGrid();

                }

                catch (Exception ex)
                {
                    this.showErrorMessage(ref ex);
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnFind control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnFind_Click(object sender, EventArgs e)
        {
            base.Session.Remove("PatientId");
            CurrentSession.Current.ResetCurrentPatient();
            Response.Redirect(this.BillingSearchPatientURL, false);
        }

        /// <summary>
        /// Handles the SelectionChanged event of the calendar1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void calendar1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                this.SelectedDate = this.calendar1.VisibleDate = this.calendar1.SelectedDate;
                //load the consumables for the day
                this.PopulateItems(this.SelectedDate, true);
                // divComponent.Update();
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Handles the DayRender event of the calendar1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DayRenderEventArgs"/> instance containing the event data.</param>
        protected void calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            e.Cell.Style.Add("cursor", "hand");
            e.Cell.Attributes.Add("onClick", e.SelectUrl);
        }

        /// <summary>
        /// Handles the RowCancelingEdit event of the gridItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCancelEditEventArgs"/> instance containing the event data.</param>
        protected void gridItems_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gridItems.EditIndex = -1;

            //fillBillGrids(false);
            this.BindGrid();
        }

        /// <summary>
        /// Handles the RowDeleting event of the gridItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewDeleteEventArgs"/> instance containing the event data.</param>
        protected void gridItems_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                //DataTable theDT = (DataTable)Session["billingInformation"];

                List<IssuableItem> theItems = this.PatientItems;
                // List<IssuableItem> theItems2 = this.PatientItems;
                IssuableItem theItem = this.PatientItems[e.RowIndex];

                if (theItem.ItemIssuanceId.HasValue)
                {
                    theItem.Active = false;
                }
                else
                {
                    this.PatientItems.RemoveAt(e.RowIndex);

                }


                //int i2,i1,i3;
                //i2= theItems2.Count;
                // i1 = theItems.Count;
                // i3 = this.PatientItems.Count;

                //this.PatientItems = theItems;
                this.BindGrid();
            }

            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Handles the RowUpdating event of the gridItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewUpdateEventArgs"/> instance containing the event data.</param>
        protected void gridItems_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {

                Label lblUpdateDate = (Label)gridItems.Rows[e.RowIndex].FindControl("lblEditDate");
                TextBox txtUpdateDescription = (TextBox)gridItems.Rows[e.RowIndex].FindControl("txtEditDescription");
                TextBox txtUpdateQuantity = (TextBox)gridItems.Rows[e.RowIndex].FindControl("txtEditQuantity");
                Label lblUpdateUnitPrice = (Label)gridItems.Rows[e.RowIndex].FindControl("lblEditUnitPrice");
                valid = true;
                isValidEntry = validateTextBox(txtUpdateQuantity);
                isValidEntry = validateTextBox(txtUpdateDescription);
                int moduleID = 0;
                string costCenterName = "";
                GridViewRow gridRow = gridItems.Rows[e.RowIndex];
                string oldDepartment, oldItemID, oldItemTypeName, oldDeptID, oldItemTypeID;
                int oldQTY;


                //   List<IssuableItem> theItems = this.PatientItems;

                IssuableItem itemEdit = this.PatientItems[e.RowIndex];
                bool Persisted = itemEdit.ItemIssuanceId.HasValue;
                if (Persisted && itemEdit.Billed)
                {
                    e.Cancel = true;
                    gridItems.EditIndex = -1;
                    this.BindGrid();
                    return;
                }

                //if (gridItems.DataKeys[e.RowIndex].Values["BillItemID"].ToString() != "")
                //    billItemID = int.Parse(gridItems.DataKeys[e.RowIndex].Values["BillItemID"].ToString());

                oldItemTypeName = itemEdit.ItemTypeName;
                oldItemTypeID = itemEdit.ItemTypeId.ToString();
                oldDeptID = itemEdit.ModuleId.ToString();
                costCenterName = oldDepartment = itemEdit.CostCenter;
                oldItemID = itemEdit.ItemId.ToString();
                oldQTY = itemEdit.IssuedQuantity;

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
                    gridItems.EditIndex = -1;
                    //  return;
                }
                else
                {
                    if (!itemEdit.ItemIssuanceId.HasValue)
                    {

                        this.PatientItems.Remove(itemEdit);
                    }
                    else
                    {
                        itemEdit.Active = false;

                        IssuableItem itemNew = new IssuableItem()
                        {
                            //   ControlFlag = "Added",
                            ItemName = txtUpdateDescription.Text,
                            ItemId = int.Parse(newItemID),
                            IssuedQuantity = Int32.Parse(txtUpdateQuantity.Text),
                            CostCenter = costCenterName,
                            ModuleId = moduleID,
                            SellingPrice = Double.Parse(lblUpdateUnitPrice.Text),
                            ItemTypeId = int.Parse(newItemTypeID),
                            ItemTypeName = newItemTypeName,
                            Paid = false,
                            Active = true,
                            IssuedById = this.UserID,
                            IssuedByName = this.UserName,
                            IssueDate = this.SelectedDate,
                            LocationId = this.LocationId,
                            PatientId = this.PatientId
                        };
                        this.PatientItems.Add(itemNew);

                    }
                }
                //  this.PatientItems = theItems;
                this.gridItems.EditIndex = -1;
                this.BindGrid();
            }

            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }

        }

        /// <summary>
        /// Handles the RowEditing event of the gridItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewEditEventArgs"/> instance containing the event data.</param>
        protected void gridItems_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gridItems.EditIndex = e.NewEditIndex;

            // fillBillGrids(false);
            this.BindGrid();
        }

        /// <summary>
        /// Handles the Click event of the btnSaveItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnSaveItems_Click(object sender, EventArgs e)
        {
            try
            {
                IBilling bMGR = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.Billing.BBilling, BusinessProcess.Billing");
                List<IssuableItem> _itemsToSave = this.PatientItems.Where(it => it.ItemIssuanceId.HasValue == false || it.Active == false).ToList<IssuableItem>();

                bMGR.IssueItem(_itemsToSave, this.PatientId, this.LocationId, this.UserID);
                //this.PopulateItems(SelectedDate, true);

                this.PopulateItems(this.SelectedDate, true);
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonClose_Click(object sender, EventArgs e)
        {
            Session["PatientId"] = null;
            base.Session[Session_Key] = null;
            this.Session["FormMode"] = null;
            string theUrl = string.Format("{0}", "./Home.aspx");
            //Response.Redirect(theUrl);
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect(theUrl, true);
        }

        /// <summary>
        /// Handles the VisibleMonthChanged event of the calendar1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MonthChangedEventArgs"/> instance containing the event data.</param>
        protected void calendar1_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            try
            {

                calendar1.VisibleDate = new DateTime(e.NewDate.Year, e.NewDate.Month, 1);
                DateTime calendarDate = this.calendar1.SelectedDate = this.calendar1.VisibleDate;
                this.SelectedDate = calendarDate;
                this.PopulateItems(calendarDate, true);
                //this.updatePanelGrid.Update();

            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
    }
}