using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Presentation;
using Application.Common;
using Interface.FormBuilder;

namespace IQCare.FormBuilder
{
    public partial class UserCtrlFormBuilder : UserControl
    {
        clsCssStyle theStyle = new clsCssStyle();

        //fixed dimenstions of the panel
        const int iPanelLeft = 12;
        const int iPanelHeight = 440; //470;
        const int iPanelWidth = 935;

        //fixed dimenstions of the grid
        const int iDataGridLeft = 13;
        const int iDataGridHeight = 300; //370;
        const int iDataGridWidth = 880;
        const int iDataGridTop = 80;

        int iPanelCount = 0;
        int iControlCount = 0;
        //position of panel from the top, keeps on changing as new panel adds to the form.
        //By default first panel value is stored.
        int iPanelTop = 14;
        int iSelectedFieldRowId = 0;
        bool IsHandleAdded;

        DataGridViewRow dgwRemoveSelectedRow = new DataGridViewRow();

      //  DataSet dsFieldDetails = new DataSet();

        DataSet dsCollectDataToSave = new DataSet();
       
        DataTable dtDeleteFields;
        DataTable dtDeleteSections;
      //  public static DataTable dtManageSectionPos;

       DataTable dtMstFeatureForManageField = new DataTable();
       DataTable dtMstSectionForManageField= new DataTable();
         DataTable dtLnkFormsForManageField= new DataTable(); //for fields

        static string strGetPath = GblIQCare.GetPath();
        Image imgList = Image.FromFile(strGetPath + "\\List.bmp");
        Image imgDisabledList = Image.FromFile(strGetPath + "\\listdesible.bmp");
        Image imgBusinessRule = Image.FromFile(strGetPath + "\\brule.bmp");
        Image imgInactiveBR = Image.FromFile(strGetPath + "\\nonactive.bmp");
        Image imgButtonUp = Image.FromFile(strGetPath + "\\btnUp.Image.gif");
        Image imgButtonDown = Image.FromFile(strGetPath + "\\btnDown.Image.gif");

        public frmFormBuilder propParentForm { get; set; }

        public String propFormName { get; set; }
        public Int32 propTechAreaID { get; set; }
        public bool propSingleVisit { get; set; }
        public bool propMultipleVisit { get; set; }

        private  static DataTable _dtManageSectionPos = new DataTable();
        public static  DataTable dtManageSectionPos
        {
            get
            {
                return _dtManageSectionPos;
            }
            set
            {
                _dtManageSectionPos = value;
            }
        }

        private DataSet _dsFieldDetails = new DataSet();
        public DataSet dsFieldDetails
        {
              get
            {
                return _dsFieldDetails;
            }
            set {
                _dsFieldDetails = value;
            }
        }



        private DataSet _dsUpdateMode = new DataSet();
        public DataSet dsUpdateMode
        {
            get
            {
                return _dsUpdateMode;
            }
            set
            {
                _dsUpdateMode = value;
            }
        }

        public UserCtrlFormBuilder()
        {
            InitializeComponent();
        }

       

