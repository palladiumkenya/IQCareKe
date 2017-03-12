using System;
using System.Web.UI.WebControls;
using Interface.Laboratory;
using Application.Presentation;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using Entities.Lab;
using System.Web.UI;
using IQCare.Web.UILogic;

namespace IQCare.Web.Laboratory.Admin
{
    public partial class LabTestMaster : System.Web.UI.Page
    {
        private bool isError = false;
        //private string session_Key_congif = "Units_Config_?&";
        //private string session_Key_Lab = "LABTEST_X#$";
        //private string session_Key_param = "TESTPARAM_X#$";
        //private string session_Key_Units = "RESULTUNITS_CDSF";
        //string session_Key_Group = "GROUPLABTEST_X#$";
        private ILabManager mGr = (ILabManager)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabManager, BusinessProcess.Laboratory");

        int UserId
        {
            get
            {
                return Convert.ToInt32(base.Session["AppUserId"]);
            }
        }
        /// <summary>
        /// Gets a value indicating whether this <see cref="frmBilling_ReverseBill"/> is debug.
        /// </summary>
        /// <value>
        ///   <c>true</c> if debug; otherwise, <c>false</c>.
        /// </value>
        bool Debug
        {
            get
            {
                return ConfigurationManager.AppSettings.Get("DEBUG").ToLower().Equals("true");
            }
        }

        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        void ShowErrorMessage(ref Exception ex)
        {
            this.isError = true;
            if (this.Debug)
            {
                lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            }
            else
            {
                lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team.  " + ex.Message;
                this.isError = this.divError.Visible = true;
                Exception lastError = ex;
                lastError.Data.Add("Domain", "Lab Management");
                SystemSetting.LogError(lastError);
                //try
                //{
                //    Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                //    logger.LogError(ex);
                //}
                //catch
                //{

                //}
            }
        }
        protected string svEdit
        {
            get
            {
                return HasPermission ? "" : "none";
            }
        }
        AuthenticationManager Authentication = new AuthenticationManager();
        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
            Exception ex = Server.GetLastError();
            this.ShowErrorMessage(ref ex);
        }
        protected string svDelete
        {
            get
            {
                return HasPermission ? "" : "none";
            }
        }
        /// <summary>
        /// Gets a value indicating whether this instance has permission.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance has permission; otherwise, <c>false</c>.
        /// </value>
        protected bool HasPermission
        {
            get
            {
                return (Authentication.HasFeatureRight("MANAGE_LABORATORY", (DataTable)Session["UserRight"]) == true);
            }
        }

