using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Clinical;
using System.Configuration;
using Application.Presentation;
using Entities.Clinical;
using System.Data;
using AjaxControlToolkit;
using IQCare.Web.UILogic;

namespace IQCare.Web.ClinicalService
{
    public partial class ServiceRecordEntry : System.Web.UI.Page
    {
        private string RedirectUrl = "../ClinicalForms/frmPatient_Home.aspx";
        private bool isError = false;
        private bool isDataEntry = false;
        private bool IsPaperless
        {
            get
            {
                return (Session["Paperless"].ToString() == "1");
            }
        }

        protected string sDataEntry
        {
            get
            {
                return this.isDataEntry ? "" : "none";
            }
        }
        protected string sServiceEntry
        {
            get
            {
                return Convert.ToInt32(this.hdSelectedModule.Value) > 0 ? "" : "none";
            }
        }
        protected string sDepartmentEntry
        {
            get
            {
                return (null == this.ServiceOrdered.Services || this.ServiceOrdered.TargetModuleId < 1) ? "" : "none";
            }
        }
        protected string hDepartmentEntry
        {
            get
            {
                return (this.ServiceOrdered.TargetModuleId > 0) ? "" : "none";
            }
        }
        protected string sPaper
        {
            get
            {
                return !IsPaperless && isDataEntry ? "" : "none";
            }
        }
        protected string sPaperless
        {
            get
            {
                return IsPaperless ? "" : "none";
            }
        }
        protected string sEdit
        {
            get
            {
                return this.ServiceOrderId > 0 ? "none" : "";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Services >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Order Form";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Service Order Form";
            if (Application["AppCurrentDate"] != null)
            {
                hdappcurrentdate.Value = Application["AppCurrentDate"].ToString();
            }
            if (!IsPostBack)
            {
                base.Session["OrderedServices"] = null;
                AutoCompleteExtender1.ContextKey = this.hdSelectedModule.Value.ToString();
                this.PopulateControls();
                txtorderedbydate.Text = txtReportedbyDate.Text =  DateTime.Now.ToString("dd-MMM-yyyy");
            }
        }
        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
            Exception ex = Server.GetLastError();
            this.ShowErrorMessage(ref ex);
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
                lastError.Data.Add("Domain", "Service Management");
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
        protected void Page_PreRender(object sender, EventArgs e)
        {

            divError.Visible = isError;

            //ddlDepartment.Enabled = (null == this.ServiceOrdered.Services && this.ServiceOrdered.TargetModuleId < 1);

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
        private void BindDropdownOrderBy()
        {

            //DataSet theDS = new DataSet();
            String userId = "";
            //theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            //if (theDS.Tables["Mst_Employee"] != null)
            //{
            DataView theDV = new DataView(this.UserList);

            string rowFilter = "EmployeeId Is Not Null Or EmployeeId > 0 And UserDeleteFlag = 0";
            
            if (IsPaperless && this.EmployeeId > 0)
            {
                userId = this.UserId.ToString();
                rowFilter = "UserId = " + userId;
            }
          
            theDV.RowFilter = rowFilter;
            if (theDV.Table != null)
            {
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);

                BindManager.BindCombo(ddlorderedbyname, theDT, "Name", "UserId", "", userId);
                ListItem item = ddlorderedbyname.Items.FindByValue(userId);
                if (item == null)
                {
                    item = ddlorderedbyname.Items.FindByValue(this.UserId.ToString());
                }
                if (item != null)
                {
                    item.Selected = true;
                }
            }


        }
        private void BindDropDownReportedBy()
        {
            
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            
            DataView theDV = new DataView(this.UserList);
           
            theDV.RowFilter = "EmployeeId Is Not Null Or EmployeeId > 0";
            
            if (theDV.Table != null)
            {
                DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddlReportedbyName, theDT, "Name", "UserId", "", this.UserId.ToString());
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
        void PopulateModules()
        {
            try
            {
                BindFunctions BindManager = new BindFunctions();
                IPatientRegistration ptnMgr = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                DataSet DSModules = ptnMgr.GetModuleNames(Convert.ToInt32(Session["AppLocationId"]));

                DataTable theDT = new DataTable();
                theDT = DSModules.Tables[0];

                if (theDT.Rows.Count > 0)
                {
                    BindManager.BindCombo(ddlDepartment, theDT, "ModuleName", "ModuleID");
                    ptnMgr = null;
                }
            }
            catch (Exception ex)
            {

                this.ShowErrorMessage(ref ex);
            }
        }
        void PopulateControls()
        {

            this.PopulateModules();
            this.BindDropdownOrderBy();
            this.BindDropDownReportedBy();

        }
        private int ServiceOrderId
        {
            get
            {
                int val = int.Parse(HServiceOrderId.Value);

                return val;
            }
            set
            {
                HServiceOrderId.Value = value.ToString();
            }
        }
        private int ServiceId
        {
            get
            {
                int val = int.Parse(hServiceId.Value);

                return val;
            }
            set
            {
                hServiceId.Value = value.ToString();
            }
        }
        private string ServiceName
        {
            get
            {
                return hServiceName.Value.Trim();
            }
            set
            {
                hServiceName.Value = value;
            }
        }
        private int ServiceModuleId
        {
            get
            {
                int val = int.Parse(hModuleServiceId.Value);

                return val;
            }
            set
            {
                hModuleServiceId.Value = value.ToString();
            }
        }
        private string ServiceModuleName
        {
            get
            {
                return hModuleServiceName.Value.Trim();
            }
            set
            {
                hModuleServiceName.Value = value.ToString();
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

        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> SearchService(string prefixText, int count, string contextKey)
        {

            IServiceRequest rMgr = (IServiceRequest)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BServiceRequest, BusinessProcess.Clinical");

            int moduleId = Convert.ToInt32(contextKey);

            List<Service> services = rMgr.FindServiceByName(prefixText, moduleId);
            List<string> ar = new List<string>();
            string custItem = string.Empty;
            if (services.Count > 0)
            {
                foreach (Service service in services)
                {
                    try
                    {
                        custItem = AutoCompleteExtender.CreateAutoCompleteItem(
                            string.Format("{0} {1}", service.Name, service.Description != "" ? "(" + service.Description + ")" : ""),
                            String.Format("{0};{1};{2};{3}",
                               service.Id,
                                service.Name,
                                service.ServiceAreaId,
                                service.ServiceArea
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
        protected void ServiceNameChanged(object sender, EventArgs e)
        {
            ServiceOrder order = this.ServiceOrdered;
            int serviceId;
            string serviceName = "";
            int moduleId = 0;
            string moduleName = "";
            this.isDataEntry = false;

            if (hdCustID.Value != "")
            {
                String[] itemCodes = hdCustID.Value.Split(';');
                if (itemCodes.Length == 4)
                {
                    serviceId = Convert.ToInt32(itemCodes[0]);
                    serviceName = itemCodes[1].ToString();
                    moduleId = Convert.ToInt32(itemCodes[2]);
                    moduleName = itemCodes[3].ToString();
                    bool proceed = false;
                    proceed = (null == order.Services || order.Services.Count == 0 || !order.Services.Exists(o => o.ServiceId == serviceId));
                    if (!proceed)
                    {
                        ((TextBox)sender).Text = "";
                        hdCustID.Value = "";
                        this.isDataEntry = false;
                        return;
                    }
                    this.ServiceId = serviceId;
                    this.ServiceName = serviceName;
                    this.ServiceModuleId = moduleId;
                    this.ServiceModuleName = moduleName;
                    this.isDataEntry = true;
                }

            }
        }
        private ServiceOrder ServiceOrdered
        {
            get
            {
                if (base.Session["OrderedServices"] == null)
                {

                    return new ServiceOrder()
                    {
                        PatientId = this.PatientId,
                        LocationId = this.LocationId,
                        UserId = this.UserId,
                        ModuleId = this.ModuleId,
                        TargetModuleId = -1
                    };
                }
                else
                {
                    return (ServiceOrder)base.Session["OrderedServices"];
                }
            }
            set
            {
                Session["OrderedServices"] = value;
            }
        }

        protected void ExitPage(object sender, EventArgs e)
        {
            hdCustID.Value = textSelectService.Text = txtRequestNote.Text = txtResultNote.Text = "";
            this.isDataEntry = false;
            base.Session["OrderedServices"] = null;
            Response.Redirect(RedirectUrl);
        }
        private void NotifyAction(string strMessage, string strTitle, bool errorFlag, string onOkScript = "")
        {
            lblNoticeInfo.Text = strMessage;
            lblNotice.Text = strTitle;
            lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
            lblNoticeInfo.Font.Bold = true;
            imgNotice.ImageUrl = (errorFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
            btnOkAction.OnClientClick = "";
            if (onOkScript != "")
            {
                btnOkAction.OnClientClick = onOkScript;
            }
            this.notifyPopupExtender.Show();
        }
        protected void btnOkAction_Click(object sender, EventArgs e)
        {

        }

        protected void gridServiceRequested_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gridServiceRequested_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    string serviceId = (e.CommandArgument.ToString());
                    ServiceOrder order = this.ServiceOrdered;

                    OrderedService orderedService = order.Services.FirstOrDefault(w => w.ServiceId.ToString() == serviceId);
                    if (orderedService != null)
                    {
                        if (orderedService.Id > 0)
                        {
                            order.Services.FirstOrDefault(w => w.ServiceId.ToString() == serviceId).DeleteFlag = true;
                        }
                        else
                        {
                            order.Services.Remove(orderedService);
                        }
                    }
                    if (null == order.Services || order.Services.Count == 0 || order.Services.Count(w => w.DeleteFlag == false) == 0)
                    {
                        order.TargetModuleId = -1;
                    }
                    this.ServiceOrdered = order;
                    this.BindServiceRequests();
                    button1_Click(button1, null);
                    divTestComponent.Update();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }

        protected void btnAddRecord_Click(object sender, EventArgs e)
        {
            OrderedService thisService = null;
            ServiceOrder order = this.ServiceOrdered;

            if (null != order && null != order.Services && order.Services.Count > 0 && order.Services.Exists(o => o.ServiceId == this.ServiceId))
            {
                this.isDataEntry = false;
                this.ServiceId = this.ServiceModuleId = -1;
                this.ServiceName = this.ServiceModuleName = "";
                hdCustID.Value = textSelectService.Text = txtRequestNote.Text = txtResultNote.Text = "";
                return;
            }
            if (order.TargetModuleId == -1 && Convert.ToInt32(hdSelectedModule.Value) > 0)
            {
                order.TargetModuleId = Convert.ToInt32(hdSelectedModule.Value);
                labelSelectedDepartment.Text = hdSelectedModuleName.Value;
                //ddlDepartment.Enabled = false;
            }
            if (null == order.Services)
            {
                order.Services = new List<OrderedService>();
            }
            thisService = new OrderedService();
            thisService.Service = new Service()
            {
                Id = this.ServiceId,
                Name = this.ServiceName,
                ServiceArea = this.ServiceModuleName,
                ServiceAreaId = this.ServiceModuleId,
                DeleteFlag = false

            };
            thisService.Id = -1;
            thisService.RequestNotes = txtRequestNote.Text;
            thisService.Quantity = Convert.ToInt32(textQuantity.Text);
            if (!IsPaperless)
            {
                thisService.ResultNotes = txtResultNote.Text;
            }
            order.Services.Add(thisService);
            this.ServiceOrdered = order;
            hdCustID.Value = textSelectService.Text = txtRequestNote.Text = txtResultNote.Text = "";
            this.isDataEntry = false;
            // ddlDepartment.Enabled = false;
            this.BindServiceRequests();
        }

        private void BindServiceRequests()
        {
            ServiceOrder order = this.ServiceOrdered;
            List<OrderedService> _test = new List<OrderedService>();
            if (null != order && null != order.Services && order.Services.Count > 0)
            {
                _test = order.Services.Where(o => o.DeleteFlag == false).ToList();
            }
            else
            {

                _test = new List<OrderedService>();
                _test.Add(new OrderedService()
                {
                    DeleteFlag = true,
                    Id = -1,
                    RequestNotes = "",
                    Service = new Service()

                }
                    );

            };
            gridServiceRequested.DataSource = _test.Where(o => o.DeleteFlag == false).ToList();
            gridServiceRequested.DataBind();
        }

        protected void CancelEntry(object sender, EventArgs e)
        {
            hdCustID.Value = textSelectService.Text = txtRequestNote.Text = txtResultNote.Text = "";
            this.isDataEntry = false;

        }
        private Boolean FieldValidation(string orderbydate, string orderby, string appcurrentdate, string resultdate, string resultBy)
        {

            IQCareUtils theUtils = new IQCareUtils();

            Page page = HttpContext.Current.Handler as Page;
            if (orderby == "0")
            {
                this.NotifyAction("Ordered By is not selected", "Field Validation", true, "javascript:HideModalPopup();return false;");
                return false;
            }
            else if (orderbydate == "")
            {
                this.NotifyAction("Ordered By Date is not specified", "Field Validation", true, "javascript:HideModalPopup();return false;");
                return false;

            }
            else if (Convert.ToDateTime(orderbydate) > Convert.ToDateTime(appcurrentdate))
            {

                this.NotifyAction("Ordered To date cannot be greater than todays date.", "Field Validation", true, "javascript:HideModalPopup();return false;");
                return false;
            }
            else if (!this.IsPaperless)
            {
                if (resultBy == "0" || resultdate == "")
                {
                    this.NotifyAction("Reported By Date and Reported By are required", "Field Validation", true, "javascript:HideModalPopup();return false;");
                    return false;
                }
                else if (Convert.ToDateTime(resultdate) > Convert.ToDateTime(appcurrentdate) || Convert.ToDateTime(resultdate) < Convert.ToDateTime(orderbydate))
                {

                    this.NotifyAction("Reported By Date cannot be greater than todays date or the Order By Date.", "Field Validation", true, "javascript:HideModalPopup();return false;");
                    return false;
                }

            }
            return true;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {

            string orderedBy = ddlorderedbyname.SelectedValue;
            string orderdate = txtorderedbydate.Text;
            string orderResultBy = ddlReportedbyName.SelectedValue;
            string orderResultdate = txtReportedbyDate.Text;
            string appcurrdate = hdappcurrentdate.Value;
            string strClinicalNotes = txtClinicalNotes.Text;
            if (FieldValidation(orderdate, orderedBy, appcurrdate, orderResultdate, orderResultBy) == false)
            {
                return;
            }

            IServiceRequest requestMgr = (IServiceRequest)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BServiceRequest, BusinessProcess.Clinical");
            if (this.ServiceOrderId > 0) //update
            {
                //
            }
            else
            {
                ServiceOrder order = this.ServiceOrdered;
                order.LocationId = this.LocationId;
                order.PatientId = this.PatientId;
                order.ModuleId = this.ModuleId;
                order.ModuleId = Convert.ToInt32(hdSelectedModule.Value);
                order.DeleteFlag = false;
                order.OrderDate = Convert.ToDateTime(orderdate);
                order.OrderedBy = Convert.ToInt32(orderedBy);
                order.ClinicalNotes = strClinicalNotes;
                order.UserId = this.UserId;
                if (!this.IsPaperless)
                {
                    order.Services.ForEach(o =>
                    {
                        o.ResultBy = Convert.ToInt32(orderResultBy);
                        o.ResultDate = Convert.ToDateTime(orderResultdate);

                    });
                }

                ServiceOrder _saveOrder = requestMgr.SaveServiceOrder(order, this.UserId, this.LocationId);
                base.Session["OrderedServices"] = null;
                this.NotifyAction(string.Format("Service Order number {0}, saved successfully", _saveOrder.OrderNumber), "Service Order", false,
                   string.Format("javascript:window.location='{0}'; return false;", this.RedirectUrl));
            }
        }

        protected void button1_Click(object sender, EventArgs e)
        {
            // ddlDepartment.Enabled = (null == this.ServiceOrdered.Services && this.ServiceOrdered.TargetModuleId < 1);
        }


    }
}