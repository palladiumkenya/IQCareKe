using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Interface.Pharmacy;
using System.Data;
using Application.Presentation;
using Interface.Clinical;
using Interface.SCM;
using Application.Common;

namespace IQCare.Web.PMSCM
{
    public partial class frmPharmacyDispense_FindPatient : LogPage
    {
        Interface.Pharmacy.IDrug PrescriptionManager;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                PrescriptionManager = (Interface.Pharmacy.IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug, BusinessProcess.Pharmacy");
                Init_Page();
            }
            catch (Exception ex)
            {

                CLogger.WriteLog(ELogLevel.ERROR, ex.ToString());
                if (Session["PatientId"] == null || Convert.ToInt32(Session["PatientId"]) != 0)
                {
                    IQCareMsgBox.NotifyAction("Application has an issue, Please contact Administrator!", "Application Error", true, this, "window.location.href='../frmFindAddCustom.aspx?srvNm=" + Session["TechnicalAreaName"] + "&mod=0'");
                    //Response.Write("<script>alert('Application has an issue, Please contact Administrator!') ; window.location.href='../frmFindAddCustom.aspx?srvNm=" + Session["TechnicalAreaName"] + "&mod=0'</script>");
                }
                else
                {
                    if (Session["TechnicalAreaId"] != null || Convert.ToInt16(Session["TechnicalAreaId"]) != 0)
                    {
                        IQCareMsgBox.NotifyAction("Application has an issue, Please contact Administrator!", "Application Error", true, this, "window.location.href='../frmFacilityHome.aspx';");
                        //Response.Write("<script>alert('Application has an issue, Please contact Administrator!') ; window.location.href='../frmFacilityHome.aspx'</script>");

                    }
                    else
                    {

                        IQCareMsgBox.NotifyAction("Application has an issue, Please contact Administrator!", "Application Error", true, this, "window.location.href='../frmLogin.aspx';");
                        //Response.Write("<script>alert('Application has an issue, Please contact Administrator!') ; window.location.href='../frmLogin.aspx'</script>");
                    }
                }
                ex = null;
            }
           
        }

        private void Init_Page()
        {
           ////(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Pharmacy Dispense";
           ////(Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
           //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("UserControl_Alerts1") as UserControl).Visible = false;
           //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
           ////(Master.FindControl("patientBanner") as Control).Visible = false;
           //(Master.FindControl("level2Navigation") as Control).Visible = false;
           ////(Master.FindControl("imageFlipLevel2") as Control).Visible = false;
           //(Master.FindControl("pnlExtruder") as Panel).Visible = false;
           //(Master.FindControl("level2Navigation") as Control).Visible = true;
           //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("patientLevelMenu") as Menu).Visible = false;
           (Master.FindControl("levelOneNavigationUserControl1").FindControl("PharmacyDispensingMenu") as Menu).Visible = true;
           (Master.FindControl("levelTwoNavigationUserControl1").FindControl("thePnlIdent") as Panel).Visible = false;

            Session["PatientId"] = 0;
           Session["TechnicalAreaId"] = 201; // 206;        
           ViewState["Facility"] = null;
           if (!IsPostBack)
           {
               this.PopulateData();
           }
            
        }
        int LocationID
        {
            get
            {
                return Convert.ToInt32(Session["AppLocationId"]);
            }
        }
        
        private void loadPrescriptions()
        {
            grdPatientPrescriptions.DataSource = null;
            DataSet theDS = PrescriptionManager.GetPharmacyPrescriptions(this.LocationID);
            grdPatientPrescriptions.DataSource = theDS.Tables[0];
            grdPatientPrescriptions.DataBind();
        }

        /// <summary>
        /// Populates the data.
        /// </summary>
        void PopulateData()
        {
           DataSet dt = PrescriptionManager.GetPharmacyPrescriptions(this.LocationID);

            grdPatientPrescriptions.DataSource = dt.Tables[0];
            grdPatientPrescriptions.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }

    

