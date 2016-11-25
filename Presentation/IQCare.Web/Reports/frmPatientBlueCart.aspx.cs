using System;
using System.Data;
using System.Text;
using System.Xml;
using Application.Common;
using Application.Presentation;
using Interface.Reports;
using Excel = Microsoft.Office.Interop.Owc11;
//using Excel = Microsoft.Office.Interop.Owc11;
using System.Web;
using System.Runtime.InteropServices;
namespace IQCare.Web.Reports
{
    /// <summary>
    /// 
    /// </summary>
    public partial class frmPatientBlueCart : System.Web.UI.Page
    {
        /// <summary>
        /// The is exportdone
        /// </summary>
        public Boolean isExportdone;
        /// <summary>
        /// The patientid
        /// </summary>
        public int patientid = 0;
        /// <summary>
        /// The rep ds
        /// </summary>
        public DataSet theRepDS;
        /// <summary>
        /// The visitid
        /// </summary>
        public Int32 visitid;
        /// <summary>
        /// The isvisit sheet
        /// </summary>
        bool isvisitSheet = false;

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["PatientID"] != null)
                {
                    try
                    {
                        patientid = Convert.ToInt32(Session["PatientId"]);
                        IReports theQBuilderReports = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports, BusinessProcess.Reports");
                        theRepDS = theQBuilderReports.GetbluecartIEFUinfo(patientid);

                        //  theRepDS.WriteXml("c:\\TestHL7.xml"); 
                        #region "TableNames"
                        // Moh reg
                        theRepDS.Tables[0].TableName = "patientinfo";
                        theRepDS.Tables[1].TableName = "EmergContact";
                        theRepDS.Tables[2].TableName = "Referred";
                        theRepDS.Tables[3].TableName = "PrevARV";
                        theRepDS.Tables[4].TableName = "HIVDiagnosis";
                        theRepDS.Tables[5].TableName = "patientAllergy";
                        theRepDS.Tables[6].TableName = "FamilyInfo";
                        theRepDS.Tables[7].TableName = "patientlabresults";
                        theRepDS.Tables[8].TableName = "firstgegimen";
                        // theRepDS.Tables[9].TableName = "secondregimen";

                        // Moh visit
                        theRepDS.Tables[9].TableName = "IEFuinfopervisit";
                        theRepDS.Tables[10].TableName = "Disclosure";
                        theRepDS.Tables[11].TableName = "NewOisOtherProblems";
                        // theRepDS.Tables[12].TableName = "Assessment";
                        theRepDS.Tables[12].TableName = "CotrimoxazoleAdherence";
                        theRepDS.Tables[13].TableName = "INHdrug";
                        theRepDS.Tables[14].TableName = "sideeffect";
                        theRepDS.Tables[15].TableName = "Patientvisitinfo";
                        theRepDS.Tables[16].TableName = "Tuberclulosis";
                        theRepDS.Tables[17].TableName = "otherMedication";
                        theRepDS.Tables[18].TableName = "labinvestigation";
                        theRepDS.Tables[19].TableName = "ArvdrugAdherence";
                        theRepDS.Tables[20].TableName = "HIVTest";


                        #endregion

                        // Use For HL7 add namespace <ClinicalDocument xmlns=""urn:hl7-org:v3"">")
                        StringBuilder a = new StringBuilder();
                        StringBuilder b = new StringBuilder();
                        StringBuilder c = new StringBuilder();
                        StringBuilder d = new StringBuilder();
                        b.Append(@"<?xml version=""1.0""?><ClinicalDocument xmlns=""urn:hl7-org:v3"">");
                        d.Append("</ClinicalDocument>");



                        //Response.Redirect("..\\ExcelFiles\\TZNACPMonthlyReport.xls");
                        theRepDS.WriteXml(Server.MapPath("..\\XMLFiles\\HL7\\" + patientid.ToString() + "HLData.xml"), XmlWriteMode.WriteSchema);


                        // Use For HL7 new xml file with namespace

                        XmlDocument doc1 = new XmlDocument();
                        doc1.Load(Server.MapPath("..\\XMLFiles\\HL7\\" + patientid.ToString() + "HLData.xml"));
                        XmlNode rootNode = doc1.SelectSingleNode("NewDataSet");
                        a.Append(rootNode.OuterXml.ToString());

                        c.Append(b).Append(a).Append(d);
                        doc1 = new XmlDocument();
                        doc1.LoadXml(c.ToString());
                        doc1.Save(Server.MapPath("..\\XMLFiles\\HL7\\" + patientid.ToString() + "HLData.xml"));



                        //theRepDS.WriteXml(Server.MapPath("..\\XMLFiles\\HL7\\" + patientid.ToString() + "HLData.xml"));
                        Excel.SpreadsheetClass theApp = new Microsoft.Office.Interop.Owc11.SpreadsheetClass();
                        string theFilePath = Server.MapPath("..\\ExcelFiles\\Templates\\Bluecard.xml");
                        theApp.XMLURL = theFilePath;
                        fillMohRegSheet(theApp);
                        fillMohVisitsheet(theApp);

                        // theApp.Export(Server.MapPath("..\\ExcelFiles\\TZNACPMonthlyReport.xls"), Microsoft.Office.Interop.Owc11.SheetExportActionEnum.ssExportActionOpenInExcel, Microsoft.Office.Interop.Owc11.SheetExportFormat.ssExportAsAppropriate);
                        theApp.Export(Server.MapPath("..\\ExcelFiles\\Bluecard.xls"), Excel.SheetExportActionEnum.ssExportActionNone, Excel.SheetExportFormat.ssExportXMLSpreadsheet);
                        //theUtils.OpenExcelFile(Server.MapPath("..\\ExcelFiles\\TZNACPMonthlyReport.xls"), Response);
                        IQWebUtils theUtl = new IQWebUtils();
                        theUtl.ShowExcelFile(Server.MapPath("..\\ExcelFiles\\Bluecard.xls"), Response);
                        releaseObject(theApp);
                    }
                    catch (System.IO.DirectoryNotFoundException exd)
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["MessageText"] = exd.Message.ToString();
                        IQCareMsgBox.Show("#C1", theBuilder, this);
                    }
                    catch (COMException cex)
                    {
                        throw cex;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

        }


