using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.FormBuilder;
using Interface.Security;
using System.Threading;


namespace IQCare.Management
{



    /// <summary>
    /// IQCare MANAGEMNET main form
    /// </summary>
    public partial class frmMain : Form
    {


        /// <summary>
        /// The form
        /// </summary>
        Form theForm;

        /// <summary>
        /// Initializes a new instance of the <see cref="frmMain"/> class.
        /// </summary>
        public frmMain()
        {

            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            this.Text = PMTCTConstants.strIQCareTitle + " [" + GblIQCare.AppUserName + "] - " + GblIQCare.AppLocation;
            lblStatus.Text = GblIQCare.AppVersion + "     Release Date: " + GblIQCare.ReleaseDate;
            lblCopyRight.Text = string.Format("©{0} Palladium",DateTime.Now.Year);

            ThreadPool.QueueUserWorkItem(new WaitCallback(GenerateCache));
            #region "Module Validation"
            DataTable theModTable = GblIQCare.dtModules;
            DataView theDV = new DataView(theModTable);
            theDV.RowFilter = "ModuleId = 201";
            if (theDV.Count < 1)
                mnuPMSCM.Visible = false;

            #endregion

            #region "User Authentication"
            if (GblIQCare.HasFeaturePermission(ApplicationAccess.FormBuilder, GblIQCare.dtUserRight) == false)
            {
                mnuFormBuilder.Visible = false;
                mnuDBMerge.Visible = false;
                mnuDBMerge.Visible = false;
                mnuDBMigration.Visible = false;
                mnuUpsize.Visible = false;
                toolStripSeparator1.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageFields, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuManageFields.Visible = false;
                mnuManageCareEndedFields.Visible = false;

            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.ConfigureHomePages, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuConfigureHomePageForms.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.ConfigureCareTermination, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuConfigCareTermination.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageForms, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuManageForms.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.DatabaseMigration, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuDBMigration.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.Upsize, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuUpsize.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.DatabaseMerge, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuDBMerge.Visible = false;
            }
            //if (GblIQCare.HasFunctionRight(ApplicationAccess.SpecialFormLinking, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            //{
            //    mnuSplFormLinking.Visible = false;
            //}
            if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageTechnicalArea, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuManageModule.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.PatientVisitConfiguration, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuVisitConfiguration.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.DrugDispense, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuPatientDrugDispense.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.PurchaseOrder, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuPurchaseOrder.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.GoodReceiveNotes, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuGoodReceivedNote.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.OpeningStock, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuOpeningStock.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.AdjustStocklevel, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuAdjustStock.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.DisposeItem, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuDisposeItem.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.BatchSummary, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuBatchSummary.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.StockSummary, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuStockSummary.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.ExpiryReport, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuExpiryReport.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.BudgetConfiguration, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuConfigureBudget.Visible = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.PatientVisitConfiguration, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                mnuVisitConfiguration.Visible = false;
            }

            #endregion
        }

