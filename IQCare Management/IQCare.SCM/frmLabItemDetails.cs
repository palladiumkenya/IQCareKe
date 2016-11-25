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
using Interface.SCM;

namespace IQCare.SCM
{
     
    public partial class frmLabItemDetails : Form
    {
        DataTable DTLablist = new DataTable();
        ILaboratory objItemCommonlist = (ILaboratory)ObjectFactory.CreateInstance("BusinessProcess.SCM.BLaboratory,BusinessProcess.SCM");

        public frmLabItemDetails()
        {
            InitializeComponent();
        }

        private void frmLabItemDetails_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Init_Form();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {

        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList,IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            this.Close();
            
        }
        private void Init_Form()
        {            
            DTLablist = objItemCommonlist.GetLabList(0);
            if (DTLablist.Rows.Count>0)
            {
                ShowGrid(DTLablist);
            }
            //Clear_Form();
        }
        private void ShowGrid(DataTable theDT)
        {
            try
            {
                dgwItemSubitemDetails.Columns.Clear();
                dgwItemSubitemDetails.DataSource = null;
                SubtestId.DataPropertyName = "SubTestId";
                LOINCCode.DataPropertyName = "LOINCCode";
                Labtest.DataPropertyName = "SubTestName";
                LabLocation.DataPropertyName = "LocationName";
                LabCost.DataPropertyName = "TestCostPrice";
                MarginPerc.DataPropertyName = "TestMargin";
                
                EffectiveDate.DataPropertyName = "EffectiveDate";
                Status.DataPropertyName = "Status";

                dgwItemSubitemDetails.Columns.Add(SubtestId);
                SubtestId.Visible = false;
                dgwItemSubitemDetails.Columns.Add(LOINCCode);
                dgwItemSubitemDetails.Columns.Add(Labtest);
                dgwItemSubitemDetails.Columns.Add(LabLocation);
                dgwItemSubitemDetails.Columns.Add(LabCost);
                dgwItemSubitemDetails.Columns.Add(MarginPerc);
                dgwItemSubitemDetails.Columns.Add(EffectiveDate);
                dgwItemSubitemDetails.Columns.Add(Status);
                dgwItemSubitemDetails.AutoGenerateColumns = false;
                dgwItemSubitemDetails.DataSource = theDT;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }
        private void Clear_Form()
        {
            
            txtLabTest.Text = "";
            txtLabTest.Focus();

        }

        private void dgwItemSubitemDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex != -1 && e.RowIndex != -1)
                {
                    if (dgwItemSubitemDetails.Rows[e.RowIndex].Cells[0].Value.ToString() != "")
                    {
                        GblIQCare.LabTestId = Convert.ToInt32(dgwItemSubitemDetails.Rows[e.RowIndex].Cells[0].Value.ToString());
                    }
                    //txtItemName.Text = dgwItemSubitemDetails.Rows[e.RowIndex].Cells[2].Value.ToString();

                    dgwItemSubitemDetails.Rows[dgwItemSubitemDetails.CurrentCell.RowIndex].Selected = true;
                    Form theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmConfigureLabTest, IQCare.SCM"));
                    theForm.MdiParent = this.MdiParent;
                    theForm.Left = 0;
                    theForm.Top = 0;
                    this.Close();
                    theForm.Show();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DTLablist = objItemCommonlist.GetLabList(0);
            if (DTLablist.Rows.Count > 0)
            {
                DataView dv = DTLablist.DefaultView; ;

                dv.RowFilter = "SubTestName LIKE '%" + txtLabTest.Text.Replace("'", "") + "%'";

                DataTable dtfilter = dv.ToTable();

                ShowGrid(dtfilter);
            }
        }
    }
}
