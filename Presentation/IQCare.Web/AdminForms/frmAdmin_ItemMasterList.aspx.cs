using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Application.Presentation;
using Interface.Administration;
using IQCare.Web.UILogic;

namespace IQCare.Web.Admin
{
    public partial class ItemMasterList : BasePage
    {
        #region variable declaration
        /// <summary>
        /// main dataset
        /// </summary>
        DataSet dsData = null;
        /// <summary>
        /// The is error
        /// </summary>
        bool isError = false;
        /// The item manager
        /// </summary>
        IItemMaster itemManager
        {
            get
            {
                return (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this <see cref="frmAdmin_ItemMasterList"/> is debug.
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
        /// Gets or sets the index of the active tab.
        /// </summary>
        /// <value>
        /// The index of the active tab.
        /// </value>
        int ItemType
        {
            get
            {
                return int.Parse(HItemType.Value);
            }
            set
            {
                HItemType.Value = value.ToString();
                Session["MainItemType"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of the item sub.
        /// </summary>
        /// <value>
        /// The type of the item sub.
        /// </value>
        int ItemSubType
        {
            get
            {
                return int.Parse(HSubItemID.Value);
            }
            set
            {
                HSubItemID.Value = value.ToString();

            }
        }

        /// <summary>
        /// Gets or sets the item identifier.
        /// </summary>
        /// <value>
        /// The item identifier.
        /// </value>
        int ItemID
        {
            get
            {
                return int.Parse(HItemID.Value);
            }
            set
            {
                HItemID.Value = value.ToString();
            }
        }
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
                return Convert.ToInt32(Session["AppUserId"]);
            }
        }
        /// <summary>
        /// Gets the item XSD schema.
        /// </summary>
        /// <value>
        /// The item XSD schema.
        /// </value>
        string ItemXSDSchema
        {
            get
            {
                string xsd =
                    @"<?xml version=""1.0"" standalone=""yes""?>
<xs:schema id=""ItemMasterData"" xmlns="""" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:msdata=""urn:schemas-microsoft-com:xml-msdata""> 	
	<xs:element name=""ItemMaster"" msdata:IsDataSet=""true"" msdata:UseCurrentLocale=""true"">
		<xs:complexType> 
			<xs:choice minOccurs=""0"" maxOccurs=""unbounded"">                
				<xs:element name=""Item"" maxOccurs=""unbounded"" minOccurs=""0"">
					 <xs:complexType>
						<xs:sequence>
                            <xs:element name=""item_pk""       type=""xs:int"" />
							<xs:element name=""itemname""      type=""xs:string"" />       
							<xs:element name=""itemtypename""  type=""xs:string"" />
                            <xs:element name=""itemtypeid""    type=""xs:int"" />                            
							<xs:element name=""status""        type=""xs:string""/>	
                            <xs:element name=""hasdetails""    type=""xs:boolean""  default=""false"" />							 
						</xs:sequence>
					</xs:complexType>
				</xs:element>			                                   
			</xs:choice>
		</xs:complexType>
	</xs:element>
 </xs:schema>";
                return (xsd);
            }
        }
        /// <summary>
        /// Gets or sets the type of the action.
        /// </summary>
        /// <value>
        /// The type of the action.
        /// </value>
        string ActionType
        {
            get
            {
                return this.HActionType.Value;
            }
            set
            {
                this.HActionType.Value = value;
            }
        }
        /// <summary>
        /// Gets a value indicating whether [have sub types].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [have sub types]; otherwise, <c>false</c>.
        /// </value>
        protected bool HaveSubTypes
        {
            get
            {
                if (ddlMainItem.SelectedIndex > 0)
                {
                    return (ddlMainItem.SelectedItem.Text == "Consumables");
                }
                else return false;
            }
        }
        #endregion /// <summary>

        #region Page Life Cycle
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //  this.ActiveTabIndex = 1;
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Master Lists >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Item Master";
                this.PopulateMainItemType();
                if (ddlMainItem.Items.Count > 1)
                {
                    ddlMainItem.SelectedIndex = 1;
                    SelectedItemTypeChanged(ddlMainItem, e);

                }
            }
            else
            {
                if (Request.Params["__EVENTTARGET"].ToString() == ChangeTabs.UniqueID)
                {
                    // Fire event
                    string v = this.HActiveTabIndex.Value;
                    string tabIndex = Request.Params["__EVENTARGUMENT"].ToString();
                    this.GetContextTabData(int.Parse(tabIndex));
                }
            }
        }
        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {

            //tabContainer.ActiveTabIndex = this.ActiveTabIndex;
            divError.Visible = isError;
            //buttonAddItem.Visible = (ActionType == "VIEW");
            //divData.Visible = (ActionType != "VIEW");
            string tabChangeScript = @"function ActiveTabChanged(sender, args) {
            var _idex = sender.get_activeTabIndex(); var _text = sender.get_activeTab().get_headerText(); var triggerControl = '" + this.ChangeTabs.UniqueID + @"';
            document.getElementById('" + this.HActiveTabIndex.ClientID + "').value = _idex;   __doPostBack(triggerControl, _idex); }";
            ScriptManager.RegisterClientScriptBlock(itemMasterTab, itemMasterTab.GetType(), "TABChange", tabChangeScript, true);


        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // gridConsumables.SelectedIndexChanged += new EventHandler(gridConsumables_SelectedIndexChanged);
            gridItemList.RowCommand += new GridViewCommandEventHandler(gridItemList_RowCommand);
            gridItemList.RowDataBound += new GridViewRowEventHandler(gridItemList_RowDataBound);
            gridItemList.RowCreated += new GridViewRowEventHandler(gridItemList_RowCreated);

            ddlMainItem.SelectedIndexChanged += new EventHandler(SelectedItemTypeChanged);

            btnGenerate.Click += new EventHandler(btnGenerate_Click);
            ChangeTabs.Click += new EventHandler(ChangeTabs_Click);
            buttonClose.Click += new EventHandler(CancelAddItem);
        }

        #endregion

        #region MainPage Tab Action
        /// <summary>
        /// Handles the Click event of the ChangeTabs control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>       
        protected void ChangeTabs_Click(object sender, EventArgs args)
        {
        }
        /// <summary>
        /// Handles the ValueChanged event of the HItemType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void HItemType_ValueChanged(object sender, EventArgs e)
        {
            this.PopulateItemSubTypes();
        }
        /// <summary>
        /// Handles the Click event of the btnGenerate control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ddlSubType.SelectedValue != "")
            {
                if (ddlSubType.SelectedValue == "All")
                { }
                else { }
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlMainItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void SelectedItemTypeChanged(object sender, EventArgs e)
        {
            if (ddlMainItem.SelectedValue != "")
            {
                this.ItemType = int.Parse(ddlMainItem.SelectedValue);

                this.PopulateItems();
                this.PopulateItemSubTypes(true);
                labelItemMainType.Text = ddlMainItem.SelectedItem.Text;
                labelItemTypeST.Text = ddlMainItem.SelectedItem.Text;

            }
        }
        /// <summary>
        /// Handles the Click event of the btn_close control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btn_close_Click(object sender, EventArgs e)
        {
            string url = "./frmAdmin_PMTCT_CustomItems.aspx";
            Response.Redirect(url, false);
        }
        /// <summary>
        /// Handles the ActiveTabChanged event of the itemMasterTab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void itemMasterTab_ActiveTabChanged(object sender, EventArgs e)
        {

            this.GetContextTabData(itemMasterTab.ActiveTabIndex);
        }
        /// <summary>
        /// Gets the context tab data.
        /// </summary>
        /// <param name="TabIndex">Index of the tab.</param>
        void GetContextTabData(int TabIndex) //This is Page 
        {
            if (TabIndex == 0)
            {
                this.PopulateMainItemType();
                if (ddlMainItem.Items.Count > 1)
                {
                    ListItem item = ddlMainItem.Items.FindByValue(this.ItemType.ToString());
                    if (item != null)
                    {
                        item.Selected = true;
                    }
                    else
                    {
                        ddlMainItem.SelectedIndex = 1;
                    }
                    SelectedItemTypeChanged(ddlMainItem, new EventArgs());
                }
                this.ActionType = "VIEW";

            }
            else if (TabIndex == 1)
            {
                this.PopulateItemSubTypes(true);
            }
            else if (TabIndex == 2)
            {
                this.PopulateMainItemType();
            }
            //return text;
        }
        #endregion
        /// <summary>
        /// Handles the RowCreated event of the gridItemList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void gridItemList_RowCreated(object sender, GridViewRowEventArgs e)
        {
           // gridItemList.Columns[4].Visible = this.ddlMainItem.SelectedItem.Text == "Billables";
        }
        /// <summary>
        /// Handles the RowDataBound event of the gridConsumables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        void gridItemList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(gridItemList, "RowClick$" + e.Row.RowIndex.ToString(), false));

            }
        }
        /// <summary>
        /// Handles the RowCommand event of the gridConsumables control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void gridItemList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {


                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gridRow = (gridItemList.Rows[index]);
                gridItemList.SelectedIndex = index;
                HItemID.Value = this.gridItemList.SelectedDataKey["item_pk"].ToString();

                if (e.CommandName == "RowClick")
                {
                    //  divData.Visible = true;
                    errorLabel.Text = "";
                    panelError.Visible = false;
                    this.ActionType = "EDIT";
                    DataTable dtSubTypes = this.GetSubTypesByType(this.ItemType);
                    DataTable dtItemSubTypes = this.itemManager.GetSubTypesForItem(int.Parse(HItemID.Value));
                    var queryTable =
                        from subType in dtSubTypes.AsEnumerable()
                        join itemSubType in dtItemSubTypes.AsEnumerable()
                        on subType["SubItemTypeID"].ToString() equals itemSubType["SubItemTypeID"].ToString()
                        into common
                        from matched in common.DefaultIfEmpty()
                        select new
                        {
                            SubTypeName = subType["SubTypeName"].ToString(),
                            SubTypeID = subType["SubItemTypeID"].ToString(),
                            Active = ((subType["DeleteFlag"].ToString() == "False") ? true : false),
                            ForThisItem = ((matched == null) ? false : true)
                        };
                    checkListSubTypes.Items.Clear();
                    foreach (var t in queryTable.Where(i => i.Active == true))
                    {

                        ListItem item = new ListItem(t.SubTypeName, t.SubTypeID);
                        item.Selected = t.ForThisItem;

                        checkListSubTypes.Items.Add(item);
                    }

                    textItemName.Text = this.gridItemList.SelectedDataKey["itemname"].ToString();
                    buttonSubmitItem.Text = "Update";

                    this.ActionType = "EDIT";
                    mpeItemPopup.Show();
                    return;
                }
                if (e.CommandName == "ManageDetails")
                {
                    Session["SearchContextKey"] = "";

                    labelSelectedBillable.Text = this.gridItemList.SelectedDataKey["itemname"].ToString();
                    this.PopulateBillableDetails(this.ItemID);
                    this.BindBillableItemsGrid();
                    buttonSaveBillable.Enabled = true;
                    //this.PopulateBillables();
                    this.PopulateBillingGroups();
                    this.ActionType = "DETAILS";
                    mpeBillableDetails.Show();
                    return;
                }

            }
            catch (Exception ex)
            {
                showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Saves the item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void SaveItem(object sender, EventArgs e)
        {
            string errorMessage = "";
            string actionType = ActionType.ToUpper().Trim();
            bool haserror = false;

            if (string.IsNullOrEmpty(textItemName.Text.Trim()))
            {
                haserror = true;
                errorMessage += "Missing Item Name";
            }
            if (this.HaveSubTypes && checkListSubTypes.SelectedIndex == -1)
            {
                haserror = true;
                errorMessage += "<br /> Missing Item Sub Type";
            }

            if (haserror)
            {
                // isError = true;
                errorLabel.Text = errorMessage;
                panelError.Visible = true;
                mpeItemPopup.Show();
                //parameterPopup.Show();
                return;
            }
            try
            {
                //   this.itemManager = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
                Dictionary<int, string> itemSubTypes = null;
                if (HaveSubTypes)
                {
                    itemSubTypes = new Dictionary<int, string>();
                    var selected = checkListSubTypes.Items.OfType<ListItem>().Where(item => item.Selected == true);
                    foreach (ListItem item in selected)
                    {
                        itemSubTypes.Add(int.Parse(item.Value), item.Text);
                    }
                }
                else
                { }
                bool itemIsActive = rblStatus.SelectedValue == "1";
                if (actionType == "NEW")
                {
                    this.itemManager.AddEditItem(textItemName.Text.Trim(), int.Parse(HItemType.Value), UserID, itemSubTypes, itemIsActive);

                }
                else if (actionType == "EDIT")
                {
                    this.itemManager.AddEditItem(textItemName.Text.Trim(), int.Parse(HItemType.Value), UserID, itemSubTypes, itemIsActive, int.Parse(HItemID.Value));
                    // this.NotifyAction("Item Updated Successfully ..", string.Format("{0} {1} ", actionType, textItemName.Text), false);
                }
                this.PopulateItems();
                this.PopulateItemSubTypes(true);
                this.NotifyAction(                    
                    string.Format("{0} {1} ", actionType, textItemName.Text), ((actionType == "NEW") ? "Item Added Successfully .." : "Item Updated Successfully .."),false);
                //parameterPopup.Hide();
                this.ActionType = "VIEW";
            }
            catch (Exception wz)
            {
                this.NotifyAction(wz.Message,"Error occurred ..", true);
                this.ActionType = "VIEW";
                // this.showErrorMessage(ref wz);
            }
        }
        /// <summary>
        /// Cancels the add item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void CancelAddItem(object sender, EventArgs e)
        {
            this.ActionType = "VIEW";
            this.mpeItemPopup.Hide();
            //   divData.Visible = false;
        }
        /// <summary>
        /// Adds the new item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void AddNewItem(object sender, EventArgs e)
        {
            errorLabel.Text = "";
            panelError.Visible = false;
            this.ActionType = "NEW";
          //  btnOkAction.CommandName = "ITEM";

            //this.ActiveTabIndex = tabContainer.ActiveTabIndex;
            buttonSubmitItem.Text = "Save";
            HItemID.Value = textItemName.Text = "";
            checkListSubTypes.SelectedIndex = -1;
            checkListSubTypes.Items.Clear();

            DataTable subTypes = this.GetSubTypesByType(this.ItemType);
            checkListSubTypes.DataTextField = "SubTypeName";
            checkListSubTypes.DataValueField = "SubItemTypeID";
            checkListSubTypes.DataSource = subTypes;
            checkListSubTypes.DataBind();
            mpeItemPopup.Show();
            ;
        }

        #region Get Populate Data
        /// <summary>
        /// Populates the type of the main item.
        /// </summary>
        private void PopulateMainItemType()
        {
            //this.itemManager = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
            DataTable dtItems = this.itemManager.GetItemTypes;
            if (itemMasterTab.ActiveTabIndex == 2)
            {
                dtItems.DefaultView.RowFilter = "";
                this.gridItemTypes.DataSource = dtItems;
                this.gridItemTypes.DataBind();
            }
            ddlMainItem.DataTextField = "ItemName";
            ddlMainItem.DataValueField = "ItemTypeID";

            dtItems.DefaultView.RowFilter = "DeleteFlag=0 And (ItemName= 'Billables') ";
            ddlMainItem.DataSource = dtItems.DefaultView;
            ddlMainItem.DataBind();
            ddlMainItem.Items.Insert(0, new ListItem("Select...", ""));
            ListItem item = ddlMainItem.Items.FindByValue(ItemType.ToString());
            if (item != null)
            {
                item.Selected = true;
            }
        }
        /// <summary>
        /// Gets the type of the sub types by.
        /// </summary>
        /// <param name="typeID">The type identifier.</param>
        /// <returns></returns>
        private DataTable GetSubTypesByType(int typeID)
        {
            DataTable dtSubTypes = itemManager.GetTypeSubType(ItemType);
            return dtSubTypes;
        }
        /// <summary>
        /// Populates the item sub types.
        /// </summary>
        private void PopulateItemSubTypes(bool parForce = false)
        {
            try
            {
                if (!HaveSubTypes)
                {
                    gridSubTypes.DataSource = "";
                    gridSubTypes.DataBind();
                    buttonAddSubType.Enabled = false;
                    return;
                }
                buttonAddSubType.Enabled = true;
                if (ddlSubType.Items.Count > 0 && parForce == false) return;
                // this.itemManager = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
                DataTable dtItems = this.GetSubTypesByType(ItemType);
                ddlSubType.DataTextField = "SubTypeName";
                ddlSubType.DataValueField = "SubItemTypeID";
                ddlSubType.DataSource = dtItems;
                ddlSubType.DataBind();
                ddlSubType.Items.Insert(0, new ListItem("Select...", ""));
                int lastIndex = ddlSubType.Items.Count;

                ddlSubType.Items.Insert(lastIndex, new ListItem("All Items", "All"));

                if (itemMasterTab.ActiveTabIndex == 1)
                {
                    gridSubTypes.DataSource = dtItems;
                    gridSubTypes.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Gets the item sub types.
        /// </summary>
        /// <param name="itemID">The item identifier.</param>
        /// <returns></returns>
        private DataTable GetItemSubTypes(int itemID)
        {
            return itemManager.GetSubTypesForItem(itemID);

        }
        /// <summary>
        /// Populates the data.
        /// </summary>
        private void PopulateItems()
        {
            try
            {
                this.dsData = new DataSet("ItemMaster");
                using (System.IO.TextReader txR = new System.IO.StringReader(this.ItemXSDSchema))
                {
                    this.dsData.ReadXmlSchema(txR);
                    txR.Close();
                }
                //  this.itemManager = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
                DataTable dtItems = itemManager.GetItemListByType(this.ItemType);
                this.gridItemList.DataSource = dtItems;
                this.gridItemList.DataBind();

            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Populates the billing groups.
        /// </summary>
        void PopulateBillingGroups()
        {
            try
            {
                // this.itemManager = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
                DataTable dtItems = itemManager.GetItemTypes;

                ddlBillingItemType.DataTextField = "ItemName";
                ddlBillingItemType.DataValueField = "ItemTypeID";

                dtItems.DefaultView.RowFilter = "DeleteFlag= 0 And (ItemName = 'Lab Tests' OR ItemName = 'Visit Type' OR ItemName = 'Pharmaceuticals' OR ItemName = 'Consumables') ";
                dtItems.DefaultView.Sort = "ItemName Asc";
                ddlBillingItemType.DataSource = dtItems.DefaultView;

                // ddlBillingItemType.DataSource = theDT;
                ddlBillingItemType.DataBind();
                ddlBillingItemType.Items.Insert(0, new ListItem("Select..", "0"));
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Gets or sets the billable items.
        /// </summary>
        /// <value>
        /// The billable items.
        /// </value>
        DataTable BillableItems
        {
            get
            {
                if (base.Session["BillableItems"] == null)
                    return new DataTable();
                else
                {
                    return (DataTable)base.Session["BillableItems"];
                }
            }
            set
            {
                base.Session["BillableItems"] = value;
            }
        }
        /// <summary>
        /// Binds the billable items grid.
        /// </summary>
        void BindBillableItemsGrid()
        {
            //frmBilling_ClientBill bill = new frmBilling_ClientBill();
            DataTable theMainDT = this.BillableItems;
            DataView dv = theMainDT.DefaultView;
            dv.RowFilter = "RowStatus <> 'Deleted'";
            DataTable theDT = dv.ToTable();

            this.gridSelectedItems.DataSource = theDT;
            this.gridSelectedItems.DataBind();
        }
        /// <summary>
        /// Populates the billable details.
        /// </summary>
        /// <param name="billableItemID">The billable item identifier.</param>
        void PopulateBillableDetails(int billableItemID)
        {
            try
            {
                if (billableItemID != -1)
                {
                    DataTable dtBillableDetails = this.itemManager.GetItemsForBillable(billableItemID);
                    DataColumn newCol = new DataColumn("RowStatus", typeof(string));
                    newCol.DefaultValue = "UnModified";
                    dtBillableDetails.Columns.Add(newCol);
                    DataColumn persistCol = new DataColumn("Persisted", typeof(bool));
                    persistCol.DefaultValue = false;
                    dtBillableDetails.Columns.Add(persistCol);
                    dtBillableDetails.AcceptChanges();
                    foreach (DataRow row in dtBillableDetails.Rows)
                    {
                        row["Persisted"] = true;
                    }
                    dtBillableDetails.AcceptChanges();
                    this.BillableItems = dtBillableDetails;
                }
                else
                {
                }

            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }

        #endregion

        #region Utility
        /// <summary>
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        private void NotifyAction(string strMessage, string strTitle, bool errorFlag)
        {
            IQCareMsgBox.NotifyAction(strMessage, strTitle,errorFlag, this,"");
            return;
        }
        /// <summary>
        /// Shows the hide sub types.
        /// </summary>
        /// <returns></returns>
        protected string ShowHideSubTypes()
        {
            return HaveSubTypes ? "" : "none";
        }
        /// <summary>
        /// Shows the hide detail column.
        /// </summary>
        /// <returns></returns>
        protected string ShowHideDetailColumn(object hasDetails)
        {
            bool has = hasDetails.ToString().ToLower().Trim() == "true";
            return has ? "" : "none";
        }
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
                lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team.  " ;
                this.isError = this.divError.Visible = true;
                Exception lastError = ex;
                lastError.Data.Add("Domain", "Item Master");
                SystemSetting.LogError(lastError);
                //try
                //{
                //    Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                //    logger.LogError(ex);
                //}
                //catch
                //{

                //}
            }
            mpeBillableDetails.Hide();
            mpeItemPopup.Hide();
            mpeSubTypePopup.Hide();
        }

        #endregion

        #region SubType
        /// <summary>
        /// Handles the Click event of the buttonSubmitST control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonSubmitST_Click(object sender, EventArgs e)
        {
            string errorMessage = "";
            string actionType = ActionType.ToUpper().Trim();
            bool haserror = false;

            if (string.IsNullOrEmpty(textSubTypeName.Text.Trim()))
            {
                haserror = true;
                errorMessage += "Missing Item Sub Type";
            }
            string subTypeName = textSubTypeName.Text.Trim();

            if (haserror)
            {
                // isError = true;
                labelError2.Text = errorMessage;
                panelError2.Visible = true;
                mpeSubTypePopup.Show();
                //parameterPopup.Show();
                return;
            }
            try
            {
                if (actionType == "NEW_ST")
                {
                    int subItemTypeID = itemManager.AddEditItemSubType(subTypeName, this.ItemType, this.UserID, -1, rblSubTypeStatus.SelectedValue == "1");
                    this.NotifyAction("Sub Type Added Successfully ..", string.Format("{0} {1} ", "Adding Sub Type", subTypeName), false);
                }
                else if (actionType == "EDIT_ST")
                {
                    int subItemTypeID = itemManager.AddEditItemSubType(subTypeName, this.ItemType, this.UserID, this.ItemSubType, rblSubTypeStatus.SelectedValue == "1");
                    this.NotifyAction("Sub Type Updated Successfully ..", string.Format("{0} {1} ", "Updating Sub Type", subTypeName), false);
                }

            }
            catch (Exception xs)
            {
                this.NotifyAction("Error occurred ..", string.Format("{0} {1} ", actionType, xs.Message), true);

                this.ActionType = "VIEW_ST";
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonAddSubType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonAddSubType_Click(object sender, EventArgs e)
        {
            if (!HaveSubTypes) return;
          //  btnOkAction.CommandName = "SUBTYPE";
            this.ActionType = "NEW_ST";
            labelError2.Text = errorLabel.Text = "";
            panelError2.Visible = panelError.Visible = false;
            //this.ActiveTabIndex = tabContainer.ActiveTabIndex;
            buttonSubmitST.Text = "Save";
            HSubItemID.Value = textSubTypeName.Text = "";
            mpeSubTypePopup.Show();

        }

        /// <summary>
        /// Handles the RowDataBound event of the gridSubTypes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridSubTypes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(gridSubTypes, "RowClick$" + e.Row.RowIndex.ToString(), false));

            }
        }

        /// <summary>
        /// Handles the RowCommand event of the gridSubTypes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridSubTypes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RowClick")
                {
                    //  divData.Visible = true;
                    errorLabel.Text = "";
                    panelError.Visible = false;
                    int index = Convert.ToInt32(e.CommandArgument);
                    this.ActionType = "EDIT_ST";

                    GridViewRow gridRow = (gridSubTypes.Rows[index]);
                    gridSubTypes.SelectedIndex = index;

                    HSubItemID.Value = this.gridSubTypes.SelectedDataKey["SubItemTypeID"].ToString();

                    string subItemName = gridRow.Cells[0].Text.Trim();
                    string stStatus = gridRow.Cells[2].Text.Trim().ToUpper();
                    textSubTypeName.Text = subItemName;
                    if (stStatus == "ACTIVE")
                        rblSubTypeStatus.SelectedIndex = 0;
                    else rblSubTypeStatus.SelectedIndex = 1;

                    buttonSubmitST.Text = "Update";
                    this.ActionType = "EDIT_ST";
                    mpeSubTypePopup.Show();
                }

            }
            catch (Exception ex)
            {
                showErrorMessage(ref ex);
            }
        }
        #endregion



        #region ItemTypes
        /// <summary>
        /// Handles the RowDataBound event of the gridIteTypes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridItemTypes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                // Retrieve the serviceStatus value for the current row.
                //if service/item has been given then it becomes uneditable
                String deleteFlag = rowView["DeleteFlag"].ToString();

                Label statusLabel = e.Row.FindControl("labelStatus") as Label;

                if (deleteFlag == "1")
                {
                    statusLabel.Text = "InActive";
                    e.Row.Attributes.Add("onclick", "javascript:return false");
                }
                else
                {
                    statusLabel.Text = "Active";
                    e.Row.Attributes.Add("onclick", "javascript:return false");
                    //e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(gridItemTypes, "RowClick$" + e.Row.RowIndex.ToString(), false));
                }

            }
        }

        /// <summary>
        /// Handles the RowCommand event of the gridIteTypes control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridItemTypes_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        #endregion

        #region ItemDetails

        /// <summary>
        /// Searches the name changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void SearchNameChanged(object sender, EventArgs e)
        {
            this.mpeBillableDetails.Show();
            if (ddlBillingItemType.SelectedIndex < 1) return;
            int ItemId, itemType;
            string ItemTypeName;

            if (hdCustID.Value != "")
            {
                try
                {
                    String[] itemCodes = hdCustID.Value.Split(';');
                    if (itemCodes.Length == 3)
                    {
                        ItemId = Convert.ToInt32(itemCodes[0]);
                        itemType = Convert.ToInt32(itemCodes[1]);
                        ItemTypeName = itemCodes[2];
                        hdItemID.Value = ItemId.ToString();
                        hdItemType.Value = itemType.ToString();

                        hdItemID.Value = ItemId.ToString();
                        hdItemType.Value = itemType.ToString();
                        DataTable theDT = this.BillableItems;
                        DataRow thisRow = null;
                        if (this.BillableItems.Rows.Count > 0)
                        {
                            thisRow =
                                theDT.AsEnumerable().DefaultIfEmpty(null)
                                .FirstOrDefault(gr => gr["ItemID"].ToString() == hdItemID.Value && gr["ItemTypeID"].ToString() == hdItemType.Value);
                        }
                        if (thisRow != null)
                        {
                            string rowStatus = thisRow["RowStatus"].ToString();
                            if (rowStatus == "Deleted")
                            {
                                thisRow["RowStatus"] = "Added";
                                theDT.AcceptChanges();
                            }
                        }
                        else
                        {

                            DataRow theDR = theDT.NewRow();
                            theDR.SetField("ItemName", textSearchName.Text);
                            theDR.SetField("ItemID", hdItemID.Value);
                            theDR.SetField("ItemTypeID", hdItemType.Value);
                            //use itemtype name from vw_billing_itemlist
                            theDR.SetField("ItemTypeName", ItemTypeName);
                            //use itemtype name from mst_billingType
                            //theDR.SetField("ItemTypeName", ddlBillingItemType.SelectedItem.Text);
                            theDR.SetField("DeleteFlag", 0);
                            theDR.SetField("RowStatus", "Added");
                            theDR.SetField("Persisted", false);
                            theDT.Rows.Add(theDR);
                        }
                        DataRow placeHolderRow = theDT.AsEnumerable().FirstOrDefault(row => row["ItemId"].ToString() == "");
                        if (placeHolderRow != null)
                            theDT.Rows.Remove(placeHolderRow);
                        theDT.AcceptChanges();
                        this.BillableItems = theDT;
                        this.BindBillableItemsGrid();

                    }
                }
                catch (Exception ex)
                {
                    NotifyAction(ex.Message, "Error Occurred..", true);
                    this.mpeBillableDetails.Show();
                    return;
                }
            }

            ((TextBox)sender).Text = "";
            hdCustID.Value = "";

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


            IItemMaster itemManager = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");

            //filling data from database
            int excludeItemID = -1; //exclude billables
            int.TryParse(System.Web.HttpContext.Current.Session["MainItemType"].ToString(), out excludeItemID);

            int? filterItemID = null;
            if (System.Web.HttpContext.Current.Session["SearchContextKey"].ToString() != "")
            {
                filterItemID = Convert.ToInt32(System.Web.HttpContext.Current.Session["SearchContextKey"].ToString());
            }
            else return ar;
            DataTable dataTable = itemManager.FindItems(prefixText, filterItemID, excludeItemID, null, false);

            string custItem = string.Empty;

            foreach (DataRow theRow in dataTable.Select("ItemTypeName <> 'Billables'")) // double check to remove billables
            {
                custItem = AutoCompleteExtender.CreateAutoCompleteItem(theRow[1].ToString(), 
                    String.Format("{0};{1};{2}", theRow["ItemID"], theRow["ItemTypeID"], theRow["ItemTypeName"]));
                //theRow[0], theRow[2], theRow[3]));

                ar.Add(custItem);
            }

            return ar;
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the rbItemType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void BillingGroupChanged(object sender, EventArgs e)
        {
            mpeBillableDetails.Show();
            if (ddlBillingItemType.SelectedIndex > 0)
            {
                int billingTypeID = Convert.ToInt32(ddlBillingItemType.SelectedValue);
                Session["SearchContextKey"] = ddlBillingItemType.SelectedValue;
            }
            else
            {
                ddlBillingItemType.ClearSelection();
                Session["SearchContextKey"] = "";
            }
        }
        /// <summary>
        /// Saves the billable details.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void SaveBillableDetails(object sender, EventArgs e)
        {
            //Save to database;
            try
            {
                DataTable theDT = this.BillableItems;
                DataView theDV = new DataView(theDT);
                theDV.RowFilter = "RowStatus ='Deleted' OR RowStatus = 'Added'";
                DataTable _tableToSave = theDV.ToTable();                
                this.itemManager.SaveBillablesItems(this.ItemID, this.UserID, _tableToSave);
                NotifyAction("Operation successful", "Success..", false);

            }
            catch (Exception ex)
            {
                NotifyAction(ex.Message, "Error Occurred..", true);
                this.mpeBillableDetails.Show();
                return;
            }


        }

        /// <summary>
        /// Handles the RowCommand event of the gridSelectedItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridSelectedItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            mpeBillableDetails.Show();
            if (e.CommandName == "RemoveItem")
            {
                DataTable theDT = this.BillableItems;
                int index = Int32.Parse(e.CommandArgument.ToString());
                DataRow rowDelete = theDT.Rows[index];

                if (Convert.ToBoolean(rowDelete["Persisted"]))
                {
                    rowDelete["RowStatus"] = "Deleted";
                    rowDelete.AcceptChanges();
                }
                else
                {
                    theDT.Rows.RemoveAt(index);
                }
                theDT.AcceptChanges();
                this.BillableItems = theDT;
                this.BindBillableItemsGrid();
            }
        }
        #endregion
    }
}