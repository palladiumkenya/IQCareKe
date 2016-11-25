using System;
using System.Data;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.Service;

    namespace IQCare.Service
    {
    public partial class frmDataUpsizing : Form
    {
        public frmDataUpsizing()
        {
            InitializeComponent();
        }

        private void btnfopen_Click(object sender, EventArgs e)
        {
            fopen.ShowDialog();
            txtAccessFile.Text = fopen.FileName.ToString(); 
        }

        private void btnUpsize_Click(object sender, EventArgs e)
        {
            try
            {
                #region "GetAccessTables"
                string[] path = txtAccessFile.Text.Split('\\');
                int i = 0;
                string theSystemDB = "";
                for (i = 0; i < path.Length - 1; i++)
                {
                    if (theSystemDB == "")
                    {
                        theSystemDB = path[i].ToString();
                    }
                    else
                    {
                        theSystemDB = theSystemDB + "\\" + path[i].ToString();
                    }
                }
                theSystemDB = theSystemDB + "\\System.mdw";
                System.Data.OleDb.OleDbConnection theAccessCon = new System.Data.OleDb.OleDbConnection();
                if (rbtCareWare.Checked == true)
                    theAccessCon.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtAccessFile.Text.Trim() + ";User ID=" + txtAccessUid.Text.Trim() + ";Password=" + txtAccessPwd.Text.Trim() + ";Jet OLEDB:System database=" + theSystemDB;
                else
                    theAccessCon.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtAccessFile.Text.Trim() + ";Jet OLEDB:Database Password=" + txtAccessPwd.Text.Trim(); //+ ";Jet OLEDB:System database=" + theSystemDB;

                theAccessCon.Open();
                DataTable theAccessTables = theAccessCon.GetSchema("TABLES");
                DataTable theColumns = theAccessCon.GetSchema("COLUMNS");

                #endregion

                pgbar1.Maximum = theAccessTables.Rows.Count;
                System.Data.OleDb.OleDbCommand theAccessCmd = new System.Data.OleDb.OleDbCommand();
                pgbar1.Value = 0;
                foreach (DataRow theDR in theAccessTables.Rows)
                {
                    if (theDR["TABLE_TYPE"].ToString() == "TABLE")
                    {

                        theAccessCmd.CommandText = "Select * from " + theDR["TABLE_NAME"].ToString();
                        theAccessCmd.Connection = theAccessCon;
                        System.Data.OleDb.OleDbDataAdapter theAccessAdpt = new System.Data.OleDb.OleDbDataAdapter(theAccessCmd);
                        DataTable theResult = new DataTable();
                        theAccessAdpt.Fill(theResult);
                        //////Migration UpsizeManager = new Migration();
                        IMigration UpsizeManager;
                        UpsizeManager = (IMigration)ObjectFactory.CreateInstance("BusinessProcess.Service.BMigration, BusinessProcess.Service");
       
                        #region "ExporttoSQL"
                        string theConstr = string.Format("data source={0};uid={1};pwd={2};initial catalog={3}", txtSqlServer.Text.Trim(), txtSqlUid.Text.Trim(), txtSqlPwd.Text.Trim(), txtSqlDbName.Text.Trim());
                        UpsizeManager.UpsizeData(theResult, theColumns, txtSqlDbName.Text, theDR["TABLE_NAME"].ToString());
                        #endregion
                    }
                    pgbar1.Value = pgbar1.Value + 1;
                }
                MessageBox.Show("Upsizing Completed.", "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString(), "IQCare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmDataUpsizing_Load(object sender, EventArgs e)
        {
            this.Top = 1;
            this.Left = 1;
            //set css begin
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //set css end

            if (GblIQCare.HasFunctionRight(ApplicationAccess.Upsize, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnUpsize.Enabled = false;
            }
           
            if (GblIQCare.HasFunctionRight(ApplicationAccess.Upsize, FunctionAccess.Update, GblIQCare.dtUserRight) == false)
            {
                btnUpsize.Enabled = false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
    }