        private void RestoreFormState()
        {
            TextBox txtFormName = (propParentForm.Controls["txtFormName"] as TextBox);
            ComboBox cmbTechArea = (propParentForm.Controls["cmbTechArea"] as ComboBox);
            RadioButton rdoSingleVisit = (propParentForm.Controls["rdoSingleVisit"] as RadioButton);
            RadioButton rdoMultipleVisit = (propParentForm.Controls["rdoMultipleVisit"] as RadioButton);
            
            pnlMainPanel.Controls.Clear();

            iPanelCount = 0;
            iControlCount = 0;
            iPanelTop = 14;

            if (dtMstFeatureForManageField.Rows.Count > 0)
                txtFormName.Text = dtMstFeatureForManageField.Rows[0]["FeatureName"].ToString();
            //mst section
            if (dtMstSectionForManageField.Rows.Count > 0)
            {
                for (int i = 0; i < dtMstSectionForManageField.Rows.Count; i++)
                {
                    if (GblIQCare.iFormBuilderId > 0)
                        CreatePanel(PMTCTConstants.strUpdate);
                    else
                        CreatePanel(PMTCTConstants.strInsert);
                }
            }

            if (dtLnkFormsForManageField.Rows.Count > 0)
            {
                int iDsRow = 0;
                int iSecCount = 0;
                foreach (Control ctrlControls in pnlMainPanel.Controls)
                {
                    if (ctrlControls is Panel && ctrlControls.Visible != false)
                    {
                        //for loop again to fetch datagrid details
                        foreach (Control ctrl in ctrlControls.Controls)
                        {
                            if (ctrl is TextBox)
                            {
                                ctrl.Text = dtMstSectionForManageField.Rows[iSecCount]["SectionName"].ToString();
                                iSecCount += 1;
                            }
                            if (ctrl is DataGridView)
                            {
                                DataGridView dgwGridData = ctrl as DataGridView;
                                // clsCommon.CreateGridColumnsForFormBuilder(dgwGridData, dsFieldDetails);
                                //dgwGridData.DataSource = dsUpdateMode.Tables[2];
                                DataTable dtLnk_Form = new DataTable();
                                dtLnk_Form = clsCommon.CreateTableLnk_Forms();
                                if (dtLnkFormsForManageField.Rows.Count > 0)
                                {

                                    foreach (DataRow drFields in dtLnkFormsForManageField.Rows)
                                    {
                                        if (dtMstSectionForManageField.Rows[iSecCount]["SectionId"].ToString() == drFields["SectionId"].ToString())
                                        {
                                            DataRow row;
                                            row = dtLnk_Form.NewRow();
                                            if (drFields["ID"] != null && drFields["ID"].ToString() != "")
                                                row["id"] = Convert.ToInt32(drFields["ID"].ToString());

                                            row["FeatureId"] = drFields["featureId"].ToString();
                                            if (drFields["SectionId"] != null && drFields["SectionId"].ToString() != "")
                                                row["SectionId"] = drFields["SectionId"].ToString();

                                            //row["ControlId"] = clsCommon.FetchSearchValue("Id", drFields["FieldId"].ToString(), dsFieldDetails.Tables[0]).Rows[0]["ControlId"];
                                            row["FieldId"] = Convert.ToInt32(drFields["FieldId"].ToString());
                                            row["FieldLabel"] = drFields["FieldLabel"].ToString();
                                            // row["UserId"] = drFields["UserId"].ToString();
                                            row["Predefined"] = drFields["Predefined"].ToString();
                                            // row["InsertUpdateStatus"] = PMTCTConstants.strUpdate;
                                            dtLnk_Form.Rows.Add(row);
                                        }

                                    }
                                    iDsRow += 1;
                                    //To set value for business rule eixst and selectlist exist for the field and add data in grid
                                    foreach (DataRow drFields in dtLnk_Form.Rows)
                                    {
                                        DataSet dsSetFieldDetail = dsFieldDetails.Copy();
                                        DataView dvFilteredRow = new DataView();
                                        dvFilteredRow = dsSetFieldDetail.Tables[0].DefaultView;
                                        DataTable dtRow = new DataTable();

                                        dvFilteredRow.RowFilter = "ID='" + drFields["FieldId"].ToString() + "' and predefine=" + drFields["Predefined"].ToString();

                                        dtRow = dvFilteredRow.ToTable();
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
                            }//endif of datagridview check
                        }
                    }//endif of panel control check

                }//end of foreach panel
                pnlMainPanel.AutoScrollPosition = new Point(0, 0);
            }
        }
        
        private void btnAddSection_Click(object sender, EventArgs e)
        {

                CreatePanel(PMTCTConstants.strInsert);
             }

        public void CreatePanel(string strMode)
        {
            if (iPanelCount >= 1)
            {
                pnlMainPanel.AutoScrollPosition = new Point(0, 0);
            }
            Panel pnlDynamicPanel = new Panel();
            pnlDynamicPanel.Name = "pnlDynamicPanel_" + iPanelCount;

            //add new panel into the main panel of form.
            //pnlDynamicPanel.Width = iPanelWidth;
            //pnlDynamicPanel.Height = iPanelHeight;
            pnlDynamicPanel.Width = iPanelWidth;
            pnlDynamicPanel.Height = iPanelHeight -20;
            pnlDynamicPanel.Left = iPanelLeft;
            pnlDynamicPanel.Top = iPanelTop;

            pnlMainPanel.Controls.Add(pnlDynamicPanel);

            ////add controls in new panel
            DataGridView dgwDataGrid = new DataGridView();
            dgwDataGrid.Name = "dgwDataGrid_" + iControlCount;
            //dgwDataGrid.Name = "dgwDataGrid_" + iPanelCount;
            dgwDataGrid.Height = iDataGridHeight;
            dgwDataGrid.Width = iDataGridWidth;
            dgwDataGrid.Left = iDataGridLeft;
            dgwDataGrid.Top = iDataGridTop - 10;
            dgwDataGrid.CellFormatting += new DataGridViewCellFormattingEventHandler(GridCellFormatting);
            dgwDataGrid.CellValueChanged += new DataGridViewCellEventHandler(GridCellValueChanged);
            //dgwDataGrid.CurrentCellDirtyStateChanged += new EventHandler(CurrentCellDirtyStateChanged);
            //dgwDataGrid.CellValueNeeded += new DataGridViewCellValueEventHandler(CellValueNeeded);
            dgwDataGrid.CellClick += new DataGridViewCellEventHandler(GridCellClick);
            //DataError event is required to handle error while rebinding grid specialy in drop down case,
            //just an empty even wud be enough to handle error
            dgwDataGrid.DataError += new DataGridViewDataErrorEventHandler(OnDataError);
            //for adding event in control inside the grid
            dgwDataGrid.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgwDataGrid_EditingControlShowing);
            dgwDataGrid.Tag = "dgwDataGridView";
            dgwDataGrid.AllowUserToResizeColumns = false;
            dgwDataGrid.AllowUserToResizeRows = false;
            dgwDataGrid.AllowUserToAddRows = false;

            ////add grid in new panel
            pnlDynamicPanel.Controls.Add(dgwDataGrid);

            clsCommon.CreateGridColumnsForFormBuilder(dgwDataGrid, dsFieldDetails);
            //create grid columns
            //bind grid with datatable

            //add label in new panel
            Label lblSectionName = new Label();
            lblSectionName.Name = "lblSectionName_" + iControlCount;
            lblSectionName.Height = 13;
            lblSectionName.Width = 80;
            lblSectionName.Left = iDataGridLeft;
            lblSectionName.Top = 5;
            lblSectionName.Text = "Name :";
            lblSectionName.Tag = "lblLabel"; // to set css for the label
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


            CheckBox chkGridView = new CheckBox();
            chkGridView.Name = "chkGridView_" + iControlCount + "_0";
            chkGridView.Text = "Grid View";
            chkGridView.Left = txtSectionName.Left + txtSectionName.Width + 30;
            chkGridView.Top = 5;
            chkGridView.Tag = "lblLabel";
            chkGridView.CheckedChanged += new EventHandler(chkGridView_CheckedChanged);

            if (strMode == PMTCTConstants.strUpdate)
            {
                if (dsUpdateMode.Tables[1].Rows.Count >= (iControlCount + 1))
                {
                    chkGridView.Name = "chkGridView_" + iControlCount + "_" + dsUpdateMode.Tables[1].Rows[iControlCount]["SectionId"].ToString();
                    if ((dsUpdateMode.Tables[1].Rows[iControlCount]["IsGridView"] != DBNull.Value) || (dsUpdateMode.Tables[1].Rows[iControlCount]["IsGridView"].ToString() != "0"))
                    {
                        chkGridView.Checked = dsUpdateMode.Tables[1].Rows[iControlCount]["IsGridView"].ToString() == "1" ? true : false;
                    } // txtSectionName.Text = dsUpdateMode.Tables[1].Rows[iControlCount]["SectionName"].ToString();
                    chkGridView.Enabled = false;
                }
            }

            pnlDynamicPanel.Controls.Add(chkGridView);

            Label labelInfo = new Label();
            labelInfo.Name = "lblSectionInfo_" + iControlCount;
            labelInfo.Height = 13;
            labelInfo.Width = 80;
            labelInfo.Left = iDataGridLeft;
            labelInfo.Top = lblSectionName.Top + lblSectionName.Height + 5;
            labelInfo.Text = "Instructions :";  
            labelInfo.Tag = "labelInfo"; // to set css for the label
            pnlDynamicPanel.Controls.Add(labelInfo);


            TextBox txtSectionInfo = new TextBox();
            txtSectionInfo.Name = "txtSectionInfo_" + iControlCount + "_0";
            txtSectionInfo.Height = 20;
            txtSectionInfo.Width = 700;             
            txtSectionInfo.Multiline = true;

            txtSectionInfo.Left = iDataGridLeft + labelInfo.Width + 5;
            txtSectionInfo.Top = txtSectionName.Top + txtSectionName.Height + 5; 
            txtSectionInfo.MaxLength = 250;
            txtSectionInfo.Tag = "txtSectionInfo";
            txtSectionInfo.KeyPress += new KeyPressEventHandler(txtSectionName_KeyPress);

            if (strMode == PMTCTConstants.strUpdate)
            {
                if (dsUpdateMode.Tables[1].Rows.Count >= (iControlCount + 1))
                {
                    txtSectionInfo.Name = "txtSectionInfo_" + iControlCount + "_" + dsUpdateMode.Tables[1].Rows[iControlCount]["SectionId"].ToString();
                    txtSectionInfo.Text = dsUpdateMode.Tables[1].Rows[iControlCount]["SectionInfo"].ToString();
                }
            }

            pnlDynamicPanel.Controls.Add(txtSectionInfo);


            //add button in new panel
            Button btnAddField = new Button();
            btnAddField.Name = "btnAddFiled_" + iControlCount;
            //btnAddField.Height = 21;
            //btnAddField.Width = 141;
            btnAddField.Height = 23;
            btnAddField.Width = 106;


            btnAddField.Left = 250 + iDataGridLeft + 100;
          //  btnAddField.Top = dgwDataGrid.Bottom + 20;
            btnAddField.Top = dgwDataGrid.Bottom + 5;
            btnAddField.Tag = "btnH25WFlexi";
            btnAddField.Text = "A&dd Field";
            btnAddField.Click += new EventHandler(AddField);
            pnlDynamicPanel.Controls.Add(btnAddField);

            Button btnRemoveField = new Button();
            btnRemoveField.Name = "btnRemoveField_" + iControlCount;
            //btnRemoveField.Height = 21;
            //btnRemoveField.Width = 141;
            btnRemoveField.Height = 23;
            btnRemoveField.Width = 106;

            btnRemoveField.Left = 300 + iDataGridLeft + btnAddField.Width + 53;
           // btnRemoveField.Top = dgwDataGrid.Bottom + 20;
            btnRemoveField.Top = dgwDataGrid.Bottom + 5;
            btnRemoveField.Tag = "btnH25WFlexi";
            btnRemoveField.Text = "&Remove Field";
            btnRemoveField.Click += new EventHandler(RemoveField);
            pnlDynamicPanel.Controls.Add(btnRemoveField);

            Button btnUp = new Button();
            btnUp.Name = "btnUp_" + iControlCount;
            btnUp.Height = 25;
            btnUp.Width = 25;
            btnUp.Left = dgwDataGrid.Right + 8;
            btnUp.Top = dgwDataGrid.Top + 100;
            btnUp.Tag = "btnH25WFlexi";
            btnUp.Image = imgButtonUp;
            btnUp.Click += new EventHandler(MoveUp);
            pnlDynamicPanel.Controls.Add(btnUp);

            Label lblMove = new Label();
            lblMove.Name = "lblMove_" + iControlCount;
            lblMove.Left = dgwDataGrid.Right + 2;
            lblMove.Top = dgwDataGrid.Top + 500 + btnUp.Height + 5;
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
            btnDown.Click += new EventHandler(MoveDown);
            pnlDynamicPanel.Controls.Add(btnDown);

            //after adding all the controls set the scrollbar to newly added panel
            pnlMainPanel.AutoScrollPosition = new Point(0, iPanelTop);


            ////after creation of panel, set the dimensions for the new panel
            iPanelTop = iPanelHeight + iPanelTop + 10;
            //iDataGridTop = iDataGridTop + 10;

            ////increment the counter 
            iControlCount = iControlCount + 1;
            iPanelCount = iPanelCount + 1;

            //set css for panel
            pnlDynamicPanel.Tag = "pnlSubPanel";

            theStyle.setStyle(pnlDynamicPanel);
        }
        private void chkGridView_CheckedChanged(Object sender, EventArgs e)
        {
            CheckBox chksender = sender as CheckBox;
            int iCounter = System.Convert.ToInt16(chksender.Name.ToString().Split('_')[1]);
            DataGridView dgwGrid = (DataGridView)pnlMainPanel.Controls.Find("dgwDataGrid_" + iCounter, true)[0];

            if (chksender.Checked)
            {
                if (dgwGrid.Rows.Count == 0)
                {
                    //foreach (DataGridViewRow theDR in dgwGrid.Rows)
                    dgwGrid.Columns.Clear();


                    IFieldDetail objFieldDetailgv;
                    objFieldDetailgv = (IFieldDetail)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BFieldDetails,BusinessProcess.FormBuilder");
                    //pass fieldName if want to retrieve specific field from GetCustomFields
                    DataSet dsFieldDetailsGridView = objFieldDetailgv.GetCustomFields("", 0, GblIQCare.iManageCareEnded, 1);
                    clsCommon.CreateGridColumnsForFormBuilder(dgwGrid, dsFieldDetailsGridView);
                }
            }
            if (dgwGrid.Rows.Count == 0)
            {
                if (!chksender.Checked)
                {
                    dgwGrid.Columns.Clear();
                    clsCommon.CreateGridColumnsForFormBuilder(dgwGrid, dsFieldDetails);
                }
            }
        }

        public void CreateField()
        {
            #region Add Field
            ///////////////////*************************************
            //lnk_froms
            if (dsUpdateMode.Tables[2].Rows.Count > 0)
            {
                int iDsRow = 0;
                foreach (Control ctrlControls in pnlMainPanel.Controls)
                {
                    //if (ctrlControls is Panel && ctrlControls.Visible != false)
                    if (ctrlControls is Panel && ctrlControls.Name.Contains("pnlDynamicPanel"))
                    {
                        //for loop again to fetch datagrid details
                        foreach (Control ctrl in ctrlControls.Controls)
                        {
                            if (ctrl is DataGridView)
                            {
                                DataGridView dgwGridData = ctrl as DataGridView;
                                // clsCommon.CreateGridColumnsForFormBuilder(dgwGridData, dsFieldDetails);
                                //dgwGridData.DataSource = dsUpdateMode.Tables[2];
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
                                            //row["ControlId"] = clsCommon.FetchSearchValue("Id", drFields["FieldId"].ToString(), dsFieldDetails.Tables[0]).Rows[0]["ControlId"];
                                            row["FieldId"] = Convert.ToInt32(drFields["FieldId"].ToString());
                                            row["FieldLabel"] = drFields["FieldLabel"].ToString();
                                            row["UserId"] = drFields["UserId"].ToString();
                                            row["Predefined"] = drFields["Predefined"].ToString();
                                            row["InsertUpdateStatus"] = PMTCTConstants.strUpdate;
                                            dtLnk_Form.Rows.Add(row);
                                        }

                                    }
                                    iDsRow += 1;
                                    //enter data in grid

                                    foreach (DataRow drFields in dtLnk_Form.Rows)
                                    {
                                        DataSet dsSetFieldDetail = dsFieldDetails.Copy();
                                        DataView dvFilteredRow = new DataView();
                                        dvFilteredRow = dsSetFieldDetail.Tables[0].DefaultView;
                                        DataTable dtRow = new DataTable();

                                        if (drFields["FieldId"].ToString().Contains("7100") == true)
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
            #endregion
        }

        void txtSectionName_KeyPress(object sender, KeyPressEventArgs e)
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

        void dgwDataGrid_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
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


        /// <summary>
        /// Required empty function,so that error dont come while setting value in grid for dropdown in update mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDataError(object sender, DataGridViewDataErrorEventArgs e)
        {

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
                        
                        //paritosh
                       // theForm.MdiParent = this.MdiParent;
                        theForm.MdiParent = propParentForm.MdiParent;

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
                        if (display.Trim() == "Select List" || display == "4" || display.Trim() == "Multi Select" || display == "9" || display.Trim() == "Select List TextBox" || display == "22")
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
                           //PARITOSH
                           // theForm.MdiParent = this.MdiParent;
                            theForm.MdiParent = propParentForm.MdiParent;
                           
                            theForm.Deactivate += new EventHandler(FrmHideChildOnLostFocus);
                            //theForm.Enabled = false;


                            theForm.Show();

                        }
                    }//select list check

                }
            }
        }

        private void AddField(object sender, EventArgs e)
        {
           //need to change 
           
            //if (cmbTechArea.SelectedIndex == 0)
            //{
            //    IQCareWindowMsgBox.ShowWindow("SelectTechnicalArea", this);
            //    cmbTechArea.Focus();
            //}
            //else
            //{
                Button btnSender = sender as Button;
                String strButtonName = btnSender.Name;
                int iCounter = System.Convert.ToInt16(btnSender.Name.ToString().Split('_')[1]);
                DataGridView dgwGrid = (DataGridView)pnlMainPanel.Controls.Find("dgwDataGrid_" + iCounter, true)[0];
                dgwGrid.AllowUserToAddRows = true;

                //dgwGrid.Rows.Add();

                int iRowCount = dgwGrid.Rows.Count;
                if (iRowCount > 0)
                {
                    dgwGrid.Rows[iRowCount - 1].DefaultCellStyle.BackColor = Color.White;
                    dgwGrid.Rows[iRowCount - 1].DefaultCellStyle.ForeColor = Color.Black;
                }
           // }

        }


        /// <summary>
        /// delete field from grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveField(object sender, EventArgs e)
        {
            string chkGridViewName;
            string StrSectionName = String.Empty;
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

                if (dsUpdateMode.Tables[1].Rows.Count >= (iCounter + 1))
                {
                    chkGridViewName = "chkGridView_" + iCounter + "_" + dsUpdateMode.Tables[1].Rows[iCounter]["SectionId"].ToString();
                    StrSectionName = dsUpdateMode.Tables[1].Rows[iCounter]["SectionName"].ToString();
                }
                else
                {
                    chkGridViewName = "chkGridView_" + iCounter + "_" + 0;
                }
                CheckBox chkGridView = (CheckBox)pnlMainPanel.Controls.Find(chkGridViewName, true)[0];
                drRemoveField["IsGridView"] = chkGridView.Checked ? "1" : "0";
                drRemoveField["SectionName"] = StrSectionName;
                dtDeleteFields.Rows.Add(drRemoveField);

               // propParentForm = (frmFormBuilder)this.Parent;
                frmFormBuilder.dtDeleteFields.Merge(dtDeleteFields);
             //   propParentForm.dtDeleteFields = dtDeleteFields;
                //iSelectedFieldRowId = 0;
            }
            if (dgwRemoveSelectedRow != null && dgwRemoveSelectedRow.Index != -1 && dgwRemoveSelectedRow.Cells[0].Value != null)
            {
                dgwGrid.Rows.Remove(dgwRemoveSelectedRow);
            }
        }

