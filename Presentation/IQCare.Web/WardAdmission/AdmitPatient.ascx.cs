using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.Administration;
using Interface.Administration;
using Interface.Clinical;
namespace IQCare.Web.WardAdmission
{
    public partial class AdmitPatient : System.Web.UI.UserControl
    {

        #region visibility control
        /// <summary>
        /// The s vid
        /// </summary>
        protected string sVid = "";
        /// <summary>
        /// The show in edit
        /// </summary>
        protected string sEdit = "";
        /// <summary>
        /// The show in new
        /// </summary>
        protected string sNew = "";
        /// <summary>
        /// The inverse new
        /// </summary>
        protected string inversNew = "";
        #endregion

        /// <summary>
        /// The is error
        /// </summary>
        bool IsError = false;

        #region properties
        /// <summary>
        /// Gets or sets the patient names.
        /// </summary>
        /// <value>
        /// The patient names.
        /// </value>
        string PatientNames
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
        /// <summary>
        /// Gets or sets the patient facility identifier.
        /// </summary>
        /// <value>
        /// The patient facility identifier.
        /// </value>
        string PatientFacilityID
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
        string PatientGender
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
        /// Gets or sets the patient age years.
        /// </summary>
        /// <value>
        /// The patient age years.
        /// </value>
        int PatientAgeYears
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
        /// Gets the patient ward category.
        /// </summary>
        /// <value>
        /// The patient ward category.
        /// </value>
        string PatientWardCategory
        {
            get
            {
                if (this.PatientAgeYears < 15)
                {
                    return "peadiatric";
                }
                return this.PatientGender.ToLower();
            }
        }
        #endregion

        #region subscriber properties

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        public int PatientID
        {

            private get
            {
                return int.Parse(this.HPatientID.Value);
            }
            set
            {
                this.HPatientID.Value = value.ToString().ToUpper();
            }
        }

