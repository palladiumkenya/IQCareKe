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
    public partial class frmStoreSourcDestLinking : Form
    {
        DataTable DTLinkStore = new DataTable();
        DataSet theDS = new DataSet();
        Button theButton;
        public frmStoreSourcDestLinking()
        {
            InitializeComponent();
        }

        private void frmStoreSourcDestLinking_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            theButton = new Button();
            theButton.Click +=new EventHandler(theButton_Click);
            theButton.PerformClick();
            SetRights();
        }

        public void SetRights()
        {
            //form level permission set
            if (GblIQCare.HasFunctionRight(ApplicationAccess.StoreSourceDestinationLinking, FunctionAccess.Add, GblIQCare.dtUserRight) == false)
            {
                btnSave.Enabled = false;
            }
            
        }

        private void BindDropdown(DataTable theDT)
        {
            BindFunctions theBind = new BindFunctions();
            //Source Combo
            DataView theDVSource = new DataView(theDT);
            //theDVSource.RowFilter = "CentralStore=1 and DeleteFlag=0";
            theDVSource.RowFilter = "DeleteFlag=0";
            DataTable theDTSource = theDVSource.ToTable();
            theBind.Win_BindCombo(cmbSourceStore, theDTSource, "Name", "Id");
            //Destination Combo
            DataView theDVDestination = new DataView(theDT);
            theDVDestination.RowFilter = "DeleteFlag=0";
            DataTable theDTDestination = theDVDestination.ToTable();
            theBind.Win_BindCombo(cmbDestinationStore, theDTDestination, "Name", "Id");
        }

        private void BindDropdownonChange(DataTable theDT, int SourceId)
        {
            BindFunctions theBind = new BindFunctions();
            //Destination Combo
            DataView theDVDestination = new DataView(theDT);
            theDVDestination.RowFilter = "DeleteFlag=0 and Id <> "+SourceId+"";
            DataTable theDTDestination = theDVDestination.ToTable();
            theBind.Win_BindCombo(cmbDestinationStore, theDTDestination, "Name", "Id");
        }

        private Boolean Validation_Form()
        {
            try
            {
                if (Convert.ToInt32(cmbSourceStore.SelectedValue) == 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Source Store";
                    IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                    return false;
                }
                else if (Convert.ToInt32(cmbDestinationStore.SelectedValue) == 0)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Destination Store";
                    IQCareWindowMsgBox.ShowWindow("BlankDropDown", theBuilder, this);
                    return false;
                }
                else if (Convert.ToInt32(cmbSourceStore.SelectedValue) == Convert.ToInt32(cmbDestinationStore.SelectedValue))
                {
                    IQCareWindowMsgBox.ShowWindow("CheckStoreSourceDest", this);
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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            
            DTLinkStore = (DataTable)dgwStoreName.DataSource;
            if (Validation_Form() == true)
            {

                try
                {
                    DTLinkStore.PrimaryKey = new DataColumn[] { DTLinkStore.Columns["SourceStore"], DTLinkStore.Columns["DestinationStore"] };
                    DataRow theDR = DTLinkStore.NewRow();
                    theDR["SourceStore"] = cmbSourceStore.SelectedValue;
                    theDR["SourceStoreName"] = cmbSourceStore.Text;
                    theDR["DestinationStore"] = cmbDestinationStore.SelectedValue;
                    theDR["DestStoreName"] = cmbDestinationStore.Text;
                    DTLinkStore.Rows.Add(theDR);
                    DTLinkStore.AcceptChanges();
                    showgrid(DTLinkStore);
                }
                catch (Exception err)
                {
                    if (err.GetType().FullName == "System.Data.ConstraintException")
                    {
                        IQCareWindowMsgBox.ShowWindow("SourceDestAlreadyExist", this);
                    }
                  
                }
            }
        }

        private void showgrid(DataTable theDT)
        {
            dgwStoreName.Columns.Clear();
            dgwStoreName.DataSource = null;
            ColSourceID.DataPropertyName = "SourceStore";
            ColSourceName.DataPropertyName = "SourceStoreName";
            ColDestId.DataPropertyName = "DestinationStore";
            ColDestinationName.DataPropertyName = "DestStoreName";

            dgwStoreName.Columns.Add(ColSourceID);
            dgwStoreName.Columns.Add(ColSourceName);
            dgwStoreName.Columns.Add(ColDestId);
            dgwStoreName.Columns.Add(ColDestinationName);
            dgwStoreName.AutoGenerateColumns = false;
            dgwStoreName.DataSource = theDT;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Form theForm = new Form();
            theForm = (Form)Activator.CreateInstance(Type.GetType("IQCare.SCM.frmMasterList, IQCare.SCM"));
            theForm.MdiParent = this.MdiParent;
            theForm.Left = 0;
            theForm.Top = 2;
            theForm.Show();
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                String TableName = "lnk_" + GblIQCare.ItemTableName;
                IMasterList objItemCommonlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                int retrows = objItemCommonlist.SaveUpdateStoreLinking(DTLinkStore, TableName, GblIQCare.AppUserId);
                theButton.PerformClick();
                IQCareWindowMsgBox.ShowWindow("ProgramSave", this);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }

        }

        private void theButton_Click(object sender, EventArgs e)
        {
            try
            {
                IMasterList objItemCommonlist = (IMasterList)ObjectFactory.CreateInstance("BusinessProcess.SCM.BMasterList,BusinessProcess.SCM");
                IQCareUtils theUtils = new IQCareUtils();
                theDS = objItemCommonlist.GetStoreDetail();
                BindDropdown(theDS.Tables[0]);
                showgrid(theDS.Tables[1]);
                objItemCommonlist = null;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
            }
        }

        private void dgwStoreName_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    cmbSourceStore.SelectedValue = dgwStoreName.Rows[e.RowIndex].Cells[0].Value.ToString();
                    BindDropdownonChange(theDS.Tables[0], Convert.ToInt32(cmbSourceStore.SelectedValue));
                    cmbDestinationStore.SelectedValue = dgwStoreName.Rows[e.RowIndex].Cells[2].Value.ToString();
                 }
                catch (Exception err)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = err.Message.ToString();
                    IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                }
            }


        }

        private void cmbSourceStore_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BindDropdownonChange(theDS.Tables[0], Convert.ToInt32(cmbSourceStore.SelectedValue));
        }

    }
}
