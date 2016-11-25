namespace IQCare.FormBuilder
{
    partial class frmServiceBusinessRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServiceBusinessRule));
            this.lblset1 = new System.Windows.Forms.Label();
            this.lblset2 = new System.Windows.Forms.Label();
            this.btnsubmit = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.chkset1currentage = new System.Windows.Forms.CheckBox();
            this.chkset1female = new System.Windows.Forms.CheckBox();
            this.chkset1male = new System.Windows.Forms.CheckBox();
            this.chkset2currentage = new System.Windows.Forms.CheckBox();
            this.chkset2female = new System.Windows.Forms.CheckBox();
            this.chkset2male = new System.Windows.Forms.CheckBox();
            this.txtset1value = new System.Windows.Forms.TextBox();
            this.txtset1value1 = new System.Windows.Forms.TextBox();
            this.txtset2value = new System.Windows.Forms.TextBox();
            this.txtset2value1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblset1
            // 
            this.lblset1.AutoSize = true;
            this.lblset1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblset1.Location = new System.Drawing.Point(12, 9);
            this.lblset1.Name = "lblset1";
            this.lblset1.Size = new System.Drawing.Size(252, 16);
            this.lblset1.TabIndex = 0;
            this.lblset1.Text = "Set 1: Service Area Business Rules";
            // 
            // lblset2
            // 
            this.lblset2.AutoSize = true;
            this.lblset2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblset2.Location = new System.Drawing.Point(12, 145);
            this.lblset2.Name = "lblset2";
            this.lblset2.Size = new System.Drawing.Size(252, 16);
            this.lblset2.TabIndex = 1;
            this.lblset2.Text = "Set 2: Service Area Business Rules";
            // 
            // btnsubmit
            // 
            this.btnsubmit.Location = new System.Drawing.Point(108, 250);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(75, 23);
            this.btnsubmit.TabIndex = 10;
            this.btnsubmit.Tag = "btnSingleText";
            this.btnsubmit.Text = "Submit";
            this.btnsubmit.UseVisualStyleBackColor = true;
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(189, 250);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 23);
            this.btncancel.TabIndex = 11;
            this.btncancel.Tag = "btnSingleText";
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // chkset1currentage
            // 
            this.chkset1currentage.AutoSize = true;
            this.chkset1currentage.Location = new System.Drawing.Point(15, 38);
            this.chkset1currentage.Name = "chkset1currentage";
            this.chkset1currentage.Size = new System.Drawing.Size(137, 17);
            this.chkset1currentage.TabIndex = 0;
            this.chkset1currentage.Text = "Active If current age >=";
            this.chkset1currentage.UseVisualStyleBackColor = true;
            // 
            // chkset1female
            // 
            this.chkset1female.AutoSize = true;
            this.chkset1female.Location = new System.Drawing.Point(15, 61);
            this.chkset1female.Name = "chkset1female";
            this.chkset1female.Size = new System.Drawing.Size(99, 17);
            this.chkset1female.TabIndex = 3;
            this.chkset1female.Text = "Active If female";
            this.chkset1female.UseVisualStyleBackColor = true;
            // 
            // chkset1male
            // 
            this.chkset1male.AutoSize = true;
            this.chkset1male.Location = new System.Drawing.Point(15, 85);
            this.chkset1male.Name = "chkset1male";
            this.chkset1male.Size = new System.Drawing.Size(90, 17);
            this.chkset1male.TabIndex = 4;
            this.chkset1male.Text = "Active If male";
            this.chkset1male.UseVisualStyleBackColor = true;
            // 
            // chkset2currentage
            // 
            this.chkset2currentage.AutoSize = true;
            this.chkset2currentage.Location = new System.Drawing.Point(15, 170);
            this.chkset2currentage.Name = "chkset2currentage";
            this.chkset2currentage.Size = new System.Drawing.Size(137, 17);
            this.chkset2currentage.TabIndex = 5;
            this.chkset2currentage.Text = "Active If current age >=";
            this.chkset2currentage.UseVisualStyleBackColor = true;
            // 
            // chkset2female
            // 
            this.chkset2female.AutoSize = true;
            this.chkset2female.Location = new System.Drawing.Point(15, 193);
            this.chkset2female.Name = "chkset2female";
            this.chkset2female.Size = new System.Drawing.Size(99, 17);
            this.chkset2female.TabIndex = 8;
            this.chkset2female.Text = "Active If female";
            this.chkset2female.UseVisualStyleBackColor = true;
            // 
            // chkset2male
            // 
            this.chkset2male.AutoSize = true;
            this.chkset2male.Location = new System.Drawing.Point(15, 216);
            this.chkset2male.Name = "chkset2male";
            this.chkset2male.Size = new System.Drawing.Size(90, 17);
            this.chkset2male.TabIndex = 9;
            this.chkset2male.Text = "Active If male";
            this.chkset2male.UseVisualStyleBackColor = true;
            // 
            // txtset1value
            // 
            this.txtset1value.Location = new System.Drawing.Point(158, 36);
            this.txtset1value.MaxLength = 3;
            this.txtset1value.Name = "txtset1value";
            this.txtset1value.Size = new System.Drawing.Size(48, 20);
            this.txtset1value.TabIndex = 1;
            this.txtset1value.Validating += new System.ComponentModel.CancelEventHandler(this.txtset1value_Validating);
            // 
            // txtset1value1
            // 
            this.txtset1value1.Location = new System.Drawing.Point(250, 35);
            this.txtset1value1.MaxLength = 4;
            this.txtset1value1.Name = "txtset1value1";
            this.txtset1value1.Size = new System.Drawing.Size(48, 20);
            this.txtset1value1.TabIndex = 2;
            this.txtset1value1.Validating += new System.ComponentModel.CancelEventHandler(this.txtset1value1_Validating);
            // 
            // txtset2value
            // 
            this.txtset2value.Location = new System.Drawing.Point(158, 167);
            this.txtset2value.MaxLength = 3;
            this.txtset2value.Name = "txtset2value";
            this.txtset2value.Size = new System.Drawing.Size(48, 20);
            this.txtset2value.TabIndex = 6;
            this.txtset2value.Validating += new System.ComponentModel.CancelEventHandler(this.txtset2value_Validating);
            // 
            // txtset2value1
            // 
            this.txtset2value1.Location = new System.Drawing.Point(250, 164);
            this.txtset2value1.MaxLength = 4;
            this.txtset2value1.Name = "txtset2value1";
            this.txtset2value1.Size = new System.Drawing.Size(48, 20);
            this.txtset2value1.TabIndex = 7;
            this.txtset2value1.Validating += new System.ComponentModel.CancelEventHandler(this.txtset2value1_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Years";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(305, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Years";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(213, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "and <";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(210, 167);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "and <";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "OR";
            // 
            // frmServiceBusinessRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(365, 293);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtset2value1);
            this.Controls.Add(this.txtset2value);
            this.Controls.Add(this.txtset1value1);
            this.Controls.Add(this.txtset1value);
            this.Controls.Add(this.chkset2male);
            this.Controls.Add(this.chkset2female);
            this.Controls.Add(this.chkset2currentage);
            this.Controls.Add(this.chkset1male);
            this.Controls.Add(this.chkset1female);
            this.Controls.Add(this.chkset1currentage);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnsubmit);
            this.Controls.Add(this.lblset2);
            this.Controls.Add(this.lblset1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmServiceBusinessRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "frmForm";
            this.Text = "Service Area Business Rule";
            this.Load += new System.EventHandler(this.frmServiceBusinessRule_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblset1;
        private System.Windows.Forms.Label lblset2;
        private System.Windows.Forms.Button btnsubmit;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.CheckBox chkset1currentage;
        private System.Windows.Forms.CheckBox chkset1female;
        private System.Windows.Forms.CheckBox chkset1male;
        private System.Windows.Forms.CheckBox chkset2currentage;
        private System.Windows.Forms.CheckBox chkset2female;
        private System.Windows.Forms.CheckBox chkset2male;
        private System.Windows.Forms.TextBox txtset1value;
        private System.Windows.Forms.TextBox txtset1value1;
        private System.Windows.Forms.TextBox txtset2value;
        private System.Windows.Forms.TextBox txtset2value1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}