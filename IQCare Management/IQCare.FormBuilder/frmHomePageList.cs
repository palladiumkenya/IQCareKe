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
    partial class frmHomePageList : Form
    {
        IHomePageConfiguration objTechArea;
      
        public frmHomePageList()
        {
            InitializeComponent();

        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
        private void frmHomePageList_Load(object sender, EventArgs e)
        {
        
              DataSet dsTechAreaDetails = new DataSet();
            try
            {

                //set css begin
                clsCssStyle theStyle = new clsCssStyle();
                theStyle.setStyle(this);
                //set css end
                cmbFormStatus.SelectedIndex = 2;
                objTechArea = (IHomePageConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BHomePageConfiguration,BusinessProcess.FormBuilder");
                dsTechAreaDetails = objTechArea.GetTechnicalArea();

                DataTable dt;
                dt = dsTechAreaDetails.Tables[1];
                DataRow drAddSelect;
                drAddSelect = dt.NewRow();
                drAddSelect["ModuleName"] = "All";
                drAddSelect["ModuleID"] = 0;

                dt.Rows.InsertAt(drAddSelect, 0);
                BindFunctions theBind = new BindFunctions();
                theBind.Win_BindCombo(cmbTechnicalArea1, dt, "ModuleName", "ModuleId"); 
                cmbFormStatus.SelectedIndex = 0;
                BindGrid();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindow("#C1", theBuilder, this);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            GblIQCare.iHomePageId = 0;
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmHomePage, IQCare.FormBuilder"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            this.Close();
            theForm.Show();
        }

        private void BindGrid()
        {
              DataSet dsQueryIndicaterDetails = new DataSet();

            try
            {
                int intModuleID = 0;
                int intPublished = -1;
                string strPublished = "";

                if (cmbTechnicalArea1.SelectedIndex != 0)
                {
                    intModuleID = Convert.ToInt32(cmbTechnicalArea1.SelectedValue);
                }

                if (cmbFormStatus.SelectedIndex != -1)
                {
                    strPublished = cmbFormStatus.SelectedItem.ToString();
                }

                if (strPublished == "All")
                {
                    intPublished = -1;
                }
                else if (strPublished == "Published")
                {
                    intPublished = 2;
                }
                else if (strPublished == "Un-Published")
                {
                    intPublished = 1;
                }

                objTechArea = (IHomePageConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BHomePageConfiguration,BusinessProcess.FormBuilder");
                dsQueryIndicaterDetails = objTechArea.GetHomePageIndicatorQuery(intModuleID, intPublished);

                dgwFormDetails.DataSource = null;
                if (dsQueryIndicaterDetails.Tables[0].Rows.Count > 0)
                {

                    string strGetPath = GblIQCare.GetPath();

                    ShowGrid(dsQueryIndicaterDetails.Tables[0]);
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
                col1.HeaderText = "Home Page Name";
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
                col4.Width = 150;
                col4.ReadOnly = true;

                DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
                col5.HeaderText = "Published";
                col5.DataPropertyName = "Published";
                col5.Width = 100;
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


        private void cmbFormStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void cmbTechnicalArea1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void dgwFormDetails_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

                        GblIQCare.iHomePageId = Convert.ToInt32(dgwFormDetails.Rows[e.RowIndex].Cells[5].Value.ToString());
                        Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.FormBuilder.frmHomePage, IQCare.FormBuilder"));
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

        private void dgwFormDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == -1)
                {
                    return;
                }
                DialogResult strResult = DialogResult.None;
                IHomePageConfiguration objTechArea;
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
                        objTechArea = (IHomePageConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BHomePageConfiguration, BusinessProcess.FormBuilder");
                        Int32 intExistModuleId = objTechArea.StatusUpdate(theHT);
                        BindGrid();

                    }
                    else if (Convert.ToString(dgwFormDetails.Rows[e.RowIndex].Cells[4].Value) == "Un-Published")
                    {
                        if (strResult == DialogResult.No)
                        {
                            return;
                        }
                        Hashtable theHT = new Hashtable();
                        theHT.Add("Status", "2");
                        theHT.Add("FeatureID", dgwFormDetails.Rows[e.RowIndex].Cells[5].Value);
                        DataTable theDT = new DataTable();
                        objTechArea = (IHomePageConfiguration)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BHomePageConfiguration, BusinessProcess.FormBuilder");
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
    }

    }              
            
        





        


