using System.Windows.Forms;
using System.ServiceProcess;
using System.Drawing;
using System.ComponentModel;
using System;
namespace IQServiceConfig
{
    partial class ServiceConfig
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ServiceConfig));
            this.timer1 = new Timer(this.components);
            this.label2 = new Label();
            this.btnExit = new Button();
            this.label1 = new Label();
            this.btnConfigChanges = new Button();
            this.txtSrvNm = new TextBox();
            this.btnConString = new Button();
            this.txtServicePath = new TextBox();
            this.grdServiceConfig = new DataGridView();
            this.label3 = new Label();
            this.btnStop = new Button();
            this.btnStart = new Button();
            this.txtStatus = new TextBox();
            this.panel2 = new Panel();
            ((ISupportInitialize)this.grdServiceConfig).BeginInit();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x17, 0x34);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x4c, 13);
            this.label2.TabIndex = 9;
            this.label2.Tag = "lblLabel";
            this.label2.Text = "Service Status";
            this.btnExit.Location = new Point(0x1e5, 0x16f);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new Size(0xad, 0x17);
            this.btnExit.TabIndex = 0x2c;
            this.btnExit.Tag = "btnH25WFlexi";
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new EventHandler(this.btnExit_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x17, 0x1a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x4a, 13);
            this.label1.TabIndex = 8;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Service Name";
            this.btnConfigChanges.Location = new Point(0x4c, 0x16f);
            this.btnConfigChanges.Name = "btnConfigChanges";
            this.btnConfigChanges.Size = new Size(0xad, 0x17);
            this.btnConfigChanges.TabIndex = 0x2b;
            this.btnConfigChanges.Tag = "btnH25WFlexi";
            this.btnConfigChanges.Text = "Save Changes";
            this.btnConfigChanges.UseVisualStyleBackColor = true;
            this.btnConfigChanges.Click += new EventHandler(this.btnConfigChanges_Click);
            this.txtSrvNm.Location = new Point(0x7d, 0x17);
            this.txtSrvNm.Name = "txtSrvNm";
            this.txtSrvNm.Size = new Size(0x223, 20);
            this.txtSrvNm.TabIndex = 7;
            this.txtSrvNm.Tag = "txtTextBox";
            this.btnConString.Location = new Point(0x111, 0xa4);
            this.btnConString.Name = "btnConString";
            this.btnConString.Size = new Size(0xad, 0x17);
            this.btnConString.TabIndex = 0x29;
            this.btnConString.Tag = "btnH25WFlexi";
            this.btnConString.Text = "Change Connection String";
            this.btnConString.UseVisualStyleBackColor = true;
            this.btnConString.Click += new EventHandler(this.btnConString_Click);
            this.txtServicePath.Location = new Point(0x7d, 0x4b);
            this.txtServicePath.Name = "txtServicePath";
            this.txtServicePath.Size = new Size(0x223, 20);
            this.txtServicePath.TabIndex = 10;
            this.txtServicePath.Tag = "txtTextBox";
            this.grdServiceConfig.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Arial", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            this.grdServiceConfig.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdServiceConfig.Location = new Point(0x1c, 0xc7);
            this.grdServiceConfig.Name = "grdServiceConfig";
            this.grdServiceConfig.ReadOnly = true;
            this.grdServiceConfig.Size = new Size(0x2c9, 150);
            this.grdServiceConfig.TabIndex = 0x2a;
            this.label3.AutoSize = true;
            this.label3.Location = new Point(0x17, 0x4b);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x44, 13);
            this.label3.TabIndex = 11;
            this.label3.Tag = "lblLabel";
            this.label3.Text = "Service Path";
            this.btnStop.Location = new Point(0x4c, 0xa4);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new Size(0xad, 0x17);
            this.btnStop.TabIndex = 40;
            this.btnStop.Tag = "btnH25WFlexi";
            this.btnStop.Text = "Stop Service";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new EventHandler(this.btnStop_Click);
            this.btnStart.Location = new Point(0x1e5, 0xa4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new Size(0xad, 0x17);
            this.btnStart.TabIndex = 0x27;
            this.btnStart.Tag = "btnH25WFlexi";
            this.btnStart.Text = "Start Service";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new EventHandler(this.btnStart_Click);
            this.txtStatus.Location = new Point(0x7d, 0x31);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new Size(0x223, 20);
            this.txtStatus.TabIndex = 6;
            this.txtStatus.Tag = "txtTextBox";
            this.panel2.BorderStyle = BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtServicePath);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtSrvNm);
            this.panel2.Controls.Add(this.txtStatus);
            this.panel2.Location = new Point(0x1c, 0x16);
            this.panel2.Margin = new Padding(1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x2c9, 0x81);
            this.panel2.TabIndex = 0x26;
            this.panel2.Tag = "pnlPanel";
            // base.AutoScaleDimensions = new SizeF(6f, 13f);
            // base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x327, 0x192);
            base.Controls.Add(this.btnExit);
            base.Controls.Add(this.btnConfigChanges);
            base.Controls.Add(this.btnConString);
            base.Controls.Add(this.grdServiceConfig);
            base.Controls.Add(this.btnStop);
            base.Controls.Add(this.btnStart);
            base.Controls.Add(this.panel2);
            // base.FormBorderStyle = FormBorderStyle.Fixed3D;
            base.Icon = (Icon)resources.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ServiceConfig";
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.Manual;
            base.Tag = "ServiceConfigForm";
            this.Text = "IQ Care Service Configuration";
            base.Load += new EventHandler(this.ServiceConfig_Load);
            ((ISupportInitialize)this.grdServiceConfig).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            base.ResumeLayout(false);


        }

        #endregion


        private Button btnConfigChanges;
        private Button btnConString;
        private Button btnExit;
        private Button btnStart;
        private Button btnStop;

        private DataGridView grdServiceConfig;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel2;
        private ServiceController theControl = new ServiceController();
        private Timer timer1;
        private TextBox txtServicePath;
        private TextBox txtSrvNm;
        private TextBox txtStatus;

    }
}

