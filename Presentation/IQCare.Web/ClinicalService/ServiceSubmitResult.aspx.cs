using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Application.Presentation;
using Entities.Clinical;
using Interface.Clinical;
using IQCare.Web.UILogic;

namespace IQCare.Web.ClinicalService
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Web.UI.Page" />
    public partial class ServiceSubmitResult : System.Web.UI.Page
    {
        /// <summary>
        /// The is error
        /// </summary>
        private bool isError = false;

        /// <summary>
        /// The redirect URL
        /// </summary>
        private string RedirectUrl = "../ClinicalForms/frmPatient_Home.aspx";
        /// <summary>
        /// The request MGR
        /// </summary>
        private IServiceRequest requestMgr = (IServiceRequest)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BServiceRequest, BusinessProcess.Clinical");

        /// <summary>
        /// Gets a value indicating whether this <see cref="ServiceSubmitResult"/> is debug.
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
        /// Gets the module identifier.
        /// </summary>
        /// <value>
        /// The module identifier.
        /// </value>
        private int ModuleId { get { return Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]); } }

        /// <summary>
        /// Gets or sets the patient identifier.
        /// </summary>
        /// <value>
        /// The patient identifier.
        /// </value>
        private int PatientId
        {
            get
            {
                int val = int.Parse(hdPatientId.Value);

                return val;
            }
            set
            {
                hdPatientId.Value = value.ToString();
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
        /// Gets or sets the service order identifier.
        /// </summary>
        /// <value>
        /// The service order identifier.
        /// </value>
        private int ServiceOrderId
        {
            get
            {
                int val = int.Parse(hdServiceOrderId.Value);

                return val;
            }
            set
            {
                hdServiceOrderId.Value = value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the this order.
        /// </summary>
        /// <value>
        /// The this order.
        /// </value>
        private ServiceOrder thisOrder
        {
            get
            {
                return (base.Session["thisOrder"] == null) ? new ServiceOrder() : (ServiceOrder)base.Session["thisOrder"];
            }
            set
            {
                base.Session["thisOrder"] = value;
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
                return Convert.ToInt32(base.Session["AppUserId"]);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnOkAction control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnOkAction_Click(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Handles the Click event of the buttonSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void buttonSave_Click(object sender, EventArgs e)
        {
            string strvalue = this.Request[hdServiceTestId.UniqueID].ToString();
            string v1 = hdServiceTestId.Value;

            try
            {
                int serviceOrderId = Int32.Parse(strvalue);

                int resultBy = Convert.ToInt32(ddlReportedbyName.SelectedValue);
                string strResult = textResult.Text;
                string strDate = txtReportedbyDate.Text;
                IServiceRequest requestMgr = (IServiceRequest)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BServiceRequest, BusinessProcess.Clinical");
                OrderedService service = this.thisOrder.Services.DefaultIfEmpty(null).FirstOrDefault(s => s.Id == serviceOrderId);

                if (resultBy > 0 && strResult != "" && strDate != "" && null != service)
                {
                    DateTime resultDate = Convert.ToDateTime(strDate);
                    if (thisOrder.OrderDate > resultDate)
                    {
                        this.NotifyAction("Wrong information: result date cannot be before the order date", "Validation", true, "");
                        this.ServiceDialog.Show();
                        return;
                    }
                    service.ResultNotes = strResult;
                    service.ResultDate = resultDate;
                    service.ResultBy = resultBy;
                    List<OrderedService> services = new List<OrderedService>();
                    services.Add(service);
                    requestMgr.SaveOrderResult(services, this.ServiceOrderId, this.UserId);
                    this.PopulateRequest();
                    BindServices();
                    this.NotifyAction("Results has been saved", "Success", false, "javascript:HideModalPopup();return true;");
                    return;
                }
                else
                {
                    this.NotifyAction("Missing information, specify all the field", "Validation", true, "");
                    this.ServiceDialog.Show();
                    return;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Enters the result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        protected string EnterResult(object result)
        {
            if (ModuleId != thisOrder.TargetModuleId)
            {
                return "none";
            }

            return (!(result.ToString() != null && String.IsNullOrEmpty(result.ToString())) && result.ToString().ToLower() == "complete") ? "none" : "";
        }

        /// <summary>
        /// Exits the page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ExitPage(object sender, EventArgs e)
        {
            base.Session["thisOrder"] = null;
            Response.Redirect(RedirectUrl);
        }

        /// <summary>
        /// Handles the RowCommand event of the gridServiceRequested control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void gridServiceRequested_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        /// <summary>
        /// Handles the RowDataBound event of the gridServiceRequested control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void gridServiceRequested_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    OrderedService rowView = (OrderedService)e.Row.DataItem;
                    string serviceOrderId = rowView.Id.ToString();
                    string serviceId = rowView.ServiceId.ToString();
                    Label labelReportedbyName = e.Row.FindControl("labelReportedbyName") as Label;
                    Label labelRequestNotes = e.Row.FindControl("labelRequestNotes") as Label;
                    Button buttonResult = e.Row.FindControl("buttonResult") as Button;
                    buttonResult.OnClientClick = string.Format("javascript:ShowModalPopup({0},'{1}');return false;", rowView.Id, rowView.ServiceName);
                    if (rowView.ResultBy.HasValue)
                    {
                        string resultby = this.GetUserFullName(rowView.ResultBy.Value);
                        string resultDate = rowView.ResultDate.Value.ToString("dd-MMM-yyyy");
                        labelReportedbyName.Text = string.Format("{0} on {1}", resultby, resultDate);
                    }
                    else
                    {
                    }
                    string requestNotes = rowView.RequestNotes;
                    if (rowView.ResultNotes.Length > 200)
                    {
                        requestNotes = rowView.RequestNotes.Substring(0, 199);
                    }
                    labelRequestNotes.Text = requestNotes;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ref ex);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.TemplateControl.Error" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnError(EventArgs e)
        {
            base.OnError(e);
            Exception ex = Server.GetLastError();
            this.ShowErrorMessage(ref ex);
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Services >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Result Page";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Service Orders Result Page";
            if (Application["AppCurrentDate"] != null)
            {
                hdappcurrentdate.Value = Convert.ToDateTime(Application["AppCurrentDate"].ToString()).ToString("dd-MMM-yyyy"); ;
            }
            if (!IsPostBack)
            {
                base.Session["thisOrder"] = null;
                this.ServiceOrderId = Convert.ToInt32(Session["PatientVisitId"]);
                this.PatientId = Convert.ToInt32(Session["PatientClientId"]);
                if (ServiceOrderId > 0)
                {
                    this.PopulateRequest();
                }
                else
                {
                    System.Web.HttpContext.Current.ApplicationInstance.CompleteRequest();
                    Response.Redirect(RedirectUrl, true);
                }
                // this.PopulateControls();
                this.BindServices();

                this.BindDropdownResultBy();
                 txtReportedbyDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            else
            {
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
            btnExitPage.OnClientClick = string.Format("javascript:window.location='{0}'; return false;", this.RedirectUrl);
        }

        /// <summary>
        /// Shows the information image.
        /// </summary>
        /// <param name="notes">The notes.</param>
        /// <returns></returns>
        protected string ShowInfoImage(object notes)
        {
            return (!(notes.ToString() != null && String.IsNullOrEmpty(notes.ToString())) && notes.ToString().Length > 200) ? "" : "none";
        }

        /// <summary>
        /// Shows the result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        protected string ShowResult(object result)
        {
            return (!(result.ToString() != null && String.IsNullOrEmpty(result.ToString())) && result.ToString().ToLower() == "complete") ? "" : "none";
        }
        /// <summary>
        /// Binds the services.
        /// </summary>
        private void BindServices()
        {
            List<OrderedService> services = new List<OrderedService>();
            ServiceOrder order = this.thisOrder;
            if (null != order && null != order.Services && order.Services.Count > 0)
            {
                services = order.Services.Where(o => o.DeleteFlag == false).ToList();
            }
            else
            {
                services = new List<OrderedService>();
                services.Add(new OrderedService()
                {
                    DeleteFlag = true,
                    Id = -1,
                    RequestNotes = "",
                    Service = new Service()
                }
                    );
            };
            gridServiceRequested.DataSource = services.Where(o => o.DeleteFlag == false);
            gridServiceRequested.DataBind();
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

          
            String userId = "";
           
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
           
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

                BindManager.BindCombo(ddlReportedbyName, theDT, "Name", "UserId", "", userId);
                ListItem item = ddlReportedbyName.Items.FindByValue(userId);
                if (item == null)
                {
                    item = ddlReportedbyName.Items.FindByValue(this.UserId.ToString());
                }
                if (item != null)
                {
                    item.Selected = true;
                }
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
        /// Notifies the action.
        /// </summary>
        /// <param name="strMessage">The string message.</param>
        /// <param name="strTitle">The string title.</param>
        /// <param name="errorFlag">if set to <c>true</c> [error flag].</param>
        /// <param name="onOkScript">The on ok script.</param>
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
        private void PopulateRequest()
        {
            ServiceOrder order = requestMgr.GetServiceOrder(this.PatientId, this.ServiceOrderId);
            labelClinicalNotes.Text = order.ClinicalNotes;
            labellaborderedbydate.Text = order.OrderDate.ToString("dd-MMM-yyyy");
            labelOrderedbyname.Text = "Ordered by " + this.GetUserFullName(order.OrderedBy) + " in " + this.GetModuleName(order.ModuleId);
            //labelOrderStatus.Text = String.Format("Order Number : {0}"  ,order.OrderStatus;
            labelOrderNumber.Text = String.Format("{0} Order Number : {1}  | Status : {2} | Ordered by {3} on {4} from {5}", 
                this.GetModuleName(order.TargetModuleId), 
                order.OrderNumber, 
                order.OrderStatus,
                this.GetUserFullName(order.OrderedBy),
                order.OrderDate.ToString("dd-MMM-yyyy"),
                this.GetModuleName(order.ModuleId));
            thisOrder = order;
        }
        /// <summary>
        /// Shows the error message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private void ShowErrorMessage(ref Exception ex)
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
                SystemSetting.LogError(ex);
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
    }
}