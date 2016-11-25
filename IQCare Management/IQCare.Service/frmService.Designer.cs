namespace IQCare.Service
{
    partial class frmService
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmService));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConfigChanges = new System.Windows.Forms.Button();
            this.txtSrvNm = new System.Windows.Forms.TextBox();
            this.btnConString = new System.Windows.Forms.Button();
            this.txtServicePath = new System.Windows.Forms.TextBox();
            this.grdServiceConfig = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.grdServiceConfig)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 9;
            this.label2.Tag = "lblLabel";
            this.label2.Text = "Service Status";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(485, 367);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(173, 23);
            this.btnExit.TabIndex = 44;
            this.btnExit.Tag = "btnH25WFlexi";
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 8;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Service Name";
            // 
            // btnConfigChanges
            // 
            this.btnConfigChanges.Location = new System.Drawing.Point(76, 367);
            this.btnConfigChanges.Name = "btnConfigChanges";
            this.btnConfigChanges.Size = new System.Drawing.Size(173, 23);
            this.btnConfigChanges.TabIndex = 43;
            this.btnConfigChanges.Tag = "btnH25WFlexi";
            this.btnConfigChanges.Text = "Save Changes";
            this.btnConfigChanges.UseVisualStyleBackColor = true;
            this.btnConfigChanges.Click += new System.EventHandler(this.btnConfigChanges_Click);
            // 
            // txtSrvNm
            // 
            this.txtSrvNm.Location = new System.Drawing.Point(125, 23);
            this.txtSrvNm.Name = "txtSrvNm";
            this.txtSrvNm.Size = new System.Drawing.Size(547, 20);
            this.txtSrvNm.TabIndex = 7;
            this.txtSrvNm.Tag = "txtTextBox";
            // 
            // btnConString
            // 
            this.btnConString.Location = new System.Drawing.Point(273, 164);
            this.btnConString.Name = "btnConString";
            this.btnConString.Size = new System.Drawing.Size(173, 23);
            this.btnConString.TabIndex = 41;
            this.btnConString.Tag = "btnH25WFlexi";
            this.btnConString.Text = "Change Connection String";
            this.btnConString.UseVisualStyleBackColor = true;
            this.btnConString.Click += new System.EventHandler(this.btnConString_Click);
            // 
            // txtServicePath
            // 
            this.txtServicePath.Location = new System.Drawing.Point(125, 75);
            this.txtServicePath.Name = "txtServicePath";
            this.txtServicePath.Size = new System.Drawing.Size(547, 20);
            this.txtServicePath.TabIndex = 10;
            this.txtServicePath.Tag = "txtTextBox";
            // 
            // grdServiceConfig
            // 
            this.grdServiceConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdServiceConfig.DefaultCellStyle = dataGridViewCellStyle1;
            this.grdServiceConfig.Location = new System.Drawing.Point(28, 199);
            this.grdServiceConfig.Name = "grdServiceConfig";
            this.grdServiceConfig.ReadOnly = true;
            this.grdServiceConfig.Size = new System.Drawing.Size(713, 150);
            this.grdServiceConfig.TabIndex = 42;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 11;
            this.label3.Tag = "lblLabel";
            this.label3.Text = "Service Path";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(76, 164);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(173, 23);
            this.btnStop.TabIndex = 40;
            this.btnStop.Tag = "btnH25WFlexi";
            this.btnStop.Text = "Stop Service";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(485, 164);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(173, 23);
            this.btnStart.TabIndex = 39;
            this.btnStart.Tag = "btnH25WFlexi";
            this.btnStart.Text = "Start Service";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(125, 49);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(547, 20);
            this.txtStatus.TabIndex = 6;
            this.txtStatus.Tag = "txtTextBox";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtServicePath);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtSrvNm);
            this.panel2.Controls.Add(this.txtStatus);
            this.panel2.Location = new System.Drawing.Point(28, 22);
            this.panel2.Margin = new System.Windows.Forms.Padding(1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(713, 129);
            this.panel2.TabIndex = 38;
            this.panel2.Tag = "pnlPanel";
            // 
            // frmService
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 402);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnConfigChanges);
            this.Controls.Add(this.btnConString);
            this.Controls.Add(this.grdServiceConfig);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmService";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Service";
            this.Load += new System.EventHandler(this.frmService_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdServiceConfig)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConfigChanges;
        private System.Windows.Forms.TextBox txtSrvNm;
        private System.Windows.Forms.Button btnConString;
        private System.Windows.Forms.TextBox txtServicePath;
        private System.Windows.Forms.DataGridView grdServiceConfig;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Panel panel2;


    }
}