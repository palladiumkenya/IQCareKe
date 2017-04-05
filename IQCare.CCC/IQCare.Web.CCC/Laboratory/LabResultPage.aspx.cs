using Application.Common;
using Application.Presentation;
using Entities.Lab;
using Entities.PatientCore;
using Interface.Clinical;
using Interface.Laboratory;
using IQCare.Web.UILogic;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace IQCare.Web.Laboratory
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class LabResultPage : System.Web.UI.Page
    {
        /// <summary>
        /// The request MGR
        /// </summary>
        private ILabRequest requestMgr = (ILabRequest)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");
       /// <summary>
        /// The redirect URL
        /// </summary>
        private string RedirectUrl = "../ClinicalForms/frmPatient_Home.aspx";

        /// The is error
        /// </summary>
        private bool isError = false;


        /// <summary>
        /// Shows the information image.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <returns></returns>
        protected string ShowInfoImage(object notes)
        {
            return (!(notes.ToString() != null && String.IsNullOrEmpty(notes.ToString())) && notes.ToString().Length > 20) ? "" : "none";
        }

        /// <summary>
        /// Shows the text div.
        /// </summary>
        /// <param name="datatype">The datatype.</param>
        /// <returns></returns>
        protected string ShowTextDiv(object datatype)
        {
            return (datatype.ToString().ToUpper() == "TEXT") ? "" : "none";
        }

        /// <summary>
        /// Shows the select div.
        /// </summary>
        /// <param name="datatype">The datatype.</param>
        /// <returns></returns>
        protected string ShowSelectDiv(object datatype)
        {
            return (datatype.ToString().ToUpper() == "SELECTLIST") ? "" : "none";
        }

        /// <summary>
        /// Shows the number div.
        /// </summary>
        /// <param name="datatype">The datatype.</param>
        /// <returns></returns>
        protected string ShowNumDiv(object datatype)
        {
            return (datatype.ToString().ToUpper() == "NUMERIC") ? "" : "none";
        }
        protected string ShowNumResult(object hasResult, object undetectable)
        {

            return Convert.ToBoolean(hasResult) && null != undetectable && !Convert.ToBoolean(undetectable) ? "" : "none";
        }
        protected string ShowUndetectable(object hasResult, object undetectable)
        {
            return Convert.ToBoolean(hasResult) && null != undetectable && Convert.ToBoolean(undetectable) ? "" : "none";
        }
        /// <summary>
        /// Shows the text result.
        /// </summary>
        /// <param name="datatype">The datatype.</param>
        /// <param name="resultText">The result text.</param>
        /// <returns></returns>
        protected string ShowTextResult(object datatype, object resultText)
        {
            return ((datatype.ToString().ToUpper() == "TEXT" || datatype.ToString().ToUpper() == "SELECT LIST") && !(resultText.ToString().Trim() != null && String.IsNullOrEmpty(resultText.ToString().Trim()))) ? "" : "none";
        }
        AuthenticationManager Authentication = new AuthenticationManager();
        protected bool HasResultPermission
        {
            get
            {
                return (Authentication.HasFeatureRight("LABORATORY_RESULT", (DataTable)Session["UserRight"]) == true);
            }
        }
        protected string hPerm
        {
            get
            {
                return HasResultPermission ? "" : "none";
            }
        }
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Laboratory >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Result Page";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Lab Result Page";
            Master.ExecutePatientLevel = true;
            if (Application["AppCurrentDate"] != null)
            {
                hdappcurrentdate.Value = Application["AppCurrentDate"].ToString();
            }
            if (!IsPostBack)
            {
                this.LabOrderId = Convert.ToInt32(Session["PatientVisitId"]);
                if (LabOrderId > 0)
                {
                    this.PopulateRequest(LabOrderId);
                }
                else
                {
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(RedirectUrl, true);
                }
            }
            
            if(CurrentSession.Current.HasFunctionRight("LABORATORY", FunctionAccess.Delete))
            {
                sDelete = "";
            }
        }
        protected string sDelete = "none";
        /// <summary>
        /// Gets or sets the ordered labs.
        /// </summary>
        /// <value>
        /// The ordered labs.
        /// </value>
        private LabOrder thisLabOrder
        {
            get
            {
                return (LabOrder)base.Session["thisLabOrder"];
            }
            set
            {
                base.Session["thisLabOrder"] = value;
            }
        }
        private DataTable ServiceAreaList
        {
            get
            {
                DataTable dt = (DataTable)Session["AppModule"];
                return dt;
            }
        }
        private string GetModuleName(int moduleId)
        {
            DataRow thisRow = ServiceAreaList.AsEnumerable().Where(r => r["ModuleId"].ToString() == moduleId.ToString()).DefaultIfEmpty(null).FirstOrDefault();
            if (null != thisRow)
            {
                return thisRow["DisplayName"].ToString();
            }
            return "";
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
        /// <summary>
        /// Populates the request.
        /// </summary>
        /// <param name="labOrderId">The lab order identifier.</param>
        private void PopulateRequest(int labOrderId)
        {
            if (null == Session["PatientInformation"])
            {
                IPatientHome PatientManager;
                PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                DataSet theDS = PatientManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
                PatientManager = null;


                Session["PatientInformation"] = theDS.Tables[0];
            }
            this.thisLabOrder = requestMgr.GetLabOrder(this.LocationId, labOrderId);
            if (this.thisLabOrder.ModuleId <= 0)
            {
                EnrollmentService es = new EnrollmentService(PatientId);
                List<PatientEnrollment> pe = es.GetPatientEnrollment(CurrentSession.Current);
                if (pe != null)
                {
                    Session["TechnicalAreaId"] = this.thisLabOrder.ModuleId = pe.FirstOrDefault().ServiceAreaId;
                    base.Session["TechnicalAreaName"] = pe.FirstOrDefault().ServiceArea.Name;
                }
            }
            else
            {
                Session["TechnicalAreaId"] = this.thisLabOrder.ModuleId;
            }
            CurrentSession session = CurrentSession.Current.ResetCurrentModule();
            PatientService service = new PatientService(session, this.PatientId, this.ModuleId);
            // CurrentSession.Current.SetCurrentModule(this.ModuleId);
            CurrentSession.Current.SetCurrentPatient(this.PatientId, this.ModuleId);

            this.LabOrderId = this.thisLabOrder.Id;

            labelClinicalNotes.Text = this.thisLabOrder.ClinicalNotes; //dt.Rows[0]["ClinicalNotes"].ToString();
            //  labellaborderedbydate.Text = this.thisLabOrder.OrderDate.ToString("dd-MMM-yyyy");
            labelLabtobeDone.Text = this.thisLabOrder.PreClinicDate.HasValue ? this.thisLabOrder.PreClinicDate.Value.ToString("dd-MMM-yyyy") : "";
            labelOrderNumber.Text = string.Format("Order Number : {0}  | Status : {1} | Ordered by {2} on {3}",
                thisLabOrder.OrderNumber,
                thisLabOrder.OrderStatus,
                this.GetUserFullName(this.thisLabOrder.OrderedBy),
                this.thisLabOrder.OrderDate.ToString("dd-MMM-yyyy"));


            //  labelOrderedbyname.Text = this.GetEmployeeName(this.thisLabOrder.OrderedBy);

            LabOrder order = this.thisLabOrder;
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

            /*
            XmlDocument reportXML = new XmlDocument();
            XmlDeclaration newChild = reportXML.CreateXmlDeclaration("1.0", "UTF-8", null);
            reportXML.AppendChild(newChild);
            XmlElement element = reportXML.CreateElement("Root");
            reportXML.AppendChild(element);
            XmlElement rootElement = reportXML.DocumentElement;
           
            //XmlNode reportNode = reportXML.CreateNode(XmlNodeType.Element, "Order", string.Empty);  
            //reportNode.InnerXml =
            //   new XElement("orderid", this.thisLabOrder.Id).ToString() +
            //    new XElement("ordernumber", this.thisLabOrder.OrderNumber).ToString() +
            //    new XElement("orderdate", thisLabOrder.OrderDate.ToString("o")).ToString() +
            //    new XElement("orderby", this.GetUserFullName(thisLabOrder.OrderedBy)).ToString() +
            //    new XElement("servicearea", this.GetModuleName(thisLabOrder.ModuleId)).ToString() +
            //    new XElement("patientname", string.Format("{0} {1}", thisLabOrder.Client.FirstName, thisLabOrder.Client.LastName)).ToString() +
            //    new XElement("patientgender", thisLabOrder.Client.Sex).ToString() +
            //    new XElement("patientdob", thisLabOrder.Client.DateOfBirth.ToString("o")) +
            //    new XElement("patientnumber", thisLabOrder.Client.UniqueFacilityId).ToString();                  
            //rootElement.AppendChild(reportNode);             
            DateTime? nulldate = null;
            bool? nullBit = null;
            Double? nullDouble = null;
            XElement orderE = new XElement("Order",
                new XElement("orderid",          this.thisLabOrder.Id),
                new XElement("ordernumber",      this.thisLabOrder.OrderNumber),
                new XElement("orderstatus",      this.thisLabOrder.OrderStatus),
                new XElement("orderdate",        thisLabOrder.OrderDate.ToString("o")), 
                new XElement("ordernotes",       thisLabOrder.ClinicalNotes),
                new XElement("orderby",          this.GetUserFullName(thisLabOrder.OrderedBy)),
                new XElement("servicearea",      this.GetModuleName(thisLabOrder.ModuleId)),
                new XElement("patientname",      string.Format("{0} {1}", thisLabOrder.Client.FirstName, thisLabOrder.Client.LastName)),
                new XElement("patientgender",    thisLabOrder.Client.Sex),
                new XElement("patientdob",       thisLabOrder.Client.DateOfBirth.ToString("o")) ,
                new XElement("patientnumber",    thisLabOrder.Client.UniqueFacilityId).ToString());
                   XmlDocumentFragment orderNode = reportXML.CreateDocumentFragment();
                    orderNode.InnerXml = orderE.ToString();
                    rootElement.AppendChild(orderNode);

          
            foreach (LabOrderTest orderedTest in thisLabOrder.OrderedTest.Where(o => o.ParentLabTestId == null))
            {
                //XElement c = new XElement("Test",
                //    new XElement("ordertestid", orderedTest.Id),
                //    new XElement("testid", orderedTest.Test.Id),
                //    new XElement("testname", orderedTest.Test.Name),
                //      new XElement("department", orderedTest.Test.DepartmentName),
                //    new XElement("testnote", orderedTest.TestNotes),
                //    new XElement("resultnote", orderedTest.ResultNotes),
                //     new XElement("orderstatus", orderedTest.TestOrderStatus),
                //    new XElement("resultdate", orderedTest.ResultDate.HasValue ? orderedTest.ResultDate.Value.ToString("o") : ""),
                //new XElement("resultby", orderedTest.ResultBy.HasValue ? this.GetUserFullName(orderedTest.ResultBy.Value) : ""));
                foreach (LabTestParameterResult result in orderedTest.ParameterResults)
                {
                    XElement r = new XElement("Result",
                         new XElement("id",  result.LabOrderTestId),                           
                        new XElement("orderedtestid", orderedTest.Id),
                        new XElement("orderid",  result.LabOrderId),
                        new XElement("testid", orderedTest.Id),
                        new XElement("testname", orderedTest.TestName),
                        new XElement("department", orderedTest.Test.DepartmentName),
                        new XElement("testnote", orderedTest.TestNotes),                           
                        new XElement("resultnote", orderedTest.ResultNotes),
                        new XElement("teststatus", orderedTest.TestOrderStatus),                           
                        new XElement("parameterid", result.ParameterId),
                        new XElement("parametername", result.ParameterName),
                        new XElement("datatype", result.ResultDataType),
                        new XElement("hasresult", result.HasResult),
                        new XElement("resultdate", orderedTest.ResultDate.HasValue ? orderedTest.ResultDate.Value : nulldate),
                        new XElement("resultby", orderedTest.ResultBy.HasValue ? this.GetUserFullName(orderedTest.ResultBy.Value) : ""),                         
                        new XElement("unit",  result.ResultUnitName),
                        new XElement("undetectable", result.Undetectable.HasValue ? result.Undetectable.Value : nullBit),
                        new XElement("detectionlimit", result.DetectionLimit.HasValue ? result.DetectionLimit.Value : nullDouble),
                        new XElement("resultvalue", result.ResultDataType == "NUMERIC" ? (result.ResultValue.HasValue? result.ResultValue.Value.ToString(): "") : (result.ResultDataType == "SELECTLIST" ? result.ResultOption : result.ResultText))  ,
                        new XElement("maxboundary", (result.Config != null && result.Config.MaxBoundary.HasValue) ? result.Config.MaxBoundary.Value.ToString() : ""),
                        new XElement("minboundary", (result.Config != null && result.Config.MinBoundary.HasValue) ? result.Config.MinBoundary.Value.ToString() : ""),
                        new XElement("maxnormalboundary", (result.Config != null && result.Config.MaxNormalRange.HasValue) ? result.Config.MaxNormalRange.Value.ToString() : ""),
                        new XElement("minnormalboundary", (result.Config != null && result.Config.MinNormalRange.HasValue) ? result.Config.MinNormalRange.Value.ToString() : "")

                        );
                    XmlDocumentFragment resultNode = reportXML.CreateDocumentFragment();
                    resultNode.InnerXml = r.ToString();
                    rootElement.AppendChild(resultNode);
                }

                //XmlDocumentFragment testNode = reportXML.CreateDocumentFragment();
                //testNode.InnerXml = c.ToString();
                //rootElement.AppendChild(testNode);
            }

            reportXML.Save(@"C:\apps\labresult.xml");

            DataSet ds = new DataSet("Report");
            using (System.IO.TextReader txR = new System.IO.StringReader(reportXML.InnerXml))
            {
                ds.ReadXml(txR);
                txR.Close();
            }
            ds.WriteXml(@"C:\apps\labresultschema.xml", XmlWriteMode.WriteSchema);*/
            repeaterLabTest.DataSource = _test.Where(o => o.DeleteFlag == false && o.Test.IsGroup==false);
            repeaterLabTest.DataBind();
        }
        /// <summary>
        /// Handles the PreRender event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            // this.BindLabTests();
            divError.Visible = isError;
            btnExitPage.OnClientClick = string.Format("javascript:window.location='{0}'; return false;", this.RedirectUrl);
        }


        /// <summary>
        /// Gets the employee list.
        /// </summary>
        /// <value>
        /// The employee list.
        /// </value>
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

        /// <summary>
        /// Gets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        private int ModuleId { get { return Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]); } }

        /// <summary>
        /// Gets the lab order identifier.
        /// </summary>
        /// <value>
        /// The lab order identifier.
        /// </value>
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

        /// <summary>
        /// Gets a value indicating whether this <see cref="LabResultPage"/> is debug.
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

        /// <summary>
        /// Gets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        private int EmployeeId
        {
            get
            {
                return Convert.ToInt32(Session["AppUserEmployeeId"].ToString());
            }
        }

        /// <summary>
        /// Gets the patient information.
        /// </summary>
        /// <value>
        /// The patient information.
        /// </value>
        private DataTable PatientInfo
        {
            get
            {
                return (DataTable)Session["PatientInformation"];
            }
        }



        /// <summary>
        /// Handles the ItemDataBound event of the repeaterLabTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void repeaterLabTest_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LabOrderTest rowView = (LabOrderTest)e.Item.DataItem;
                string labOrderId = rowView.LabOrderId.ToString();
                string labTestId = rowView.TestId.ToString();

                int labOrdertestId = rowView.LabTestId;
                Label labReportedbyDate = e.Item.FindControl("labReportedbyDate") as Label;
              //  Label labelReportedbyName = e.Item.FindControl("labelReportedbyName") as Label;
                Button buttonResult = e.Item.FindControl("buttonResult") as Button;
                Label labelRequestNotes = e.Item.FindControl("labelRequestNotes") as Label;

                if (rowView.TestNotes != "")
                {
                    if (rowView.TestNotes.Length <= 20)
                    {
                        labelRequestNotes.Text = rowView.TestNotes;
                    }
                    else
                    {
                        labelRequestNotes.Text = rowView.TestNotes.Substring(0, 19);
                    }
                }

                DataTable _user = this.UserList;
                if (rowView.ResultBy.HasValue && rowView.ResultBy.HasValue)
                {

                   // labelReportedbyName.Text = this.GetUserFullName(rowView.ResultBy.Value);
                    labReportedbyDate.Text = rowView.ResultDate.Value.ToString("dd-MMM-yyyy hh:mm");
                    // buttonResult.Visible = false;
                    buttonResult.Enabled = false;
                    buttonResult.Text = this.GetUserFullName(rowView.ResultBy.Value);
                }
                else
                {
                    //labelReportedbyName.Visible = false;
                    buttonResult.Enabled = this.HasResultPermission;
                    buttonResult.Text = "Enter Result";
                    //buttonResult.OnClientClick = "javascript:pageopen();return"//

                }
                Repeater repeater = e.Item.FindControl("repeaterResult") as Repeater;
                if (null != repeater)
                {
                    List<LabTestParameterResult> paramResults = requestMgr.GetLabTestParameterResult(labOrdertestId);
                    repeater.DataSource = paramResults;
                    repeater.DataBind();
                }

            }
        }

        /// <summary>
        /// Handles the ItemDataBound event of the repeaterResult control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void repeaterResult_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                LabTestParameterResult rowView = (LabTestParameterResult)e.Item.DataItem;
                string strName = rowView.ParameterName;
                int strParamId = rowView.ParameterId;
                int strResultId = rowView.Id;
                bool hasResult = rowView.HasResult;
                string strDataType = rowView.ResultDataType;

                Label labelResultText = e.Item.FindControl("labelResultText") as Label;
                Label labelDetectionLimit = e.Item.FindControl("labelDetectionLimit") as Label;
                Label labelResultOption = e.Item.FindControl("labelResultOption") as Label;

                Label labelResultValue = e.Item.FindControl("labelResultValue") as Label;

                Label labelRanges = e.Item.FindControl("labelRanges") as Label;

                labelResultValue.Visible = labelDetectionLimit.Visible = hasResult;



                if (strDataType == "NUMERIC")
                {
                    if (hasResult)
                    {
                        labelDetectionLimit.Visible = false;

                        if (rowView.Undetectable != null && rowView.Undetectable.Value)
                        {
                            // labelResultValue.Text = "Undetectable";
                            labelResultValue.Visible = false;
                            labelDetectionLimit.Visible = true;
                            labelDetectionLimit.Text = string.Format("Undetectable  (detection limit = {0}) {1}", rowView.DetectionLimit, rowView.ResultUnitName);
                        }
                        else
                        {
                            labelResultValue.Visible = true;
                            labelResultValue.Text = string.Format("{0} {1}", rowView.ResultValue, rowView.ResultUnitName);
                            if (null != rowView.Config)
                            {
                                labelRanges.Text = string.Format("{0} - {1} {2}", rowView.Config.MinNormalRange, rowView.Config.MaxNormalRange, rowView.ResultUnitName);
                            }
                        }
                    }

                }
                else if (strDataType == "SELECTLIST")
                {
                    if (hasResult)
                    {
                        labelResultOption.Visible = true;
                        labelResultOption.Text = rowView.ResultOption;
                        // ddlResultList.Visible = false;
                    }
                    else
                    {
                        // ddlResultList.Visible = true;
                        labelResultOption.Visible = false;
                        // this.PopulateSelectList(ref ddlResultList, strParamId);
                    }
                }
                else if (strDataType == "TEXT")
                {
                    if (hasResult)
                    {
                        labelResultText.Text = rowView.ResultText;
                        //txtResultText.Visible = false;
                    }
                    else
                    {
                        labelResultText.Visible = false;
                        // txtResultText.Visible = true;
                    }
                }
            }
        }

        protected void repeaterLabTest_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.CommandName == "EnterResult")
                {
                    int labtesOrderId = Convert.ToInt32(e.CommandArgument);
                    //this.LabOrderId
                    LabOrderTest thisTest = thisLabOrder.OrderedTest.DefaultIfEmpty(null).FirstOrDefault(t => t.Id == labtesOrderId);
                    if (thisTest != null)
                    {
                        Guid g = Guid.NewGuid();
                        base.Session[SessionKey.SelectedLabTestOrder] = thisTest;
                       HttpContext.Current.ApplicationInstance.CompleteRequest();
                        Response.Redirect("~/Laboratory/EnterResultPage.aspx?key=" + g.ToString(), true);
                    }
                }
            }
        }

        protected void deleteConfirmed_Click(object sender, EventArgs e)
        {
            if (CurrentSession.Current.HasFunctionRight("LABORATORY", FunctionAccess.Delete))
            {
                requestMgr.DeleteLabOrder(this.LabOrderId, UserId, txtDeleteReason.Text.Trim());
                IQCareMsgBox.NotifyAction(string.Format("Lab order {0} has been deleted", thisLabOrder.OrderNumber), "Delete LabOrder", false, this, string.Format("window.location.href='{0}'", RedirectUrl));
            }
        }
    }
}