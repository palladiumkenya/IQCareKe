namespace IQCare.FormBuilder
{
    partial class frmCareEnded
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCareEnded));
            this.label1 = new System.Windows.Forms.Label();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.txtSection = new System.Windows.Forms.TextBox();
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.lblSection = new System.Windows.Forms.Label();
            this.lblTechArea = new System.Windows.Forms.Label();
            this.pnl_maingrd = new System.Windows.Forms.Panel();
            this.dgwCareEnded = new System.Windows.Forms.DataGridView();
            this.pnl_ReasonSelect = new System.Windows.Forms.Panel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnReasonClose = new System.Windows.Forms.Button();
            this.btnConditionalField = new System.Windows.Forms.Button();
            this.btnAddReason = new System.Windows.Forms.Button();
            this.txtAddReason = new System.Windows.Forms.TextBox();
            this.btnUnSelect = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.lstSelExitReason = new System.Windows.Forms.ListBox();
            this.lstExitReason = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlCondFields = new System.Windows.Forms.Panel();
            this.btnBack = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbExitReason = new System.Windows.Forms.ComboBox();
            this.dgwExitReason = new System.Windows.Forms.DataGridView();
            this.theUPExitReason = new System.Windows.Forms.Button();
            this.theDownExitreason = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbTechnicalArea = new System.Windows.Forms.ComboBox();
            this.pnl_DeathReasonSelect = new System.Windows.Forms.Panel();
            this.btnDeathSubmit = new System.Windows.Forms.Button();
            this.btnDeathReasonClose = new System.Windows.Forms.Button();
            this.btnDeathConditionalField = new System.Windows.Forms.Button();
            this.btnAddDeathReason = new System.Windows.Forms.Button();
            this.txtAddDeathReason = new System.Windows.Forms.TextBox();
            this.btnDeathUnSelect = new System.Windows.Forms.Button();
            this.btnDeathSelect = new System.Windows.Forms.Button();
            this.lstSelDeathReason = new System.Windows.Forms.ListBox();
            this.lstDeathReason = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnl_maingrd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwCareEnded)).BeginInit();
            this.pnl_ReasonSelect.SuspendLayout();
            this.pnlCondFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwExitReason)).BeginInit();
            this.pnl_DeathReasonSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(98, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 48;
            this.label1.Tag = "lblLabel";
            this.label1.Text = "Move";
            // 
            // btnUp
            // 
            this.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnUp.Image = ((System.Drawing.Image)(resources.GetObject("btnUp.Image")));
            this.btnUp.Location = new System.Drawing.Point(103, 3);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(24, 26);
            this.btnUp.TabIndex = 47;
            this.btnUp.Tag = "btnFlexible";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnDown.Image = ((System.Drawing.Image)(resources.GetObject("btnDown.Image")));
            this.btnDown.Location = new System.Drawing.Point(103, 72);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(24, 26);
            this.btnDown.TabIndex = 46;
            this.btnDown.Tag = "btnFlexible";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(53, 476);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(131, 26);
            this.btnSave.TabIndex = 49;
            this.btnSave.Tag = "btnH25WFlexi";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(469, 476);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(131, 26);
            this.btnCancel.TabIndex = 50;
            this.btnCancel.Tag = "btnH25WFlexi";
            this.btnCancel.Text = "&Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(207, 476);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(132, 26);
            this.btnRemove.TabIndex = 51;
            this.btnRemove.Tag = "btnH25WFlexi";
            this.btnRemove.Text = "&Remove Field";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // txtSection
            // 
            this.txtSection.Location = new System.Drawing.Point(317, 24);
            this.txtSection.Name = "txtSection";
            this.txtSection.ReadOnly = true;
            this.txtSection.Size = new System.Drawing.Size(198, 20);
            this.txtSection.TabIndex = 54;
            this.txtSection.Text = "Patient Exit Reasons";
            // 
            // txtFormName
            // 
            this.txtFormName.Location = new System.Drawing.Point(53, 23);
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.ReadOnly = true;
            this.txtFormName.Size = new System.Drawing.Size(198, 20);
            this.txtFormName.TabIndex = 45;
            this.txtFormName.Text = "Care End";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.SystemColors.Control;
            this.lblName.Location = new System.Drawing.Point(12, 27);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 55;
            this.lblName.Tag = "lblLabel";
            this.lblName.Text = "Name";
            // 
            // lblSection
            // 
            this.lblSection.AutoSize = true;
            this.lblSection.Location = new System.Drawing.Point(271, 25);
            this.lblSection.Name = "lblSection";
            this.lblSection.Size = new System.Drawing.Size(43, 13);
            this.lblSection.TabIndex = 56;
            this.lblSection.Tag = "lblLabel";
            this.lblSection.Text = "Section";
            // 
            // lblTechArea
            // 
            this.lblTechArea.AutoSize = true;
            this.lblTechArea.Location = new System.Drawing.Point(517, 27);
            this.lblTechArea.Name = "lblTechArea";
            this.lblTechArea.Size = new System.Drawing.Size(43, 13);
            this.lblTechArea.TabIndex = 57;
            this.lblTechArea.Tag = "lblLabel";
            this.lblTechArea.Text = "Service";
            // 
            // pnl_maingrd
            // 
            this.pnl_maingrd.Controls.Add(this.dgwCareEnded);
            this.pnl_maingrd.Controls.Add(this.btnUp);
            this.pnl_maingrd.Controls.Add(this.btnDown);
            this.pnl_maingrd.Controls.Add(this.label1);
            this.pnl_maingrd.Location = new System.Drawing.Point(2, 50);
            this.pnl_maingrd.Name = "pnl_maingrd";
            this.pnl_maingrd.Size = new System.Drawing.Size(130, 101);
            this.pnl_maingrd.TabIndex = 58;
            // 
            // dgwCareEnded
            // 
            this.dgwCareEnded.AllowUserToDeleteRows = false;
            this.dgwCareEnded.AllowUserToResizeColumns = false;
            this.dgwCareEnded.AllowUserToResizeRows = false;
            this.dgwCareEnded.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwCareEnded.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgwCareEnded.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwCareEnded.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgwCareEnded.Location = new System.Drawing.Point(4, 3);
            this.dgwCareEnded.Name = "dgwCareEnded";
            this.dgwCareEnded.Size = new System.Drawing.Size(86, 95);
            this.dgwCareEnded.TabIndex = 42;
            this.dgwCareEnded.Tag = "dgwDataGridView";
            this.dgwCareEnded.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwCareEnded_CellClick);
            this.dgwCareEnded.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwCareEnded_CellFormatting);
            this.dgwCareEnded.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgwCareEnded_DataError);
            this.dgwCareEnded.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgwCareEnded_EditingControlShowing);
            // 
            // pnl_ReasonSelect
            // 
            this.pnl_ReasonSelect.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_ReasonSelect.Controls.Add(this.btnSubmit);
            this.pnl_ReasonSelect.Controls.Add(this.btnReasonClose);
            this.pnl_ReasonSelect.Controls.Add(this.btnConditionalField);
            this.pnl_ReasonSelect.Controls.Add(this.btnAddReason);
            this.pnl_ReasonSelect.Controls.Add(this.txtAddReason);
            this.pnl_ReasonSelect.Controls.Add(this.btnUnSelect);
            this.pnl_ReasonSelect.Controls.Add(this.btnSelect);
            this.pnl_ReasonSelect.Controls.Add(this.lstSelExitReason);
            this.pnl_ReasonSelect.Controls.Add(this.lstExitReason);
            this.pnl_ReasonSelect.Controls.Add(this.label2);
            this.pnl_ReasonSelect.Location = new System.Drawing.Point(156, 124);
            this.pnl_ReasonSelect.Name = "pnl_ReasonSelect";
            this.pnl_ReasonSelect.Size = new System.Drawing.Size(504, 296);
            this.pnl_ReasonSelect.TabIndex = 59;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(170, 266);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnReasonClose
            // 
            this.btnReasonClose.Location = new System.Drawing.Point(264, 266);
            this.btnReasonClose.Name = "btnReasonClose";
            this.btnReasonClose.Size = new System.Drawing.Size(75, 23);
            this.btnReasonClose.TabIndex = 8;
            this.btnReasonClose.Text = "Cancel";
            this.btnReasonClose.UseVisualStyleBackColor = true;
            this.btnReasonClose.Click += new System.EventHandler(this.btnReasonClose_Click);
            // 
            // btnConditionalField
            // 
            this.btnConditionalField.Location = new System.Drawing.Point(343, 224);
            this.btnConditionalField.Name = "btnConditionalField";
            this.btnConditionalField.Size = new System.Drawing.Size(125, 23);
            this.btnConditionalField.TabIndex = 7;
            this.btnConditionalField.Text = "Conditional Fields";
            this.btnConditionalField.UseVisualStyleBackColor = true;
            this.btnConditionalField.Click += new System.EventHandler(this.btnConditionalField_Click);
            // 
            // btnAddReason
            // 
            this.btnAddReason.Location = new System.Drawing.Point(168, 225);
            this.btnAddReason.Name = "btnAddReason";
            this.btnAddReason.Size = new System.Drawing.Size(75, 23);
            this.btnAddReason.TabIndex = 6;
            this.btnAddReason.Text = "Add";
            this.btnAddReason.UseVisualStyleBackColor = true;
            this.btnAddReason.Click += new System.EventHandler(this.btnAddReason_Click);
            // 
            // txtAddReason
            // 
            this.txtAddReason.Location = new System.Drawing.Point(4, 227);
            this.txtAddReason.Name = "txtAddReason";
            this.txtAddReason.Size = new System.Drawing.Size(160, 20);
            this.txtAddReason.TabIndex = 5;
            // 
            // btnUnSelect
            // 
            this.btnUnSelect.Location = new System.Drawing.Point(213, 130);
            this.btnUnSelect.Name = "btnUnSelect";
            this.btnUnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnUnSelect.TabIndex = 4;
            this.btnUnSelect.Text = "<<";
            this.btnUnSelect.UseVisualStyleBackColor = true;
            this.btnUnSelect.Click += new System.EventHandler(this.btnUnSelect_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(212, 83);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 3;
            this.btnSelect.Text = ">>";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lstSelExitReason
            // 
            this.lstSelExitReason.FormattingEnabled = true;
            this.lstSelExitReason.Location = new System.Drawing.Point(302, 21);
            this.lstSelExitReason.Name = "lstSelExitReason";
            this.lstSelExitReason.Size = new System.Drawing.Size(194, 199);
            this.lstSelExitReason.TabIndex = 2;
            // 
            // lstExitReason
            // 
            this.lstExitReason.FormattingEnabled = true;
            this.lstExitReason.Location = new System.Drawing.Point(4, 21);
            this.lstExitReason.Name = "lstExitReason";
            this.lstExitReason.Size = new System.Drawing.Size(194, 199);
            this.lstExitReason.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(-2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(501, 17);
            this.label2.TabIndex = 0;
            this.label2.Tag = "lblHeaderLabel";
            this.label2.Text = "Exit Reason";
            // 
            // pnlCondFields
            // 
            this.pnlCondFields.Controls.Add(this.btnBack);
            this.pnlCondFields.Controls.Add(this.label4);
            this.pnlCondFields.Controls.Add(this.cmbExitReason);
            this.pnlCondFields.Controls.Add(this.dgwExitReason);
            this.pnlCondFields.Controls.Add(this.theUPExitReason);
            this.pnlCondFields.Controls.Add(this.theDownExitreason);
            this.pnlCondFields.Controls.Add(this.label3);
            this.pnlCondFields.Location = new System.Drawing.Point(542, 56);
            this.pnlCondFields.Name = "pnlCondFields";
            this.pnlCondFields.Size = new System.Drawing.Size(276, 137);
            this.pnlCondFields.TabIndex = 60;
            // 
            // btnBack
            // 
            this.btnBack.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnBack.Location = new System.Drawing.Point(106, 104);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 51;
            this.btnBack.Text = "Submit";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 50;
            this.label4.Text = "Exit Reason";
            // 
            // cmbExitReason
            // 
            this.cmbExitReason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExitReason.FormattingEnabled = true;
            this.cmbExitReason.Location = new System.Drawing.Point(84, 6);
            this.cmbExitReason.Name = "cmbExitReason";
            this.cmbExitReason.Size = new System.Drawing.Size(150, 21);
            this.cmbExitReason.TabIndex = 49;
            this.cmbExitReason.SelectedValueChanged += new System.EventHandler(this.cmbExitReason_SelectedValueChanged);
            this.cmbExitReason.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbExitReason_KeyDown);
            // 
            // dgwExitReason
            // 
            this.dgwExitReason.AllowUserToDeleteRows = false;
            this.dgwExitReason.AllowUserToResizeColumns = false;
            this.dgwExitReason.AllowUserToResizeRows = false;
            this.dgwExitReason.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgwExitReason.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgwExitReason.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwExitReason.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgwExitReason.Location = new System.Drawing.Point(3, 33);
            this.dgwExitReason.Name = "dgwExitReason";
            this.dgwExitReason.Size = new System.Drawing.Size(226, 53);
            this.dgwExitReason.TabIndex = 42;
            this.dgwExitReason.Tag = "dgwDataGridView";
            this.dgwExitReason.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwExitReason_CellClick);
            this.dgwExitReason.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwExitReason_CellContentClick);
            this.dgwExitReason.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwExitReason_CellFormatting);
            this.dgwExitReason.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgwExitReason_DataError);
            this.dgwExitReason.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgwExitReason_EditingControlShowing);
            // 
            // theUPExitReason
            // 
            this.theUPExitReason.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.theUPExitReason.Image = ((System.Drawing.Image)(resources.GetObject("theUPExitReason.Image")));
            this.theUPExitReason.Location = new System.Drawing.Point(236, 21);
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
            this.theDownExitreason.Location = new System.Drawing.Point(236, 90);
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
            this.label3.Location = new System.Drawing.Point(231, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 48;
            this.label3.Tag = "lblLabel";
            this.label3.Text = "Move";
            // 
            // cmbTechnicalArea
            // 
            this.cmbTechnicalArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTechnicalArea.FormattingEnabled = true;
            this.cmbTechnicalArea.Location = new System.Drawing.Point(602, 23);
            this.cmbTechnicalArea.Name = "cmbTechnicalArea";
            this.cmbTechnicalArea.Size = new System.Drawing.Size(205, 21);
            this.cmbTechnicalArea.TabIndex = 52;
            this.cmbTechnicalArea.Tag = "ddlDropDownList";
            // 
            // pnl_DeathReasonSelect
            // 
            this.pnl_DeathReasonSelect.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_DeathReasonSelect.Controls.Add(this.btnDeathSubmit);
            this.pnl_DeathReasonSelect.Controls.Add(this.btnDeathReasonClose);
            this.pnl_DeathReasonSelect.Controls.Add(this.btnDeathConditionalField);
            this.pnl_DeathReasonSelect.Controls.Add(this.btnAddDeathReason);
            this.pnl_DeathReasonSelect.Controls.Add(this.txtAddDeathReason);
            this.pnl_DeathReasonSelect.Controls.Add(this.btnDeathUnSelect);
            this.pnl_DeathReasonSelect.Controls.Add(this.btnDeathSelect);
            this.pnl_DeathReasonSelect.Controls.Add(this.lstSelDeathReason);
            this.pnl_DeathReasonSelect.Controls.Add(this.lstDeathReason);
            this.pnl_DeathReasonSelect.Controls.Add(this.label5);
            this.pnl_DeathReasonSelect.Location = new System.Drawing.Point(157, 148);
            this.pnl_DeathReasonSelect.Name = "pnl_DeathReasonSelect";
            this.pnl_DeathReasonSelect.Size = new System.Drawing.Size(504, 296);
            this.pnl_DeathReasonSelect.TabIndex = 61;
            // 
            // btnDeathSubmit
            // 
            this.btnDeathSubmit.Location = new System.Drawing.Point(170, 266);
            this.btnDeathSubmit.Name = "btnDeathSubmit";
            this.btnDeathSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnDeathSubmit.TabIndex = 9;
            this.btnDeathSubmit.Text = "Submit";
            this.btnDeathSubmit.UseVisualStyleBackColor = true;
            this.btnDeathSubmit.Click += new System.EventHandler(this.btnDeathSubmit_Click);
            // 
            // btnDeathReasonClose
            // 
            this.btnDeathReasonClose.Location = new System.Drawing.Point(264, 266);
            this.btnDeathReasonClose.Name = "btnDeathReasonClose";
            this.btnDeathReasonClose.Size = new System.Drawing.Size(75, 23);
            this.btnDeathReasonClose.TabIndex = 8;
            this.btnDeathReasonClose.Text = "Cancel";
            this.btnDeathReasonClose.UseVisualStyleBackColor = true;
            this.btnDeathReasonClose.Click += new System.EventHandler(this.btnDeathReasonClose_Click);
            // 
            // btnDeathConditionalField
            // 
            this.btnDeathConditionalField.Location = new System.Drawing.Point(343, 224);
            this.btnDeathConditionalField.Name = "btnDeathConditionalField";
            this.btnDeathConditionalField.Size = new System.Drawing.Size(125, 23);
            this.btnDeathConditionalField.TabIndex = 7;
            this.btnDeathConditionalField.Text = "Conditional Fields";
            this.btnDeathConditionalField.UseVisualStyleBackColor = true;
            this.btnDeathConditionalField.Click += new System.EventHandler(this.btnDeathConditionalField_Click);
            // 
            // btnAddDeathReason
            // 
            this.btnAddDeathReason.Location = new System.Drawing.Point(168, 225);
            this.btnAddDeathReason.Name = "btnAddDeathReason";
            this.btnAddDeathReason.Size = new System.Drawing.Size(75, 23);
            this.btnAddDeathReason.TabIndex = 6;
            this.btnAddDeathReason.Text = "Add";
            this.btnAddDeathReason.UseVisualStyleBackColor = true;
            this.btnAddDeathReason.Click += new System.EventHandler(this.btnAddDeathReason_Click);
            // 
            // txtAddDeathReason
            // 
            this.txtAddDeathReason.Location = new System.Drawing.Point(4, 227);
            this.txtAddDeathReason.Name = "txtAddDeathReason";
            this.txtAddDeathReason.Size = new System.Drawing.Size(160, 20);
            this.txtAddDeathReason.TabIndex = 5;
            // 
            // btnDeathUnSelect
            // 
            this.btnDeathUnSelect.Location = new System.Drawing.Point(213, 130);
            this.btnDeathUnSelect.Name = "btnDeathUnSelect";
            this.btnDeathUnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnDeathUnSelect.TabIndex = 4;
            this.btnDeathUnSelect.Text = "<<";
            this.btnDeathUnSelect.UseVisualStyleBackColor = true;
            this.btnDeathUnSelect.Click += new System.EventHandler(this.btnDeathUnSelect_Click);
            // 
            // btnDeathSelect
            // 
            this.btnDeathSelect.Location = new System.Drawing.Point(212, 83);
            this.btnDeathSelect.Name = "btnDeathSelect";
            this.btnDeathSelect.Size = new System.Drawing.Size(75, 23);
            this.btnDeathSelect.TabIndex = 3;
            this.btnDeathSelect.Text = ">>";
            this.btnDeathSelect.UseVisualStyleBackColor = true;
            this.btnDeathSelect.Click += new System.EventHandler(this.btnDeathSelect_Click);
            // 
            // lstSelDeathReason
            // 
            this.lstSelDeathReason.FormattingEnabled = true;
            this.lstSelDeathReason.Location = new System.Drawing.Point(302, 21);
            this.lstSelDeathReason.Name = "lstSelDeathReason";
            this.lstSelDeathReason.Size = new System.Drawing.Size(194, 199);
            this.lstSelDeathReason.TabIndex = 2;
            // 
            // lstDeathReason
            // 
            this.lstDeathReason.FormattingEnabled = true;
            this.lstDeathReason.Location = new System.Drawing.Point(4, 21);
            this.lstDeathReason.Name = "lstDeathReason";
            this.lstDeathReason.Size = new System.Drawing.Size(194, 199);
            this.lstDeathReason.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label5.ForeColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(-2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(501, 17);
            this.label5.TabIndex = 0;
            this.label5.Tag = "lblHeaderLabel";
            this.label5.Text = "Death Reason";
            // 
            // frmCareEnded
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 510);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.pnl_DeathReasonSelect);
            this.Controls.Add(this.pnl_ReasonSelect);
            this.Controls.Add(this.cmbTechnicalArea);
            this.Controls.Add(this.pnlCondFields);
            this.Controls.Add(this.pnl_maingrd);
            this.Controls.Add(this.lblTechArea);
            this.Controls.Add(this.lblSection);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtSection);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.txtFormName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCareEnded";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "Care Ended";
            this.Load += new System.EventHandler(this.frmCareEnded_Load);
            this.pnl_maingrd.ResumeLayout(false);
            this.pnl_maingrd.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwCareEnded)).EndInit();
            this.pnl_ReasonSelect.ResumeLayout(false);
            this.pnl_ReasonSelect.PerformLayout();
            this.pnlCondFields.ResumeLayout(false);
            this.pnlCondFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwExitReason)).EndInit();
            this.pnl_DeathReasonSelect.ResumeLayout(false);
            this.pnl_DeathReasonSelect.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.TextBox txtSection;
        private System.Windows.Forms.TextBox txtFormName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblSection;
        private System.Windows.Forms.Label lblTechArea;
        private System.Windows.Forms.Panel pnl_maingrd;
        private System.Windows.Forms.DataGridView dgwCareEnded;
        private System.Windows.Forms.Panel pnl_ReasonSelect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnReasonClose;
        private System.Windows.Forms.Button btnConditionalField;
        private System.Windows.Forms.Button btnAddReason;
        private System.Windows.Forms.TextBox txtAddReason;
        private System.Windows.Forms.Button btnUnSelect;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.ListBox lstSelExitReason;
        private System.Windows.Forms.ListBox lstExitReason;
        private System.Windows.Forms.Panel pnlCondFields;
        private System.Windows.Forms.DataGridView dgwExitReason;
        private System.Windows.Forms.Button theUPExitReason;
        private System.Windows.Forms.Button theDownExitreason;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbExitReason;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ComboBox cmbTechnicalArea;
        private System.Windows.Forms.Panel pnl_DeathReasonSelect;
        private System.Windows.Forms.Button btnDeathSubmit;
        private System.Windows.Forms.Button btnDeathReasonClose;
        private System.Windows.Forms.Button btnDeathConditionalField;
        private System.Windows.Forms.Button btnAddDeathReason;
        private System.Windows.Forms.TextBox txtAddDeathReason;
        private System.Windows.Forms.Button btnDeathUnSelect;
        private System.Windows.Forms.Button btnDeathSelect;
        private System.Windows.Forms.ListBox lstSelDeathReason;
        private System.Windows.Forms.ListBox lstDeathReason;
        private System.Windows.Forms.Label label5;
    }
}