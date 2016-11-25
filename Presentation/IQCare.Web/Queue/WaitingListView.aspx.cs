using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;
using IQCare.Web.UILogic;

namespace IQCare.Web.Queue
{
    public partial class WaitingListView : System.Web.UI.Page
    {
        private bool isError = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Waiting List";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            Session["PatientId"] = 0;
            if (!IsPostBack)
            {
                
                ddWaitingFor.Visible = false;
                lblWaitingfor.Visible = false;
                BindQueues();
            }
        }

        void BindQueues()
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("~\\XMLFiles\\AllMasters.con"));

            DataView theDV = new DataView();
            DataTable theDT = new DataTable();

            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "DeleteFlag=0 and ListName='Waiting List' and (SystemID=0 or SystemID=" + Session["SystemId"] + ")";
            if (theDV.Table != null)
            {
                theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddwaitingList, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
            }

        }
        protected void SelectedQueueChanged(object sender, EventArgs e)
        {

            ddWaitingFor.Visible = false;
            lblWaitingfor.Visible = false;
            if (ddwaitingList.SelectedItem.Value == "0")
            {
                grdWaitingList.DataSource = null;
                grdWaitingList.DataBind();

            }
            else
            {
                LoadWaitList();
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
                SystemSetting.LogError(ex);
                //lblError.Text = "An error has occured within IQCARE during processing. Please contact the support team.  " + ex.Message;
                //this.isError = this.divError.Visible = true;
                //Exception lastError = ex;
                //lastError.Data.Add("Domain", "Service Management");
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
        private void LoadWaitList()
        {
            IPatientRegistration PManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            int moduleID = Convert.ToInt32(Session["TechnicalAreaId"]);
            //System.Data.DataTable theDt = PManager.GetPatientsOnWaitingList(Convert.ToInt32(ddwaitingList.SelectedItem.Value), Convert.ToInt32(Session["TechnicalAreaId"]));
            System.Data.DataTable theDt = PManager.GetPatientsOnWaitingList(
                Convert.ToInt32(ddwaitingList.SelectedItem.Value),
                (moduleID > 0 ? moduleID : 0));
            Session["WaitlistPatients"] = theDt;
            //check whether we need to filter by current user for consultation 
            if (ddwaitingList.SelectedItem.Text == "Consultation")
            {
                this.PopulateUsersList();
                ddWaitingFor.Visible = true;
                lblWaitingfor.Visible = true;
                ddWaitingFor.SelectedValue = base.Session["AppUserId"].ToString();
            }

            grdWaitingList.DataSource = Session["WaitlistPatients"];

            grdWaitingList.DataBind();

        }
        void PopulateUsersList()
        {
            if (ddWaitingFor.Items.Count > 0) return;
            DataTable theUserDt;
            Iuser UManager = (Iuser)ObjectFactory.CreateInstance("BusinessProcess.Administration.BUser, BusinessProcess.Administration");
            theUserDt = (UManager.GetUserList()).Tables[0];
            DataColumn newColumn;
            newColumn = new DataColumn("Name");
            newColumn.Expression = " UserFirstName +' '+ UserLastName";
            theUserDt.Columns.Add(newColumn);
            //Add system admin who was removed at query level

            DataRow theAdminRow = theUserDt.NewRow();
            theAdminRow.SetField("UserID", 1);
            theAdminRow.SetField("UserFirstName", "System");
            theAdminRow.SetField("UserLastName", "Admin");
            theAdminRow.SetField("Status", "Active");
            theAdminRow.SetField("Name", "System Admin");
            theUserDt.Rows.Add(theAdminRow);

            DataView dv = theUserDt.DefaultView;
            dv.RowFilter = "Status = 'Active'";
            dv.Sort = "Name Asc";
            dv.ToTable("Selected", true, "UserID", "Name");

            DataTable theDT = dv.ToTable("Selected", true, "UserID", "Name");

            BindFunctions bindFunctions = new BindFunctions();
            bindFunctions.BindCombo(ddWaitingFor, theDT, "Name", "UserID");
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
        private int UserId
        {
            get
            {
                return Convert.ToInt32(Session["AppUserId"]);
            }
        }

        protected void SelectedWaitingForChanged(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)Session["WaitlistPatients"];
            DataView dv = dt.DefaultView;
            if (ddWaitingFor.SelectedValue != "0")
                dv.RowFilter = String.Format("WaitingFor={0} OR WaitingFor=0", ddWaitingFor.SelectedValue);
            grdWaitingList.DataSource = dv;
            grdWaitingList.DataBind();

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

        protected void grdWaitingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdWaitingList, "Select$" + e.Row.RowIndex);
            }
        }

        protected void grdWaitingList_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            for (int i = 0; i < grdWaitingList.Columns.Count; i++)
            {
                TableHeaderCell cell = new TableHeaderCell();
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = grdWaitingList.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox";
                cell.Controls.Add(txtSearch);
                row.Controls.Add(cell);
            }
            try { grdWaitingList.HeaderRow.Parent.Controls.AddAt(1, row); }
            catch { }
            if (grdWaitingList.Rows.Count < 5)
            {
                row.Style.Clear();
                row.Style.Add("display", "none");
            }
        }

        protected void ExitPage(object sender, EventArgs e)
        {
            
            base.Session["LAB_REQTEST"] = null;
            base.Session["OrderedLabs"] = null;
            Response.Redirect(string.Format("~/Patient/FindAdd.aspx?srvNm={0}&mod={1}",Session["TechnicalAreaName"].ToString(), Session["TechnicalAreaId"].ToString()));
        }

        protected void SelectedPatientChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["TechnicalAreaId"]) == 0) return;//do nothing if we are in records

            int technicalAreaID = (Convert.ToInt32(Session["TechnicalAreaId"]));
            string theUrl = string.Empty;
            int patientID = int.Parse(grdWaitingList.SelectedDataKey.Values["Ptn_PK"].ToString());
            int moduleID = int.Parse(grdWaitingList.SelectedDataKey.Values["ModuleID"].ToString());
            HttpContext.Current.Session["PatientId"] = patientID;
            HttpContext.Current.Session["PatientVisitId"] = 0;
            HttpContext.Current.Session["ServiceLocationId"] = 0;
            HttpContext.Current.Session["LabId"] = 0;
            Session["TechnicalAreaId"] = moduleID < 1 ? (Convert.ToInt32(Session["TechnicalAreaId"])) : moduleID;
            int WaitingListID = int.Parse(grdWaitingList.SelectedDataKey.Values["WaitingListID"].ToString());

            //remove patient from the waiting list
            IPatientRegistration PManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            PManager.ChangeWaitingListStatus(WaitingListID, 1, Convert.ToInt32(base.Session["AppUserId"]));

            Response.Redirect(string.Format("~/ClinicalForms/frmPatient_Home.aspx?srvNm={0}&mod={1}", Session["TechnicalAreaName"].ToString(), Session["TechnicalAreaId"].ToString()));
            //String theOrdScript;
            //theOrdScript = "<script language='javascript' id='openPatient'>\n";
            //theOrdScript += "window.opener.location.href = './ClinicalForms/frmPatient_Home.aspx';\n";
            //theOrdScript += "window.close();\n";
            //theOrdScript += "</script>\n";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "closePage", theOrdScript);
        }
    }
}