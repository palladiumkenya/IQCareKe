using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Application.Presentation;
using Entities.Lab;
using Interface.Laboratory;
using Interface.Security;
using Telerik.Web.UI;
using IQCare.Web.UILogic;

namespace IQCare.Web.Laboratory
{
    public partial class LabRecordEntry : System.Web.UI.Page
    {
        private ILabRequest requestMgr = (ILabRequest)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");
        private ILabManager labMgr = (ILabManager)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabManager, BusinessProcess.Laboratory");
        private string RedirectUrl = "../ClinicalForms/frmPatient_Home.aspx";

        protected string sOption
        {
            get
            {
                return hdDataType.Value == "SELECTLIST" ? "" : "none";
            }
        }

        protected string sText
        {
            get
            {
                return hdDataType.Value == "TEXT" ? "" : "none";
            }
        }

        protected string sNumeric
        {
            get
            {
                return hdDataType.Value == "NUMERIC" ? "" : "none";
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
        private bool isError = false;
        private bool isDataEntry = false;

        protected string ShowTextDiv(object datatype)
        {
            return (datatype.ToString().ToUpper() == "TEXT") ? "" : "none";
        }

        protected string ShowSelectDiv(object datatype)
        {
            return (datatype.ToString().ToUpper() == "SELECTLIST") ? "" : "none";
        }

        protected string ShowNumDiv(object datatype)
        {
            return (datatype.ToString().ToUpper() == "NUMERIC") ? "" : "none";
        }

        protected string ShowTextResult(object datatype, object resultText)
        {
            return ((datatype.ToString().ToUpper() == "TEXT" || datatype.ToString().ToUpper() == "SELECT LIST") && resultText.ToString().Trim() != "") ? "" : "none";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["Paperless"].ToString() == "1")
            //{
               
            //        string theUrl = string.Format("{0}", "~/Laboratory/LabRequestForm.aspx");
            //        //Response.Redirect(theUrl);
            //        System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
            //        Response.Redirect(theUrl, true);
                
            //}
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
                base.Session["OrderedLabs"] = null;
                base.Session["LAB_REQTEST"] = null;
                this.PopulateControls();
                this.BindTestParameterResults();
                this.BindLabTest();
                txtlaborderedbydate.Text = txtlabReportedbyDate.Text = txtLabtobeDone.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            else
            {
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.BindTestParameterResults();
            divError.Visible = isError;
            
        }
        decimal? nullDecimal = null;
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
                IsGroup = false,
                DeleteFlag = false,
                UserId = this.UserId

            };
            // List<ParameterResultConfig) _params  thisTest.ParameterResults
            foreach (RepeaterItem dataItem in repeaterResult.Items)
            {
                string _dataType = "";


                bool hasResult = false;
                int paramId;

                HiddenField hdataType = dataItem.FindControl("HResultDataType") as HiddenField;
                string resultId = (dataItem.FindControl("HResultId") as HiddenField).Value;
                _dataType = hdataType.Value.ToUpper();
                paramId = Convert.ToInt32((dataItem.FindControl("hParameterId") as HiddenField).Value);
                string testOrderId = (dataItem.FindControl("hTestOrderId") as HiddenField).Value;

                RadComboBox ddlResultUnit = dataItem.FindControl("ddlResultUnit") as RadComboBox;
                DropDownList ddlResultList = dataItem.FindControl("ddlResultList") as DropDownList;
                TextBox txtResultText = dataItem.FindControl("textResultText") as TextBox;
                Label labelResultText = dataItem.FindControl("labelResultText") as Label;
                string parameterName = (dataItem.FindControl("labelParameterName") as Label).Text;
                CheckBox cBox = dataItem.FindControl("checkUndetectable") as CheckBox;
                TextBox txtLimit = dataItem.FindControl("textDetectionLimit") as TextBox;
                TextBox txtResultValue = dataItem.FindControl("textResultValue") as TextBox;

                hasResult = txtResultValue.Text.Trim() != "" || (ddlResultList.SelectedValue!="" && Convert.ToInt32(ddlResultList.SelectedValue) > 0) || txtResultText.Text.Trim() != "";
                if (!hasResult)
                {
                    this.isDataEntry = true;
                    continue;

                }


                LabTestParameterResult _result = new LabTestParameterResult()
                {
                    LabOrderId = this.LabOrderId,
                    LabOrderTestId = this.LabOrderTestId,
                    UserId = this.UserId,
                    DeleteFlag = false
                    
                };
                _result.Parameter = new TestParameter()
                {
                    Id = paramId,
                    Name = parameterName,
                    DataType = _dataType,
                    LabTestId = this.LabTestId,
                    DeleteFlag = false
                };
                                if (_dataType == "NUMERIC")
                {

                    _result.ResultValue = string.IsNullOrEmpty(txtResultValue.Text) ? nullDecimal : Convert.ToDecimal(txtResultValue.Text.Trim());
                    _result.Undetectable = cBox.Checked;
                    _result.DetectionLimit = string.IsNullOrEmpty(txtLimit.Text) ? nullDecimal : Convert.ToDecimal(txtLimit.Text.Trim());
                    _result.ResultUnit = new ResultUnit() { Id = Convert.ToInt32(ddlResultUnit.SelectedValue), Text = ddlResultUnit.SelectedItem.Text };

                    RadComboBoxItem item = ddlResultUnit.SelectedItem;
                    try
                    {
                        string min_value = item.Attributes["min"].ToString();
                        string max_value = item.Attributes["max"].ToString();
                        string min_normal = item.Attributes["min_normal"].ToString();
                        string max_normal = item.Attributes["max_normal"].ToString();
                        string detection_limit = item.Attributes["detection_limit"].ToString();
                        _result.DetectionLimit = string.IsNullOrEmpty(txtLimit.Text) ? Convert.ToDecimal(detection_limit) : Convert.ToDecimal(txtLimit.Text.Trim());
                        string config_id = item.Attributes["config_id"].ToString();
                        _result.Config = new ParameterResultConfig()
                        {
                            Id = Convert.ToInt32(config_id),
                            DetectionLimit = Convert.ToDecimal(detection_limit),
                            MinBoundary = Convert.ToDecimal(min_value),
                            MaxBoundary = Convert.ToDecimal(max_value),
                            MinNormalRange = Convert.ToDecimal(min_normal),
                            MaxNormalRange = Convert.ToDecimal(max_normal)
                        };
                        
                    }
                    catch { }

                }
                else if (_dataType == "TEXT")
                {
                    _result.ResultText = txtResultText.Text.Trim();
                }
                else if (_dataType == "SELECTLIST")
                {
                    _result.ResultOptionId = Convert.ToInt32(ddlResultList.SelectedValue);
                    _result.ResultOption = ddlResultList.SelectedItem.Text;
                }
                if (null != thisTest.ParameterResults)
                {
                    thisTest.ParameterResults.Add(_result);
                }
                else
                {
                    thisTest.ParameterResults = new List<LabTestParameterResult>();
                    thisTest.ParameterResults.Add(_result);
                }
            }
            thisTest.TestNotes = txtTestNotes.Text;
            _order.OrderedTest.Add(thisTest);
            this.OrderedLabs = _order;
            hdCustID.Value = textSelectLab.Text = txtTestNotes.Text = "";
            this.isDataEntry = false;
            //base.Session["LAB_REQTEST"] = null;
            //base.Session["OrderedLabs"] = null;
            this.BindLabTest();
        }        
        private bool FieldValidation(string labtobedone, string orderbydate, string orderby, string appcurrentdate,string resultdate, string resultBy)
        {          
            DateTime _theCurrentDate = SystemSetting.SystemDate;           
            IQCareUtils theUtils = new IQCareUtils();

            Page page = HttpContext.Current.Handler as Page;
            if (orderby == "0")
            {
                IQCareMsgBox.NotifyAction("Ordered By is not selected", "Field Validation", true,this, "javascript:HideModalPopup();return false;");
                return false;
            }
            else if (orderbydate == "")
            {
                IQCareMsgBox.NotifyAction("Ordered By Date is not specified", "Field Validation", true, this, "javascript:HideModalPopup();return false;");
                return false;

            }
            else if (Convert.ToDateTime(orderbydate) > Convert.ToDateTime(appcurrentdate))
            {

                IQCareMsgBox.NotifyAction("Ordered To date cannot be greater than todays date.", "Field Validation", true, this, "javascript:HideModalPopup();return false;");
                return false;
            }
            else if (resultBy == "0" || resultdate == "")
            {
                IQCareMsgBox.NotifyAction("Reported By Date and Resported By are required", "Field Validation", true, this, "javascript:HideModalPopup();return false;");
                return false;
            }
            else if (Convert.ToDateTime(resultdate) > Convert.ToDateTime(appcurrentdate) || Convert.ToDateTime(resultdate) < Convert.ToDateTime(orderbydate))
            {

                IQCareMsgBox.NotifyAction("Reported By Date cannot be greater than todays date or the Order By Date.", "Field Validation", true, this, "javascript:HideModalPopup();return false;");
                return false;
            }
            if (labtobedone != "")
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string labtobedone = txtLabtobeDone.Text;
            string laborder = ddlaborderedbyname.SelectedValue;
            string laborderdate = txtlaborderedbydate.Text;
            string laborderResultBy = ddlLabReportedbyName.SelectedValue;
            string laborderResultdate = txtlabReportedbyDate.Text;
            string appcurrdate = hdappcurrentdate.Value;
            string strClinicalNotes = txtClinicalNotes.Text;
            if (FieldValidation(labtobedone, laborderdate, laborder, appcurrdate,laborderResultdate, laborderResultBy) == false)
            {
                return;
            }
            ILabRequest requestMgr = (ILabRequest)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");
            if (LabOrderId > 0) //update
            {
                //
            }
            else
            {
                DateTime? nullDatetime = null;

                LabOrder order = this.OrderedLabs;
                order.LocationId = this.LocationId;
                order.PatientId = this.PatientId;
                order.ModuleId = this.ModuleId;
                order.DeleteFlag = false;
                order.CreateDate = DateTime.Now;
                order.PreClinicDate = labtobedone == "" ? nullDatetime : Convert.ToDateTime(labtobedone);
                order.OrderDate = Convert.ToDateTime(laborderdate);
                order.OrderedBy = Convert.ToInt32(ddlaborderedbyname.SelectedValue);
                order.ClinicalNotes = strClinicalNotes;
                order.UserId = this.UserId;

                order.OrderedTest.ForEach(o=> 
                {
                    o.ResultBy = Convert.ToInt32(ddlLabReportedbyName.SelectedValue);
                    o.ResultDate = Convert.ToDateTime(txtlabReportedbyDate.Text);
                    //o.ParameterResults.ForEach(p =>
                    //{
                       
                    //});
                });


                LabOrder _saveOrder = requestMgr.SaveLabOrder(order,this.UserId,this.LocationId);
                IQCareMsgBox.NotifyAction(string.Format("Lab Order number {0}, saved successfully", _saveOrder.OrderNumber), "Lab Order", false,this,
                   string.Format("javascript:window.location='{0}'; return false;", this.RedirectUrl));
            }
        }

