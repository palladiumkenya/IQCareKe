using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using IQCare.Web.UILogic;

namespace IQCare.Web.Clinical
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class FindPatient : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            if (CurrentSession.Current == null)
            {
                string theUrl = string.Format("{0}", "~/frmLogin.aspx");
                //Response.Redirect(theUrl);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Redirect(theUrl, true);
            }
            CurrentSession session = CurrentSession.Current;
            HFormName.Value = Request.QueryString["FormName"].ToString();
            HPatientID.Value = session.CurrentPatient.Id.ToString();
            HLocationID.Value = session.CurrentPatient.LocationId.ToString();
            HModuleID.Value = session.CurrentServiceArea.Id.ToString();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event to initialize the page.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            PatientFinder1.SelectedPatientChanged += new CommandEventHandler(PatientFinder1_SelectedPatientChanged);
            PatientFinder1.CancelFind += new EventHandler(PatientFinder1_CancelFind);
            PatientFinder1.NotifyParent += new CommandEventHandler(PatientFinder1_NotifyParent);
        }

        /// <summary>
        /// Handles the NotifyParent event of the PatientFinder1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        void PatientFinder1_NotifyParent(object sender, CommandEventArgs e)
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
        /// Handles the CancelFind event of the PatientFinder1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void PatientFinder1_CancelFind(object sender, EventArgs e)
        {
            if (HFormName.Value == "") return;

            string formName = HFormName.Value;

            if (formName == "FamilyInfo")
            {
                Response.Redirect("~/ClinicalForms/frmFamilyInformation.aspx?name=Add");
            }
            else
            {
                Response.Redirect("~/ClinicalForms/frmPatient_Home.aspx");
            }
        }

        /// <summary>
        /// Handles the SelectedPatientChanged event of the PatientFinder1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="CommandEventArgs"/> instance containing the event data.</param>
        void PatientFinder1_SelectedPatientChanged(object sender, CommandEventArgs e)
        {
            int locationId, patientId;

            List<KeyValuePair<string, Object>> param = e.CommandArgument as List<KeyValuePair<string, Object>>;
            patientId = (int)param.Find(l => l.Key == "PatientID").Value;
            locationId = (int)param.Find(l => l.Key == "LocationID").Value;
            if (HFormName.Value == "FamilyInfo")
            {
              string  theUrl = string.Format("{0}?RefId={1}&&PatientId={2}", "~/ClinicalForms/frmFamilyInformation.aspx", patientId, Session["PtnRedirect"].ToString());

              Response.Redirect(theUrl);
              
          
            }
        }
    }
}