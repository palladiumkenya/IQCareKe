using Application.Common;
using Application.Presentation;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace IQCare.FormBuilder
{
    public partial class frmFormBusinessRule : Form
    {
       
        private Label lblset1;
        private Label lblset2;
        private Button btnsubmit;
        private Button btncancel;
        private CheckBox chkset1currentage;
        private CheckBox chkset1female;
        private CheckBox chkset1male;
        private CheckBox chkset2currentage;
        private CheckBox chkset2female;
        private CheckBox chkset2male;
        private TextBox txtset1value;
        private TextBox txtset1value1;
        private TextBox txtset2value;
        private TextBox txtset2value1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private CheckBox chkSignature;
        private RadioButton rdoMultipleVisit;
        private RadioButton rdoSingleVisit;

        public frmFormBusinessRule()
        {
            this.InitializeComponent();
        }

        

        private void frmServiceBusinessRule_Load(object sender, EventArgs e)
        {
            this.rdoSingleVisit.Checked = true;
            if (GblIQCare.blncontrolunabledesable)
                this.chkSignature.Enabled = true;
            else
                this.chkSignature.Enabled = false;
            this.BindBusinessRules();
        }

        public void BindBusinessRules()
        {
            if (GblIQCare.blnSingleVisit)
                this.rdoSingleVisit.Checked = true;
            if (GblIQCare.blnMultivisit)
                this.rdoMultipleVisit.Checked = true;
            if (GblIQCare.blnSignatureOneachpage)
                this.chkSignature.Checked = true;
            if (GblIQCare.dtformBusinessValues.Rows.Count <= 0)
                return;
            foreach (DataRow row in (InternalDataCollectionBase)GblIQCare.dtformBusinessValues.Rows)
            {
                if (row["BusRuleId"].ToString() == "16" && row["SetType"].ToString() == "1")
                {
                    this.chkset1currentage.Checked = true;
                    this.txtset1value.Text = row["Value"].ToString();
                    this.txtset1value1.Text = row["Value1"].ToString();
                }
                if (row["BusRuleId"].ToString() == "16" && row["SetType"].ToString() == "2")
                {
                    this.chkset2currentage.Checked = true;
                    this.txtset2value.Text = row["Value"].ToString();
                    this.txtset2value1.Text = row["Value1"].ToString();
                }
                if (row["BusRuleId"].ToString() == "15" && row["SetType"].ToString() == "1")
                    this.chkset1female.Checked = true;
                if (row["BusRuleId"].ToString() == "15" && row["SetType"].ToString() == "2")
                    this.chkset2female.Checked = true;
                if (row["BusRuleId"].ToString() == "14" && row["SetType"].ToString() == "1")
                    this.chkset1male.Checked = true;
                if (row["BusRuleId"].ToString() == "14" && row["SetType"].ToString() == "2")
                    this.chkset2male.Checked = true;
            }
        }

        private bool FieldValidation()
        {
            if (this.chkset1currentage.Checked && (this.txtset1value.Text == "" || this.txtset1value1.Text == ""))
            {
                IQCareWindowMsgBox.ShowWindow("EnterValue", (Control)this);
                return false;
            }
            if (this.chkset2currentage.Checked && (this.txtset2value.Text == "" || this.txtset2value1.Text == ""))
            {
                IQCareWindowMsgBox.ShowWindow("EnterValue", (Control)this);
                return false;
            }
            if (this.chkset1currentage.Checked && Convert.ToDecimal(this.txtset1value.Text) >= Convert.ToDecimal(this.txtset1value1.Text))
            {
                IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", (Control)this);
                return false;
            }
            if (!this.chkset2currentage.Checked || !(Convert.ToDecimal(this.txtset2value.Text) >= Convert.ToDecimal(this.txtset2value1.Text)))
                return true;
            IQCareWindowMsgBox.ShowWindow("EnterCorrectValue", (Control)this);
            return false;
        }

        private void btnsubmit_Click(object sender, EventArgs e)
        {
            if (!this.FieldValidation())
                return;
            GblIQCare.blnSingleVisit = this.rdoSingleVisit.Checked;
            GblIQCare.blnMultivisit = this.rdoMultipleVisit.Checked;
            GblIQCare.blnSignatureOneachpage = this.chkSignature.Checked;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ServiceAreaName", typeof(string));
            dataTable.Columns.Add("BusRuleId", typeof(string));
            dataTable.Columns.Add("Value", typeof(string));
            dataTable.Columns.Add("Value1", typeof(string));
            dataTable.Columns.Add("SetType", typeof(string));
            if (this.chkset1currentage.Checked && this.txtset1value.Text != "" && this.txtset1value1.Text != "")
            {
                DataRow row = dataTable.NewRow();
                row["ServiceAreaName"] = (object)GblIQCare.strserviceareaname;
                row["BusRuleId"] = (object)"16";
                row["Value"] = (object)this.txtset1value.Text;
                row["Value1"] = (object)this.txtset1value1.Text;
                row["SetType"] = (object)"1";
                dataTable.Rows.Add(row);
            }
            if (this.chkset2currentage.Checked && this.txtset2value.Text != "" && this.txtset2value1.Text != "")
            {
                DataRow row = dataTable.NewRow();
                row["ServiceAreaName"] = (object)GblIQCare.strserviceareaname;
                row["BusRuleId"] = (object)"16";
                row["Value"] = (object)this.txtset2value.Text;
                row["Value1"] = (object)this.txtset2value1.Text;
                row["SetType"] = (object)"2";
                dataTable.Rows.Add(row);
            }
            if (this.chkset1female.Checked)
            {
                DataRow row = dataTable.NewRow();
                row["ServiceAreaName"] = (object)GblIQCare.strserviceareaname;
                row["BusRuleId"] = (object)"15";
                row["SetType"] = (object)"1";
                dataTable.Rows.Add(row);
            }
            if (this.chkset2female.Checked)
            {
                DataRow row = dataTable.NewRow();
                row["ServiceAreaName"] = (object)GblIQCare.strserviceareaname;
                row["BusRuleId"] = (object)"15";
                row["SetType"] = (object)"2";
                dataTable.Rows.Add(row);
            }
            if (this.chkset1male.Checked)
            {
                DataRow row = dataTable.NewRow();
                row["ServiceAreaName"] = (object)GblIQCare.strserviceareaname;
                row["BusRuleId"] = (object)"14";
                row["SetType"] = (object)"1";
                dataTable.Rows.Add(row);
            }
            if (this.chkset2male.Checked)
            {
                DataRow row = dataTable.NewRow();
                row["ServiceAreaName"] = (object)GblIQCare.strserviceareaname;
                row["BusRuleId"] = (object)"14";
                row["SetType"] = (object)"2";
                dataTable.Rows.Add(row);
            }
            GblIQCare.dtformBusinessValues = dataTable;
            this.Close();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtset1value_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.txtset1value.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", (Control)this);
                }
                else
                {
                    if ((double)float.Parse(this.txtset1value.Text) >= 0.0)
                        return;
                    e.Cancel = true;
                    IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", (Control)this);
                }
            }
            catch (Exception ex)
            {
                MsgBuilder MessageBuilder = new MsgBuilder();
                MessageBuilder.DataElements["MessageText"] = ex.Message.ToString();
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
            }
        }

        private void txtset2value_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.txtset2value.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", (Control)this);
                }
                else
                {
                    if ((double)float.Parse(this.txtset2value.Text) >= 0.0)
                        return;
                    e.Cancel = true;
                    IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", (Control)this);
                }
            }
            catch (Exception ex)
            {
                MsgBuilder MessageBuilder = new MsgBuilder();
                MessageBuilder.DataElements["MessageText"] = ex.Message.ToString();
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
            }
        }

        private void txtset1value1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.txtset1value1.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", (Control)this);
                }
                else
                {
                    if ((double)float.Parse(this.txtset1value1.Text) >= 1.0)
                        return;
                    e.Cancel = true;
                    IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", (Control)this);
                }
            }
            catch (Exception ex)
            {
                MsgBuilder MessageBuilder = new MsgBuilder();
                MessageBuilder.DataElements["MessageText"] = ex.Message.ToString();
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
            }
        }

        private void txtset2value1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (this.txtset2value1.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("EnterValue", (Control)this);
                }
                else
                {
                    if ((double)float.Parse(this.txtset2value1.Text) >= 1.0)
                        return;
                    e.Cancel = true;
                    IQCareWindowMsgBox.ShowWindow("PMTCTNotValid", (Control)this);
                }
            }
            catch (Exception ex)
            {
                MsgBuilder MessageBuilder = new MsgBuilder();
                MessageBuilder.DataElements["MessageText"] = ex.Message.ToString();
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
            }
        }

        private void frmFormBusinessRule_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;
            e.Cancel = true;
        }
    }
}
