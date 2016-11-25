using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Application.Common;
using Interface.FormBuilder;
using Application.Presentation;
using System.Configuration;

namespace IQCare.FormBuilder
{
    public partial class frmCareEndedList : Form
    {
        ICareEndedConfiguration objCareEnd;
        public frmCareEndedList()
        {
            InitializeComponent();
        }

        private void frmCareEndedList_Load(object sender, EventArgs e)
        {
            DataSet dsCareEndedDetails = new DataSet();

            try
            {
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);

                cmbFormStatus.SelectedIndex = 2;
                objCareEnd = (ICareEndedConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BCareEndedConfiguration,BusinessProcess.FormBuilder");
                dsCareEndedDetails = objCareEnd.GetCareEndedDetails();

                DataTable dt;
                dt = dsCareEndedDetails.Tables[0];
                DataRow drAddSelect;
                drAddSelect = dt.NewRow();
                drAddSelect["ModuleName"] = "All";
                drAddSelect["ModuleID"] = 0;

                dt.Rows.InsertAt(drAddSelect, 0);
                BindFunctions theBind = new BindFunctions();
                theBind.Win_BindCombo(cmbTechnicalArea, dt, "ModuleName", "ModuleId");
                cmbFormStatus.SelectedIndex = 0;
                ///BindGrid();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
            }
        }

        // private void btnAdd_Click(object sender, EventArgs e)
        //{
        //    GblIQCare.iHomePageId = 0;
        //    Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frm, IQCare.FormBuilder"));
        //    theForm.MdiParent = this.MdiParent;
        //    theForm.Left = 0;
        //    theForm.Top = 2;
        //    this.Close();
        //    theForm.Show();
        //}

