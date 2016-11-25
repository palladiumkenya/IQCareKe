using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Application.Presentation;
using System.Xml;

namespace IQCare.SCM
{
    public partial class frmMasterList : Form
    {
        public frmMasterList()
        {
            InitializeComponent();
        }

        private void frmMasterList_Load(object sender, EventArgs e)
        {
            BindGrid();
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);

        }
        private void BindGrid()
        {
            //string filePath = "C:/AidsRelief Ver3.2/SourceBase/IQCare Management/XMLFiles/Masterlist.xml";
            //dsMasterList.ReadXml(filePath);
            DataSet dsMasterList = new DataSet();
            dsMasterList.ReadXml(GblIQCare.GetXMLPath() + "\\Masterlist.xml");
            if (dsMasterList.Tables[0].Rows.Count > 0)
            {
                ShowGrid(dsMasterList.Tables[0]);
            }

        }
        private void ShowGrid(DataTable theDT)
        {
            try
            {
                DataView TheDV = new DataView(theDT);

              //  TheDV.Sort = "ListName";
                IQCareUtils theUtils = new IQCareUtils();
                theDT = theUtils.CreateTableFromDataView(TheDV);

                dgwMasterList.DataSource = null;
                DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
                col1.HeaderText = "TableName";
                col1.DataPropertyName = "TableName";
                col1.Width = 225;
                col1.ReadOnly = true;
                col1.Visible = false;

                dgwMasterList.DataSource = null;
                DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn();
                col2.HeaderText = "CategoryId";
                col2.DataPropertyName = "CategoryId";
                col2.Width = 225;
                col2.ReadOnly = true;
                col2.Visible = false;

                dgwMasterList.DataSource = null;
                DataGridViewTextBoxColumn col3 = new DataGridViewTextBoxColumn();
                col3.HeaderText = "FormName";
                col3.DataPropertyName = "FormName";
                col3.Width = 225;
                col3.ReadOnly = true;
                col3.Visible = false;

                dgwMasterList.DataSource = null;
                DataGridViewTextBoxColumn col4 = new DataGridViewTextBoxColumn();
                col4.HeaderText = "Master List Name";
                col4.DataPropertyName = "ListName";
                col4.Width = 780;
                col4.ReadOnly = true;
                col4.Visible = true;

                dgwMasterList.DataSource = null;
                DataGridViewTextBoxColumn col5 = new DataGridViewTextBoxColumn();
                col5.HeaderText = "FeatureID";
                col5.DataPropertyName = "FeatureID";
                col5.Width = 225;
                col5.ReadOnly = true;
                col5.Visible = false;

                dgwMasterList.DataSource = null;
                DataGridViewTextBoxColumn col6 = new DataGridViewTextBoxColumn();
                col6.HeaderText = "Update";
                col6.DataPropertyName = "Update";
                col6.Width = 225;
                col6.ReadOnly = true;
                col6.Visible = false;

                dgwMasterList.DataSource = null;
                DataGridViewTextBoxColumn col7 = new DataGridViewTextBoxColumn();
                col7.HeaderText = "SystemId";
                col7.DataPropertyName = "SystemId";
                col7.Width = 225;
                col7.ReadOnly = true;
                col7.Visible = false;

                dgwMasterList.DataSource = null;
                DataGridViewTextBoxColumn col8 = new DataGridViewTextBoxColumn();
                col8.HeaderText = "ModuleId";
                col8.DataPropertyName = "ModuleId";
                col8.Width = 225;
                col8.ReadOnly = true;
                col8.Visible = false;

                dgwMasterList.DataSource = null;
                DataGridViewTextBoxColumn col9 = new DataGridViewTextBoxColumn();
                col9.HeaderText = "CountryID";
                col9.DataPropertyName = "CountryID";
                col9.Width = 225;
                col9.ReadOnly = true;
                col9.Visible = false;

                dgwMasterList.Columns.Add(col1);
                dgwMasterList.Columns.Add(col2);
                dgwMasterList.Columns.Add(col3);
                dgwMasterList.Columns.Add(col4);
                dgwMasterList.Columns.Add(col5);
                dgwMasterList.Columns.Add(col6);
                dgwMasterList.Columns.Add(col7);
                dgwMasterList.Columns.Add(col8);
                dgwMasterList.Columns.Add(col9);
                dgwMasterList.AutoGenerateColumns = false;
                dgwMasterList.DataSource = theDT;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }     

        private void dgwMasterList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void dgwMasterList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (dgwMasterList.Rows[e.RowIndex].Cells[2].Value != null)
            //{
            //    Form theForm = new Form();
            //    string strformname = dgwMasterList.Rows[e.RowIndex].Cells[2].Value.ToString();
            //    if (strformname.ToString() != " ")
            //    {
            //        GblIQCare.ItemLabel = dgwMasterList.Rows[e.RowIndex].Cells[3].Value.ToString();
            //        GblIQCare.ItemCategoryId = dgwMasterList.Rows[e.RowIndex].Cells[1].Value.ToString();
            //        GblIQCare.ItemTableName = dgwMasterList.Rows[e.RowIndex].Cells[0].Value.ToString();
            //        GblIQCare.ItemFeatureId = Convert.ToInt32(dgwMasterList.Rows[e.RowIndex].Cells[4].Value);
            //        theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM." + strformname + ", IQCare.SCM"));
            //        theForm.MdiParent = this.MdiParent;
            //        theForm.Left = 0;
            //        theForm.Top = 2;
            //        theForm.Show();
            //        this.Close();
            //    }
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {     
            this.Close();

        }

        private void dgwMasterList_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgwMasterList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgwMasterList.Rows[e.RowIndex].Cells[2].Value != null)
            {
                Form theForm = new Form();
                string strformname = dgwMasterList.Rows[e.RowIndex].Cells[2].Value.ToString();
                if (strformname.ToString() != " ")
                {
                    GblIQCare.ItemLabel = dgwMasterList.Rows[e.RowIndex].Cells[3].Value.ToString();
                    GblIQCare.ItemCategoryId = dgwMasterList.Rows[e.RowIndex].Cells[1].Value.ToString();
                    GblIQCare.ItemTableName = dgwMasterList.Rows[e.RowIndex].Cells[0].Value.ToString();
                    GblIQCare.ItemFeatureId = Convert.ToInt32(dgwMasterList.Rows[e.RowIndex].Cells[4].Value);
                    theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM." + strformname + ", IQCare.SCM"));
                    theForm.MdiParent = this.MdiParent;
                    theForm.Left = 0;
                    theForm.Top = 2;
                    theForm.Show();
                    this.Close();
                }
            }
        }

        private void dgwMasterList_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

       
        
    }
}
