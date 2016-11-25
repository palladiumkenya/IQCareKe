using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Application.Common;
using Interface.FormBuilder;
using Application.Presentation;

namespace IQCare.FormBuilder
{
    public partial class Gridviewform : Form
    {
        public Gridviewform()
        {
            InitializeComponent();
        }

        private void Gridviewform_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            //Int32 i = 0;
            BindFunctions objBindControls = new BindFunctions();
            IManageForms objManageForms = (IManageForms)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BManageForms,BusinessProcess.FormBuilder");
            DataSet dsModule = objManageForms.GetPublishedModuleList();

            //dgwDetails.DataSource = null;
            //dgwDetails.AutoGenerateColumns = false;
            //dgwDetails.Columns.Clear();
           // dgwDetails.Column[0].Visible = false; 

            
            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn();
            col1.HeaderText = "ModuleName";
            col1.DataPropertyName = "ModuleName";
            col1.Width = 268;
            col1.ReadOnly = true;

            //dgwDetails.TopLeftHeaderCell.
            DataGridViewTextBoxColumn theColumnId = new DataGridViewTextBoxColumn();
            theColumnId.HeaderText = "ModuleId";
            theColumnId.DataPropertyName = "ModuleId";
            theColumnId.Width = 10;
            theColumnId.Visible = false;

            dgwDetails.RowHeadersVisible = false; 
            dgwDetails.Columns.Add(col1);
            dgwDetails.Columns.Add(theColumnId);

           


             dgwDetails.DataSource = dsModule.Tables[0];
           // foreach (DataRow theDR in dsModule.Tables[0].Rows)
           // {
           //     dgwDetails.Columns.Add(theDR["ModuleName"].ToString(),i);
           //     dgwDetails.Columns.Add(theDR["ModuleName"].ToString();
           //     //i = i + 1;
           //}
        }

       
    }
}
