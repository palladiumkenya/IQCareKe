using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;

namespace IQCare.Web.Admin
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeletePatient : System.Web.UI.Page
    {
        /// <summary>
        /// The authentication MGR
        /// </summary>
        private AuthenticationManager authMgr = new AuthenticationManager();

        /// <summary>
        /// Gets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        protected int LocationId
        {
            get
            {
                return int.Parse(HLocationId.Value);
            }
            private set
            {
                HLocationId.Value = value.ToString();
            }
        }

        /// <summary>
        /// Gets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        protected int PatientId
        {
            get
            {
                return int.Parse(HPatientId.Value);
            }
            private set
            {
                base.Session["PatientId"] = value;
                HPatientId.Value = value.ToString();
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
                return Convert.ToInt32(base.Session["AppUserId"]);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnOkAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOkAction_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the buttonProxy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonProxy_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Deletes the patient.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void DeleteSelectedPatient(object sender, EventArgs e)
        {
            IDeletePatient FormManager;
            FormManager = (IDeletePatient)ObjectFactory.CreateInstance("BusinessProcess.Administration.BDeletePatient, BusinessProcess.Administration");
            int theResultRow = FormManager.DeletePatient(this.PatientId, this.UserId);

            if (theResultRow == 0)
            {
                string errorMessage = IQCareMsgBox.GetMessage("DeletePatientError", this);
                this.NotifyAction(errorMessage, "Patient Delete Error", true, string.Format("javascript:return false;", "../frmFacilityHome.aspx"));
            }
            else
            {
                string strResponse = labelDeleteText.Text.Replace("Delete all records for ","") + " has been deleted successfully";
                this.NotifyAction(strResponse, "Patient Delete", false, string.Format("javascript:window.location='{0}'; return false;", "../frmFacilityHome.aspx"));
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            ctrlFindPatient.CancelFind += new EventHandler(ctrlFindPatient_CancelFind);
            ctrlFindPatient.NotifyParent += new CommandEventHandler(ctrlFindPatient_NotifyParent);
            ctrlFindPatient.SelectedPatientChanged += new CommandEventHandler(ctrlFindPatient_SelectedPatientChanged);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Administration >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Delete Patient";
            Master.ExecutePatientLevel = false;
            if (authMgr.HasFeatureRight(ApplicationAccess.DeletePatient, (DataTable)Session["UserRight"]) == false)
            {
                this.Redirect();
            }

            Page.EnableViewState = true;
        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("{0}?FormName={1}&mnuClicked={2}", "../frmFacilityHome.aspx", "DeletePatient", "DeletePatient");
            btnBack.OnClientClick = string.Format("javascript:window.location='{0}'; return false;", theUrl);
        }

        /// <summary>
        /// Handles the CancelFind event of the ctrlFindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ctrlFindPatient_CancelFind(object sender, EventArgs e)
        {
            this.Redirect();
        }

        /// <summary>
        /// Handles the NotifyParent event of the ctrlFindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void ctrlFindPatient_NotifyParent(object sender, CommandEventArgs e)
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
        /// Handles the SelectedPatientChanged event of the ctrlFindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        private void ctrlFindPatient_SelectedPatientChanged(object sender, CommandEventArgs e)
        {
            List<KeyValuePair<string, Object>> param = e.CommandArgument as List<KeyValuePair<string, Object>>;
            this.PatientId = (int)param.Find(l => l.Key == "PatientID").Value;
            LocationId = (int)param.Find(l => l.Key == "LocationID").Value;

            string strFirstName = param.Find(l => l.Key == "FirstName").Value.ToString();

            string strMidName = param.Find(l => l.Key == "MiddleName").Value.ToString();
            string strLastName = param.Find(l => l.Key == "LastName").Value.ToString();
            DateTime dateOfBirth = Convert.ToDateTime(param.Find(l => l.Key == "DOB").Value);
            string strGender = param.Find(l => l.Key == "Gender").Value.ToString();
            DateTime dateOfReg = Convert.ToDateTime(param.Find(l => l.Key == "RegistrationDate").Value);
            labelDeleteText.Text = string.Format("Delete all records for {0} {1} {2}?", strFirstName, strMidName, strLastName);
            if (this.LocationId == Convert.ToInt32(base.Session["AppLocationId"]))
            {
                this.buttonProxy_Click(sender, new EventArgs());
                divActionComponent.Update();
                mpePatientDelete.Show();
            }
            else
            {
                string script = "alert('This Patient belongs to a different Location. Please log-in with the patient\\'s location.'); return false;";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FindPatientAlert", script, true);
            }
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
        /// Redirects this instance.
        /// </summary>
        private void Redirect()
        {
            string theUrl;
            theUrl = string.Format("{0}?mnuClicked={1}", "../frmFacilityHome.aspx", "DeletePatient");
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect(theUrl, true);
        }
    }
}