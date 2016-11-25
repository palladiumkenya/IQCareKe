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
    public partial class frmViewGoodReceiveNote : Form
    {
        public frmViewGoodReceiveNote()
        {
            InitializeComponent();
        }
        string frmname;
        bool IsSourceStore = false;
        private void frmViewGoodReceiveNote_Load(object sender, EventArgs e)
        {
            if (GblIQCare.ModePurchaseOrder == 2)
            {
                this.Text = "View Issue Voucher";
                frmname = "IQCare.SCM.frmIssueVoucher, IQCare.SCM";
                this.IsSourceStore = true;
            }
            else
            {
                this.Text = "View Goods Receive Note";
                frmname = "IQCare.SCM.frmGoodsRecieveNote, IQCare.SCM";
                this.IsSourceStore = false;
            }
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            CreateGrid();
           
        }
        private void CreateGrid()
        {
            try
            {
                //IMasterList objPODetails = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                IPurchase objPODetails = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
                DataTable theDTPODetails = objPODetails.GetPurchaseOrderDetailsForGRN(GblIQCare.AppUserId, GblIQCare.intStoreId, GblIQCare.AppLocationId,this.IsSourceStore);

                dgwGRN.Columns.Clear();
                dgwGRN.AutoGenerateColumns = false;
                dgwGRN.AllowUserToAddRows = false;


                DataGridViewTextBoxColumn theColumnPoId = new DataGridViewTextBoxColumn();
                //  theColumnItemCode.HeaderText = "Item Code";
                theColumnPoId.Name = "POId";
                theColumnPoId.DataPropertyName = "POId";

                DataGridViewTextBoxColumn theColumnGRNId = new DataGridViewTextBoxColumn();
                theColumnGRNId.Name = "GRNId";
                theColumnGRNId.DataPropertyName = "GRNId";
                theColumnGRNId.Visible = false;

                DataGridViewTextBoxColumn theColumnStatus = new DataGridViewTextBoxColumn();
                theColumnStatus.Name = "Status";
                theColumnStatus.DataPropertyName = "Status";
                theColumnStatus.Width = 143;


                DataGridViewTextBoxColumn theColumnOrderNumber = new DataGridViewTextBoxColumn();
                theColumnOrderNumber.HeaderText = "Order Number";
                theColumnOrderNumber.Name = "OrderNo";
                theColumnOrderNumber.DataPropertyName = "OrderNo";
                theColumnOrderNumber.Width = 130;
                theColumnOrderNumber.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnPODate = new DataGridViewTextBoxColumn();
                theColumnPODate.HeaderText = "Order Date";
                theColumnPODate.DataPropertyName = "OrderDate";
                theColumnPODate.Name = "OrderDate";
                theColumnPODate.Width = 140;
                theColumnPODate.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnSupplier = new DataGridViewTextBoxColumn();
                theColumnSupplier.HeaderText = "Supplier";
                theColumnSupplier.DataPropertyName = "SupplierName";
                theColumnSupplier.Name = "SupplierName";
                theColumnSupplier.Width = 175;
                theColumnSupplier.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnSourceStore = new DataGridViewTextBoxColumn();
                theColumnSourceStore.HeaderText = "Source Store";
                theColumnSourceStore.DataPropertyName = "SourceName";
                theColumnSourceStore.Name = "SourceName";
                theColumnSourceStore.Width = 180;
                theColumnSourceStore.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnDestStore = new DataGridViewTextBoxColumn();
                theColumnDestStore.HeaderText = "Destination Store";
                theColumnDestStore.DataPropertyName = "DestStore";
                theColumnDestStore.Name = "DestStore";
                theColumnDestStore.Width = 180;
                theColumnDestStore.ReadOnly = true;

                dgwGRN.DataSource = theDTPODetails;

                dgwGRN.Columns.Add(theColumnPoId);
                dgwGRN.Columns.Add(theColumnGRNId);
                dgwGRN.Columns.Add(theColumnOrderNumber);
                dgwGRN.Columns.Add(theColumnPODate);
                dgwGRN.Columns.Add(theColumnSupplier);
                dgwGRN.Columns.Add(theColumnSourceStore);
                dgwGRN.Columns.Add(theColumnDestStore);
                dgwGRN.Columns.Add(theColumnStatus);
                theColumnPoId.Visible = false;
                if (GblIQCare.ModePurchaseOrder == 2)
                {
                    theColumnSupplier.Visible = false;
                    theColumnSourceStore.Visible = true;
                }
                else
                {
                    theColumnSupplier.Visible = true;
                    theColumnSourceStore.Visible = false;
                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgwGRN_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (!String.IsNullOrEmpty(Convert.ToString(dgwGRN.Rows[e.RowIndex].Cells[0].Value)))
                {
                    dgwGRN.Rows[dgwGRN.CurrentCell.RowIndex].Selected = true;
                    GblIQCare.PurchaseOrderID = Convert.ToInt32(dgwGRN.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Form theForm = (Form)Activator.CreateInstance(Type.GetType(frmname.ToString()));
                    theForm.Top = 2;
                    theForm.Left = 2;
                    theForm.MdiParent = this.MdiParent;
                    theForm.Show();
                    this.Close();
                }

            }
        }
    }
}
