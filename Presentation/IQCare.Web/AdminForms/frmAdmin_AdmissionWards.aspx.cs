using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.Administration;
using Interface.Administration;
using IQCare.Web.UILogic;

namespace IQCare.Web.Admin
{
    public partial class AdmissionWards : System.Web.UI.Page
    {
        /// <summary>
        /// The is error
        /// </summary>
        private bool isError = false;

        /// <summary>
        /// Gets or sets the type of the action.
        /// </summary>
        /// <value>
        /// The type of the action.
        /// </value>
        private string ActionType
        {
            get
            {
                return this.HActionType.Value;
            }
            set
            {
                this.HActionType.Value = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="frmAdmin_AdmissionWards"/> is debug.
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
        private int LocationID
        {
            get
            {
                return Convert.ToInt32(Session["AppLocationId"]);
            }
        }

        /// <summary>
        /// Gets or sets the selected ward identifier.
        /// </summary>
        /// <value>
        /// The selected ward identifier.
        /// </value>
        private int? SelectedWardID
        {
            get
            {
                return int.Parse(HSelectedID.Value);
            }
            set
            {
                HSelectedID.Value = value.ToString();
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
        /// Adds the new ward.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void AddNewWard(object sender, EventArgs e)
        {
            this.ActionType = "NEW";
            textWardName.Text = textCapacity.Text = "";
            ddlPatientCategory.SelectedIndex = 0;
            rblStatus.SelectedIndex = 0;
            buttonSubmitWard.Text = "Save";
            mpeWardPopup.Show();
        }

        /// <summary>
        /// Handles the Click event of the btnCloseMain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCloseMain_Click(object sender, EventArgs e)
        {
            string url = "./frmAdmin_PMTCT_CustomItems.aspx";
            Response.Redirect(url, false);
        }

        /// <summary>
        /// Handles the RowCommand event of the gridWardList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridWardList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow gridRow = (gridWardList.Rows[index]);
                gridWardList.SelectedIndex = index;
                if (e.CommandName == "RowClick")
                {
                    HSelectedID.Value = this.gridWardList.SelectedDataKey[0].ToString();
                    string wardName = gridRow.Cells[0].Text.Trim();
                    string category = gridRow.Cells[1].Text.Trim();
                    string capacity = gridRow.Cells[2].Text.Trim();
                    string status = gridRow.Cells[3].Text.Trim();
                    textWardName.Text = wardName;
                    ddlPatientCategory.ClearSelection();
                    ListItem item = ddlPatientCategory.Items.FindByValue(category);
                    if (item != null)
                        item.Selected = true;
                    textCapacity.Text = capacity;

                    rblStatus.SelectedIndex = (status == "Active") ? 0 : 1;

                    buttonSubmitWard.Text = "Update";
                    this.ActionType = "EDIT";
                    mpeWardPopup.Show();
                }

                return;
            }
            catch (Exception ex)
            {
                showErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Handles the RowDataBound event of the gridWardList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridWardList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(gridWardList, "RowClick$" + e.Row.RowIndex.ToString(), false));

                PatientWard rowView = (PatientWard)e.Row.DataItem;
                string status = rowView.Active ? "Active" : "Inactive";
                e.Row.Cells[3].Text = status;
            }
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
                this.PopulateWardList();
            }
        }

        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            divError.Visible = isError;
        }

        /// <summary>
        /// Saves the ward.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void SaveWard(object sender, EventArgs e)
        {
            string errorMessage = "";
            string actionType = ActionType.ToUpper().Trim();
            bool haserror = false;

            if (string.IsNullOrEmpty(textWardName.Text.Trim()))
            {
                haserror = true;
                errorMessage += "Missing: Ward Name";
            }
            if (string.IsNullOrEmpty(textCapacity.Text.Trim()))
            {
                haserror = true;
                errorMessage += "<br /> Missing: Bed capacity";
            }
            if (ddlPatientCategory.SelectedValue == "")
            {
                haserror = true;
                errorMessage += "<br /> Missing: Ward's patient category";
            }
            if (haserror)
            {
                // isError = true;
                errorLabel.Text = errorMessage;
                panelError.Visible = true;
                mpeWardPopup.Show();
                //parameterPopup.Show();
                return;
            }
            try
            {
                //database action
                IWardsMaster wardMaster = (IWardsMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BWardMaster, BusinessProcess.Administration");
                PatientWard ward = new PatientWard()
                {
                    WardID = this.ActionType == "EDIT" ? this.SelectedWardID : null,
                    WardName = textWardName.Text.Trim(),
                    Active = rblStatus.SelectedValue == "1",
                    Capacity = int.Parse(textCapacity.Text),
                    PatientCategory = ddlPatientCategory.SelectedValue,
                    LocationID = this.LocationID
                };
                wardMaster.SaveWard(ward, this.UserID);
                this.PopulateWardList();
                this.ActionType = "VIEW";
            }
            catch (Exception ex)
            {
                this.NotifyAction(string.Format("{0} {1} ", actionType, ex.Message), "Error occurred ..", true);
                this.ActionType = "VIEW";
            }
        }

        /// <summary>
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        private void NotifyAction(string strMessage, string strTitle, bool errorFlag)
        {
            lblNoticeInfo.Text = strMessage;
            lblNotice.Text = strTitle;
            lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
            lblNoticeInfo.Font.Bold = true;
            imgNotice.ImageUrl = (errorFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
            this.notifyPopupExtender.Show();
        }

        /// <summary>
        /// Populates the ward list.
        /// </summary>
        private void PopulateWardList()
        {
            try
            {
                IWardsMaster wardMaster = (IWardsMaster)ObjectFactory.CreateInstance("BusinessProcess.Administration.BWardMaster, BusinessProcess.Administration");
                List<PatientWard> wards = wardMaster.GetWards(this.LocationID);
                gridWardList.DataSource = wards;
                gridWardList.DataBind();
            }
            catch (Exception ex)
            {
                this.showErrorMessage(ref ex);
            }
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
            }
            mpeWardPopup.Hide();
            notifyPopupExtender.Hide();
        }
    }
}