using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Reports;
using Excel = Microsoft.Office.Interop.Owc11;
namespace IQCare.Web.Reports
{
    public partial class frmReportFacilityJump : System.Web.UI.Page
    {
        DateTime theARVYear;
        DateTime theProgressYear;
        private int patientId =0;
        public DataSet theDS;
        DataSet theDSXML = new DataSet();
        string theModList;
        private void Init_Page()
        {
            txtCountryNo.Text = "";
            txtPosNo.Text = "";
            txtSatelliteNo.Text = "";
            txtStartDate.Text = "";
            txtEndDate.Text = "";
            TxtFromDate.Text = "";
            TxtToDate.Text = "";
            txtDefaulterAsOf.Text = "";
            txtPatientId.Text = "";

            rdoARVPickup.Checked = false;
            rdoMissARVPickup.Checked = false;
            rdoPatiEnrollMonth.Checked = false;
            rdoTBStatus.Checked = false;
            rdoARVRegimen.Checked = false;
            rdoNonArtPatientsReport.Checked = false;
            rdoptnotvisitedUnknown.Checked = false;
            rdoGeoPatientsDistribution.Checked = false;
            rdoNNRIMS.Checked = false;
            rdoUgandaMOH.Checked = false;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["PatientId"] = 0;
            //initParams.Attributes.Add("value", "TranDateFrom=" + txtTranDateFrom.Text + ", TranDateTo=" + txtTranDateTo.Text + ", ReportID=1");
            Master.PageScriptManager.RegisterPostBackControl(tabControl);
            AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Authentiaction.HasFunctionRight(ApplicationAccess.FacilityReports, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                btnSubmit.Enabled = false;
            }
            if (Page.IsPostBack != true)
            {
                BindFunctions BindManager = new BindFunctions();
                DataTable Month = BindManager.GetMonths();
                BindManager.BindCombo(ddMonth, Month, "Name", "Id");
                BindManager.BindCombo(ARVddMonth, Month, "Name", "Id");
                BindManager.BindCombo(ddlMonthKenyaHealth, Month, "Name", "Id");
                BindManager.BindCombo(ddlUgandaMOH, Month, "Name", "Id");
                IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                DataTable dtuserDetail = (DataTable)ReportDetails.GetUsers();
                BindManager.BindCombo(ddlSelectUser, dtuserDetail, "UserName", "UserId");

                txtTranDateFrom.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'4')");
                txtTranDateFrom.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'4')");

