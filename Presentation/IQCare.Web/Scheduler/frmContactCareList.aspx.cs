using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Presentation;
namespace IQCare.Web.Scheduler
{
    public partial class ContactCareList : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Nisha Nagpal
        // Written Date      : 27th Sep 2006
        // Modification Date : 
        // Description       : Contact Care Tracking List
        //
        /// /////////////////////////////////////////////////////////////////

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["patientid"] = Convert.ToInt32(Request.QueryString["PatientId"]);
                // GetContactListforID(Convert.ToInt32(ViewState["patientid"]));
            }

        }
        #region "User Functions"

        // Function for Binding Grid
        /* protected void GetContactListforID(int Patient_ID)
         {
             IContactCare CareManager;
             try
             {
                 CareManager = (IContactCare)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BContactCare,BusinessProcess.Scheduler");
                 DataSet theDS = (DataSet)CareManager.GetContactListforID(Patient_ID);
                 this.grdContactCareList.DataSource = AlterTable(theDS);
                 if (ViewState["grdDataSource"] == null)
                     ViewState["grdDataSource"] = theDS.Tables[0];
                 ViewState["SortDirection"] = "Desc";
                 BindGrid();
                 HideGrid();
             }
             catch (Exception err)
             {
                 MsgBuilder theBuilder = new MsgBuilder();
                 theBuilder.DataElements["MessageText"] = err.Message.ToString();
                 IQCareMsgBox.Show("#C1", theBuilder, this);
                 return;
             }
             finally
             {
                 CareManager = null;
             }
         }
         */
        // Concatinating EnrollmentNo with other fields and bindind it to Grid
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

        //Hiding some fields of the Grid at runtime
        private void HideGrid()
        {
            grdContactCareList.Columns[4].Visible = false;
            grdContactCareList.Columns[5].Visible = false;
            grdContactCareList.Columns[6].Visible = false;
        }

        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Patient EnrollmentID";
            theCol0.DataField = "EnrollmentNo";
            theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.SortExpression = "EnrollmentNo";
            theCol0.ReadOnly = true;
            theCol0.Visible = true;


            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Existing PatientID";
            theCol1.DataField = "PatientClinicID";
            theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.SortExpression = "PatientClinicID";
            theCol1.ReadOnly = true;
            theCol1.Visible = true;

            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Patient Name";
            theCol2.DataField = "name";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.SortExpression = "name";
            theCol2.ReadOnly = true;

            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Tracking Date";
            theCol3.DataField = "CreateDate";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.SortExpression = "CreateDate";
            theCol3.ReadOnly = true;

            BoundField theCol4 = new BoundField();
            theCol4.HeaderText = "CareEndedID";
            theCol4.DataField = "CareEndedID";
            theCol4.ItemStyle.CssClass = "textstyle";
            theCol4.ReadOnly = false;

            BoundField theCol5 = new BoundField();
            theCol5.HeaderText = "Patient ID";
            theCol5.DataField = "Ptn_Pk";
            theCol5.ItemStyle.CssClass = "textstyle";

            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "TrackingID";
            theCol6.DataField = "trackingId";
            theCol6.ItemStyle.CssClass = "textstyle";
            theCol6.ReadOnly = true;

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            grdContactCareList.Columns.Add(theCol0);
            grdContactCareList.Columns.Add(theCol1);
            grdContactCareList.Columns.Add(theCol2);
            grdContactCareList.Columns.Add(theCol3);
            grdContactCareList.Columns.Add(theCol4);
            grdContactCareList.Columns.Add(theCol5);
            grdContactCareList.Columns.Add(theCol6);

            grdContactCareList.Columns.Add(theBtn);
            grdContactCareList.DataBind();

        }
        #endregion

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string theUrl = string.Format("{0}PatientId={1}", "frmContactCare_Tracking.aspx?name=" + "Add" + "&", ViewState["patientid"]);
            Response.Redirect(theUrl);
        }

        protected void grdContactCareList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.Backcolor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.backgroundcolor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdContactCareList, "Select$" + e.Row.RowIndex.ToString()));
            }
        }


        protected void grdContactCareList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow theRow = grdContactCareList.Rows[e.NewSelectedIndex];
            int theCareEndId = Convert.ToInt32(grdContactCareList.Rows[0].Cells[4].Text);
            int theTrackingId = Convert.ToInt32(grdContactCareList.Rows[0].Cells[6].Text);
            string theUrl = string.Format("{0}PatientId={1}&CareEndedId={2}&TrackingId={3}", "frmContactCare_Tracking.aspx?name=" + "Edit" + "&", Convert.ToInt32(ViewState["patientid"]), theCareEndId, theTrackingId);
            Response.Redirect(theUrl);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", Request.QueryString["PatientId"].ToString());
            Response.Redirect(theUrl);
        }
        protected void grdContactCareList_Sorting(object sender, GridViewSortEventArgs e)
        {
            IQCareUtils clsUtil = new IQCareUtils();
            DataView theDV;
            if (ViewState["SortDirection"].ToString() == "Asc")
            {
                theDV = clsUtil.GridSort((DataTable)ViewState["grdDataSource"], e.SortExpression, ViewState["SortDirection"].ToString());
                ViewState["SortDirection"] = "Desc";
            }
            else
            {
                theDV = clsUtil.GridSort((DataTable)ViewState["grdDataSource"], e.SortExpression, ViewState["SortDirection"].ToString());
                ViewState["SortDirection"] = "Asc";
            }
            grdContactCareList.Columns.Clear();
            grdContactCareList.DataSource = theDV;
            BindGrid();
            HideGrid();

        }
    }
}