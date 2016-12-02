using Application.Common;
using Application.Presentation;
using Interface.FormBuilder;
using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace IQCare.FormBuilder
{
    public partial class frmFieldDetails : Form
    {
        Image img1;
        Image img2;
        Image img3;
        Image img4;
        Image img5;
        Image img6;
        Form theForm;
        DataTable dtFieldDetail;
        DataSet mainDataset = new DataSet();
        Bitmap blankImg;
        public bool blnDisplayType = false;
        public bool blnFieldNameChange = false;
        public bool blnActiveField = false;
        public bool blnDisplayTypeChange = false;
        public bool blnNameChange = false;
        public bool blnRecordSave = false;
        DataView dv = new DataView();
        public int gblFieldID = 0;
        public string gblFieldName = string.Empty;
        public string gblPredefine = string.Empty;

        bool IsHandleAdded;

        public frmFieldDetails()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This function is used to load style sheet and Bind the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmFieldDetails_Load(object sender, EventArgs e)
        {
            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //set css end
            GblIQCare.iConditionalbtn = 0;

            BindFunctions objBindControls = new BindFunctions();
            IManageForms objManageForms = (IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder");
            DataSet dsModule = objManageForms.GetPublishedModuleList();
            DataTable dtAddAll = dsModule.Tables[0];
            DataRow theDR = dtAddAll.NewRow();
            theDR["ModuleName"] = "All";
            theDR["ModuleId"] = 0;
            dtAddAll.Rows.InsertAt(theDR, 0);
            objBindControls.Win_BindCombo(cmbTechArea, dtAddAll, "ModuleName", "ModuleId");
            cmbTechArea.SelectedIndex = 0;
            GblIQCare.strSelectListstr = "";

            if (GblIQCare.iManageCareEnded == 1)
            {
                label2.Visible = false;
                cmbTechArea.Visible = false;
                BindGrid("", 0);
            }
            else if (GblIQCare.iManageCareEnded == 2)
            {
                GblIQCare.iConditionalbtn = 0;
                label2.Visible = false;
                cmbTechArea.Visible = false;
                BindGrid("", 0);
            }
            else
            {
                label2.Visible = true;
                cmbTechArea.Visible = true;
                BindGrid("", 0);
            }
            SetRights();


        }

        public void SetRights()
        {

            if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageFields, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnadd.Enabled = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageFields, FunctionAccess.Update, GblIQCare.dtUserRight) == false)
            {
                btnsave.Enabled = false;
            }
            if (GblIQCare.HasFunctionRight(ApplicationAccess.ManageFields, FunctionAccess.Delete, GblIQCare.dtUserRight) == false)
            {
                btndelete.Enabled = false;
            }

        }

        /// <summary>
        /// This function is used to search record 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (GblIQCare.iManageCareEnded == 1)
                BindGrid(txtfieldName.Text, 0);
            else
                BindGrid(txtfieldName.Text, Convert.ToInt32(cmbTechArea.SelectedValue));

            RemoveHastTable();
        }
        /// <summary>
        /// This function is used to search all record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnshowall_Click(object sender, EventArgs e)
        {
            txtfieldName.Text = "";
            BindGrid("", Convert.ToInt32(cmbTechArea.SelectedValue));
            RemoveHastTable();
        }
        /// <summary>
        /// This function is used to bind grid with database
        /// </summary>
        /// <param name="fieldName"></param>
        public void BindGrid(string fieldName, int iTechArea)
        {
            try
            {
                IFieldDetail objFieldDetail;
                btnadd.Enabled = true;
                GblIQCare.strRetainSelectList = "";
                GblIQCare.DsFieldDetailsCon = null;
                IsHandleAdded = false;
                objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                mainDataset = objFieldDetail.GetCustomFields(fieldName, iTechArea, GblIQCare.iManageCareEnded);
                GblIQCare.DsFieldDetailsCon = mainDataset;
                dtFieldDetail = CreateTable();
                GblIQCare.objHashtbl.Clear();
                GblIQCare.objhashSelectList.Clear();
                GblIQCare.objhashBusinessRule.Clear();
                RemoveHastTable();
                if (mainDataset.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in mainDataset.Tables[0].Rows)
                    {
                        DataRow row;
                        try
                        {
                            string referenceId = r["ReferenceId"].ToString();
                            row = dtFieldDetail.NewRow();
                            row["id"] = Convert.ToInt32(r["ID"].ToString());
                            row["FieldName"] = r["fieldName"].ToString();
                            row["FieldDesc"] = r["fieldDesc"].ToString();
                            row["ControlID"] = Convert.ToInt32(r["controlID"].ToString());
                            row["ControlName"] = r["Name"].ToString();
                            row["ReferenceId"] = referenceId;
                            row["Predefined"] = GetPredefine(Convert.ToInt32(r["Predefine"].ToString()));
                            row["DeleteFlag"] = Convert.ToInt32(r["DeleteFlag"].ToString());
                            row["UpdatedBy"] = r["UserName"].ToString();
                            row["LastUpdate"] = r["UpdateDate"].ToString();
                            row["AssociatedFields"] = r["ConditionalField"].ToString();
                            row["ListValue"] = "";
                            // if(referenceId == "SELECTLIST_TEXTBOX" || referenceId=="SELECT_LIST" || referenceId=="SELECT_LIST_MULTI") 
                            // {
                            //     row["List"] = 1  ;

                            // }
                            //else
                            // {
                            row["List"] = r["LookupValues"].ToString() != "" ? 1 : 0;
                            //  }
                            //Convert.ToString(GetListDetails(Convert.ToInt32(r["ID"].ToString()), mainDataset.Tables[2], Convert.ToInt32(r["Predefine"].ToString())));
                            //row["ListValue"] = Convert.ToString(GetListValues(Convert.ToInt32(r["ID"].ToString()), mainDataset.Tables[2], Convert.ToInt32(r["Predefine"].ToString())));
                            row["ListValue"] = r["LookupValues"].ToString();
                            row["BussinessRule"] = Convert.ToString(GetBussinessRules(Convert.ToInt32(r["ID"].ToString()), mainDataset.Tables[1], Convert.ToInt32(r["Predefine"].ToString())));
                            row["ModuleId"] = Convert.ToInt32(r["ModuleID"].ToString());
                            dtFieldDetail.Rows.Add(row);
                        }
                        catch (Exception err)
                        {
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["MessageText"] = err.Message.ToString();
                            IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                        }
                    }
                    DataView dvFieldDetails = new DataView();
                    dvFieldDetails = dtFieldDetail.DefaultView;
                    dvFieldDetails.Sort = "Predefined desc";
                    //added on 16may2011
                    dvFieldDetails.RowFilter = string.Format("Id <> 71");
                    //
                    DataTable dt = new DataTable();
                    dt = dvFieldDetails.ToTable();

                    ShowGrid(mainDataset, dt);

                }
                else
                {
                    if (mainDataset.Tables[0].Rows.Count == 0)
                    {
                        ShowGrid(mainDataset, dtFieldDetail);
                    }
                    else
                    {
                        IQCareWindowMsgBox.ShowWindow("PMTCTNoRecord", this);

                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
            ///SetRights();
        }
        /// <summary>
        /// This function is used to create datagrid column on runtime
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="dt"></param>
        /// 
        private DataTable BindSelect(DataTable TheDT)
        {
            DataTable dt = new DataTable();

            DataColumn ControlID = new DataColumn("ControlID");
            ControlID.DataType = Type.GetType("System.Int32");
            dt.Columns.Add(ControlID);

            DataColumn Name = new DataColumn("Name");
            Name.DataType = Type.GetType("System.String");
            dt.Columns.Add(Name);

            DataRow DRSelect;
            DRSelect = dt.NewRow();
            DRSelect["ControlID"] = "0";
            DRSelect["Name"] = "Select";
            dt.Rows.Add(DRSelect);

            foreach (DataRow theDR in TheDT.Rows)
            {
                DRSelect = dt.NewRow();
                DRSelect["ControlID"] = Convert.ToInt32(theDR["ControlID"]);
                DRSelect["Name"] = Convert.ToString(theDR["Name"]);
                dt.Rows.Add(DRSelect);
            }
            return dt;
        }

        public void ShowGrid(DataSet ds, DataTable dt)
        {
            ClearGrid();
            string strGetPath = GblIQCare.GetPath();
            img1 = Image.FromFile(strGetPath + "\\List.bmp");
            img4 = Image.FromFile(strGetPath + "\\listdesible.bmp");
            img2 = Image.FromFile(strGetPath + "\\brule.bmp");
            img3 = Image.FromFile(strGetPath + "\\nonactive.bmp");
            img5 = Image.FromFile(strGetPath + "\\associatedfield.jpg");
            img6 = Image.FromFile(strGetPath + "\\associatedfieldGrey.jpg");

            blankImg = new Bitmap(100, 100);
            dgwFieldDetails.AutoGenerateColumns = false;
            dgwFieldDetails.AllowUserToAddRows = false;
            dgwFieldDetails.AllowUserToOrderColumns = true;
            dgwFieldDetails.DefaultCellStyle.BackColor = this.BackColor;
            dgwFieldDetails.DefaultCellStyle.ForeColor = SystemColors.GrayText;
            DataGridViewComboBoxColumn DisplayType = new DataGridViewComboBoxColumn();
            //added on 16may2011
            DataView theDV = new DataView(ds.Tables[3]);
            DataTable dtfilter = new DataTable();

            if (GblIQCare.iManageCareEnded == 0)
            {
                theDV.RowFilter = string.Format("ControlID <> 15");
            }
            dtfilter = theDV.ToTable();
            DataView theDVNew = new DataView(dtfilter);

            theDVNew.RowFilter = string.Format("ControlId <> 13 and ControlId <> 18");
            IQCareUtils theUtils = new IQCareUtils();
            DataTable DT = new DataTable();
            DT = theUtils.CreateTableFromDataView(theDVNew);
            //DisplayType.DataSource = BindSelect(ds.Tables[3]);
            DisplayType.DataSource = BindSelect(DT);
            //

            DisplayType.DisplayMember = "Name";
            DisplayType.ValueMember = "ControlID";
            DisplayType.DataPropertyName = "Name";
            DisplayType.DefaultCellStyle.NullValue = "Select";

            DataGridViewTextBoxColumn fieldname = new DataGridViewTextBoxColumn();
            fieldname.MaxInputLength = 50;

            dgwFieldDetails.Columns.AddRange(
                fieldname,
                DisplayType,
                new DataGridViewTextBoxColumn(),
                new DataGridViewImageColumn(),
                new DataGridViewImageColumn(),
                new DataGridViewImageColumn(),
                new DataGridViewImageColumn(),
                new DataGridViewTextBoxColumn(),
                new DataGridViewTextBoxColumn(),
                new DataGridViewTextBoxColumn(),
                new DataGridViewTextBoxColumn(),
                new DataGridViewTextBoxColumn(),
                new DataGridViewTextBoxColumn(),
                new DataGridViewTextBoxColumn());
            dgwFieldDetails.Columns[0].Name = "Field Name";
            dgwFieldDetails.Columns[0].Width = 300;
            dgwFieldDetails.Columns[1].Name = "Display Type";
            dgwFieldDetails.Columns[1].Width = 150;
            dgwFieldDetails.Columns[2].Name = "Predefined";
            dgwFieldDetails.Columns[2].Width = 100;
            dgwFieldDetails.Columns[3].Name = "List";
            dgwFieldDetails.Columns[3].Width = 50;
            dgwFieldDetails.Columns[4].Name = "Business Rule";
            dgwFieldDetails.Columns[4].Width = 100;
            dgwFieldDetails.Columns[5].Name = "Active";
            dgwFieldDetails.Columns[5].Width = 70;
            dgwFieldDetails.Columns[6].Name = "Associated Fields";
            dgwFieldDetails.Columns[6].Width = 100;
            dgwFieldDetails.Columns[7].Name = "Last Updated";
            dgwFieldDetails.Columns[7].Width = 100;
            dgwFieldDetails.Columns[8].Name = "Updated By";
            dgwFieldDetails.Columns[8].Width = 80;
            dgwFieldDetails.Columns[9].Name = "ID";
            dgwFieldDetails.Columns[10].Name = "ListValue";
            dgwFieldDetails.Columns[8].Visible = false;
            dgwFieldDetails.Columns[9].Visible = false;
            dgwFieldDetails.Columns[10].Visible = false;
            dgwFieldDetails.Columns[2].ReadOnly = true;
            dgwFieldDetails.Columns[6].ReadOnly = true;
            dgwFieldDetails.Columns[8].ReadOnly = true;
            dgwFieldDetails.Columns[11].Visible = false;
            dgwFieldDetails.Columns[12].Visible = false;
            dgwFieldDetails.Columns[12].Name = "ModuleId";
            dgwFieldDetails.Columns[13].Name = "Control Reference Id";
            dgwFieldDetails.Columns[13].Visible = false;

            foreach (DataRow r in dt.Rows)
            {
                dgwFieldDetails.Rows.Add(r["FieldName"].ToString(), r["ControlName"].ToString(), r["Predefined"].ToString(), r["List"].ToString(),
                    r["BussinessRule"].ToString(), r["DeleteFlag"].ToString(), r["AssociatedFields"].ToString(), r["LastUpdate"].ToString(), r["UpdatedBy"].ToString(),
                    r["id"].ToString(), r["ListValue"].ToString(), r["Auto"].ToString(), r["ModuleId"].ToString(), r["ReferenceId"].ToString());
            }

            int RowCount = dt.Rows.Count;
            if (blnRecordSave)
            {
                dgwFieldDetails.CurrentCell = dgwFieldDetails[0, RowCount - 1];
            }
            blnRecordSave = false;
        }

        private void on_dataerror(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        /// <summary>
        /// Thsi function is used to find out predefine and coustom field 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        string GetPredefine(int p)
        {
            string Predefine = string.Empty;
            if (p == 1)
            {
                Predefine = "P";
            }
            else
            {
                Predefine = "C";
            }
            return Predefine;
        }
        /// <summary>
        /// This function is used to find out there is any list is associated with field or not 
        /// </summary>
        /// <param name="fieldID"></param>
        /// <param name="dtlist"></param>
        /// <returns></returns>
        int GetListDetails(int fieldID, DataTable dtlist, int fieldStatus)
        {
            DataView theDV = new DataView(dtlist);
            theDV.RowFilter = "FieldId=" + fieldID.ToString() + " and Predefined=" + fieldStatus.ToString();
            if (theDV.Count > 0)
                return 1;
            else
            {


                return 0;


            }

        }
        /// <summary>
        /// This function is find out the list value for a field
        /// </summary>
        /// <param name="fieldID"></param>
        /// <param name="dtlist"></param>
        /// <returns></returns>
        string GetListValues(int fieldID, DataTable dtlist, int fieldStatus)
        {
            DataView theDV = new DataView(dtlist);
            theDV.RowFilter = "FieldId=" + fieldID.ToString() + " and Predefined=" + fieldStatus.ToString();
            if (theDV.Count > 0)
            {
                return theDV[0]["FieldValue"].ToString();
            }
            else
            {

                return "";

            }

        }
        /// <summary>
        /// This function is used to find out there is any business rules is associated with field or not
        /// </summary>
        /// <param name="fieldID"></param>
        /// <param name="dtbRule"></param>
        /// <returns></returns>
        int GetBussinessRules(int fieldID, DataTable dtbRule, int fieldStatus)
        {
            DataView theDV = new DataView(dtbRule);
            theDV.RowFilter = "FieldId=" + fieldID.ToString() + " and Predefined=" + fieldStatus.ToString();
            if (theDV.Count > 0)
                return 1;
            else
                return 0;
        }
        /// <summary>
        /// this function is used to create datatable on runtime
        /// </summary>
        /// <returns></returns>
        DataTable CreateTable()
        {
            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "id";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "FieldName";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "FieldDesc";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "ControlID";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ControlName";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ReferenceId";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "Predefined";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.ColumnName = "DeleteFlag";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "UpdatedBy";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "LastUpdate";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "List";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "ListValue";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BussinessRule";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "AssociatedFields";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.AutoIncrement = true;
            myDataColumn.AutoIncrementStep = 1;
            myDataColumn.ColumnName = "Auto";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.Int32");
            myDataColumn.AutoIncrement = true;
            myDataColumn.AutoIncrementStep = 1;
            myDataColumn.ColumnName = "ModuleId";
            myDataTable.Columns.Add(myDataColumn);

            return myDataTable;
        }
        /// <summary>
        /// This function is used to create new row in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnadd_Click(object sender, EventArgs e)
        {
            dgwFieldDetails.AllowUserToAddRows = true;
            GblIQCare.dtBusinessValues.Clear();

            int RowCount = dgwFieldDetails.Rows.Count;
            if (RowCount > 0)
            {
                dgwFieldDetails.Rows[RowCount - 1].DefaultCellStyle.BackColor = Color.White;
                dgwFieldDetails.Rows[RowCount - 1].DefaultCellStyle.ForeColor = Color.Black;
            }

            int newrowindex = dgwFieldDetails.Rows.Count - 1;
            dgwFieldDetails.Rows[newrowindex].Cells[9].Value = "0";
            dgwFieldDetails.Rows[newrowindex].Cells[10].Value = "";
            dgwFieldDetails.Rows[newrowindex].Cells[2].Value = "C";
            dgwFieldDetails.Rows[newrowindex].Cells[0].Value = "";
            dgwFieldDetails.CurrentCell = dgwFieldDetails[0, newrowindex];
            if (!(GblIQCare.objhashSelectList.ContainsKey(0)))
            {
                GblIQCare.objhashSelectList.Add(0, "");
            }

            btnadd.Enabled = false;

        }
        /// <summary>
        /// This function is used to close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        /// <summary>
        /// this function is used to clear the grid rows column and value
        /// </summary>
        private void ClearGrid()
        {
            for (int i = 0; i < dgwFieldDetails.Rows.Count; i++)
            {
                dgwFieldDetails.Columns.Clear();
                dgwFieldDetails.Rows.Clear();

            }
        }
        /// <summary>
        /// This functions is used to fill business rules globla table .this table is used on business rules change form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void BindBusinessRulesTable(int index, int fieldId, string thePredefined)
        {
            GblIQCare.dtBusinessValues.Clear();
            GblIQCare.dtBusinessValues.Columns.Clear();
            GblIQCare.dtBusinessValues.Rows.Clear();
            if (fieldId != 0)
            {
                //  IFieldDetail objFieldDetail;
                // objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                // mainDataset = objFieldDetail.GetCustomFields("", Convert.ToInt32(cmbTechArea.SelectedValue), GblIQCare.iManageCareEnded);
                if (mainDataset.Tables[4].Rows.Count > 0)
                {

                    dv = mainDataset.Tables[4].DefaultView;
                    if (thePredefined == "P")
                        dv.RowFilter = "FieldID='" + fieldId + "' and Predefined =1";
                    else
                        dv.RowFilter = "FieldID='" + fieldId + "' and Predefined =0";
                    DataView DvFilter = new DataView();
                    DataTable dt = new DataTable();
                    dt = dv.ToTable();
                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            GblIQCare.dtBusinessValues = dt;
                            if (!(GblIQCare.objhashBusinessRule.ContainsKey(fieldId)))
                            {
                                GblIQCare.objhashBusinessRule.Add(fieldId, dt);
                            }
                            else
                            {
                                GblIQCare.objhashBusinessRule[fieldId] = dt;
                            }
                        }
                    }

                }
            }

        }
        /// <summary>
        /// This function is used when use click on perticular cell in the grid to open new form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwFieldDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // string Displaytype = "";
            gblFieldID = 0;
            gblPredefine = "";
            gblFieldName = "";
            if (e.RowIndex == -1)
            {
                return;
            }
            else
            {
                GblIQCare.gblRowIndex = Convert.ToInt32(dgwFieldDetails.Rows[e.RowIndex].Cells["ID"].Value);
            }
            DataGridViewRow currentRow = dgwFieldDetails.Rows[e.RowIndex];
            string controlReferenceId = string.Empty;

            if (e.RowIndex > -1)
            {
                if (currentRow.Cells["ID"].Value != null)
                {
                    if (currentRow.Cells["ID"].Value.ToString() != "0")
                    {
                        gblFieldID = Convert.ToInt32(currentRow.Cells["ID"].Value);
                        gblPredefine = currentRow.Cells["Predefined"].Value.ToString();
                        gblFieldName = currentRow.Cells["Field Name"].Value.ToString();
                    }
                    if (!(GblIQCare.objhashSelectList.ContainsKey(gblFieldID)))
                    {
                        GblIQCare.objhashSelectList.Add(gblFieldID, currentRow.Cells["ListValue"].Value.ToString());
                        //GblIQCare.strSelectListstr = dgwFieldDetails.Rows[e.RowIndex].Cells[10].Value.ToString();
                    }
                    BindBusinessRulesTable(e.RowIndex, gblFieldID, gblPredefine);
                    IFieldDetail objConditional = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                    DataSet dsFieldDetails = objConditional.GetConditionalFieldsDetails(Convert.ToInt32(currentRow.Cells["ID"].Value), GblIQCare.iManageCareEnded);
                    if (dsFieldDetails.Tables.Count > 0)
                    {
                        DataTable theDTCon = dsFieldDetails.Tables[1];
                        #region "Alter ConditionTable"
                        DataView theDVCon = new DataView(theDTCon);
                        if (currentRow.Cells["Predefined"].Value.ToString() == "C")
                            theDVCon.RowFilter = "ConPredefine =0";
                        else
                            theDVCon.RowFilter = "ConPredefine =1";
                        GblIQCare.dtConditionalFields = theDVCon.ToTable();
                        #endregion
                    }
                }
            }
            if (e.ColumnIndex != -1 && e.RowIndex > -1)
            {

                GblIQCare.strDisplayType = "";
                GblIQCare.strFieldName = "";
                GblIQCare.strSelectFieldName = "";
                GblIQCare.strSelectListValue = "";
                GblIQCare.strPredefinevalue = "";
                GblIQCare.strControlReferenceId = "";
                try
                {
                    controlReferenceId = currentRow.Cells["Control Reference Id"].Value.ToString();
                    GblIQCare.strControlReferenceId = controlReferenceId;
                }
                catch { }
                if (currentRow.Cells["Predefined"].Value != null)
                    GblIQCare.strPredefinevalue = currentRow.Cells["Predefined"].Value.ToString();

                if (dgwFieldDetails.Columns[e.ColumnIndex].Name == "Business Rule")
                {
                    string display = string.Empty;
                    if (currentRow.Cells["Display Type"].Value != null)
                        display = currentRow.Cells["Display Type"].Value.ToString();

                    if (display != "CheckBox" && display != "7" && display.Trim() != "Disease/Symptoms" && display.Trim() != "15" && display.Trim() != "16" && display.Trim() != "ICD10")
                    {

                        if (GblIQCare.objHashtbl.Count == 0)
                        {
                            if (currentRow.Cells["ID"].Value != null)
                                GblIQCare.objHashtbl.Add(currentRow.Cells["ID"].Value, currentRow.Cells[e.ColumnIndex].Value);
                            if (currentRow.Cells["Display Type"].Value != null)
                                GblIQCare.strDisplayType = GetControlName(currentRow.Cells["Display Type"].Value.ToString());
                            if (currentRow.Cells["Field Name"].Value != null)
                                GblIQCare.strFieldName = currentRow.Cells["Field Name"].Value.ToString();
                            if (currentRow.Cells["ID"].Value != null)
                            {
                                if (!(GblIQCare.objhashSelectList.ContainsKey(currentRow.Cells["ID"].Value)))
                                {
                                    GblIQCare.objhashSelectList.Add(currentRow.Cells["ID"].Value, currentRow.Cells["ListValue"].Value.ToString());
                                }
                                //BindBusinessRulesTable(e.RowIndex, Convert.ToInt32(currentRow.Cells[9].Value),currentRow.Cells[2].Value.ToString());
                            }
                            else
                            {
                                if (!(GblIQCare.objhashBusinessRule.ContainsKey(currentRow.Cells["ID"].Value)))
                                {
                                    DataTable dtEmptyDataTable = new DataTable();
                                    GblIQCare.objhashBusinessRule.Add(currentRow.Cells["ID"].Value, dtEmptyDataTable);
                                }
                            }
                            GblIQCare.iFormMode = 0;
                            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmBusinessRule, IQCare.FormBuilder"));
                            theForm.Left = 0;
                            theForm.Top = 0;
                            theForm.Show();
                        }

                    }
                    if (display.Trim() == "Disease/Symptoms" || display.Trim() == "15" || display.Trim() == "ICD10" || display.Trim() == "16")
                    {
                        IQCareWindowMsgBox.ShowWindow("NoBusinessRule", this);
                    }

                }
                else if (dgwFieldDetails.Columns[e.ColumnIndex].Name == "List" || dgwFieldDetails.Columns[e.ColumnIndex].Name == "Associated Fields")
                {
                    string display = string.Empty;

                    if (currentRow.Cells["Display Type"].Value != null)
                    {
                        display = currentRow.Cells["Display Type"].Value.ToString();
                        GblIQCare.strYesNo = display;
                    }

                    if (display.Trim() == "Select List" || display == "4" || display.Trim() == "Multi Select" || display == "9" || display.Trim() == "Yes No" || display == "6" || controlReferenceId == "SELECTLIST_TEXTBOX")
                    {
                        GblIQCare.strSelectList = "frmFieldDetails";
                        GblIQCare.strMainGrdFldName = currentRow.Cells["Field Name"].Value.ToString();
                        GblIQCare.iFieldId = Convert.ToInt32(currentRow.Cells["ID"].Value);
                        GblIQCare.iModuleId = Convert.ToInt32(currentRow.Cells["ModuleId"].Value);
                        GblIQCare.strgblPredefined = gblPredefine;
                        //GblIQCare.strFieldIdcareend = "2";
                        if (dgwFieldDetails.Columns[e.ColumnIndex].Name == "Associated Fields")
                        {

                            GblIQCare.iFormMode = 1;
                            GblIQCare.iConditionalbtn = 0;

                            if (GblIQCare.iFieldId == 167)
                            {
                                GblIQCare.iConditionalbtn = 1;
                            }
                            else
                            {
                                GblIQCare.iConditionalbtn = 0;

                            }
                        }
                        else
                        {
                            GblIQCare.iFormMode = 0;
                            GblIQCare.iConditionalbtn = 1;
                        }

                        if (GblIQCare.objHashtbl.Count == 0)
                        {
                            if (currentRow.Cells["ID"].Value != null)
                            {
                                GblIQCare.objHashtbl.Add(currentRow.Cells["ID"].Value, currentRow.Cells[e.ColumnIndex].Value);
                            }
                            if (currentRow.Cells["Field Name"].Value != null)
                            {
                                GblIQCare.strSelectFieldName = "select";
                            }
                            if (currentRow.Cells["ID"].Value != null)
                            {
                                if (!(GblIQCare.objhashSelectList.ContainsKey(currentRow.Cells["ID"].Value)))
                                {
                                    if (currentRow.Cells["ListValue"].Value != null)
                                    {
                                        GblIQCare.objhashSelectList.Add(currentRow.Cells["ID"].Value, currentRow.Cells["ListValue"].Value.ToString());
                                        GblIQCare.strSelectListValue = currentRow.Cells["ListValue"].Value.ToString();
                                    }
                                    else
                                    {
                                        GblIQCare.objhashSelectList.Add(currentRow.Cells["ID"].Value, "");
                                        GblIQCare.strSelectListValue = "";
                                    }
                                }

                            }
                            else
                            {
                                if (!(GblIQCare.objhashSelectList.ContainsKey(e.RowIndex)))
                                {
                                    GblIQCare.objhashSelectList.Add(currentRow.Cells["ID"].Value, "");
                                }
                            }
                            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmSelectList, IQCare.FormBuilder"));
                            theForm.Left = 0;
                            theForm.Top = 0;
                            theForm.Show();
                        }
                    }
                    else if (display.Trim() == "ICD10" || display == "16")
                    {
                        if (dgwFieldDetails.Columns[e.ColumnIndex].Name == "List")
                        {
                            GblIQCare.iFieldId = Convert.ToInt32(currentRow.Cells["ID"].Value);
                            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmICD10Selector, IQCare.FormBuilder"));
                            theForm.Left = 0;
                            theForm.Top = 0;
                            theForm.ShowDialog(this);
                        }

                    }
                }
                else if (dgwFieldDetails.Columns[e.ColumnIndex].Name == "Active")
                {
                    string value = string.Empty;
                    string Predefinevalue = string.Empty;
                    if (currentRow.Cells[e.ColumnIndex].Value != null)
                    {
                        value = currentRow.Cells[e.ColumnIndex].Value.ToString();
                    }
                    if (currentRow.Cells["Predefined"].Value != null)
                    {
                        Predefinevalue = currentRow.Cells["Predefined"].Value.ToString();
                    }
                    if (Predefinevalue.ToString() != "P")
                    {
                        if (value.ToString() == "0")
                        {
                            DialogResult strResult;
                            strResult = IQCareWindowMsgBox.ShowWindowConfirm("PMTCTInactivefield", this);
                            if (strResult.ToString() == "Yes")
                            {
                                RemoveHastTable();
                                GblIQCare.objHashtbl.Add(currentRow.Cells["ID"].Value, currentRow.Cells[e.ColumnIndex].Value);
                                currentRow.Cells[e.ColumnIndex].Value = 1;
                                SaveUpdateActive();
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (value.ToString() == "1")
                        {
                            DialogResult strResult;

                            strResult = IQCareWindowMsgBox.ShowWindowConfirm("PMTCTActivefield", this);
                            if (strResult.ToString() == "Yes")
                            {
                                RemoveHastTable();
                                GblIQCare.objHashtbl.Add(currentRow.Cells["ID"].Value, currentRow.Cells[e.ColumnIndex].Value);
                                currentRow.Cells[e.ColumnIndex].Value = 0;
                                SaveUpdateActive();
                            }
                            else
                            {
                                return;
                            }
                        }

                    }

                }
            }
        }
        /// <summary>
        /// This function is used to formatting grid cells value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwFieldDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

            if (e.ColumnIndex != -1 && e.RowIndex > -1)
            {
                string display = string.Empty;
                string controlReferenceId = string.Empty;
                DataGridViewRow currentRow = dgwFieldDetails.Rows[e.RowIndex];
                DataGridViewColumn currentColumn = dgwFieldDetails.Columns[e.ColumnIndex];
                if (currentRow.Cells["Display Type"].Value != null)
                {
                    display = currentRow.Cells["Display Type"].Value.ToString();
                }
                if (currentRow.Cells["Control Reference Id"].Value != null)
                {
                    controlReferenceId = currentRow.Cells["Control Reference Id"].Value.ToString();
                }
                if (dgwFieldDetails.Columns[e.ColumnIndex].Name == "List")
                {

                    //if (dgwFieldDetails.Rows[e.RowIndex].Cells[1].Value != null)   

                    if (e.Value != null && e.Value.ToString() == "1")
                    {
                        if (controlReferenceId.Trim() == "SELECT_LIST" || controlReferenceId.Trim() == "SELECT_LIST_MULTI" || controlReferenceId.Trim() == "SELECTLIST_TEXTBOX")
                        {
                            e.Value = img1;
                        }
                        else
                        {
                            e.Value = img4;
                        }
                    }
                    else if (display.Trim() == "ICD10")
                    {
                        if (findICD10List(Convert.ToInt32(currentRow.Cells["ID"].Value)))
                        {
                            e.Value = img1;
                        }
                        else
                        {
                            e.Value = img4;
                        }

                    }
                    else
                    {
                        e.Value = img4;
                    }
                }
                else if (currentColumn.Name == "Business Rule")
                {
                    if (e.Value != null && e.Value.ToString() == "1")
                    {
                        e.Value = img2;
                    }
                    else
                    {
                        e.Value = img3;
                    }
                }
                else if (currentColumn.Name == "Active")
                {
                    if (e.Value != null && e.Value.ToString() == "0")
                    {
                        e.Value = img2;
                    }
                    else
                    {
                        e.Value = img3;
                    }
                }
                else if (currentColumn.Name == "Associated Fields")
                {
                    //string display = string.Empty;
                    //if (dgwFieldDetails.Rows[e.RowIndex].Cells[1].Value != null)
                    //{
                    //    display = dgwFieldDetails.Rows[e.RowIndex].Cells[1].Value.ToString();
                    //}
                    if (e.Value != null && e.Value.ToString() != "0")
                    {
                        // if (display.Trim() == "Select List" || display.Trim() == "Multi Select" || display.Trim() == "Yes No")
                        if (controlReferenceId.Trim() == "SELECT_LIST" || controlReferenceId.Trim() == "SELECT_LIST_MULTI" || controlReferenceId.Trim() == "SELECTLIST_TEXTBOX" || controlReferenceId.Trim() == "YES_NO")
                        {
                            e.Value = img5;
                        }
                        else
                            e.Value = img6;
                    }
                    else
                    {
                        e.Value = img6;
                    }
                    //if (Convert.ToInt32(e.Value) > 0)
                    //     e.Value = img5;
                    // else
                    //     e.Value = img6;


                }

                else if (currentColumn.Name == "Predefined")
                {
                    if (e.Value != null)
                    {
                        if (e.Value.ToString() == "P")
                        {
                            currentRow.Cells["Field Name"].ReadOnly = true;
                            currentRow.Cells["Display Type"].ReadOnly = true;
                            currentRow.Cells["Active"].ReadOnly = true;
                        }
                        else if (e.Value.ToString() == "C")
                        {
                            // DataView theDV = new DataView(mainDataset.Tables[6]);
                            //  dgwFieldDetails.Columns[6].Name = "Associated Fields";
                            try
                            {
                                if (currentRow.Cells["Associated Fields"].Value != null && Convert.ToInt32(currentRow.Cells["Associated Fields"].Value) > 0)
                                {
                                    // theDV.RowFilter = "LnkFrmFieldID=" + currentRow.Cells["ID"].Value;
                                    // if (theDV.ToTable().Rows.Count >= 1)
                                    // {
                                    currentRow.Cells["Field Name"].ReadOnly = true;
                                    currentRow.Cells["Display Type"].ReadOnly = true;
                                    currentRow.Cells["Active"].ReadOnly = true;
                                    // }
                                }
                            }
                            catch { }
                        }
                    }
                }
            }

        }

        public bool findICD10List(int fieldid)
        {
            bool icd10 = false;

            IFieldDetail objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
            DataSet theDSTV = objFieldDetail.GetICD10Values(fieldid);
            if (theDSTV.Tables[0].Rows.Count > 0)
            {
                icd10 = true;
            }
            return icd10;
        }
        /// <summary>
        /// This function is used to track error on the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwFieldDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        /// <summary>
        /// This function is used when user click on cells
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwFieldDetails_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {


                if (GblIQCare.objHashtbl.Count != 0)
                {
                    ArrayList arrayList = new ArrayList(GblIQCare.objHashtbl.Keys);
                    if (arrayList.Count > 0)
                    {
                        if (dgwFieldDetails.Rows[e.RowIndex].Cells["ID"].Value != null)
                        {
                            if (arrayList[0].ToString() != dgwFieldDetails.Rows[e.RowIndex].Cells["ID"].Value.ToString())
                            {
                                //SaveUpdateMessage();
                            }
                        }
                    }
                }

                for (int i = 0; i < dgwFieldDetails.Rows.Count; i++)
                {
                    if (i == e.RowIndex)
                    {

                        dgwFieldDetails.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                        dgwFieldDetails.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {

                        dgwFieldDetails.Rows[i].DefaultCellStyle.BackColor = this.BackColor; ;
                        dgwFieldDetails.Rows[i].DefaultCellStyle.ForeColor = SystemColors.GrayText;

                    }
                }
            }
        }
        /// <summary>
        /// This function is used when user try to change cells value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgwFieldDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                if (GblIQCare.objHashtbl.Count == 0)
                {
                    if (dgwFieldDetails.Columns[e.ColumnIndex].Name == "Field Name")
                    {
                        blnFieldNameChange = true;
                        int _id = Convert.ToInt32(dgwFieldDetails.Rows[e.RowIndex].Cells["ID"].Value);
                        string Value = dgwFieldDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                        if (_id > 0)
                        {
                            GblIQCare.objHashtbl.Add(_id, Value);
                            blnNameChange = true;
                        }
                    }
                    else if (dgwFieldDetails.Columns[e.ColumnIndex].Name == "Display Type")
                    {
                        string _id = Convert.ToString(dgwFieldDetails.Rows[e.RowIndex].Cells["ID"].Value);
                        blnDisplayTypeChange = true;
                        if (!string.IsNullOrEmpty(_id))
                        {
                            object List = dgwFieldDetails.Rows[e.RowIndex].Cells["List"].Value;
                            object brule = dgwFieldDetails.Rows[e.RowIndex].Cells["Business Rule"].Value;
                            string DisplayType = Convert.ToString(dgwFieldDetails.Rows[e.RowIndex].Cells["Display Type"].Value);
                            DataRow[] rows = mainDataset.Tables[3].Select("ControlID = " + DisplayType);

                            string controlReferenceId = rows[0]["ReferenceId"].ToString(); ;

                            dgwFieldDetails.Rows[e.RowIndex].Cells["Control Reference Id"].Value = controlReferenceId;
                            string predefine = Convert.ToString(dgwFieldDetails.Rows[e.RowIndex].Cells["Predefined"].Value);
                            int predefinestatus = 0;
                            if (predefine == "P")
                            {
                                predefinestatus = 1;
                            }
                            else
                            {
                                predefinestatus = 0;
                            }

                            string fieldname = Convert.ToString(dgwFieldDetails.Rows[e.RowIndex].Cells["Field Name"].Value);
                            if (_id != "0")
                            {
                                if (List.ToString() == "1" || brule.ToString() == "1")
                                {
                                    bool blnChange = ChangeDisplayType(Convert.ToInt32(_id), 9, Convert.ToInt32(GetControlID(DisplayType)), predefinestatus, fieldname);
                                    if (blnChange)
                                    {
                                        blnDisplayType = true;
                                        if (dgwFieldDetails.Rows[e.RowIndex].Cells["ID"].Value != null)
                                            GblIQCare.objHashtbl.Add(dgwFieldDetails.Rows[e.RowIndex].Cells["ID"].Value, dgwFieldDetails.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                                        ChangeDataTypeSaveUpdate();
                                        BindGrid("", Convert.ToInt32(cmbTechArea.SelectedValue));
                                    }
                                    else
                                    {
                                        blnDisplayType = false;
                                        BindGrid("", Convert.ToInt32(cmbTechArea.SelectedValue));
                                    }
                                }
                                else
                                {
                                    blnDisplayType = true;
                                }
                            }
                        }
                        else
                        {
                            blnDisplayType = true;
                        }

                    }

                }
                else
                {
                    //SaveUpdateMessage();
                }
            }
        }
        /// <summary>
        /// This function is used to find out control id 
        /// </summary>
        /// <param name="Controltext"></param>
        /// <returns></returns>
        string GetControlID(string Controltext)
        {
            string controlId = "0";
            foreach (DataRow r in mainDataset.Tables[3].Rows)
            {
                if (r["Name"].ToString() == Controltext)
                {
                    controlId = r["ControlId"].ToString();
                }
                else if (r["ControlId"].ToString() == Controltext)
                {
                    controlId = r["ControlId"].ToString();
                }


            }
            return controlId;
        }
        /// <summary>
        /// This function is used to get control name
        /// </summary>
        /// <param name="Controltext"></param>
        /// <returns></returns>
        string GetControlName(string Controltext)
        {
            string controlName = string.Empty;
            foreach (DataRow r in mainDataset.Tables[3].Rows)
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
        /// <summary>
        /// This function is used when user try to change display type inside the grid
        /// </summary>
        /// <param name="id"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool ChangeDisplayType(int id, int flag, int controlId, int predefinestatus, string fieldName)
        {
            IFieldDetail objFieldDetail;

            objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
            DialogResult strResult;
            strResult = IQCareWindowMsgBox.ShowWindowConfirm("PMTCTChangeDisplayType", this);
            if (strResult.ToString() == "Yes")
            {
                int rowstate = objFieldDetail.ResetCustomFieldRules(id, flag, predefinestatus, fieldName);
                RemoveHastTable();
                return true;
            }
            else
            {
                RemoveHastTable();
                return false;
            }
            // return true;
        }
        /// <summary>
        /// This function is used to update the record in the grid
        /// </summary>
        /// 

        public void SaveUpdateMessage()
        {
            int intRowIndex = 0;
            GblIQCare.strRetainSelectList = "";
            GblIQCare.strRetainSelectField = "";
            int iModuleId;

            if (GblIQCare.blhselectlistChange || GblIQCare.blnBusinessRuleChange || blnDisplayType || blnFieldNameChange)
            {
                DialogResult strResult;
                strResult = IQCareWindowMsgBox.ShowWindowConfirm("PMTCTeditRecord", this);
                if (strResult.ToString() == "Yes")
                {

                    foreach (DataGridViewRow r in dgwFieldDetails.Rows)
                    {
                        IFieldDetail objFieldDetail;

                        objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                        string str = Convert.ToString(r.Cells[9].Value);
                        foreach (string key in GblIQCare.objHashtbl.Keys)
                            if (str == key)
                            {
                                if (r.Cells[0].Value != null)
                                {
                                    string strListValue = "";
                                    intRowIndex = dgwFieldDetails.CurrentRow.Index;
                                    string id = Convert.ToString(r.Cells[9].Value);
                                    string strname = r.Cells[0].Value.ToString();
                                    string strcontrolid = GetControlID(r.Cells[1].Value.ToString());
                                    int predefinestatus = 0;
                                    string predefine = r.Cells[2].Value.ToString();
                                    if (predefine == "P")
                                    {
                                        predefinestatus = 1;
                                    }
                                    else
                                    {
                                        predefinestatus = 0;
                                    }
                                    if (GblIQCare.hashTblSelectList.Count > 0)
                                    {
                                        strListValue = GblIQCare.hashTblSelectList["select"].ToString();
                                    }
                                    int Status = Convert.ToInt32(r.Cells[5].Value.ToString());
                                    if (Status == 0)
                                    {
                                        if (strname != "")
                                        {
                                            DataSet objDsDuplicate = new DataSet();
                                            if (r.Cells[11].Value != null)
                                            {
                                                iModuleId = Convert.ToInt32(r.Cells[11].Value.ToString());
                                            }
                                            else
                                                iModuleId = 0;
                                            objDsDuplicate = objFieldDetail.GetDuplicateCustomFields(Convert.ToInt32(id), strname, iModuleId, GblIQCare.iManageCareEnded);
                                            if (objDsDuplicate.Tables[0].Rows.Count > 0)
                                            {
                                                IQCareWindowMsgBox.ShowWindow("PMTCTduplicatefield", this);
                                            }
                                            else
                                            {
                                                int RowSave;
                                                if (GblIQCare.iManageCareEnded == 1)
                                                    RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), Status, 1, 1, 1, strListValue, GblIQCare.dtBusinessValues, predefinestatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);
                                                else
                                                    RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), Status, 1, 0, 1, strListValue, GblIQCare.dtBusinessValues, predefinestatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);

                                                IQCareWindowMsgBox.ShowWindow("PMTCTCustomfieldsave", this);
                                            }

                                        }
                                        else
                                        {
                                            IQCareWindowMsgBox.ShowWindow("PMTCTFieldName", this);
                                        }
                                    }
                                    else
                                    {
                                        IQCareWindowMsgBox.ShowWindow("PMTCTsaveinactivefield", this);
                                    }
                                }
                            }

                    }
                    GblIQCare.blnFieldDetailsChange = false;
                    BindGrid("", Convert.ToInt32(cmbTechArea.SelectedValue));
                    RemoveHastTable();
                    GblIQCare.hashTblSelectList.Clear();
                }
                else
                {

                    RemoveHastTable();
                    GblIQCare.blnFieldDetailsChange = false;
                    GblIQCare.hashTblSelectList.Clear();
                    BindGrid("", Convert.ToInt32(cmbTechArea.SelectedValue));

                }
            }
            if (intRowIndex > 0)
            {
                //dgwFieldDetails.CurrentCell = dgwFieldDetails[0, intRowIndex];
            }
            GblIQCare.blhselectlistChange = false;
            GblIQCare.blnBusinessRuleChange = false;
            blnDisplayType = false;
            blnFieldNameChange = false;
        }
        /// <summary>
        /// This function is used to activate/inactivate custom field in the system
        /// </summary>
        public void SaveUpdateActive()
        {
            // int iModuleId;
            foreach (DataGridViewRow r in dgwFieldDetails.Rows)
            {
                IFieldDetail objFieldDetail;
                int RowSave = 0;
                objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                string str = Convert.ToString(r.Cells[9].Value);
                string strfieldName = Convert.ToString(r.Cells[0].Value);
                foreach (string key in GblIQCare.objHashtbl.Keys)
                    if (str == key && strfieldName == gblFieldName)
                    {
                        if (r.Cells[0].Value != null)
                        {
                            string strListValue = "";
                            string id = Convert.ToString(r.Cells[9].Value);
                            string predefine = r.Cells[2].Value.ToString();
                            BindBusinessRulesTable(r.Index, Convert.ToInt32(id), r.Cells[2].Value.ToString());
                            string strname = r.Cells[0].Value.ToString();
                            string strcontrolid = GetControlID(r.Cells[1].Value.ToString());
                            if (GblIQCare.hashTblSelectList.Count > 0)
                            {
                                strListValue = GblIQCare.hashTblSelectList["select"].ToString();

                            }
                            else
                            {
                                strListValue = Convert.ToString(r.Cells[10].Value);
                            }
                            if (predefine != "P")
                            {
                                if (!fnValidate(Convert.ToInt32(strcontrolid), strListValue))
                                {
                                    IQCareWindowMsgBox.ShowWindow("PMTCTSelectlistvalidate", this);
                                    return;
                                }
                            }
                            int predefinestatus = 0;
                            //string predefine = r.Cells[2].Value.ToString();
                            if (predefine == "P")
                            {
                                predefinestatus = 1;
                            }
                            else
                            {
                                predefinestatus = 0;
                            }
                            int Status = Convert.ToInt32(r.Cells[5].Value.ToString());
                            if (Status == 1)
                            {
                                if (predefine == "P")
                                {
                                    DataSet DsPredefine = new DataSet();
                                    DsPredefine = objFieldDetail.CheckPredefineField(Convert.ToInt32(id));
                                    if (DsPredefine.Tables[0].Rows.Count == 0)
                                    {

                                        if (GblIQCare.iManageCareEnded == 1)
                                            RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), Status, GblIQCare.AppUserId, 1, 4, strListValue, GblIQCare.dtBusinessValues, predefinestatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);
                                        else
                                            RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), Status, GblIQCare.AppUserId, 0, 4, strListValue, GblIQCare.dtBusinessValues, predefinestatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);

                                        IQCareWindowMsgBox.ShowWindow("PMTCTCustomfieldsave", this);
                                    }
                                    else
                                    {
                                        IQCareWindowMsgBox.ShowWindow("PMTCTFieldInactive", this);
                                    }
                                }
                                else
                                {
                                    DataSet DsCustom = new DataSet();
                                    DsCustom = objFieldDetail.CheckCustomFields(Convert.ToInt32(id));
                                    if (DsCustom.Tables[0].Rows.Count == 0)
                                    {

                                        if (GblIQCare.iManageCareEnded == 1)
                                            RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), Status, GblIQCare.AppUserId, 1, 4, strListValue, GblIQCare.dtBusinessValues, predefinestatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);
                                        else
                                            RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), Status, GblIQCare.AppUserId, 0, 4, strListValue, GblIQCare.dtBusinessValues, predefinestatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);

                                        IQCareWindowMsgBox.ShowWindow("PMTCTCustomfieldsave", this);
                                    }
                                    else
                                    {

                                        IQCareWindowMsgBox.ShowWindow("PMTCTFieldInactive", this);
                                    }
                                }
                            }
                            else
                            {

                                if (GblIQCare.iManageCareEnded == 1)
                                    RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), Status, GblIQCare.AppUserId, 1, 4, strListValue, GblIQCare.dtBusinessValues, predefinestatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);
                                else
                                    RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), Status, GblIQCare.AppUserId, 0, 4, strListValue, GblIQCare.dtBusinessValues, predefinestatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);
                                //RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), Status, GblIQCare.AppUserId, 4, strListValue, GblIQCare.dtBusinessValues, predefinestatus, GblIQCare.SystemId);
                                IQCareWindowMsgBox.ShowWindow("PMTCTCustomfieldsave", this);
                            }
                        }
                    }
            }
            gblFieldName = "";
            BindGrid("", Convert.ToInt32(cmbTechArea.SelectedValue));
            RemoveHastTable();
        }
        /// <summary>
        /// This function is used to remove hash table
        /// </summary>
        public void RemoveHastTable()
        {
            blnDisplayTypeChange = false;

        }
        /// <summary>
        /// This function is used to validate data entery in the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void ChangeDataTypeSaveUpdate()
        {
            int intRowIndex = 0;
            GblIQCare.strRetainSelectList = "";
            GblIQCare.strRetainSelectField = "";
            int iModuleId;
            if (GblIQCare.blhselectlistChange || GblIQCare.blnBusinessRuleChange || blnDisplayType || blnFieldNameChange)
            {
                GblIQCare.dtBusinessValues.Clear();
                GblIQCare.dtBusinessValues.Columns.Clear();
                GblIQCare.dtBusinessValues.Rows.Clear();
                string strcontrolid;
                foreach (DataGridViewRow r in dgwFieldDetails.Rows)
                {
                    IFieldDetail objFieldDetail;

                    objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                    string str = Convert.ToString(r.Cells["ID"].Value);
                    foreach (string key in GblIQCare.objHashtbl.Keys)
                        if (str == key)
                        {
                            if (r.Cells["Field Name"].Value != null)
                            {
                                string strListValue = "";
                                intRowIndex = dgwFieldDetails.CurrentRow.Index;
                                string id = Convert.ToString(r.Cells["ID"].Value);
                                string strname = r.Cells["Field Name"].Value.ToString();
                                strcontrolid = GetControlID(Convert.ToString(r.Cells["Display Type"].Value));

                                int predefinestatus = 0;
                                string predefine = r.Cells["Predefined"].Value.ToString();
                                if (predefine == "P")
                                {
                                    predefinestatus = 1;
                                }
                                else
                                {
                                    predefinestatus = 0;
                                }
                                if (GblIQCare.hashTblSelectList.Count > 0)
                                {
                                    strListValue = GblIQCare.hashTblSelectList["select"].ToString();
                                }
                                if (predefine != "P")
                                {
                                    if (!fnValidate(Convert.ToInt32(strcontrolid), strListValue))
                                    {
                                        IQCareWindowMsgBox.ShowWindow("PMTCTSelectlistvalidate", this);
                                        return;
                                    }
                                }
                                int Status = Convert.ToInt32(r.Cells["Active"].Value.ToString());
                                if (Status == 0)
                                {
                                    if (strname != "")
                                    {
                                        DataSet objDsDuplicate = new DataSet();
                                        if (r.Cells["ModuleId"].Value != null)
                                        {
                                            iModuleId = Convert.ToInt32(r.Cells["ModuleId"].Value.ToString());
                                        }
                                        else
                                            iModuleId = 0;
                                        objDsDuplicate = objFieldDetail.GetDuplicateCustomFields(Convert.ToInt32(id), strname, iModuleId, GblIQCare.iManageCareEnded);
                                        if (objDsDuplicate.Tables[0].Rows.Count > 0)
                                        {
                                            IQCareWindowMsgBox.ShowWindow("PMTCTduplicatefield", this);
                                        }
                                        else
                                        {
                                            int RowSave;
                                            if (GblIQCare.iManageCareEnded == 1)
                                                RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), Status, 1, 1, 1, "", GblIQCare.dtBusinessValues, predefinestatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);
                                            else
                                                RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), Status, 1, 0, 1, "", GblIQCare.dtBusinessValues, predefinestatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);

                                        }

                                    }
                                    else
                                    {
                                        IQCareWindowMsgBox.ShowWindow("PMTCTFieldName", this);
                                    }
                                }
                                else
                                {
                                    IQCareWindowMsgBox.ShowWindow("PMTCTsaveinactivefield", this);
                                }
                            }
                        }

                }
                GblIQCare.blnFieldDetailsChange = false;
                BindGrid("", Convert.ToInt32(cmbTechArea.SelectedValue));
                RemoveHastTable();
                GblIQCare.hashTblSelectList.Clear();

            }
            if (intRowIndex > 0)
            {
                //dgwFieldDetails.CurrentCell = dgwFieldDetails[0, intRowIndex];
            }
            GblIQCare.blhselectlistChange = false;
            GblIQCare.blnBusinessRuleChange = false;
            blnDisplayType = false;
            blnFieldNameChange = false;
        }
        /// <summary>
        /// This function is used to save new record in the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsave_Click(object sender, EventArgs e)
        {
            bool blnSave = true;
            GblIQCare.strRetainSelectList = "";
            GblIQCare.strRetainSelectField = "";
            GblIQCare.dtTempValue.Clear();
            GblIQCare.strTempName = "";
            GblIQCare.strCount = "";
            int iModuleId;
            foreach (DataGridViewRow r in dgwFieldDetails.Rows)
            {
                IFieldDetail objFieldDetail;
                objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                int str = Convert.ToInt32(r.Cells["ID"].Value);
                string strfieldName = Convert.ToString(r.Cells["Field Name"].Value);
                string strControlType = Convert.ToString(r.Cells["Display Type"].Value);
                string strPredefineStatus = Convert.ToString(r.Cells["Predefined"].Value);
                if (str == 0)
                {
                    if (r.Cells[0].Value != null)
                    {
                        string strListValue = "";
                        int id = Convert.ToInt32(r.Cells["ID"].Value);
                        string predefine = Convert.ToString(r.Cells["Predefined"].Value);
                        string strname = r.Cells["Field Name"].Value.ToString();
                        DataTable dtBusinessRule = new DataTable();
                        if (GblIQCare.objhashBusinessRule.Contains(id))
                        {
                            if (GblIQCare.objhashBusinessRule[id].ToString() != "" || ((DataTable)GblIQCare.objhashBusinessRule[id]).Rows.Count != 0)
                            {
                                dtBusinessRule = (DataTable)(GblIQCare.objhashBusinessRule[id]);
                            }
                        }
                        if (GblIQCare.objhashSelectList.Contains(id))
                        {
                            strListValue = GblIQCare.objhashSelectList[id].ToString();
                        }
                        string strcontrolid = Convert.ToString(r.Cells["Display Type"].Value);
                        if (strname != "" && strControlType != "" && strControlType != "Select")
                        {
                            if (!fnValidateFieldName(strname))
                            {
                                IQCareWindowMsgBox.ShowWindow("PMTCTFieldNamevalid", this);
                                return;
                            }
                            if (predefine != "P")
                            {
                                if (!fnValidate(Convert.ToInt32(strcontrolid), strListValue))
                                {
                                    IQCareWindowMsgBox.ShowWindow("PMTCTSelectlistvalidate", this);
                                    return;
                                }
                            }

                            DataSet objDsDuplicate = new DataSet();
                            if (r.Cells["ModuleId"].Value != null)
                            {
                                iModuleId = Convert.ToInt32(r.Cells["ModuleId"].Value.ToString());
                            }
                            else
                                iModuleId = 0;
                            objDsDuplicate = objFieldDetail.GetDuplicateCustomFields(0, strname, iModuleId, GblIQCare.iManageCareEnded);
                            if (objDsDuplicate.Tables[0].Rows.Count > 0)
                            {
                                IQCareWindowMsgBox.ShowWindow("PMTCTduplicatefield", this);
                            }
                            else
                            {
                                if (strListValue != "")
                                {
                                    XDocument xmlDoc = new XDocument();
                                    XElement xElm = new XElement("selectlist",
                                                        from l in strListValue.Split(';')
                                                        select new XElement("option", l)
                                                    );
                                    xmlDoc.Add(xElm);

                                    strListValue = xmlDoc.ToString();
                                }
                                int RowSave;
                                if (GblIQCare.iManageCareEnded == 1)
                                    RowSave = objFieldDetail.SaveUpdateCusomtField(0, strname, Convert.ToInt32(strcontrolid), 0, GblIQCare.AppUserId, 1, 3, strListValue, dtBusinessRule, 0, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);
                                else if (GblIQCare.iManageCareEnded == 2)
                                    RowSave = objFieldDetail.SaveUpdateCusomtField(0, strname, Convert.ToInt32(strcontrolid), 0, GblIQCare.AppUserId, 2, 2, strListValue, dtBusinessRule, 0, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);
                                else
                                    RowSave = objFieldDetail.SaveUpdateCusomtField(0, strname, Convert.ToInt32(strcontrolid), 0, GblIQCare.AppUserId, 0, 0, strListValue, dtBusinessRule, 0, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);
                                blnRecordSave = true;
                                IQCareWindowMsgBox.ShowWindow("PMTCTCustomfieldsave", this);
                            }
                        }
                        else if (strname == "")
                        {
                            IQCareWindowMsgBox.ShowWindow("PMTCTFieldName", this);
                            GblIQCare.hashTblSelectList.Clear();
                        }
                        else
                        {
                            IQCareWindowMsgBox.ShowWindow("PMTCTDisplayType", this);
                        }
                    }
                }
                else
                {
                    if (blnNameChange)
                    {
                        if (Convert.ToInt32(str) == gblFieldID && strPredefineStatus == "C")
                        {
                            if (r.Cells[0].Value != null)
                            {
                                string strListValue = "";
                                int id = Convert.ToInt32(r.Cells[9].Value);
                                string strname = Convert.ToString(r.Cells[0].Value);
                                string strcontrolid = GetControlID(r.Cells[1].Value.ToString());

                                string strList = Convert.ToString(r.Cells[3].Value);
                                string strbrule = Convert.ToString(r.Cells[4].Value);
                                int predefineStatus = 0;
                                string predefine = Convert.ToString(r.Cells[2].Value);
                                DataTable dtBusinessRule = new DataTable();
                                if (GblIQCare.objhashBusinessRule.Contains(id))
                                {
                                    dtBusinessRule = (DataTable)(GblIQCare.objhashBusinessRule[id]);
                                }
                                if (predefine == "P")
                                {
                                    predefineStatus = 1;
                                }
                                else
                                {
                                    predefineStatus = 0;
                                }
                                int Status = Convert.ToInt32(r.Cells[5].Value.ToString());
                                if (Status == 0)
                                {
                                    if (blnDisplayTypeChange)
                                    {
                                        if (strList == "1" || strbrule == "1")
                                        {

                                            bool blnChange = ChangeDisplayType(Convert.ToInt32(id), 9, Convert.ToInt32(strcontrolid), predefineStatus, strname);
                                            blnSave = blnChange;
                                        }
                                    }
                                    if (GblIQCare.objhashSelectList.Contains(id))
                                    {
                                        strListValue = GblIQCare.objhashSelectList[id].ToString();
                                    }

                                    if (predefine != "P")
                                    {
                                        if (!fnValidate(Convert.ToInt32(strcontrolid), strListValue))
                                        {
                                            IQCareWindowMsgBox.ShowWindow("PMTCTSelectlistvalidate", this);
                                            return;
                                        }
                                    }


                                    if (strname != "")
                                    {
                                        if (blnSave)
                                        {
                                            if (!fnValidateFieldName(strname))
                                            {
                                                IQCareWindowMsgBox.ShowWindow("PMTCTFieldNamevalid", this);
                                                return;
                                            }
                                            DataSet objDsDuplicate = new DataSet();
                                            if (r.Cells[11].Value.ToString() != null)
                                            {
                                                iModuleId = Convert.ToInt32(r.Cells[11].Value.ToString());
                                            }
                                            else
                                                iModuleId = 0;
                                            objDsDuplicate = objFieldDetail.GetDuplicateCustomFields(Convert.ToInt32(id), strname, iModuleId, GblIQCare.iManageCareEnded);
                                            if (objDsDuplicate.Tables[0].Rows.Count > 0)
                                            {
                                                IQCareWindowMsgBox.ShowWindow("PMTCTduplicatefield", this);
                                            }
                                            else
                                            {
                                                int RowSave;
                                                if (strListValue != "")
                                                {
                                                    XDocument xmlDoc = new XDocument();
                                                    XElement xElm = new XElement("selectlist",
                                                                        from l in strListValue.Split(';')
                                                                        select new XElement("option", l)
                                                                    );
                                                    xmlDoc.Add(xElm);

                                                    strListValue = xmlDoc.ToString();
                                                }
                                                if (GblIQCare.iManageCareEnded == 1)
                                                    RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), 0, GblIQCare.AppUserId, 1, 1, strListValue, dtBusinessRule, predefineStatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);

                                                if (GblIQCare.iManageCareEnded == 2)
                                                    RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), 0, GblIQCare.AppUserId, 2, 1, strListValue, dtBusinessRule, predefineStatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);

                                                else
                                                    RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), 0, GblIQCare.AppUserId, 0, 1, strListValue, dtBusinessRule, predefineStatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);
                                                GblIQCare.hashTblSelectList.Clear();
                                                IQCareWindowMsgBox.ShowWindow("PMTCTCustomfieldsave", this);
                                            }
                                        }
                                    }
                                    else if (strname == "")
                                    {
                                        IQCareWindowMsgBox.ShowWindow("PMTCTFieldName", this);
                                        GblIQCare.hashTblSelectList.Clear();
                                    }
                                    else
                                    {
                                        IQCareWindowMsgBox.ShowWindow("PMTCTDisplayType", this);
                                    }
                                }
                                else
                                {
                                    IQCareWindowMsgBox.ShowWindow("PMTCTsaveinactivefield", this);
                                }
                            }
                            gblFieldID = 0;
                            gblFieldName = "";
                            blnNameChange = false;
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(str) == gblFieldID && strfieldName == gblFieldName)
                        {
                            if (r.Cells[0].Value != null)
                            {
                                string strListValue = "";
                                Int32 id = Convert.ToInt32(r.Cells[9].Value);
                                string strname = r.Cells[0].Value.ToString();
                                string strcontrolid = "";
                                if (r.Cells[1].Value == null)
                                {
                                    strcontrolid = "0";
                                }
                                else
                                {
                                    strcontrolid = GetControlID(r.Cells[1].Value.ToString());
                                }
                                string strList = Convert.ToString(r.Cells[3].Value);
                                string strbrule = Convert.ToString(r.Cells[4].Value);
                                int predefineStatus = 0;
                                string predefine = Convert.ToString(r.Cells[2].Value);
                                DataTable dtBusinessRule = new DataTable();
                                if (GblIQCare.objhashBusinessRule.Contains(id))
                                {
                                    dtBusinessRule = (DataTable)(GblIQCare.objhashBusinessRule[id]);
                                }
                                if (predefine == "P")
                                {
                                    predefineStatus = 1;
                                }
                                else
                                {
                                    predefineStatus = 0;
                                }
                                int Status = Convert.ToInt32(r.Cells[5].Value.ToString());
                                if (Status == 0)
                                {
                                    if (blnDisplayTypeChange)
                                    {
                                        if (strList == "1" || strbrule == "1")
                                        {
                                            bool blnChange = ChangeDisplayType(id, 2, Convert.ToInt32(strcontrolid), predefineStatus, strname);
                                            blnSave = blnChange;
                                        }
                                    }
                                    if (GblIQCare.objhashSelectList.Contains(id))
                                    {
                                        strListValue = GblIQCare.objhashSelectList[id].ToString();
                                    }

                                    if (predefine != "P")
                                    {
                                        if (!fnValidate(Convert.ToInt32(strcontrolid), strListValue))
                                        {
                                            IQCareWindowMsgBox.ShowWindow("PMTCTSelectlistvalidate", this);
                                            return;
                                        }
                                    }


                                    if (strname != "")
                                    {
                                        if (blnSave)
                                        {
                                            if (!fnValidateFieldName(strname))
                                            {
                                                IQCareWindowMsgBox.ShowWindow("PMTCTFieldNamevalid", this);
                                                return;
                                            }
                                            DataSet objDsDuplicate = new DataSet();
                                            if (r.Cells[11].Value != null)
                                            {
                                                iModuleId = Convert.ToInt32(r.Cells[11].Value.ToString());
                                            }
                                            else
                                            {
                                                iModuleId = 0;
                                            }
                                            objDsDuplicate = objFieldDetail.GetDuplicateCustomFields(id, strname, iModuleId, GblIQCare.iManageCareEnded);
                                            if (objDsDuplicate.Tables[0].Rows.Count > 0)
                                            {
                                                IQCareWindowMsgBox.ShowWindow("PMTCTduplicatefield", this);
                                            }
                                            else
                                            {
                                                int RowSave;
                                                if (strListValue != "")
                                                {
                                                    XDocument xmlDoc = new XDocument();
                                                    XElement xElm = new XElement("selectlist",
                                                                        from l in strListValue.Split(';')
                                                                        select new XElement("option", l)
                                                                    );
                                                    xmlDoc.Add(xElm);

                                                    strListValue = xmlDoc.ToString();
                                                }
                                                if (GblIQCare.iManageCareEnded == 1)
                                                    RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), 0, GblIQCare.AppUserId, 1, 1, strListValue, dtBusinessRule, predefineStatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);
                                                else if (GblIQCare.iManageCareEnded == 2)
                                                    RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), 0, GblIQCare.AppUserId, 2, 1, strListValue, dtBusinessRule, predefineStatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);
                                                else
                                                    RowSave = objFieldDetail.SaveUpdateCusomtField(Convert.ToInt32(id), strname, Convert.ToInt32(strcontrolid), 0, GblIQCare.AppUserId, 0, 1, strListValue, dtBusinessRule, predefineStatus, GblIQCare.SystemId, GblIQCare.dtConditionalFields, GblIQCare.dtICD10Code);
                                                GblIQCare.hashTblSelectList.Clear();
                                                IQCareWindowMsgBox.ShowWindow("PMTCTCustomfieldsave", this);
                                            }
                                        }

                                    }
                                    else if (strname == "")
                                    {
                                        IQCareWindowMsgBox.ShowWindow("PMTCTFieldName", this);
                                        GblIQCare.hashTblSelectList.Clear();
                                    }
                                    else
                                    {
                                        IQCareWindowMsgBox.ShowWindow("PMTCTDisplayType", this);
                                    }
                                }
                                else
                                {
                                    IQCareWindowMsgBox.ShowWindow("PMTCTsaveinactivefield", this);
                                }
                            }
                            gblFieldID = 0;
                            gblFieldName = "";
                        }
                    }
                }
            }

            btnadd.Enabled = true;
            ClearGrid();
            RemoveHastTable();
            txtfieldName.Text = "";
            BindGrid("", Convert.ToInt32(cmbTechArea.SelectedValue));
        }
        /// <summary>
        /// This function is used to delete the custom field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndelete_Click(object sender, EventArgs e)
        {
            IFieldDetail objFieldDetail;
            objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
            int Row = 0;
            foreach (DataGridViewRow r in dgwFieldDetails.Rows)
            {
                if (r.Selected)
                {
                    string predefine = Convert.ToString(r.Cells[2].Value);

                    if (predefine == "C" || predefine == "")
                    {
                        int fieldID = Convert.ToInt32(r.Cells[9].Value);
                        string fieldName = r.Cells[0].Value.ToString();
                        DialogResult dlgRes;

                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "field Name " + fieldName;
                        dlgRes = IQCareWindowMsgBox.ShowWindowConfirm("ConfirmDelete", theBuilder, this);

                        if (dlgRes == DialogResult.Yes)
                        {
                            if (fieldID != 0)
                            {

                                DataSet DsCustom = new DataSet();
                                DsCustom = objFieldDetail.CheckCustomFields(Convert.ToInt32(fieldID));
                                if (DsCustom.Tables[0].Rows.Count == 0)
                                {
                                    Row = objFieldDetail.DeleteCustomField(fieldID, 3);
                                }
                                else
                                {
                                    string msg = "Field Name " + fieldName + " is in used so it can't be deleted";
                                    MessageBox.Show(msg);

                                }
                            }
                            BindGrid("", Convert.ToInt32(cmbTechArea.SelectedValue));
                        }
                    }
                    else
                    {
                        IQCareWindowMsgBox.ShowWindow("PMTCTNotDeletePredefine", this);
                    }
                }
            }
        }
        /// <summary>
        /// This function validate whether there is data in select list or not
        /// </summary>
        /// <param name="controlId"></param>
        /// <param name="ControlValue"></param>
        /// <returns></returns>
        public bool fnValidate(int controlId, string controlValue)
        {
            bool blnreturn = true;
            if (controlId == 4 || controlId == 9 || controlId == 0)
            {
                if (controlValue == "")
                {
                    blnreturn = false;
                }
            }
            return blnreturn;
        }
        public bool fnValidateFieldName(string strVal)
        {
            //-=\\/!@#$%^ &*()+|.,<>?`~\";:'[]{},"-"
            string[] arStr = { "*", "?", "=", "/", "\\", "!", "@", "#", "$", "%", "^", "&", "(", ")", ":", "[", "]", "+", "~" };
            bool blnreturn = true;
            for (int i = 0; i < arStr.Length; i++)
            {
                if (strVal.Contains(arStr[i].ToString()))
                {
                    blnreturn = false;
                }
            }

            return blnreturn;
        }
        private void txtfieldName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RemoveHastTable();
                BindGrid(txtfieldName.Text, Convert.ToInt32(cmbTechArea.SelectedValue));
            }
        }

        private void dgwFieldDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView dgwDataGridForEvent = sender as DataGridView;
            if (!IsHandleAdded && dgwDataGridForEvent.CurrentCell.ColumnIndex == 0)
            {
                TextBox txtFieldName = e.Control as TextBox;
                if (txtFieldName != null)
                {
                    txtFieldName.KeyPress += new KeyPressEventHandler(txtFieldName_KeyPress);
                    IsHandleAdded = true;
                }
            }
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                ComboBox cb = e.Control as ComboBox;
                if (cb != null)
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDown;
                    cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                    cb.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cb.SelectedIndexChanged -= new EventHandler(cb_SelectedIndexChanged);
                    cb.SelectedIndexChanged += new EventHandler(cb_SelectedIndexChanged);
                }


            }

        }

        void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ComboBox cb = sender as ComboBox;
            //string controlId = cb.SelectedValue.ToString();

            //DataRow[] rows = mainDataset.Tables[3].Select("ControlID = " + controlId);

            //string referenceId = rows[0]["ReferenceId"].ToString(); ;

        }



        void txtFieldName_KeyPress(object sender, KeyPressEventArgs e)
        {
            String strVal = e.KeyChar.ToString();
            string strSearch = "-=\\/!@#$%^ &*()+|.,<>?`~\";:'[]{}";
            if (strSearch.IndexOf(strVal) >= 0)
            {
                e.Handled = true;
            }
        }

        private void dgwFieldDetails_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex != 0)
            {
                string display = string.Empty;
                string controlReference = string.Empty;
                string strname = string.Empty;
                if (dgwFieldDetails.Rows[e.RowIndex].Cells["Field Name"].Value != null)
                {
                    strname = dgwFieldDetails.Rows[e.RowIndex].Cells["Field Name"].Value.ToString();
                }
                if (dgwFieldDetails.Rows[e.RowIndex].Cells["Predefined"].Value.ToString() != "P" && !fnValidateFieldName(strname))
                {
                    IQCareWindowMsgBox.ShowWindow("PMTCTFieldNamevalid", this);
                    e.Cancel = true;
                }
                if (dgwFieldDetails.Rows[e.RowIndex].Cells["Display Type"].Value != null)
                {
                    display = dgwFieldDetails.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
                }
                if (dgwFieldDetails.Rows[e.RowIndex].Cells["Control Reference Id"].Value != null)
                {
                    controlReference = dgwFieldDetails.Rows[e.RowIndex].Cells["Control Reference Id"].Value.ToString();
                }
                // if (display.Trim() == "Select List" || display.Trim() == "4" || display.Trim() == "Multi Select" || display.Trim() == "9")
                if (controlReference.Trim() == "SELECT_LIST" || display.Trim() == "4" || controlReference.Trim() == "SELECTLIST_TEXTBOX" || controlReference.Trim() == "SELECT_LIST_MULTI" || display.Trim() == "9")
                {
                    if (!(GblIQCare.objhashSelectList.ContainsKey(e.RowIndex)))
                    {
                        if (dgwFieldDetails.Rows[e.RowIndex].Cells["ListValue"].Value != null)
                        {
                            GblIQCare.objhashSelectList.Add(e.RowIndex, dgwFieldDetails.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                            //GblIQCare.strSelectListstr = dgwFieldDetails.Rows[e.RowIndex].Cells[10].Value.ToString();
                        }
                    }

                    if (!(GblIQCare.objhashSelectList.ContainsKey(e.RowIndex)))
                    {
                        GblIQCare.objhashSelectList.Add(e.RowIndex, "");
                    }
                }
            }
        }

        private void cmbTechArea_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbTechArea.SelectedValue.GetType().ToString() == "System.Data.DataRowView")
                BindGrid(txtfieldName.Text, Convert.ToInt32(((DataRowView)(cmbTechArea.SelectedValue)).Row.ItemArray[0]));
            else
                BindGrid(txtfieldName.Text, Convert.ToInt32(cmbTechArea.SelectedValue));

        }
    }
}