        protected void ExitPage(object sender, EventArgs e)
        {
            hdCustID.Value = textSelectLab.Text = txtTestNotes.Text = "";
            this.isDataEntry = false;
            base.Session["LAB_REQTEST"] = null;
            base.Session["OrderedLabs"] = null;
            Response.Redirect(RedirectUrl);
        }

        protected void CancelLabEntry(object sender, EventArgs e)
        {

            hdCustID.Value = textSelectLab.Text = txtTestNotes.Text = "";
            this.isDataEntry = false;
            base.Session["LAB_REQTEST"] = null;
            base.Session["OrderedLabs"] = null;
        }
            

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

        private int LabOrderId
        {
            get
            {
                int val = int.Parse(HLabOrderId.Value);

                return val;
            }
             set
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
        private int LocationId
        {
            get
            {
                return Convert.ToInt32(Session["AppLocationId"]);
            }
        }

        /// <summary>
        /// The patient identifier
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        private int PatientId
        {
            get
            {
                return Convert.ToInt32(Session["PatientId"].ToString());
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

        private int ModuleId { get { return Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]); } }

        private LabOrder OrderedLabs
        {
            get
            {
                if (base.Session["OrderedLabs"] == null)
                {
                    
                    return new LabOrder()
                    {
                        PatientId = this.PatientId,
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

        protected void LabNameChanged(object sender, EventArgs e)
        {
            //  textSelectLab.Text = "";
            //   DataSet ds = this.RequestedTests;
            //DataTable dt = ds.Tables["LabTest"];

            LabOrder _order = this.OrderedLabs;

            int _testId;
            string _testname = "";
            string _refId = "";
            string _testDepartmentId = "";
            string _testDepartment = "";
            string _group = "";
            this.isDataEntry = false;
            int _paramCount;
            if (hdCustID.Value != "")
            {
                String[] itemCodes = hdCustID.Value.Split(';');
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

                    //DataRow thisRow = null;
                    //if (dt.Rows.Count > 0)
                    //{
                    //    thisRow = dt.AsEnumerable()
                    //       .DefaultIfEmpty(null)
                    //       .FirstOrDefault(r => r["TestId"].ToString() == _testId.ToString());
                    //}
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
                   
                    List<TestParameter> parameters = labMgr.GetLabTestParameters(_testId);

                    DataTable thisTest = this.SelectedTestParameters;
                    thisTest.Rows.Clear();
                    parameters.ForEach(p =>
                    {
                        DataRow paramRow = thisTest.NewRow();
                        paramRow.SetField("TestOrderId", -1);
                        paramRow.SetField("ResultId", -1);
                        paramRow.SetField("LabOrderId", -1);
                        paramRow.SetField("TestId", _testId);
                        paramRow.SetField("ParameterId", p.Id);
                        paramRow.SetField("UserId", this.UserId);
                        paramRow.SetField("ResultDate", DBNull.Value);
                        paramRow.SetField("ParameterName", p.Name);
                        paramRow.SetField("TestName", _testname);
                        paramRow.SetField("DataType", p.DataType);
                        thisTest.Rows.Add(paramRow);
                    });
                    thisTest.AcceptChanges();
                    this.SelectedTestParameters = thisTest;                   
                    this.isDataEntry = true;
                    this.BindTestParameterResults();
                }

            }
        }
        private void BindLabTest()
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

        }
        private void BindTestParameterResults()
        {
            DataView dv = this.SelectedTestParameters.DefaultView;
            dv.RowFilter = "DeleteFlag  ='False'";
            DataTable theDT = dv.ToTable();
            repeaterResult.DataSource = theDT;
            repeaterResult.DataBind();
            
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
            List<string> Labdetail = new List<string>();
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

        private DataTable SelectedTestParameters
        {
            get
            {
                if (base.Session["LAB_REQTEST"] == null)
                {
                    DataTable dtResult = new DataTable("Parameters");
                    dtResult.Columns.Add("TestOrderId", System.Type.GetType("System.Int32"));
                    dtResult.Columns.Add("ResultId", System.Type.GetType("System.Int32"));
                    dtResult.Columns.Add("LabOrderId", System.Type.GetType("System.Int32"));
                    dtResult.Columns.Add("TestId", System.Type.GetType("System.Int32"));
                    dtResult.Columns.Add("ParameterId", System.Type.GetType("System.Int32"));
                    dtResult.Columns.Add("UserId", System.Type.GetType("System.Int32"));
                    dtResult.Columns.Add("ResultDate", System.Type.GetType("System.DateTime"));
                    dtResult.Columns.Add("ParameterName", System.Type.GetType("System.String"));
                    dtResult.Columns.Add("TestName", System.Type.GetType("System.String"));
                    dtResult.Columns.Add("DataType", System.Type.GetType("System.String"));
                    dtResult.Columns.Add("ResultValue", System.Type.GetType("System.Decimal"));
                    dtResult.Columns.Add("UnDetectable", System.Type.GetType("System.Boolean"));
                    dtResult.Columns["UnDetectable"].DefaultValue = false;
                    dtResult.Columns.Add("DetectionLimit", System.Type.GetType("System.Decimal"));
                    dtResult.Columns.Add("ResultText", System.Type.GetType("System.String"));
                    dtResult.Columns.Add("ResultOption", System.Type.GetType("System.String"));
                    dtResult.Columns.Add("ResultUnit", System.Type.GetType("System.String"));
                    dtResult.Columns.Add("Persisted", System.Type.GetType("System.Boolean"));
                    dtResult.Columns["Persisted"].DefaultValue = false;
                    dtResult.Columns.Add("DeleteFlag", System.Type.GetType("System.Boolean"));
                    dtResult.Columns["DeleteFlag"].DefaultValue = false;
                    return dtResult;
                }
                else
                {
                    return (DataTable)base.Session["LAB_REQTEST"];
                }
            }
            set
            {
                base.Session["LAB_REQTEST"] = value;
            }
        }

       

        private void InjectScript(ref CheckBox cBox, ref TextBox txtBox)
        {
            string checkUndectable = cBox.ClientID;
            string detectionLimit = txtBox.ClientID;
            string script = @"$(function () {$(""#" + checkUndectable + @""").click(function () {
             $(""#" + detectionLimit + @""").val("""");
            if ($(this).is("":checked"")) {
                $(""#" + detectionLimit + @""").removeAttr(""disabled"");
                $(""#" + detectionLimit + @""").focus();
            } else {
                $(""#" + detectionLimit + @""").attr(""disabled"", ""disabled"");
            }
        }); });";
            ScriptManager.RegisterStartupScript(cBox, cBox.GetType(), checkUndectable, script, true);
        }

