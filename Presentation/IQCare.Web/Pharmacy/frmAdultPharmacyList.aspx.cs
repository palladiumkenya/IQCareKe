using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

using Application.Common;
using Application.Presentation;
using Interface.Pharmacy;

namespace IQCare.Web.Pharmacy
{
    public partial class AdultPharmacyList : System.Web.UI.Page
    {
        //int PatientId = Convert.ToInt32(Request.QueryString["PatientId"]);
        #region "Bind Grid"

        private DataTable AlterTable(DataSet theDS)
        {
            theDS.Tables[0].Columns.Add("EnrollmentNo");
            foreach (DataRow dr in theDS.Tables[0].Rows)
            {
                string theEnroll = Session["AppCountryId"].ToString() + "-" + Session["AppPosID"].ToString() + "-" + Session["AppSatelliteId"].ToString() + "-" + dr["PatientEnrollmentID"].ToString();
                dr["EnrollmentNo"] = theEnroll;
            }
            return theDS.Tables[0];
        }



        public void BindPharmacyGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.DataField = "ptn_pk";
            theCol0.HeaderText = "ptn_pk";
            theCol0.HeaderStyle.CssClass = "textstyle";
            theCol0.ReadOnly = true;

            BoundField theCol1 = new BoundField();
            theCol1.DataField = "FirstName";
            theCol1.HeaderText = "First Name";
            theCol1.HeaderStyle.CssClass = "textstyle";
            theCol1.ReadOnly = true;

            BoundField theCol2 = new BoundField();
            theCol2.DataField = "LastName";
            theCol2.HeaderText = "Last Name";
            theCol2.HeaderStyle.CssClass = "textstyle";
            theCol2.ReadOnly = true;

            BoundField theCol3 = new BoundField();
            theCol3.DataField = "EnrollmentNo";
            theCol3.HeaderText = "Enrollment ID";
            theCol3.HeaderStyle.CssClass = "textstyle";
            theCol3.ReadOnly = true;

            BoundField theCol4 = new BoundField();
            theCol4.DataField = "PatientClinicID";
            theCol4.HeaderText = "Patient Clinic ID";
            theCol4.HeaderStyle.CssClass = "textstyle";
            theCol4.ReadOnly = true;

            //BoundField theCol5 = new BoundField();
            //theCol5.DataField = "RegistrationDate";
            //theCol5.HeaderText = "Registration Date";
            //theCol5.HeaderStyle.CssClass = "textstyle";
            //theCol5.ReadOnly = true;

            BoundField theCol5 = new BoundField();
            theCol5.DataField = "OrderedByDate";
            theCol5.HeaderText = "Ordered Date";
            theCol5.SortExpression = "OrderedByDate";
            theCol5.HeaderStyle.CssClass = "textstyle";
            theCol5.ReadOnly = true;

            BoundField theCol6 = new BoundField();
            theCol6.DataField = "ptn_pharmacy_pk";
            theCol6.HeaderText = "ptn_pharmacy_pk";
            theCol6.HeaderStyle.CssClass = "textstyle";
            theCol6.ReadOnly = true;



            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            grdAdultPharmacyList.Columns.Add(theCol0);
            grdAdultPharmacyList.Columns.Add(theCol1);
            grdAdultPharmacyList.Columns.Add(theCol2);
            grdAdultPharmacyList.Columns.Add(theCol3);
            grdAdultPharmacyList.Columns.Add(theCol4);
            //grdAdultPharmacyList.Columns.Add(theCol5);
            grdAdultPharmacyList.Columns.Add(theCol5);
            grdAdultPharmacyList.Columns.Add(theCol6);
            grdAdultPharmacyList.Columns.Add(theBtn);

            grdAdultPharmacyList.DataBind();
            grdAdultPharmacyList.Columns[0].Visible = false;
            grdAdultPharmacyList.Columns[6].Visible = false;
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            IDrug PharmacyManager;
            int PatientID;

            PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
            ViewState["PatientId"] = PatientID;
            try
            {
                if (!IsPostBack)
                {
                    PharmacyManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug, BusinessProcess.Pharmacy");
                    DataSet theDS = (DataSet)PharmacyManager.GetPharmacyList(PatientID);

                    ////if (theDS.Tables.Count > 0 && theDS.Tables[0].Rows.Count == 0)
                    ////{
                    ////    IQCareMsgBox.Show("NoPharmacyRecordExists", this);
                    ////    return;
                    ////}
                    ViewState["grddata"] = theDS;
                    ViewState["gridSortDirection"] = "Desc";
                    this.grdAdultPharmacyList.DataSource = AlterTable(theDS); //.Tables[0].DefaultView);
                    this.grdAdultPharmacyList.DataBind();
                    BindPharmacyGrid();
                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", ViewState["PatientId"].ToString());
            Response.Redirect(theUrl);
        }

        protected void grdAdultPharmacyList_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView theGD = (GridView)sender;
            IQCareUtils SortManager = new IQCareUtils();
            DataView theDV;
            if (ViewState["gridSortDirection"].ToString() == "Asc")
            {
                theDV = SortManager.GridSort((DataTable)ViewState["grddata"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
                ViewState["gridSortDirection"] = "Desc";
            }
            else
            {
                theDV = SortManager.GridSort((DataTable)ViewState["grddata"], e.SortExpression.ToString(), ViewState["gridSortDirection"].ToString());
                ViewState["gridSortDirection"] = "Asc";
            }

            theGD.Columns.Clear();
            theGD.DataSource = theDV;
            BindPharmacyGrid();
        }

        protected void grdAdultPharmacyList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            int thePage = grdAdultPharmacyList.PageIndex;
            int thePageSize = grdAdultPharmacyList.PageSize;

            GridViewRow theRow = grdAdultPharmacyList.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;

            int PharmacyID = Convert.ToInt32(theRow.Cells[6].Text.ToString());

            int PatientID = Convert.ToInt32(theRow.Cells[0].Text.ToString());

            string theUrl = string.Format("{0}PharmacyID={1}&PatientId={2}", "frmAdultPharmacy.aspx?name=" + "Edit" + "&", PharmacyID, PatientID);
            //string theUrl = string.Format("{0}?PharmacyID={1}&PatientId={2}", "frmAdultPharmacy.aspx?name=" + "Edit" + "&", PharmacyID,PatientID);
            Response.Redirect(theUrl);

        }

        protected void grdAdultPharmacyList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdAdultPharmacyList, "Select$" + e.Row.RowIndex.ToString()));
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string url;
            int PatientId = Convert.ToInt32(ViewState["PatientId"]);
            url = string.Format("{0}PatientID={1}", "frmAdultPharmacy.aspx?name=" + "Add" + "&", PatientId);
            Response.Redirect(url);
        }
    }
}