namespace IQCare.FormBuilder
{
    partial class frmSelectList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectList));
            this.lstSelectList = new System.Windows.Forms.ListBox();
            this.lblEntervalue = new System.Windows.Forms.Label();
            this.txtEnterValue = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.lblmove = new System.Windows.Forms.Label();
            this.pnlselectlist = new System.Windows.Forms.Panel();
            this.btnconditionalfield = new System.Windows.Forms.Button();
            this.pnlselectlist.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstSelectList
            // 
            this.lstSelectList.FormattingEnabled = true;
            this.lstSelectList.Location = new System.Drawing.Point(6, 38);
            this.lstSelectList.Name = "lstSelectList";
            this.lstSelectList.Size = new System.Drawing.Size(255, 238);
            this.lstSelectList.TabIndex = 0;
            this.lstSelectList.Tag = "lstListBox";
            // 
            // lblEntervalue
            // 
            this.lblEntervalue.AutoSize = true;
            this.lblEntervalue.Location = new System.Drawing.Point(3, 10);
            this.lblEntervalue.Name = "lblEntervalue";
            this.lblEntervalue.Size = new System.Drawing.Size(62, 13);
            this.lblEntervalue.TabIndex = 1;
            this.lblEntervalue.Tag = "lblLabel";
            this.lblEntervalue.Text = "Enter Value";
            // 
            // txtEnterValue
            // 
            this.txtEnterValue.Location = new System.Drawing.Point(89, 7);
            this.txtEnterValue.Name = "txtEnterValue";
            this.txtEnterValue.Size = new System.Drawing.Size(172, 20);
            this.txtEnterValue.TabIndex = 2;
            this.txtEnterValue.Tag = "txtTextBox";
            this.txtEnterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEnterValue_KeyPress);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(267, 4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Tag = "btnSingleText";
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(62, 282);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Tag = "btnSingleText";
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(143, 282);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Tag = "btnSingleText";
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUp
            // 
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(267, 169);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(35, 23);
            this.btnUp.TabIndex = 6;
            this.btnUp.Tag = "btnFlexible";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(268, 108);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(34, 23);
            this.btnDown.TabIndex = 7;
            this.btnDown.Tag = "btnFlexible";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // lblmove
            // 
            this.lblmove.AutoSize = true;
            this.lblmove.Location = new System.Drawing.Point(268, 143);
            this.lblmove.Name = "lblmove";
            this.lblmove.Size = new System.Drawing.Size(34, 13);
            this.lblmove.TabIndex = 8;
            this.lblmove.Tag = "lblLabel";
            this.lblmove.Text = "Move";
            // 
            // pnlselectlist
            // 
            this.pnlselectlist.Controls.Add(this.lstSelectList);
            this.pnlselectlist.Controls.Add(this.btnDown);
            this.pnlselectlist.Controls.Add(this.lblmove);
            this.pnlselectlist.Controls.Add(this.btnAdd);
            this.pnlselectlist.Controls.Add(this.btnDelete);
            this.pnlselectlist.Controls.Add(this.txtEnterValue);
            this.pnlselectlist.Controls.Add(this.btnSubmit);
            this.pnlselectlist.Controls.Add(this.lblEntervalue);
            this.pnlselectlist.Controls.Add(this.btnUp);
            this.pnlselectlist.Location = new System.Drawing.Point(-1, -1);
            this.pnlselectlist.Name = "pnlselectlist";
            this.pnlselectlist.Size = new System.Drawing.Size(362, 316);
            this.pnlselectlist.TabIndex = 9;
            this.pnlselectlist.Tag = "pnlPanel";
            // 
            // btnconditionalfield
            // 
            this.btnconditionalfield.Location = new System.Drawing.Point(236, 282);
            this.btnconditionalfield.Name = "btnconditionalfield";
            this.btnconditionalfield.Size = new System.Drawing.Size(102, 23);
            this.btnconditionalfield.TabIndex = 11;
            this.btnconditionalfield.Tag = "btnH25W80Flexi";
            this.btnconditionalfield.Text = "Conditional Fields";
            this.btnconditionalfield.UseVisualStyleBackColor = true;
            this.btnconditionalfield.Click += new System.EventHandler(this.btnconditionalfield_Click);
            // 
            // frmSelectList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 317);
            this.Controls.Add(this.btnconditionalfield);
            this.Controls.Add(this.pnlselectlist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "frmForm";
            this.Text = "Select List";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmSelectList_FormClosed);
            this.Load += new System.EventHandler(this.frmSelectList_Load);
            this.pnlselectlist.ResumeLayout(false);
            this.pnlselectlist.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstSelectList;
        private System.Windows.Forms.Label lblEntervalue;
        private System.Windows.Forms.TextBox txtEnterValue;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Label lblmove;
        private System.Windows.Forms.Panel pnlselectlist;
        private System.Windows.Forms.Button btnconditionalfield;
    }
}