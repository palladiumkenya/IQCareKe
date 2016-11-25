namespace IQCare.FormBuilder
{
    partial class frmSCM_PharmacyMasterImportExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSCM_PharmacyMasterImportExport));
            this.btnexport = new System.Windows.Forms.Button();
            this.grpimport = new System.Windows.Forms.GroupBox();
            this.lblfilename = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.txtfile = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblmessage = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chk16 = new System.Windows.Forms.CheckBox();
            this.chk15 = new System.Windows.Forms.CheckBox();
            this.chk14 = new System.Windows.Forms.CheckBox();
            this.chk13 = new System.Windows.Forms.CheckBox();
            this.chk12 = new System.Windows.Forms.CheckBox();
            this.chk11 = new System.Windows.Forms.CheckBox();
            this.chk10 = new System.Windows.Forms.CheckBox();
            this.chk9 = new System.Windows.Forms.CheckBox();
            this.chk8 = new System.Windows.Forms.CheckBox();
            this.chk7 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chk6 = new System.Windows.Forms.CheckBox();
            this.chk5 = new System.Windows.Forms.CheckBox();
            this.chk4 = new System.Windows.Forms.CheckBox();
            this.chk3 = new System.Windows.Forms.CheckBox();
            this.chk2 = new System.Windows.Forms.CheckBox();
            this.chk1 = new System.Windows.Forms.CheckBox();
            this.grpimport.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnexport
            // 
            this.btnexport.Location = new System.Drawing.Point(340, 464);
            this.btnexport.Name = "btnexport";
            this.btnexport.Size = new System.Drawing.Size(75, 23);
            this.btnexport.TabIndex = 0;
            this.btnexport.Text = "Export";
            this.btnexport.UseVisualStyleBackColor = true;
            this.btnexport.Click += new System.EventHandler(this.btnexport_Click);
            // 
            // grpimport
            // 
            this.grpimport.Controls.Add(this.lblfilename);
            this.grpimport.Controls.Add(this.btnBrowse);
            this.grpimport.Controls.Add(this.btnImport);
            this.grpimport.Controls.Add(this.txtfile);
            this.grpimport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpimport.Location = new System.Drawing.Point(12, 12);
            this.grpimport.Name = "grpimport";
            this.grpimport.Size = new System.Drawing.Size(790, 70);
            this.grpimport.TabIndex = 18;
            this.grpimport.TabStop = false;
            this.grpimport.Text = "Import";
            // 
            // lblfilename
            // 
            this.lblfilename.AutoSize = true;
            this.lblfilename.Location = new System.Drawing.Point(93, 35);
            this.lblfilename.Name = "lblfilename";
            this.lblfilename.Size = new System.Drawing.Size(54, 13);
            this.lblfilename.TabIndex = 4;
            this.lblfilename.Text = "File Name";
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowse.BackgroundImage")));
            this.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBrowse.Location = new System.Drawing.Point(495, 32);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(31, 20);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Tag = "";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click_1);
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(546, 32);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 2;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // txtfile
            // 
            this.txtfile.BackColor = System.Drawing.Color.White;
            this.txtfile.Location = new System.Drawing.Point(178, 32);
            this.txtfile.Name = "txtfile";
            this.txtfile.ReadOnly = true;
            this.txtfile.Size = new System.Drawing.Size(311, 20);
            this.txtfile.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblmessage
            // 
            this.lblmessage.AutoSize = true;
            this.lblmessage.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmessage.Location = new System.Drawing.Point(264, 445);
            this.lblmessage.Name = "lblmessage";
            this.lblmessage.Size = new System.Drawing.Size(107, 16);
            this.lblmessage.TabIndex = 19;
            this.lblmessage.Text = "Save Location";
            // 
            // btnclose
            // 
            this.btnclose.Location = new System.Drawing.Point(426, 464);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(75, 23);
            this.btnclose.TabIndex = 20;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(790, 354);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Export";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chk16);
            this.groupBox3.Controls.Add(this.chk15);
            this.groupBox3.Controls.Add(this.chk14);
            this.groupBox3.Controls.Add(this.chk13);
            this.groupBox3.Controls.Add(this.chk12);
            this.groupBox3.Controls.Add(this.chk11);
            this.groupBox3.Controls.Add(this.chk10);
            this.groupBox3.Controls.Add(this.chk9);
            this.groupBox3.Controls.Add(this.chk8);
            this.groupBox3.Controls.Add(this.chk7);
            this.groupBox3.Location = new System.Drawing.Point(361, 38);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(336, 297);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Other SCM master lists";
            // 
            // chk16
            // 
            this.chk16.AutoSize = true;
            this.chk16.Location = new System.Drawing.Point(7, 247);
            this.chk16.Name = "chk16";
            this.chk16.Size = new System.Drawing.Size(104, 17);
            this.chk16.TabIndex = 9;
            this.chk16.Text = "Lab test location";
            this.chk16.UseVisualStyleBackColor = true;
            // 
            // chk15
            // 
            this.chk15.AutoSize = true;
            this.chk15.Location = new System.Drawing.Point(7, 223);
            this.chk15.Name = "chk15";
            this.chk15.Size = new System.Drawing.Size(93, 17);
            this.chk15.TabIndex = 8;
            this.chk15.Text = "Return reason";
            this.chk15.UseVisualStyleBackColor = true;
            // 
            // chk14
            // 
            this.chk14.AutoSize = true;
            this.chk14.Location = new System.Drawing.Point(7, 199);
            this.chk14.Name = "chk14";
            this.chk14.Size = new System.Drawing.Size(113, 17);
            this.chk14.TabIndex = 7;
            this.chk14.Text = "Adjustment reason";
            this.chk14.UseVisualStyleBackColor = true;
            // 
            // chk13
            // 
            this.chk13.AutoSize = true;
            this.chk13.Location = new System.Drawing.Point(7, 175);
            this.chk13.Name = "chk13";
            this.chk13.Size = new System.Drawing.Size(64, 17);
            this.chk13.TabIndex = 6;
            this.chk13.Text = "Supplier";
            this.chk13.UseVisualStyleBackColor = true;
            // 
            // chk12
            // 
            this.chk12.AutoSize = true;
            this.chk12.Location = new System.Drawing.Point(7, 151);
            this.chk12.Name = "chk12";
            this.chk12.Size = new System.Drawing.Size(173, 17);
            this.chk12.TabIndex = 5;
            this.chk12.Text = "Store source destination linking";
            this.chk12.UseVisualStyleBackColor = true;
            // 
            // chk11
            // 
            this.chk11.AutoSize = true;
            this.chk11.Location = new System.Drawing.Point(7, 127);
            this.chk11.Name = "chk11";
            this.chk11.Size = new System.Drawing.Size(79, 17);
            this.chk11.TabIndex = 4;
            this.chk11.Text = "Store detail";
            this.chk11.UseVisualStyleBackColor = true;
            // 
            // chk10
            // 
            this.chk10.AutoSize = true;
            this.chk10.Location = new System.Drawing.Point(7, 103);
            this.chk10.Name = "chk10";
            this.chk10.Size = new System.Drawing.Size(139, 17);
            this.chk10.TabIndex = 3;
            this.chk10.Text = "Cost allocation category";
            this.chk10.UseVisualStyleBackColor = true;
            // 
            // chk9
            // 
            this.chk9.AutoSize = true;
            this.chk9.Location = new System.Drawing.Point(7, 79);
            this.chk9.Name = "chk9";
            this.chk9.Size = new System.Drawing.Size(128, 17);
            this.chk9.TabIndex = 2;
            this.chk9.Text = "Program donor linking";
            this.chk9.UseVisualStyleBackColor = true;
            // 
            // chk8
            // 
            this.chk8.AutoSize = true;
            this.chk8.Location = new System.Drawing.Point(7, 55);
            this.chk8.Name = "chk8";
            this.chk8.Size = new System.Drawing.Size(55, 17);
            this.chk8.TabIndex = 1;
            this.chk8.Text = "Donor";
            this.chk8.UseVisualStyleBackColor = true;
            // 
            // chk7
            // 
            this.chk7.AutoSize = true;
            this.chk7.Location = new System.Drawing.Point(7, 31);
            this.chk7.Name = "chk7";
            this.chk7.Size = new System.Drawing.Size(65, 17);
            this.chk7.TabIndex = 0;
            this.chk7.Text = "Program";
            this.chk7.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chk6);
            this.groupBox2.Controls.Add(this.chk5);
            this.groupBox2.Controls.Add(this.chk4);
            this.groupBox2.Controls.Add(this.chk3);
            this.groupBox2.Controls.Add(this.chk2);
            this.groupBox2.Controls.Add(this.chk1);
            this.groupBox2.Location = new System.Drawing.Point(16, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 297);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pharmacy and item related master lists and linking";
            // 
            // chk6
            // 
            this.chk6.AutoSize = true;
            this.chk6.Location = new System.Drawing.Point(7, 151);
            this.chk6.Name = "chk6";
            this.chk6.Size = new System.Drawing.Size(119, 17);
            this.chk6.TabIndex = 5;
            this.chk6.Text = "Supplier item linking";
            this.chk6.UseVisualStyleBackColor = true;
            // 
            // chk5
            // 
            this.chk5.AutoSize = true;
            this.chk5.Location = new System.Drawing.Point(7, 127);
            this.chk5.Name = "chk5";
            this.chk5.Size = new System.Drawing.Size(106, 17);
            this.chk5.TabIndex = 4;
            this.chk5.Text = "Store item linking";
            this.chk5.UseVisualStyleBackColor = true;
            // 
            // chk4
            // 
            this.chk4.AutoSize = true;
            this.chk4.Location = new System.Drawing.Point(7, 103);
            this.chk4.Name = "chk4";
            this.chk4.Size = new System.Drawing.Size(120, 17);
            this.chk4.TabIndex = 3;
            this.chk4.Text = "Program item linking";
            this.chk4.UseVisualStyleBackColor = true;
            // 
            // chk3
            // 
            this.chk3.AutoSize = true;
            this.chk3.Location = new System.Drawing.Point(7, 79);
            this.chk3.Name = "chk3";
            this.chk3.Size = new System.Drawing.Size(117, 17);
            this.chk3.TabIndex = 2;
            this.chk3.Text = "Manufacturer detail";
            this.chk3.UseVisualStyleBackColor = true;
            // 
            // chk2
            // 
            this.chk2.AutoSize = true;
            this.chk2.Location = new System.Drawing.Point(7, 55);
            this.chk2.Name = "chk2";
            this.chk2.Size = new System.Drawing.Size(110, 17);
            this.chk2.TabIndex = 1;
            this.chk2.Text = "Item configuration";
            this.chk2.UseVisualStyleBackColor = true;
            // 
            // chk1
            // 
            this.chk1.AutoSize = true;
            this.chk1.Location = new System.Drawing.Point(7, 31);
            this.chk1.Name = "chk1";
            this.chk1.Size = new System.Drawing.Size(122, 17);
            this.chk1.TabIndex = 0;
            this.chk1.Text = "Pharmacy master list";
            this.chk1.UseVisualStyleBackColor = true;
            // 
            // frmSCM_PharmacyMasterImportExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 499);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnexport);
            this.Controls.Add(this.btnclose);
            this.Controls.Add(this.lblmessage);
            this.Controls.Add(this.grpimport);
            this.Name = "frmSCM_PharmacyMasterImportExport";
            this.Text = "Copy SCM Configuration";
            this.Load += new System.EventHandler(this.frmSCM_PharmacyMasterImportExport_Load);
            this.grpimport.ResumeLayout(false);
            this.grpimport.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpimport;
        private System.Windows.Forms.TextBox txtfile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnexport;
        private System.Windows.Forms.Label lblmessage;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblfilename;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chk16;
        private System.Windows.Forms.CheckBox chk15;
        private System.Windows.Forms.CheckBox chk14;
        private System.Windows.Forms.CheckBox chk13;
        private System.Windows.Forms.CheckBox chk12;
        private System.Windows.Forms.CheckBox chk11;
        private System.Windows.Forms.CheckBox chk10;
        private System.Windows.Forms.CheckBox chk9;
        private System.Windows.Forms.CheckBox chk8;
        private System.Windows.Forms.CheckBox chk7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chk6;
        private System.Windows.Forms.CheckBox chk5;
        private System.Windows.Forms.CheckBox chk4;
        private System.Windows.Forms.CheckBox chk3;
        private System.Windows.Forms.CheckBox chk2;
        private System.Windows.Forms.CheckBox chk1;
    }
}