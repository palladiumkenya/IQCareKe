using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.Administration;
using IQCare.Web.UILogic;
using Ward = Entities.Administration;

namespace IQCare.Web.WardAdmission
{
    public partial class Home : System.Web.UI.Page
    {
        bool isError = false;

    //    AuthenticationManager Authentication = new AuthenticationManager();

        #region properties
        /// <summary>
        /// Gets a value indicating whether this instance can admit.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can admit; otherwise, <c>false</c>.
        /// </value>
        bool CanAdmit
        {
            get
            {
                return (CurrentSession.Current.HasFeaturePermission("WARD_ADMIT") == true);
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance can update admission.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can update admission; otherwise, <c>false</c>.
        /// </value>
        bool CanUpdateAdmission
        {
            get
            {
                return (CurrentSession.Current.HasFeaturePermission("WARD_ADMISSION_MODIFY") == true);
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance can discharge.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance can discharge; otherwise, <c>false</c>.
        /// </value>
        bool CanDischarge
        {
            get
            {
                return (CurrentSession.Current.HasFeaturePermission("WARD_DISCHARGE") == true);
            }
        }
        /// <summary>
        /// Gets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        int LocationID
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
        int UserID
        {
            get
            {
                return Convert.ToInt32(base.Session["AppUserId"]);
            }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="frmAdmissionHome"/> is debug.
        /// </summary>
        /// <value>
        ///   <c>true</c> if debug; otherwise, <c>false</c>.
        /// </value>
        bool Debug
        {
            get
            {
                bool _debug = true;
                bool.TryParse(ConfigurationManager.AppSettings.Get("DEBUG").ToLower(), out _debug);
                return _debug;
            }
        } 
        #endregion

        #region format
        /// <summary>
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        /// <param name="onOkScript">The on ok script.</param>
        void NotifyAction(string strMessage, string strTitle, bool errorFlag, string onOkScript = "")
        {

            lblNoticeInfo.Text = strMessage;
            lblNotice.Text = strTitle;
            lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
            lblNoticeInfo.Font.Bold = true;
            btnOkAction.OnClientClick = "";
            var list = new List<KeyValuePair<string, string>>();
            list.Add(new KeyValuePair<string, string>("Message", strMessage));
            list.Add(new KeyValuePair<string, string>("Title", strTitle));
            list.Add(new KeyValuePair<string, string>("errorFlag", errorFlag.ToString().ToLower()));
            if (onOkScript != "")
            {
                list.Add(new KeyValuePair<string, string>("OkScript", onOkScript));
                btnOkAction.OnClientClick = onOkScript;
            }
            this.notifyPopupExtender.Show();

        }
        /// <summary>
        /// Injects the script.
        /// </summary>
        void InjectScript()
        {
            //calendarButtonExtender.E = DateTime.Now;
            string ClientIDWard = ddlPatientWard.ClientID;
            string scriptWard = @"$('#" + ClientIDWard + @"').change(function(e){var val = $(this).val(); if(val=='-1' ){e= (e || window.event); e.stopPropagation();stopImmediatePropagation();} });";

            string scriptFutureDates = @" function disable_future_dates(sender,args){
                                        var end = new Date();
                                        end.setHours(23,59,59,999);
                                        if(sender._selectedDate > end ){
                                            alert('You cannot select a day after today'); 
                                            sender._selectedDate=new Date();sender._textbox.set_Value(sender._selectedDate.format(sender._format));    }}";

            string ClientIDAdmission = HSelectedID.ClientID;           
            string ClientIDDate = HDate.ClientID;
            string ClientIDStatus = HStatus.ClientID;
            string scriptDialog = @"function showDiag(argID,argDT,argST){$('#" + ClientIDAdmission + "').val(argID);$('#" + ClientIDDate + @"').val(argDT);
                $('#" + ClientIDStatus + "').val(argST);$find('mpeBID32803').show();return false;}";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "_futuredates", scriptFutureDates, true);
            ScriptManager.RegisterStartupScript(ddlPatientWard, ddlPatientWard.GetType(), "_wardadmission", scriptWard, true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "_DIAGX_X", scriptDialog, true);

        }
        /// <summary>
        /// Shows the discharge.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        protected string ShowDischarge(object status)
        {
            return (status.ToString().ToLower() == "false") ? "" : "none";
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
                //lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team.  ";
                //this.isError = this.divError.Visible = true;
                //Exception lastError = ex;
                //lastError.Data.Add("Domain", "Wards Admission Home");
                //try
                //{
                //    Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                //    logger.LogError(ex);
                //}
                //catch
                //{
                SystemSetting.LogError(ex);
                //}
            }
            notifyPopupExtender.Hide();
            this.ctrlAdmit.EnableModelDialog(false);
        }
       
        #endregion

        #region populate data
        /// <summary>
        /// Popultes the ward admission.
        /// </summary>
        /// <param name="admissions">The admissions.</param>
        void PopulateWardAdmission()
        {
            if (ddlPatientWard.SelectedIndex ==-1) return;
           // if (ddlPatientWard.SelectedIndex == 0) return;
            int wardId = int.Parse(ddlPatientWard.SelectedValue);
            if (wardId == -1) return;
            int? _wardID = null;
            bool excludeDischarged = rblOption.SelectedValue == "No";
            if (wardId != 0) _wardID = wardId;

            IWardsMaster wardMaster = (IWardsMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BWardMaster, BusinessProcess.Administration");
            List<Ward.WardAdmission> _admissions;

            _admissions = wardMaster.GetWardAdmission(this.LocationID, _wardID, null, null, excludeDischarged);

            gridAdmission.DataSource = _admissions;
            gridAdmission.DataBind();
        }
        /// <summary>
        /// Populates the wards.
        /// </summary>
        void PopulateWards(int selectedWard = -1)
        {
            IWardsMaster wardMaster = (IWardsMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BWardMaster, BusinessProcess.Administration");
            var wards = wardMaster.GetWards(this.LocationID)
                .OrderBy(wd => wd.WardName);

            ddlPatientWard.Items.Clear();
            if (selectedWard == -1) 
            { 
            int.TryParse(ddlPatientWard.SelectedValue.ToString(), out selectedWard);
            }
            foreach (Ward.PatientWard wd in wards)
            {
                ddlPatientWard.Items.Add(new ListItem(string.Format("{0}  ({1})", wd.WardName, wd.Capacity - wd.Occupancy), wd.WardID.ToString()));
            }
            int wardCount = ddlPatientWard.Items.Count;

            if (wardCount > 1)
            {
                ddlPatientWard.Items.Add(new ListItem("All wards", "0"));
                ListItem item = new ListItem("Select...", "-1");
                ddlPatientWard.Items.Insert(0, item);
            }
            else if (wardCount == 1)
            {
                ddlPatientWard.SelectedIndex = 0;
            }
            else
            {
                ListItem selectedItem = ddlPatientWard.Items.FindByValue(selectedWard.ToString());
                if (selectedItem != null) selectedItem.Selected = true;
            }

        }
        #endregion

        #region event handlers
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Master.PageScriptManager.AsyncPostBackError += new EventHandler<AsyncPostBackErrorEventArgs>(PageScriptManager_AsyncPostBackError);
            //admission control
            this.ctrlAdmit.DataLoadComplete += new EventHandler(AdmitPatient_DataLoadComplete);
            this.ctrlAdmit.ErrorOccurred += new CommandEventHandler(AdmitPatient_ErrorOccurred);
            this.ctrlAdmit.NotifyCommand += new CommandEventHandler(AdmitPatient_NotifyCommand);
        }
        /// <summary>
        /// Handles the NotifyCommand event of the AdmitPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void AdmitPatient_NotifyCommand(object sender, CommandEventArgs e)
        {
            List<KeyValuePair<string, string>> param = e.CommandArgument as List<KeyValuePair<string, string>>;
            string message = param.Find(p => p.Key == "Message").Value.ToString();
            string title = param.Find(p => p.Key == "Title").Value.ToString();
            bool error = param.Find(p => p.Key == "errorFlag").Value.ToString().Equals("true");
            this.NotifyAction(message, title, error, "");
        }
        /// <summary>
        /// Handles the ErrorOccurred event of the AdmitPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void AdmitPatient_ErrorOccurred(object sender, CommandEventArgs e)
        {
            Exception ex = e.CommandArgument as Exception;
            this.showErrorMessage(ref ex);
        }
        /// <summary>
        /// Handles the DataLoadComplete event of the AdmitPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void AdmitPatient_DataLoadComplete(object sender, EventArgs e)
        {
            divAdmitComponent.Update();
        }
        /// <summary>
        /// Handles the AsyncPostBackError event of the PageScriptManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AsyncPostBackErrorEventArgs"/> instance containing the event data.</param>
        void PageScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            string message = e.Exception.Message;
            Master.PageScriptManager.AsyncPostBackErrorMessage = message;
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

                this.PopulateWards();
               // btnNewAdmission.Enabled = this.CanAdmit;
                textDischargeDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");

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
            btnNewAdmission.OnClientClick = "javascript:window.location='./NewAdmission.aspx'; return false;";
            divError.Visible = this.isError;
        }
        /// <summary>
        /// Handles the Click event of the btnOkAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOkAction_Click(object sender, EventArgs e)
        {
            divAdmitComponent.Update();
            divGridComponent.Update();
            divErrorCompenent.Update();
        }
        /// <summary>
        /// Handles the Click event of the btnFilter control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                this.PopulateWardAdmission();
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Handles the RowCommand event of the gridAdmission control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridAdmission_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Int32.Parse(e.CommandArgument.ToString());
            gridAdmission.SelectedIndex = index;
            int _admissionID = int.Parse(gridAdmission.SelectedDataKey.Values["AdmissionID"].ToString());
            int _patientID = int.Parse(gridAdmission.SelectedDataKey.Values["PatientID"].ToString());
            int _Ward = int.Parse(gridAdmission.SelectedDataKey.Values["WardID"].ToString());

            GridViewRow row = gridAdmission.SelectedRow;
            bool discharged = row.Cells[6].Text.Trim().ToLower() == "yes";
            try
            {
                if (e.CommandName == "Discharge")
                {
                  
                }
                if (e.CommandName == "ViewEdit")
                {
                    if (!this.CanUpdateAdmission || discharged) this.ctrlAdmit.OpenMode = "VIEW";

                    this.ctrlAdmit.PatientID = _patientID;
                    this.ctrlAdmit.FacilityID = this.LocationID;
                    this.ctrlAdmit.UserID = this.UserID;
                    this.ctrlAdmit.OpenMode = discharged ? "VIEW" : "EDIT";
                    this.ctrlAdmit.AdmissionID = _admissionID;
                    this.ctrlAdmit.Rebind();
                    this.ctrlAdmit.EnableModelDialog(true);

                    return;
                }
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
        }
        /// <summary>
        /// Handles the DataBound event of the gridAdmission control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void gridAdmission_DataBound(object sender, EventArgs e)
        {
            //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            //for (int i = 0; i < 6; i++)
            //{
            //    TableHeaderCell cell = new TableHeaderCell();
            //    TextBox txtSearch = new TextBox();
            //    txtSearch.Attributes["placeholder"] = gridAdmission.Columns[i].HeaderText;
            //    txtSearch.CssClass = "search_textbox";
            //    cell.Controls.Add(txtSearch);
            //    row.Controls.Add(cell);
            //}
            //gridAdmission.HeaderRow.Parent.Controls.AddAt(1, row);
        }
        /// <summary>
        /// Handles the RowDataBound event of the gridAdmission control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridAdmission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Entities.Administration.WardAdmission rowView = (Entities.Administration.WardAdmission)e.Row.DataItem;
                string discharged = rowView.Discharged ? "Yes" : "No";
                e.Row.Cells[6].Text = discharged;
                if (!rowView.Discharged)
                {
                    TextBox dichargeText = e.Row.FindControl("textDischargeDate") as TextBox;
                    if (dichargeText != null)
                    {
                        dichargeText.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                    }
                    Button btn = e.Row.FindControl("buttonDischarge") as Button;
                    if (btn != null)
                    {
                       //id, date,status
                        btn.OnClientClick = string.Format("javascript:showDiag('{0}','{1}','{2}');return false;"
                            ,rowView.AdmissionID
                            ,rowView.AdmissionDate.ToString("dd-MMM-yyyy")
                            ,discharged);
                    }
                }
               
            }
        }
        /// <summary>
        /// Selecteds the ward changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void SelectedWardChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Handles the Click event of the buttonDischarge control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonDischarge_Click(object sender, EventArgs e)
        {
            int _admissionID = int.Parse(HSelectedID.Value);
            bool discharged = HStatus.Value.Equals("Yes");
            string dWardID = ddlPatientWard.SelectedValue;

            if (!this.CanDischarge || discharged)
            {
                return;
            }
            try
            {
                string stradmissionDate = (HDate.Value);
                DateTime admissionDate = Convert.ToDateTime(stradmissionDate);
                DateTime dischargeDate = Convert.ToDateTime(textDischargeDate.Text.Trim());
                if (admissionDate > dischargeDate)
                {
                    throw new Exception("Date of discharge cannot be earlier than date of admission");
                }
                if (dischargeDate > DateTime.Now)
                {
                    throw new Exception("Date of discharge cannot be after today");
                }
                IWardsMaster wardMaster = (IWardsMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BWardMaster, BusinessProcess.Administration");
                wardMaster.DischargeAdmission(_admissionID, this.UserID, this.UserID, dischargeDate);
                this.PopulateWardAdmission();
                this.PopulateWards();
                this.NotifyAction("Patient successfully discharged", "Discharge Operation", false);
                return;
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);               
            }
            finally
            {
                HDate.Value = HStatus.Value = HSelectedID.Value = textDischargeDate.Text = "";
                modalDischarge.Hide();
            }
        }
        #endregion

             
    }
}