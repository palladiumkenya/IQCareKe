using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Entities.Administration;
using Interface.Scheduler;
using IQCare.Web.UILogic;

namespace IQCare.Web.Scheduler
{
    /// <summary>
    ///
    /// </summary>
    public partial class AppointmentMain : System.Web.UI.Page
    {
       
        
        /// <summary>
        /// Gets a value indicating whether this <see cref="AppointmentMain"/> is debug.
        /// </summary>
        /// <value>
        ///   <c>true</c> if debug; otherwise, <c>false</c>.
        /// </value>
        private bool Debug
        {
            get
            {
                bool _debug = true;
                bool.TryParse(ConfigurationManager.AppSettings.Get("DEBUG").ToLower(), out _debug);
                return _debug;
            }
        }

        /// <summary>
        /// Gets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        private int LocationId
        {
            get
            {
                return Convert.ToInt32(Session["AppLocationId"]);
            }
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        private int UserID
        {
            get
            {
                return Convert.ToInt32(Session["AppUserId"]);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("../frmFacilityHome.aspx");
            Response.Redirect(theUrl);
        }

        /// <summary>
        /// Handles the Click event of the btnExcel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = this.AppGrid.GetAppointmentListForExport();
            IQWebUtils theUtils = new IQWebUtils();
            theUtils.ExporttoExcel(dt, Response);
        }

        /// <summary>
        /// Handles the Click event of the btnOkAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOkAction_Click(object sender, EventArgs e)
        {
            this.AppGrid.Rebind();
            this.SchedulePatient.EnableModelDialog(false);
        }

        /// <summary>
        /// Handles the Click event of the Button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Master.PageScriptManager.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(PageScriptManager_AsyncPostBackError);
            //admission control
            this.AppGrid.DataLoadComplete += new EventHandler(AppGrid_DataLoadComplete);
            this.AppGrid.ErrorOccurred += new CommandEventHandler(AppGrid_ErrorOccurred);
            this.AppGrid.NotifyCommand += new CommandEventHandler(AppGrid_NotifyCommand);
            this.AppGrid.AppointmentSelectedChanged += new CommandEventHandler(AppGrid_AppointmentSelectedChanged);

            this.AppGrid.CurrentUser = new KeyValuePair<int, string>(this.UserID, Session["AppUserName"].ToString());
            this.AppGrid.FacilityId = this.LocationId;
            this.AppGrid.ShowFilterPane = true;
            this.AppGrid.PatientId = null;

            this.SchedulePatient.DataLoadComplete += new EventHandler(SchedulePatient_DataLoadComplete);
            this.SchedulePatient.ErrorOccurred += new CommandEventHandler(SchedulePatient_ErrorOccurred);
            this.SchedulePatient.NotifyCommand += new CommandEventHandler(SchedulePatient_NotifyCommand);
            this.SchedulePatient.CurrentUser = new KeyValuePair<int, string>(this.UserID, Session["AppUserName"].ToString());
            this.SchedulePatient.ModalDialogCancel += new EventHandler(SchedulePatient_ModalDialogCancel);
        }

        /// <summary>
        /// Handles the Init event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            //RTyagi..17April.07
            /***************** Check For User Rights ****************/
            AuthenticationManager Authentiaction = new AuthenticationManager();

