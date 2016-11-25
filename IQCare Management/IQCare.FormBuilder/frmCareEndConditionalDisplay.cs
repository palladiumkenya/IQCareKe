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

namespace IQCare.FormBuilder
{
    public partial class frmCareEndConditionalDisplay : Form
    {
        ComboBox theGrdCombo;
        DataTable theGrdSource = new DataTable();
        DataSet dsFieldDetails = new DataSet();
        DataSet objDsFieldDetails = new DataSet();
        DataView dv = new DataView();
        public int gblFieldID = 0;
        public string gblFieldName = string.Empty;
        public string gblPredefine = string.Empty;
        int flag = 0;
        bool IsHandleAdded;    
        int codeid = 0;
        string strdeathconditional = "";
       // int intdeath = 0;

        public frmCareEndConditionalDisplay()
        {
            InitializeComponent();
        }

        private void Init_Form()
        {
            dgwConditionalField.AllowUserToAddRows = false;
            pnlCondFields.Left = 2;
            pnlCondFields.Top = 50;
            pnlCondFields.Width = 880;
            pnlCondFields.Height = 455;
            lbloptionalfield.Text = GblIQCare.strMainGrdFldName;
            strdeathconditional = "1";

            ICareEndedConfiguration objConditional = (ICareEndedConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BCareEndedConfiguration,BusinessProcess.FormBuilder");

            dsFieldDetails = objConditional.GetCareendConditionalFieldsDetails(Convert.ToInt32(GblIQCare.iFieldId.ToString().Substring(1, GblIQCare.iFieldId.ToString().Length - 1)), GblIQCare.iCareEndedId);
            //dsFieldDetails = objConditional.GetCareendConditionalFieldsDetails(GblIQCare.iFieldId, GblIQCare.iCareEndedId);
            theGrdSource = dsFieldDetails.Tables[1];
            DataSet dsConditional = new DataSet();
            dsConditional = dsFieldDetails;

            BindFunctions theBind = new BindFunctions();

            GblIQCare.iFieldId = Convert.ToInt32((GblIQCare.iFieldId.ToString().Substring(1, GblIQCare.iFieldId.ToString().Length - 1)));


            if (GblIQCare.dtCareEndConditional.Rows.Count != 0)
            {


                theBind.Win_BindCombo(cmbConditionalField, GblIQCare.dtCareEndConditional, "name", "DeathReasonID");
            }

            #region "Alter ConditionTable"
            theGrdSource.Columns.Add("SectionName", typeof(System.String));
            theGrdSource.Columns.Add("FeatureName", typeof(System.String));
            theGrdSource.Columns.Add("ModuleID", typeof(System.Int32));
            theGrdSource.AcceptChanges();
            //DataView theDV = new DataView(dsConditional.Tables[0]);
            DataView theDV = new DataView(GblIQCare.dtCareEndConditional);
            if (theGrdSource.Rows.Count > 0)
            {
                foreach (DataRow theDR in theGrdSource.Rows)
                {
                    theDV.RowFilter = "DeathReasonID =" + theDR["SectionId"].ToString();
                    if (theDV.Count > 0)
                    theDR["SectionName"] = theDV[0]["Name"].ToString();
                    theDR["FeatureName"] = GblIQCare.strCareEndFeatureName;
                    theDR["FeatureName"] = GblIQCare.strCareEndFeatureName;
                    theDR["ModuleID"] = GblIQCare.ModuleId;
                }
            }
            #endregion
            if (cmbConditionalField.DataSource != null)
            {
                //cmbConditionalField.SelectedIndex = 0;
                cmbConditionalField.SelectedValue = 0;
                EventArgs s = new EventArgs();
                cmbConditionalField_SelectedValueChanged(this, s);
            }
            
            
           
        }
        private void Init_ManageCareEnded()
        {
            
            dgwConditionalField.AllowUserToAddRows = false;
            pnlCondFields.Left = 2;
            pnlCondFields.Top = 50;
            pnlCondFields.Width = 880;
            pnlCondFields.Height = 455;
            lbloptionalfield.Text = GblIQCare.strMainGrdFldName;
            strdeathconditional = "2";

            string strlist = GblIQCare.objhashSelectList[GblIQCare.gblRowIndex].ToString().Trim();
            //string strlist = GblIQCare.strSelectListstr.ToString();

            IFieldDetail objConditional = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
            if(GblIQCare.strPredefinevalue.ToString()=="P")
                dsFieldDetails = objConditional.GetConditionalFieldsDetails(Convert.ToInt32(GblIQCare.iFieldId.ToString()), GblIQCare.iManageCareEnded);
            else
                dsFieldDetails = objConditional.GetConditionalFieldsDetails(GblIQCare.iFieldId, GblIQCare.iManageCareEnded);
            theGrdSource = dsFieldDetails.Tables[1];
            DataSet dsConditional = new DataSet();

            #region "Alter ConditionTable"
            theGrdSource.Columns.Add("ModuleID", typeof(System.Int32));
            theGrdSource.AcceptChanges();
            #endregion

            if (GblIQCare.strPredefinevalue.ToString() != "P")
            {
                flag = 0;
                codeid = FindCodeID(GblIQCare.DsFieldDetailsCon.Tables[2], 0);
                dsConditional = objConditional.GetConditionalFieldslist(codeid, GblIQCare.iFieldId, 0);
            }
            else
            {
                flag = 1;
                codeid = FindCodeID(GblIQCare.DsFieldDetailsCon.Tables[2], 1);
                dsConditional = objConditional.GetConditionalFieldslist(codeid, GblIQCare.iFieldId, 1);

            }
            BindFunctions theBind = new BindFunctions();


            if (dsConditional.Tables[0].Rows.Count == 0)
            {
                string[] List = strlist.Split(';');
                DataRow DRSelect1;
                for (int i = 0; i <= List.Length - 1; i++)
                {
                    //cmbConditionalField.Items.Add("Select");
                    //cmbConditionalField.Items.Add(List[i]);

                    DRSelect1 = dsConditional.Tables[0].NewRow();
                    DRSelect1["ID"] = 999;
                    DRSelect1["Name"] = List[i].ToString();
                    dsConditional.Tables[0].Rows.Add(DRSelect1);
                }

                theBind.Win_BindCombo(cmbConditionalField, dsConditional.Tables[0], "Name", "ID");
                GblIQCare.strSelectListstr = "";
            }
            else
            {
                DataRow DRSelect;
                DataView dv = new DataView(dsConditional.Tables[0]);
                string[] List = strlist.Split(';');
                for (int i = 0; i <= List.Length - 1; i++)
                {
                    string strlistdata = List[i].ToString().Trim();
                    strlistdata.Trim();


                    dv.RowFilter = "Name='" + strlistdata + "'";

                    if (dv.Count == 0)
                    {
                        DRSelect = dsConditional.Tables[0].NewRow();
                        DRSelect["ID"] = 999;
                        DRSelect["Name"] = List[i].ToString();

                        dsConditional.Tables[0].Rows.Add(DRSelect);

                    }

                }

                theBind.Win_BindCombo(cmbConditionalField, dsConditional.Tables[0], "Name", "ID");
                GblIQCare.strSelectListstr = "";
            }

            //#region "Alter ConditionTable"
            //theGrdSource.Columns.Add("SectionName", typeof(System.String));
            //theGrdSource.AcceptChanges();
            //DataView theDV= new DataView(dsConditional.Tables[0]);
            //if (theGrdSource.Rows.Count > 0)
            //{
            //    foreach (DataRow theDR in theGrdSource.Rows)
            //    {
            //        theDV.RowFilter = "ID ="+theDR["SectionId"].ToString();
            //        if(theDV.Count>0)
            //            theDR["SectionName"] = theDV[0]["Name"].ToString();
            //    }
            //}
            //#endregion
            dsFieldDetails.Tables[1].Columns["FieldLabel"].AllowDBNull = false;
            dgwConditionalField.DataSource = dsFieldDetails.Tables[1];
            EventArgs s = new EventArgs();
            cmbConditionalField.SelectedValue = 0;
            cmbConditionalField_SelectedValueChanged(this, s);
            BindGrid_Conditionalfields(dsFieldDetails);
            cmbConditionalField.Focus();

          
        }

        private void frmConditionalDisplay_Load(object sender, EventArgs e)
        {
            try
            {
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
                if (GblIQCare.strCareEndcon == "1")
                {
                    Init_ManageCareEnded();

                }
                else
                {
                    Init_Form();
                
                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void cmbConditionalField_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbConditionalField.SelectedValue.ToString() == "System.Data.DataRowView")
            {
                return;
            }
            if (cmbConditionalField.SelectedValue.ToString() == "0")
                dgwConditionalField.AllowUserToAddRows = false;
            else
                dgwConditionalField.AllowUserToAddRows = true;
            if ((cmbConditionalField.SelectedValue != null) && (cmbConditionalField.SelectedValue.ToString() != "System.Data.DataRowView"))
            {
                DataView theDV;
                if (Convert.ToInt32(cmbConditionalField.SelectedValue) > 0)
                {
                    DataTable theDT = (DataTable)dgwConditionalField.DataSource;
                    if (theDT != null)
                    {

                        if (theDT.Rows.Count > 0)
                        {
                            foreach (DataRow theDR in theDT.Rows)
                            {
                                theDV = new DataView(theGrdSource);
                                theDV.RowFilter = "ConFieldId=" + theDR["ConfieldId"].ToString() + " and SectionId=" + theDR["SectionId"].ToString() +
                                    " and FieldId=" + theDR["FieldId"].ToString();
                                if (theDV.Count < 1)
                                {
                                    DataRow theDRow = theGrdSource.NewRow();
                                    //if (strdeathconditional == "1")
                                    //{
                                    //    //theDRCon["FieldId"].ToString().Substring(1, theDRCon["FieldId"].ToString().Length - 1)

                                    //    theDRow["ConFieldId"] = Convert.ToInt32(GblIQCare.iFieldId.ToString().Substring(1, GblIQCare.iFieldId.ToString().Length - 1));
                                    //}
                                    //else
                                    //{
                                    //    theDRow["ConFieldId"] = GblIQCare.iFieldId;
                                    //}
                                    theDRow["ConFieldId"] = GblIQCare.iFieldId;
                                    theDRow["SectionId"] = theDR["SectionId"];
                                    theDRow["FieldId"] = theDR["FieldId"];
                                    theDRow["FieldLabel"] = theDR["FieldLabel"];
                                    theDRow["Predefined"] = theDR["Predefined"];
                                    theDRow["Seq"] = theDR["Seq"];
                                    theDRow["FieldName"] = theDR["FieldName"];
                                    theDRow["DataType"] = theDR["DataType"];
                                    theDRow["SectionName"] = theDR["SectionName"];
                                    theDRow["FeatureID"] = GblIQCare.iCareEndedId;
                                    theDRow["FeatureName"] = GblIQCare.strCareEndFeatureName;
                                    theDRow["ModuleID"] = GblIQCare.ModuleId;
                                    theDRow["Conpredefine"] = flag;

                                    theGrdSource.Rows.Add(theDRow);
                                }
                            }
                        }
                    }
                }
                theDV = null;
                if (theGrdSource.Rows.Count > 0)
                {
                    theDV = new DataView(theGrdSource);
                    theDV.RowFilter = "SectionId=" + cmbConditionalField.SelectedValue.ToString();

                    DataTable theFldDT = theDV.ToTable();
                    

                    //dgwConditionalField.DataSource = theDV.ToTable();
                    
                        dgwConditionalField.DataSource = theFldDT;

                        BindGrid_Conditionalfields(dsFieldDetails);
                   
                }
                else
                {
                    dgwConditionalField.DataSource = theGrdSource;
                    BindGrid_Conditionalfields(dsFieldDetails);
                }
            }
        }

        private void BindGrid_Conditionalfields(DataSet theDS)
        {
            string strGetPath = GblIQCare.GetPath();

            dgwConditionalField.Columns.Clear();
            dgwConditionalField.AutoGenerateColumns = false;
            DataView thecomDV;
            thecomDV = new DataView(theDS.Tables[0]);
            thecomDV.RowFilter = "Id<>" + GblIQCare.iFieldId.ToString();
            DataTable theComDT = thecomDV.ToTable();

         
            DataGridViewComboBoxColumn theColumnExitReasonFieldName = new DataGridViewComboBoxColumn();
            theColumnExitReasonFieldName.HeaderText = "Table Field Name";
            theColumnExitReasonFieldName.Name = "Table Field Name";
            theColumnExitReasonFieldName.DataPropertyName = "FieldId";
            //theColumnExitReasonFieldName.DataSource = theDS.Tables[0];

            if(strdeathconditional=="1")
            {
                theColumnExitReasonFieldName.DataSource = dsFieldDetails.Tables[0];
           
            }
            else
            {
                theColumnExitReasonFieldName.DataSource = theComDT;
            
            }
       
            //theColumnExitReasonFieldName.DataSource = theComDT;
            theColumnExitReasonFieldName.DisplayMember = "PdfName";
            theColumnExitReasonFieldName.ValueMember = "Id";
            theColumnExitReasonFieldName.Width = 250;
            theColumnExitReasonFieldName.ReadOnly = false;

            DataGridViewTextBoxColumn theColumnExitReasonFieldLabel = new DataGridViewTextBoxColumn();
            theColumnExitReasonFieldLabel.HeaderText = "Field Label";
            theColumnExitReasonFieldLabel.DataPropertyName = "FieldLabel";
            theColumnExitReasonFieldLabel.Width = 200;
            theColumnExitReasonFieldLabel.ReadOnly = false;

            DataGridViewTextBoxColumn theColumnExitReasonDisplay = new DataGridViewTextBoxColumn();
            theColumnExitReasonDisplay.HeaderText = "Display Type";
            theColumnExitReasonDisplay.Name = "Display Type";
            theColumnExitReasonDisplay.DataPropertyName = "DataType";
            theColumnExitReasonDisplay.Width = 150;
            theColumnExitReasonDisplay.ReadOnly = true;
           
            DataGridViewImageColumn theColumnExitReasonList = new DataGridViewImageColumn();
            theColumnExitReasonList.HeaderText = "List";
            theColumnExitReasonList.Name = "List";
            theColumnExitReasonList.Width = 100;
            theColumnExitReasonList.ReadOnly = true;

            DataGridViewImageColumn theColumnExitReasonBusiness = new DataGridViewImageColumn();
            theColumnExitReasonBusiness.HeaderText = "Business";
            theColumnExitReasonBusiness.DataPropertyName = "Business";
            theColumnExitReasonBusiness.Name = "Business";
            theColumnExitReasonBusiness.Width = 100;
            theColumnExitReasonBusiness.ReadOnly = true;

            DataGridViewTextBoxColumn theColumnExitReasonPredefine = new DataGridViewTextBoxColumn();
            theColumnExitReasonPredefine.HeaderText = "Predefined";
            theColumnExitReasonPredefine.DataPropertyName = "Predefined";
            theColumnExitReasonPredefine.Width = 10;
            theColumnExitReasonPredefine.Name = "Predefined";
            theColumnExitReasonPredefine.Visible = false;
            theColumnExitReasonPredefine.ReadOnly = true;

            DataGridViewTextBoxColumn theColumnExitReasonSeq = new DataGridViewTextBoxColumn();
            theColumnExitReasonSeq.HeaderText = "Seq";
            theColumnExitReasonSeq.DataPropertyName = "Seq";
            theColumnExitReasonSeq.Width = 10;
            theColumnExitReasonSeq.Visible = false;
            theColumnExitReasonSeq.ReadOnly= true;

            DataGridViewTextBoxColumn theColumnConfieldid = new DataGridViewTextBoxColumn();
            theColumnConfieldid.HeaderText = "Confieldid";
            theColumnConfieldid.DataPropertyName = "Confieldid";
            theColumnConfieldid.Width = 10;
            theColumnConfieldid.Visible = false;
            theColumnConfieldid.ReadOnly = true;

            DataGridViewTextBoxColumn theColumnSectionID = new DataGridViewTextBoxColumn();
            theColumnSectionID.HeaderText = "SectionID";
            theColumnSectionID.DataPropertyName = "SectionID";
            theColumnSectionID.Width = 10;
            theColumnSectionID.Visible = false;
            theColumnSectionID.ReadOnly = true;

            DataGridViewTextBoxColumn theColumnSectionName = new DataGridViewTextBoxColumn();
            theColumnSectionName.HeaderText = "SectionName";
            theColumnSectionName.DataPropertyName = "SectionName";
            theColumnSectionName.Width = 10;
            theColumnSectionName.Visible = false;
            theColumnSectionName.ReadOnly = true;

            DataGridViewTextBoxColumn theColumnFeatureId = new DataGridViewTextBoxColumn();
            theColumnFeatureId.HeaderText = "FeatureId";
            theColumnFeatureId.DataPropertyName = "FeatureId";
            theColumnFeatureId.Width = 10;
            theColumnFeatureId.Visible = false;
            theColumnFeatureId.ReadOnly = true;

            DataGridViewTextBoxColumn theColumnFeatureName = new DataGridViewTextBoxColumn();
            theColumnFeatureName.HeaderText = "FeatureName";
            theColumnFeatureName.DataPropertyName = "FeatureName";
            theColumnFeatureName.Width = 10;
            theColumnFeatureName.Visible = false;
            theColumnFeatureName.ReadOnly = true;

            DataGridViewTextBoxColumn theColumnModuleId = new DataGridViewTextBoxColumn();
            theColumnModuleId.HeaderText = "ModuleId";
            theColumnModuleId.DataPropertyName = "ModuleId";
            theColumnModuleId.Width = 10;
            theColumnModuleId.Visible = false;
            theColumnModuleId.ReadOnly = true;

            DataGridViewTextBoxColumn theColumnConpredefine = new DataGridViewTextBoxColumn();
            theColumnConpredefine.HeaderText = "Conpredefine";
            theColumnConpredefine.DataPropertyName = "Conpredefine";
            theColumnConpredefine.Width = 10;
            theColumnConpredefine.Visible = false;
            theColumnConpredefine.ReadOnly = true;
           



            dgwConditionalField.Columns.Add(theColumnExitReasonFieldName);
            dgwConditionalField.Columns.Add(theColumnExitReasonFieldLabel);
            dgwConditionalField.Columns.Add(theColumnExitReasonDisplay);
            dgwConditionalField.Columns.Add(theColumnExitReasonList);
            dgwConditionalField.Columns.Add(theColumnExitReasonBusiness);
            dgwConditionalField.Columns.Add(theColumnExitReasonPredefine);
            dgwConditionalField.Columns.Add(theColumnExitReasonSeq);
            dgwConditionalField.Columns.Add(theColumnConfieldid);    
            dgwConditionalField.Columns.Add(theColumnSectionID);
            dgwConditionalField.Columns.Add(theColumnSectionName);
            dgwConditionalField.Columns.Add(theColumnFeatureId);
            dgwConditionalField.Columns.Add(theColumnFeatureName);
            dgwConditionalField.Columns.Add(theColumnModuleId);
            dgwConditionalField.Columns.Add(theColumnConpredefine);
            dgwConditionalField.AllowUserToAddRows = true;
        }

        private void dgwConditionalField_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            {
                Image imgQuery;

                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {

                    if (dgwConditionalField.Columns[e.ColumnIndex].Name == "List")
                    {
                        dgwConditionalField.Rows[e.RowIndex].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        string display = string.Empty;
                        if (dgwConditionalField.Rows[e.RowIndex].Cells["Display Type"].Value != null)
                        {
                            display = dgwConditionalField.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
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
                    if (dgwConditionalField.Columns[e.ColumnIndex].Name == "Business")
                    {
                        dgwConditionalField.Rows[e.RowIndex].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\brule.bmp");
                        e.Value = imgQuery;
                    }
                }

            }

        }

        private void dgwConditionalField_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int Cfiled = 0;
            if (e.RowIndex > dgwConditionalField.Rows.Count - 2)
                return;

            string strSelectValue = string.Empty;
            if (e.ColumnIndex != -1 && e.RowIndex > -1)
            {
                IFieldDetail objConditional = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                DataSet dsConditional = new DataSet();

                if (dgwConditionalField.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                {
                    
                    Cfiled = Convert.ToInt32(dgwConditionalField.Rows[e.RowIndex].Cells[0].Value.ToString());
                }

                
               
                    //dsConditional = objConditional.GetConditionalFieldslist(Convert.ToInt32((Cfiled.ToString().Substring(1, Cfiled.ToString().Length - 1))), 1);
                

                if ((dsConditional.Tables.Count > 0) && (dsConditional.Tables[0].Rows.Count > 0))
                {
                    for (int i = 0; i < dsConditional.Tables[0].Rows.Count; i++)
                    {
                        if (strSelectValue == "")
                        {
                            strSelectValue = dsConditional.Tables[0].Rows[i]["Name"].ToString();
                        }
                        else
                        {
                            strSelectValue = strSelectValue + ";" + dsConditional.Tables[0].Rows[i]["Name"].ToString();
                        }
                    }
                }
            }

            GblIQCare.gblRowIndex = e.RowIndex;
            if (e.ColumnIndex != -1 && e.RowIndex > -1)
            {
                if (dgwConditionalField.Rows[e.RowIndex].Cells[0].Value != null && dgwConditionalField.Rows[e.RowIndex].Cells[0].Value.ToString()!="")
                {

                    if (!(GblIQCare.objhashSelectList.ContainsKey(e.RowIndex)))
                    {
                        GblIQCare.objhashSelectList[GblIQCare.gblRowIndex] = strSelectValue;
                        //GblIQCare.objhashSelectList.Add(e.RowIndex, dgwConditionalField.Rows[e.RowIndex].Cells[10].Value.ToString());
                    }
                    //BindBusinessRulesTable(e.RowIndex, Convert.ToInt32((Cfiled.ToString().Substring(1, Cfiled.ToString().Length - 1))), dgwConditionalField.Rows[e.RowIndex].Cells[2].Value.ToString());

                }
            }

            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {


                if (dgwConditionalField.Columns[e.ColumnIndex].Name == "Business")
                {

                    string display = string.Empty;
                    if (dgwConditionalField.Rows[e.RowIndex].Cells[0].Value != null)
                    {
                        
                        display = dgwConditionalField.Rows[e.RowIndex].Cells[0].Value.ToString();
                    }
                    if (display != "CheckBox" && display != "7")
                    {
                        if (GblIQCare.objHashtbl.Count == 0)
                        {
                            if (dgwConditionalField.Rows[e.RowIndex].Cells[4].Value != null)
                                GblIQCare.objHashtbl.Add(dgwConditionalField.Rows[e.RowIndex].Cells[4].Value, dgwConditionalField.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                            if (dgwConditionalField.Rows[e.RowIndex].Cells[2].Value != null)
                                GblIQCare.strDisplayType = GetControlName(dgwConditionalField.Rows[e.RowIndex].Cells[2].Value.ToString());
                            if (dgwConditionalField.Rows[e.RowIndex].Cells[0].Value != null)
                                GblIQCare.strFieldName = dgwConditionalField.Rows[e.RowIndex].Cells[0].Displayed.ToString();
                            if (dgwConditionalField.Rows[e.RowIndex].Cells[4].Value != null)
                            {
                                if (!(GblIQCare.objhashSelectList.ContainsKey(e.RowIndex)))
                                {

                                    GblIQCare.objhashSelectList[GblIQCare.gblRowIndex] = strSelectValue;
                                    //GblIQCare.objhashSelectList.Add(e.RowIndex, dgwConditionalField.Rows[e.RowIndex].Cells[10].Value.ToString());
                                }
                                BindBusinessRulesTable(e.RowIndex, Convert.ToInt32((Cfiled.ToString().Substring(1, Cfiled.ToString().Length - 1))), dgwConditionalField.Rows[e.RowIndex].Cells[2].Value.ToString());
                            }
                            else
                            {
                                if (!(GblIQCare.objhashBusinessRule.ContainsKey(e.RowIndex)))
                                {

                                    //GblIQCare.objhashBusinessRule.Add(e.RowIndex, "");
                                    DataTable dtEmptyDataTable = new DataTable();
                                    GblIQCare.objhashBusinessRule.Add(e.RowIndex, dtEmptyDataTable);
                                }
                            }
                            Form theForm;
                            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmBusinessRule, IQCare.FormBuilder"));
                            //theForm.MdiParent = this.MdiParent.MdiParent;
                            theForm.Left = 0;
                            theForm.Top = 0;
                            theForm.Show();

                        }
                    }
                }

                else if (dgwConditionalField.Columns[e.ColumnIndex].Name == "List")
                {
                    string display = string.Empty;
                    if (dgwConditionalField.Rows[e.RowIndex].Cells[1].Value != null)
                    {
                        display = dgwConditionalField.Rows[e.RowIndex].Cells[2].Value.ToString();

                    }

                    if (display.Trim() == "Select List" || display.Trim() == "Multi Select")
                    {
                        //GblIQCare.strMainGrdFldName = dgwConditionalField.Rows[e.RowIndex].Cells[0].Value.ToString();
                        //GblIQCare.iFieldId = Convert.ToInt32(dgwConditionalField.Rows[e.RowIndex].Cells[0].Value);

                        if (GblIQCare.objHashtbl.Count == 0)
                        {
                            //GblIQCare.strDisplayCol = display;
                            if (dgwConditionalField.Rows[e.RowIndex].Cells[0].Value != null)
                            {
                                GblIQCare.objHashtbl.Add(Convert.ToInt32((Cfiled.ToString().Substring(1, Cfiled.ToString().Length - 1))), dgwConditionalField.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                            }
                            if (dgwConditionalField.Rows[e.RowIndex].Cells[0].Value != null)
                            {
                                //GblIQCare.strSelectFieldName = dgwFieldDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
                                GblIQCare.strSelectFieldName = "select";
                            }
                            if (dgwConditionalField.Rows[e.RowIndex].Cells[0].Value != null)
                            {
                                BindBusinessRulesTable(e.RowIndex, Convert.ToInt32((Cfiled.ToString().Substring(1, Cfiled.ToString().Length - 1))), dgwConditionalField.Rows[e.RowIndex].Cells[5].Value.ToString());
                                if ((GblIQCare.objhashSelectList.ContainsKey(e.RowIndex)))
                                {

                                    GblIQCare.objhashSelectList[GblIQCare.gblRowIndex] = strSelectValue;
                                    //GblIQCare.objhashSelectList[e.RowIndex] = dgwConditionalField.Rows[e.RowIndex].Cells[3].Value.ToString();
                                }
                                else
                                {
                                    GblIQCare.objhashSelectList[GblIQCare.gblRowIndex] = strSelectValue;
                                    //GblIQCare.objhashSelectList.Add(e.RowIndex, dgwConditionalField.Rows[e.RowIndex].Cells[3].Value.ToString());
                                }
                                //GblIQCare.strSelectListValue = dgwConditionalField.Rows[e.RowIndex].Cells[3].Value.ToString();
                            }
                            else
                            {
                                if (!(GblIQCare.objhashSelectList.ContainsKey(e.RowIndex)))
                                {
                                    GblIQCare.objhashSelectList.Add(e.RowIndex, "");
                                }
                            }

                            GblIQCare.iFormMode = 1;
                            GblIQCare.iConditionalbtn = 1;
                            Form theForm;
                            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmSelectList, IQCare.FormBuilder"));
                            //theForm.MdiParent = this.MdiParent.MdiParent;
                            theForm.Left = 0;
                            theForm.Top = 0;
                            theForm.Show();

                        }
                        //}
                    }


                }
            }
        }

        private void dgwConditionalField_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType().ToString() == "System.Windows.Forms.DataGridViewComboBoxEditingControl")
            {
                theGrdCombo = (ComboBox)e.Control;
                theGrdCombo.SelectedValueChanged += null;
                theGrdCombo.SelectedValueChanged += new EventHandler(theCombo_SelectedValueChanged);
            }

            DataGridView dgwDataGridForEvent = sender as DataGridView;
            if (!IsHandleAdded && dgwDataGridForEvent.CurrentCell.ColumnIndex == 1)
            {
                TextBox txtFieldLabel = e.Control as TextBox;
                if (txtFieldLabel != null)
                {
                    txtFieldLabel.KeyPress += new KeyPressEventHandler(txtFieldLabel_KeyPress);
                    IsHandleAdded = true;
                }
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
                        if (dgwConditionalField.CurrentRow.Cells[0].Value.ToString() != theDV[0]["ID"].ToString())
                        {
                                for (int i = 0; i < theGrdSource.Rows.Count; i++)
                                {  

                                    if (GblIQCare.iFieldId.ToString() != "0")
                                    {

                                        if (theGrdSource.Rows[i]["ConfieldId"].ToString() == (Convert.ToInt32((GblIQCare.iFieldId.ToString().Substring(1, GblIQCare.iFieldId.ToString().Length - 1)))).ToString()
                                           && theGrdSource.Rows[i]["SectionId"].ToString() == cmbConditionalField.SelectedValue.ToString()
                                           && theGrdSource.Rows[i]["FieldId"].ToString() != theDV[0]["ID"].ToString())
                                        {
                                            theGrdSource.Rows[i].AcceptChanges();
                                            theGrdSource.Rows[i].BeginEdit();
                                            theGrdSource.Rows[i]["FieldId"] = theDV[0]["ID"].ToString();
                                            theGrdSource.Rows[i].EndEdit();
                                        }

                                    }
                                    else
                                    {
                                        if (theGrdSource.Rows[i]["ConfieldId"].ToString() == "0"
                                              && theGrdSource.Rows[i]["SectionId"].ToString() == cmbConditionalField.SelectedValue.ToString()
                                              && theGrdSource.Rows[i]["FieldId"].ToString() != theDV[0]["ID"].ToString())
                                        {
                                            theGrdSource.Rows[i].AcceptChanges();
                                            theGrdSource.Rows[i].BeginEdit();
                                            theGrdSource.Rows[i]["FieldId"] = theDV[0]["ID"].ToString();
                                            theGrdSource.Rows[i].EndEdit();
                                        }
                                    
                                    
                                    
                                    
                                    }

                                 }
                          }

                        dgwConditionalField.CurrentRow.Cells[0].Value = theDV[0]["ID"].ToString();                  
                        dgwConditionalField.CurrentRow.Cells[2].Value = theDV[0]["Name"].ToString();
                        dgwConditionalField.CurrentRow.Cells[5].Value = theDV[0]["Predefine"].ToString();
                        dgwConditionalField.CurrentRow.Cells[6].Value = dgwConditionalField.CurrentRow.Index + 1;
                        //dgwConditionalField.CurrentRow.Cells[7].Value = Convert.ToInt32((GblIQCare.iFieldId.ToString().Substring(1, GblIQCare.iFieldId.ToString().Length - 1)));
                       
                         dgwConditionalField.CurrentRow.Cells[7].Value = GblIQCare.iFieldId;

                        dgwConditionalField.CurrentRow.Cells[8].Value = cmbConditionalField.SelectedValue.ToString();
                        dgwConditionalField.CurrentRow.Cells[9].Value = cmbConditionalField.Text;
                        dgwConditionalField.CurrentRow.Cells[10].Value = GblIQCare.iCareEndedId;
                        dgwConditionalField.CurrentRow.Cells[11].Value = GblIQCare.strCareEndFeatureName;
                        dgwConditionalField.CurrentRow.Cells[12].Value = GblIQCare.ModuleId;
                        dgwConditionalField.CurrentRow.Cells[13].Value = flag;

                    }
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable theDT = ((DataTable)dgwConditionalField.DataSource).Copy();
                DataView theDV;
                if (theDT != null)
                {
                    if (cmbConditionalField.SelectedValue != null)
                    {
                        ////////////////////////
                        theDV = new DataView(theGrdSource);
                        theDV.RowFilter = "ConFieldId=" + GblIQCare.iFieldId + " and SectionId=" + cmbConditionalField.SelectedValue.ToString();
                        if (theDV.Count > 0)
                        {
                            while (theDV.Count > 0)
                            {
                                theDV.Delete(0);
                            }
                        }
                        theGrdSource.AcceptChanges();
                        ///////////////////////////

                        foreach (DataRow theDR in theDT.Rows)
                        {

                            theDV = new DataView(theGrdSource);
                            theDV.RowFilter = "ConFieldId=" + theDR["ConfieldId"].ToString() + " and SectionId=" + theDR["SectionId"].ToString() +
                            " and FieldId=" + theDR["FieldId"].ToString();

                            if (theDV.Count < 1)
                            {

                                DataRow theDRow = theGrdSource.NewRow();
                                theDRow["ConFieldId"] = theDR["ConFieldId"];
                                theDRow["SectionId"] = theDR["SectionId"];
                                theDRow["FieldId"] = theDR["FieldId"];
                                theDRow["FieldLabel"] = theDR["FieldLabel"];
                                theDRow["Predefined"] = theDR["Predefined"];
                                theDRow["Seq"] = theDR["Seq"];
                                theDRow["FieldName"] = theDR["FieldName"];
                                theDRow["DataType"] = theDR["DataType"];
                               // theDRow["SectionName"] = theDR["SectionName"];
                                theDRow["SectionName"] = cmbConditionalField.Text;
                                theDRow["FeatureID"] = GblIQCare.iCareEndedId;
                                theDRow["FeatureName"] = GblIQCare.strCareEndFeatureName;
                                theDRow["ModuleID"] = GblIQCare.ModuleId;
                                theDRow["Conpredefine"] = flag;
                                theGrdSource.Rows.Add(theDRow);

                            }
                            //else
                            //{
                            //    theGrdSource = theDT;
                            //}

                        }
                            //else
                            //{

                            //    for (int i = 0; i < theGrdSource.Rows.Count; i++)
                            //    {
                            //        if (theGrdSource.Rows[i]["ConfieldId"].ToString() == theDR["ConfieldId"].ToString()
                            //           && theGrdSource.Rows[i]["SectionId"].ToString() == theDR["SectionId"].ToString()
                            //           && theGrdSource.Rows[i]["FieldId"].ToString() == theDR["FieldId"].ToString()
                            //           && theGrdSource.Rows[i]["FieldLabel"].ToString() != theDR["FieldLabel"].ToString())
                            //        {
                            //            theGrdSource.Rows[i].AcceptChanges();
                            //            theGrdSource.Rows[i].BeginEdit();
                            //            theGrdSource.Rows[i]["FieldLabel"] = theDR["FieldLabel"].ToString();
                            //            theGrdSource.Rows[i].EndEdit();

                            //        }
                            //    }

                            //}

                
                      
                    }
                }

                GblIQCare.dtConditionalFields.Clear();
                GblIQCare.dtConditionalFields = theGrdSource.Copy();
                this.Close();
            }
            catch (Exception err)
            {
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

        public void BindBusinessRulesTable(Int32 index, Int32 FieldID, string thePredefined)
        {
            GblIQCare.dtBusinessValues.Clear();
            GblIQCare.dtBusinessValues.Columns.Clear();
            GblIQCare.dtBusinessValues.Rows.Clear();
            if (FieldID != 0)
            {
                IFieldDetail objFieldDetail;
                objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                objDsFieldDetails = objFieldDetail.GetCustomFields("", Convert.ToInt32(GblIQCare.iModuleId), GblIQCare.iManageCareEnded);
                if (objDsFieldDetails.Tables[4].Rows.Count > 0)
                {

                    dv = objDsFieldDetails.Tables[4].DefaultView;
                    if (thePredefined == "P")
                        dv.RowFilter = "FieldID='" + FieldID + "' and Predefined =1";
                    else
                        dv.RowFilter = "FieldID='" + FieldID + "' and Predefined =0";
                    DataView DvFilter = new DataView();
                    DataTable dt = new DataTable();
                    dt = dv.ToTable();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            GblIQCare.dtBusinessValues = dt;
                            if (!(GblIQCare.objhashBusinessRule.ContainsKey(index)))
                            {
                                GblIQCare.objhashBusinessRule.Add(index, dt);
                            }
                            else
                            {
                                GblIQCare.objhashBusinessRule[index] = dt;
                            }
                        }
                    }

                }
            }

        }

        string GetControlName(string Controltext)
        {
            string controlName = string.Empty;
            foreach (DataRow r in objDsFieldDetails.Tables[3].Rows)
            {
                if (Controltext.Length > 2)
                {
                    if (r["Name"].ToString() == Controltext)
                    {
                        controlName = r["Name"].ToString();
                    }
                }
                else
                {
                    if (r["ControlId"].ToString() == Controltext)
                    {
                        controlName = r["Name"].ToString();
                    }
                }
            }
            return controlName;
        }

        private void theUPExitReason_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgwConditionalField.Rows.Count <= 1)
                {
                    return;
                }
                if (dgwConditionalField.CurrentRow.Index > 0)
                {

                    Int32 iPos = dgwConditionalField.CurrentRow.Index;
                    DataTable theDT = (DataTable)dgwConditionalField.DataSource;
                    DataRow theDR = theDT.NewRow();
                    theDR.ItemArray = theDT.Rows[dgwConditionalField.CurrentRow.Index].ItemArray;
                    theDT.Rows[dgwConditionalField.CurrentRow.Index].Delete();
                    theDT.Rows.InsertAt(theDR, iPos - 1);
                    theDT.AcceptChanges();
                    dgwConditionalField.Refresh();

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
                if (dgwConditionalField.Rows.Count <= 1)
                {
                    return;
                }
                else if (dgwConditionalField.CurrentRow.Index >= 0)
                {
                    Int32 iPos = dgwConditionalField.CurrentRow.Index;
                    DataTable theDT = (DataTable)dgwConditionalField.DataSource;
                    DataRow theDR = theDT.NewRow();
                    theDR.ItemArray = theDT.Rows[dgwConditionalField.CurrentRow.Index].ItemArray;
                    theDT.Rows[dgwConditionalField.CurrentRow.Index].Delete();
                    theDT.Rows.InsertAt(theDR, iPos + 2);
                    theDT.AcceptChanges();
                    dgwConditionalField.Refresh();
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

        private void cmbConditionalField_SelectionChangeCommitted(object sender, EventArgs e)
        {

            //if (theGrdCombo.Focused == true && theGrdCombo.SelectedValue != null)
            //{
            //    if (theGrdCombo.DataSource != null && theGrdCombo.SelectedValue.GetType().ToString() != "System.Data.DataRowView")
            //    {
            //        DataTable theDT = (DataTable)theGrdCombo.DataSource;
            //        DataView theDV = new DataView(theDT.Copy());
            //        theDV.RowFilter = "Id = " + theGrdCombo.SelectedValue.ToString();
            //        if (theDV.Count > 0)
            //        {
            //            //dgwConditionalField.CurrentRow.Cells[0].Value = theDV[0]["Name"].ToString();
            //            dgwConditionalField.CurrentRow.Cells[2].Value = theDV[0]["Name"].ToString();
            //            dgwConditionalField.CurrentRow.Cells[5].Value = theDV[0]["Predefine"].ToString();
            //            dgwConditionalField.CurrentRow.Cells[6].Value = dgwConditionalField.CurrentRow.Index + 1;
            //            dgwConditionalField.CurrentRow.Cells[7].Value = Convert.ToInt32((GblIQCare.iFieldId.ToString().Substring(1, GblIQCare.iFieldId.ToString().Length - 1)));
            //            dgwConditionalField.CurrentRow.Cells[8].Value = cmbConditionalField.SelectedValue.ToString();
            //            dgwConditionalField.CurrentRow.Cells[9].Value = cmbConditionalField.Text;
            //            dgwConditionalField.CurrentRow.Cells[10].Value = GblIQCare.iCareEndedId;
            //            dgwConditionalField.CurrentRow.Cells[11].Value = GblIQCare.strCareEndFeatureName;
            //            dgwConditionalField.CurrentRow.Cells[12].Value = GblIQCare.ModuleId;

            //        }
            //    }
            //}



        }

        void txtFieldLabel_KeyPress(object sender, KeyPressEventArgs e)
        {
            //String strVal = e.KeyChar.ToString();
            //string strSearch = "-=\\/!@#$%^&*()+|.,<>?`~\";:'[]{}";
            //if (strSearch.IndexOf(strVal) >= 0)
            //{
            //    e.Handled = true;
            //}

            String strVal = e.KeyChar.ToString();
            string strSearch = "=\\/!@#$%^*+|.<>`~\";:[]{}";
            if (strSearch.IndexOf(strVal) >= 0)
            {
                e.Handled = true;
            }
        }
        #region CareEndConditionalField

        public int FindCodeID(DataTable dt, int ipredefine)
        {
            DataView dv = new DataView(dt);
            DataTable dtable = new DataTable();
            int iCodeid = 0;
            dv.RowFilter = "FieldId=" + GblIQCare.iFieldId.ToString() + " and predefined=" + ipredefine.ToString();
            if (dv.Count > 0)
            {
                dtable = dv.ToTable();
                foreach (DataRow theDR in dtable.Rows)
                {
                    iCodeid = Convert.ToInt32(theDR["CodeId"].ToString());

                }

            }
            else
            {

                iCodeid = 0;
            }

            return iCodeid;
        }

        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgwConditionalField.CurrentRow.Index > -1)
            {
                string strfieldval = dgwConditionalField.CurrentRow.Cells[0].Value.ToString();
                DataTable theDT = (DataTable)dgwConditionalField.DataSource;
                              
                Int32 theDtRows = theGrdSource.Rows.Count;

                //DataView dtview = new DataView(theGrdSource);
                //dtview.RowFilter=

                if (theDtRows > 0)
                {
                    
                    for (int i = 0; i < theDtRows; i++)
                    {
                        if (theGrdSource.Rows[i]["ConfieldId"].ToString() == GblIQCare.iFieldId.ToString()
                          && theGrdSource.Rows[i]["SectionId"].ToString() == cmbConditionalField.SelectedValue.ToString()
                          && theGrdSource.Rows[i]["FieldId"].ToString() == strfieldval)
                        {
                            theGrdSource.Rows[i].Delete();
                            theGrdSource.AcceptChanges();
                            //DataTable theDT = (DataTable)dgwConditionalField.DataSource;
                            theDT.Rows[dgwConditionalField.CurrentRow.Index].Delete();
                            return;
                        }
                        else
                        {
                            if (theGrdSource.Rows[i]["ConfieldId"].ToString() == GblIQCare.iFieldId.ToString()
                                && theGrdSource.Rows[i]["SectionId"].ToString() == cmbConditionalField.SelectedValue.ToString())
                            {
                                if (dgwConditionalField.CurrentRow.Index > -1)
                                {
                                    theDT.Rows[dgwConditionalField.CurrentRow.Index].Delete();
                                    return;
                                }

                            }
                            else
                            {
                                if (dgwConditionalField.CurrentRow.Index > -1)
                                {
                                    theDT.Rows[dgwConditionalField.CurrentRow.Index].Delete();
                                    return;
                                }
                            }


                        }
                    }
                }
                else
                {
                    if (dgwConditionalField.CurrentRow.Index > -1)
                    {
                        theDT.Rows[dgwConditionalField.CurrentRow.Index].Delete();
                        return;
                    }
                
                }

                

            }

        }


       


    }
}
