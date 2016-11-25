using System;
using System.Collections;
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
    public partial class frmItemMaster : Form
    {
        DataTable theDTAllItems = new DataTable();
        string ItemCode;
        public frmItemMaster()
        {
            InitializeComponent();
        }

        private void Init_Form()
        {
            Bind_Controls();
            txtItemCode.Select();
        }

        private void Bind_Controls()
        {
            string[] theItem = GblIQCare.theItemId.ToString().Split('-');
            IMasterList theMasterList = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList, BusinessProcess.SCM");
            DataSet theDS = theMasterList.GetItemDetails(Convert.ToInt32(theItem[2]));
            theDTAllItems = theDS.Tables[5];
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_BindCombo(cmbDispensingUnit, theDS.Tables[3].Copy(), "Name", "Id");
            theBindManager.Win_BindCombo(cmbPurchaseUnit, theDS.Tables[3].Copy(), "Name", "Id");
            theBindManager.Win_BindCombo(cmbManufacturer, theDS.Tables[4], "Name", "Id");
            theBindManager.Win_BindCombo(cmbDispensingUnit, theDS.Tables[3].Copy(), "Name", "Id");

            if (theDS.Tables[0].Rows.Count > 0)
            {
                txtItemType.Text = theItem[0].ToString();
                txtItemSubtype.Text = theItem[1].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("DrugId") == true)
                    txtItemCode.Text = "";
                else
                    txtItemCode.Text = theDS.Tables[0].Rows[0]["DrugId"].ToString();
                    ItemCode = txtItemCode.Text;
                if (theDS.Tables[0].Rows[0].IsNull("FDACode") == true)
                    txtFDACode.Text = "";
                else
                    txtFDACode.Text = theDS.Tables[0].Rows[0]["FDACode"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("GenericName") == true)
                    txtGeneric.Text = "";
                else
                    txtGeneric.Text = theDS.Tables[0].Rows[0]["GenericName"].ToString();
                txtTradeName.Text = theDS.Tables[0].Rows[0]["DrugName"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("DispensingUnit") == true)
                    cmbDispensingUnit.SelectedValue = 0;
                else
                    cmbDispensingUnit.SelectedValue = theDS.Tables[0].Rows[0]["DispensingUnit"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("GenAbbr") == true)
                    txtArvAbbrevstion.Text = "";
                else
                    txtArvAbbrevstion.Text = theDS.Tables[0].Rows[0]["GenAbbr"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("PurchaseUnit") == true)
                    cmbPurchaseUnit.SelectedValue = 0;
                else
                    cmbPurchaseUnit.SelectedValue = theDS.Tables[0].Rows[0]["PurchaseUnit"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("QtyPerPurchaseUnit") == true)
                    txtUnitQty.Text = "";
                else
                    txtUnitQty.Text = theDS.Tables[0].Rows[0]["QtyPerPurchaseUnit"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("PurchaseUnitPrice") == true)
                    txtPurchaseUnitPrice.Text = "";
                else
                    txtPurchaseUnitPrice.Text = theDS.Tables[0].Rows[0]["PurchaseUnitPrice"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("Manufacturer") == true)
                    cmbManufacturer.SelectedValue = 0;
                else
                    cmbManufacturer.SelectedValue = theDS.Tables[0].Rows[0]["Manufacturer"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("DispensingUnitPrice") == true)
                    txtDispenseUnitPrice.Text = "";
                else
                    txtDispenseUnitPrice.Text = theDS.Tables[0].Rows[0]["DispensingUnitPrice"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("DispensingMargin") == true)
                    txtdespensingMargin.Text = "";
                else
                    txtdespensingMargin.Text = theDS.Tables[0].Rows[0]["DispensingMargin"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("EffectiveDate") == true)
                    dtpEffectiveDate.CustomFormat = "dd-MMM-yyyy";
                else
                {
                    dtpEffectiveDate.CustomFormat = "dd-MMM-yyyy";
                    dtpEffectiveDate.Text = theDS.Tables[0].Rows[0]["EffectiveDate"].ToString();
                }
                if (theDS.Tables[0].Rows[0].IsNull("SellingUnitPrice") == true)
                    txteditsellingprice.Text = "";
                else
                    txteditsellingprice.Text = theDS.Tables[0].Rows[0]["SellingUnitPrice"].ToString();

                if (theDS.Tables[0].Rows[0].IsNull("MinStock") == true)
                    txtMinStock.Text = "";
                else
                    txtMinStock.Text = theDS.Tables[0].Rows[0]["MinStock"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("MaxStock") == true)
                    txtMaxStock.Text = "";
                else
                    txtMaxStock.Text = theDS.Tables[0].Rows[0]["MaxStock"].ToString();
                if (theDS.Tables[0].Rows[0].IsNull("DeleteFlag") == true)
                    cmbStatus.SelectedIndex = 0;
                else
                    cmbStatus.SelectedIndex = Convert.ToInt32(theDS.Tables[0].Rows[0]["DeleteFlag"]);

                CalculateSellingPrice();
            }

        }

        private void frmItemMaster_Load(object sender, EventArgs e)
        {
            try
            {
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
                Init_Form();
            }
            catch(Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        private void txtItemCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_String(e);
        }

        private void txtFDACode_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_String(e);
        }

        private void txtUnitQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_Numeric(e);
        }

        private void txtPurchaseUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_decimal(e);
        }

        private void txtDispenseUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_decimal(e);
           
        }

        private void txtdespensingMargin_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_decimalNagetive(e);

        }

        private void txtMinStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_Numeric(e);
        }

        private void txtMaxStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions theBindManager = new BindFunctions();
            theBindManager.Win_Numeric(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmSCMitemList, IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            this.Close();
        }

        private void CalculateSellingPrice()
        {
            if (txtdespensingMargin.Text == ".")
                return;
 
            decimal theDispUnitPrice = 0;
            decimal theMargin = 0;

            if (txtDispenseUnitPrice.Text == "")
                theDispUnitPrice = 0;
            else
                theDispUnitPrice = Convert.ToDecimal(txtDispenseUnitPrice.Text);
            if (txtdespensingMargin.Text == "")
                theMargin = 0;
            else 
                 theMargin = Convert.ToDecimal(txtdespensingMargin.Text);
            if (theDispUnitPrice == 0)
                txtSellingPrice.Text = "0";
            else if (theMargin == 0)
                txtSellingPrice.Text = theDispUnitPrice.ToString();
            else
                txtSellingPrice.Text = Math.Round((theDispUnitPrice + (theMargin / 100) * theDispUnitPrice), 2).ToString();

            if (txteditsellingprice.Text == "")
            {
                txteditsellingprice.Text = txtSellingPrice.Text;
            }
        }

        private void CalucateUnitPrice()
        {
            Int32 UnitQty = 0;
            decimal UnitPrice = 0;
            if (txtUnitQty.Text == "")
                UnitQty = 0;
            else
                UnitQty = Convert.ToInt32(txtUnitQty.Text);
            if (txtPurchaseUnitPrice.Text == "")
                UnitPrice = 0;
            else
                UnitPrice = Convert.ToDecimal(txtPurchaseUnitPrice.Text);
            if (UnitQty == 0 || UnitPrice == 0)
                txtDispenseUnitPrice.Text = "0";
            else
                txtDispenseUnitPrice.Text = Math.Round((UnitPrice / UnitQty),2).ToString();
        }

        private void txtdespensingMargin_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtdespensingMargin.Text.Contains("-") && txtdespensingMargin.Text.Length == 1)
            {
            }
            else
            {
                CalculateSellingPrice();
            }
        }

        private bool ValidateForm()
        {
            DataView theDV = new DataView(theDTAllItems);
            if (ItemCode != txtItemCode.Text)
            {
                theDV.RowFilter = "DrugId = '" + txtItemCode.Text + "'";
                if (theDV.ToTable().Rows.Count > 0)
                {
                    IQCareWindowMsgBox.ShowWindow("CheckDuplicateValue", this);
                    return false;
                }
            }

            if (Convert.ToInt32(cmbDispensingUnit.SelectedValue) < 1)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Dispensing Unit";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankDropDown", theBuilder, this);
                return false;
            }
            if (Convert.ToInt32(cmbPurchaseUnit.SelectedValue) < 1)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Purchase Unit";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankDropDown", theBuilder, this);
                return false;
            }
            if (txtUnitQty.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Purchase Unit Quantity";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
                return false;
            }
            if (txtPurchaseUnitPrice.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Purchase Unit Price";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
                return false;
            }
            if (Convert.ToInt32(cmbManufacturer.SelectedValue) < 1)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Manufacturer";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankDropDown", theBuilder, this);
                return false;
            }
            if (txtDispenseUnitPrice.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Dispensing Unit Price";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
                return false;
            }
            if (txtdespensingMargin.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Dispensing Margin";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
                return false;
            }
          /*  if (txteditsellingprice.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Selling price";
                IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
                return false;
            }*/
            return true;
        }
        private Hashtable BuildHashTable()
        {
            string[] theItem = GblIQCare.theItemId.ToString().Split('-');
            Hashtable theHash = new Hashtable();
            theHash.Add("Drug_Pk", theItem[2].ToString());
            theHash.Add("ItemCode", txtItemCode.Text);
            theHash.Add("FDACode", txtFDACode.Text);
            theHash.Add("DispensingUnit", cmbDispensingUnit.SelectedValue.ToString());
            theHash.Add("PurchaseUnit", cmbPurchaseUnit.SelectedValue.ToString());
            theHash.Add("PurchaseUnitQty", txtUnitQty.Text);
            theHash.Add("PurchaseUnitPrice", txtPurchaseUnitPrice.Text);
            theHash.Add("Manufacturer",cmbManufacturer.SelectedValue.ToString());
            theHash.Add("DispensingUnitPrice",txtDispenseUnitPrice.Text);
            theHash.Add("DispensingMargin",txtdespensingMargin.Text);
            theHash.Add("SellingPrice",txtSellingPrice.Text);
            ///theHash.Add("SellingPrice", txteditsellingprice.Text);
            theHash.Add("EffectiveDate",dtpEffectiveDate.Text);
            theHash.Add("Status",cmbStatus.SelectedIndex);
            theHash.Add("MaxQty",txtMaxStock.Text);
            theHash.Add("MinQty",txtMinStock.Text);
            theHash.Add("UserId", GblIQCare.AppUserId.ToString());
            return theHash;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateForm() == false)
                    return;

                Hashtable theHash = BuildHashTable();
                IMasterList theItemManager = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList, BusinessProcess.SCM");
                theItemManager.SaveUpdateItemMaster(theHash);
                IQCareWindowMsgBox.ShowWindowConfirm("ItemMasterUpdate", this);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void txtUnitQty_KeyUp(object sender, KeyEventArgs e)
        {
            CalucateUnitPrice();
        }

        private void txtPurchaseUnitPrice_KeyUp(object sender, KeyEventArgs e)
        {
            CalucateUnitPrice();
        }

       


    }
}
