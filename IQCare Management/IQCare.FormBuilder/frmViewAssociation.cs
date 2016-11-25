using System;
using System.Data;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using Interface.FormBuilder;


    namespace IQCare.FormBuilder
    {
    public partial class frmViewAssociation : Form
    {
        IViewAssociation objViewAssociation;
        DataSet objDsViewAssociation = new DataSet();
        string FieldName = "";
        int ModuleId = 0;
        public frmViewAssociation()
        {
            InitializeComponent();
        }

        private void frmViewAssociation_Load(object sender, EventArgs e)
        {
           //----CSS Set Begin------------------------------
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
           //----CSS Set End------------------------------

            objViewAssociation = (IViewAssociation)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BViewAssociation,BusinessProcess.FormBuilder");
            objDsViewAssociation = objViewAssociation.GetMoudleName();
            DataTable dt;
            dt = objDsViewAssociation.Tables[0];
            DataRow drAddSelect;
            drAddSelect = dt.NewRow();
            drAddSelect["ModuleName"] = "All";
            drAddSelect["ModuleID"] = 0;
            dt.Rows.InsertAt(drAddSelect, 0);
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(cmbTechnicalArea1, dt, "ModuleName", "ModuleId");
      
            BindGrid();
         }
        public Boolean BindGrid()
        {
          
            try
            {
                objViewAssociation = (IViewAssociation)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BViewAssociation,BusinessProcess.FormBuilder");
                objDsViewAssociation = objViewAssociation.GetViewAssociationFields(FieldName, ModuleId);
                if (objDsViewAssociation.Tables[0].Rows.Count > 0)
                {
                    ShowGrid(objDsViewAssociation.Tables[0]);
                }
                else
                {
                    return false;
                }
               
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
            return true;       

        }
        public void ShowGrid(DataTable dt)
        {

            try
            {
                dgwViewAssociation.DataSource = null;
                dgwViewAssociation.Rows.Clear();
                dgwViewAssociation.Columns.Clear();

                DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                col1.HeaderText = " Field Name";
                col1.DataPropertyName = "Field Name";
                col1.Width = 170;
                col1.ReadOnly = true;


                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = " Display Type";
                col2.DataPropertyName = "DisplayType";
                col2.Width = 107;
                col2.ReadOnly = true;

                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col3.HeaderText = "Predefined";
                col3.DataPropertyName = "Predefined";
                col3.Width = 80;
                col3.ReadOnly = true;

                DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
                col4.HeaderText = "Section Name";
                col4.DataPropertyName = "Section Name";
                col4.Width = 135;
                col4.ReadOnly = true;

                DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
                col5.HeaderText = "Form Used In";
                col5.DataPropertyName = "Form Used In";
                col5.Width = 140;
                col5.ReadOnly = true;

                DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
                col6.HeaderText = "Service";
                col6.DataPropertyName = "Module Name";
                col6.Width = 150;
                col6.ReadOnly = true;
                DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
                col7.HeaderText = "Published";
                col7.DataPropertyName = "Published";
                col7.Width = 100;
                col7.ReadOnly = true;

                dgwViewAssociation.AutoGenerateColumns = false;
                dgwViewAssociation.AllowUserToAddRows = false;
                dgwViewAssociation.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
                dgwViewAssociation.DefaultCellStyle.Font.Size.Equals(12);
                dgwViewAssociation.ColumnHeadersDefaultCellStyle.Font.Bold.Equals(true);

                dgwViewAssociation.Columns.Add(col1);
                dgwViewAssociation.Columns.Add(col2);
                dgwViewAssociation.Columns.Add(col3);
                dgwViewAssociation.Columns.Add(col4);
                dgwViewAssociation.Columns.Add(col5);
                dgwViewAssociation.Columns.Add(col6);
                dgwViewAssociation.Columns.Add(col7);

                dgwViewAssociation.DataSource = dt;
                
            }
            catch (Exception err)
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

         private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtFieldName.Text != "")
            {
                FieldName = txtFieldName.Text;
                BindGrid();               
            }
            else
            {
                IQCareWindowMsgBox.ShowWindow("BlankFieldName",this);
                return;
            }
            if (txtFieldName.Text == "")
            {
                ModuleId = Convert.ToInt32(cmbTechnicalArea1.SelectedValue);
                BindGrid();
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {

            FieldName = "";         
            BindGrid();
        }

        private void cmbTechnicalArea1_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (txtFieldName.Text == "")
            {
                dgwViewAssociation.DataSource = null;
                dgwViewAssociation.Rows.Clear();
                dgwViewAssociation.Columns.Clear();

                ModuleId = Convert.ToInt32(cmbTechnicalArea1.SelectedValue);
                BindGrid();
            }
             dgwViewAssociation.DataSource = null;
                dgwViewAssociation.Rows.Clear();
                dgwViewAssociation.Columns.Clear();

                ModuleId = Convert.ToInt32(cmbTechnicalArea1.SelectedValue);
                BindGrid();
        }
    }
    }

