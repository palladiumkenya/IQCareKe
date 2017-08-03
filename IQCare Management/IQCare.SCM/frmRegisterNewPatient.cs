using Application.Common;
using Application.Presentation;
using Interface.SCM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IQCare.SCM
{
    public partial class frmRegisterNewPatient : Form
    {
        private DataSet XMLDS = new DataSet();

        private DataSet XMLPharDS = new DataSet();
        public frmRegisterNewPatient()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtFName.Text == "")
            {
                MessageBox.Show("Please enter First Name");
                return;
            }
                
            if(txtLName.Text == "")
            {
                MessageBox.Show("Please enter Last Name");
                return;
            }

            if(cmbGender.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please select the gender");
                return;
            }
            if (cmbService.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("Please select the Service");
                return;
            }



            IDrug drug = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            try
            {
                drug.saveUpdatePatientRegistration(txtFName.Text, txtMName.Text, txtLName.Text, txtPatientNumber.Text, dtDOB.Value.ToString(), 
                    cmbGender.SelectedValue.ToString(), GblIQCare.AppLocationId.ToString(), dtRegDate.Value.ToString(), 
                    GblIQCare.AppUserId.ToString(), cmbService.SelectedValue.ToString());
                MessageBox.Show("Registration successful");
                getAllPatients();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void frmRegisterNewPatient_Load(object sender, EventArgs e)
        {
            BindCombo();
            getAllPatients();
        }

        private void BindCombo()
        {
            XMLDS.ReadXml(GblIQCare.GetXMLPath() + "\\AllMasters.con");
            BindFunctions theBindManager = new BindFunctions();

            DataView theDV = new DataView(XMLDS.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeId = 4 and (DeleteFlag =0 or DeleteFlag is null)";
            DataTable theGender = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbGender, theGender, "Name", "Id");

            theDV = new DataView(XMLDS.Tables["Mst_Code"]);
            theDV.RowFilter = "Name = 'ServiceRegisteredForAtPharmacy' and (DeleteFlag =0 or DeleteFlag is null)";
            string codeID = theDV[0]["CodeId"].ToString();

            theDV = new DataView(XMLDS.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeId = " + codeID + " and (DeleteFlag =0 or DeleteFlag is null)";
            DataTable theService = theDV.ToTable();
            theBindManager.Win_BindCombo(cmbService, theService, "Name", "Id");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void getAllPatients()
        {
            IDrug drug = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            DataTable theDT = drug.getPatientsRegistered();
            BindPharmacyDispenseGrid(theDT);
        }
        private void BindPharmacyDispenseGrid(DataTable theDT)
        {
            try
            {
                dgvPatients.DataSource = null;
                dgvPatients.Columns.Clear();
                dgvPatients.AutoGenerateColumns = false;

                DataGridViewTextBoxColumn colptnpk = new DataGridViewTextBoxColumn();
                colptnpk.HeaderText = "ptnpk";
                colptnpk.Name = colptnpk.DataPropertyName = "ptn_pk";
                colptnpk.Width = 35;
                colptnpk.Visible = false;
                colptnpk.ReadOnly = true;

                DataGridViewTextBoxColumn colfname = new DataGridViewTextBoxColumn();
                colfname.HeaderText = "First Name";
                colfname.Name = colfname.DataPropertyName = "fname";
                colfname.Width = 120;
                colfname.ReadOnly = true;

                DataGridViewTextBoxColumn colmname = new DataGridViewTextBoxColumn();
                colmname.HeaderText = "Middle Name";
                colmname.Name = colmname.DataPropertyName = "mname";
                colmname.Width = 120;
                colmname.ReadOnly = true;

                DataGridViewTextBoxColumn collname = new DataGridViewTextBoxColumn();
                collname.HeaderText = "Last Name";
                collname.Name = collname.DataPropertyName = "lname";
                collname.Width = 120;
                collname.ReadOnly = true;

                DataGridViewTextBoxColumn colEnrolmentID = new DataGridViewTextBoxColumn();
                colEnrolmentID.HeaderText = "Patient #";
                colEnrolmentID.Name = colEnrolmentID.DataPropertyName = "PatientEnrollmentID";
                colEnrolmentID.Width = 120;
                colEnrolmentID.ReadOnly = true;

                DataGridViewTextBoxColumn colAge = new DataGridViewTextBoxColumn();
                colAge.HeaderText = "Age";
                colAge.Name = colAge.DataPropertyName = "age";
                colAge.Width = 70;
                colAge.ReadOnly = true;

                DataGridViewTextBoxColumn colgender = new DataGridViewTextBoxColumn();
                colgender.HeaderText = "Gender";
                colgender.Name = colgender.DataPropertyName = "gender";
                colgender.Width = 100;
                colgender.ReadOnly = true;

                DataGridViewTextBoxColumn colservice = new DataGridViewTextBoxColumn();
                colservice.HeaderText = "Service";
                colservice.Name = colservice.DataPropertyName = "service";
                colservice.Width = 150;
                colservice.ReadOnly = true;


                dgvPatients.Columns.Add(colptnpk);
                dgvPatients.Columns.Add(colfname);
                dgvPatients.Columns.Add(colmname);
                dgvPatients.Columns.Add(collname);
                dgvPatients.Columns.Add(colEnrolmentID);
                dgvPatients.Columns.Add(colAge);
                dgvPatients.Columns.Add(colgender);
                dgvPatients.Columns.Add(colservice);
               
                dgvPatients.DataSource = theDT;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
    }
}
