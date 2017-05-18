using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Application.Presentation;
using Entities.Lab;
using Interface.Laboratory;
using IQCare.Web.UILogic;

namespace IQCare.Web.Laboratory
{
    public partial class EnterResultPage : Page
    {
        /// <summary>
        /// The redirect URL
        /// </summary>
         private ILabRequest requestMgr = (ILabRequest)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");
     
        private string RedirectUrl {
            get
            {
                Guid g = Guid.NewGuid();
                return "../Laboratory/LabResultPage.aspx?key="+g.ToString();
            }
        }
        private bool IsPaperless
        {
            get
            {
                return (Session["Paperless"].ToString() == "1");
            }
        }
        private void BindDropdownResultBy()
        {


            string userId = "";

            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();

            DataView theDV = new DataView(this.UserList);

            string rowFilter = "(EmployeeId Is Not Null Or EmployeeId > 0) And UserDeleteFlag = 0 And EmployeeDeleteFlag = 0";

            if (IsPaperless && this.EmployeeId > 0)
            {
                userId = this.UserId.ToString();
                rowFilter = "UserId = " + userId;
            }

            theDV.RowFilter = rowFilter;
            if (theDV.Table != null)
            {
                DataTable theDT = theUtils.CreateTableFromDataView(theDV);

                BindManager.BindCombo(ddlLabReportedbyName, theDT, "Name", "UserId", "", userId);
                ListItem item = ddlLabReportedbyName.Items.FindByValue(userId);
                if (item == null)
                {
                    item = ddlLabReportedbyName.Items.FindByValue(this.UserId.ToString());
                }
                if (item != null)
                {
                    item.Selected = true;
                }
            }


        }
        protected string ColCount(object datatype)
        {
            return (datatype.ToString().ToUpper() == "NUMERIC") ? "3" : "9";
        }
        protected string ShowTextDiv(object datatype)
        {
            return (datatype.ToString().ToUpper() == "TEXT") ? "" : "none";
        }

        protected string ShowSelectDiv(object datatype)
        {
            return (datatype.ToString().ToUpper() == "SELECTLIST") ? "" : "none";
        }
        protected string showNotes = "none";
        protected string ShowNumDiv(object datatype)
        {
            return (datatype.ToString().ToUpper() == "NUMERIC") ? "" : "none";
        }

        protected string ShowTextResult(object datatype, object resultText)
        {
            return ((datatype.ToString().ToUpper() == "TEXT" || datatype.ToString().ToUpper() == "SELECTLIST") && resultText.ToString().Trim() != "") ? "" : "none";
        }
       
