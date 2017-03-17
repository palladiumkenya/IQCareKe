using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Reports;
using Interface.Security;

namespace IQCare.Web.Statistics
{
    public partial class Facility : System.Web.UI.Page
    {
        #region "Variable Declaration"



        /// <summary>
        /// The ds
        /// </summary>
        System.Data.DataSet theDS;

        /// <summary>
        /// The string facility
        /// </summary>
        StringBuilder stringFacility = new StringBuilder();

        #endregion

        /// <summary>
        /// Shows the report.
        /// </summary>
        /// <param name="theTable">The table.</param>
        /// <param name="FileName">Name of the file.</param>
        private void ShowReport(DataTable theTable, string FileName)
        {
            IQWebUtils theUtils = new IQWebUtils();
            theUtils.ExporttoExcel(theTable, Response);
        }
        /// <summary>
        /// Fill_s the dropdown.
        /// </summary>
        private void Fill_Dropdown()
        {
            IUser theLocationManager;
            theLocationManager = (IUser)ObjectFactory.CreateInstance("BusinessProcess.Security.BUser, BusinessProcess.Security");
            DataTable theDT = theLocationManager.GetFacilityList();
            ViewState["Facility"] = theDT;
            IQCareUtils theUtils = new IQCareUtils();
            DataView theDV = new DataView(theDT);
            theDV.Sort = "FacilityID";
            DataRowView[] drv = theDV.FindRows(Session["AppLocationId"]);
            if (drv[0].Row.ItemArray[2].ToString() == "1")
            {
                theDV.RowFilter = "Preferred = 1";
                DataTable theDT1 = (DataTable)theUtils.CreateTableFromDataView(theDV);
                DataRow theDR = theDT1.NewRow();
                theDR["FacilityName"] = "All";
                theDR["FacilityId"] = 9999;
                theDR["Preferred"] = 1;
                theDT1.Rows.InsertAt(theDR, 0);
                ddFacility.DataSource = theDT1;
                ddFacility.DataTextField = "FacilityName";
                ddFacility.DataValueField = "FacilityId";
                ddFacility.DataBind();

            }
            else
            {
                DataTable theDT1 = (DataTable)theUtils.CreateTableFromDataView(theDV);
                DataRow theDR = theDT1.NewRow();
                theDR["FacilityName"] = "All";
                theDR["FacilityId"] = 9999;
                theDR["Preferred"] = 0;
                theDT1.Rows.InsertAt(theDR, 0);
                ddFacility.DataSource = theDT1;
                ddFacility.DataTextField = "FacilityName";
                ddFacility.DataValueField = "FacilityId";
                ddFacility.DataBind();


            }
            ddFacility.SelectedValue = Session["AppLocationId"].ToString();
        }
        /// <summary>
        /// Init_s the form.
        /// </summary>
        private void Init_Form()
        {

            IFacility FacilityManager;
            // Double thePercent, theResultPercent, theTotalPateint, theTotalPMTCTPatient;
            FacilityManager = (IFacility)ObjectFactory.CreateInstance("BusinessProcess.Security.BFacility, BusinessProcess.Security");
            theDS = FacilityManager.GetFacilityStats(Convert.ToInt32(ddFacility.SelectedValue));
            panelPMTCT.Visible = false;//tblPMTCTCare.Visible = 
            panelHEI.Visible = false;//tblExpInfant.Visible =
            panelHIV.Visible = false;//tblHIVCare.Visible =
            ViewState["theDS"] = theDS;
            FacilityManager = null;
            DataTable dttecareas = new DataTable();
            dttecareas = theDS.Tables[10];

            lblTotalActivePatients.Text = theDS.Tables[0].Rows.Count.ToString();
            lblActiveNonARTPatients.Text = theDS.Tables[1].Rows.Count.ToString();
            lblActiveARTPatients.Text = theDS.Tables[2].Rows.Count.ToString();
            lblCurrentMotherPMTCT.Text = theDS.Tables[3].Rows.Count.ToString();
            lblANC.Text = theDS.Tables[4].Rows.Count.ToString();
            lblLD.Text = theDS.Tables[5].Rows.Count.ToString();
            lblPostnatal.Text = theDS.Tables[6].Rows.Count.ToString();
            lblCurrentTotalExposedInfants.Text = theDS.Tables[7].Rows.Count.ToString();
            lblCurrentPMTCTInfants.Text = theDS.Tables[8].Rows.Count.ToString();
            lblCurrentHIVCareInfants.Text = theDS.Tables[9].Rows.Count.ToString();

            Session["Facility"] = ddFacility.SelectedValue.ToString();
            string FacName = ddFacility.SelectedItem.Text;
            string[] theFacility = FacName.Split(new char[] { '-' });

            if (theFacility[0] == "All")
            {
                Session["FacilityName"] = "All Facilities";

            }
            else
            {
                Session["FacilityName"] = Convert.ToString(theDS.Tables[12].Rows[0]["FacilityName"]);
            }


            DataView theDV = new DataView(theDS.Tables[13]);
            theDV.RowFilter = "ModuleID=1";
            if (theDV.Count > 0)
            {
                panelPMTCT.Visible = true;//=  tblPMTCTCare.Visible 
                panelHEI.Visible = true;//=  tblExpInfant.Visible
            }
            theDV.RowFilter = "ModuleID=2 or ModuleID=203";
            if (theDV.Count > 0)
            {
                panelHIV.Visible = true;//= tblHIVCare.Visible 
            }
            
        }