        /// <summary>
        /// Gets or sets the facility identifier.
        /// </summary>
        /// <value>
        /// The facility identifier.
        /// </value>
        public int FacilityID
        {

            private get
            {
                return int.Parse(this.HLocationID.Value);
            }
            set
            {
                this.HLocationID.Value = value.ToString().ToUpper();
            }
        }
        /// <summary>
        /// Gets or sets the facility identifier.
        /// </summary>
        /// <value>
        /// The facility identifier.
        /// </value>    
        public int UserID
        {

            private get
            {
                return int.Parse(this.HUserID.Value);
            }
            set
            {
                this.HUserID.Value = value.ToString().ToUpper();
            }
        }
        /// <summary>
        /// Gets or sets the admission identifier.
        /// </summary>
        /// <value>
        /// The admission identifier.
        /// </value>
        public int AdmissionID
        {

            private get
            {
                return int.Parse(this.HAdmissionID.Value);
            }
            set
            {
                this.HAdmissionID.Value = value.ToString().ToUpper();
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

        #endregion

        #region public subscriber events
        /// <summary>
        /// Occurs when [error occurred].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when an error occurs.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler ErrorOccurred;

        /// <summary>
        /// Occurs when [data load complete].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when an data loading is complete.")]
        [System.ComponentModel.Bindable(true)]
        public event EventHandler DataLoadComplete;

        /// <summary>
        /// Occurs when [notify command].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when a notifcation need to be sent.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler NotifyCommand;
        #endregion

        #region public methods
        /// <summary>
        /// Rebinds this instance.
        /// </summary>
        public void Rebind()
        {
            this.GetPatientDetails();

            if (this.OpenMode == "NEW")
            {
                textAdmissionDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                calendarButtonExtender.SelectedDate = DateTime.Now;
                this.PopulateWards();
                this.PopulateServiceArea();
            }
            else if (this.OpenMode == "EDIT")
            {
                this.PopulateWards();
                this.PopulateServiceArea();
                this.BindAdmissionDetails();
            }
            else if (this.OpenMode == "VIEW")
            {
                this.BindAdmissionDetails();
            }
            this.DataBind(true);
        }
        /// <summary>
        /// Enables the model dialog.
        /// </summary>
        /// <param name="visibility">if set to <c>true</c> [visibility].</param>
        public void EnableModelDialog(bool visibility)
        {
            if (visibility)
            {
                AdmissionDialog.Show();
            }
            else
            {
                AdmissionDialog.Hide();
            }
        }
        #endregion

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
        #endregion
        /// <summary>
        /// Injects the script.
        /// </summary>
        void InjectScript()
        {
            //calendarButtonExtender.E = DateTime.Now;
            // string ClientIDAdmisionNo = textAdmissionNumber.ClientID;
            //  string ClientIDAutoCode = chkAutoCode.ClientID;
            string ClientIDWard = ddlPatientWard.ClientID;
            string ClientIDReferral = ddlReferral.ClientID;

            //            string scriptCheckBox = @"$('#" + chkAutoCode.ClientID + @"').change(function(){
            //                if (this.checked){
            //                    $('#" + ClientIDAdmisionNo + @"').attr('readonly',true);
            //                    $('#" + ClientIDAdmisionNo + @"').css('background-color','#DEDEDE');
            //                    $('#" + ClientIDAdmisionNo + @"').val('');
            //                 }
            //                else{ $('#" + ClientIDAdmisionNo + @"').attr('readonly',false);
            //                    $('#" + ClientIDAdmisionNo + @"').css('background-color','white'); }
            //                });";

            string scriptWard = @"$('#" + ClientIDWard + @"').change(function(e){var val = $(this).val(); if(val=='-1'){ e= (e || window.event); e.stopPropagation();stopImmediatePropagation();} });";

            string scriptReferral = @"$('#" + ClientIDReferral + @"').change(function(){var refr = $(this).val(); if(refr=='Others') {$('#trOtherSource').css('display','');}else{$('#trOtherSource').css('display','none');} });";

            string scriptFutureDates = @" function disable_future_dates(sender,args){
                                        if(sender._selectedDate > new Date()){
                                            alert('You cannot select a day after today'); 
                                            sender._selectedDate=new Date();sender._textbox.set_Value(sender._selectedDate.format(sender._format));    }}";
            //  ScriptManager.RegisterStartupScript(chkAutoCode, chkAutoCode.GetType(), "_checkbox", scriptCheckBox, true);
            ScriptManager.RegisterStartupScript(ddlPatientWard, ddlPatientWard.GetType(), "_wardadmission", scriptWard, true);
            ScriptManager.RegisterStartupScript(ddlReferral, ddlReferral.GetType(), "_referral", scriptReferral, true);
            ScriptManager.RegisterStartupScript(textAdmissionDate, textAdmissionDate.GetType(), "_futuredates", scriptFutureDates, true);
        }

        #region event-handlers
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
            sEdit = this.OpenMode == "EDIT" ? "" : "none";
            sVid = this.OpenMode == "VIEW" ? "none" : "";
            sNew = this.OpenMode == "NEW" ? "" : "none";
            inversNew = this.OpenMode == "NEW" ? "none" : "";

            this.labelAdmissionDate.Visible
                //  = labelAdmissionNumber.Visible
                = labelBedNumber.Visible
                = labelExpectedDOD.Visible
                = labelReferred.Visible
                = labelWard.Visible = this.OpenMode == "VIEW";

            labelAdmittedBy.Visible = labelDischarge.Visible = this.OpenMode != "NEW";
            labelWard.Visible = (this.OpenMode == "VIEW" || this.OpenMode == "EDIT");

        }
        /// <summary>
        /// Handles the Click event of the buttonAdmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonAdmit_Click(object sender, EventArgs e)
        {
            try
            {
                this.EnableModelDialog(true);
                //todo validation;
                if (ddlPatientWard.SelectedIndex == -1) throw new Exception("Please select the ward");
                int wardId = int.Parse(ddlPatientWard.SelectedValue);
                if (wardId == -1) throw new Exception("Please select the ward");

                if (ddlReferral.SelectedValue == "") throw new Exception("Please specify where patient has been referred from");
                DateTime dateAdmitted = DateTime.Now;
                if (!string.IsNullOrEmpty(textAdmissionDate.Text))
                    dateAdmitted = Convert.ToDateTime(textAdmissionDate.Text);
                else
                {
                    throw new Exception("Admission date cannot be blank");
                }
                if (dateAdmitted > DateTime.Now) throw new Exception("Admission date cannot be after today");
                DateTime? expectedDOD = null;
                if (!string.IsNullOrEmpty(textExpectedDOD.Text.Trim())) expectedDOD = Convert.ToDateTime(textExpectedDOD.Text.Trim());

                if (expectedDOD.HasValue && expectedDOD.Value < dateAdmitted)
                {
                    throw new Exception("Expected date of discharge cannot be before than the admission date");
                }

                string admissionNumber = "";
                //if (!chkAutoCode.Checked)
                //    admissionNumber = textAdmissionNumber.Text.Trim();
                string referredFrom = ddlReferral.SelectedItem.Text;
                string bedNumber = textBedNumber.Text.Trim();
                //eo validation
                Entities.Administration.WardAdmission admission = new Entities.Administration.WardAdmission()
                {
                    WardID = wardId,
                    AdmissionDate = dateAdmitted,
                    AdmissionNumber = admissionNumber,
                    ReferredFrom = referredFrom,
                    BedNumber = bedNumber,
                    AdmittedBy = this.UserID,
                    PatientID = this.PatientID,
                    Active = true,
                    ExpectedDischarge = expectedDOD
                };
                if (this.OpenMode == "EDIT")
                    admission.AdmissionID = this.AdmissionID;

                IWardsMaster wardMaster = (IWardsMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BWardMaster, BusinessProcess.Administration");
                admissionNumber = wardMaster.SaveAdmission(admission, this.UserID);

                var list = new List<KeyValuePair<string, string>>();
                list.Add(new KeyValuePair<string, string>("Message", "The operation has completed successfully: Admission number is " + admissionNumber));
                list.Add(new KeyValuePair<string, string>("Title", "Successful Admission"));
                list.Add(new KeyValuePair<string, string>("errorFlag", "false"));
                this.EnableModelDialog(false);
                this.OnNotifyCommand(this, new CommandEventArgs("Notify", list));

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
        /// Selecteds the ward changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void SelectedWardChanged(object sender, EventArgs e)
        {
            if (ddlPatientWard.SelectedIndex == 0) return;

            int wardId = int.Parse(ddlPatientWard.SelectedValue);
            IWardsMaster wardMaster = (IWardsMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BWardMaster, BusinessProcess.Administration");
            Entities.Administration.PatientWard ward = wardMaster.GetWards(this.FacilityID, wardId).DefaultIfEmpty(null).FirstOrDefault();

            if (ward != null)
            {
                int availableBeds = ward.Capacity - ward.Occupancy;
                if (availableBeds > 0)
                {
                    labelAvailablity.Text = string.Format("{0} beds available", availableBeds);
                }
                else
                {
                    labelAvailablity.Text = string.Format("Overbooked by {0} beds", availableBeds * -1);
                }

            }
            this.EnableModelDialog(true);
        }
        #endregion

        #region populate data
        /// <summary>
        /// Populates the wards.
        /// </summary>
        void PopulateWards()
        {
            IWardsMaster wardMaster = (IWardsMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BWardMaster, BusinessProcess.Administration");
            var wards = wardMaster.GetWards(this.FacilityID)
                .Where(wd => wd.PatientCategory.ToLower() == this.PatientWardCategory.ToLower() || wd.PatientCategory.ToLower() == "all" || this.OpenMode == "EDIT")
                .OrderBy(wd => wd.WardName);

            ddlPatientWard.Items.Clear();
            ddlPatientWard.Items.Add(new ListItem("Select..", "-1"));
            foreach (Entities.Administration.PatientWard wd in wards)
            {
                ddlPatientWard.Items.Add(new ListItem(string.Format("{0}  ({1})", wd.WardName, wd.Capacity - wd.Occupancy), wd.WardID.ToString()));
            }

        }
        /// <summary>
        /// Populates the published service area.
        /// </summary>
        void PopulateServiceArea()
        {

            IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet DSModules = ptnMgr.GetModuleNames(this.FacilityID);

            DataTable theDT = new DataTable();
            theDT = DSModules.Tables[0];
            ddlReferral.Items.Clear();

            ddlReferral.Items.Add(new ListItem("Select..", ""));
            foreach (DataRow dr in theDT.Rows)
            {
                ddlReferral.Items.Add(new ListItem(dr["ModuleName"].ToString(), dr["ModuleName"].ToString()));
            }
            // ddlReferral.DataTextField = "ModuleName";
            //ddlReferral.DataValueField = "ModuleName";
            //ddlReferral.DataSource = theDT;
            // ddlReferral.DataBind();

            this.ddlReferral.Items.Add(new ListItem("Others", "Others"));
        }
        /// <summary>
        /// Populates the details.
        /// </summary>
        /// <param name="admissionid">The admissionid.</param>
        void BindAdmissionDetails()
        {
            try
            {
                IWardsMaster wardMaster = (IWardsMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BWardMaster, BusinessProcess.Administration");
                Entities.Administration.WardAdmission admission = wardMaster.GetWardAdmission(this.FacilityID, null, this.AdmissionID, null).DefaultIfEmpty(null).FirstOrDefault();
                if (admission == null) throw new Exception("Could not load details for the selected admission");
                //   textAdmissionNumber.Text = labelAdmissionNumber.Text = admission.AdmissionNumber;
                textAdmissionDate.Text = labelAdmissionDate.Text = admission.AdmissionDate.ToString("dd-MMM-yyyy");
                calendarButtonExtender.SelectedDate = admission.AdmissionDate;
                textBedNumber.Text = labelBedNumber.Text = admission.BedNumber;
                labelWard.Text = admission.WardName;
                labelReferred.Text = admission.ReferredFrom;
                labelAdmittedBy.Text = this.GetUserDetails(admission.AdmittedBy);
                labelDischarge.Text = "Not yet discharged";
                if (admission.Discharged)
                {
                    string dischargedBy = this.GetUserDetails(admission.DischargedBy.Value);
                    labelDischarge.Text = dischargedBy + " on " + admission.DischargeDate.Value.ToString("dd-MMM-yyyy");
                }
                if (admission.ExpectedDischarge.HasValue)
                {
                    textExpectedDOD.Text = labelExpectedDOD.Text = admission.ExpectedDischarge.Value.ToString("dd-MMM-yyyy");
                    CalendarExtender1.SelectedDate = admission.ExpectedDischarge.Value;
                }
                if (this.OpenMode == "EDIT")
                {
                    ListItem refItem = ddlReferral.Items.FindByValue(admission.ReferredFrom);
                    if (refItem != null)
                    {
                        refItem.Selected = true;
                    }
                    else
                    {
                        ddlReferral.Items.FindByText("Others").Selected = true;
                        textReferral.Text = admission.ReferredFrom;
                    }
                    //ddlReferral.SelectedItem.Text = admission.ReferredFrom;  
                    ListItem item = ddlPatientWard.Items.FindByValue(admission.WardID.ToString());
                    if (item != null) item.Selected = true;
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
        void GetPatientDetails()
        {
            try
            {
                IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                DataTable theDT = ptnMgr.GetPatientRecord(this.PatientID);
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
        /// Gets the user details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        string GetUserDetails(int userId)
        {
            try
            {
                Iuser userMaster = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser, BusinessProcess.Administration");
                DataSet ds = userMaster.GetUserRecord(userId);
                return ds.Tables[0].Rows[0]["UserLastName"].ToString() + ", " + ds.Tables[0].Rows[0]["UserFirstName"].ToString();
            }
            catch (Exception ex)
            {
                this.OnErrorOccured(this, new CommandEventArgs("Error", ex));
                return "";
            }
        }

        #endregion
    }
}