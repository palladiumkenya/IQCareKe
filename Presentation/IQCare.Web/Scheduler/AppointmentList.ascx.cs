using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.Administration;
using Interface.Scheduler;

namespace IQCare.Web.Scheduler
{
    public partial class AppointmentList : System.Web.UI.UserControl
    {
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when an appointment entry is selected.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler AppointmentSelectedChanged;

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

        /// <summary>
        /// Occurs when [notify command].
        /// </summary>
        [System.ComponentModel.Category("Events")]
        [System.ComponentModel.Description("Raised when a notifcation need to be sent.")]
        [System.ComponentModel.Bindable(true)]
        public event CommandEventHandler NotifyCommand;

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
            get
            {
                return int.Parse(this.hLocationId.Value);
            }
            set
            {
                this.hLocationId.Value = value.ToString().ToUpper();
            }
        }

        public int RecordCount
        {
            get
            {
                return this.grdSearchResult.Rows.Count;
            }
        }
        public int? FilterAppointmentReason
        {
            get
            {
                if (this.hAppReason.Value == "")
                {
                    return null;
                }
                return int.Parse(this.hAppReason.Value);
            }
            private set
            {
                this.hAppReason.Value = value.ToString().ToUpper();
            }
        }

        public int? FilterAppointmentStatus
        {
            get
            {
                if (this.hAppStatus.Value == "")
                {
                    return null;
                }
                return int.Parse(this.hAppStatus.Value);
            }
            private set
            {
                this.hAppStatus.Value = value.ToString().ToUpper();
            }
        }

        public DateTime? FilterDateFrom
        {
            get
            {
                if (hFromDate.Value == "")
                {
                    return null;
                }
                return Convert.ToDateTime(hFromDate.Value);
            }
            private set
            {
                hFromDate.Value = value.Value.ToString("dd-MMM-yyyy");
            }
        }

        public DateTime? FilterDateTo
        {
            get
            {
                if (hToDate.Value == "")
                {
                    return null;
                }
                return Convert.ToDateTime(hToDate.Value);
            }
            private set
            {
                hToDate.Value = value.Value.ToString("dd-MMM-yyyy");
            }
        }

        public int? FilterServiceArea
        {
            get
            {
                if (this.hServiceArea.Value == "")
                {
                    return null;
                }
                return int.Parse(this.hServiceArea.Value);
            }
            private set
            {
                this.hServiceArea.Value = value.ToString().ToUpper();
            }
        }

        public int? PatientId
        {
            private get
            {
                if (this.hPatientId.Value == "")
                {
                    return null;
                }
                return int.Parse(this.hPatientId.Value);
            }
            set
            {
                this.hPatientId.Value = value.ToString().ToUpper();
            }
        }