        private bool isError = false;


     
        int UserId
        {
            get
            {
                return Convert.ToInt32(base.Session["AppUserId"]);
            }
        }
        private int EmployeeId
        {
            get
            {
                return Convert.ToInt32(Session["AppUserEmployeeId"].ToString());
            }
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
                        dt = theUtils.CreateTableFromDataView(theDV);
                    }
                }
                return dt;
            }
        }

        int LabTestId
        {
            get
            {
                return Convert.ToInt32(hLabTestId.Value);
            }
            set
            {
                hLabTestId.Value = value.ToString();
            }
        }
        int LabOrderTestId
        {
            get
            {
                return Convert.ToInt32(HLabOrderTestId.Value);
            }
            set
            {
                HLabOrderTestId.Value = value.ToString();
            }
        }
        int LabOrderId
        {
            get
            {
                return Convert.ToInt32(HLabOrderId.Value);
            }
            set
            {
                HLabOrderId.Value = value.ToString();
            }
        }
        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
            Exception ex = Server.GetLastError();
            this.ShowErrorMessage(ref ex);
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
                SystemSetting.LogError(ex);
                
            }
        }
     
        protected void Page_PreRender(object sender, EventArgs e)
        {

            divError.Visible = isError;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Services >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Result Page";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Service Orders Result Page";
            Master.ExecutePatientLevel = true;
            if (Application["AppCurrentDate"] != null)
            {
                hdappcurrentdate.Value = Application["AppCurrentDate"].ToString();
            }
            if (!IsPostBack)
            {
                if (Session[SessionKey.SelectedLabTestOrder] == null)
                {
                    string theUrl = string.Format("{0}", "~/Laboratory/LabRequestForm.aspx");
                    //Response.Redirect(theUrl);
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(theUrl, true);
                }
                else
                {
                    LabOrderTest selectedLab = (LabOrderTest)Session[SessionKey.SelectedLabTestOrder];
                 
                    this.LabTestId = selectedLab.Test.Id;
                    this.LabOrderTestId = selectedLab.Id;
                   // this.LabOrderTestId = selectedLab.LabOrderId;
                    this.PopulateLabDetails(selectedLab);
                    this.BindDropdownResultBy() ;
                     txtlabReportedbyDate.Text =  DateTime.Now.ToString("dd-MMM-yyyy");
                }
                
            }
        }
       
        string GetUserFullName(int userId)
        {
            DataTable _user = this.UserList;
            DataRow thisRow = _user.AsEnumerable().Where(r => r["UserId"].ToString() == userId.ToString()).DefaultIfEmpty(null).FirstOrDefault();
            if (null != thisRow)
            {
                return thisRow["Name"].ToString();
            }
            return "";
        }
        private void PopulateLabDetails(LabOrderTest selectedLab)
        {
            try
            {
              List<LabOrderTest> orderTests =   requestMgr.GetOrderedTests(selectedLab.LabOrderId, selectedLab.Id);
              thisTestOrder = orderTests.FirstOrDefault();
                labelOrderNumber.Text = string.Format("Result for {0} ordered on {1:dd-MMM-yyyy} by {2} . Order number {3}", 
                    thisTestOrder.TestName, 
                    thisTestOrder.OrderDate,
                    this.GetUserFullName(thisTestOrder.OrderedBy),
                    thisTestOrder.OrderNumber);
                labelTestOrderStatus.Text = thisTestOrder.TestOrderStatus;
                labelTestNotes.Text = thisTestOrder.TestNotes;
                showNotes = !string.IsNullOrEmpty(thisTestOrder.TestNotes.Trim())? "" : "none";
                this.LabTestId = thisTestOrder.Test.Id;
                this.LabOrderTestId = thisTestOrder.Id;
                //this.LabOrderTestId = thisTestOrder.LabOrderId;
                this.BindTests();               
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }
        LabOrderTest thisTestOrder
        {
            get
            {
                return (base.Session["thisTestOrder"] == null) ? new LabOrderTest() : (LabOrderTest)base.Session["thisTestOrder"];
            }
            set
            {
                base.Session["thisTestOrder"] = value;
            }
        }
        void BindTests()
        {
            List<LabTestParameterResult> paramResults = new List<LabTestParameterResult>();
            LabOrderTest order = this.thisTestOrder;
            if (null != order && null != order.ParameterResults && order.ParameterResults.Count > 0)
            {
                paramResults = order.ParameterResults.Where(o => o.DeleteFlag == false).ToList();
            }
            else
            {

                paramResults = new List<LabTestParameterResult>();
                paramResults.Add(new LabTestParameterResult()
                {
                    DeleteFlag = true,
                    Id = -1,
                  Parameter = new TestParameter()
                }
                );

            };
            repeaterResult.DataSource = paramResults.Where(o => o.DeleteFlag == false);
            repeaterResult.DataBind();
        }
        private bool FieldValidation(string appcurrentdate, string resultdate, string resultBy)
        {

            
            if (resultBy == "0" || resultdate == "")
            {
                IQCareMsgBox.NotifyAction("Reported By Date and Resported By are required", "Field Validation", true,this, "javascript:HideModalPopup();return false;");
                return false;
            }
            else if (Convert.ToDateTime(resultdate) > Convert.ToDateTime(appcurrentdate) || Convert.ToDateTime(resultdate) < Convert.ToDateTime(thisTestOrder.OrderDate))
            {

                IQCareMsgBox.NotifyAction("Reported By Date cannot be greater than todays date or the Order By Date.", "Field Validation", true,this, "javascript:HideModalPopup();return false;");
                return false;
            }
           

            return true;
        }
        decimal? nullDecimal = null;
        protected void SaveResults(object sender, EventArgs e)
        {
            

         
            LabOrderTest thisTest = this.thisTestOrder;
            List<LabTestParameterResult> paramToSave = new List<LabTestParameterResult>(); 
           // testToSave.ParameterResults = new List<LabTestParameterResult>();
            string laborderResultBy = ddlLabReportedbyName.SelectedValue;
            string laborderResultdate = txtlabReportedbyDate.Text;
            string appcurrdate = hdappcurrentdate.Value;
            if (!this.FieldValidation(appcurrdate, laborderResultdate, laborderResultBy))
            {
                return;
            }
            int withResult = 0;
            
            foreach (RepeaterItem dataItem in repeaterResult.Items)
            {
                string _dataType = "";


                bool hasResult = false;
                int paramId;

                HiddenField hdataType = dataItem.FindControl("HResultDataType") as HiddenField;
                int resultId = Convert.ToInt32((dataItem.FindControl("HResultId") as HiddenField).Value);
                _dataType = hdataType.Value.ToUpper();
                paramId = Convert.ToInt32((dataItem.FindControl("hParameterId") as HiddenField).Value);
                string testOrderId = (dataItem.FindControl("hTestOrderId") as HiddenField).Value;

                DropDownList ddlResultUnit = dataItem.FindControl("ddlResultUnit") as DropDownList;
                DropDownList ddlResultList = dataItem.FindControl("ddlResultList") as DropDownList;
                TextBox txtResultText = dataItem.FindControl("textResultText") as TextBox;
                Label labelResultText = dataItem.FindControl("labelResultText") as Label;
               // string parameterName = (dataItem.FindControl("labelParameterName") as Label).Text;
                CheckBox cBox = dataItem.FindControl("checkUndetectable") as CheckBox;
                TextBox txtLimit = dataItem.FindControl("textDetectionLimit") as TextBox;
                TextBox txtResultValue = dataItem.FindControl("textResultValue") as TextBox;
                switch (_dataType)
                {
                    case "NUMERIC":
                        hasResult = txtResultValue.Text.Trim() != "" || cBox.Checked;
                        break;
                    case "SELECTLIST":
                        try { hasResult = Convert.ToInt32(ddlResultList.SelectedValue) > 0; }
                        catch { }
                        break;
                    case"TEXT":
                        hasResult = txtResultText.Text.Trim() != "";
                        break;
                }
              //  hasResult = txtResultValue.Text.Trim() != "" || Convert.ToInt32(ddlResultList.SelectedValue) > 0 || txtResultText.Text.Trim() != "";
                LabTestParameterResult thisParam = thisTest.ParameterResults.Where(pr => pr.Id == resultId).DefaultIfEmpty(null).FirstOrDefault();
                if (!hasResult || null == thisParam)
                {
                    //isDataEntry = true;
                    continue;

                }
                withResult += 1;
                if (_dataType == "NUMERIC")
                {

                    thisParam.ResultValue = string.IsNullOrEmpty(txtResultValue.Text) ? nullDecimal : Convert.ToDecimal(txtResultValue.Text.Trim());
                    thisParam.Undetectable = cBox.Checked;
                    thisParam.DetectionLimit = string.IsNullOrEmpty(txtLimit.Text) ? nullDecimal : Convert.ToDecimal(txtLimit.Text.Trim());
                    try
                    {
                        thisParam.ResultUnit = null;
                        if (ddlResultUnit.SelectedIndex > -1 && ddlResultUnit.SelectedValue != "")
                        {
                            thisParam.ResultUnit = new ResultUnit() { Id = Convert.ToInt32(ddlResultUnit.SelectedValue), Text = ddlResultUnit.SelectedItem.Text };
                        }
                        ListItem item = ddlResultUnit.SelectedItem;
                        string min_value = item.Attributes["min"].ToString();
                        string max_value = item.Attributes["max"].ToString();
                        string min_normal = item.Attributes["min_normal"].ToString();
                        string max_normal = item.Attributes["max_normal"].ToString();
                        string detection_limit = item.Attributes["detection_limit"].ToString();
                        thisParam.DetectionLimit = string.IsNullOrEmpty(txtLimit.Text) ? Convert.ToDecimal(detection_limit) : Convert.ToDecimal(txtLimit.Text.Trim());
                        string config_id = item.Attributes["config_id"].ToString();
                        thisParam.Config = new ParameterResultConfig()
                        {
                            Id = Convert.ToInt32(config_id),
                            DetectionLimit = detection_limit == "" ? nullDecimal : Convert.ToDecimal(detection_limit),
                            MinBoundary = min_value == "" ? nullDecimal : Convert.ToDecimal(min_value),
                            MaxBoundary = max_value == "" ? nullDecimal : Convert.ToDecimal(max_value),
                            MinNormalRange = min_normal == "" ? nullDecimal : Convert.ToDecimal(min_normal),
                            MaxNormalRange = max_normal == "" ? nullDecimal : Convert.ToDecimal(max_normal)
                        };
                    }
                    catch { thisParam.ResultUnit = null; }
                }
                else if (_dataType == "TEXT")
                {
                    thisParam.ResultText = txtResultText.Text.Trim();
                }
                else if (_dataType == "SELECTLIST")
                {
                    thisParam.ResultOptionId = Convert.ToInt32(ddlResultList.SelectedValue);
                    thisParam.ResultOption = ddlResultList.SelectedItem.Text;
                }
                paramToSave.Add(thisParam);
                
            }
            if (withResult > 0)
            {
                requestMgr.SaveLabResults(
                    paramToSave,
                    thisTest.Id,
                    txtResultNotes.Text,
                    this.UserId, Convert.ToInt32(ddlLabReportedbyName.SelectedValue), Convert.ToDateTime(txtlabReportedbyDate.Text));

                base.Session["thisTestOrder"] = null;
                Session[SessionKey.SelectedLabTestOrder] = null;
                base.Session["LAB_REQTEST"] = null;
                IQCareMsgBox.NotifyAction("Result saved successfully", "Success", false,this,
                    string.Format("javascript:window.location='{0}'; return false;", this.RedirectUrl));
            }
            else
            {
                IQCareMsgBox.NotifyAction("Nothing was saved", "Failure", true, this,"");
            }
            
        }

        protected void ExitPage(object sender, EventArgs e)
        {
            Session[SessionKey.SelectedLabTestOrder] = null;
            Response.Redirect(RedirectUrl);
        }
        private void InjectScript(ref CheckBox cBox, ref TextBox txtLimitBox, ref TextBox txtValueBox)
        {
            string checkUndectable = cBox.ClientID;
            string detectionLimit = txtLimitBox.ClientID;
            string valueBox = txtValueBox.ClientID;
            string script = @"$(function () {$(""#" + checkUndectable + @""").click(function () {
             $(""#" + detectionLimit + @""").val("""");
            if ($(this).is("":checked"")) {
                $(""#" + detectionLimit + @""").removeAttr(""disabled"");
                $(""#" + detectionLimit + @""").focus();
                $(""#" + valueBox + @""").val("""");
                $(""#" + valueBox + @""").attr(""disabled"", ""disabled"");
            } else {
                $(""#" + detectionLimit + @""").attr(""disabled"", ""disabled"");
                $(""#" + valueBox + @""").removeAttr(""disabled"");
                $(""#" + valueBox + @""").focus();
            }
        }); });";
            ScriptManager.RegisterStartupScript(cBox, cBox.GetType(), checkUndectable, script, true);
        }
        private void PopulateUnits(ref DropDownList ddlControl, ref TextBox textLimit, int parameterid)
        {
            ILabManager labMgr = (ILabManager)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabManager, BusinessProcess.Laboratory");
            List<ParameterResultConfig> config = labMgr.GetParameterConfig(parameterid);
            if (config != null)
            {
                ddlControl.Items.Clear();
                ddlControl.ClearSelection();
                if (config.Count > 1)
                {
                    ddlControl.Items.Add(new ListItem("Select...", "-1"));
                }
                string strDefaultId = "";
                foreach (ParameterResultConfig row in config)
                {
                    ListItem item = (new ListItem(row.UnitName, row.UnitId.ToString()));
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
                    ListItem item = ddlControl.Items.FindByValue(strDefaultId);//.FindByValue(strDefaultId);
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
                
                foreach (ParameterResultOption option in options)
                {
                    ListItem item = (new ListItem(option.Text, option.Id.ToString()));
                    ddlControl.Items.Add(item);
                }
            }
        }
        private void AddValidation(ref Control cntrl, string itemName, string min,string max)
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
                LabTestParameterResult rowView = (LabTestParameterResult)e.Item.DataItem;
                string strDataType = rowView.ResultDataType;
                decimal? strResultValue = rowView.ResultValue;
                string strResultText = rowView.ResultText;
                decimal? strLimit = rowView.DetectionLimit;
                int strParameterId = rowView.ParameterId;

                bool hasResult = rowView.HasResult;
                FilteredTextBoxExtender fteLimit = e.Item.FindControl("fteLimit") as FilteredTextBoxExtender;
                FilteredTextBoxExtender fteValue = e.Item.FindControl("fteValue") as FilteredTextBoxExtender;
                DropDownList ddlResultUnit = e.Item.FindControl("ddlResultUnit") as DropDownList;
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
                        this.InjectScript(ref cBox, ref txtLimit, ref txtResultValue);
                    }

                    this.PopulateUnits(ref ddlResultUnit, ref txtLimit,strParameterId);

                }
                else if (strDataType == "SELECTLIST")
                {
                    this.PopulateSelectList(ref ddlResultList, strParameterId);

                }
            }
        }

        protected void btnOkAction_Click(object sender, EventArgs e)
        {

        }
    }
}