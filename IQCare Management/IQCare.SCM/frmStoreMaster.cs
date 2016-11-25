using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.SCM;


namespace IQCare.SCM
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmStoreMaster : Form
    {
        /// <summary>
        /// The dt itemlist
        /// </summary>
        DataTable DTItemlist = new DataTable();
        /// <summary>
        /// The p identifier
        /// </summary>
        int PId;
        int MaxPId;
        /// <summary>
        /// The store identifier
        /// </summary>
        string StoreId, StoreName;
        /// <summary>
        /// Initializes a new instance of the <see cref="frmStoreMaster"/> class.
        /// </summary>
        public frmStoreMaster()
        {
            InitializeComponent();
            txtStoreID.Select();
        }

        /// <summary>
        /// Binds the dropdown.
        /// </summary>
        private void BindDropdown()
        {
            ddlStatus.Items.Clear();
            ddlStatus.Items.Add("Active");
            ddlStatus.Items.Add("InActive");
            ddlStatus.SelectedIndex = 0;

            ddlStoreCategory.SelectedIndex = 0;
            //ddlPurchasingStore.Items.Clear();
            //ddlPurchasingStore.Items.Add("No");
            //ddlPurchasingStore.Items.Add("Yes");
          //  ddlPurchasingStore.SelectedIndex = 0;
          //  cmbDispensing.SelectedIndex = 0;

        }

        /// <summary>
        /// Validation_s the form.
        /// </summary>
        /// <returns></returns>
        private Boolean Validation_Form()
        {
            try
            {
                if (txtStoreID.Text == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Store ID";
                    IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                    txtStoreID.Focus();
                    return false;
                }

                 if (txtStoreName.Text == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Store Name";
                    IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                    txtStoreID.Focus();
                    return false;
                }
                 DataTable theDT = (DataTable)dgwStoreName.DataSource;
                 DataView theDV = new DataView(theDT);
                 theDV.RowFilter = "Srno='" + PId + "'";
                 if (theDV.Count == 0)
                 {
                     foreach (DataRow theDR in theDT.Rows)
                     {
                         if (txtStoreID.Text.ToUpper() == Convert.ToString(theDR["StoreId"]).ToUpper())
                         {
                             IQCareWindowMsgBox.ShowWindow("StoreIdduplicate", this);
                             txtStoreID.Focus();
                             return false;
                         }

                         else if (txtStoreName.Text.ToUpper() == Convert.ToString(theDR["Name"]).ToUpper())
                         {
                             IQCareWindowMsgBox.ShowWindow("StoreNameduplicate", this);
                             txtStoreName.Focus();
                             return false;
                         }
                     }
                 }
                 if (ddlStoreCategory.SelectedItem.ToString() == "")
                 {
                     MsgBuilder theBuilder = new MsgBuilder();
                     theBuilder.DataElements["MessageText"] = "Specify the store category";
                     IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                     return false;
                 }
                 //if ((ddlPurchasingStore.SelectedIndex != 0) && (cmbDispensing.SelectedIndex != 0))
                 //{
                     //if ((ddlPurchasingStore.SelectedItem =="Yes") && (cmbDispensing.SelectedItem=="Yes"))
                     //{
                     //    IQCareWindowMsgBox.ShowWindow("PurchasingDispensingSameStore", this);
                     //    return false;
                     //}
                 //}

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
            return true;
        }


        /// <summary>
        /// Sets the rights.
        /// </summary>
        public void SetRights()
        {
            //Form level authentication
            if (GblIQCare.HasFunctionRight(ApplicationAccess.Store, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }
        }

        /// <summary>
        /// Clear_s the form.
        /// </summary>
        private void Clear_Form()
        {
            txtStoreID.Text = "";
            txtStoreName.Text = "";
            ddlStoreCategory.SelectedIndex = 0;
            //ddlPurchasingStore.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
           // cmbDispensing.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            chkDelete.Checked = false;
            txtStoreID.Focus();
        }

        /// <summary>
        /// Init_s the form.
        /// </summary>
        private void Init_Form()
        {
            IMasterList objItemCommonlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            String tableName = "Mst_" + GblIQCare.ItemTableName;
            DTItemlist = objItemCommonlist.GetCommonItemList(GblIQCare.ItemCategoryId, tableName,GblIQCare.AppLocationId);
            if (DTItemlist.Rows.Count > 0)
            {
                if (tableName != "Mst_Drugtype")
                {
                    MaxPId = Convert.ToInt32(DTItemlist.Select("SRNo=MAX(SRNo)")[0].ItemArray[3]);
                }
            }
            ShowGrid(DTItemlist);
            Clear_Form();
        }

        /// <summary>
        /// Shows the grid.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwStoreName.Columns.Clear();
                dgwStoreName.DataSource = null;
              ID.Name=  ID.DataPropertyName = "Id";
              ID.Width = 10;
              SRNo.Name=  SRNo.DataPropertyName = "SRNo";
              SRNo.Width = 0;
              ColStoreID.Name=  ColStoreID.DataPropertyName = "StoreId";
              ColStoreName.Name=  ColStoreName.DataPropertyName = "Name";
               // ColPurchasingStore.DataPropertyName = "CentralStore";
               // ColDispensingStore.DataPropertyName = "DispensingStore";
             ColStatus.Name=   ColStatus.DataPropertyName = "Status";
             StoreCategory.Name=   StoreCategory.DataPropertyName = "StoreCategory";
             StoreCategory.HeaderText = "Category";
             DataGridViewTextBoxColumn colDelete = new DataGridViewTextBoxColumn();
             colDelete.Name = colDelete.DataPropertyName = "DeleteFlag";
             colDelete.HeaderText = "Deleted";
                ID.Visible = false;
                SRNo.Visible = false;//deepika
                dgwStoreName.Columns.Add(ID);
                dgwStoreName.Columns.Add(ColStoreID);
                dgwStoreName.Columns.Add(ColStoreName);
                dgwStoreName.Columns.Add(StoreCategory);

                //dgwStoreName.Columns.Add(ColPurchasingStore);
                //dgwStoreName.Columns.Add(ColDispensingStore);
                dgwStoreName.Columns.Add(ColStatus);
                dgwStoreName.Columns.Add(SRNo);
                dgwStoreName.Columns.Add(colDelete);

                dgwStoreName.AutoGenerateColumns = false;
                dgwStoreName.DataSource = theDT;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Form theForm = new Form();
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList, IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Load event of the frmStoreMaster control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmStoreMaster_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            BindDropdown();
            Init_Form();
            SetRights();
           
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Validation_Form() != false)
            {
                DTItemlist = (DataTable)dgwStoreName.DataSource;
                //DataView theDV = new DataView(DTItemlist);
                //theDV.RowFilter = "SRNo='" + PId + "'";
                DataView theDV = new DataView(DTItemlist);
                theDV.RowFilter = "Srno='" + PId + "'";
                DataTable TmpDT = (DataTable)dgwStoreName.DataSource;
                DataView TmpDV = new DataView(TmpDT);
                theDV.RowFilter = "Srno='" + PId + "'";

                //if (theDV.Count > 0)
                //{
                   
                //        theDV[0]["StoreId"] = txtStoreID.Text;
                //        theDV[0]["Name"] = txtStoreName.Text;
                //        theDV[0]["CentralStore"] = ddlPurchasingStore.SelectedItem;
                //        theDV[0]["DispensingStore"] = cmbDispensing.SelectedItem;
                //        theDV[0]["Status"] = ddlStatus.SelectedItem;
                   
                //}
                if (theDV.Count > 0)
                {
                    if (StoreId != txtStoreID.Text)
                    {
                        if (StoreName != txtStoreName.Text)
                        {
                            TmpDV.RowFilter = "Name='" + txtStoreName.Text + "'";
                            if (TmpDV.Count > 0)
                            {
                                IQCareWindowMsgBox.ShowWindow("StoreNameduplicate", this);
                                txtStoreID.Focus();
                                return;
                            }
                        }

                        TmpDV.RowFilter = "StoreId='" + txtStoreID.Text + "'";
                        if (TmpDV.Count > 0)
                        {

                            IQCareWindowMsgBox.ShowWindow("StoreIdduplicate", this);
                            txtStoreID.Focus();
                            return;
                        }
                        theDV[0]["StoreId"] = txtStoreID.Text;
                        theDV[0]["Name"] = txtStoreName.Text;
                        theDV[0]["CentralStore"] = ddlPurchasingStore.SelectedItem;
                        theDV[0]["DispensingStore"] = cmbDispensing.SelectedItem;
                        theDV[0]["Status"] = ddlStatus.SelectedItem;
                        theDV[0]["DeleteFlag"] = chkDelete.Checked;

                    }
                    else
                    {
                        if (StoreName != txtStoreName.Text)
                        {
                            TmpDV.RowFilter = "Name='" + txtStoreName.Text + "'";
                            if (TmpDV.Count > 0)
                            {
                                IQCareWindowMsgBox.ShowWindow("StoreNameduplicate", this);
                                txtStoreID.Focus();
                                return;
                            }
                        }
                        theDV[0]["StoreId"] = txtStoreID.Text;
                        theDV[0]["Name"] = txtStoreName.Text;
                        theDV[0]["StoreCategory"] = ddlStoreCategory.SelectedItem;
                        theDV[0]["CentralStore"] = (ddlStoreCategory.SelectedItem.ToString() == "Purchasing") ? "Yes" : "No"; // ddlPurchasingStore.SelectedItem;
                        theDV[0]["DispensingStore"] = (ddlStoreCategory.SelectedItem.ToString() == "Dispensing") ? "Yes" : "No"; // cmbDispensing.SelectedItem;
                        theDV[0]["Status"] = ddlStatus.SelectedItem;
                        theDV[0]["DeleteFlag"] = chkDelete.Checked;
                    }

                }
                else
                {
                   
                        MaxPId = MaxPId + 1;
                        DataRow theDRow = DTItemlist.NewRow();
                        theDRow["StoreId"] = txtStoreID.Text;
                        theDRow["Name"] = txtStoreName.Text;
                        theDRow["StoreCategory"] = ddlStoreCategory.SelectedItem;
                        theDRow["CentralStore"] = (ddlStoreCategory.SelectedItem.ToString() == "Purchasing") ? "Yes" : "No"; // ddlPurchasingStore.SelectedItem;
                        theDRow["DispensingStore"] = (ddlStoreCategory.SelectedItem.ToString() == "Dispensing") ? "Yes" : "No"; // cmbDispensing.SelectedItem;
                        theDRow["Status"] = ddlStatus.SelectedItem;
                        theDRow["DeleteFlag"] = chkDelete.Checked;
                        theDRow["SRNo"] = MaxPId;
                        DTItemlist.Rows.Add(theDRow);
                   
                }
                DTItemlist.AcceptChanges();
                ShowGrid(DTItemlist);
                Clear_Form();
                PId = 0;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                String tableName = "Mst_" + GblIQCare.ItemTableName;
                IMasterList objItemlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                int retrows = objItemlist.SaveUpdateStore(DTItemlist,   GblIQCare.AppUserId, GblIQCare.AppLocationId);
                IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
                Init_Form();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the CellClick event of the dgwStoreName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgwStoreName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    if (dgwStoreName.Rows[e.RowIndex].Cells["SRNo"].Value.ToString() != "")
                    {
                        PId = Convert.ToInt32(dgwStoreName.Rows[e.RowIndex].Cells["SRNo"].Value.ToString());
                    }
                    txtStoreID.Text = dgwStoreName.Rows[e.RowIndex].Cells["StoreId"].Value.ToString();
                    StoreId = txtStoreID.Text;
                    txtStoreName.Text = dgwStoreName.Rows[e.RowIndex].Cells["Name"].Value.ToString();
                    StoreName = txtStoreName.Text;
                    ddlStoreCategory.SelectedItem = dgwStoreName.Rows[e.RowIndex].Cells["StoreCategory"].Value.ToString();

                   // ddlPurchasingStore.SelectedItem = dgwStoreName.Rows[e.RowIndex].Cells[3].Value.ToString();
                    //cmbDispensing.SelectedItem = dgwStoreName.Rows[e.RowIndex].Cells[4].Value.ToString();
                    ddlStatus.SelectedItem = dgwStoreName.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                    chkDelete.Checked = dgwStoreName.Rows[e.RowIndex].Cells["DeleteFlag"].Value.ToString() == "1";
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Selected = true;
                }
                catch (Exception err)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                    IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                }
            }


        }

        /// <summary>
        /// Handles the Click event of the btnUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Index >= 1)
                {
                    DTItemlist = (DataTable)dgwStoreName.DataSource;
                    string Id = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[0].Value.ToString();
                    string StoreId = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[1].Value.ToString();
                    string StrName = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[2].Value.ToString();
                    string StrCentralStore = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[3].Value.ToString();
                    string StrDispensingStore = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[4].Value.ToString();
                    string StrStatus = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[5].Value.ToString();
                  
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[0].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[0].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[1].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[1].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[2].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[2].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[3].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[3].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[4].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[4].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex - 1].Cells[5].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[5].Value;

                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[0].Value = Id;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[1].Value = StoreId;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[2].Value = StrName;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[3].Value = StrCentralStore;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[4].Value = StrDispensingStore;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[5].Value = StrStatus;
                    this.dgwStoreName.CurrentCell = this.dgwStoreName[2, dgwStoreName.CurrentCell.RowIndex - 1];
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Selected = true;
                    DTItemlist = (DataTable)dgwStoreName.DataSource;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Index < dgwStoreName.Rows.Count - 1)
                {
                    DTItemlist = (DataTable)dgwStoreName.DataSource;
                    string Id = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[0].Value.ToString();
                    string StoreId = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[1].Value.ToString();
                    string StrName = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[2].Value.ToString();
                    string StrCentralStore = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[3].Value.ToString();
                    string StrStatus = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[4].Value.ToString();

                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[0].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[0].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[1].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[1].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[2].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[2].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[3].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[3].Value;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex + 1].Cells[4].Value = dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[4].Value;

                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[0].Value = Id;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[1].Value = StoreId;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[2].Value = StrName;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[3].Value = StrCentralStore;
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Cells[4].Value = StrStatus;
                    this.dgwStoreName.CurrentCell = this.dgwStoreName[2, dgwStoreName.CurrentCell.RowIndex + 1];
                    dgwStoreName.Rows[dgwStoreName.CurrentCell.RowIndex].Selected = true;
                    DTItemlist = (DataTable)dgwStoreName.DataSource;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        /// <summary>
        /// Checks the validation.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        /// <returns></returns>
        public bool checkValidation(DataTable theDT)
        {
            bool ret = true;
            for (int i = 0; i < theDT.Rows.Count; i++)
            {
                if (Convert.ToString(theDT.Rows[i]["StoreID"]).ToLower() == Convert.ToString(txtStoreID.Text).ToLower())
                {
                    MsgBuilder theBuilder = new MsgBuilder(); 
                    theBuilder.DataElements["Control"] = Convert.ToString(txtStoreID.Text);
                    IQCareWindowMsgBox.ShowWindowConfirm("DuplicateStoreID", theBuilder, this);
                    ret = false;
                    return ret;
                }
                if (Convert.ToString(theDT.Rows[i]["Name"]).ToLower() == Convert.ToString(txtStoreName.Text).ToLower())
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = Convert.ToString(txtStoreName.Text);
                    IQCareWindowMsgBox.ShowWindowConfirm("DuplicateStoreName", theBuilder, this);
                    ret = false;
                    return ret;
                }
            }
            return ret;
        }

      

        private void dgwStoreName_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgwStoreName.Columns[e.ColumnIndex].Name == "DeleteFlag")
            {
                if (e.Value is int)
                {
                    
                    e.Value = (((int)e.Value)==1) ? "Yes" : "No";
                    e.FormattingApplied = true;
                }
            }
        }
    }
}