        public bool ShowFilterPane
        {
            get
            {
                
                if (this.hShowFilter.Value == "")
                    this.hShowFilter.Value = "TRUE";
                return bool.Parse(this.hShowFilter.Value);
            }
            set
            {
                hShowFilter.Value = value.ToString().ToUpper();
            }
        }
        protected string showFilter
        {
            get
            {
                if (!this.ShowFilterPane) return "none";                
                return "";
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
                return int.Parse(this.hUserId.Value);
            }
            set
            {
                this.hUserId.Value = value.ToString().ToUpper();
            }
        }
        /// <summary>
        /// Rebinds this instance.
        /// </summary>
        public void Rebind()
        {
            this.PopulateAppointment();
            this.DataBind(true);
        }
        /// <summary>
        /// Clears the index of the selected.
        /// </summary>
        public void ClearSelectedIndex()
        {
            this.grdSearchResult.SelectedIndex = -1;
            DataTable dt = (DataTable)ViewState["GrdData"];
            grdSearchResult.DataSource = dt;
            ddAppointmentStatus.ClearSelection();
            ddlAppointmentReason.ClearSelection(); 
            ddlServiceAreas.ClearSelection(); ;
            grdSearchResult.DataBind();
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
        public DataTable GetAppointmentListForExport()
        {

            DataTable dtAppointment = null;
            if (null != ViewState["GrdData"])
            {
                dtAppointment = (DataTable)ViewState["GrdData"];

                if (dtAppointment.Rows.Count > 0)
                {
                    DataTable theDT = dtAppointment.Copy();
                    theDT.TableName = dtAppointment.TableName;
                    try
                    {
                        theDT.Columns.Remove("AppointmentId");
                        theDT.Columns.Remove("patientid");
                        theDT.Columns.Remove("LocationId");
                        theDT.Columns.Remove("VisitId");
                        theDT.Columns.Remove("PurposeId");
                        theDT.Columns.Remove("AppointmentStatusId");
                        theDT.Columns.Remove("PatientStatus");
                        theDT.Columns.Remove("ServiceAreaId");
                        theDT.Columns.Remove("ProviderId");
                        theDT.Columns.Remove("CreatedById");
                        theDT.Columns.Remove("UpdatedById");
                        theDT.Columns.Remove("UpdatedBy");
                        theDT.Columns.Remove("CreatedBy");
                        theDT.Columns.Remove("FacilityName");
                        return theDT;
                    }

                    catch (Exception ex)
                    {
                        this.OnErrorOccured(this, new CommandEventArgs("Error", ex.Message));
                        return null;
                    }
                }
            }
            return dtAppointment;
        }
        /// <summary>
        /// Handles the RowCommand event of the grdSearchResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs" /> instance containing the event data.</param>
        protected void grdSearchResult_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex;
            rowIndex = Int32.Parse(e.CommandArgument.ToString());
            grdSearchResult.SelectedIndex = rowIndex;
            //   this.AppointmentId = int.Parse(gridAppointment.DataKeys[rowIndex]["AppointmentId"].ToString());
            int _appointmentId = int.Parse(grdSearchResult.DataKeys[rowIndex]["AppointmentId"].ToString());
            int _appointmentVisitId = int.Parse(grdSearchResult.DataKeys[rowIndex]["VisitId"].ToString());
            int _patientId = int.Parse(grdSearchResult.DataKeys[rowIndex]["PatientId"].ToString());
            // this.AppointmentVisitId = int.Parse(gridAppointment.DataKeys[rowIndex]["VisitId"].ToString());
            Appointment _selectedAppointment = new Appointment()
            {
                AppointmentId = _appointmentId,
                VisitId = _appointmentVisitId,
                Deleted = false,
                PatientId = _patientId,
                Location = new KeyValuePair<int, string>(this.FacilityId, Session["AppLocation"].ToString())
            };
            GridViewRow row = (grdSearchResult.Rows[rowIndex]);


            if (e.CommandName == "RowClick")
            {
                string strServiceArea = (row.FindControl("labelServiceArea") as Label).Text.Trim();
                string strServiceAreaId = (row.FindControl("hModuleId") as HiddenField).Value;
                if (strServiceAreaId == "")
                {
                    _selectedAppointment.ServiceArea = new KeyValuePair<int, string>();
                }
                else { _selectedAppointment.ServiceArea = new KeyValuePair<int, string>(int.Parse(strServiceAreaId), strServiceArea); }

                string strStatus = (row.FindControl("labelAppStatus") as Label).Text.Trim();
                string strStatusId = (row.FindControl("hStatuId") as HiddenField).Value;
                if (strStatusId == "")
                {
                    _selectedAppointment.Status = new KeyValuePair<int, string>();
                }
                else
                {
                    _selectedAppointment.Status = new KeyValuePair<int, string>(int.Parse(strStatusId), strStatus);
                }

                string strProvider = (row.FindControl("labelProvider") as Label).Text.Trim();
                string strProviderId = (row.FindControl("hProviderId") as HiddenField).Value;
                if (strProviderId == "")
                {
                    _selectedAppointment.Provider = new KeyValuePair<int, string>();
                }
                else
                {
                    _selectedAppointment.Provider = new KeyValuePair<int, string>(int.Parse(strProviderId), strProvider);
                }
                string strNotes = (row.FindControl("hdNotes") as HiddenField).Value.Trim();
                _selectedAppointment.Notes = strNotes;

                string strPurpose = (row.FindControl("labelPurpose") as Label).Text.Trim();
                string strPurposeId = (row.FindControl("hPurposeId") as HiddenField).Value;
                if (strPurposeId == "")
                {
                    _selectedAppointment.Purpose = new KeyValuePair<int, string>();
                }
                else
                {
                    _selectedAppointment.Purpose = new KeyValuePair<int, string>(int.Parse(strPurposeId), strPurpose);
                }

                string strAppDate = (row.FindControl("labelAppDate") as Label).Text.Trim();
                _selectedAppointment.AppointmentDate = (Convert.ToDateTime(strAppDate));
                string strDateMet = (row.FindControl("labelMetDate") as Label).Text.Trim();
                if (strDateMet != "")
                {
                    _selectedAppointment.DateMet = (Convert.ToDateTime(strDateMet));
                }
                string strCreatedById = (row.FindControl("hCreateUserId") as HiddenField).Value;
                string strCreateUser = (row.FindControl("hCreateUser") as HiddenField).Value;
                string strUpdatedById = (row.FindControl("hUpdateUserId") as HiddenField).Value;
                string strUpdateUser = (row.FindControl("hUpdateUser") as HiddenField).Value;
                string strStatusDate = (row.FindControl("hStatusDate") as HiddenField).Value;
                if (strUpdatedById == "")
                {
                    _selectedAppointment.BookedBy = new KeyValuePair<int, string>(int.Parse(strCreatedById), strCreateUser);
                }
                else
                {
                    _selectedAppointment.BookedBy = new KeyValuePair<int, string>(int.Parse(strUpdatedById), strUpdateUser);
                }
                _selectedAppointment.StatusDate = Convert.ToDateTime(strStatusDate);
                //this.SchedulePatient.OpenMode = "EDIT";
                //this.SchedulePatient.PatientId = _patientId;
                //this.SchedulePatient.SelectedAppointment = _selectedAppointment;
                //this.SchedulePatient.FacilityId = this.LocationId;
                //this.SchedulePatient.Rebind();
                //this.Button1_Click(this.Button1, EventArgs.Empty);
                //this.SchedulePatient.EnableModelDialog(true);
                this.OnAppointmentSelected(this, new CommandEventArgs("EDIT", _selectedAppointment));
            }
            else if (e.CommandName == "REMOVE")
            {
                //*******Delete the patient on the basis of patient id ********//
            }
        }
        protected string ShowNotes(object noteString)
        {
            if (noteString.ToString().Length > 15) return "";
            else return "none";
        }
        /// <summary>
        /// Handles the RowDataBound event of the grdSearchResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdSearchResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string theUrl = string.Empty;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView rowView = (e.Row.DataItem) as DataRowView;
                string _status = rowView["AppointmentStatus"].ToString();
                string _patientId = rowView["PatientId"].ToString();
                string _enrollmentId = rowView["PatientEnrollmentID"].ToString();
                string _visitId = rowView["VisitId"].ToString();
                string _locationId = rowView["LocationId"].ToString();

