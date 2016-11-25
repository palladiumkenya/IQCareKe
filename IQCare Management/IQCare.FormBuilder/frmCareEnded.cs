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


namespace IQCare.FormBuilder
{
    public partial class frmCareEnded : Form
    {
        int ID = 0;
        ComboBox theGrdCombo;
        ComboBox theGrdComboExitReason;
        DataTable theExitReasonFields = new DataTable();
        DataTable theExitFields = new DataTable();
        DataSet dsCareEndedInfo = new DataSet();
        DataTable theExitDeathReason = new DataTable();
       
        public frmCareEnded()
        {
            InitializeComponent();
        }

        private void frmCareEnded_Load(object sender, EventArgs e)
        {
            try
            {
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);

                pnl_maingrd.Width = 855;
                pnl_maingrd.Height = 455;
                pnl_ReasonSelect.Visible = false;
                pnlCondFields.Visible = false;
                pnl_DeathReasonSelect.Visible = false;
                GblIQCare.dtConditionalFields.Clear();
                Init_Form();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();

                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }
        }
        private void Init_Form()
         {
            ICareEndedConfiguration objCareEnd = (ICareEndedConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BCareEndedConfiguration,BusinessProcess.FormBuilder");
            DataSet dsCareEndedDetails = objCareEnd.GetCareEndedDetails();
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(cmbTechnicalArea, dsCareEndedDetails.Tables[0], "ModuleName", "ModuleId");
           
            dsCareEndedInfo = objCareEnd.GetCareEndedInfo(GblIQCare.iCareEndedId);

            theExitDeathReason = dsCareEndedInfo.Tables[6].Copy();

            if (dsCareEndedInfo.Tables[0].Rows.Count > 0)
            {
                ID = Convert.ToInt32(dsCareEndedInfo.Tables[0].Rows[0]["Section"].ToString());
            }
            if (dsCareEndedInfo.Tables[5].Rows.Count > 0)
            {
                cmbTechnicalArea.SelectedValue=dsCareEndedInfo.Tables[5].Rows[0]["ModuleId"].ToString();
                cmbTechnicalArea.Enabled = false;
            }
            else
                cmbTechnicalArea.SelectedValue = 0;
            #region "FilterFromReason"
            DataTable theExitReason = dsCareEndedInfo.Tables[2].Copy();
            foreach (DataRow theDR in dsCareEndedInfo.Tables[3].Rows)
            {
                DataView theDV = new DataView(theExitReason);
                theDV.RowFilter = "Id=" + theDR["ExitReasonsId"].ToString();
                theDV.Delete(0);
            }
            #endregion

            theBind.Win_BindListBox(lstExitReason, theExitReason, "Name", "Id");
            theBind.Win_BindListBox(lstSelExitReason, dsCareEndedInfo.Tables[3], "Name", "ExitReasonsId");
           
            if(dsCareEndedInfo.Tables[0].Rows.Count<1)
            {
                #region "PreSelectedFields"
                DataRow theDR = dsCareEndedInfo.Tables[0].NewRow();
                theDR["FieldId"] = "172";
                theDR["Field Label"] = "Care Ended Exit Reason";
                theDR["Display Type"] = "Select List";
                theDR["Predefine"] = "1";
                
                dsCareEndedInfo.Tables[0].Rows.Add(theDR);

                theDR = dsCareEndedInfo.Tables[0].NewRow();
                theDR["FieldId"] = "176";
                theDR["Field Label"] = "Care Ended Date";
                theDR["Display Type"] = "Date";
                theDR["Predefine"] = "1";
               
                dsCareEndedInfo.Tables[0].Rows.Add(theDR);

                theDR = dsCareEndedInfo.Tables[0].NewRow();
                theDR["FieldId"] = "177";
                theDR["Field Label"] = "Signature";
                theDR["Display Type"] = "Select List";
                theDR["Predefine"] = "1";
               
                dsCareEndedInfo.Tables[0].Rows.Add(theDR);
                #endregion
            
            }
            DataColumn theColumn = dsCareEndedInfo.Tables[0].Columns["FieldId"];
            dsCareEndedInfo.Tables[0].Columns["Field Label"].AllowDBNull = false;
            dsCareEndedInfo.Tables[0].Constraints.Add("Pk", theColumn, true);
           
            BindGrid(dsCareEndedInfo);

            DataColumn[] thePkey = new DataColumn[2];
            thePkey.SetValue(dsCareEndedInfo.Tables[4].Columns["FieldId"], 0);
            thePkey.SetValue(dsCareEndedInfo.Tables[4].Columns["ExitReasonId"], 1);
            dsCareEndedInfo.Tables[4].Constraints.Add("Pk_ExitReason", thePkey, true);
            //dsCareEndedInfo.Tables[4].Constraints.Add("Req_FieldLable", dsCareEndedInfo.Tables[4].Columns["Field Label"], false);
            dsCareEndedInfo.Tables[4].Columns["Field Label"].AllowDBNull = false;
            //dsCareEndedInfo.Tables[4].Constraints.Add("Pk_ExitReasons", dsCareEndedInfo.Tables[4].Columns["FieldId"], true);
            theExitReasonFields = dsCareEndedInfo.Tables[4];
           
            BindGrid_ExitReason(dsCareEndedInfo);
        }
        private void BindGrid(DataSet theDS)
        {
            string strGetPath = GblIQCare.GetPath();
            #region "BindGridCombo"
            theExitFields = theDS.Tables[1];
            //DataRow theDR = theExitFields.NewRow();
            //theDR[0] = "0";
            //theDR[1] = "Select";
            //theExitFields.Rows.InsertAt(theDR, 0);
            #endregion

            dgwCareEnded.DataSource = null;
            dgwCareEnded.Columns.Clear();
            dgwCareEnded.AutoGenerateColumns = false;

            DataGridViewComboBoxColumn theColumnFieldName = new DataGridViewComboBoxColumn();
            theColumnFieldName.HeaderText = "Table Field Name";
            theColumnFieldName.Name = "Table Field Name";
            theColumnFieldName.DataPropertyName = "FieldId";
            theColumnFieldName.DataSource = theExitFields;
            theColumnFieldName.DisplayMember = "PdfName";
            theColumnFieldName.ValueMember = "Id";
            theColumnFieldName.Width = 228;
            
            DataGridViewTextBoxColumn theColumnFieldLabel = new DataGridViewTextBoxColumn();
            theColumnFieldLabel.HeaderText = "Field Label";
            theColumnFieldLabel.DataPropertyName = "Field Label";
            theColumnFieldLabel.Width = 150;

            DataGridViewTextBoxColumn theColumnDisplay = new DataGridViewTextBoxColumn();
            theColumnDisplay.HeaderText = "Display Type";
            theColumnDisplay.DataPropertyName = "Display Type";
            theColumnDisplay.Name = "Display Type";
            theColumnDisplay.Width = 110;
            
            DataGridViewImageColumn theColumnAssociatedFields = new DataGridViewImageColumn();
            theColumnAssociatedFields.HeaderText = "Associated Fields";
            theColumnAssociatedFields.Name = "Associated Fields";
            theColumnAssociatedFields.Width = 100;

            DataGridViewTextBoxColumn theColumnFieldPredefine = new DataGridViewTextBoxColumn();
            theColumnFieldPredefine.HeaderText = "Predefine";
            theColumnFieldPredefine.DataPropertyName = "Predefine";
            theColumnFieldPredefine.Width = 150;
            theColumnFieldPredefine.Name = "Predefine";
            theColumnFieldPredefine.Visible = false;

            DataGridViewImageColumn theColumnList = new DataGridViewImageColumn();
            theColumnList.HeaderText = "List";
            theColumnList.Name = "List";
            theColumnList.Width = 90;

            DataGridViewImageColumn theColumnBusiness = new DataGridViewImageColumn();
            theColumnBusiness.HeaderText = "Business";
            theColumnBusiness.DataPropertyName = "Business";
            theColumnBusiness.Name = "Business";
            theColumnBusiness.Width = 90;

            DataGridViewTextBoxColumn theColumnSeq = new DataGridViewTextBoxColumn();
            theColumnSeq.HeaderText = "Seq";
            theColumnSeq.DataPropertyName = "Seq";
            theColumnSeq.Width = 10;
            theColumnSeq.Visible = false;
                
            dgwCareEnded.DataSource = theDS.Tables[0];
            dgwCareEnded.Columns.Add(theColumnFieldName);
            dgwCareEnded.Columns.Add(theColumnFieldLabel);
            dgwCareEnded.Columns.Add(theColumnDisplay);
            dgwCareEnded.Columns.Add(theColumnAssociatedFields);
            dgwCareEnded.Columns.Add(theColumnFieldPredefine);
            dgwCareEnded.Columns.Add(theColumnList);
            dgwCareEnded.Columns.Add(theColumnBusiness);
            dgwCareEnded.Columns.Add(theColumnSeq);


        }

        private void BindGrid_ExitReason(DataSet theDS)
        {
            string strGetPath = GblIQCare.GetPath();
           
            dgwExitReason.DataSource = null;
            dgwExitReason.Columns.Clear();
            dgwExitReason.AutoGenerateColumns = false;

            DataGridViewComboBoxColumn theColumnExitReasonFieldName = new DataGridViewComboBoxColumn();
            theColumnExitReasonFieldName.HeaderText = "Table Field Name";
            theColumnExitReasonFieldName.Name = "Table Field Name";
            theColumnExitReasonFieldName.DataPropertyName = "FieldId";
            theColumnExitReasonFieldName.DataSource = theExitFields;
            theColumnExitReasonFieldName.DisplayMember = "PdfName";
            theColumnExitReasonFieldName.ValueMember = "Id";
            theColumnExitReasonFieldName.Width = 251;

            DataGridViewTextBoxColumn theColumnExitReasonFieldLabel = new DataGridViewTextBoxColumn();
            theColumnExitReasonFieldLabel.HeaderText = "Field Label";
            theColumnExitReasonFieldLabel.DataPropertyName = "Field Label";
            theColumnExitReasonFieldLabel.Width = 164;

            DataGridViewTextBoxColumn theColumnExitReasonDisplay = new DataGridViewTextBoxColumn();
            theColumnExitReasonDisplay.HeaderText = "Display Type";
            theColumnExitReasonDisplay.Name = "Display Type";
            theColumnExitReasonDisplay.DataPropertyName = "Display Type";
            theColumnExitReasonDisplay.Width = 160;


            DataGridViewImageColumn theColumnExitReasonList = new DataGridViewImageColumn();
            theColumnExitReasonList.HeaderText = "List";
            theColumnExitReasonList.Name = "List";
            theColumnExitReasonList.Width = 160;

            DataGridViewImageColumn theColumnExitReasonBusiness = new DataGridViewImageColumn();
            theColumnExitReasonBusiness.HeaderText = "Business";
            theColumnExitReasonBusiness.DataPropertyName = "Business";
            theColumnExitReasonBusiness.Name = "Business";
            theColumnExitReasonBusiness.Width = 140;

            DataGridViewTextBoxColumn theColumnExitReasonId = new DataGridViewTextBoxColumn();
            theColumnExitReasonId.HeaderText = "ExitReasonId";
            theColumnExitReasonId.DataPropertyName = "ExitReasonId";
            theColumnExitReasonId.Visible = false;
            theColumnExitReasonId.ReadOnly = true;

            DataGridViewTextBoxColumn theColumnExitReasonPredefine = new DataGridViewTextBoxColumn();
            theColumnExitReasonPredefine.HeaderText = "Predefine";
            theColumnExitReasonPredefine.DataPropertyName = "Predefine";
            theColumnExitReasonPredefine.Width = 10;
            theColumnExitReasonPredefine.Name = "Predefine";
            theColumnExitReasonPredefine.Visible = false;

            DataGridViewTextBoxColumn theColumnExitReasonSeq = new DataGridViewTextBoxColumn();
            theColumnExitReasonSeq.HeaderText = "Seq";
            theColumnExitReasonSeq.DataPropertyName = "Seq";
            theColumnExitReasonSeq.Width = 10;
            theColumnExitReasonSeq.Visible = false;

            dgwExitReason.DataSource = theExitReasonFields;
            dgwExitReason.Columns.Add(theColumnExitReasonFieldName);
            dgwExitReason.Columns.Add(theColumnExitReasonFieldLabel);
            dgwExitReason.Columns.Add(theColumnExitReasonDisplay);
            dgwExitReason.Columns.Add(theColumnExitReasonList);
            dgwExitReason.Columns.Add(theColumnExitReasonBusiness);
            dgwExitReason.Columns.Add(theColumnExitReasonId);
            dgwExitReason.Columns.Add(theColumnExitReasonPredefine);
            dgwExitReason.Columns.Add(theColumnExitReasonSeq);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmCareEndedList, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            this.Close();
            theForm.Show();
        }

        private void dgwCareEnded_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Image imgQuery;
          //  Image imgQuery2;
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                if (dgwCareEnded.Columns[e.ColumnIndex].Name == "Associated Fields")
                {
                    dgwCareEnded.Rows[e.RowIndex].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    //imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\associatedfield.jpg");
                    //imgQuery2 = Image.FromFile(GblIQCare.GetPath() + "\\associatedfieldGrey.jpg");
                    //e.Value = imgQuery;
                    string display = string.Empty;
                    if (dgwCareEnded.Rows[e.RowIndex].Cells["Display Type"].Value != null)
                    {
                        display = dgwCareEnded.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
                    }
                    if (display.Trim() == "Select List" || display == "4" || display.Trim() == "Multi Select" || display == "9")
                    {
                        imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\associatedfield.jpg");
                        e.Value = imgQuery;
                    }
                    else
                    {
                        imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\associatedfieldGrey.jpg");
                        e.Value = imgQuery;
                    }
                }
                if (dgwCareEnded.Columns[e.ColumnIndex].Name == "List")
                {
                    dgwCareEnded.Rows[e.RowIndex].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    string display = string.Empty;
                    if (dgwCareEnded.Rows[e.RowIndex].Cells["Display Type"].Value != null)
                    {
                        display = dgwCareEnded.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
                    }
                    if (display.Trim() == "Select List" || display == "4" || display.Trim() == "Multi Select" || display == "9")
                    {
                        imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\List.bmp");
                        e.Value = imgQuery;
                    }
                    else
                    {
                        imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\listdesible.bmp");
                        e.Value = imgQuery;
                    }

                }

                if (dgwCareEnded.Columns[e.ColumnIndex].Name == "Business")
                {
                    dgwCareEnded.Rows[e.RowIndex].Cells[5].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\brule.bmp");
                    e.Value = imgQuery;
                }
            }
        }
     
        private void dgwCareEnded_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType().ToString() == "System.Windows.Forms.DataGridViewComboBoxEditingControl")
            {
                theGrdCombo = (ComboBox)e.Control;
                theGrdCombo.SelectedValueChanged += null;
                theGrdCombo.SelectedValueChanged += new EventHandler(theCombo_SelectedValueChanged);
            }
        }

        void theCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            
            if (theGrdCombo.Focused == true && theGrdCombo.SelectedValue != null)
            {
                if (theGrdCombo.DataSource != null && theGrdCombo.SelectedValue.GetType().ToString() != "System.Data.DataRowView")
                {
                    DataTable theDT = (DataTable)theGrdCombo.DataSource;
                    DataView theDV = new DataView(theDT.Copy());
                    theDV.RowFilter = "Id = " + theGrdCombo.SelectedValue.ToString();
                    if (theDV.Count > 0)
                    {
                        dgwCareEnded.CurrentRow.Cells[2].Value = theDV[0]["Name"].ToString();
                        dgwCareEnded.CurrentRow.Cells[4].Value = theDV[0]["Predefine"].ToString();
                    }
                }
            }
        }
        void theComboExitReason_SelectedValueChanged(object sender, EventArgs e)
        {

            
                if (theGrdComboExitReason.Focused == true && theGrdComboExitReason.SelectedValue != null && theGrdComboExitReason.SelectedValue.GetType().ToString() != "System.Data.DataRowView"
                         && Convert.ToInt32(theGrdComboExitReason.SelectedValue) > 0)
                {
                    if (theGrdComboExitReason.DataSource != null)
                    {

                        DataTable theDT = (DataTable)theGrdComboExitReason.DataSource;
                        DataView theDV = new DataView(theDT);
                        theDV.RowFilter = "Id = " + theGrdComboExitReason.SelectedValue.ToString();
                        if (theDV.Count > 0)
                        {

                            

                            dgwExitReason.CurrentRow.Cells[2].Value = theDV[0]["Name"].ToString();
                            dgwExitReason.CurrentRow.Cells[5].Value = cmbExitReason.SelectedValue;
                            dgwExitReason.CurrentRow.Cells[6].Value = Convert.ToInt32(theDV[0]["Predefine"].ToString());
                           
                        }

                    }

                }
            
            
        }

        private void dgwCareEnded_CellClick(object sender, DataGridViewCellEventArgs e)
        {
     

            GblIQCare.gblRowIndex = e.RowIndex;
            if (e.RowIndex > dgwCareEnded.Rows.Count - 2)
                return;

            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                GblIQCare.strDisplayType = "";
                GblIQCare.strFieldName = "";
                GblIQCare.strSelectFieldName = "";
                GblIQCare.strSelectListValue = "";
                GblIQCare.strSelectList = "";
                if (dgwCareEnded.Columns[e.ColumnIndex].Name == "Business")
                {
                    //string strDisplay = string.Empty;
                    if (dgwCareEnded.Rows[e.RowIndex].Cells["Business"].Value != null && dgwCareEnded.Rows[e.RowIndex].Cells["Business"].Value.ToString() != "0" && dgwCareEnded.Rows[e.RowIndex].Cells["Business"].Value.ToString() != "System.Drawing.Bitmap")
                    {
                        GblIQCare.strDisplayType = dgwCareEnded.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
                        GblIQCare.intFieldDetaisChange = Convert.ToInt32(dgwCareEnded.Rows[e.RowIndex].Cells[0].Value);
                        GblIQCare.strFieldName = dgwCareEnded.Rows[e.RowIndex].Cells["Table Field Name"].FormattedValue.ToString();
                        GblIQCare.dtBusinessValues.Clear();
                        GblIQCare.dtBusinessValues.Columns.Clear();
                        GblIQCare.dtBusinessValues.Rows.Clear();
                        IFieldDetail objFieldDetail;
                        objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                        //pass fieldName if want to retrieve specific field from GetCustomFields

                        DataSet dsFieldDetails = new DataSet();
                        dsFieldDetails = objFieldDetail.GetCustomFields("", (cmbTechnicalArea.SelectedIndex == 0) ? 0 : Convert.ToInt32(cmbTechnicalArea.SelectedValue), GblIQCare.iManageCareEnded);
                        DataSet dsFillBusinessRule = dsFieldDetails.Copy();
                        if (dsFillBusinessRule.Tables[4].Rows.Count > 0)
                        {
                            DataView dv;
                            dv = dsFillBusinessRule.Tables[4].DefaultView;
                            //dv.RowFilter = "FieldID=" + dgwCareEnded.Rows[e.RowIndex].Cells["FieldId"].Value + " and predefined=" + dgwCareEnded.Rows[e.RowIndex].Cells["Predefined"].Value.ToString();

                            dv.RowFilter = "FieldId='" + dgwCareEnded.Rows[e.RowIndex].Cells[0].Value.ToString().Substring(1,dgwCareEnded.Rows[e.RowIndex].Cells[0].Value.ToString().Length -1) + "' and Predefined=" + dgwCareEnded.Rows[e.RowIndex].Cells[4].Value.ToString();
                            DataView DvFilter = new DataView();
                            DataTable dt = new DataTable();
                            dt = dv.ToTable();
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    if (GblIQCare.objhashBusinessRule.ContainsKey(e.RowIndex))
                                    {
                                        GblIQCare.objhashBusinessRule[e.RowIndex] = dt;
                                    }
                                    else
                                    {
                                        GblIQCare.objhashBusinessRule.Add(e.RowIndex, dt);
                                    }
                                    GblIQCare.dtBusinessValues = dt;
                                }
                            }

                        }


                        Form theForm;
                        theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmBusinessRule, IQCare.FormBuilder"));
                        theForm.Left = 0;
                        theForm.Top = 0;
                        theForm.MdiParent = this.MdiParent;
                        GblIQCare.iFormMode = 1; //to open form in readonly mode   
                        theForm.Deactivate += new EventHandler(FrmHideChildOnLostFocus);
                        theForm.Show();

                    }
                } //business rule endif

                if (dgwCareEnded.Columns[e.ColumnIndex].Name == "Associated Fields")
                {

                    if (dgwCareEnded.Rows[e.RowIndex].Cells["Table Field Name"].Value.ToString() == "172")
                    {
                    
                        pnl_ReasonSelect.Visible = true;
                    }

                }
                if (dgwCareEnded.Columns[e.ColumnIndex].Name == "Display Type")
                {
                    dgwCareEnded.Rows[e.RowIndex].Cells["Display Type"].ReadOnly = true;
                }
                    
                if (dgwCareEnded.Columns[e.ColumnIndex].Name == "List")
                {

                    string display = string.Empty;
                    if (dgwCareEnded.Rows[e.RowIndex].Cells["Display Type"].Value != null)
                    {
                        display = dgwCareEnded.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
                    }

                    if (dgwCareEnded.Rows[e.RowIndex].Cells["Table Field Name"].Value != null)
                    {
                        GblIQCare.intFieldDetaisChange = Convert.ToInt32(dgwCareEnded.Rows[e.RowIndex].Cells[0].Value.ToString());
                    }
                    if (display.Trim() == "Select List" || display == "4" || display.Trim() == "Multi Select" || display == "9")
                    {
                        if (dgwCareEnded.Rows[e.RowIndex].Cells["Table Field Name"].Value != null)
                        {

                            GblIQCare.strSelectFieldName = dgwCareEnded.Rows[e.RowIndex].Cells["Table Field Name"].FormattedValue.ToString();
                            IFieldDetail objFieldDetail;
                            objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                            //pass fieldName if want to retrieve specific field from GetCustomFields

                            DataSet dsFieldDetails = new DataSet();
                            dsFieldDetails = objFieldDetail.GetCustomFields("", (cmbTechnicalArea.SelectedIndex == 0) ? 0 : Convert.ToInt32(cmbTechnicalArea.SelectedValue), GblIQCare.iManageCareEnded);

                            if (dgwCareEnded.Rows[e.RowIndex].Cells["Table Field Name"].Value != null)
                            {
                                GblIQCare.strSelectFieldName = dgwCareEnded.Rows[e.RowIndex].Cells["Table Field Name"].FormattedValue.ToString();

                                DataSet dsFillBusinessRule = dsFieldDetails.Copy();
                                if (dsFillBusinessRule.Tables[2].Rows.Count > 0)
                                {

                                    DataView dv;
                                    dv = dsFillBusinessRule.Tables[2].DefaultView;
                                    //if (dgwCareEnded.Rows[e.RowIndex].Cells[0].Value.ToString() != "" && dgwCareEnded.Rows[e.RowIndex].Cells[4].Value.ToString() != "")
                                    //{
                                    dv.RowFilter = "FieldId='" + dgwCareEnded.Rows[e.RowIndex].Cells[0].Value.ToString() + "' and Predefined='" + dgwCareEnded.Rows[e.RowIndex].Cells[4].Value.ToString() + "'";
                                    //}
                                    DataTable dt = new DataTable();
                                    if (dv.Count > 0)
                                        dt = dv.ToTable();

                                    if (dt != null)
                                    {
                                        if (dt.Rows.Count > 0)
                                        {
                                            if (!(GblIQCare.objhashSelectList.ContainsKey(e.RowIndex)))
                                            {
                                                GblIQCare.objhashSelectList.Add(e.RowIndex, dt.Rows[0]["FieldValue"].ToString());
                                            }
                                            else
                                            {
                                                GblIQCare.objhashSelectList[e.RowIndex] = dt.Rows[0]["FieldValue"].ToString();
                                            }

                                        }
                                    }

                                }
                            }
                            Form theForm;
                            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmSelectList, IQCare.FormBuilder"));
                            theForm.Left = 0;
                            theForm.Top = 0;
                            GblIQCare.iFormMode = 1; //to open form in readonly mode
                            GblIQCare.iConditionalbtn = 1;
                            theForm.MdiParent = this.MdiParent;
                            theForm.Deactivate += new EventHandler(FrmHideChildOnLostFocus);
                            theForm.Show();

                        }
                    }//select list check

                }
            }
        }
        private void FrmHideChildOnLostFocus(object sender, EventArgs e)
        {
            Form theSenderForm = sender as Form;
            theSenderForm.Close();
            GblIQCare.iFormMode = 0;
        }
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (lstExitReason.Items.Count > 0)
            {
                DataTable theFromDT = (DataTable)lstExitReason.DataSource;
                DataView theDV = new DataView(theFromDT);
                //if (lstExitReason.DataSource.ToString() != null)
                //{

                theDV.RowFilter = "Id = " + lstExitReason.SelectedValue.ToString();

                if (theDV.Count > 0)
                {
                    DataTable theToDT = (DataTable)lstSelExitReason.DataSource;
                    DataRow theDR = theToDT.NewRow();
                    theDR[0] = theDV[0][0];
                    theDR[1] = theDV[0][1];
                    theToDT.Rows.Add(theDR);
                    theDR = null;
                    theDV.Delete(0);



                }
                theDV = null;
                //}
                //else{
                //    IQCareWindowMsgBox.ShowWindowConfirm("AddReason", this);
                //    return;

                //}
            }

        }

        private void btnUnSelect_Click(object sender, EventArgs e)
        {
            if (lstSelExitReason.Items.Count > 0)
            {
                DataTable theToDT = (DataTable)lstSelExitReason.DataSource;
                DataView theDV = new DataView(theToDT);

                theDV.RowFilter = "ExitReasonsId =" + lstSelExitReason.SelectedValue.ToString();
                if (theDV.Count > 0)
                {
                    DataTable theFromDT = (DataTable)lstExitReason.DataSource;
                    DataRow theDR = theFromDT.NewRow();
                    theDR[0] = theDV[0][0];
                    theDR[1] = theDV[0][1];
                    theFromDT.Rows.Add(theDR);


                    theDR = null;
                    theDV.Delete(0);
                }
                theDV = null;
            }
        }
            private void btnAddReason_Click(object sender, EventArgs e)
        {
            if (txtAddReason.Text.Trim() == "")
            {
                IQCareWindowMsgBox.ShowWindowConfirm("AddReason",this);
                return;
            }
            ICareEndedConfiguration theCareEndManager = (ICareEndedConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BCareEndedConfiguration,BusinessProcess.FormBuilder");
            DataTable theDT = theCareEndManager.SaveNewPatientExitReason(txtAddReason.Text, GblIQCare.AppUserId, 0);
            DataTable theExitReason = (DataTable)lstExitReason.DataSource;
            DataRow theDR = theExitReason.NewRow();
            theDR["Id"] = theDT.Rows[0][0];
            theDR["Name"] = txtAddReason.Text;
            theExitReason.Rows.Add(theDR);
            txtAddReason.Text = ""; 
        }

        private void btnConditionalField_Click(object sender, EventArgs e)
        {
            if (lstSelExitReason.Items.Count < 1)
            {
                IQCareWindowMsgBox.ShowWindowConfirm("BlankExitSelect", this);
                return;
            }
            pnlCondFields.Visible = true;
            pnl_ReasonSelect.Visible = false;
            pnl_maingrd.Visible = false;
            btnSave.Enabled = false;
            pnlCondFields.Left = 2;
            pnlCondFields.Top = 50;
            pnlCondFields.Width = 980;
            pnlCondFields.Height = 455;
            DataTable theDT = (DataTable)lstSelExitReason.DataSource;
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(cmbExitReason, theDT.Copy(), "Name", "ExitReasonsId");
            cmbExitReason.SelectedValue = 0;
                            
        }

        private void dgwExitReason_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Image imgQuery;

            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {

                if (dgwExitReason.Columns[e.ColumnIndex].Name == "List")
                {
                    dgwExitReason.Rows[e.RowIndex].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    string display = string.Empty;
                    if (dgwExitReason.Rows[e.RowIndex].Cells["Display Type"].Value != null)
                    {
                        display = dgwExitReason.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
                    }
                    if (display.Trim() == "Select List" || display == "4" || display.Trim() == "Multi Select" || display == "9")
                    {
                        imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\List.bmp");
                        e.Value = imgQuery;
                    }
                    else
                    {
                        imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\listdesible.bmp");
                        e.Value = imgQuery;
                    }
                }
               if (dgwExitReason.Columns[e.ColumnIndex].Name == "Business")
                  {
                        dgwExitReason.Rows[e.RowIndex].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\brule.bmp");
                        e.Value = imgQuery;
                 }
                }

          

        }

        private void dgwExitReason_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType().ToString() == "System.Windows.Forms.DataGridViewComboBoxEditingControl")
            {
                theGrdComboExitReason = (ComboBox)e.Control;
                theGrdComboExitReason.SelectedValueChanged += null;
                theGrdComboExitReason.SelectedValueChanged += new EventHandler(theComboExitReason_SelectedValueChanged);
            }
        }

        private void cmbExitReason_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbExitReason.SelectedValue.ToString() != "System.Data.DataRowView" )
                {
                    DataView theDV;
                    if (Convert.ToInt32(cmbExitReason.SelectedValue)> 0)
                    {
                        pnl_DeathReasonSelect.Visible = false;
                        #region "Copy Filtered Data in Original Table"
                        DataTable theDT = (DataTable)dgwExitReason.DataSource;
                        if (theDT.Rows.Count > 0)
                        {
                            theDV = new DataView(theExitReasonFields);
                            theDV.RowFilter = "ExitReasonId=" + theDT.Rows[0]["ExitReasonId"].ToString();
                            int maxCount = theDV.Count;
                            if (maxCount > 0)
                            {
                                for (int i = 0; i < maxCount; i++)
                                {
                                    theDV.Delete(0);
                                }
                            }
                            
                            ////////////////////////////////////////////////////////////////////////////////
                            
                            
                            ///////////////////////////////////////////////////////////////////////////


                            theExitReasonFields.AcceptChanges();
                            foreach (DataRow theNDR in theDT.Rows)
                            {
                                DataRow theNewRow = theExitReasonFields.NewRow();
                                theNewRow["FieldId"] = theNDR["FieldId"];
                                theNewRow["Table Field Name"] = theNDR["Table Field Name"];
                                theNewRow["Field Label"] = theNDR["Field Label"];
                                theNewRow["Display Type"] = theNDR["Display Type"];
                                theNewRow["ExitReasonId"] = theNDR["ExitReasonId"];
                                theNewRow["Predefine"] = theNDR["Predefine"];
                                theExitReasonFields.Rows.Add(theNewRow);
                            }
                        }
                    #endregion
                        dgwExitReason.AllowUserToAddRows = true;
                    }
                    else if (Convert.ToInt32(cmbExitReason.SelectedValue) == 0)
                    {

                        dgwExitReason.AllowUserToAddRows = false;
                    }
                    theDV = new DataView(theExitReasonFields);
                    theDV.RowFilter = "FieldId=174 and ExitReasonId=93";
                    if (theDV.Count < 1 && cmbExitReason.SelectedValue.ToString() == "93")
                    {
                        DataRow theDR = theExitReasonFields.NewRow();
                        theDR["FieldId"] = "174";
                        theDR["Field Label"] = "Death Date";
                        theDR["Display Type"] = "Date";
                        theDR["ExitReasonId"] = "93";
                        theDR["Predefine"] = "1";

                        theExitReasonFields.Rows.Add(theDR);

                        DataRow theDReason = theExitReasonFields.NewRow();
                        theDReason["FieldId"] = "1167";
                        theDReason["Field Label"] = "Death Reason";
                        theDReason["Display Type"] = "Select List";
                        theDReason["ExitReasonId"] = "93";
                        theDReason["Predefine"] = "1";

                        theExitReasonFields.Rows.Add(theDReason);


                    }
                    theDV = new DataView(theExitReasonFields);
                    theDV.RowFilter = "ExitReasonId =" + cmbExitReason.SelectedValue.ToString();
                    DataTable theNewDT = theDV.ToTable();
                    ///Contraints
                    DataColumn[] thePkey = new DataColumn[2];
                    thePkey.SetValue(theNewDT.Columns["FieldId"], 0);
                    thePkey.SetValue(theNewDT.Columns["ExitReasonId"], 1);
                    theNewDT.Constraints.Add("Pk_ExitReason", thePkey, true);
                    //dsCareEndedInfo.Tables[4].Constraints.Add("Req_FieldLable", dsCareEndedInfo.Tables[4].Columns["Field Label"], false);
                    theNewDT.Columns["Field Label"].AllowDBNull = false;

                    dgwExitReason.DataSource = theNewDT;
                }
            }
            catch (Exception err)
            {
                //BindGrid_ExitReason(dsCareEndedInfo);
                MsgBuilder theBuilder = new MsgBuilder();
                if (err.TargetSite.ToString() == "Void CheckConstraint(System.Data.DataRow, System.Data.DataRowAction)")
                {

                    theBuilder.DataElements["MessageText"] = "Duplicate Selection. Try Again.";
                }
                else
                {
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                }
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;

            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                #region "Copy Filtered Data in Original Table"
                DataTable theDT = (DataTable)dgwExitReason.DataSource;
                DataView theDV;
                if (Convert.ToInt32(cmbExitReason.SelectedValue) > 0)
                {
                    if (theDT.Rows.Count > 0)
                    {
                        theDV = new DataView(theExitReasonFields);
                        theDV.RowFilter = "ExitReasonId=" + theDT.Rows[0]["ExitReasonId"].ToString();
                        int maxCount = theDV.Count;
                        if (maxCount > 0)
                        {
                            for (int i = 0; i < maxCount; i++)
                            {
                                theDV.Delete(0);
                            }
                        }


                        theExitReasonFields.AcceptChanges();

                        foreach (DataRow theNDR in theDT.Rows)
                        {

                            DataRow theNewRow = theExitReasonFields.NewRow();
                            theNewRow["FieldId"] = theNDR["FieldId"];
                            theNewRow["Table Field Name"] = theNDR["Table Field Name"];
                            theNewRow["Field Label"] = theNDR["Field Label"];
                            
                            theNewRow["Display Type"] = theNDR["Display Type"];
                            theNewRow["ExitReasonId"] = theNDR["ExitReasonId"];
                            theNewRow["Predefine"] = theNDR["Predefine"];
                            theExitReasonFields.Rows.Add(theNewRow);
                        }
                    }
                #endregion
                }
                pnlCondFields.Visible = false;
                //pnl_ReasonSelect.Visible = true;
                pnl_maingrd.Visible = true;
                btnSave.Enabled = true;

                
        
            }
            catch (Exception err)
            {
                //BindGrid_ExitReason(dsCareEndedInfo);
                MsgBuilder theBuilder = new MsgBuilder();
                if (err.TargetSite.ToString() == "Void CheckConstraint(System.Data.DataRow, System.Data.DataRowAction)")
                {

                    theBuilder.DataElements["MessageText"] = "Duplicate Selection. Try Again.";
                }
                else
                {
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                }
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
               
            }
           
        }

        private void btnReasonClose_Click(object sender, EventArgs e)
        {
            pnl_maingrd.Visible = true;
            pnlCondFields.Visible = false;
            pnl_ReasonSelect.Visible = false;
        }

        private void dgwExitReason_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            GblIQCare.gblRowIndex = e.RowIndex;
            if (e.RowIndex > dgwExitReason.Rows.Count - 2)
                return;

            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                GblIQCare.strDisplayType = "";
                GblIQCare.strFieldName = "";
                GblIQCare.strSelectFieldName = "";
                GblIQCare.strSelectListValue = "";
                GblIQCare.strSelectList = "";
                if (dgwExitReason.Columns[e.ColumnIndex].Name == "Business")
                {
                    //string strDisplay = string.Empty;
                    if (dgwExitReason.Rows[e.RowIndex].Cells["Business"].Value != null && dgwExitReason.Rows[e.RowIndex].Cells["Business"].Value.ToString() != "0" && dgwExitReason.Rows[e.RowIndex].Cells["Business"].Value.ToString() != "System.Drawing.Bitmap")
                    {
                        GblIQCare.strDisplayType = dgwExitReason.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
                        GblIQCare.intFieldDetaisChange = Convert.ToInt32(dgwExitReason.Rows[e.RowIndex].Cells[0].Value);
                        GblIQCare.strFieldName = dgwExitReason.Rows[e.RowIndex].Cells["Table Field Name"].FormattedValue.ToString();


                        GblIQCare.dtBusinessValues.Clear();
                        GblIQCare.dtBusinessValues.Columns.Clear();
                        GblIQCare.dtBusinessValues.Rows.Clear();
                        IFieldDetail objFieldDetail;
                        objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                        //pass fieldName if want to retrieve specific field from GetCustomFields

                        DataSet dsFieldDetails = new DataSet();
                        dsFieldDetails = objFieldDetail.GetCustomFields("", (cmbTechnicalArea.SelectedIndex == 0) ? 0 : Convert.ToInt32(cmbTechnicalArea.SelectedValue), GblIQCare.iManageCareEnded);
                        DataSet dsFillBusinessRule = dsFieldDetails.Copy();
                        if (dsFillBusinessRule.Tables[4].Rows.Count > 0)
                        {
                            DataView dv;
                            dv = dsFillBusinessRule.Tables[4].DefaultView;
                            //dv.RowFilter = "FieldID=" + dgwExitReason.Rows[e.RowIndex].Cells["FieldId"].Value + " and predefined=" + dgwExitReason.Rows[e.RowIndex].Cells["Predefined"].Value.ToString();
                            dv.RowFilter = "FieldId='" + dgwExitReason.Rows[e.RowIndex].Cells[0].Value.ToString() + "' and Predefined='" + dgwExitReason.Rows[e.RowIndex].Cells[6].Value.ToString() + "'";
                            DataView DvFilter = new DataView();
                            DataTable dt = new DataTable();
                            dt = dv.ToTable();
                            if (dt != null)
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    if (GblIQCare.objhashBusinessRule.ContainsKey(e.RowIndex))
                                    {
                                        GblIQCare.objhashBusinessRule[e.RowIndex] = dt;
                                    }
                                    else
                                    {
                                        GblIQCare.objhashBusinessRule.Add(e.RowIndex, dt);
                                    }
                                    GblIQCare.dtBusinessValues = dt;
                                }
                            }

                        }


                        Form theForm;
                        theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmBusinessRule, IQCare.FormBuilder"));
                        theForm.Left = 0;
                        theForm.Top = 0;
                      
                        theForm.MdiParent = this.MdiParent;
                        GblIQCare.iFormMode = 1; //to open form in readonly mode
                       
                        theForm.Deactivate += new EventHandler(FrmHideChildOnLostFocus);
                      
                        theForm.Show();

                    }
                } //business rule endif
                if (dgwExitReason.Columns[e.ColumnIndex].Name == "Display Type")
                {
                    dgwExitReason.Rows[e.RowIndex].Cells["Display Type"].ReadOnly = true;
                }
              
                if (dgwExitReason.Columns[e.ColumnIndex].Name == "List")
                {

                     string display = string.Empty;
                     string strFieldid = string.Empty;

                     if (dgwExitReason.Rows[e.RowIndex].Cells[0].Value.ToString() == "1167")
                     {
                         pnl_DeathReasonSelect.Visible = true;
                         GblIQCare.strMainGrdFldName = "";
                         GblIQCare.strMainGrdFldName = dgwExitReason.Rows[e.RowIndex].Cells["Table Field Name"].FormattedValue.ToString();
                         GblIQCare.iFieldId = Convert.ToInt32(dgwExitReason.Rows[e.RowIndex].Cells[0].Value.ToString());
                         GblIQCare.ModuleId = Convert.ToInt32(cmbTechnicalArea.SelectedValue.ToString());
                         GblIQCare.strCareEndFeatureName = "CareEnd_" + cmbTechnicalArea.Text.ToString().Trim();
                         if (GblIQCare.iFieldId == 1167)
                         {
                             GblIQCare.iConditionalbtn = 1;
                         }
                         else
                         {
                             GblIQCare.iConditionalbtn = 0;

                         }
                         //GblIQCare.strFieldIdcareend = "1";

                         DisplayDeathreasonpnl();
                     }
                     else
                     {
                         pnl_DeathReasonSelect.Visible = false;
                         if (dgwExitReason.Rows[e.RowIndex].Cells["Display Type"].Value != null)
                         {
                             display = dgwExitReason.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
                         }

                         if (dgwExitReason.Rows[e.RowIndex].Cells["Table Field Name"].Value != null)
                         {
                             GblIQCare.intFieldDetaisChange = Convert.ToInt32(dgwExitReason.Rows[e.RowIndex].Cells[0].Value.ToString());
                         }
                         if (display.Trim() == "Select List" || display == "4" || display.Trim() == "Multi Select" || display == "9")
                         {
                             if (dgwExitReason.Rows[e.RowIndex].Cells["Table Field Name"].Value != null)
                             {

                                 GblIQCare.strSelectFieldName = dgwExitReason.Rows[e.RowIndex].Cells["Table Field Name"].FormattedValue.ToString();
                                 IFieldDetail objFieldDetail;
                                 objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                                 //pass fieldName if want to retrieve specific field from GetCustomFields

                                 DataSet dsFieldDetails = new DataSet();
                                 dsFieldDetails = objFieldDetail.GetCustomFields("", (cmbTechnicalArea.SelectedIndex == 0) ? 0 : Convert.ToInt32(cmbTechnicalArea.SelectedValue), GblIQCare.iManageCareEnded);

                                 if (dgwExitReason.Rows[e.RowIndex].Cells["Table Field Name"].Value != null)
                                 {
                                     GblIQCare.strSelectFieldName = dgwExitReason.Rows[e.RowIndex].Cells["Table Field Name"].FormattedValue.ToString();

                                     DataSet dsFillBusinessRule = dsFieldDetails.Copy();
                                     if (dsFillBusinessRule.Tables[2].Rows.Count > 0)
                                     {

                                         DataView dv;
                                         dv = dsFillBusinessRule.Tables[2].DefaultView;
                                         //if (dgwExitReason.Rows[e.RowIndex].Cells[0].Value.ToString() != "" && dgwExitReason.Rows[e.RowIndex].Cells[4].Value.ToString() != "")
                                         //{
                                         //dv.RowFilter="FieldID=" + dgwExitReason.Rows[e.RowIndex].Cells[0].Value + " and Predefined=" + dgwExitReason.Rows[e.RowIndex].Cells[4].Value;
                                         dv.RowFilter = "Fieldid='" + dgwExitReason.Rows[e.RowIndex].Cells[0].Value.ToString() + "' and Predefined='" + dgwExitReason.Rows[e.RowIndex].Cells[6].Value.ToString() + "'";
                                         //}
                                         DataTable dt = new DataTable();
                                         if (dv.Count > 0)
                                             dt = dv.ToTable();

                                         if (dt != null)
                                         {
                                             if (dt.Rows.Count > 0)
                                             {
                                                 if (!(GblIQCare.objhashSelectList.ContainsKey(e.RowIndex)))
                                                 {
                                                     GblIQCare.objhashSelectList.Add(e.RowIndex, dt.Rows[0]["FieldValue"].ToString());
                                                 }
                                                 else
                                                 {
                                                     GblIQCare.objhashSelectList[e.RowIndex] = dt.Rows[0]["FieldValue"].ToString();
                                                 }


                                             }
                                         }

                                     }
                                 }
                                 Form theForm;
                                 theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmSelectList, IQCare.FormBuilder"));
                                 theForm.Left = 0;
                                 theForm.Top = 0;
                                 GblIQCare.iFormMode = 1; //to open form in readonly mode
                                 GblIQCare.iConditionalbtn = 1;
                                 theForm.MdiParent = this.MdiParent;
                                 theForm.Deactivate += new EventHandler(FrmHideChildOnLostFocus);
                                 theForm.Show();

                             }
                         }//select list check
                     }

                }
            }
         }

        private void dgwCareEnded_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Message.Contains("constrained to be unique")== true)
            {
                IQCareWindowMsgBox.ShowWindow("CheckDuplicateValue", this);
                return;
            }
            else
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = e.Exception.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
               
                DataTable theExitReason = (DataTable)lstSelExitReason.DataSource;
                DataTable theExitReasonFormFields = (DataTable)dgwCareEnded.DataSource;  ///MainGrid//
                theExitReason.AcceptChanges();
                theExitReasonFormFields.AcceptChanges();
                theExitReasonFields.AcceptChanges(); ////Exit Reason Grid///
                //Death Reason
                DataTable theDeathReason = new DataTable();
                if (lstSelDeathReason.Items.Count > 0)
                {
                    theDeathReason = (DataTable)lstSelDeathReason.DataSource;
                    theDeathReason.AcceptChanges();
                }
                //Death Reason End
                foreach (DataRow theDR in theExitReasonFormFields.Rows)
                {
                    foreach (DataRow theExitDR in theExitReasonFields.Rows)
                    {
                        if (theExitDR["FieldId"].ToString() == theDR["FieldId"].ToString())
                        {
                            DataView theDVView = new DataView(dsCareEndedInfo.Tables[1]);
                            theDVView.RowFilter = "Id = " + theExitDR["FieldId"].ToString();
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["FieldName"] = theDVView[0]["PdfName"].ToString();
                            IQCareWindowMsgBox.ShowWindow("DuplicateFieldSelection",theBuilder,this);
                            return;
                        }
                    }
                }

                if (Validation() == false)
                {
                    return;
                }
                if ((theExitReason.Rows.Count < 1) &&(theExitReasonFormFields.Rows.Count < 1)&& (theExitReasonFields.Rows.Count < 1))
                {
                    return;
                }
                DataSet dsCareEndDetail = new DataSet();
                dsCareEndDetail.Tables.Add(theExitReasonFormFields.Copy());
                dsCareEndDetail.Tables.Add(theExitReason.Copy());
                dsCareEndDetail.Tables.Add(theExitReasonFields.Copy());
                
                
                ICareEndedConfiguration objCareEnd = (ICareEndedConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BCareEndedConfiguration,BusinessProcess.FormBuilder");
                int iSave = objCareEnd.SaveCareEndDetails(dsCareEndDetail, GblIQCare.iCareEndedId,"CareEnd_"+cmbTechnicalArea.Text.ToString().Trim(), txtSection.Text,
                   Convert.ToInt32(cmbTechnicalArea.SelectedValue), GblIQCare.AppUserId, GblIQCare.AppCountryId, ID, GblIQCare.dtConditionalFields, theDeathReason);
                GblIQCare.dtConditionalFields.Clear();
                btnCancel_Click(sender, e);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }


        }

        private Boolean Validation()
        {
            if (cmbTechnicalArea.SelectedIndex == 0)
            {
                IQCareWindowMsgBox.ShowWindow("SelectTechnicalArea", this);
                return false;

            }
            return true;
        
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            pnl_ReasonSelect.Visible = false;
            btnSave.Enabled = true;
            
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgwCareEnded.Rows.Count <= 1)
                {
                    return;
                }
                if (dgwCareEnded.CurrentRow.Index > 0)
                {

                    Int32 iPos = dgwCareEnded.CurrentRow.Index;
                    DataTable theDT = (DataTable)dgwCareEnded.DataSource;
                    DataRow theDR = theDT.NewRow();
                    theDR.ItemArray = theDT.Rows[dgwCareEnded.CurrentRow.Index].ItemArray;
                    theDT.Rows[dgwCareEnded.CurrentRow.Index].Delete();
                    theDT.Rows.InsertAt(theDR, iPos - 1);
                    theDT.AcceptChanges();
                    dgwCareEnded.Refresh();
                    
                }
               
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }  
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgwCareEnded.Rows.Count <= 1)
                {
                    return;
                }
                else if (dgwCareEnded.CurrentRow.Index >= 0)
                {
                    Int32 iPos = dgwCareEnded.CurrentRow.Index;
                    DataTable theDT = (DataTable)dgwCareEnded.DataSource;
                    DataRow theDR = theDT.NewRow();
                    theDR.ItemArray = theDT.Rows[dgwCareEnded.CurrentRow.Index].ItemArray;
                    theDT.Rows[dgwCareEnded.CurrentRow.Index].Delete();
                    theDT.Rows.InsertAt(theDR, iPos + 2);
                    theDT.AcceptChanges();
                    dgwCareEnded.Refresh();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();

                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }  
        }

        private void dgwExitReason_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Message.Contains("constrained to be unique")== true)
            {
                IQCareWindowMsgBox.ShowWindow(" CheckDuplicateValue", this);
                e.Cancel= true; 
                return;
               
            }
            else
            {
                e.Cancel = true;
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = e.Exception.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (pnlCondFields.Visible == true)
            {
                if (dgwExitReason.Rows.Count > 1)
                {
                    if (Convert.ToInt32(dgwExitReason.Rows[dgwExitReason.CurrentRow.Index].Cells["Table Field Name"].Value) == 174)
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "Death Date";
                        IQCareWindowMsgBox.ShowWindow("UndeletePredefined", theMsg, this);
                        return;
                    }
                    //if (Convert.ToInt32(dgwExitReason.Rows[dgwExitReason.CurrentRow.Index].Cells["Table Field Name"].Value) == 1167)
                    //{
                    //    MsgBuilder theMsg = new MsgBuilder();
                    //    theMsg.DataElements["Control"] = "Death Reason";
                    //    IQCareWindowMsgBox.ShowWindow("UndeletePredefined", theMsg, this);
                    //    return;
                    //}
                    if (dgwExitReason.Rows[dgwExitReason.CurrentRow.Index].Cells[0].Value != DBNull.Value)
                    {
                        //DataTable theDT = (DataTable)dgwExitReason.DataSource;
                        //DataView theDV = new DataView(theDT);
                        //theDV.RowFilter = "FieldId=" + dgwExitReason.Rows[dgwExitReason.CurrentRow.Index].Cells[0].Value.ToString();
                        //theDV.Delete(0);

                        ///////////////////
                        DataTable theDT = (DataTable)dgwExitReason.DataSource;
                        DataView theDV = new DataView(theDT);
                        string theFieldId = dgwExitReason.Rows[dgwExitReason.CurrentRow.Index].Cells[0].Value.ToString();
                        theDV.RowFilter = "FieldId=" + theFieldId;
                        theDV.Delete(0);
                        theDV = new DataView(theExitReasonFields);
                        theDV.RowFilter = "FieldId=" + theFieldId;
                        if (theDV.Count > 0)
                        {
                            theDV.Delete(0);
                        }
                        //theDV.Delete(0);
                        //////////////////////////



                    }
                }

            }
            else
            {
                if (Convert.ToInt32(dgwCareEnded.Rows[dgwCareEnded.CurrentRow.Index].Cells["Table Field Name"].Value) == 172)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Patient Exit Reason";
                    IQCareWindowMsgBox.ShowWindow("UndeletePredefined", theMsg, this);
                    return;
                }
                if (Convert.ToInt32(dgwCareEnded.Rows[dgwCareEnded.CurrentRow.Index].Cells["Table Field Name"].Value) == 176)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Care Ended Date";
                    IQCareWindowMsgBox.ShowWindow("UndeletePredefined", theMsg, this);
                    return;
                }
                if (Convert.ToInt32(dgwCareEnded.Rows[dgwCareEnded.CurrentRow.Index].Cells["Table Field Name"].Value) == 177)
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Signature";
                    IQCareWindowMsgBox.ShowWindow("UndeletePredefined", theMsg, this);
                    return;
                }

                if (dgwCareEnded.Rows[dgwCareEnded.CurrentRow.Index].Cells[0].Value != DBNull.Value)
                {
                    DataTable theDT = (DataTable)dgwCareEnded.DataSource;
                    DataView theDV = new DataView(theDT);
                    theDV.RowFilter = "FieldId=" + dgwCareEnded.Rows[dgwCareEnded.CurrentRow.Index].Cells[0].Value.ToString();
                    theDV.Delete(0);

                }
            }
        }

        private void theUPExitReason_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgwExitReason.Rows.Count <= 1)
                {
                    return;
                }
                if (dgwExitReason.CurrentRow.Index > 0)
                {
                    Int32 iPos = dgwExitReason.CurrentRow.Index;
                    DataTable theDT = (DataTable)dgwExitReason.DataSource;
                    DataRow theDR = theDT.NewRow();
                    theDR.ItemArray = theDT.Rows[dgwExitReason.CurrentRow.Index].ItemArray;
                    theDT.Rows[dgwExitReason.CurrentRow.Index].Delete();
                    theDT.Rows.InsertAt(theDR, iPos - 1);
                    theDT.AcceptChanges();
                    dgwExitReason.Refresh();
                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }  
        }

        private void theDownExitreason_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgwExitReason.Rows.Count <= 1)
                {
                    return;
                }
                else if (dgwExitReason.CurrentRow.Index >= 0)
                {
                    Int32 iPos = dgwExitReason.CurrentRow.Index;
                    DataTable theDT = (DataTable)dgwExitReason.DataSource;
                    DataRow theDR = theDT.NewRow();
                    theDR.ItemArray = theDT.Rows[dgwExitReason.CurrentRow.Index].ItemArray;
                    theDT.Rows[dgwExitReason.CurrentRow.Index].Delete();
                    theDT.Rows.InsertAt(theDR, iPos + 2);
                    theDT.AcceptChanges();
                    dgwExitReason.Refresh();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
                return;
            }  
        }

        private void cmbExitReason_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void dgwExitReason_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        #region DeathReasonConditionalField

        private void DisplayDeathreasonpnl()
        {
            ICareEndedConfiguration objCareEndDeath = (ICareEndedConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BCareEndedConfiguration,BusinessProcess.FormBuilder");
            DataSet dsCareEndedDeath = objCareEndDeath.GetCareEndedDeathReason(Convert.ToInt32(cmbTechnicalArea.SelectedValue.ToString()));
            BindFunctions theBindDeath = new BindFunctions();
            lstDeathReason.DataSource = null;
            lstSelDeathReason.DataSource = null;


            #region "FilterDeathReason"
            DataTable theDeathExitReason = dsCareEndedDeath.Tables[0].Copy();
            foreach (DataRow theDR in dsCareEndedDeath.Tables[1].Rows)
            {
                DataView theDV1 = new DataView(theDeathExitReason);
                theDV1.RowFilter = "Id=" + theDR["DeathReasonID"].ToString();
                theDV1.Delete(0);
            }
            #endregion

            //theBind.Win_BindListBox(lstExitReason, theExitReason, "Name", "Id");
            //theBind.Win_BindListBox(lstSelExitReason, dsCareEndedInfo.Tables[3], "Name", "ExitReasonsId");

            theBindDeath.Win_BindListBox(lstDeathReason, theDeathExitReason, "Name", "Id");
            theBindDeath.Win_BindListBox(lstSelDeathReason, dsCareEndedDeath.Tables[1], "Name", "DeathReasonID");


        }

        private void btnDeathSelect_Click(object sender, EventArgs e)
        {
            if (lstDeathReason.Items.Count > 0)
            {
                DataTable theFromDT = (DataTable)lstDeathReason.DataSource;
                DataView theDV = new DataView(theFromDT);
                //if (lstExitReason.DataSource.ToString() != null)
                //{

                theDV.RowFilter = "ID = " + lstDeathReason.SelectedValue.ToString();

                if (theDV.Count > 0)
                {
                    DataTable theToDT = (DataTable)lstSelDeathReason.DataSource;
                    DataRow theDR = theToDT.NewRow();
                    theDR[0] = theDV[0][0];
                    theDR[1] = theDV[0][1];
                    theToDT.Rows.Add(theDR);
                    theDR = null;
                    theDV.Delete(0);
                }
                theDV = null;
                //}
                //else{
                //    IQCareWindowMsgBox.ShowWindowConfirm("AddReason", this);
                //    return;

                //}
            }

        }

        private void btnDeathUnSelect_Click(object sender, EventArgs e)
        {
            if (lstSelDeathReason.Items.Count > 0)
            {
                DataTable theToDT = (DataTable)lstSelDeathReason.DataSource;
                DataView theDV = new DataView(theToDT);

                theDV.RowFilter = "DeathReasonID =" + lstSelDeathReason.SelectedValue.ToString();
                if (theDV.Count > 0)
                {
                    DataTable theFromDT = (DataTable)lstDeathReason.DataSource;
                    DataRow theDR = theFromDT.NewRow();
                    theDR[0] = theDV[0][0];
                    theDR[1] = theDV[0][1];
                    theFromDT.Rows.Add(theDR);


                    theDR = null;
                    theDV.Delete(0);
                }
                theDV = null;
            }


        }

        private void btnAddDeathReason_Click(object sender, EventArgs e)
        {
            {
                if (txtAddDeathReason.Text.Trim() == "")
                {
                    IQCareWindowMsgBox.ShowWindowConfirm("AddReason", this);
                    return;
                }
                ICareEndedConfiguration theCareEndManager = (ICareEndedConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BCareEndedConfiguration,BusinessProcess.FormBuilder");
                DataTable theDT = theCareEndManager.SaveNewPatientDeathReason(txtAddDeathReason.Text, GblIQCare.AppUserId, 0);
                DataTable theDeathReason = (DataTable)lstDeathReason.DataSource;
                DataRow theDR = theDeathReason.NewRow();
                theDR["Id"] = theDT.Rows[0][0];
                theDR["Name"] = txtAddDeathReason.Text;
                theDeathReason.Rows.Add(theDR);
                txtAddDeathReason.Text = "";
            }

        }

        private void btnDeathSubmit_Click(object sender, EventArgs e)
        {
            pnl_DeathReasonSelect.Visible = false;
            btnSave.Enabled = true;

        }

        private void btnDeathReasonClose_Click(object sender, EventArgs e)
        {

            pnl_maingrd.Visible = true;
            pnlCondFields.Visible = false;
            pnl_DeathReasonSelect.Visible = false;

        }

        private void btnDeathConditionalField_Click(object sender, EventArgs e)
        {
            if (lstSelDeathReason.Items.Count < 1)
            {
                IQCareWindowMsgBox.ShowWindowConfirm("BlankDeathSelect", this);
                return;
            }
            pnlCondFields.Visible = true;
            pnl_DeathReasonSelect.Visible = false;
            pnl_maingrd.Visible = false;
            btnSave.Enabled = false;
            pnlCondFields.Left = 2;
            pnlCondFields.Top = 50;
            pnlCondFields.Width = 980;
            pnlCondFields.Height = 455;
            DataTable theDT = (DataTable)lstSelDeathReason.DataSource;
            BindFunctions theBind = new BindFunctions();
            //theBind.Win_BindCombo(cmbExitReason, theDT.Copy(), "Name", "Id");
            //cmbExitReason.SelectedValue = 0;
            GblIQCare.dtCareEndConditional = theDT.Copy();
            Form theForm;
            GblIQCare.strCareEndcon = "0";
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmCareEndConditionalDisplay, IQCare.FormBuilder"));
            //theForm.MdiParent = this.MdiParent.MdiParent;
            theForm.Left = 50;
            theForm.Top = 100;
            theForm.Show();
            //this.Close();
           

        }

        #endregion

      

    }
}
