using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Application.Common;
using Application.Presentation;
using Interface.Clinical;
using Interface.Laboratory;
using Interface.Security;
using System.Linq;

namespace IQCare.Web.Laboratory
{
    /// <summary>
    /// 
    /// </summary>
    public partial class XRayOrderForm : System.Web.UI.Page
    {
        DataTable RequestedTests
        {
            get
            {
                if (base.Session["LAB_REQ"] == null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("TestId", System.Type.GetType("System.Int32"));
                    dt.Columns.Add("TestName", System.Type.GetType("System.String"));
                    dt.Columns.Add("TestNotes", System.Type.GetType("System.String"));
                    dt.Columns.Add("Department", System.Type.GetType("System.String"));
                    DataColumn deleteCol = new DataColumn("DeleteFlag", typeof(bool));
                    deleteCol.DefaultValue = false;
                    dt.Columns.Add(deleteCol);

                    DataColumn persistCol = new DataColumn("Persisted", typeof(bool));
                    persistCol.DefaultValue = false;
                    dt.Columns.Add(persistCol);
                    return dt;
                }
                else
                {
                    return (DataTable)base.Session["LAB_REQ"];
                }
            }
            set
            {
                base.Session["LAB_REQ"] = value;
            }
        }


        /// The dv
        /// </summary>
        DataView theDV = new DataView();
        /// <summary>
        /// The d ttest date
        /// </summary>
        DataTable DTtestDate = new DataTable();


        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {

            //  Ajax.Utility.RegisterTypeForAjax(typeof(Laboratory_LabOrderForm));
            txtlaborderedbydate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtlaborderedbydate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");

            txtLabtobeDone.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtLabtobeDone.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");

            DataTable dtPatientInfo = (DataTable)Session["PatientInformation"];
            if (dtPatientInfo != null && dtPatientInfo.Rows.Count > 0)
            {
                if (Session["SystemId"].ToString() == "1")
                    lblpatientname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["FirstName"].ToString();
                else
                    lblpatientname.Text = dtPatientInfo.Rows[0]["LastName"].ToString() + ", " + dtPatientInfo.Rows[0]["MiddleName"].ToString() + " , " + dtPatientInfo.Rows[0]["FirstName"].ToString();
                lblIQnumber.Text = dtPatientInfo.Rows[0]["IQNumber"].ToString();



            }
            TechnicalAreaIdentifier();

            AuthenticationManager Authentication = new AuthenticationManager();
            if (Session["LabId"] != null)
            {
                Session["Lab_ID"] = Session["LabId"];
                ViewState["LabTestID"] = Session["LabTestID"];
                Session["LabId"] = null;
                Session["LabTestID"] = null;
            }
            if (Application["AppCurrentDate"] != null)
            {
                hdappcurrentdate.Value = Application["AppCurrentDate"].ToString();
            }
            if (!IsPostBack)
            {
                ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                DataSet theDSPatient = LabResultManager.GetPatientInfo(Session["PatientId"].ToString());
                if (theDSPatient.Tables[1].Rows.Count > 0)
                {

                    ViewState["IEVisitDate"] = theDSPatient.Tables[1].Rows[0]["VisitDate"];

                }
                //  dtLabTest = new DataTable();
                // dtLabTest.Rows.Clear();
                hdControlExists.Value = "";
                base.Session["LAB_REQ"] = null;
                GetLaborderdate();
                BindControls();
                this.BindLabTests();
            }
            {
                this.CatchPostBack();
            }

        }

        void BindLabTests()
        {

            DataView dv = this.RequestedTests.DefaultView;
            dv.RowFilter = "DeleteFlag  ='False'";
            DataTable theDT = dv.ToTable();

            repeaterLabTest.DataSource = this.RequestedTests;
            repeaterLabTest.DataBind();

        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            this.BindLabTests();
            buttonSave.Visible = repeaterLabTest.Items.Count > 0;
            // panelReporter.Visible = !this.Paperless;
        }
        /// <summary>
        /// Gets the laborderdate.
        /// </summary>
        private void GetLaborderdate()
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            int LocationID = Convert.ToInt32(Session["AppLocationId"]);
            ILabFunctions LabTestsMgrDate;
            if (Session["Lab_ID"] != null)
            {
                LabTestsMgrDate = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                DTtestDate = (DataTable)LabTestsMgrDate.GetLaborderdate(PatientID, LocationID, Convert.ToInt32(Session["Lab_ID"]));
                ViewState["TestDate"] = DTtestDate;
            }
        }
        /// <summary>
        /// Technicals the area identifier.
        /// </summary>
        private void TechnicalAreaIdentifier()
        {
            int intmoduleID = Convert.ToInt32(Session["TechnicalAreaId"]);
            IPatientHome PatientHome = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataSet DSTab = PatientHome.GetTechnicalAreaIdentifierFuture(intmoduleID, Convert.ToInt32(Session["PatientId"]));

            if (DSTab.Tables[0].Rows.Count > 0)
            {
                if (DSTab.Tables[0].Rows.Count > 0)
                {


                    //thePnlIdent.Controls.Add(new LiteralControl("<td class='bold pad18' style='width: 25%'>"));
                    Label theLabelIdentifier1 = new Label();
                    theLabelIdentifier1.ID = "Lbl_" + DSTab.Tables[0].Rows[0][0].ToString();
                    int i = 0;
                    foreach (DataRow DRLabel in DSTab.Tables[0].Rows)
                    {
                        foreach (DataRow DRLabel1 in DSTab.Tables[1].Rows)
                        {
                            theLabelIdentifier1.Text = theLabelIdentifier1.Text + "    " + DRLabel[0].ToString() + " : " + DRLabel1[i].ToString();
                        }
                        i++;
                    }


                    thePnlIdent.Controls.Add(theLabelIdentifier1);


                }


            }


        }

