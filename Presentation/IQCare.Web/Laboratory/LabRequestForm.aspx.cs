using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Application.Presentation;
using Entities.Lab;
using Interface.Laboratory;
using IQCare.Web.UILogic;

namespace IQCare.Web.Laboratory
{
    public partial class LabRequestForm : System.Web.UI.Page
    {
        private bool isDataEntry = false;
        private bool isError = false;
        private string RedirectUrl = "../ClinicalForms/frmPatient_Home.aspx";
        private ILabRequest requestMgr = (ILabRequest)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");
        protected int LabTestId
        {
            get
            {
                int val = int.Parse(hLabTestId.Value);
                if (val == -1) return -1;
                return val;
            }
            set
            {
                hLabTestId.Value = value.ToString();
            }
        }
        protected int ParameterCount
        {
            get
            {
                return Convert.ToInt32(hParamCount.Value.Trim());
            }
            set
            {
                hParamCount.Value = value.ToString();
            }
        }
        protected int DepartmentId
        {
            get
            {
                return Convert.ToInt32(hdDepartmentId.Value.Trim());
            }
            set
            {
                hdDepartmentId.Value = value.ToString();
            }
        }
        protected string TestReferenceId
        {
            get
            {
                return hdReferenceId.Value;
            }
            set
            {
                hdReferenceId.Value = value.Trim();
            }
        }
        protected string Department
        {
            get
            {
                return hdDepartmentname.Value.Trim();
            }
            set
            {
                hdDepartmentname.Value = value.Trim();
            }
        }

        protected string LabTestName
        {
            get
            {
                return hdTestName.Value.Trim();
            }
            set
            {
                hdTestName.Value = value.Trim();
            }
        }

        public int LabOrderId
        {
            get
            {
                int val = int.Parse(HLabOrderId.Value);

                return val;
            }
            private set
            {
                HLabOrderId.Value = value.ToString();
            }
        }
        public int LabOrderTestId
        {
            get
            {
                int val = int.Parse(HLabOrderTestId.Value);

                return val;
            }
            private set
            {
                HLabOrderTestId.Value = value.ToString();
            }
        }
        public bool IsGroup
        {
            get
            {
                return (hdIsGroup.Value.ToLower()) == "true";
            }
            set
            {
                hdIsGroup.Value = value.ToString().ToLower();
            }
        }
        protected string sDataEntry
        {
            get
            {
                return this.isDataEntry ? "" : "none";
            }
        }
        protected string sEdit
        {
            get
            {
                return this.LabOrderId > 0 ? "none" : "";
            }
        }
        protected string sView
        {
            get
            {
                return this.LabOrderId > 0 ? "" : "none";
            }
        }
        protected string sHasData
        {
            get
            {
                if (this.OrderedLabs.OrderedTest != null && this.OrderedLabs.OrderedTest.Count > 0)
                {
                    return this.OrderedLabs.OrderedTest.Count(o => o.Id < 1) > 0 ? "" : "none";
                }
                return "none";
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

        private int EmployeeId
        {
            get
            {
                return Convert.ToInt32(Session["AppUserEmployeeId"].ToString());
            }
        }

        /// <summary>
        /// Gets the location identifier.
        /// </summary>
        /// <value>
        /// The location identifier.
        /// </value>
        private int LocationId
        {
            get
            {
                return Convert.ToInt32(Session["AppLocationId"]);
            }
        }

        private int ModuleId { get { return Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]); } }

        private LabOrder OrderedLabs
        {
            get
            {
                if (base.Session["OrderedLabs"] == null)
                {

                    return new LabOrder()
                    {
                        PatientId = this.PatientPk,
                        LocationId = this.LocationId,
                        UserId = this.UserId,
                        ModuleId = this.ModuleId
                    };
                }
                else
                {
                    return (LabOrder)base.Session["OrderedLabs"];
                }
            }
            set
            {
                Session["OrderedLabs"] = value;
            }
        }

        /// <summary>
        /// The patient identifier
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        private int PatientPk
        {
            get
            {
                return Convert.ToInt32(Session["PatientId"].ToString());
            }
        }

