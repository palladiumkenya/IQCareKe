using Application.Presentation;
using Entities.Lab;
using Entities.PatientCore;
using Interface.Laboratory;
using IQCare.Web.UILogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IQCare.Web.Laboratory.Request
{
    public partial class FindLabOrder : Page
    {
        AuthenticationManager Authentication = new AuthenticationManager();
        private ILabRequest requestMgr = (ILabRequest)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");
        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Laboratory";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
            Session["PatientId"] = 0;
            Session["TechnicalAreaId"] = null;
            Session["TechnicalAreaName"] = null;
            if (!IsPostBack)
            {
                this.PopulateData();
            }
        }

        protected void ckbTodaysOrders_CheckedChanged(object sender, EventArgs e)
        {
            this.PopulateData();
        }



        protected void grdPatienOrder_DataBound(object sender, EventArgs e)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            for (int i = 0; i < grdPatienOrder.Columns.Count; i++)
            {
                TableHeaderCell cell = new TableHeaderCell();
                TextBox txtSearch = new TextBox();
                txtSearch.Attributes["placeholder"] = grdPatienOrder.Columns[i].HeaderText;
                txtSearch.CssClass = "search_textbox";
                cell.Controls.Add(txtSearch);
                row.Controls.Add(cell);
            }
            try { grdPatienOrder.HeaderRow.Parent.Controls.AddAt(1, row); }
            catch { }
            if (grdPatienOrder.Rows.Count < 5)
            {
                row.Style.Clear();
                row.Style.Add("display", "none");
            }
        }
        private DataTable UserList
        {
            get
            {
                DataSet theDS = new DataSet();
                theDS.ReadXml(MapPath("..\\..\\XMLFiles\\ALLMasters.con"));

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
        protected void grdPatienOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdPatienOrder, "Select$" + e.Row.RowIndex);

                Label labelName = e.Row.FindControl("labelName") as Label;
                LabOrder row = ((LabOrder)e.Row.DataItem);
                if (labelName != null)
                {
                   

                    labelName.Text = string.Format("{0} {1} {2}", row.Client.FirstName, row.Client.MiddleName, row.Client.LastName);
                }
                Label labelOrderedBy = e.Row.FindControl("labelOrderedBy") as Label;
                if (labelOrderedBy != null)
                {
                    labelOrderedBy.Text = GetUserFullName(row.OrderedBy);
                }
            }
        }

        protected void grdPatienOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            string theUrl = string.Empty;
            //DataTable theDT = (DataTable)grdPatienBill.DataSource;
            //  DataRow theDR = theDT.Rows[grdPatienBill.SelectedIndex];
            int orderId = int.Parse(grdPatienOrder.SelectedDataKey.Values["Id"].ToString());
            int patientPk = int.Parse(grdPatienOrder.SelectedDataKey.Values["PatientPk"].ToString());
            int moduleId = int.Parse(grdPatienOrder.SelectedDataKey.Values["ModuleId"].ToString());
            if(moduleId <= 0)
            {
                EnrollmentService es = new EnrollmentService(patientPk);
               List<PatientEnrollment> pe =  es.GetPatientEnrollment(CurrentSession.Current);
                if(pe!= null)
                {
                    moduleId = pe.FirstOrDefault().ServiceAreaId;
                    base.Session["TechnicalAreaName"] = pe.FirstOrDefault().ServiceArea.Name;
                }
            }else
            {
                base.Session["TechnicalAreaName"] = this.GetModuleName(moduleId);
            }
            // patientID = Int32.Parse(theDR.ItemArray[0].ToString());
            base.Session["PatientId"] = patientPk;
            base.Session["PatientVisitId"] = orderId;
            base.Session["TechnicalAreaId"] = moduleId;
          
            Session["PatientInformation"] = null;
            theUrl = "~/Laboratory/LabResultPage.aspx";
            base.Session[SessionKey.LabClient] = null;
            Response.Redirect(theUrl, false);
        }

        protected void FindOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtlst_findOrder.SelectedValue == "Patient")
            {
                base.Session[SessionKey.LabClient] = null;
                string theUrl;
                theUrl = string.Format("{0}?FormName={1}&mnuClicked={2}", "~//Laboratory/Request//FindLabPatient.aspx", "FindLabPatient", "FindLabPatient");
                //string.Format("{0}?PatientId={1}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString());
                Response.Redirect(theUrl, false);

            }

            else
            {
                base.Session[SessionKey.LabClient] = null;
                //this.LoadOpenBills();
                this.PopulateData();
            }
        }
        int LocationId
        {
            get
            {
                return Convert.ToInt32(Session["AppLocationId"]);
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
        protected string ShowPatientDetail
        {
            get
            {
                return null != base.Session[SessionKey.LabClient] ? "" : "none";
            }
        }
        private void PopulateData()
        {
            DateTime? _dateFrom = null;
            DateTime? _dateTo = null;
            List<LabOrder> orders = new List<LabOrder>();
            if (null == base.Session[SessionKey.LabClient])
            {
                if (ckbTodaysOrders.Checked)
                {
                    _dateFrom = DateTime.Today;
                    _dateTo = DateTime.Today.AddDays(1).AddMilliseconds(-1);
                }
                string orderStatus = rbtlst_findOrder.SelectedValue == "Pending" ? "Pending" : "";
                LabOrderFilter filter = new LabOrderFilter()
                {
                    LocationId = this.LocationId,
                    DeleteFlag = false,
                    DateFrom = _dateFrom,
                    DateTo = _dateTo,
                    OrderStatus = orderStatus
                    
                };
                ILabRepo repoMgr = (ILabRepo)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");
                orders = repoMgr.GetAll(filter);
            }
            else
            {
                List<KeyValuePair<string, object>> param = base.Session[SessionKey.LabClient] as List<KeyValuePair<string, object>>;
                int patient = (int)param.Find(l => l.Key == "Patient").Value;
                int patientPk = (int)param.Find(l => l.Key == "PatientID").Value;
                int location = (int)param.Find(l => l.Key == "LocationID").Value;
                lblname.Text = string.Format("{0} {1} {2}", (string)(param.Find(l => l.Key == "FirstName").Value.ToString())
                    , (string)(param.Find(l => l.Key == "MiddleName").Value.ToString())
                    , (string)(param.Find(l => l.Key == "LastName").Value.ToString()));
                //set session Greencard patient_Id
                Session["patient"] = patient;

                //calculate age
                DateTime now = DateTime.Today;
                int age = now.Year - (Convert.ToDateTime(param.Find(l => l.Key == "DOB").Value).Year);
                
                

                lbldob.Text = Convert.ToDateTime(param.Find(l => l.Key == "DOB").Value.ToString()).ToString("dd-MMM-yyyy");
                lbldob.Text = lbldob.Text  + "    Age: " + age.ToString();
                lblFacilityID.Text = param.Find(l => l.Key == "FacilityID").Value.ToString();
                lblsex.Text = param.Find(l => l.Key == "Gender").Value.ToString();
                LabOrderFilter filter = new LabOrderFilter()
                {
                    LocationId = location,
                    DeleteFlag = false,
                    DateFrom = null,
                    DateTo = null,
                    PatientId = patientPk
                };
                ILabRepo repoMgr = (ILabRepo)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");
                orders = repoMgr.GetAll(filter);
            }
            grdPatienOrder.DataSource = orders;
            grdPatienOrder.DataBind();
        }

        /// <summary>
        /// Handles the RowCommand event of the grdPatienOrder control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewCommandEventArgs"/> instance containing the event data.</param>
        protected void grdPatienOrder_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}