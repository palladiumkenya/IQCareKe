using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Interface.FormBuilder;
using Application.Presentation;
using System.Configuration;
using System.Web;

namespace IQCare.FormBuilder
{
    public partial class frmManageForms : Form
    {
        Image imgPublished;
        Image imgUnpublished;
        Image imgInprocess;
        Form theForm;
        DataSet objDsFormDetails = new DataSet();
        string strPubValue;
        string strFormName;
        string strFormNameDb;
        string strPublished;
        int iColIndex;


        public frmManageForms()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This function is used to load style sheet and bind the grid;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmManageForms_Load(object sender, EventArgs e)
        {
            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //set css end
            EnableControl();
            cmbFormStatus.SelectedIndex = 3;

            BindFunctions objBindControls = new BindFunctions();
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
            objBindControls.Win_BindCombo(cmbTechArea, dtAddAll, "ModuleName", "ModuleId");
            cmbTechArea.SelectedIndex = 0;

            SetRights();
        }

        public void SetRights()
        {
            //form level permission set

            if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageForms, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnAdd.Enabled = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageForms, FunctionAccess.Update, GblIQCare.dtUserRight) == false)
            {
                btnEdit.Enabled = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageForms, FunctionAccess.Delete, GblIQCare.dtUserRight) == false)
            {
                btnDelete.Enabled = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.SpecialFormLinking, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnLnkServiceArea.Enabled = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.SpecialFormLinking, FunctionAccess.Update, GblIQCare.dtUserRight) == false)
            {
                btnLnkServiceArea.Enabled = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.SpecialFormLinking, FunctionAccess.View, GblIQCare.dtUserRight) == false)
            {
                btnLnkServiceArea.Enabled = false;
            }
        }

        /// <summary>
        /// This function is used to bind grid with database
        /// </summary>
        /// <param name="FieldName"></param>
        public void BindGrid()
        {
            try
            {
                IManageForms objFormDetail;
                objFormDetail = (IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder");
                objDsFormDetails = objFormDetail.GetFormDetail(cmbFormStatus.SelectedIndex.ToString(), (cmbTechArea.SelectedValue == null || cmbTechArea.SelectedIndex == 0) ? "0" : cmbTechArea.SelectedValue.ToString(), Convert.ToInt16(GblIQCare.AppCountryId));
                if (objDsFormDetails.Tables[0].Rows.Count > 0)
                {
                    ClearGrid();
                    string strGetPath = GblIQCare.GetPath();
                    imgPublished = Image.FromFile(strGetPath + "\\Published1.bmp");
                    imgUnpublished = Image.FromFile(strGetPath + "\\Unpublished11.bmp");
                    imgInprocess = Image.FromFile(strGetPath + "\\Inprocess11.bmp");
                    ShowGrid(objDsFormDetails.Tables[0]);
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
        /// This function is used to create datagrid column on runtime
        /// </summary>
        /// <param name="ds"></param>
        public void ShowGrid(DataTable dt)
        {
            try
            {
                dgwFormDetails.DataSource = null;
                DataGridViewTextBoxColumn colFormName = new DataGridViewTextBoxColumn();
                colFormName.HeaderText = "Form Name";
                colFormName.Name = colFormName.DataPropertyName = "FormName";
                colFormName.Width = 450;
                colFormName.ReadOnly = true;

                DataGridViewTextBoxColumn colUpdatedBy = new DataGridViewTextBoxColumn();
                colUpdatedBy.HeaderText = "Updated By";
                colUpdatedBy.Name = colUpdatedBy.DataPropertyName = "UpdatedBy";
                colUpdatedBy.Width = 180;
                colUpdatedBy.ReadOnly = true;

                DataGridViewTextBoxColumn colLastUpdated = new DataGridViewTextBoxColumn();
                colLastUpdated.HeaderText = "Last Updated";
                colLastUpdated.Name = colLastUpdated.DataPropertyName = "LastUpdate";
                colLastUpdated.Width = 120;
                colLastUpdated.ReadOnly = true;

                DataGridViewImageColumn colPublished = new DataGridViewImageColumn();
                colPublished.HeaderText = "Published";
                colPublished.Name = "PublishedImg";
                colPublished.Width = 105;
                colPublished.ReadOnly = false;

                DataGridViewTextBoxColumn colPublished2 = new DataGridViewTextBoxColumn();
                colPublished2.HeaderText = "PublishedValue";
                //colPublished2.HeaderCell.Style = 
                colPublished2.Name = colPublished2.DataPropertyName = "Published";
                colPublished2.Width = 0;
              //  colPublished2.Visible = false;
                
                colPublished2.ReadOnly = false;

                dgwFormDetails.AutoGenerateColumns = false;
                dgwFormDetails.AllowUserToAddRows = false;

                dgwFormDetails.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                dgwFormDetails.DefaultCellStyle.Font.Size.Equals(12);
                // dgwFormDetails.ColumnHeadersDefaultCellStyle= new DataGridViewCellStyle().Font.  

                dgwFormDetails.Columns.Add(colFormName);
                dgwFormDetails.Columns.Add(colUpdatedBy);
                dgwFormDetails.Columns.Add(colLastUpdated);
                dgwFormDetails.Columns.Add(colPublished);
                dgwFormDetails.Columns.Add(colPublished2);
                dgwFormDetails.DataSource = dt;
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }
        /// <summary>
        /// This function is used to close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// This function is used to format the datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwFormDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Color ClrRed = Color.FromArgb(204, 0, 0);
                Color ClrPink = Color.FromArgb(255, 204, 204);
                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    if (dgwFormDetails.Columns[e.ColumnIndex].HeaderText.Equals("PublishedValue"))
                    {
                        if (e.Value == null || e.Value.ToString() == "")
                        {
                            strPubValue = "0";
                        }
                        else
                        {
                            strPubValue = e.Value.ToString();
                        }

                        switch (strPubValue)
                        {
                            case "0":
                                dgwFormDetails.Rows[e.RowIndex].Cells["FormName"].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells["UpdatedBy"].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells["LastUpdate"].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells["PublishedImg"].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells["PublishedImg"].Value = imgInprocess;
                                break;
                            case "1":
                                dgwFormDetails.Rows[e.RowIndex].Cells["FormName"].Style.BackColor = ClrRed;
                                dgwFormDetails.Rows[e.RowIndex].Cells["UpdatedBy"].Style.BackColor = ClrRed;
                                dgwFormDetails.Rows[e.RowIndex].Cells["LastUpdate"].Style.BackColor = ClrRed;
                                dgwFormDetails.Rows[e.RowIndex].Cells["PublishedImg"].Style.BackColor = ClrRed;
                                dgwFormDetails.Rows[e.RowIndex].Cells["PublishedImg"].Value = imgUnpublished;
                                break;
                            case "2":
                                dgwFormDetails.Rows[e.RowIndex].Cells["FormName"].Style.BackColor = System.Drawing.Color.Green;
                                dgwFormDetails.Rows[e.RowIndex].Cells["UpdatedBy"].Style.BackColor = System.Drawing.Color.Green;
                                dgwFormDetails.Rows[e.RowIndex].Cells["LastUpdate"].Style.BackColor = System.Drawing.Color.Green;
                                dgwFormDetails.Rows[e.RowIndex].Cells["PublishedImg"].Style.BackColor = System.Drawing.Color.Green;
                                dgwFormDetails.Rows[e.RowIndex].Cells["PublishedImg"].Value = imgPublished;
                                break;
                            default:
                                dgwFormDetails.Rows[e.RowIndex].Cells["FormName"].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells["UpdatedBy"].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells["LastUpdate"].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells["PublishedImg"].Style.BackColor = ClrPink;
                                dgwFormDetails.Rows[e.RowIndex].Cells["PublishedImg"].Value = imgInprocess;
                                break;
                        }
                    }

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
        /// This function is used when formstatus is changed in form Status combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFormStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgwFormDetails.DataSource = null;
            BindGrid();
            if (cmbFormStatus.SelectedIndex.ToString() == "1")
            {
                DisableControl();
            }
            else
            {
                EnableControl();
            }

        }
        /// <summary>
        /// This function is used to ad the new form(here we navigate from form details to formbuilder)   
        /// /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            GblIQCare.iFormBuilderId = 0;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmFormBuilder, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
            this.Close();
        }
        /// <summary>
        /// This function is used to Edit the form details(here we only pass the form id and navigate to formbuilder form
        /// </summary>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < objDsFormDetails.Tables[0].Rows.Count; i++)
                {
                    if (strFormNameDb == objDsFormDetails.Tables[0].Rows[i]["FormName"].ToString())
                    {
                        if (objDsFormDetails.Tables[0].Rows[i]["Published"].ToString() == "2")
                        {
                            IQCareWindowMsgBox.ShowWindow("PMTCTPublishedForm", this);
                            return;
                        }
                        else
                        {
                            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmFormBuilder, IQCare.FormBuilder"));
                            theForm.MdiParent = this.MdiParent;
                            theForm.Left = 0;
                            theForm.Top = 0;
                            theForm.Show();
                            this.Close();
                        }
                    }
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
        /// This function is used to preview  the form in HTML viwewer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (strFormNameDb == null)
                {
                    IQCareWindowMsgBox.ShowWindow("PMTCTSelectAtleastOne", this);
                    return;
                }
                else
                {
                    theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmPreview, IQCare.FormBuilder"));
                    theForm.MdiParent = this.MdiParent;
                    theForm.Left = 0;
                    theForm.Top = 0;
                    theForm.Show();
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
        /// This function is used to delete the selected form if no data is available for that form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (strFormNameDb == null)
                {
                    IQCareWindowMsgBox.ShowWindow("PMTCTSelectAtleastOne", this);
                    return;
                }
                else
                {
                    for (int i = 0; i < objDsFormDetails.Tables[0].Rows.Count; i++)
                    {
                        if (strFormNameDb == objDsFormDetails.Tables[0].Rows[i]["FormName"].ToString())
                        {
                            if (objDsFormDetails.Tables[0].Rows[i]["Published"].ToString() == "2")
                            {
                                IQCareWindowMsgBox.ShowWindow("PMTCTPublishedForm", this);
                                return;
                            }
                            else
                            {
                                IManageForms objFormDetail;
                                objFormDetail = (IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder");
                                objDsFormDetails = objFormDetail.CheckFormDetail(strFormName, GblIQCare.iFormBuilderId);

                                if (objDsFormDetails.Tables[0].Rows.Count > 0)
                                {
                                    if (objDsFormDetails.Tables[1].Rows[0]["Form_Exist"].ToString() == "0" || objDsFormDetails.Tables[1].Rows[0]["Form_Exist"].ToString() == null || objDsFormDetails.Tables[1].Rows[0]["Form_Exist"].ToString() == "")
                                    {
                                        DialogResult strResult;
                                        //strResult = MessageBox.Show("Do you want to delete this record", "Yes/No", MessageBoxButtons.YesNo);
                                        strResult = IQCareWindowMsgBox.ShowWindowConfirm("PMTCTConfirmDelete", this);

                                        if (strResult.ToString() == "Yes")
                                        {
                                            objFormDetail.DeleteFormTableDetail(strFormName, GblIQCare.iFormBuilderId);
                                            dgwFormDetails.DataSource = null;
                                            BindGrid();
                                            MessageBox.Show("Form " + strFormNameDb + " deleted sucessfully", "Confirm", MessageBoxButtons.OK);
                                            strFormNameDb = null;
                                        }
                                        else
                                        {
                                            dgwFormDetails.DataSource = null;
                                            BindGrid();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        IQCareWindowMsgBox.ShowWindow("PMTCTNotDeleted", this);
                                        dgwFormDetails.DataSource = null;
                                        BindGrid();
                                        return;
                                    }
                                }
                            }
                        }
                    }
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
        /// This function is used when we click on particular row/cell
        /// This is used to fetch the datailks of particular row/cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwFormDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageForms, FunctionAccess.Update, GblIQCare.dtUserRight) == true)
                {
                    EnableControl();

                    if (e.RowIndex != -1)
                    {
                        strFormName = "";
                        strFormNameDb = dgwFormDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
                        strFormName = strFormNameDb.Replace(" ", "_");
                        for (int i = 0; i < objDsFormDetails.Tables[0].Rows.Count; i++)
                        {
                            if (strFormNameDb == objDsFormDetails.Tables[0].Rows[i]["FormName"].ToString())
                            {
                                GblIQCare.iFormBuilderId = Convert.ToInt32(objDsFormDetails.Tables[0].Rows[i]["FormId"]);
                                strPublished = objDsFormDetails.Tables[0].Rows[i][3].ToString();
                                break;
                            }

                        }
                        if (strPublished == "2")
                        {
                            DisableControl();
                        }
                        else
                        {
                            EnableControl();
                        }
                        if (e.ColumnIndex == -1)
                        {
                            iColIndex = 0;
                        }
                        else
                        {
                            iColIndex = e.ColumnIndex;
                        }
                        if (dgwFormDetails.Columns[iColIndex].HeaderText == "Published")
                        {

                            if (strPublished == "0" || strPublished == "1")
                            {
                                IManageForms objFormDetail;
                                objFormDetail = (IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder");
                                DialogResult strResult;
                                strResult = MessageBox.Show("Do you want to publish this form", "Yes/No", MessageBoxButtons.YesNo);
                                if (strResult.ToString() == "Yes")
                                {
                                    string strValue;
                                    strValue = "2";
                                    int rowstate = objFormDetail.ResetFormStatus(GblIQCare.iFormBuilderId, strValue, GblIQCare.AppUserId);
                                    dgwFormDetails.DataSource = null;
                                    BindGrid();
                                }

                            }
                            else
                            {
                                IManageForms objFormDetail;
                                objFormDetail = (IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder");
                                DialogResult strResult;
                                strResult = MessageBox.Show("Do you want to unpublish this form?", "Yes/No", MessageBoxButtons.YesNo);
                                if (strResult.ToString() == "Yes")
                                {
                                    string strValue;
                                    strValue = "1";
                                    int rowstate = objFormDetail.ResetFormStatus(GblIQCare.iFormBuilderId, strValue, GblIQCare.AppUserId);
                                    dgwFormDetails.DataSource = null;
                                    BindGrid();
                                    EnableControl();

                                }

                            }
                        }
                    }
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
        /// This function is used to clear the grid.
        /// </summary>
        private void ClearGrid()
        {
            dgwFormDetails.Columns.Clear();
            dgwFormDetails.Rows.Clear();
            dgwFormDetails.Refresh();

        }
        /// <summary>
        /// This function is used when we double click on particular row/cell
        /// It will open the formbuilder form in edit mode.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwFormDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageForms, FunctionAccess.Update, GblIQCare.dtUserRight) == true)
                {

                    EnableControl();

                    if (e.ColumnIndex != -1 && e.RowIndex != -1)
                    {
                        strFormName = "";
                        strFormNameDb = dgwFormDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
                        strFormName = strFormNameDb.Replace(" ", "_");
                        for (int i = 0; i < objDsFormDetails.Tables[0].Rows.Count; i++)
                        {
                            if (strFormNameDb == objDsFormDetails.Tables[0].Rows[i]["FormName"].ToString())
                            {
                                GblIQCare.iFormBuilderId = Convert.ToInt32(objDsFormDetails.Tables[0].Rows[i]["FormId"]);
                                strPublished = objDsFormDetails.Tables[0].Rows[i][3].ToString();
                                break;
                            }
                        }
                    }
                    if (strPublished == "2")
                    {
                        DisableControl();
                    }
                    else
                    {
                        EnableControl();
                    }
                    for (int i = 0; i < objDsFormDetails.Tables[0].Rows.Count; i++)
                    {
                        if (strFormNameDb == objDsFormDetails.Tables[0].Rows[i]["FormName"].ToString())
                        {
                            if (objDsFormDetails.Tables[0].Rows[i]["Published"].ToString() == "2")
                            {
                                IQCareWindowMsgBox.ShowWindow("PMTCTPublishedForm", this);
                                return;
                            }
                            else
                            {
                                theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmFormBuilder, IQCare.FormBuilder"));
                                theForm.MdiParent = this.MdiParent;
                                theForm.Left = 0;
                                theForm.Top = 0;
                                theForm.Show();
                                this.Close();
                            }

                        }
                    }
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
        /// This function is used to Enable the edit & delete button if form is published.
        /// </summary>
        private void EnableControl()
        {
            btnEdit.Enabled = true;
            btnDelete.Enabled = true;
            SetRights();
        }
        /// <summary>
        /// This function is used to Disable the edit & delete button if form is published.
        /// </summary>
        private void DisableControl()
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void cmbTechArea_SelectedIndexChanged(object sender, EventArgs e)
        {

            dgwFormDetails.DataSource = null;
            BindGrid();
            if (cmbFormStatus.SelectedIndex.ToString() == "1")
            {
                DisableControl();
            }
            else
            {
                EnableControl();
            }
        }

        private void btnOrderForms_Click(object sender, EventArgs e)
        {

            //theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmOrderForms, IQCare.FormBuilder"));
            //theForm.MdiParent = this.MdiParent;
            //theForm.Left = 0;
            //theForm.Top = 0;
            //theForm.Show();
            // this.Close();

            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmOrderForms, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Deactivate += new EventHandler(FrmOrderFormsHideChildOnLostFocus);
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Focus();
            theForm.Show();
        }
        private void FrmOrderFormsHideChildOnLostFocus(object sender, EventArgs e)
        {
            Form theSenderForm = sender as Form;
            theSenderForm.Close();
            //iControlCount = 0;
            //GblIQCare.ResetSection = 1;
            frmManageForms_Load(sender, e);
        }
        private void btnLnkServiceArea_Click(object sender, EventArgs e)
        {
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmFormModuleLink, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Deactivate += new EventHandler(FrmOrderFormsHideChildOnLostFocus);
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Focus();
            theForm.Show();
        }
    }


}
