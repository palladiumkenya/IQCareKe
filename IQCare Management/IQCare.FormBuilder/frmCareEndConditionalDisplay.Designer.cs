namespace IQCare.FormBuilder
{
    partial class frmCareEndConditionalDisplay
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCareEndConditionalDisplay));
            this.pnlCondFields = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lbloptionalfield = new System.Windows.Forms.Label();
            this.cmbConditionalField = new System.Windows.Forms.ComboBox();
            this.dgwConditionalField = new System.Windows.Forms.DataGridView();
            this.theUPExitReason = new System.Windows.Forms.Button();
            this.theDownExitreason = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlCondFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwConditionalField)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCondFields
            // 
            this.pnlCondFields.Controls.Add(this.btnDelete);
            this.pnlCondFields.Controls.Add(this.btnBack);
            this.pnlCondFields.Controls.Add(this.lbloptionalfield);
            this.pnlCondFields.Controls.Add(this.cmbConditionalField);
            this.pnlCondFields.Controls.Add(this.dgwConditionalField);
            this.pnlCondFields.Controls.Add(this.theUPExitReason);
            this.pnlCondFields.Controls.Add(this.theDownExitreason);
            this.pnlCondFields.Controls.Add(this.label3);
            this.pnlCondFields.Location = new System.Drawing.Point(2, 4);
            this.pnlCondFields.Name = "pnlCondFields";
            this.pnlCondFields.Size = new System.Drawing.Size(746, 463);
            this.pnlCondFields.TabIndex = 62;
            this.pnlCondFields.Tag = "pnlPanel";
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDelete.Location = new System.Drawing.Point(378, 429);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 53;
            this.btnDelete.Tag = "btnH25WFlexi";
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBack.Location = new System.Drawing.Point(495, 4);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 51;
            this.btnBack.Tag = "btnH25WFlexi";
            this.btnBack.Text = "Submit";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lbloptionalfield
            // 
            this.lbloptionalfield.Location = new System.Drawing.Point(9, 10);
            this.lbloptionalfield.Name = "lbloptionalfield";
            this.lbloptionalfield.Size = new System.Drawing.Size(132, 13);
            this.lbloptionalfield.TabIndex = 50;
            this.lbloptionalfield.Tag = "lblLabel";
            this.lbloptionalfield.Text = "List Option";
            // 
            // cmbConditionalField
            // 
            this.cmbConditionalField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConditionalField.FormattingEnabled = true;
            this.cmbConditionalField.Location = new System.Drawing.Point(147, 6);
            this.cmbConditionalField.Name = "cmbConditionalField";
            this.cmbConditionalField.Size = new System.Drawing.Size(333, 21);
            this.cmbConditionalField.TabIndex = 49;
            this.cmbConditionalField.Tag = "ddlDropDownList";
            this.cmbConditionalField.SelectionChangeCommitted += new System.EventHandler(this.cmbConditionalField_SelectionChangeCommitted);
            this.cmbConditionalField.SelectedValueChanged += new System.EventHandler(this.cmbConditionalField_SelectedValueChanged);
            // 
            // dgwConditionalField
            // 
            this.dgwConditionalField.AllowUserToAddRows = false;
            this.dgwConditionalField.AllowUserToDeleteRows = false;
            this.dgwConditionalField.AllowUserToResizeColumns = false;
            this.dgwConditionalField.AllowUserToResizeRows = false;
            this.dgwConditionalField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwConditionalField.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgwConditionalField.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwConditionalField.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgwConditionalField.Location = new System.Drawing.Point(3, 33);
            this.dgwConditionalField.Name = "dgwConditionalField";
            this.dgwConditionalField.Size = new System.Drawing.Size(697, 391);
            this.dgwConditionalField.TabIndex = 42;
            this.dgwConditionalField.Tag = "dgwDataGridView";
            this.dgwConditionalField.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwConditionalField_CellClick);
            this.dgwConditionalField.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwConditionalField_CellFormatting);
            this.dgwConditionalField.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgwConditionalField_EditingControlShowing);
            // 
            // theUPExitReason
            // 
            this.theUPExitReason.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.theUPExitReason.Image = ((System.Drawing.Image)(resources.GetObject("theUPExitReason.Image")));
            this.theUPExitReason.Location = new System.Drawing.Point(706, 184);
            this.theUPExitReason.Name = "theUPExitReason";
            this.theUPExitReason.Size = new System.Drawing.Size(24, 26);
            this.theUPExitReason.TabIndex = 47;
            this.theUPExitReason.Tag = "btnFlexible";
            this.theUPExitReason.UseVisualStyleBackColor = true;
            this.theUPExitReason.Click += new System.EventHandler(this.theUPExitReason_Click);
            // 
            // theDownExitreason
            // 
            this.theDownExitreason.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.theDownExitreason.Image = ((System.Drawing.Image)(resources.GetObject("theDownExitreason.Image")));
            this.theDownExitreason.Location = new System.Drawing.Point(706, 253);
            this.theDownExitreason.Name = "theDownExitreason";
            this.theDownExitreason.Size = new System.Drawing.Size(24, 26);
            this.theDownExitreason.TabIndex = 46;
            this.theDownExitreason.Tag = "btnFlexible";
            this.theDownExitreason.UseVisualStyleBackColor = true;
            this.theDownExitreason.Click += new System.EventHandler(this.theDownExitreason_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(701, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 48;
            this.label3.Tag = "lblLabel";
            this.label3.Text = "Move";
            // 
            // frmCareEndConditionalDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 468);
            this.Controls.Add(this.pnlCondFields);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCareEndConditionalDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "frmCareEndConditionalDisplay";
            this.Load += new System.EventHandler(this.frmConditionalDisplay_Load);
            this.pnlCondFields.ResumeLayout(false);
            this.pnlCondFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwConditionalField)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCondFields;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lbloptionalfield;
        private System.Windows.Forms.ComboBox cmbConditionalField;
        private System.Windows.Forms.DataGridView dgwConditionalField;
        private System.Windows.Forms.Button theUPExitReason;
        private System.Windows.Forms.Button theDownExitreason;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDelete;
    }
}