                txtTranDateTo.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'4')");
                txtTranDateTo.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'4')");
            }
            string xmlFilePath = MapPath("..\\XMLFiles\\Currency.xml");
            theDSXML.ReadXml(xmlFilePath);
            DataView theDV = new DataView(theDSXML.Tables[0]);
            string countryid = "";
            if (Session["AppCurrency"] != null)
            {
                countryid = Session["AppCurrency"].ToString();
                theDV.RowFilter = "id=" + countryid + "";
                DataTable theDT = new DataTable();
                theDT = theDV.ToTable();
                hdCountryId.Value = countryid.ToString();
            }
            if (Session["SystemId"] != null)
                hdSystemID.Value = Session["SystemId"].ToString();
            if (Session["AppModule"] != null)
            {
                DataTable theDTModule = (DataTable)Session["AppModule"];
                theModList = "";

                foreach (DataRow theDR in theDTModule.Rows)
                {
                    if (theModList == "")
                        theModList = theDR["ModuleId"].ToString();
                    else
                        theModList = "0";
                }
            }

            showhide();
            //(Master.FindControl("lblRoot") as Label).Text = "Reports >>";
            //(Master.FindControl("lblMark") as Label).Visible = false;        
            //(Master.FindControl("lblheader") as Label).Text = "Facility Reports";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Facility Reports";

            txtStartDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            txtStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtEndDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            txtEndDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtDefaulterAsOf.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            txtDefaulterAsOf.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtStartDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            txtStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtEndDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            txtEndDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtYear.Attributes.Add("onkeyup", "chkNumeric('" + txtYear.ClientID + "')");
            txtyears.Attributes.Add("onkeyup", "chkNumeric('" + txtyears.ClientID + "')");

            TxtFromDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            TxtFromDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            TxtToDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            TxtToDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");

            btnSubmit.Attributes.Add("Onclick", "javaScript:return Validate();");
            hdModuleID.Value = theModList;

            if (tabControl.ActiveTabIndex.ToString() == "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script language = 'javascript' defer ='defer' id = 'scriptonload'>ShowHide('2');  </script>");

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script language = 'javascript' defer ='defer' id = 'scriptonload'>ShowHide('1');  </script>");

            }
            //ClientScript.RegisterStartupScript(this.GetType(),"onload", "<script language = 'javascript' defer ='defer' id = 'scriptonload'>ShowHide('2');  </script>");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string theReportName = string.Empty;
                IQCareUtils theUtils = new IQCareUtils();
                string theStartDate;
                string theEndDate;
                IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                if (rdoARVPickup.Checked)
                {
                    if (rdoARVPickupForAll.Checked)
                    {
                        theReportName = "AllPatientARVPickup";
                        if (FieldValidation(theReportName) == false)
                        {
                            return;
                        }
                        DataTable dtDrugARVPickup = (DataTable)ReportDetails.GetAllPatientDrugARVPickup(Convert.ToInt32(Session["AppLocationId"])).Tables[0];
                        dtDrugARVPickup.WriteXmlSchema(Server.MapPath("..\\XMLFiles\\AllPatientARVPickup.xml"));
                        Session["dtDrugARVPickup"] = dtDrugARVPickup;
                    }
                    else if (rdoARVPickupForAPatient.Checked)
                    {

                        theReportName = "SinglePatientARVPickup";
                        if (FieldValidation(theReportName) == false)
                        {
                            return;
                        }
                        if (NoEnrolexist(theReportName) == false)
                        {
                            return;
                        }
                        if (txtSatelliteNo.Text != "" && txtCountryNo.Text != "" && txtPosNo.Text != "" && txtPatientId.Text != "")
                        {

                            DataTable dtDrugARVPickup = (DataTable)ReportDetails.GetDrugARVPickup(Convert.ToInt32(txtPatientId.Text), Convert.ToDateTime("01-01-1900"), Convert.ToDateTime("01-01-1900"), txtSatelliteNo.Text, txtCountryNo.Text, txtPosNo.Text, 0).Tables[0];
                            if (dtDrugARVPickup.Rows.Count > 0)
                            {
                                dtDrugARVPickup.WriteXmlSchema(Server.MapPath("..\\XMLFiles\\SinglePatientARVPickUP.xml"));
                                Session["dtDrugARVPickup"] = dtDrugARVPickup;
                            }
                            else
                            {
                                IQCareMsgBox.Show("NoData", this);
                                return;
                            }
                        }
                    }
                }
                else if (rdoMissARVPickup.Checked)
                {
                    theReportName = "MisARVPickup";
                    if (FieldValidation(theReportName) == false)
                    {
                        return;
                    }
                    if (txtDefaulterAsOf.Text != "")
                    {
                        DataTable dtDrugARVPickup = (DataTable)ReportDetails.GetMissARVPickup(Convert.ToDateTime(txtDefaulterAsOf.Text), Session["AppLocationId"].ToString()).Tables[0];
                        Session["dtDrugARVPickup"] = dtDrugARVPickup;
                        dtDrugARVPickup.WriteXmlSchema(Server.MapPath("..\\XMLFiles\\MisARVPickup.xml"));
                    }

                }
                else if (rdoPatiEnrollMonth.Checked)
                {
                    theReportName = "PatientEnrollmentMonth";
                    if (FieldValidation(theReportName) == false)
                    {
                        return;
                    }
                    if (txtStartDate.Text != "" && txtEndDate.Text != "")
                    {
                        DataSet dtPatiEnrollMonth = (DataSet)ReportDetails.GetPatientEnrollMonth(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Session["AppLocationId"].ToString());
                        dtPatiEnrollMonth.WriteXmlSchema(Server.MapPath("..\\XMLFiles\\PatientEnrollmentMonth.xml"));
                        Session["dtPatiEnrollMonth"] = dtPatiEnrollMonth;
                    }
                }

                else if (rdoTBStatus.Checked)
                {
                    theReportName = "TB Status by Age and Sex Report";
                    if (FieldValidation(theReportName) == false)
                    {
                        return;
                    }
                    if (txtStartDate.Text != "" && txtEndDate.Text != "")
                    {
                        theDS = (DataSet)ReportDetails.GetTBStatusbyAgeandSex(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToInt32(Session["AppLocationId"]));
                        Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                        string thePath = Server.MapPath("..\\ExcelFiles\\Templates\\TBStatusbyAgeandSex.xml");
                        theApp.XMLURL = thePath;

                        #region "Tables Defination"
                        theDS.Tables[0].TableName = "Tbl1";
                        theDS.Tables[1].TableName = "Tbl2";
                        theDS.Tables[2].TableName = "Tbl3";
                        theDS.Tables[3].TableName = "Tbl4";
                        theDS.Tables[4].TableName = "Tbl5";
                        theDS.Tables[5].TableName = "Tbl6";
                        theDS.Tables[6].TableName = "Tbl7";
                        theDS.Tables[7].TableName = "Tbl8";
                        theDS.Tables[8].TableName = "Tbl9";
                        theDS.Tables[9].TableName = "Tbl10";
                        theDS.Tables[10].TableName = "Tbl11";
                        theDS.Tables[11].TableName = "Tbl12";
                        theDS.Tables[12].TableName = "Tbl13";
                        theDS.Tables[13].TableName = "Tbl14";
                        theDS.Tables[14].TableName = "Tbl15";
                        theDS.Tables[15].TableName = "Tbl16";
                        theDS.Tables[16].TableName = "Tbl17";
                        theDS.Tables[17].TableName = "Tbl18";
                        theDS.Tables[18].TableName = "Tbl19";
                        theDS.Tables[19].TableName = "Tbl20";
                        theDS.Tables[20].TableName = "Tbl21";
                        theDS.Tables[21].TableName = "Tbl22";
                        theDS.Tables[22].TableName = "Tbl23";
                        theDS.Tables[23].TableName = "Tbl24";
                        theDS.Tables[24].TableName = "Tbl25";
                        theDS.Tables[25].TableName = "Tbl26";
                        theDS.Tables[26].TableName = "Tbl27";
                        theDS.Tables[27].TableName = "Tbl28";
                        theDS.Tables[28].TableName = "Tbl29";
                        theDS.Tables[29].TableName = "Tbl30";
                        theDS.Tables[30].TableName = "Tbl31";
                        theDS.Tables[31].TableName = "Tbl32";
                        theDS.Tables[32].TableName = "FacilityName";
                        #endregion

                        writeCellWiseInExcel(theApp);
                        theApp.Export(Server.MapPath("..\\ExcelFiles\\TBStsAgeSex.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                        //Response.Redirect("..\\ExcelFiles\\TBStsAgeSex.xls");
                        IQWebUtils theUtl = new IQWebUtils();
                        theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\TBStsAgeSex.xls"), Response);
                    }
                }

                else if (rdoTBCase.Checked)
                {
                    theReportName = "TB Cases before and after starting ARVs";
                    if (FieldValidation(theReportName) == false)
                    {
                        return;
                    }
                    theDS = (DataSet)ReportDetails.GetTotalNoTBPatientwithARVwithoutARV(txtStartDate.Text, txtEndDate.Text, Session["AppLocationId"].ToString());
                    Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                    string thePath = Server.MapPath("..\\ExcelFiles\\Templates\\NoTBPatientswithwithoutARVs.xml");
                    theApp.XMLURL = thePath;
                    WriteCellWiseInExcel_TBWithARV(theApp);
                    theApp.Export(Server.MapPath("..\\ExcelFiles\\NoTBPatientswithwithoutARVs.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                    //Response.Redirect("..\\ExcelFiles\\NoTBPatientswithwithoutARVs.xls");
                    IQWebUtils theUtl = new IQWebUtils();
                    theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\NoTBPatientswithwithoutARVs.xls"), Response);

                }
                else if (rdoARVRegimen.Checked)
                {
                    theReportName = "ARV Regimen for Adult/Child Report";
                    if (FieldValidation(theReportName) == false)
                    {
                        return;
                    }
                    theDS = (DataSet)ReportDetails.GetARVRegimenforAdultandChild(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(txtEndDate.Text), Convert.ToInt32(Session["AppLocationId"]));
                    Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                    string thePath = Server.MapPath("..\\ExcelFiles\\Templates\\ARV_regimen_adult-child.xml");
                    theApp.XMLURL = thePath;
                    #region "TableNames"
                    theDS.Tables[0].TableName = "Tbl1";
                    theDS.Tables[1].TableName = "Tbl2";
                    theDS.Tables[2].TableName = "Tbl3";
                    theDS.Tables[3].TableName = "Tbl4";
                    theDS.Tables[4].TableName = "Tbl5";
                    #endregion
                    WriteCellWiseInExcel_ARVRegimenAdultChild(theApp);
                    theApp.Export(Server.MapPath("..\\ExcelFiles\\ARV_regimen_adult-child.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                    //Response.Redirect("..\\ExcelFiles\\ARV_regimen_adult-child.xls");
                    IQWebUtils theUtl = new IQWebUtils();
                    theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\ARV_regimen_adult-child.xls"), Response);

                }
                else if (rdoNonArtPatientsReport.Checked)
                {
                    theReportName = "Non_ARTPatient";
                    if (FieldValidation(theReportName) == false)
                    {
                        return;
                    }

                    theDS = (DataSet)ReportDetails.GetNonArtPatient(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(Application["AppCurrentDate"]), Convert.ToInt32(Session["AppLocationId"]));
                    if (theDS.Tables[0].Rows.Count > 0)
                    {
                        Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                        string thePath = Server.MapPath("..\\ExcelFiles\\Templates\\Non-ARTPatient.xml");
                        theApp.XMLURL = thePath;
                        theDS.Tables[0].TableName = "Tbl1";
                        writeCellWiseInExcel_Non_ARTPatientReport(theApp);
                        theApp.Export(Server.MapPath("..\\ExcelFiles\\Non-ARTPatient.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                        //Response.Redirect("..\\ExcelFiles\\Non-ARTPatient.xls");
                        IQWebUtils theUtl = new IQWebUtils();
                        theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\Non-ARTPatient.xls"), Response);

                    }
                    //else
                    //{
                    //    IQCareMsgBox.Show("NoPatientExists", this);
                    //    return;


                    //}

                }

                else if (rdoptnotvisitedUnknown.Checked)
                {
                    theReportName = "Unknown Patients who have not visited recently";
                    if (FieldValidation(theReportName) == false)
                    {
                        return;
                    }
                    theDS = (DataSet)ReportDetails.GetPtnotvisitedrecentlyUnknown(Convert.ToDateTime(txtStartDate.Text), Convert.ToDateTime(Application["AppCurrentDate"]), Convert.ToInt32(Session["AppLocationId"]));
                    if (theDS.Tables[0].Rows.Count > 0)
                    {
                        Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                        string thePath = Server.MapPath("..\\ExcelFiles\\Templates\\UnknownPatientsnotvisitedrecently.xml");
                        theApp.XMLURL = thePath;
                        theDS.Tables[0].TableName = "Tbl1";
                        WriteCellWiseInExcel_UnknownPtnotvisited(theApp);
                        theApp.Export(Server.MapPath("..\\ExcelFiles\\UnknownPatientsnotvisitedrecently.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                        //Response.Redirect("..\\ExcelFiles\\UnknownPatientsnotvisitedrecently.xls");
                        IQWebUtils theUtl = new IQWebUtils();
                        theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\UnknownPatientsnotvisitedrecently.xls"), Response);

                    }
                    else
                    {
                        IQCareMsgBox.Show("NoPatientExists", this);
                        return;


                    }

                }
                else if (rdoKenyaHealth.Checked)
                {
                    theReportName = "Kenya National Integrated Form for reproductive Health,HIV/Aids,Malaria,TB and child Nutrition(711B)";
                    if (FieldValidation(theReportName) == false)
                    {
                        return;

                    }
                    theDS = (DataSet)ReportDetails.GetKenyaMonthlyReport(Convert.ToInt32(ddlMonthKenyaHealth.SelectedValue), Convert.ToInt32(txtYearKenyaHealth.Text), Convert.ToInt32(Session["AppLocationId"]));
                    #region "TableNames"
                    theDS.Tables[0].TableName = "TableDate";
                    theDS.Tables[1].TableName = "Table0";
                    theDS.Tables[2].TableName = "Table1";


                    #endregion

                    Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                    string thePath = Server.MapPath("..\\ExcelFiles\\Templates\\Kenya711BMonthlyReport.xml");
                    theApp.XMLURL = thePath;
                    WriteCellWiseInExcel_KenyaMonthlyReport(theApp);
                    theApp.Export(Server.MapPath("..\\ExcelFiles\\Kenya711BMonthlyReport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                    IQWebUtils theUtl = new IQWebUtils();
                    theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\Kenya711BMonthlyReport.xls"), Response);

                }
                else if (rdoNNRIMS.Checked)
                {
                    theReportName = "NNRIMS Monthly Report";
                    if (FieldValidation(theReportName) == false)
                    {
                        return;

                    }
                    theDS = (DataSet)ReportDetails.GetNNRIMSFacilityMonthlyReport(Convert.ToInt32(ddlMonthKenyaHealth.SelectedValue), Convert.ToInt32(txtYearKenyaHealth.Text), Convert.ToInt32(Session["AppLocationId"]));

                    #region "TableNames"
                    theDS.Tables[0].TableName = "TableDate";
                    theDS.Tables[1].TableName = "Table1";
                    #endregion

                    Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                    string thePath = Server.MapPath("..\\ExcelFiles\\Templates\\NNRIMSMonthlyReport.xml");
                    theApp.XMLURL = thePath;
                    WriteCellWiseInExcel_NNRIMSMonthlyReport(theApp);
                    theApp.Export(Server.MapPath("..\\ExcelFiles\\NNRIMSMonthlyReport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                    IQWebUtils theUtl = new IQWebUtils();
                    theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\NNRIMSMonthlyReport.xls"), Response);

                }

                else if (rdoGeoPatientsDistribution.Checked)
                {
                    theReportName = "Geographical Patients Distribution Report";
                    theDS = (DataSet)ReportDetails.GetGeographicalPatientsDistribution(Convert.ToInt32(Session["AppLocationId"]));
                    Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                    string thePath = Server.MapPath("..\\ExcelFiles\\Templates\\GeographicalPatientDistribution.xml");
                    theApp.XMLURL = thePath;
                    theDS.Tables[0].TableName = "Tbl1";
                    theDS.Tables[1].TableName = "Tbl2";
                    theDS.Tables[2].TableName = "Tbl3";
                    theDS.Tables[3].TableName = "Tbl4";
                    WriteCellWiseInExcel_GeographicalPatientDistribution(theApp);
                    theApp.Export(Server.MapPath("..\\ExcelFiles\\GeographicalPatientDistribution.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                    IQWebUtils theUtl = new IQWebUtils();
                    theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\GeographicalPatientDistribution.xls"), Response);
                }





                IQCareUtils theUtil = new IQCareUtils();
                string theUrl = "";


                if (theReportName == "PatientEnrollmentMonth")
                {
                    theStartDate = theUtil.MakeDate(txtStartDate.Text);
                    theEndDate = theUtil.MakeDate(txtEndDate.Text);
                    theUrl = string.Format("{0}ReportName={1}&StartDate={2}&EndDate={3}&MstPage={4}", "frmReportViewerARV.aspx?", theReportName, theStartDate, theEndDate, 1);
                }
                /////////////////////////////////////////////////////
                else if (rdoUserDetail.Checked || RdoUserDetail1.Checked)
                {
                    theReportName = "User Detail";
                    string userid;
                    if (FieldValidation(theReportName) == false)
                    {
                        return;
                    }
                    if (chkAllUser.Checked == true)
                    {
                        userid = "0";
                    }
                    else
                    {
                        userid = ddlSelectUser.SelectedValue.ToString();
                    }
                    theStartDate = theUtil.MakeDate(TxtFromDate.Text);
                    theEndDate = theUtil.MakeDate(TxtToDate.Text);
                    theDS = (DataSet)ReportDetails.GetUserDetail(Convert.ToDateTime(theStartDate), Convert.ToDateTime(theEndDate), userid, Convert.ToInt32(Session["AppLocationId"]), Convert.ToInt32(theModList));
                    Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                    string thePath = Server.MapPath("..\\ExcelFiles\\Templates\\UserDetail.xml");
                    theApp.XMLURL = thePath;
                    WriteCellWiseInExcel_UserDetail(theApp);
                    theApp.Export(Server.MapPath("..\\ExcelFiles\\UserDetail.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                    //Response.Redirect("..\\ExcelFiles\\UserDetail.xls");
                    IQWebUtils theUtl = new IQWebUtils();
                    theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\UserDetail.xls"), Response);
                }

                //else if (rdoUgandaMOH.Checked)
                //{
                //    theReportName = "PMTCT Monthly Report Form - MOH";
                //    if (FieldValidation(theReportName) == false)
                //    {
                //        return;

                //    }

                //    if (hdCountryId.Value == "304")
                //    {
                //        theDS = (DataSet)ReportDetails.GetUgandaMOHMonthlyReport(Convert.ToInt32(ddlUgandaMOH.SelectedValue), Convert.ToInt32(txtUgandaMOH.Text), Convert.ToInt32(Session["AppLocationId"]));
                //        #region "TableNames"
                //        theDS.Tables[0].TableName = "TableDate";
                //        theDS.Tables[1].TableName = "Table0";
                //        theDS.Tables[2].TableName = "Table1";
                //        theDS.Tables[3].TableName = "Table2";
                //        theDS.Tables[4].TableName = "Table3";


                //        #endregion
                //        Excel.Spreadsheet   theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet()  ;
                //        string thePath = Server.MapPath("..\\ExcelFiles\\Templates\\UgandaMonthlyReport.xml");
                //        theApp.XMLURL = thePath;
                //        WriteCellWiseInExcel_UgandaMonthlyReport(theApp);
                //        theApp.Export(Server.MapPath("..\\ExcelFiles\\UgandaMonthlyReport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                //        Response.Redirect("..\\ExcelFiles\\UgandaMonthlyReport.xls");


                //    }
                //else if (hdCountryId.Value == "290")
                //{

                    //    theDS = (DataSet)ReportDetails.GetTanzaniaPMTCTMonthlyMoHReport(Convert.ToInt32(ddlUgandaMOH.SelectedValue), Convert.ToInt32(txtUgandaMOH.Text), Convert.ToInt32(Session["AppLocationId"]));
                //    #region "TableNames"
                //    theDS.Tables[0].TableName = "TableDate";
                //    theDS.Tables[1].TableName = "Table0";
                //    theDS.Tables[2].TableName = "Table1";
                //    theDS.Tables[3].TableName = "Table2";
                //    theDS.Tables[4].TableName = "Table3";
                //    theDS.Tables[5].TableName = "Table4";


                    //    #endregion




                    //    Excel.Spreadsheet   theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet()  ;
                //    string thePath = Server.MapPath("..\\ExcelFiles\\Templates\\TanzaniaMOHReport.xml");
                //    theApp.XMLURL = thePath;
                //    WriteCellWiseInExcel_TanzaniaMonthlyReport(theApp);
                //    theApp.Export(Server.MapPath("..\\ExcelFiles\\TanzaniaMOHReport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                //    IQWebUtils theUtl = new IQWebUtils();
                //    theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\TanzaniaMOHReport.xls"), Response);



                    //}






                ////////////////////////////////Born-To-Live////////////////////////////////////////////////////////
                else if (rdoBornToLive.Checked)
                {
                    theReportName = "Born-To-Live";
                    if (FieldValidation(theReportName) == false)
                    {
                        return;
                    }
                    theStartDate = theUtil.MakeDate(TxtFromDate.Text);
                    theEndDate = theUtil.MakeDate(TxtToDate.Text);
                    theDS = (DataSet)ReportDetails.GetBornToLive(Convert.ToInt32(ddlMonthKenyaHealth.SelectedValue), Convert.ToInt32(txtYearKenyaHealth.Text), Convert.ToInt32(Session["AppLocationId"]));
                    Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                    string thePath = Server.MapPath("..\\ExcelFiles\\Templates\\BornToLive.xml");
                    theApp.XMLURL = thePath;
                    WriteCellWiseInExcel_BornToLive(theApp);
                    theApp.Export(Server.MapPath("..\\ExcelFiles\\BornToLive.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                    IQWebUtils theUtl = new IQWebUtils();
                    theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\BornToLive.xls"), Response);
                }
                /////////////////////////////////////////////////////////////////////////////////////////////////
                else if (rdoNASCOP.Checked)
                {
                    theReportName = "NASCOP Monthly Report";
                    if (FieldValidation(theReportName) == false)
                    {
                        return;
                    }
                    theStartDate = theUtil.MakeDate(TxtFromDate.Text);
                    theEndDate = theUtil.MakeDate(TxtToDate.Text);
                    theDS = (DataSet)ReportDetails.GetPatientNascop(Convert.ToInt32(ddlMonthKenyaHealth.SelectedValue), Convert.ToInt32(txtYearKenyaHealth.Text), Convert.ToInt32(Session["AppLocationId"]));
                    #region "TableNames"
                    theDS.Tables[0].TableName = "TableDate";
                    theDS.Tables[1].TableName = "Table1";
                    theDS.Tables[2].TableName = "Table2";
                    theDS.Tables[3].TableName = "Table3";
                    theDS.Tables[4].TableName = "Table4";
                    theDS.Tables[5].TableName = "Table5";
                    theDS.Tables[6].TableName = "Table6";
                    theDS.Tables[7].TableName = "Table7";
                    theDS.Tables[8].TableName = "Table8";
                    #endregion
                    Excel.Spreadsheet theApp = new Microsoft.Office.Interop.Owc11.Spreadsheet();
                    string thePath = Server.MapPath("..\\ExcelFiles\\Templates\\NASCOP.xml");
                    theApp.XMLURL = thePath;
                    WriteCellWiseInExcel_NASCOP(theApp);
                    theApp.Export(Server.MapPath("..\\ExcelFiles\\NASCOP.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionNone, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportXMLSpreadsheet);
                    IQWebUtils theUtl = new IQWebUtils();
                    theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\NASCOP.xls"), Response);
                }
                ///////////////////////////////////////////////////////////////////////////////////////  
                else if (theReportName == "MisARVPickup")
                {
                    theStartDate = theUtil.MakeDate(txtDefaulterAsOf.Text);
                    theUrl = string.Format("{0}ReportName={1}&StartDate={2}&EndDate={3}&MstPage={4}", "frmReportViewerARV.aspx?", theReportName, theStartDate, null, 1);
                }
                else if (theReportName == "AllPatientARVPickup")
                {
                    theUrl = string.Format("{0}ReportName={1}&MstPage={2}", "frmReportViewerARV.aspx?", theReportName, 1);
                }
                else if (theReportName == "SinglePatientARVPickup")
                {
                    theUrl = string.Format("{0}ReportName={1}&MstPage={2}&PatientId={3}", "frmReportViewerARV.aspx?", theReportName, 1, patientId);
                }

                ReportDetails = null;
                if (theUrl != "")
                    Response.Redirect(theUrl);
                //-----------------------
            }

            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
        }

        private void writeInCell(Excel.Spreadsheet theSheet, string cell, string tablename, string column)
        {
            try
            {
                Excel.Range theRange = theSheet.Cells.get_Range(cell, cell);
                theRange.BorderAround(Excel.LineStyleEnum.owcLineStyleSolid, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexNone, "Black");
                //theRange.Borders();

                string theExitvalue = "";
                if (theRange.Value2 != null)
                    theExitvalue = theRange.Value2.ToString();
                else
                    theExitvalue = "";

                if (tablename != "")
                {
                    if (theExitvalue.ToString().Trim() == "")
                        theRange.Value2 = theDS.Tables[tablename].Rows[0][column].ToString();
                    else
                        theRange.Value2 = theExitvalue + "  " + theDS.Tables[tablename].Rows[0][column].ToString();
                }
                else
                {
                    if (theExitvalue.ToString().Trim() == "")
                        theRange.Value2 = column;
                    else
                        theRange.Value2 = theExitvalue + "  " + column;
                }
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

            }
        }
        private void WriteInCellforLoop(Excel.Spreadsheet theSheet, string cell, string value)
        {
            try
            {
                Excel.Range theRange = theSheet.Cells.get_Range(cell, cell);
                string theExitvalue = "";
                if (theRange.Value2 != null)
                    theExitvalue = theRange.Value2.ToString();
                else
                    theExitvalue = "";

                if (value != "")
                {

                    if (theExitvalue.ToString().Trim() == "")

                        theRange.Value2 = value;
                    else
                        theRange.Value2 = theExitvalue + "  " + value;

                }
                else
                {
                    if (theExitvalue.ToString().Trim() == "")
                        theRange.Value2 = value;
                    else
                        theRange.Value2 = theExitvalue + "  " + value;
                }
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

            }
        }


        #region "Kenya Monthly Report"
        private void WriteCellWiseInExcel_KenyaMonthlyReport(Excel.Spreadsheet theSheet)
        {
            writeInCell(theSheet, "A4", "TableDate", "Date");
            writeInCell(theSheet, "A3", "TableDate", "FName");
            writeInCell(theSheet, "E15", "Table0", "ANCClientsWithHIVTestResult");
            writeInCell(theSheet, "E18", "Table0", "PreventiveARVsWithProphylaxis");
            writeInCell(theSheet, "E19", "Table0", "InfantTestedHIVAt6week");
            writeInCell(theSheet, "E20", "Table0", "InfantTestedHIVAfter3months");
            writeInCell(theSheet, "E22", "Table0", "PartnerReferred");
            writeInCell(theSheet, "E24", "Table0", "WomenCounseledOn");
            writeInCell(theSheet, "E27", "Table0", "PartnerHIVStatus");

            writeInCell(theSheet, "E33", "Table1", "MaternityMotherHIVTestResult");
            writeInCell(theSheet, "E34", "Table1", "MaternityPreventiveARVsWithProphylaxis");
            writeInCell(theSheet, "E35", "Table1", "MaternityInfantPreARV");
            writeInCell(theSheet, "E36", "Table1", "ChildrenEnrolled");
            writeInCell(theSheet, "E37", "Table1", "WomenPostNatalWithSulfa");
            writeInCell(theSheet, "E38", "Table1", "ChildrenAfterBirthWithSeptrin");

        }

        #endregion


        //#region "Uganda Monthly Report"
        //private void WriteCellWiseInExcel_UgandaMonthlyReport(Excel.Spreadsheet   theSheet)
        //{
        //    writeInCell(theSheet, "A3", "TableDate", "FName");
        //    writeInCell(theSheet, "A4", "TableDate", "Date");

        //    writeInCell(theSheet, "B8", "Table0", "PregnantWomenTestedHIVPositive");

        //    writeInCell(theSheet, "B14", "Table1", "PregnantWomenWithNVP");
        //    writeInCell(theSheet, "B15", "Table1", "PregnantWomenWithAZTandNVP");
        //    writeInCell(theSheet, "B16", "Table1", "PregnantWomenWithTripleTherapy");
        //    writeInCell(theSheet, "B17", "Table1", "PregnantWomenWithTripleTherapy1");

        //    writeInCell(theSheet, "B21", "Table2", "HIVPositiveDeliveries");
        //    writeInCell(theSheet, "B22", "Table2", "DeliveriesWithNVPDuringLabour");
        //    writeInCell(theSheet, "B23", "Table2", "DeliveriesWithAZTandNVPDuringLabour");
        //    writeInCell(theSheet, "B24", "Table2", "DeliveriesWithTripleTherapy");
        //    //writeInCell(theSheet, "B25", "Table2", "PartnerReferred");
        //    writeInCell(theSheet, "B27", "Table2", "MaternityMotherWithExclusiveBreastFeeding");
        //    writeInCell(theSheet, "B28", "Table2", "InfantsWithNVP");
        //    writeInCell(theSheet, "B29", "Table2", "InfantsWithNVPANDAZT");

        //    writeInCell(theSheet, "B32", "Table3", "childrenTestedForHIVEqualto2Months");
        //    writeInCell(theSheet, "B33", "Table3", "childrenTestedForHIVEqualto12Months");
        //    writeInCell(theSheet, "B34", "Table3", "childrenTestedForHIVEqualto18Months");
        //    writeInCell(theSheet, "B35", "Table3", "childrenTestedForHIVEqualto5Years");

        //    writeInCell(theSheet, "B37", "Table3", "childrenTestedForHIVPositiveEqualto2Months");
        //    writeInCell(theSheet, "B38", "Table3", "childrenTestedForHIVPositiveEqualto12Months");
        //    writeInCell(theSheet, "B39", "Table3", "childrenTestedForHIVPositiveEqualto18Months");
        //    writeInCell(theSheet, "B40", "Table3", "childrenTestedForHIVPositiveEqualto5Years");



        //}

        //#endregion
        #region "Tanzania Monthly Report"
        private void WriteCellWiseInExcel_TanzaniaMonthlyReport(Excel.Spreadsheet theSheet)
        {
            writeInCell(theSheet, "A2", "TableDate", "Date");

            writeInCell(theSheet, "B9", "Table0", "TestedHIVPosWithANC");

            writeInCell(theSheet, "B21", "Table1", "TestedHIVPosWithLabour");

            writeInCell(theSheet, "B24", "Table2", "SwallowedNVPonlyatLabour");
            writeInCell(theSheet, "B25", "Table2", "SwallowedNVPAZTand3TConlyatLabour");
            writeInCell(theSheet, "B26", "Table2", "ArtWithAnyTripleTherapyatLabour");
            writeInCell(theSheet, "B27", "Table2", "DrugAZTand3TConlyatLabour");
            writeInCell(theSheet, "B28", "Table2", "InfantRecNVP");
            writeInCell(theSheet, "B29", "Table2", "Infantdischargewith1weekAZT");
            writeInCell(theSheet, "B30", "Table2", "Infantdischargewith4weeksAZT");
            writeInCell(theSheet, "B31", "Table2", "MaternityMotherWithExclusiveBreastFeed");
            writeInCell(theSheet, "B32", "Table2", "MaternityMotherWithReplacementFeed");
            //writeInCell(theSheet, "B33", "Table2", "");


            writeInCell(theSheet, "B36", "Table3", "ChildNewRegistered");
            writeInCell(theSheet, "B37", "Table3", "ChildFollowUpWithExclusiveBreastFeed");
            writeInCell(theSheet, "B38", "Table3", "ChildFollowUpWithReplacementFeed");
            writeInCell(theSheet, "B39", "Table3", "ChildWithSeptrin");
            writeInCell(theSheet, "B40", "Table3", "ChildWithSeptrin2Months");
            writeInCell(theSheet, "B41", "Table3", "ChildTested18Months");
            writeInCell(theSheet, "B42", "Table3", "ChildPCRandHIVTestPos18Months");
            writeInCell(theSheet, "B43", "Table3", "ChildRapidandHIVTestPos18Months");
            writeInCell(theSheet, "B44", "Table3", "ChildRapidandConfirmandHIVTestPos18Months");
            writeInCell(theSheet, "B45", "Table3", "ChildPCRandConfirmandHIVTestPos18Months");
            //writeInCell(theSheet, "B46", "Table3", "");
            writeInCell(theSheet, "B47", "Table3", "MotherFollowUpUsingFPMethod");

            writeInCell(theSheet, "B50", "Table4", "MotherEnrolledPMTCT");
            writeInCell(theSheet, "B51", "Table4", "WomenTakenCotrimxizole");
            writeInCell(theSheet, "B52", "Table4", "WomenCounseledOn");
            //writeInCell(theSheet, "B53", "Table4", "WomenDiscloseHIVStatus");
            //writeInCell(theSheet, "B54", "Table4", "");
            //writeInCell(theSheet, "B55", "Table4", "");
            //writeInCell(theSheet, "B56", "Table4", "");
            writeInCell(theSheet, "B57", "Table4", "WomenGivenNVPatANC");
            writeInCell(theSheet, "B58", "Table4", "WomenGivenNVPAndAZTatANC");
            writeInCell(theSheet, "B59", "Table4", "WomenGivenTTatANCART");




        }

        #endregion
        #region "Nigeria Monthly Report"
        private void WriteCellWiseInExcel_NNRIMSMonthlyReport(Excel.Spreadsheet theSheet)
        {
            writeInCell(theSheet, "A2", "", "Facility : " + Session["AppLocation"].ToString());
            writeInCell(theSheet, "A3", theDS.Tables[0].TableName.ToString(), "Date");
            string ColumnI13 = Convert.ToString(Convert.ToInt16(theDS.Tables[1].Rows[0]["C13"].ToString()) + Convert.ToInt16(theDS.Tables[1].Rows[0]["D13"].ToString()));
            string ColumnI14 = Convert.ToString(Convert.ToInt16(theDS.Tables[1].Rows[0]["C14"].ToString()) + Convert.ToInt16(theDS.Tables[1].Rows[0]["D14"].ToString()));
            string ColumnI16 = Convert.ToString(Convert.ToInt16(theDS.Tables[1].Rows[0]["C16"].ToString()) + Convert.ToInt16(theDS.Tables[1].Rows[0]["D16"].ToString()));
            string ColumnI17 = Convert.ToString(Convert.ToInt16(theDS.Tables[1].Rows[0]["C17"].ToString()) + Convert.ToInt16(theDS.Tables[1].Rows[0]["D17"].ToString()));

            writeInCell(theSheet, "C13", theDS.Tables[1].TableName.ToString(), "C13");
            writeInCell(theSheet, "D13", theDS.Tables[1].TableName.ToString(), "D13");
            writeInCell(theSheet, "I13", "", ColumnI13);
            writeInCell(theSheet, "C14", theDS.Tables[1].TableName.ToString(), "C14");
            writeInCell(theSheet, "D14", theDS.Tables[1].TableName.ToString(), "D14");
            writeInCell(theSheet, "I14", "", ColumnI14);
            writeInCell(theSheet, "C16", theDS.Tables[1].TableName.ToString(), "C16");
            writeInCell(theSheet, "D16", theDS.Tables[1].TableName.ToString(), "D16");
            writeInCell(theSheet, "I16", "", ColumnI16);
            writeInCell(theSheet, "C17", theDS.Tables[1].TableName.ToString(), "C17");
            writeInCell(theSheet, "D17", theDS.Tables[1].TableName.ToString(), "D17");
            writeInCell(theSheet, "I17", "", ColumnI17);
            writeInCell(theSheet, "I18", theDS.Tables[1].TableName.ToString(), "I18");
        }
        #endregion


        #region "TB Status by Age and Sex Report"
        private void writeCellWiseInExcel(Excel.Spreadsheet theSheet)
        {
            writeInCell(theSheet, "B3", "", txtStartDate.Text + " To " + txtEndDate.Text);
            writeInCell(theSheet, "C10", theDS.Tables[0].TableName.ToString(), "NoTBStatusFNotonARV-15-100");
            writeInCell(theSheet, "C11", theDS.Tables[0].TableName.ToString(), "NoTBStatusFNotonARV-5-14");
            writeInCell(theSheet, "C12", theDS.Tables[0].TableName.ToString(), "NoTBStatusFNotonARV-2-4");
            writeInCell(theSheet, "C13", theDS.Tables[0].TableName.ToString(), "NoTBStatusFNotonARV-0-1");

            writeInCell(theSheet, "C14", theDS.Tables[1].TableName.ToString(), "NoTBStatusMNotonARV-15-100");
            writeInCell(theSheet, "C15", theDS.Tables[1].TableName.ToString(), "NoTBStatusMNotonARV-5-14");
            writeInCell(theSheet, "C16", theDS.Tables[1].TableName.ToString(), "NoTBStatusMNotonARV-2-4");
            writeInCell(theSheet, "C17", theDS.Tables[1].TableName.ToString(), "NoTBStatusMNotonARV-0-1");

            writeInCell(theSheet, "D10", theDS.Tables[2].TableName.ToString(), "NoTBStatusFOnARV-15-100");
            writeInCell(theSheet, "D11", theDS.Tables[2].TableName.ToString(), "NoTBStatusFOnARV-5-14");
            writeInCell(theSheet, "D12", theDS.Tables[2].TableName.ToString(), "NoTBStatusFOnARV-2-4");
            writeInCell(theSheet, "D13", theDS.Tables[2].TableName.ToString(), "NoTBStatusFOnARV-0-1");

            writeInCell(theSheet, "D14", theDS.Tables[3].TableName.ToString(), "NoTBStatusMOnARV-15-100");
            writeInCell(theSheet, "D15", theDS.Tables[3].TableName.ToString(), "NoTBStatusMOnARV-5-14");
            writeInCell(theSheet, "D16", theDS.Tables[3].TableName.ToString(), "NoTBStatusMOnARV-2-4");
            writeInCell(theSheet, "D17", theDS.Tables[3].TableName.ToString(), "NoTBStatusMOnARV-0-1");

            writeInCell(theSheet, "C30", theDS.Tables[4].TableName.ToString(), "NOTSuspectedFNotonARV-15-100");
            writeInCell(theSheet, "C31", theDS.Tables[4].TableName.ToString(), "NOTSuspectedFNotonARV-5-14");
            writeInCell(theSheet, "C32", theDS.Tables[4].TableName.ToString(), "NOTSuspectedFNotonARV-2-4");
            writeInCell(theSheet, "C33", theDS.Tables[4].TableName.ToString(), "NOTSuspectedFNotonARV-0-1");

            writeInCell(theSheet, "C20", theDS.Tables[24].TableName.ToString(), "CurrentlyonINHProphylaxisFNotonARV-15-100");
            writeInCell(theSheet, "C21", theDS.Tables[24].TableName.ToString(), "CurrentlyonINHProphylaxisFNotonARV-5-14");
            writeInCell(theSheet, "C22", theDS.Tables[24].TableName.ToString(), "CurrentlyonINHProphylaxisFNotonARV-2-4");
            writeInCell(theSheet, "C23", theDS.Tables[24].TableName.ToString(), "CurrentlyonINHProphylaxisFNotonARV-0-1");

            writeInCell(theSheet, "C24", theDS.Tables[25].TableName.ToString(), "CurrentlyonINHProphylaxisMNotonARV-15-100");
            writeInCell(theSheet, "C25", theDS.Tables[25].TableName.ToString(), "CurrentlyonINHProphylaxisMNotonARV-5-14");
            writeInCell(theSheet, "C26", theDS.Tables[25].TableName.ToString(), "CurrentlyonINHProphylaxisMNotonARV-2-4");
            writeInCell(theSheet, "C27", theDS.Tables[25].TableName.ToString(), "CurrentlyonINHProphylaxisMNotonARV-0-1");

            writeInCell(theSheet, "D20", theDS.Tables[26].TableName.ToString(), "CurrentlyonINHProphylaxisFOnARV-15-100");
            writeInCell(theSheet, "D21", theDS.Tables[26].TableName.ToString(), "CurrentlyonINHProphylaxisFOnARV-5-14");
            writeInCell(theSheet, "D22", theDS.Tables[26].TableName.ToString(), "CurrentlyonINHProphylaxisFOnARV-2-4");
            writeInCell(theSheet, "D23", theDS.Tables[26].TableName.ToString(), "CurrentlyonINHProphylaxisFOnARV-0-1");

            writeInCell(theSheet, "D24", theDS.Tables[27].TableName.ToString(), "CurrentlyonINHProphylaxisMOnARV-15-100");
            writeInCell(theSheet, "D25", theDS.Tables[27].TableName.ToString(), "CurrentlyonINHProphylaxisMOnARV-5-14");
            writeInCell(theSheet, "D26", theDS.Tables[27].TableName.ToString(), "CurrentlyonINHProphylaxisMOnARV-2-4");
            writeInCell(theSheet, "D27", theDS.Tables[27].TableName.ToString(), "CurrentlyonINHProphylaxisMOnARV-0-1");

            writeInCell(theSheet, "C34", theDS.Tables[5].TableName.ToString(), "NOTSuspectedMNotonARV-15-100");
            writeInCell(theSheet, "C35", theDS.Tables[5].TableName.ToString(), "NOTSuspectedMNotonARV-5-14");
            writeInCell(theSheet, "C36", theDS.Tables[5].TableName.ToString(), "NOTSuspectedMNotonARV-2-4");
            writeInCell(theSheet, "C37", theDS.Tables[5].TableName.ToString(), "NOTSuspectedMNotonARV-0-1");

            writeInCell(theSheet, "D30", theDS.Tables[6].TableName.ToString(), "NOTSuspectedFOnARV-15-100");
            writeInCell(theSheet, "D31", theDS.Tables[6].TableName.ToString(), "NOTSuspectedFOnARV-5-14");
            writeInCell(theSheet, "D32", theDS.Tables[6].TableName.ToString(), "NOTSuspectedFOnARV-2-4");
            writeInCell(theSheet, "D33", theDS.Tables[6].TableName.ToString(), "NOTSuspectedFOnARV-0-1");

            writeInCell(theSheet, "D34", theDS.Tables[7].TableName.ToString(), "NOTSuspectedMOnARV-15-100");
            writeInCell(theSheet, "D35", theDS.Tables[7].TableName.ToString(), "NOTSuspectedMOnARV-5-14");
            writeInCell(theSheet, "D36", theDS.Tables[7].TableName.ToString(), "NOTSuspectedMOnARV-2-4");
            writeInCell(theSheet, "D37", theDS.Tables[7].TableName.ToString(), "NOTSuspectedMOnARV-0-1");

            writeInCell(theSheet, "C40", theDS.Tables[8].TableName.ToString(), "TBSuspectedandResultRecordedFNotonARV-15-100");
            writeInCell(theSheet, "C41", theDS.Tables[8].TableName.ToString(), "TBSuspectedandResultRecordedFNotonARV-5-14");
            writeInCell(theSheet, "C42", theDS.Tables[8].TableName.ToString(), "TBSuspectedandResultRecordedFNotonARV-2-4");
            writeInCell(theSheet, "C43", theDS.Tables[8].TableName.ToString(), "TBSuspectedandResultRecordedFNotonARV-0-1");

            writeInCell(theSheet, "C44", theDS.Tables[9].TableName.ToString(), "TBSuspectedandResultRecordedMNotonARV-15-100");
            writeInCell(theSheet, "C45", theDS.Tables[9].TableName.ToString(), "TBSuspectedandResultRecordedMNotonARV-5-14");
            writeInCell(theSheet, "C46", theDS.Tables[9].TableName.ToString(), "TBSuspectedandResultRecordedMNotonARV-2-4");
            writeInCell(theSheet, "C47", theDS.Tables[9].TableName.ToString(), "TBSuspectedandResultRecordedMNotonARV-0-1");

            writeInCell(theSheet, "D40", theDS.Tables[10].TableName.ToString(), "TBSuspectedandResultRecordedFOnARV-15-100");
            writeInCell(theSheet, "D41", theDS.Tables[10].TableName.ToString(), "TBSuspectedandResultRecordedFOnARV-5-14");
            writeInCell(theSheet, "D42", theDS.Tables[10].TableName.ToString(), "TBSuspectedandResultRecordedFOnARV-2-4");
            writeInCell(theSheet, "D43", theDS.Tables[10].TableName.ToString(), "TBSuspectedandResultRecordedFOnARV-0-1");

            writeInCell(theSheet, "D44", theDS.Tables[11].TableName.ToString(), "TBSuspectedandResultRecordedMOnARV-15-100");
            writeInCell(theSheet, "D45", theDS.Tables[11].TableName.ToString(), "TBSuspectedandResultRecordedMOnARV-5-14");
            writeInCell(theSheet, "D46", theDS.Tables[11].TableName.ToString(), "TBSuspectedandResultRecordedMOnARV-2-4");
            writeInCell(theSheet, "D47", theDS.Tables[11].TableName.ToString(), "TBSuspectedandResultRecordedMOnARV-0-1");

            writeInCell(theSheet, "C50", theDS.Tables[12].TableName.ToString(), "TBSuspectedandReferredforEvaFNotonARV-15-100");
            writeInCell(theSheet, "C51", theDS.Tables[12].TableName.ToString(), "TBSuspectedandReferredforEvaFNotonARV-5-14");
            writeInCell(theSheet, "C52", theDS.Tables[12].TableName.ToString(), "TBSuspectedandReferredforEvaFNotonARV-2-4");
            writeInCell(theSheet, "C53", theDS.Tables[12].TableName.ToString(), "TBSuspectedandReferredforEvaFNotonARV-0-1");

            writeInCell(theSheet, "C54", theDS.Tables[13].TableName.ToString(), "TBSuspectedandReferredforEvaMNotonARV-15-100");
            writeInCell(theSheet, "C55", theDS.Tables[13].TableName.ToString(), "TBSuspectedandReferredforEvaMNotonARV-5-14");
            writeInCell(theSheet, "C56", theDS.Tables[13].TableName.ToString(), "TBSuspectedandReferredforEvaMNotonARV-2-4");
            writeInCell(theSheet, "C57", theDS.Tables[13].TableName.ToString(), "TBSuspectedandReferredforEvaMNotonARV-0-1");

            writeInCell(theSheet, "D50", theDS.Tables[14].TableName.ToString(), "TBSuspectedandReferredforEvaFOnARV-15-100");
            writeInCell(theSheet, "D51", theDS.Tables[14].TableName.ToString(), "TBSuspectedandReferredforEvaFOnARV-5-14");
            writeInCell(theSheet, "D52", theDS.Tables[14].TableName.ToString(), "TBSuspectedandReferredforEvaFOnARV-2-4");
            writeInCell(theSheet, "D53", theDS.Tables[14].TableName.ToString(), "TBSuspectedandReferredforEvaFOnARV-0-1");

            writeInCell(theSheet, "D54", theDS.Tables[15].TableName.ToString(), "TBSuspectedandReferredforEvaMOnARV-15-100");
            writeInCell(theSheet, "D55", theDS.Tables[15].TableName.ToString(), "TBSuspectedandReferredforEvaMOnARV-5-14");
            writeInCell(theSheet, "D56", theDS.Tables[15].TableName.ToString(), "TBSuspectedandReferredforEvaMOnARV-2-4");
            writeInCell(theSheet, "D57", theDS.Tables[15].TableName.ToString(), "TBSuspectedandReferredforEvaMOnARV-0-1");

            writeInCell(theSheet, "C62", theDS.Tables[16].TableName.ToString(), "TBConfermedFNotonARV-15-100");
            writeInCell(theSheet, "C63", theDS.Tables[16].TableName.ToString(), "TBConfermedFNotonARV-5-14");
            writeInCell(theSheet, "C64", theDS.Tables[16].TableName.ToString(), "TBConfermedFNotonARV-2-4");
            writeInCell(theSheet, "C65", theDS.Tables[16].TableName.ToString(), "TBConfermedFNotonARV-0-1");

            writeInCell(theSheet, "C66", theDS.Tables[17].TableName.ToString(), "TBConfermedMNotonARV-15-100");
            writeInCell(theSheet, "C67", theDS.Tables[17].TableName.ToString(), "TBConfermedMNotonARV-5-14");
            writeInCell(theSheet, "C68", theDS.Tables[17].TableName.ToString(), "TBConfermedMNotonARV-2-4");
            writeInCell(theSheet, "C69", theDS.Tables[17].TableName.ToString(), "TBConfermedMNotonARV-0-1");

            writeInCell(theSheet, "D62", theDS.Tables[18].TableName.ToString(), "TBConfermedFOnARV-15-100");
            writeInCell(theSheet, "D63", theDS.Tables[18].TableName.ToString(), "TBConfermedFOnARV-5-14");
            writeInCell(theSheet, "D64", theDS.Tables[18].TableName.ToString(), "TBConfermedFOnARV-2-4");
            writeInCell(theSheet, "D65", theDS.Tables[18].TableName.ToString(), "TBConfermedFOnARV-0-1");

            writeInCell(theSheet, "D66", theDS.Tables[19].TableName.ToString(), "TBConfermedMOnARV-15-100");
            writeInCell(theSheet, "D67", theDS.Tables[19].TableName.ToString(), "TBConfermedMOnARV-5-14");
            writeInCell(theSheet, "D68", theDS.Tables[19].TableName.ToString(), "TBConfermedMOnARV-2-4");
            writeInCell(theSheet, "D69", theDS.Tables[19].TableName.ToString(), "TBConfermedMOnARV-0-1");

            writeInCell(theSheet, "C72", theDS.Tables[20].TableName.ToString(), "CurrentlyonTBFNotonARV-15-100");
            writeInCell(theSheet, "C73", theDS.Tables[20].TableName.ToString(), "CurrentlyonTBFNotonARV-5-14");
            writeInCell(theSheet, "C74", theDS.Tables[20].TableName.ToString(), "CurrentlyonTBFNotonARV-2-4");
            writeInCell(theSheet, "C75", theDS.Tables[20].TableName.ToString(), "CurrentlyonTBFNotonARV-0-1");

            writeInCell(theSheet, "C76", theDS.Tables[21].TableName.ToString(), "CurrentlyonTBMNotonARV-15-100");
            writeInCell(theSheet, "C77", theDS.Tables[21].TableName.ToString(), "CurrentlyonTBMNotonARV-5-14");
            writeInCell(theSheet, "C78", theDS.Tables[21].TableName.ToString(), "CurrentlyonTBMNotonARV-2-4");
            writeInCell(theSheet, "C79", theDS.Tables[21].TableName.ToString(), "CurrentlyonTBMNotonARV-0-1");

            writeInCell(theSheet, "D72", theDS.Tables[22].TableName.ToString(), "CurrentlyonTBFOnARV-15-100");
            writeInCell(theSheet, "D73", theDS.Tables[22].TableName.ToString(), "CurrentlyonTBFOnARV-5-14");
            writeInCell(theSheet, "D74", theDS.Tables[22].TableName.ToString(), "CurrentlyonTBFOnARV-2-4");
            writeInCell(theSheet, "D75", theDS.Tables[22].TableName.ToString(), "CurrentlyonTBFOnARV-0-1");

            writeInCell(theSheet, "D76", theDS.Tables[23].TableName.ToString(), "CurrentlyonTBMOnARV-15-100");
            writeInCell(theSheet, "D77", theDS.Tables[23].TableName.ToString(), "CurrentlyonTBMOnARV-5-14");
            writeInCell(theSheet, "D78", theDS.Tables[23].TableName.ToString(), "CurrentlyonTBMOnARV-2-4");
            writeInCell(theSheet, "D79", theDS.Tables[23].TableName.ToString(), "CurrentlyonTBMOnARV-0-1");


            writeInCell(theSheet, "C82", theDS.Tables[28].TableName.ToString(), "HasTBFNotonARV-15-100");
            writeInCell(theSheet, "C83", theDS.Tables[28].TableName.ToString(), "HasTBFNotonARV-5-14");
            writeInCell(theSheet, "C84", theDS.Tables[28].TableName.ToString(), "HasTBFNotonARV-2-4");
            writeInCell(theSheet, "C85", theDS.Tables[28].TableName.ToString(), "HasTBFNotonARV-0-1");

            writeInCell(theSheet, "C86", theDS.Tables[29].TableName.ToString(), "HasTBMNotonARV-15-100");
            writeInCell(theSheet, "C87", theDS.Tables[29].TableName.ToString(), "HasTBMNotonARV-5-14");
            writeInCell(theSheet, "C88", theDS.Tables[29].TableName.ToString(), "HasTBMNotonARV-2-4");
            writeInCell(theSheet, "C89", theDS.Tables[29].TableName.ToString(), "HasTBMNotonARV-0-1");

            writeInCell(theSheet, "D82", theDS.Tables[30].TableName.ToString(), "HasTBFOnARV-15-100");
            writeInCell(theSheet, "D83", theDS.Tables[30].TableName.ToString(), "HasTBFOnARV-5-14");
            writeInCell(theSheet, "D84", theDS.Tables[30].TableName.ToString(), "HasTBFOnARV-2-4");
            writeInCell(theSheet, "D85", theDS.Tables[30].TableName.ToString(), "HasTBFOnARV-0-1");

            writeInCell(theSheet, "D86", theDS.Tables[31].TableName.ToString(), "HasTBMOnARV-15-100");
            writeInCell(theSheet, "D87", theDS.Tables[31].TableName.ToString(), "HasTBMOnARV-5-14");
            writeInCell(theSheet, "D88", theDS.Tables[31].TableName.ToString(), "HasTBMOnARV-2-4");
            writeInCell(theSheet, "D89", theDS.Tables[31].TableName.ToString(), "HasTBMOnARV-0-1");

            writeInCell(theSheet, "B5", theDS.Tables[32].TableName.ToString(), "FacilityName");
        }
        #endregion

        #region "User Detail"
        private void WriteCellWiseInExcel_UserDetail(Excel.Spreadsheet thesheet)
        {
            writeInCell(thesheet, "B1", "", "Quality Assurance From  " + TxtFromDate.Text + "  To  " + TxtToDate.Text);
            int i = 8;
            int dq;
            string tdate = "01-01-1980";
            int count = 0;
            int totalform = 0;//count total form for a day
            int totaldq = 0;//count data quality check  for a day
            int totalallform = 0;
            int totalalldq = 0;
            int zeroval = 0;
            int rowcount = 0;
            if (chkAllUser.Checked == true)
            {
                foreach (DataRow Dr in theDS.Tables[0].Rows)
                {

                    if (count != 0)
                    {
                        writeInCell(thesheet, "E" + i + "", "", totalform.ToString());
                        writeInCell(thesheet, "F" + i + "", "", totaldq.ToString());
                        totalallform = totalform + totalallform;
                        totalalldq = totaldq + totalalldq;
                        i = i + 1;
                        writeInCell(thesheet, "E" + i + "", "", totalallform.ToString());
                        writeInCell(thesheet, "F" + i + "", "", totalalldq.ToString());
                        i = i + 1;
                        totalform = 0;
                        totaldq = 0;
                        totalallform = 0;
                        totalalldq = 0;

                    }
                    count = 0;

                    writeInCell(thesheet, "B" + i + "", "", Dr["UserName"].ToString());
                    i = i + 1;


                    foreach (DataRow Drid in theDS.Tables[1].Rows)
                    {

                        rowcount++;
                        if (Dr["UserId"].ToString() == Drid["UserId"].ToString())
                        {
                            //for first time
                            if (count == 0)
                            {
                                writeInCell(thesheet, "C" + i + "", "", Drid["TranDate"].ToString());
                                i = i + 1;
                                count = count + 1;
                                totalform = totalform + Convert.ToInt16(Drid["Count"].ToString());


                            }
                            else
                            {
                                if (Drid["TranDate"].ToString() == tdate)
                                {
                                    totalform = totalform + Convert.ToInt16(Drid["Count"].ToString());

                                    writeInCell(thesheet, "C" + i + "", "", " ");
                                }
                                else
                                {
                                    writeInCell(thesheet, "E" + i + "", "", totalform.ToString());
                                    writeInCell(thesheet, "F" + i + "", "", totaldq.ToString());
                                    totalallform = totalform + totalallform;
                                    totalalldq = totaldq + totalalldq;
                                    totalform = Convert.ToInt16(Drid["Count"].ToString());
                                    totaldq = 0;
                                    i = i + 1;
                                    writeInCell(thesheet, "C" + i + "", "", Drid["TranDate"].ToString());
                                    i = i + 1;
                                }
                            }
                            writeInCell(thesheet, "D" + i + "", "", Drid["FormName"].ToString());
                            writeInCell(thesheet, "E" + i + "", "", Drid["Count"].ToString());
                            dq = 0;
                            foreach (DataRow Drdq in theDS.Tables[2].Rows)
                            {
                                if (Drdq[1].ToString() == Drid["TranDate"].ToString() && Drdq["UserId"].ToString() == Drid["UserId"].ToString() && Drdq["Priority"].ToString() == Drid["Priority"].ToString())
                                {
                                    writeInCell(thesheet, "F" + i + "", "", Drdq["Dataquality"].ToString());
                                    dq = dq + 1;
                                    totaldq = totaldq + Convert.ToInt16(Drdq["Dataquality"].ToString());
                                }

                            }
                            if (dq == 0)
                            {
                                writeInCell(thesheet, "F" + i + "", "", zeroval.ToString());
                            }

                            i = i + 1;
                            tdate = Drid["TranDate"].ToString();
                            //if (theDS.Tables[1].Rows.Count == rowcount+1)
                            //{

                            //    writeInCell(thesheet, "E" + i + "", "", totalform.ToString());
                            //    writeInCell(thesheet, "F" + i + "", "", totaldq.ToString());
                            //    totalallform = totalform + totalallform;
                            //    totalalldq = totaldq + totalalldq;
                            //    i = i + 1;

                            //}
                        }


                    }

                }
                writeInCell(thesheet, "E" + i + "", "", totalform.ToString());
                writeInCell(thesheet, "F" + i + "", "", totaldq.ToString());
                totalallform = totalform + totalallform;
                totalalldq = totaldq + totalalldq;
                i = i + 1;
                writeInCell(thesheet, "E" + i + "", "", totalallform.ToString());
                writeInCell(thesheet, "F" + i + "", "", totalalldq.ToString());
                i = i + 1;
            }
            else
            {
                foreach (DataRow Dr in theDS.Tables[0].Rows)
                {
                    if (Dr["UserName"].ToString() == ddlSelectUser.SelectedItem.ToString())
                    {
                        writeInCell(thesheet, "B" + i + "", "", Dr["UserName"].ToString());
                    }
                }

                foreach (DataRow Drid in theDS.Tables[1].Rows)
                {
                    rowcount++;

                    //for first time
                    if (count == 0)
                    {
                        writeInCell(thesheet, "C" + i + "", "", Drid["TranDate"].ToString());
                        i = i + 1;
                        count = count + 1;
                        totalform = totalform + Convert.ToInt16(Drid["Count"].ToString());


                    }
                    else
                    {
                        if (Drid["TranDate"].ToString() == tdate)
                        {
                            totalform = totalform + Convert.ToInt16(Drid["Count"].ToString());

                            writeInCell(thesheet, "C" + i + "", "", " ");
                        }
                        else
                        {
                            writeInCell(thesheet, "E" + i + "", "", totalform.ToString());
                            writeInCell(thesheet, "F" + i + "", "", totaldq.ToString());
                            totalallform = totalform + totalallform;
                            totalalldq = totaldq + totalalldq;
                            totalform = Convert.ToInt16(Drid["Count"].ToString());
                            totaldq = 0;
                            i = i + 1;
                            writeInCell(thesheet, "C" + i + "", "", Drid["TranDate"].ToString());
                            i = i + 1;
                        }
                    }

                    writeInCell(thesheet, "D" + i + "", "", Drid["FormName"].ToString());
                    writeInCell(thesheet, "E" + i + "", "", Drid["Count"].ToString());
                    dq = 0;
                    foreach (DataRow Drdq in theDS.Tables[2].Rows)
                    {
                        if (Drdq[1].ToString() == Drid["TranDate"].ToString() && Drdq["UserId"].ToString() == Drid["UserId"].ToString() && Drdq["Priority"].ToString() == Drid["Priority"].ToString())
                        {
                            writeInCell(thesheet, "F" + i + "", "", Drdq["Dataquality"].ToString());
                            dq = dq + 1;
                            totaldq = totaldq + Convert.ToInt16(Drdq["Dataquality"].ToString());
                        }

                    }
                    if (dq == 0)
                    {
                        writeInCell(thesheet, "F" + i + "", "", zeroval.ToString());
                    }
                    i = i + 1;
                    tdate = Drid["TranDate"].ToString();
                    if (theDS.Tables[1].Rows.Count == rowcount)
                    {

                        writeInCell(thesheet, "E" + i + "", "", totalform.ToString());
                        writeInCell(thesheet, "F" + i + "", "", totaldq.ToString());
                        totalallform = totalform + totalallform;
                        totalalldq = totaldq + totalalldq;
                        i = i + 1;

                    }
                }

                if (count != 0)
                {

                    writeInCell(thesheet, "E" + i + "", "", totalallform.ToString());
                    writeInCell(thesheet, "F" + i + "", "", totalalldq.ToString());
                }

            }



        }
        #endregion

        #region "Born-To-Live"

        private void WriteCellWiseInExcel_BornToLive(Excel.Spreadsheet theSheet)
        {
            //string iColumnE6a = Convert.ToString(Convert.ToInt16(theDS.Tables[9].Rows[0][0].ToString()) + Convert.ToInt16(theDS.Tables[9].Rows[1][0].ToString()));
            //string  iColumnF6a = Convert.ToString(Convert.ToInt16(theDS.Tables[10].Rows[0][0].ToString()) + Convert.ToInt16(theDS.Tables[10].Rows[1][0].ToString()));
            writeInCell(theSheet, "B8", "", "Hospital : " + Session["AppLocation"].ToString());
            writeInCell(theSheet, "C8", "", "Reporting Month :" + ddlMonthKenyaHealth.SelectedItem.Text.ToString());
            writeInCell(theSheet, "E8", "", "Compiled Date : " + Application["AppCurrentDate"].ToString());

            writeInCell(theSheet, "E17", theDS.Tables[0].TableName.ToString(), "Column_E1");
            writeInCell(theSheet, "F17", theDS.Tables[1].TableName.ToString(), "Column_F1");
            writeInCell(theSheet, "E21", theDS.Tables[2].TableName.ToString(), "Column_E2");
            writeInCell(theSheet, "F21", theDS.Tables[3].TableName.ToString(), "Column_F2");
            writeInCell(theSheet, "E23", theDS.Tables[4].TableName.ToString(), "Column_E3");
            writeInCell(theSheet, "F23", theDS.Tables[5].TableName.ToString(), "Column_F3");
            int colE5 = 0;
            int colE6 = 0;
            int colF6 = 0;
            int colE8 = 0;
            foreach (DataRow dr in theDS.Tables[8].Rows)
            {
                colE5 = colE5 + Convert.ToInt16(dr[0]);
            }
            foreach (DataRow dr in theDS.Tables[9].Rows)
            {
                colE6 = colE6 + Convert.ToInt16(dr[0]);
            }
            foreach (DataRow dr in theDS.Tables[10].Rows)
            {
                colF6 = colF6 + Convert.ToInt16(dr[0]);
            }
            foreach (DataRow dr in theDS.Tables[12].Rows)
            {
                colE8 = colE8 + Convert.ToInt16(dr[0]);
            }
            //int colE5 = Convert.ToInt16(theDS.Tables[8].Rows[0][0]) + Convert.ToInt16(theDS.Tables[8].Rows[1][0]) + Convert.ToInt16(theDS.Tables[8].Rows[2][0]) + Convert.ToInt16(theDS.Tables[8].Rows[3][0]);
            //int colE6 = Convert.ToInt16(theDS.Tables[9].Rows[0][0]) + Convert.ToInt16(theDS.Tables[9].Rows[1][0]) + Convert.ToInt16(theDS.Tables[9].Rows[2][0]) + Convert.ToInt16(theDS.Tables[9].Rows[3][0]);
            //int colF6 = Convert.ToInt16(theDS.Tables[10].Rows[0][0]) + Convert.ToInt16(theDS.Tables[10].Rows[1][0]) + Convert.ToInt16(theDS.Tables[10].Rows[2][0]) + Convert.ToInt16(theDS.Tables[10].Rows[3][0]);
            //int colE8 = Convert.ToInt16(theDS.Tables[12].Rows[0][0]) + Convert.ToInt16(theDS.Tables[12].Rows[1][0]) ;
            writeInCell(theSheet, "E32", theDS.Tables[6].TableName.ToString(), "Column_E4");
            writeInCell(theSheet, "F32", theDS.Tables[7].TableName.ToString(), "Column_F4");

            writeInCell(theSheet, "E36", "", colE5.ToString());
            writeInCell(theSheet, "E37", "", colE6.ToString());
            writeInCell(theSheet, "F37", "", colF6.ToString());
            writeInCell(theSheet, "F38", theDS.Tables[11].TableName.ToString(), "Column_F7");
            writeInCell(theSheet, "F39", "", colE8.ToString());

            writeInCell(theSheet, "E41", theDS.Tables[13].TableName.ToString(), "Column_E9");
            writeInCell(theSheet, "E42", theDS.Tables[14].TableName.ToString(), "Column_E10");
            writeInCell(theSheet, "E45", theDS.Tables[15].TableName.ToString(), "Column_E11");
            writeInCell(theSheet, "F45", theDS.Tables[16].TableName.ToString(), "Column_F11");
            writeInCell(theSheet, "E46", theDS.Tables[17].TableName.ToString(), "Column_E11A");
            writeInCell(theSheet, "F46", theDS.Tables[18].TableName.ToString(), "Column_F11A");

            writeInCell(theSheet, "E50", theDS.Tables[19].TableName.ToString(), "Column_E12");
            writeInCell(theSheet, "E52", theDS.Tables[20].TableName.ToString(), "Column_E13");
            writeInCell(theSheet, "F52", theDS.Tables[21].TableName.ToString(), "Column_F13");
            writeInCell(theSheet, "E53", theDS.Tables[22].TableName.ToString(), "Column_E13A");
            writeInCell(theSheet, "F53", theDS.Tables[23].TableName.ToString(), "Column_F13B");
            writeInCell(theSheet, "F54", theDS.Tables[24].TableName.ToString(), "Column_F14");
            writeInCell(theSheet, "F55", theDS.Tables[25].TableName.ToString(), "Column_F15");
            writeInCell(theSheet, "F57", theDS.Tables[26].TableName.ToString(), "Column_F16");
            writeInCell(theSheet, "F58", theDS.Tables[27].TableName.ToString(), "Column_F17");
            writeInCell(theSheet, "F59", theDS.Tables[28].TableName.ToString(), "Column_F17A");

            writeInCell(theSheet, "F61", theDS.Tables[29].TableName.ToString(), "Column_F18");
            writeInCell(theSheet, "F62", theDS.Tables[30].TableName.ToString(), "Column_F18A");
            writeInCell(theSheet, "F63", theDS.Tables[31].TableName.ToString(), "Column_F19");
            writeInCell(theSheet, "F64", theDS.Tables[32].TableName.ToString(), "Column_F19A");
            writeInCell(theSheet, "F66", theDS.Tables[33].TableName.ToString(), "Column_F66");
            writeInCell(theSheet, "F67", theDS.Tables[34].TableName.ToString(), "Column_F67");

            writeInCell(theSheet, "E73", theDS.Tables[35].TableName.ToString(), "Column_E20");
            writeInCell(theSheet, "F73", theDS.Tables[36].TableName.ToString(), "Column_F20");
            writeInCell(theSheet, "E74", theDS.Tables[37].TableName.ToString(), "Column_E20A");
            writeInCell(theSheet, "F74", theDS.Tables[38].TableName.ToString(), "Column_F20A");
            writeInCell(theSheet, "E75", theDS.Tables[39].TableName.ToString(), "Column_E21");
            writeInCell(theSheet, "F75", theDS.Tables[40].TableName.ToString(), "Column_F21");
            writeInCell(theSheet, "F76", theDS.Tables[41].TableName.ToString(), "Column_F22");
            writeInCell(theSheet, "F78", theDS.Tables[42].TableName.ToString(), "Column_F23");
            writeInCell(theSheet, "F79", theDS.Tables[43].TableName.ToString(), "Column_F24");

            //writeInCell(theSheet, "E82", theDS.Tables[44].TableName.ToString(), "Column_E25");
            //writeInCell(theSheet, "F82", theDS.Tables[45].TableName.ToString(), "Column_F25");


        }
        private void WriteCellWiseInExcel_NASCOP(Excel.Spreadsheet theSheet)
        {


            //writeInCell(theSheet, "D8", "", "" + ddlMonthKenyaHealth.SelectedItem.Text.ToString());
            //writeInCell(theSheet, "J8", "", "Compiled Date : " + Application["AppCurrentDate"].ToString());
            writeInCell(theSheet, "A5", "TableDate", "Date");

            writeInCell(theSheet, "D14", "Table1", "ANCWithHIVTestResultPositive");
            writeInCell(theSheet, "F14", "Table1", "MaternityWithHIVTestResultPositive");
            writeInCell(theSheet, "G14", "Table1", "FollowUpHIVTestResultPositive");

            writeInCell(theSheet, "D16", "Table2", "PreventiveARVsDuringPregnancy");
            writeInCell(theSheet, "F16", "Table2", "PreventiveARVsAtDelivery");
            writeInCell(theSheet, "G16", "Table2", "PreventiveARVsWithPostNatal");

            writeInCell(theSheet, "G17", "Table3", "InfantsWithNVP");
            writeInCell(theSheet, "F18", "Table3", "InfantsWithNVP1");

            writeInCell(theSheet, "D19", "Table4", "WomenDuringPregnancyWithSulfa");
            writeInCell(theSheet, "F19", "Table4", "WomenAtDeliveryWithSulfa");
            writeInCell(theSheet, "G19", "Table4", "WomenPostNatalWithSulfa");
            writeInCell(theSheet, "F20", "Table4", "ChildrenAtBirthWithSeptrin");
            writeInCell(theSheet, "G20", "Table4", "ChildrenAfterBirthWithSeptrin");

            writeInCell(theSheet, "D23", "Table5", "ANCClientsWithPartnerHIVTestResultPositive");
            writeInCell(theSheet, "F23", "Table5", "MaternityWithPartnerHIVTestResultPositive");
            writeInCell(theSheet, "G23", "Table5", "FollowUpWithPartnerHIVTestResultPositive");

            writeInCell(theSheet, "D24", "Table6", "ANCMotherReferredWithHIVTestResultPositive");
            writeInCell(theSheet, "F24", "Table6", "MaternityMotherReferredWithHIVTestResultPositive");
            writeInCell(theSheet, "G24", "Table6", "PostNatalMotherReferredWithHIVTestResultPositive");
            writeInCell(theSheet, "G25", "Table6", "FollowUpInfantReferredWithHIVTestResultPositive");
            writeInCell(theSheet, "D26", "Table6", "ANCPartnerReferredHIVTestResultPositive");
            writeInCell(theSheet, "F26", "Table6", "MaternityPartnerReferredHIVTestResultPositive");
            writeInCell(theSheet, "G26", "Table6", "FollowUpPartnerReferredHIVTestResultPositive");

            writeInCell(theSheet, "D27", "Table7", "WomenCounseledOnWithANCForm");
            writeInCell(theSheet, "F27", "Table7", "WomenCounseledOnWithMaternityMotherForm");
            writeInCell(theSheet, "G27", "Table7", "WomenCounseledOnWithFollowUp");

            writeInCell(theSheet, "G28", "Table8", "InfantTestedHIVAt6week");
            writeInCell(theSheet, "G29", "Table8", "InfantTestedHIVAfter3months");


        }

        #endregion

        #region "Non-ARTPatientReport"
        private void writeCellWiseInExcel_Non_ARTPatientReport(Excel.Spreadsheet thesheet)
        {
            writeInCell(thesheet, "B1", theDS.Tables[0].TableName.ToString(), "FacilityName");
            writeInCell(thesheet, "B2", "", txtStartDate.Text + " to " + Application["AppCurrentDate"]);

            int j = 0;
            for (int i = 6; i < theDS.Tables[0].Rows.Count + 6; i++)
            {
                //writeInCell(thesheet, "B" + i + "", "", theDS.Tables[0].Rows[j]["FacilityName"].ToString());
                writeInCell(thesheet, "B" + i + "", "", theDS.Tables[0].Rows[j]["PatientName"].ToString());
                writeInCell(thesheet, "C" + i + "", "", theDS.Tables[0].Rows[j]["PatientID"].ToString());
                writeInCell(thesheet, "D" + i + "", "", theDS.Tables[0].Rows[j]["PatientFile#"].ToString());
                writeInCell(thesheet, "E" + i + "", "", theDS.Tables[0].Rows[j]["LastVisitDate"].ToString());
                writeInCell(thesheet, "F" + i + "", "", theDS.Tables[0].Rows[j]["DaysSinceLastVisit"].ToString());
                writeInCell(thesheet, "G" + i + "", "", theDS.Tables[0].Rows[j]["PhoneNumber"].ToString());
                writeInCell(thesheet, "H" + i + "", "", theDS.Tables[0].Rows[j]["ContactDetails"].ToString());
                writeInCell(thesheet, "I" + i + "", "", theDS.Tables[0].Rows[j]["Village/Street"].ToString());
                writeInCell(thesheet, "I" + i + "", "", theDS.Tables[0].Rows[j]["Ward"].ToString());
                writeInCell(thesheet, "J" + i + "", "", theDS.Tables[0].Rows[j]["Region"].ToString());
                writeInCell(thesheet, "J" + i + "", "", theDS.Tables[0].Rows[j]["Division"].ToString());
                j++;

            }


        }
        #endregion

        #region "ARVCohortReport"
        private void writeCellWiseInExcel_ARVCohortReport(Excel.Spreadsheet theSheet)
        {


            //writeInCell(theSheet, "B1", theDS.Tables[0].TableName.ToString(), "FacilityName");
            //writeInCell(theSheet, "B2", "", ddMonth.SelectedItem.Text.ToString());
            //writeInCell(theSheet, "B2", "", txtYear.Text);
            //writeInCell(theSheet, "B3", "", ddMonth.SelectedItem.Text.ToString());
            //writeInCell(theSheet, "B4", "", string.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["Date"]));
            //writeInCell(theSheet, "B9", "", string.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["Date"]));
            //writeInCell(theSheet, "B10", "", string.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["Date"]));
            //writeInCell(theSheet, "D8", theDS.Tables[1].TableName.ToString(), "D8");
            //writeInCell(theSheet, "D9", theDS.Tables[1].TableName.ToString(), "D9");
            //writeInCell(theSheet, "D10", theDS.Tables[1].TableName.ToString(), "D10");
            //writeInCell(theSheet, "D13", theDS.Tables[1].TableName.ToString(), "D13");
            ////writeInCell(theSheet, "B16", "", string.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["Date1"]));
            //writeInCell(theSheet, "B16", "", ddMonth.SelectedItem.Text.ToString());
            //writeInCell(theSheet, "B16", "", txtYear.Text);
        }
        #endregion

        #region "ArvRegimenReport"
        private void writeCellWiseInExcel_ARVRegimenReport(Excel.Spreadsheet theSheet)
        {
            //    Excel.Range theRange;
            //    writeInCell(theSheet, "B2", "", txtStartDate.Text + " To " + txtEndDate.Text);
            //    writeInCell(theSheet, "B3", theDS.Tables[0].TableName.ToString(), "FacilityName");

            //    Int32 theRows = 7;
            //    Int32 theOldRow = 8;
            //    int theTotalPatient = 0;
            //    double thePercentSum = 0;
            //    int j = 0;
            //    for (int i = 8; j < theDS.Tables[1].Rows.Count; i++)
            //    {
            //        writeInCell(theSheet, "B" + i + "", "", theDS.Tables[1].Rows[j]["RegimenCode"].ToString());
            //        writeInCell(theSheet, "C" + i + "", "", theDS.Tables[1].Rows[j]["RegimenType"].ToString());
            //        writeInCell(theSheet, "D" + i + "", "", theDS.Tables[1].Rows[j]["Number of Visitor"].ToString());
            //        theTotalPatient = theTotalPatient + Convert.ToInt32(theDS.Tables[1].Rows[j]["Number of Visitor"]);
            //        j++;
            //        theRows++;

            //    }
            //    j = 0;
            //    for (int i = 8; j < theDS.Tables[1].Rows.Count; i++)
            //    {
            //        writeInCell(theSheet, "E" + i + "", "", Convert.ToDecimal(Convert.ToDecimal(theDS.Tables[1].Rows[j]["Number of Visitor"]) * 100 / theTotalPatient).ToString("#0.##"));
            //        thePercentSum = thePercentSum + Convert.ToDouble(theDS.Tables[1].Rows[j]["Number of Visitor"]) * 100 / theTotalPatient;
            //        j++;
            //    }

            //    theRows = theRows + 1;
            //    writeInCell(theSheet, "C" + theRows, "", "Total");
            //    writeInCell(theSheet, "D" + theRows, "", theTotalPatient.ToString());
            //    writeInCell(theSheet, "E" + theRows, "", thePercentSum.ToString());
            //    theRange = theSheet.Cells.get_Range("A" + theRows, "C" + theRows);
            //    theRange.BorderAround(Excel.LineStyleEnum.owcLineStyleSolid, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexNone, "Black");


        }
        #endregion

        #region " Patients who have not visited recently with Unknown Status"
        private void WriteCellWiseInExcel_UnknownPtnotvisited(Excel.Spreadsheet theSheet)
        {

            writeInCell(theSheet, "B1", theDS.Tables[0].TableName.ToString(), "FacilityName");
            writeInCell(theSheet, "B2", "", txtStartDate.Text + "  to " + Application["AppCurrentDate"]);

            int j = 0;
            for (int i = 6; i < theDS.Tables[0].Rows.Count + 6; i++)
            {
                //writeInCell(theSheet, "B" + i + "", "", theDS.Tables[0].Rows[j]["FacilityName"].ToString());
                writeInCell(theSheet, "B" + i + "", "", theDS.Tables[0].Rows[j]["PatientName"].ToString());
                writeInCell(theSheet, "C" + i + "", "", theDS.Tables[0].Rows[j]["PatientID"].ToString());
                writeInCell(theSheet, "D" + i + "", "", theDS.Tables[0].Rows[j]["PatientFile#"].ToString());
                writeInCell(theSheet, "E" + i + "", "", theDS.Tables[0].Rows[j]["LastVisitDate"].ToString());
                writeInCell(theSheet, "F" + i + "", "", theDS.Tables[0].Rows[j]["DaysSinceLastVisit"].ToString());
                writeInCell(theSheet, "G" + i + "", "", theDS.Tables[0].Rows[j]["PhoneNumber"].ToString());
                writeInCell(theSheet, "H" + i + "", "", theDS.Tables[0].Rows[j]["ContactDetails"].ToString());
                writeInCell(theSheet, "I" + i + "", "", theDS.Tables[0].Rows[j]["Village/Street"].ToString());
                writeInCell(theSheet, "I" + i + "", "", theDS.Tables[0].Rows[j]["Ward"].ToString());
                writeInCell(theSheet, "J" + i + "", "", theDS.Tables[0].Rows[j]["Division"].ToString());
                writeInCell(theSheet, "J" + i + "", "", theDS.Tables[0].Rows[j]["Region"].ToString());
                j++;
            }
        }
        #endregion

        #region "ARV Regimen for Adult/Child"
        private void WriteCellWiseInExcel_ARVRegimenAdultChild(Excel.Spreadsheet theSheet)
        {
            Excel.Range theRange;
            writeInCell(theSheet, "A2", "", txtStartDate.Text + " To " + txtEndDate.Text);
            writeInCell(theSheet, "A3", theDS.Tables[0].TableName.ToString(), "FacilityName");

            theRange = theSheet.Cells.get_Range("B7", "E7");
            theRange.BorderAround(Excel.LineStyleEnum.owcLineStyleSolid, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexNone, "Black");
            writeInCell(theSheet, "A7", "", "Adult 15+");
            Int32 ReportTotalPatient = 0;

            ////////// "Adult 15+" //////////
            Int32 theRows = 8;
            Int32 theOldRow = 9;
            int theTotalPatient = 0;
            double thePercentSum = 0;
            int j = 0;
            for (int i = 9; j < theDS.Tables[4].Rows.Count; i++)
            {
                writeInCell(theSheet, "A" + i + "", "", theDS.Tables[4].Rows[j]["RegimenCode"].ToString());
                writeInCell(theSheet, "B" + i + "", "", theDS.Tables[4].Rows[j]["Stage"].ToString());
                writeInCell(theSheet, "C" + i + "", "", theDS.Tables[4].Rows[j]["RegimenType"].ToString());
                writeInCell(theSheet, "D" + i + "", "", theDS.Tables[4].Rows[j]["Ptn_Count"].ToString());
                theTotalPatient = theTotalPatient + Convert.ToInt32(theDS.Tables[4].Rows[j]["Ptn_Count"]);
                j++;
                theRows++;
            }
            j = 0;
            for (int i = 9; j < theDS.Tables[4].Rows.Count; i++)
            {
                writeInCell(theSheet, "E" + i + "", "", Convert.ToDecimal(Convert.ToDecimal(theDS.Tables[4].Rows[j]["Ptn_Count"]) * 100 / theTotalPatient).ToString("#0.##"));
                thePercentSum = thePercentSum + Convert.ToDouble(theDS.Tables[4].Rows[j]["Ptn_Count"]) * 100 / theTotalPatient;
                j++;
            }

            theRows = theRows + 1;
            writeInCell(theSheet, "B" + theRows, "", "Total");
            writeInCell(theSheet, "D" + theRows, "", theTotalPatient.ToString());
            writeInCell(theSheet, "E" + theRows, "", thePercentSum.ToString());
            ReportTotalPatient = ReportTotalPatient + theTotalPatient;
            theRange = theSheet.Cells.get_Range("A" + theRows, "C" + theRows);
            theRange.BorderAround(Excel.LineStyleEnum.owcLineStyleSolid, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexNone, "Black");


            ////////// Child 5-15" //////////

            theRows = theRows + 2;
            theRange = theSheet.Cells.get_Range("B" + theRows, "E" + theRows);
            theRange.BorderAround(Excel.LineStyleEnum.owcLineStyleSolid, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexNone, "Black");
            writeInCell(theSheet, "A" + theRows, "", "Child 5-15");
            theRows = theRows + 1;


            j = 0;
            theTotalPatient = 0;
            thePercentSum = 0;
            theOldRow = theRows;

            for (int i = theRows; j < theDS.Tables[3].Rows.Count; i++)
            {

                writeInCell(theSheet, "A" + i + "", "", theDS.Tables[3].Rows[j]["RegimenCode"].ToString());
                writeInCell(theSheet, "B" + i + "", "", theDS.Tables[3].Rows[j]["Stage"].ToString());
                writeInCell(theSheet, "C" + i + "", "", theDS.Tables[3].Rows[j]["RegimenType"].ToString());
                writeInCell(theSheet, "D" + i + "", "", theDS.Tables[3].Rows[j]["Ptn_Count"].ToString());
                theTotalPatient = theTotalPatient + Convert.ToInt32(theDS.Tables[3].Rows[j]["Ptn_Count"]);
                j++;
                theRows++;
            }
            j = 0;
            for (int i = theOldRow; j < theDS.Tables[3].Rows.Count; i++)
            {
                writeInCell(theSheet, "E" + i + "", "", Convert.ToDecimal(Convert.ToDecimal(theDS.Tables[3].Rows[j]["Ptn_Count"]) * 100 / theTotalPatient).ToString("#0.##"));
                thePercentSum = thePercentSum + Convert.ToDouble(theDS.Tables[3].Rows[j]["Ptn_Count"]) * 100 / theTotalPatient;
                j++;
            }

            //theRows = theRows + 1;
            writeInCell(theSheet, "B" + theRows, "", "Total");
            writeInCell(theSheet, "D" + theRows, "", theTotalPatient.ToString());
            writeInCell(theSheet, "E" + theRows, "", thePercentSum.ToString());
            ReportTotalPatient = ReportTotalPatient + theTotalPatient;
            theRange = theSheet.Cells.get_Range("A" + theRows, "C" + theRows);
            theRange.BorderAround(Excel.LineStyleEnum.owcLineStyleSolid, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexNone, "Black");



            ////////// "Child 2-4" //////////

            theRows = theRows + 2;
            theRange = theSheet.Cells.get_Range("B" + theRows, "E" + theRows);
            theRange.BorderAround(Excel.LineStyleEnum.owcLineStyleSolid, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexNone, "Black");
            writeInCell(theSheet, "A" + theRows, "", "Child 2-4");
            theRows = theRows + 2;

            j = 0;
            theTotalPatient = 0;
            thePercentSum = 0;
            theOldRow = theRows;

            for (int i = theRows; j < theDS.Tables[2].Rows.Count; i++)
            {

                writeInCell(theSheet, "A" + i + "", "", theDS.Tables[2].Rows[j]["RegimenCode"].ToString());
                writeInCell(theSheet, "B" + i + "", "", theDS.Tables[2].Rows[j]["Stage"].ToString());
                writeInCell(theSheet, "C" + i + "", "", theDS.Tables[2].Rows[j]["RegimenType"].ToString());
                writeInCell(theSheet, "D" + i + "", "", theDS.Tables[2].Rows[j]["Ptn_Count"].ToString());
                theTotalPatient = theTotalPatient + Convert.ToInt32(theDS.Tables[2].Rows[j]["Ptn_Count"]);
                j++;
                theRows++;

            }
            j = 0;
            for (int i = theOldRow; j < theDS.Tables[2].Rows.Count; i++)
            {
                writeInCell(theSheet, "E" + i + "", "", Convert.ToDecimal(Convert.ToDecimal(theDS.Tables[2].Rows[j]["Ptn_Count"]) * 100 / theTotalPatient).ToString("#0.##"));
                thePercentSum = thePercentSum + Convert.ToDouble(theDS.Tables[2].Rows[j]["Ptn_Count"]) * 100 / theTotalPatient;
                j++;
            }
            //theRows = theRows + 1;
            writeInCell(theSheet, "B" + theRows, "", "Total");
            writeInCell(theSheet, "D" + theRows, "", theTotalPatient.ToString());
            writeInCell(theSheet, "E" + theRows, "", thePercentSum.ToString());
            ReportTotalPatient = ReportTotalPatient + theTotalPatient;
            theRange = theSheet.Cells.get_Range("A" + theRows, "C" + theRows);
            theRange.BorderAround(Excel.LineStyleEnum.owcLineStyleSolid, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexNone, "Black");



            ////////// "Child 0-1" ///////////
            theRows = theRows + 2;
            theRange = theSheet.Cells.get_Range("B" + theRows, "E" + theRows);
            theRange.BorderAround(Excel.LineStyleEnum.owcLineStyleSolid, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexNone, "Black");
            writeInCell(theSheet, "A" + theRows, "", "Child 0-1");
            //writeInCell(theSheet, "B" +theRows, "", "(Calculate the age of the patient as of the Dispensed by Date)");
            theRows = theRows + 1;


            j = 0;
            theTotalPatient = 0;
            thePercentSum = 0;
            theOldRow = theRows;

            for (int i = theRows; j < theDS.Tables[1].Rows.Count; i++)
            {

                writeInCell(theSheet, "A" + i + "", "", theDS.Tables[1].Rows[j]["RegimenCode"].ToString());
                writeInCell(theSheet, "B" + i + "", "", theDS.Tables[1].Rows[j]["Stage"].ToString());
                writeInCell(theSheet, "C" + i + "", "", theDS.Tables[1].Rows[j]["RegimenType"].ToString());
                writeInCell(theSheet, "D" + i + "", "", theDS.Tables[1].Rows[j]["Ptn_Count"].ToString());
                theTotalPatient = theTotalPatient + Convert.ToInt32(theDS.Tables[1].Rows[j]["Ptn_Count"]);
                j++;
                theRows++;
            }
            j = 0;
            for (int i = theOldRow; j < theDS.Tables[1].Rows.Count; i++)
            {
                writeInCell(theSheet, "E" + i + "", "", Convert.ToDecimal(Convert.ToDecimal(theDS.Tables[1].Rows[j]["Ptn_Count"]) * 100 / theTotalPatient).ToString("#0.##"));
                thePercentSum = thePercentSum + Convert.ToDouble(theDS.Tables[1].Rows[j]["Ptn_Count"]) * 100 / theTotalPatient;
                j++;
            }

            //theRows = theRows + 1;
            writeInCell(theSheet, "B" + theRows, "", "Total");
            writeInCell(theSheet, "D" + theRows, "", theTotalPatient.ToString());
            writeInCell(theSheet, "E" + theRows, "", thePercentSum.ToString());
            ReportTotalPatient = ReportTotalPatient + theTotalPatient;
            theRange = theSheet.Cells.get_Range("A" + theRows, "C" + theRows);
            theRange.BorderAround(Excel.LineStyleEnum.owcLineStyleSolid, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexNone, "Black");


            theRows = theRows + 2;
            theRange = theSheet.Cells.get_Range("B" + theRows, "E" + theRows);
            theRange.BorderAround(Excel.LineStyleEnum.owcLineStyleSolid, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexNone, "Black");
            writeInCell(theSheet, "A" + theRows, "", "Number of patients whose ARV regimen was recorded");
            writeInCell(theSheet, "D" + theRows, "", ReportTotalPatient.ToString());

        }
        #endregion

        #region "TBWithARV"
        private void WriteCellWiseInExcel_TBWithARV(Excel.Spreadsheet theSheet)
        {
            writeInCell(theSheet, "C3", theDS.Tables[0].TableName.ToString(), "DateRange");
            writeInCell(theSheet, "B3", theDS.Tables[0].TableName.ToString(), "FacilityName");

            writeInCell(theSheet, "C5", theDS.Tables[1].TableName.ToString(), "TotalARVPatients");
            writeInCell(theSheet, "C8", theDS.Tables[2].TableName.ToString(), "TotalTBPatientWithARVs");
            writeInCell(theSheet, "D8", theDS.Tables[2].TableName.ToString(), "%TotalTBPatientwithARVs");

            writeInCell(theSheet, "C11", theDS.Tables[3].TableName.ToString(), "TotalTBPatientafterARVs");
            writeInCell(theSheet, "D11", theDS.Tables[3].TableName.ToString(), "%TotalTBPatientafterARVs");

            int j = 0;
            for (int i = 12; i < theDS.Tables[4].Rows.Count + 12; i++)
            {
                writeInCell(theSheet, "B" + i + "", "", theDS.Tables[4].Rows[j]["GotTBafterStartingARVs"].ToString());
                writeInCell(theSheet, "C" + i + "", "", theDS.Tables[4].Rows[j]["GotTBafterARVsinMonth"].ToString());
                writeInCell(theSheet, "D" + i + "", "", theDS.Tables[4].Rows[j]["%TotalTBPatient_inMonthsafterARVs"].ToString());
                j++;
            }
        }
        #endregion

        #region "Geographical Patient Distribution Report"
        private void WriteCellWiseInExcel_GeographicalPatientDistribution(Excel.Spreadsheet theSheet)
        {
            int i = 7;
            int j = 0;
            string theRegion = "";
            string theDistrict = "";
            int theRegionPos = 0;
            int theDistrictPos = 0;
            int RegionTot = 0;
            Double RegionPercentage = 0;
            Double DistrictPercentage = 0;
            Double WardPercentage = 0;
            int DistrictTot = 0;

            writeInCell(theSheet, "B1", theDS.Tables[0].TableName.ToString(), "FacilityName");
            writeInCell(theSheet, "H4", theDS.Tables[2].TableName.ToString(), "NoWardDistrictRegion");
            writeInCell(theSheet, "G5", theDS.Tables[2].TableName.ToString(), "NoWardDistrictRegion");
            writeInCell(theSheet, "F6", theDS.Tables[2].TableName.ToString(), "NoWardDistrictRegion");
            writeInCellDemographic(theSheet, "J4", "", Convert.ToString(Math.Round((Convert.ToDouble(theDS.Tables[2].Rows[0]["NoWardDistrictRegion"]) * 100 / Convert.ToDouble(theDS.Tables[3].Rows[0]["TotalPatients"])), 2)));
            writeInCellDemographic(theSheet, "J5", "", Convert.ToString(Math.Round((Convert.ToDouble(theDS.Tables[2].Rows[0]["NoWardDistrictRegion"]) * 100 / Convert.ToDouble(theDS.Tables[3].Rows[0]["TotalPatients"])), 2)));
            writeInCellDemographic(theSheet, "J6", "", Convert.ToString(Math.Round((Convert.ToDouble(theDS.Tables[2].Rows[0]["NoWardDistrictRegion"]) * 100 / Convert.ToDouble(theDS.Tables[3].Rows[0]["TotalPatients"])), 2)));



            foreach (DataRow dr in theDS.Tables[1].Rows)
            {
                if (theRegion != theDS.Tables[1].Rows[j]["Region Name"].ToString())
                {
                    writeInCell(theSheet, "B" + i + "", "", theDS.Tables[1].Rows[j]["Region Name"].ToString());
                    theRegion = theDS.Tables[1].Rows[j]["Region Name"].ToString();
                    theRegionPos = i;
                    RegionTot = 0;
                    i = i + 1;
                }

                if (theDistrict != theDS.Tables[1].Rows[j]["District Name"].ToString())
                {
                    writeInCell(theSheet, "C" + i + "", "", theDS.Tables[1].Rows[j]["District Name"].ToString());
                    theDistrict = theDS.Tables[1].Rows[j]["District Name"].ToString();
                    theDistrictPos = i;
                    DistrictTot = 0;
                    i = i + 1;
                }

                writeInCell(theSheet, "D" + i + "", "", theDS.Tables[1].Rows[j]["Ward Name"].ToString());
                writeInCell(theSheet, "F" + i + "", "", theDS.Tables[1].Rows[j]["NoofPatients"].ToString());
                WardPercentage = Math.Round((Convert.ToDouble(theDS.Tables[1].Rows[j]["NoofPatients"]) * 100 / Convert.ToDouble(theDS.Tables[3].Rows[0]["TotalPatients"])), 2);
                writeInCellDemographic(theSheet, "J" + i + "", "", WardPercentage.ToString());
                RegionTot = RegionTot + Convert.ToInt32(theDS.Tables[1].Rows[j]["NoofPatients"]);
                RegionPercentage = Math.Round((Convert.ToDouble(RegionTot) * 100 / Convert.ToDouble(theDS.Tables[3].Rows[0]["TotalPatients"])), 2);
                DistrictTot = DistrictTot + Convert.ToInt32(theDS.Tables[1].Rows[j]["NoofPatients"]);
                DistrictPercentage = Math.Round((Convert.ToDouble(DistrictTot) * 100 / Convert.ToDouble(theDS.Tables[3].Rows[0]["TotalPatients"])), 2);
                writeInCellDemographic(theSheet, "G" + theDistrictPos + "", "", DistrictTot.ToString());
                writeInCellDemographic(theSheet, "H" + theRegionPos + "", "", RegionTot.ToString());
                writeInCellDemographic(theSheet, "J" + theRegionPos + "", "", RegionPercentage.ToString());
                writeInCellDemographic(theSheet, "J" + theDistrictPos + "", "", DistrictPercentage.ToString());

                j = j + 1;
                i = i + 1;
            }

        }

        private void writeInCellDemographic(Excel.Spreadsheet theSheet, string cell, string tablename, string column)
        {
            Excel.Range theRange = theSheet.Cells.get_Range(cell, cell);
            theRange.BorderAround(Excel.LineStyleEnum.owcLineStyleSolid, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexNone, "Black");
            //theRange.Borders();
            theRange.Value2 = column.ToString();
        }

        #endregion

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmFacilityHome.aspx");
        }

        private Boolean FieldValidation(string theReportName)
        {
            try
            {
                if (rdoARVPickup.Checked)
                {
                    if (theReportName == "")
                    {
                        //flash a message for "select either of the option"
                        IQCareMsgBox.Show("SelectARVOption", this);
                        return false;

                    }
                    else if (theReportName == "SinglePatientARVPickup")
                    {

                        //Check for patient id

                        if (txtPatientId.Text.Trim() == "")
                        {
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["Control"] = "Enrollment Number";
                            IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                            txtPatientId.Focus();
                            return false;
                        }

                    }
                    else if (theReportName == "AllPatientARVPickup")
                    {
                        return true;
                    }
                }
                else if (theReportName == "User Detail")
                {
                    if (TxtToDate.Text == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Ordered to date";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        TxtToDate.Focus();
                        return false;
                    }
                    if (TxtFromDate.Text == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Ordered from date";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        TxtFromDate.Focus();
                        return false;
                    }
                    if (chkAllUser.Checked == false && ddlSelectUser.SelectedIndex == '0')
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Select User";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        TxtFromDate.Focus();
                        return false;
                    }


                }

                else if (theReportName == "PatientEnrollmentMonth" || theReportName == "TB Status by Age and Sex Report" || theReportName == "ARV Regimen for Adult/Child Report" || theReportName == "TB Cases before and after starting ARVs")
                {
                    if (txtStartDate.Text == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Ordered from date";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtStartDate.Focus();
                        return false;
                    }

                    if (txtEndDate.Text == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Ordered to date";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtEndDate.Focus();
                        return false;
                    }

                    if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(txtEndDate.Text))
                    {
                        IQCareMsgBox.Show("StartEndDate", this);
                        txtEndDate.Focus();
                        return false;
                    }
                }


                else if (theReportName == "Unknown Patients who have not visited recently" || theReportName == "Non_ARTPatient")
                {
                    if (txtStartDate.Text == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Date Order From";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtStartDate.Focus();
                        return false;
                    }
                    if (Convert.ToDateTime(txtStartDate.Text) > Convert.ToDateTime(Application["AppCurrentDate"]))
                    {
                        IQCareMsgBox.Show("StartEndDate", this);
                        txtEndDate.Focus();
                        return false;
                    }
                }


                else if (theReportName == "ARVCohort")
                {
                    if (txtYear.Text == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "ARV Year ";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtStartDate.Focus();
                        return false;
                    }

                    if (txtyears.Text == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Progress Year";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtEndDate.Focus();
                        return false;
                    }

                    if (ddMonth.SelectedItem.Text == "Select")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "Month";
                        IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                        return false;
                    }

                    if (ARVddMonth.SelectedItem.Text == "Select")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "Month";
                        IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                        return false;
                    }
                    if ((ddMonth.SelectedItem.Text != "Select" && txtYear.Text != "") && (ARVddMonth.SelectedItem.Text != "Select" && txtyears.Text != ""))
                    {
                        DateTime app = Convert.ToDateTime(Application["AppCurrentDate"]);
                        IQCareUtils theUtils = new IQCareUtils();
                        theARVYear = Convert.ToDateTime(theUtils.MakeDate("01-" + ddMonth.SelectedItem.Text + "-" + txtYear.Text));
                        theProgressYear = Convert.ToDateTime(theUtils.MakeDate("01-" + ARVddMonth.SelectedItem.Text + "-" + txtyears.Text));

                        if (theARVYear.Year > theProgressYear.Year)
                        {
                            IQCareMsgBox.Show("ARVYear", this);
                            return false;


                        }

                        if (theARVYear > theProgressYear)
                        {
                            IQCareMsgBox.Show("ARVMonth", this);
                            return false;


                        }

                        if ((theARVYear.Year > app.Year) || (theProgressYear.Year > app.Year))
                        {
                            IQCareMsgBox.Show("Year", this);
                            txtYear.Focus();
                            return false;
                        }
                        if ((theARVYear > Convert.ToDateTime(Application["AppCurrentDate"])) || (theProgressYear > Convert.ToDateTime(Application["AppCurrentDate"])))
                        {
                            IQCareMsgBox.Show("Month", this);
                            txtYear.Focus();
                            return false;
                        }

                    }


                }

                else if (theReportName == "MisARVPickup")
                {
                    if (txtDefaulterAsOf.Text == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Defaulter as of date";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtDefaulterAsOf.Focus();
                        return false;
                    }
                }
                else if ((theReportName == "Kenya National Integrated Form for reproductive Health,HIV/Aids,Malaria,TB and child Nutrition(711B)") || (theReportName == "Born-To-Live") || (theReportName == "NNRIMS Monthly Report") || (theReportName == "NASCOP Monthly Report"))
                {
                    IQCareUtils theUtils = new IQCareUtils();
                    DateTime app = Convert.ToDateTime(Application["AppCurrentDate"]);

                    if (ddlMonthKenyaHealth.SelectedItem.Text != "Select" && txtYearKenyaHealth.Text != "")
                    {
                        DateTime theYear = Convert.ToDateTime(theUtils.MakeDate("01-" + ddlMonthKenyaHealth.SelectedItem.Text + "-" + txtYearKenyaHealth.Text));
                        if (theYear.Year > app.Year)
                        {
                            IQCareMsgBox.Show("Year", this);
                            txtYear.Focus();
                            return false;
                        }
                        if (theYear > Convert.ToDateTime(Application["AppCurrentDate"]))
                        {
                            IQCareMsgBox.Show("Month", this);
                            txtYear.Focus();
                            return false;
                        }


                    }

                    if (txtYearKenyaHealth.Text == "")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "Year";
                        IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                        return false;


                    }

                    if (ddlMonthKenyaHealth.SelectedItem.Text == "Select")
                    {
                        MsgBuilder theMsg = new MsgBuilder();
                        theMsg.DataElements["Control"] = "Month";
                        IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                        return false;
                    }


                }


            }
            catch
            {
                return false;
            }

            return true;
        }

        private void showhide()
        {
            if (rdoARVPickupForAPatient.Checked == true)
            {
                string scriptForAPatient = "<script language = 'javascript' defer ='defer' id = 'ForAPatient'>\n";
                scriptForAPatient += "show('patientId'); \n";
                scriptForAPatient += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ForAPatient", scriptForAPatient);
            }


        }

        protected void lnkCostPerPatientPerVisit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTranDateFrom.Text) || !string.IsNullOrEmpty(txtTranDateTo.Text))
            {
                IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
                DataTable dtFacilityPatientsCostPerMonth = ReportDetails.GetFacilityPatientsCostPerMonth(Convert.ToDateTime(txtTranDateFrom.Text), Convert.ToDateTime(txtTranDateTo.Text)).Tables[0];
                string FName = "FacilityPatientsCostPerMonth";
                IQWebUtils theUtils = new IQWebUtils();
                string thePath = Server.MapPath(".\\ExcelFiles\\" + FName + ".xls");
                string theTemplatePath = Server.MapPath(".\\ExcelFiles\\IQCareTemplate.xls");
                theUtils.ExporttoExcel(dtFacilityPatientsCostPerMonth, Response);
                Response.Redirect(".\\ExcelFiles\\" + FName + ".xls");
            }
        }

        private Boolean NoEnrolexist(string theReportName)
        {
            try
            {
                if (theReportName == "SinglePatientARVPickup")
                {

                    if (txtCountryNo.Text.Trim() == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Country:";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtCountryNo.Focus();
                        return false;
                    }
                    if (txtPosNo.Text.Trim() == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "LPTF#:";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtPosNo.Focus();
                        return false;
                    }
                    if (txtSatelliteNo.Text.Trim() == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Satellite/Clinic#:";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtSatelliteNo.Focus();
                        return false;
                    }

                   // IQCareUtils theUtil = new IQCareUtils();
                    IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    DataSet EnrollmentNo = (DataSet)ReportDetails.EnrollmentNoCheck(txtPatientId.Text, Session["AppLocationId"].ToString(), txtCountryNo.Text.ToString(), txtPosNo.Text.ToString(), txtSatelliteNo.Text.ToString());

                    if (EnrollmentNo.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        IQCareMsgBox.Show("InvalidEnrollmentNumber", this);
                        txtPatientId.Text = "";
                        txtPatientId.Focus();
                        return false;

                    }
                    ViewState["ptn_pk"] = EnrollmentNo.Tables[1].Rows[0][0].ToString();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        protected void ddlSelectUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}