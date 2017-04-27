using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using CrystalDecisions.CrystalReports.Engine;
using Interface.SCM;
using System.Text;

namespace IQCare.SCM
{
    public partial class frmStockSummaryLine : Form
    {
        #region "Variables"

        private DataTable theDTItems = new DataTable();
        private DataTable dtStoreItems = new DataTable();
        #endregion "Variables"

        public frmStockSummaryLine()
        {
            InitializeComponent();
        }


        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {

                if (dgwStockSummary.RowCount > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Excel Documents (*.xls)|*.xls";
                    sfd.FileName = string.Format("StockList{0}.xls", DateTime.Now.ToString("yyyyMMddHHmmss"));
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        //ToCsV(dataGridView1, @"c:\export.xls");
                        ExportToExcel(dgwStockSummary, sfd.FileName); // Here dataGridview1 is your grid view name
                    }


                    //if (dgwStockSummary.RowCount > 0)
                    //{
                    //    IQCareUtils theUtil = new IQCareUtils();
                    //    DataTable theDT = (DataTable)dgwStockSummary.DataSource;
                    //    theDT.Columns.Remove("StoreId");
                    //    //theDT.Columns.Remove("StoreName");
                    //    theDT.Columns.Remove("ItemId");
                    //    string theFilePath = System.Configuration.ConfigurationManager.AppSettings["ExcelFilesPath"];
                    //    theFilePath = theFilePath + "StockSummary.xls";
                    //    theUtil.ExportToExcel_Windows(theDT, theFilePath, "");
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




        private void frmStockSummary_Load(object sender, EventArgs e)
        {
            try
            {
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
                GetStockSummaryItems();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void GetStockSummaryItems()
        {
            ISCMReport objOpenStock = (ISCMReport)ObjectFactory.CreateInstance("BusinessProcess.SCM.BSCMReport,BusinessProcess.SCM");
            DataTable dt = objOpenStock.GetStockSummaryLineList();
            ShowGrid(dt);
        }
        int selectedItemId = 0;
        private void lstSearch_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Int32 theItemId = Convert.ToInt32(lstSearch.SelectedValue);
                selectedItemId = theItemId;
                txtItemName.Text = lstSearch.Text;
                //DataView theDV = new DataView(theDTItems);
                DataView theDV = new DataView(dtStoreItems);
                theDV.RowFilter = "ItemId=" + theItemId.ToString();
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
                dgwStockSummary.Columns.Clear();
                dgwStockSummary.DataSource = null;

                DataGridViewTextBoxColumn colDrugPk = new DataGridViewTextBoxColumn();
                colDrugPk.HeaderText = "Drug PK";
                colDrugPk.DataPropertyName = "Drug_pk";
                colDrugPk.Name = "Drug_pk";
                colDrugPk.Width = 5;
                colDrugPk.ReadOnly = true;
                colDrugPk.Visible = false;

                DataGridViewTextBoxColumn colItemCode = new DataGridViewTextBoxColumn();
                colItemCode.HeaderText = "Item Code";
                colItemCode.DataPropertyName = "ItemCode";
                colItemCode.Name = "ItemCode";
                colItemCode.Width = 100;
                colItemCode.ReadOnly = true;
                colItemCode.Visible = true;

                DataGridViewTextBoxColumn colItemName = new DataGridViewTextBoxColumn();
                colItemName.HeaderText = "Item Description";
                colItemName.DataPropertyName = "DrugName";
                colItemName.Width = 320;
                colItemName.Name = "DrugName";
                colItemName.ReadOnly = true;

                DataGridViewTextBoxColumn colUnit = new DataGridViewTextBoxColumn();
                colUnit.HeaderText = "Unit";
                colUnit.DataPropertyName = "PurchaseUnit";
                colUnit.Name = "PurchaseUnit";
                colUnit.Width = 86;
                colUnit.ReadOnly = true;

                DataGridViewTextBoxColumn colUnitPrice = new DataGridViewTextBoxColumn();
                colUnitPrice.HeaderText = "Purchase Unit Price";
                colUnitPrice.Name = colUnitPrice.DataPropertyName = "PurchaseUnitPrice";
                colUnitPrice.Width = 90;
                colUnitPrice.ReadOnly = true;

                DataGridViewTextBoxColumn colQtyAvail = new DataGridViewTextBoxColumn();
                colQtyAvail.HeaderText = "Quantity Available";
                colQtyAvail.Name = colQtyAvail.DataPropertyName = "Quantity";
                colQtyAvail.Width = 90;
                colQtyAvail.ReadOnly = true;

                DataGridViewTextBoxColumn colItemType = new DataGridViewTextBoxColumn();
                colItemType.HeaderText = "Item Type";
                colItemType.Name = colItemType.DataPropertyName = "ItemType";
                colItemType.Width = 90;
                colItemType.ReadOnly = true;



                dgwStockSummary.Columns.Add(colDrugPk);
                dgwStockSummary.Columns.Add(colItemName);
                dgwStockSummary.Columns.Add(colItemCode);
                dgwStockSummary.Columns.Add(colItemType);
                dgwStockSummary.Columns.Add(colUnit);
                dgwStockSummary.Columns.Add(colUnitPrice);
                dgwStockSummary.Columns.Add(colQtyAvail);
                dgwStockSummary.AutoGenerateColumns = false;
                dgwStockSummary.DataSource = theDT;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        private void ExportToExcel(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }
        private void txtItemName_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                selectedItemId = 0;

                if (txtItemName.Text != "" && txtItemName.TextLength > 2)
                {
                    lstSearch.Visible = true;
                    lstSearch.Width = txtItemName.Width;
                    lstSearch.Left = txtItemName.Left;
                    lstSearch.Top = txtItemName.Top + txtItemName.Height;
                    lstSearch.Height = 300;
                    lstSearch.BringToFront();
                    //DataView theDV = new DataView(theDTItems);
                    DataView theDV = new DataView(dtStoreItems);
                    theDV.RowFilter = "DrugName like '" + txtItemName.Text + "*'";
                    //theDV.RowFilter = "ItemName like '" + txtItemName.Text + "*'";
                    if (theDV.Count > 0)
                    {
                        DataTable theDTdata = theDV.ToTable();
                        BindFunctions theBindManager = new BindFunctions();
                        //theBindManager.Win_BindListBox(lstSearch, theDTdata, "DrugName", "Drug_pk");
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
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
    }
}