        /// <summary>
        /// Handles the PreRender event of the objdView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void objdView_PreRender(object sender, EventArgs e)
        {
            GridView theGView = (GridView)sender;
            theGView.DataBind();
        }


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "<i class='fa fa-angle-double-right' aria-hidden='true'></i> Facility Home";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;

            try
            {

                if (IsPostBack != true)
                {
                    Session["PatientId"] = 0;

                    ViewState["Facility"] = null;
                    AuthenticationManager Authentiaction = new AuthenticationManager();

                    if (Authentiaction.HasFunctionRight(ApplicationAccess.Schedular, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                    {
                        DirectScheduler.Visible = false;
                        MissedScheduler.Visible = false;
                    }

                    Fill_Dropdown();
                    Init_Form();
                    FacilityStats1.DataBind();
                    string theUrl;
                    theUrl = string.Format("{0}&AppointmentStatus={1}", "~/Scheduler/frmScheduler_AppointmentMain.aspx?name=Add", "All");
                    DirectScheduler.HRef = theUrl;


                    theUrl = string.Format("{0}&AppointmentStatus={1}", "~/Scheduler/frmScheduler_AppointmentMain.aspx?name=Add", "Missed");
                    MissedScheduler.HRef = theUrl;
                }

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }
        /// <summary>
        /// Handles the SelectedIndexChanged event of the ddFacility control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void ddFacility_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Init_Form();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkpreferred control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void chkpreferred_CheckedChanged(object sender, EventArgs e)
        {
            IQCareUtils theUtils = new IQCareUtils();
            DataView theDV = new DataView((DataTable)ViewState["Facility"]);
            theDV.Sort = "FacilityID";
            if (chkpreferred.Checked == true)
            {
                theDV.RowFilter = "Preferred = 1";
                DataTable theDT1 = (DataTable)theUtils.CreateTableFromDataView(theDV);
                DataRow theDR = theDT1.NewRow();
                theDR["FacilityName"] = "All";
                theDR["FacilityId"] = 9999;
                theDR["Preferred"] = 1;
                theDT1.Rows.InsertAt(theDR, 0);
                ddFacility.DataSource = theDT1;
                ddFacility.DataTextField = "FacilityName";
                ddFacility.DataValueField = "FacilityId";
                ddFacility.DataBind();

            }
            else
            {
                DataTable theDT1 = (DataTable)theUtils.CreateTableFromDataView(theDV);
                DataRow theDR = theDT1.NewRow();
                theDR["FacilityName"] = "All";
                theDR["FacilityId"] = 9999;
                theDR["Preferred"] = 0;
                theDT1.Rows.InsertAt(theDR, 0);
                ddFacility.DataSource = theDT1;
                ddFacility.DataTextField = "FacilityName";
                ddFacility.DataValueField = "FacilityId";
                ddFacility.DataBind();

            }
            Init_Form();
            //Page_Load(sender, e);
        }

        #region "Lost to Follow-up Patient List"
        /// <summary>
        /// Handles the Click event of the hlLosttoFollowUp control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hlLosttoFollowUp_Click(object sender, EventArgs e)
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            DataTable dtLosttoFollowupPatientReport = (DataTable)ReportDetails.GetLosttoFollowupPatientReport(Convert.ToInt32(ddFacility.SelectedValue)).Tables[0];
            string FName = "LstFollowup";
            IQWebUtils theUtils = new IQWebUtils();
            string thePath = Server.MapPath("~\\ExcelFiles\\" + FName + ".xls");
            string theTemplatePath = Server.MapPath("~\\ExcelFiles\\IQCareTemplate.xls");
            theUtils.ExporttoExcel(dtLosttoFollowupPatientReport, Response);
            Response.Redirect("~\\ExcelFiles\\" + FName + ".xls");
            //theUtils.OpenExcelFile(thePath, Response);

        }
        #endregion


        #region "ART Unknown Patient List"
        /// <summary>
        /// Handles the Click event of the hlartunknown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hlartunknown_Click(object sender, EventArgs e)
        {

            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
            System.Data.DataSet dsARTUnknown = ReportDetails.GetPtnotvisitedrecentlyUnknown(Convert.ToDateTime(Application["AppCurrentDate"]), Convert.ToDateTime(Application["AppCurrentDate"]), Convert.ToInt32(ddFacility.SelectedValue));
            string FName = "ARTunknown";
            IQWebUtils theUtils = new IQWebUtils();
            string thePath = Server.MapPath("~\\ExcelFiles\\" + FName + ".xls");
            string theTemplatePath = Server.MapPath("~\\ExcelFiles\\IQCareTemplate.xls");
            theUtils.ExporttoExcel(dsARTUnknown.Tables[0], Response);
            Response.Redirect("~\\ExcelFiles\\" + FName + ".xls");

        }
        #endregion


        /// <summary>
        /// Handles the Click event of the hlTotalActivePatients control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hlTotalActivePatients_Click(object sender, EventArgs e)
        {
            ShowReport(((DataSet)ViewState["theDS"]).Tables[0], "ActivePatients");
        }

        /// <summary>
        /// Handles the Click event of the hlActiveNonARTPatients control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hlActiveNonARTPatients_Click(object sender, EventArgs e)
        {
            ShowReport(((DataSet)ViewState["theDS"]).Tables[1], "ActiveNonARTPatients");
        }

        /// <summary>
        /// Handles the Click event of the hlActiveARTPatient control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hlActiveARTPatient_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[2], "ActiveARTPatients");
        }
        /// <summary>
        /// Handles the Click event of the hllnkCurrentMotherPMTCT control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hllnkCurrentMotherPMTCT_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[3], "PMTCTCurrentMothers");
        }

