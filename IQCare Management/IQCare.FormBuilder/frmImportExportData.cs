using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.Threading;
using System.Xml;

using Application.Common;
using Application.Presentation;
using Interface.Security;
using Interface.FormBuilder;


namespace IQCare.FormBuilder
{

    public partial class frmImportExportData : Form
    {
        public frmImportExportData()
        {
            InitializeComponent();
        }

        private void frmImportExportData_Load(object sender, EventArgs e)
        {
            clsCssStyle theStyle = new clsCssStyle();
            theStyle.setStyle(this);
            Init_Form();
        }

        private void Init_Form()
        {

            Bind_Combo();
            rdoImport.Checked = false;
            rdoExport.Checked = false;
            grpExport.Enabled = false;
            grpImport.Enabled = false;
            txtFileName.Text = "";
            cmbExpLocation.SelectedValue = "0";
            cmbImpLocation.SelectedValue = "0";

        }

        private void Bind_Combo()
        {
            DataTable theDT = new DataTable();
            IUser UserManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser,BusinessProcess.Security");
            theDT = UserManager.GetFacilityList();
            BindFunctions theBind = new BindFunctions();
            theBind.Win_BindCombo(cmbExpLocation, theDT.Copy(), "FacilityName", "FacilityId");
            theBind.Win_BindCombo(cmbImpLocation, theDT.Copy(), "FacilityName", "FacilityId");
        }

        private void rdoImport_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoImport.Checked == true)
                grpImport.Enabled = true;
            else
                grpImport.Enabled = false;
        }

        private void rdoExport_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoExport.Checked == true)
                grpExport.Enabled = true;
            else
                grpExport.Enabled = false;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToInt32(cmbExpLocation.SelectedValue) < 1)
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "Export Location";
                    IQCareWindowMsgBox.ShowWindowConfirm("BlankDropDown", theBuilder, this);
                    return;
                }

                if (Convert.ToDateTime(dtFrmDate.Value) > Convert.ToDateTime(dtToDate.Value))
                {
                    IQCareWindowMsgBox.ShowWindow("FromDTgreaterToDate", this);
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                IImportExport theExportManager = (IImportExport)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BImportExport, BusinessProcess.FormBuilder");
                DataTable theDT = theExportManager.ExportIQCareDB(Convert.ToInt32(cmbExpLocation.SelectedValue), dtFrmDate.Value, dtToDate.Value);
                this.Cursor = Cursors.Default;
                Int32 theRec = theDT.Rows.Count - 1;
                MsgBuilder theExpBuilder = new MsgBuilder();
                theExpBuilder.DataElements["Control"] = theDT.Rows[theRec][0].ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("ExportSuccess", theExpBuilder, this);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFile.DefaultExt = "*.bak";
            openFile.ShowDialog();
            txtFileName.Text = openFile.FileName;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFileName.Text.Trim() == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["MessageText"] = "Import Database";
                    IQCareWindowMsgBox.ShowWindowConfirm("BlankTextBox", theBuilder, this);
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                IImportExport theImportManager = (IImportExport)ObjectFactory.CreateInstance("BusinessProcess.FormBuilder.BImportExport, BusinessProcess.FormBuilder");
                theImportManager.ImportIQCareData(Convert.ToInt32(cmbImpLocation.SelectedValue), txtFileName.Text.ToString());
                this.Cursor = Cursors.Default;
                IQCareWindowMsgBox.ShowWindowConfirm("ImportDBSuccess", this);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareWindowMsgBox.ShowWindowConfirm("#C1", theBuilder, this);
                return;
            }
        }
    }

}
