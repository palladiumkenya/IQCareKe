using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Interface.FormBuilder;
using Application.Common;
using Application.Presentation;

   namespace IQCare.FormBuilder
    {
    public partial class frmModule : Form
    {
        DataSet objDsModuleDetails = new DataSet();
        int intModuleId = 0;
        String Status = "";

      public frmModule()
        {
            InitializeComponent();
        }

        private void frmModule_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            BindGrid();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            GblIQCare.UpdateFlag = 0;
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmModuleDetails, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            GblIQCare.ModuleId = 0;
            theForm.Left = 0;
            theForm.Top = 0;
            theForm.Show();
            this.Close();
        }

        private void BindGrid()
        {
            IModule objModuleDetail;
            objModuleDetail = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule,BusinessProcess.FormBuilder");
            objDsModuleDetails = objModuleDetail.GetModuleDetail();
            if (objDsModuleDetails.Tables[0].Rows.Count > 0)
            {
                ShowGrid(objDsModuleDetails.Tables[0]);
            }
        }

        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwModuleDetails.DataSource = null;
                DataGridViewTextBoxColumn colName = new DataGridViewTextBoxColumn();
                colName.HeaderText = "Service";
                colName.DataPropertyName = "ModuleName";
                colName.Name = "ModuleName";
                colName.Width = 150;
                colName.ReadOnly = true;

                dgwModuleDetails.DataSource = null;
                DataGridViewTextBoxColumn colDisplayName = new DataGridViewTextBoxColumn();
                colDisplayName.HeaderText = "Service Display Name";
                colDisplayName.DataPropertyName = "DisplayName";
                colDisplayName.Name = "DisplayName";
                colDisplayName.Width = 150;
                colDisplayName.ReadOnly = true;

                dgwModuleDetails.DataSource = null;
                DataGridViewTextBoxColumn colEnroll = new DataGridViewTextBoxColumn();
                colEnroll.HeaderText = "Can Enroll";
                colEnroll.DataPropertyName = "CanEnroll";
                colEnroll.Name = "CanEnroll";
                colEnroll.Width = 85;
                colEnroll.ReadOnly = true;

                DataGridViewTextBoxColumn colPatIdentifier = new DataGridViewTextBoxColumn();
                colPatIdentifier.HeaderText = "Patient Identifier";
                colPatIdentifier.DataPropertyName = "PatientIdentifier";
                colPatIdentifier.Name = "PatientIdentifier";
                colPatIdentifier.Width = 500;
                colPatIdentifier.ReadOnly = true;

                DataGridViewTextBoxColumn colStatus = new DataGridViewTextBoxColumn();
                colStatus.HeaderText = "Status";
                colStatus.DataPropertyName = "Status";
                colStatus.Name = "Status";
                colStatus.Width = 85;
                colStatus.ReadOnly = true;

                DataGridViewTextBoxColumn colModuleId = new DataGridViewTextBoxColumn();
                colModuleId.HeaderText = "ModuleId";
                colModuleId.DataPropertyName = "ModuleId";
                colModuleId.Name = "ModuleId";
                colModuleId.Width = 0;
                colModuleId.Visible = false;

                DataGridViewTextBoxColumn colUpdateFlag = new DataGridViewTextBoxColumn();
                colUpdateFlag.HeaderText = "UpdateFlag";
                colUpdateFlag.DataPropertyName = "UpdateFlag";
                colUpdateFlag.Name = "UpdateFlag";
                colUpdateFlag.Width = 0;
                colUpdateFlag.Visible = false;

                DataGridViewTextBoxColumn colIdentifier = new DataGridViewTextBoxColumn();
                colIdentifier.HeaderText = "Identifier";
                colIdentifier.DataPropertyName = "Identifier";
                colIdentifier.Name = "Identifier";
                colIdentifier.Width = 0;
                colIdentifier.Visible = false;

                DataGridViewTextBoxColumn colPharmacyFlag = new DataGridViewTextBoxColumn();
                colPharmacyFlag.HeaderText = "PharmacyFlag";
                colPharmacyFlag.DataPropertyName = "PharmacyFlag";
                colPharmacyFlag.Name = "PharmacyFlag";
                colPharmacyFlag.Width = 0;
                colPharmacyFlag.Visible = false;
           
                dgwModuleDetails.Columns.Add(colName);
                dgwModuleDetails.Columns.Add(colDisplayName);
                dgwModuleDetails.Columns.Add(colEnroll);
                dgwModuleDetails.Columns.Add(colPatIdentifier);
                dgwModuleDetails.Columns.Add(colStatus);
                dgwModuleDetails.Columns.Add(colModuleId); //invisible
                dgwModuleDetails.Columns.Add(colUpdateFlag); //invisible
                dgwModuleDetails.Columns.Add(colIdentifier); //invisible
                dgwModuleDetails.Columns.Add(colPharmacyFlag); //invisi

                dgwModuleDetails.DataSource = theDT;

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void dgwModuleDetails_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != -1 && e.RowIndex != -1)
            {
                DataTable tbl1 = (DataTable)dgwModuleDetails.DataSource;
                if (e.RowIndex > tbl1.Rows.Count)
                    return;

                GblIQCare.ModuleId = Convert.ToInt32(dgwModuleDetails.Rows[e.RowIndex].Cells["ModuleId"].Value);
                GblIQCare.ModuleName = dgwModuleDetails.Rows[e.RowIndex].Cells["ModuleName"].Value.ToString();
                GblIQCare.ModuleDisplayName = dgwModuleDetails.Rows[e.RowIndex].Cells["DisplayName"].Value.ToString();
                GblIQCare.ModuleCanEnroll = dgwModuleDetails.Rows[e.RowIndex].Cells["CanEnroll"].Value.ToString().Trim().ToLower() == "yes";
                GblIQCare.UpdateFlag = Convert.ToInt32(dgwModuleDetails.Rows[e.RowIndex].Cells["UpdateFlag"].Value);
                GblIQCare.Identifier = Convert.ToInt32(dgwModuleDetails.Rows[e.RowIndex].Cells["Identifier"].Value);
                GblIQCare.PharmacyFlag = Convert.ToInt32(dgwModuleDetails.Rows[e.RowIndex].Cells["PharmacyFlag"].Value);
                

                if (dgwModuleDetails.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "Published")
                {
                    IQCareWindowMsgBox.ShowWindow("PublicTechenicalArea", this);
                    return;
                }

                Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmModuleDetails, IQCare.FormBuilder"));
                theForm.MdiParent = this.MdiParent;
                theForm.Left = 0;
                theForm.Top = 0;
                this.Close();
                theForm.Show();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void dgwModuleDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (GblIQCare.UpdateFlag == 1 || intModuleId == 3)
            {
                IQCareWindowMsgBox.ShowWindow("SelectedModule", this);
                return;
            }

            if (Status == "Published")
            {
                IQCareWindowMsgBox.ShowWindow("SelectedModule", this);
                return;
            }

            IModule objIdentifier;
            if (intModuleId != 0)
            {
                DialogResult strResult;
                strResult = MessageBox.Show("Do you want to delete this record ?", "IQCare Managemant", MessageBoxButtons.YesNo);
                if (strResult.ToString() == "Yes")
                {
                    objIdentifier = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");
                    objIdentifier.DeleteModule(intModuleId);
                    IQCareWindowMsgBox.ShowWindow("ModuleDelete", this);
                }
                BindGrid();
                return;
            }
         }

        private void dgwModuleDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           // int intUpdateflag;

            MsgBuilder theBuilder = new MsgBuilder();

            if (e.RowIndex != -1)
            {
                intModuleId = Convert.ToInt32(dgwModuleDetails.Rows[e.RowIndex].Cells["ModuleId"].Value);
                GblIQCare.UpdateFlag = Convert.ToInt32(dgwModuleDetails.Rows[e.RowIndex].Cells["UpdateFlag"].Value);
                Status = Convert.ToString(dgwModuleDetails.Rows[e.RowIndex].Cells["Status"].Value);
            }

            if (e.ColumnIndex == -1)
            {
                return;
            }
            if (GblIQCare.UpdateFlag == 1)
            {
                return;
            }


            if (dgwModuleDetails.Columns[e.ColumnIndex].HeaderText == "Status")
            {

                if ((Status == "Published") || (Status == "Un-Published"))
                {
                    IModule objIdentifier;
                    DialogResult strResult;

                    if (Status == "Published")
                    {
                        theBuilder.DataElements["PgStatus"] = "UnPublish";
                    }
                    else
                    {
                        theBuilder.DataElements["PgStatus"] = "Publish";
                    }
                    strResult = IQCareWindowMsgBox.ShowWindowConfirm("Publish", theBuilder, this);

                    if (strResult == DialogResult.No)
                    {
                        return;
                    }

                    if (strResult.ToString() == "Yes")
                    {
                        if (Status == "Published")
                        {
                            Hashtable theHT = new Hashtable();
                            theHT.Add("ModuleID", intModuleId);
                            theHT.Add("Status", 1);
                            theHT.Add("DeleteFlag", 0);
                            theHT.Add("UserID", GblIQCare.AppUserId);
                            DataTable theDT = new DataTable();
                            objIdentifier = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");
                            Int32 intExistModuleId = objIdentifier.StatusUpdate(theHT);
                            BindGrid();

                        }
                        if (Status == "Un-Published")
                        {
                            Hashtable theHT = new Hashtable();
                            theHT.Add("ModuleID", intModuleId);
                            theHT.Add("Status", 2);
                            theHT.Add("DeleteFlag", 0);
                            theHT.Add("UserID", GblIQCare.AppUserId);
                            DataTable theDT = new DataTable();
                            objIdentifier = (IModule)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BModule, BusinessProcess.FormBuilder");
                            Int32 intExistModuleId = objIdentifier.StatusUpdate(theHT);
                            BindGrid();

                        }

                    }

                }

            }

       }

        private void dgwModuleDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            {
                try
                {
                    Color ClrRed = Color.FromArgb(204, 0, 0);
                    string strPubValue = "";
                    if (e.ColumnIndex != -1 && e.RowIndex != -1)
                    {
                        if (dgwModuleDetails.Columns[e.ColumnIndex].HeaderText.Equals("Status"))
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
                        else if (dgwModuleDetails.Rows.Count >= 1)
                        {
                            if (dgwModuleDetails.Rows[e.RowIndex].Cells["Status"].Value.ToString() == "")
                            {
                                strPubValue = "0";
                            }
                            else
                                strPubValue = dgwModuleDetails.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                        }
                       
                    }

                       
                    switch (strPubValue)
                    {
                        case "Un-Published":
                            dgwModuleDetails.Rows[e.RowIndex].Cells["ModuleName"].Style.BackColor = System.Drawing.Color.Red;
                            dgwModuleDetails.Rows[e.RowIndex].Cells["DisplayName"].Style.BackColor = System.Drawing.Color.Red;
                            dgwModuleDetails.Rows[e.RowIndex].Cells["CanEnroll"].Style.BackColor = System.Drawing.Color.Red;
                            dgwModuleDetails.Rows[e.RowIndex].Cells["Status"].Style.BackColor = System.Drawing.Color.Red;
                            dgwModuleDetails.Rows[e.RowIndex].Cells["PatientIdentifier"].Style.BackColor = System.Drawing.Color.Red;
                            break;
                        case "Published":
                            dgwModuleDetails.Rows[e.RowIndex].Cells["ModuleName"].Style.BackColor = System.Drawing.Color.Green;
                            dgwModuleDetails.Rows[e.RowIndex].Cells["DisplayName"].Style.BackColor = System.Drawing.Color.Green;
                            dgwModuleDetails.Rows[e.RowIndex].Cells["CanEnroll"].Style.BackColor = System.Drawing.Color.Green;
                            dgwModuleDetails.Rows[e.RowIndex].Cells["Status"].Style.BackColor = System.Drawing.Color.Green;
                            dgwModuleDetails.Rows[e.RowIndex].Cells["PatientIdentifier"].Style.BackColor = System.Drawing.Color.Green;
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

        }

        private void dgwModuleDetails_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    if (dgwModuleDetails.Columns[e.ColumnIndex].HeaderText.Equals("PublishedValue"))
                    { }
                }
            }
            catch { }
        }
    }
    }



        





