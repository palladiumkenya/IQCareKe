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
using System.Configuration;

namespace IQCare.SCM
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmBatchSummaryByStore : Form
    {
        /// <summary>
        /// The item identifier
        /// </summary>
        Int32 theItemId = 0;
        /// <summary>
        /// The drug listing
        /// </summary>
        DataTable theDrugListing = new DataTable();
        /// <summary>
        /// Initializes a new instance of the <see cref="frmBatchSummaryByStore"/> class.
        /// </summary>
        public frmBatchSummaryByStore()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frmBatchSummaryByStore control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmBatchSummaryByStore_Load(object sender, EventArgs e)
        {
            try
            {
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
                BindCombo();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        /// <summary>
        /// Binds the combo.
        /// </summary>
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
        /// <summary>
        /// Shows the grid.
        /// </summary>
        /// <param name="theDT">The dt.</param>
        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwBatchSummary.Columns.Clear();
                dgwBatchSummary.DataSource = null;

                DataGridViewTextBoxColumn ItemCode = new DataGridViewTextBoxColumn();
                ItemCode.HeaderText = "Item Code";
                ItemCode.DataPropertyName = "ItemId";
                ItemCode.Width = 100;
                ItemCode.ReadOnly = true;
                ItemCode.Visible = false;

                DataGridViewTextBoxColumn ItemName = new DataGridViewTextBoxColumn();
                ItemName.HeaderText = "Item Description";
                ItemName.DataPropertyName = "Item Description";
                ItemName.Width = 250;
                ItemName.ReadOnly = true;

                DataGridViewTextBoxColumn Unit = new DataGridViewTextBoxColumn();
                Unit.HeaderText = "Unit";
                Unit.DataPropertyName = "Unit";
                Unit.Width = 75;
                Unit.ReadOnly = true;

                DataGridViewTextBoxColumn ExpiryDate = new DataGridViewTextBoxColumn();
                ExpiryDate.HeaderText = "Expiry Date";
                ExpiryDate.DataPropertyName = "Expiry Date";
                ExpiryDate.Width = 90;
                ExpiryDate.ReadOnly = true;

                DataGridViewTextBoxColumn Batch = new DataGridViewTextBoxColumn();
                Batch.HeaderText = "Batch No";
                Batch.DataPropertyName = "BatchId";
                Batch.Width = 60;
                Batch.ReadOnly = true;
                Batch.Visible = false;

                DataGridViewTextBoxColumn BatchName = new DataGridViewTextBoxColumn();
                BatchName.HeaderText = "Batch No.";
                BatchName.DataPropertyName = "Batch Name";
                BatchName.Width = 105;
                BatchName.ReadOnly = true;

                DataGridViewTextBoxColumn OpeningQty = new DataGridViewTextBoxColumn();
                OpeningQty.HeaderText = "Opening Stock";
                OpeningQty.DataPropertyName = "Opening Stock";
                OpeningQty.Width = 105;
                OpeningQty.ReadOnly = true;

                DataGridViewTextBoxColumn QtyRecieved = new DataGridViewTextBoxColumn();
                QtyRecieved.HeaderText = "Recieved Quantity";
                QtyRecieved.DataPropertyName = "Recieved Quantity";
                QtyRecieved.Width = 107;
                QtyRecieved.ReadOnly = true;

                DataGridViewTextBoxColumn QtyDispensed = new DataGridViewTextBoxColumn();
                QtyDispensed.HeaderText = "Dispensed Quantity";
                QtyDispensed.DataPropertyName = "Dispensed Quantity";
                QtyDispensed.Width = 107;
                QtyDispensed.ReadOnly = true;

                DataGridViewTextBoxColumn ClosingQty = new DataGridViewTextBoxColumn();
                ClosingQty.HeaderText = "Closing Quantity";
                ClosingQty.DataPropertyName = "Closing Quantity";
                ClosingQty.Width = 107;
                ClosingQty.ReadOnly = true;
                dgwBatchSummary.Columns.Add(ItemCode);
                dgwBatchSummary.Columns.Add(ItemName);
                dgwBatchSummary.Columns.Add(Unit);
                dgwBatchSummary.Columns.Add(ExpiryDate);
                dgwBatchSummary.Columns.Add(Batch);
                dgwBatchSummary.Columns.Add(BatchName);
                dgwBatchSummary.Columns.Add(OpeningQty);
                dgwBatchSummary.Columns.Add(QtyRecieved);
                dgwBatchSummary.Columns.Add(QtyDispensed);
                dgwBatchSummary.Columns.Add(ClosingQty);
                dgwBatchSummary.AutoGenerateColumns = false;
                dgwBatchSummary.DataSource = theDT;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }
        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="StoreId">The store identifier.</param>
        /// <param name="ItemsId">The items identifier.</param>
        /// <param name="FromDate">From date.</param>
        /// <param name="ToDate">To date.</param>
        /// <returns></returns>
        private DataSet GetItems(int StoreId, int ItemsId, DateTime FromDate, DateTime ToDate)
        {
            BindFunctions theBindManager = new BindFunctions();
            //IMasterList objOpenStock = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            ISCMReport objOpenStock = (ISCMReport)ObjectFactory.CreateInstance("BusinessProcess.SCM.BSCMReport,BusinessProcess.SCM");
            return objOpenStock.GetBatchSummary(StoreId, ItemsId, FromDate, ToDate);
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the ddlStore control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ddlStore_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlStore.SelectedValue) != 0)
                {
                    BindFunctions theBindManager = new BindFunctions();
                    DataSet theDS = GetItems(Convert.ToInt32(ddlStore.SelectedValue), 0, Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text));
                    theDrugListing = theDS.Tables[0];
                }
                else
                {
                    txtItemName.Text = "";
                    dgwBatchSummary.Columns.Clear();
                    dgwBatchSummary.DataSource = null;
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
        /// Handles the SelectionChangeCommitted event of the ddlItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ddlItems_SelectionChangeCommitted(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ddlStore.SelectedValue) == 0)
            {
                IQCareWindowMsgBox.ShowWindow("StoreName", this);
                ddlStore.Focus();
                return;
            }
            TimeSpan ts = Convert.ToDateTime(dtpTo.Text) - Convert.ToDateTime(dtpFrom.Text);
            Int32 days = ts.Days;
            if (days < 0)
            {
                IQCareWindowMsgBox.ShowWindow("FromToDate", this);
                return;
            }
            try
            {
                Int32 itemId = 0;
                if (txtItemName.Text.Trim() != "")
                {
                    itemId = Convert.ToInt32(lstSearch.SelectedValue);
                }
                DataSet theDS = GetItems(Convert.ToInt32(ddlStore.SelectedValue), itemId, Convert.ToDateTime(dtpFrom.Text), Convert.ToDateTime(dtpTo.Text));
                if (itemId == 0)
                {
                    ShowGrid(theDS.Tables[1]);
                }
                else
                {
                    ShowGrid(theDS.Tables[2]);
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
        /// Handles the DoubleClick event of the lstSearch control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void lstSearch_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                theItemId = Convert.ToInt32(lstSearch.SelectedValue);
                txtItemName.Text = lstSearch.Text;
                DataView theDV = new DataView(theDrugListing);
                theDV.RowFilter = "Drug_Pk=" + theItemId.ToString();
                if (theDV.ToTable().Rows.Count > 0)
                {
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

        /// <summary>
        /// Validates the login.
        /// </summary>
        /// <returns></returns>
        private bool ValidateLogin()
        {
            if (Convert.ToInt32(ddlStore.SelectedValue) == 0)
            {
                IQCareWindowMsgBox.ShowWindow("StoreName", this);
                ddlStore.Focus();
                return false;
            }            
            return true;
        }

        /// <summary>
        /// Handles the Click event of the btnExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgwBatchSummary.RowCount > 0)
                {
                    IQCareUtils theUtil = new IQCareUtils();
                    DataTable theDT = (DataTable)dgwBatchSummary.DataSource;
                    theDT.Columns.Remove("ItemId");
                    theDT.Columns.Remove("BatchId");
                    string theFilePath = ConfigurationManager.AppSettings.Get("ExcelFilesPath");
                        //System.Configuration.ConfigurationSettings.AppSettings["ExcelFilesPath"];
                    //string theFilePath = System.IO.Directory.GetParent(System.Windows.Forms.Application.ExecutablePath).Parent.Parent.Parent.FullName + "\\IQCare Management\\ExcelFiles\\";
                    theFilePath = theFilePath + "BatchSummary.xls";
                    theUtil.ExportToExcel_Windows(theDT, theFilePath, "");
                }
                else
                {
                    IQCareWindowMsgBox.ShowWindow("GridData", this);
                    return;
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
        /// Handles the KeyUp event of the txtItemName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void txtItemName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlStore.SelectedValue) > 0)
                {
                    if (txtItemName.Text != "")
                    {
                        lstSearch.Visible = true;
                        lstSearch.Width = txtItemName.Width;
                        lstSearch.Left = txtItemName.Left;
                        lstSearch.Top = txtItemName.Top + txtItemName.Height;
                        lstSearch.Height = 300;
                        DataView theDV = new DataView(theDrugListing);
                        theDV.RowFilter = "DrugName like '%" + txtItemName.Text + "%'";
                        if (theDV.Count > 0)
                        {
                            DataTable theDT = theDV.ToTable();
                            BindFunctions theBindManager = new BindFunctions();
                            theBindManager.Win_BindListBox(lstSearch, theDT, "DrugName", "Drug_pk");
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

        /// <summary>
        /// Handles the Click event of the btn_Close control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
