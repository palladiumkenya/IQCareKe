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
    public partial class frmConditionalDisplay : Form
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
        int codeid = 0;
        bool IsHandleAdded;

        public frmConditionalDisplay()
        {
            InitializeComponent();
        }

        private void Init_Form()
        {
            dgwConditionalField.AllowUserToAddRows = false;
            //pnlCondFields.Left = 2;
           // pnlCondFields.Top = 50;
           // pnlCondFields.Width = 880;
          //  pnlCondFields.Height = 455;
            lbloptionalfield.Text = GblIQCare.strMainGrdFldName;

            string strlist = GblIQCare.objhashSelectList[GblIQCare.gblRowIndex].ToString().Trim();
            //string strlist = GblIQCare.strSelectListstr.ToString();

            IFieldDetail objConditional = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
            dsFieldDetails = objConditional.GetConditionalFieldsDetails(GblIQCare.iFieldId, GblIQCare.iManageCareEnded);
            theGrdSource = dsFieldDetails.Tables[1];
            DataSet dsConditional = new DataSet();

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
                DataRow DRSelect1;
                if (GblIQCare.strYesNo == "Yes No" || GblIQCare.strYesNo == "6")
                {
                    
                    DRSelect1 = dsConditional.Tables[0].NewRow();
                    DRSelect1["ID"] = 1;
                    DRSelect1["Name"] = "Yes";
                    dsConditional.Tables[0].Rows.Add(DRSelect1);
                    DRSelect1 = dsConditional.Tables[0].NewRow();
                    DRSelect1["ID"] = 2;
                    DRSelect1["Name"] = "No";
                    dsConditional.Tables[0].Rows.Add(DRSelect1);

                }
                else
                {

                    string[] List = strlist.Split(';');
                    
                    for (int i = 0; i <= List.Length - 1; i++)
                    {
                        //cmbConditionalField.Items.Add("Select");
                        //cmbConditionalField.Items.Add(List[i]);

                        DRSelect1 = dsConditional.Tables[0].NewRow();
                        DRSelect1["ID"] = 999;
                        DRSelect1["Name"] = List[i].ToString();
                        dsConditional.Tables[0].Rows.Add(DRSelect1);
                    }
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
            //BindGrid_Conditionalfields(dsFieldDetails);
            //cmbConditionalField.Focus();

        }

        private void frmConditionalDisplay_Load(object sender, EventArgs e)
        {
            try
            {
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
                Init_Form();
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

            if ((cmbConditionalField.SelectedValue.ToString() != "999"))
            {
                DataTable theDT = (DataTable)dgwConditionalField.DataSource;
                DataView theDV;
                if (theDT != null)
                {
                    if (theDT.Rows.Count > 0)
                    {
                        foreach (DataRow theDR in theDT.Rows)
                        {
                            theDV = new DataView(theGrdSource);
                            theDV.RowFilter = "ConFieldId=" + theDR["ConfieldId"].ToString() + " and SectionId=" + theDR["SectionId"].ToString() +
                                " and FieldId=" + theDR["FieldId"].ToString();
                            theDV.Sort = "Seq";
                                
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
                                theDRow["SectionName"] = theDR["SectionName"];
                                theDRow["Conpredefine"] = flag;
                                theGrdSource.Rows.Add(theDRow);
                            }
                        }
                    }
                }
                theDV = null;
                if (theGrdSource.Rows.Count > 0)
                {
                    theDV = new DataView(theGrdSource);
                    theDV.RowFilter = "SectionId=" + cmbConditionalField.SelectedValue.ToString();
                    theDV.Sort = "Seq";
                    DataTable theFldDT = theDV.ToTable();
                    theFldDT.Columns["FieldLabel"].AllowDBNull = false;

                    foreach (DataRow theDR in theFldDT.Rows)
                    {
                        if (theDR["FieldId"].ToString().Contains("00000") == true)
                        {
                            theDR["FieldId"] = "171";

                        }
                    }
                    dgwConditionalField.DataSource = theFldDT;
                    BindGrid_Conditionalfields(dsFieldDetails);
                    //if (theFldDT.Rows.Count > 0)
                    //{
                    //    this.dgwConditionalField.Columns["Seq"].SortMode = DataGridViewColumnSortMode.Automatic;
                    //}
                    
                }
                else
                {
                    dgwConditionalField.DataSource = theGrdSource;
                    BindGrid_Conditionalfields(dsFieldDetails);
                    //if (theGrdSource.Rows.Count > 0)
                    //{
                    //    this.dgwConditionalField.Columns["Seq"].SortMode = DataGridViewColumnSortMode.Automatic;
                    //}
                }
            }
            else
            {
                if (cmbConditionalField.Text.ToString() != "")
                {

                    DataTable theDT = (DataTable)dgwConditionalField.DataSource;
                    DataView theDV;
                    if (theDT != null)
                    {
                        if (theDT.Rows.Count > 0)
                        {
                            foreach (DataRow theDR in theDT.Rows)
                            {
                                theDV = new DataView(theGrdSource);
                                theDV.RowFilter = "ConFieldId=" + theDR["ConfieldId"].ToString() + " and SectionId=" + theDR["SectionId"].ToString() +
                                " and FieldId=" + theDR["FieldId"].ToString();// +" and SectionName='" + cmbConditionalField.Text.ToString() + "'";
                                theDV.Sort = "Seq"; 
                                if (theDV.Count < 1)
                                {
                                    DataRow theDRow = theGrdSource.NewRow();
                                    theDRow["ConFieldId"] = theDR["ConfieldId"];
                                    theDRow["SectionId"] = theDR["SectionId"];
                                    theDRow["FieldId"] = theDR["FieldId"];
                                    theDRow["FieldLabel"] = theDR["FieldLabel"];
                                    theDRow["Predefined"] = theDR["Predefined"];
                                    theDRow["Seq"] = theDR["Seq"];
                                    theDRow["FieldName"] = theDR["FieldName"];
                                    theDRow["DataType"] = theDR["DataType"];
                                    theDRow["SectionName"] = cmbConditionalField.Text.ToString();
                                    theDRow["Conpredefine"] = flag;
                                    theGrdSource.Rows.Add(theDRow);
                                }
                            }
                        }
                    }
                    theDV = null;
                    if (theGrdSource.Rows.Count > 0)
                    {
                        theDV = new DataView(theGrdSource);

                        theDV.RowFilter = "SectionId=0" + " and SectionName='" + cmbConditionalField.Text.ToString() + "'";
                        theDV.Sort = "Seq";
                        DataTable theFldDT = theDV.ToTable();
                        theFldDT.Columns["FieldLabel"].AllowDBNull = false;

                        foreach (DataRow theDR in theFldDT.Rows)
                        {
                            if (theDR["FieldId"].ToString().Contains("00000")==true)
                            {
                                theDR["FieldId"] = "171"; 
                               
                            }
                        }
                             
                        dgwConditionalField.DataSource = theFldDT;
                        BindGrid_Conditionalfields(dsFieldDetails);
                        //if (theFldDT.Rows.Count > 0)
                        //{
                        //    this.dgwConditionalField.Columns["Seq"].SortMode = DataGridViewColumnSortMode.Automatic;
                        //}
                    }
                    else
                    {
                        dgwConditionalField.DataSource = theGrdSource;
                        BindGrid_Conditionalfields(dsFieldDetails);
                        //if (theGrdSource.Rows.Count > 0)
                        //{
                        //    this.dgwConditionalField.Columns["Seq"].SortMode = DataGridViewColumnSortMode.Automatic;
                        //}
                    }
                }

            }

        }

        private void BindGrid_Conditionalfields(DataSet theDS)
        {
            string strGetPath = GblIQCare.GetPath();
            dgwConditionalField.Columns.Clear();
            dgwConditionalField.AutoGenerateColumns = false;

            DataGridViewComboBoxColumn theColumnExitReasonFieldName = new DataGridViewComboBoxColumn();
            theColumnExitReasonFieldName.HeaderText = "Table Field Name";
            theColumnExitReasonFieldName.Name = "Table Field Name";
            theColumnExitReasonFieldName.DataPropertyName = "FieldId";
            theColumnExitReasonFieldName.DataSource = theDS.Tables[0];
            theColumnExitReasonFieldName.DisplayMember = "PdfName";
            theColumnExitReasonFieldName.ValueMember = "FieldId";
            theColumnExitReasonFieldName.Width = 250;

            DataGridViewTextBoxColumn theColumnExitReasonFieldLabel = new DataGridViewTextBoxColumn();
            theColumnExitReasonFieldLabel.HeaderText = "Field Label";
            theColumnExitReasonFieldLabel.DataPropertyName = "FieldLabel";
            theColumnExitReasonFieldLabel.Width = 200;

            DataGridViewTextBoxColumn theColumnExitReasonDisplay = new DataGridViewTextBoxColumn();
            theColumnExitReasonDisplay.HeaderText = "Display Type";
            theColumnExitReasonDisplay.Name = "Display Type";
            theColumnExitReasonDisplay.ReadOnly = true;
            theColumnExitReasonDisplay.DataPropertyName = "DataType";
            theColumnExitReasonDisplay.Width = 150;

            DataGridViewImageColumn theColumnExitReasonList = new DataGridViewImageColumn();
            theColumnExitReasonList.HeaderText = "List";
            theColumnExitReasonList.Name = "List";
            theColumnExitReasonList.Width = 100;

            DataGridViewImageColumn theColumnExitReasonBusiness = new DataGridViewImageColumn();
            theColumnExitReasonBusiness.HeaderText = "Business";
            theColumnExitReasonBusiness.DataPropertyName = "Business";
            theColumnExitReasonBusiness.Name = "Business";
            theColumnExitReasonBusiness.Width = 100;

            DataGridViewTextBoxColumn theColumnExitReasonPredefine = new DataGridViewTextBoxColumn();
            theColumnExitReasonPredefine.HeaderText = "Predefined";
            theColumnExitReasonPredefine.DataPropertyName = "Predefined";
            theColumnExitReasonPredefine.Width = 10;
            theColumnExitReasonPredefine.Name = "Predefined";
            theColumnExitReasonPredefine.Visible = false;

            DataGridViewTextBoxColumn theColumnExitReasonSeq = new DataGridViewTextBoxColumn();
            theColumnExitReasonSeq.HeaderText = "Seq";
            theColumnExitReasonSeq.DataPropertyName = "Seq";
            theColumnExitReasonSeq.Width = 10;
            
            theColumnExitReasonSeq.Visible = false;

            DataGridViewTextBoxColumn theColumnConfieldid = new DataGridViewTextBoxColumn();
            theColumnConfieldid.HeaderText = "Confieldid";
            theColumnConfieldid.DataPropertyName = "Confieldid";
            theColumnConfieldid.Width = 10;
            

            theColumnConfieldid.Visible = false;

            DataGridViewTextBoxColumn theColumnSectionID = new DataGridViewTextBoxColumn();
            theColumnSectionID.HeaderText = "SectionID";
            theColumnSectionID.DataPropertyName = "SectionID";
            theColumnSectionID.Width = 10;
            theColumnSectionID.Visible = false;

            DataGridViewTextBoxColumn theColumnSectionName = new DataGridViewTextBoxColumn();
            theColumnSectionName.HeaderText = "SectionName";
            theColumnSectionName.DataPropertyName = "SectionName";
            theColumnSectionName.Width = 10;
            theColumnSectionName.Visible = false;

            DataGridViewTextBoxColumn theColumnconpredefine = new DataGridViewTextBoxColumn();
            theColumnconpredefine.HeaderText = "Conpredefine";
            theColumnconpredefine.DataPropertyName = "Conpredefine";
            theColumnconpredefine.Width = 10;
            theColumnconpredefine.Visible = false;

            DataGridViewTextBoxColumn theColumnFieldName = new DataGridViewTextBoxColumn();
            theColumnFieldName.HeaderText = "FieldName";
            theColumnFieldName.DataPropertyName = "FieldName";
            theColumnFieldName.Width = 10;
            theColumnFieldName.Visible = false;

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
            dgwConditionalField.Columns.Add(theColumnconpredefine);
            dgwConditionalField.Columns.Add(theColumnFieldName);
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
                        string displaytype = string.Empty;
                        if (dgwConditionalField.Rows[e.RowIndex].Cells["Display Type"].Value != null)
                        {
                            displaytype = dgwConditionalField.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
                        }
                        if (displaytype.Trim() == "PlaceHolder")
                        {
                            dgwConditionalField.Rows[e.RowIndex].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\nonactive.bmp");
                            e.Value = imgQuery;

                        }
                        else
                        {

                            dgwConditionalField.Rows[e.RowIndex].Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            imgQuery = Image.FromFile(GblIQCare.GetPath() + "\\brule.bmp");
                            e.Value = imgQuery;
                        }

                    }
                }

            }

        }

        private void dgwConditionalField_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ////////if (e.ColumnIndex != -1 && e.RowIndex > -1)
            ////////{
            ////////    string Displaytype = "";
            ////////    Int32 theId = Convert.ToInt32(dgwConditionalField.Rows[e.RowIndex].Cells[0].Value);
            ////////    Form theForm;
            ////////    if (dgwConditionalField.Columns[e.ColumnIndex].Name == "Business Rule")
            ////////    {
            ////////        string display = string.Empty;
            ////////        if (dgwConditionalField.Rows[e.RowIndex].Cells[1].Value != null)
            ////////            display = dgwConditionalField.Rows[e.RowIndex].Cells[1].Value.ToString();

            ////////        if (display != "CheckBox" && display != "7")
            ////////        {
            ////////            if (GblIQCare.objHashtbl.Count == 0)
            ////////            {
            ////////                if (dgwConditionalField.Rows[e.RowIndex].Cells[0].Value != null)
            ////////                    GblIQCare.objHashtbl.Add(theId, dgwConditionalField.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            ////////                if (dgwConditionalField.Rows[e.RowIndex].Cells[2].Value != null)
            ////////                    GblIQCare.strDisplayType = GetControlName(dgwConditionalField.Rows[e.RowIndex].Cells[1].Value.ToString());
            ////////                if (dgwConditionalField.Rows[e.RowIndex].Cells[11].Value != null)
            ////////                    GblIQCare.strFieldName = dgwConditionalField.Rows[e.RowIndex].Cells[11].Value.ToString();
            ////////                if (dgwConditionalField.Rows[e.RowIndex].Cells[0].Value != null)
            ////////                {
            ////////                    if (!(GblIQCare.objhashSelectList.ContainsKey(theId)))
            ////////                    {
            ////////                        GblIQCare.objhashSelectList.Add(theId, dgwConditionalField.Rows[e.RowIndex].Cells[3].Value.ToString());
            ////////                    }
            ////////                }
            ////////                else
            ////////                {
            ////////                    if (!(GblIQCare.objhashBusinessRule.ContainsKey(theId)))
            ////////                    {
            ////////                        DataTable dtEmptyDataTable = new DataTable();
            ////////                        GblIQCare.objhashBusinessRule.Add(theId, dtEmptyDataTable);
            ////////                    }
            ////////                }
            ////////                GblIQCare.iFormMode = 0;
            ////////                theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmBusinessRule, IQCare.FormBuilder"));
            ////////                theForm.Left = 0;
            ////////                theForm.Top = 0;
            ////////                theForm.Show();
            ////////            }
            ////////        }
            ////////    }
            ////////    else if (dgwConditionalField.Columns[e.ColumnIndex].Name == "List" || dgwConditionalField.Columns[e.ColumnIndex].Name == "Associated Fields")
            ////////    {
            ////////        string display = string.Empty;
            ////////        if (dgwConditionalField.Rows[e.RowIndex].Cells[2].Value != null)
            ////////        {
            ////////            display = dgwConditionalField.Rows[e.RowIndex].Cells[2].Value.ToString();
            ////////        }

            ////////        if (display.Trim() == "Select List" || display == "4" || display.Trim() == "Multi Select" || display == "9" || display.Trim() == "Yes No" || display == "6")
            ////////        {
            ////////            //GblIQCare.strSelectList = "frmFieldDetails";
            ////////            //GblIQCare.strMainGrdFldName = dgwFieldDetails.Rows[e.RowIndex].Cells[0].Value.ToString();
            ////////            //GblIQCare.iFieldId = Convert.ToInt32(dgwFieldDetails.Rows[e.RowIndex].Cells[9].Value);
            ////////            //GblIQCare.iModuleId = Convert.ToInt32(dgwFieldDetails.Rows[e.RowIndex].Cells[12].Value);
            ////////            //GblIQCare.strgblPredefined = gblPredefine;
            ////////            if (dgwConditionalField.Columns[e.ColumnIndex].Name == "Associated Fields")
            ////////            {
            ////////                GblIQCare.iFormMode = 1;
            ////////                GblIQCare.iConditionalbtn = 0;

            ////////                if (GblIQCare.iFieldId == 167)
            ////////                {
            ////////                    GblIQCare.iConditionalbtn = 1;
            ////////                }
            ////////                else
            ////////                {
            ////////                    GblIQCare.iConditionalbtn = 0;

            ////////                }
            ////////            }
            ////////            else
            ////////            {
            ////////                GblIQCare.iFormMode = 0;
            ////////                GblIQCare.iConditionalbtn = 1;
            ////////            }

            ////////            if (GblIQCare.objHashtbl.Count == 0)
            ////////            {
            ////////                if (dgwConditionalField.Rows[e.RowIndex].Cells[0].Value != null)
            ////////                {
            ////////                    GblIQCare.objHashtbl.Add(theId, dgwConditionalField.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            ////////                }
            ////////                if (dgwConditionalField.Rows[e.RowIndex].Cells[0].Value != null)
            ////////                {
            ////////                    GblIQCare.strSelectFieldName = "select";
            ////////                }
            ////////                if (dgwConditionalField.Rows[e.RowIndex].Cells[0].Value != null)
            ////////                {
            ////////                    if (!(GblIQCare.objhashSelectList.ContainsKey(theId)))
            ////////                    {
            ////////                        if (dgwConditionalField.Rows[e.RowIndex].Cells[3].Value != null)
            ////////                        {
            ////////                            GblIQCare.objhashSelectList.Add(theId, dgwConditionalField.Rows[e.RowIndex].Cells[3].Value.ToString());
            ////////                            GblIQCare.strSelectListValue = dgwConditionalField.Rows[e.RowIndex].Cells[3].Value.ToString();
            ////////                        }
            ////////                        else
            ////////                        {
            ////////                            GblIQCare.objhashSelectList.Add(theId, "");
            ////////                            GblIQCare.strSelectListValue = "";
            ////////                        }
            ////////                    }

            ////////                }
            ////////                else
            ////////                {
            ////////                    if (!(GblIQCare.objhashSelectList.ContainsKey(theId)))
            ////////                    {
            ////////                        GblIQCare.objhashSelectList.Add(theId, "");
            ////////                    }
            ////////                }
            ////////                theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmSelectList, IQCare.FormBuilder"));
            ////////                theForm.Left = 0;
            ////////                theForm.Top = 0;
            ////////                theForm.Show();
            ////////            }
            ////////        }
            ////////    }
            ////////}

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
                        if (theGrdCombo.SelectedValue.ToString() == "171")
                        {
                            dgwConditionalField.CurrentRow.Cells[1].Value = "PlaceHolder";
                            dgwConditionalField.CurrentRow.Cells[1].ReadOnly = true;

                        }
                        else
                        {
                            dgwConditionalField.CurrentRow.Cells[1].ReadOnly = false;
                            dgwConditionalField.CurrentRow.Cells[1].Value = "";
                        }
                        dgwConditionalField.CurrentRow.Cells[0].Value = theDV[0]["ID"].ToString();
                        dgwConditionalField.CurrentRow.Cells[2].Value = theDV[0]["Name"].ToString();
                        dgwConditionalField.CurrentRow.Cells[5].Value = theDV[0]["Predefine"].ToString();
                        dgwConditionalField.CurrentRow.Cells[6].Value = dgwConditionalField.CurrentRow.Index + 1;
                        dgwConditionalField.CurrentRow.Cells[7].Value = GblIQCare.iFieldId;


                        if (cmbConditionalField.SelectedValue.ToString() != "999")
                        {
                            dgwConditionalField.CurrentRow.Cells[8].Value = cmbConditionalField.SelectedValue.ToString();
                        }
                        else
                        {
                            dgwConditionalField.CurrentRow.Cells[8].Value = 0;
                        }
                        dgwConditionalField.CurrentRow.Cells[9].Value = cmbConditionalField.Text;
                        dgwConditionalField.CurrentRow.Cells[10].Value = flag;
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
                                theDRow["SectionName"] = theDR["SectionName"];
                                theDRow["Conpredefine"] = flag;
                                theGrdSource.Rows.Add(theDRow);

                            }


                        }

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
            /* Jayant
            if (cmbConditionalField.SelectedValue.ToString() == "0")
            {
                return;
            }

            try
            {
                DataTable theDT = (DataTable)dgwConditionalField.DataSource;
                DataView theDV;
                int icountplaceHolder = 1;
                if (theDT.Rows.Count > 0)
                {

                    if (cmbConditionalField.SelectedValue.ToString() != "999")
                    {

                        foreach (DataRow theDR in theDT.Rows)
                        {
                            if (theDR["FieldId"].ToString() == "171")
                            {
                                theDR["FieldId"] = theDR["FieldId"].ToString() + "00000" + icountplaceHolder.ToString();
                                icountplaceHolder += 1;
                            }
                            theDV = new DataView(theGrdSource);
                            //theDV = new DataView(theDR);
                            theDV.RowFilter = "ConFieldId=" + theDR["ConfieldId"].ToString() + " and SectionId=" + theDR["SectionId"].ToString() +
                                " and FieldId=" + theDR["FieldId"].ToString();
                            // + " and FieldLabel=" + theDR["FieldLabel"].ToString();

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
                                theDRow["SectionName"] = theDR["SectionName"];
                                theDRow["Conpredefine"] = flag;
                                theGrdSource.Rows.Add(theDRow);
                            }
                            else
                            {
                                theGrdSource = theDT;
                            }


                        }
                    }
                    else
                    {
                        foreach (DataRow theDR in theDT.Rows)
                        {

                            theDV = new DataView(theGrdSource);
                            theDV.RowFilter = "ConFieldId=" + theDR["ConfieldId"].ToString() + " and SectionId=" + theDR["SectionId"].ToString() +
                                " and FieldId=" + theDR["FieldId"].ToString();
                            if (theDV.Count < 1)
                            {

                                DataRow theDRow = theGrdSource.NewRow();
                                theDRow["ConFieldId"] = theDR["ConfieldId"];
                                theDRow["SectionId"] = theDR["SectionId"];
                                theDRow["FieldId"] = theDR["FieldId"];
                                theDRow["FieldLabel"] = theDR["FieldLabel"];
                                theDRow["Predefined"] = theDR["Predefined"];
                                theDRow["Seq"] = theDR["Seq"];
                                theDRow["FieldName"] = theDR["FieldName"];
                                theDRow["DataType"] = theDR["DataType"];
                                theDRow["SectionName"] = cmbConditionalField.Text;
                                theDRow["Conpredefine"] = flag;
                                theGrdSource.Rows.Add(theDRow);

                            }

                        }

                    }
                }
                //}

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
            */
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

        private void dgwConditionalField_KeyDown(object sender, KeyEventArgs e)
        {

            //////if (e.KeyValue == 46)
            //////{
            //////    string strfieldval = dgwConditionalField.CurrentRow.Cells[0].Value.ToString();

            //////    for (int i = 0; i < theGrdSource.Rows.Count; i++)
            //////    {
            //////        if (theGrdSource.Rows[i]["ConfieldId"].ToString() == GblIQCare.iFieldId.ToString()
            //////           && theGrdSource.Rows[i]["SectionId"].ToString() == cmbConditionalField.SelectedValue.ToString()
            //////           && theGrdSource.Rows[i]["FieldId"].ToString() == strfieldval)
            //////        {
            //////            theGrdSource.Rows[i].AcceptChanges();
            //////            theGrdSource.Rows[i].Delete();
            //////            theGrdSource.Rows[i].AcceptChanges();

            //////        }
            //////    }

            //////}

        }

        private void dgwConditionalField_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > 0)
            //{
            //    string strfieldval = dgwConditionalField.CurrentRow.Cells[1].Value.ToString();

            //    if (strfieldval == "")
            //    {

            //        IQCareWindowMsgBox.ShowWindow("ConditionalFieldBlank", this);

            //        dgwConditionalField.CurrentCell = dgwConditionalField.CurrentRow.Cells[1];

            //        return;

            //    }
            //}
        }

        private void dgwConditionalField_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex > 0)
            //{
            //    string strfieldval = dgwConditionalField.CurrentRow.Cells[1].Value.ToString();

            //    if (strfieldval == "")
            //    {

            //        IQCareWindowMsgBox.ShowWindow("ConditionalFieldBlank", this);

            //        dgwConditionalField.CurrentCell = dgwConditionalField.CurrentRow.Cells[1];

            //        return;

            //    }
            //}


        }

        private void dgwConditionalField_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Message.Contains("constrained to be unique") == true)
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgwConditionalField.CurrentRow.Index > -1)
            {
                string strfieldval = dgwConditionalField.CurrentRow.Cells[0].Value.ToString();
                DataTable theDT = (DataTable)dgwConditionalField.DataSource;
                Int32 theDtRows = theGrdSource.Rows.Count;
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
                            theDT.Rows[dgwConditionalField.CurrentRow.Index].Delete();
                            return;

                        }


                    }
                }

            }
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

    }
}
