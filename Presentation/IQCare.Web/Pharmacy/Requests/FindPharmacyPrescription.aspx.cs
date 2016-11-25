using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Pharmacy;
using Application.Presentation;
using Entities.PatientCore;
using Entities.Pharmacy;
using Entities.Common;
using System.Data;
using IQCare.Web.Pharmacy;

namespace IQCare.Web.Pharmacy.Request
{
    public partial class FindPharmacyPrescription : System.Web.UI.Page
    {
        AuthenticationManager Authentication = new AuthenticationManager();
        //private IPharmacyRequest requestMgr = (Iph)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");
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
        protected void grdPatienOrder_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdPatienOrder, "Select$" + e.Row.RowIndex);

                Label labelName = e.Row.FindControl("labelName") as Label;
                Prescription row = ((Prescription)e.Row.DataItem);
                if (labelName != null)
                {
                   

                    labelName.Text = string.Format("{0} {1} {2}", row.Client.FirstName, row.Client.MiddleName, row.Client.LastName);
                }
                Label labelOrderedBy = e.Row.FindControl("labelOrderedBy") as Label;
                if (labelOrderedBy != null)
                {
                    labelOrderedBy.Text = GetUserFullName(row.PrescribedBy);
                }
            }
        }

        protected void grdPatienOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            string theUrl = string.Empty;
            //DataTable theDT = (DataTable)grdPatienBill.DataSource;
            //  DataRow theDR = theDT.Rows[grdPatienBill.SelectedIndex];
            int orderId = int.Parse(grdPatienOrder.SelectedDataKey.Values["Id"].ToString());
            int patientId = int.Parse(grdPatienOrder.SelectedDataKey.Values["PatientId"].ToString());
            int moduleId = int.Parse(grdPatienOrder.SelectedDataKey.Values["ModuleId"].ToString());
            // patientID = Int32.Parse(theDR.ItemArray[0].ToString());
            base.Session["PatientId"] = patientId;
            base.Session["PatientVisitId"] = orderId;
            base.Session["TechnicalAreaId"] = moduleId;
            base.Session["TechnicalAreaName"] = this.GetModuleName(moduleId);
            theUrl = "~/Pharmacy/LabResultPage.aspx";
            //base.Session[SessionKey.LabClient] = null;
            Response.Redirect(theUrl, false);
        }

        protected void FindOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtlst_findOrder.SelectedValue == "Patient")
            {
                //base.Session[SessionKey.LabClient] = null;
                string theUrl;
                theUrl = string.Format("{0}?FormName={1}&mnuClicked={2}", "~//Laboratory/Request//FindLabPatient.aspx", "FindLabPatient", "FindLabPatient");
                //string.Format("{0}?PatientId={1}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString());
                Response.Redirect(theUrl, false);

            }

            else
            {
                //base.Session[SessionKey.LabClient] = null;
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
                return null; //!= base.Session[SessionKey.LabClient] ? "" : "none";
            }
        }
        private void PopulateData()
        {
            DateTime? _dateFrom = null;
            DateTime? _dateTo = null;
            List<Prescription> orders = new List<Prescription>();
            //if (null == base.Session[SessionKey.LabClient])
            //{
                if (ckbTodaysOrders.Checked)
                {
                    _dateFrom = DateTime.Today;
                    _dateTo = DateTime.Today.AddDays(1).AddMilliseconds(-1);
                }
                string orderStatus = rbtlst_findOrder.SelectedValue == "Pending" ? "1" : "3";
                PrescriptionFilter filter = new PrescriptionFilter()
                {
                    LocationId = this.LocationId,
                    DeleteFlag = false,
                    DateFrom = _dateFrom,
                    DateTo = _dateTo,
                    OrderStatus = orderStatus
                    
                };
                IPharmacyRepo repoMgr = (IPharmacyRepo)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BPharmacyRequest, BusinessProcess.Pharmacy");
                orders = repoMgr.GetAll(filter);
            //}
            //else
            //{
            //    List<KeyValuePair<string, Object>> param = base.Session[SessionKey.LabClient] as List<KeyValuePair<string, Object>>;
            //    int patientId = (int)param.Find(l => l.Key == "PatientID").Value;
            //    int location = (int)param.Find(l => l.Key == "LocationID").Value;
            //    lblname.Text = string.Format("{0} {1} {2}", (string)(param.Find(l => l.Key == "FirstName").Value.ToString())
            //        , (string)(param.Find(l => l.Key == "MiddleName").Value.ToString())
            //        , (string)(param.Find(l => l.Key == "LastName").Value.ToString()));
            //    lbldob.Text = Convert.ToDateTime(param.Find(l => l.Key == "DOB").Value.ToString()).ToString("dd-MMM-yyyy");
            //    lblFacilityID.Text = param.Find(l => l.Key == "FacilityID").Value.ToString();
            //    lblsex.Text = param.Find(l => l.Key == "Gender").Value.ToString();
            //    LabOrderFilter filter = new LabOrderFilter()
            //    {
            //        LocationId = location,
            //        DeleteFlag = false,
            //        DateFrom = null,
            //        DateTo = null,
            //        PatientId = patientId
            //    };
            //    ILabRepo repoMgr = (ILabRepo)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabRequest, BusinessProcess.Laboratory");
            //    orders = repoMgr.GetAll(filter);
            //}
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