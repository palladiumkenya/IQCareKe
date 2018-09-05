using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.Administration;
using Interface.Security;
using IQCare.Web.UILogic;

namespace IQCare.Web.Admin
{
    public partial class DBBackup : BasePage
    {
        protected string showRestore
        {
            get
            {
                CurrentSession session = CurrentSession.Current;
                return session.HasFeaturePermission("RESTORE_DATABASE") ? "" : "none";
            }
        }
        protected string showSetup
        {
            get
            {
                CurrentSession session = CurrentSession.Current;
                return session.HasFeaturePermission("SETUP_DATABASE_BACKUP") ? "" : "none";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = 0;
            //(Master.FindControl("lblheaderfacility") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "Back up/Restore";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Back up/Restore";



            if (IsPostBack == false)
            {
                txtbakuppath.Text = Session["BackupDrive"].ToString() + "\\IQCareDBBackup";
                txtbakuppath.Attributes.Add("readonly", "true");
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
                {
                    ddBackupTime.SelectedValue = ((DateTime)theDT.Rows[0]["BackupTime"]).TimeOfDay.ToString();

                }
                if (theDT.Rows[0].IsNull("BackupDrive") != true)
                {
                    txtbakuppath.Text = theDT.Rows[0]["BackupDrive"].ToString() + "\\IQCareDBBackup";
                    ddBackupDrive.SelectedValue = theDT.Rows[0]["BackupDrive"].ToString();
                }
            }
            if (Application["BackupSetFile"] != null)
            {
                txtRestore.Text = Application["BackupSetFile"].ToString();
                ViewState["BkpPosition"] = Application["Position"].ToString();
                //chkDeidentified.Checked = false;
                Application.Remove("BackupSetFile");
                Application.Remove("Position");
            }



        }

        protected void btnBackup_Click(object sender, EventArgs e)
        {
            try
            {

                IIQCareSystem DBManager;
                DBManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                if (chkDeidentified.Checked == true)
                {
                    DBManager.DataBaseBackup(txtbakuppath.Text, Convert.ToInt32(Session["AppLocationId"]), 1);
                }
                else
                {
                    DBManager.DataBaseBackup(txtbakuppath.Text, Convert.ToInt32(Session["AppLocationId"]), 0);
                }
                //IQCareMsgBox.Show("DataBackup", this);

                IQCareMsgBox.NotifyAction("Database backup completed successfully", "Database Backup", false, this, "");
            }
            catch (Exception err)
            {
                //MsgBuilder theBuilder = new MsgBuilder();
                //theBuilder.DataElements["MessageText"] = err.Message.ToString();
                //IQCareMsgBox.Show("#C1", theBuilder, this);
                IQCareMsgBox.NotifyAction(err.Message, "Database Backup", true, this, "");
            }
        }
        protected void btnBrowse_Click(object sender, EventArgs e)
        {
            string theScript;
            string thePath = txtbakuppath.Text;
            thePath = thePath.Insert(3, "\\");
            theScript = "<script language='javascript' id='BkpPopup'>\n";
            theScript += "window.open('frmBackupset.aspx?drv=" + thePath + "','BackupDevice','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=550,scrollbars=yes');\n";
            theScript += "</script>\n";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "BkpPopup", theScript);

        }
        protected void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.showRestore == "")
                {
                    IIQCareSystem DBManager;
                    DBManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                    DBManager.RestoreDataBase(txtRestore.Text.Trim(), Convert.ToInt32(ViewState["BkpPosition"]));
                    //IQCareMsgBox.Show("DataRestore", this);
                    IQCareMsgBox.NotifyAction("Database restore completed successfully", "Database Restore", false, this, "");
                }
                else
                {
                    IQCareMsgBox.NotifyAction("Your are not authorized to restore database", "Database Restore", true, this, "");
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect("~/frmLogOff.aspx", false);
                }
            }
            catch (Exception err)
            {
                //MsgBuilder theBuilder = new MsgBuilder();
                //theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.NotifyAction(err.Message, "Database Restore", true, this, "");

            }
        }



        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmFacilityHome.aspx");
        }

        protected void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (showSetup == "")
                {
                    IQCareUtils theUtils = new IQCareUtils();
                    DateTime theTime;
                    string timeStr = Request.Form[ddBackupTime.UniqueID].ToString();
                    string driveStr = Request.Form[ddBackupDrive.UniqueID].ToString();

                    if (timeStr != "0" || timeStr != "")
                        theTime = Convert.ToDateTime(timeStr);
                    else
                        theTime = Convert.ToDateTime("1900-01-01");
                    Session["BackupDrive"] = driveStr;
                    IFacilitySetup BackupManger = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
                    int noRows = BackupManger.SaveBackupSetup(driveStr, theTime);
                    BackupManger = null;
                    //IQCareMsgBox.Show("BackupSetupSave", this);
                    IQCareMsgBox.NotifyAction("Backup Setup Updated Successfully", "Database Backup Setup", false, this, "window.location.href='../AdminForms/frmDBBackup.aspx'");
                }
                else
                {
                    IQCareMsgBox.NotifyAction("Your are not authorized to setup database backup", "Database Backup Setup", true, this, "");
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect("~/frmLogOff.aspx", false);
                }
            }
            catch (Exception err)
            {
                //MsgBuilder theBuilder = new MsgBuilder();
                //theBuilder.DataElements["MessageText"] = err.Message.ToString();
                //IQCareMsgBox.Show("#C1", theBuilder, this);
                IQCareMsgBox.NotifyAction(err.Message, "Database Backup Setup", true, this, "");
            }
        }
    }
}