namespace IQCare.Service
{
    partial class frmDataUpsizing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDataUpsizing));
            this.fopen = new System.Windows.Forms.OpenFileDialog();
            this.pgbar1 = new System.Windows.Forms.ProgressBar();
            this.btnUpsize = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAccessUid = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbtCTC = new System.Windows.Forms.RadioButton();
            this.rbtCareWare = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSqlServer = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSqlPwd = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtSqlUid = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSqlDbName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAccessPwd = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnfopen = new System.Windows.Forms.Button();
            this.txtAccessFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // fopen
            // 
            this.fopen.Filter = "Access DataBase|*.mdb";
            // 
            // pgbar1
            // 
            this.pgbar1.Location = new System.Drawing.Point(2, 393);
            this.pgbar1.Name = "pgbar1";
            this.pgbar1.Size = new System.Drawing.Size(668, 22);
            this.pgbar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgbar1.TabIndex = 11;
            // 
            // btnUpsize
            // 
            this.btnUpsize.BackColor = System.Drawing.SystemColors.Control;
            this.btnUpsize.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpsize.Location = new System.Drawing.Point(229, 364);
            this.btnUpsize.Name = "btnUpsize";
            this.btnUpsize.Size = new System.Drawing.Size(74, 23);
            this.btnUpsize.TabIndex = 10;
            this.btnUpsize.Tag = "btnSingleText";
            this.btnUpsize.Text = "Upsize";
            this.btnUpsize.UseVisualStyleBackColor = false;
            this.btnUpsize.Click += new System.EventHandler(this.btnUpsize_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.txtAccessUid);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.txtAccessPwd);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnfopen);
            this.groupBox1.Controls.Add(this.txtAccessFile);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(649, 348);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "grpGroupBox";
            this.groupBox1.Text = "DataBase Upsizing";
            // 
            // txtAccessUid
            // 
            this.txtAccessUid.Location = new System.Drawing.Point(102, 56);
            this.txtAccessUid.Name = "txtAccessUid";
            this.txtAccessUid.PasswordChar = '*';
            this.txtAccessUid.Size = new System.Drawing.Size(477, 20);
            this.txtAccessUid.TabIndex = 20;
            this.txtAccessUid.Tag = "txtTextBox";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbtCTC);
            this.groupBox3.Controls.Add(this.rbtCareWare);
            this.groupBox3.Location = new System.Drawing.Point(23, 124);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(596, 35);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Tag = "grpGroupBox";
            // 
            // rbtCTC
            // 
            this.rbtCTC.AutoSize = true;
            this.rbtCTC.Location = new System.Drawing.Point(113, 12);
            this.rbtCTC.Name = "rbtCTC";
            this.rbtCTC.Size = new System.Drawing.Size(61, 17);
            this.rbtCTC.TabIndex = 1;
            this.rbtCTC.TabStop = true;
            this.rbtCTC.Tag = "rdoButton";
            this.rbtCTC.Text = "CTC - 2";
            this.rbtCTC.UseVisualStyleBackColor = true;
            // 
            // rbtCareWare
            // 
            this.rbtCareWare.AutoSize = true;
            this.rbtCareWare.Location = new System.Drawing.Point(13, 12);
            this.rbtCareWare.Name = "rbtCareWare";
            this.rbtCareWare.Size = new System.Drawing.Size(73, 17);
            this.rbtCareWare.TabIndex = 0;
            this.rbtCareWare.TabStop = true;
            this.rbtCareWare.Tag = "rdoButton";
            this.rbtCareWare.Text = "CareWare";
            this.rbtCareWare.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSqlServer);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtSqlPwd);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtSqlUid);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtSqlDbName);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(20, 174);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(599, 126);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Tag = "grpGroupBox";
            this.groupBox2.Text = "Upsized SQL DataBase";
            // 
            // txtSqlServer
            // 
            this.txtSqlServer.Location = new System.Drawing.Point(101, 19);
            this.txtSqlServer.Name = "txtSqlServer";
            this.txtSqlServer.Size = new System.Drawing.Size(458, 20);
            this.txtSqlServer.TabIndex = 0;
            this.txtSqlServer.Tag = "txtTextBox";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 13);
            this.label10.TabIndex = 13;
            this.label10.Tag = "lblLabel";
            this.label10.Text = "Server Name";
            // 
            // txtSqlPwd
            // 
            this.txtSqlPwd.Location = new System.Drawing.Point(101, 96);
            this.txtSqlPwd.Name = "txtSqlPwd";
            this.txtSqlPwd.PasswordChar = '*';
            this.txtSqlPwd.Size = new System.Drawing.Size(458, 20);
            this.txtSqlPwd.TabIndex = 3;
            this.txtSqlPwd.Tag = "txtTextBox";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 11;
            this.label9.Tag = "lblLabel";
            this.label9.Text = "Password";
            // 
            // txtSqlUid
            // 
            this.txtSqlUid.Location = new System.Drawing.Point(101, 70);
            this.txtSqlUid.Name = "txtSqlUid";
            this.txtSqlUid.Size = new System.Drawing.Size(458, 20);
            this.txtSqlUid.TabIndex = 2;
            this.txtSqlUid.Tag = "txtTextBox";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 13);
            this.label8.TabIndex = 9;
            this.label8.Tag = "lblLabel";
            this.label8.Text = "User Name";
            // 
            // txtSqlDbName
            // 
            this.txtSqlDbName.Location = new System.Drawing.Point(101, 44);
            this.txtSqlDbName.Name = "txtSqlDbName";
            this.txtSqlDbName.Size = new System.Drawing.Size(458, 20);
            this.txtSqlDbName.TabIndex = 1;
            this.txtSqlDbName.Tag = "txtTextBox";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 13);
            this.label7.TabIndex = 7;
            this.label7.Tag = "lblLabel";
            this.label7.Text = "DataBase Name";
            // 
            // txtAccessPwd
            // 
            this.txtAccessPwd.Location = new System.Drawing.Point(102, 82);
            this.txtAccessPwd.Name = "txtAccessPwd";
            this.txtAccessPwd.PasswordChar = '*';
            this.txtAccessPwd.Size = new System.Drawing.Size(477, 20);
            this.txtAccessPwd.TabIndex = 2;
            this.txtAccessPwd.Tag = "txtTextBox";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 85);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 15;
            this.label6.Tag = "lblLabel";
            this.label6.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 13;
            this.label5.Tag = "lblLabel";
            this.label5.Text = "User Name";
            // 
            // btnfopen
            // 
            this.btnfopen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnfopen.Location = new System.Drawing.Point(590, 28);
            this.btnfopen.Name = "btnfopen";
            this.btnfopen.Size = new System.Drawing.Size(29, 23);
            this.btnfopen.TabIndex = 12;
            this.btnfopen.Tag = "btnFlexible";
            this.btnfopen.Text = "...";
            this.btnfopen.UseVisualStyleBackColor = true;
            this.btnfopen.Click += new System.EventHandler(this.btnfopen_Click);
            // 
            // txtAccessFile
            // 
            this.txtAccessFile.BackColor = System.Drawing.SystemColors.Control;
            this.txtAccessFile.Location = new System.Drawing.Point(102, 30);
            this.txtAccessFile.Name = "txtAccessFile";
            this.txtAccessFile.ReadOnly = true;
            this.txtAccessFile.Size = new System.Drawing.Size(477, 20);
            this.txtAccessFile.TabIndex = 0;
            this.txtAccessFile.Tag = "txtGreyTextBoxCss";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 10;
            this.label4.Tag = "lblLabel";
            this.label4.Text = "DataBase File";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.SystemColors.Control;
            this.btnExit.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(309, 364);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(83, 23);
            this.btnExit.TabIndex = 12;
            this.btnExit.Tag = "btnSingleText";
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmDataUpsizing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(673, 444);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.pgbar1);
            this.Controls.Add(this.btnUpsize);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDataUpsizing";
            this.Tag = "frmForm";
            this.Text = "Data Upsizing";
            this.Load += new System.EventHandler(this.frmDataUpsizing_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog fopen;
        private System.Windows.Forms.ProgressBar pgbar1;
        private System.Windows.Forms.Button btnUpsize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtAccessUid;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbtCTC;
        private System.Windows.Forms.RadioButton rbtCareWare;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSqlServer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSqlPwd;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSqlUid;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSqlDbName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAccessPwd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnfopen;
        private System.Windows.Forms.TextBox txtAccessFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnExit;
    }
}