        /// <summary>
        /// Fills the moh reg sheet.
        /// </summary>
        /// <param name="theApp">The application.</param>
        private void fillMohRegSheet(Excel.Spreadsheet theApp)
        {
            Excel.Worksheet theSheet = (Excel.Worksheet)theApp.Sheets[2];
            writeInCell(theSheet, "F4", "patientinfo", "FacilityName", 0);
            writeInCell(theSheet, "X4", "patientinfo", "PatientClinicNumber", 0);

            writeInCell(theSheet, "S8", "patientinfo", "UniquePatNumber", 0);
            string name = theRepDS.Tables["patientinfo"].Rows[0]["FirstName"].ToString() + " " + theRepDS.Tables["patientinfo"].Rows[0]["LastName"].ToString();
            writeInCell(theSheet, "C11", "", name, 0);
            if (theRepDS.Tables["patientinfo"].Rows[0]["Gender"].ToString().Contains("Female"))
            {
                // writeInCell(theSheet, "D13", "", "N", 0);
                writeInCell(theSheet, "I13", "", "Y", 0);
            }
            else
            {
                writeInCell(theSheet, "D13", "", "Y", 0);

                // writeInCell(theSheet, "I13", "", "N", 0);
            }

            writeInCell(theSheet, "W11", "", theRepDS.Tables["PatientInfo"].Rows[0]["DOB"].ToString(), 0); ;
            writeInCell(theSheet, "AD11", "patientinfo", "Age", 0);
            writeInCell(theSheet, "X13", "patientinfo", "Address", 0);
            writeInCell(theSheet, "D14", "patientinfo", "TelContact", 0);

            writeInCell(theSheet, "D15", "patientinfo", "District", 0);
            writeInCell(theSheet, "W15", "patientinfo", "Location", 0);


            writeInCell(theSheet, "D16", "patientinfo", "SubLocation", 0);
            writeInCell(theSheet, "W16", "patientinfo", "LandMark", 0);

            if (theRepDS.Tables["patientinfo"].Rows[0]["MaritalStatus"].ToString().Contains("Single"))
            {

                writeInCell(theSheet, "T21", "", "Y", 0);
            }
            else if (theRepDS.Tables["patientinfo"].Rows[0]["MaritalStatus"].ToString().Contains("Married"))
            {
                writeInCell(theSheet, "D21", "", "Y", 0);
            }
            else if (theRepDS.Tables["patientinfo"].Rows[0]["MaritalStatus"].ToString().Contains("Divorced"))
            {
                writeInCell(theSheet, "N19", "", "Y", 0);
            }
            else if (theRepDS.Tables["patientinfo"].Rows[0]["MaritalStatus"].ToString().Contains("Cohabiting"))
            {

                writeInCell(theSheet, "T19", "", "Y", 0);
            }
            else if (theRepDS.Tables["patientinfo"].Rows[0]["MaritalStatus"].ToString().Contains("Married Polygamous"))
            {

                writeInCell(theSheet, "D19", "", "Y", 0);
            }
            else if (theRepDS.Tables["patientinfo"].Rows[0]["MaritalStatus"].ToString().Contains("Married Monogamous"))
            {

                writeInCell(theSheet, "D21", "", "Y", 0);
            }
            else if (theRepDS.Tables["patientinfo"].Rows[0]["MaritalStatus"].ToString().Contains("Widowed"))
            {

                writeInCell(theSheet, "N21", "", "Y", 0);
            }


            // EmergContact
            writeInCell(theSheet, "F23", "EmergContact", "TSName", 0);
            writeInCell(theSheet, "Y23", "EmergContact", "TSRelation", 0);
            writeInCell(theSheet, "F24", "EmergContact", "TSAddress", 0);
            writeInCell(theSheet, "W24", "EmergContact", "TSPhone", 0);

            //
            //  for (int i = 0; i < theRepDS.Tables["Referred"].Rows.Count; i++)
            if (theRepDS.Tables["Referred"].Rows.Count > 0)
            {
                if (theRepDS.Tables["Referred"].Rows[0]["ReferredFrom"].ToString().Contains("VCT"))
                {
                    writeInCell(theSheet, "D30", "", "Y", 0);
                }
                else if (theRepDS.Tables["Referred"].Rows[0]["ReferredFrom"].ToString().Contains("PMTCT"))
                {
                    writeInCell(theSheet, "D28", "", "Y", 0);
                }
                else if (theRepDS.Tables["Referred"].Rows[0]["ReferredFrom"].ToString().Contains("IPD - Ad"))
                {
                    writeInCell(theSheet, "N28", "", "Y", 0);
                }
                else if (theRepDS.Tables["Referred"].Rows[0]["ReferredFrom"].ToString().Contains("TB Clinic"))
                {
                    writeInCell(theSheet, "V28", "", "Y", 0);
                }
                else if (theRepDS.Tables["Referred"].Rows[0]["ReferredFrom"].ToString().Contains("OPD"))
                {
                    writeInCell(theSheet, "AD28", "", "Y", 0);
                }
                else if (theRepDS.Tables["Referred"].Rows[0]["ReferredFrom"].ToString().Contains("IPD - Ch"))
                {
                    writeInCell(theSheet, "L30", "", "Y", 0);
                }
                else if (theRepDS.Tables["Referred"].Rows[0]["ReferredFrom"].ToString().Contains("MCH child"))
                {
                    writeInCell(theSheet, "V30", "", "Y", 0);
                }
                else
                {
                    writeInCell(theSheet, "D32", "", "Y", 0);
                    writeInCell(theSheet, "M32", "Referred", "Transfered From Facility", 0);
                }

            }

            writeInCell(theSheet, "G34", "Referred", "TransferInDate", 0);
            writeInCell(theSheet, "V34", "Referred", "ReferredFrom", 0);
            writeInCell(theSheet, "G36", "Referred", "Transfered From Facility", 0);
            writeInCell(theSheet, "Z36", "Referred", "ARTStartDate", 0);

            //PrevARV
            if (theRepDS.Tables["PrevARV"].Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(Convert.ToString(theRepDS.Tables["PrevARV"].Rows[0]["PrevARV1"])))
                {
                    writeInCell(theSheet, "P43", "PrevARV", "PrevARV1", 0);
                    writeInCell(theSheet, "AB43", "PrevARV", "PrevARV1dtUsed", 0);
                }

                if (!String.IsNullOrEmpty(Convert.ToString(theRepDS.Tables["PrevARV"].Rows[0]["PrevARV2"])))
                {
                    writeInCell(theSheet, "P45", "PrevARV", "PrevARV2", 0);
                    writeInCell(theSheet, "AB45", "PrevARV", "PrevARV2dtUsed", 0);
                }
                if (!String.IsNullOrEmpty(Convert.ToString(theRepDS.Tables["PrevARV"].Rows[0]["PrevARV3"])))
                {
                    writeInCell(theSheet, "P47", "PrevARV", "PrevARV3", 0);
                    writeInCell(theSheet, "AB47", "PrevARV", "PrevARV3dtUsed", 0);
                }
            }
            //HIVDiagnosis
            writeInCell(theSheet, "I49", "HIVDiagnosis", "DtConfirmHIVPositive", 0);
            writeInCell(theSheet, "I50", "HIVDiagnosis", "dtEnrolledHIVCare", 0);
            writeInCell(theSheet, "AB51", "HIVDiagnosis", "WHOStage", 0);
            writeInCell(theSheet, "I51", "patientAllergy", "Allergy", 0);

