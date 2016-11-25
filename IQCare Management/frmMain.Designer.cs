using System.Configuration;
using System;
using System.Windows.Forms;
namespace IQCare.Management
{
    partial class frmMain
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
            System.Windows.Forms.ToolStripStatusLabel lblIcon;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnuServiceAdmin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDBOperations = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUpsize = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDBMigration = new System.Windows.Forms.ToolStripMenuItem();
            this.sCMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDBMerge = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRefereshSystemCache = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRebuildCustomReportDB = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIQCareDBMaintenance = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManageModule = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFormBuilder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManageForms = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSplFormLinking = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManageFields = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManageCareEndedFields = new System.Windows.Forms.ToolStripMenuItem();
            this.manageRegistrationFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewFieldAsscociation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuImportExportForms = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfigureHomePageForms = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuConfigCareTermination = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReportFieldValidation = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridviewFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQueryBuilder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPMSCM = new System.Windows.Forms.ToolStripMenuItem();
            this.masterListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPatientDrugDispense = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPurchaseOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGoodReceivedNote = new System.Windows.Forms.ToolStripMenuItem();
            this.counterRequisitionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.issueVoucherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuOpeningStock = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdjustStock = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDisposeItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBatchSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStockSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExpiryReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuConfigureBudget = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuVisitConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBudgetView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOthers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCalculator = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNotepad = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printPreviewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCopyRight = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            lblIcon = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIcon
            // 
            lblIcon.AutoSize = false;
            lblIcon.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lblIcon.BackgroundImage")));
            lblIcon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            lblIcon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            lblIcon.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            lblIcon.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            lblIcon.Name = "lblIcon";
            lblIcon.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            lblIcon.Size = new System.Drawing.Size(60, 17);
            lblIcon.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            lblIcon.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuServiceAdmin,
            this.mnuDBOperations,
            this.mnuManageModule,
            this.mnuFormBuilder,
            this.mnuQueryBuilder,
            this.mnuPMSCM,
            this.mnuHelp,
            this.mnuOthers,
            this.logoutToolStripMenuItem,
            this.mnuExit,
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(778, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // mnuServiceAdmin
            // 
            this.mnuServiceAdmin.Name = "mnuServiceAdmin";
            this.mnuServiceAdmin.Size = new System.Drawing.Size(138, 20);
            this.mnuServiceAdmin.Text = "&Service Administration";
            this.mnuServiceAdmin.Visible = false;
            this.mnuServiceAdmin.Click += new System.EventHandler(this.mnuServiceAdmin_Click);
            // 
            // mnuDBOperations
            // 
            this.mnuDBOperations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUpsize,
            this.mnuDBMigration,
            this.sCMToolStripMenuItem,
            this.mnuDBMerge,
            this.toolStripSeparator1,
            this.mnuRefereshSystemCache,
            this.mnuRebuildCustomReportDB,
            this.mnuIQCareDBMaintenance});
            this.mnuDBOperations.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.mnuDBOperations.Name = "mnuDBOperations";
            this.mnuDBOperations.Size = new System.Drawing.Size(95, 20);
            this.mnuDBOperations.Text = "&DB Operations";
            // 
            // mnuUpsize
            // 
            this.mnuUpsize.ImageTransparentColor = System.Drawing.Color.Black;
            this.mnuUpsize.Name = "mnuUpsize";
            this.mnuUpsize.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.mnuUpsize.Size = new System.Drawing.Size(257, 22);
            this.mnuUpsize.Text = "&Upsize";
            this.mnuUpsize.Click += new System.EventHandler(this.mnuUpsize_Click);
            // 
            // mnuDBMigration
            // 
            this.mnuDBMigration.Name = "mnuDBMigration";
            this.mnuDBMigration.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.mnuDBMigration.Size = new System.Drawing.Size(257, 22);
            this.mnuDBMigration.Text = "Database M&igration";
            this.mnuDBMigration.Click += new System.EventHandler(this.mnuDBMigration_Click);
            // 
            // sCMToolStripMenuItem
            // 
            this.sCMToolStripMenuItem.Name = "sCMToolStripMenuItem";
            this.sCMToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.sCMToolStripMenuItem.Size = new System.Drawing.Size(257, 22);
            this.sCMToolStripMenuItem.Text = "Copy SCM Configuration";
            this.sCMToolStripMenuItem.Click += new System.EventHandler(this.sCMToolStripMenuItem_Click);
            // 
            // mnuDBMerge
            // 
            this.mnuDBMerge.Name = "mnuDBMerge";
            this.mnuDBMerge.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.mnuDBMerge.Size = new System.Drawing.Size(257, 22);
            this.mnuDBMerge.Text = "Database S&ynchronization";
            this.mnuDBMerge.Click += new System.EventHandler(this.mnuDBMerge_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(254, 6);
            // 
            // mnuRefereshSystemCache
            // 
            this.mnuRefereshSystemCache.Name = "mnuRefereshSystemCache";
            this.mnuRefereshSystemCache.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuRefereshSystemCache.Size = new System.Drawing.Size(257, 22);
            this.mnuRefereshSystemCache.Text = "Refresh &System Cache";
            this.mnuRefereshSystemCache.Click += new System.EventHandler(this.mnuRefereshSystemCache_Click);
            // 
            // mnuRebuildCustomReportDB
            // 
            this.mnuRebuildCustomReportDB.Name = "mnuRebuildCustomReportDB";
            this.mnuRebuildCustomReportDB.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.mnuRebuildCustomReportDB.Size = new System.Drawing.Size(257, 22);
            this.mnuRebuildCustomReportDB.Text = "Rebuild &Custom Report DB";
            this.mnuRebuildCustomReportDB.Click += new System.EventHandler(this.mnuRebuildCustomReportDB_Click);
            // 
            // mnuIQCareDBMaintenance
            // 
            this.mnuIQCareDBMaintenance.Name = "mnuIQCareDBMaintenance";
            this.mnuIQCareDBMaintenance.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mnuIQCareDBMaintenance.Size = new System.Drawing.Size(257, 22);
            this.mnuIQCareDBMaintenance.Text = "&IQCare DB Maintenance";
            this.mnuIQCareDBMaintenance.Click += new System.EventHandler(this.mnuIQCareDBMaintenance_Click);
            // 
            // mnuManageModule
            // 
            this.mnuManageModule.Name = "mnuManageModule";
            this.mnuManageModule.Size = new System.Drawing.Size(102, 20);
            this.mnuManageModule.Text = "Manage Service";
            this.mnuManageModule.Click += new System.EventHandler(this.mnuManageModule_Click);
            // 
            // mnuFormBuilder
            // 
            this.mnuFormBuilder.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuManageForms,
            this.mnuSplFormLinking,
            this.mnuManageFields,
            this.mnuManageCareEndedFields,
            this.manageRegistrationFieldsToolStripMenuItem,
            this.toolStripSeparator6,
            this.mnuViewFieldAsscociation,
            this.toolStripSeparator7,
            this.mnuImportExportForms,
            this.mnuConfigureHomePageForms,
            this.mnuConfigCareTermination,
            this.configureToolStripMenuItem,
            this.mnuReportFieldValidation,
            this.listViewFormToolStripMenuItem,
            this.gridviewFormToolStripMenuItem});
            this.mnuFormBuilder.Name = "mnuFormBuilder";
            this.mnuFormBuilder.Size = new System.Drawing.Size(87, 20);
            this.mnuFormBuilder.Text = "&Form Builder";
            // 
            // mnuManageForms
            // 
            this.mnuManageForms.ImageTransparentColor = System.Drawing.Color.Black;
            this.mnuManageForms.Name = "mnuManageForms";
            this.mnuManageForms.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuManageForms.Size = new System.Drawing.Size(274, 22);
            this.mnuManageForms.Text = "Manage &Forms";
            this.mnuManageForms.Click += new System.EventHandler(this.mnuManageForms_Click);
            // 
            // mnuSplFormLinking
            // 
            this.mnuSplFormLinking.Name = "mnuSplFormLinking";
            this.mnuSplFormLinking.Size = new System.Drawing.Size(274, 22);
            this.mnuSplFormLinking.Text = "Special Forms Linking";
            this.mnuSplFormLinking.Visible = false;
            this.mnuSplFormLinking.Click += new System.EventHandler(this.mnuSplFormLinking_Click);
            // 
            // mnuManageFields
            // 
            this.mnuManageFields.ImageTransparentColor = System.Drawing.Color.Black;
            this.mnuManageFields.Name = "mnuManageFields";
            this.mnuManageFields.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.mnuManageFields.Size = new System.Drawing.Size(274, 22);
            this.mnuManageFields.Text = "Manage F&ields";
            this.mnuManageFields.Click += new System.EventHandler(this.mnuManageFields_Click);
            // 
            // mnuManageCareEndedFields
            // 
            this.mnuManageCareEndedFields.Name = "mnuManageCareEndedFields";
            this.mnuManageCareEndedFields.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuManageCareEndedFields.Size = new System.Drawing.Size(274, 22);
            this.mnuManageCareEndedFields.Text = "Manage Care Ended Fields";
            this.mnuManageCareEndedFields.Click += new System.EventHandler(this.mnuManageCareEndedFields_Click);
            // 
            // manageRegistrationFieldsToolStripMenuItem
            // 
            this.manageRegistrationFieldsToolStripMenuItem.Name = "manageRegistrationFieldsToolStripMenuItem";
            this.manageRegistrationFieldsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.manageRegistrationFieldsToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.manageRegistrationFieldsToolStripMenuItem.Text = "Manage Registration Fields ";
            this.manageRegistrationFieldsToolStripMenuItem.Click += new System.EventHandler(this.manageRegistrationFieldsToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuViewFieldAsscociation
            // 
            this.mnuViewFieldAsscociation.ImageTransparentColor = System.Drawing.Color.Black;
            this.mnuViewFieldAsscociation.Name = "mnuViewFieldAsscociation";
            this.mnuViewFieldAsscociation.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.mnuViewFieldAsscociation.Size = new System.Drawing.Size(274, 22);
            this.mnuViewFieldAsscociation.Text = "&View Field Associations";
            this.mnuViewFieldAsscociation.Click += new System.EventHandler(this.mnuViewFieldAssociation_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(271, 6);
            // 
            // mnuImportExportForms
            // 
            this.mnuImportExportForms.Name = "mnuImportExportForms";
            this.mnuImportExportForms.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnuImportExportForms.Size = new System.Drawing.Size(274, 22);
            this.mnuImportExportForms.Text = "Import &Export Forms";
            this.mnuImportExportForms.Click += new System.EventHandler(this.mnuImportExportForms_Click);
            // 
            // mnuConfigureHomePageForms
            // 
            this.mnuConfigureHomePageForms.Name = "mnuConfigureHomePageForms";
            this.mnuConfigureHomePageForms.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.mnuConfigureHomePageForms.Size = new System.Drawing.Size(274, 22);
            this.mnuConfigureHomePageForms.Text = "Configure &Home Pages";
            this.mnuConfigureHomePageForms.Click += new System.EventHandler(this.mnuConfigureHomePageForms_Click);
            // 
            // mnuConfigCareTermination
            // 
            this.mnuConfigCareTermination.Name = "mnuConfigCareTermination";
            this.mnuConfigCareTermination.Size = new System.Drawing.Size(274, 22);
            this.mnuConfigCareTermination.Text = "Configure Care Ended";
            this.mnuConfigCareTermination.Click += new System.EventHandler(this.mnuConfigCareTermination_Click);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.ShortcutKeyDisplayString = "Ctrl+P";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.configureToolStripMenuItem.Text = "Configure Patient Registration";
            this.configureToolStripMenuItem.Click += new System.EventHandler(this.configureToolStripMenuItem_Click);
            // 
            // mnuReportFieldValidation
            // 
            this.mnuReportFieldValidation.Name = "mnuReportFieldValidation";
            this.mnuReportFieldValidation.Size = new System.Drawing.Size(274, 22);
            this.mnuReportFieldValidation.Text = "Report Field Validation";
            this.mnuReportFieldValidation.Click += new System.EventHandler(this.mnuReportFieldValidation_Click);
            // 
            // listViewFormToolStripMenuItem
            // 
            this.listViewFormToolStripMenuItem.Name = "listViewFormToolStripMenuItem";
            this.listViewFormToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.listViewFormToolStripMenuItem.Text = "ListViewForm";
            this.listViewFormToolStripMenuItem.Visible = false;
            this.listViewFormToolStripMenuItem.Click += new System.EventHandler(this.listViewFormToolStripMenuItem_Click);
            // 
            // gridviewFormToolStripMenuItem
            // 
            this.gridviewFormToolStripMenuItem.Name = "gridviewFormToolStripMenuItem";
            this.gridviewFormToolStripMenuItem.Size = new System.Drawing.Size(274, 22);
            this.gridviewFormToolStripMenuItem.Text = "GridviewForm";
            this.gridviewFormToolStripMenuItem.Visible = false;
            this.gridviewFormToolStripMenuItem.Click += new System.EventHandler(this.gridviewFormToolStripMenuItem_Click);
            // 
            // mnuQueryBuilder
            // 
            this.mnuQueryBuilder.Name = "mnuQueryBuilder";
            this.mnuQueryBuilder.Size = new System.Drawing.Size(91, 20);
            this.mnuQueryBuilder.Text = "&Query Builder";
            this.mnuQueryBuilder.Click += new System.EventHandler(this.mnuQueryBuilder_Click);
            // 
            // mnuPMSCM
            // 
            this.mnuPMSCM.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.masterListToolStripMenuItem,
            this.mnuPatientDrugDispense,
            this.toolStripSeparator10,
            this.mnuPurchaseOrder,
            this.mnuGoodReceivedNote,
            this.counterRequisitionToolStripMenuItem,
            this.issueVoucherToolStripMenuItem,
            this.toolStripSeparator12,
            this.mnuOpeningStock,
            this.mnuAdjustStock,
            this.mnuDisposeItem,
            this.mnuBatchSummary,
            this.mnuStockSummary,
            this.mnuExpiryReport,
            this.mnuReports,
            this.toolStripSeparator11,
            this.mnuConfigureBudget,
            this.mnuVisitConfiguration,
            this.mnuBudgetView});
            this.mnuPMSCM.Name = "mnuPMSCM";
            this.mnuPMSCM.Size = new System.Drawing.Size(67, 20);
            this.mnuPMSCM.Text = "PM/SCM";
            // 
            // masterListToolStripMenuItem
            // 
            this.masterListToolStripMenuItem.Name = "masterListToolStripMenuItem";
            this.masterListToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.masterListToolStripMenuItem.Text = "Master Lists";
            this.masterListToolStripMenuItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.masterListToolStripMenuItem.Click += new System.EventHandler(this.masterListToolStripMenuItem_Click);
            // 
            // mnuPatientDrugDispense
            // 
            this.mnuPatientDrugDispense.Name = "mnuPatientDrugDispense";
            this.mnuPatientDrugDispense.Size = new System.Drawing.Size(192, 22);
            this.mnuPatientDrugDispense.Text = "Dispense Drugs ";
            this.mnuPatientDrugDispense.Click += new System.EventHandler(this.mnuPatientDrugDispense_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(189, 6);
            // 
            // mnuPurchaseOrder
            // 
            this.mnuPurchaseOrder.Name = "mnuPurchaseOrder";
            this.mnuPurchaseOrder.Size = new System.Drawing.Size(192, 22);
            this.mnuPurchaseOrder.Text = "Purchase Order";
            this.mnuPurchaseOrder.Click += new System.EventHandler(this.purchaseOrderToolStripMenuItem_Click);
            // 
            // mnuGoodReceivedNote
            // 
            this.mnuGoodReceivedNote.Name = "mnuGoodReceivedNote";
            this.mnuGoodReceivedNote.Size = new System.Drawing.Size(192, 22);
            this.mnuGoodReceivedNote.Text = "Goods Received Note";
            this.mnuGoodReceivedNote.Click += new System.EventHandler(this.goodsRecievedNoteToolStripMenuItem_Click);
            // 
            // counterRequisitionToolStripMenuItem
            // 
            this.counterRequisitionToolStripMenuItem.Name = "counterRequisitionToolStripMenuItem";
            this.counterRequisitionToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.counterRequisitionToolStripMenuItem.Text = "Counter Requisition";
            this.counterRequisitionToolStripMenuItem.Click += new System.EventHandler(this.counterRequisitionToolStripMenuItem_Click);
            // 
            // issueVoucherToolStripMenuItem
            // 
            this.issueVoucherToolStripMenuItem.Name = "issueVoucherToolStripMenuItem";
            this.issueVoucherToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.issueVoucherToolStripMenuItem.Text = "Issue Voucher";
            this.issueVoucherToolStripMenuItem.Click += new System.EventHandler(this.issueVoucherToolStripMenuItem_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(189, 6);
            // 
            // mnuOpeningStock
            // 
            this.mnuOpeningStock.Name = "mnuOpeningStock";
            this.mnuOpeningStock.Size = new System.Drawing.Size(192, 22);
            this.mnuOpeningStock.Text = "Opening Stock";
            this.mnuOpeningStock.Click += new System.EventHandler(this.openingStoreToolStripMenuItem_Click);
            // 
            // mnuAdjustStock
            // 
            this.mnuAdjustStock.Name = "mnuAdjustStock";
            this.mnuAdjustStock.Size = new System.Drawing.Size(192, 22);
            this.mnuAdjustStock.Text = "Adjust Stock Level";
            this.mnuAdjustStock.Click += new System.EventHandler(this.adjustStockLevelToolStripMenuItem_Click);
            // 
            // mnuDisposeItem
            // 
            this.mnuDisposeItem.Name = "mnuDisposeItem";
            this.mnuDisposeItem.Size = new System.Drawing.Size(192, 22);
            this.mnuDisposeItem.Text = "Dispose Items";
            this.mnuDisposeItem.Click += new System.EventHandler(this.frmDisposeItemDrugsToolStripMenuItem_Click);
            // 
            // mnuBatchSummary
            // 
            this.mnuBatchSummary.Name = "mnuBatchSummary";
            this.mnuBatchSummary.Size = new System.Drawing.Size(192, 22);
            this.mnuBatchSummary.Text = "Batch Summary";
            this.mnuBatchSummary.Click += new System.EventHandler(this.batchSummaryToolStripMenuItem_Click);
            // 
            // mnuStockSummary
            // 
            this.mnuStockSummary.Name = "mnuStockSummary";
            this.mnuStockSummary.Size = new System.Drawing.Size(192, 22);
            this.mnuStockSummary.Text = "Stock Summary";
            this.mnuStockSummary.Click += new System.EventHandler(this.stockSummaryToolStripMenuItem_Click);
            // 
            // mnuExpiryReport
            // 
            this.mnuExpiryReport.Name = "mnuExpiryReport";
            this.mnuExpiryReport.Size = new System.Drawing.Size(192, 22);
            this.mnuExpiryReport.Text = "Expiry Report";
            this.mnuExpiryReport.Click += new System.EventHandler(this.expiryReportToolStripMenuItem_Click);
            // 
            // mnuReports
            // 
            this.mnuReports.Name = "mnuReports";
            this.mnuReports.Size = new System.Drawing.Size(192, 22);
            this.mnuReports.Text = "Reports";
            this.mnuReports.Click += new System.EventHandler(this.ReportsToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(189, 6);
            // 
            // mnuConfigureBudget
            // 
            this.mnuConfigureBudget.Name = "mnuConfigureBudget";
            this.mnuConfigureBudget.Size = new System.Drawing.Size(192, 22);
            this.mnuConfigureBudget.Text = "Configure Budget";
            this.mnuConfigureBudget.Click += new System.EventHandler(this.configureBudgetToolStripMenuItem_Click);
            // 
            // mnuVisitConfiguration
            // 
            this.mnuVisitConfiguration.Name = "mnuVisitConfiguration";
            this.mnuVisitConfiguration.Size = new System.Drawing.Size(192, 22);
            this.mnuVisitConfiguration.Text = "Configure Patient Visit";
            this.mnuVisitConfiguration.Click += new System.EventHandler(this.configurePatientVisitToolStripMenuItem_Click);
            // 
            // mnuBudgetView
            // 
            this.mnuBudgetView.Name = "mnuBudgetView";
            this.mnuBudgetView.Size = new System.Drawing.Size(192, 22);
            this.mnuBudgetView.Text = "Budget View";
            this.mnuBudgetView.Click += new System.EventHandler(this.holisticBudgetViewToolStripMenuItem_Click);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator8,
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(128, 6);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(131, 22);
            this.mnuAbout.Text = "&About ... ...";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuOthers
            // 
            this.mnuOthers.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCalculator,
            this.mnuNotepad});
            this.mnuOthers.Name = "mnuOthers";
            this.mnuOthers.Size = new System.Drawing.Size(54, 20);
            this.mnuOthers.Text = "&Others";
            // 
            // mnuCalculator
            // 
            this.mnuCalculator.Name = "mnuCalculator";
            this.mnuCalculator.Size = new System.Drawing.Size(128, 22);
            this.mnuCalculator.Text = "&Calculator";
            this.mnuCalculator.Click += new System.EventHandler(this.mnuCalculatortool_Click);
            // 
            // mnuNotepad
            // 
            this.mnuNotepad.Name = "mnuNotepad";
            this.mnuNotepad.Size = new System.Drawing.Size(128, 22);
            this.mnuNotepad.Text = "&Notepad";
            this.mnuNotepad.Click += new System.EventHandler(this.mnuNotepad_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.logoutToolStripMenuItem.Text = "&Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.mnuLogOut_Click);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.Size = new System.Drawing.Size(37, 20);
            this.mnuExit.Text = "E&xit";
            this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.toolStripSeparator,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator2,
            this.printToolStripMenuItem,
            this.printPreviewToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.Visible = false;
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
            this.newToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newToolStripMenuItem.Text = "&New";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.openToolStripMenuItem.Text = "&Open";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(143, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.saveAsToolStripMenuItem.Text = "Save &As";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(143, 6);
            // 
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripMenuItem.Image")));
            this.printToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.printToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.printToolStripMenuItem.Text = "&Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.configureToolStripMenuItem_Click);
            // 
            // printPreviewToolStripMenuItem
            // 
            this.printPreviewToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripMenuItem.Image")));
            this.printPreviewToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripMenuItem.Name = "printPreviewToolStripMenuItem";
            this.printPreviewToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.printPreviewToolStripMenuItem.Text = "Print Pre&view";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(143, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator4,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.toolStripSeparator5,
            this.selectAllToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "&Edit";
            this.editToolStripMenuItem.Visible = false;
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.undoToolStripMenuItem.Text = "&Undo";
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.redoToolStripMenuItem.Text = "&Redo";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(141, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripMenuItem.Image")));
            this.cutToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.cutToolStripMenuItem.Text = "Cu&t";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolStripMenuItem.Image")));
            this.copyToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem.Text = "&Copy";
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolStripMenuItem.Image")));
            this.pasteToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.pasteToolStripMenuItem.Text = "&Paste";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(141, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.selectAllToolStripMenuItem.Text = "Select &All";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customizeToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            this.toolsToolStripMenuItem.Visible = false;
            // 
            // customizeToolStripMenuItem
            // 
            this.customizeToolStripMenuItem.Name = "customizeToolStripMenuItem";
            this.customizeToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.customizeToolStripMenuItem.Text = "&Customize";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator9,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.Visible = false;
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(119, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            // 
            // statusStrip
            // 
            this.statusStrip.AllowDrop = true;
            this.statusStrip.BackColor = System.Drawing.Color.Navy;
            this.statusStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblCopyRight,
            lblIcon});
            this.statusStrip.Location = new System.Drawing.Point(0, 463);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(778, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "IQCare";
            // 
            // lblStatus
            // 
            this.lblStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(68, 17);
            this.lblStatus.Text = "IQCare V1.0";
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblCopyRight.ForeColor = System.Drawing.Color.White;
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.Size = new System.Drawing.Size(635, 17);
            this.lblCopyRight.Spring = true;
            this.lblCopyRight.Tag = "lblCopyRight";
            this.lblCopyRight.Text = "CopyRight";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(778, 485);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Tag = "theForm";
            this.Text = "IQCare Management";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
       
      
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuDBOperations;
        private System.Windows.Forms.ToolStripMenuItem mnuUpsize;
        private System.Windows.Forms.ToolStripMenuItem mnuFormBuilder;
        private System.Windows.Forms.ToolStripMenuItem mnuManageForms;
        private System.Windows.Forms.ToolStripMenuItem mnuManageFields;
        private System.Windows.Forms.ToolStripMenuItem mnuViewFieldAsscociation;
        private System.Windows.Forms.ToolStripMenuItem mnuOthers;
        private System.Windows.Forms.ToolStripMenuItem mnuCalculator;
        private System.Windows.Forms.ToolStripMenuItem mnuNotepad;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripStatusLabel lblCopyRight;
        private System.Windows.Forms.ToolStripMenuItem mnuDBMigration;
        private System.Windows.Forms.ToolStripMenuItem mnuServiceAdmin;
        private System.Windows.Forms.ToolStripMenuItem mnuDBMerge;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuRefereshSystemCache;
        private System.Windows.Forms.ToolStripMenuItem mnuRebuildCustomReportDB;
        private System.Windows.Forms.ToolStripMenuItem mnuIQCareDBMaintenance;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuImportExportForms;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuManageModule;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printPreviewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuQueryBuilder;
        private System.Windows.Forms.ToolStripMenuItem mnuConfigureHomePageForms;
        private System.Windows.Forms.ToolStripMenuItem mnuConfigCareTermination;
        private System.Windows.Forms.ToolStripMenuItem mnuManageCareEndedFields;
        private System.Windows.Forms.ToolStripMenuItem mnuSplFormLinking;
        private System.Windows.Forms.ToolStripMenuItem mnuReportFieldValidation;
        private System.Windows.Forms.ToolStripMenuItem listViewFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridviewFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuPMSCM;
        private System.Windows.Forms.ToolStripMenuItem mnuPatientDrugDispense;
        private System.Windows.Forms.ToolStripMenuItem masterListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuConfigureBudget;
        private System.Windows.Forms.ToolStripMenuItem mnuDisposeItem;
        private System.Windows.Forms.ToolStripMenuItem mnuExpiryReport;
        private System.Windows.Forms.ToolStripMenuItem mnuPurchaseOrder;
        private System.Windows.Forms.ToolStripMenuItem mnuGoodReceivedNote;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem mnuVisitConfiguration;
        private System.Windows.Forms.ToolStripMenuItem mnuAdjustStock;
        private System.Windows.Forms.ToolStripMenuItem mnuOpeningStock;
        private System.Windows.Forms.ToolStripMenuItem mnuBudgetView;
        private System.Windows.Forms.ToolStripMenuItem mnuStockSummary;
        private System.Windows.Forms.ToolStripMenuItem mnuBatchSummary;
        private System.Windows.Forms.ToolStripMenuItem mnuReports;
        private System.Windows.Forms.ToolStripMenuItem manageRegistrationFieldsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sCMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem counterRequisitionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem issueVoucherToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
    }
}



