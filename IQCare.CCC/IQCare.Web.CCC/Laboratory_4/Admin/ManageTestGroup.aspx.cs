using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Interface.Laboratory;
using Application.Presentation;
using System.Data;
using AjaxControlToolkit;
using Entities.Lab;
using IQCare.Web.UILogic;

namespace IQCare.Web.Laboratory.Admin
{
    public partial class ManageTestGroup : System.Web.UI.Page
    {
        private bool isDataEntry = false;
        private bool isError = false;
        private string RedirectUrl = string.Format("{0}", "~/Laboratory/Admin/LabTestMaster.aspx");
        //string session_Key_param = "TESTPARAM_X#$";
        //string session_Key_Lab = "LABTEST_X#$";
        //string session_Key_Units = "RESULTUNITS_CDSF";
        //string session_Key_Group = "GROUPLABTEST_X#$";
        ILabManager mGr = (ILabManager)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabManager, BusinessProcess.Laboratory");
        protected int MainLabTestId
        {
            get
            {
                return int.Parse(hMainLabTestId.Value);
            }
            set
            {
                hMainLabTestId.Value = value.ToString();
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
        protected string svDelete
        {
            get
            {
                return HasPermission ? "" : "none";
            }
        }
        protected bool HasPermission
        {
            get
            {
                return (Authentication.HasFeatureRight("MANAGE_LABORATORY", (DataTable)Session["UserRight"]) == true);
            }
        }
        protected string sDataEntry
        {
            get
            {
                return this.isDataEntry ? "" : "none";
            }
        }
        private bool Debug
        {
            get
            {
                bool _debug = true;
                bool.TryParse(ConfigurationManager.AppSettings.Get("DEBUG").ToLower(), out _debug);
                return _debug;
            }
        }
        private int UserId
        {
            get
            {
                return Convert.ToInt32(Session["AppUserId"]);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = 0;
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Laboratory >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Test Groups";
            if (!IsPostBack)
            {
                Session.Remove("PameterResultOption");
                Session[SessionKey.TestParameters] = null;
                Session[SessionKey.ResultUnit] = null;
                Session[SessionKey.UnitConfig] = null;
                if (Session[SessionKey.SelectedLab] == null)
                {
                    string theUrl = string.Format("{0}", "~/Laboratory/Admin/LabTestMaster.aspx");
                    //Response.Redirect(theUrl);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }

                this.PopulateLabTest();
            }
        }
        LabTestGroup SelectedGroup
        {
            set
            {
                base.Session[SessionKey.SelectedGroup] = value;
            }
            get
            {
                return base.Session[SessionKey.SelectedGroup] == null ? new LabTestGroup() : (LabTestGroup)base.Session[SessionKey.SelectedGroup];
            }
        }
        private void PopulateLabTest()
        {
            try
            {
                LabTest selectedLab = (LabTest)Session[SessionKey.SelectedLab];
                this.MainLabTestId = selectedLab.Id;
                labelTestName.Text = selectedLab.Name;
                LabTestGroup group = mGr.GetGroupLabTest(this.MainLabTestId);
                this.SelectedGroup = group;
                if (null != group && null != group.ComponentTest)
                {
                    gridlabMaster.DataSource = group.ComponentTest;
                    gridlabMaster.DataBind();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }

        protected void AddLabRecord(object sender, EventArgs e)
        {
            if (null == this.SelectedTest)
            {
                 hdCustID.Value = textSelectLab.Text = "";
                 this.isDataEntry = false;
                 return;
            }
            LabTestGroup testGroup = this.SelectedGroup;
            LabTest candidate = this.SelectedTest;
            bool proceed = false;
            proceed = (null == testGroup.ComponentTest || testGroup.ComponentTest.Count == 0 || !testGroup.ComponentTest.Exists(o => o.Id == candidate.Id));
            if (!proceed || candidate.IsGroup)
            {
                hdCustID.Value = textSelectLab.Text = "";
                this.isDataEntry = false;
                return;
            }
            try
            {
                mGr.SaveGroupLabTest(candidate.Id, this.MainLabTestId);
                this.PopulateLabTest();
                IQCareMsgBox.NotifyAction(string.Format("{0} has been added to this group", candidate.Name), "Success", false,this, "");
                this.SelectedTest = null;
                hdCustID.Value = textSelectLab.Text = "";
                this.isDataEntry = false;
                return;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }

        protected void CancelLabEntry(object sender, EventArgs e)
        {
            this.SelectedTest = null;
            hdCustID.Value = textSelectLab.Text = "";
            this.isDataEntry = false;

        }

        protected void ExitPage(object sender, EventArgs e)
        {
            hdCustID.Value = textSelectLab.Text = "";
            this.isDataEntry = false;
            base.Session[SessionKey.TestParameters] = base.Session[SessionKey.SelectedLab] = base.Session[SessionKey.UnitConfig] = base.Session[SessionKey.SelectedGroup] = null;
            System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            Response.Redirect(RedirectUrl, true);
        }


        protected void LabNameChanged(object sender, EventArgs e)
        {
            LabTestGroup testGroup = this.SelectedGroup;
            int _testId;
            string _testname = "";
            string _refId = "";
            string _testDepartmentId = "";
            string _testDepartment = "";
            bool isGroup = true;
            int _paramCount;
            if (!(hdCustID.Value != null && String.IsNullOrEmpty(hdCustID.Value)))
            {
                String[] itemCodes = hdCustID.Value.Split(';');
                if (itemCodes.Length == 7)
                {
                    _testId = Convert.ToInt32(itemCodes[0]);
                    _testname = itemCodes[1].ToString();
                    _refId = itemCodes[2].ToString();
                    _paramCount = Convert.ToInt32(itemCodes[3]);
                    isGroup = bool.Parse(itemCodes[4].ToString());
                    _testDepartmentId = itemCodes[5].ToString();
                    _testDepartment = itemCodes[6].ToString();

                    bool proceed = false;
                    proceed = (null == testGroup.ComponentTest || testGroup.ComponentTest.Count == 0 || !testGroup.ComponentTest.Exists(o => o.Id == _testId));

                    if (!proceed || isGroup)
                    {
                        ((TextBox)sender).Text = "";
                        hdCustID.Value = "";
                        this.isDataEntry = false;
                        return;
                    }
                    this.SelectedTest = new LabTest()
                    {
                        Id = _testId,
                        Name = _testname,
                        Department = new TestDepartment() { Id = Convert.ToInt32(_testDepartmentId), Name = _testDepartment , DeleteFlag=false},
                        ReferenceId= _refId,
                        ParameterCount = _paramCount,
                        IsGroup = isGroup
                    };
                    
                    this.isDataEntry = true;
                }

            }
        }
        LabTest SelectedTest
        {
            get
            {
                return (LabTest)base.Session["SelectedTest"];
            }
            set
            {
                base.Session["SelectedTest"] = value;
            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {

            divError.Visible = isError;
        }
        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
            Exception ex = Server.GetLastError();
            this.ShowErrorMessage(ref ex);
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
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> Searchlab(string prefixText, int count)
        {
            ILabRequest rMgr = (ILabRequest)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");

            DataTable dt = new DataTable();
            dt = rMgr.FindLabByName(prefixText, true);
            List<string> ar = new List<string>();
            string custItem = string.Empty;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        custItem = AutoCompleteExtender.CreateAutoCompleteItem(
                            row["Name"].ToString(),
                            String.Format("{0};{1};{2};{3};{4};{5};{6}",
                                row["Id"],
                                row["Name"],
                                row["ReferenceId"],
                                row["ParameterCount"],
                                row["IsGroup"],
                                row["DepartmentId"] == DBNull.Value ? "-1" : row["DepartmentId"].ToString(),
                                row["DepartmentId"] == DBNull.Value ? "-1" : row["Department"]
                            )
                            );
                        ar.Add(custItem);
                    }
                    catch
                    {
                    }
                }
            }

            return ar;
        }
        

        protected void gridlabMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "RemoveFromGroup")
                {
                    int testId = Int32.Parse(e.CommandArgument.ToString());
                    mGr.RemoveTestFromGroup(testId,this.MainLabTestId);
                    IQCareMsgBox.NotifyAction("Lab test has been removed from this group", "Success", false,this, "");
                    this.PopulateLabTest();
                    return;
                }

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }

        protected void gridlabMaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void grdLab_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnOkAction_Click(object sender, EventArgs e)
        {

        }

    }
}