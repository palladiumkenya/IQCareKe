using System;
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
    public partial class frmDisposeItemDrugs : Form
    {
        public frmDisposeItemDrugs()
        {
            InitializeComponent();
        }

        private void FrmDisposeItemDrugs_Load(object sender, EventArgs e)
        {
            dtpAsofDate.CustomFormat= "dd-MMM-yyyy";
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            BindCombo();
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwDisposeItem.Columns.Clear();
                dgwDisposeItem.DataSource = null;

                DataGridViewTextBoxColumn ItemCode = new DataGridViewTextBoxColumn();
                ItemCode.HeaderText = "Item Code";
                ItemCode.DataPropertyName = "ItemId";
                ItemCode.Width = 50;
                ItemCode.ReadOnly = true;
                ItemCode.Visible = false;


                DataGridViewTextBoxColumn ItemName = new DataGridViewTextBoxColumn();
                ItemName.HeaderText = "Item Name";
                ItemName.DataPropertyName = "ItemName";
                ItemName.Width = 270;
                ItemName.ReadOnly = true;

                DataGridViewTextBoxColumn BatchId = new DataGridViewTextBoxColumn();
                BatchId.HeaderText = "Batch No";
                BatchId.DataPropertyName = "BatchId";
                BatchId.Width = 50;
                BatchId.ReadOnly = true;
                BatchId.Visible = false;


                DataGridViewTextBoxColumn BatchName = new DataGridViewTextBoxColumn();
                BatchName.HeaderText = "Batch No.";
                BatchName.DataPropertyName = "BatchName";
                BatchName.Width = 100;
                BatchName.ReadOnly = true;

                DataGridViewTextBoxColumn Quantity = new DataGridViewTextBoxColumn();
                Quantity.HeaderText = "Quantity";
                Quantity.DataPropertyName = "Quantity";
                Quantity.Width = 70;
                Quantity.ReadOnly = true;

                DataGridViewTextBoxColumn DispensingUnit = new DataGridViewTextBoxColumn();
                DispensingUnit.HeaderText = "Dispensing Unit";
                DispensingUnit.DataPropertyName = "DispensingUnit";
                DispensingUnit.Width = 120;
                DispensingUnit.ReadOnly = true;

                DataGridViewTextBoxColumn ExpiryDate = new DataGridViewTextBoxColumn();
                ExpiryDate.HeaderText = "Expiry Date";
                ExpiryDate.DataPropertyName = "ExpiryDate";
                ExpiryDate.Width = 100;
                ExpiryDate.ReadOnly = true;

                DataGridViewTextBoxColumn TotalPurchasePrice = new DataGridViewTextBoxColumn();
                TotalPurchasePrice.HeaderText = "Total Purchase Price";
                TotalPurchasePrice.DataPropertyName = "TotPurcPrice";
                TotalPurchasePrice.Width = 130;
                TotalPurchasePrice.ReadOnly = true;

                DataGridViewTextBoxColumn UnitPrice = new DataGridViewTextBoxColumn();
                UnitPrice.HeaderText = "Unit Price";
                UnitPrice.DataPropertyName = "DispensingUnitPrice";
                UnitPrice.Width = 80;
                UnitPrice.ReadOnly = true;

                DataGridViewCheckBoxColumn chkDispose = new DataGridViewCheckBoxColumn();
                chkDispose.HeaderText = "Dispose";
                chkDispose.DataPropertyName = "Dispose";
                chkDispose.Width = 80;

                dgwDisposeItem.Columns.Add(ItemCode);
                dgwDisposeItem.Columns.Add(ItemName);
                dgwDisposeItem.Columns.Add(BatchId);
                dgwDisposeItem.Columns.Add(BatchName);
                dgwDisposeItem.Columns.Add(Quantity);
                dgwDisposeItem.Columns.Add(DispensingUnit);
                dgwDisposeItem.Columns.Add(ExpiryDate);
                dgwDisposeItem.Columns.Add(TotalPurchasePrice);
                dgwDisposeItem.Columns.Add(UnitPrice);
                dgwDisposeItem.Columns.Add(chkDispose);
                dgwDisposeItem.AutoGenerateColumns = false;
                dgwDisposeItem.DataSource = theDT;
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

          
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable theDT = new DataTable();
            theDT = (DataTable)dgwDisposeItem.DataSource;
            //IMasterList objOpenStock = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            IPurchase objOpenStock = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
            int SaveRecord = objOpenStock.SaveDisposeItems(Convert.ToInt32(ddlStore.SelectedValue), GblIQCare.AppLocationId, Convert.ToDateTime(dtpAsofDate.Text), GblIQCare.AppUserId, theDT);
            if (SaveRecord == 1)
            {
                IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
            }
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            //IMasterList objOpenStock = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            IPurchase objOpenStock = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
            DataSet theDSDisposeStock = objOpenStock.GetDisposeStock(Convert.ToInt32(ddlStore.SelectedValue), Convert.ToDateTime(dtpAsofDate.Text));
            ShowGrid(theDSDisposeStock.Tables[0]);

        }

    }
}