        private void PopulateUnits(ref RadComboBox ddlControl, ref TextBox textLimit, int parameterid)
        {
            ILabManager labMgr = (ILabManager)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabManager, BusinessProcess.Laboratory");
            List<ParameterResultConfig> config = labMgr.GetParameterConfig(parameterid);
            //  DataTable theDT = labMgr.GetTestParameterResultUnit(parameterid);
            if (config != null)
            {
                ddlControl.Items.Clear();
                ddlControl.ClearSelection();
              //  ddlControl.Items.Add(new RadComboBoxItem())
                if (config.Count > 1)
                {
                    ddlControl.Items.Add(new RadComboBoxItem("Select...", "-1"));
                }
                //  theDT.DefaultView.Sort = "UnitName Asc";
                //  DataTable dt = theDT.DefaultView.ToTable();
                string strDefaultId = "";
                foreach (ParameterResultConfig row in config)
                {
                    RadComboBoxItem item = (new RadComboBoxItem(row.UnitName, row.UnitId.ToString()));
                    if (Convert.ToBoolean(row.IsDefault))
                    {
                        strDefaultId = row.UnitId.ToString();
                    }
                    item.Attributes.Add("is_default", row.IsDefault.ToString().ToLower());
                    item.Attributes.Add("min", row.MinBoundary.ToString());
                    item.Attributes.Add("max", row.MaxBoundary.ToString());
                    item.Attributes.Add("min_normal", row.MinNormalRange.ToString());
                    item.Attributes.Add("max_normal", row.MaxNormalRange.ToString());
                    item.Attributes.Add("detection_limit", row.DetectionLimit.ToString());
                    item.Attributes.Add("config_id", row.Id.ToString());
                    ddlControl.Items.Add(item);
                }
                if (strDefaultId != "")
                {
                    RadComboBoxItem item = ddlControl.Items.FindItemByValue(strDefaultId);//.FindByValue(strDefaultId);
                    if (null != item)
                    {
                        item.Selected = true;
                        textLimit.Text = item.Attributes["detection_limit"];
                    }
                }
            }
        }

