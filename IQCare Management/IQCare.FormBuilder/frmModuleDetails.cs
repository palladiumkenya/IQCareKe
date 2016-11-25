using System;
using System.Collections;
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
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmModuleDetails : Form
    {
        /// <summary>
        /// The dt submit
        /// </summary>
        private DataTable dtSubmit = new DataTable();
        /// <summary>
        /// The int identifierid
        /// </summary>
        private int intIdentifierid = 0;
        /// <summary>
        /// The intlabel
        /// </summary>
        private int intlabel = 0;
        /// <summary>
        /// The int old identifierrow
        /// </summary>
        private int intOldIdentifierrow = 0;
        /// <summary>
        /// The int old module identifier
        /// </summary>
        private int intOldModuleId = 0;
        /// <summary>
        /// The int update flag
        /// </summary>
        private int intUpdateFlag = 0;
        /// <summary>
        /// The object ds identifier details
        /// </summary>
        private DataSet datasetIdentifierDetails = new DataSet();
        /// <summary>
        /// The string module name
        /// </summary>
        private string strModuleName;
        /// <summary>
        /// The string old module name
        /// </summary>
        private string strOldModuleName = "";
        private Form theForm;
        /// <summary>
        /// Initializes a new instance of the <see cref="frmModuleDetails"/> class.
        /// </summary>
        public frmModuleDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Binds the grid.
        /// </summary>
        public void BindGrid()
        {
            try
            {
                IModule objIdentifier;
                BindFunctions BindMgr = new BindFunctions();
                strModuleName = "";
                if (GblIQCare.ModuleName != "SMART ART FORM")
                {
                    strModuleName = txtModuleName.Text.Replace(" ", "");
                }
                else
                {
                    strModuleName = txtModuleName.Text;
                }

                objIdentifier = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");
                this.datasetIdentifierDetails = objIdentifier.GetModuleIdentifier(GblIQCare.ModuleId);
                if (this.datasetIdentifierDetails.Tables[0].Rows.Count > 0)
                {
                    BindFunctions theBind = new BindFunctions();
                    theBind.Win_BindCombo(this.cmbFieldType, this.datasetIdentifierDetails.Tables[0], "Name", "ControlId");
                    this.dtSubmit = this.datasetIdentifierDetails.Tables[1];
                }

                if (this.datasetIdentifierDetails.Tables[1].Rows.Count > 0)
                {
                    this.dtSubmit = this.datasetIdentifierDetails.Tables[1];
                    this.ClearGrid();
                    this.ShowGrid(this.datasetIdentifierDetails.Tables[1]);
                }
                if (this.datasetIdentifierDetails.Tables[2].Rows.Count > 0)
                {
                    btnBusinessRule.BackColor = System.Drawing.Color.Green;
                    GblIQCare.dtServiceBusinessValues = this.datasetIdentifierDetails.Tables[2];
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
        /// Saves the update module.
        /// </summary>
        public void SaveUpdateModule()
        {
            string Anthem = "Star Spangled Banner";
            Anthem = Anthem.Replace(" ", "");

            Hashtable theHT = new Hashtable();
            theHT.Clear();
            IModule objIdentifier;
            strModuleName = "";
            strModuleName = txtModuleName.Text;

            string strDisplayName = textDisplayName.Text;
            bool canEnrol = chkEnroll.Checked;
            Int32 pharmacyFlag = 0;
            //if (chkHivCareTrmt.Checked)
            //{
            pharmacyFlag = GblIQCare.PharmacyFlag;
            //}

            //if (GblIQCare.ModuleName != "SMART ART FORM")
            //{
            //    strModuleName = txtModuleName.Text.Replace(" ", "");
            //}
            //else
            //{
            //    strModuleName = txtModuleName.Text;

            //}
            if ((GblIQCare.ModuleId == 0) && (intOldModuleId != 0) && (strOldModuleName == strModuleName))
            {
                theHT.Add("ModuleId", intOldModuleId);
            }
            else
            {
                strOldModuleName = strModuleName;
                theHT.Add("ModuleId", GblIQCare.ModuleId);
            }

            theHT.Add("ModuleName", strModuleName);
            theHT.Add("DisplayName", strDisplayName);
            theHT.Add("CanEnroll", canEnrol);
            theHT.Add("Status", 1);
            theHT.Add("DeleteFlag", 0);
            theHT.Add("UserID", GblIQCare.AppUserId);

            theHT.Add("PharmacyFlag", pharmacyFlag);

            objIdentifier = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");
            Int32 intExistModuleId = objIdentifier.SaveUpdateModuleDetail(theHT, dtSubmit, GblIQCare.dtServiceBusinessValues);
            intOldModuleId = intExistModuleId;
            if (intExistModuleId == 0)
            {
                IQCareWindowMsgBox.ShowWindow("ModuleExist", this);
                return;
            }
            else
            {
                Form theForm;
                theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmModule, IQCare.FormBuilder"));
                theForm.MdiParent = this.MdiParent;
                theForm.Left = 0;
                theForm.Top = 0;
                theForm.Focus();
                theForm.Show();
                this.Close();
            }
        }

        /// <summary>
        /// Shows the grid.
        /// </summary>
        /// <param name="dt">The dt.</param>
        public void ShowGrid(DataTable dt)
        {
            try
            {
                dgwFieldDetails.Columns.Clear();
                dgwFieldDetails.DataSource = null;
                DataGridViewTextBoxColumn colIdentifierName = new DataGridViewTextBoxColumn();
                colIdentifierName.HeaderText = "Identifier Name";
                colIdentifierName.DataPropertyName = "IdentifierName";
                colIdentifierName.Name = "IdentifierName";
                colIdentifierName.Width = 200;
                colIdentifierName.ReadOnly = true;

                DataGridViewTextBoxColumn colIdentifierLabel = new DataGridViewTextBoxColumn();
                colIdentifierLabel.HeaderText = "Identifier Label";
               colIdentifierLabel.Name= colIdentifierLabel.DataPropertyName = "Label";
                colIdentifierLabel.Width = 250;
                colIdentifierLabel.ReadOnly = true;

                DataGridViewCheckBoxColumn colSelected = new DataGridViewCheckBoxColumn();
                colSelected.HeaderText = "Select";
                colSelected.DataPropertyName = "Selected";
                colSelected.Name = "Selected";
                colSelected.Width = 100;
                if (GblIQCare.UpdateFlag == 1)
                {
                    colSelected.ReadOnly = true;
                }
                else
                {
                    colSelected.ReadOnly = false;
                }

                if ((GblIQCare.UpdateFlag == 1) && (GblIQCare.ModuleName == "SMART ART FORM"))
                {
                    colSelected.ReadOnly = false;
                }
                DataGridViewCheckBoxColumn colRequired = new DataGridViewCheckBoxColumn();
                colRequired.HeaderText = "Required";
                colRequired.Name = colRequired.DataPropertyName = "Required";
                colRequired.Width = 100;
                colRequired.ReadOnly = false;

                DataGridViewTextBoxColumn colId = new DataGridViewTextBoxColumn();
                colId.HeaderText = "";
                colId.Name = colId.DataPropertyName = "ID";
                colId.Width = -1;
                colId.ReadOnly = true;
                colId.Visible = false;

                DataGridViewTextBoxColumn colFieldTypeId = new DataGridViewTextBoxColumn();
                colFieldTypeId.HeaderText = "";
                colFieldTypeId.Name = colFieldTypeId.DataPropertyName = "FieldType";
                colFieldTypeId.Width = -1;
                colFieldTypeId.ReadOnly = true;
                colFieldTypeId.Visible = false;

                DataGridViewTextBoxColumn colFieldTypeName = new DataGridViewTextBoxColumn();
                colFieldTypeName.HeaderText = "Data Type";
                colFieldTypeName.Name = colFieldTypeName.DataPropertyName = "FieldTypeName";
                colFieldTypeName.Width = 150;
                colFieldTypeName.ReadOnly = true;
                colFieldTypeName.Visible = true;

                DataGridViewTextBoxColumn colUpdateFlag = new DataGridViewTextBoxColumn();
                colUpdateFlag.HeaderText = "";
                colUpdateFlag.Name = colUpdateFlag.DataPropertyName = "UpdateFlag";
                colUpdateFlag.Width = -1;
                colUpdateFlag.ReadOnly = true;
                colUpdateFlag.Visible = false;

                dgwFieldDetails.AutoGenerateColumns = false;
                dgwFieldDetails.AllowUserToAddRows = false;

                dgwFieldDetails.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                dgwFieldDetails.DefaultCellStyle.Font.Size.Equals(12);
                dgwFieldDetails.ColumnHeadersDefaultCellStyle.Font.Bold.Equals(true);
               
                dgwFieldDetails.Columns.Add(colId);
                dgwFieldDetails.Columns.Add(colIdentifierName);
                dgwFieldDetails.Columns.Add(colIdentifierLabel);
                dgwFieldDetails.Columns.Add(colFieldTypeId);
                dgwFieldDetails.Columns.Add(colFieldTypeName);
                dgwFieldDetails.Columns.Add(colSelected);
                 dgwFieldDetails.Columns.Add(colRequired);               
                dgwFieldDetails.Columns.Add(colUpdateFlag);
               

                dgwFieldDetails.DataSource = dt;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnClose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Form theForm;
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmModule, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if ((GblIQCare.UpdateFlag == 1) && (GblIQCare.ModuleName != "SMART ART FORM"))
            {
                IQCareWindowMsgBox.ShowWindow("UpdateModule", this);
                return;
            }
            int intIdentifierno = 0;
            int intAutoPopulatedidentifier = 0;
            if (txtModuleName.Text != "")
            {
                dtSubmit = (DataTable)dgwFieldDetails.DataSource;
                for (int i = 0; i < dtSubmit.Rows.Count; i++)
                {
                    if (dtSubmit.Rows[i]["Selected"].ToString() == "True")
                    {
                        intIdentifierno += 1;
                    }
                    if (dtSubmit.Rows[i]["Selected"].ToString() == "True" && dtSubmit.Rows[i]["FieldType"].ToString() == "17")
                    {
                        intAutoPopulatedidentifier += 1;
                    }
                }

                if (intIdentifierno > 4)
                {
                    IQCareWindowMsgBox.ShowWindow("IdentifierNo", this);
                    return;
                }
                if (intIdentifierno == 0 && GblIQCare.Identifier < 1 && chkEnroll.Checked)
                {
                    IQCareWindowMsgBox.ShowWindow("ModuleSave", this);
                    return;
                }
                if (intAutoPopulatedidentifier > 1)
                {
                    IQCareWindowMsgBox.ShowWindow("moreautopopulateidentifer", this);
                    return;
                }
                SaveUpdateModule();
            }
            else
            {
                IQCareWindowMsgBox.ShowWindow("ModuleRequired", this);
                return;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSumit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void btnSumit_Click(object sender, EventArgs e)
        {
            if ((txtFieldName.Text.Trim() == "") || Convert.ToInt32(cmbFieldType.SelectedValue) == 0 || (txtModuleName.Text == "" || txtlabel.Text == ""))
            {
                IQCareWindowMsgBox.ShowWindow("IdentifierTypeRequired", this);
                return;
            }
            if (cmbFieldType.SelectedValue.ToString() == "17")
            {
                if (txtstartingnumber.Text == "")
                {
                    IQCareWindowMsgBox.ShowWindow("autopopulateidentifer", this);
                    return;
                }
            }
            ///
            DataView theDV = new DataView(dtSubmit);
            theDV.RowFilter = "IdentifierName='" + txtFieldName.Text.Trim().Replace(" ", "_") + "'";
            if (theDV.Count > 0)
            {
                theDV[0]["IdentifierName"] = txtFieldName.Text.Trim().Replace(" ", "_");
                theDV[0]["Label"] = txtlabel.Text.Trim();
            }
            else
            {
                DataRow dr = dtSubmit.NewRow();
                dr["IdentifierName"] = txtFieldName.Text.Trim().Replace(" ", "_");
                dr["Label"] = txtlabel.Text.Trim();
                dr["ModuleID"] = GblIQCare.ModuleId;
                dr["Selected"] = "True";
                dr["ID"] = 0;
                dr["FieldType"] = cmbFieldType.SelectedValue;
                dr["UpdateFlag"] = 0;
                if (txtstartingnumber.Text != "")
                {
                    dr["autopopulatenumber"] = txtstartingnumber.Text;
                }
                dtSubmit.Rows.Add(dr);
            }

            cmbFieldType.Enabled = true;
            txtFieldName.Text = "";
            txtlabel.Text = "";
            cmbFieldType.SelectedValue = 0;
            intIdentifierid = 0;
            txtstartingnumber.Text = "";
            txtstartingnumber.Visible = false;
            lblstartingnumber.Visible = false;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkEnroll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chkEnroll_CheckedChanged(object sender, EventArgs e)
        {
            txtFieldName.ReadOnly = txtlabel.ReadOnly = cmbFieldType.Enabled = btnSumit.Enabled = !chkEnroll.Checked;
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkHivCareTrmt control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chkHivCareTrmt_CheckedChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Clears the grid.
        /// </summary>
        private void ClearGrid()
        {
            dgwFieldDetails.Columns.Clear();
            dgwFieldDetails.Rows.Clear();
            dgwFieldDetails.Refresh();
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbFieldType control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbFieldType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFieldType.SelectedValue.ToString() == "17")
            {
                txtstartingnumber.Visible = true;
                lblstartingnumber.Visible = true;
            }
            else
            {
                txtstartingnumber.Visible = false;
                lblstartingnumber.Visible = false;
            }
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the dgwFieldDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgwFieldDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                intOldIdentifierrow = e.RowIndex;
                intlabel = e.RowIndex;
                txtFieldName.Text = dgwFieldDetails.Rows[e.RowIndex].Cells["IdentifierName"].Value.ToString();

                txtlabel.Text = dgwFieldDetails.Rows[e.RowIndex].Cells["Label"].Value.ToString();
                cmbFieldType.SelectedValue = Convert.ToInt32(dgwFieldDetails.Rows[e.RowIndex].Cells["FieldType"].Value.ToString());
                intIdentifierid = Convert.ToInt32(dgwFieldDetails.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                intUpdateFlag = Convert.ToInt32(dgwFieldDetails.Rows[e.RowIndex].Cells["UpdateFlag"].Value.ToString());

                if (Convert.ToInt32(intUpdateFlag) == 1)
                {
                    txtlabel.ReadOnly = txtFieldName.ReadOnly = true;
                    intUpdateFlag = 0;
                }
                else
                {
                    txtlabel.ReadOnly = txtFieldName.ReadOnly = false;
                }
                cmbFieldType.Enabled = false;
            }
        }

        /// <summary>
        /// Handles the Load event of the frmModuleDetails control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmModuleDetails_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            cmbFieldType.Enabled = true;
            txtstartingnumber.Visible = false;
            lblstartingnumber.Visible = false;
            this.ClearBusinessRules();
            txtModuleName.ReadOnly = GblIQCare.ModuleId != 0;
            if (GblIQCare.ModuleId != 0)
            {
                txtModuleName.Text = GblIQCare.ModuleName;
                textDisplayName.Text = GblIQCare.ModuleDisplayName;
                chkEnroll.Checked = GblIQCare.ModuleCanEnroll;
                btnSumit.Enabled = GblIQCare.ModuleCanEnroll;
                txtlabel.ReadOnly = txtFieldName.ReadOnly = !GblIQCare.ModuleCanEnroll;
                //if (GblIQCare.PharmacyFlag == 1)
                //{
                //    chkHivCareTrmt.Checked = true;
                //}
                //else
                //{
                //    chkHivCareTrmt.Checked = false;
                //}
            }
            else
            {
                chkEnroll.Checked = true;
            }
            //if (GblIQCare.UpdateFlag != 0)
            //{
            //    //txtModuleName.ReadOnly = true;
            //    txtFieldName.ReadOnly = true;
            //    btnSumit.Enabled = false;
            //    btnSave.Enabled = false;
            //}
            //else
            //{
            //    txtModuleName.ReadOnly = false;
            //    txtFieldName.ReadOnly = false;
            //    btnSumit.Enabled = true;
            //    btnSave.Enabled = chkEnroll.Checked;//true;
            //}
            if ((GblIQCare.UpdateFlag != 0) && (GblIQCare.ModuleName == "SMART ART FORM"))
            {
                txtModuleName.ReadOnly = true;
                txtFieldName.ReadOnly = false;
                btnSumit.Enabled = true;
                btnSave.Enabled = true;
            }
            BindGrid();
        }

        private void ClearBusinessRules()
        {
            GblIQCare.dtServiceBusinessValues.Clear();
            GblIQCare.dtServiceBusinessValues.Columns.Clear();
            GblIQCare.dtServiceBusinessValues.Rows.Clear();
        }

        /// <summary>
        /// Handles the KeyPress event of the textDisplayName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void textDisplayName_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strRestrictCharList = "[]{}\\|'\";:?/><.,~!@#$%^&*()_-+=";

            if (strRestrictCharList.IndexOf(e.KeyChar) >= 0)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txtFieldName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtFieldName_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strRestrictCharList = "[]{}\\|'\";:?/><.,~!@#$%^&*()_-+=";

            if (strRestrictCharList.IndexOf(e.KeyChar) >= 0)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the Leave event of the txtFieldName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtFieldName_Leave(object sender, EventArgs e)
        {
            txtlabel.Text = txtFieldName.Text;
        }

        /// <summary>
        /// Handles the KeyPress event of the txtModuleName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtModuleName_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strRestrictCharList = "[]{}\\|'\";:?/><,~!@#$%^&*()_-+=";

            if (strRestrictCharList.IndexOf(e.KeyChar) >= 0)
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Handles the Leave event of the txtModuleName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtModuleName_Leave(object sender, EventArgs e)
        {
            if (textDisplayName.Text == "")
            {
                textDisplayName.Text = txtModuleName.Text;
            }
        }

        /// <summary>
        /// Handles the KeyPress event of the txtstartingnumber control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtstartingnumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            string strRestrictCharList = "0123456789\b";

            if (strRestrictCharList.IndexOf(e.KeyChar) == -1)
            {
                e.Handled = true;
            }
        }

        private void btnBusinessRule_Click(object sender, EventArgs e)
        {
            if (txtModuleName.Text != "" && chkEnroll.Checked)
            {
                GblIQCare.strserviceareaname = txtModuleName.Text.Trim();
            }
            else
            {
                IQCareWindowMsgBox.ShowWindow("ModuleRequired", this);
                return;
            }
            if (theForm == null)
            {
                GblIQCare.iFormMode = 0;
                theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmServiceBusinessRule, IQCare.FormBuilder"));
                theForm.Left = 0;
                theForm.Top = 0;
                theForm.FormClosed += new FormClosedEventHandler(theForm_FormClosed);
                theForm.Show();
            }
        }
        void theForm_FormClosed(object sender, EventArgs e)
        {
            theForm = null;
        }
    }
}