using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace IQCare.FormBuilder
{
    partial class frmFormBusinessRule
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
       

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormBusinessRule));
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
            this.chkSignature = new System.Windows.Forms.CheckBox();
            this.rdoMultipleVisit = new System.Windows.Forms.RadioButton();
            this.rdoSingleVisit = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lblset1
            // 
            this.lblset1.AutoSize = true;
            this.lblset1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblset1.Location = new System.Drawing.Point(12, 79);
            this.lblset1.Name = "lblset1";
            this.lblset1.Size = new System.Drawing.Size(234, 16);
            this.lblset1.TabIndex = 0;
            this.lblset1.Text = "Set 1: Form Area Business Rules";
            // 
            // lblset2
            // 
            this.lblset2.AutoSize = true;
            this.lblset2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblset2.Location = new System.Drawing.Point(9, 204);
            this.lblset2.Name = "lblset2";
            this.lblset2.Size = new System.Drawing.Size(234, 16);
            this.lblset2.TabIndex = 1;
            this.lblset2.Text = "Set 2: Form Area Business Rules";
            // 
            // btnsubmit
            // 
            this.btnsubmit.Location = new System.Drawing.Point(100, 301);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(75, 23);
            this.btnsubmit.TabIndex = 13;
            this.btnsubmit.Tag = "btnSingleText";
            this.btnsubmit.Text = "Submit";
            this.btnsubmit.UseVisualStyleBackColor = true;
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(181, 301);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(75, 23);
            this.btncancel.TabIndex = 14;
            this.btncancel.Tag = "btnSingleText";
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // chkset1currentage
            // 
            this.chkset1currentage.AutoSize = true;
            this.chkset1currentage.Location = new System.Drawing.Point(15, 108);
            this.chkset1currentage.Name = "chkset1currentage";
            this.chkset1currentage.Size = new System.Drawing.Size(137, 17);
            this.chkset1currentage.TabIndex = 3;
            this.chkset1currentage.Text = "Active If current age >=";
            this.chkset1currentage.UseVisualStyleBackColor = true;
            // 
            // chkset1female
            // 
            this.chkset1female.AutoSize = true;
            this.chkset1female.Location = new System.Drawing.Point(15, 131);
            this.chkset1female.Name = "chkset1female";
            this.chkset1female.Size = new System.Drawing.Size(99, 17);
            this.chkset1female.TabIndex = 6;
            this.chkset1female.Text = "Active If female";
            this.chkset1female.UseVisualStyleBackColor = true;
            // 
            // chkset1male
            // 
            this.chkset1male.AutoSize = true;
            this.chkset1male.Location = new System.Drawing.Point(15, 155);
            this.chkset1male.Name = "chkset1male";
            this.chkset1male.Size = new System.Drawing.Size(90, 17);
            this.chkset1male.TabIndex = 7;
            this.chkset1male.Text = "Active If male";
            this.chkset1male.UseVisualStyleBackColor = true;
            // 
            // chkset2currentage
            // 
            this.chkset2currentage.AutoSize = true;
            this.chkset2currentage.Location = new System.Drawing.Point(12, 226);
            this.chkset2currentage.Name = "chkset2currentage";
            this.chkset2currentage.Size = new System.Drawing.Size(137, 17);
            this.chkset2currentage.TabIndex = 8;
            this.chkset2currentage.Text = "Active If current age >=";
            this.chkset2currentage.UseVisualStyleBackColor = true;
            // 
            // chkset2female
            // 
            this.chkset2female.AutoSize = true;
            this.chkset2female.Location = new System.Drawing.Point(12, 249);
            this.chkset2female.Name = "chkset2female";
            this.chkset2female.Size = new System.Drawing.Size(99, 17);
            this.chkset2female.TabIndex = 11;
            this.chkset2female.Text = "Active If female";
            this.chkset2female.UseVisualStyleBackColor = true;
            // 
            // chkset2male
            // 
            this.chkset2male.AutoSize = true;
            this.chkset2male.Location = new System.Drawing.Point(12, 272);
            this.chkset2male.Name = "chkset2male";
            this.chkset2male.Size = new System.Drawing.Size(90, 17);
            this.chkset2male.TabIndex = 12;
            this.chkset2male.Text = "Active If male";
            this.chkset2male.UseVisualStyleBackColor = true;
            // 
            // txtset1value
            // 
            this.txtset1value.Location = new System.Drawing.Point(158, 106);
            this.txtset1value.MaxLength = 3;
            this.txtset1value.Name = "txtset1value";
            this.txtset1value.Size = new System.Drawing.Size(48, 20);
            this.txtset1value.TabIndex = 4;
            this.txtset1value.Validating += new System.ComponentModel.CancelEventHandler(this.txtset1value_Validating);
            // 
            // txtset1value1
            // 
            this.txtset1value1.Location = new System.Drawing.Point(250, 105);
            this.txtset1value1.MaxLength = 4;
            this.txtset1value1.Name = "txtset1value1";
            this.txtset1value1.Size = new System.Drawing.Size(48, 20);
            this.txtset1value1.TabIndex = 5;
            this.txtset1value1.Validating += new System.ComponentModel.CancelEventHandler(this.txtset1value1_Validating);
            // 
            // txtset2value
            // 
            this.txtset2value.Location = new System.Drawing.Point(161, 226);
            this.txtset2value.MaxLength = 3;
            this.txtset2value.Name = "txtset2value";
            this.txtset2value.Size = new System.Drawing.Size(48, 20);
            this.txtset2value.TabIndex = 9;
            this.txtset2value.Validating += new System.ComponentModel.CancelEventHandler(this.txtset2value_Validating);
            // 
            // txtset2value1
            // 
            this.txtset2value1.Location = new System.Drawing.Point(253, 223);
            this.txtset2value1.MaxLength = 4;
            this.txtset2value1.Name = "txtset2value1";
            this.txtset2value1.Size = new System.Drawing.Size(48, 20);
            this.txtset2value1.TabIndex = 10;
            this.txtset2value1.Validating += new System.ComponentModel.CancelEventHandler(this.txtset2value1_Validating);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(305, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Years";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(308, 229);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Years";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(213, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "and <";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(213, 226);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "and <";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 178);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "OR";
            // 
            // chkSignature
            // 
            this.chkSignature.AutoSize = true;
            this.chkSignature.Location = new System.Drawing.Point(17, 44);
            this.chkSignature.Name = "chkSignature";
            this.chkSignature.Size = new System.Drawing.Size(135, 17);
            this.chkSignature.TabIndex = 2;
            this.chkSignature.Text = "Signature on each Tab";
            this.chkSignature.UseVisualStyleBackColor = true;
            // 
            // rdoMultipleVisit
            // 
            this.rdoMultipleVisit.AutoSize = true;
            this.rdoMultipleVisit.Location = new System.Drawing.Point(108, 12);
            this.rdoMultipleVisit.Name = "rdoMultipleVisit";
            this.rdoMultipleVisit.Size = new System.Drawing.Size(83, 17);
            this.rdoMultipleVisit.TabIndex = 1;
            this.rdoMultipleVisit.TabStop = true;
            this.rdoMultipleVisit.Tag = "rdoButton";
            this.rdoMultipleVisit.Text = "Multiple Visit";
            this.rdoMultipleVisit.UseVisualStyleBackColor = true;
            // 
            // rdoSingleVisit
            // 
            this.rdoSingleVisit.AutoSize = true;
            this.rdoSingleVisit.Location = new System.Drawing.Point(17, 12);
            this.rdoSingleVisit.Name = "rdoSingleVisit";
            this.rdoSingleVisit.Size = new System.Drawing.Size(76, 17);
            this.rdoSingleVisit.TabIndex = 0;
            this.rdoSingleVisit.TabStop = true;
            this.rdoSingleVisit.Tag = "rdoButton";
            this.rdoSingleVisit.Text = "Single Visit";
            this.rdoSingleVisit.UseVisualStyleBackColor = true;
            // 
            // frmFormBusinessRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(365, 336);
            this.Controls.Add(this.chkSignature);
            this.Controls.Add(this.rdoMultipleVisit);
            this.Controls.Add(this.rdoSingleVisit);
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
            this.Name = "frmFormBusinessRule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "frmForm";
            this.Text = "Form  Business Rule";
            this.Load += new System.EventHandler(this.frmServiceBusinessRule_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
