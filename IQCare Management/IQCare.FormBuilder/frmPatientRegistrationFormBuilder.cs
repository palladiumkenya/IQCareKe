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
using System.Text.RegularExpressions;

namespace IQCare.FormBuilder
{
    public partial class frmPatientRegistrationFormBuilder : Form
    {
        clsCssStyle theStyle = new clsCssStyle();
        int iPanelCount = 0;
        //Dimensions of Panel
        const int iPanelWidth = 935;
        const int iPanelHeight = 440;
        const int iPanelLeft = 12;
        int iPanelTop = 14;
        int iControlCount = 0;

        //fixed dimenstions of the grid
        const int iDataGridHeight = 345; 
        const int iDataGridWidth = 880;
        const int iDataGridLeft = 13;
        const int iDataGridTop = 44;
        
        DataSet dsUpdateMode = new DataSet();
        DataSet dsFieldDetails = new DataSet();
        DataSet dsCollectDataToSave = new DataSet();
        DataTable dtDeleteFields = new DataTable();
        DataTable dtDeleteSections = new DataTable();
       

        DataGridViewRow dgwRemoveSelectedRow = new DataGridViewRow();
        DataTable dtMstFeatureForManageField;
        DataTable dtMstSectionForManageField;
        DataTable dtLnkFormsForManageField; ////for fields

        int iSelectedFieldRowId = 0;

        public static DataTable dtManageSectionPos;

        static string strGetPath = GblIQCare.GetPath();
        Image imgList = Image.FromFile(strGetPath + "\\List.bmp");
        Image imgDisabledList = Image.FromFile(strGetPath + "\\listdesible.bmp");
        Image imgBusinessRule = Image.FromFile(strGetPath + "\\brule.bmp");
        Image imgButtonUp = Image.FromFile(strGetPath + "\\btnUp.Image.gif");
        Image imgButtonDown = Image.FromFile(strGetPath + "\\btnDown.Image.gif");
        Image imgInactiveBR = Image.FromFile(strGetPath + "\\nonactive.bmp");

        public frmPatientRegistrationFormBuilder()
        {
            InitializeComponent();
        }

        private void CreatePanel(string strMode)
        {
            if (iPanelCount >= 1)
            {
                pnlMainPanel.AutoScrollPosition = new Point(0, 0);
            }
            Panel pnlDynamicPanel = new Panel();
            pnlDynamicPanel.Name = "pnlDynamicPanel_" + iPanelCount;

            //add new panel into the main panel of form.
            pnlDynamicPanel.Width = iPanelWidth;
            pnlDynamicPanel.Height = iPanelHeight;
            pnlDynamicPanel.Left = iPanelLeft;
            pnlDynamicPanel.Top = iPanelTop;

            pnlMainPanel.Controls.Add(pnlDynamicPanel);

            ////add controls in new panel
            DataGridView dgwDataGrid = new DataGridView();
            dgwDataGrid.Name = "dgwDataGrid_" + iControlCount;
            dgwDataGrid.Height = iDataGridHeight;
            dgwDataGrid.Width = iDataGridWidth;
            dgwDataGrid.Left = iDataGridLeft;
            dgwDataGrid.Top = iDataGridTop;
            dgwDataGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(dgwDataGrid_CellFormatting);
            dgwDataGrid.CellValueChanged += new DataGridViewCellEventHandler(dgwDataGrid_CellValueChanged);
            dgwDataGrid.CellClick += new DataGridViewCellEventHandler(dgwDataGrid_CellClick);
            //DataError event is required to handle error while rebinding grid specialy in drop down case,
            //just an empty even wud be enough to handle error
            dgwDataGrid.DataError += new DataGridViewDataErrorEventHandler(dgwDataGrid_DataError);
            //for adding event in control inside the grid
            dgwDataGrid.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgwDataGrid_EditingControlShowing);
            dgwDataGrid.Tag = "dgwDataGridView";
            dgwDataGrid.AllowUserToResizeColumns = false;
            dgwDataGrid.AllowUserToResizeRows = false;
            dgwDataGrid.AllowUserToAddRows = false;

            ////add grid in new panel
            pnlDynamicPanel.Controls.Add(dgwDataGrid);

            clsCommon.CreateGridColumnsForFormBuilder(dgwDataGrid, dsFieldDetails);

            //add label in new panel
            Label lblSectionName = new Label();
            lblSectionName.Name = "lblSectionName_" + iControlCount;
            lblSectionName.Height = 13;
            lblSectionName.Width = 80;
            lblSectionName.Left = iDataGridLeft;
            lblSectionName.Top = 5;
            lblSectionName.Text = "Section Name :";
            lblSectionName.Tag = "lblLabel"; // css for the label
            pnlDynamicPanel.Controls.Add(lblSectionName);

            //add text box in new panel
            TextBox txtSectionName = new TextBox();
            txtSectionName.Name = "txtSectionName_" + iControlCount + "_0";
            txtSectionName.Height = 20;
            txtSectionName.Width = 355;
            txtSectionName.Left = iDataGridLeft + lblSectionName.Width + 5;
            txtSectionName.Top = 5;
            txtSectionName.MaxLength = 50;
            txtSectionName.Tag = "txtTextBox";
            txtSectionName.KeyPress += new KeyPressEventHandler(txtSectionName_KeyPress);
            //in update mode set sectionid after last underscore
            if (strMode == PMTCTConstants.strUpdate)
            {
                if (dsUpdateMode.Tables[1].Rows.Count >= (iControlCount + 1))
                {
                    txtSectionName.Name = "txtSectionName_" + iControlCount + "_" + dsUpdateMode.Tables[1].Rows[iControlCount]["SectionId"].ToString();
                    txtSectionName.Text = dsUpdateMode.Tables[1].Rows[iControlCount]["SectionName"].ToString();
                }
            }

            pnlDynamicPanel.Controls.Add(txtSectionName);

            //add button in new panel
            Button btnAddField = new Button();
            btnAddField.Name = "btnAddFiled_" + iControlCount;
            btnAddField.Height = 21;
            btnAddField.Width = 141;
            btnAddField.Left = 250 + iDataGridLeft + 5;
            btnAddField.Top = dgwDataGrid.Bottom + 20;
            btnAddField.Tag = "btnH25WFlexi";
            btnAddField.Text = "A&dd Field";
            btnAddField.Click += new EventHandler(btnAddField_Click);
            pnlDynamicPanel.Controls.Add(btnAddField);

            Button btnRemoveField = new Button();
            btnRemoveField.Name = "btnRemoveField_" + iControlCount;
            btnRemoveField.Height = 21;
            btnRemoveField.Width = 141;
            btnRemoveField.Left = 300 + iDataGridLeft + 5 + btnAddField.Width + 5;
            btnRemoveField.Top = dgwDataGrid.Bottom + 20;
            btnRemoveField.Tag = "btnH25WFlexi";
            btnRemoveField.Text = "&Remove Field";
            btnRemoveField.Click += new EventHandler(btnRemoveField_Click);
            pnlDynamicPanel.Controls.Add(btnRemoveField);

            Button btnUp = new Button();
            btnUp.Name = "btnUp_" + iControlCount;
            btnUp.Height = 25;
            btnUp.Width = 25;
            btnUp.Left = dgwDataGrid.Right + 8;
            btnUp.Top = dgwDataGrid.Top + 100;
            btnUp.Tag = "btnH25WFlexi";
            btnUp.Image = imgButtonUp;
            btnUp.Click += new EventHandler(btnUp_Click);
            pnlDynamicPanel.Controls.Add(btnUp);

            Label lblMove = new Label();
            lblMove.Name = "lblMove_" + iControlCount;
            lblMove.Left = dgwDataGrid.Right + 2;
            lblMove.Top = dgwDataGrid.Top + 100 + btnUp.Height + 5;
            lblMove.Tag = "lblLabel";
            lblMove.Text = "Move";
            pnlDynamicPanel.Controls.Add(lblMove);