            if (Authentiaction.HasFunctionRight(ApplicationAccess.Schedular, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
            {
                AppGrid.Visible = false;
                btnNewAppointment.Enabled = false;
            }
            if (Authentiaction.HasFunctionRight(ApplicationAccess.Schedular, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                AppGrid.Visible = false;
                btnNewAppointment.Enabled = false;
            }
            Form.EnableViewState = true;
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = 0;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Scheduler";

            if (Session["PatientStatus"] != null)
            {
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Convert.ToString(Session["PatientStatus"]);
            }

            if (!IsPostBack)
            {
                setFromToDateAndShowData();
            }
            this.InjectScript();
        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("{0}?FormName={1}&mnuClicked={2}", "./FindSchedulePatient.aspx", "AppointmentMain", "AppointmentMain");
            btnNewAppointment.OnClientClick = string.Format("javascript:window.location='{0}'; return false;", theUrl);
        }

        /// <summary>
        /// Shows the notes.
        /// </summary>
        /// <param name="noteString">The note string.</param>
        /// <returns></returns>
        protected string ShowNotes(object noteString)
        {
            if (noteString.ToString().Length > 15) return "";
            else return "none";
        }

        /// <summary>
        /// Handles the AppointmentSelectedChanged event of the AppGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void AppGrid_AppointmentSelectedChanged(object sender, CommandEventArgs e)
        {
            Appointment appointment = e.CommandArgument as Appointment;
            this.SchedulePatient.OpenMode = e.CommandName;
            this.SchedulePatient.PatientId = appointment.PatientId;
            this.SchedulePatient.SelectedAppointment = appointment;
            this.SchedulePatient.FacilityId = this.LocationId;
            this.Button1_Click(this.Button1, EventArgs.Empty);
            this.SchedulePatient.Rebind();
            
            this.SchedulePatient.EnableModelDialog(true);
            this.divScheduleComponet.Update();
            this.SchedulePatient.DataBind();
        }

        private void AppGrid_DataLoadComplete(object sender, EventArgs e)
        {
            divGridComponent.Update();
        }

        private void AppGrid_ErrorOccurred(object sender, CommandEventArgs e)
        {
            Exception ex = e.CommandArgument as Exception;
            this.showErrorMessage(ref ex);
        }

        private void AppGrid_NotifyCommand(object sender, CommandEventArgs e)
        {
            List<KeyValuePair<string, string>> param = e.CommandArgument as List<KeyValuePair<string, string>>;

            string message = param.Find(p => p.Key == "Message").Value.ToString();
            string title = param.Find(p => p.Key == "Title").Value.ToString();
            bool error = param.Find(p => p.Key == "errorFlag").Value.ToString().Equals("true");
            string script = "";
            if (param.Exists(p => p.Key == "okscript"))
            {
                script = param.Find(p => p.Key == "okscript").Value;
            }
            this.NotifyAction(message, title, error, script);
        }

        /// <summary>
        /// Injects the script.
        /// </summary>
        private void InjectScript()
        {
            string scriptPastDates = @" function disable_past_dates(sender,args){
                                                    var senderDate = new Date(sender._selectedDate);senderDate.setHours(0,0,0,0)
                                                    var nowDate =new Date();  nowDate.setHours(0,0,0,0);
                                                    if(senderDate < nowDate){
                                                        alert('You cannot select a day before today');
                                                        sender._selectedDate=new Date();sender._textbox.set_Value(sender._selectedDate.format(sender._format));    }}";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "_pastdates", scriptPastDates, true);

            string hideMpe = " function HideModalPopup() { $find(\"ptpBehavior\").hide();return false; }";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "_hideMPE", hideMpe, true);
        }

        /// <summary>
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        /// <param name="onOkScript">The on ok script.</param>
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

        /// <summary>
        /// Handles the AsyncPostBackError event of the PageScriptManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AsyncPostBackErrorEventArgs"/> instance containing the event data.</param>
        private void PageScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            string message = e.Exception.Message;
            Master.PageScriptManager.AsyncPostBackErrorMessage = message;
        }

        /// <summary>
        /// Handles the DataLoadComplete event of the AdmitPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void SchedulePatient_DataLoadComplete(object sender, EventArgs e)
        {
            divScheduleComponet.Update();
        }

        /// <summary>
        /// Handles the ErrorOccurred event of the AdmitPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs" /> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void SchedulePatient_ErrorOccurred(object sender, CommandEventArgs e)
        {
            Exception ex = e.CommandArgument as Exception;
            this.showErrorMessage(ref ex);
        }

        private void SchedulePatient_ModalDialogCancel(object sender, EventArgs e)
        {
            this.AppGrid.ClearSelectedIndex();
            this.Button1_Click(this.Button1, EventArgs.Empty);
            AppGrid.DataBind();
        }

        /// <summary>
        /// Handles the NotifyCommand event of the SchedulePatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void SchedulePatient_NotifyCommand(object sender, CommandEventArgs e)
        {
            List<KeyValuePair<string, string>> param = e.CommandArgument as List<KeyValuePair<string, string>>;

            string message = param.Find(p => p.Key == "Message").Value.ToString();
            string title = param.Find(p => p.Key == "Title").Value.ToString();
            bool error = param.Find(p => p.Key == "errorFlag").Value.ToString().Equals("true");
            string script = "";
            if (param.Exists(p => p.Key == "okscript"))
            {
                script = param.Find(p => p.Key == "okscript").Value;
            }
            this.NotifyAction(message, title, error, script);
        }

        /// <summary>
        /// Sets from to date and show data.
        /// </summary>
        private void setFromToDateAndShowData()
        {
            btnNewAppointment.CssClass = "btn btn-success";
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void showErrorMessage(ref Exception ex)
        {
          //  this.isError = true;
            if (this.Debug)
            {
                lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            }
            else
            {
                SystemSetting.LogError(ex);
                //lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team.  " + ex.Message;
                //this.isError = this.divError.Visible = true;
                //Exception lastError = ex;
                //lastError.Data.Add("Domain", "Patient Appointment Home");
                //try
                //{
                //    Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                //    logger.LogError(ex);
                //}
                //catch
                //{
                //}
            }
            notifyPopupExtender.Hide();
            this.SchedulePatient.EnableModelDialog(false);
        }

        protected void btnNewAppointment_Click(object sender, EventArgs e)
        {

        }
    }
}