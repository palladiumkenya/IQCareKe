#region Import namespace
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Interface.Clinical;
using Interface.Pharmacy;
using Interface.Reports;
#endregion
namespace IQCare.Web.Reports
{
    public partial class frmReportViewer : System.Web.UI.Page
    {
        #region "Export Variables"
        DataTable theExcelDT;
        #endregion

        # region  Variables Declaration
        private string theReportName = string.Empty;
        private string theDType = string.Empty;
        private string theReportSource = string.Empty;
        private string theQuarter = string.Empty;
        private string theYear = string.Empty;
        private string theReportQuery = string.Empty;
        private string theReportTitle = string.Empty;
        private int thePatientId = 0;
        private int theReportId = 0;
        private ReportDocument rptDocument;
        int ImageFlag = 0;
        public static int TemplateType = 0;

        #endregion

        #region "User Defined Functions"

        private void Init_Page()
        {
            try
            {
                string theReportHeading = string.Empty;

                if (Request.QueryString["ReportId"] != null)
                {
                    ViewState["ReportId"] = Convert.ToInt32(Request.QueryString["ReportId"]);
                    theReportId = Convert.ToInt32(ViewState["ReportId"]);
                }
                if (Request.QueryString["ReportName"] != null)
                {
                    theReportName = Request.QueryString["ReportName"];
                }
                if (Request.QueryString["rptType"] != null)
                    theReportName = Request.QueryString["rptType"].ToString();

                if (Request.QueryString["StartDate"] != null)
                {
                    ViewState["theStartDate"] = Request.QueryString["StartDate"];
                }
                if (Request.QueryString["EndDate"] != null)
                {
                    ViewState["theEndDate"] = Request.QueryString["EndDate"];
                }
                if (Request.QueryString["PatientId"] != null && Request.QueryString["PatientId"].ToString() != "")
                {
                    thePatientId = Convert.ToInt32(Request.QueryString["PatientId"]);
                }
                else
                {
                    thePatientId = 0;
                }


                if (Request.QueryString["DType"] != null)
                {
                    theDType = Request.QueryString["DType"];
                }

                if (theReportName != "")
                {
                    if (theReportName == "UpARVPickup")
                    {
                        theReportHeading = "Upcoming ARV Pickup Report";
                        theReportSource = "rptUpcomingARVPickup.rpt";

                    }

                    else if (theReportName == "NewPatients")
                    {
                        theReportHeading = "New Patients Report";
                        theReportSource = "rptUpcomingARVPickup.rpt";

                    }
                    else if (theReportName == "PregnantFU")
                    {
                        theReportHeading = "PregnantFU Report";
                        theReportSource = "rptUpcomingARVPickup.rpt";

                    }
                    else if (theReportName == "PatientARVPickup")
                    {
                        theReportHeading = "Patient ARV Pickup Report";
                        theReportSource = "rptPatientARVPickup.rpt";
                        //(Master.FindControl("lblRoot") as Label).Text = "Patient Reports >>";
                        //(Master.FindControl("lblMark") as Label).Text = "»";
                        //(Master.FindControl("lblMark") as Label).Visible = false;
                        //(Master.FindControl("lblheader") as Label).Text = "Patient ARV Pickup";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Patient Reports >> ";
                        (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Patient ARV Pickup";
                        btnExcel.Visible = true;
                    }

                    else if (theReportName == "ARVAdherence")
                    {
                        theReportHeading = "Adherence to ARV Collection Report";
                        theReportSource = "rptAdARVCollectionClients.rpt";
                    }
                    else if (theReportName == "PatientProfile")
                    {

                        if (Session["SystemId"].ToString() == "1")
                        {
                            theReportHeading = "Patient Profile";
                            theReportSource = "rptPatientProfile.rpt";
                            crViewer.EnableDrillDown = false;
                        }
                        else
                        {
                            if (Session["SystemId"].ToString() == "2")
                                theReportHeading = "Patient Profile";
                            theReportSource = "rptPatientProfile_CTC.rpt";
                            crViewer.EnableDrillDown = false;
                        }

                    }
                    else if (theReportName == "MisARVAppointment")
                    {
                        theReportHeading = "Missing ARV Appointment Report";
                        theReportSource = "rptMisARVAppointClients.rpt";
                    }
                    else if (theReportName == "PharmacyPrescription")
                    {
                        theReportHeading = "Patient Pharmacy Prescription";
                        theReportSource = strPrintPrescriptionRpt();
                    }

                }

                if (theReportId > 0)
                {
                    if (theReportName == "rptPotrait")
                        theReportSource = "rptPortraitCustomReport.rpt";
                    else
                        theReportSource = "rptLandScapeCustomReport.rpt";
                    theReportHeading = "Custom Report";
                    btnExcel.Visible = true;
                    //(Master.FindControl("lblRoot") as Label).Text = "Reports";
                    //(Master.FindControl("lblMark") as Label).Text = "»";
                    //(Master.FindControl("lblheader") as Label).Text = "Custom Reports";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Reports >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Custom Reports";
                }
                hBar.InnerText = theReportHeading;
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
        private string strPrintPrescriptionRpt()
        {
            string thestrRPTSource = "";
            DataSet theCacheDS = new DataSet();
            theCacheDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            if (theCacheDS.Tables["Mst_Facility"] != null)
            {
                IQCareUtils theUtils = new IQCareUtils();
                DataView theDV = new DataView(theCacheDS.Tables["Mst_Facility"]);
                theDV.RowFilter = "FacilityId =" + Convert.ToInt32(Session["AppLocationId"]);
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    if (theDT.Rows[0]["FacilityTemplate"].ToString() == "1")
                    {
                        thestrRPTSource = "rptKNHPrescription.rpt";

                    }
                    else
                    {
                        thestrRPTSource = "rptSimplePrescription.rpt";

                    }
                }
                else
                {
                    thestrRPTSource = "rptSimplePrescription.rpt";

                }

            }
            else
            {
                thestrRPTSource = "rptSimplePrescription.rpt";
            }
            return thestrRPTSource;
        }
        private DataSet GetDSPrintPrescription()
        {
            IDrug PediatricManager = (IDrug)ObjectFactory.CreateInstance("BusinessProcess.Pharmacy.BDrug, BusinessProcess.Pharmacy");
            int PharmacyID = Convert.ToInt32(Session["PatientVisitId"]);
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            ViewState["PatientId"] = PatientID;

            DataSet theDS = PediatricManager.GetPharmacyPrescriptionDetails(PharmacyID, PatientID, 1);

            //Image Streaming
            DataTable dtFacility = new DataTable();
            // object of data row
            DataRow drow = null;
            // add the column in table to store the image of Byte array type
            dtFacility.Columns.Add("FacilityImage", System.Type.GetType("System.Byte[]"));
            drow = dtFacility.NewRow();
            // define the filestream object to read the image
            FileStream fs = default(FileStream);
            // define te binary reader to read the bytes of image
            BinaryReader br = default(BinaryReader);
            // check the existance of image
            if (File.Exists(GblIQCare.GetPath() + theDS.Tables[3].Rows[0]["FacilityLogo"].ToString()))
            {
                // open image in file stream
                fs = new FileStream(GblIQCare.GetPath() + theDS.Tables[3].Rows[0]["FacilityLogo"].ToString(), FileMode.Open);

                // initialise the binary reader from file streamobject
                br = new BinaryReader(fs);
                // define the byte array of filelength
                byte[] imgbyte = new byte[fs.Length + 1];
                // read the bytes from the binary reader
                imgbyte = br.ReadBytes(Convert.ToInt32((fs.Length)));
                drow[0] = imgbyte;
                ImageFlag = 1;
                // add the image in bytearray
                dtFacility.Rows.Add(drow);
                // add row into the datatable
                br.Close();
                // close the binary reader
                fs.Close();
                // close the file stream
            }

            theDS.Tables.Add(dtFacility);
            ////////////////////////////////////////

            theDS.WriteXmlSchema(Server.MapPath("..\\XMLFiles\\PatientPharmacyPrescription.xml"));
            PediatricManager = null;
            return theDS;
        }

        private void Set_PatientReports()
        {
            try
            {
                string theEnrollmentID = string.Empty;
                rptDocument = new ReportDocument();
                rptDocument.Load(Server.MapPath(theReportSource));
                #region "Patient Pharmacy Prescription"
                if (theReportName == "PharmacyPrescription")
                {

                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Patient Reports >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Patient Pharmacy Prescription";
                    DataTable theDTMod = (DataTable)Session["AppModule"];
                    DataView theDVMod = new DataView(theDTMod);
                    theDVMod.RowFilter = "ModuleId=" + Convert.ToInt32(Session["TechnicalAreaId"]);

                    rptDocument.SetDataSource(GetDSPrintPrescription());
                    rptDocument.SetParameterValue("PharmacyID", Convert.ToInt32(Session["PatientId"]).ToString());
                    rptDocument.SetParameterValue("ModuleName", theDVMod[0]["ModuleName"].ToString());
                    rptDocument.SetParameterValue("Currency", getCurrency().ToString());
                    rptDocument.SetParameterValue("FacilityName", Session["AppLocation"].ToString());
                    rptDocument.SetParameterValue("ImageFlag", ImageFlag.ToString());


                }
                #endregion
                IPatientHome PatientManager;
                PatientManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
                DataTable theDT = PatientManager.GetPatientVisitDetail(Convert.ToInt32(Request.QueryString["PatientId"]));


                if (theReportName != "ARVAdherence" && theReportName != "PatientProfile" && theReportName != "MisARVAppointment" && theReportName != "PatientARVPickup" && theReportName != "rptPotrait" && theReportName != "rptLandscape" && theReportName != "PharmacyPrescription")
                {
                    IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    DataTable dtReportsPatient = (DataTable)ReportDetails.GetPatientDetails(thePatientId, Convert.ToDateTime(ViewState["theStartDate"]), Convert.ToDateTime(ViewState["theEndDate"])).Tables[0];

                    rptDocument.SetDataSource(dtReportsPatient);
                    rptDocument.SetParameterValue("StartDate", (ViewState["theStartDate"]));
                    rptDocument.SetParameterValue("EndDate", (ViewState["theEndDate"]));
                    ReportDetails = null;
                }
                else if (theReportName == "PatientARVPickup")
                {
                    if (Convert.ToInt32(theDT.Rows[0]["Status"]) == 0)
                    {
                        (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "0";
                    }
                    else
                    {
                        (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "1";
                    }

                    DataTable dtDrugARVPickup = (DataTable)Session["dtDrugARVPickup"];

                    theExcelDT = new DataTable();
                    theExcelDT.Columns.Add("Patient Name", System.Type.GetType("System.String"));
                    theExcelDT.Columns.Add("Enrollment No", System.Type.GetType("System.String"));
                    theExcelDT.Columns.Add("Exiting Patient ID", System.Type.GetType("System.String"));
                    theExcelDT.Columns.Add("Date Dispensed", System.Type.GetType("System.String"));
                    theExcelDT.Columns.Add("Longest Duration", System.Type.GetType("System.Int32"));
                    theExcelDT.Columns.Add("Next Collection", System.Type.GetType("System.String"));
                    theExcelDT.Columns.Add("Days", System.Type.GetType("System.Int32"));
                    theExcelDT.Columns.Add("Late/Early", System.Type.GetType("System.String"));
                    theExcelDT.Columns.Add("Facility Name", System.Type.GetType("System.String"));

                    int i = 0;
                    DateTime orderedByDate;
                    TimeSpan ts;
                    Int32 dateDiff;
                    ////////////////////
                    ////////////////////
                    for (i = 0; i < dtDrugARVPickup.Rows.Count; i++)
                    {
                        DataRow theDR = theExcelDT.NewRow();

                        theDR[0] = dtDrugARVPickup.Rows[i]["Name"].ToString();
                        theDR[1] = dtDrugARVPickup.Rows[i]["PatientEnrollmentID"];
                        theDR[2] = dtDrugARVPickup.Rows[i]["PatientClinicID"];

                        if (dtDrugARVPickup.Rows[i].IsNull("DispensedByDate") == true)
                        {
                            theDR[3] = dtDrugARVPickup.Rows[i]["DispensedByDate"];
                        }
                        else
                        {
                            theDR[3] = ((DateTime)dtDrugARVPickup.Rows[i]["DispensedByDate"]).ToString(Session["AppDateFormat"].ToString());
                        }

                        theDR[4] = dtDrugARVPickup.Rows[i]["LongestDaysLate"];

                        orderedByDate = Convert.ToDateTime(dtDrugARVPickup.Rows[i]["DispensedByDate"]);
                        ts = (Convert.ToDateTime(Application["AppCurrentDate"]) - orderedByDate);
                        dateDiff = ts.Days;

                        if (dtDrugARVPickup.Rows[i].IsNull("LongestDaysLate") == true)
                        {
                            theDR[5] = "Patient Not Due Yet";
                        }
                        else if ((dtDrugARVPickup.Rows[i].IsNull("DateArrived") == true) && (dateDiff <= Convert.ToInt32(dtDrugARVPickup.Rows[i]["LongestDaysLate"])))
                        {
                            theDR[5] = "Patient Not Due Yet";
                        }
                        else
                        {
                            if (dtDrugARVPickup.Rows[i].IsNull("DateArrived") == false)
                            {
                                theDR[5] = ((DateTime)dtDrugARVPickup.Rows[i]["DateArrived"]).ToString(Session["AppDateFormat"].ToString());

                            }
                            else
                            {
                                theDR[5] = "Patient Late";
                            }
                        }

                        if (dtDrugARVPickup.Rows[i].IsNull("DaysLateOrEarly") == true)
                        {
                            theDR[6] = 0;
                            theDR[7] = "";
                        }
                        else
                        {
                            theDR[6] = dtDrugARVPickup.Rows[i]["DaysLateOrEarly"];
                            if (Convert.ToInt32(dtDrugARVPickup.Rows[i]["DaysLateOrEarly"]) < 0)
                            {
                                theDR[7] = "Early";
                            }
                            else
                            {
                                theDR[7] = "Late";
                            }
                        }

                        theDR[8] = dtDrugARVPickup.Rows[i]["FacilityName"];
                        theExcelDT.Rows.InsertAt(theDR, i);
                    }
                    Session["RptData"] = theExcelDT;
                    ViewState["FName"] = "PatientARVPickup";
                    rptDocument.SetDataSource(dtDrugARVPickup);
                    rptDocument.SetParameterValue("StartDate", ViewState["theStartDate"]);
                    rptDocument.SetParameterValue("EndDate", ViewState["theEndDate"]);
                    if (Session["SystemId"].ToString() == "1")
                    {
                        rptDocument.SetParameterValue("Patient_FileId", "Existing Patient ClinicId:");
                    }
                    else
                    {
                        rptDocument.SetParameterValue("Patient_FileId", "File Reference:");
                    }
                }


                else if (theReportName == "ARVAdherence")
                {
                    IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    DataTable dtReportsClinical = (DataTable)ReportDetails.GetARVCollectionClients(thePatientId).Tables[0];
                    rptDocument.SetDataSource(dtReportsClinical);
                    ReportDetails = null;
                }
                else if (theReportName == "MisARVAppointment")
                {

                    IQCareUtils theUtil = new IQCareUtils();
                    IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    DataTable dtReportsClinical = (DataTable)ReportDetails.GetMisARVAppointClients(theDType, Convert.ToDateTime(theUtil.MakeDate(ViewState["theStartDate"].ToString()))).Tables[0];
                    rptDocument.SetDataSource(dtReportsClinical);
                    rptDocument.SetParameterValue("DateCategory", theDType);
                    ReportDetails = null;
                }
                else if (theReportName == "PatientProfile")
                {
                    if (Convert.ToInt32(theDT.Rows[0]["Status"]) == 0)
                    {
                        (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "0";
                    }
                    else
                    {
                        (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "1";
                    }
                    //(Master.FindControl("lblRoot") as Label).Text = "Patient Reports";
                    //(Master.FindControl("lblheader") as Label).Text = "HIV Care Patient Profile";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Patient Reports >> ";
                    (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "HIV Care Patient Profile";
                    DataSet theDS = PatientProfile();
                    rptDocument.SetDataSource(theDS);
                    rptDocument.SetParameterValue("SatelliteId", Session["AppSatelliteId"].ToString());

                }



                //-----First add the columns to theExcelDT
                Int32 columnCouter;
                columnCouter = 0;
                if ((theReportName == "rptLandscape") || (theReportName == "rptPotrait"))
                {

                    DataSet theDS = (DataSet)Session["ReportData"];
                    string theFPath = Server.MapPath("..\\XMLFiles\\CustomReport.xml");
                    theDS.WriteXmlSchema(theFPath);
                    rptDocument.SetDataSource(theDS);
                    rptDocument.SetParameterValue("@TotalCount", theDS.Tables[2].Rows.Count);
                    Session.Remove("ReportData");

                    DataTable dtForLabel;
                    DataTable dtForValue;

                    if (theReportName == "PCustomReport")
                    {
                        dtForLabel = theDS.Tables[3];
                        dtForValue = theDS.Tables[2];
                    }
                    else
                    {
                        dtForLabel = theDS.Tables[3];
                        dtForValue = theDS.Tables[2];
                    }
                    theExcelDT = new DataTable();
                    if (dtForLabel.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtForLabel.Columns.Count; i++)
                        {
                            if (dtForLabel.Rows[0][i].ToString() != "")
                            {

                                //theExcelDT.Columns.Add(dtForLabel.Rows[0][i].ToString(), (System.Type) dtForLabel.Columns[i].DataType.); //System.Type.GetType(dsCustomReport.Tables["dtPortraitCustomReportLabel"].Columns[i].DataType.ToString())  );//   System.Type.GetType());
                                theExcelDT.Columns.Add(dtForLabel.Rows[0][i].ToString()); //System.Type.GetType(dsCustomReport.Tables["dtPortraitCustomReportLabel"].Columns[i].DataType.ToString())  );//   System.Type.GetType());
                                columnCouter++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        //Set values in columns
                        for (int i = 0; i < dtForValue.Rows.Count; i++)
                        {
                            DataRow theDR = theExcelDT.NewRow();
                            for (int j = 0; j < columnCouter; j++)
                            {
                                if (isDate(dtForValue.Rows[i][j].ToString()) == 1)
                                {
                                    theDR[j] = Convert.ToDateTime(dtForValue.Rows[i][j]).ToString(Session["AppDateFormat"].ToString());
                                }
                                else
                                {
                                    theDR[j] = dtForValue.Rows[i][j].ToString();
                                }
                            }
                            theExcelDT.Rows.InsertAt(theDR, i);
                        }
                    }

                    Session["RptData"] = theExcelDT;

                    ViewState["FName"] = "Custom Report";
                }

                //------------------------------------

                if ((theReportName != "rptLandscape") && (theReportName != "rptPotrait"))
                {

                    theEnrollmentID = Session["AppCountryId"].ToString() + "-" + Session["AppPosID"].ToString() + "-" + Session["AppSatelliteId"].ToString() + "-";
                    rptDocument.SetParameterValue("EnrollmentID", theEnrollmentID);
                    crViewer.EnableParameterPrompt = false;

                }
                crViewer.ReportSource = rptDocument;
                crViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;


                rptDocument.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("..\\ExcelFiles\\PView.pdf"));
                ViewState["RepName"] = theReportName;
                Session["Report"] = rptDocument;

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
        private string getCurrency()
        {
            DataTable dtCurrency = new DataTable();
            System.Data.DataSet theDS = new System.Data.DataSet();
            theDS.ReadXml(Server.MapPath("..\\XMLFiles\\Currency.xml"));
            DataView theCurrDV = new DataView(theDS.Tables[0]);
            theCurrDV.RowFilter = "Id=" + Convert.ToInt32(Session["AppCurrency"]);
            string thestringCurrency = theCurrDV[0]["Name"].ToString();
            return thestringCurrency.Substring(thestringCurrency.LastIndexOf("(") + 1, 3);

        }
        private int isDate(string columnValue)
        {
            DateTime dt;
            try
            {
                dt = Convert.ToDateTime(columnValue);
                return 1;
            }
            catch
            {
                return 0;
            }
        }
        #region Patient Prorile Function for fill Datasource
        private DataSet PatientProfile()
        {
            IReports ReportDetails = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
            DataSet dsReportsPatientProfile = (DataSet)ReportDetails.GetPatientProfileAndHistory(thePatientId);
            ReportDetails = null;
            dsReportsPatientProfile.WriteXmlSchema(Server.MapPath("..\\XMLFiles\\PatientProfile.xml"));//Rupesh-22-Apr-08
            return dsReportsPatientProfile;
        }
        #endregion

        #endregion

        # region System Generated Code


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack != true)
            {
                Init_Page();
                Set_PatientReports();
            }
            else
            {
                crViewer.ReportSource = (ReportDocument)Session["Report"];
                crViewer.ToolPanelView = CrystalDecisions.Web.ToolPanelViewType.None;
            }
        }


        protected void btnExcel_Click(object sender, EventArgs e)
        {
            ////string FName = ViewState["FName"].ToString();
            ////IQCareUtils theUtils = new IQCareUtils();
            ////string thePath = Server.MapPath("..\\ExcelFiles\\" + FName + ".xls");
            ////string theTemplatePath = Server.MapPath("..\\ExcelFiles\\IQCareTemplate.xls");
            ////theUtils.ExportToExcel((DataTable)Session["RptData"], thePath, theTemplatePath);
            //////theUtils.OpenExcelFile(thePath, Response);
            ////Response.Redirect("..\\ExcelFiles\\" + FName + ".xls");

            IQWebUtils Util = new IQWebUtils();
            Util.ExporttoExcel((DataTable)Session["RptData"], Response);
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //string theScript = "<script language='javascript' id='PrintPopup'>\n";
            //theScript += "window.open('"+ Server.MapPath("..\\ExcelFiles\\PView.pdf").ToString().Replace("\\","/") +"');\n";
            //theScript += "</script>\n";
            //Page.RegisterClientScriptBlock("PrintPopup", theScript); 
            //if (Request.QueryString["ReportName"].ToString() == "PharmacyPrescription")
            //{

            //    #region "Patient Pharmacy Prescription"

            //        ReportDocument rptPrintDocument = new ReportDocument();
            //        rptPrintDocument.Load(Server.MapPath(strPrintPrescriptionRpt()+"1.rpt"));
            //        DataTable theDTMod = (DataTable)Session["AppModule"];
            //        DataView theDVMod = new DataView(theDTMod);
            //        theDVMod.RowFilter = "ModuleId=" + Convert.ToInt32(Session["TechnicalAreaId"]);

            //        rptPrintDocument.SetDataSource(GetDSPrintPrescription());
            //        rptPrintDocument.SetParameterValue("PharmacyID", Convert.ToInt32(Session["PatientId"]).ToString());
            //        rptPrintDocument.SetParameterValue("ModuleName", theDVMod[0]["ModuleName"].ToString());
            //        rptPrintDocument.SetParameterValue("Currency", getCurrency().ToString());
            //        rptPrintDocument.SetParameterValue("FacilityName", Session["AppLocation"].ToString());
            //        rptPrintDocument.SetParameterValue("ImageFlag", ImageFlag.ToString());
            //        rptPrintDocument.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("..\\ExcelFiles\\PView.pdf"));

            //    #endregion
            //}
            Response.Redirect("..\\ExcelFiles\\PView.pdf");


        }
        #endregion
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.ToString().IndexOf("ReportName") != -1)
            {
                if (Request.QueryString["ReportName"].ToString() == "PatientARVPickup")
                {

                    string theUrl = string.Format("{0}?PatientId={1}&SatelliteID={2}&CountryID={3}&PosID={4}", "frmReport_PatientARVPickup.aspx", Request.QueryString["PatientId"].ToString(), Request.QueryString["SatelliteID"].ToString(), Request.QueryString["CountryID"].ToString(), Request.QueryString["PosID"].ToString());
                    Response.Redirect(theUrl);

                }

                if (Request.QueryString["ReportName"].ToString() == "PatientProfile")
                {
                    Response.Redirect("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Convert.ToInt32(Request.QueryString["PatientId"]));
                }
                if (Request.QueryString["ReportName"].ToString() == "PharmacyPrescription")
                {
                    Response.Redirect("../Pharmacy/frmPharmacyForm.aspx");
                }
            }
            else
            {
                if (Request.QueryString["ReportId"] != null && Request.QueryString["ReportId"].ToString() != "")
                {
                    if (Convert.ToInt32(Request.QueryString["ReportId"]) > 0)
                    {
                        Response.Redirect("frmReportCustomNew.aspx?ReportId=" + Request.QueryString["ReportId"].ToString(), true);
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Back", "<script>history.back();</script>");
                }
            }
            Session.Remove("Report");
        }
    }
}