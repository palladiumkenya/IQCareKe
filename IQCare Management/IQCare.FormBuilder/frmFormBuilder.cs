using Application.Common;
using Application.Presentation;
using Interface.FormBuilder;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace IQCare.FormBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmFormBuilder : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormBuilder));
            this.pnlPanel = new System.Windows.Forms.Panel();
            this.btnformbusinessrules = new System.Windows.Forms.Button();
            this.txtTabCaptionPlaceHolder = new System.Windows.Forms.TextBox();
            this.btnManageTab = new System.Windows.Forms.Button();
            this.btnAddTab = new System.Windows.Forms.Button();
            this.tabFormBuilder = new System.Windows.Forms.TabControl();
            this.cmbTechArea = new System.Windows.Forms.ComboBox();
            this.lblTechArea = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddCustomField = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtFormName = new System.Windows.Forms.TextBox();
            this.lblFormName = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblDisplayName = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.pnlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPanel
            // 
            this.pnlPanel.Controls.Add(this.txtDisplayName);
            this.pnlPanel.Controls.Add(this.lblDisplayName);
            this.pnlPanel.Controls.Add(this.btnformbusinessrules);
            this.pnlPanel.Controls.Add(this.txtTabCaptionPlaceHolder);
            this.pnlPanel.Controls.Add(this.btnManageTab);
            this.pnlPanel.Controls.Add(this.btnAddTab);
            this.pnlPanel.Controls.Add(this.tabFormBuilder);
            this.pnlPanel.Controls.Add(this.cmbTechArea);
            this.pnlPanel.Controls.Add(this.lblTechArea);
            this.pnlPanel.Controls.Add(this.btnClose);
            this.pnlPanel.Controls.Add(this.btnAddCustomField);
            this.pnlPanel.Controls.Add(this.btnSave);
            this.pnlPanel.Controls.Add(this.txtFormName);
            this.pnlPanel.Controls.Add(this.lblFormName);
            this.pnlPanel.Location = new System.Drawing.Point(2, 3);
            this.pnlPanel.Name = "pnlPanel";
            this.pnlPanel.Size = new System.Drawing.Size(965, 585);
            this.pnlPanel.TabIndex = 0;
            this.pnlPanel.Tag = "pnlPanel";
            // 
            // btnformbusinessrules
            // 
            this.btnformbusinessrules.Location = new System.Drawing.Point(776, 0);
            this.btnformbusinessrules.Name = "btnformbusinessrules";
            this.btnformbusinessrules.Size = new System.Drawing.Size(163, 23);
            this.btnformbusinessrules.TabIndex = 16;
            this.btnformbusinessrules.Text = "Form Business Rules";
            this.btnformbusinessrules.UseVisualStyleBackColor = true;
            this.btnformbusinessrules.Click += new System.EventHandler(this.btnformbusinessrules_Click);
            // 
            // txtTabCaptionPlaceHolder
            // 
            this.txtTabCaptionPlaceHolder.Location = new System.Drawing.Point(629, 442);
            this.txtTabCaptionPlaceHolder.MaxLength = 50;
            this.txtTabCaptionPlaceHolder.Name = "txtTabCaptionPlaceHolder";
            this.txtTabCaptionPlaceHolder.Size = new System.Drawing.Size(226, 20);
            this.txtTabCaptionPlaceHolder.TabIndex = 15;
            this.txtTabCaptionPlaceHolder.Tag = "txtTextBox";
            this.txtTabCaptionPlaceHolder.Visible = false;
            this.txtTabCaptionPlaceHolder.Leave += new System.EventHandler(this.txtTabCaptionPlaceHolder_Leave);
            // 
            // btnManageTab
            // 
            this.btnManageTab.Location = new System.Drawing.Point(269, 551);
            this.btnManageTab.Name = "btnManageTab";
            this.btnManageTab.Size = new System.Drawing.Size(106, 26);
            this.btnManageTab.TabIndex = 14;
            this.btnManageTab.Tag = "btnH25WFlexi";
            this.btnManageTab.Text = "Manage &Tabs";
            this.btnManageTab.UseVisualStyleBackColor = true;
            this.btnManageTab.Click += new System.EventHandler(this.btnManageTab_Click);
            // 
            // btnAddTab
            // 
            this.btnAddTab.Location = new System.Drawing.Point(160, 551);
            this.btnAddTab.Name = "btnAddTab";
            this.btnAddTab.Size = new System.Drawing.Size(106, 26);
            this.btnAddTab.TabIndex = 13;
            this.btnAddTab.Tag = "btnH25WFlexi";
            this.btnAddTab.Text = "&Add Tab";
            this.btnAddTab.UseVisualStyleBackColor = true;
            this.btnAddTab.Click += new System.EventHandler(this.btnAddTab_Click);
            // 
            // tabFormBuilder
            // 
            this.tabFormBuilder.Location = new System.Drawing.Point(4, 63);
            this.tabFormBuilder.Name = "tabFormBuilder";
            this.tabFormBuilder.SelectedIndex = 0;
            this.tabFormBuilder.Size = new System.Drawing.Size(958, 485);
            this.tabFormBuilder.TabIndex = 12;
            this.tabFormBuilder.Tag = "";
            this.tabFormBuilder.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tabFormBuilder_MouseDoubleClick);
            // 
            // cmbTechArea
            // 
            this.cmbTechArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTechArea.FormattingEnabled = true;
            this.cmbTechArea.Location = new System.Drawing.Point(457, 3);
            this.cmbTechArea.Name = "cmbTechArea";
            this.cmbTechArea.Size = new System.Drawing.Size(274, 21);
            this.cmbTechArea.TabIndex = 9;
            this.cmbTechArea.Tag = "ddlDropDownList";
            this.cmbTechArea.SelectionChangeCommitted += new System.EventHandler(this.cmbTechArea_SelectionChangeCommitted);
            // 
            // lblTechArea
            // 
            this.lblTechArea.AutoSize = true;
            this.lblTechArea.Location = new System.Drawing.Point(390, 6);
            this.lblTechArea.Name = "lblTechArea";
            this.lblTechArea.Size = new System.Drawing.Size(49, 13);
            this.lblTechArea.TabIndex = 8;
            this.lblTechArea.Tag = "lblLabel";
            this.lblTechArea.Text = "Service :";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(487, 551);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(106, 26);
            this.btnClose.TabIndex = 7;
            this.btnClose.Tag = "btnH25WFlexi";
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddCustomField
            // 
            this.btnAddCustomField.Location = new System.Drawing.Point(596, 551);
            this.btnAddCustomField.Name = "btnAddCustomField";
            this.btnAddCustomField.Size = new System.Drawing.Size(106, 26);
            this.btnAddCustomField.TabIndex = 6;
            this.btnAddCustomField.Tag = "btnH25WFlexi";
            this.btnAddCustomField.Text = "Manage &Fields";
            this.btnAddCustomField.UseVisualStyleBackColor = true;
            this.btnAddCustomField.Click += new System.EventHandler(this.btnAddCustomField_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(378, 551);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(106, 26);
            this.btnSave.TabIndex = 6;
            this.btnSave.Tag = "btnH25WFlexi";
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtFormName
            // 
            this.txtFormName.Location = new System.Drawing.Point(85, 3);
            this.txtFormName.MaxLength = 50;
            this.txtFormName.Name = "txtFormName";
            this.txtFormName.Size = new System.Drawing.Size(300, 20);
            this.txtFormName.TabIndex = 1;
            this.txtFormName.Tag = "txtTextBox";
            this.txtFormName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFormName_KeyPress);
            this.txtFormName.Leave += new System.EventHandler(this.txtFormName_Leave);
            // 
            // lblFormName
            // 
            this.lblFormName.AutoSize = true;
            this.lblFormName.Location = new System.Drawing.Point(7, 6);
            this.lblFormName.Name = "lblFormName";
            this.lblFormName.Size = new System.Drawing.Size(67, 13);
            this.lblFormName.TabIndex = 0;
            this.lblFormName.Tag = "lblLabel";
            this.lblFormName.Text = "Form Name :";
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(200, 100);
            this.tabPage1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(0, 0);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(200, 100);
            this.tabPage2.TabIndex = 0;
            // 
            // lblDisplayName
            // 
            this.lblDisplayName.AutoSize = true;
            this.lblDisplayName.Location = new System.Drawing.Point(7, 38);
            this.lblDisplayName.Name = "lblDisplayName";
            this.lblDisplayName.Size = new System.Drawing.Size(78, 13);
            this.lblDisplayName.TabIndex = 17;
            this.lblDisplayName.Tag = "lblLabel";
            this.lblDisplayName.Text = "Display Name :";
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(85, 31);
            this.txtDisplayName.MaxLength = 100;
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(453, 20);
            this.txtDisplayName.TabIndex = 18;
            this.txtDisplayName.Tag = "txtDisplayName";
            // 
            // frmFormBuilder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 591);
            this.Controls.Add(this.pnlPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFormBuilder";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "frmForm";
            this.Text = "IQCare Form Builder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmFormBuilder_FormClosing);
            this.Load += new System.EventHandler(this.frmFormBuilder_Load);
            this.pnlPanel.ResumeLayout(false);
            this.pnlPanel.PerformLayout();
            this.ResumeLayout(false);

        }
        /// <summary>
        /// The PNL panel
        /// </summary>
        private Panel pnlPanel;
        /// <summary>
        /// The label1
        /// </summary>
        private Label lblFormName;
        /// <summary>
        /// The text form name
        /// </summary>
        private TextBox txtFormName;
        /// <summary>
        /// The BTN add custom field
        /// </summary>
        private Button btnAddCustomField;
        /// <summary>
        /// The BTN close
        /// </summary>
        private Button btnClose;
        /// <summary>
        /// The form
        /// </summary>
        private Form theForm;
        /// <summary>
        /// The BTN save
        /// </summary>
        private Button btnSave;
        /// <summary>
        /// The label tech area
        /// </summary>
        private Label lblTechArea;
        /// <summary>
        /// The CMB tech area
        /// </summary>
        private ComboBox cmbTechArea;
        /// <summary>
        /// The BTN add tab
        /// </summary>
        private Button btnAddTab;
        /// <summary>
        /// The tab form builder
        /// </summary>
        private TabControl tabFormBuilder;
        /// <summary>
        /// The tab page1
        /// </summary>
        private TabPage tabPage1;
        /// <summary>
        /// The tab page2
        /// </summary>
        private TabPage tabPage2;
        /// <summary>
        /// The BTN manage tab
        /// </summary>
        private Button btnManageTab;
        /// <summary>
        /// The text tab caption place holder
        /// </summary>
        private TextBox txtTabCaptionPlaceHolder;
        /// <summary>
        /// The btnformbusinessrules
        /// </summary>
        private Button btnformbusinessrules;
        /// <summary>
        /// The _DT delete fields
        /// </summary>
        private static DataTable _dtDeleteFields = new DataTable();
        /// <summary>
        /// The _DT delete sections
        /// </summary>
        private static DataTable _dtDeleteSections = new DataTable();
        /// <summary>
        /// The _DT delete tabs
        /// </summary>
        private static DataTable _dtDeleteTabs = new DataTable();
        /// <summary>
        /// The tab identifier
        /// </summary>
        private static int tabID = 0;
        /// <summary>
        /// The string get path
        /// </summary>
        private static string strGetPath = GblIQCare.GetPath();
        /// <summary>
        /// The style
        /// </summary>
        private clsCssStyle theStyle = new clsCssStyle();
      
        /// <summary>
        /// The DGW remove selected row
        /// </summary>
        private DataGridViewRow dgwRemoveSelectedRow = new DataGridViewRow();
        /// <summary>
        /// The ds field details
        /// </summary>
        private DataSet dsFieldDetails = new DataSet();
        /// <summary>
        /// The ds collect data to save
        /// </summary>
        private DataSet dsCollectDataToSave = new DataSet();
        /// <summary>
        /// The ds update mode
        /// </summary>
        private DataSet dsUpdateMode = new DataSet();
        /// <summary>
        /// The img list
        /// </summary>
        private Image imgList = Image.FromFile(frmFormBuilder.strGetPath + "\\List.bmp");
        /// <summary>
        /// The img disabled list
        /// </summary>
        private Image imgDisabledList = Image.FromFile(frmFormBuilder.strGetPath + "\\listdesible.bmp");
        /// <summary>
        /// The img business rule
        /// </summary>
        private Image imgBusinessRule = Image.FromFile(frmFormBuilder.strGetPath + "\\brule.bmp");
        /// <summary>
        /// The img inactive br
        /// </summary>
        private Image imgInactiveBR = Image.FromFile(frmFormBuilder.strGetPath + "\\nonactive.bmp");
        /// <summary>
        /// The img button up
        /// </summary>
        private Image imgButtonUp = Image.FromFile(frmFormBuilder.strGetPath + "\\btnUp.Image.gif");
        /// <summary>
        /// The img button down
        /// </summary>
        private Image imgButtonDown = Image.FromFile(frmFormBuilder.strGetPath + "\\btnDown.Image.gif");
        /// <summary>
        /// The i panel left
        /// </summary>
        private const int iPanelLeft = 12;
        /// <summary>
        /// The i panel height
        /// </summary>
        private const int iPanelHeight = 440;
        /// <summary>
        /// The i panel width
        /// </summary>
        private const int iPanelWidth = 935;
        /// <summary>
        /// The i data grid left
        /// </summary>
        private const int iDataGridLeft = 13;
        /// <summary>
        /// The i data grid height
        /// </summary>
        private const int iDataGridHeight = 345;
        /// <summary>
        /// The i data grid width
        /// </summary>
        private const int iDataGridWidth = 880;
        /// <summary>
        /// The i data grid top
        /// </summary>
        private const int iDataGridTop = 44;

        /// <summary>
        /// The dt manage section position
        /// </summary>
        public static DataTable dtManageSectionPos;
        /// <summary>
        /// The dt manage tab position
        /// </summary>
        public static DataTable dtManageTabPos;
        /// <summary>
        /// The dt MST feature for manage field
        /// </summary>
        private DataTable dtMstFeatureForManageField;
        /// <summary>
        /// The dt MST section for manage field
        /// </summary>
        private DataTable dtMstSectionForManageField;
        /// <summary>
        /// The dt LNK forms for manage field
        /// </summary>
        private DataTable dtLnkFormsForManageField;
        /// <summary>
        /// The components
        /// </summary>
        //private IContainer components;


        /// <summary>
        /// Gets or sets the dt delete fields.
        /// </summary>
        /// <value>
        /// The dt delete fields.
        /// </value>
        public static DataTable dtDeleteFields
        {
            get
            {
                return frmFormBuilder._dtDeleteFields;
            }
            set
            {
                frmFormBuilder._dtDeleteFields = value;
            }
        }

        /// <summary>
        /// Gets or sets the dt delete sections.
        /// </summary>
        /// <value>
        /// The dt delete sections.
        /// </value>
        public static DataTable dtDeleteSections
        {
            get
            {
                return frmFormBuilder._dtDeleteSections;
            }
            set
            {
                frmFormBuilder._dtDeleteSections = value;
            }
        }

        /// <summary>
        /// Gets or sets the dt delete tabs.
        /// </summary>
        /// <value>
        /// The dt delete tabs.
        /// </value>
        public static DataTable dtDeleteTabs
        {
            get
            {
                return frmFormBuilder._dtDeleteTabs;
            }
            set
            {
                frmFormBuilder._dtDeleteTabs = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="frmFormBuilder"/> class.
        /// </summary>
        public frmFormBuilder()
        {
            this.InitializeComponent();
            frmFormBuilder.tabID = 0;
        }

        /// <summary>
        /// Handles the Load event of the frmFormBuilder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void frmFormBuilder_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.cmbTechArea.DataSource == null)
                    new BindFunctions().Win_BindCombo(this.cmbTechArea,
                        ((IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder"))
                        .GetPublishedModuleList().Tables[0], "ModuleName", "ModuleId");
                this.ClearBusinessRules();
                if (GblIQCare.RefillDdlFields == 1)
                {
                    this.dsFieldDetails = ((IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder"))
                        .GetCustomFields("", 0, GblIQCare.iManageCareEnded);
                    GblIQCare.RefillDdlFields = 0;
                }
                else
                {
                    this.theStyle.setStyle((Control)this);
                    //this.dsFieldDetails = ((IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder"))
                    //    .GetCustomFields("", 0, GblIQCare.iManageCareEnded);
                    this.dsUpdateMode.Tables.Clear();
                    if (frmFormBuilder.dtDeleteFields != null)
                        frmFormBuilder.dtDeleteFields.Clear();
                    frmFormBuilder.dtDeleteFields = new DataTable();
                    frmFormBuilder.dtDeleteFields = clsCommon.RemoveFieldLnkGridView_Forms();
                    frmFormBuilder.dtDeleteSections = clsCommon.ManageSectionPos();
                    if (frmFormBuilder.dtDeleteTabs != null)
                        frmFormBuilder.dtDeleteFields.Clear();
                    frmFormBuilder.dtDeleteTabs = clsCommon.ManageDeleteTab();
                    if (GblIQCare.iFormBuilderId <= 0)
                        return;
                    this.dsUpdateMode = ((IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder"))
                        .GetFormDetail(GblIQCare.iFormBuilderId);
                    this.FillData();
                }
            }
            catch (Exception ex)
            {
                MsgBuilder MessageBuilder = new MsgBuilder();
                MessageBuilder.DataElements["MessageText"] = ex.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", MessageBuilder, (Control)this);
            }
        }

        /// <summary>
        /// Clears the business rules.
        /// </summary>
        public void ClearBusinessRules()
        {
            GblIQCare.blnSingleVisit = false;
            GblIQCare.blnMultivisit = false;
            GblIQCare.blnSignatureOneachpage = false;
            GblIQCare.blncontrolunabledesable = true;
            GblIQCare.dtformBusinessValues.Clear();
            GblIQCare.dtformBusinessValues.Columns.Clear();
            GblIQCare.dtformBusinessValues.Rows.Clear();
        }

        /// <summary>
        /// Fills the data.
        /// </summary>
        private void FillData()
        {
            if (this.dsUpdateMode.Tables[0].Rows.Count > 0)
            {
                this.txtFormName.Text = this.dsUpdateMode.Tables[0].Rows[0]["FeatureName"].ToString();
                this.txtDisplayName.Text = this.dsUpdateMode.Tables[0].Rows[0]["FormDescription"].ToString();
                this.cmbTechArea.SelectedValue = (object)this.dsUpdateMode.Tables[0].Rows[0]["ModuleId"].ToString();
                this.cmbTechArea.Enabled = false;
                this.dsFieldDetails = ((IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder")).GetCustomFields("", 0, GblIQCare.iManageCareEnded);
                if (this.dsUpdateMode.Tables[0].Rows[0]["MultiVisit"].ToString() == "0")
                    GblIQCare.blnSingleVisit = true;
                else if (this.dsUpdateMode.Tables[0].Rows[0]["MultiVisit"].ToString() == "1")
                    GblIQCare.blnMultivisit = true;
            }
            if (this.dsUpdateMode.Tables[6].Rows.Count > 0)
            {
                this.btnformbusinessrules.BackColor = Color.Green;
                GblIQCare.dtformBusinessValues = this.dsUpdateMode.Tables[6];
            }
            DataSet dataSet1 = new DataSet();
            DataSet dataSet2 = this.dsUpdateMode.Copy();
            if (this.dsUpdateMode.Tables[1].Rows.Count > 0)
            {
                for (int index1 = 0; index1 < this.dsUpdateMode.Tables[3].Rows.Count; ++index1)
                {
                    if (Convert.ToInt32(this.dsUpdateMode.Tables[3].Rows[index1]["Signature"]) == 1)
                        GblIQCare.blnSignatureOneachpage = true;
                    UserCtrlFormBuilder tab = this.CreateTab(this.dsUpdateMode.Tables[3].Rows[index1]["TabName"].ToString());
                    DataTable tabFeatureByTabId1 = this.GetSectionTabFeatureBYTabID(this.dsUpdateMode, Convert.ToInt32(this.dsUpdateMode.Tables[3].Rows[index1]["TabID"].ToString()), 1);
                    this.dsUpdateMode.Tables[1].Clear();
                    this.dsUpdateMode.Tables[1].Merge(tabFeatureByTabId1);
                    DataTable tabFeatureByTabId2 = this.GetSectionTabFeatureBYTabID(this.dsUpdateMode, Convert.ToInt32(this.dsUpdateMode.Tables[3].Rows[index1]["TabID"].ToString()), 2);
                    this.dsUpdateMode.Tables[2].Clear();
                    this.dsUpdateMode.Tables[2].Merge(tabFeatureByTabId2);
                    tab.dsUpdateMode = this.dsUpdateMode;
                    for (int index2 = 0; index2 < this.dsUpdateMode.Tables[1].Rows.Count; ++index2)
                        tab.CreatePanel(PMTCTConstants.strUpdate);
                    tab.CreateField();
                    this.dsUpdateMode = dataSet2.Copy();
                    tabFeatureByTabId1.Dispose();
                    tabFeatureByTabId2.Dispose();
                }
            }
            GblIQCare.blncontrolunabledesable = !(this.dsUpdateMode.Tables[5].Rows[0]["Signature"].ToString() == "1");
            dataSet2.Dispose();
            dataSet1 = (DataSet)null;
        }

        /// <summary>
        /// Gets the section tab feature by tab identifier.
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <param name="tabid">The tabid.</param>
        /// <param name="tabbleNo">The tabble no.</param>
        /// <returns></returns>
        public DataTable GetSectionTabFeatureBYTabID(DataSet ds, int tabid, int tabbleNo)
        {
            var myQuery = (from sec in ds.Tables[tabbleNo].AsEnumerable()
                           join lnk in ds.Tables[4].AsEnumerable()
                               on sec.Field<int>("SectionID") equals
                               lnk.Field<int>("SectionID")
                           join tab in ds.Tables[3].AsEnumerable()
                               on lnk.Field<int>("TabID") equals
                               tab.Field<int>("TabID")
                           where tab.Field<int>("TabID") == tabid

                           select sec);

            DataTable dataTable = myQuery.CopyToDataTable();

            return dataTable;
        }

        /// <summary>
        /// Creates the tab.
        /// </summary>
        /// <param name="tabName">Name of the tab.</param>
        /// <returns></returns>
        private UserCtrlFormBuilder CreateTab(string tabName)
        {
            TabPage tabPage = new TabPage(tabName);
            if (this.dsUpdateMode.Tables[3].Rows.Count >= frmFormBuilder.tabID + 1)
                tabPage.Name = "Tab_" + (object)frmFormBuilder.tabID + "_" + this.dsUpdateMode.Tables[3].Rows[frmFormBuilder.tabID]["TabID"].ToString();
            this.tabFormBuilder.TabPages.Add(tabPage);
            UserCtrlFormBuilder userCtrlFormBuilder = new UserCtrlFormBuilder();
            userCtrlFormBuilder.Name = "UserCtrl_" + (object)frmFormBuilder.tabID;
            userCtrlFormBuilder.propParentForm = this;
            userCtrlFormBuilder.propFormName = this.txtFormName.Text;
            userCtrlFormBuilder.propTechAreaID = Convert.ToInt32(this.cmbTechArea.SelectedValue);
            userCtrlFormBuilder.propSingleVisit = GblIQCare.blnSingleVisit;
            userCtrlFormBuilder.propMultipleVisit = GblIQCare.blnMultivisit;
            userCtrlFormBuilder.dsFieldDetails = this.dsFieldDetails;
            userCtrlFormBuilder.Tag = (object)"pnlSubPanel";
            this.theStyle.setStyle((Control)userCtrlFormBuilder);
            this.tabFormBuilder.TabPages[frmFormBuilder.tabID].Controls.Add((Control)userCtrlFormBuilder);
            ++frmFormBuilder.tabID;
            return userCtrlFormBuilder;
        }

        /// <summary>
        /// Handles the KeyPress event of the txtFormName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtFormName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ("_-=\\/!@#$&%^*()+|.,<>?`~\";:'[]{}".IndexOf(e.KeyChar.ToString()) < 0)
                return;
            e.Handled = true;
        }

        /// <summary>
        /// Handles the SelectionChangeCommitted event of the cmbTechArea control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbTechArea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GblIQCare.RefillDdlFields = 1;
            this.frmFormBuilder_Load(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the btnAddTab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddTab_Click(object sender, EventArgs e)
        {
            if (this.ValidationCheck())
                return;
            TabPage tabPage = new TabPage("Tab" + (object)frmFormBuilder.tabID);
            tabPage.Name = "Tab_" + (object)frmFormBuilder.tabID + "_0";
            this.tabFormBuilder.TabPages.Add(tabPage);
            UserCtrlFormBuilder userCtrlFormBuilder = new UserCtrlFormBuilder();
            userCtrlFormBuilder.propParentForm = this;
            userCtrlFormBuilder.propFormName = this.txtFormName.Text;
            userCtrlFormBuilder.propTechAreaID = Convert.ToInt32(this.cmbTechArea.SelectedValue);
            userCtrlFormBuilder.propSingleVisit = GblIQCare.blnSingleVisit;
            userCtrlFormBuilder.propMultipleVisit = GblIQCare.blnMultivisit;
            userCtrlFormBuilder.dsFieldDetails = this.dsFieldDetails;
            userCtrlFormBuilder.Tag = (object)"pnlSubPanel";
            this.theStyle.setStyle((Control)userCtrlFormBuilder);
            this.tabFormBuilder.TabPages[this.tabFormBuilder.TabPages.Count - 1].Controls.Add((Control)userCtrlFormBuilder);
            ++frmFormBuilder.tabID;
        }

        /// <summary>
        /// Validations the check.
        /// </summary>
        /// <returns></returns>
        public bool ValidationCheck()
        {
            bool flag = false;
            if (this.txtFormName.Text.ToString().Trim() == "")
            {
                IQCareWindowMsgBox.ShowWindow("PMTCTFormNameMandatory", (Control)this);
                this.txtFormName.Focus();
                flag = true;
            }
            if (this.cmbTechArea.SelectedIndex == 0)
            {
                IQCareWindowMsgBox.ShowWindow("SelectTechnicalArea", (Control)this);
                this.cmbTechArea.Focus();
                flag = true;
            }
            return flag;
        }

        /// <summary>
        /// Creates the tab table_ forms.
        /// </summary>
        /// <returns></returns>
        public DataTable CreateTabTable_Forms()
        {
            return new DataTable()
            {
                Columns = {
          new DataColumn()
          {
            DataType = System.Type.GetType("System.Int32"),
            ColumnName = "TabID"
          },
          new DataColumn()
          {
            DataType = System.Type.GetType("System.String"),
            ColumnName = "TabName"
          },
          new DataColumn()
          {
            DataType = System.Type.GetType("System.Int32"),
            ColumnName = "FeatureID"
          },
          new DataColumn()
          {
            DataType = System.Type.GetType("System.Int32"),
            ColumnName = "UserId"
          },
          new DataColumn()
          {
            DataType = System.Type.GetType("System.Int32"),
            ColumnName = "DeleteFlag"
          },
          new DataColumn()
          {
            DataType = System.Type.GetType("System.Int32"),
            ColumnName = "seq"
          },
          new DataColumn()
          {
            DataType = System.Type.GetType("System.String"),
            ColumnName = "InsertUpdateStatus"
          },
          new DataColumn()
          {
            DataType = System.Type.GetType("System.Int32"),
            ColumnName = "Signature"
          }
        }
            };
        }

        /// <summary>
        /// Creates the LNK_ form tab section.
        /// </summary>
        /// <returns></returns>
        public DataTable CreateLnk_FormTabSection()
        {
            return new DataTable()
            {
                Columns = {
          new DataColumn()
          {
            DataType = System.Type.GetType("System.Int32"),
            ColumnName = "ID"
          },
          new DataColumn()
          {
            DataType = System.Type.GetType("System.Int32"),
            ColumnName = "TabID"
          },
          new DataColumn()
          {
            DataType = System.Type.GetType("System.Int32"),
            ColumnName = "SectionID"
          },
          new DataColumn()
          {
            DataType = System.Type.GetType("System.Int32"),
            ColumnName = "FeatureID"
          },
          new DataColumn()
          {
            DataType = System.Type.GetType("System.Int32"),
            ColumnName = "UserId"
          },
          new DataColumn()
          {
            DataType = System.Type.GetType("System.String"),
            ColumnName = "InsertUpdateStatus"
          },
          new DataColumn()
          {
            DataType = System.Type.GetType("System.Int32"),
            ColumnName = "DeleteFlag"
          }
        }
            };
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.btnSave.Enabled = false;
            bool flag = true;
            this.dsCollectDataToSave.Tables.Clear();
            if (this.txtFormName.Text.ToString().Trim() == "")
            {
                IQCareWindowMsgBox.ShowWindow("PMTCTFormNameMandatory", (Control)this);
                this.txtFormName.Focus();
                flag = false;
            }
            else if (this.cmbTechArea.SelectedIndex == 0)
            {
                IQCareWindowMsgBox.ShowWindow("SelectTechnicalArea", (Control)this);
                this.cmbTechArea.Focus();
                flag = false;
            }
            else
            {
                int moduleId = Convert.ToInt32(this.cmbTechArea.SelectedValue);
                IFormBuilder formBuilder = (IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder");
                bool isDuplicate = false;
                if (GblIQCare.iFormBuilderId > 0)
                {
                    isDuplicate = formBuilder.CheckDuplicate(PMTCTConstants.strMstFeature,
                                            PMTCTConstants.strColMstFeatureName,
                                            this.txtFormName.Text.ToString().Trim(),
                                            PMTCTConstants.strColMstFeatureId,
                                            GblIQCare.iFormBuilderId.ToString(), "1", moduleId);
                }
                else
                {
                    isDuplicate = formBuilder.CheckDuplicate(PMTCTConstants.strMstFeature, PMTCTConstants.strColMstFeatureName, this.txtFormName.Text.ToString().Trim(), "1", moduleId);
                }
                if (flag && isDuplicate)
                {
                    IQCareWindowMsgBox.ShowWindow("PMTCTDupFormName", (Control)this);
                    this.txtFormName.Focus();
                    flag = false;
                }
                /*
                 * if ((GblIQCare.iFormBuilderId > 0 ? 
                    formBuilder.CheckDuplicate(PMTCTConstants.strMstFeature, PMTCTConstants.strColMstFeatureName, 
                    this.txtFormName.Text.ToString().Trim(), PMTCTConstants.strColMstFeatureId, GblIQCare.iFormBuilderId.ToString(), "1", moduleId)
            
                    : formBuilder.CheckDuplicate(PMTCTConstants.strMstFeature, PMTCTConstants.strColMstFeatureName, this.txtFormName.Text.ToString().Trim(), "1", moduleId)) && flag)
                {
                  IQCareWindowMsgBox.ShowWindow("PMTCTDupFormName", (Control) this);
                  this.txtFormName.Focus();
                  flag = false;
                }*/
            }
            DataTable dataTable1 = new DataTable();
            DataTable tableMstFeature = clsCommon.CreateTableMstFeature();
            int num1 = 0;
            DataRow rowForm = tableMstFeature.NewRow();
            int formBuilderFeatureId;
            if (GblIQCare.iFormBuilderId != 0)
            {
                formBuilderFeatureId = GblIQCare.iFormBuilderId;
                rowForm["FeatureId"] = (object)formBuilderFeatureId;
                rowForm["InsertUpdateStatus"] = (object)PMTCTConstants.strUpdate;
            }
            else
            {
                formBuilderFeatureId = ((IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder")).RetrieveMaxId(PMTCTConstants.strMstFeature) + 1;
                rowForm["InsertUpdateStatus"] = (object)PMTCTConstants.strInsert;
            }
            rowForm["FeatureId"] = (object)formBuilderFeatureId;
            rowForm["FeatureName"] = (object)this.txtFormName.Text.ToString().Trim();
            rowForm["FormDescription"] = this.txtDisplayName.Text.Trim().Length > 0 ? this.txtDisplayName.Text.Trim() : this.txtFormName.Text.ToString().Trim();
            rowForm["ReportFlag"] = (object)0;
            rowForm["DeleteFlag"] = (object)0;
            rowForm["AdminFlag"] = (object)0;
            rowForm["UserId"] = (object)GblIQCare.AppUserId;
            rowForm["CountryId"] = (object)GblIQCare.AppCountryId;
            rowForm["SystemId"] = (object)1;
            rowForm["ModuleId"] = this.cmbTechArea.SelectedValue;
            rowForm["MultiVisit"] = (object)(GblIQCare.blnSingleVisit ? 0 : 1);
            tableMstFeature.Rows.Add(rowForm);
            DataTable dataTable2 = new DataTable();
            DataTable dataTable3 = new DataTable();
            DataTable dtTab = new DataTable();
            DataTable dataTable4 = new DataTable();
            dtTab = this.CreateTabTable_Forms();
            DataTable tableFormTabSection = this.CreateLnk_FormTabSection();
            DataTable tableLnkForms = clsCommon.CreateTableLnk_Forms();
            DataTable tableMstSection = clsCommon.CreateTableMstSection();
            int num3 = 0;
            int index1 = 0;
            int num4 = 1;
            int num5 = 0;
            foreach (Control tabControl in (ArrangedElementCollection)this.tabFormBuilder.Controls)
            {
                if (tabControl.GetType().ToString() == "System.Windows.Forms.TabPage")
                {
                    foreach (Control tabPage in (ArrangedElementCollection)tabControl.Controls)
                    {
                        foreach (Control controlSectionBuilder in (ArrangedElementCollection)tabPage.Controls)
                        {
                            if (controlSectionBuilder.GetType().ToString() == "System.Windows.Forms.Panel" && controlSectionBuilder.Name == "pnlSectionBuilder")
                            {
                                foreach (Control controlPanel in (ArrangedElementCollection)controlSectionBuilder.Controls)
                                {
                                    if (controlPanel.GetType().ToString() == "System.Windows.Forms.Panel" && controlPanel.Name.Contains("pnlMainPanel"))
                                    {
                                        int num6 = 0;
                                        int num7 = 1;
                                        int tabIndex = 0;
                                        DataRow rowTab = dtTab.NewRow();
                                        if (GblIQCare.iFormBuilderId != 0)
                                        {
                                            tabIndex = Convert.ToInt32(tabControl.Name.Split('_')[2]);
                                            if (tabIndex > 0)
                                                rowTab["TabId"] = (object)tabIndex;
                                            else if (num5 > 0)
                                            {
                                                ++num5;
                                            }
                                            else
                                            {
                                                num5 = ((IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder")).RetrieveMaxId(PMTCTConstants.strMstTab);
                                                ++num5;
                                            }
                                            rowTab["InsertUpdateStatus"] = (object)PMTCTConstants.strUpdate;
                                        }
                                        else if (num5 > 0)
                                        {
                                            ++num5;
                                        }
                                        else
                                        {
                                            num5 = ((IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder")).RetrieveMaxId(PMTCTConstants.strMstTab);
                                            ++num5;
                                            rowTab["InsertUpdateStatus"] = (object)PMTCTConstants.strInsert;
                                        }
                                        rowTab["TabID"] = tabIndex <= 0 ? (object)num5 : (object)tabIndex;
                                        rowTab["TabName"] = (object)tabControl.Text.ToString().Trim();
                                        rowTab["seq"] = num6 <= 0 ? (object)num4 : (object)num6;
                                        rowTab["DeleteFlag"] = (object)0;
                                        rowTab["UserId"] = (object)GblIQCare.AppUserId;
                                        rowTab["FeatureId"] = (object)formBuilderFeatureId;
                                        rowTab["Signature"] = !GblIQCare.blnSignatureOneachpage ? (object)0 : (object)1;
                                        dtTab.Rows.Add(rowTab);
                                        ++num4;
                                        foreach (Control sectionPanel in (ArrangedElementCollection)controlPanel.Controls)
                                        {
                                            if (sectionPanel is Panel && sectionPanel.Name.Contains("pnlDynamicPanel") && sectionPanel.Tag.ToString() != "deleted")
                                            {
                                                string strSectionName = string.Empty;
                                                string strSectionInfo = string.Empty;
                                                TextBox tName = null;
                                                TextBox tInfo = null;
                                                List<TextBox> textboxes = sectionPanel.Controls.OfType<TextBox>().ToList();
                                                if (textboxes != null)
                                                {
                                                    tName = textboxes.Where(t => t.Name.Contains("txtSectionName_")).DefaultIfEmpty(null).FirstOrDefault();
                                                    if (tName != null)
                                                    {
                                                        strSectionName = tName.Text;
                                                    }
                                                    tInfo = textboxes.Where(t => t.Name.Contains("txtSectionInfo_")).DefaultIfEmpty(null).FirstOrDefault();
                                                    if (tInfo != null)
                                                    {
                                                        strSectionInfo = tInfo.Text;
                                                    }

                                                    if (strSectionName != "")
                                                    {
                                                        int sequence = 0;
                                                        string[] strArray = sectionPanel.Name.Split('_');
                                                        if (strArray.Length > 2)
                                                            sequence = Convert.ToInt32(strArray[2]);
                                                        int sectionIndex = 0;
                                                        DataRow rowSection = tableMstSection.NewRow();
                                                        if (GblIQCare.iFormBuilderId != 0)
                                                        {
                                                            sectionIndex = Convert.ToInt32(tName.Name.Split('_')[2]);
                                                            if (sectionIndex > 0)
                                                                rowSection["SectionId"] = (object)sectionIndex;
                                                            else if (num1 > 0)
                                                            {
                                                                ++num1;
                                                            }
                                                            else
                                                            {
                                                                num1 = ((IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder")).RetrieveMaxId(PMTCTConstants.strMstSection);
                                                                ++num1;
                                                            }
                                                            rowSection["InsertUpdateStatus"] = (object)PMTCTConstants.strUpdate;
                                                        }
                                                        else if (num1 > 0)
                                                        {
                                                            ++num1;
                                                        }
                                                        else
                                                        {
                                                            num1 = ((IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder")).RetrieveMaxId(PMTCTConstants.strMstSection);
                                                            ++num1;
                                                            rowSection["InsertUpdateStatus"] = (object)PMTCTConstants.strInsert;
                                                        }
                                                        rowSection["SectionId"] = sectionIndex <= 0 ? (object)num1 : (object)sectionIndex;
                                                        rowSection["SectionName"] = (object)strSectionName.Trim();
                                                        rowSection["SectionInfo"] = (object)strSectionInfo.Trim();
                                                        rowSection["Sequence"] = sequence <= 0 ? (object)num7 : (object)sequence;
                                                        rowSection["CustomFlag"] = (object)0;
                                                        rowSection["DeleteFlag"] = sectionPanel.Tag.ToString() == "deleted" ? (object)"1" : (object)"0";
                                                        rowSection["UserId"] = (object)GblIQCare.AppUserId;
                                                        rowSection["FeatureId"] = (object)formBuilderFeatureId;
                                                        tableMstSection.Rows.Add(rowSection);
                                                        ++num7;
                                                        DataRow rowFormTabSection = tableFormTabSection.NewRow();
                                                        rowFormTabSection["TabID"] = tabIndex <= 0 ? (object)num5 : (object)tabIndex;
                                                        rowFormTabSection["SectionID"] = sectionIndex <= 0 ? (object)num1 : (object)sectionIndex;
                                                        rowFormTabSection["UserId"] = (object)GblIQCare.AppUserId;
                                                        rowFormTabSection["FeatureId"] = (object)formBuilderFeatureId;
                                                        rowFormTabSection["DeleteFlag"] = (object)0;
                                                        if (GblIQCare.iFormBuilderId != 0)
                                                        {
                                                            rowFormTabSection["InsertUpdateStatus"] = (object)PMTCTConstants.strUpdate;
                                                        }
                                                        else
                                                        {
                                                            rowFormTabSection["InsertUpdateStatus"] = (object)PMTCTConstants.strInsert;
                                                            rowFormTabSection["ID"] = (object)0;
                                                        }
                                                        tableFormTabSection.Rows.Add(rowFormTabSection);
                                                    }
                                                    else if (flag)
                                                    {
                                                        IQCareWindowMsgBox.ShowWindow("PMTCTSectionNameMandatory", (Control)this);
                                                        tName.Focus();
                                                        flag = false;
                                                    }

                                                }
                                               
                                                List<CheckBox> checkBoxes = sectionPanel.Controls.OfType<CheckBox>().ToList();
                                                if (checkBoxes != null)
                                                {
                                                    CheckBox checkGrid = checkBoxes.Where(c => c.Name.Contains("chkGridView_")).DefaultIfEmpty(null).FirstOrDefault();
                                                    if (checkGrid != null)
                                                    {
                                                        int defaultIndex = 0;
                                                        int checkIndex = 0;
                                                        if (GblIQCare.iFormBuilderId != 0)
                                                        {
                                                            checkIndex = Convert.ToInt32(checkGrid.Name.Split('_')[2]);
                                                            if (checkIndex > 0)
                                                                defaultIndex = checkIndex;
                                                            else if (num3 > 0)
                                                            {
                                                                ++num3;
                                                            }
                                                            else
                                                            {
                                                                num3 = ((IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder")).RetrieveMaxId(PMTCTConstants.strMstSection);
                                                                ++num3;
                                                            }
                                                        }
                                                        else if (num3 > 0)
                                                        {
                                                            ++num3;
                                                        }
                                                        else
                                                        {
                                                            num3 = ((IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder")).RetrieveMaxId(PMTCTConstants.strMstSection);
                                                            ++num3;
                                                        }
                                                        int num11 = checkIndex <= 0 ? num3 : checkIndex;
                                                        foreach (DataRow dataRow in tableMstSection.Select("SectionId=" + num11.ToString()))
                                                            dataRow["IsgridView"] = checkGrid.Checked ? (object)"1" : (object)"0";
                                                        tableMstSection.AcceptChanges();
                                                    }
                                                }
                                                
                                                int num12 = 1;
                                                List<DataGridView> gridView = sectionPanel.Controls.OfType<DataGridView>().ToList();
                                                if (gridView != null && gridView.Count > 0)
                                                {
                                                    DataGridView dataGridView = gridView[0] as DataGridView;
                                                      if (dataGridView.Rows.Count > 0 && dataGridView.Rows[0].Cells[0].Value != null)
                                                        {
                                                            for (int index2 = 0; index2 < dataGridView.Rows.Count; ++index2)
                                                            {
                                                                DataRow row3 = tableLnkForms.NewRow();
                                                                if (dataGridView.Rows[index2].Cells[0].Value != null && flag)
                                                                {
                                                                    if (dataGridView.Rows[index2].Cells["Field Label"].Value != null && dataGridView.Rows[index2].Cells["Field Label"].Value.ToString().Trim() != "")
                                                                    {
                                                                        row3["InsertUpdateStatus"] = GblIQCare.iFormBuilderId == 0 || dataGridView.Rows[index2].Cells["ID"].Value == null || !(dataGridView.Rows[index2].Cells["ID"].Value.ToString() != "") ? (object)PMTCTConstants.strInsert : (object)PMTCTConstants.strUpdate;
                                                                        if (GblIQCare.iFormBuilderId > 0)
                                                                        {
                                                                            row3["Id"] = dataGridView.Rows[index2].Cells["Id"].Value != null ? dataGridView.Rows[index2].Cells["Id"].Value : (object)"0";
                                                                            row3["FieldId"] = !dataGridView.Rows[index2].Cells["FieldId"].Value.ToString().StartsWith("999971") ? dataGridView.Rows[index2].Cells["FieldId"].Value : (object)dataGridView.Rows[index2].Cells["FieldId"].Value.ToString().Replace("9999", "");
                                                                        }
                                                                        else
                                                                            row3["FieldId"] = (object)dataGridView.Rows[index2].Cells["FieldId"].Value.ToString().Trim();
                                                                        row3["FeatureId"] = (object)formBuilderFeatureId;
                                                                        row3["SectionId"] = tableMstSection.Rows[index1]["SectionId"];
                                                                        row3["FieldLabel"] = (object)dataGridView.Rows[index2].Cells["Field Label"].Value.ToString().Trim();
                                                                        row3["Sequence"] = (object)num12;
                                                                        row3["Predefined"] = dataGridView.Rows[index2].Cells["Predefined"].Value;
                                                                        row3["UserId"] = (object)GblIQCare.AppUserId;
                                                                        tableLnkForms.Rows.Add(row3);
                                                                        ++num12;
                                                                    }
                                                                    else if (flag)
                                                                    {
                                                                        IQCareWindowMsgBox.ShowWindow("PMTCTFieldLabelMandatory", (Control)this);
                                                                        flag = false;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else if (flag)
                                                        {
                                                            IQCareWindowMsgBox.ShowWindow("PMTCTNoFieldCreated", (Control)this);
                                                            flag = false;
                                                        }
                                                      ++index1;
                                                }
                                           
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (dtTab.Rows.Count > 0)
            {
                var duplicates = dtTab.AsEnumerable().GroupBy(r => new { TabName = r[1] }).Where(gr => gr.Count() > 1).ToList();
                if (duplicates.Count > 0)
                {
                    String dup = "";
                    for (int i = 0; i < duplicates.Count; i++)
                    {
                        if (!String.IsNullOrEmpty(dup))
                        {
                            dup += " , ";
                        }
                        dup += duplicates[i].Key.TabName;
                    }
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = dup;
                    IQCareWindowMsgBox.ShowWindow("PMTCTDupTab", theBuilder, this);
                    flag = false;
                }
                if (tableMstSection.Rows.Count > 0 && flag && tableFormTabSection.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTab.Rows.Count; ++i)
                    {
                        if (tableFormTabSection.AsEnumerable().Join((IEnumerable<DataRow>)tableMstSection.AsEnumerable(), (Func<DataRow, int>)(tab => tab.Field<int>("SectionID")), (Func<DataRow, int>)(sec => sec.Field<int>("SectionID")), (tab, sec) => new { tab = tab, sec = sec }).Where(param0 => param0.tab.Field<int>("TabId") == Convert.ToInt32(dtTab.Rows[i]["TabId"]) & param0.sec.Field<int>("deleteflag") == 0).Select(param0 => param0.tab).Count<DataRow>() == 0)
                        {
                            this.SelectTab(dtTab.Rows[i]["TabName"].ToString());
                            flag = false;
                            break;
                        }
                    }
                }
            }
            if (tableMstSection.Rows.Count > 0)
            {
                for (int index2 = 0; index2 < tableMstSection.Rows.Count; ++index2)
                {
                    for (int index3 = index2 + 1; index3 < tableMstSection.Rows.Count; ++index3)
                    {
                        if (tableMstSection.Rows[index2]["SectionName"].ToString() == tableMstSection.Rows[index3]["SectionName"].ToString() && flag)
                        {
                            MsgBuilder MessageBuilder = new MsgBuilder();
                            MessageBuilder.DataElements["Control"] = tableMstSection.Rows[index2]["SectionName"].ToString();
                            IQCareWindowMsgBox.ShowWindow("PMTCTDupSectionName", MessageBuilder, (Control)this);
                            flag = false;
                            break;
                        }
                    }
                }
                if (flag)
                {
                    for (int index2 = 0; index2 < tableMstSection.Rows.Count; ++index2)
                    {
                        if (tableMstSection.Rows[index2]["IsGridView"].ToString() == "1")
                        {
                            MsgBuilder MessageBuilder = new MsgBuilder();
                            MessageBuilder.DataElements["MessageText"] = "In Section - " + tableMstSection.Rows[index2]["SectionName"].ToString() + " has following error(s)-:";
                            DataView dataView1 = new DataView();
                            DataView defaultView = tableLnkForms.DefaultView;
                            defaultView.RowFilter = "SectionId=" + tableMstSection.Rows[index2]["SectionId"].ToString();
                            if (defaultView.Count > 8)
                            {
                                NameValueCollection dataElements;
                                (dataElements = MessageBuilder.DataElements)["MessageText"] = dataElements["MessageText"] + "\r\n Should not be contain more than 8 fields.";
                                flag = false;
                            }
                            for (int index3 = 0; index3 < defaultView.Count; ++index3)
                            {
                                if (defaultView[index3]["Predefined"].ToString() == "1")
                                {
                                    NameValueCollection dataElements;
                                    (dataElements = MessageBuilder.DataElements)["MessageText"] = dataElements["MessageText"] + "\r\n Field Label - " + defaultView[index3]["FieldLabel"].ToString() + " is predefine field.Should be contain only custom field.";
                                    flag = false;
                                }
                                DataSet dataSet = this.dsFieldDetails.Copy();
                                DataView dataView2 = new DataView(dataSet.Tables[0]);
                                dataView2.RowFilter = "ConditionalField >0  and Id =" + defaultView[index3]["FieldId"].ToString();
                                if (dataView2.Count > 0)
                                {
                                    NameValueCollection dataElements;
                                    (dataElements = MessageBuilder.DataElements)["MessageText"] = dataElements["MessageText"] + "\r\n Field Label - " + defaultView[index3]["FieldLabel"].ToString() + " has a conditional field.";
                                    flag = false;
                                }
                                dataView2.Dispose();
                                if (new DataView(dataSet.Tables[0])
                                {
                                    RowFilter = ("controlID = 9 and Id =" + defaultView[index3]["FieldId"].ToString())
                                }.Count > 0)
                                {
                                    NameValueCollection dataElements;
                                    (dataElements = MessageBuilder.DataElements)["MessageText"] = dataElements["MessageText"] + "\r\n Field Label - " + defaultView[index3]["FieldLabel"].ToString() + " is multi select control.";
                                    flag = false;
                                }
                                dataSet.Dispose();
                                if (new DataView(dataSet.Tables[0])
                                {
                                    RowFilter = ("controlID = 15 and Id =" + defaultView[index3]["FieldId"].ToString())
                                }.Count > 0)
                                {
                                    NameValueCollection dataElements;
                                    (dataElements = MessageBuilder.DataElements)["MessageText"] = dataElements["MessageText"] + "\r\n Field Label - " + defaultView[index3]["FieldLabel"].ToString() + " is Disease/Symptoms control.";
                                    flag = false;
                                }
                                dataSet.Dispose();
                                if (new DataView(dataSet.Tables[0])
                                {
                                    RowFilter = ("controlID = 12 and Id =" + defaultView[index3]["FieldId"].ToString())
                                }.Count > 0)
                                {
                                    NameValueCollection dataElements;
                                    (dataElements = MessageBuilder.DataElements)["MessageText"] = dataElements["MessageText"] + "\r\n Field Label - " + defaultView[index3]["FieldLabel"].ToString() + " is Lab Selection control.";
                                    flag = false;
                                }
                                dataSet.Dispose();
                                if (new DataView(dataSet.Tables[0])
                                {
                                    RowFilter = ("controlID = 11 and Id =" + defaultView[index3]["FieldId"].ToString())
                                }.Count > 0)
                                {
                                    NameValueCollection dataElements;
                                    (dataElements = MessageBuilder.DataElements)["MessageText"] = dataElements["MessageText"] + "\r\n Field Label - " + defaultView[index3]["FieldLabel"].ToString() + " is Drug Selection control.";
                                    flag = false;
                                }
                                dataSet.Dispose();
                                if (new DataView(dataSet.Tables[0])
                                {
                                    RowFilter = ("controlID = 16 and Id =" + defaultView[index3]["FieldId"].ToString())
                                }.Count > 0)
                                {
                                    NameValueCollection dataElements;
                                    (dataElements = MessageBuilder.DataElements)["MessageText"] = dataElements["MessageText"] + "\r\n Field Label - " + defaultView[index3]["FieldLabel"].ToString() + " is ICD10 control.";
                                    flag = false;
                                }
                                dataSet.Dispose();
                            }
                            if (!flag)
                            {
                                int num6 = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
                                break;
                            }
                        }
                    }
                }
            }
            else if (flag)
            {
                IQCareWindowMsgBox.ShowWindow("PMTCTNoSectionCreated", (Control)this);
                flag = false;
            }
            for (int index2 = 0; index2 < tableLnkForms.Rows.Count; ++index2)
            {
                for (int index3 = index2 + 1; index3 < tableLnkForms.Rows.Count; ++index3)
                {
                    if (tableLnkForms.Rows[index2]["FieldId"].ToString() == tableLnkForms.Rows[index3]["FieldId"].ToString() && tableLnkForms.Rows[index2]["Predefined"].ToString() == tableLnkForms.Rows[index3]["Predefined"].ToString() && (flag && tableLnkForms.Rows[index2]["FieldLabel"].ToString() != "PlaceHolder"))
                    {
                        MsgBuilder MessageBuilder = new MsgBuilder();
                        MessageBuilder.DataElements["MessageText"] = "Field Label " + tableLnkForms.Rows[index2]["FieldLabel"].ToString() + " and " + tableLnkForms.Rows[index3]["FieldLabel"].ToString() + " belongs to same field.";
                        int num6 = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
                        flag = false;
                        break;
                    }
                    if (tableLnkForms.Rows[index2]["FieldLabel"].ToString() == tableLnkForms.Rows[index3]["FieldLabel"].ToString() && flag && tableLnkForms.Rows[index2]["FieldLabel"].ToString() != "PlaceHolder")
                    {
                        IQCareWindowMsgBox.ShowWindow("PMTCTDupFieldLabel", (Control)this);
                        flag = false;
                        break;
                    }
                }
            }
            DataSet dataSet1 = this.dsFieldDetails.Copy();
            DataView dataView3 = new DataView();
            DataView defaultView1 = dataSet1.Tables[5].DefaultView;
            for (int index2 = 0; index2 < tableLnkForms.Rows.Count; ++index2)
            {
                for (int index3 = 0; index3 < tableLnkForms.Rows.Count; ++index3)
                {
                    defaultView1.RowFilter = "ConfieldId=" + tableLnkForms.Rows[index2]["FieldID"].ToString() + " and FieldId=" + tableLnkForms.Rows[index3]["FieldID"].ToString();
                    if (defaultView1.ToTable().Rows.Count > 0)
                    {
                        MsgBuilder MessageBuilder = new MsgBuilder();
                        MessageBuilder.DataElements["MessageText"] = "Field Label " + tableLnkForms.Rows[index3]["FieldLabel"].ToString() + " is associate with Field Label " + tableLnkForms.Rows[index2]["FieldLabel"].ToString() + ".";
                        int num6 = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
                        flag = false;
                        break;
                    }
                }
            }
            this.dsFieldDetails.Copy();
            DataSet dataSet2 = this.dsFieldDetails.Copy();
            if (flag)
            {
                for (int index2 = 0; index2 < tableLnkForms.Rows.Count; ++index2)
                {
                    DataView dataView1 = new DataView();
                    DataView defaultView2 = dataSet1.Tables[5].DefaultView;
                    defaultView2.RowFilter = "ConfieldId=" + tableLnkForms.Rows[index2]["FieldID"].ToString();
                    for (int index3 = index2 + 1; index3 < tableLnkForms.Rows.Count; ++index3)
                    {
                        DataView dataView2 = new DataView();
                        DataView defaultView3 = dataSet2.Tables[5].DefaultView;
                        defaultView3.RowFilter = "ConfieldId=" + tableLnkForms.Rows[index3]["FieldID"].ToString();
                        for (int index4 = 0; index4 < defaultView2.ToTable().Rows.Count; ++index4)
                        {
                            for (int index5 = 0; index5 < defaultView3.ToTable().Rows.Count; ++index5)
                            {
                                if (defaultView2.ToTable().Rows[index4]["FieldID"].ToString() == defaultView3.ToTable().Rows[index5]["FieldID"].ToString())
                                {
                                    MsgBuilder MessageBuilder = new MsgBuilder();
                                    MessageBuilder.DataElements["MessageText"] = "Field Label " + tableLnkForms.Rows[index2]["FieldLabel"].ToString() + " and " + tableLnkForms.Rows[index3]["FieldLabel"].ToString() + " has common associate field(s).";
                                    int num6 = (int)IQCareWindowMsgBox.ShowWindowConfirm("#C1", MessageBuilder, (Control)this);
                                    flag = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (flag)
            {
                this.dsCollectDataToSave.Tables.Add(tableMstFeature);
                this.dsCollectDataToSave.Tables.Add(tableMstSection);
                this.dsCollectDataToSave.Tables.Add(tableLnkForms);
                this.dsCollectDataToSave.Tables.Add(frmFormBuilder.dtDeleteFields);
                if (frmFormBuilder.dtDeleteSections != null)
                {
                    frmFormBuilder.dtDeleteSections.TableName = "DeleteSection";
                    this.dsCollectDataToSave.Tables.Add(frmFormBuilder.dtDeleteSections);
                }
                dtTab.TableName = "MstTab";
                this.dsCollectDataToSave.Tables.Add(dtTab);
                tableFormTabSection.TableName = "LnkSectionTab";
                this.dsCollectDataToSave.Tables.Add(tableFormTabSection);
                if (frmFormBuilder.dtDeleteTabs != null)
                {
                    frmFormBuilder.dtDeleteTabs.TableName = "DeleteTab";
                    this.dsCollectDataToSave.Tables.Add(frmFormBuilder.dtDeleteTabs);
                }
                ((IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder"))
                    .SaveFormDetail(this.dsCollectDataToSave, this.dsFieldDetails.Tables[0], GblIQCare.dtformBusinessValues);
                GblIQCare.iFormBuilderId = 0;
                Form form = (Form)Activator.CreateInstance(System.Type.GetType("IQCare.FormBuilder.frmManageForms, IQCare.FormBuilder"));
                form.MdiParent = this.MdiParent;
                form.Left = 0;
                form.Top = 0;
                form.Focus();
                form.Show();
                this.Close();
            }
            this.btnSave.Enabled = true;
        }

        /// <summary>
        /// Selects the tab.
        /// </summary>
        /// <param name="TabName">Name of the tab.</param>
        private void SelectTab(string TabName)
        {
            IQCareWindowMsgBox.ShowWindow("PMTCTNoSectionCreated", (Control)this);
            foreach (TabPage tabPage in this.tabFormBuilder.TabPages)
            {
                if (tabPage.Text == TabName)
                {
                    this.tabFormBuilder.SelectedTab = tabPage;
                    break;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            GblIQCare.iFormBuilderId = 0;
            if (frmFormBuilder.dtDeleteFields.Rows.Count > 0)
                frmFormBuilder.dtDeleteFields.Clear();
            this.dsUpdateMode.Clear();
            this.dsFieldDetails.Clear();
            if (frmFormBuilder.dtDeleteSections != null && frmFormBuilder.dtDeleteSections.Rows.Count > 0)
                frmFormBuilder.dtDeleteSections.Clear();
            if (this.dtMstFeatureForManageField != null && this.dtMstFeatureForManageField.Rows.Count > 0)
                this.dtMstFeatureForManageField.Clear();
            if (this.dtMstSectionForManageField != null && this.dtMstSectionForManageField.Rows.Count > 0)
                this.dtMstSectionForManageField.Clear();
            if (this.dtLnkFormsForManageField != null && this.dtLnkFormsForManageField.Rows.Count > 0)
                this.dtLnkFormsForManageField.Clear();
            Form form = (Form)Activator.CreateInstance(System.Type.GetType("IQCare.FormBuilder.frmManageForms, IQCare.FormBuilder"));
            form.Left = 0;
            form.Top = 0;
            form.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnAddCustomField control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnAddCustomField_Click(object sender, EventArgs e)
        {
            if (this.ValidationCheck())
                return;
            if (this.dtMstFeatureForManageField != null && this.dtMstFeatureForManageField.Rows.Count > 0)
                this.dtMstFeatureForManageField.Clear();
            if (this.dtMstSectionForManageField != null && this.dtMstSectionForManageField.Rows.Count > 0)
                this.dtMstSectionForManageField.Clear();
            if (this.dtLnkFormsForManageField != null && this.dtLnkFormsForManageField.Rows.Count > 0)
                this.dtLnkFormsForManageField.Clear();
            this.dtMstFeatureForManageField = clsCommon.CreateTableMstFeature();
            DataRow row = this.dtMstFeatureForManageField.NewRow();
            row["FeatureId"] = (object)GblIQCare.iFormBuilderId;
            row["FeatureName"] = (object)this.txtFormName.Text.Trim();
            this.dtMstFeatureForManageField.Rows.Add(row);
            this.dtMstSectionForManageField = clsCommon.CreateTableMstSection();
            this.dtLnkFormsForManageField = clsCommon.CreateTableLnk_Forms();
            Form form = (Form)Activator.CreateInstance(System.Type.GetType("IQCare.FormBuilder.frmFieldDetails, IQCare.FormBuilder"));
            form.MdiParent = this.MdiParent;
            form.Deactivate += new EventHandler(this.FrmManageFieldHideChildOnLostFocus);
            form.Left = 0;
            form.Top = 0;
            form.Focus();
            form.Show();
        }

        /// <summary>
        /// FRMs the manage field hide child on lost focus.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FrmManageFieldHideChildOnLostFocus(object sender, EventArgs e)
        {
            (sender as Form).Close();
            GblIQCare.RefillDdlFields = 1;
            this.frmFormBuilder_Load(sender, e);
        }

        /// <summary>
        /// FRMs the manage tab hide child on lost focus.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FrmManageTabHideChildOnLostFocus(object sender, EventArgs e)
        {
            (sender as Form).Close();
            this.RefreshTab();
        }

        /// <summary>
        /// Refreshes the tab.
        /// </summary>
        private void RefreshTab()
        {
            List<TabPage> tabPageList = new List<TabPage>();
            for (int index = 0; index < frmFormBuilder.dtManageTabPos.Rows.Count; ++index)
            {
                foreach (TabPage tabPage in this.tabFormBuilder.TabPages)
                {
                    if (frmFormBuilder.dtManageTabPos.Rows[index]["TabName"].ToString() == tabPage.Text.ToString().Trim())
                    {
                        if (frmFormBuilder.dtManageTabPos.Rows[index]["DeleteFlag"].ToString() == "0")
                            tabPageList.Add(tabPage);
                        else if (frmFormBuilder.dtManageTabPos.Rows[index]["DeleteFlag"].ToString() == "1")
                        {
                            DataRow row = frmFormBuilder.dtDeleteTabs.NewRow();
                            row["TabId"] = frmFormBuilder.dtManageTabPos.Rows[index]["TabId"];
                            row["DeleteFlag"] = frmFormBuilder.dtManageTabPos.Rows[index]["DeleteFlag"];
                            row["TabName"] = frmFormBuilder.dtManageTabPos.Rows[index]["TabName"];
                            frmFormBuilder.dtDeleteTabs.Rows.Add(row);
                        }
                    }
                }
            }
            this.tabFormBuilder.TabPages.Clear();
            foreach (TabPage tabPage in tabPageList)
                this.tabFormBuilder.TabPages.Add(tabPage);
            this.pnlPanel.Refresh();
        }

        /// <summary>
        /// Handles the Click event of the btnManageTab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnManageTab_Click(object sender, EventArgs e)
        {
            if (this.ValidationCheck())
                return;
            if (frmFormBuilder.dtManageTabPos != null)
                frmFormBuilder.dtManageTabPos.Clear();
            frmFormBuilder.dtManageTabPos = clsCommon.ManageTabPos();
            foreach (Control control1 in (ArrangedElementCollection)this.Controls)
            {
                if (control1 is Panel && control1.Name.Contains("pnlPanel"))
                {
                    foreach (Control control2 in (ArrangedElementCollection)control1.Controls)
                    {
                        if (control2 is TabControl)
                        {
                            int num = 1;
                            foreach (TabPage control3 in (ArrangedElementCollection)control2.Controls)
                            {
                                DataRow row = frmFormBuilder.dtManageTabPos.NewRow();
                                if (GblIQCare.iFormBuilderId > 0)
                                {
                                    string[] strArray = control3.Name.Split('_');
                                    if (strArray.Length > 1 && Convert.ToInt32(strArray[2]) > 0)
                                        row["TabId"] = (object)strArray[2];
                                }
                                row["TabName"] = (object)control3.Text.ToString().Trim();
                                row["TopPos"] = (object)num;
                                row["DeleteFlag"] = (object)0;
                                frmFormBuilder.dtManageTabPos.Rows.Add(row);
                                ++num;
                            }
                        }
                    }
                }
            }
            frmManageFBTab frmManageFbTab = new frmManageFBTab();
            frmManageFbTab.MdiParent = this.ParentForm;
            frmManageFbTab.dtTabPos = frmFormBuilder.dtManageTabPos;
            frmManageFbTab.Deactivate += new EventHandler(this.FrmManageTabHideChildOnLostFocus);
            frmManageFbTab.Left = 0;
            frmManageFbTab.Top = 0;
            frmManageFbTab.Focus();
            frmManageFbTab.Show();
        }

        /// <summary>
        /// Handles the Leave event of the txtTabCaptionPlaceHolder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtTabCaptionPlaceHolder_Leave(object sender, EventArgs e)
        {
            if (this.tabFormBuilder.TabPages == null)
                return;
            this.tabFormBuilder.TabPages[this.tabFormBuilder.SelectedIndex].Text = this.txtTabCaptionPlaceHolder.Text;
            this.txtTabCaptionPlaceHolder.Hide();
        }

        /// <summary>
        /// Handles the MouseDoubleClick event of the tabFormBuilder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        private void tabFormBuilder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Rectangle tabRect = this.tabFormBuilder.GetTabRect(this.tabFormBuilder.SelectedIndex);
            this.txtTabCaptionPlaceHolder.Location = new Point(this.tabFormBuilder.Location.X + tabRect.X + 2, this.tabFormBuilder.Location.Y + tabRect.Y + 2);
            this.txtTabCaptionPlaceHolder.Size = new Size(tabRect.Size.Width - 2, tabRect.Height + 2);
            this.txtTabCaptionPlaceHolder.Text = this.tabFormBuilder.TabPages[this.tabFormBuilder.SelectedIndex].Text;
            this.txtTabCaptionPlaceHolder.Show();
            this.txtTabCaptionPlaceHolder.BringToFront();
            this.txtTabCaptionPlaceHolder.Focus();
            this.txtTabCaptionPlaceHolder.SelectAll();
        }

        /// <summary>
        /// Handles the FormClosing event of the frmFormBuilder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void frmFormBuilder_FormClosing(object sender, FormClosingEventArgs e)
        {
            GC.Collect();
        }

        /// <summary>
        /// Handles the Click event of the btnformbusinessrules control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnformbusinessrules_Click(object sender, EventArgs e)
        {
            if (this.txtFormName.Text.ToString().Trim() == "")
            {
                IQCareWindowMsgBox.ShowWindow("PMTCTFormNameMandatory", (Control)this);
                this.txtFormName.Focus();
            }
            else
            {
                if (this.theForm != null)
                    return;
                GblIQCare.iFormMode = 0;
                this.theForm = (Form)Activator.CreateInstance(System.Type.GetType("IQCare.FormBuilder.frmFormBusinessRule, IQCare.FormBuilder"));
                this.theForm.FormClosed += new FormClosedEventHandler(this.theForm_FormClosed);
                this.theForm.Left = 0;
                this.theForm.Top = 0;
                this.theForm.Show();
            }
        }

        /// <summary>
        /// Handles the FormClosed event of the theForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void theForm_FormClosed(object sender, EventArgs e)
        {
            this.theForm = (Form)null;
        }

        private void txtFormName_Leave(object sender, EventArgs e)
        {
            if (txtDisplayName.Text == "")
            {
                txtDisplayName.Text = txtFormName.Text;
            }
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.Form" />.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        //protected override void Dispose(bool disposing)
        //{
        //  if (disposing && this.components != null)
        //    this.components.Dispose();
        //  base.Dispose(disposing);
        //}

        /// <summary>
        /// Initializes the component.
        /// </summary>

    }
}
