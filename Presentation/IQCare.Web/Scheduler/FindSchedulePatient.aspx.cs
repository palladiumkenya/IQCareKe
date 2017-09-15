using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using System.Data;
using Application.Presentation;
using System.Configuration;
using IQCare.Web.UILogic;

namespace IQCare.Web.Scheduler
{
    public partial class FindSchedulePatient : System.Web.UI.Page
    {
        AuthenticationManager Authentication = new AuthenticationManager();
        /// <summary>
        /// The is error
        /// </summary>
        private bool isError = false;

        /// <summary>
        /// Gets a value indicating whether this instance can admit.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can admit; otherwise, <c>false</c>.
        /// </value>
        private bool CanSchedule
        {
            get
            {
                return (Authentication.HasFeatureRight(ApplicationAccess.SchedularAppointment, (DataTable)Session["UserRight"]) == true);
            }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="frmAdmissionHome"/> is debug.
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
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        private int UserID
        {
            get
            {
                return Convert.ToInt32(base.Session["AppUserId"]);
            }
        }
        #region page-lifecycle

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.FindPatient.CancelFind += new EventHandler(FindPatient_CancelFind);
            this.FindPatient.NotifyParent += new CommandEventHandler(FindPatient_NotifyParent);
            this.FindPatient.SelectedPatientChanged += new CommandEventHandler(FindPatient_SelectedPatientChanged);
            Master.PageScriptManager.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(PageScriptManager_AsyncPostBackError);
            //admission control
            this.SchedulePatient.DataLoadComplete += new EventHandler(SchedulePatient_DataLoadComplete);
            this.SchedulePatient.ErrorOccurred += new CommandEventHandler(SchedulePatient_ErrorOccurred);
            this.SchedulePatient.NotifyCommand += new CommandEventHandler(SchedulePatient_NotifyCommand);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!this.CanSchedule)
                {
                    string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                    //Response.Redirect(theUrl);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
                HFormName.Value = Request.QueryString["FormName"].ToString();
                
            }
           
        }
     

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            divError.Visible = this.isError;
            btnBack.OnClientClick = "javascript:window.location='./frmScheduler_AppointmentMain.aspx'; return false;";
        }

        #endregion page-lifecycle

        #region event-handlers

        /// <summary>
        /// Handles the Click event of the Button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the DataLoadComplete event of the AdmitPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void SchedulePatient_DataLoadComplete(object sender, EventArgs e)
        {
            divScheduleComponet.Update();
        }

        /// <summary>
        /// Handles the ErrorOccurred event of the AdmitPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void SchedulePatient_ErrorOccurred(object sender, CommandEventArgs e)
        {
            Exception ex = e.CommandArgument as Exception;
            this.showErrorMessage(ref ex);
        }

        /// <summary>
        /// Handles the NotifyCommand event of the AdmitPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void SchedulePatient_NotifyCommand(object sender, CommandEventArgs e)
        {
            List<KeyValuePair<string, string>> param = e.CommandArgument as List<KeyValuePair<string, string>>;

            string message = param.Find(p => p.Key == "Message").Value.ToString();
            string title = param.Find(p => p.Key == "Title").Value.ToString();
            bool error = param.Find(p => p.Key == "errorFlag").Value.ToString().Equals("true");
            if (e.CommandName == "Success")
            {
                IQCareMsgBox.NotifyAction(message, title, error,this, "javascript:window.location='./frmScheduler_AppointmentMain.aspx'; return true;");
            }
            else
            {
                IQCareMsgBox.NotifyAction(message, title, error,this, "");
            }
        }

        /// <summary>
        /// Handles the CancelFind event of the FindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void FindPatient_CancelFind(object sender, EventArgs e)
        {
            if (HFormName.Value == "") return;

            string formName = HFormName.Value;

            if (formName == "AppointmentMain")
            {
                Response.Redirect("~/Scheduler/frmScheduler_AppointmentMain.aspx");
            }
            else
            {
                Response.Redirect("~/frmFacilityHome.aspx");
            }
        }

        /// <summary>
        /// Handles the NotifyParent event of the FindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void FindPatient_NotifyParent(object sender, CommandEventArgs e)
        {
            string commandName = e.CommandName;
            if (e.CommandArgument != null)
            {
                MsgBuilder theBuilder = (MsgBuilder)e.CommandArgument;
                IQCareMsgBox.Show(commandName, theBuilder, this);
            }
            else
            {
                IQCareMsgBox.Show(commandName, this);
            }
            return;
        }

        /// <summary>
        /// Handles the SelectedPatientChanged event of the FindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void FindPatient_SelectedPatientChanged(object sender, CommandEventArgs e)
        {
            List<KeyValuePair<string, Object>> param = e.CommandArgument as List<KeyValuePair<string, Object>>;
            int patientID = (int)param.Find(l => l.Key == "PatientID").Value;
            int locationID = (int)param.Find(l => l.Key == "LocationID").Value;
           
            this.SchedulePatient.PatientId = patientID;
            this.SchedulePatient.FacilityId = locationID;
            this.SchedulePatient.UserId = this.UserID;
            this.SchedulePatient.OpenMode = "New";
            this.SchedulePatient.CurrentUser = new KeyValuePair<int, string>(this.UserID, Session["AppUserName"].ToString());
            if (locationID == Convert.ToInt32(base.Session["AppLocationId"]))
            {
                this.SchedulePatient.Rebind();
                this.Button1_Click(this.Button1, EventArgs.Empty);
                this.SchedulePatient.EnableModelDialog(true);
                return;
            }
            else
            {
                string script = "alert('This Patient belongs to a different Location. Please log-in with the patient\\'s location.'); return false;";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FindPatientAlert", script, true);
            }
        }

        /// Handles the AsyncPostBackError event of the PageScriptManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AsyncPostBackErrorEventArgs"/> instance containing the event data.</param>
        private void PageScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            string message = e.Exception.Message;
            Master.PageScriptManager.AsyncPostBackErrorMessage = message;
        }

        #endregion event-handlers

        #region private methods

        /// <summary>
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        //private void NotifyAction(string strMessage, string strTitle, bool errorFlag, string onOkScript = "")
        //{
        //    lblNoticeInfo.Text = strMessage;
        //    lblNotice.Text = strTitle;
        //    lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
        //    lblNoticeInfo.Font.Bold = true;
        //    btnOkAction.OnClientClick = "";
        //    var list = new List<KeyValuePair<string, string>>();
        //    list.Add(new KeyValuePair<string, string>("Message", strMessage));
        //    list.Add(new KeyValuePair<string, string>("Title", strTitle));
        //    list.Add(new KeyValuePair<string, string>("errorFlag", errorFlag.ToString().ToLower()));
        //    if (onOkScript != "")
        //    {
        //        list.Add(new KeyValuePair<string, string>("OkScript", onOkScript));
        //        btnOkAction.OnClientClick = onOkScript;
        //    }
        //    this.notifyPopupExtender.Show();
        //}

     

        /// <summary>
        /// Redirects this instance.
        /// </summary>
        private void Redirect()
        {
            Response.Redirect("~/Scheduler/frmScheduler_AppointmentMain.aspx");
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void showErrorMessage(ref Exception ex)
        {
            this.isError = true;
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
            //notifyPopupExtender.Hide();
            this.SchedulePatient.EnableModelDialog(false);
        }

        #endregion private methods
    }
}