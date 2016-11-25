using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.Administration;
using Interface.Administration;
using IQCare.Web.UILogic;

namespace IQCare.Web.Scheduler
{
    /// <summary>
    ///
    /// </summary>
    public partial class AppointmentNewHistory : System.Web.UI.Page
    {
        protected string showStatus = "";


        /// <summary>
        /// Gets a value indicating whether this instance can delete.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can delete; otherwise, <c>false</c>.
        /// </value>
        private bool CanDelete
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance can edit.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can edit; otherwise, <c>false</c>.
        /// </value>
        private bool CanEdit
        {
            get
            {
                return true;
            }
        }

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
        /// The patient identifier
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        private int PatientId
        {
            get
            {
                
                 return CurrentSession.Current.CurrentPatient.Id;
            }
        }

        /// <summary>
        /// Gets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        private int UserId
        {
            get
            {
                return CurrentSession.Current.User.Id;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnBack control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void btnBack_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the btnNewAppointment control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnNewAppointment_Click(object sender, EventArgs e)
        {
            this.SchedulePatient.PatientId = this.PatientId;
            this.SchedulePatient.FacilityId = this.LocationId;
            this.SchedulePatient.UserId = this.UserId;
            this.SchedulePatient.OpenMode = "New";
            this.SchedulePatient.CurrentUser = new KeyValuePair<int, string>(this.UserId, CurrentSession.Current.User.FullName);

            this.SchedulePatient.Rebind();
            this.Button1_Click(this.Button1, EventArgs.Empty);
            this.SchedulePatient.EnableModelDialog(true);
            return;
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
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Button1_Click(object sender, EventArgs e)
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Master.PageScriptManager.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(PageScriptManager_AsyncPostBackError);
            //admission control
            this.AppGrid.AppointmentSelectedChanged += new CommandEventHandler(AppGrid_AppointmentSelectedChanged);
            this.AppGrid.NotifyCommand += new CommandEventHandler(AppGrid_NotifyCommand);
            this.AppGrid.ErrorOccurred += new CommandEventHandler(AppGrid_ErrorOccurred);
            this.AppGrid.DataLoadComplete += new EventHandler(AppGrid_DataLoadComplete);
            this.AppGrid.CurrentUser = new KeyValuePair<int, string>(this.UserId, Session["AppUserName"].ToString());
            this.AppGrid.FacilityId = this.LocationId;
            this.AppGrid.ShowFilterPane = false;
            this.AppGrid.PatientId = this.PatientId;

            this.SchedulePatient.DataLoadComplete += new EventHandler(SchedulePatient_DataLoadComplete);
            this.SchedulePatient.ErrorOccurred += new CommandEventHandler(SchedulePatient_ErrorOccurred);
            this.SchedulePatient.NotifyCommand += new CommandEventHandler(SchedulePatient_NotifyCommand);
            this.SchedulePatient.CurrentUser = new KeyValuePair<int, string>(this.UserId, Session["AppUserName"].ToString());
            this.SchedulePatient.ModalDialogCancel += new EventHandler(SchedulePatient_ModalDialogCancel);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Appointment";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Patient Appointment";
            if (Session["PatientStatus"] != null)
            {
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Convert.ToString(Session["PatientStatus"]);
            }
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
            Master.ExecutePatientLevel = true;
            if (!IsPostBack)
            {
                this.getPatientDetails(PatientId);
                this.AppGrid.PatientId = PatientId;
                this.AppGrid.Rebind();
                //string theUrl = "";

                
            }
            this.InjectScript();
        }

        /// <summary>
        /// Handles the PreInit event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
        }

        //    theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), ApplicationAccess.SchedularAppointment,0);
        /// <summary>
        /// Handles the Click event of the theBtn control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        protected void theBtn_Click(object sender, EventArgs e)
        {
            //deleteAppointment();
        }

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
            IQCareMsgBox.NotifyAction(message, title, error, this, script);
        }

        /// <summary>
        /// Fills the patient appointmnt details in grid.
        /// </summary>
        /// <param name="patientId">The patient identifier.</param>
        private void getPatientDetails(int patientId)
        {
          //  IDeletePatient FormManager;
            //*******Get the patient details on the basis of Patient Enrollment Id and show the details.*******//
           // FormManager = (IDeletePatient)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDeletePatient, BusinessProcess.Administration");

           // DataTable theDT = FormManager.GetPatientDetails(patientId);
            //if (theDT.Rows.Count != 0)
            //{
                //*******Check whether the patient is inactive*******//
                //*******Inactive patient show message and disable  delete*******//
                if (CurrentSession.Current.CurrentPatient.Status.ToString() == "1")
                {
                    string script = "<script language = 'javascript' defer ='defer' id = 'confirm1'>\n";
                    script += "var ans=true;\n";
                    script += "alert('Patient is Inactive');\n";
                    script += "</script>\n";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "confirm1", script);
                }
                else
                {
                    //btndelete.Enabled = true;
                }
            //}
        }

        private void InjectScript()
        {
//            string scriptPastDates = @" function disable_past_dates(sender,args){
//                                                    var senderDate = new Date(sender._selectedDate);senderDate.setHours(0,0,0,0)
//                                                    var nowDate =new Date();  nowDate.setHours(0,0,0,0);
//                                                    if(senderDate < nowDate){
//                                                        alert('You cannot select a day before today');
//                                                        sender._selectedDate=new Date();sender._textbox.set_Value(sender._selectedDate.format(sender._format));    }}";

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "_pastdates", scriptPastDates, true);

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

        private void SchedulePatient_ModalDialogCancel(object sender, EventArgs e)
        {
            this.AppGrid.ClearSelectedIndex();
            this.Button1_Click(this.Button1, EventArgs.Empty);
            AppGrid.DataBind();
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

            IQCareMsgBox.NotifyAction(message, title, error,this, "");
        }

        private void showErrorMessage(ref Exception ex)
        {
           // this.isError = true;
            if (this.Debug)
            {
                lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            }
            else
            {
                SystemSetting.LogError(ex);
            }
            //notifyPopupExtender.Hide();
            this.SchedulePatient.EnableModelDialog(false);
        }
    }
}