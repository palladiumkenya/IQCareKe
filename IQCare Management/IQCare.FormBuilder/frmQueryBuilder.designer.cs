namespace IQCare.FormBuilder
{
    partial class frmQueryBuilder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmQueryBuilder));
            this.plainTextSQLBuilder1 = new ActiveDatabaseSoftware.ActiveQueryBuilder.PlainTextSQLBuilder(this.components);
            this.QBControl = new ActiveDatabaseSoftware.ActiveQueryBuilder.QueryBuilder();
            this.expressionEditor1 = new ActiveDatabaseSoftware.ExpressionEditor.ExpressionEditor(this.components);
            this.mssqlMetadataProvider1 = new ActiveDatabaseSoftware.ActiveQueryBuilder.MSSQLMetadataProvider(this.components);
            this.mssqlSyntaxProvider1 = new ActiveDatabaseSoftware.ActiveQueryBuilder.MSSQLSyntaxProvider(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tbQBuilder = new System.Windows.Forms.TabControl();
            this.tbBldQuery = new System.Windows.Forms.TabPage();
            this.pnlSave = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtReportName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNewCategory = new System.Windows.Forms.TextBox();
            this.lblNewCat = new System.Windows.Forms.Label();
            this.btnNewCategory = new System.Windows.Forms.Button();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlOpen = new System.Windows.Forms.Panel();
            this.cmbOpRepName = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdOpCancel = new System.Windows.Forms.Button();
            this.cmdOpOk = new System.Windows.Forms.Button();
            this.cmbOpCategory = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtQBQuery = new ActiveDatabaseSoftware.ExpressionEditor.SqlTextEditor();
            this.tbPreview = new System.Windows.Forms.TabPage();
            this.dgQResult = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnReportExport = new System.Windows.Forms.ToolStripButton();
            this.btnImportReport = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            this.tbQBuilder.SuspendLayout();
            this.tbBldQuery.SuspendLayout();
            this.pnlSave.SuspendLayout();
            this.pnlOpen.SuspendLayout();
            this.tbPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgQResult)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // plainTextSQLBuilder1
            // 
            this.plainTextSQLBuilder1.DynamicIndents = false;
            this.plainTextSQLBuilder1.DynamicRightMargin = false;
            this.plainTextSQLBuilder1.ExpressionSubqueryFormat.FromClauseFormat.NewLineAfterDatasource = false;
            this.plainTextSQLBuilder1.ExpressionSubqueryFormat.MainPartsFromNewLine = false;
            this.plainTextSQLBuilder1.QueryBuilder = this.QBControl;
            this.plainTextSQLBuilder1.SQLUpdated += new System.EventHandler(this.QBControl_SQLUpdated);
            // 
            // QBControl
            // 
            this.QBControl.AddObjectFormOptions.MinimumSize = new System.Drawing.Size(430, 430);
            this.QBControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.QBControl.DatabaseSchemaTreeOptions.BackColor = System.Drawing.SystemColors.Window;
            this.QBControl.DatabaseSchemaTreeOptions.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QBControl.DatabaseSchemaTreeOptions.TextColor = System.Drawing.SystemColors.WindowText;
            this.QBControl.DataSourceOptions.BackgroundColor = System.Drawing.SystemColors.Window;
            this.QBControl.DataSourceOptions.DescriptionColumnOptions.Color = System.Drawing.Color.LightBlue;
            this.QBControl.DataSourceOptions.FocusedBackgroundColor = System.Drawing.SystemColors.Window;
            this.QBControl.DataSourceOptions.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QBControl.DataSourceOptions.MarkColumnOptions.PrimaryKeyIcon = ((System.Drawing.Image)(resources.GetObject("resource.PrimaryKeyIcon")));
            this.QBControl.DataSourceOptions.NameColumnOptions.Color = System.Drawing.SystemColors.WindowText;
            this.QBControl.DataSourceOptions.NameColumnOptions.PrimaryKeyColor = System.Drawing.SystemColors.WindowText;
            this.QBControl.DataSourceOptions.TypeColumnOptions.Color = System.Drawing.SystemColors.GrayText;
            this.QBControl.DesignPaneOptions.BackColor = System.Drawing.SystemColors.Window;
            this.QBControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QBControl.ExpressionEditor = this.expressionEditor1;
            this.QBControl.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QBControl.Location = new System.Drawing.Point(0, 0);
            this.QBControl.MetadataProvider = this.mssqlMetadataProvider1;
            this.QBControl.MetadataStructureOptions.ProceduresFolderText = "Procedures";
            this.QBControl.MetadataStructureOptions.SynonymsFolderText = "Synonyms";
            this.QBControl.MetadataStructureOptions.TablesFolderText = "Tables";
            this.QBControl.MetadataStructureOptions.ViewsFolderText = "Views";
            this.QBControl.Name = "QBControl";
            this.QBControl.QueryColumnListOptions.AlternateRowColor = System.Drawing.SystemColors.Window;
            this.QBControl.QueryColumnListOptions.BackColor = System.Drawing.SystemColors.Window;
            this.QBControl.QueryColumnListOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QBControl.QueryColumnListOptions.TextColor = System.Drawing.SystemColors.WindowText;
            this.QBControl.QueryStructureTreeOptions.BackColor = System.Drawing.SystemColors.Window;
            this.QBControl.QueryStructureTreeOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QBControl.QueryStructureTreeOptions.QueriesImageIndex = 17;
            this.QBControl.QueryStructureTreeOptions.TextColor = System.Drawing.SystemColors.WindowText;
            this.QBControl.Size = new System.Drawing.Size(861, 386);
            this.QBControl.SyntaxProvider = this.mssqlSyntaxProvider1;
            this.QBControl.TabIndex = 1;
            this.QBControl.Tag = "treeView";
            this.QBControl.SQLUpdated += new System.EventHandler(this.QBControl_SQLUpdated);
            this.QBControl.Load += new System.EventHandler(this.QBControl_Load);
            // 
            // expressionEditor1
            // 
            this.expressionEditor1.ActiveUnionSubQuery = null;
            this.expressionEditor1.BackColor = System.Drawing.SystemColors.Window;
            this.expressionEditor1.CommentColor = System.Drawing.Color.Green;
            this.expressionEditor1.Expression = "";
            this.expressionEditor1.FunctionColor = System.Drawing.Color.Purple;
            this.expressionEditor1.Height = 530;
            this.expressionEditor1.InactiveSelectionBackColor = System.Drawing.SystemColors.InactiveCaption;
            this.expressionEditor1.KeywordColor = System.Drawing.Color.Blue;
            this.expressionEditor1.Left = 0;
            this.expressionEditor1.NumberColor = System.Drawing.Color.DarkCyan;
            this.expressionEditor1.QueryBuilder = this.QBControl;
            this.expressionEditor1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.expressionEditor1.SelectionTextColor = System.Drawing.SystemColors.HighlightText;
            this.expressionEditor1.ShowSuggestionAfterCharCount = 1;
            this.expressionEditor1.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.expressionEditor1.StringColor = System.Drawing.Color.DarkRed;
            this.expressionEditor1.TextColor = System.Drawing.SystemColors.WindowText;
            this.expressionEditor1.TextEditorFont = new System.Drawing.Font("Courier New", 9F);
            this.expressionEditor1.Top = 0;
            this.expressionEditor1.Width = 754;
            // 
            // mssqlMetadataProvider1
            // 
            this.mssqlMetadataProvider1.Connection = null;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.tbQBuilder);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(879, 535);
            this.panel1.TabIndex = 2;
            // 
            // tbQBuilder
            // 
            this.tbQBuilder.Controls.Add(this.tbBldQuery);
            this.tbQBuilder.Controls.Add(this.tbPreview);
            this.tbQBuilder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbQBuilder.Location = new System.Drawing.Point(0, 0);
            this.tbQBuilder.Name = "tbQBuilder";
            this.tbQBuilder.SelectedIndex = 0;
            this.tbQBuilder.Size = new System.Drawing.Size(875, 531);
            this.tbQBuilder.TabIndex = 2;
            this.tbQBuilder.Click += new System.EventHandler(this.tbQBuilder_Click);
            // 
            // tbBldQuery
            // 
            this.tbBldQuery.Controls.Add(this.splitContainer1);
            this.tbBldQuery.Location = new System.Drawing.Point(4, 22);
            this.tbBldQuery.Name = "tbBldQuery";
            this.tbBldQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tbBldQuery.Size = new System.Drawing.Size(867, 505);
            this.tbBldQuery.TabIndex = 0;
            this.tbBldQuery.Text = "Build Query";
            this.tbBldQuery.UseVisualStyleBackColor = true;
            // 
            // pnlSave
            // 
            this.pnlSave.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSave.Controls.Add(this.label1);
            this.pnlSave.Controls.Add(this.btnCancel);
            this.pnlSave.Controls.Add(this.btnOk);
            this.pnlSave.Controls.Add(this.txtReportName);
            this.pnlSave.Controls.Add(this.label5);
            this.pnlSave.Controls.Add(this.txtNewCategory);
            this.pnlSave.Controls.Add(this.lblNewCat);
            this.pnlSave.Controls.Add(this.btnNewCategory);
            this.pnlSave.Controls.Add(this.cmbCategory);
            this.pnlSave.Controls.Add(this.label4);
            this.pnlSave.Location = new System.Drawing.Point(439, 15);
            this.pnlSave.Name = "pnlSave";
            this.pnlSave.Size = new System.Drawing.Size(387, 205);
            this.pnlSave.TabIndex = 4;
            this.pnlSave.Tag = "pnlPanel";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Blue;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(383, 22);
            this.label1.TabIndex = 15;
            this.label1.Tag = "";
            this.label1.Text = "Save Report";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(164, 151);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(74, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Tag = "btnSingleText";
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(84, 151);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(74, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Tag = "btnSingleText";
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtReportName
            // 
            this.txtReportName.Location = new System.Drawing.Point(100, 102);
            this.txtReportName.Name = "txtReportName";
            this.txtReportName.Size = new System.Drawing.Size(194, 20);
            this.txtReportName.TabIndex = 1;
            this.txtReportName.Tag = "txtTextBox";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 14;
            this.label5.Tag = "lblLabel";
            this.label5.Text = "ReportName :";
            // 
            // txtNewCategory
            // 
            this.txtNewCategory.Location = new System.Drawing.Point(100, 68);
            this.txtNewCategory.Name = "txtNewCategory";
            this.txtNewCategory.Size = new System.Drawing.Size(194, 20);
            this.txtNewCategory.TabIndex = 1;
            this.txtNewCategory.Tag = "txtTextBox";
            // 
            // lblNewCat
            // 
            this.lblNewCat.AutoSize = true;
            this.lblNewCat.Location = new System.Drawing.Point(12, 71);
            this.lblNewCat.Name = "lblNewCat";
            this.lblNewCat.Size = new System.Drawing.Size(80, 13);
            this.lblNewCat.TabIndex = 12;
            this.lblNewCat.Tag = "lblLabel";
            this.lblNewCat.Text = "New Category :";
            // 
            // btnNewCategory
            // 
            this.btnNewCategory.Location = new System.Drawing.Point(297, 38);
            this.btnNewCategory.Name = "btnNewCategory";
            this.btnNewCategory.Size = new System.Drawing.Size(30, 23);
            this.btnNewCategory.TabIndex = 11;
            this.btnNewCategory.Tag = "";
            this.btnNewCategory.Text = "...";
            this.btnNewCategory.UseVisualStyleBackColor = true;
            this.btnNewCategory.Click += new System.EventHandler(this.btnNewCategory_Click);
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Location = new System.Drawing.Point(100, 40);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(194, 21);
            this.cmbCategory.TabIndex = 0;
            this.cmbCategory.Tag = "ddlDropDownList";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 9;
            this.label4.Tag = "lblLabel";
            this.label4.Text = "ReportCategory :";
            // 
            // pnlOpen
            // 
            this.pnlOpen.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlOpen.Controls.Add(this.cmbOpRepName);
            this.pnlOpen.Controls.Add(this.label3);
            this.pnlOpen.Controls.Add(this.label2);
            this.pnlOpen.Controls.Add(this.cmdOpCancel);
            this.pnlOpen.Controls.Add(this.cmdOpOk);
            this.pnlOpen.Controls.Add(this.cmbOpCategory);
            this.pnlOpen.Controls.Add(this.label7);
            this.pnlOpen.Location = new System.Drawing.Point(0, 0);
            this.pnlOpen.Name = "pnlOpen";
            this.pnlOpen.Size = new System.Drawing.Size(387, 205);
            this.pnlOpen.TabIndex = 5;
            this.pnlOpen.Tag = "pnlPanel";
            // 
            // cmbOpRepName
            // 
            this.cmbOpRepName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpRepName.FormattingEnabled = true;
            this.cmbOpRepName.Location = new System.Drawing.Point(100, 82);
            this.cmbOpRepName.Name = "cmbOpRepName";
            this.cmbOpRepName.Size = new System.Drawing.Size(221, 21);
            this.cmbOpRepName.TabIndex = 16;
            this.cmbOpRepName.Tag = "ddlDropDownList";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 17;
            this.label3.Tag = "lblLabel";
            this.label3.Text = "Report Name :";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.Blue;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(383, 22);
            this.label2.TabIndex = 15;
            this.label2.Tag = "";
            this.label2.Text = "Open Report";
            // 
            // cmdOpCancel
            // 
            this.cmdOpCancel.Location = new System.Drawing.Point(206, 152);
            this.cmdOpCancel.Name = "cmdOpCancel";
            this.cmdOpCancel.Size = new System.Drawing.Size(74, 23);
            this.cmdOpCancel.TabIndex = 3;
            this.cmdOpCancel.Tag = "btnSingleText";
            this.cmdOpCancel.Text = "Cancel";
            this.cmdOpCancel.UseVisualStyleBackColor = true;
            this.cmdOpCancel.Click += new System.EventHandler(this.cmdOpCancel_Click);
            // 
            // cmdOpOk
            // 
            this.cmdOpOk.Location = new System.Drawing.Point(126, 152);
            this.cmdOpOk.Name = "cmdOpOk";
            this.cmdOpOk.Size = new System.Drawing.Size(74, 23);
            this.cmdOpOk.TabIndex = 2;
            this.cmdOpOk.Tag = "btnSingleText";
            this.cmdOpOk.Text = "Ok";
            this.cmdOpOk.UseVisualStyleBackColor = true;
            this.cmdOpOk.Click += new System.EventHandler(this.cmdOpOk_Click);
            // 
            // cmbOpCategory
            // 
            this.cmbOpCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOpCategory.FormattingEnabled = true;
            this.cmbOpCategory.Location = new System.Drawing.Point(100, 40);
            this.cmbOpCategory.Name = "cmbOpCategory";
            this.cmbOpCategory.Size = new System.Drawing.Size(221, 21);
            this.cmbOpCategory.TabIndex = 0;
            this.cmbOpCategory.Tag = "ddlDropDownList";
            this.cmbOpCategory.SelectedValueChanged += new System.EventHandler(this.cmbOpCategory_SelectedValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 13);
            this.label7.TabIndex = 9;
            this.label7.Tag = "lblLabel";
            this.label7.Text = "Report Category :";
            // 
            // txtQBQuery
            // 
            this.txtQBQuery.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtQBQuery.CommentColor = System.Drawing.Color.Green;
            this.txtQBQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtQBQuery.FunctionColor = System.Drawing.Color.Purple;
            this.txtQBQuery.InactiveSelectionBackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtQBQuery.KeywordColor = System.Drawing.Color.Blue;
            this.txtQBQuery.Location = new System.Drawing.Point(0, 0);
            this.txtQBQuery.Name = "txtQBQuery";
            this.txtQBQuery.NumberColor = System.Drawing.Color.DarkCyan;
            this.txtQBQuery.QueryBuilder = this.QBControl;
            this.txtQBQuery.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.txtQBQuery.SelectionTextColor = System.Drawing.SystemColors.HighlightText;
            this.txtQBQuery.Size = new System.Drawing.Size(861, 109);
            this.txtQBQuery.StringColor = System.Drawing.Color.DarkRed;
            this.txtQBQuery.TabIndex = 6;
            this.txtQBQuery.Text = "sqlTextEditor1";
            this.txtQBQuery.TextColor = System.Drawing.SystemColors.WindowText;
            this.txtQBQuery.TextPadding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.txtQBQuery.Leave += new System.EventHandler(this.txtQBQuery2_Leave);
            // 
            // tbPreview
            // 
            this.tbPreview.Controls.Add(this.dgQResult);
            this.tbPreview.Location = new System.Drawing.Point(4, 22);
            this.tbPreview.Name = "tbPreview";
            this.tbPreview.Padding = new System.Windows.Forms.Padding(3);
            this.tbPreview.Size = new System.Drawing.Size(867, 505);
            this.tbPreview.TabIndex = 1;
            this.tbPreview.Text = "Preview";
            this.tbPreview.UseVisualStyleBackColor = true;
            // 
            // dgQResult
            // 
            this.dgQResult.BackgroundColor = System.Drawing.Color.White;
            this.dgQResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgQResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgQResult.Location = new System.Drawing.Point(3, 3);
            this.dgQResult.Name = "dgQResult";
            this.dgQResult.ReadOnly = true;
            this.dgQResult.Size = new System.Drawing.Size(861, 499);
            this.dgQResult.TabIndex = 0;
            this.dgQResult.Tag = "dgwDataGrid";
            this.dgQResult.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgQResult_DataError);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.btnExport,
            this.btnReportExport,
            this.btnImportReport,
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(879, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNew.Image = ((System.Drawing.Image)(resources.GetObject("btnNew.Image")));
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(35, 22);
            this.btnNew.Text = "New";
            this.btnNew.ToolTipText = "New Report";
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(40, 22);
            this.btnOpen.Text = "Open";
            this.btnOpen.ToolTipText = "Open Report";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.ForeColor = System.Drawing.Color.Black;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(35, 22);
            this.btnSave.Text = "Save";
            this.btnSave.ToolTipText = "Save Report";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(90, 22);
            this.btnExport.Text = "Export To Excel";
            this.btnExport.ToolTipText = "Export to Excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnReportExport
            // 
            this.btnReportExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnReportExport.Image = ((System.Drawing.Image)(resources.GetObject("btnReportExport.Image")));
            this.btnReportExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReportExport.Name = "btnReportExport";
            this.btnReportExport.Size = new System.Drawing.Size(82, 22);
            this.btnReportExport.Text = "Export Report";
            this.btnReportExport.Click += new System.EventHandler(this.btnReportExport_Click);
            // 
            // btnImportReport
            // 
            this.btnImportReport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnImportReport.Image = ((System.Drawing.Image)(resources.GetObject("btnImportReport.Image")));
            this.btnImportReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImportReport.Name = "btnImportReport";
            this.btnImportReport.Size = new System.Drawing.Size(85, 22);
            this.btnImportReport.Text = "Import Report";
            this.btnImportReport.Click += new System.EventHandler(this.btnImportReport_Click);
            // 
            // btnExit
            // 
            this.btnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnExit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(29, 22);
            this.btnExit.Text = "Exit";
            this.btnExit.ToolTipText = "Close";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlSave);
            this.splitContainer1.Panel1.Controls.Add(this.pnlOpen);
            this.splitContainer1.Panel1.Controls.Add(this.QBControl);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtQBQuery);
            this.splitContainer1.Size = new System.Drawing.Size(861, 499);
            this.splitContainer1.SplitterDistance = 386;
            this.splitContainer1.TabIndex = 7;
            // 
            // frmQueryBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(879, 560);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "frmQueryBuilder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "frmQueryBuilder";
            this.Load += new System.EventHandler(this.frmQueryBuilder_Load);
            this.panel1.ResumeLayout(false);
            this.tbQBuilder.ResumeLayout(false);
            this.tbBldQuery.ResumeLayout(false);
            this.pnlSave.ResumeLayout(false);
            this.pnlSave.PerformLayout();
            this.pnlOpen.ResumeLayout(false);
            this.pnlOpen.PerformLayout();
            this.tbPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgQResult)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ActiveDatabaseSoftware.ActiveQueryBuilder.PlainTextSQLBuilder plainTextSQLBuilder1;
        private ActiveDatabaseSoftware.ActiveQueryBuilder.MSSQLMetadataProvider mssqlMetadataProvider1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tbQBuilder;
        private System.Windows.Forms.TabPage tbBldQuery;
        private ActiveDatabaseSoftware.ActiveQueryBuilder.QueryBuilder QBControl;
        private System.Windows.Forms.TabPage tbPreview;
        private System.Windows.Forms.DataGridView dgQResult;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.Panel pnlSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.TextBox txtReportName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNewCategory;
        private System.Windows.Forms.Label lblNewCat;
        private System.Windows.Forms.Button btnNewCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlOpen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdOpCancel;
        private System.Windows.Forms.Button cmdOpOk;
        private System.Windows.Forms.ComboBox cmbOpCategory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbOpRepName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton btnReportExport;
        private System.Windows.Forms.ToolStripButton btnImportReport;
        private ActiveDatabaseSoftware.ExpressionEditor.SqlTextEditor txtQBQuery;
        private ActiveDatabaseSoftware.ExpressionEditor.ExpressionEditor expressionEditor1;
        private ActiveDatabaseSoftware.ActiveQueryBuilder.MSSQLSyntaxProvider mssqlSyntaxProvider1;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
