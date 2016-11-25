using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using IQCare.Web.Laboratory.Admin;

namespace IQCare.Web.Laboratory.Request
{
    public partial class FindLabPatient : System.Web.UI.Page
    {

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["FormName"] != null)
                {
                    this.HFormName.Value = Request.QueryString["FormName"];
                }
            }
        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            this.ctrlFindPatient.CancelFind += new EventHandler(FindPatient_CancelFind);
            this.ctrlFindPatient.NotifyParent += new CommandEventHandler(FindPatient_NotifyParent);
            this.ctrlFindPatient.SelectedPatientChanged += new CommandEventHandler(FindPatient_SelectedPatientChanged);
            this.ctrlFindPatient.PatientEnrollmentChanged += new CommandEventHandler(FindPatient_PatientEnrollmentChanged);
        }

        /// <summary>
        /// Handles the PatientEnrollmentChanged event of the FindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void FindPatient_PatientEnrollmentChanged(object sender, CommandEventArgs e)
        {
            List<KeyValuePair<string, int>> param = e.CommandArgument as List<KeyValuePair<string, int>>;
            this.PatientId = param.Find(l => l.Key == "PatientID").Value;
            this.LocationId = param.Find(l => l.Key == "LocationID").Value;
            this.ModuleId = param.Find(l => l.Key == "ModuleID").Value;
        }

        /// <summary>
        /// Handles the SelectedPatientChanged event of the FindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void FindPatient_SelectedPatientChanged(object sender, CommandEventArgs e)
        {
            List<KeyValuePair<string, Object>> param = e.CommandArgument as List<KeyValuePair<string, Object>>;
            PatientId = (int)param.Find(l => l.Key == "PatientID").Value;
            LocationId = (int)param.Find(l => l.Key == "LocationID").Value;

            if (this.LocationId == Convert.ToInt32(base.Session["AppLocationId"]))
            {
                base.Session[SessionKey.LabClient] = param;
                this.Redirect();
            }
            else
            {
                string script = "alert('This Patient belongs to a different Location. Please log-in with the patient\\'s location.'); return false;";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "FindPatientAlert", script, true);
            }
        }

        /// <summary>
        /// Handles the NotifyParent event of the FindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void FindPatient_NotifyParent(object sender, CommandEventArgs e)
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
        /// Handles the CancelFind event of the FindPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void FindPatient_CancelFind(object sender, EventArgs e)
        {
            if (HFormName.Value == "") return;

            string formName = HFormName.Value;
            base.Session["PTServLines"] = null;

            if (formName == "FindLabPatient")
            {

                Response.Redirect("~/Laboratory/Request/FindLabOrder.aspx");
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
                return int.Parse(HPatientID.Value);
            }
            private set
            {
                base.Session["PatientId"] = value;
                HPatientID.Value = value.ToString();
            }

        }

        /// <summary>
        /// Gets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        protected int ModuleId
        {
            get
            {
                return int.Parse(HModuleID.Value);
            }
            private set
            {

                HModuleID.Value = value.ToString();
            }

        }
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
                return int.Parse(HLocationID.Value);
            }
            private set
            {

                HLocationID.Value = value.ToString();
            }

        }
        /// <summary>
        /// Redirects this instance.
        /// </summary>
        void Redirect()
        {
            if (HFormName.Value == "") return;

            string formName = HFormName.Value;
            string theUrl = "";
            if (formName == "FindLabPatient")
            {
                theUrl = string.Format("{0}?PatientId={1}", "./FindLabOrder.aspx", PatientId);
                Response.Redirect(theUrl);
            }
           
        }
    }
}