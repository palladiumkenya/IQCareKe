using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Application.Common;
using Application.Presentation;
using Interface.Administration;
namespace IQCare.Web.Admin
{
    public partial class DBBackupSetup : BasePage
    {
        private void Init_Page()
        {
            IQCareUtils theUtil = new IQCareUtils();
            DataTable theDT = theUtil.CreateTimeTable(15);
            DataRow theDR = theDT.NewRow();
            theDR[0] = "0";
            theDR[1] = "Select";
            theDT.Rows.InsertAt(theDR, 0);
            ddBackupTime.DataSource = theDT;
            ddBackupTime.DataTextField = "Time";
            ddBackupTime.DataValueField = "Id";
            ddBackupTime.DataBind();
            ddBackupDrive.SelectedValue = "Select";

            IFacilitySetup BackupManger = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
            theDT = BackupManger.GetBackupSetup();
            if (theDT.Rows[0].IsNull("BackupTime") != true)
                ddBackupTime.SelectedValue = ((DateTime)theDT.Rows[0]["BackupTime"]).TimeOfDay.ToString();

            ddBackupDrive.SelectedValue = theDT.Rows[0]["BackupDrive"].ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack != true)
            {
                Init_Page();
            }
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                IQCareUtils theUtils = new IQCareUtils();
                DateTime theTime;
                if (ddBackupTime.SelectedValue != "0")
                    theTime = Convert.ToDateTime(ddBackupTime.SelectedItem.ToString());
                else
                    theTime = Convert.ToDateTime("1900-01-01");

                IFacilitySetup BackupManger = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
                int noRows = BackupManger.SaveBackupSetup(ddBackupDrive.SelectedItem.ToString(), theTime);
                BackupManger = null;
                IQCareMsgBox.Show("BackupSetupSave", this);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }
    }
}