        /// <summary>
        /// Binds the controls.
        /// </summary>
        private void BindControls()
        {
            BindFunctions theBindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDSXML = new DataSet();
            DataTable theDT = new DataTable();
            theDSXML.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));
            if (theDSXML.Tables["Mst_Employee"] != null)
            {
                theDV = new DataView(theDSXML.Tables["Mst_Employee"]);
                theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                    {
                        theDV = new DataView(theDT);
                        theDV.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
                        if (theDV.Count > 0)
                            theDT = theUtils.CreateTableFromDataView(theDV);
                    }

                    theBindManager.BindCombo(ddlaborderedbyname, theDT, "EmployeeName", "EmployeeId");
                    theDV.Dispose();
                    theDT.Clear();
                }

            }
            if (Convert.ToInt32(Session["Lab_ID"]) > 0)
            {

                BindDropdownOrderBy(DTtestDate.Rows[0]["OrderedbyName"].ToString());
                ddlaborderedbyname.SelectedValue = DTtestDate.Rows[0]["OrderedbyName"].ToString();
                txtlaborderedbydate.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(DTtestDate.Rows[0]["OrderedbyDate"]));
                if (DTtestDate.Rows[0].IsNull("PreClinicLabDate") == false)
                    if (((DateTime)DTtestDate.Rows[0]["PreClinicLabDate"]).ToString(Session["AppDateFormat"].ToString()) != "01-Jan-1900")
                        this.txtLabtobeDone.Text = (((DateTime)DTtestDate.Rows[0]["PreClinicLabDate"]).ToString(Session["AppDateFormat"].ToString())).ToString();

                if (this.txtLabtobeDone.Text != "")
                {
                    this.preclinicLabs.Checked = true;
                }


            }


        }
        /// <summary>
        /// Binds the dropdown order by.
        /// </summary>
        /// <param name="EmployeeId">The employee identifier.</param>
        private void BindDropdownOrderBy(String EmployeeId)
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            if (theDS.Tables["Mst_Employee"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                    {
                        theDV = new DataView(theDT);
                        theDV.RowFilter = "EmployeeId IN(" + Session["AppUserEmployeeId"].ToString() + "," + EmployeeId + ")";
                        if (theDV.Count > 0)
                            theDT = theUtils.CreateTableFromDataView(theDV);
                    }
                    BindManager.BindCombo(ddlaborderedbyname, theDT, "EmployeeName", "EmployeeId");

                }
            }

        }
        /// <summary>
        /// Finds the control recursive.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Control FindControlRecursive(Control container, string name)
        {
            if (container.ID == name)
                return container;

            foreach (Control ctrl in container.Controls)
            {
                //string id = ctrl.ClientID;
                Control foundCtrl = FindControlRecursive(ctrl, name);
                if (foundCtrl != null)
                    return foundCtrl;
            }
            return null;
        }
        #region "AutoComplete Method"


        /// <summary>
        /// Gets the lab.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static List<string> GetLab(string prefixText, int count)
        {
            List<Labs> items = new List<Labs>();
            ILabFunctions LabTestsMgrDate;
            List<string> ar = new List<string>();
            string custItem = string.Empty;

            LabTestsMgrDate = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            string query = "Exec dbo.Laboratory_GetLabTestID @LabName='" + prefixText + "', @IncludeLabDepartment=8";
            //filling data from database
            // dtLabResult = LabTestsMgrDate.ReturnLabQuery(sqlQuery);
            DataTable dt = new DataTable();
            dt = LabTestsMgrDate.ReturnLabQuery(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {

                        custItem = AutoCompleteExtender.CreateAutoCompleteItem(row["LabName"].ToString(), String.Format("{0};{1};{2}", row["LabTestID"], row["LabName"], row["labdepartmentname"]));
                        ar.Add(custItem);
                    }
                    catch
                    {

                    }
                }

            }

            return ar;
            //return items;
        }


        /// <summary>
        /// Searchlabs the specified prefix text.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        [System.Web.Script.Services.ScriptMethod()]
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> Searchlab(string prefixText, int count)
        {
            List<string> Labdetail = new List<string>();
            ILabFunctions LabTestsMgrDate;
            LabTestsMgrDate = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            //List<string> lstDrugsDetail = GetLab(prefixText, count);
            DataTable dt = new DataTable();
            dt = LabTestsMgrDate.FindLabByName(prefixText, 8, null);
            List<string> ar = new List<string>();
            string custItem = string.Empty;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {

                        custItem = AutoCompleteExtender.CreateAutoCompleteItem(
                            row["LabName"].ToString(), 
                            String.Format("{0};{1};{2};{3}", 
                                row["LabTestID"], 
                                row["LabName"], 
                                row["labdepartmentname"], 
                                row["DataType"]
                            )
                            );
                        ar.Add(custItem);
                    }
                    catch
                    {

                    }
                }

            }

            return ar;

        }



        #region "Class for Lab"
        /// <summary>
        /// 
        /// </summary>
        public class Labs
        {
            /// <summary>
            /// The _ sub test identifier
            /// </summary>
            protected int _SubTestId;
            /// <summary>
            /// Gets or sets the sub test identifier.
            /// </summary>
            /// <value>
            /// The sub test identifier.
            /// </value>
            public int SubTestId
            {
                get { return _SubTestId; }
                set { _SubTestId = value; }
            }

            /// <summary>
            /// The _ lab department identifier
            /// </summary>
            protected int _LabDepartmentId;
            /// <summary>
            /// Gets or sets the lab department identifier.
            /// </summary>
            /// <value>
            /// The lab department identifier.
            /// </value>
            public int LabDepartmentId
            {
                get { return _LabDepartmentId; }
                set { _LabDepartmentId = value; }
            }

            /// <summary>
            /// The _ lab department name
            /// </summary>
            protected string _LabDepartmentName;
            /// <summary>
            /// Gets or sets the name of the lab department.
            /// </summary>
            /// <value>
            /// The name of the lab department.
            /// </value>
            public string LabDepartmentName
            {
                get { return _LabDepartmentName; }
                set { _LabDepartmentName = value; }
            }
            /// <summary>
            /// The _ sub test name
            /// </summary>
            protected string _SubTestName;
            /// <summary>
            /// Gets or sets the name of the sub test.
            /// </summary>
            /// <value>
            /// The name of the sub test.
            /// </value>
            public string SubTestName
            {
                get { return _SubTestName; }
                set { _SubTestName = value; }
            }
            Boolean _requireNotes = false;
            /// <summary>
            /// Gets or sets a value indicating whether [require notes].
            /// </summary>
            /// <value>
            ///   <c>true</c> if [require notes]; otherwise, <c>false</c>.
            /// </value>
            public Boolean RequireNotes { get { return _requireNotes; } set { _requireNotes = value; } }

        }
        #endregion



        #endregion
        /// <summary>
        /// Handles the TextChanged event of the txtautoTestName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void txtautoTestName_TextChanged(object sender, EventArgs e)
        {

            txtautoTestName.Text = "";

            DataTable dt = this.RequestedTests;

            int _testID;
            string _testname = "";
            string _testDepartment = "";
            string _datatype = "";
            if (hdCustID.Value != "")
            {
                String[] itemCodes = hdCustID.Value.Split(';');
                if (itemCodes.Length == 4)
                {
                    _testID = Convert.ToInt32(itemCodes[0]);
                    _testname = itemCodes[1].ToString();
                    _testDepartment = itemCodes[2].ToString();
                    _datatype = itemCodes[3].ToString();

                    DataRow thisRow = null;
                    if (dt.Rows.Count > 0)
                    {
                        thisRow = dt.AsEnumerable()
                           .DefaultIfEmpty(null)
                           .FirstOrDefault(r => r["TestID"].ToString() == _testID.ToString());
                    }
                    if (thisRow != null)
                    {
                        ((TextBox)sender).Text = "";
                        hdCustID.Value = "";
                        return;
                    }
                    thisRow = dt.NewRow();
                    thisRow["TestId"] = _testID;
                    thisRow["TestName"] = _testname;
                    thisRow["Department"] = _testDepartment;

                    dt.Rows.Add(thisRow);
                    dt.AcceptChanges();
                    this.RequestedTests = dt;

                }

                // LoadLabResult(dt);

            }


        }
        /// <summary>
        /// Loads the lab result.
        /// </summary>
        /// <param name="dtresult">The dtresult.</param>
        public void LoadLabResult(DataTable dtresult)
        {

            if (dtresult.Rows.Count > 0)
            {
                pnllabtest.Visible = true;
            }
            string count = string.Empty;
            Hashtable ht = new Hashtable();
            Panel thelblPnl = new Panel();
            thelblPnl.ID = "pnlheading" + pnlselectlab.ID;
            thelblPnl.Height = 20;
            thelblPnl.Width = 100;
            thelblPnl.Controls.Clear();

            Label theLabel = new Label();
            theLabel.ID = "lblheading" + pnlselectlab.ID;
            theLabel.Text = "Lab Test";
            theLabel.Font.Bold = true;
            thelblPnl.Controls.Add(theLabel);
            pnlselectlab.Controls.Add(thelblPnl);

            Label lblStSp = new Label();
            lblStSp.Width = 5;
            lblStSp.Text = "";
            lblStSp.Height = 20;
            pnlselectlab.Controls.Add(lblStSp);

            foreach (DataRow r in dtresult.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;

                if (!(ht.ContainsKey("lblheading" + r["Department"].ToString())))
                {
                    var rowcount = dtresult.Select("Department = '" + r["Department"].ToString() + "'");

                    count = rowcount.Length.ToString();
                    Panel thepnl = new Panel();
                    thepnl.ID = "pnlsubheading" + r["Department"].ToString();
                    thepnl.Height = 40 * rowcount.Length;
                    thepnl.Width = 800;

                    Literal deptLit = new Literal();

                    deptLit.Text = String.Format("<h3 class=\"forms\" align=\"left\">{0}</h3>", r["Department"].ToString());

                    //Label thesubheading = new Label();
                    //thesubheading.ID = "lblheading" + r["Department"].ToString();
                    //thesubheading.Text = r["Department"].ToString();
                    //thesubheading.Font.Bold = true;
                    //thesubheading.ForeColor = System.Drawing.Color.Blue;
                    // thepnl.Controls.Add(thesubheading);
                    thepnl.Controls.Add(deptLit);
                    pnlselectlab.Controls.Add(thepnl);

                    Table testTable = new Table();
                    testTable.CellSpacing = 6;
                    testTable.CellPadding = 0;
                    testTable.Width = Unit.Percentage(100);
                    testTable.BorderWidth = 0;
                    testTable.ID = "table_" + thepnl.ID;

                    TableRow placeHolderRow = new TableRow();
                    // testRow.CssClass = "border center formbg";
                    placeHolderRow.CssClass = "border leftallign1 whitebg";
                    TableCell testName = new TableCell();
                    testName.Text = "Test Name";
                    testName.Font.Bold = true;
                    // testName.Font.Size = 12;            
                    placeHolderRow.Cells.Add(testName);
                    TableCell testNotes = new TableCell();
                    testNotes.Text = "Description";
                    testNotes.Font.Bold = true;
                    // testName.Font.Size = 12;            
                    placeHolderRow.Cells.Add(testNotes);

                    TableCell testresult = new TableCell();
                    testresult.Text = "Action";
                    testresult.Font.Bold = true;
                    placeHolderRow.Cells.Add(testresult);

                    placeHolderRow.Attributes.Add("display", "none");
                    testTable.Rows.Add(placeHolderRow);

                    thepnl.Controls.Add(testTable);
                    ht.Add("lblheading" + r["Department"].ToString(), "lblheading" + r["Department"].ToString());


                }



            }
            BindChildLabTest(dtresult, count);
        }
        /// <summary>
        /// Binds the child lab test.
        /// </summary>
        /// <param name="dtchild">The dtchild.</param>
        /// <param name="rowcount">The rowcount.</param>
        public void BindChildLabTest(DataTable dtchild, string rowcount)
        {
            foreach (DataRow r in dtchild.Rows)
            {
                if (r.RowState == DataRowState.Deleted) continue;
                hdControlExists.Value = r["TestId"].ToString();
                Panel pnlctrl = (Panel)pnlselectlab.FindControl("pnlsubheading" + r["Department"].ToString());

                Table tableData = pnlctrl.FindControl("table_" + pnlctrl.ID) as Table;

                TableRow testRow = new TableRow();
                // testRow.CssClass = "border center formbg";
                testRow.CssClass = "border leftallign1 whitebg";
                #region "testName"
                TableCell testName = new TableCell();

                Label labelTestName = new Label();
                labelTestName.ID = "lblheading" + r["TestId"].ToString();
                labelTestName.Text = r["TestName"].ToString();
                labelTestName.Width = 400;

                testName.Controls.Add(labelTestName);

                // testName.Font.Size = 12;            
                testRow.Cells.Add(testName);
                #endregion


                #region MyRegion
                TableCell testNotes = new TableCell();

                TextBox theRequestNotes = new TextBox();
                theRequestNotes.ID = "txtNotes" + r["TestId"].ToString();
                theRequestNotes.Text = "";
                theRequestNotes.Width = 400;
                theRequestNotes.TextMode = TextBoxMode.MultiLine;
                theRequestNotes.Rows = 3;
                theRequestNotes.Columns = 60;
                testNotes.Controls.Add(theRequestNotes);

                // testName.Font.Size = 12;            
                testRow.Cells.Add(testNotes);
                #endregion

                #region "remove"
                TableCell testAction = new TableCell();

                Label theSpace10 = new Label();
                theSpace10.ID = "theSpace10" + r["TestId"].ToString();
                theSpace10.Width = 100;
                theSpace10.Text = "";
                testAction.Controls.Add(theSpace10);

                LinkButton thelnkRemove = new LinkButton();
                thelnkRemove.ID = "lnkrmv%" + tableData.ID + "^" + r["TestId"].ToString();
                thelnkRemove.Width = 20;
                thelnkRemove.Text = "Remove";
                thelnkRemove.OnClientClick = "return removelink('" + pnlctrl.ClientID + "','" + tableData.ClientID + "','" + r["TestId"].ToString() + "','" + rowcount + "')";
                testAction.Controls.Add(thelnkRemove);

                testRow.Cells.Add(testAction);
                #endregion

                tableData.Rows.Add(testRow);



            }

        }
        /// <summary>
        /// Handles the AsyncPostBackError event of the ActionScriptManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AsyncPostBackErrorEventArgs" /> instance containing the event data.</param>
        protected void ActionScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            string message = e.Exception.Message;
            this.ScriptManager1.AsyncPostBackErrorMessage = message;
        }
        //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        /// <summary>
        /// Updates the table.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true), ScriptMethod]
        public static string UpdateTable(string id)
        {
            string rowcount;
            DataTable dt = (DataTable)HttpContext.Current.Session["LAB_REQ"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["TestId"].ToString() == id)
                    {
                        dt.Rows[i].Delete();
                    }
                }

            }
            rowcount = dt.Rows.Count.ToString();
            dt.AcceptChanges();
            HttpContext.Current.Session["LAB_REQ"] = dt;
            return rowcount;

        }



        // [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        /// <summary>
        /// Saves the specified labtobedone.
        /// </summary>
        /// <param name="labtobedone">The labtobedone.</param>
        /// <param name="laborder">The laborder.</param>
        /// <param name="laborderdate">The laborderdate.</param>
        /// <param name="appcurrdate">The appcurrdate.</param>
        /// <returns></returns>
        //[WebMethod(EnableSession = true), ScriptMethod]
        string Save(string labtobedone, string laborder, string laborderdate, string appcurrdate)
        {
            string strresult = string.Empty;
            // Page page = HttpContext.Current.Handler as Page;
            if (FieldValidation(labtobedone, laborderdate, laborder, appcurrdate) == false)
            {
                return "false";
            }

            string TestName = "";
            string TestDescription = "";

            string TestID = "";

            DataTable dt = this.RequestedTests;

            DataTable datatable = new DataTable();
            DataColumn colTestID = new DataColumn("TestID", typeof(int));
            datatable.Columns.Add(colTestID);
            DataColumn colTestName = new DataColumn("TestName", typeof(string));
            datatable.Columns.Add(colTestName);
            DataColumn colTestNote = new DataColumn("TestNotes", typeof(string));
            datatable.Columns.Add(colTestNote);
            DataColumn colDelete = new DataColumn("DeleteFlag", typeof(bool));
            colDelete.DefaultValue = false;
            datatable.Columns.Add(colDelete);


            foreach (RepeaterItem dataItem in repeaterLabTest.Items)
            {
                TestName = (dataItem.FindControl("labelTestName") as Label).Text;
                TestDescription = (dataItem.FindControl("txtRequestNotes") as TextBox).Text;
                TestID = (dataItem.FindControl("TestID") as HiddenField).Value;

                DataRow thisRow = dt.AsEnumerable().FirstOrDefault(r => r["TestID"].ToString() == TestID.ToString());
                if (thisRow != null)
                {
                    DataRow newRow = datatable.NewRow();
                    newRow.SetField("TestID", TestID);
                    newRow.SetField("TestName", TestName);
                    newRow.SetField("TestNotes", TestDescription);
                    newRow.SetField("DeleteFlag", false);
                    datatable.Rows.Add(newRow);
                    datatable.AcceptChanges();
                }
                dt.AcceptChanges();
                this.RequestedTests = dt;
            }


            IQCareUtils theUtils = new IQCareUtils();
            ILabFunctions LabTestsManager;
            if (Convert.ToInt32(HttpContext.Current.Session["Lab_ID"]) == 0)
            {
                int PatientID = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);
                int LocationID = Convert.ToInt32(HttpContext.Current.Session["AppLocationId"]);
                LabTestsManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(
                    PatientID,
                    LocationID,
                    datatable,
                    Convert.ToInt32(HttpContext.Current.Session["AppUserId"]),
                    Convert.ToInt32(laborder),
                    laborderdate,
                    Convert.ToString(HttpContext.Current.Session["Lab_ID"]),
                    88,
                    labtobedone,
                    Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]),
                    true
                    );
                HttpContext.Current.Session["Lab_ID"] = DSsavedLabID.Tables[0].Rows[0]["LabID"].ToString();
                // ViewState["LabOther"] = DSsavedLabID.Tables[0];
                HttpContext.Current.Session["LabOther"] = DSsavedLabID.Tables[0];
                //ViewState["LabTestID"] = DSsavedLabID.Tables[1];
                HttpContext.Current.Session["LabTestID"] = DSsavedLabID.Tables[1];
                // page.ViewState["TestDatetxt"] = laborderdate;
                HttpContext.Current.Session["TestDatetxt"] = laborderdate;

            }
            else
            {
                int PatientID = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);
                int LocationID = Convert.ToInt32(HttpContext.Current.Session["ServiceLocationId"]);

                if (Convert.ToInt32(HttpContext.Current.Session["Lab_ID"]) > 0)
                {
                    LabTestsManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                    DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(PatientID, LocationID, datatable, Convert.ToInt32(HttpContext.Current.Session["AppUserId"]),
                        Convert.ToInt32(laborder), laborderdate, Convert.ToString(HttpContext.Current.Session["Lab_ID"]), 99,
                        labtobedone,
                        Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]), true);
                    HttpContext.Current.Session["Lab_ID"] = DSsavedLabID.Tables[0].Rows[0]["LabID"].ToString();
                    //  ViewState["LabOther"] = DSsavedLabID.Tables[0];
                    HttpContext.Current.Session["LabOther"] = DSsavedLabID.Tables[0];
                    //ViewState["LabTestID"] = DSsavedLabID.Tables[1];
                    HttpContext.Current.Session["LabTestID"] = DSsavedLabID.Tables[1];
                    //ViewState["TestDatetxt"] = laborderdate;
                    HttpContext.Current.Session["TestDatetxt"] = laborderdate;

                }
                else
                {
                    LocationID = Convert.ToInt32(HttpContext.Current.Session["AppLocationId"]);
                    LabTestsManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                    DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(PatientID, LocationID, datatable, Convert.ToInt32(HttpContext.Current.Session["AppUserId"]),
                        Convert.ToInt32(laborder), laborderdate, Convert.ToString(HttpContext.Current.Session["Lab_ID"]), 88, labtobedone,
                        Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]), true);
                    HttpContext.Current.Session["Lab_ID"] = DSsavedLabID.Tables[0].Rows[0]["LabID"].ToString();
                    //ViewState["LabOther"] = DSsavedLabID.Tables[0];
                    HttpContext.Current.Session["LabOther"] = DSsavedLabID.Tables[0];
                    //ViewState["LabTestID"] = DSsavedLabID.Tables[1];
                    HttpContext.Current.Session["LabTestID"] = DSsavedLabID.Tables[1];
                    //ViewState["TestDatetxt"] = laborderdate;
                    HttpContext.Current.Session["TestDatetxt"] = laborderdate;

                }
            }
            strresult = HttpContext.Current.Session["Lab_ID"].ToString();
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", "OnLabSave('" + strresult + "')");
            return strresult;
        }
        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <param name="labtobedone">The labtobedone.</param>
        /// <param name="orderbydate">The orderbydate.</param>
        /// <param name="orderby">The orderby.</param>
        /// <param name="appcurrentdate">The appcurrentdate.</param>
        /// <returns></returns>
        private Boolean FieldValidation(string labtobedone, string orderbydate, string orderby, string appcurrentdate)
        {

            IIQCareSystem _iqcareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime _theCurrentDate = (DateTime)_iqcareSecurity.SystemDate();
            IQCareUtils theUtils = new IQCareUtils();

            Page page = HttpContext.Current.Handler as Page;
            if (orderby == "0")
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Ordered By";
                IQCareMsgBox.Show("BlankDropDown", theMsg, page);
                return false;
            }
            else if (orderbydate == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Ordered By Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, page);
                return false;
            }
            else if (Convert.ToDateTime(orderbydate) > Convert.ToDateTime(appcurrentdate))
            {
                IQCareMsgBox.Show("OrderedToDate", page);
                return false;
            }
            //----------



            if (labtobedone != "")
            {
                DateTime _theVisitDate = Convert.ToDateTime(theUtils.MakeDate(labtobedone));
                if (HttpContext.Current.Session["IEVisitDate"] != null)//ViewState["IEVisitDate"] != null)
                {
                    DateTime _theIEVisitDate = Convert.ToDateTime(HttpContext.Current.Session["IEVisitDate"].ToString());//Convert.ToDateTime(ViewState["IEVisitDate"].ToString());
                    if (_theIEVisitDate > _theVisitDate)
                    {
                        IQCareMsgBox.Show("CompareLabTobeDoneDate", page);
                        return false;
                    }

                }
            }

            return true;
        }
     
        /// <summary>
        /// Handles the Click event of the btnclose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnclose_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language='javascript' type='text/javascript'>window.close();</script>");
        }


        /// <summary>
        /// Handles the TextChanged event of the txtLabtobeDone control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void txtLabtobeDone_TextChanged(object sender, EventArgs e)
        {
            if (this.txtLabtobeDone.Text != "")
            {
                this.preclinicLabs.Checked = true;
            }
        }

        protected void repeaterLabTest_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                RepeaterItem item = e.Item;
                if (e.CommandName == "REMOVE")
                {
                    string testID = (e.CommandArgument.ToString());
                    DataTable dt = this.RequestedTests;
                    DataRow row = dt.AsEnumerable().FirstOrDefault(r => r["TestID"].ToString() == testID);
                    if (row != null)
                    {

                        if (Convert.ToBoolean(row["Persisted"]))
                        {
                            row["DeleteFlag"] = true;
                            row.AcceptChanges();
                        }
                        else
                        {
                            dt.Rows.Remove(row);
                            dt.AcceptChanges();
                        }

                    }
                    this.RequestedTests = dt;
                }
                this.BindLabTests();
            }
        }

        protected void repeaterLabTest_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void repeaterLabTest_DataBinding(object sender, EventArgs e)
        {

        }

        void CatchPostBack()
        {
            foreach (RepeaterItem dataItem in repeaterLabTest.Items)
            {
                string TestName = "";
                string TestDescription = "";

                string TestID = "";

                DataTable dt = this.RequestedTests;
                TestName = (dataItem.FindControl("labelTestName") as Label).Text;
                TestDescription = (dataItem.FindControl("txtRequestNotes") as TextBox).Text;
                TestID = (dataItem.FindControl("TestID") as HiddenField).Value;

                DataRow thisRow = dt.AsEnumerable().FirstOrDefault(r => r["TestID"].ToString() == TestID.ToString());
                if (thisRow != null)
                {
                    thisRow.SetField("TestNotes", TestDescription);
                }
                dt.AcceptChanges();
                this.RequestedTests = dt;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // this.Save(txtLabtobeDone.Text, ddlaborderedbyname.SelectedValue, txtlaborderedbydate.Text, hdappcurrentdate.Value);


            string labtobedone = txtLabtobeDone.Text;
            string laborder = ddlaborderedbyname.SelectedValue;
            string laborderdate = txtlaborderedbydate.Text;
            string appcurrdate = hdappcurrentdate.Value;

            string strresult = string.Empty;
            // Page page = HttpContext.Current.Handler as Page;
            if (FieldValidation(labtobedone, laborderdate, laborder, appcurrdate) == false)
            {
                return;
            }

            string TestName = "";
            string TestDescription = "";

            string TestID = "";

            DataTable dt = this.RequestedTests;

            DataTable datatable = new DataTable();
            DataColumn colTestID = new DataColumn("TestID", typeof(int));
            datatable.Columns.Add(colTestID);
            DataColumn colTestName = new DataColumn("TestName", typeof(string));
            datatable.Columns.Add(colTestName);
            DataColumn colTestNote = new DataColumn("TestNotes", typeof(string));
            datatable.Columns.Add(colTestNote);
            DataColumn colDelete = new DataColumn("DeleteFlag", typeof(bool));
            colDelete.DefaultValue = false;
            datatable.Columns.Add(colDelete);


            foreach (RepeaterItem dataItem in repeaterLabTest.Items)
            {
                TestName = (dataItem.FindControl("labelTestName") as Label).Text;
                TestDescription = (dataItem.FindControl("txtRequestNotes") as TextBox).Text;
                TestID = (dataItem.FindControl("TestID") as HiddenField).Value;

                DataRow thisRow = dt.AsEnumerable().FirstOrDefault(r => r["TestID"].ToString() == TestID.ToString());
                if (thisRow != null)
                {
                    DataRow newRow = datatable.NewRow();
                    newRow.SetField("TestID", TestID);
                    newRow.SetField("TestName", TestName);
                    newRow.SetField("TestNotes", TestDescription);
                    newRow.SetField("DeleteFlag", false);
                    datatable.Rows.Add(newRow);
                    datatable.AcceptChanges();
                }
                dt.AcceptChanges();
                this.RequestedTests = dt;
            }


            IQCareUtils theUtils = new IQCareUtils();
            ILabFunctions LabTestsManager;
            if (Convert.ToInt32(HttpContext.Current.Session["Lab_ID"]) == 0)
            {
                int PatientID = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);
                int LocationID = Convert.ToInt32(HttpContext.Current.Session["AppLocationId"]);
                LabTestsManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(
                    PatientID,
                    LocationID,
                    datatable,
                    Convert.ToInt32(HttpContext.Current.Session["AppUserId"]),
                    Convert.ToInt32(laborder),
                    laborderdate,
                    Convert.ToString(HttpContext.Current.Session["Lab_ID"]),
                    88,
                    labtobedone,
                    Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]),
                    true
                    );
                HttpContext.Current.Session["Lab_ID"] = DSsavedLabID.Tables[0].Rows[0]["LabID"].ToString();
                // ViewState["LabOther"] = DSsavedLabID.Tables[0];
                HttpContext.Current.Session["LabOther"] = DSsavedLabID.Tables[0];
                //ViewState["LabTestID"] = DSsavedLabID.Tables[1];
                HttpContext.Current.Session["LabTestID"] = DSsavedLabID.Tables[1];
                // page.ViewState["TestDatetxt"] = laborderdate;
                HttpContext.Current.Session["TestDatetxt"] = laborderdate;

            }
            else
            {
                int PatientID = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);
                int LocationID = Convert.ToInt32(HttpContext.Current.Session["ServiceLocationId"]);

                if (Convert.ToInt32(HttpContext.Current.Session["Lab_ID"]) > 0)
                {
                    LabTestsManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                    DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(
                        PatientID,
                        LocationID,
                        datatable,
                        Convert.ToInt32(HttpContext.Current.Session["AppUserId"]),
                        Convert.ToInt32(laborder),
                        laborderdate,
                        Convert.ToString(HttpContext.Current.Session["Lab_ID"]),
                        99,
                        labtobedone,
                        Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]),
                        true
                        );
                    HttpContext.Current.Session["Lab_ID"] = DSsavedLabID.Tables[0].Rows[0]["LabID"].ToString();
                    //  ViewState["LabOther"] = DSsavedLabID.Tables[0];
                    HttpContext.Current.Session["LabOther"] = DSsavedLabID.Tables[0];
                    //ViewState["LabTestID"] = DSsavedLabID.Tables[1];
                    HttpContext.Current.Session["LabTestID"] = DSsavedLabID.Tables[1];
                    //ViewState["TestDatetxt"] = laborderdate;
                    HttpContext.Current.Session["TestDatetxt"] = laborderdate;

                }
                else
                {
                    LocationID = Convert.ToInt32(HttpContext.Current.Session["AppLocationId"]);
                    LabTestsManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                    DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(PatientID, LocationID, datatable, Convert.ToInt32(HttpContext.Current.Session["AppUserId"]),
                        Convert.ToInt32(laborder), laborderdate, Convert.ToString(HttpContext.Current.Session["Lab_ID"]), 88, labtobedone, Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]));
                    HttpContext.Current.Session["Lab_ID"] = DSsavedLabID.Tables[0].Rows[0]["LabID"].ToString();
                    //ViewState["LabOther"] = DSsavedLabID.Tables[0];
                    HttpContext.Current.Session["LabOther"] = DSsavedLabID.Tables[0];
                    //ViewState["LabTestID"] = DSsavedLabID.Tables[1];
                    HttpContext.Current.Session["LabTestID"] = DSsavedLabID.Tables[1];
                    //ViewState["TestDatetxt"] = laborderdate;
                    HttpContext.Current.Session["TestDatetxt"] = laborderdate;

                }
            }
            strresult = HttpContext.Current.Session["Lab_ID"].ToString();

            ScriptManager.RegisterStartupScript(buttonSave, this.GetType(), "confirm", "OnLabSave('" + strresult + "')", true);


        }


    }
}