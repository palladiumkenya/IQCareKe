using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System;
namespace IQConnection
{
    partial class ConnectionConfig
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
            this.panelIQCare = new System.Windows.Forms.Panel();
            this.btnexit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.ConfigIQtools = new System.Windows.Forms.CheckBox();
            this.grpbIQtools = new System.Windows.Forms.GroupBox();
            this.refreshIntervalText = new System.Windows.Forms.NumericUpDown();
            this.labelRefreshTime = new System.Windows.Forms.Label();
            this.IQToolsDBPassConfirm = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.IQToolsDatabase = new System.Windows.Forms.TextBox();
            this.IQToolsDBUser = new System.Windows.Forms.TextBox();
            this.IQToolsDBPass = new System.Windows.Forms.TextBox();
            this.IQToolServer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtconfirmpass = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDBnm = new System.Windows.Forms.TextBox();
            this.txtUsernm = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtServernm = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panelIQCare.SuspendLayout();
            this.grpbIQtools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshIntervalText)).BeginInit();
            this.SuspendLayout();
            // 
            // panelIQCare
            // 
            this.panelIQCare.Controls.Add(this.btnexit);
            this.panelIQCare.Controls.Add(this.btnSave);
            this.panelIQCare.Controls.Add(this.ConfigIQtools);
            this.panelIQCare.Controls.Add(this.grpbIQtools);
            this.panelIQCare.Controls.Add(this.txtconfirmpass);
            this.panelIQCare.Controls.Add(this.label5);
            this.panelIQCare.Controls.Add(this.txtDBnm);
            this.panelIQCare.Controls.Add(this.txtUsernm);
            this.panelIQCare.Controls.Add(this.txtPassword);
            this.panelIQCare.Controls.Add(this.txtServernm);
            this.panelIQCare.Controls.Add(this.label4);
            this.panelIQCare.Controls.Add(this.label3);
            this.panelIQCare.Controls.Add(this.label2);
            this.panelIQCare.Controls.Add(this.label1);
            this.panelIQCare.Location = new System.Drawing.Point(2, 1);
            this.panelIQCare.Name = "panelIQCare";
            this.panelIQCare.Size = new System.Drawing.Size(400, 414);
            this.panelIQCare.TabIndex = 9;
            this.panelIQCare.Tag = "pnlPanel";
            // 
            // btnexit
            // 
            this.btnexit.Location = new System.Drawing.Point(226, 388);
            this.btnexit.Name = "btnexit";
            this.btnexit.Size = new System.Drawing.Size(95, 23);
            this.btnexit.TabIndex = 19;
            this.btnexit.Tag = "btnSingleText";
            this.btnexit.Text = "E&xit";
            this.btnexit.UseVisualStyleBackColor = true;
            this.btnexit.Click += new System.EventHandler(this.btnexit_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(19, 388);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 18;
            this.btnSave.Tag = "btnSingleText";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ConfigIQtools
            // 
            this.ConfigIQtools.AutoSize = true;
            this.ConfigIQtools.Location = new System.Drawing.Point(141, 147);
            this.ConfigIQtools.Name = "ConfigIQtools";
            this.ConfigIQtools.Size = new System.Drawing.Size(128, 17);
            this.ConfigIQtools.TabIndex = 22;
            this.ConfigIQtools.Text = "IQTools Configuration";
            this.ConfigIQtools.UseVisualStyleBackColor = true;
            this.ConfigIQtools.CheckedChanged += new System.EventHandler(this.ConfigIQtools_CheckedChanged);
            // 
            // grpbIQtools
            // 
            this.grpbIQtools.Controls.Add(this.refreshIntervalText);
            this.grpbIQtools.Controls.Add(this.labelRefreshTime);
            this.grpbIQtools.Controls.Add(this.IQToolsDBPassConfirm);
            this.grpbIQtools.Controls.Add(this.label6);
            this.grpbIQtools.Controls.Add(this.IQToolsDatabase);
            this.grpbIQtools.Controls.Add(this.IQToolsDBUser);
            this.grpbIQtools.Controls.Add(this.IQToolsDBPass);
            this.grpbIQtools.Controls.Add(this.IQToolServer);
            this.grpbIQtools.Controls.Add(this.label7);
            this.grpbIQtools.Controls.Add(this.label8);
            this.grpbIQtools.Controls.Add(this.label9);
            this.grpbIQtools.Controls.Add(this.label10);
            this.grpbIQtools.Enabled = false;
            this.grpbIQtools.Location = new System.Drawing.Point(10, 184);
            this.grpbIQtools.Name = "grpbIQtools";
            this.grpbIQtools.Size = new System.Drawing.Size(387, 189);
            this.grpbIQtools.TabIndex = 21;
            this.grpbIQtools.TabStop = false;
            this.grpbIQtools.Text = "IQTools Connection";
            // 
            // refreshIntervalText
            // 
            this.refreshIntervalText.Increment = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.refreshIntervalText.Location = new System.Drawing.Point(185, 160);
            this.refreshIntervalText.Maximum = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            this.refreshIntervalText.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.refreshIntervalText.Name = "refreshIntervalText";
            this.refreshIntervalText.Size = new System.Drawing.Size(120, 20);
            this.refreshIntervalText.TabIndex = 33;
            this.refreshIntervalText.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // labelRefreshTime
            // 
            this.labelRefreshTime.AutoSize = true;
            this.labelRefreshTime.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRefreshTime.Location = new System.Drawing.Point(28, 160);
            this.labelRefreshTime.Name = "labelRefreshTime";
            this.labelRefreshTime.Size = new System.Drawing.Size(149, 15);
            this.labelRefreshTime.TabIndex = 32;
            this.labelRefreshTime.Text = "Refresh Interval (minutes)";
            // 
            // IQToolsDBPassConfirm
            // 
            this.IQToolsDBPassConfirm.BackColor = System.Drawing.SystemColors.Window;
            this.IQToolsDBPassConfirm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IQToolsDBPassConfirm.Location = new System.Drawing.Point(145, 132);
            this.IQToolsDBPassConfirm.Name = "IQToolsDBPassConfirm";
            this.IQToolsDBPassConfirm.PasswordChar = '*';
            this.IQToolsDBPassConfirm.Size = new System.Drawing.Size(216, 21);
            this.IQToolsDBPassConfirm.TabIndex = 29;
            this.IQToolsDBPassConfirm.Tag = "txtTextBox";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(25, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 15);
            this.label6.TabIndex = 30;
            this.label6.Tag = "lblLabel";
            this.label6.Text = "Verify Password";
            // 
            // IQToolsDatabase
            // 
            this.IQToolsDatabase.BackColor = System.Drawing.SystemColors.Window;
            this.IQToolsDatabase.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IQToolsDatabase.Location = new System.Drawing.Point(145, 51);
            this.IQToolsDatabase.Name = "IQToolsDatabase";
            this.IQToolsDatabase.Size = new System.Drawing.Size(216, 21);
            this.IQToolsDatabase.TabIndex = 23;
            this.IQToolsDatabase.Tag = "txtTextBox";
            // 
            // IQToolsDBUser
            // 
            this.IQToolsDBUser.BackColor = System.Drawing.SystemColors.Window;
            this.IQToolsDBUser.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IQToolsDBUser.Location = new System.Drawing.Point(145, 78);
            this.IQToolsDBUser.Name = "IQToolsDBUser";
            this.IQToolsDBUser.Size = new System.Drawing.Size(216, 21);
            this.IQToolsDBUser.TabIndex = 26;
            this.IQToolsDBUser.Tag = "txtTextBox";
            // 
            // IQToolsDBPass
            // 
            this.IQToolsDBPass.BackColor = System.Drawing.SystemColors.Window;
            this.IQToolsDBPass.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IQToolsDBPass.Location = new System.Drawing.Point(145, 105);
            this.IQToolsDBPass.Name = "IQToolsDBPass";
            this.IQToolsDBPass.PasswordChar = '*';
            this.IQToolsDBPass.Size = new System.Drawing.Size(216, 21);
            this.IQToolsDBPass.TabIndex = 28;
            this.IQToolsDBPass.Tag = "txtTextBox";
            // 
            // IQToolServer
            // 
            this.IQToolServer.BackColor = System.Drawing.SystemColors.Window;
            this.IQToolServer.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IQToolServer.Location = new System.Drawing.Point(145, 24);
            this.IQToolServer.Name = "IQToolServer";
            this.IQToolServer.Size = new System.Drawing.Size(216, 21);
            this.IQToolServer.TabIndex = 21;
            this.IQToolServer.Tag = "txtTextBox";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(25, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 15);
            this.label7.TabIndex = 27;
            this.label7.Tag = "lblLabel";
            this.label7.Text = "DataBase Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(25, 80);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(71, 15);
            this.label8.TabIndex = 25;
            this.label8.Tag = "lblLabel";
            this.label8.Text = "User Name";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(25, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 15);
            this.label9.TabIndex = 24;
            this.label9.Tag = "lblLabel";
            this.label9.Text = "Password";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(25, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 15);
            this.label10.TabIndex = 22;
            this.label10.Tag = "lblLabel";
            this.label10.Text = "Server Name";
            // 
            // txtconfirmpass
            // 
            this.txtconfirmpass.BackColor = System.Drawing.SystemColors.Window;
            this.txtconfirmpass.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtconfirmpass.Location = new System.Drawing.Point(141, 119);
            this.txtconfirmpass.Name = "txtconfirmpass";
            this.txtconfirmpass.PasswordChar = '*';
            this.txtconfirmpass.Size = new System.Drawing.Size(216, 21);
            this.txtconfirmpass.TabIndex = 17;
            this.txtconfirmpass.Tag = "txtTextBox";
            this.txtconfirmpass.Leave += new System.EventHandler(this.txtconfirmpass_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(21, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(94, 15);
            this.label5.TabIndex = 20;
            this.label5.Tag = "lblLabel";
            this.label5.Text = "Verify Password";
            // 
            // txtDBnm
            // 
            this.txtDBnm.BackColor = System.Drawing.SystemColors.Window;
            this.txtDBnm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBnm.Location = new System.Drawing.Point(141, 38);
            this.txtDBnm.Name = "txtDBnm";
            this.txtDBnm.Size = new System.Drawing.Size(216, 21);
            this.txtDBnm.TabIndex = 11;
            this.txtDBnm.Tag = "txtTextBox";
            // 
            // txtUsernm
            // 
            this.txtUsernm.BackColor = System.Drawing.SystemColors.Window;
            this.txtUsernm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsernm.Location = new System.Drawing.Point(141, 65);
            this.txtUsernm.Name = "txtUsernm";
            this.txtUsernm.Size = new System.Drawing.Size(216, 21);
            this.txtUsernm.TabIndex = 14;
            this.txtUsernm.Tag = "txtTextBox";
            this.txtUsernm.Leave += new System.EventHandler(this.txtUsernm_Leave);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.Window;
            this.txtPassword.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(141, 92);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(216, 21);
            this.txtPassword.TabIndex = 16;
            this.txtPassword.Tag = "txtTextBox";
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // txtServernm
            // 
            this.txtServernm.BackColor = System.Drawing.SystemColors.Window;
            this.txtServernm.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServernm.Location = new System.Drawing.Point(141, 11);
            this.txtServernm.Name = "txtServernm";
            this.txtServernm.Size = new System.Drawing.Size(216, 21);
            this.txtServernm.TabIndex = 9;
            this.txtServernm.Tag = "txtTextBox";
            this.txtServernm.Leave += new System.EventHandler(this.txtServernm_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(21, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 15);
            this.label4.TabIndex = 15;
            this.label4.Tag = "lblLabel";
            this.label4.Text = "DataBase Name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 15);
            this.label3.TabIndex = 13;
            this.label3.Tag = "lblLabel";
            this.label3.Text = "User Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 15);
            this.label2.TabIndex = 12;
            this.label2.Tag = "lblLabel";
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(21, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 10;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Server Name";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // ConnectionConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 420);
            this.Controls.Add(this.panelIQCare);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectionConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "frmForm";
            this.Text = "New Connection";
            this.Load += new System.EventHandler(this.ConnectionConfig_Load);
            this.panelIQCare.ResumeLayout(false);
            this.panelIQCare.PerformLayout();
            this.grpbIQtools.ResumeLayout(false);
            this.grpbIQtools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.refreshIntervalText)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelIQCare;
        private System.Windows.Forms.Button btnexit;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtconfirmpass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDBnm;
        private System.Windows.Forms.TextBox txtUsernm;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtServernm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox ConfigIQtools;
        private System.Windows.Forms.GroupBox grpbIQtools;
        private System.Windows.Forms.TextBox IQToolsDBPassConfirm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox IQToolsDatabase;
        private System.Windows.Forms.TextBox IQToolsDBUser;
        private System.Windows.Forms.TextBox IQToolsDBPass;
        private System.Windows.Forms.TextBox IQToolServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private Label labelRefreshTime;
     
        private BackgroundWorker backgroundWorker1;
        private NumericUpDown refreshIntervalText;
        private ContextMenuStrip contextMenuStrip1;

    }
}

