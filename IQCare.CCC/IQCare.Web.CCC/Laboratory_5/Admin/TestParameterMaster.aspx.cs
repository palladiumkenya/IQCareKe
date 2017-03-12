using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Laboratory;
using Application.Presentation;
using Entities.Lab;
using System.Configuration;
using Application.Common;
using System.Data;
using IQCare.Web.UILogic;

namespace IQCare.Web.Laboratory.Admin
{
    public partial class TestParameterMaster : System.Web.UI.Page
    {
        private bool isError = false;
        //string session_Key_param = "TESTPARAM_X#$";
        //string session_Key_Lab = "LABTEST_X#$";
        //string session_Key_Units = "RESULTUNITS_CDSF";
        ILabManager mGr = (ILabManager)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabManager, BusinessProcess.Laboratory");

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
        void showErrorMessage(ref Exception ex)
        {
            this.isError = true;
            if (this.Debug)
            {
                lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            }
            else
            {
                SystemSetting.LogError(ex);
                //lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team.  " + ex.Message;
                //this.isError = this.divError.Visible = true;
                //Exception lastError = ex;
                //lastError.Data.Add("Domain", "Lab Management");
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
        AuthenticationManager Authentication = new AuthenticationManager();
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
        protected int LabTestId
        {
            get
            {
              return int.Parse(hLabTestId.Value); 
            }
            set
            {
                hLabTestId.Value = value.ToString();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = 0;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Laboratory >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Test Parameters";
            if (!IsPostBack)
            {
                Session.Remove("PameterResultOption");
                Session[SessionKey.TestParameters] = null;
                Session[SessionKey.ResultUnit] = null;
                Session[SessionKey.ResultOptions] = null;
                Session[SessionKey.SelectedGroup] = null;
               
                if (Session[SessionKey.SelectedLab] == null)
                {
                    string theUrl = string.Format("{0}", "~/Laboratory/Admin/LabTestMaster.aspx");
                    //Response.Redirect(theUrl);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
               
                this.PopulateParameters();
            }
        }
        void ShowErrorMessage(ref Exception ex)
        {
            this.isError = true;
            if (this.Debug)
            {
                lblError.Text = ex.Message + ex.StackTrace + ex.Source;
            }
            else
            {
                SystemSetting.LogError(ex);
                //lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team.  " + ex.Message;
                //this.isError = this.divError.Visible = true;
                //Exception lastError = ex;
                //lastError.Data.Add("Domain", "Lab Management");
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
        protected void Page_PreRender(object sender, EventArgs e)
        {
            divError.Visible = this.isError;
            btnCancel.OnClientClick = "javascript:window.location='./LabTestMaster.aspx'; return false;";
            btnAdd.OnClientClick = "javascript:window.location='./ManageTestParameter.aspx'; return false;";
        }
        void PopulateParameters()
        {
            LabTest selectedLab = (LabTest)Session[SessionKey.SelectedLab];
                LabTestId = selectedLab.Id;
                labelTestName.Text = selectedLab.Name;
              gridParamMaster.DataSource =  mGr.GetLabTestParameters(selectedLab.Id);
              gridParamMaster.DataBind();
        }
       
        protected void gridParamMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            int index;
            if (e.CommandName == "DeleteParam")
            {
                int paramId = Int32.Parse(e.CommandArgument.ToString());
                //gridParamMaster.SelectedIndex = index;
               // int paramId = int.Parse(gridParamMaster.SelectedDataKey.Values["Id"].ToString());
                mGr.DeleteTestParameter(paramId,this.LabTestId,this.UserId);
                IQCareMsgBox.NotifyAction("Test Parameter has been deleted", "Success Deletion", false, this, "");
                this.PopulateParameters();
                return;
            }
            else if(e.CommandName == "Modify")
            {
                index = Int32.Parse(e.CommandArgument.ToString());
                gridParamMaster.SelectedIndex = index;
                int paramId = int.Parse(gridParamMaster.SelectedDataKey.Values["Id"].ToString());
                GridViewRow row = (gridParamMaster.Rows[index]);
                string paramName = row.Cells[0].Text.Trim();
                string refernece = row.Cells[1].Text.Trim();
                TestParameter param = new TestParameter() { Id = paramId, LabTestId = this.LabTestId, ReferenceId= refernece, Name=paramName,DeleteFlag=false };

                Session[SessionKey.TestParameters] = param;
                Guid g = Guid.NewGuid();
                string theUrl = "./ManageTestParameter.aspx?key="+g.ToString();
                Response.Redirect(theUrl, true);
                return;
            }
        }

        protected void gridParamMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TestParameter row = ((TestParameter)e.Row.DataItem);
                    e.Row.Cells[2].Text = row.DataType == "SELECTLIST" ? "Select List" : (row.DataType == "NUMERIC") ? "Numeric" : row.DataType;
                    e.Row.Cells[5].Text = row.DeleteFlag ? "In Active" : "Active";

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }											

        }

        protected void gridParamMaster_DataBound(object sender, EventArgs e)
        {
            //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            //for (int i = 0; i < gridParamMaster.Columns.Count; i++)
            //{
            //    if (gridParamMaster.Columns[i].Visible && !string.IsNullOrEmpty(gridParamMaster.Columns[i].HeaderText.Trim()))
            //    {
            //        TableHeaderCell cell = new TableHeaderCell();
            //        TextBox txtSearch = new TextBox();
            //        txtSearch.Attributes["placeholder"] = gridParamMaster.Columns[i].HeaderText;
            //        txtSearch.CssClass = "search_textbox";
            //        cell.Controls.Add(txtSearch);
            //        row.Controls.Add(cell);
            //    }
            //}
            //try { gridParamMaster.HeaderRow.Parent.Controls.AddAt(1, row); }
            //catch { }
            //if (gridParamMaster.Rows.Count < 5)
            //{
               // row.Style.Clear();
                //row.Style.Add("display", "none");
          //  }
        }
    }
}