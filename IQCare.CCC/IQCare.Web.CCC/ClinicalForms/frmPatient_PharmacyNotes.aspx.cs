using System;
using System.Data;
using System.Web.UI.WebControls;
using Application.Presentation;

//using Interface.Security;
//using Interface.Reports;
using Interface.Clinical;
using Interface.Pharmacy;

namespace IQCare.Web.Clinical
{
    public partial class PharmacyNotes : BasePage
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : John Macharia
        // Written Date      : 26th Jul 2012
        // Description       : Pharmacy Notes Form
        //
        /// /////////////////////////////////////////////////////////////////

        #region "UserFunctions"

        private void Init_Form()
        {
            string strPatientEnrollmentId = string.Empty;
            IPatientHome PatientManager;
            PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataSet theDS = PatientManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
            PatientManager = null;

            #region "Patient Data"

            Session["PatientInformation"] = theDS.Tables[0];

            #endregion "Patient Data"

            #region "Fill Details"

            if (theDS.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(Session["SystemId"]) == 1)
                {
                    lblpatientname.Text = theDS.Tables[0].Rows[0]["LastName"].ToString() + ", " + theDS.Tables[0].Rows[0]["FirstName"].ToString();
                    lblage.Text = theDS.Tables[0].Rows[0]["AGEINYEARMONTH"].ToString();
                    lblgender.Text = theDS.Tables[0].Rows[0]["SexNM"].ToString();
                    if (theDS.Tables[19].Rows.Count > 0)
                    {
                        if (theDS.Tables[19].Rows[0]["ART/PalliativeCare"].ToString() == "Care Ended")
                        {
                            lblptnstatus.Text = "Care Ended";
                        }
                        else if (theDS.Tables[19].Rows[0]["ART/PalliativeCare"].ToString() != "Care Ended")
                        {
                            lblptnstatus.Text = "Active";
                        }

                        DataTable dt = new DataTable();
                        dt = theDS.Tables[42];
                        if (theDS.Tables[42].Rows.Count > 0)
                        {
                            if (dt.Rows[0]["PatientExitReason"].ToString() == "93")
                            {
                                lblptnstatus.Text = "Care Ended";
                            }
                        }
                    }
                }
                else
                {
                }
            }

            #endregion "Fill Details"

            IDrug PNotes;
            PNotes = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug, BusinessProcess.Pharmacy");
            System.Data.DataSet thePN = PNotes.GetPharmacyNotes(Convert.ToInt32(Session["PatientId"]));
            PNotes = null;

            Session["GrdData"] = thePN.Tables[0];
            Session["SortDirection"] = "Asc";

            grdPharmacyNotes.DataSource = thePN.Tables[0];
            grdPharmacyNotes.DataBind();
        }

        #endregion "UserFunctions"

        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "Date";
            theCol0.DataField = "OrderedByDate";
            //theCol0.SortExpression = "DispensedByDate";
            theCol0.ItemStyle.Width = Unit.Percentage(2);
            //theCol0.HeaderStyle.ForeColor = System.Drawing.Color.Blue;
            //theCol0.ItemStyle.CssClass = "textstyle";
            theCol0.ReadOnly = true;

            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Pharmacy Notes";
            theCol1.DataField = "PharmacyNotes";
            //theCol1.HeaderStyle.ForeColor = System.Drawing.Color.Blue;
            //theCol1.SortExpression = "lastname";
            //theCol1.ItemStyle.CssClass = "textstyle";
            theCol1.ItemStyle.Width = Unit.Percentage(8);
            theCol1.ReadOnly = true;

            grdPharmacyNotes.Columns.Add(theCol0);
            grdPharmacyNotes.Columns.Add(theCol1);

            grdPharmacyNotes.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Ajax.Utility.RegisterTypeForAjax(typeof(ClinicalForms_frmPatient_PharmacyNotes));
            if (!IsPostBack)
            {
                Init_Form();
                BindGrid();
            }
        }

        protected void grdPharmacyNotes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string theUrl = string.Empty;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.BackColor = System.Drawing.Color.White;
                e.Row.Attributes.Add("onmouseover", "this.style.cursor='hand';");
            }
        }

        protected void grdPharmacyNotes_Sorting(object sender, GridViewSortEventArgs e)
        {
            //IQCareUtils clsUtil = new IQCareUtils();
            //DataView theDV;

            SortAndSetDataInGrid(e.SortExpression);

            if (Session["SortDirection"].ToString() == "Asc")
            {
                Session["SortDirection"] = "Desc";
            }
            else
            {
                Session["SortDirection"] = "Asc";
            }
        }

        private void SortAndSetDataInGrid(String SortExpression)
        {
            IQCareUtils clsUtil = new IQCareUtils();
            DataView theDV;

            if (SortExpression == "OrderedByDate")
            {
                SortExpression = "OrderedByDate";
            }

            theDV = clsUtil.GridSort((DataTable)Session["GrdData"], SortExpression, Session["SortDirection"].ToString());

            grdPharmacyNotes.DataSource = null;
            grdPharmacyNotes.Columns.Clear();

            grdPharmacyNotes.DataSource = theDV;
            BindGrid();
        }
    }
}