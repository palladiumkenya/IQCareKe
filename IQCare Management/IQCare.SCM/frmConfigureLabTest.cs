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
    public partial class frmConfigureLabTest : Form
    {
        public frmConfigureLabTest()
        {
            InitializeComponent();
        }

        private void frmConfigureLabTest_Load(object sender, EventArgs e)
        {
            dtpEffectiveDate.CustomFormat = "dd-MMM-yyyy";
            dtpEffectiveDate.Text = GblIQCare.CurrentDate;
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            SetRights();
            BindCombo();
            FillallDetails();
            cmbTestLocation_SelectionChangeCommitted(sender, e);
        }
        public void BindCombo()
        {
            try
            {
                BindFunctions objBindControls = new BindFunctions();
                DataTable dtLocs = new DataTable();
                ILaboratory objFormDetail;
                objFormDetail = (ILaboratory)ObjectFactory.CreateInstance("BusinessProcess.SCM.BLaboratory,BusinessProcess.SCM");
                dtLocs = objFormDetail.GetLabLocationList();
                objBindControls.Win_BindCombo(cmbTestLocation, dtLocs, "Name", "Id");
                cmbTestLocation.SelectedIndex = 0;
                

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        private void FillallDetails()
        {
            try
            {
                ILaboratory objallFormDetail;
                objallFormDetail = (ILaboratory)ObjectFactory.CreateInstance("BusinessProcess.SCM.BLaboratory,BusinessProcess.SCM");
                DataTable dtalldetails = objallFormDetail.GetLabList(Convert.ToInt32(GblIQCare.LabTestId));
                if(dtalldetails.Rows.Count>0)
                {

                    txtDisplayName.Text = dtalldetails.Rows[0]["SubTestName"].ToString();
                    txtlionccode.Text = dtalldetails.Rows[0]["loinccode"].ToString();
                    if (dtalldetails.Rows[0]["TestLocation"].ToString() != "")
                    {
                        cmbTestLocation.SelectedValue = Convert.ToInt32(dtalldetails.Rows[0]["TestLocation"].ToString());
                    }
                    else
                        cmbTestLocation.SelectedIndex = 0;
                    dtpEffectiveDate.Text = (dtalldetails.Rows[0]["EffectiveDate"].ToString() == "") ? GblIQCare.CurrentDate : dtalldetails.Rows[0]["EffectiveDate"].ToString();
                    txtCost.Text = dtalldetails.Rows[0]["TestCostPrice"].ToString();
                    txtMarginpect.Text = dtalldetails.Rows[0]["TestMargin"].ToString();
                    txtOutSrcLoc.Text = dtalldetails.Rows[0]["OutsrcLocation"].ToString();
                    txtsellingprice.Text = dtalldetails.Rows[0]["TestSellingPrice"].ToString();
                    if (dtalldetails.Rows[0]["DeleteFlag"].ToString() == "0")
                    {
                        cmbStatus.SelectedItem = "Active";
                    }
                    else if (dtalldetails.Rows[0]["DeleteFlag"].ToString() == "1")
                    {
                        cmbStatus.SelectedItem = "In-Active";
                        txtCost.Enabled = false;
                        cmbTestLocation.Enabled = false;
                        txtMarginpect.Enabled = false;
                    }
                    else
                        cmbStatus.SelectedItem = "Active";


                }


            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void cmbTestLocation_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //if ((cmbTestLocation.SelectedValue.ToString() == "302") || (cmbTestLocation.SelectedValue.ToString() == "303") || (cmbTestLocation.SelectedValue.ToString() == "306"))
            if (((System.Data.DataRowView)(cmbTestLocation.SelectedItem)).Row.ItemArray[1].ToString().ToUpper().StartsWith("OUTSOURCED"))
            {
                txtOutSrcLoc.Enabled = true;
            }
            else
            {
                txtOutSrcLoc.Enabled = false;
                txtOutSrcLoc.Text = "";
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmLabItemDetails, IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 0;
            this.Close();
            GblIQCare.LabTestId = 0;
            theForm.Show();
            
        }

        private void btsave_Click(object sender, EventArgs e)
        {
            if (Validation_Form() == true)
            {
                DataTable theDT = new DataTable();
                theDT.Columns.Add("SubTestId", typeof(int));
                theDT.Columns.Add("SubTestName", typeof(string));
                theDT.Columns.Add("Lionccode", typeof(string));
                theDT.Columns.Add("TestLocation", typeof(int));
                theDT.Columns.Add("EffectiveDate", typeof(DateTime));
                theDT.Columns.Add("TestCostPrice", typeof(float));
                theDT.Columns.Add("TestMargin", typeof(float));
                theDT.Columns.Add("TestSellingPrice", typeof(float));
                theDT.Columns.Add("OutsrcLocation", typeof(string));
                theDT.Columns.Add("Status", typeof(string));
                ///////////////////////////
                DataRow theDRow = theDT.NewRow();
                theDRow["SubTestId"] = GblIQCare.LabTestId;
                theDRow["SubTestName"] = txtDisplayName.Text;
                theDRow["Lionccode"] = txtlionccode.Text;
                theDRow["TestLocation"] = cmbTestLocation.SelectedValue;
                theDRow["EffectiveDate"] = String.Format("{0:dd-MMM-yyyy}", dtpEffectiveDate.Text);
                theDRow["TestCostPrice"] = txtCost.Text;
                theDRow["TestMargin"] = txtMarginpect.Text;
                theDRow["TestSellingPrice"] = txtsellingprice.Text;
                theDRow["OutsrcLocation"] = txtOutSrcLoc.Text;
                theDRow["Status"] = cmbStatus.SelectedItem;
                //theDRow["FundingEndDate"] = String.Format("{0:dd-MMM-yyyy}", dtpEndDate.Text);
                theDT.Rows.Add(theDRow);
                theDT.AcceptChanges();

                ILaboratory objLaboratory = (ILaboratory)ObjectFactory.CreateInstance("BusinessProcess.SCM.BLaboratory,BusinessProcess.SCM");
                int retrows = objLaboratory.SaveUpdateLabConfiguration(theDT, GblIQCare.AppUserId);
                IQCareWindowMsgBox.ShowWindow("ProgramSave", this);

                Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmLabItemDetails, IQCare.SCM"));
                theForm.MdiParent = this.MdiParent;
                theForm.Left = 0;
                theForm.Top = 0;
                this.Close();
                GblIQCare.LabTestId = 0;
                theForm.Show();
            }

        }

        public void SetRights()
        {
            if (GblIQCare.HasFunctionRight(ApplicationAccess.Laboratory, FunctionAccess.Update, GblIQCare.dtUserRight) == false)
            {
                btsave.Enabled = false;
            }
            

        }
        private void CalculateSellingPrice()
        {
            decimal theDispUnitPrice = 0;
            decimal theMargin = 0;

            if (txtCost.Text == "")
                theDispUnitPrice = 0;
            else
                theDispUnitPrice = Convert.ToDecimal(txtCost.Text);
            if (txtMarginpect.Text == "")
                theMargin = 0;
            else
                theMargin = Convert.ToDecimal(txtMarginpect.Text);
            if (theDispUnitPrice == 0)
                txtsellingprice.Text = "0";
            else
            {
                if (theMargin == 0)
                    txtsellingprice.Text = theDispUnitPrice.ToString();
                else
                    txtsellingprice.Text = (theDispUnitPrice + (theMargin / 100) * theDispUnitPrice).ToString();

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCost_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateSellingPrice();

        }

        private void txtMarginpect_KeyUp(object sender, KeyEventArgs e) //deepika
        {
            if (txtMarginpect.Text.Contains("-") && txtMarginpect.Text.Length == 1)
            {
            }
            else
            {
                CalculateSellingPrice();
            }

        }
        private Boolean Validation_Form()
        {
            if (cmbTestLocation.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Test Location";
                IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                return false;

            }
            if (txtCost.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Cost Price";
                IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                return false;
            }
            if (txtMarginpect.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Margin %";
                IQCareWindowMsgBox.ShowWindow("BlankTextBox", theBuilder, this);
                return false;
            }
            
            return true;
        }

        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions obj = new BindFunctions();
            obj.Win_decimal(e);
        }

        private void txtMarginpect_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions objMargin = new BindFunctions();
            objMargin.Win_decimalNagetive(e);
        }

        private void txtlionccode_KeyPress(object sender, KeyPressEventArgs e)
        {
            BindFunctions objlionc = new BindFunctions();
            objlionc.Win_String(e);

        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedItem.ToString() == "Active")
            {
                txtCost.Enabled = true;
                cmbTestLocation.Enabled = true;
                txtMarginpect.Enabled = true;
            }
            else
            {
                txtCost.Enabled = false;
                cmbTestLocation.Enabled = false;
                txtMarginpect.Enabled = false;
            }
        }

     

       
    }
}
