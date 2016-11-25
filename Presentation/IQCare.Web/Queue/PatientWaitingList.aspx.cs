using Application.Common;
using Application.Presentation;
using Entities.Queue;
using Interface.Clinical;
using IQCare.Web.UILogic;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.Queue
{
    public partial class PatientWaitingList : Page
    {
        private bool isError = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;
            Session["dtWaitingList"] = null;
            Session["WLTechnicalArea"] = null;
            Session["WLTechnicalAreaName"] = null;
            Session["WLPatientID"] = 0;
            string pID = Session["PatientId"].ToString();
            if (pID == "0")
                pID = Request.QueryString["PID"];
            if (Request.QueryString["hashtag"] == null)
            {
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                Response.Redirect("~/frmFacilityHome.aspx", true);
            }
            else
            {
                string returnUrl = Request.QueryString["hashtag"].Replace(" ","+");
                Utility objUtil = new Utility();
                returnUrl = objUtil.Decrypt((returnUrl));
                btnBack.OnClientClick = String.Format("javascript:window.location = '../{0}';return false;", returnUrl);
            }
            if (Request.QueryString["mod"] != null)
            {
                Master.ExecutePatientLevel = false;
            }
            else
            {
                Master.ExecutePatientLevel = true;
            }
            if (Session["PatientInformation"] == null)
            {
                IPatientHome PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                //System.Data.DataSet theDS = PatientManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]));
               DataSet theDS = PatientManager.GetPatientDetails(Convert.ToInt32(pID), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
                PatientManager = null;
                Session["PatientInformation"] = theDS.Tables[0];
            }
            BindQueues();
            DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
            if (Request.QueryString["srvNm"] != null)
            {
               lblTechnicalArea.Text = Request.QueryString["srvNm"];
                Session["WLTechnicalArea"] = Request.QueryString["mod"];
                Session["WLTechnicalAreaName"] = Request.QueryString["srvNm"];
                Session["WLPatientID"] = Request.QueryString["PID"];
            }
            else
            {
              lblTechnicalArea.Text = Session["TechnicalAreaName"].ToString();
                Session["WLTechnicalArea"] = Session["TechnicalAreaId"];
                Session["WLTechnicalAreaName"] = Session["TechnicalAreaName"];
                Session["WLPatientID"] = HttpContext.Current.Session["PatientId"];
            }

            LoadPatientsWaitList(Convert.ToInt32(HttpContext.Current.Session["WLPatientID"]));
        }
       //private void NotifyAction(string strMessage, string strTitle, bool errorFlag, string onOkScript = "")
        //{
        //    lblNoticeInfo.Text = strMessage;
        //    lblNotice.Text = strTitle;
        //    lblNoticeInfo.ForeColor = (errorFlag) ? System.Drawing.Color.DarkRed : System.Drawing.Color.DarkGreen;
        //    lblNoticeInfo.Font.Bold = true;
        //    imgNotice.ImageUrl = (errorFlag) ? "~/images/mb_hand.gif" : "~/images/mb_information.gif";
        //    btnOkAction.OnClientClick = "";
        //    if (onOkScript != "")
        //    {
        //        btnOkAction.OnClientClick = onOkScript;
        //    }
        //    this.notifyPopupExtender.Show();
        //}
        /// <summary>
        /// Loads the patients wait list.
        /// </summary>
        /// <param name="patientID">The patient identifier.</param>
        private void LoadPatientsWaitList(int patientID)
        {
            IPatientHome PManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataTable theDt = PManager.GetPatientWaitList(patientID);

            grdWaitingList.DataSource = theDt;
            Session["dtWaitingList"] = theDt;
            grdWaitingList.DataBind();

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
                theDT = theUtils.CreateTableFromDataView(theDV);
                BindManager.BindCombo(ddWList, theDT, "Name", "ID");
                theDV.Dispose();
                theDT.Clear();
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
        protected void QueuePatient(object sender, EventArgs e)
        {
            DataRow[] foundRows;
            //validate waiting list item selected
            if (ddWList.SelectedItem.Text.Trim() == "Select")
            {
                ddWList.BorderColor = System.Drawing.Color.Red;
                ddWList.BackColor = System.Drawing.Color.Orange;
                return;
            }
            else
            {
                ddWList.BorderColor = System.Drawing.Color.Black;
                ddWList.BackColor = System.Drawing.Color.White;
            }




            DataTable theDT = new DataTable();
            theDT = (DataTable)Session["dtWaitingList"];
            foundRows = theDT.Select(string.Format("ListID='{0}' and ModuleID='{1}' and RowStatus=0", ddWList.SelectedItem.Value, Session["WLTechnicalArea"]));

            if (foundRows.Length < 1)
            {
                IPatientQueue mgr = (IPatientQueue)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientQueue, BusinessProcess.Clinical");
              int wlId =  mgr.QueuePatient(
                     Convert.ToInt32(Session["WLPatientID"]),
                     Convert.ToInt32(ddWList.SelectedItem.Value),
                     QueueStatus.Pending,
                     (QueuePriority)int.Parse(ddPriority.SelectedItem.Value),
                     int.Parse(Session["WLTechnicalArea"].ToString()),
                     UserId                     
                    );
              if (wlId > 0)
              {
                 DataRow theDR = theDT.NewRow();

                theDR["ListName"] = ddWList.SelectedItem.Text;
                theDR["ModuleName"] = Session["WLTechnicalAreaName"].ToString();
                theDR["ModuleID"] = int.Parse(Session["WLTechnicalArea"].ToString());
                theDR["AddedBy"] = Session["AppUserName"].ToString();
                theDR["ListID"] = ddWList.SelectedItem.Value;
                theDR["Priority"] = ddPriority.SelectedItem.Value;
                theDR["RowStatus"] = QueueStatus.Pending;
                theDR["Persisted"] = 1;
                theDR["WaitingListID"] = wlId;
                theDT.Rows.Add(theDR);

                Session["dtWaitingList"] = theDT;
                BindWaitingGrid();

                IQCareMsgBox.NotifyAction("Patient has been added to the " + ddWList.SelectedItem.Text + " waiting list", "Waiting List!",false, this,"");
              }
             
            }
            else
            {
                IQCareMsgBox.NotifyAction("Patient is already in " + ddWList.SelectedItem.Text + " waiting list", "Waiting List!", false, this, "");

                return;
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

        protected void SelectedPatientChanged(object sender, EventArgs e)
        {

        }

        protected void grdWaitingList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable theDT = (DataTable)Session["dtWaitingList"];

            DataRow rowDelete = theDT.Rows[e.RowIndex];

            if (Convert.ToInt32(rowDelete["Persisted"]) == 1)
            {
                rowDelete["RowStatus"] = QueueStatus.Deleted;
                rowDelete.AcceptChanges();
                int waitingListId = Convert.ToInt32(rowDelete["WaitingListID"]);

                 IPatientRegistration PManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                 PManager.ChangeWaitingListStatus(waitingListId, (int) QueueStatus.Deleted, this.UserId);
            }
            else
            {
                theDT.Rows.RemoveAt(e.RowIndex);
            }
          
        }
        void BindWaitingGrid()
        {
            DataTable theMainDT = (DataTable)Session["dtWaitingList"];
            DataView dv = theMainDT.DefaultView;
            dv.RowFilter = string.Format("RowStatus = '{0}'", (int)QueueStatus.Pending);
            DataTable theDT = dv.ToTable();

            grdWaitingList.DataSource = theDT;
            grdWaitingList.DataBind();


        }

        protected void grdWaitingList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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

       

    }
}