        /// <summary>
        /// DataGridViewCellValueEventArgs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GridCellValueChanged(object sender, DataGridViewCellEventArgs e)
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

                    if (dgwDataGrid.Rows[e.RowIndex].Cells["FieldId"].Value.ToString() == "999971")
                    {
                        //dgwDataGrid.Rows[e.RowIndex].Cells["FieldId"].Value = dgwDataGrid.Rows[e.RowIndex].Cells["FieldId"].Value + "00000" + e.RowIndex.ToString() + iControlCount.ToString();
                        dgwDataGrid.Rows[e.RowIndex].Cells["FieldId"].Value = dgwDataGrid.Rows[e.RowIndex].Cells["FieldId"].Value;
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
                    if (dtRow.Rows[0]["Id"].ToString() == "999971")
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
                    if (dtRow.Rows[0]["Id"].ToString() != "999971")
                    {
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
        }


        private void MoveUp(object sender, EventArgs e)
        {
            try
            {
                //Find datagrid
                Button btnSender = sender as Button;
                int iCounter = System.Convert.ToInt16(btnSender.Name.ToString().Split('_')[1]);
                DataGridView dgwGrid = (DataGridView)pnlMainPanel.Controls.Find("dgwDataGrid_" + iCounter, true)[0];

                if (dgwGrid.SelectedRows[0] != null)
                {
                   

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
                        //dgwGrid.Rows[currRow].Selected = true;
                    }
                }
            }
            catch
            {
                //on catch of exception of uncomited row cannot be deleted, do nothing and resume next.
                //error comes when try to remove downrow while datagrid, and grid has one empty row at end 
                //and user try to move last row to downward.
            }

        }

