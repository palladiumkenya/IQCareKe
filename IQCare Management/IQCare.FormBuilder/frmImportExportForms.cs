using System;
using System.Data;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.FormBuilder;

namespace IQCare.FormBuilder
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmImportExportForms : Form
    {
        /// <summary>
        /// The object ds form details
        /// </summary>
        DataSet objDsFormDetails = new DataSet();
        /// <summary>
        /// Initializes a new instance of the <see cref="frmImportExportForms"/> class.
        /// </summary>
        public frmImportExportForms()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frmImportExportForms control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmImportExportForms_Load(object sender, EventArgs e)
        {
            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //set css end 
            DataSet dsModule;
            DataTable dtAddAll;
            IManageForms objManageForms;
            objManageForms = (IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder");
            dsModule = objManageForms.GetPublishedModuleList();
            dtAddAll = dsModule.Tables[0];
            DataRow theDR = dtAddAll.NewRow();
            theDR["ModuleName"] = "All";
            theDR["ModuleId"] = 0;
            dtAddAll.Rows.InsertAt(theDR, 0);
            BindFunctions objBindControls = new BindFunctions();
            objBindControls.Win_BindCombo(cmbTechArea, dtAddAll, "ModuleName", "ModuleId");
            cmbTechArea.SelectedIndex = 0;
            ddlFormType.SelectedIndex = 0;
            ShowForms();
        }

        /// <summary>
        /// Shows the forms.
        /// </summary>
        public void ShowForms()
        {
            IImportExportForms objFormDetail;
            int iHeight;
            int iTechArea;

            chkLstBoxForms.Items.Clear();

            if (cmbTechArea.SelectedValue.ToString() == "System.Data.DataRowView")
                iTechArea = Convert.ToInt32(((System.Data.DataRowView)(cmbTechArea.SelectedValue)).Row.ItemArray[0]);
            else
                iTechArea = Convert.ToInt32(cmbTechArea.SelectedValue);

            objFormDetail = (IImportExportForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BImportExportForms,BusinessProcess.FormBuilder");
            if (ddlFormType.Text.ToString() == "" || ddlFormType.SelectedItem.ToString() == "Forms")
            {
                objDsFormDetails = objFormDetail.GetAllFormDetail("1", iTechArea.ToString(), Convert.ToInt16(GblIQCare.AppCountryId), "");
            }
            else
            {
                objDsFormDetails = objFormDetail.GetAllFormDetail("1", iTechArea.ToString(), Convert.ToInt16(GblIQCare.AppCountryId), "Home Page");
            }

            if (objDsFormDetails.Tables[0].Rows.Count > 0)
            {

                for (int i = 0; i < objDsFormDetails.Tables[0].Rows.Count; i++)
                {
                    chkLstBoxForms.Items.Add(objDsFormDetails.Tables[0].Rows[i]["FormName"].ToString());

                }

                iHeight = 21 * objDsFormDetails.Tables[0].Rows.Count;
                chkLstBoxForms.Size = new System.Drawing.Size(403, iHeight);
                chkLstBoxForms.Visible = true;
            }
            else
            {
                chkLstBoxForms.Visible = false;
            }

        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkCheckAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < chkLstBoxForms.Items.Count; i++)
            {
                if (chkCheckAll.Checked)
                    chkLstBoxForms.SetItemChecked(i, true);
                else
                    chkLstBoxForms.SetItemChecked(i, false);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnExport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (chkLstBoxForms.CheckedItems.Count > 0)
            {
                DataSet dsCollectDataToSave = new DataSet();

                IImportExportForms objExportFormDetails;
                objExportFormDetails = (IImportExportForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BImportExportForms,BusinessProcess.FormBuilder");
                for (int i = 0; i < chkLstBoxForms.CheckedItems.Count; i++)
                {
                    if (ddlFormType.Text.ToString() == "" || ddlFormType.SelectedItem.ToString() == "Forms")
                    {
                        objDsFormDetails = objExportFormDetails.GetImportExportFormDetail(chkLstBoxForms.CheckedItems[i].ToString());
                        if (dsCollectDataToSave.Tables.Count == 0)
                        {
                            ////mst_feature
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[0].Copy());
                            ////mst_section
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[1].Copy());
                            ////lnk_forms
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[2].Copy());
                            ////Select List Val only for custom fields
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[3].Copy());
                            ////Business Rule only for custom fields
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[4].Copy());
                            ////mst_module-tech area
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[5].Copy());
                            ////module identifier
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[6].Copy());
                            ////Conditional Fields
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[7].Copy());
                            ////Select List Val only for Conditional custom fields
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[8].Copy());
                            ////Business Rule only for Conditional custom fields
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[9].Copy());
                            ////APPAdmin for Version Check
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[10].Copy());
                            ////Field ICD Code Linking Table
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[11].Copy());
                            ////Master Table for Tabs
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[12].Copy());
                            ////Linking Table for Tabs and Section Id
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[13].Copy());
                            ////Special form linking
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[14].Copy());


                        }
                        else
                        {
                            foreach (DataRow dr in objDsFormDetails.Tables[0].Rows)
                            {
                                dsCollectDataToSave.Tables[0].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[1].Rows)
                            {
                                dsCollectDataToSave.Tables[1].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[2].Rows)
                            {
                                dsCollectDataToSave.Tables[2].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[3].Rows)
                            {
                                dsCollectDataToSave.Tables[3].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[4].Rows)
                            {
                                dsCollectDataToSave.Tables[4].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[5].Rows)
                            {
                                dsCollectDataToSave.Tables[5].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[6].Rows)
                            {
                                dsCollectDataToSave.Tables[6].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[7].Rows)
                            {
                                dsCollectDataToSave.Tables[7].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[8].Rows)
                            {
                                dsCollectDataToSave.Tables[8].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[9].Rows)
                            {
                                dsCollectDataToSave.Tables[9].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[10].Rows)
                            {
                                dsCollectDataToSave.Tables[10].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[11].Rows)
                            {
                                dsCollectDataToSave.Tables[11].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[12].Rows)
                            {
                                dsCollectDataToSave.Tables[12].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[13].Rows)
                            {
                                dsCollectDataToSave.Tables[13].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[14].Rows)
                            {
                                dsCollectDataToSave.Tables[14].ImportRow(dr);
                            }


                        }

                    }
                    else //export only home pages
                    {
                        objDsFormDetails = objExportFormDetails.GetImportExportHomeFormDetail(chkLstBoxForms.CheckedItems[i].ToString());

                        if (dsCollectDataToSave.Tables.Count == 0)
                        {
                            ////mst_feature
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[0].Copy());
                            ////mst_section
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[1].Copy());
                            ////lnk_forms
                            dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[2].Copy());
                            //////Select List Val only for custom fields
                            //dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[3].Copy());
                            //////Business Rule only for custom fields
                            //dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[4].Copy());
                            //////mst_module-tech area
                            //dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[5].Copy());
                            //////module identifier
                            //dsCollectDataToSave.Tables.Add(objDsFormDetails.Tables[6].Copy());

                        }
                        else
                        {
                            foreach (DataRow dr in objDsFormDetails.Tables[0].Rows)
                            {
                                dsCollectDataToSave.Tables[0].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[1].Rows)
                            {
                                dsCollectDataToSave.Tables[1].ImportRow(dr);
                            }
                            foreach (DataRow dr in objDsFormDetails.Tables[2].Rows)
                            {
                                dsCollectDataToSave.Tables[2].ImportRow(dr);
                            }

                        }
                    }
                }
                FileDialog oDialog = new SaveFileDialog();
                oDialog.DefaultExt = "xml";
                oDialog.FileName = "Export Form-" + DateTime.Today.ToString("ddMMMyyyy") + ".xml";
                oDialog.Filter = "Form (*.xml)|*.xml";
                if (oDialog.ShowDialog() == DialogResult.OK)
                {
                    string strFilename = oDialog.FileName;
                    dsCollectDataToSave.WriteXml(strFilename);
                }
            }
            else
            {
                IQCareWindowMsgBox.ShowWindow("SelectForm", this);
                txtFileName.Focus();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnBrowse control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FileDialog oDialog = new OpenFileDialog();
            oDialog.DefaultExt = "xml";
            oDialog.Filter = "Form (*.xml)|*.xml";
            if (oDialog.ShowDialog() == DialogResult.OK)
            {
                txtFileName.Text = oDialog.FileName;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnImport control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {

                int iRes;
                if (txtFileName.Text.ToString() != "")
                {
                    DataSet dsCollectDataToSave = new DataSet();
                    dsCollectDataToSave.ReadXml(txtFileName.Text.ToString());
                    IImportExportForms objImportFormDetails;
                    objImportFormDetails = (IImportExportForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BImportExportForms,BusinessProcess.FormBuilder");
                    if (ddlFormType.Text.ToString() == "" || ddlFormType.SelectedItem.ToString() == "Forms")
                    {
                        string strVerName = "";
                        if (dsCollectDataToSave.Tables.Count < 11)
                        {
                            IQCareWindowMsgBox.ShowWindow("ImportFormsCheckVersion", this);
                            return;

                        }
                        else if (dsCollectDataToSave.Tables.Count > 10)
                        {
                            strVerName = dsCollectDataToSave.Tables[10].Rows[0]["AppVer"].ToString();
                            if (GblIQCare.AppVersion.ToString() != strVerName)
                            {
                                IQCareWindowMsgBox.ShowWindow("ImportFormsCheckVersion", this);
                                return;
                            }

                        }
                        iRes = objImportFormDetails.ImportForms(dsCollectDataToSave, GblIQCare.AppUserId, System.Convert.ToInt32(GblIQCare.AppCountryId));
                    }
                    else
                    {
                        iRes = objImportFormDetails.ImportHomeForms(dsCollectDataToSave, GblIQCare.AppUserId, System.Convert.ToInt32(GblIQCare.AppCountryId));
                    }
                    if (iRes == 1)
                    {
                        IQCareWindowMsgBox.ShowWindow("ImportSuccess", this);
                        txtFileName.Text = "";
                    }
                }
                else
                {
                    IQCareWindowMsgBox.ShowWindow("BrowseFile", this);
                    txtFileName.Focus();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnRefresh control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            chkLstBoxForms.Items.Clear();
            ShowForms();
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbTechArea control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbTechArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowForms();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddlFormType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ddlFormType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowForms();
        }
    }
}
