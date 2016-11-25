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
using System.Collections;


namespace IQCare.SCM
{
    public partial class frmAdjustStockLevel : Form
    {
        public frmAdjustStockLevel()
        {
            InitializeComponent();
        }
        private void frmAdjustStockLevel_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            SetRights();
            BindCombo();
            DataTable theDT = new DataTable();
            ShowGrid(theDT);

        }
        private void BindCombo()
        {
            DataSet XMLDS = new DataSet();
            XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
            BindFunctions theBindManager = new BindFunctions();

            DataView theDV = new DataView(XMLDS.Tables["Mst_Store"]);
            theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
            theDV.Sort = "Name ASC";
            DataTable theStoreDT = theDV.ToTable();
            theBindManager.Win_BindCombo(ddlStoreName, theStoreDT, "Name", "Id");

            theDV = new DataView(XMLDS.Tables["Mst_Employee"]);
            theDV.RowFilter = "(DeleteFlag =0 or DeleteFlag is null)";
            DataTable theEMPPrepDT = theDV.ToTable();
            DataTable theEMPAuthDT = theDV.ToTable();
            theBindManager.Win_BindCombo(ddlPreparedBy, theEMPPrepDT, "Name", "Id");
            theBindManager.Win_BindCombo(ddlAuthoriseBy, theEMPAuthDT, "Name", "Id");
            theBindManager.Win_BindCombo(ddlAuthoriseBy, theEMPAuthDT, "Name", "Id");

            theDV = new DataView(XMLDS.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=205 and (DeleteFlag =0 or DeleteFlag is null)";
            DataTable theAdjustmentReason = theDV.ToTable();
            theBindManager.Win_BindCombo(ddlAdjustmentReason, theAdjustmentReason, "Name", "Id");
        }
        private Boolean Validation_Form()
        {
            try
            {
                String AdjustmentDate = string.Format("{0:dd-MMM-yyyy}", dtpEffectiveDate.Text);
                if (Convert.ToInt32(ddlStoreName.SelectedValue) == 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Store Name";
                    IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                    return false;
                }
               
                else if (Convert.ToInt32(ddlPreparedBy.SelectedValue) == 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Prepared By";
                    IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                    return false;
                }
                else if (Convert.ToInt32(ddlAuthoriseBy.SelectedValue) == 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Authorised By";
                    IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                    return false;
                }
                else if (Convert.ToDateTime(AdjustmentDate) > Convert.ToDateTime(GblIQCare.CurrentDate))
                {
                    IQCareWindowMsgBox.ShowWindow("AdjustmentStockDate", this);
                    return false;
                }
                DataTable thestockLevel = (DataTable)dgwStockLevelDetails.DataSource;
                bool IsAdjustreasonset = false;
                int AdjQtyCount = 0;

                for (int i = 0; i < thestockLevel.Rows.Count; i++)
                {
                    if (!String.IsNullOrEmpty(Convert.ToString(thestockLevel.Rows[i]["AdjQty"])))
                    {
                        if (Convert.ToInt32(thestockLevel.Rows[i]["AdjQty"]) == 0)
                        {

                            AdjQtyCount++;
                        }
                    }
                }

                if (thestockLevel.Rows.Count > 0)
                {
                    if (thestockLevel.Rows.Count == AdjQtyCount)
                    {
                        IQCareWindowMsgBox.ShowWindow("AdjustStockLevel", this);
                        return false;
                    }
                }

                for (int i = 0; i < thestockLevel.Rows.Count; i++)
                {
                    if(thestockLevel.Rows[i]["AdjQty"].ToString()=="")
                    {
                        IQCareWindowMsgBox.ShowWindow("AdjustStockLevel", this);
                        return false;
                    }
                }

                //AdjustReason

                for (int i = 0; i < thestockLevel.Rows.Count; i++)
                {

                    if (!String.IsNullOrEmpty(Convert.ToString(thestockLevel.Rows[i]["AdjustReasonId"])))
                    {
                        if (Convert.ToInt32(thestockLevel.Rows[i]["AdjustReasonId"]) > 0)
                        {

                            IsAdjustreasonset = true;
                            break;
                        }
                    }
                }
                if (thestockLevel.Rows.Count > 0)
                {
                    if (!IsAdjustreasonset)
                    {
                        IQCareWindowMsgBox.ShowWindow("AdjustmentReason", this);
                        return false;
                    }
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
        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwStockLevelDetails.Columns.Clear();
                dgwStockLevelDetails.AutoGenerateColumns = false;
                dgwStockLevelDetails.DataSource = null;

                DataGridViewTextBoxColumn Itemcode = new DataGridViewTextBoxColumn();
                Itemcode.HeaderText = "ItemId";
                Itemcode.DataPropertyName = "ItemId";
                Itemcode.Width = 30;
                Itemcode.ReadOnly = true;
                Itemcode.Visible = false;

                DataGridViewTextBoxColumn ItemName = new DataGridViewTextBoxColumn();
                ItemName.HeaderText = "Item Name";
                ItemName.DataPropertyName = "DrugName";
                ItemName.Width = 305;
                ItemName.ReadOnly = true;

                DataGridViewTextBoxColumn DispensingUnit = new DataGridViewTextBoxColumn();
                DispensingUnit.HeaderText = "Dispensing Unit";
                DispensingUnit.DataPropertyName = "DispensingUnit";
                DispensingUnit.Width = 80;
                DispensingUnit.ReadOnly = true;

                DataGridViewTextBoxColumn BatchId = new DataGridViewTextBoxColumn();
                BatchId.HeaderText = "Batch Id";
                BatchId.DataPropertyName = "BatchId";
                BatchId.Width = 100;
                BatchId.ReadOnly = false;
                BatchId.Visible = false;

                DataGridViewTextBoxColumn BatchNo = new DataGridViewTextBoxColumn();
                BatchNo.HeaderText = "Batch No.";
                BatchNo.DataPropertyName = "Batch";
                BatchNo.Width = 100;
                BatchNo.ReadOnly = false;

                DataGridViewTextBoxColumn ExpiryDate = new DataGridViewTextBoxColumn();
                ExpiryDate.HeaderText = "Expiry Date";
                ExpiryDate.DataPropertyName = "ExpiryDate";
                ExpiryDate.Width = 100;
                ExpiryDate.ReadOnly = true;
                ExpiryDate.Visible = true;


                DataGridViewTextBoxColumn CurrentQuantity = new DataGridViewTextBoxColumn();
                CurrentQuantity.HeaderText = "Current Quantity";
                CurrentQuantity.DataPropertyName = "AvailableQTY";
                CurrentQuantity.Width = 80;
                CurrentQuantity.ReadOnly = true;

                DataGridViewTextBoxColumn AdjustedQuantity = new DataGridViewTextBoxColumn();
                AdjustedQuantity.HeaderText = "Adjusted Quantity";
                AdjustedQuantity.DataPropertyName = "AdjQty";
                AdjustedQuantity.Width = 80;
                AdjustedQuantity.ReadOnly = false;

                DataGridViewComboBoxColumn AdjustedReason = new DataGridViewComboBoxColumn();
                AdjustedReason.HeaderText = "Adjusted Reason";
                AdjustedReason.DataPropertyName = "AdjustReasonId";
                AdjustedReason.Width = 200;
                DataSet XMLDS = new DataSet();
                XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
                DataRow theDRReasoon = XMLDS.Tables["Mst_Decode"].NewRow();
                theDRReasoon["Id"] = 0;
                theDRReasoon["Name"] = "Select";
                theDRReasoon["CodeId"] = 205;
                theDRReasoon["DeleteFlag"] = 0;
                XMLDS.Tables["Mst_Decode"].Rows.Add(theDRReasoon);
                DataView theDV = new DataView(XMLDS.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=205 and (DeleteFlag =0 or DeleteFlag is null)";
                theDV.Sort = "Id ASC";
                //AdjustedReason
                AdjustedReason.DataSource = theDV.ToTable();
                AdjustedReason.DisplayMember = "Name";
                AdjustedReason.ValueMember = "Id";
                AdjustedReason.ReadOnly = false;

                dgwStockLevelDetails.Columns.Add(Itemcode);
                dgwStockLevelDetails.Columns.Add(ItemName);
                dgwStockLevelDetails.Columns.Add(DispensingUnit);
                dgwStockLevelDetails.Columns.Add(BatchNo);
                dgwStockLevelDetails.Columns.Add(BatchId);
                dgwStockLevelDetails.Columns.Add(ExpiryDate);
                dgwStockLevelDetails.Columns.Add(CurrentQuantity);
                dgwStockLevelDetails.Columns.Add(AdjustedQuantity);
                dgwStockLevelDetails.Columns.Add(AdjustedReason);
                dgwStockLevelDetails.DataSource = theDT;

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        public void SetRights()
        {
            //form level permission set
            if (GblIQCare.HasFunctionRight(ApplicationAccess.AdjustStocklevel, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ddlStoreName_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //String AdjustmentDate  = String.Format("{0:dd-MMM-yyyy}", dtpEffectiveDate.Text);
            //IMasterList objStock = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            //DataSet dsStockLevel = objStock.GetStockforAdjustment(Convert.ToInt32(ddlStoreName.SelectedValue), AdjustmentDate);
            //ShowGrid(dsStockLevel.Tables[0]);
            //if (dsStockLevel.Tables[1].Rows.Count > 0)
            //{
            //    ddlPreparedBy.SelectedValue = dsStockLevel.Tables[1].Rows[0][4].ToString();
            //    ddlAuthoriseBy.SelectedValue = dsStockLevel.Tables[1].Rows[0][5].ToString();
            //    if (Convert.ToInt32(dsStockLevel.Tables[1].Rows[0][6]) == 1)
            //    {
            //        chkUpdateStockFlag.Checked = true;
            //    }
            //}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Validation_Form() == true)
            {
                String AdjustmentDate = string.Format("{0:dd-MMM-yyyy}", dtpEffectiveDate.Text);
                int updateStock = 1;
                DataTable theDT = (DataTable)dgwStockLevelDetails.DataSource;
                DataView dv = theDT.DefaultView;
                dv.RowFilter = "AdjQty > 0 OR AdjQty < 0";
                DataTable dt = dv.ToTable();

                //IMasterList objStock = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                IPurchase objStock = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
                int ret = objStock.SaveUpdateStockAdjustment(
                    dt, 
                    GblIQCare.AppLocationId, 
                    Convert.ToInt32(ddlStoreName.SelectedValue), 
                    AdjustmentDate, 
                    Convert.ToInt32(ddlPreparedBy.SelectedValue), 
                    Convert.ToInt32(ddlAuthoriseBy.SelectedValue), 
                    updateStock, 
                    GblIQCare.AppUserId);
                if (ret > 0)
                {
                    IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
                    ResetField();
                    return;
                }
            }
        }
        private void ddlAdjustmentReason_SelectedValueChanged(object sender, EventArgs e)
        {
            //foreach (DataGridViewRow theDR in dgwStockLevelDetails.Rows)
            //{
            //    theDR.Cells[8].Value = ddlAdjustmentReason.SelectedValue;
            //}
        }
    
        private void dgwStockLevelDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(dgwStockLevelDetails.Rows[e.RowIndex].Cells[7].Value == DBNull.Value)
            {
                IQCareWindowMsgBox.ShowWindow("Adjusted Quantity Should Not be a NULL Value", "!", "", this);
                //MessageBox.Show("Adjusted Quantity Should Not be a NULL Value");
                //dgwStockLevelDetails.Rows[e.RowIndex].Cells[7].Value = 0;
                return;
            }
            int AvailQty = Convert.ToInt32(dgwStockLevelDetails.Rows[e.RowIndex].Cells[6].Value);
            int AdjustQty = Convert.ToInt32(dgwStockLevelDetails.Rows[e.RowIndex].Cells[7].Value == DBNull.Value ? 0 : dgwStockLevelDetails.Rows[e.RowIndex].Cells[7].Value);
            int BalQty = AvailQty + AdjustQty;
            if (BalQty < 0)
            {
                IQCareWindowMsgBox.ShowWindow("Adjusted Quantity Should be less than Available Quantity", "!", "", this);
                dgwStockLevelDetails.Rows[e.RowIndex].Cells[7].Value = 0;
                return;
            }
        }

        private void ResetField()
        {
                dtpEffectiveDate.Text = GblIQCare.CurrentDate;
                //IMasterList objStock = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                IPurchase objStock = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
                DataSet dsStockLevel = objStock.GetStockforAdjustment(0, GblIQCare.CurrentDate);
                ShowGrid(dsStockLevel.Tables[0]);
                ddlPreparedBy.SelectedValue = 0;
                ddlAuthoriseBy.SelectedValue = 0;
                chkUpdateStockFlag.Checked = false;
                ddlAdjustmentReason.SelectedValue = 0;
                ddlStoreName.SelectedValue = 0;
        }

        private void dgwStockLevelDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgwDataGridForEvent = sender as DataGridView;
            if (dgwDataGridForEvent.CurrentCell.ColumnIndex == 7)
            {
                TextBox txtAdjustedQuantity = e.Control as TextBox;
                if (txtAdjustedQuantity != null)
                {
                    txtAdjustedQuantity.KeyPress += new KeyPressEventHandler(txtAdjustedQuantity_KeyPress);
                    //IsHandleAdded = true;
                }
            }

        }

        void txtAdjustedQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBind = new BindFunctions();
            theBind.Win_Integer(e);
        }

        private void chkAdjReason_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdjReason.Checked == true)
            {
                foreach (DataGridViewRow theDR in dgwStockLevelDetails.Rows)
                {
                    theDR.Cells[8].Value = ddlAdjustmentReason.SelectedValue;
                }

            }
            else
            {
                foreach (DataGridViewRow theDR in dgwStockLevelDetails.Rows)
                {
                    theDR.Cells[8].Value = 0;
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                String AdjustmentDate = String.Format("{0:dd-MMM-yyyy}", dtpEffectiveDate.Text);
                //IMasterList objStock = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                IPurchase objStock = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
                DataSet dsStockLevel = objStock.GetStockforAdjustment(Convert.ToInt32(ddlStoreName.SelectedValue), AdjustmentDate);
                ShowGrid(dsStockLevel.Tables[0]);
                if (dsStockLevel.Tables[1].Rows.Count > 0)
                {
                    ddlPreparedBy.SelectedValue = dsStockLevel.Tables[1].Rows[0][4].ToString();
                    ddlAuthoriseBy.SelectedValue = dsStockLevel.Tables[1].Rows[0][5].ToString();
                    if (Convert.ToInt32(dsStockLevel.Tables[1].Rows[0][6]) == 1)
                    {
                        chkUpdateStockFlag.Checked = true;
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

      
      
      }
}