        /// <summary>
        /// Handles the Click event of the hllnkANC control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hllnkANC_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[4], "ProANCMothers");
        }
        /// <summary>
        /// Handles the Click event of the hllnkLD control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hllnkLD_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[5], "ProLDMothers");
        }
        /// <summary>
        /// Handles the Click event of the hllnkPostnatal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hllnkPostnatal_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[6], "ProLDMothers");
        }
        /// <summary>
        /// Handles the Click event of the hllnkCurrentTotalExposedInfants control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hllnkCurrentTotalExposedInfants_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[7], "CurrentTotalExposedInfants");
        }
        /// <summary>
        /// Handles the Click event of the hllnkCurrentPMTCTInfants control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hllnkCurrentPMTCTInfants_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[8], "CurrentPMTCTInfants");
        }
        /// <summary>
        /// Handles the Click event of the hllnkCurrentHIVCareInfants control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hllnkCurrentHIVCareInfants_Click(object sender, EventArgs e)
        {
            ShowReport(((System.Data.DataSet)ViewState["theDS"]).Tables[9], "CurrentHIVCareInfants");
        }
        /// <summary>
        /// Handles the Click event of the more control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void more_Click(object sender, EventArgs e)
        {

            string theScript;
            theScript = "<script language='javascript' id='Popup'>\n";
            theScript += "window.open('HivCare.aspx','popupwindow','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=yes,resizable=no,width=950,height=650,scrollbars=yes');\n";
            theScript += "</script>\n";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "Popup", theScript);

        }
        /// <summary>
        /// Handles the Click event of the hlmore1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hlmore1_Click(object sender, EventArgs e)
        {

            string theScript;
            theScript = "<script language='javascript' id='pmtctpopup'>\n";
            theScript += "window.open('PMTCT.aspx','popupwindow','toolbars=no,location=no, directories=no,dependent=yes,top=10,left=30.maximize=yes, resizable=no,width=900,height=630,scrollbars=yes');\n";
            theScript += "</script>\n";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "pmtctpopup", theScript);

        }
        /// <summary>
        /// Handles the Click event of the hlmore2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void hlmore2_Click(object sender, EventArgs e)
        {

            string theScript;
            theScript = "<script language='javascript' id='pmtctpopup'>\n";
            theScript += "window.open('HEI.aspx','popupwindow','toolbars=no,location=no, directories=no,dependent=yes,top=10,left=30.maximize=yes, resizable=no,width=900,height=630,scrollbars=yes');\n";
            theScript += "</script>\n";
            ClientScript.RegisterClientScriptBlock(this.GetType(), "pmtctpopup", theScript);
        }
    }
}