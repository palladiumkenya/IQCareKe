using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Reports;

namespace IQCare.Web.Admin
{
    public partial class Export : System.Web.UI.Page
    {
        private DataTable dtColName = new DataTable();
        private DataTable dtViewName = new DataTable();

        // stores all colname for each view
        private string OrderBy = "";

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Session.Remove("SelectedData");
            Session.Remove("AvailableData");
            Response.Redirect("../frmFacilityHome.aspx");
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstAvailable.Items.Count > 0)
                {
                    string theIdentifier = "";

                    string theSelect = "";
                    string theViews = "";
                    string join = "";
                    IReports CustomReport;
                    CustomReport = (IReports)ObjectFactory.CreateInstance("BusinessProcess.Reports.BReports,BusinessProcess.Reports");
                    for (int i = 0; i < lstAvailable.Items.Count; i++)
                    {
                        DataSet theDS = CustomReport.GetFields(Convert.ToInt32(lstAvailable.Items[i].Value), Convert.ToInt32(Session["SystemId"].ToString()));
                        foreach (DataRow theDR in theDS.Tables[0].Rows)
                        {
                            if (theViews == "" && theViews.Contains(theDR["ViewName"].ToString()) != true)
                                theViews = theDR["ViewName"].ToString();
                            else if (theViews.Contains(theDR["ViewName"].ToString()) != true)
                                theViews = theViews + "," + theDR["ViewName"].ToString();
                        }

                        for (int j = 0; j < theDS.Tables[0].Rows.Count; j++)
                        {
                            if (ChkIdentity.Checked == true)
                            {
                                if (theDS.Tables[0].Rows[j]["FieldName"].ToString().Trim() != "Patient Last Name" &&
                                    theDS.Tables[0].Rows[j]["FieldName"].ToString().Trim() != "Patient First Name" &&
                                    theDS.Tables[0].Rows[j]["FieldName"].ToString().Trim() != "Patient Name" &&
                                    theDS.Tables[0].Rows[j]["FieldName"].ToString().Trim() != "Local Council" &&
                                    theDS.Tables[0].Rows[j]["FieldName"].ToString().Trim() != "Patient Phone" &&
                                    theDS.Tables[0].Rows[j]["FieldName"].ToString().Trim() != "Patient Address" &&
                                    theDS.Tables[0].Rows[j]["FieldName"].ToString().Trim() != "Province" &&
                                    theDS.Tables[0].Rows[j]["FieldName"].ToString().Trim() != "Village" &&
                                    theDS.Tables[0].Rows[j]["FieldName"].ToString().Trim() != "Guardian Name" &&
                                    theDS.Tables[0].Rows[j]["FieldName"].ToString().Trim() != "Emergency Contact Name" &&
                                    theDS.Tables[0].Rows[j]["FieldName"].ToString().Trim() != "Emergency Contact Phone" &&
                                    theDS.Tables[0].Rows[j]["FieldName"].ToString().Trim() != "Emergency Contact Address" &&
                                    theDS.Tables[0].Rows[j]["FieldName"].ToString().Trim() != "hvPatientCaretaker")
                                {
                                    if (theSelect == "")
                                        theSelect = theDS.Tables[0].Rows[j]["ViewName"].ToString() + ".[" + theDS.Tables[0].Rows[j]["FieldName"].ToString() + "]";
                                    else
                                        theSelect = theSelect + "," + theDS.Tables[0].Rows[j]["ViewName"].ToString() + ".[" + theDS.Tables[0].Rows[j]["FieldName"].ToString() + "]";
                                }
                            }
                            else
                            {
                                if (theSelect == "")
                                    //theSelect = "[" + theDS.Tables[0].Rows[j]["FieldName"].ToString() + "]";
                                    theSelect = theDS.Tables[0].Rows[j]["ViewName"].ToString() + ".[" + theDS.Tables[0].Rows[j]["FieldName"].ToString() + "]";
                                else
                                    //theSelect = theSelect + ",[" + theDS.Tables[0].Rows[j]["FieldName"].ToString() + "]";
                                    theSelect = theSelect + "," + theDS.Tables[0].Rows[j]["ViewName"].ToString() + ".[" + theDS.Tables[0].Rows[j]["FieldName"].ToString() + "]";
                            }
                        }
                    }

                    #region "Join Creation"
                    string[] arrView = theViews.Split(',');

                    DataTable dtCustomReportJoin = new DataTable();
                    if (arrView.Length > 1)
                    {
                        DataTable theJoins = new DataTable();
                        theJoins.Columns.Add("Criteria", System.Type.GetType("System.String"));
                        int j;
                        int i;
                        for (i = 0; i < arrView.Length; i++)
                        {
                            for (j = i; j < arrView.Length; j++)
                            {
                                ////// As the system is binded with patient and Visit, these two views will have
                                ///// high importance than any other view
                                if (theViews.Contains("Rpt_PatientDemographics") == true)
                                {
                                    theIdentifier = "Rpt_PatientDemographics.Ptn_Pk";
                                    if (arrView[j].ToString() != "Rpt_PatientDemographics")
                                        dtCustomReportJoin = CustomReport.GetCustomReportJoin("Rpt_PatientDemographics", arrView[j].ToString(), 0);
                                }
                                #region "Put Joins Together"
                                if (dtCustomReportJoin != null)
                                {
                                    foreach (DataRow dr in dtCustomReportJoin.Rows)
                                    {
                                        if (theJoins.Rows.Count > 0)
                                        {
                                            DataRow[] theDR = theJoins.Select("Criteria = '" + dr[0].ToString() + "'");
                                            if (theDR.Length < 1)
                                            {
                                                DataRow JDR = theJoins.NewRow();
                                                JDR[0] = dr[0];
                                                theJoins.Rows.Add(JDR);
                                            }
                                        }
                                        else
                                        {
                                            DataRow JDR = theJoins.NewRow();
                                            JDR[0] = dr[0];
                                            theJoins.Rows.Add(JDR);
                                        }
                                    }
                                }

                                #endregion "Put Joins Together"
                                if (theViews.Contains("Rpt_Visit") == true)
                                {
                                    if (arrView[j].ToString() != "Rpt_Visit")
                                    {
                                        theIdentifier = "Rpt_Visit.Ptn_Pk";
                                        DataTable theDT = (DataTable)ViewState["RptTable"];
                                        DataView theDV1 = new DataView(theDT);
                                        theDV1.RowFilter = "FieldId=311";
                                        if (arrView[j].ToString() != "Rpt_PatientDemographics")
                                        {
                                            dtCustomReportJoin = CustomReport.GetCustomReportJoin("Rpt_Visit", arrView[j].ToString(), 0);
                                        }
                                    }
                                }
                                #region "Put Joins Together"
                                if (dtCustomReportJoin != null)
                                {
                                    foreach (DataRow dr in dtCustomReportJoin.Rows)
                                    {
                                        if (theJoins.Rows.Count > 0)
                                        {
                                            DataRow[] theDR = theJoins.Select("Criteria = '" + dr[0].ToString() + "'");
                                            if (theDR.Length < 1)
                                            {
                                                DataRow JDR = theJoins.NewRow();
                                                JDR[0] = dr[0];
                                                theJoins.Rows.Add(JDR);
                                            }
                                        }
                                        else
                                        {
                                            DataRow JDR = theJoins.NewRow();
                                            JDR[0] = dr[0];
                                            theJoins.Rows.Add(JDR);
                                        }
                                    }
                                }

                                #endregion "Put Joins Together"

                                if (theViews.Contains("Rpt_Visit") == false && theViews.Contains("Rpt_PatientDemographics") == false)
                                {
                                    if (arrView[i].ToString() != "Rpt_Employee")
                                    {
                                        theIdentifier = arrView[i].ToString() + ".Ptn_Pk";
                                    }
                                    dtCustomReportJoin = CustomReport.GetCustomReportJoin(arrView[i].ToString(), arrView[j].ToString(), 0);
                                }

                                #region "Put Joins Together"
                                if (dtCustomReportJoin != null)
                                {
                                    foreach (DataRow dr in dtCustomReportJoin.Rows)
                                    {
                                        if (theJoins.Rows.Count > 0)
                                        {
                                            DataRow[] theDR = theJoins.Select("Criteria = '" + dr[0].ToString() + "'");
                                            if (theDR.Length < 1)
                                            {
                                                DataRow JDR = theJoins.NewRow();
                                                JDR[0] = dr[0];
                                                theJoins.Rows.Add(JDR);
                                            }
                                        }
                                        else
                                        {
                                            DataRow JDR = theJoins.NewRow();
                                            JDR[0] = dr[0];
                                            theJoins.Rows.Add(JDR);
                                        }
                                    }
                                }

                                #endregion "Put Joins Together"
                            }
                        }
                        DataView theDV = new DataView(theJoins);

                        for (i = 0; i < theDV.Count; i++)
                        {
                            if (join != "")
                            {
                                join = join + "  and  " + theDV[i]["Criteria"].ToString();
                            }
                            else
                            {
                                join = theDV[i]["Criteria"].ToString();
                            }
                        }
                        theJoins.Rows.Clear();
                    }
                    else if (arrView.Length == 1)
                    {
                        if (arrView[0].ToString() != "Rpt_Employee")
                        {
                            theIdentifier = arrView[0].ToString() + ".Ptn_Pk";
                        }
                    }
                    #endregion "Join Creation"

                    StringBuilder FinalQuery = new StringBuilder().Append(theSelect.ToString() + " From " + theViews.ToString());
                    if (join.Trim() != "")
                        FinalQuery.Append(" Where " + join);
                    if (arrView[0].ToString() != "Rpt_Employee" && arrView[0].ToString() != "Rpt_Facility")
                    {
                        FinalQuery.Append(" Order By [PatientId]");
                        FinalQuery.Insert(0, "SELECT DISTINCT dbo.fn_GetPatientEnrollmentNumber_Constella(" + theIdentifier + ") [PatientId],");
                        //FinalQuery.Replace("SELECT", "SELECT DISTINCT");
                    }
                    else
                    {
                        FinalQuery.Insert(0, "SELECT DISTINCT ");
                    }
                    if (rdoExcel.Checked == true)
                    {
                        ExportToExcel(FinalQuery.ToString());
                    }
                    if (rdoCSV.Checked == true)
                    {
                        ExportToCSV(FinalQuery.ToString());
                    }
                }
                else
                {
                    IQCareMsgBox.Show("ExportSelectFieldGroup", this);
                }
            }
            catch (IndexOutOfRangeException err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = "No data in selected Group :: " +err.Message;
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        protected void btnSelectGroup_Click(object sender, EventArgs e)
        {
            try
            {
                string theScript = "<script language='javascript' id='DrugPopup'>\n";
                theScript += "window.open('frmAdmin_FieldGroupSelector.aspx?','FieldGroup','toolbars=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
                theScript += "</script>\n";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "DrugPopup", theScript);
                #region "Display Controls"
                theScript = "";
                theScript = "<script language='javascript' defer = 'defer' id='ShowStatus'>\n";
                theScript += "</script>\n";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "ShowStatus", theScript);
                #endregion "Display Controls"
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["PatientId"] = 0;
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Visible = false;
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Export Data";
                if (Page.IsPostBack)
                {
                    if ((Session["SelectedData"].ToString() != "") && (Session["SelectedData"] != null))
                    {
                        BindFunctions BindManger = new BindFunctions();
                        BindManger.BindList(lstAvailable, (DataTable)Session["SelectedData"], "GroupName", "GroupId");
                    }
                }
                else
                {
                    Session.Remove("SelectedData"); // later
                    Session.Remove("AvailableData");// later
                }

                Page.EnableViewState = true;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private static void SortDataTable(DataTable dt, string sort)
        {
            DataTable newDT = dt.Clone();
            int rowCount = dt.Rows.Count;

            DataRow[] foundRows = dt.Select(null, sort);
            for (int i = 0; i < rowCount; i++)
            {
                object[] arr = new object[dt.Columns.Count];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    arr[j] = foundRows[i][j];
                }
                DataRow data_row = newDT.NewRow();
                data_row.ItemArray = arr;
                newDT.Rows.Add(data_row);
            }

            //clear the incoming dt
            dt.Rows.Clear();

            for (int i = 0; i < newDT.Rows.Count; i++)
            {
                object[] arr = new object[dt.Columns.Count];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    arr[j] = newDT.Rows[i][j];
                }

                DataRow data_row = dt.NewRow();
                data_row.ItemArray = arr;
                dt.Rows.Add(data_row);
            }
        }

        private void ExportToCSV(string theQuery)
        {
            try
            {
                DataView theDV = new DataView();
                theDV = ManipulateData(theQuery.ToString());

                DataTable theDT = new DataTable();
                theDT = theDV.ToTable();

                StringBuilder str = new StringBuilder();
                for (int i = 0; i < theDT.Columns.Count; i++)
                {
                    str.Append(theDT.Columns[i].ColumnName.ToString());
                    str.Append(",");
                }
                str.Append("\n");
                str.Append("\n");

                for (int i = 0; i <= theDT.Rows.Count - 1; i++)
                {
                    for (int j = 0; j <= theDT.Columns.Count - 1; j++)
                    {
                        str.Append(theDT.Rows[i][j].ToString());
                        str.Append(",");
                    }
                    str.Append("\n");
                }

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=Export.csv");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.csv";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                Response.Write(str.ToString());
                Response.End();
            }
            catch
            {
                IQCareMsgBox.Show("TooManyRec", this);
            }
        }

        private void ExportToExcel(string theQuery)
        {
            try
            {
                IExport ExportManager = (IExport)ObjectFactory.CreateInstance("BusinessProcess.Administration.BExport, BusinessProcess.Administration");
                DataTable dtToExport = ExportManager.RunQuery(theQuery);
                ExportManager = null;

                //dg1.DataSource = dvToExport2;
                dg1.DataSource = dtToExport;
                dg1.DataBind();
                dg1.HeaderStyle.Font.Bold = true;
                dg1.GridLines = GridLines.Vertical;

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=Export.xls");
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = "application/vnd.xls";
                System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                dg1.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
                //dg1.Visible = false;
                #region "EXPORT TEST - Column wise"
                /*
            //Excel.ApplicationClass theApp = new Excel.ApplicationClass();

            Excel._Application theApp = new Excel._Application();
            //Excel.Workbook theWorkBook = theApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Workbook theWorkBook = theApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            //Excel.Worksheet theSheet = (Excel.Worksheet)theWorkBook.Worksheets[1];
            Excel.Worksheet theSheet = (Excel.Worksheet)theWorkBook.Worksheets[1];

            /////////////////// Include Header ////////////////////

            Excel.Range theRange = theSheet.get_Range(GetIndexLetter(0) + "1", GetIndexLetter(dg1.Columns.Count) + "1");
            for (int i = 0; i < dg1.Columns.Count; i++)
            {
                ((Excel.Range)theRange["1", GetIndexLetter(i)]).Value2 = dg1.Columns[i].HeaderText.ToString();
                ((Excel.Range)theRange["1", GetIndexLetter(i)]).ColumnWidth = 20;
            }

            // theSheet.SaveAs("c:\\Issues.xls", ".xls", "", "", false, false, false, "", "", true);

            theApp.Workbooks[1].Close(true, "c:\\Issues.xls", null);
            //theApp.Save("c:\\issue.xls");
            theSheet = null;
            theApp = null;
            theWorkBook = null;
            */
                #endregion "EXPORT TEST - Column wise"
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                //IQCareMsgBox.Show("TooManyRec",this);
            }
        }

        private StringBuilder MakeCondition(DataTable dtViewName)
        {
            try
            {
                //------make WHERE condition on basis of ptnpk,locid,visit id
                //----for ptnpk
                StringBuilder theCondP = new StringBuilder(); //for ptnpk
                for (int i = 0; i < dtViewName.Rows.Count - 1; i++)
                {
                    if ((dtViewName.Rows[i]["Ptn_Pk"].ToString().Trim() == "T"))
                    {
                        for (int j = i + 1; j < dtViewName.Rows.Count; j++)
                        {
                            if (dtViewName.Rows[j]["Ptn_Pk"].ToString().Trim() == "T")
                            {
                                //added
                                if ((ViewState["DemographicsFound"].ToString() == "F") || ((ViewState["DemographicsFound"].ToString() == "T") &&
                                    ((dtViewName.Rows[i]["ViewName"].ToString().Trim() == "Rpt_PatientDemographics") ||
                                    (dtViewName.Rows[j]["ViewName"].ToString().Trim() == "Rpt_PatientDemographics"))))
                                {
                                    //first time without and - rest of time with AND
                                    if (theCondP.ToString() == "")
                                    {
                                        theCondP.Append(dtViewName.Rows[i]["ViewName"].ToString().Trim() + ".[Ptn_Pk]");
                                        theCondP.Append("=" + dtViewName.Rows[j]["ViewName"].ToString().Trim() + ".[Ptn_Pk]");
                                    }
                                    else
                                    {
                                        theCondP.Append(" AND " + dtViewName.Rows[i]["ViewName"].ToString().Trim() + ".[Ptn_Pk]");
                                        theCondP.Append("=" + dtViewName.Rows[j]["ViewName"].ToString().Trim() + ".[Ptn_Pk]");
                                    }
                                }//added
                            }
                        }
                    }
                }

                //----for VisitID
                StringBuilder theCondV = new StringBuilder(); //for VisitID
                for (int i = 0; i < dtViewName.Rows.Count - 1; i++)
                {
                    if ((dtViewName.Rows[i]["Visit_Id"].ToString().Trim() == "T"))
                    {
                        for (int j = (i + 1); j < dtViewName.Rows.Count; j++)
                        {
                            if (dtViewName.Rows[j]["Visit_Id"].ToString().Trim() == "T")
                            {
                                //added
                                if ((ViewState["VisitFound"].ToString() == "F") || ((ViewState["VisitFound"].ToString() == "T") &&
                                    ((dtViewName.Rows[i]["ViewName"].ToString().Trim() == "Rpt_Visit") ||
                                    (dtViewName.Rows[j]["ViewName"].ToString().Trim() == "Rpt_Visit"))))
                                {
                                    //first time without and - rest of time with AND
                                    if (theCondV.ToString() == "")
                                    {
                                        theCondV.Append(dtViewName.Rows[i]["ViewName"].ToString().Trim() + ".[Visit_Id]");
                                        theCondV.Append("=" + dtViewName.Rows[j]["ViewName"].ToString().Trim() + ".[Visit_Id]");
                                    }
                                    else
                                    {
                                        theCondV.Append(" AND " + dtViewName.Rows[i]["ViewName"].ToString().Trim() + ".[Visit_Id]");
                                        theCondV.Append("=" + dtViewName.Rows[j]["ViewName"].ToString().Trim() + ".[Visit_Id]");
                                    }
                                }
                            }
                        }
                    }
                }

                StringBuilder theCond = new StringBuilder();
                if (theCondP.ToString() != "")
                    theCond.Append(theCondP.ToString());
                if ((theCond.ToString() != "") & theCondV.ToString() != "")
                {
                    theCond.Append(" AND " + theCondV.ToString());
                }
                else if ((theCondV.ToString() != ""))
                {
                    theCond.Append(theCondV.ToString());
                }

                //--ends

                if (theCond.ToString() != "")
                    theCond.Insert(0, " WHERE ");
                return theCond;
            }
            catch (Exception err)
            {
                StringBuilder Err = new StringBuilder().Append("Error in MakeCondition");
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return Err;
            }
        }

        private DataTable MakeExportID(DataTable dtViewName)
        {
            //--- make query using all viewname , execute the query to get distinct ptn_pk and store them in table dtViewPtnPk
            IExport ExportManager = (IExport)ObjectFactory.CreateInstance("BusinessProcess.Administration.BExport, BusinessProcess.Administration");

            string theQuery = "";
            int r = 0;

            foreach (DataRow theDRV in dtViewName.Rows)
            {
                if (theDRV["Ptn_Pk"].ToString() == "T")
                {
                    if (r == 0)
                        theQuery = "SELECT DISTINCT " + theDRV["ViewName"] + ".Ptn_Pk" + " FROM " + theDRV["ViewName"];
                    else
                        if (theQuery.ToString() != "")
                            theQuery = theQuery + " UNION " + " SELECT DISTINCT " + theDRV["ViewName"] + ".Ptn_Pk" + " FROM " + theDRV["ViewName"];
                        else
                            theQuery = " SELECT DISTINCT " + theDRV["ViewName"] + ".Ptn_Pk" + " FROM " + theDRV["ViewName"];
                }
                r = 1;
            }
            theQuery = theQuery + " ORDER BY Ptn_Pk";

            DataTable dtViewPtnPk = new DataTable();
            dtViewPtnPk = ExportManager.RunQuery(theQuery);

            string Ptn_Pk_List = "";
            foreach (DataRow theDR in dtViewPtnPk.Rows)
            {
                if (Ptn_Pk_List == "")
                    Ptn_Pk_List = Ptn_Pk_List + theDR["Ptn_Pk"].ToString();
                else
                    Ptn_Pk_List = Ptn_Pk_List + "," + theDR["Ptn_Pk"].ToString();
            }
            dtViewPtnPk = null;

            DataTable dtPtnPkExport = new DataTable();
            if (Ptn_Pk_List != "")
                dtPtnPkExport = ExportManager.MakeExportID(Ptn_Pk_List);
            return dtPtnPkExport;
        }

        private StringBuilder MakeFrom(DataTable dtViewName)
        {
            try
            {
                StringBuilder theFrom = new StringBuilder();
                Boolean blnOIS = false, blnSymp = false;
                foreach (DataRow theDR in dtViewName.Rows)
                {
                    if (theDR["ViewName"].ToString().Trim() == "Rpt_OISorAIDSDefingIllnesses")
                        blnOIS = true;
                    if (theDR["ViewName"].ToString().Trim() == "Rpt_PatientSymptoms")
                        blnSymp = true;
                }

                if ((blnOIS == true) && (blnSymp == true))
                {
                    theFrom.Append("[Rpt_OISorAIDSDefingIllnesses] FULL OUTER JOIN [Rpt_PatientSymptoms] on ");
                    theFrom.Append("[Rpt_OISorAIDSDefingIllnesses].[visit_id]=[Rpt_PatientSymptoms].[visit_Id]");
                    foreach (DataRow theDR in dtViewName.Rows)
                    {
                        if ((theDR["ViewName"].ToString() != "Rpt_OISorAIDSDefingIllnesses") &&
                            (theDR["ViewName"].ToString() != "Rpt_PatientSymptoms"))
                        {
                            theFrom.Append(", " + "[" + theDR["ViewName"].ToString().Trim() + "]");
                        }
                    }
                }
                else
                {
                    foreach (DataRow theDR in dtViewName.Rows)
                    {
                        if (theFrom.ToString() == "")
                            theFrom.Append("[" + theDR["ViewName"].ToString().Trim() + "]");
                        else
                            theFrom.Append(", " + "[" + theDR["ViewName"].ToString().Trim() + "]");
                    }
                }
                theFrom.Insert(0, " FROM ");

                //---ends-----

                return theFrom;
            }
            catch (Exception err)
            {
                StringBuilder Err = new StringBuilder().Append("Error in MakeFrom");
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return Err;
            }
        }

        private StringBuilder MakeSelect(DataTable dtColName)
        {
            try
            {
                StringBuilder theSelect = new StringBuilder();
                Boolean Found_PtnPk = false;
                Boolean Found_LocID = false;
                Boolean Found_VisitId = false;

                string fldPE = ""; // PatientEnrollment#

                foreach (DataRow theDR in dtColName.Rows)
                {
                    //--- for identiy
                    if ((ChkIdentity.Checked == true) &&
                        ((theDR["ColName"].ToString().Trim() == "Patient First Name") ||
                        (theDR["ColName"].ToString().Trim() == "Patient Last Name") ||
                        (theDR["ColName"].ToString().Trim() == "Patient Name") ||
                        (theDR["ColName"].ToString().Trim() == "Local Council") ||
                        (theDR["ColName"].ToString().Trim() == "Patient Phone") ||
                        (theDR["ColName"].ToString().Trim() == "Patient Address") ||
                        (theDR["ColName"].ToString().Trim() == "Province") ||
                        (theDR["ColName"].ToString().Trim() == "Village") ||
                        (theDR["ColName"].ToString().Trim() == "Guardian Name") ||
                        (theDR["ColName"].ToString().Trim() == "Emergency Contact Name") ||
                        (theDR["ColName"].ToString().Trim() == "Emergency Contact Phone") ||
                        (theDR["ColName"].ToString().Trim() == "Emergency Contact Address") ||
                        (theDR["ColName"].ToString().Trim() == "hvPatientCaretaker")))
                    {
                        //do nothing
                    }
                    else
                    {
                        if ((theDR["ColName"].ToString().Trim().ToUpper() != "PTN_PK") && (theDR["ColName"].ToString().Trim().ToUpper() != "LOCATIONID") && (theDR["ColName"].ToString().Trim().ToUpper() != "VISTID"))
                        {
                            //---if not date type
                            if (theDR["ColName"].ToString().Trim().ToUpper().IndexOf("DATE") == -1)
                            {
                                if ((theDR["ColName"].ToString().Trim().ToUpper() != "PATIENT ENROLLMENT #")) //--230707
                                {
                                    if (theSelect.ToString() == "")//if first field
                                        theSelect.Append(theDR["ViewName"].ToString().Trim() + "." + "[" + theDR["ColName"].ToString().Trim() + "]");
                                    else //if not first field
                                        theSelect.Append(", " + theDR["ViewName"].ToString().Trim() + "." + "[" + theDR["ColName"].ToString().Trim() + "]");
                                }//--230707
                                else
                                {
                                    fldPE = theDR["ViewName"].ToString().Trim() + "." + "[" + theDR["ColName"].ToString().Trim() + "]"; //--230707
                                }
                            }
                            else
                            {
                                //---if date type -- convert(varchar,Rpt_PatientDemographics.[Date of Birth],106) [Date of Birth]
                                if (theSelect.ToString() == "")//if first field
                                    theSelect.Append("Convert(varchar," + theDR["ViewName"].ToString().Trim() + "." + "[" + theDR["ColName"].ToString().Trim() + "],106)" + "[" + theDR["ColName"].ToString().Trim() + "]");
                                else //if not first field
                                    theSelect.Append(", " + "Convert(varchar," + theDR["ViewName"].ToString().Trim() + "." + "[" + theDR["ColName"].ToString().Trim() + "],106)" + "[" + theDR["ColName"].ToString().Trim() + "]");
                            }
                        }
                        else if ((theDR["ColName"].ToString().Trim().ToUpper() == "PTN_PK") && Found_PtnPk == false) //---for ptnpk
                        {
                            if (theSelect.ToString() == "") //if first field
                                theSelect.Append(theDR["ViewName"].ToString().Trim() + "." + "[" + theDR["ColName"].ToString().Trim() + "]");
                            else //if not first field
                                theSelect.Append(", " + theDR["ViewName"].ToString().Trim() + "." + "[" + theDR["ColName"].ToString().Trim() + "]");

                            Found_PtnPk = true;
                            OrderBy = theDR["ViewName"].ToString().Trim() + "." + "[" + theDR["ColName"].ToString().Trim() + "]";
                        }
                        else if ((theDR["ColName"].ToString().Trim() == "LocationID") && Found_LocID == false) //---for LocationID
                        {
                            if (theSelect.ToString() == "") //if first field
                                theSelect.Append(theDR["ViewName"].ToString().Trim() + "." + "[" + theDR["ColName"].ToString().Trim() + "]");
                            else //if not first field
                                theSelect.Append(", " + theDR["ViewName"].ToString().Trim() + "." + "[" + theDR["ColName"].ToString().Trim() + "]");

                            Found_LocID = true;
                        }
                        else if ((theDR["ColName"].ToString().Trim() == "Visit_Id") && Found_VisitId == false) //---for VisitID
                        {
                            if (theSelect.ToString() == "") //if first field
                                theSelect.Append(theDR["ViewName"].ToString().Trim() + "." + "[" + theDR["ColName"].ToString().Trim() + "]");
                            else //if not first field
                                theSelect.Append(", " + theDR["ViewName"].ToString().Trim() + "." + "[" + theDR["ColName"].ToString().Trim() + "]");

                            Found_VisitId = true;
                        }
                    }
                }

                if (fldPE != "")
                    theSelect.Insert(0, fldPE + ", ");

                theSelect.Insert(0, "SELECT 'E' as ExportId , ");

                //----- ends --------
                return theSelect;
            }
            catch (Exception err)
            {
                StringBuilder Err = new StringBuilder().Append("Error in MakeSelect");
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
                return Err;
            }
        }

        private DataView ManipulateData(string theQuery)
        {
            #region"RUN QUERY AND GET ALL DATA"
            IExport ExportManager = (IExport)ObjectFactory.CreateInstance("BusinessProcess.Administration.BExport, BusinessProcess.Administration");
            DataTable dtToExport = ExportManager.RunQuery(theQuery);
            #endregion

            #region "GET UNIQUE EXPORTID AND STORE IN A TABLE - call MakeExportID"
            DataTable dtExportId = new DataTable();//--to store ptnpk , export it
            dtExportId = MakeExportID(dtViewName);

            DataView theDV = new DataView(dtExportId);
            foreach (DataRow theDR1 in dtToExport.Rows)
            {
                theDV.RowFilter = "Ptn_Pk=" + theDR1["Ptn_Pk"];
                theDR1["ExportId"] = theDV[0][1]; //insert exportid in dtToExport
            }
            dtExportId = null;
            #endregion

            #region " REMOVE ALL ID COLUMNS "
            string[] IdColNames = new string[50];
            int j = 0;
            foreach (DataColumn theCol in dtToExport.Columns)
            {
                if ((theCol.ColumnName.ToString().Trim().ToUpper() != "EXPORTID") &&
                    (theCol.ColumnName.ToString().Trim().ToUpper() != "APPOINTMENT PROVIDER") &&
                    (theCol.ColumnName.ToString().Trim().ToUpper() != "AIDSRELIEF FINANCED LAB") &&
                    (theCol.ColumnName.ToString().Trim().ToUpper() != "AIDSRELIEF FINANCED DRUG"))
                {
                    if ((theCol.ColumnName.ToString().ToUpper().IndexOf("ID") >= 0) ||
                        (theCol.ColumnName.ToString().ToUpper().IndexOf("PTN_PK") >= 0))
                    {
                        IdColNames[j] = theCol.ColumnName;
                        j++;
                    }
                }
            }

            for (int k = 0; k < j; k++)
            {
                if (IdColNames[k].ToString().Trim() == null)
                    break;
                foreach (DataColumn theCol in dtToExport.Columns)
                {
                    if (theCol.ColumnName.ToString().Trim() == IdColNames[k].ToString().Trim())
                    {
                        dtToExport.Columns.Remove(theCol);
                        break;
                    }
                }
            }
            #endregion

            #region "MONTHLY INCOME"
            //IQCareUtils theUtils = new IQCareUtils();
            //int r = 0;

            //foreach (DataRow theDR in dtToExport.Rows)
            //{
            //    int c = 0;
            //    foreach (DataColumn theCol in dtToExport.Columns)
            //    {
            //        if (theCol.ColumnName.ToString().Trim() == "Monthly Income")
            //        {
            //            if (dtToExport.Rows[r][c] == null)
            //            {
            //                dtToExport.Rows[r][c] = System.DBNull.Value;
            //            }
            //            else if (dtToExport.Rows[r][c].ToString() == "")
            //            {
            //                dtToExport.Rows[r][c] = System.DBNull.Value;
            //            }
            //            else if (Convert.ToInt32(dtToExport.Rows[r][c]) == 0)
            //            {
            //                dtToExport.Rows[r][c] = System.DBNull.Value;
            //            }
            //        }
            //        c++;
            //    }
            //    r++;
            //}
            #endregion

            #region "CHANGE COLUMN NAME FROM EXPORTID TO PATIENTID"
            foreach (DataColumn theDC in dtToExport.Columns)
            {
                if (theDC.ColumnName.ToString() == "ExportId")
                {
                    theDC.ColumnName = "PatientId";
                    break;
                }
            }
            #endregion

            #region"REMOVE BLANK ROWS-1  & SORT"
            dtToExport = RemoveBlankRow(dtToExport);

            SortDataTable(dtToExport, "PatientId Asc");
            #endregion

            #region "MERGE DATA"
            //DataTable dtToExport2 = new DataTable();
            //foreach (DataColumn theDC in dtToExport.Columns)
            //{
            //    dtToExport2.Columns.Add(theDC.ColumnName.ToString().Trim(), System.Type.GetType("System.String"));
            //}
            ////--insert unique values

            //int r = 0;
            //foreach (DataRow theDR in dtToExport.Rows)
            //{
            //    int c = 0;
            //    foreach (DataColumn theDC in dtToExport.Columns)
            //    {
            //        if (c == 0) //insert new row else update
            //        {
            //            DataRow theDR2 = dtToExport2.NewRow();
            //            theDR2[c] = "";
            //            dtToExport2.Rows.Add(theDR2);
            //        }
            //        if (r == 0)
            //        {
            //            dtToExport2.Rows[r][c] = theDR[c].ToString().Trim();
            //        }
            //        else
            //        {
            //            if (dtToExport.Rows[r][0].ToString().Trim() == dtToExport.Rows[r - 1][0].ToString().Trim())
            //            {
            //                if (dtToExport.Rows[r][c].ToString().Trim() != dtToExport.Rows[r - 1][c].ToString().Trim())
            //                    dtToExport2.Rows[r][c] = theDR[c].ToString().Trim();
            //            }
            //            else
            //            {
            //                if (dtToExport.Rows[r][c].ToString().Trim().Length != 0)
            //                    dtToExport2.Rows[r][c] = theDR[c].ToString().Trim();
            //            }
            //        }
            //        c++;
            //    }
            //    r++;
            //}
            //dtToExport = null;
            #endregion

            #region "REMOVE BALNK ROW-2"

            DataView dvToExport2 = new DataView();
            dvToExport2 = RemoveBlankRow2(dtToExport);
            return dvToExport2;
            #endregion
        }

        private DataTable RemoveBlankRow(DataTable dtToExport2)
        {
            //---removing all the blank rows & and all rows which have data only in id column
            int c, r;
            Boolean blnkRow = true;
            dtToExport2.Columns.Add("DeleteFlag1", System.Type.GetType("System.String"));
            r = 0;
            foreach (DataRow theDR2 in dtToExport2.Rows)
            {
                c = 0;
                foreach (DataColumn theDC2 in dtToExport2.Columns)
                {
                    if ((theDR2[c].ToString().Length > 0) && (c != 0))
                    {
                        blnkRow = false;
                        break;
                    }
                    c++;
                }
                if (blnkRow == true)
                {
                    theDR2["DeleteFlag1"] = "D";
                }
                blnkRow = true;
                r++;
            }

            for (int i = 0; i < dtToExport2.Rows.Count; i++)
            {
                if (dtToExport2.Rows[i]["DeleteFlag1"].ToString() == "D")
                {
                    dtToExport2.Rows[i].Delete();
                    dtToExport2.AcceptChanges();
                    i--;
                }
            }
            dtToExport2.Columns.Remove("DeleteFlag1");
            return dtToExport2;
        }

        private DataView RemoveBlankRow2(DataTable dtToExport2)
        {
            //---removing all the blank rows & and all rows which have data only in id column
            int c, r;
            Boolean blnkRow = true;
            dtToExport2.Columns.Add("DeleteFlag1", System.Type.GetType("System.String"));
            r = 0;
            foreach (DataRow theDR2 in dtToExport2.Rows)
            {
                c = 0;
                foreach (DataColumn theDC2 in dtToExport2.Columns)
                {
                    if ((theDR2[c].ToString().Length > 0) && (c != 0))
                    {
                        blnkRow = false;
                        theDR2["DeleteFlag1"] = "ND";
                        break;
                    }
                    c++;
                }
                if (blnkRow == true)
                {
                    theDR2["DeleteFlag1"] = "D";
                }
                blnkRow = true;
                r++;
            }

            DataView theDV = new DataView(dtToExport2);
            theDV.RowFilter = "DeleteFlag1='ND'";
            dtToExport2.Columns.Remove("DeleteFlag1");
            dtToExport2 = null;
            //dtToExport2.Dispose();
            return theDV;
        }
    }
}