                if ((_status != "Met") &&
                    (_status != "CareEnded") &&
                    (_status != "Missed") &&
                    (_status != "Previously Missed"))
                {
                    e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                    e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                    for (int i = 0; i <= 7; i++)
                    {
                        e.Row.Cells[i].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdSearchResult, "RowClick$" + e.Row.RowIndex);
                    }
                }
            }
        }

        /// <summary>
        /// Handles the Sorting event of the grdSearchResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs" /> instance containing the event data.</param>
        protected void grdSearchResult_Sorting(object sender, GridViewSortEventArgs e)
        {
            IQCareUtils clsUtil = new IQCareUtils();

            SortAndSetDataInGrid(e.SortExpression);
            if (ViewState["SortDirection"].ToString() == "Asc")
            {
                ViewState["SortDirection"] = "Desc";
            }
            else
            {
                ViewState["SortDirection"] = "Asc";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.ShowFilterPane)
            {
                this.PopulateDropDown();
                txtFrom.Text = txtTo.Text = DateTime.Today.ToString("dd-MMM-yyyy");
            }
        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            grdSearchResult.Columns[0].Visible = grdSearchResult.Columns[1].Visible = !this.PatientId.HasValue;
        }

        private void OnAppointmentSelected(object sender, CommandEventArgs e)
        {
            if (this.AppointmentSelectedChanged != null)
            {
                this.AppointmentSelectedChanged(sender, e);
            }
        }

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
        private void PopulateDropDown()
        {
            IAppointment FormManager;
            DataSet theDtSet;

            //*******Get the patient details on the basis of Patient Enrollment Id and show the details.*******//
            FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");
            theDtSet = FormManager.GetAppointmentStatus();

            BindFunctions appBind = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            appBind.BindCombo(ddAppointmentStatus, theDtSet.Tables[0], "Name", "Id", "Id");
            ListItem _item = ddAppointmentStatus.Items.FindByText("Pending");
            if (_item != null)
            {
                _item.Selected = true;
            }
            else
            {
                ddAppointmentStatus.ClearSelection();
            }

            DataSet theDtSetPurpose = FormManager.GetAppointmentReasons(0);

            DataView TheDV = new DataView(theDtSetPurpose.Tables[0]);
            TheDV.RowFilter = "DeleteFlag=0";
            DataTable TheDT = (DataTable)theUtils.CreateTableFromDataView(TheDV);
            appBind.BindCombo(ddlAppointmentReason, TheDT, "Name", "Id");
            TheDV.Dispose();
            TheDT.Clear();

            appBind = new BindFunctions();
            DataTable dt = (((DataTable)Session["AppModule"]).DefaultView).ToTable(true, "ModuleName", "ModuleId");
            dt.DefaultView.RowFilter = "ModuleName Not In ('PM/SCM')";
            appBind.BindCombo(ddlServiceAreas, dt, "ModuleName", "ModuleId", "ModuleName");

            ddlServiceAreas.ClearSelection();
        }

        private void PopulateAppointment()
        {
            try
            {
                IAppointment objMgr = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");

                int? moduleId = null;
                DateTime? fromDate = null;
                DateTime? toDate = null;
                int? appointmentReason = null;
                int? appointmentStatus = null;
                string strdtRange = "";
                if (ShowFilterPane)
                {
                    if (ddlServiceAreas.SelectedIndex > 0) { this.FilterServiceArea = moduleId = Convert.ToInt32(ddlServiceAreas.SelectedValue); }
                    else
                    {
                        this.FilterServiceArea = null;
                    }
                    if (ddlAppointmentReason.SelectedIndex > 0) { this.FilterAppointmentReason = appointmentReason = Convert.ToInt32(ddlAppointmentReason.SelectedValue); }

                    if ((ddAppointmentStatus.SelectedIndex > 0))
                    {
                        this.FilterAppointmentStatus = appointmentStatus = Convert.ToInt32(ddAppointmentStatus.SelectedValue);
                    }

                    if (txtFrom.Text != "") { this.FilterDateFrom = fromDate = Convert.ToDateTime(txtFrom.Text); } else { this.FilterDateFrom = fromDate = DateTime.Today; };

                    if (txtTo.Text != "") { this.FilterDateTo = toDate = Convert.ToDateTime(txtTo.Text); } else { this.FilterDateTo = toDate = DateTime.Today; }
                    { strdtRange = FilterDateFrom.Value.ToString("ddMMMyyyy"); }
                    { strdtRange += FilterDateTo.Value.ToString("ddMMMyyyy"); }
                }

                DataTable dt = objMgr.GetAppointmentList(this.FacilityId, this.PatientId, moduleId, null, appointmentStatus, fromDate, toDate, appointmentReason);


                dt.TableName = "AppointmentList_" + strdtRange;
                grdSearchResult.DataSource = dt;// ds.Tables[1];
                grdSearchResult.DataBind();

                ViewState["GrdData"] = dt;
                ViewState["SortDirection"] = "Asc";

                //if (grdSearchResult.Rows.Count == 0)
                //{

               
                //    //var list = new List<KeyValuePair<string, string>>();
                //    //list.Add(new KeyValuePair<string, string>("Message", IQCareMsgBox.GetMessage("NoAppointmentRecordExists", this)));
                //    //list.Add(new KeyValuePair<string, string>("Title", "Notification"));
                //    //list.Add(new KeyValuePair<string, string>("errorFlag", "true"));
                //    //list.Add(new KeyValuePair<string, string>("okscript", "javascript:return false;"));
                //    //this.OnNotifyCommand(this, new CommandEventArgs("Notice", list));
                //}
            }
            catch (Exception ex)
            {
                this.OnErrorOccured(this, new CommandEventArgs("Error", ex.Message));
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            try
            {

                //get the data in dataset on the basis of slected status and appointment dates

                this.PopulateAppointment();


            }
            catch (Exception err)
            {
                this.OnErrorOccured(this, new CommandEventArgs("Error", err.Message));
                return;
            }
            finally
            {

            }
        }
        private void SortAndSetDataInGrid(String SortExpression)
        {
            IQCareUtils clsUtil = new IQCareUtils();
            DataView theDV;

            if (SortExpression == "AppointmentDate")
            {
                SortExpression = "AppointmentDate";
            }
            theDV = clsUtil.GridSort((DataTable)ViewState["GrdData"], SortExpression, ViewState["SortDirection"].ToString());
            //DataTable theDV = (DataTable)ViewState["GrdData"];
            grdSearchResult.DataSource = null;
            //grdSearchResult.Columns.Clear();

            grdSearchResult.DataSource = theDV;
            grdSearchResult.DataBind();
        }
    }
}