            Button btnDown = new Button();
            btnDown.Name = "btnDown_" + iControlCount;
            btnDown.Height = 25;
            btnDown.Width = 25;
            btnDown.Left = dgwDataGrid.Right + 8;
            btnDown.Top = dgwDataGrid.Top + 100 + btnUp.Height + lblMove.Height + 10;
            btnDown.Tag = "btnH25WFlexi";
            btnDown.Image = imgButtonDown;
            btnDown.Click += new EventHandler(btnDown_Click);
            pnlDynamicPanel.Controls.Add(btnDown);

            //after adding all the controls set the scrollbar to newly added panel
            pnlMainPanel.AutoScrollPosition = new Point(0, iPanelTop);

            ////after creation of panel, set the dimensions for the new panel
            iPanelTop = iPanelHeight + iPanelTop + 10;

            ////increment the counter 
            iControlCount = iControlCount + 1;
            iPanelCount = iPanelCount + 1;

            //set css for panel
            pnlDynamicPanel.Tag = "pnlSubPanel";
            theStyle.setStyle(pnlDynamicPanel);
        }

        void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                //find datagrid
                Button btnSender = sender as Button;
                int iCounter = System.Convert.ToInt16(btnSender.Name.ToString().Split('_')[1]);
                DataGridView dgwGrid = (DataGridView)pnlMainPanel.Controls.Find("dgwDataGrid_" + iCounter, true)[0];
                if (dgwGrid.SelectedRows[0] != null)
                {
                    //Move down
                    int currentIndex = dgwGrid.SelectedRows[0].Index;
                    if (currentIndex <= dgwGrid.Rows.Count - 2)
                    {
                        DataGridViewRow currRow = dgwGrid.SelectedRows[0];
                        DataGridViewRow downRow = dgwGrid.Rows[currentIndex + 1];
                        dgwGrid.Rows.Remove(downRow);
                        dgwGrid.Rows.Remove(currRow);
                        dgwGrid.Rows.Insert(currentIndex, currRow);
                        dgwGrid.Rows.Insert(currentIndex, downRow);
                        currRow.Selected = true;
                        dgwGrid.CurrentRow.Selected = false;
                    }
                }
            }
            catch
            {

            }
        }

        void btnUp_Click(object sender, EventArgs e)
        {
            try
            {
                //Find datagrid
                Button btnSender = sender as Button;
                int iCounter = System.Convert.ToInt16(btnSender.Name.ToString().Split('_')[1]);
                DataGridView dgwGrid = (DataGridView)pnlMainPanel.Controls.Find("dgwDataGrid_" + iCounter, true)[0];
                if (dgwGrid.SelectedRows[0] != null)
                {
                    //move up
                    int currentIndex = dgwGrid.SelectedRows[0].Index;
                    if (currentIndex > 0)
                    {
                        DataGridViewRow currRow = dgwGrid.SelectedRows[0];
                        DataGridViewRow upRow = dgwGrid.Rows[currentIndex - 1];
                        dgwGrid.CurrentRow.Selected = false;
                        dgwGrid.Rows.Remove(upRow);
                        dgwGrid.Rows.Remove(currRow);
                        dgwGrid.Rows.Insert(currentIndex - 1, currRow);
                        dgwGrid.Rows.Insert(currentIndex, upRow);
                        currRow.Selected = true;
                        dgwGrid.CurrentRow.Selected = false;
                    }
                }
            }
            catch
            {

            }
        }

        void btnRemoveField_Click(object sender, EventArgs e)
        {
            Button btnSender = sender as Button;
            String strButtonName = btnSender.Name;
            int iCounter = System.Convert.ToInt16(btnSender.Name.ToString().Split('_')[1]);
            DataGridView dgwGrid = (DataGridView)pnlMainPanel.Controls.Find("dgwDataGrid_" + iCounter, true)[0];

            if (iSelectedFieldRowId > 0 && dgwRemoveSelectedRow.Index != -1)
            {
                DataRow drRemoveField;
                drRemoveField = dtDeleteFields.NewRow();
                drRemoveField["Id"] = iSelectedFieldRowId;
                drRemoveField["FieldName"] = dgwRemoveSelectedRow.Cells["Table Field Name"].FormattedValue.ToString();
                dtDeleteFields.Rows.Add(drRemoveField);
                //iSelectedFieldRowId = 0;
            }
            if (dgwRemoveSelectedRow != null && dgwRemoveSelectedRow.Index != -1 && dgwRemoveSelectedRow.Cells[0].Value != null)
            {
                dgwGrid.Rows.Remove(dgwRemoveSelectedRow);
            }  


        }

        void btnAddField_Click(object sender, EventArgs e)
        {
            Button btnSender = sender as Button;
            String strButtonName = btnSender.Name;
            int iCounter = System.Convert.ToInt16(btnSender.Name.ToString().Split('_')[1]);
            DataGridView dgwGrid = (DataGridView)pnlMainPanel.Controls.Find("dgwDataGrid_" + iCounter, true)[0];
            dgwGrid.AllowUserToAddRows = true;

            int iRowCount = dgwGrid.Rows.Count;
            if (iRowCount > 0)
            {
                dgwGrid.Rows[iRowCount - 1].DefaultCellStyle.BackColor = Color.White;
                dgwGrid.Rows[iRowCount - 1].DefaultCellStyle.ForeColor = Color.Black;
            }
        }

        void txtSectionName_KeyPress(object sender, KeyPressEventArgs e)
        {
            String strVal = e.KeyChar.ToString();
            string strSearch = "=\\/!@#$%^*+|.<>`~\";:[]{}";
            if (strSearch.IndexOf(strVal) >= 0)
            {
                e.Handled = true;
            }
        }

        void dgwDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            
        }

        void dgwDataGrid_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            

        }

        void dgwDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgwGridData = sender as DataGridView;
            //reset selected row values
            if (e.RowIndex != -1)
            {
                iSelectedFieldRowId = 0;
                dgwRemoveSelectedRow = null;
                GblIQCare.gblRowIndex = e.RowIndex;
                //remove field 
                if (dgwGridData.Rows[e.RowIndex].Cells["ID"].Value != null && dgwGridData.Rows[e.RowIndex].Cells["ID"].Value.ToString() != "")
                {
                    iSelectedFieldRowId = System.Convert.ToInt32(dgwGridData.Rows[e.RowIndex].Cells["ID"].Value);

                }
                dgwRemoveSelectedRow = dgwGridData.Rows[e.RowIndex];
            }

            //if business rule column clicked or select list column clicked then open the window
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                GblIQCare.strDisplayType = "";
                GblIQCare.strFieldName = "";
                GblIQCare.strSelectFieldName = "";
                GblIQCare.strSelectListValue = "";
                if (dgwGridData.Columns[e.ColumnIndex].Name == "Business Rule")
                {
                    //string strDisplay = string.Empty;
                    if (dgwGridData.Rows[e.RowIndex].Cells["Business Rule"].Value != null && dgwGridData.Rows[e.RowIndex].Cells["Business Rule"].Value.ToString() != "0" && dgwGridData.Rows[e.RowIndex].Cells["Business Rule"].Value.ToString() != "System.Drawing.Bitmap")
                    {
                        GblIQCare.strDisplayType = dgwGridData.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
                        GblIQCare.intFieldDetaisChange = Convert.ToInt32(dgwGridData.Rows[e.RowIndex].Cells["FieldId"].Value);
                        GblIQCare.strFieldName = dgwGridData.Rows[e.RowIndex].Cells["Table Field Name"].FormattedValue.ToString();


                        GblIQCare.dtBusinessValues.Clear();
                        GblIQCare.dtBusinessValues.Columns.Clear();
                        GblIQCare.dtBusinessValues.Rows.Clear();
                        DataSet dsFillBusinessRule = dsFieldDetails.Copy();
                        if (dsFillBusinessRule.Tables[4].Rows.Count > 0)
                        {
                            DataView dv;
                            dv = dsFillBusinessRule.Tables[4].DefaultView;
                            dv.RowFilter = "FieldID=" + dgwGridData.Rows[e.RowIndex].Cells["FieldId"].Value + " and predefined=" + dgwGridData.Rows[e.RowIndex].Cells["Predefined"].Value.ToString();
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
                        //theForm.MdiParent = this.MdiParent.MdiParent;

                        //theForm.Parent = this.MdiParent.MdiParent;
                        theForm.MdiParent = this.MdiParent;
                        GblIQCare.iFormMode = 1; //to open form in readonly mode
                        //theForm.MdiParent = GblIQCare.theMainForm;
                        theForm.Deactivate += new EventHandler(FrmHideChildOnLostFocus);
                        //theForm.Enabled=false;
                        theForm.Show();

                    }
                } //business rule endif
                else if (dgwGridData.Columns[e.ColumnIndex].Name == "List")
                {
                    if (dgwGridData.Rows[e.RowIndex].Cells["List"].Value != null && dgwGridData.Rows[e.RowIndex].Cells["List"].Value.ToString() != "0" && dgwGridData.Rows[e.RowIndex].Cells["List"].Value.ToString() != "System.Drawing.Bitmap")
                    {
                        string display = string.Empty;
                        if (dgwGridData.Rows[e.RowIndex].Cells["Display Type"].Value != null)
                        {
                            display = dgwGridData.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
                        }
                        if (dgwGridData.Rows[e.RowIndex].Cells["Table Field Name"].Value != null)
                        {
                            GblIQCare.intFieldDetaisChange = Convert.ToInt32(dgwGridData.Rows[e.RowIndex].Cells["FieldId"].Value.ToString());
                        }
                        //if ((display.Trim() == "Select List" || display == "4" || display.Trim() == "Multi Select" || display == "9") && (dgwGridData.Rows[e.RowIndex].Cells["Predefined"].Value.ToString()=="0"))
                        if ((display.Trim() == "Select List" || display == "4" || display.Trim() == "Multi Select" || display == "9"))
                        {
                            if (dgwGridData.Rows[e.RowIndex].Cells["Table Field Name"].Value != null)
                            {
                                GblIQCare.strSelectFieldName = dgwGridData.Rows[e.RowIndex].Cells["Table Field Name"].FormattedValue.ToString();

                                DataSet dsFillBusinessRule = dsFieldDetails.Copy();
                                if (dsFillBusinessRule.Tables[2].Rows.Count > 0)
                                {
                                    DataView dv;
                                    dv = dsFillBusinessRule.Tables[2].DefaultView;
                                    dv.RowFilter = "FieldID='" + dgwGridData.Rows[e.RowIndex].Cells["FieldId"].Value.ToString() + "' and predefined=" + dgwGridData.Rows[e.RowIndex].Cells["Predefined"].Value.ToString();
                                    DataView DvFilter = new DataView();
                                    DataTable dt = new DataTable();
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
                                            //GblIQCare.strSelectListValue = dt.Rows[0]["FieldValue"].ToString();

                                        }
                                    }

                                }
                            }
                            Form theForm;
                            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmSelectList, IQCare.FormBuilder"));
                            theForm.Left = 0;
                            theForm.Top = 0;
                            GblIQCare.iFormMode = 1; //to open form in readonly mode
                            theForm.MdiParent = this.MdiParent;
                            //theForm.MdiParent = GblIQCare.theMainForm;
                            theForm.Deactivate += new EventHandler(FrmHideChildOnLostFocus);
                            //theForm.Enabled = false;
                            theForm.Show();
                        }
                    }//select list check

                }
            }
        }

        void dgwDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                DataGridView dgwDataGrid = sender as DataGridView;
                if (dgwDataGrid.Columns[e.ColumnIndex].Name == "Table Field Name" && dgwDataGrid.Rows[e.RowIndex].Cells["Table Field Name"].Value != null)
                {
                    DataSet dsSetFieldDetail = dsFieldDetails.Copy();
                    DataView dvFilteredRow = new DataView();
                    dvFilteredRow = dsSetFieldDetail.Tables[0].DefaultView;
                    DataTable dtRow = new DataTable();

                    dvFilteredRow.RowFilter = "FieldName='" + dgwDataGrid.Rows[e.RowIndex].Cells["Table Field Name"].Value.ToString() + "'";
                    dtRow = dvFilteredRow.ToTable();

                    dgwDataGrid.Rows[e.RowIndex].Cells["Display Type"].Value = dtRow.Rows[0]["Name"].ToString();
                    dgwDataGrid.Rows[e.RowIndex].Cells["ControlId"].Value = dtRow.Rows[0]["ControlId"].ToString();
                    dgwDataGrid.Rows[e.RowIndex].Cells["Predefined"].Value = dtRow.Rows[0]["Predefine"].ToString();
                    dgwDataGrid.Rows[e.RowIndex].Cells["FieldId"].Value = dtRow.Rows[0]["Id"].ToString();

                    if (dgwDataGrid.Rows[e.RowIndex].Cells["FieldId"].Value.ToString() == "71")
                    {
                        dgwDataGrid.Rows[e.RowIndex].Cells["FieldId"].Value = dgwDataGrid.Rows[e.RowIndex].Cells["FieldId"].Value + "00000" + e.RowIndex.ToString() + iControlCount.ToString();
                        dgwDataGrid.Rows[e.RowIndex].Cells["Field Label"].Value = "PlaceHolder";
                        dgwDataGrid.Rows[e.RowIndex].Cells["Field Label"].ReadOnly = true;
                    }
                    else
                    {

                        dgwDataGrid.Rows[e.RowIndex].Cells["Field Label"].ReadOnly = false;
                        dgwDataGrid.Rows[e.RowIndex].Cells["Field Label"].Value = "";
                    }

                    //fetch business rule value
                    //implement fetchsearchvalue instead of checkdataexists
                    String[] strSearchCol = new string[2];
                    String[] strSearchColVal = new string[2];
                    strSearchCol[0] = "FieldId";
                    strSearchCol[1] = "Predefined";
                    if (dtRow.Rows[0]["Id"].ToString() == "71")
                    {
                        strSearchColVal[0] = dgwDataGrid.Rows[e.RowIndex].Cells["FieldId"].Value.ToString();
                    }
                    else
                    {
                        strSearchColVal[0] = dtRow.Rows[0]["Id"].ToString();
                    }
                    strSearchColVal[1] = dtRow.Rows[0]["Predefine"].ToString();

                    // check for business rule
                    if (clsCommon.FetchSearchValue(strSearchCol, strSearchColVal, dsSetFieldDetail.Tables[4]))
                    {
                        dgwDataGrid.Rows[e.RowIndex].Cells[4].Value = 1;
                    }
                    else
                    {
                        dgwDataGrid.Rows[e.RowIndex].Cells[4].Value = 0;
                    }

                    // check for List
                    if (clsCommon.FetchSearchValue(strSearchCol, strSearchColVal, dsSetFieldDetail.Tables[2]))
                    {
                        dgwDataGrid.Rows[e.RowIndex].Cells[3].Value = 1;
                    }
                    else
                    {
                        dgwDataGrid.Rows[e.RowIndex].Cells[3].Value = 0;
                    }
                }

            }
        }

        void dgwDataGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgwDataGrid = sender as DataGridView;
            if (e.ColumnIndex != -1 && e.RowIndex > -1)
            {
                if (dgwDataGrid.Columns[e.ColumnIndex].Name == "List")
                {

                    if (e.Value.ToString() == "1")
                    {
                        e.Value = imgList;
                    }
                    else
                    {
                        e.Value = imgDisabledList;
                    }

                }
                else if (dgwDataGrid.Columns[e.ColumnIndex].Name == "Business Rule")
                {
                    if (e.Value.ToString() == "1")
                    {
                        e.Value = imgBusinessRule;
                    }
                    else
                    {
                        e.Value = imgInactiveBR;
                    }
                }

            }


        }

        private void btnAddSection_Click(object sender, EventArgs e)
        {
            CreatePanel(GblIQCare.strPatientRegistrationInsert);
        }

        private void FillData()
        {
            //mst feature
            if (dsUpdateMode.Tables[0].Rows.Count > 0)
            {
                txtFormName.Text = dsUpdateMode.Tables[0].Rows[0]["FeatureName"].ToString();
                //feature id is already in GblIQCare.iFormBuilderId in update mode
                IFieldDetail objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                //pass fieldName if want to retrieve specific field from GetCustomFields
                dsFieldDetails = objFieldDetail.GetCustomFields("", 0, 2);
                ///////////////////////////////////////
            }

            //mst section
            if (dsUpdateMode.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < dsUpdateMode.Tables[1].Rows.Count; i++)
                {
                    CreatePanel(PMTCTConstants.strUpdate);
                }
            }
            ///////////////////*************************************
            //lnk_froms
            if (dsUpdateMode.Tables[2].Rows.Count > 0)
            {
                int iDsRow = 0;
                foreach (Control ctrlControls in pnlMainPanel.Controls)
                {
                    if (ctrlControls is Panel && ctrlControls.Visible != false)
                    {
                        //for loop again to fetch datagrid details
                        foreach (Control ctrl in ctrlControls.Controls)
                        {
                            if (ctrl is DataGridView)
                            {
                                DataGridView dgwGridData = ctrl as DataGridView;
                                DataTable dtLnk_Form = new DataTable();
                                dtLnk_Form = clsCommon.CreateTableLnk_Forms();
                                if (dsUpdateMode.Tables[2].Rows.Count > 0)
                                {

                                    foreach (DataRow drFields in dsUpdateMode.Tables[2].Rows)
                                    {
                                        if (dsUpdateMode.Tables[1].Rows[iDsRow]["SectionId"].ToString() == drFields["SectionId"].ToString())
                                        {
                                            DataRow row;
                                            row = dtLnk_Form.NewRow();
                                            row["id"] = Convert.ToInt32(drFields["ID"].ToString());
                                            row["FeatureId"] = drFields["featureId"].ToString();
                                            row["SectionId"] = drFields["SectionId"].ToString();
                                            row["FieldId"] = Convert.ToInt32(drFields["FieldId"].ToString());
                                            row["FieldLabel"] = drFields["FieldLabel"].ToString();
                                            row["UserId"] = drFields["UserId"].ToString();
                                            row["Predefined"] = drFields["Predefined"].ToString();
                                            row["InsertUpdateStatus"] = PMTCTConstants.strUpdate;
                                            dtLnk_Form.Rows.Add(row);
                                        }

                                    }
                                    iDsRow += 1;
                                    foreach (DataRow drFields in dtLnk_Form.Rows)
                                    {
                                        DataSet dsSetFieldDetail = dsFieldDetails.Copy();
                                        DataView dvFilteredRow = new DataView();
                                        dvFilteredRow = dsSetFieldDetail.Tables[0].DefaultView;
                                        DataTable dtRow = new DataTable();

                                        if (drFields["FieldId"].ToString().Contains("7100000") == true)
                                            dvFilteredRow.RowFilter = "ID='71' and predefine=" + drFields["Predefined"].ToString();
                                        else
                                            dvFilteredRow.RowFilter = "ID='" + drFields["FieldId"].ToString() + "' and predefine=" + drFields["Predefined"].ToString();

                                        dtRow = dvFilteredRow.ToTable();
                                        if (dtRow.Rows.Count > 0)
                                        {
                                            int iBRValExist, iLstValExist;
                                            String[] strSearchCol = new string[2];
                                            String[] strSearchColVal = new string[2];

                                            strSearchCol[0] = "FieldId";
                                            strSearchCol[1] = "Predefined";
                                            strSearchColVal[0] = drFields["FieldId"].ToString();
                                            strSearchColVal[1] = drFields["Predefined"].ToString();
                                            // check for business rule
                                            if (clsCommon.FetchSearchValue(strSearchCol, strSearchColVal, dsSetFieldDetail.Tables[4]))
                                            {
                                                iBRValExist = 1;
                                            }
                                            else
                                            {
                                                iBRValExist = 0;
                                            }

                                            // check for List
                                            if (clsCommon.FetchSearchValue(strSearchCol, strSearchColVal, dsSetFieldDetail.Tables[2]))
                                            {
                                                iLstValExist = 1;
                                            }
                                            else
                                            {
                                                iLstValExist = 0;
                                            }


                                            dgwGridData.Rows.Add(dtRow.Rows[0]["FieldName"].ToString(), drFields["FieldLabel"].ToString(), dtRow.Rows[0]["Name"].ToString(), iLstValExist, iBRValExist, drFields["Id"].ToString(), "", drFields["Predefined"].ToString(), drFields["FieldId"].ToString());
                                        }
                                    }
                                }
                            }//endif of datagridview check
                        }
                    }//endif of panel control check

                }//end of foreach panel
                pnlMainPanel.AutoScrollPosition = new Point(0, 0);
            }
        }

        private void frmPatientRegistrationFormBuilder_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            txtFormName.Text = "Patient Registration";
            GblIQCare.iManageCareEnded = 2;
            if (GblIQCare.ResetSection == 1)
            {
                GblIQCare.ResetSection = 0;
                RefereshSection();
            }
            else if (GblIQCare.RefillDdlFields == 1)
            {
                IFieldDetail objFieldDetail;
                objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                //pass fieldName if want to retrieve specific field from GetCustomFields
                dsFieldDetails = objFieldDetail.GetCustomFields("", 0, GblIQCare.iManageCareEnded);
                GblIQCare.RefillDdlFields = 0;
            }
            else
            {
                IFieldDetail objFieldDetail;
                objFieldDetail = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                //pass fieldName if want to retrieve specific field from GetCustomFields
                //2 is for PatientRegistration Field
                dsFieldDetails = objFieldDetail.GetCustomFields("", 0, GblIQCare.iManageCareEnded);
                if (GblIQCare.iFormBuilderId > 0) //update mode
                {
                    IFormBuilder objFormBuilder;
                    objFormBuilder = (IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder");
                    dsUpdateMode = objFormBuilder.GetFormDetail(GblIQCare.iFormBuilderId);
                    GblIQCare.iConditionalbtn = 1;
                    FillData();
                    if (dtDeleteFields != null)
                        dtDeleteFields.Clear();
                    dtDeleteFields = new DataTable();
                    dtDeleteFields = clsCommon.RemoveFieldLnk_Forms();
                    dtDeleteSections = clsCommon.ManageSectionPos();
                }
            }
        }

        private void GridCellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgwGridData = sender as DataGridView;
            //reset selected row values
            if (e.RowIndex != -1)
            {
                iSelectedFieldRowId = 0;
                dgwRemoveSelectedRow = null;
                GblIQCare.gblRowIndex = e.RowIndex;
                //remove field 
                if (dgwGridData.Rows[e.RowIndex].Cells["ID"].Value != null && dgwGridData.Rows[e.RowIndex].Cells["ID"].Value.ToString() != "")
                {
                    iSelectedFieldRowId = System.Convert.ToInt32(dgwGridData.Rows[e.RowIndex].Cells["ID"].Value);

                }
                dgwRemoveSelectedRow = dgwGridData.Rows[e.RowIndex];
            }

            //if business rule column clicked or select list column clicked then open the window
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                GblIQCare.strDisplayType = "";
                GblIQCare.strFieldName = "";
                GblIQCare.strSelectFieldName = "";
                GblIQCare.strSelectListValue = "";
                if (dgwGridData.Columns[e.ColumnIndex].Name == "Business Rule")
                {
                    if (dgwGridData.Rows[e.RowIndex].Cells["Business Rule"].Value != null && dgwGridData.Rows[e.RowIndex].Cells["Business Rule"].Value.ToString() != "0" && dgwGridData.Rows[e.RowIndex].Cells["Business Rule"].Value.ToString() != "System.Drawing.Bitmap")
                    {
                        GblIQCare.strDisplayType = dgwGridData.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
                        GblIQCare.intFieldDetaisChange = Convert.ToInt32(dgwGridData.Rows[e.RowIndex].Cells["FieldId"].Value);
                        GblIQCare.strFieldName = dgwGridData.Rows[e.RowIndex].Cells["Table Field Name"].FormattedValue.ToString();

                        GblIQCare.dtBusinessValues.Clear();
                        GblIQCare.dtBusinessValues.Columns.Clear();
                        GblIQCare.dtBusinessValues.Rows.Clear();
                        DataSet dsFillBusinessRule = dsFieldDetails.Copy();
                        if (dsFillBusinessRule.Tables[4].Rows.Count > 0)
                        {
                            DataView dv;
                            dv = dsFillBusinessRule.Tables[4].DefaultView;
                            dv.RowFilter = "FieldID=" + dgwGridData.Rows[e.RowIndex].Cells["FieldId"].Value + " and predefined=" + dgwGridData.Rows[e.RowIndex].Cells["Predefined"].Value.ToString();
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
                else if (dgwGridData.Columns[e.ColumnIndex].Name == "List")
                {
                    if (dgwGridData.Rows[e.RowIndex].Cells["List"].Value != null && dgwGridData.Rows[e.RowIndex].Cells["List"].Value.ToString() != "0" && dgwGridData.Rows[e.RowIndex].Cells["List"].Value.ToString() != "System.Drawing.Bitmap")
                    {
                        string display = string.Empty;
                        if (dgwGridData.Rows[e.RowIndex].Cells["Display Type"].Value != null)
                        {
                            display = dgwGridData.Rows[e.RowIndex].Cells["Display Type"].Value.ToString();
                        }
                        if (dgwGridData.Rows[e.RowIndex].Cells["Table Field Name"].Value != null)
                        {
                            GblIQCare.intFieldDetaisChange = Convert.ToInt32(dgwGridData.Rows[e.RowIndex].Cells["FieldId"].Value.ToString());
                        }
                        if ((display.Trim() == "Select List" || display == "4" || display.Trim() == "Multi Select" || display == "9"))
                        {
                            if (dgwGridData.Rows[e.RowIndex].Cells["Table Field Name"].Value != null)
                            {
                                GblIQCare.strSelectFieldName = dgwGridData.Rows[e.RowIndex].Cells["Table Field Name"].FormattedValue.ToString();

                                DataSet dsFillBusinessRule = dsFieldDetails.Copy();
                                if (dsFillBusinessRule.Tables[2].Rows.Count > 0)
                                {
                                    DataView dv;
                                    dv = dsFillBusinessRule.Tables[2].DefaultView;
                                    dv.RowFilter = "FieldID='" + dgwGridData.Rows[e.RowIndex].Cells["FieldId"].Value.ToString() + "' and predefined=" + dgwGridData.Rows[e.RowIndex].Cells["Predefined"].Value.ToString();
                                    DataView DvFilter = new DataView();
                                    DataTable dt = new DataTable();
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
                            theForm.MdiParent = this.MdiParent;
                            theForm.Deactivate += new EventHandler(FrmHideChildOnLostFocus);
                            theForm.Show();
                        }
                    }

                }
            }
        }
        private void FrmHideChildOnLostFocus(object sender, EventArgs e)
        {
            Form theSenderForm = sender as Form;
            theSenderForm.Close();
            GblIQCare.iFormMode = 0;
        }
        public void RefereshSection()
        {
            DataView theDiv;
            theDiv = dtManageSectionPos.DefaultView;
            theDiv.RowFilter = "DeleteFlag=0";
            theDiv.Sort = "TopPos desc";
            //iPanelCount = 0;
            //last top position of active panel will be updated in iPanelTop. iPanelTop position will
            //be reused to create new panel position
            if (theDiv.Count > 0)
            {
                iPanelTop = System.Convert.ToInt32(theDiv[0]["TopPos"]) + iPanelHeight + 10;
            }
            else
            {
                iPanelTop = 14;
            }

            foreach (Control ctrlControls in pnlMainPanel.Controls)
            {
                if (ctrlControls is Panel && ctrlControls.Visible != false)
                {
                    //for loop again to fetch datagrid details
                    foreach (Control ctrl in ctrlControls.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            if (ctrl.Text.ToString().Trim() != "")
                            {
                                for (int x = 0; x < dtManageSectionPos.Rows.Count; x++)
                                {
                                    if (dtManageSectionPos.Rows[x]["SectionName"].ToString() == ctrl.Text.ToString().Trim())
                                    {
                                        if (dtManageSectionPos.Rows[x]["DeleteFlag"].ToString() == "0")
                                        {
                                            ctrlControls.Top = System.Convert.ToInt32(dtManageSectionPos.Rows[x]["TopPos"]);

                                            //x is concatinated after underscore for sequence of section
                                            ctrlControls.Name += "_" + (x + 1);
                                        }
                                        else if (dtManageSectionPos.Rows[x]["DeleteFlag"].ToString() == "1" && GblIQCare.iFormBuilderId > 0)
                                        {
                                            //if after underscore "D" is found then that means section is marked for deletion

                                            DataRow drDelSec = dtDeleteSections.NewRow();
                                            string[] strDelSectionId;
                                            //ctrl.Name += "_D";
                                            strDelSectionId = ctrl.Name.Split('_');
                                            if (strDelSectionId.Length > 1)
                                            {
                                                if (System.Convert.ToInt32(strDelSectionId[2]) > 0)
                                                {
                                                    drDelSec["SectionID"] = strDelSectionId[2];
                                                    dtDeleteSections.Rows.Add(drDelSec);
                                                }
                                            }
                                            ctrlControls.Hide();

                                        }
                                        else
                                        {
                                            ctrlControls.Hide();
                                        }
                                    }
                                }
                            }

                        }
                    }// end of foreach of controls at section level
                }
            }//end of foreach of mainpanel control
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            Hashtable theHTDuplicatefield = new Hashtable();
            theHTDuplicatefield.Clear();
            btnSave.Enabled = false;
            bool blnSaveData = true;
            dsCollectDataToSave.Tables.Clear();

            DataTable dtMstFeature = new DataTable();
            dtMstFeature = clsCommon.CreateTableMstFeature();
            DataRow row;
            int iFormId=126;//iFormId = 126 is for Patient Registration
            int iSectionId = 0;

            row = dtMstFeature.NewRow();
            if (GblIQCare.iFormBuilderId != 0)
            {
                row["FeatureId"] = iFormId;
                row["InsertUpdateStatus"] = PMTCTConstants.strUpdate;
            }
            row["FeatureName"] = txtFormName.Text.ToString().Trim();
            row["ReportFlag"] = 0;
            row["DeleteFlag"] = 0;
            row["AdminFlag"] = 0;
            row["UserId"] = GblIQCare.AppUserId;
            row["CountryId"] = GblIQCare.AppCountryId;
            row["SystemId"] = 1;
            row["ModuleId"] = 0; 
            row["MultiVisit"] = 0;

            dtMstFeature.Rows.Add(row);
            //For FieldName Validation
            DataView theDV = new DataView(dsFieldDetails.Tables[0]);
            theDV.RowFilter = "Predefine=1";
            DataTable theDTFieldName = theDV.ToTable();

            //set lnk_form & mst_section values
            DataTable dtLnk_Form = new DataTable();
            DataTable dtMstSection = new DataTable();
            dtLnk_Form = clsCommon.CreateTableLnk_Forms();
            dtMstSection = clsCommon.CreateTableMstSection();
            DataRow rowLnk_Form;
            DataRow rowMstSection;

            int iSectionRowCounter = 0;
            int iSecSequence = 1;
            foreach (Control ctrlControls in pnlMainPanel.Controls)
            {
                if (ctrlControls is Panel && ctrlControls.Visible != false)
                {
                    //for each panel execute code
                    foreach (Control ctrl in ctrlControls.Controls)
                    {
                        //section name
                        if (ctrl is TextBox)
                        {
                            if (ctrl.Text.ToString().Trim() != "")
                            {
                                int iSecSequenceReset = 0;
                                string[] strNameSplit;
                                strNameSplit = ctrlControls.Name.Split('_');
                                if (strNameSplit.Length > 2)
                                    iSecSequenceReset = System.Convert.ToInt32(strNameSplit[2]);

                                int iSecIdInUpdateMode = 0;
                                rowMstSection = dtMstSection.NewRow();
                                //sectionid in update mode
                                if (GblIQCare.iFormBuilderId != 0)
                                {
                                    iSecIdInUpdateMode = System.Convert.ToInt32(ctrl.Name.Split('_')[2]);

                                    if (iSecIdInUpdateMode > 0)
                                    {
                                        rowMstSection["SectionId"] = iSecIdInUpdateMode;
                                    }
                                    else if (iSectionId > 0)
                                    {
                                        iSectionId += 1;
                                    }
                                    else
                                    {
                                        IFormBuilder objFormBuilder;
                                        objFormBuilder = (IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder");
                                        iSectionId = objFormBuilder.RetrieveMaxId(PMTCTConstants.strMstSection);
                                        iSectionId += 1;
                                    }

                                    rowMstSection["InsertUpdateStatus"] = PMTCTConstants.strUpdate;

                                }
         
                                if (iSecIdInUpdateMode > 0)
                                    rowMstSection["SectionId"] = iSecIdInUpdateMode;
                                else
                                    rowMstSection["SectionId"] = iSectionId;

                                rowMstSection["SectionName"] = ctrl.Text.ToString().Trim();
                                if (iSecSequenceReset > 0)
                                    rowMstSection["Sequence"] = iSecSequenceReset;
                                else
                                    rowMstSection["Sequence"] = iSecSequence;

                                rowMstSection["CustomFlag"] = 0;
                                rowMstSection["DeleteFlag"] = 0;
                                rowMstSection["UserId"] = GblIQCare.AppUserId;
                                rowMstSection["FeatureId"] = iFormId;
                                dtMstSection.Rows.Add(rowMstSection);

                                iSecSequence += 1;
                            }
                            else if (blnSaveData != false) //if section empty,show error,but error status shud not be already false
                            {
                                IQCareWindowMsgBox.ShowWindow("PMTCTSectionNameMandatory", this);
                                ctrl.Focus();
                                blnSaveData = false;
                            }
                        } //End of textbox control

                    }//end of foreach inside panel control

                    int iSeq = 1;
                    //for loop again to fetch datagrid details
                    foreach (Control ctrl in ctrlControls.Controls)
                    {

                        if (ctrl is DataGridView)
                        {
                            DataGridView dgwGridData = ctrl as DataGridView;
                            if (dgwGridData.Rows.Count > 0 && dgwGridData.Rows[0].Cells[0].Value != null)
                            {
                                //foreach (DataGridView dgwGridData in ctrl)
                                for (int iGridRow = 0; iGridRow < dgwGridData.Rows.Count; iGridRow++)
                                {

                                    rowLnk_Form = dtLnk_Form.NewRow();
                                    if (dgwGridData.Rows[iGridRow].Cells[0].Value != null && blnSaveData != false)
                                    {
                                        if (dgwGridData.Rows[iGridRow].Cells["Field Label"].Value != null && dgwGridData.Rows[iGridRow].Cells["Field Label"].Value.ToString().Trim() != "")
                                        {
                                            //update mode of field
                                            if (GblIQCare.iFormBuilderId != 0 && dgwGridData.Rows[iGridRow].Cells["ID"].Value != null && dgwGridData.Rows[iGridRow].Cells["ID"].Value.ToString() != "")
                                            {
                                                rowLnk_Form["InsertUpdateStatus"] = PMTCTConstants.strUpdate;
                                            }
                                            else //insert mode
                                            {
                                                rowLnk_Form["InsertUpdateStatus"] = PMTCTConstants.strInsert;
                                            }

                                            //update mode
                                            if (GblIQCare.iFormBuilderId > 0)
                                            {
                                                if (dgwGridData.Rows[iGridRow].Cells["Id"].Value == null)
                                                    rowLnk_Form["Id"] = "0";
                                                else
                                                    rowLnk_Form["Id"] = dgwGridData.Rows[iGridRow].Cells["Id"].Value;
                                                    rowLnk_Form["FieldId"] = dgwGridData.Rows[iGridRow].Cells["FieldId"].Value;
                                            }
                                            rowLnk_Form["FeatureId"] = iFormId;
                                            rowLnk_Form["SectionId"] = dtMstSection.Rows[iSectionRowCounter]["SectionId"];
                                            rowLnk_Form["FieldLabel"] = dgwGridData.Rows[iGridRow].Cells["Field Label"].Value.ToString().Trim();
                                            rowLnk_Form["Sequence"] = iSeq;
                                            rowLnk_Form["Predefined"] = dgwGridData.Rows[iGridRow].Cells["Predefined"].Value;
                                            rowLnk_Form["UserId"] = GblIQCare.AppUserId;
                                            dtLnk_Form.Rows.Add(rowLnk_Form);
                                            iSeq += 1;
                                        }
                                        else if (blnSaveData != false) // error status shud not be false
                                        {
                                            //error message for field name not entered.
                                            IQCareWindowMsgBox.ShowWindow("PMTCTFieldLabelMandatory", this);
                                            //set focus on field label
                                            blnSaveData = false;
                                        }
                                    }

                                }//end of foreach of datagrid 
                                //first grid data goes in first section, and so on.
                            }//if field exist in section i.e. dgwGridData.rows.count>0
                            else if (blnSaveData != false) //else if field doesnot exist in section then show error message
                            {
                                IQCareWindowMsgBox.ShowWindow("PMTCTNoFieldCreated", this);
                                blnSaveData = false;
                            }
                            iSectionRowCounter += 1;
                        }//endif of datagridview check
                    }
                }//endif of panel control check

            }//end of foreach panel


            if (dtMstSection.Rows.Count > 0)
            {
                //check for duplicate section name
                //section name cannot be duplicate on one form. one section name can be same on two different forms.
                for (int iDupSec = 0; iDupSec < dtMstSection.Rows.Count; iDupSec++)
                {
                    for (int x = iDupSec + 1; x < dtMstSection.Rows.Count; x++)
                    {
                        if (dtMstSection.Rows[iDupSec]["SectionName"].ToString() == dtMstSection.Rows[x]["SectionName"].ToString() && blnSaveData != false)
                        {
                            //pass message builder message
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["Control"] = dtMstSection.Rows[iDupSec]["SectionName"].ToString();
                            IQCareWindowMsgBox.ShowWindow("PMTCTDupSectionName", theBuilder, this);
                            blnSaveData = false;
                            break;
                        }
                    }
                }
            }
            else if (blnSaveData != false)
            {
                //at least one section should exists
                IQCareWindowMsgBox.ShowWindow("PMTCTNoSectionCreated", this);
                blnSaveData = false;
            }

            //check for duplicate field in same form
            for (int iDupFld = 0; iDupFld < dtLnk_Form.Rows.Count; iDupFld++)
            {
                for (int x = iDupFld + 1; x < dtLnk_Form.Rows.Count; x++)
                {
                    //two field id can be same in case of one is custom field and another is predefined field,
                    //so need to check predefined check also while comparing duplicate field
                    if (dtLnk_Form.Rows[iDupFld]["FieldId"].ToString() == dtLnk_Form.Rows[x]["FieldId"].ToString() && dtLnk_Form.Rows[iDupFld]["Predefined"].ToString() == dtLnk_Form.Rows[x]["Predefined"].ToString() && blnSaveData != false)
                    {
                        //pass message builder message
                        if (dtLnk_Form.Rows[iDupFld]["FieldLabel"].ToString() != "PlaceHolder")
                        {
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["MessageText"] = "Field Label " + dtLnk_Form.Rows[iDupFld]["FieldLabel"].ToString() + " and " + dtLnk_Form.Rows[x]["FieldLabel"].ToString() + " belongs to same field.";
                            IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                            blnSaveData = false;
                            break;
                        }
                    }
                    //duplicate label should not be allowed
                    if (dtLnk_Form.Rows[iDupFld]["FieldLabel"].ToString() == dtLnk_Form.Rows[x]["FieldLabel"].ToString() && blnSaveData != false)
                    {
                        if (dtLnk_Form.Rows[iDupFld]["FieldLabel"].ToString() != "PlaceHolder")
                        {
                            IQCareWindowMsgBox.ShowWindow("PMTCTDupFieldLabel", this);
                            blnSaveData = false;
                            break;
                        }
                    }
                }
                ////duplicate field name should not be allowed
                try
                {
                    string strfieldname = GetBindFieldName(theDTFieldName, dtLnk_Form.Rows[iDupFld]["FieldId"].ToString());
                    if (strfieldname != "")
                    {
                        theHTDuplicatefield.Add(strfieldname.ToUpper().Trim(), strfieldname.ToUpper().Trim());

                    }
                }
                catch //(Exception ex)
                {
                    blnSaveData = false;
                }
                if (!(blnSaveData))
                {
                    IQCareWindowMsgBox.ShowWindow("PMTCTDupFieldName", this);
                    blnSaveData = false;
                    break;
                }
            }

            // for checking duplicate field in conditional field
            DataSet dscondfield = dsFieldDetails.Copy();
            DataView dvFilteredRowcondfield = new DataView();
            dvFilteredRowcondfield = dscondfield.Tables[5].DefaultView;
            for (int iDupFld = 0; iDupFld < dtLnk_Form.Rows.Count; iDupFld++)
            {
                for (int x = 0; x < dtLnk_Form.Rows.Count; x++)
                {
                    dvFilteredRowcondfield.RowFilter = "ConfieldId=" + dtLnk_Form.Rows[iDupFld]["FieldID"].ToString() + " and FieldId=" + dtLnk_Form.Rows[x]["FieldID"].ToString();
                    if (dvFilteredRowcondfield.ToTable().Rows.Count > 0)
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["MessageText"] = "Field Label " + dtLnk_Form.Rows[x]["FieldLabel"].ToString() + " is associate with Field Label " + dtLnk_Form.Rows[iDupFld]["FieldLabel"].ToString() + ".";
                        IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                        blnSaveData = false;
                        break;
                    }

                }
            }
            DataSet dscond = dsFieldDetails.Copy();
            DataSet dscond1 = dsFieldDetails.Copy();
            if (blnSaveData)
            {
                for (int iDupFld = 0; iDupFld < dtLnk_Form.Rows.Count; iDupFld++)
                {
                    DataView dvFilteredRowcondfield1 = new DataView();
                    dvFilteredRowcondfield1 = dscond.Tables[5].DefaultView;
                    dvFilteredRowcondfield1.RowFilter = "ConfieldId=" + dtLnk_Form.Rows[iDupFld]["FieldID"].ToString();
                    for (int x = iDupFld + 1; x < dtLnk_Form.Rows.Count; x++)
                    {
                        DataView dvFilteredRowcondfield2 = new DataView();
                        dvFilteredRowcondfield2 = dscond1.Tables[5].DefaultView;

                        dvFilteredRowcondfield2.RowFilter = "ConfieldId=" + dtLnk_Form.Rows[x]["FieldID"].ToString();
                        for (int y = 0; y < dvFilteredRowcondfield1.ToTable().Rows.Count; y++)
                        {
                            for (int z = 0; z < dvFilteredRowcondfield2.ToTable().Rows.Count; z++)
                            {
                                if (dvFilteredRowcondfield1.ToTable().Rows[y]["FieldID"].ToString() == dvFilteredRowcondfield2.ToTable().Rows[z]["FieldID"].ToString())
                                {
                                    MsgBuilder theBuilder = new MsgBuilder();
                                    theBuilder.DataElements["MessageText"] = "Field Label " + dtLnk_Form.Rows[iDupFld]["FieldLabel"].ToString() + " and " + dtLnk_Form.Rows[x]["FieldLabel"].ToString() + " has common associate field(s).";
                                    IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                                    blnSaveData = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            //call save method
            if (blnSaveData)
            {
                dsCollectDataToSave.Tables.Add(dtMstFeature);
                dsCollectDataToSave.Tables.Add(dtMstSection);
                dsCollectDataToSave.Tables.Add(dtLnk_Form);
                dsCollectDataToSave.Tables.Add(dtDeleteFields);
                if (dtDeleteSections != null)
                {
                    dtDeleteSections.TableName = "DeleteSection";
                    dsCollectDataToSave.Tables.Add(dtDeleteSections);
                }

                IFormBuilder objFormBuilder;
                objFormBuilder = (IFormBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFormBuilder,BusinessProcess.FormBuilder");
                int res = objFormBuilder.SaveFormDetail(dsCollectDataToSave, dsFieldDetails.Tables[0]);
                GblIQCare.iFormBuilderId = 0;

                Form theForm;
                theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmPatientRegistrationManageForms, IQCare.FormBuilder"));
                theForm.MdiParent = this.MdiParent;
                theForm.Left = 0;
                theForm.Top = 0;
                theForm.Focus();
                theForm.Show();

                this.Close();
            }
            btnSave.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            GblIQCare.iFormBuilderId = 0;
            if (dtDeleteFields.Rows.Count > 0)
                dtDeleteFields.Clear();
            dsUpdateMode.Clear();
            dsFieldDetails.Clear();
            if (dtDeleteSections != null)
            {
                if (dtDeleteSections.Rows.Count > 0)
                    dtDeleteSections.Clear();
            }
            if (dtMstFeatureForManageField != null)
            {
                if (dtMstFeatureForManageField.Rows.Count > 0)
                    dtMstFeatureForManageField.Clear();
            }
            if (dtMstSectionForManageField != null)
            {
                if (dtMstSectionForManageField.Rows.Count > 0)
                    dtMstSectionForManageField.Clear();
            }
            if (dtLnkFormsForManageField != null)
            {
                if (dtLnkFormsForManageField.Rows.Count > 0)
                    dtLnkFormsForManageField.Clear();
            }
            Form theForm;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmPatientRegistrationManageForms, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
            this.Close();
        }

        private void btnManageSection_Click(object sender, EventArgs e)
        {

            pnlMainPanel.AutoScrollPosition = new Point(0, 0);
            //retrieve all section name with positions
            if (dtManageSectionPos != null)
            {
                dtManageSectionPos.Clear();
            }
            dtManageSectionPos = clsCommon.ManageSectionPos();
            foreach (Control ctrlControls in pnlMainPanel.Controls)
            {
                if (ctrlControls is Panel && ctrlControls.Visible != false)
                {
                    //for loop again to fetch datagrid details
                    foreach (Control ctrl in ctrlControls.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            if (ctrl.Text.ToString().Trim() != "")
                            {
                                DataRow row;
                                row = dtManageSectionPos.NewRow();
                                if (GblIQCare.iFormBuilderId > 0)
                                {
                                    string[] strSecId;
                                    strSecId = ctrl.Name.Split('_');
                                    if (strSecId.Length > 1)
                                    {
                                        if (System.Convert.ToInt32(strSecId[2]) > 0)
                                        {
                                            row["SectionId"] = strSecId[2];
                                        }
                                    }
                                }
                                row["SectionName"] = ctrl.Text.ToString().Trim();
                                row["TopPos"] = ctrlControls.Top;
                                row["DeleteFlag"] = 0;
                                dtManageSectionPos.Rows.Add(row);
                            }

                        }
                    }// end of foreach of controls at section level
                }
            }//end of foreach of mainpanel control


            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmManageSection, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Deactivate += new EventHandler(theForm_Deactivate);
            //theForm.Deactivate += new EventHandler(FrmManageSectionHideChildOnLostFocus);
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Focus();
            theForm.Show();

        }

        void theForm_Deactivate(object sender, EventArgs e)
        {
            Form theSenderForm = sender as Form;
            theSenderForm.Close();
            GblIQCare.ResetSection = 1;
            frmPatientRegistrationFormBuilder_Load(sender, e);
        }

        private void btnAddCustomField_Click(object sender, EventArgs e)
        {
            //store all form data in datatable to maintain form state.

            if (dtMstFeatureForManageField != null)
            {
                if (dtMstFeatureForManageField.Rows.Count > 0)
                    dtMstFeatureForManageField.Clear();
            }
            if (dtMstSectionForManageField != null)
            {
                if (dtMstSectionForManageField.Rows.Count > 0)
                    dtMstSectionForManageField.Clear();
            }
            if (dtLnkFormsForManageField != null)
            {
                if (dtLnkFormsForManageField.Rows.Count > 0)
                    dtLnkFormsForManageField.Clear();
            }

            DataRow drRow;
            dtMstFeatureForManageField = clsCommon.CreateTableMstFeature();
            drRow = dtMstFeatureForManageField.NewRow();
            drRow["FeatureId"] = GblIQCare.iFormBuilderId;
            drRow["FeatureName"] = txtFormName.Text.Trim();
            dtMstFeatureForManageField.Rows.Add(drRow);

            dtMstSectionForManageField = clsCommon.CreateTableMstSection();
            dtLnkFormsForManageField = clsCommon.CreateTableLnk_Forms();

            DataRow rowMstSection;
            DataRow rowLnk_Form;

            int iSecSequence = 0;
            int iSectionRowCounter = 0;
            foreach (Control ctrlControls in pnlMainPanel.Controls)
            {
                if (ctrlControls is Panel && ctrlControls.Visible != false)
                {
                    //for each panel execute code
                    foreach (Control ctrl in ctrlControls.Controls)
                    {
                        //section name
                        if (ctrl is TextBox)
                        {
                            if (ctrl.Text.ToString().Trim() != "")
                            {
                                int iSecSequenceReset = 0;
                                string[] strNameSplit;
                                strNameSplit = ctrlControls.Name.Split('_');
                                if (strNameSplit.Length > 2)
                                    iSecSequenceReset = System.Convert.ToInt32(strNameSplit[2]);

                                int iSecIdInUpdateMode = 0;
                                rowMstSection = dtMstSectionForManageField.NewRow();
                                //sectionid in update mode
                                if (GblIQCare.iFormBuilderId != 0)
                                {
                                    iSecIdInUpdateMode = System.Convert.ToInt32(ctrl.Name.Split('_')[2]);

                                    if (iSecIdInUpdateMode > 0)
                                    {
                                        rowMstSection["SectionId"] = iSecIdInUpdateMode;
                                    }
                                    rowMstSection["InsertUpdateStatus"] = PMTCTConstants.strUpdate;

                                }

                                rowMstSection["SectionName"] = ctrl.Text.ToString().Trim();
                                if (iSecSequenceReset > 0)
                                    rowMstSection["Sequence"] = iSecSequenceReset;
                                else
                                    rowMstSection["Sequence"] = iSecSequence;

                                rowMstSection["CustomFlag"] = 0;
                                rowMstSection["DeleteFlag"] = 0;
                                rowMstSection["UserId"] = GblIQCare.AppUserId;
                                rowMstSection["FeatureId"] = GblIQCare.iFormBuilderId;
                                dtMstSectionForManageField.Rows.Add(rowMstSection);

                                iSecSequence += 1;
                            }

                        } //End of textbox control

                    }//end of foreach inside panel control

                    int iSeq = 1;
                    //for loop again to fetch datagrid details
                    foreach (Control ctrl in ctrlControls.Controls)
                    {

                        if (ctrl is DataGridView)
                        {
                            DataGridView dgwGridData = ctrl as DataGridView;
                            if (dgwGridData.Rows.Count > 0)
                            {
                                //foreach (DataGridView dgwGridData in ctrl)
                                for (int iGridRow = 0; iGridRow < dgwGridData.Rows.Count; iGridRow++)
                                {
                                    rowLnk_Form = dtLnkFormsForManageField.NewRow();
                                    if (dgwGridData.Rows[iGridRow].Cells[0].Value != null && dgwGridData.Rows[iGridRow].Cells["Field Label"].Value != null)
                                    {
                                        //update mode of field
                                        if (GblIQCare.iFormBuilderId != 0 && dgwGridData.Rows[iGridRow].Cells["ID"].Value != null && dgwGridData.Rows[iGridRow].Cells["ID"].Value.ToString() != "")
                                        {
                                            rowLnk_Form["InsertUpdateStatus"] = PMTCTConstants.strUpdate;
                                            rowLnk_Form["Id"] = dgwGridData.Rows[iGridRow].Cells["ID"].Value;
                                        }
                                        else //insert mode
                                        {
                                            rowLnk_Form["InsertUpdateStatus"] = PMTCTConstants.strInsert;
                                        }

                                        //update mode
                                        if (GblIQCare.iFormBuilderId > 0)
                                        {

                                            DataSet dsSetFieldDetail = dsFieldDetails.Copy();
                                            DataView dvFilteredRow = new DataView();
                                            dvFilteredRow = dsSetFieldDetail.Tables[0].DefaultView;
                                            DataTable dtRow = new DataTable();

                                            dvFilteredRow.RowFilter = "FieldName='" + dgwGridData.Rows[iGridRow].Cells["Table Field Name"].Value + "'";
                                            dtRow = dvFilteredRow.ToTable();
                                            rowLnk_Form["FieldId"] = dtRow.Rows[0]["Id"];
                                        }
                                        else //insert mode
                                        {
                                            rowLnk_Form["FieldId"] = dgwGridData.Rows[iGridRow].Cells["FieldId"].Value.ToString().Trim();
                                        }
                                        rowLnk_Form["FieldName"] = dgwGridData.Rows[iGridRow].Cells["Table Field Name"].Value.ToString().Trim();
                                        rowLnk_Form["FeatureId"] = GblIQCare.iFormBuilderId;
                                        rowLnk_Form["SectionId"] = dtMstSectionForManageField.Rows[iSectionRowCounter]["SectionId"];
                                        rowLnk_Form["FieldLabel"] = dgwGridData.Rows[iGridRow].Cells["Field Label"].Value.ToString().Trim();
                                        rowLnk_Form["Sequence"] = iSeq;
                                        rowLnk_Form["Predefined"] = dgwGridData.Rows[iGridRow].Cells["Predefined"].Value;
                                        //rowLnk_Form["UserId"] = GblIQCare.AppUserId;
                                        dtLnkFormsForManageField.Rows.Add(rowLnk_Form);

                                        iSeq += 1;
                                    }

                                }//end of foreach of datagrid 
                                //first grid data goes in first section, and so on.
                            }//if field exist in section i.e. dgwGridData.rows.count>0

                            iSectionRowCounter += 1;
                        }//endif of datagridview check
                    }
                }//endif of panel control check

            }//end of foreach panel

            Form theForm;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmFieldDetails, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Deactivate += new EventHandler(FrmManageFieldHideChildOnLostFocus);
            //theForm.LostFocus += new EventHandler(frmLostFocus);
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Focus();
            theForm.Show();
        }

        private void FrmManageFieldHideChildOnLostFocus(object sender, EventArgs e)
        {
            Form theSenderForm = sender as Form;
            theSenderForm.Close();
            GblIQCare.RefillDdlFields = 1;
            frmPatientRegistrationFormBuilder_Load(sender, e);
        }

        private string GetBindFieldName(DataTable theDT, string ID)
        {
            string strFieldName = "";
            DataRow[] theRow = theDT.Select("ID=" + ID + "");
            if (theRow.Length > 0)
            {
                strFieldName = theRow[0]["PDFTableName"].ToString() + "^" + theRow[0]["Bindfieldname"].ToString();
            }
            return strFieldName;
        }
    }
}
