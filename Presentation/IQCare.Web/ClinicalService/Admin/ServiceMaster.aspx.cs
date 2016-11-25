using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using Application.Presentation;
using Interface.Clinical;
using Entities.Clinical;
using IQCare.Web.UILogic;

namespace IQCare.Web.ClinicalService.Admin
{
    public partial class ServiceMaster : System.Web.UI.Page
    {
        private bool isError = false;
        IServiceManager mGr = (IServiceManager)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BServicesManager, BusinessProcess.Clinical");
        int UserId
        {
            get
            {
                return Convert.ToInt32(base.Session["AppUserId"]);
            }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="frmBilling_ReverseBill"/> is debug.
        /// </summary>
        /// <value>
        ///   <c>true</c> if debug; otherwise, <c>false</c>.
        /// </value>
        bool Debug
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("DEBUG").ToLower().Equals("true");
            }
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void ShowErrorMessage(ref Exception ex)
        {
            this.isError = true;
            if (this.Debug)
            {
                lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            }
            else
            {
                lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team.  " + ex.Message;
                this.isError = this.divError.Visible = true;
                Exception lastError = ex;
                lastError.Data.Add("Domain", "Lab Management");
                SystemSetting.LogError(ex);
                //try
                //{
                //    Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                //    logger.LogError(ex);
                //}
                //catch
                //{

                //}
            }
        }
        AuthenticationManager Authentication = new AuthenticationManager();
        protected bool HasPermission
        {
            get
            {
                return (Authentication.HasFeatureRight("MANAGE_CLINICALSERIVICES", (DataTable)Session["UserRight"]) == true);
            }
        }
        private void NotifyAction(string strMessage, string strTitle, bool errorFlag, string onOkScript = "")
        {
            lblNoticeInfo.Text = strMessage;
            lblNotice.Text = strTitle;
            lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
            lblNoticeInfo.Font.Bold = true;
            imgNotice.ImageUrl = (errorFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
            btnOkAction.OnClientClick = "";
            if (onOkScript != "")
            {
                btnOkAction.OnClientClick = onOkScript;
            }
            this.notifyPopupExtender.Show();
        }
        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
            Exception ex = Server.GetLastError();
            this.ShowErrorMessage(ref ex);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
             Session["PatientId"] = 0;
             (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Services >> ";
             (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Services";
             if (!IsPostBack)
             {
                 base.Session["servicelist"] = null;
                 this.PopulateModules();
                 this.PopulateServices();
             }
        }
        protected string svPerm
        {
            get
            {
                return HasPermission ? "" : "none";
            }
        }
        List<Service> ServiceList
        {
            set
            {
                base.Session["servicelist"] = value;
            }
            get
            {
                return base.Session["servicelist"] == null ? new List<Service>() : (List<Service>)base.Session["servicelist"];
            }
        }
        private void PopulateServices()
        {
           this.ServiceList = mGr.GetServices();
           gridServiceMaster.DataSource = this.ServiceList;
            gridServiceMaster.DataBind();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            divError.Visible = this.isError;
            
            btnAdd.OnClientClick = "javascript:ShowModalPopup(); return false;";
            
        }
        void PopulateModules()
        {
            try
            {
                BindFunctions BindManager = new BindFunctions();
                IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                DataSet DSModules = ptnMgr.GetModuleNames(Convert.ToInt32(Session["AppLocationId"]));

                DataTable theDT = new DataTable();
                theDT = DSModules.Tables[0];

                if (theDT.Rows.Count > 0)
                {
                    BindManager.BindCombo(ddlServiceArea, theDT, "ModuleName", "ModuleID");
                    ptnMgr = null;
                }
            }
            catch (Exception ex)
            {

                this.ShowErrorMessage(ref ex);
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
             try
            {
                string strName = textServiceName.Text;
                string strDescription = textDescription.Text;
                string strModule = ddlServiceArea.SelectedValue;
                string strModuleName = ddlServiceArea.SelectedItem.Text;
                if (this.ServiceList.Exists(m => m.Name == strName))
                {
                    this.NotifyAction("A service with a similar name exist", "Duplication", true, "");
                    return;
                }
                 Service service = new Service()
                 {
                    Name=strName,
                    Description = strDescription,
                    DeleteFlag =false,
                    ServiceAreaId = Convert.ToInt32(strModule),
                    ServiceArea = strModuleName
                 };
                 mGr.AddService(service, this.UserId);
                 this.PopulateServices();
                 this.NotifyAction("A new service has been saved ", "Success", false, "");
            }
             catch (Exception ex)
             {

                 this.ShowErrorMessage(ref ex);
             }
        }

        protected void gridServiceMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Service row = ((Service)e.Row.DataItem);
                    e.Row.Cells[3].Text = row.DeleteFlag ? "In Active" : "Active";

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }

        protected void gridServiceMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
             try
            {
                if (e.CommandName == "DeleteService")
                {
                    int serviceId = Int32.Parse(e.CommandArgument.ToString());
                    mGr.DeleteService(serviceId, this.UserId);
                    this.NotifyAction("Service has been deleted", "Success Deletion", false, "");
                    this.PopulateServices();
                    return;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }
    }
}