        protected void grdPatientPrescriptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            AuthenticationManager Authentication = new AuthenticationManager();
            string ModuleId;
            DataView theDV = new DataView((DataTable)Session["UserRight"]);
            if (Session["TechnicalAreaId"] != null || Session["TechnicalAreaId"].ToString() != "")
            {
                if (Convert.ToInt32(Session["TechnicalAreaId"].ToString()) != 0)
                {
                    ModuleId = "0," + Session["TechnicalAreaId"].ToString();
                }
                else
                    ModuleId = "0";

            }
            else
                ModuleId = "0";
            theDV.RowFilter = "ModuleId in (" + ModuleId + ")";
            DataTable theDT = new DataTable();
            theDT = theDV.ToTable();

            string theUrl = string.Empty;
            int patientID = int.Parse(grdPatientPrescriptions.SelectedDataKey.Values["Ptn_pk"].ToString());
            Session["PatientVisitID"] = int.Parse(grdPatientPrescriptions.SelectedDataKey.Values["VisitID"].ToString());
            base.Session["PatientId"] = patientID;
            if (Authentication.HasFeatureRight(ApplicationAccess.Dispense, theDT) == false)
            {
                //theUrl = "frmPharmacy_ReferenceMaterials.aspx";
                theUrl = "frmPharmacyDispense_PatientOrder.aspx";
            }
            else
            {
                theUrl = "frmPharmacyDispense_PatientOrder.aspx";
            }

            #region "Refresh Patient Records"
            IPatientHome PManager;
            PManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataSet thePDS = PManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
            //System.Data.DataSet thePDS = PManager.GetPatientDetails(Convert.ToInt32(Request.QueryString["PatientId"]), Convert.ToInt32(Session["SystemId"]));

            Session["PatientInformation"] = thePDS.Tables[0];
            //Session["DynamicLabels"] = thePDS.Tables[23];
            thePDS.Dispose();
            #endregion

            Interface.SCM.IDrug thePharmacyManager = (Interface.SCM.IDrug)ObjectFactory.CreateInstance("BusinessProcess.SCM.BDrug, BusinessProcess.SCM");
            thePharmacyManager.LockpatientForDispensing(patientID, 0, Session["AppUserName"].ToString(), DateTime.Now.ToString("dd-MMM-yyyy"), true);
           
            Response.Redirect(theUrl,false);
        }



        protected void rbtlst_findBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtlst_findPrescription.SelectedValue == "Patient")
            {
                string theUrl;
                //theUrl = string.Format("{0}?FormName={1}&mnuClicked={2}", "../frmFindAddPatient.aspx", "pharmacyDispense", "pharmacyDispense");
                theUrl = string.Format("../frmFindAddCustom.aspx?srvNm={0}&mod={1}", "Pharmacy Dispense", "201");
                //theUrl = string.Format("../Patient/FindAdd.aspx?srvNm={0}&mod={1}", "Pharmacy Dispense", "201");
                Response.Redirect(theUrl,false);
            }

            else
            {
                loadPrescriptions();
            }
        }

        
        protected void grdPatientPrescriptions_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdPatientPrescriptions, "Select$" + e.Row.RowIndex);
                //e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(grdPatientPrescriptions, e.Row.RowIndex.ToString());
            }

            //theUrl = string.Format("{0}?RefId={1}&&PatientId={2}", "./ClinicalForms/frmFamilyInformation.aspx", e.Row.Cells[0].Text, Session["PtnRedirect"].ToString());
            //e.Row.Attributes.Add("onclick", "window.location.href=('" + theUrl + "')"); 

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    //Session["PatientId"] = 3;
            //    string url = "frmPharmacyDispense_PatientOrder.aspx?patientID=3";
            //    e.Row.Attributes.Add("onclick", "fnGoToURL('" + url + "');");
            //}
        }

        protected void grdPatientPrescriptions_DataBound(object sender, EventArgs e)
        {
            if (grdPatientPrescriptions.Rows.Count > 4)
            {
                GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                for (int i = 1; i < grdPatientPrescriptions.Columns.Count; i++)
                {
                    TableHeaderCell cell = new TableHeaderCell();
                    TextBox txtSearch = new TextBox();
                    txtSearch.Attributes["placeholder"] = grdPatientPrescriptions.Columns[i].HeaderText;
                    txtSearch.CssClass = "search_textbox";
                    txtSearch.Width = grdPatientPrescriptions.Columns[i].HeaderStyle.Width;
                    cell.Controls.Add(txtSearch);
                    row.Controls.Add(cell);
                }
                grdPatientPrescriptions.HeaderRow.Parent.Controls.AddAt(1, row);
            }
        }
    }
}