        protected void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string testName = textLabName.Text.Trim();
                string testRef = textReference.Text;
                if (testRef == "")
                {
                    textLabName.Text.Trim().ToUpper().Replace(' ', '_');
                }
                bool isGroup = chkGroupYes.Checked;
                TestDepartment department = null;
                if (!isGroup)
                {
                    if (ddlDepartment.SelectedValue == "-1")
                    {
                        throw new Exception("Please select department");
                    }
                    department = new TestDepartment() { Id = Convert.ToInt32(ddlDepartment.SelectedValue), Name = ddlDepartment.SelectedItem.Text };
                }
                LabTest test = new LabTest()
                {
                    Id = -1,
                    Name = testName,
                    ReferenceId = testRef,
                    IsGroup = isGroup,
                    DeleteFlag = false,
                    Department = department,
                    LoincCode = txtLoinc.Text.Trim(),
                    UserId = this.UserId,
                    TestParameter = null
                };
                LabTest result = mGr.SaveLabTest(test, this.UserId);
                this.PopulateLabTests();
                IQCareMsgBox.NotifyAction("Lab " + result.Name + " successfully saved", "Success", false,this, "javascript:HideModalPopup();return false;");
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }
        //private void NotifyAction(string strMessage, string strTitle, bool errorFlag, string onOkScript = "")
        //{
        //    lblNoticeInfo.Text = strMessage;
        //    lblNotice.Text = strTitle;
        //    lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
        //    lblNoticeInfo.Font.Bold = true;
        //    imgNotice.ImageUrl = (errorFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
        //    btnOkAction.OnClientClick = "";
        //    if (onOkScript != "")
        //    {
        //        btnOkAction.OnClientClick = onOkScript;
        //    }
        //    this.notifyPopupExtender.Show();
        //}
        
        protected void gridlabMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "DeleteLab")
                {
                    int testId = Int32.Parse(e.CommandArgument.ToString());
                    mGr.DeleteLabTest(testId, this.UserId);
                    IQCareMsgBox.NotifyAction("Lab test has been made inactive", "Success", false, this, "");
                    this.PopulateLabTests();
                    return;
                }
                else if (e.CommandName == "SetActive")
                {
                    int testId = Int32.Parse(e.CommandArgument.ToString());
                    mGr.ActivateLabTest(testId, true,this.UserId);
                    IQCareMsgBox.NotifyAction("Lab test has been activated", "Success", false, this, "");
                    this.PopulateLabTests();
                    return;
                }
                else if (e.CommandName == "SetInactive")
                {
                    int testId = Int32.Parse(e.CommandArgument.ToString());
                    mGr.ActivateLabTest(testId, false, this.UserId);
                    IQCareMsgBox.NotifyAction("Lab test has been inactivated", "Success", false, this, "");
                    this.PopulateLabTests();
                    return;
                }
                else if (e.CommandName == "ViewParam")
                {
              
                    Control ctr = sender as Control;
                    GridViewRow gvRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                 
                    int index = gvRow.RowIndex;

                    int testId = Int32.Parse(e.CommandArgument.ToString());
                    //GridViewRow row = (gridlabMaster.Rows[index]);
                    string labName = gvRow.Cells[0].Text.Trim();
                    string refernece = gvRow.Cells[1].Text.Trim();
                    LabTest labtest = new LabTest() { Id = testId, Name = labName, ReferenceId = refernece };
                    Session[SessionKey.SelectedLab] = labtest;
                    Guid g = Guid.NewGuid();
                    string theUrl = "./TestParameterMaster.aspx?key=" + g.ToString();

                    Response.Redirect(theUrl, true);

                }
                else if (e.CommandName == "ViewGroup")
                {
                    Control ctr = sender as Control;
                    GridViewRow gvRow = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

                    int index = gvRow.RowIndex;
                 //   gridlabMaster.SelectedIndex = index;
                    int testId = Int32.Parse(e.CommandArgument.ToString());
                    //GridViewRow row = (gridlabMaster.Rows[index]);
                    string labName = gvRow.Cells[0].Text.Trim();
                    string refernece = gvRow.Cells[1].Text.Trim();
                    LabTest labtest = new LabTest() { Id = testId, Name = labName, ReferenceId = refernece };
                    Session[SessionKey.SelectedLab] = labtest;
                    Guid g = Guid.NewGuid();
                    string theUrl = "./ManageTestGroup.aspx?key=" + g.ToString();

                    Response.Redirect(theUrl, true);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }

        private void PopulateLabTests()
        {
            try
            {
                gridlabMaster.DataSource = mGr.GetLabTests();
                gridlabMaster.DataBind();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }

        protected void gridlabMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LabTest row = ((LabTest)e.Row.DataItem);
                    e.Row.Cells[2].Text = row.IsGroup ? "Yes" : "No";
                    e.Row.Cells[5].Text = row.Active ? "Active" : "Inactive";
                    Button buttonActivate = e.Row.FindControl("buttonEdit") as Button;

                    Label labelText = e.Row.FindControl("labelText") as Label;
                    if (null != buttonActivate)
                    {
                        buttonActivate.Text = row.Active ? "Make Inactive" : "Activate";
                        buttonActivate.CommandName = row.Active ? "SetInactive" : "SetActive";
                    }
                    if (null != labelText)
                    {
                        labelText.Text = string.Format("You are about delete {0}. &nbsp;<br /> Are you sure you want to proceed?", row.Name);
                    }

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = 0;
            if (!IsPostBack)
            {
                Session[SessionKey.SelectedGroup]
                    = Session[SessionKey.SelectedLab]
                    = Session[SessionKey.TestParameters]
                     = Session[SessionKey.SelectedLabTestOrder]
                    = Session[SessionKey.ResultOptions]
                    = Session[SessionKey.ResultUnit]
                    = Session[SessionKey.UnitConfig] = null;
                this.PopulateDepartment();
                this.PopulateLabTests();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            divError.Visible = this.isError;
            btnCancel.OnClientClick = "javascript:window.location='./Home.aspx'; return false;";
            btnAdd.OnClientClick = "javascript:ShowModalPopup(); return false;";
        }
        protected string ShowParam(object isGroup, object deleteFlag)
        {
            return Convert.ToBoolean(isGroup.ToString()) || Convert.ToBoolean(deleteFlag) ? "none" : "";
        }
        protected string ShowGroup(object isGroup, object deleteFlag)
        {
            return Convert.ToBoolean(isGroup.ToString()) && !Convert.ToBoolean(deleteFlag) ? "" : "none";
        }
        protected string ShowDelete(object deleteFlag)
        {
            return Convert.ToBoolean(deleteFlag.ToString()) ? "none" : "";
        }
        void PopulateDepartment()
        {
            try
            {
                List<TestDepartment> department = mGr.GetTestDepartments();
                ddlDepartment.DataSource = department;
                ddlDepartment.DataTextField = "Name";
                ddlDepartment.DataValueField = "Id";
                ddlDepartment.DataBind();
                ddlDepartment.Items.Insert(0, new ListItem("Select", "-1"));
            }
            catch (Exception ex)
            {

                this.ShowErrorMessage(ref ex);
            }
        }
        protected void grdLab_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void gridlabMaster_DataBound(object sender, EventArgs e)
        {
            //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            //for (int i = 0; i < gridlabMaster.Columns.Count; i++)
            //{
            //    if (gridlabMaster.Columns[i].Visible && !string.IsNullOrEmpty(gridlabMaster.Columns[i].HeaderText.Trim()))
            //    {
            //        TableHeaderCell cell = new TableHeaderCell();
            //        TextBox txtSearch = new TextBox();
            //        txtSearch.Attributes["placeholder"] = gridlabMaster.Columns[i].HeaderText;
            //        txtSearch.CssClass = "search_textbox";
            //        cell.Controls.Add(txtSearch);
            //        row.Controls.Add(cell);
            //    }
            //}
            //try { gridlabMaster.HeaderRow.Parent.Controls.AddAt(1, row); }
            //catch { }
            //if (gridlabMaster.Rows.Count < 5)
            //{
                //row.Style.Clear();
               // row.Style.Add("display", "none");
            //}
        }
    }
}