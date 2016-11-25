using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.SCM;

namespace IQCare.SCM
{
    public partial class frmBillingDetails : Form
    {
        int BillingTypeId;
        IItemMaster itemManager = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
        public frmBillingDetails()
        {
            InitializeComponent();
        }

        private void txtdespensingMargin_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_decimalNagetive(e);
        }

        private void frmBillingDetails_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Init_Form();
            SetRights();
        }
        public void SetRights()
        {
            //form level permission set
            /* if (GblIQCare.HasFunctionRight(ApplicationAccess.ItemTypeSubTypeLinking, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
               {
                   btnSave.Enabled = false;
               }*/

        }
        private void Init_Form()
        {
            BindBillableDropdown();
            BindItemTypeList();
            //  BindDrugTypeList();

        }

        /// <summary>
        /// Binds the billable dropdown.
        /// </summary>
        private void BindBillableDropdown()
        {
            IQCareUtils theUtils = new IQCareUtils();
            ddlBillable.Items.Clear();
            //  IMasterList objProgramlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            // DataTable theDT = objProgramlist.GetBillables();

            // DataView theDV = new DataView(theDT);
            //  theDV.RowFilter = "DeleteFlag = 0";
            //BindFunctions theBind = new BindFunctions();
            //theBind.Win_BindCombo(ddlBillable, theDV.ToTable(), "ItemType", "ID");


            int billableItemTypeID ;
            billableItemTypeID= this.itemManager.GetItemTypeIDByName("Billables");
            if (billableItemTypeID == -1) return;
            this.BillingTypeId = billableItemTypeID;

            DataTable dtItems = itemManager.GetItemListByType(billableItemTypeID);

            DataView theDV = new DataView(dtItems);
               theDV.RowFilter = "Status='Active'";
            theDV.Sort="ItemName Asc";

            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(ddlBillable, theDV.ToTable(), "ItemName", "Item_PK");
        }
        /// <summary>
        /// Binds the item type list.
        /// </summary>
        private void BindItemTypeList()
        {
           // IQCareUtils theUtils = new IQCareUtils();
           /* lstItemType.Items.Clear();
            IMasterList objProgramlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataTable theDT = objProgramlist.GetBillingGroups();
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "DeleteFlag = 0 and BillingTypeID<>0";
            
            BindFunctions theBind = new BindFunctions();
           theBind.Win_BindListBox(lstItemType, theDV.ToTable(), "Name", "BillingTypeID");
           
            lstItemType.ClearSelected();*/
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlBillable control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ddlBillable_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstSelectedItems.DataSource = null;

            if (ddlBillable.SelectedIndex != 0)
            {
                grbDetails.Enabled = true;
                grbItemType.Enabled = true;
                BindSelectedItemsList(Convert.ToInt32(ddlBillable.SelectedValue.ToString()));
                //   BindDrugTypeListByItemID(Convert.ToInt32(ddlBillable.SelectedValue.ToString()));
            }
            else
            {
                grbDetails.Enabled = false;
                grbItemType.Enabled = false;
            }
            lstItemType.ClearSelected();
        }
        /// <summary>
        /// Binds the available items list.
        /// </summary>
        /// <param name="billableID">The billable identifier.</param>
        private void BindAvailableItemsList(int billableID)
        {
            //IQCareUtils theUtils = new IQCareUtils();
            //lstSelectedItems.Items.Clear();
        /*    IMasterList objProgramlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");                   
                DataTable theDT = objProgramlist.GetBillingGroupItems(billableID);
           // DataTable dtItems = itemManager.GetItemListByType(billableID);
           DataView theDV = new DataView(theDT);
           // DataView theDV = new DataView(dtItems);
           if (theDT.Columns.Count > 0)
            {
                theDV.RowFilter = "DeleteFlag = 0";
                BindFunctions theBind = new BindFunctions();
                theBind.Win_BindListBox(lstAvailableItems, theDV.ToTable(), "Name", "ID");
            }*/
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the lstItemType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lstItemType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //String strItem;
            lstAvailableItems.DataSource = null;
            BillingTypeId = 0;
            foreach (DataRowView listItem in lstItemType.SelectedItems)
            {
                BillingTypeId = (int)listItem["BillingTypeId"];
                BindAvailableItemsList(BillingTypeId);
                //lstAvailableItems.Items.Add( listItem["BillingTypeId"].ToString());
            }

        }
        private void BindSelectedItemsList(int DecodeID)
        {
            //IQCareUtils theUtils = new IQCareUtils();
            //lstSelectedItems.Items.Clear();
            /*IMasterList objProgramlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataTable theDT = objProgramlist.GetBillablesItems(DecodeID);
            DataView theDV = new DataView(theDT);
            theDV.RowFilter = "DeleteFlag = 0";
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindListBox(lstSelectedItems, theDV.ToTable(), "Name", "ID");*/
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if (lstAvailableItems.SelectedItems.Count > 0)
            {
                List<DataRow> var = new List<DataRow>();


                for (int i = 0; i < lstAvailableItems.SelectedIndices.Count; i++)
                {
                    DataRow item = ((DataRowView)lstAvailableItems.SelectedItems[i]).Row;
                    

                    var.Add(item);
                  //DataRow row=  ((DataTable)lstSelectedItems.DataSource).NewRow();
                  //row["ID"] = item["Item_PK"];
                  //row["Name"] = item["ItemName"];
                  //row["BillTypeID"] = item["ItemTypeID"];
                  //((DataTable)lstSelectedItems.DataSource).Rows.Add(row);
                    ((DataTable)lstSelectedItems.DataSource).ImportRow(item);
                }

                //lstAvailableItems.SelectedIndex = -1;
                foreach (DataRow inum in var)
                {
                    ((DataTable)lstAvailableItems.DataSource).Rows.Remove(inum);


                }
            }

        }




        private void btnUnselectAll_Click(object sender, EventArgs e)
        {

            if (lstSelectedItems.SelectedItems.Count > 0)
            {
                List<DataRow> var = new List<DataRow>();


                for (int i = 0; i < lstSelectedItems.SelectedIndices.Count; i++)
                {
                    DataRow item = ((DataRowView)lstSelectedItems.SelectedItems[i]).Row;

                    var.Add(item);

                    if ((int)item["BillTypeId"] == BillingTypeId)
                        ((DataTable)lstAvailableItems.DataSource).ImportRow(item);
                }

                //lstSelectedItems.SelectedIndex = -1;
                foreach (DataRow row in var)
                {
                    ((DataTable)lstSelectedItems.DataSource).Rows.Remove(row);


                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable theBillableItems = (DataTable)lstSelectedItems.DataSource;
            if (theBillableItems != null)
            {

              //  IBilling objProgramlist = (IBilling)ObjectFactory.CreateInstance("BusinessProcess.SCM.BBilling, BusinessProcess.SCM");
                IItemMaster objProgramlist = (IItemMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BItemMaster, BusinessProcess.Administration");
                objProgramlist.SaveBillablesItems((int)ddlBillable.SelectedValue, GblIQCare.AppUserId, theBillableItems);
                MessageBox.Show("Saved Successfully.", "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            closeform();

        }

        private void closeform()
        {
            /*  Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList,IQCare.SCM"));
              theForm.MdiParent = this.MdiParent;
              theForm.Left = 0;
              theForm.Top = 2;
              theForm.Show();*/

            this.Close();

        }




    }
}
