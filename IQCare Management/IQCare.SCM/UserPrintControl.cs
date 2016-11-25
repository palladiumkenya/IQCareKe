using Application.Common;
using Application.Presentation;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IQCare.SCM
{
    public class UserPrintControl : UserControl
    {
        private IContainer components;
        private Label lblexpire1;
        private Panel pnlprint;
        private Label lblfacility;
        private Label lblPatientName;
        private TextBox txtinstruction;
        private Label lbldrgquantity;
        private CheckBox chkDrugName;
        private ComboBox cmbnocopies;
        private Label lblnoofcopies;
        private Label lblStore;

        public Panel print
        {
            get
            {
                return this.pnlprint;
            }
            set
            {
                this.pnlprint = value;
            }
        }

        public Label Facility
        {
            get
            {
                return this.lblfacility;
            }
            set
            {
                this.lblfacility = value;
            }
        }

        public Label Store
        {
            get
            {
                return this.lblStore;
            }
            set
            {
                this.lblStore = value;
            }
        }

        public Label PatientName
        {
            get
            {
                return this.lblPatientName;
            }
            set
            {
                this.lblPatientName = value;
            }
        }

        public CheckBox chkDName
        {
            get
            {
                return this.chkDrugName;
            }
            set
            {
                this.chkDrugName = value;
            }
        }

        public Label lbldrugquantity
        {
            get
            {
                return this.lbldrgquantity;
            }
            set
            {
                this.lbldrgquantity = value;
            }
        }

        public Label lblExpiry
        {
            get
            {
                return this.lblexpire1;
            }
            set
            {
                this.lblexpire1 = value;
            }
        }

        public TextBox txtInstruction
        {
            get
            {
                return this.txtinstruction;
            }
            set
            {
                this.txtinstruction = value;
            }
        }

        public UserPrintControl()
        {
            this.InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblexpire1 = new Label();
            this.pnlprint = new Panel();
            this.lblfacility = new Label();
            this.lblPatientName = new Label();
            this.txtinstruction = new TextBox();
            this.lbldrgquantity = new Label();
            this.chkDrugName = new CheckBox();
            this.cmbnocopies = new ComboBox();
            this.lblnoofcopies = new Label();
            this.lblStore = new Label();
            this.pnlprint.SuspendLayout();
            this.SuspendLayout();
            this.lblexpire1.AutoSize = true;
            this.lblexpire1.Location = new Point(110, 68);
            this.lblexpire1.Name = "lblexpire1";
            this.lblexpire1.Size = new Size(0, 16);
            this.lblexpire1.TabIndex = 7;
            this.pnlprint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlprint.Controls.Add((Control)this.lblStore);
            this.pnlprint.Controls.Add((Control)this.lblfacility);
            this.pnlprint.Controls.Add((Control)this.lblPatientName);
            this.pnlprint.Controls.Add((Control)this.txtinstruction);
            this.pnlprint.Controls.Add((Control)this.lbldrgquantity);
            this.pnlprint.Controls.Add((Control)this.chkDrugName);
            this.pnlprint.Location = new Point(-1, -1);
            this.pnlprint.Name = "pnlprint";
            this.pnlprint.Size = new Size(286, 142);
            this.pnlprint.TabIndex = 12;
            this.lblfacility.AutoSize = true;
            this.lblfacility.Font = new Font("Arial Narrow", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.lblfacility.Location = new Point(8, 1);
            this.lblfacility.Name = "lblfacility";
            this.lblfacility.Size = new Size(60, 16);
            this.lblfacility.TabIndex = 15;
            this.lblfacility.Text = "facilityName";
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new Font("Arial Narrow", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.lblPatientName.Location = new Point(8, 40);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new Size(31, 16);
            this.lblPatientName.TabIndex = 14;
            this.lblPatientName.Tag = (object)"";
            this.lblPatientName.Text = "name";
            this.txtinstruction.CharacterCasing = CharacterCasing.Upper;
            this.txtinstruction.ForeColor = SystemColors.ControlText;
            this.txtinstruction.Location = new Point(8, 116);
            this.txtinstruction.Margin = new Padding(3, 4, 3, 4);
            this.txtinstruction.Name = "txtinstruction";
            this.txtinstruction.Size = new Size(273, 21);
            this.txtinstruction.TabIndex = 13;
            this.lbldrgquantity.AutoSize = true;
            this.lbldrgquantity.Font = new Font("Arial Narrow", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.lbldrgquantity.Location = new Point(8, 96);
            this.lbldrgquantity.Name = "lbldrgquantity";
            this.lbldrgquantity.Size = new Size(66, 16);
            this.lbldrgquantity.TabIndex = 12;
            this.lbldrgquantity.Text = "Drug Quantity";
            this.chkDrugName.Font = new Font("Arial Narrow", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.chkDrugName.Location = new Point(8, 60);
            this.chkDrugName.Margin = new Padding(3, 4, 3, 4);
            this.chkDrugName.Name = "chkDrugName";
            this.chkDrugName.Size = new Size(273, 38);
            this.chkDrugName.TabIndex = 11;
            this.chkDrugName.Text = "chkDrugName";
            this.chkDrugName.UseVisualStyleBackColor = true;
            this.cmbnocopies.DisplayMember = "1";
            this.cmbnocopies.FormattingEnabled = true;
            this.cmbnocopies.Items.AddRange(new object[10]
      {
        (object) "1",
        (object) "2",
        (object) "3",
        (object) "4",
        (object) "5",
        (object) "6",
        (object) "7",
        (object) "8",
        (object) "9",
        (object) "10"
      });
            this.cmbnocopies.Location = new Point(99, 148);
            this.cmbnocopies.Margin = new Padding(3, 4, 3, 4);
            this.cmbnocopies.Name = "cmbnocopies";
            this.cmbnocopies.Size = new Size(101, 24);
            this.cmbnocopies.TabIndex = 14;
            this.cmbnocopies.ValueMember = "1";
            this.lblnoofcopies.AutoSize = true;
            this.lblnoofcopies.Location = new Point(8, 149);
            this.lblnoofcopies.Name = "lblnoofcopies";
            this.lblnoofcopies.Size = new Size(85, 16);
            this.lblnoofcopies.TabIndex = 13;
            this.lblnoofcopies.Text = "Number of copies:";
            this.lblStore.AutoSize = true;
            this.lblStore.Font = new Font("Arial Narrow", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.lblStore.Location = new Point(8, 20);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new Size(56, 16);
            this.lblStore.TabIndex = 16;
            this.lblStore.Text = "StoreName";
            this.AutoScaleDimensions = new SizeF(6f, 16f);
             this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
             this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add((Control)this.cmbnocopies);
            this.Controls.Add((Control)this.lblnoofcopies);
            this.Controls.Add((Control)this.pnlprint);
            this.Controls.Add((Control)this.lblexpire1);
            this.Font = new Font("Arial Narrow", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.Margin = new Padding(3, 4, 3, 4);
            this.Name = "UserPrintControl";
            this.Size = new Size(287, 180);
            this.Tag = (object)"frmForm";
            this.Load += new EventHandler(this.UserPrintControl_Load);
            this.pnlprint.ResumeLayout(false);
            this.pnlprint.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void UserPrintControl_Load(object sender, EventArgs e)
        {
            try
            {
                new clsCssStyle().setStyle((Control)this);
                this.cmbnocopies.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MsgBuilder MessageBuilder = new MsgBuilder();
                MessageBuilder.DataElements["MessageText"] = ex.Message.ToString();
                int num = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
            }
        }
    }
}