        private void PopulateSelectList(ref DropDownList ddlControl, int parameterId)
        {
            ILabManager labMgr = (ILabManager)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabManager, BusinessProcess.Laboratory");

            List<ParameterResultOption> options = labMgr.GetParameterResultOption(parameterId);
            if (options != null)
            {
                options.OrderBy(o => o.Text);
                ddlControl.Items.Clear();
                ddlControl.ClearSelection();
                ddlControl.Items.Add(new ListItem("Select...", "-1"));
                // theDT.DefaultView.Sort = "Value Asc";
                // DataTable dt = theDT.DefaultView.ToTable();
                foreach (ParameterResultOption option in options)
                {
                    ListItem item = (new ListItem(option.Text, option.Id.ToString()));
                    ddlControl.Items.Add(item);
                }
            }
        }
        private void AddValidation(ref Control cntrl, String itemName, String min, String max)
        {
            TextBox txtcntrl = cntrl as TextBox;
            if (txtcntrl != null)
            {
                if (max.Trim() != "" && min.Trim() != "")
                {
                    txtcntrl.Attributes.Add("onblur", "isBetween('" + txtcntrl.ClientID + "', '" + itemName + "','" + min + "', '" + max + "')");
                    txtcntrl.Attributes.Add("onkeyup", "chkDecimal('" + txtcntrl.ClientID + "')");
                    txtcntrl.Attributes.Add("data-resultType", "num");
                }
                else if (max.Trim() != "")
                {
                    txtcntrl.Attributes.Add("onblur", "checkMax('" + txtcntrl.ClientID + "', '" + itemName + "', '" + max + "')");
                    txtcntrl.Attributes.Add("onkeyup", "chkDecimal('" + txtcntrl.ClientID + "')");
                    txtcntrl.Attributes.Add("data-resultType", "num");
                }
                else if (min.Trim() != "")
                {
                    txtcntrl.Attributes.Add("onblur", "checkMin('" + txtcntrl.ClientID + "', '" + itemName + "', '" + min + "')");
                    txtcntrl.Attributes.Add("onkeyup", "chkDecimal('" + txtcntrl.ClientID + "')");
                    txtcntrl.Attributes.Add("data-resultType", "num");
                }
                

            }
        }
        protected void repeaterResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                string strDataType = rowView["DataType"].ToString().ToUpper();
                string strResultValue = rowView["ResultValue"].ToString();
                string strResultText = rowView["ResultText"].ToString();
                string strLimit = rowView["DetectionLimit"].ToString();
                string strParameterId = rowView["ParameterId"].ToString();
                bool hasResult = (strResultText != "" || strResultValue != "");
                FilteredTextBoxExtender fteLimit = e.Item.FindControl("fteLimit") as FilteredTextBoxExtender;
                FilteredTextBoxExtender fteValue = e.Item.FindControl("fteValue") as FilteredTextBoxExtender;
                RadComboBox ddlResultUnit = e.Item.FindControl("ddlResultUnit") as RadComboBox;
                DropDownList ddlResultList = e.Item.FindControl("ddlResultList") as DropDownList;
                TextBox txtResultText = e.Item.FindControl("textResultText") as TextBox;
                //Label labelResultText = e.Item.FindControl("labelResultText") as Label;
                CheckBox cBox = e.Item.FindControl("checkUndetectable") as CheckBox;
                TextBox txtLimit = e.Item.FindControl("textDetectionLimit") as TextBox;
                TextBox txtResultValue = e.Item.FindControl("textResultValue") as TextBox;