        private void MoveDown(object sender, EventArgs e)
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
                //on catch of exception of uncomited row cannot be deleted, do nothing and resume next.
                //error comes when try to remove downrow while datagrid, and grid has one empty row at end 
                //and user try to move last row to downward.
            }
        }
        
        //re arrange sections on the form
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
                                            ctrlControls.Tag = "deleted";

                                        }
                                        else
                                        {
                                            ctrlControls.Hide();
                                            ctrlControls.Tag = "deleted";
                                        }
                                    }
                                }
                            }

                        }
                    }// end of foreach of controls at section level
                }
            }//end of foreach of mainpanel control

            frmFormBuilder.dtDeleteSections.Merge(dtDeleteSections);
        }

        private void pnlPanel_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void pnlMainPanel_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void UserCtrlFormBuilder_Load(object sender, EventArgs e)
        {
          //  propParentForm =(frmFormBuilder)this.Parent;
         
            theStyle.setStyle(this);
            if (dtDeleteFields != null)
                dtDeleteFields.Clear();

            dtDeleteFields = new DataTable();
            //  dtDeleteFields = clsCommon.RemoveFieldLnk_Forms();
            dtDeleteFields = clsCommon.RemoveFieldLnkGridView_Forms();
            dtDeleteSections = clsCommon.ManageSectionPos();
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

            frmManageSection theForm = new frmManageSection();
           theForm.MdiParent = propParentForm.MdiParent;
            theForm.dtManageSectionPos = dtManageSectionPos; 
          //  theForm.dtManageSectionPos = dtManageSectionPos;
            theForm.Deactivate += new EventHandler(FrmManageSectionHideChildOnLostFocus);
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Focus();
            theForm.Show();
        }

        private void FrmHideChildOnLostFocus(object sender, EventArgs e)
        {
            Form theSenderForm = sender as Form;
            theSenderForm.Close();
            GblIQCare.iFormMode = 0;
        }
        private void FrmManageSectionHideChildOnLostFocus(object sender, EventArgs e)
        {
            Form theSenderForm = sender as Form;
            theSenderForm.Close();
            RefereshSection();
        }

      
    }
}
