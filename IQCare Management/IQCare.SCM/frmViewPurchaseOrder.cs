using System;
using System.Data;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.SCM;

namespace IQCare.SCM
{
    public partial class frmViewPurchaseOrder : Form
    {
       
        public frmViewPurchaseOrder()
        {
            InitializeComponent();
        }
        string frmname;
        private void frmViewPurchaseOrder_Load(object sender, EventArgs e)
        {
            
            if (GblIQCare.theArea == "PO")
            {
                this.Text = "View Purchase Order";
                frmname = "IQCare.SCM.frmPurchaseOrder, IQCare.SCM";
            }
            else if (GblIQCare.theArea == "CR")
            {
                this.Text = "View Counter Requisitions";
                frmname = "IQCare.SCM.frmInterStoreTransfer, IQCare.SCM";
            }
            if (GblIQCare.CurrentMenu == MenuChoice.PurchaseOrder)
            {
                this.Text = "View Purchase Order";
                frmname = "IQCare.SCM.frmPurchaseOrder, IQCare.SCM";
            }
            else if (GblIQCare.CurrentMenu == MenuChoice.CounterRequistion)
            {
                this.Text = "View Counter Requisitions";
                frmname = "IQCare.SCM.frmInterStoreTransfer, IQCare.SCM";
            }
            else if (GblIQCare.CurrentMenu == MenuChoice.POWithGRN)
            {
                this.Text = "View Purchase Order";
                frmname = "IQCare.SCM.frmPurchaseReceiveOrder, IQCare.SCM";
            }
            else if (GblIQCare.CurrentMenu == MenuChoice.CRWithIV)
            {
                this.Text = "View Inter Store Transfer/Counter Requisition";
                frmname = "IQCare.SCM.frmInterRequisitionIssue, IQCare.SCM";
            }
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            CreateGrid();
            ActiveControl = btnNew;
        }

        private void  CreateGrid()
        {
            try
            {
                //IMasterList objPODetails = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                IPurchase objPODetails = (IPurchase)ObjectFactory.CreateInstance("BusinessProcess.SCM.BPurchase,BusinessProcess.SCM");
                DataTable theDTPODetails = objPODetails.GetPurchaseOrderDetails(GblIQCare.AppUserId, GblIQCare.intStoreId, GblIQCare.AppLocationId);

                dgwPurchaseOrder.Columns.Clear();
                dgwPurchaseOrder.AutoGenerateColumns = false;
                dgwPurchaseOrder.AllowUserToAddRows = false;
              

                DataGridViewTextBoxColumn theColumnPoId = new DataGridViewTextBoxColumn();
              //  theColumnItemCode.HeaderText = "Item Code";
                theColumnPoId.Name = "POId";
                theColumnPoId.DataPropertyName = "POId";

                DataGridViewTextBoxColumn theColumnStatus = new DataGridViewTextBoxColumn();
                //  theColumnItemCode.HeaderText = "Item Code";
                theColumnStatus.Name = "Status";
                theColumnStatus.DataPropertyName = "Status";
                theColumnStatus.Width = 143;


                DataGridViewTextBoxColumn theColumnOrderNumber = new DataGridViewTextBoxColumn();
                theColumnOrderNumber.HeaderText = "Order Number";
                theColumnOrderNumber.Name = "PONumber";
                theColumnOrderNumber.DataPropertyName = "PONumber";
                theColumnOrderNumber.Width = 130;
                theColumnOrderNumber.ReadOnly = true;

                DataGridViewTextBoxColumn theColumnPODate = new DataGridViewTextBoxColumn();
                theColumnPODate.HeaderText = "Order Date";
                theColumnPODate.DataPropertyName = "OrderDate";
                theColumnPODate.Name = "OrderDate";
                theColumnPODate.Width =140;
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

                dgwPurchaseOrder.DataSource = theDTPODetails;
                dgwPurchaseOrder.Columns.Add(theColumnPoId);
             
                dgwPurchaseOrder.Columns.Add(theColumnOrderNumber);
                dgwPurchaseOrder.Columns.Add(theColumnPODate);
                dgwPurchaseOrder.Columns.Add(theColumnSupplier);
                dgwPurchaseOrder.Columns.Add(theColumnSourceStore);               
                dgwPurchaseOrder.Columns.Add(theColumnDestStore);
                dgwPurchaseOrder.Columns.Add(theColumnStatus);
                theColumnPoId.Visible = false;
                if (GblIQCare.theArea == "PO")
                {
                    theColumnSourceStore.Visible = false;
                    theColumnSupplier.Visible = true;
                }
                                  
                else if (GblIQCare.theArea == "CR")
                {
                    theColumnSupplier.Visible = false;
                    theColumnSourceStore.Visible = true;
                }
               // theColumnStatus.Visible=false;
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            GblIQCare.PurchaseOrderID = 0;
            if (GblIQCare.theArea == "PO")
            {
                
                frmname = "IQCare.SCM.frmPurchaseOrder, IQCare.SCM";
  
            }
            else if (GblIQCare.theArea == "CR")
            {
                
                frmname = "IQCare.SCM.frmInterStoreTransfer, IQCare.SCM";
            }
            else if (GblIQCare.CurrentMenu == MenuChoice.CRWithIV)
            {
          
                   frmname = "IQCare.SCM.frmInterRequisitionIssue, IQCare.SCM";
            }
            else if (GblIQCare.CurrentMenu == MenuChoice.POWithGRN)
            {

                frmname = "IQCare.SCM.frmPurchaseReceiveOrder, IQCare.SCM";
            }
            Form theForm = (Form)Activator.CreateInstance(Type.GetType(frmname.ToString()));
            
            theForm.Top = 2;
            theForm.Left = 2;
            theForm.MdiParent = this.MdiParent;
            theForm.Show();
            this.Close();
        }

        private void dgwPurchaseOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                if (!String.IsNullOrEmpty(Convert.ToString(dgwPurchaseOrder.Rows[e.RowIndex].Cells[0].Value)))
                {
                    dgwPurchaseOrder.Rows[dgwPurchaseOrder.CurrentCell.RowIndex].Selected = true;
                    GblIQCare.PurchaseOrderID = Convert.ToInt32(dgwPurchaseOrder.Rows[e.RowIndex].Cells[0].Value.ToString());
                    //Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmPurchaseOrder, IQCare.SCM"));
                    Form theForm = (Form)Activator.CreateInstance(Type.GetType(frmname.ToString()));
                    theForm.Top = 2;
                    theForm.Left = 2;
                    theForm.MdiParent = this.MdiParent;
                    theForm.Show();
                    this.Close();
                }
             
            }

        }

        private void dgwPurchaseOrder_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }

        
    }
}
