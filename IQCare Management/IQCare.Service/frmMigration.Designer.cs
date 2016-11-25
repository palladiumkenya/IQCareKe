namespace IQCare.Service
{
    partial class frmMigration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMigration));
            this.btnMigrate = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pgbar = new System.Windows.Forms.ProgressBar();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.dgmigration = new System.Windows.Forms.DataGridView();
            this.txtnewdatabase = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtdatabase = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtuserid = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtserver = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgmigration)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMigrate
            // 
            this.btnMigrate.Location = new System.Drawing.Point(484, 294);
            this.btnMigrate.Name = "btnMigrate";
            this.btnMigrate.Size = new System.Drawing.Size(80, 23);
            this.btnMigrate.TabIndex = 39;
            this.btnMigrate.Tag = "btnSingleText";
            this.btnMigrate.Text = "Migrate";
            this.btnMigrate.UseVisualStyleBackColor = true;
            this.btnMigrate.Click += new System.EventHandler(this.btnMigrate_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnMigrate);
            this.panel2.Controls.Add(this.pgbar);
            this.panel2.Controls.Add(this.btncancel);
            this.panel2.Controls.Add(this.btnOk);
            this.panel2.Controls.Add(this.dgmigration);
            this.panel2.Controls.Add(this.txtnewdatabase);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.txtdatabase);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.txtpassword);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.txtuserid);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.txtserver);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Location = new System.Drawing.Point(12, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(745, 350);
            this.panel2.TabIndex = 19;
            this.panel2.Tag = "pnlPanel";
            // 
            // pgbar
            // 
            this.pgbar.Location = new System.Drawing.Point(6, 323);
            this.pgbar.Name = "pgbar";
            this.pgbar.Size = new System.Drawing.Size(721, 17);
            this.pgbar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pgbar.TabIndex = 38;
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(143, 217);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(79, 23);
            this.btncancel.TabIndex = 37;
            this.btncancel.Tag = "btnSingleText";
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(62, 217);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 36;
            this.btnOk.Tag = "btnSingleText";
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // dgmigration
            // 
            this.dgmigration.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgmigration.Location = new System.Drawing.Point(293, -4);
            this.dgmigration.Name = "dgmigration";
            this.dgmigration.ReadOnly = true;
            this.dgmigration.Size = new System.Drawing.Size(445, 292);
            this.dgmigration.TabIndex = 35;
            // 
            // txtnewdatabase
            // 
            this.txtnewdatabase.Location = new System.Drawing.Point(116, 175);
            this.txtnewdatabase.Name = "txtnewdatabase";
            this.txtnewdatabase.Size = new System.Drawing.Size(171, 20);
            this.txtnewdatabase.TabIndex = 34;
            this.txtnewdatabase.Tag = "txtTextBox";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 178);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 33;
            this.label11.Tag = "lblLabel";
            this.label11.Text = "New Database";
            // 
            // txtdatabase
            // 
            this.txtdatabase.Location = new System.Drawing.Point(115, 149);
            this.txtdatabase.Name = "txtdatabase";
            this.txtdatabase.Size = new System.Drawing.Size(171, 20);
            this.txtdatabase.TabIndex = 32;
            this.txtdatabase.Tag = "txtTextBox";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 152);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 13);
            this.label12.TabIndex = 31;
            this.label12.Tag = "lblLabel";
            this.label12.Text = "DataBase Name";
            // 
            // txtpassword
            // 
            this.txtpassword.Location = new System.Drawing.Point(115, 123);
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.Size = new System.Drawing.Size(171, 20);
            this.txtpassword.TabIndex = 30;
            this.txtpassword.Tag = "txtTextBox";
            this.txtpassword.UseSystemPasswordChar = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(2, 126);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 13);
            this.label13.TabIndex = 29;
            this.label13.Tag = "lblLabel";
            this.label13.Text = "Password";
            // 
            // txtuserid
            // 
            this.txtuserid.Location = new System.Drawing.Point(116, 97);
            this.txtuserid.Name = "txtuserid";
            this.txtuserid.Size = new System.Drawing.Size(171, 20);
            this.txtuserid.TabIndex = 28;
            this.txtuserid.Tag = "txtTextBox";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(3, 100);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 13);
            this.label14.TabIndex = 27;
            this.label14.Tag = "lblLabel";
            this.label14.Text = "User Id";
            // 
            // txtserver
            // 
            this.txtserver.Location = new System.Drawing.Point(116, 71);
            this.txtserver.Name = "txtserver";
            this.txtserver.Size = new System.Drawing.Size(171, 20);
            this.txtserver.TabIndex = 26;
            this.txtserver.Tag = "txtTextBox";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(3, 74);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 13);
            this.label15.TabIndex = 25;
            this.label15.Tag = "lblLabel";
            this.label15.Text = "Server Name";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(350, 364);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(78, 23);
            this.btnExit.TabIndex = 20;
            this.btnExit.Tag = "btnSingleText";
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmMigration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 395);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMigration";
            this.Tag = "frmForm";
            this.Text = "Migration";
            this.Load += new System.EventHandler(this.frmMigration_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgmigration)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnMigrate;
        private System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.ProgressBar pgbar;
        internal System.Windows.Forms.Button btncancel;
        internal System.Windows.Forms.Button btnOk;
        internal System.Windows.Forms.DataGridView dgmigration;
        internal System.Windows.Forms.TextBox txtnewdatabase;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.TextBox txtdatabase;
        internal System.Windows.Forms.Label label12;
        internal System.Windows.Forms.TextBox txtpassword;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.TextBox txtuserid;
        internal System.Windows.Forms.Label label14;
        internal System.Windows.Forms.TextBox txtserver;
        internal System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnExit;



    }
}