using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Interface.SCM;
using Application.Common;
using Application.Presentation;
using System.Xml;

namespace IQCare.SCM
{
    public partial class frmOpeningStock : Form
    {
        #region "Variables"
        Int32 theItemId = 0, BatchId = 0, StoreId = 0;
        Int32 theItemIdGbl = 0;
        string ExpiryDate;
        DataTable theDT = new DataTable();
        DataTable theDTBatch = new DataTable();
        DataSet dsOpenStock = new DataSet();
        DataTable theDTStock = new DataTable();
        #endregion

        public frmOpeningStock()
        {
            InitializeComponent();
        }

        private void frmOpeningStock_Load(object sender, EventArgs e)
        {
            try
            {
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
                Init_Form();
                SetRights();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        private void Init_Form()
        {
            //IMasterList objOpenStock = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            IPurchase objOpenStock = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
            dsOpenStock = objOpenStock.GetOpenStock();
            BindCombo();
            if (dsOpenStock.Tables[2].Rows.Count > 0)
            {
                DataView theDV = new DataView(dsOpenStock.Tables[2]);

                theDV.RowFilter = "StoreID='" + Convert.ToInt32(ddlStore.SelectedValue.ToString()) + "'";
                DataTable theDTExistStock = theDV.ToTable();
                ClearField();
                ShowGrid(theDTExistStock);
            }
        }
        public void SetRights()
        {
            //form level permission set
            if (GblIQCare.HasFunctionRight(ApplicationAccess.OpeningStock, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }
        }

        private void BindCombo()
        {
            try
            {
                DataSet XMLDS = new DataSet();
                XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
                BindFunctions theBindManager = new BindFunctions();

                DataView theDV = new DataView(XMLDS.Tables["Mst_Store"]);
                theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
                theDV.Sort = "Name ASC";
                DataTable theStoreDT = theDV.ToTable();
                theBindManager.Win_BindCombo(ddlStore, theStoreDT, "Name", "Id");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        private void ClearField()
        {
            lstSearch.ClearSelected();
            ddlStore.SelectedIndex = 0;

        }
        private void txtItemName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlStore.SelectedValue) > 0)
                {
                    if (txtItemName.Text != "" && txtItemName.Text.Length > 2)
                    {
                        lstSearch.Visible = true;
                        lstSearch.Width = txtItemName.Width;
                        lstSearch.Left = txtItemName.Left;
                        lstSearch.Top = txtItemName.Top + txtItemName.Height;
                        lstSearch.Height = 300;
                        lstSearch.BringToFront();

                        DataView theDV = new DataView(theDT);
                        theDV.RowFilter = "DrugName like '%" + txtItemName.Text + "*'";
                        if (theDV.Count > 0)
                        {
                            DataTable theDTdata = theDV.ToTable();
                            BindFunctions theBindManager = new BindFunctions();
                            theBindManager.Win_BindListBox(lstSearch, theDTdata, "DrugName", "Drug_pk");
                        }
                        else
                        {
                            lstSearch.DataSource = null;
                            lstSearch.Visible = false;
                        }
                    }
                    else
                    {
                        lstSearch.Visible = false;
                    }
                    if (e.KeyCode == Keys.Down)
                        lstSearch.Select();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void ddlStore_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataView theDV = new DataView(dsOpenStock.Tables[0]);
                theDV.RowFilter = "StoreID='" + Convert.ToInt32(ddlStore.SelectedValue.ToString()) + "'";
                theDT = theDV.ToTable();
                BindFunctions theBindManager = new BindFunctions();
                theBindManager.Win_BindListBox(lstSearch, theDT, "DrugName", "Drug_Pk");

                theDV = new DataView(dsOpenStock.Tables[2]);
                // theDV.RowFilter = "StoreID='" + Convert.ToInt32(ddlStore.SelectedValue.ToString()) + "' and TransacDate<='" + dtpTransdate.Text + "'";
                theDV.RowFilter = "StoreID='" + Convert.ToInt32(ddlStore.SelectedValue.ToString()) + "'";//+"' and TransacDate<='" + dtpTransdate.Text + "'";
                DataTable theDTExistStock = theDV.ToTable();
                ShowGrid(theDTExistStock);

                theDV = new DataView(dsOpenStock.Tables[1]);
                theDTBatch = theDV.ToTable();
                theBindManager.Win_BindListBox(lstSearchBatch, theDTBatch, "Name", "Id");
                Clear();
            }

            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void lstSearch_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                theItemId = Convert.ToInt32(lstSearch.SelectedValue);
                txtItemName.Text = lstSearch.Text;
                DataView theDV = new DataView(theDT);
                theDV.RowFilter = "Drug_Pk=" + theItemId.ToString();
                if (theDV.ToTable().Rows.Count > 0)
                {
                    txtDispensingUnit.Text = theDV[0]["DispensingUnit"].ToString();
                    lstSearch.Visible = false;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void lstSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstSearch_DoubleClick(sender, e);
            }
        }

        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwOpeningStock.Columns.Clear();
                dgwOpeningStock.DataSource = null;

                DataGridViewTextBoxColumn ItemId = new DataGridViewTextBoxColumn();
                ItemId.HeaderText = "Item Id";
                ItemId.Name = ItemId.DataPropertyName = "ItemId";
                ItemId.Width = 10;
                ItemId.ReadOnly = true;
                ItemId.Visible = false;

                DataGridViewTextBoxColumn ItemName = new DataGridViewTextBoxColumn();
                ItemName.HeaderText = "Item Name";
                ItemName.Name = ItemName.DataPropertyName = "ItemName";
                ItemName.Width = 420;
                ItemName.ReadOnly = true;
                ItemName.Visible = true;

                DataGridViewTextBoxColumn StoreId = new DataGridViewTextBoxColumn();
                StoreId.HeaderText = "Store";
                StoreId.Name = StoreId.DataPropertyName = "StoreId";
                StoreId.Width = 15;
                StoreId.ReadOnly = true;
                StoreId.Visible = false;

                DataGridViewTextBoxColumn StoreName = new DataGridViewTextBoxColumn();
                StoreName.HeaderText = "Store";
                StoreName.Name = StoreName.DataPropertyName = "StoreName";
                StoreName.Width = 130;
                StoreName.ReadOnly = true;
                StoreName.Visible = true;

                DataGridViewTextBoxColumn DispensingUnit = new DataGridViewTextBoxColumn();
                DispensingUnit.HeaderText = "Dispensing Unit";
                DispensingUnit.Name = DispensingUnit.DataPropertyName = "DispensingUnit";
                DispensingUnit.Width = 50;
                DispensingUnit.ReadOnly = true;
                DispensingUnit.Visible = false;

                DataGridViewTextBoxColumn BatchId = new DataGridViewTextBoxColumn();
                BatchId.HeaderText = "BatchNo";
                BatchId.Name = BatchId.DataPropertyName = "BatchId";
                BatchId.Width = 15;
                BatchId.ReadOnly = true;
                BatchId.Visible = false;


                DataGridViewTextBoxColumn BatchName = new DataGridViewTextBoxColumn();
                BatchName.HeaderText = "Batch No.";
                BatchName.Name = BatchName.DataPropertyName = "BatchNo";
                BatchName.Width = 135;
                BatchName.ReadOnly = true;
                BatchName.Visible = true;


                DataGridViewTextBoxColumn ExpiryDate = new DataGridViewTextBoxColumn();
                ExpiryDate.HeaderText = "Expiry Date";
                ExpiryDate.Name = ExpiryDate.DataPropertyName = "ExpiryDate";
                ExpiryDate.Width = 120;
                ExpiryDate.ReadOnly = true;
                ExpiryDate.Visible = true;

                DataGridViewTextBoxColumn Quantity = new DataGridViewTextBoxColumn();
                Quantity.HeaderText = "Quantity";
                Quantity.Name = Quantity.DataPropertyName = "Quantity";
                Quantity.Width = 120;
                Quantity.ReadOnly = true;
                Quantity.Visible = true;

                DataGridViewTextBoxColumn OpeningStock = new DataGridViewTextBoxColumn();
                OpeningStock.HeaderText = "OpeningStock";
                OpeningStock.Name = OpeningStock.DataPropertyName = "OpeningStock";
                OpeningStock.Width = 10;
                OpeningStock.ReadOnly = true;
                OpeningStock.Visible = false;

                //DataGridViewTextBoxColumn TransDate = new DataGridViewTextBoxColumn();
                //TransDate.HeaderText = "TransDate";
                //TransDate.DataPropertyName = "TransDate";
                //TransDate.Width = 10;
                //TransDate.ReadOnly = true;
                //TransDate.Visible = false;

                dgwOpeningStock.Columns.Add(ItemId);
                dgwOpeningStock.Columns.Add(ItemName);
                dgwOpeningStock.Columns.Add(StoreId);
                dgwOpeningStock.Columns.Add(StoreName);
                dgwOpeningStock.Columns.Add(DispensingUnit);
                dgwOpeningStock.Columns.Add(BatchId);
                dgwOpeningStock.Columns.Add(BatchName);
                dgwOpeningStock.Columns.Add(ExpiryDate);
                dgwOpeningStock.Columns.Add(Quantity);
                dgwOpeningStock.Columns.Add(OpeningStock);
                //dgwOpeningStock.Columns.Add(TransDate);
                dgwOpeningStock.AutoGenerateColumns = false;
                dgwOpeningStock.DataSource = theDT;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }
        private Boolean Validation_Form()
        {
            try
            {
                if (Convert.ToInt32(ddlStore.SelectedValue) == 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Store";
                    IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                    return false;
                }
                else if (txtBatchName.Text == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Batch No";
                    IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                    return false;
                }
                else if (txtItemName.Text == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Item Name";
                    IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                    return false;
                }
                else if (Convert.ToDateTime(dtpExpiryDate.Text) <= Convert.ToDateTime(GblIQCare.CurrentDate))
                {
                    IQCareWindowMsgBox.ShowWindow("ExpiryDate", this);
                    return false;

                }
                else if (Convert.ToDateTime(dtpTransdate.Text) > Convert.ToDateTime(GblIQCare.CurrentDate))
                {
                    IQCareWindowMsgBox.ShowWindow("AsofDate", this);
                    return false;

                }
                else if (txtOpeningQty.Text == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Opening Quantity";
                    IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                    return false;

                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
            return true;
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (Validation_Form() == true)
            {
                try
                {
                    theDTStock = (DataTable)dgwOpeningStock.DataSource;

                    DataTable dtb = theDTStock.Copy();

                    DataTable dtCheck = (DataTable)dgwOpeningStock.DataSource;
                    DataView dt11 = dtCheck.DefaultView;

                    theDTStock.PrimaryKey = new DataColumn[] { theDTStock.Columns["ItemId"], theDTStock.Columns["BatchId"], theDTStock.Columns["StoreId"], theDTStock.Columns["ExpiryDate"] };
                    DataView theDV = new DataView(theDTStock);
                    DataTable dtNew = null;
                    theDV.RowFilter = "ItemId=" + theItemIdGbl + " and BatchId=" + BatchId + " and StoreId=" + StoreId + " and ExpiryDate='" + ExpiryDate + "'";
                    if (theDV.Count > 0)
                    {
                        theDV.Delete(0);

                        dtNew = dt11.ToTable();

                        if (dtNew.Rows.Count > 0)
                        {
                            for (int batchVal = 0; batchVal < dtNew.Rows.Count; batchVal++)
                            {
                                if (dtNew.Rows[batchVal]["BatchNo"].ToString() == txtBatchName.Text.ToString())
                                {
                                    ShowGrid(dtb);
                                    IQCareWindowMsgBox.ShowWindow("DuplicateBatchName", this);
                                    return;
                                }
                            }
                        }
                        DataRow theDRow = theDTStock.NewRow();
                        if (lstSearchBatch.SelectedValue == null)
                        {
                            BindFunctions theBindManager = new BindFunctions();
                            IMasterList objItemlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                            DataSet theDSBatchName = objItemlist.SaveBatchName(txtBatchName.Text, GblIQCare.AppUserId, lstSearch.SelectedValue.ToString(), dtpExpiryDate.Text);
                            theDRow["BatchId"] = Convert.ToString(theDSBatchName.Tables[0].Rows[0]["BatchId"]);
                            theDTBatch.Clear();
                            theDTBatch = theDSBatchName.Tables[1];
                            theBindManager.Win_BindListBox(lstSearchBatch, theDTBatch, "Name", "Id");
                        }
                        else
                        {
                            theDRow["BatchId"] = lstSearchBatch.SelectedValue;
                        }
                        theDRow["ItemId"] = lstSearch.SelectedValue;
                        theDRow["ItemName"] = txtItemName.Text;
                        theDRow["StoreId"] = ddlStore.SelectedValue;
                        theDRow["StoreName"] = ddlStore.Text;
                        theDRow["DispensingUnit"] = txtDispensingUnit.Text;
                        theDRow["BatchNo"] = txtBatchName.Text;
                        theDRow["ExpiryDate"] = string.Format("{0:dd-MMM-yyyy}", dtpExpiryDate.Text);
                        theDRow["Quantity"] = txtOpeningQty.Text;
                        theDRow["OpeningStock"] = 0;
                        //theDRow["TransDate"] = string.Format("{0:dd-MMM-yyyy}", dtpExpiryDate.Text);
                        theDTStock.Rows.Add(theDRow);
                    }
                    else
                    {
                        dtNew = dt11.ToTable();
                        if (dtNew.Rows.Count > 0)
                        {
                            for (int batchVal = 0; batchVal < dtNew.Rows.Count; batchVal++)
                            {
                                if (dtNew.Rows[batchVal]["BatchNo"].ToString() == txtBatchName.Text.ToString())
                                {
                                    ShowGrid(theDTStock);
                                    IQCareWindowMsgBox.ShowWindow("DuplicateBatchName", this);
                                    return;
                                }
                            }
                        }

                        DataRow theDRow = theDTStock.NewRow();
                        if (lstSearchBatch.SelectedValue == null)
                        {
                            BindFunctions theBindManager = new BindFunctions();
                            IMasterList objItemlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                            //DataSet theDSBatchName = objItemlist.SaveBatchName(txtBatchName.Text, GblIQCare.AppUserId);

                            DataSet theDSBatchName = objItemlist.SaveBatchName(txtBatchName.Text, GblIQCare.AppUserId, lstSearch.SelectedValue.ToString(), dtpExpiryDate.Text);


                            theDRow["BatchId"] = Convert.ToString(theDSBatchName.Tables[0].Rows[0]["BatchId"]);
                            theDTBatch.Clear();
                            theDTBatch = theDSBatchName.Tables[1];
                            theBindManager.Win_BindListBox(lstSearchBatch, theDTBatch, "Name", "Id");
                        }
                        else
                        {
                            theDRow["BatchId"] = lstSearchBatch.SelectedValue;
                        }

                        theDRow["ItemId"] = lstSearch.SelectedValue;
                        theDRow["ItemName"] = txtItemName.Text;
                        theDRow["StoreId"] = ddlStore.SelectedValue;
                        theDRow["StoreName"] = ddlStore.Text;
                        theDRow["DispensingUnit"] = txtDispensingUnit.Text;
                        theDRow["BatchNo"] = txtBatchName.Text;
                        theDRow["ExpiryDate"] = string.Format("{0:dd-MMM-yyyy}", dtpExpiryDate.Text);
                        theDRow["Quantity"] = txtOpeningQty.Text;
                        theDRow["OpeningStock"] = 0;
                        //theDRow["TransacDate"] = string.Format("{0:dd-MMM-yyyy}", dtpTransdate.Text);
                        theDTStock.Rows.Add(theDRow);
                    }
                    theDTStock.AcceptChanges();
                    ShowGrid(theDTStock);
                    Clear();
                }
                catch (Exception err)
                {
                    if (err.GetType().FullName == "System.Data.ConstraintException")
                    {
                        IQCareWindowMsgBox.ShowWindow("DuplicateItemName", this);
                    }

                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable theDT = new DataTable();
                //IMasterList objItemOpeningStock = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                IPurchase objItemOpeningStock = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
                if (theDTStock.Rows.Count > 0)
                {
                    DateTime TransDate = Convert.ToDateTime(String.Format("{0:dd-MMM-yyyy}", dtpTransdate.Text));
                    DataRow[] theDRStock = theDTStock.Select("OpeningStock = 0");
                    theDT = theDRStock.CopyToDataTable();
                    int retrows = objItemOpeningStock.SaveUpdateOpeningStock(theDT, GblIQCare.AppUserId, TransDate);
                    IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
                    Init_Form();
                    theDTStock.Clear();
                    Clear();
                }
                else
                {
                    IQCareWindowMsgBox.ShowWindow("NoDatatoSave", this);
                    //Init_Form();
                    Clear();
                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        private void dgwOpeningStock_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex > -1)
                {

                    if (dgwOpeningStock.Rows[e.RowIndex].Cells[9].Value.ToString() == "1")
                    {
                        IQCareWindowMsgBox.ShowWindow("RowUpdate", this);
                        return;
                    }
                    ddlStore.SelectedItem = dgwOpeningStock.Rows[e.RowIndex].Cells[3].Value.ToString();
                    txtDispensingUnit.Text = dgwOpeningStock.Rows[e.RowIndex].Cells[4].Value.ToString();
                    txtItemName.Text = dgwOpeningStock.Rows[e.RowIndex].Cells[1].Value.ToString();
                    dtpExpiryDate.Text = dgwOpeningStock.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtBatchName.Text = dgwOpeningStock.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtOpeningQty.Text = dgwOpeningStock.Rows[e.RowIndex].Cells[8].Value.ToString();
                    //Variable Set for Validation
                    StoreId = Convert.ToInt32(dgwOpeningStock.Rows[e.RowIndex].Cells[2].Value);
                    BatchId = Convert.ToInt32(dgwOpeningStock.Rows[e.RowIndex].Cells[5].Value);
                    theItemIdGbl = Convert.ToInt32(dgwOpeningStock.Rows[e.RowIndex].Cells[0].Value);
                    ExpiryDate = dgwOpeningStock.Rows[e.RowIndex].Cells[7].Value.ToString();

                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void txtOpeningQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBind = new BindFunctions();
            theBind.Win_Numeric(e);
        }
        private void Clear()
        {
            //ddlStore.SelectedIndex = 0;
            txtDispensingUnit.Text = "";
            txtItemName.Text = "";
            txtOpeningQty.Text = "";
            txtBatchName.Text = "";
            this.dtpExpiryDate.Text = GblIQCare.CurrentDate;
            this.dtpTransdate.Text = GblIQCare.CurrentDate;
        }

        private void txtBatchName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlStore.SelectedValue) > 0)
                {
                    if (txtBatchName.Text != "")
                    {
                        lstSearchBatch.Visible = true;
                        lstSearchBatch.Width = txtBatchName.Width;
                        lstSearchBatch.Left = txtBatchName.Left;
                        lstSearchBatch.Top = txtBatchName.Top + txtBatchName.Height;
                        lstSearchBatch.Height = 300;
                        lstSearchBatch.BringToFront();
                        DataView theDV = new DataView(theDTBatch);
                        theDV.RowFilter = "Name like '" + txtBatchName.Text + "*'";
                        if (theDV.Count > 0)
                        {
                            DataTable theDTdata = theDV.ToTable();
                            BindFunctions theBindManager = new BindFunctions();
                            theBindManager.Win_BindListBox(lstSearchBatch, theDTdata, "Name", "Id");
                        }
                        else
                        {
                            lstSearchBatch.DataSource = null;
                            lstSearchBatch.Visible = false;
                        }

                    }

                    else
                    {
                        lstSearchBatch.Visible = false;
                    }
                    if (e.KeyCode == Keys.Down)
                        lstSearchBatch.Select();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void lstSearchBatch_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                txtBatchName.Text = lstSearchBatch.Text;
                lstSearchBatch.Visible = false;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        private void lstSearchBatch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lstSearchBatch_DoubleClick(sender, e);
            }
        }


    }
}
