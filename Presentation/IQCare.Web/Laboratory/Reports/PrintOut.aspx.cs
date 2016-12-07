using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using Application.Presentation;
using Entities.Lab;
using Interface.Laboratory;
using System.Xml;
using System.Xml.Linq;

namespace IQCare.Web.Laboratory.Reports
{
    public partial class PrintOut : System.Web.UI.Page
    {
        /// <summary>
        /// The RPT document
        /// </summary>
        ReportDocument rptDocument;
        /// <summary>
        /// The request MGR
        /// </summary>
        private ILabRequest requestMgr = (ILabRequest)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");

        /// <summary>
        /// Gets or sets the this lab order.
        /// </summary>
        /// <value>
        /// The this lab order.
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
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(this.thisLabOrder== null )
            {
              int labOrderId = Convert.ToInt32(Session["PatientVisitId"]);
                if (labOrderId > 0)
                {
                   this.thisLabOrder = requestMgr.GetLabOrder(this.LocationId, labOrderId);
                }
                else
                {
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect("~/frmFacilityHome.aspx", true);
                }
            }                 
                
            this.GenerateReport();
        }
        /// <summary>
        /// Gets the name of the facility.
        /// </summary>
        /// <param name="locationId">The location identifier.</param>
        /// <returns></returns>
        private string GetFacilityProperty(int locationId,string propertName)
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("~\\XMLFiles\\ALLMasters.con"));

            IQCareUtils theUtils = new IQCareUtils();
            DataTable dt = new DataTable("Mst_Facility");
            if (theDS.Tables["Mst_Facility"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Facility"]);
                if (theDV.Table != null)
                {
                    dt = (DataTable)theUtils.CreateTableFromDataView(theDV);
                }
                DataRow thisRow = dt.AsEnumerable().Where(r => r["FacilityId"].ToString() == locationId.ToString()).DefaultIfEmpty(null).FirstOrDefault();
                if (null != thisRow)
                {
                    return thisRow[propertName].ToString();
                }
            }
            return "";
        }
      
        /// <summary>
        /// Gets the user list.
        /// </summary>
        /// <value>
        /// The user list.
        /// </value>
        private DataTable UserList
        {
            get
            {
                DataSet theDS = new DataSet();
                theDS.ReadXml(MapPath("~\\XMLFiles\\ALLMasters.con"));

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
        /// <summary>
        /// Gets the service area list.
        /// </summary>
        /// <value>
        /// The service area list.
        /// </value>
        private DataTable ServiceAreaList
        {
            get
            {
                DataTable dt = (DataTable)Session["AppModule"];
                return dt;
            }
        }
        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns></returns>
        private string GetModuleName(int moduleId)
        {
            DataRow thisRow = ServiceAreaList.AsEnumerable().Where(r => r["ModuleId"].ToString() == moduleId.ToString()).DefaultIfEmpty(null).FirstOrDefault();
            if (null != thisRow)
            {
                return thisRow["DisplayName"].ToString();
            }
            return "";
        }
        /// <summary>
        /// Gets the full name of the user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
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
        /// Generates the report.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void GenerateReport()
        {
             XmlDocument reportXML = new XmlDocument();
            XmlDeclaration newChild = reportXML.CreateXmlDeclaration("1.0", "UTF-8", null);
            reportXML.AppendChild(newChild);
            XmlElement element = reportXML.CreateElement("Root");
            reportXML.AppendChild(element);
            XmlElement rootElement = reportXML.DocumentElement;
           
                  
            //DateTime? nulldate = null;
            bool? nullBit = null;
            decimal? nullDecimal = null;
            XElement orderE = new XElement("Order",
                new XElement("orderid",          this.thisLabOrder.Id),
                new XElement("ordernumber",      this.thisLabOrder.OrderNumber),
                new XElement("orderstatus",      this.thisLabOrder.OrderStatus),
                new XElement("orderdate",       thisLabOrder.OrderDate.ToString("dd-MMM-yyyy HH:mm")), 
                new XElement("ordernotes",       thisLabOrder.ClinicalNotes),
                new XElement("orderby",          this.GetUserFullName(thisLabOrder.OrderedBy)),
                new XElement("servicearea",      this.GetModuleName(thisLabOrder.ModuleId)),
                new XElement("patientname",      string.Format("{0} {1}", thisLabOrder.Client.FirstName, thisLabOrder.Client.LastName)),
                new XElement("patientgender",    thisLabOrder.Client.Sex),
                new XElement("patientdob",       thisLabOrder.Client.DateOfBirth.ToString("dd-MMM-yyyy")) ,
                new XElement("patientageyears", (thisLabOrder.OrderDate.Year - thisLabOrder.Client.DateOfBirth.Year)),
                new XElement("patientnumber",    thisLabOrder.Client.UniqueFacilityId),
                new XElement("facilityname",     this.GetFacilityProperty(thisLabOrder.LocationId,"FacilityName")),
                new XElement("facilityaddress", this.GetFacilityProperty(thisLabOrder.LocationId, "FacilityAddress")),
                new XElement("facilitytel",      this.GetFacilityProperty(thisLabOrder.LocationId, "FacilityTel")),
                new XElement("facilityemail",    this.GetFacilityProperty(thisLabOrder.LocationId, "FacilityEmail")),
                new XElement("facilitylogo",     Server.MapPath("~/Images/"+this.GetFacilityProperty(thisLabOrder.LocationId, "FacilityLogo")))
                );
                   XmlDocumentFragment orderNode = reportXML.CreateDocumentFragment();
                    orderNode.InnerXml = orderE.ToString();
                    rootElement.AppendChild(orderNode);

          
            foreach (LabOrderTest orderedTest in thisLabOrder.OrderedTest.Where(o => o.ParentLabTestId == null))
            {               
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
                        new XElement("resultdate", orderedTest.ResultDate.HasValue ? orderedTest.ResultDate.Value.ToString("dd-MMM-yyyy HH:mm") : ""),
                        new XElement("resultby", orderedTest.ResultBy.HasValue ? this.GetUserFullName(orderedTest.ResultBy.Value) : ""),                         
                        new XElement("unit",  result.ResultUnitName),
                        new XElement("undetectable", result.Undetectable.HasValue ? result.Undetectable.Value : nullBit),
                        new XElement("detectionlimit", result.DetectionLimit.HasValue ? result.DetectionLimit.Value : nullDecimal),
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

            }    
            DataSet reportData = new DataSet("Report");
            using (System.IO.TextReader txR = new System.IO.StringReader(reportXML.InnerXml))
            {
                reportData.ReadXml(txR);
                txR.Close();
            }
            reportData.AcceptChanges();

            this.rptDocument = new ReportDocument();

            rptDocument.Load(MapPath(string.Format("~\\Laboratory\\Reports\\{0}", "LabReport.rpt")));
            rptDocument.SetDataSource(reportData);
            rptDocument.SetParameterValue("currentuser", Session["AppUserName"]);

            reportViewer.EnableParameterPrompt = false;
            reportViewer.ReportSource = rptDocument;
            reportViewer.DataBind();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Unload" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            this.UnLoadReport();
        }
        /// <summary>
        /// Handles the Unload event of the reportViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void reportViewer_Unload(object sender, EventArgs e)
        {
            this.UnLoadReport();
        }
        /// <summary>
        /// Uns the load report.
        /// </summary>
        void UnLoadReport()
        {
            try
            {
                rptDocument.Dispose();
                this.rptDocument = null;
            }
            catch { }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
           
            btnExitPage.OnClientClick = string.Format("javascript:window.location='{0}'; return false;",  "../../Laboratory/LabResultPage.aspx");
        }
    }
}