        private DataTable PatientInfo
        {
            get
            {
                return (DataTable)Session["PatientInformation"];
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
                return Convert.ToInt32(Session["AppUserId"]);
            }
        }
        /// <summary>
        /// Searchlabs the specified prefix text.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> Searchlab(string prefixText, int count)
        {
            ILabRequest rMgr = (ILabRequest)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");

            DataTable dt = new DataTable();
            dt = rMgr.FindLabByName(prefixText);
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

        protected void AddLabRecord(object sender, EventArgs e)
        {
            LabOrderTest thisTest = null;
            LabOrder _order = this.OrderedLabs;
            if (null != _order && null != _order.OrderedTest && _order.OrderedTest.Count > 0 && _order.OrderedTest.Exists(o => o.TestId == this.LabTestId))
            {
                this.isDataEntry = false;
                return;
            }
            if (null == _order.OrderedTest)
            {
                _order.OrderedTest = new List<LabOrderTest>();
            }
            thisTest = new LabOrderTest();
            thisTest.Test = new LabTest()
            {
                Id = this.LabTestId,
                Name = this.LabTestName,
                Department = new TestDepartment() { Id = this.DepartmentId, Name = this.Department, DeleteFlag = false },
                ParameterCount = this.ParameterCount,
                ReferenceId = this.TestReferenceId,
                IsGroup = this.IsGroup,
                DeleteFlag = false,
                UserId = this.UserId

            };
            thisTest.Id = -1;
            thisTest.TestNotes = txtTestNotes.Text;
            _order.OrderedTest.Add(thisTest);
            this.OrderedLabs = _order;
            hdCustID.Value = textSelectLab.Text = txtTestNotes.Text = "";
            this.isDataEntry = false;

            this.BindLabTests();
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
        }

        protected void btnOkAction_Click(object sender, EventArgs e)
        {
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string labtobedone = txtLabtobeDone.Text;
            string laborder = ddlaborderedbyname.SelectedValue;
            string laborderdate = txtlaborderedbydate.Text;
            string appcurrdate = hdappcurrentdate.Value;
            string strClinicalNotes = txtClinicalNotes.Text;
            if (FieldValidation(labtobedone, laborderdate, laborder, appcurrdate) == false)
            {
                return;
            }


            ILabRequest requestMgr = (ILabRequest)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory"); ;
            if (LabOrderId > 0) //update
            {
                //
            }
            else
            {
                DateTime? nullDatetime = null;

                LabOrder order = this.OrderedLabs;

                order.PatientId = this.PatientPk;
                order.LocationId = this.LocationId;
                order.ModuleId = this.ModuleId;
                order.DeleteFlag = false;
                order.OrderDate = Convert.ToDateTime(laborderdate);
                order.CreateDate = DateTime.Now;
                order.PreClinicDate = (labtobedone != null && String.IsNullOrEmpty(labtobedone)) ? nullDatetime : Convert.ToDateTime(labtobedone);
                order.OrderedBy = Convert.ToInt32(ddlaborderedbyname.SelectedValue);
                order.UserId = this.UserId;
                order.ClinicalNotes = strClinicalNotes;

                if (order.OrderedTest.Count == 0)
                {
                    IQCareMsgBox.NotifyAction("No lab test selected", "Error saving lab order", true,this, "");
                    return;
                }
                LabOrder _saveOrder = requestMgr.SaveLabOrder(order, this.UserId, this.LocationId);
                IQCareMsgBox.NotifyAction(string.Format("Lab Order number {0}, saved successfully", _saveOrder.OrderNumber), "Lab Order", false, this, string.Format("javascript:window.location='{0}'; return true;", this.RedirectUrl));
              
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // this.BindLabTests();
        }

        protected void CancelLabEntry(object sender, EventArgs e)
        {

            hdCustID.Value = textSelectLab.Text = txtTestNotes.Text = "";
            this.isDataEntry = false;
            base.Session["LAB_REQTEST"] = null;
            base.Session["OrderedLabs"] = null;
        }

        protected void ExitPage(object sender, EventArgs e)
        {
            hdCustID.Value = textSelectLab.Text = txtTestNotes.Text = "";
            this.isDataEntry = false;
            base.Session["LAB_REQTEST"] = null;
            base.Session["OrderedLabs"] = null;
            Response.Redirect(RedirectUrl);
        }

        protected void LabNameChanged(object sender, EventArgs e)
        {
            LabOrder _order = this.OrderedLabs;
            int _testId;
            string _testname = "";
            string _refId = "";
            string _testDepartmentId = "";
            string _testDepartment = "";
            string _group = "";
            int _paramCount;
            if (!(hdCustID.Value != null && string.IsNullOrEmpty(hdCustID.Value)))
            {
                string[] itemCodes = hdCustID.Value.Split(';');
                if (itemCodes.Length == 7)
                {
                    _testId = Convert.ToInt32(itemCodes[0]);
                    _testname = itemCodes[1].ToString();
                    _refId = itemCodes[2].ToString();
                    _paramCount = Convert.ToInt32(itemCodes[3]);
                    _group = itemCodes[4].ToString();
                    _testDepartmentId = itemCodes[5].ToString();
                    _testDepartment = itemCodes[6].ToString();

                    bool proceed = false;
                    proceed = (null == _order.OrderedTest || _order.OrderedTest.Count == 0 || !_order.OrderedTest.Exists(o => o.TestId == _testId));

                    if (!proceed || _paramCount == 0)
                    {
                        ((TextBox)sender).Text = "";
                        hdCustID.Value = "";
                        this.isDataEntry = false;
                        return;
                    }
                    this.LabTestId = _testId;
                    this.LabTestName = _testname;
                    this.DepartmentId = Convert.ToInt32(_testDepartmentId);
                    this.Department = _testDepartment;
                    this.TestReferenceId = _refId;
                    this.ParameterCount = _paramCount;
                    this.IsGroup = bool.Parse(_group);
                    this.isDataEntry = true;
                }

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!CurrentSession.Current.Facility.PaperLess)
            {

                string theUrl = string.Format("{0}", "~/Laboratory/LabRecordEntry.aspx");
                //Response.Redirect(theUrl);
                System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Redirect(theUrl, true);

            }
            txtlaborderedbydate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtlaborderedbydate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");

            txtLabtobeDone.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtLabtobeDone.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Laboratory >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Request Form";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Lab Request Form";
            Master.ExecutePatientLevel = true;
            if (Application["AppCurrentDate"] != null)
            {
                hdappcurrentdate.Value = Application["AppCurrentDate"].ToString();
            }
            if (!IsPostBack)
            {
                base.Session["LAB_REQTEST"] = null;
                base.Session["OrderedLabs"] = null;
                this.LabOrderId = Convert.ToInt32(Session["PatientVisitId"]);
                if (LabOrderId > 0)
                {
                    this.PopulateRequest();
                }
                else
                {
                    txtLabtobeDone.Text = txtlaborderedbydate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                }
                this.BindLabTests();
                this.PopulateControls();
            }

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //this.BindLabTests();
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

        private void BindLabTests()
        {
            LabOrder order = this.OrderedLabs;
            List<LabOrderTest> _test = new List<LabOrderTest>();
            if (null != order && null != order.OrderedTest && order.OrderedTest.Count > 0)
            {
                _test = order.OrderedTest.Where(o => o.DeleteFlag == false).ToList();
            }
            else
            {

                _test = new List<LabOrderTest>();
                _test.Add(new LabOrderTest()
                {
                    DeleteFlag = true,
                    Id = -1,
                    TestNotes = "",
                    Test = new LabTest()

                }
                    );

            };
            gridTestRequested.DataSource = _test.Where(o => o.DeleteFlag == false).ToList();
            gridTestRequested.DataBind();
            try { gridTestRequested.HeaderRow.TableSection = TableRowSection.TableHeader; }
            catch { }
        }


        private Boolean FieldValidation(string labtobedone, string orderbydate, string orderby, string appcurrentdate)
        {

            IQCareUtils theUtils = new IQCareUtils();


            if (orderby == "0")
            {
                IQCareMsgBox.NotifyAction("Ordered by is not selected", "Field Validation", true,this, "javascript:HideModalPopup();return false;");
                return false;
            }
            else if ((orderbydate != null && String.IsNullOrEmpty(orderbydate)))
            {
                IQCareMsgBox.NotifyAction("Order date is not specified", "Field Validation", true, this, "javascript:HideModalPopup();return false;");
                return false;
            }
            else if (Convert.ToDateTime(orderbydate) > Convert.ToDateTime(appcurrentdate))
            {
                IQCareMsgBox.NotifyAction("Order date cannot be greater than todays date.", "Field Validation", true, this, "javascript:HideModalPopup();return false;");
                return false;
            }
            else
            {
                DateTime dtRegDate = Convert.ToDateTime(((DataTable)Session["PatientInformation"]).Rows[0]["RegistrationDate"]);
                if (Convert.ToDateTime(orderbydate) < dtRegDate)
                {
                    IQCareMsgBox.NotifyAction("Order date cannot be less than registration date.", "Field Validation", true, this, "javascript:HideModalPopup();return false;");
                    return false;
                }
            }
            if (!(labtobedone != null && String.IsNullOrEmpty(labtobedone)))
            {
                DateTime _theVisitDate = Convert.ToDateTime(theUtils.MakeDate(labtobedone));
                if (HttpContext.Current.Session["IEVisitDate"] != null)//ViewState["IEVisitDate"] != null)
                {
                    DateTime _theIEVisitDate = Convert.ToDateTime(HttpContext.Current.Session["IEVisitDate"].ToString());//Convert.ToDateTime(ViewState["IEVisitDate"].ToString());
                    if (_theIEVisitDate > _theVisitDate)
                    {
                        IQCareMsgBox.NotifyAction("Lab to be done date cannot be less than the Patient Enrollment date.", "Field Validation", true, this, "javascript:HideModalPopup();return false;");
                        return false;
                    }
                }
            }

            return true;
        }

        //private void NotifyAction(string strMessage, string strTitle, bool errorFlag, string onOkScript = "")
        //{
        //    lblNoticeInfo.Text = strMessage;
        //    lblNotice.Text = strTitle;
        //    lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
        //    lblNoticeInfo.Font.Bold = true;
        //    imgNotice.ImageUrl = (errorFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
        //    btnOkAction.OnClientClick = "";
        //    if (!(onOkScript != null && String.IsNullOrEmpty(onOkScript)))
        //    {
        //        btnOkAction.OnClientClick = onOkScript;
        //    }
        //    this.notifyPopupExtender.Show();
        //}

        /// <summary>
        /// Populates the controls.
        /// </summary>
        private void PopulateControls()
        {

            BindUserDropDown(ref ddlaborderedbyname, this.UserId.ToString());
        }
        private void BindUserDropDown(ref DropDownList dropDownList, String userId = "")
        {
            // DataSet theDS = new DataSet();
            userId = userId == "0" ? "" : userId;
            //theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions bindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            //if (theDS.Tables["Mst_Employee"] != null)
            //{
            DataView theDV = new DataView(this.UserList);
            if (this.EmployeeId > 0)
            {
                theDV.RowFilter = "EmployeeId = " + this.EmployeeId;
            }
            else
            {
                theDV.RowFilter = "EmployeeId Is Not Null Or EmployeeId > 0";
            }
            if (theDV.Table != null)
            {
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);

                bindManager.BindCombo(dropDownList, theDT, "Name", "UserId", "", userId);
            }
            //}
        }
        private DataTable UserList
        {
            get
            {
                DataSet theDS = new DataSet();
                theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));

                IQCareUtils theUtils = new IQCareUtils();
                DataTable dt = new DataTable("Users");
                if (theDS.Tables["Users"] != null)
                {
                    DataView theDV = new DataView(theDS.Tables["Users"]);
                    if (theDV.Table != null)
                    {
                        dt = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    }
                }
                return dt;
            }
        }
        private DataTable EmployeeList
        {
            get
            {
                DataSet theDS = new DataSet();
                theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));

