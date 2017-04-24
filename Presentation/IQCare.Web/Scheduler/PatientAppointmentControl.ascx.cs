using Application.Presentation;
using Entities.Administration;
using Interface.Clinical;
using Interface.Scheduler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace IQCare.Web.Scheduler
{
    public partial class PatientAppointmentControl : System.Web.UI.UserControl
    {
        #region visibility control

        /// <summary>
        /// The inverse new
        /// </summary>
        protected string inversNew = "";

        /// <summary>
        /// The show in edit
        /// </summary>
        protected string sEdit = "";

        /// <summary>
        /// The show in new
        /// </summary>
        protected string sNew = "";

        /// <summary>
        /// The s vid
        /// </summary>
        protected string sVid = "";

        #endregion visibility control

        /// <summary>
        /// The is error
        /// </summary>
        private bool IsError = false;

        #region properties

        /// <summary>
        /// Gets or sets the patient age years.
        /// </summary>
        /// <value>
        /// The patient age years.
        /// </value>
        private int PatientAgeYears
        {
            get
            {
                int age = 0;
                int.TryParse(this.HPatientAge.Value, out age);
                return age;
            }
            set
            {
                this.HPatientAge.Value = value.ToString().ToUpper();
                this.lblAge.Text = value.ToString() + " years";
            }
        }

        /// <summary>
        /// Gets or sets the patient facility identifier.
        /// </summary>
        /// <value>
        /// The patient facility identifier.
        /// </value>
        private string PatientFacilityID
        {
            get
            {
                return this.lblFacilityID.Text;
            }
            set
            {
                this.lblFacilityID.Text = value.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Gets or sets the ward category.
        /// </summary>
        /// <value>
        /// The ward category.
        /// </value>
        private string PatientGender
        {
            get
            {
                return this.HPatientGender.Value;
            }
            set
            {
                this.HPatientGender.Value = value.ToString().ToUpper();
                this.lblSex.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets the patient names.
        /// </summary>
        /// <value>
        /// The patient names.
        /// </value>
        private string PatientNames
        {
            get
            {
                return this.lblname.Text;
            }
            set
            {
                this.lblname.Text = value.ToString().ToUpper();
            }
        }

        #endregion properties

        #region subscriber properties

        /// <summary>
        /// Gets or sets the admission identifier.
        /// </summary>
        /// <value>
        /// The admission identifier.
        /// </value>
        public int AppointmentId
        {
            private get
            {
                return int.Parse(this.HAppointmentId.Value);
            }
            set
            {
                this.HAppointmentId.Value = value.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Gets or sets the current user.
        /// </summary>
        /// <value>
        /// The current user.
        /// </value>
        public KeyValuePair<int, string> CurrentUser
        {
            get
            {
                return (KeyValuePair<int, string>)ViewState["CurrentUser"];
            }
            set
            {
                ViewState["CurrentUser"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the facility identifier.
        /// </summary>
        /// <value>
        /// The facility identifier.
        /// </value>
        public int FacilityId
        {
            private get
            {
                return int.Parse(this.HLocationId.Value);
            }
            set
            {
                this.HLocationId.Value = value.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Gets or sets the open mode.
        /// </summary>
        /// <value>
        /// The open mode.
        /// </value>
        public string OpenMode
        {
            private get
            {
                return HMode.Value;
            }
            set
            {
                this.HMode.Value = value.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public int PatientId
        {
            private get
            {
                return int.Parse(this.HPatientId.Value);
            }
            set
            {
                this.HPatientId.Value = value.ToString().ToUpper();
            }
        }

        public Appointment SelectedAppointment
        {
            get
            {
                Appointment _appointment = null;
                if (null == ViewState["SelectedAppointment"])
                { }
                else
                {
                    _appointment = (Appointment)ViewState["SelectedAppointment"];
                }
                return _appointment;
            }
            set
            {
                ViewState["SelectedAppointment"] = (Appointment)value;
            }
        }

        /// <summary>
        /// Gets or sets the facility identifier.
        /// </summary>
        /// <value>
        /// The facility identifier.
        /// </value>
        public int UserId
        {
            private get
            {
                return int.Parse(this.HUserId.Value);
            }
            set
            {
                this.HUserId.Value = value.ToString().ToUpper();
            }
        }

        #endregion subscriber properties

        #region public subscriber events

        /// <summary>
        /// Occurs when [data load complete].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when an data loading is complete.")]
        [System.ComponentModel.Bindable(true)]
        public event EventHandler DataLoadComplete;

        /// <summary>
        /// Occurs when [error occurred].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when an error occurs.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler ErrorOccurred;

        public event EventHandler ModalDialogCancel;

        /// <summary>
        /// Occurs when [notify command].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when a notifcation need to be sent.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler NotifyCommand;

        #endregion public subscriber events

        #region public methods

        /// <summary>
        /// Gets the s delete.
        /// </summary>
        /// <value>
        /// The s delete.
        /// </value>
        protected string sDelete
        {
            get
            {
                return OpenMode == "EDIT" ? "" : "none";
            }
        }

        /// <summary>
        /// Enables the model dialog.
        /// </summary>
        /// <param name="visibility">if set to <c>true</c> [visibility].</param>
        public void EnableModelDialog(bool visibility)
        {
            if (visibility && OpenMode != "")
            {
                AppointmentDialog.Show();
            }
            else
            {
                HMode.Value = "";
                AppointmentDialog.Hide();
            }
        }

        /// <summary>
        /// Rebinds this instance.
        /// </summary>
        public void Rebind()
        {
            this.GetPatientDetails();

            if (this.OpenMode == "NEW")
            {
                textAppointmentDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                calendarButtonExtender.SelectedDate = DateTime.Now;
                //dpAppointment.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                this.PopulateProviders();
                this.PopulateServiceArea();
                this.PopulatePurpose();
                txtAppNotes.Text = "";
                this.buttonSave.Text = "Save Appointment";
                this.BindAppointmentDetails();
            }
            else if (this.OpenMode == "EDIT")
            {
                ListItem selectedProvider = null;
                ListItem selectedServiceArea = null;
                ListItem selectedPurpose = null;
                if (null != SelectedAppointment)
                {
                    try
                    {
                        if (!SelectedAppointment.Provider.Equals(default(KeyValuePair<int, string>)))
                        {
                            selectedProvider = new ListItem(SelectedAppointment.Provider.Value, SelectedAppointment.Provider.Key.ToString());
                        }
                        if (!SelectedAppointment.ServiceArea.Equals(default(KeyValuePair<int, string>)))
                        {
                            selectedServiceArea = new ListItem(SelectedAppointment.ServiceArea.Value, SelectedAppointment.ServiceArea.Key.ToString());
                        }
                        if (!SelectedAppointment.Purpose.Equals(default(KeyValuePair<int, string>)))
                        {
                            selectedPurpose = new ListItem(SelectedAppointment.Purpose.Value, SelectedAppointment.Purpose.Key.ToString());
                        }
                    }
                    catch { }
                }
                this.PopulateProviders(selectedProvider);
                this.PopulateServiceArea(selectedServiceArea);
                this.PopulatePurpose(selectedPurpose);
                this.BindAppointmentDetails();
                this.buttonSave.Text = "Update Appointment";
            }
            else
            {
                this.EnableModelDialog(false);
                this.OnErrorOccured(this, new CommandEventArgs("Error", "Unknown mode of rendering"));
            }
            this.DataBind(true);
        }

        #endregion public methods

        #region subscriber events

        /// <summary>
        /// Called when [data load complete].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnDataLoadComplete(object sender, EventArgs e)
        {
            if (this.DataLoadComplete != null)
            {
                this.DataLoadComplete(sender, e);
            }
        }

        /// <summary>
        /// Called when [error occured].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnErrorOccured(object sender, CommandEventArgs e)
        {
            if (this.ErrorOccurred != null)
            {
                this.ErrorOccurred(sender, e);
            }
            else
            {
                Exception ex = e.CommandArgument as Exception;
                this.errorLabel.Text = ex.Message;
                this.IsError = true;
            }
        }

        /// <summary>
        /// Called when [modal dialog cancel].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnModalDialogCancel(object sender, EventArgs e)
        {
            if (this.ModalDialogCancel != null)
            {
                this.ModalDialogCancel(sender, e);
            }
        }

        /// <summary>
        /// Called when [notify command].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void OnNotifyCommand(object sender, CommandEventArgs e)
        {
            if (this.NotifyCommand != null)
            {
                this.NotifyCommand(sender, e);
            }
        }

        #endregion subscriber events

        protected void buttonCancel_Click(object sender, EventArgs e)
        {
            this.EnableModelDialog(false);
            this.OnModalDialogCancel(this, new EventArgs());
        }

        private void InjectScript()
        {
            string scriptPastDates = @" function disable_past_dates(sender,args){
                                                    var senderDate = new Date(sender._selectedDate);senderDate.setHours(0,0,0,0)
                                                    var nowDate =new Date();  nowDate.setHours(0,0,0,0);
                                                    if(senderDate < nowDate){
                                                        alert('You cannot select a day before today');
                                                        sender._selectedDate=new Date();sender._textbox.set_Value(sender._selectedDate.format(sender._format));    }}";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "_pastdates", scriptPastDates, true);
        }

        #region event-handlers

        /// <summary>
        /// Handles the Click event of the buttonDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                IAppointment objMgr = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
                int theResultRow = objMgr.DeletePatientAppointmentDetails(this.AppointmentId, this.CurrentUser.Key);
                var list = new List<KeyValuePair<string, string>>();
                if (theResultRow == 0)
                {
                    list.Add(new KeyValuePair<string, string>("Message", "The operation has completed successfully "));
                    list.Add(new KeyValuePair<string, string>("Title", "Deleting Appointment"));
                    list.Add(new KeyValuePair<string, string>("errorFlag", "true"));
                    list.Add(new KeyValuePair<string, string>("okscript", "HideModalPopup();return false;"));
                    //return;
                }
                else
                {
                    list.Add(new KeyValuePair<string, string>("Message", "The operation has completed successfully "));
                    list.Add(new KeyValuePair<string, string>("Title", "Deleting Appointment"));
                    list.Add(new KeyValuePair<string, string>("errorFlag", "false"));
                    list.Add(new KeyValuePair<string, string>("okscript", "HideModalPopup();return false;"));
                }

                this.EnableModelDialog(false);
                this.OnNotifyCommand(this, new CommandEventArgs("Success", list));
            }
            catch (Exception ex)
            {
                this.EnableModelDialog(true);
                this.errorLabel.Text = ex.Message;
                this.IsError = true;
                this.DataBind();
            }
        }

        /// <summary>
        /// Handles the Click event of the buttonAdmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.EnableModelDialog(true);
                //todo validation;
                if (ddlServiceArea.SelectedIndex == -1) throw new Exception("Please select the Service Area");

                int selectedModuleId = int.Parse(ddlServiceArea.SelectedValue);
                if (selectedModuleId == -1) throw new Exception("Please select the service area");

              //  KeyValuePair<int, string>? nullKvp = null;
                if (ddAppPurpose.SelectedValue == "") throw new Exception("Please specify the purpose for the appointment");
                //if (ddAppProvider.SelectedValue == "") throw new Exception("Please specify where reason for the appointment provider");
                DateTime appointmentDate = DateTime.Now;
                if (!string.IsNullOrEmpty(textAppointmentDate.Text))
                    appointmentDate = Convert.ToDateTime(textAppointmentDate.Text);               
                else
                {
                    throw new Exception("Appointment date cannot be blank");
                }
                if (appointmentDate < DateTime.Now) throw new Exception("Appointment date cannot be after today");

               
                IAppointment objMgr = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
                if (this.OpenMode == "NEW")
                {
                    Appointment appointment = new Appointment()
                    {
                        PatientId = this.PatientId,
                        AppointmentDate = appointmentDate,
                        Location = new KeyValuePair<int, string>(this.FacilityId, ""),
                        Notes = txtAppNotes.Text.Trim(),
                        Purpose = new KeyValuePair<int, string>(int.Parse(ddAppPurpose.SelectedValue), ddAppPurpose.SelectedItem.Text),
                        ServiceArea = new KeyValuePair<int, string>(int.Parse(ddlServiceArea.SelectedValue), ddlServiceArea.SelectedItem.Text),
                        Provider = (ddAppProvider.SelectedValue == "") ? default(KeyValuePair<int, string>) : new KeyValuePair<int, string>(int.Parse(ddAppProvider.SelectedValue), ddAppProvider.SelectedItem.Text),
                        BookedBy = this.CurrentUser,
                        StatusDate = DateTime.Now
                    };
                    objMgr.SaveAppointment(appointment);
                }
                else if (this.OpenMode == "EDIT")
                {
                    Appointment appointment = new Appointment()
                    {
                        PatientId = this.PatientId,
                        AppointmentDate = appointmentDate,
                        Notes = txtAppNotes.Text.Trim(),
                        Purpose = new KeyValuePair<int, string>(int.Parse(ddAppPurpose.SelectedValue), ddAppPurpose.SelectedItem.Text),
                        ServiceArea = new KeyValuePair<int, string>(int.Parse(ddlServiceArea.SelectedValue), ddlServiceArea.SelectedItem.Text),
                        Provider = (ddAppProvider.SelectedValue == "") ? default(KeyValuePair<int, string>) : new KeyValuePair<int, string>(int.Parse(ddAppProvider.SelectedValue), ddAppProvider.SelectedItem.Text),
                        BookedBy = this.CurrentUser,
                        StatusDate = DateTime.Now,
                        AppointmentId = this.AppointmentId,
                        VisitId = SelectedAppointment.VisitId.Value
                    };
                    objMgr.UpdatePatientppointment(appointment);
                }
                var list = new List<KeyValuePair<string, string>>();
                list.Add(new KeyValuePair<string, string>("Message", "The operation has completed successfully "));
                list.Add(new KeyValuePair<string, string>("Title", "Successful appointment"));
                list.Add(new KeyValuePair<string, string>("errorFlag", "false"));
                this.EnableModelDialog(false);
                this.OnNotifyCommand(this, new CommandEventArgs("Success", list));
            }
            catch (Exception ex)
            {
                this.EnableModelDialog(true);
                this.errorLabel.Text = ex.Message;
                this.IsError = true;
                this.DataBind();
                //this.OnErrorOccured(this, new CommandEventArgs("Error", ex));
            }
        }

        /// <summary>
        /// Binds a data source to the invoked server control and all its child controls with an option to raise the <see cref="E:System.Web.UI.Control.DataBinding" /> event.
        /// </summary>
        /// <param name="raiseOnDataBinding">true if the <see cref="E:System.Web.UI.Control.DataBinding" /> event is raised; otherwise, false.</param>
        protected override void DataBind(bool raiseOnDataBinding)
        {
            base.DataBind(raiseOnDataBinding);
            this.OnDataLoadComplete(this, EventArgs.Empty);
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
            panelError.Visible = this.IsError;
            //calendarButtonExtender.OnClientDateSelectionChanged = "disable_past_dates";
            //sEdit = this.OpenMode == "EDIT" ? "" : "none";
            //sVid = this.OpenMode == "VIEW" ? "none" : "";
            //sNew = this.OpenMode == "NEW" ? "" : "none";
            //inversNew = this.OpenMode == "NEW" ? "none" : "";

        }

        /// <summary>
        /// Selecteds the ward changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void SelectedProviderChanged(object sender, EventArgs e)
        {
            if (ddAppProvider.SelectedIndex == 0) return;

            int providerId = int.Parse(ddAppProvider.SelectedValue);

            this.EnableModelDialog(true);
        }

        private bool AppointmentExist(DateTime appDate, int ReasonId, int ModuleId, int VisitId)
        {
            DataTable theDt;
            IAppointment FormManager;
            FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");

            theDt = (DataTable)FormManager.CheckAppointmentExistance(this.PatientId, this.FacilityId, appDate, ReasonId, VisitId, ModuleId);

            if (Convert.ToInt32(theDt.Rows[0][0]) > 0)
            {
                return true;
            }

            return false;
        }

        #endregion event-handlers

        #region populate data

        /// Populates the details.
        /// </summary>
        /// <param name="admissionid">The admissionid.</param>
        private void BindAppointmentDetails()
        {
            try
            {
                if (OpenMode == "NEW")
                {
                    labelAppStatus.Text = "New Appointment";
                    this.AppointmentId = -1;
                    this.txtAppNotes.Text = "";
                    labelBokeddBy.Text = CurrentUser.Value;
                }
                else
                {
                    labelAppStatus.Text = this.SelectedAppointment.Status.Value;
                    this.AppointmentId = this.SelectedAppointment.AppointmentId.Value;
                    textAppointmentDate.Text = this.SelectedAppointment.AppointmentDate.ToString("dd-MMM-yyyy");
                    calendarButtonExtender.SelectedDate = this.SelectedAppointment.AppointmentDate;
                    txtAppNotes.Text = this.SelectedAppointment.Notes;
                    labelBokeddBy.Text = string.Format("{0} on {1:dd-MMM-yyyy hh:mm:ss}", this.SelectedAppointment.BookedBy.Value, this.SelectedAppointment.StatusDate);
                }
            }
            catch (Exception ex)
            {
                this.OnErrorOccured(this, new CommandEventArgs("Error", ex));
                this.EnableModelDialog(false);
            }
            //todo bind control
        }

        /// <summary>
        /// Gets the patient details.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        private void GetPatientDetails()
        {
            try
            {
                IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                DataTable theDT = ptnMgr.GetPatientRecord(this.PatientId);
                //Session["ClientInfo"] = theDS;
                if (theDT.Rows.Count > 0)
                {
                    string patientName = String.Format("{0} {1} {2}", theDT.Rows[0]["Firstname"], theDT.Rows[0]["Middlename"], theDT.Rows[0]["Lastname"]);
                    this.PatientNames = patientName;

                    this.PatientGender = (theDT.Rows[0]["sex"].ToString() == "16") ? "Male" : "Female";
                    string patientFacilityID = theDT.Rows[0]["PatientFacilityID"].ToString();
                    int age = Convert.ToInt32(theDT.Rows[0]["age"]);
                    this.PatientAgeYears = age;
                    this.PatientFacilityID = patientFacilityID;
                }
                ptnMgr = null;
            }
            catch (Exception ex)
            {
                this.OnErrorOccured(this, new CommandEventArgs("Error", ex));
            }
        }

        /// <summary>
        /// Populates the providers.
        /// </summary>
        /// <param name="selectedItem">The selected item.</param>
        private void PopulateProviders(ListItem selectedItem = null)
        {
            if (ddAppProvider.Items.Count == 0)
            {
                IAppointment objMgr = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
                DataSet dsEmployee = objMgr.GetEmployees(0);
                ddAppProvider.Items.Clear();
                ddAppProvider.Items.Add(new ListItem("Select..", ""));
                foreach (DataRow dr in dsEmployee.Tables[0].Rows)
                {
                    ddAppProvider.Items.Add(new ListItem(dr["EmployeeName"].ToString(), dr["EmployeeId"].ToString()));
                }
            }
            ddAppProvider.ClearSelection();
            if (selectedItem != null)
            {
                ListItem _item = ddAppProvider.Items.FindByValue(selectedItem.Value);
                if (_item == null)
                {
                    ddAppProvider.Items.Add(selectedItem);
                    ddAppProvider.SelectedIndex = (ddAppProvider.Items.Count - 1);
                }
                else
                {
                    _item.Selected = true;
                }
            }
            else if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0 && OpenMode == "NEW")
            {
                ListItem item = ddAppProvider.Items.FindByValue(Session["AppUserEmployeeId"].ToString());
                if (item != null)
                {
                    item.Selected = true;
                }
            }
        }

        /// <summary>
        /// Populates the providers.
        /// </summary>
        /// <param name="selectedItem">The selected item.</param>
        private void PopulatePurpose(ListItem selectedItem = null)
        {
            if (ddAppPurpose.Items.Count == 0)
            {
                IAppointment objMgr = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
                DataSet ds = objMgr.GetAppointmentReasons(0);
                DataTable dt = (ds.Tables[0]);
                dt.DefaultView.RowFilter = "DeleteFlag=0";
                ddAppPurpose.Items.Clear();
                ddAppPurpose.Items.Add(new ListItem("Select..", ""));
                foreach (DataRow dr in dt.Rows)
                {
                    ddAppPurpose.Items.Add(new ListItem(dr["Name"].ToString(), dr["Id"].ToString()));
                }
            }
            ddAppPurpose.ClearSelection();
            if (selectedItem != null)
            {
                ListItem _item = ddAppPurpose.Items.FindByValue(selectedItem.Value);
                if (_item == null)
                {
                    ddAppPurpose.Items.Add(selectedItem);
                    ddAppPurpose.SelectedIndex = (ddAppPurpose.Items.Count - 1);
                }
                else
                {
                    _item.Selected = true;
                }
            }
        }

        /// <summary>
        /// Populates the service area.
        /// </summary>
        /// <param name="selectedItem">The selected item.</param>
        private void PopulateServiceArea(ListItem selectedItem = null)
        {
            if (ddlServiceArea.Items.Count == 0)
            {
                BindFunctions appBind = new BindFunctions();
                DataTable dt = (((DataTable)Session["AppModule"]).DefaultView).ToTable(true, "ModuleName", "ModuleId");
                dt.DefaultView.Sort = "ModuleName Asc";
                ddlServiceArea.Items.Clear();
                ddlServiceArea.Items.Add(new ListItem("Select..", ""));
                foreach (DataRow dr in dt.Rows)
                {
                    ddlServiceArea.Items.Add(new ListItem(dr["ModuleName"].ToString(), dr["ModuleId"].ToString()));
                }
            }
            ddlServiceArea.ClearSelection();
            if (selectedItem != null)
            {
                ListItem _item = ddlServiceArea.Items.FindByValue(selectedItem.Value);
                if (_item == null)
                {
                    ddlServiceArea.Items.Add(selectedItem);
                    ddlServiceArea.SelectedIndex = (ddlServiceArea.Items.Count - 1);
                }
                else
                {
                    _item.Selected = true;
                }
            }
            else if (Session["TechnicalAreaId"] != null && OpenMode == "NEW")
            {
                string moduleId = (Session["TechnicalAreaId"].ToString());
                string moduleName = Session["TechnicalAreaName"].ToString();
                ListItem item = ddlServiceArea.Items.FindByValue(moduleId);
                if (item != null)
                {
                    item.Selected = true;
                }
            }
        }/// <summary>

        #endregion populate data
    }
}