using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.ProviderBase;
using Application.Common;
using Application.Presentation;
using Interface.FormBuilder;
using System.Text.RegularExpressions;
using System.Xml.Linq;

    namespace IQCare.FormBuilder
    {
    public partial class frmQueryBuilder : Form
    {
        public frmQueryBuilder()
        {
            InitializeComponent();
        }

        Int32 ReportId;
        Int32 CategoryId;
        string RepName = "";

        #region "User Code"
        private void Init_Form()
        {
            txtQBQuery.Text = "";
            txtNewCategory.Text = "";
            txtReportName.Text = "";
            ReportId = 0;
            CategoryId = 0;
            RepName = "";
            txtNewCategory.Visible = false;
            pnlSave.Visible = false;
            pnlOpen.Visible = false;
            tbQBuilder.Focus();
            QBControl.Clear();
        }
        private void SetQueryBuilderConnection()
        {
            IQueryBuilder theQBuilder = (IQueryBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BQueryBuilder, BusinessProcess.FormBuilder");
            SqlConnection theConnection = theQBuilder.GetConnectionQueryBuilder();

            mssqlMetadataProvider1.Connection = theConnection;

            QBControl.MetadataProvider = mssqlMetadataProvider1;
            QBControl.SyntaxProvider = mssqlSyntaxProvider1;
            plainTextSQLBuilder1.QueryBuilder = QBControl;
            txtQBQuery.QueryBuilder = QBControl;
            
            QBControl.ExpressionEditor = expressionEditor1;
            expressionEditor1.QueryBuilder = QBControl;
            QBControl.InitializeDatabaseSchemaTree();
        }
        private void Fill_Combo()
        {
            IQueryBuilder theQBuilder = (IQueryBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BQueryBuilder, BusinessProcess.FormBuilder");
            DataTable theDT = theQBuilder.GetReportsCategory();
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(cmbCategory, theDT, "CategoryName", "CategoryId");
        }
        private void Fill_OpenCategoryCombo()
        {
            IQueryBuilder theQBuilder = (IQueryBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BQueryBuilder, BusinessProcess.FormBuilder");
            DataTable theDT = theQBuilder.GetReportsCategory();
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(cmbOpCategory, theDT, "CategoryName", "CategoryId");
        }
        private void Fill_OpenReportCombo()
        {
            IQueryBuilder theQBuilder = (IQueryBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BQueryBuilder, BusinessProcess.FormBuilder");
            DataTable theDT = theQBuilder.GetCustomReports(Convert.ToInt32(cmbOpCategory.SelectedValue));
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(cmbOpRepName, theDT, "ReportName", "ReportId");
        }
        #endregion

        private void frmQueryBuilder_Load(object sender, EventArgs e)
        {
            try
            {
                Init_Form();
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
              
               // QBControl.InitializeDatabaseSchemaTree();
            }
            catch (Exception err)
            {
                MsgBuilder Builder = new MsgBuilder();
                Builder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", Builder, this);
            }
        }

        private void QBControl_SQLUpdated(object sender, EventArgs e)
        {
            txtQBQuery.Text = plainTextSQLBuilder1.SQL;
        }
       
        private void txtQBQuery_Leave(object sender, EventArgs e)
        {
          
        }

        private void tbQBuilder_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbQBuilder.SelectedTab.Text == "Preview")
                {
                    dgQResult.DataSource = null;
                    dgQResult.Columns.Clear();
                    //if (txtQBQuery.Text.ToUpper().Contains("INSERT") == true || txtQBQuery.Text.ToUpper().Contains("UPDATE") == true || txtQBQuery.Text.ToUpper().Contains("DELETE") == true)
                    if (txtQBQuery.Text.ToUpper().Equals("INSERT") == true || txtQBQuery.Text.ToUpper().Equals("UPDATE") == true || txtQBQuery.Text.ToUpper().Equals("DELETE") == true)
                    {                        
                        MessageBox.Show("Invalid Command. Insert, Update or Delete commands are not allowed.", "IQCare Management", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (txtQBQuery.Text != "")
                    {
                        IQueryBuilder theQBuilder = (IQueryBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BQueryBuilder, BusinessProcess.FormBuilder");
                        DataSet theDS = theQBuilder.ReturnQueryResult(txtQBQuery.Text.Trim());
                        if (theDS.Tables.Count > 0)
                        {
                            dgQResult.DataSource = theDS.Tables[0];
                            dgQResult.AutoGenerateColumns = true;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder Builder = new MsgBuilder();
                Builder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", Builder, this);
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            pnlOpen.Visible = false;
            pnlSave.Visible = true;
            tbQBuilder.SelectedTab=tbQBuilder.TabPages[0];
           
            pnlSave.Location = new Point(246, 106);
            pnlSave.Height = 220;
            pnlSave.Width = 390;
            txtNewCategory.Visible = false;
            lblNewCat.Visible = false;
            Fill_Combo();
            cmbCategory.SelectedValue = CategoryId;
            txtReportName.Text = RepName;
            QBControl.Enabled = false;
            cmbCategory.Focus();
        }
        private bool Validation()
        {

            if (txtNewCategory.Visible == true && txtNewCategory.Text.Trim() == "" && Convert.ToInt32(cmbCategory.SelectedValue) < 1)
            {
                MessageBox.Show("Category cannot be blank.");
                return false;
            }
            if (Convert.ToInt32(cmbCategory.SelectedValue) < 1 && txtNewCategory.Visible == false)
            {
                MessageBox.Show("Select the Category first.");
                return false;
            }
            if (txtReportName.Text.Trim() == "")
            {
                MessageBox.Show("Report Name cannot be Blank.");
                return false;
            }
            if (txtQBQuery.Text.Trim() == "")
            {
                MessageBox.Show("Query cannot be blank");
                return false;

            }
            return true;

        }
        string GetSqlDBTypeFromstring(string paramType)
        {
            string dbtype;
            switch (paramType.ToLower())
            {
                case "nvarchar":
                case "varchar":
                case "string":
                case "text":
                    dbtype = "text";
                    break;
                case "int":
                case "int32":
                case "int64":
                case "int16":
                    dbtype = "whole number";
                    break;
                case "datetime":
                case "datetime2":
                case "date":
                    dbtype = "date";
                    break;
                case "decimal":
                case "numeric":
                case "float":
                    dbtype = "decimal number";
                    break;
                default:
                    dbtype = "text";
                    break;

            }
            return dbtype;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder theErrorMsg = new StringBuilder();
                if (txtNewCategory.Visible == true && txtNewCategory.Text.Trim() == "")
                {
                    theErrorMsg.Append("Category cannot be blank.").AppendLine();
                }
                if (Convert.ToInt32(cmbCategory.SelectedValue) < 1 && txtNewCategory.Visible == false)
                {
                    theErrorMsg.Append("Select the Category first.").AppendLine();
                }
                if (txtReportName.Text.Trim() == "")
                {
                    theErrorMsg.Append("Report Name cannot be Blank.").AppendLine();
                }
                if (txtQBQuery.Text.Trim() == "")
                {
                    MessageBox.Show("Query cannot be blank");
                }

                if (theErrorMsg.Length > 0)
                {
                    MessageBox.Show("Following are mandatory:" + theErrorMsg.ToString(), "IQCare Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Validation() == false)
                {
                    return;
                }

                string CategoryName = "";
                if (Convert.ToInt32(cmbCategory.SelectedValue) > 0)
                    CategoryName = cmbCategory.Text;
                else
                    CategoryName = txtNewCategory.Text;


                if (CategoryId == 0)
                    CategoryId = cmbCategory.SelectedIndex;

                QBControl.SQL = txtQBQuery.Text.Trim();
                ActiveDatabaseSoftware.ActiveQueryBuilder.ParameterList pl = QBControl.Parameters;
                XElement xmlDoc = null;
                if (pl != null)
                {
                    var x = (from p in pl
                             select p);
                    xmlDoc = new XElement("parameters",
                       from p in pl
                       select new XElement("parameter",
                           new XElement("name", p.FullName),
                           new XElement("datatype", p.DataType.ToString()),
                           new XElement("datatype2",p.DataType.GetType().ToString()),
                           new XElement("comparedfield", p.ComparedField),
                           new XElement("symbol", p.Symbol),
                           new XElement("basetype", this.GetSqlDBTypeFromstring(p.MetadataField.FieldTypeName))));
                }
                if (xmlDoc == null)
                {
                    IQueryBuilder theQBuilder = (IQueryBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BQueryBuilder, BusinessProcess.FormBuilder");
                    DataTable theDT = theQBuilder.SaveCustomReport(ReportId, CategoryId, CategoryName, txtReportName.Text, txtQBQuery.Text.Trim(), GblIQCare.AppUserId);
                    ReportId = Convert.ToInt32(theDT.Rows[0][0]);
                    CategoryId = Convert.ToInt32(theDT.Rows[0][1]);
                    RepName = theDT.Rows[0][2].ToString();
                    QBControl.Enabled = true;
                    pnlSave.Visible = false;
                }
                else
                {
                    IQueryBuilder theQBuilder = (IQueryBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BQueryBuilder, BusinessProcess.FormBuilder");
                    DataTable theDT = theQBuilder.SaveCustomReport(ReportId, CategoryId, CategoryName, txtReportName.Text, txtQBQuery.Text.Trim(), GblIQCare.AppUserId,xmlDoc.ToString());
                    ReportId = Convert.ToInt32(theDT.Rows[0][0]);
                    CategoryId = Convert.ToInt32(theDT.Rows[0][1]);
                    RepName = theDT.Rows[0][2].ToString();
                    QBControl.Enabled = true;
                    pnlSave.Visible = false;
                }

            }
            catch (Exception err)
            {
                MsgBuilder Builder = new MsgBuilder();
                Builder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", Builder, this);
                return;
            }
        }

        private void btnNewCategory_Click(object sender, EventArgs e)
        {
            txtNewCategory.Visible = true;
            txtNewCategory.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            pnlSave.Visible = false;
            QBControl.Enabled = true;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            pnlSave.Visible = false;
            pnlOpen.Visible = true;
            pnlOpen.Location = new Point(246, 90);
            pnlOpen.Height = 220;
            pnlOpen.Width = 390;
            Fill_OpenCategoryCombo();
            cmbOpRepName.DataSource = null;
            QBControl.Enabled = false;
            cmbOpCategory.Focus();

            label2.Location = new Point(0, 0);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Init_Form();
        }

        private void cmbOpCategory_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                Fill_OpenReportCombo();
            }
            catch { }
        }

        private void cmdOpCancel_Click(object sender, EventArgs e)
        {
            pnlOpen.Visible = false;
            QBControl.Enabled = true;
        }

        private void cmdOpOk_Click(object sender, EventArgs e)
        {
            StringBuilder theErrorMsg = new StringBuilder();
            if (Convert.ToInt32(cmbOpCategory.SelectedValue) < 0)
            {
                theErrorMsg.Append("Select Report Category first.").AppendLine();
            }
            if (Convert.ToInt32(cmbOpRepName.SelectedValue) < 0)
            {
                theErrorMsg.Append("Select Report Name first.").AppendLine();
            }
            if (theErrorMsg.Length > 0)
            {
                MessageBox.Show("Following are mandatory:" + theErrorMsg.ToString(), "IQCare Management", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable theDt = (DataTable)cmbOpRepName.DataSource;
            DataView theDV = new DataView(theDt);
            theDV.RowFilter = "CategoryId= " + cmbOpCategory.SelectedValue + " and ReportId = " + cmbOpRepName.SelectedValue;
            if (theDV.Count > 0)
            {
                QBControl.Clear();
                txtQBQuery.Text = "";
                txtQBQuery.Text = theDV[0]["ReportQuery"].ToString();
                ReportId = Convert.ToInt32(theDV[0]["ReportId"]);
                CategoryId = Convert.ToInt32(theDV[0]["CategoryId"]);
                RepName = theDV[0]["ReportName"].ToString();
            }
            QBControl.Enabled = true;
            pnlOpen.Visible = false;

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable theDT = (DataTable)dgQResult.DataSource;
            if (theDT.Rows.Count > 0)
            {
                IQCareUtils theUtils = new IQCareUtils();
                theUtils.ExportToExcel_Windows(theDT, GblIQCare.GetExcelPath() + "QBReport.xls", GblIQCare.GetExcelPath() + "\\Templates\\BlankReport.xml");

            }
        }

        private void dgQResult_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            MsgBuilder Builder = new MsgBuilder();
            Builder.DataElements["MessageText"] = e.Exception.Message.ToString();
            IQCareWindowMsgBox.ShowWindow("#C1", Builder, this);
            dgQResult.DataSource = null;
            dgQResult.Rows.Clear();
            return;

        }

        private void btnReportExport_Click(object sender, EventArgs e)
        {
            if (ReportId > 0)
            {
                IQueryBuilder theQBuilder = (IQueryBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BQueryBuilder, BusinessProcess.FormBuilder");
                DataSet theDS = theQBuilder.ExportReport(ReportId);
                
                // Create new SaveFileDialog object
                SaveFileDialog DialogSave = new SaveFileDialog();
                DialogSave.DefaultExt = "xml";
                DialogSave.Filter = "XML file (*.xml)|*.xml";
                DialogSave.AddExtension = true;
                DialogSave.RestoreDirectory = true;
                DialogSave.Title = "Save";
                DialogSave.FileName = "QBReport.xml";
                if (DialogSave.ShowDialog() == DialogResult.OK)
                {
                    theDS.WriteXml(DialogSave.FileName);
                    IQCareWindowMsgBox.ShowWindow("PMTCTExportData", this);
                }
                
                DialogSave.Dispose();
                DialogSave = null;

               }
          }

        private void btnImportReport_Click(object sender, EventArgs e)
        {
            DataSet theDS= new DataSet();
            OpenFileDialog ReadFile = new OpenFileDialog();
            ReadFile.Title = " Open File";
            ReadFile.Filter = "XML file (*.xml)|*.xml";
            ReadFile.FilterIndex = 2;
            ReadFile.RestoreDirectory = true;
            ReadFile.FileName ="";
            if (ReadFile.ShowDialog() == DialogResult.OK)
            {
                theDS.ReadXml(ReadFile.FileName);
            }
            if (theDS.Tables.Count > 0)
            {
                if (theDS.Tables[0].Rows.Count > 0)
                {
                    IQueryBuilder theQBuilder = (IQueryBuilder)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BQueryBuilder, BusinessProcess.FormBuilder");
                    int iSave = theQBuilder.SaveUpdateQueryBuilderReport(theDS, GblIQCare.AppUserId);
                    IQCareWindowMsgBox.ShowWindow("PMTCTCustomfieldsave", this);
                }
            }

        }

        private void txtQBQuery2_Leave(object sender, EventArgs e)
        {
            //if (txtQBQuery.Modified)
            //{
            //    try
            //    {
            //        QBControl.SQL = txtQBQuery.Text;
            //    }
            //    catch (Exception err)
            //    {
            //        MsgBuilder Builder = new MsgBuilder();
            //        Builder.DataElements["MessageText"] = err.Message.ToString();
            //        IQCareWindowMsgBox.ShowWindow("#C1", Builder, this);
            //    }

            //    txtQBQuery.Modified = false;
            //}
            //Changed txtSQL to a SQL text editor for Intellisense feature
            try
            {
                QBControl.SQL = txtQBQuery.Text;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message, "Parsing error"); }
        }

        private void QBControl_Load(object sender, EventArgs e)
        {
            SetQueryBuilderConnection();
        }
        

      
    }
    }