                IQCareUtils theUtils = new IQCareUtils();
                DataTable dt = new DataTable("Mst_Employee");
                if (theDS.Tables["Mst_Employee"] != null)
                {
                    DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
                    if (theDV.Table != null)
                    {
                        dt = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    }
                }
                return dt;
            }
        }
     
        /// <summary>
        /// Populates the request.
        /// </summary>
        /// <param name="labVisitId">The lab visit identifier.</param>
        private void PopulateRequest()
        {
            if (LabOrderId > 0) // an existing lab order
            {
                this.OrderedLabs = requestMgr.GetLabOrder(this.LocationId,this.LabOrderId);

                labelClinicalNotes.Text = txtClinicalNotes.Text = this.OrderedLabs.ClinicalNotes;
                labellaborderedbydate.Text = txtlaborderedbydate.Text = this.OrderedLabs.OrderDate.ToString("dd-MMM-yyyy");
                labelOrderStatus.Text = this.OrderedLabs.OrderStatus;
                labelOrderNumber.Text = this.OrderedLabs.OrderNumber;
                DataTable _user = this.UserList;

                DataRow thisRow = _user.AsEnumerable().Where(r => r["UserId"].ToString() == this.OrderedLabs.OrderedBy.ToString()).DefaultIfEmpty(null).FirstOrDefault();
                if (null != thisRow)
                {
                    labelOrderedBy.Text = thisRow["Name"].ToString();
                }
            }

        }

        protected void gridTestRequested_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gridTestRequested_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    string testId = (e.CommandArgument.ToString());
                    LabOrder orders = this.OrderedLabs;

                    LabOrderTest orderedTest = orders.OrderedTest.FirstOrDefault(w => w.TestId.ToString() == testId);
                    if (orderedTest != null)
                    {
                        if (orderedTest.Id > 0)
                        {
                            orders.OrderedTest.FirstOrDefault(w => w.TestId.ToString() == testId).DeleteFlag = true;
                        }
                        else
                        {
                            orders.OrderedTest.Remove(orderedTest);
                        }
                    }
                    this.OrderedLabs = orders;
                    this.BindLabTests();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }

    }


}
