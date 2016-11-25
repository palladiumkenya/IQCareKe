using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Scheduler;
namespace IQCare.Web.Scheduler
{
    public partial class HomeVisitList : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Nisha Nagpal
        // Written Date      : 18th Oct 2006
        // Modification Date : 
        // Description       : Home Visit List
        //
        /// /////////////////////////////////////////////////////////////////

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["patientid"] = Convert.ToInt32(Request.QueryString["PatientId"]);
                GetFieldsforGrid(Convert.ToInt32(ViewState["patientid"]));
            }
        }

        #region "User Functions"

        protected void GetFieldsforGrid(int Patient_ID)
        {
            IHomeVisit HomeVisitManager;
            try
            {
                HomeVisitManager = (IHomeVisit)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BHomeVisit,BusinessProcess.Scheduler");
                DataSet theDS = (DataSet)HomeVisitManager.GetFieldsforGrid(Patient_ID);
                this.grdHomeVisitList.DataSource = AlterTable(theDS);
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
                HomeVisitManager = null;
            }

        }

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
            grdHomeVisitList.Columns[4].Visible = false;
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
            theCol4.HeaderText = "Patient ID";
            theCol4.DataField = "Ptn_Pk";
            theCol4.ItemStyle.CssClass = "textstyle";

            ButtonField theBtn = new ButtonField();
            theBtn.ButtonType = ButtonType.Link;
            theBtn.CommandName = "Select";
            theBtn.HeaderStyle.CssClass = "textstylehidden";
            theBtn.ItemStyle.CssClass = "textstylehidden";

            grdHomeVisitList.Columns.Add(theCol0);
            grdHomeVisitList.Columns.Add(theCol1);
            grdHomeVisitList.Columns.Add(theCol2);
            grdHomeVisitList.Columns.Add(theCol3);
            grdHomeVisitList.Columns.Add(theCol4);

            grdHomeVisitList.Columns.Add(theBtn);
            grdHomeVisitList.DataBind();


        }

        #endregion
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl;
            theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", Request.QueryString["PatientId"].ToString());
            Response.Redirect(theUrl);
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string theUrl = string.Format("{0}PatientId={1}", "frmScheduler_HomeVisit.aspx?name=" + "Add" + "&", ViewState["patientid"]);
            Response.Redirect(theUrl);
        }
        /// <summary>
        /// Handles the RowDataBound event of the grdHomeVisitList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewRowEventArgs"/> instance containing the event data.</param>
        protected void grdHomeVisitList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';this.Backcolor='#666699';");
                e.Row.Attributes.Add("onmouseout", "this.backgroundcolor='';");
                e.Row.Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(grdHomeVisitList, "Select$" + e.Row.RowIndex.ToString()));
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanging event of the grdHomeVisitList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSelectEventArgs"/> instance containing the event data.</param>
        protected void grdHomeVisitList_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            GridViewRow theRow = grdHomeVisitList.Rows[e.NewSelectedIndex];
            string CreateDate = grdHomeVisitList.Rows[0].Cells[3].Text;
            string theUrl = string.Format("{0}PatientId={1}&CreateDate={2}", "frmScheduler_HomeVisit.aspx?name=" + "Edit" + "&", Convert.ToInt32(ViewState["patientid"]), CreateDate);
            Response.Redirect(theUrl);
        }
        /// <summary>
        /// Handles the Sorting event of the grdHomeVisitList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="GridViewSortEventArgs"/> instance containing the event data.</param>
        protected void grdHomeVisitList_Sorting(object sender, GridViewSortEventArgs e)
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
            grdHomeVisitList.Columns.Clear();
            grdHomeVisitList.DataSource = theDV;
            BindGrid();
            HideGrid();
        }
    }
}