        /// <summary>
        /// Handles the Click event of the mnuUpsize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuUpsize_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.Service.frmDataUpsizing, IQCare.Service"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the mnuExit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuExit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();

        }

        /// <summary>
        /// Handles the Click event of the mnuManageForms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuManageForms_Click(object sender, EventArgs e)
        {
            GblIQCare.iManageCareEnded = 0;
            GblIQCare.iConditionalbtn = 1;
            Form theForm;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmManageForms, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the mnuManageFields control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuManageFields_Click(object sender, EventArgs e)
        {
            GblIQCare.iManageCareEnded = 0;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmFieldDetails, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the mnuViewFieldAssociation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuViewFieldAssociation_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmViewAssociation, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the mnuCalculatortool control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuCalculatortool_Click(object sender, EventArgs e)
        {
            Process.Start("Calc.exe");
        }

        /// <summary>
        /// Handles the Click event of the mnuNotepad control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuNotepad_Click(object sender, EventArgs e)
        {
            Process.Start("Notepad.exe");
        }

        /// <summary>
        /// Handles the Click event of the mnuAbout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuAbout_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmAbout, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the FormClosing event of the frmMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        /// <summary>
        /// Handles the Click event of the mnuServiceAdmin control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuServiceAdmin_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.Service.frmService, IQCare.Service"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the mnuDBMigration control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuDBMigration_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.Service.frmMigration, IQCare.Service"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();

        }

        /// <summary>
        /// Handles the Click event of the mnuDBMerge control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuDBMerge_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmImportExportData, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }

        /// <summary>
        /// Generates the cache.
        /// </summary>
        /// <param name="StateInfo">The state information.</param>
        public static void GenerateCache(object StateInfo)
        {
            string xmlPath = GblIQCare.GetXMLPath();
            IIQCareSystem DateManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theDTime = DateManager.SystemDate();
            try
            {
               
                IIQCareSystem theCacheManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem,BusinessProcess.Security");
                DataSet theMainDS = theCacheManager.GetSystemCache();
                DataSet WriteXMLDS = new DataSet();

                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_CouncellingType"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_CouncellingTopic"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Provider"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Division"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Ward"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_District"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Reason"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Education"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Designation"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Employee"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Occupation"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Province"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Village"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Code"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HIVAIDSCareTypes"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARTSponsor"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HivDisease"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Assessment"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Symptom"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Decode"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Feature"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Function"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HivDisclosure"].Copy());
                //WriteXMLDS.Tables.Add(theMainDS.Tables["mst_Satellite"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LPTF"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["mst_StoppedReason"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["mst_facility"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_HIVCareStatus"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_RelationshipType"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_TBStatus"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_ARVStatus"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LostFollowreason"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Regimen"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Store"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Supplier"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["mst_Donor"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Program"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Batch"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["VWDiseaseSymptom"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["mst_RegimenLine"].Copy());
                if (theMainDS.Tables.Contains("Users"))
                {
                    WriteXMLDS.Tables.Add(theMainDS.Tables["Users"].Copy());
                }
                WriteXMLDS.WriteXml(xmlPath + "\\AllMasters.con", XmlWriteMode.WriteSchema);

                WriteXMLDS.Tables.Clear();
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Strength"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_FrequencyUnits"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Drug"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Generic"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_DrugType"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_Frequency"].Copy());
                WriteXMLDS.WriteXml(xmlPath + "\\DrugMasters.con", XmlWriteMode.WriteSchema);

                WriteXMLDS.Tables.Clear();
                WriteXMLDS.Tables.Add(theMainDS.Tables["Mst_LabTest"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_TestParameter"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_LabValue"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["Lnk_ParameterResult"].Copy());
                WriteXMLDS.Tables.Add(theMainDS.Tables["LabTestOrder"].Copy());

                WriteXMLDS.WriteXml(xmlPath + "\\LabMasters.con", XmlWriteMode.WriteSchema);
               // IQCareUtils.WriteCache(ref theMainDS, theDTime.AddDays(-1));
            }
            catch { }
       
        }

        /// <summary>
        /// Handles the Click event of the mnuRefereshSystemCache control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuRefereshSystemCache_Click(object sender, EventArgs e)
        {

           /**/
            try
            {
                GenerateCache(null);
                IQCareWindowMsgBox.ShowWindow("SysCacheRefresh", this);
            }
            catch { }
        }

        /// <summary>
        /// Handles the Click event of the mnuRebuildCustomReportDB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuRebuildCustomReportDB_Click(object sender, EventArgs e)
        {
            IDBMaintenance objDBMaintenance;
            objDBMaintenance = (IDBMaintenance)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BDBMaintenance,BusinessProcess.FormBuilder");
            objDBMaintenance.RebuildCustomRptDB();
        }

        /// <summary>
        /// Handles the Click event of the mnuIQCareDBMaintenance control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuIQCareDBMaintenance_Click(object sender, EventArgs e)
        {
            //rebuild indexes and truncate log
            //pr_SystemAdmin_DBMaintenance_Constella

            IDBMaintenance objDBMaintenance;
            objDBMaintenance = (IDBMaintenance)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BDBMaintenance,BusinessProcess.FormBuilder");
            objDBMaintenance.DBMaintenance();
        }

        /// <summary>
        /// Handles the Click event of the btnService control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnService_Click(object sender, EventArgs e)
        {
            mnuServiceAdmin_Click(sender, e);
        }

        /// <summary>
        /// Handles the Click event of the mnuImportExportForms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuImportExportForms_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmImportExportForms, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the mnuLogOut control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuLogOut_Click(object sender, EventArgs e)
        {
            frmLogin objForm1 = new frmLogin();
            objForm1.Show();
            this.Hide();
            GblIQCare.AppUserId = 0;
            GblIQCare.AppUserName = "";
            GblIQCare.EnrollFlag = 0;
            //GblIQCare.UserRight = theDs.Tables[1].ToString();

            GblIQCare.SystemId = 0;
            GblIQCare.AppCountryId = "";
            GblIQCare.AppDateFormat = "";
            GblIQCare.AppGracePeriod = "";
            GblIQCare.AppLocationId = 0;
            GblIQCare.AppLocation = "";
            GblIQCare.AppPosID = "";
            GblIQCare.AppSatelliteId = "";
            GblIQCare.BackupDrive = "";


        }

        /// <summary>
        /// Handles the Click event of the mnuManageModule control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuManageModule_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmModule, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the mnuQueryBuilder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuQueryBuilder_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmQueryBuilder, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();


        }

        /// <summary>
        /// Handles the Click event of the mnuConfigureHomePageForms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuConfigureHomePageForms_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmHomePageList, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the mnuConfigCareTermination control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuConfigCareTermination_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmCareEndedList, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the mnuManageCareEndedFields control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuManageCareEndedFields_Click(object sender, EventArgs e)
        {
            GblIQCare.iManageCareEnded = 1;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmFieldDetails, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the mnuSplFormLinking control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuSplFormLinking_Click(object sender, EventArgs e)
        {
            //theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmSplFormLink, IQCare.FormBuilder"));
            //theForm.MdiParent = this;
            //theForm.Left = 0;
            //theForm.Top = 2;
            //theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the mnuReportFieldValidation control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuReportFieldValidation_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmRptFieldValidations, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }


        /// <summary>
        /// Handles the Click event of the listViewFormToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void listViewFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.FrmListView, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();


        }

        /// <summary>
        /// Handles the Click event of the gridviewFormToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void gridviewFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.Gridviewform, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();

        }

        /// <summary>
        /// Handles the Click event of the mnuPatientDrugDispense control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuPatientDrugDispense_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmSetUserStore, IQCare.SCM"));
            theForm.MdiParent = this;
            GblIQCare.theArea = "Dispense";
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the itemMasterToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void itemMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmItemMaster, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }

        //private void donorToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmConfigureDonermaster, IQCare.SCM"));
        //    theForm.MdiParent = this;
        //    theForm.Left = 0;
        //    theForm.Top = 2;
        //    theForm.Show();
        //}

        /// <summary>
        /// Handles the Click event of the masterListToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void masterListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            //theForm.WindowState = FormWindowState.Minimized;
            //theForm.WindowState = FormWindowState.Maximized;
        }

        //private void supplyToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmSCMitemList, IQCare.SCM"));
        //    theForm.MdiParent = this;
        //    theForm.Left = 0;
        //    theForm.Top = 2;
        //    theForm.Show();
        //}

        /// <summary>
        /// Handles the Click event of the labItemToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void labItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmLabItemDetails, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the configureBudgetToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void configureBudgetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmConfigureBudget, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the frmDisposeItemDrugsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmDisposeItemDrugsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmDisposeItemDrugs, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the expiryReportToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void expiryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmExpiryReport, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }
        /// <summary>
        /// Handles the Click event of the programItemListToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void programItemListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmProgramItemLinking, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the itemSubItemLinkingToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void itemSubItemLinkingToolStripMenuItem_Click(object sender, EventArgs e)
        {

            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmItemTypeSubTypeLinking, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the supplierToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void supplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmSupplierItem, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the configurePatientVisitToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void configurePatientVisitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmPatientVisitsPerMonth, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the adjustStockLevelToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void adjustStockLevelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmAdjustStockLevel, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the openingStoreToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void openingStoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmOpeningStock, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();

        }

        /// <summary>
        /// Handles the Click event of the holisticBudgetViewToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void holisticBudgetViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmHolisticBudgetView, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the stockSummaryToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void stockSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmStockSummary, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the batchSummaryToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void batchSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmBatchSummaryByStore, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the ReportsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmReports, IQCare.SCM"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the manageRegistrationFieldsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void manageRegistrationFieldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GblIQCare.iManageCareEnded = 2;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmFieldDetails, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the configureToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GblIQCare.iManageCareEnded = 0;
            Form theForm;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmPatientRegistrationManageForms, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the sCMToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void sCMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GblIQCare.iManageCareEnded = 0;
            Form theForm;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmSCM_PharmacyMasterImportExport, IQCare.FormBuilder"));
            theForm.MdiParent = this;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
        }

      

        /// <summary>
        /// Handles the Click event of the mnuPriceList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void mnuPriceList_Click(object sender, EventArgs e)
        {
            //theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmItemCostConfiguration, IQCare.SCM"));
            //theForm.MdiParent = this;
            //theForm.Left = 0;
            //theForm.Top = 2;
            //theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the billingDetailsToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void billingDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmBillingDetails, IQCare.SCM"));
            //theForm.MdiParent = this;
            //theForm.Left = 0;
            //theForm.Top = 2;
            //theForm.Show();
        }

        /// <summary>
        /// Handles the Click event of the billablesToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void billablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GblIQCare.ItemLabel = "Billables";
            //GblIQCare.ItemCategoryId = "213";
            //GblIQCare.ItemTableName = "Decode";
            //GblIQCare.ItemFeatureId = 173;
            //theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmCommonItemMaster,IQCare.SCM"));
            //theForm.MdiParent = this;
            //theForm.Left = 0;
            //theForm.Top = 2;
            //theForm.Show();

            //<ItemName TableName="Decode" CategoryId="213" FormName="frmCommonItemMaster" ListName="Billables" FeatureID="173" Update="0" SystemId="1" ModuleId="0" CountryID="99"/>

        }

        private void registerPatientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmRegisterNewPatient, IQCare.SCM"));
            theForm.MdiParent = this;
            GblIQCare.theArea = "Register";
            GblIQCare.CurrentMenu = MenuChoice.Register;
            theForm.Show();
        }
     
        private void CRMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmSetUserStore, IQCare.SCM"));
            theForm.MdiParent = this;
            GblIQCare.theArea = "CR";
            GblIQCare.CurrentMenu = MenuChoice.CounterRequistion;
            theForm.StartPosition = FormStartPosition.CenterScreen;
            theForm.Show();
        }

        private void IVMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmSetUserStore, IQCare.SCM"));
            theForm.MdiParent = this;

            GblIQCare.theArea = "IV";
            GblIQCare.CurrentMenu = MenuChoice.IssueVoucher;
            GblIQCare.ModePurchaseOrder = 2;

            theForm.StartPosition = FormStartPosition.CenterScreen;
            theForm.Show();
        }
        private void CRIVMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmSetUserStore, IQCare.SCM"));
            theForm.MdiParent = this;
            GblIQCare.theArea = "CR_IV";
            GblIQCare.CurrentMenu = MenuChoice.CRWithIV;
            theForm.StartPosition = FormStartPosition.CenterScreen;
            theForm.Show();

        }

        private void POMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmSetUserStore, IQCare.SCM"));
            theForm.MdiParent = this;
            GblIQCare.theArea = "PO";
            GblIQCare.CurrentMenu = MenuChoice.PurchaseOrder;
            theForm.StartPosition = FormStartPosition.CenterScreen;
            theForm.Show();
        }

        private void GRNMenuItem_Click(object sender, EventArgs e)
        {
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmSetUserStore, IQCare.SCM"));
            theForm.MdiParent = this;
            GblIQCare.theArea = "GRN";
            GblIQCare.CurrentMenu = MenuChoice.GoodReceived;
            theForm.StartPosition = FormStartPosition.CenterScreen;
            theForm.Show();
        }

        private void POGRNMenuItem_Click(object sender, EventArgs e)
        {

            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmSetUserStore, IQCare.SCM"));
            theForm.MdiParent = this;
            GblIQCare.theArea = "PO_GRN";
            GblIQCare.CurrentMenu = MenuChoice.POWithGRN;
            theForm.StartPosition = FormStartPosition.CenterScreen;
            theForm.Show();
        }

      
    }
}
