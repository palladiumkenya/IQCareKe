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
    public partial class frmSetUserStore : Form
    {
        public frmSetUserStore()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSetUserStore_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            lblUserName.Text = GblIQCare.AppUserName;
            BindStoreNameDropdown(GblIQCare.AppUserId);
            if (GblIQCare.theArea == "IV" || GblIQCare.theArea=="CR_IV") lblStore.Text = "Select Source Store";
            if(GblIQCare.theArea == "Dispense") lblStore.Text = "Select Dispensing Store";
            if(GblIQCare.CurrentMenu == MenuChoice.IssueVoucher ) lblStore.Text = "Select Source Store";
          if( GblIQCare.CurrentMenu == MenuChoice.CRWithIV)  lblStore.Text = "Destination Store Name:";
            if (GblIQCare.CurrentMenu == MenuChoice.Dispense) lblStore.Text = "Select Dispensing Store";
        }
        private void BindStoreNameDropdown(int UserID)
        {
            IQCareUtils theUtils = new IQCareUtils();
            ddlStoreName.Items.Clear();
            IMasterList objProgramlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
            DataTable theDT = objProgramlist.GetStoreByUser(UserID);
            DataView theDV = new DataView(theDT);
            BindFunctions theBind = new BindFunctions();
            switch (GblIQCare.CurrentMenu)
            {
                case MenuChoice.PurchaseOrder:  //purchase order or good received note
                case MenuChoice.GoodReceived:
                case MenuChoice.POWithGRN:
                    theDV.RowFilter = "StoreCategory = 'Purchasing' OR StoreCategory = 'Purchasing and Dispensing' ";
                    break;
                case MenuChoice.Dispense:
                    theDV.RowFilter = "StoreCategory = 'Dispensing' OR StoreCategory = 'Purchasing and Dispensing' ";
                    break;
                case MenuChoice.CounterRequistion: //counter requisition
                case MenuChoice.CRWithIV:
                    theDV.RowFilter = "StoreCategory <> 'Purchasing' OR StoreCategory <> 'Purchasing and Dispensing' ";
                    break;
            }
            /*switch (GblIQCare.theArea)
            {
                case  "PO":  //purchase order or good received note
                case "GRN":
                     theDV.RowFilter = "StoreCategory = 'Purchasing' ";
                    break;
                case "Dispense":
                    theDV.RowFilter = "StoreCategory = 'Dispensing' ";
                    break;
                case "CR": //counter requisition
                    theDV.RowFilter = "StoreCategory <> 'Purchasing' ";
                    break;
            }*/
        
            DataTable theStoreDT = theDV.ToTable();
            
            theBind.Win_BindCombo(ddlStoreName, theStoreDT, "StoreName", "StoreId","StoreName");
            if (theDT.Rows.Count == 1)
            {
                ddlStoreName.SelectedIndex = 0;
                //ddlStoreName.Enabled = false;
            }
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ddlStoreName.SelectedValue.ToString() == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Store Name";
                IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                ddlStoreName.Focus();
                return;
            }
                
            GblIQCare.intStoreId = Convert.ToInt32(ddlStoreName.SelectedValue);

            
            if ((GblIQCare.theArea == "PO")|| (GblIQCare.theArea == "PO_GRN") || (GblIQCare.theArea == "CR") || (GblIQCare.theArea == "CR_IV"))
            {
                DataTable theDT = (DataTable)ddlStoreName.DataSource;
                DataView theDV = new DataView(theDT);
              
                //theDV.RowFilter = "StoreId=" + ddlStoreName.SelectedValue.ToString() + " and CentralStore=1";
                theDV.RowFilter = "StoreId=" + ddlStoreName.SelectedValue.ToString() + " and (StoreCategory='Purchasing' OR StoreCategory = 'Purchasing and Dispensing')";
                if (theDV.Count < 1)
                {
                    //IQCareWindowMsgBox.ShowWindow("StoreNotAuthorize", this);
                    //return;
                    GblIQCare.ModePurchaseOrder = 2;
                }
                else
                {
                    GblIQCare.ModePurchaseOrder = 1;
                }
                Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmViewPurchaseOrder, IQCare.SCM"));
                theForm.Top = 2;
                theForm.Left = 2;
                theForm.MdiParent = this.MdiParent;
                theForm.Show();
                this.Close();
            }
            if (GblIQCare.theArea == "Dispense")
            {
                DataTable theDT = (DataTable)ddlStoreName.DataSource;
                DataView theDV = new DataView(theDT);
                //theDV.RowFilter = "StoreId=" + ddlStoreName.SelectedValue.ToString() + " and DispensingStore=1";
                theDV.RowFilter = "StoreId=" + ddlStoreName.SelectedValue.ToString() + " and (StoreCategory='Dispensing' OR StoreCategory = 'Purchasing and Dispensing')";
                if (theDV.Count < 1)
                {
                    IQCareWindowMsgBox.ShowWindow("StoreNotAuthorize", this);
                    return;
                }
                Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmPatientDrugDispense, IQCare.SCM"));
                theForm.Top = 2;
                theForm.Left = 2;
                theForm.MdiParent = this.MdiParent;
                theForm.Show();
                this.Close();
            }
          else  if ((GblIQCare.theArea == "GRN")||(GblIQCare.theArea == "IV") )
            {
                DataTable theDT = (DataTable)ddlStoreName.DataSource;
                DataView theDV = new DataView(theDT);
               
                string rowFilter = "StoreId=" + ddlStoreName.SelectedValue.ToString();
                if ((GblIQCare.theArea == "GRN"))
                {
                    rowFilter += " and (StoreCategory='Purchasing' OR StoreCategory = 'Purchasing and Dispensing')";
                   // rowFilter += " and CentralStore=1";
                }
                if ((GblIQCare.theArea == "PO_GRN"))
                {
                    rowFilter += " and (StoreCategory='Purchasing' OR StoreCategory = 'Purchasing and Dispensing')";
                    // rowFilter += " and CentralStore=1";
                }
                theDV.RowFilter = rowFilter;
                GblIQCare.ModePurchaseOrder = (GblIQCare.theArea == "IV" || (GblIQCare.theArea == "CR_IV")) ? 2 : 1;
                
                Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmViewGoodReceiveNote, IQCare.SCM"));
                theForm.Top = 2;
                theForm.Left = 2;
                theForm.MdiParent = this.MdiParent;
                theForm.Show();
                this.Close();
            }
        }
    }
}