            //FamilyInfo
            for (int i = 0; i < 5; i++)
            {
                writeInCell(theSheet, "C" + (57 + i).ToString(), "", (i + 1).ToString(), 0);
                writeInCell(theSheet, "D" + (57 + i).ToString(), "FamilyInfo", "RName", i);
                writeInCell(theSheet, "K" + (57 + i).ToString(), "FamilyInfo", "AgeYear", i);
                writeInCell(theSheet, "M" + (57 + i).ToString(), "FamilyInfo", "Relation", i);

                writeInCell(theSheet, "T" + (57 + i).ToString(), "FamilyInfo", "HIVStatus", i);
                writeInCell(theSheet, "W" + (57 + i).ToString(), "FamilyInfo", "InCare", i);
                writeInCell(theSheet, "Z" + (57 + i).ToString(), "FamilyInfo", "CCCNumber", i);
            }

            //
            if (theRepDS.Tables["patientlabresults"].Rows.Count > 0)
            {
                // if (!String.IsNullOrEmpty(theRepDS.Tables["patientlabresults"].Rows[0]["CD4Count"].ToString()))
                // {
                writeInCell(theSheet, "BA10", "", "Y", 0);
                writeInCell(theSheet, "BI11", "patientlabresults", "CD4Count", 0);
                writeInCell(theSheet, "AV11", "patientlabresults", "WHOStage", 0);
                writeInCell(theSheet, "AO17", "patientlabresults", "Weight", 0);
                writeInCell(theSheet, "AV17", "patientlabresults", "Height", 0);
                writeInCell(theSheet, "BD17", "patientlabresults", "BMI", 0);
                writeInCell(theSheet, "BK17", "patientlabresults", "WHOStage", 0);
                // }
            }
            for (int i = 0; i < 3; i++)
            {
                writeInCell(theSheet, "AS" + (i + 22).ToString(), "firstgegimen", "RegimenType", i);
                writeInCell(theSheet, "AK" + (i + 22).ToString(), "firstgegimen", "RegDate", i);
            }