                fteLimit.Enabled = fteValue.Enabled =
                    cBox.Enabled = ddlResultUnit.Enabled =
                      txtResultValue.Enabled = strDataType == "NUMERIC";  
                if (strDataType == "NUMERIC")
                {
                    txtLimit.Attributes.Add("disabled", "disabled");
                  
                    if (null != cBox)
                    {
                        this.InjectScript(ref cBox, ref txtLimit);
                    }
                    
                    this.PopulateUnits(ref ddlResultUnit, ref txtLimit, int.Parse(strParameterId));
                
                }
                
                else if (strDataType == "SELECTLIST")
                {
                    this.PopulateSelectList(ref ddlResultList, int.Parse(strParameterId));
                    
                }
            }
        }

        protected void btnOkAction_Click(object sender, EventArgs e)
        {

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
        
        protected void ddlaborderedbyname_DataBound(object sender, EventArgs e)
        {

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
      
        private void BindUserDropDown(ref DropDownList dropDownList, String userId = "")
        {
            //DataSet theDS = new DataSet();
            userId = userId == "0" ? "" : userId;
            //theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            //if (theDS.Tables["Mst_Employee"] != null)
            //{
            DataView theDV = new DataView(this.UserList);
            //if (this.EmployeeId > 0)
            //{
            //    theDV.RowFilter = "EmployeeId = " + this.EmployeeId;
            //}
            //else
            //{
            theDV.RowFilter = "EmployeeId Is Not Null Or EmployeeId > 0";
            //}
            if (theDV.Table != null)
            {
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);

                BindManager.BindCombo(dropDownList, theDT, "Name", "UserId", "", userId);

            }
        }
        private int EmployeeId
        {
            get
            {
                return Convert.ToInt32(Session["AppUserEmployeeId"].ToString());
            }
        }
        private DataTable PatientInfo
        {
            get
            {
                return (DataTable)Session["PatientInformation"];
            }
        }
        void PopulateControls()
        {


            BindUserDropDown(ref ddlaborderedbyname, this.UserId.ToString());
            BindUserDropDown(ref ddlLabReportedbyName, "");

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
                    this.BindLabTest();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }

        protected void gridTestRequested_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }
    }

}