        private void BindGrid()
        {
            DataSet dsQueryCareEndDetails = new DataSet();

            try
            {
                int intPublished = -1;

                if (cmbFormStatus.Text == "All")
                {
                    intPublished = -1;
                }
                else if (cmbFormStatus.Text == "Published")
                {
                    intPublished = 2;
                }
                else if (cmbFormStatus.Text == "UnPublished")
                {
                    intPublished = 1;
                }

                if (cmbTechnicalArea.SelectedValue!= null && Convert.ToInt32(cmbTechnicalArea.SelectedValue) > -1)
                {
                    objCareEnd = (ICareEndedConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BCareEndedConfiguration,BusinessProcess.FormBuilder");
                    dsQueryCareEndDetails = objCareEnd.GetCareEndQuery(Convert.ToInt32(cmbTechnicalArea.SelectedValue), intPublished);

                    dgwFormDetails.DataSource = null;
                    string strGetPath = GblIQCare.GetPath();
                    ShowGrid(dsQueryCareEndDetails.Tables[0]);
                }
            }

            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }
        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwFormDetails.Columns.Clear();
                dgwFormDetails.AutoGenerateColumns = false;
                dgwFormDetails.DataSource = null;
                DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                col1.HeaderText = "Form Name";
                col1.DataPropertyName = "FeatureName";
                col1.Width = 268;
                col1.ReadOnly = true;

                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = "Service";
                col2.DataPropertyName = "ModuleName";
                col2.Width = 270;
                col2.ReadOnly = true;

                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col3.HeaderText = "Updated By";
                col3.DataPropertyName = "UserName";
                col3.Width = 150;
                col3.ReadOnly = true;

                DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
                col4.HeaderText = "Last Updated";
                col4.DataPropertyName = "ModifiedDate";
                col4.Width = 120;
                col4.ReadOnly = true;

                DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
                col5.HeaderText = "Published";
                col5.DataPropertyName = "Published";
                col5.Width = 130;
                col5.ReadOnly = true;

                DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
                col6.HeaderText = "FeatureID";
                col6.DataPropertyName = "FeatureID";
                col6.Width = 0;
                col6.Visible = false;


                DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
                col7.HeaderText = "ModuleID";
                col7.DataPropertyName = "ModuleID";
                col7.Width = 0;
                col7.Visible = false;

                dgwFormDetails.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                dgwFormDetails.DefaultCellStyle.Font.Size.Equals(12);
                dgwFormDetails.ColumnHeadersDefaultCellStyle.Font.Bold.Equals(true);

                dgwFormDetails.Columns.Add(col1);
                dgwFormDetails.Columns.Add(col2);
                dgwFormDetails.Columns.Add(col3);
                dgwFormDetails.Columns.Add(col4);
                dgwFormDetails.Columns.Add(col5);
                dgwFormDetails.Columns.Add(col6);
                dgwFormDetails.Columns.Add(col7);
                dgwFormDetails.DataSource = theDT;
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgwFormDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    if (Convert.ToString(dgwFormDetails.Rows[e.RowIndex].Cells[4].Value) == "Published")
                    {
                        IQCareWindowMsgBox.ShowWindow("PublishTechenicalArea", this);
                        return;
                    }

                    DataTable tbl1 = (DataTable)dgwFormDetails.DataSource;
                    if (tbl1.Rows.Count > 0)
                    {
                        if (e.RowIndex > tbl1.Rows.Count)
                        {
                            return;
                        }

                        GblIQCare.iCareEndedId = Convert.ToInt32(dgwFormDetails.Rows[e.RowIndex].Cells[5].Value.ToString());
                        Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmCareEnded, IQCare.FormBuilder"));
                        theForm.MdiParent = this.MdiParent;
                        theForm.Left = 0;
                        theForm.Top = 2;
                        this.Close();
                        theForm.Show();
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

        private void dgwFormDetails_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                Color ClrRed = Color.FromArgb(204, 0, 0);
                string strPubValue = "";
                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    if (dgwFormDetails.Columns[e.ColumnIndex].HeaderText.Equals("Published"))
                    {
                        if (e.Value == null || e.Value.ToString() == "")
                        {
                            strPubValue = "0";
                        }
                        else
                        {
                            strPubValue = e.Value.ToString();
                        }
                    }
                    else if (dgwFormDetails.Rows.Count >= 1)
                    {
                        if (dgwFormDetails.Rows[e.RowIndex].Cells[4].Value.ToString() == "")
                        {
                            strPubValue = "0";
                        }
                        else
                            strPubValue = dgwFormDetails.Rows[e.RowIndex].Cells[4].Value.ToString();
                    }
                }


                switch (strPubValue)
                {
                    case "Un-Published":
                        dgwFormDetails.Rows[e.RowIndex].Cells[0].Style.BackColor = System.Drawing.Color.Red;
                        dgwFormDetails.Rows[e.RowIndex].Cells[1].Style.BackColor = System.Drawing.Color.Red;
                        dgwFormDetails.Rows[e.RowIndex].Cells[2].Style.BackColor = System.Drawing.Color.Red;
                        dgwFormDetails.Rows[e.RowIndex].Cells[3].Style.BackColor = System.Drawing.Color.Red;
                        dgwFormDetails.Rows[e.RowIndex].Cells[4].Style.BackColor = System.Drawing.Color.Red;
                        break;
                    case "Published":
                        dgwFormDetails.Rows[e.RowIndex].Cells[0].Style.BackColor = System.Drawing.Color.Green;
                        dgwFormDetails.Rows[e.RowIndex].Cells[1].Style.BackColor = System.Drawing.Color.Green;
                        dgwFormDetails.Rows[e.RowIndex].Cells[2].Style.BackColor = System.Drawing.Color.Green;
                        dgwFormDetails.Rows[e.RowIndex].Cells[3].Style.BackColor = System.Drawing.Color.Green;
                        dgwFormDetails.Rows[e.RowIndex].Cells[4].Style.BackColor = System.Drawing.Color.Green;
                        break;
                }
            }

            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }


        }

        private void dgwFormDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == -1)
                {
                    return;
                }
                DialogResult strResult = DialogResult.None;
                ICareEndedConfiguration objTechArea;
                MsgBuilder theBuilder = new MsgBuilder();
                if (dgwFormDetails.Columns[e.ColumnIndex].HeaderText == "Published")
                {
                    if (Convert.ToString(dgwFormDetails.Rows[e.RowIndex].Cells[4].Value) == "Published")
                    {
                        theBuilder.DataElements["PgStatus"] = "UnPublish";
                    }
                    else
                    {
                        theBuilder.DataElements["PgStatus"] = "Publish";
                    }
                    strResult = IQCareWindowMsgBox.ShowWindowConfirm("Publish", theBuilder, this);

                    if (Convert.ToString(dgwFormDetails.Rows[e.RowIndex].Cells[4].Value) == "Published")
                    {
                        if (strResult == DialogResult.No)
                        {
                            return;
                        }
                        Hashtable theHT = new Hashtable();
                        theHT.Add("Status", "1");
                        theHT.Add("FeatureID", dgwFormDetails.Rows[e.RowIndex].Cells[5].Value);
                        DataTable theDT = new DataTable();
                        objTechArea = (ICareEndedConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BCareEndedConfiguration, BusinessProcess.FormBuilder");
                        Int32 intExistModuleId = objTechArea.StatusUpdate(theHT);
                        BindGrid();

                    }
                    else if (Convert.ToString(dgwFormDetails.Rows[e.RowIndex].Cells[4].Value) == "Un-Published")
                    {
                        if (strResult == DialogResult.No)
                        {
                            return;
                        }
                        //strResult = IQCareWindowMsgBox.ShowWindowConfirm("UnPublish", this);
                        Hashtable theHT = new Hashtable();
                        theHT.Add("Status", "2");
                        theHT.Add("FeatureID", dgwFormDetails.Rows[e.RowIndex].Cells[5].Value);
                        DataTable theDT = new DataTable();
                        objTechArea = (ICareEndedConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BCareEndedConfiguration, BusinessProcess.FormBuilder");
                        Int32 intExistModuleId = objTechArea.StatusUpdate(theHT);
                        BindGrid();
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

        private void dgwFormDetails_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    if (Convert.ToString(dgwFormDetails.Rows[e.RowIndex].Cells[4].Value) == "Published")
                    {
                        IQCareWindowMsgBox.ShowWindow("PublishTechenicalArea", this);
                        return;
                    }

                    DataTable tbl1 = (DataTable)dgwFormDetails.DataSource;
                    if (tbl1.Rows.Count > 0)
                    {
                        if (e.RowIndex > tbl1.Rows.Count)
                        {
                            return;
                        }

                        GblIQCare.iCareEndedId = Convert.ToInt32(dgwFormDetails.Rows[e.RowIndex].Cells[5].Value.ToString());
                        Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmCareEnded,IQCare.FormBuilder"));
                        theForm.MdiParent = this.MdiParent;
                        theForm.Left = 0;
                        theForm.Top = 2;
                        this.Close();
                        theForm.Show();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GblIQCare.iManageCareEnded = 1;
            GblIQCare.iCareEndedId = 0;
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmCareEnded, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            this.Close();
            theForm.Show();
        }

        private void BtnManage_Click(object sender, EventArgs e)
        {
            GblIQCare.iManageCareEnded = 1;
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmFieldDetails, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            this.Close();
            theForm.Show();
        }

        private void cmbFormStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void cmbTechnicalArea_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbTechnicalArea.SelectedValue.GetType().ToString() != "System.Data.DataRowView")
                BindGrid();
        }

       
    }
}
   




