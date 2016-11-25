using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.ServiceProcess;
using System.ServiceProcess.Design;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Specialized;
using System.Xml;
using System.Data.Common;
using Application.Presentation;
using Interface.Service;
using Application.Common;

    //using BusinessLayer;
    namespace IQCare.Service
    {
    public partial class frmMigration : Form
    {
        Int32 LocationId = 0;

        public frmMigration()
        {
            InitializeComponent();
        }

        private void frmMigration_Load(object sender, EventArgs e)
        {
            this.Top = 1;
            this.Left = 1;
            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //set css end
           
            if (GblIQCare.HasFunctionRight(ApplicationAccess.DatabaseMigration, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnOk.Enabled = false;
                btnMigrate.Enabled = false;
            }
           
            if (GblIQCare.HasFunctionRight(ApplicationAccess.DatabaseMigration, FunctionAccess.Update, GblIQCare.dtUserRight) == false)
            {
                btnOk.Enabled = false;
                btnMigrate.Enabled = false;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                IMigration objGetProcedures;
                objGetProcedures = (IMigration)ObjectFactory.CreateInstance("BusinessProcess.Service.BMigration, BusinessProcess.Service");
       
                String constr = "";
                constr = String.Format("data source={0};uid={1};pwd={2};initial catalog={3}", txtserver.Text.Trim(), txtuserid.Text.Trim(), txtpassword.Text.Trim(), txtdatabase.Text.Trim());
                DataSet ds = new DataSet();
                ds = objGetProcedures.GetProcedures(constr, txtnewdatabase.Text.Trim());
                dgmigration.DataSource = null;
                Bind_Grid(ds.Tables[0]);
                LocationId = Convert.ToInt32(ds.Tables[1].Rows[0][0]);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString(), "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnMigrate_Click(object sender, EventArgs e)
        {
            try
            {
                //////Migration bl = new Migration();
                IMigration objMigrateData;
                objMigrateData = (IMigration)ObjectFactory.CreateInstance("BusinessProcess.IQCareManagement.BMigration, BusinessProcess.IQCareManagement");
       
                String constr;
                Int32 i;
                Int32 norec;
                Int32 RowCount;
                Int32 LocId;
                constr = String.Format("data source={0};uid={1};pwd={2};initial catalog={3}", txtserver.Text.Trim(), txtuserid.Text.Trim(), txtpassword.Text.Trim(), txtdatabase.Text.Trim());
                i = 0;
                norec = dgmigration.Rows.Count;
                pgbar.Maximum = norec - 1;
                pgbar.Minimum = 0;
                while (i < norec - 1)
                {
                    if (Convert.ToInt32(dgmigration.Rows[i].Cells[2].Value) > 0)
                    {
                        LocId = LocationId;
                    }
                    else
                    {
                        LocId = 0;
                    }
                    if (Convert.ToInt32(dgmigration.Rows[i].Cells[3].Value) < 1)
                        RowCount = objMigrateData.MigrateData(dgmigration.Rows[i].Cells[1].Value.ToString(), LocId, txtnewdatabase.Text.Trim(), Convert.ToInt32(dgmigration.Rows[i].Cells[0].Value));
                    btnOk_Click(sender, e);
                    i = i + 1;
                    pgbar.Value = i;
                }
                MessageBox.Show("Migration Completed Successfully.", "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString(), "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmd_Minimise_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Bind_Grid(DataTable dt)
        {
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "Procedure";
            col1.DataPropertyName = "ProcedureName";
            col1.Width = 300;
            col1.ReadOnly = true;

            DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
            col2.HeaderText = "Status";
            col2.DataPropertyName = "Status";
            col2.Width = 80;
            col2.ReadOnly = true;

            DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
            col3.HeaderText = "Id";
            col3.DataPropertyName = "Id";
            col3.Visible = false;
            col3.Width = 0;
            col3.ReadOnly = true;

            DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
            col4.HeaderText = "Location";
            col4.DataPropertyName = "Location";
            col4.Width = 0;
            col4.Visible = false;
            col4.ReadOnly = true;

            DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
            col5.HeaderText = "Selected";
            col5.DataPropertyName = "Selected";
            col5.Width = 0;
            col5.Visible = false;
            col5.ReadOnly = true;

            DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
            col6.HeaderText = "Complete";
            col6.DataPropertyName = "Complete";
            col6.Visible = false;
            col6.ReadOnly = true;

            dgmigration.Columns.Add(col1);
            dgmigration.Columns.Add(col2);
            dgmigration.Columns.Add(col3);
            dgmigration.Columns.Add(col4);
            dgmigration.Columns.Add(col5);
            dgmigration.Columns.Add(col6);

            dgmigration.DataSource = dt;

        }

     }
    }