            releaseObject(theSheet);
        }
        //
        /// <summary>
        /// Fills the moh visitsheet.
        /// </summary>
        /// <param name="theApp">The application.</param>
        private void fillMohVisitsheet(Excel.Spreadsheet theApp)
        {
            Int32 theColIdx = 73;
            Int32 theRepIdx = 64;
            string xlcolumn;
            isvisitSheet = true;
            string lastcolumn = string.Empty;

            Excel.Worksheet worksheet = (Excel.Worksheet)theApp.Sheets[3];
            writeInCell(worksheet, "E5", "Patientvisitinfo", "IQNumber", 0);
            //xlcolumn = 'I';
            Excel.Range theRange;
            for (int a = 0; a < theRepDS.Tables["IEFuinfopervisit"].Rows.Count; a++)
            {
                visitid = Convert.ToInt32(theRepDS.Tables["IEFuinfopervisit"].Rows[a]["Visit_id"].ToString());
                if (a > 17)
                {
                    if (theColIdx > 90)
                    {
                        theColIdx = 65;
                        theRepIdx = theRepIdx + 1;
                    }

                    xlcolumn = Convert.ToChar(theRepIdx).ToString() + Convert.ToChar(theColIdx).ToString();
                    theColIdx = theColIdx + 1;
                }
                else
                {
                    xlcolumn = Convert.ToChar(theColIdx).ToString();
                    theColIdx = theColIdx + 1;
                }

                if (a > 0)
                {
                    writeInCell(worksheet, xlcolumn + "7", "", "V" + (a + 1).ToString(), 0);
                    //
                    theRange = (Excel.Range)theApp.ActiveSheet.Cells[7, 73 + a];
                    theRange.EntireRow.Font.set_Bold(true);



                    for (int b = 7; b <= 44; b++)
                    {

                        theRange = worksheet.Cells.get_Range(xlcolumn + b.ToString(), xlcolumn + b.ToString());
                        theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, 1);
                    }

                }
                lastcolumn = xlcolumn;
                writeCellWiseInExcel(worksheet, xlcolumn, a);
            }
            /////////////////////
            if (!string.IsNullOrEmpty(lastcolumn))
            {
                theRange = worksheet.get_Range("B4", lastcolumn + "6");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);


                theRange = worksheet.get_Range("B7", lastcolumn + "7");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                object obj = new Object();
                obj = (object)("0xD8D8D8").ToString();
                theRange.Interior.set_Color(ref obj);

                theRange = worksheet.get_Range("B4", lastcolumn + "44");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B10", lastcolumn + "11");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B12", lastcolumn + "15");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B16", lastcolumn + "17");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B18", lastcolumn + "19");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B20", lastcolumn + "21");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B22", lastcolumn + "22");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B23", lastcolumn + "23");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B24", lastcolumn + "24");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);


                theRange = worksheet.get_Range("B25", lastcolumn + "26");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);


                theRange = worksheet.get_Range("B27", lastcolumn + "27");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B28", lastcolumn + "28");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B29", lastcolumn + "31");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B32", lastcolumn + "36");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);


                theRange = worksheet.get_Range("B37", lastcolumn + "37");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B38", lastcolumn + "38");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);


                theRange = worksheet.get_Range("B39", lastcolumn + "42");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B43", lastcolumn + "43");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);

                theRange = worksheet.get_Range("B44", lastcolumn + "44");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);
                //////////////////////////

            }
            else
            {
                theRange = worksheet.get_Range("B4", "I6");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);


                theRange = worksheet.get_Range("B7", "I7");
                theRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, 1);
            }

            releaseObject(theRange);
            releaseObject(worksheet);

        }
        /// <summary>
        /// Writes the cell wise in excel.
        /// </summary>
        /// <param name="theSheet">The sheet.</param>
        /// <param name="charcolumn">The charcolumn.</param>
        /// <param name="tablerow">The tablerow.</param>
        private void writeCellWiseInExcel(Excel.Worksheet theSheet, string charcolumn, int tablerow)
        {
            try
            {
                writeInCell(theSheet, charcolumn + "8", "IEFuinfopervisit", "visitdate", tablerow);
                writeInCell(theSheet, charcolumn + "9", "IEFuinfopervisit", "Visit Type", tablerow);
                //  writeInCell(theSheet, charcolumn + "10", "IEFuinfopervisit", "HistoricalARTStDate", tablerow);
                writeInCell(theSheet, charcolumn + "10", "Patientvisitinfo", "ARTStartDate", tablerow);
                writeInCell(theSheet, charcolumn + "11", "IEFuinfopervisit", "HistoricalART", tablerow);
                writeInCell(theSheet, charcolumn + "12", "IEFuinfopervisit", "Weight", tablerow);
                writeInCell(theSheet, charcolumn + "13", "IEFuinfopervisit", "BP", tablerow);
                writeInCell(theSheet, charcolumn + "14", "IEFuinfopervisit", "Height", tablerow);
                writeInCell(theSheet, charcolumn + "15", "IEFuinfopervisit", "BMI", tablerow);
                writeInCell(theSheet, charcolumn + "16", "IEFuinfopervisit", "Pregnancy Status", tablerow);
                writeInCell(theSheet, charcolumn + "17", "IEFuinfopervisit", "Pregnancy EDD", tablerow);
                writeInCell(theSheet, charcolumn + "24", "IEFuinfopervisit", "WHOStage", tablerow);
                writeInCell(theSheet, charcolumn + "43", "IEFuinfopervisit", "AppDate", tablerow);

                if ((Convert.ToString(theRepDS.Tables["IEFuinfopervisit"].Rows[tablerow]["Adherence_Missed Last Week"]) == "99999") && (Convert.ToString(theRepDS.Tables["IEFuinfopervisit"].Rows[tablerow]["Adherence_Missed Last_month"]) == "99999"))
                {
                    writeInCell(theSheet, charcolumn + "29", "", "Good", tablerow);
                }
                else
                {
                    //ArvdrugAdherence
                    if (theRepDS.Tables["ArvdrugAdherence"].Rows.Count > 0)
                    {
                        DataView theDV = new DataView((DataTable)theRepDS.Tables["ArvdrugAdherence"]);
                        // DataTable thedt = new DataTable();
                        theDV.RowFilter = "Visit_Pk =" + visitid.ToString();
                        if (theDV.Count > 0)
                        {
                            string weekmissedAderence = " ";
                            string monthmissedAderence = " ";
                            writeInCell(theSheet, charcolumn + "30", "", theDV[0]["Adherence_Missed_Reason"].ToString(), 0);
                            if ((Convert.ToString(theRepDS.Tables["IEFuinfopervisit"].Rows[tablerow]["Adherence_Missed Last Week"]) != "") && (Convert.ToString(theRepDS.Tables["IEFuinfopervisit"].Rows[tablerow]["Adherence_Missed Last Week"]) != "99999"))
                            {
                                weekmissedAderence = "Week-" + theRepDS.Tables["IEFuinfopervisit"].Rows[tablerow]["Adherence_Missed Last Week"].ToString();
                            }
                            if ((Convert.ToString(theRepDS.Tables["IEFuinfopervisit"].Rows[tablerow]["Adherence_Missed Last_month"]) != "") && (Convert.ToString(theRepDS.Tables["IEFuinfopervisit"].Rows[tablerow]["Adherence_Missed Last_month"]) != "99999"))
                            {
                                monthmissedAderence = "Month-" + theRepDS.Tables["IEFuinfopervisit"].Rows[tablerow]["Adherence_Missed Last_month"].ToString();
                            }
                            writeInCell(theSheet, charcolumn + "31", "", weekmissedAderence + " " + monthmissedAderence, 0);
                        }
                    }
                }

                //Patientvisitinfo


                if (theRepDS.Tables["Patientvisitinfo"].Rows.Count > 0)
                {
                    DataView theDV = new DataView((DataTable)theRepDS.Tables["Patientvisitinfo"]);
                    // DataTable thedt = new DataTable();
                    theDV.RowFilter = "Visit_Id =" + visitid.ToString();
                    if (theDV.Count > 0)
                    {
                        writeInCell(theSheet, charcolumn + "44", "", theDV[0]["signature"].ToString(), 0);

                    }
                }
                // CotrimoxazoleAdherence
                if (theRepDS.Tables["CotrimoxazoleAdherence"].Rows.Count > 0)
                {
                    DataView theDV = new DataView((DataTable)theRepDS.Tables["CotrimoxazoleAdherence"]);
                    // DataTable thedt = new DataTable();
                    theDV.RowFilter = "Visit_Id =" + visitid.ToString();
                    if (theDV.Count > 0)
                    {
                        writeInCell(theSheet, charcolumn + "25", "", "Yes", tablerow);
                        writeInCell(theSheet, charcolumn + "26", "", "Yes", tablerow);
                    }
                }
                // INHdrug
                if (theRepDS.Tables["INHdrug"].Rows.Count > 0)
                {
                    DataView theDV = new DataView((DataTable)theRepDS.Tables["INHdrug"]);
                    // DataTable thedt = new DataTable();
                    theDV.RowFilter = "Visit_Id =" + visitid.ToString();
                    if (theDV.Count > 0)
                    {
                        writeInCell(theSheet, charcolumn + "27", "", "Yes", 0);
                    }
                }
                //NewOisOtherProblems
                if (theRepDS.Tables["NewOisOtherProblems"].Rows.Count > 0)
                {
                    DataView theDV = new DataView((DataTable)theRepDS.Tables["NewOisOtherProblems"]);
                    // DataTable thedt = new DataTable();
                    theDV.RowFilter = "Visit_Pk =" + visitid.ToString();
                    if (theDV.Count > 0)
                    {
                        //Excel.Range theRange1 = theSheet.Cells.get_Range(charcolumn + "23", charcolumn + "23");
                        //theRange1.Value2 = theDV[0]["patientdisease"];
                        writeInCell(theSheet, charcolumn + "23", "", theDV[0]["patientdisease"].ToString(), 0);
                        if (theDV[0]["patientdisease"].ToString() != "")
                        {
                            writeInCell(theSheet, charcolumn + "42", "", "Yes", 0);
                        }
                    }
                }

                //sideeffect
                if (theRepDS.Tables["sideeffect"].Rows.Count > 0)
                {
                    DataView theDV = new DataView((DataTable)theRepDS.Tables["sideeffect"]);
                    // DataTable thedt = new DataTable();
                    theDV.RowFilter = "Visit_Pk =" + visitid.ToString();
                    if (theDV.Count > 0)
                    {
                        //Excel.Range theRange2 = theSheet.Cells.get_Range(charcolumn + "22", charcolumn + "22");
                        //theRange2.Value2 = theDV[0]["symptom"];
                        writeInCell(theSheet, charcolumn + "22", "", theDV[0]["symptom"].ToString(), 0);
                    }
                }
                //Tuberclulosis
                if (theRepDS.Tables["Tuberclulosis"].Rows.Count > 0)
                {
                    DataView theDV = new DataView((DataTable)theRepDS.Tables["Tuberclulosis"]);
                    // DataTable thedt = new DataTable();
                    theDV.RowFilter = "Visit_Id =" + visitid.ToString();
                    if (theDV.Count > 0)
                    {
                        //Excel.Range theRange2 = theSheet.Cells.get_Range(charcolumn + "22", charcolumn + "22");
                        //theRange2.Value2 = theDV[0]["symptom"];
                        writeInCell(theSheet, charcolumn + "20", "", theDV[0]["TBStatus"].ToString(), 0);
                        writeInCell(theSheet, charcolumn + "21", "", theDV[0]["TBRegimen"].ToString(), 0);
                    }
                }
                //otherMedication
                if (theRepDS.Tables["otherMedication"].Rows.Count > 0)
                {
                    DataView theDV = new DataView((DataTable)theRepDS.Tables["otherMedication"]);
                    // DataTable thedt = new DataTable();
                    theDV.RowFilter = "Visit_Id =" + visitid.ToString();
                    if (theDV.Count > 0)
                    {
                        //Excel.Range theRange2 = theSheet.Cells.get_Range(charcolumn + "22", charcolumn + "22");
                        //theRange2.Value2 = theDV[0]["symptom"];
                        writeInCell(theSheet, charcolumn + "28", "", theDV[0]["OtherMeds"].ToString(), 0);

                    }
                }
                // labinvestigation
                if (theRepDS.Tables["labinvestigation"].Rows.Count > 0)
                {
                    DataView theDV = new DataView((DataTable)theRepDS.Tables["labinvestigation"]);
                    theDV.RowFilter = "Visit_Id =" + visitid.ToString();
                    if (theDV.Count > 0)
                    {
                        for (int i = 0; i < theDV.Count; i++)
                        {
                            // 1 for cd4
                            if (theDV[i]["ParameterId"].ToString() == "1")
                            {
                                writeInCell(theSheet, charcolumn + "32", "", theDV[i]["TestResults"].ToString(), 0);
                            }
                            //6 for HB
                            if (theDV[i]["ParameterId"].ToString() == "6")
                            {
                                writeInCell(theSheet, charcolumn + "33", "", theDV[i]["TestResults"].ToString(), 0);
                            }
                            //75 for RPR
                            if (theDV[i]["ParameterId"].ToString() == "75")
                            {
                                writeInCell(theSheet, charcolumn + "34", "", theDV[i]["TestResults"].ToString(), 0);
                            }
                            //6 for HB
                            if ((theDV[i]["ParameterId"].ToString() == "17") || (theDV[i]["ParameterId"].ToString() == "18") || (theDV[i]["ParameterId"].ToString() == "19"))
                            {
                                writeInCell(theSheet, charcolumn + "35", "", theDV[i]["TestResults"].ToString(), 0);
                            }
                        }
                    }
                }
                //Disclosure
                if (theRepDS.Tables["Disclosure"].Rows.Count > 0)
                {

                    writeInCell(theSheet, charcolumn + "39", "Disclosure", "DisclosureTo", 0);
                }
                //  
                writeInCell(theSheet, charcolumn + "41", "", theRepDS.Tables["HIVTest"].Rows[0]["PartnerTested"].ToString(), 0);

            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
        }
        /// <summary>
        /// Writes the in cell.
        /// </summary>
        /// <param name="theSheet">The sheet.</param>
        /// <param name="cell">The cell.</param>
        /// <param name="tablename">The tablename.</param>
        /// <param name="column">The column.</param>
        /// <param name="tablerow">The tablerow.</param>
        private void writeInCell(Excel.Worksheet theSheet, string cell, string tablename, string column, int tablerow)
        {

            Excel.Range theRange = theSheet.Cells.get_Range(cell, cell);

            //theRange.WrapText = true;
            string theExitvalue = "";
            if (theRange.Value2 != null)
                theExitvalue = theRange.Value2.ToString();
            else
                theExitvalue = "";

            try
            {
                if (tablename != "")
                {
                    if (theExitvalue.ToString().Trim() == "")
                    {
                        // theRange.Value2 = theRepDS.Tables[tablename].Rows[0][column].ToString();
                        theRange.Value2 = theRepDS.Tables[tablename].Rows[tablerow][column].ToString();

                    }
                    else
                    {
                        // theRange.Value2 = theExitvalue + "  " + theRepDS.Tables[tablename].Rows[0][column].ToString();
                        theRange.Value2 = theExitvalue + "  " + theRepDS.Tables[tablename].Rows[tablerow][column].ToString();

                    }
                }
                else
                {
                    if (theExitvalue.ToString().Trim() == "")
                        theRange.Value2 = column;
                    else
                        theRange.Value2 = theExitvalue + "  " + column;

                }
                if (isvisitSheet)
                {
                    theRange.EntireColumn.AutoFit();
                }
                releaseObject(theRange);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
        }

        /// <summary>
        /// Releases the object.
        /// </summary>
        /// <param name="obj">The object.</param>
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
                //MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}