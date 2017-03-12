using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Interface.Laboratory;
using Application.Presentation;
using Application.Common;

namespace IQCare.Web.Laboratory
{
    public partial class DynamicLabForm : System.Web.UI.Page
    {
        /// <summary>
        /// The ordered labs dataset
        /// </summary>
        DataSet orderedLabs;
        string FormMode
        {
            get
            {
                return HMODe.Value;
            }
            set
            {
                HMODe.Value = value;
            }
        }
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Request.QueryString["mode"] != null)
            {
                FormMode = "XRAY";
            }
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            if (FormMode == "XRAY")
            {
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "X-RAY Test";
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "X-RAY Tests";

                lnkLabTest.Text = "Edit X-Ray Order";
            }
            else
            {
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Lab Test";
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Laboratory Tests";
            }
            // (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
            Init_page();
        }

        /// <summary>
        /// Init_pages this instance.
        /// </summary>
        private void Init_page()
        {

            ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            ViewState["LabId"] = Session["PatientVisitId"];
            int labID = 0;
            if (ViewState["LabId"] != null)
                int.TryParse(ViewState["LabId"].ToString(), out labID);

            orderedLabs = LabResultManager.GetOrderedLabs(labID);
            if (!IsPostBack)
            {
                if (orderedLabs.Tables[0].Rows.Count > 0)
                {

                    BindDropdownReportedBy(orderedLabs.Tables[0].Rows[0]["ReportedByName"].ToString());
                    ddlLabReportedbyName.SelectedValue = orderedLabs.Tables[0].Rows[0]["ReportedByName"].ToString();
                    string reportDate = orderedLabs.Tables[0].Rows[0]["ReportedbyDate"].ToString();
                    if (reportDate != "")
                        txtlabReportedbyDate.Text = ((DateTime.Parse(reportDate)).ToString(Session["AppDateFormat"].ToString()));
                    ViewState["labOrderDate"] = ((DateTime)orderedLabs.Tables[0].Rows[0]["OrderedbyDate"]);

                }
            }

            DataView dv = orderedLabs.Tables[0].DefaultView;

            DataTable theDT = dv.ToTable(true, "LabDepartmentName");
            foreach (DataRow r in theDT.Rows)
            {
                AddDepartment(r[0].ToString());
            }
            txtlabReportedbyDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtlabReportedbyDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");


        }

        /// <summary>
        /// Adds the department.
        /// </summary>
        /// <param name="dept">The dept.</param>
        private void AddDepartment(String dept)
        {
            Literal deptLit = new Literal();

            deptLit.Text = String.Format("<h3 class=\"forms\" align=\"left\">{0}</h3>", dept);
            maindiv.Controls.Add(deptLit);

            Panel DIVCustomItem = new Panel();
            DIVCustomItem.CssClass = "border center pad5 whitebg";

            //maindiv.Controls.Add(new LiteralControl("<table cellspacing='6' cellpadding='0' width='100%' border='0'>"));
            DIVCustomItem.Controls.Add(createTestTable(dept));
            maindiv.Controls.Add(DIVCustomItem);

            //  maindiv.Controls.Add(new LiteralControl("</table>"));
            //  maindiv.Controls.Add(new LiteralControl("</br>"));

        }


        /// <summary>
        /// Creates the test table.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns></returns>
        private Table createTestTable(String department)
        {

            Table testTable = new Table();
            testTable.CellSpacing = 6;
            testTable.CellPadding = 0;
            testTable.Width = Unit.Percentage(100);
            testTable.BorderWidth = 0;
            DataView dv = orderedLabs.Tables[0].DefaultView;
            dv.RowFilter = "LabDepartmentName= '" + department + "'";
            DataTable theDT = dv.ToTable();
            testTable.Rows.Add(CreateHeaderRow());
            int i = 0;
            foreach (DataRow r in theDT.Rows)
            {
                i++;
                testTable.Rows.Add(CreateTestRow(r,i));
            }


            return testTable;
        }
        /// <summary>
        /// Creates the header row.
        /// </summary>
        /// <returns></returns>
        private TableRow CreateHeaderRow()
        {
            TableRow testRow = new TableRow();

            // testRow.CssClass = "border center formbg";
            testRow.CssClass = "border leftallign1 whitebg";

            TableCell testName = new TableCell();
            testName.Text = "Test Name";
            testName.Font.Bold = true;
            // testName.Font.Size = 12;            
            testRow.Cells.Add(testName);
            if (FormMode == "XRAY")
            {
                TableCell testNotes = new TableCell();
                testNotes.Text = "Description";
                testNotes.Font.Bold = true;
                // testName.Font.Size = 12;            
                testRow.Cells.Add(testNotes);
            }

            TableCell testresult = new TableCell();
            testresult.Text = "Test Result";
            testresult.Font.Bold = true;
            testRow.Cells.Add(testresult);

            TableCell testUnit = new TableCell();
            testUnit.Text = "Unit";
            testUnit.Font.Bold = true;
            testRow.Cells.Add(testUnit);
            return testRow;
        }
        /// <summary>
        /// Creates the test row.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <returns></returns>
        private TableRow CreateTestRow(DataRow dr,int rowIndex)
        {
            TableRow testRow = new TableRow();
            testRow.CssClass = "border leftallign1 whitebg";
            TableCell testName = new TableCell();
            String strTestName;
            strTestName = dr["SubTestName"].ToString();
            testName.Text = strTestName;
            testRow.Cells.Add(testName);
            if (FormMode == "XRAY")
            {

                TableCell testNotes = new TableCell();
                String strTestNotes = dr["RequestNotes"].ToString();
                testNotes.Text = strTestNotes;
                testRow.Cells.Add(testNotes);
            }
            TableCell testresult = new TableCell();
            //TODO code for adding item depending on type of test
            int ParameterID = (int)dr["ParameterID"];
            DataRow[] r = orderedLabs.Tables[1].Select("ParameterID=" + ParameterID);
            if (r.Length > 0)//select list item
            {
                DropDownList ddlResults = new DropDownList();
                ddlResults.ID = strTestName.Replace(" ", "_")+rowIndex;
                ddlResults.Attributes.Add("Tag", ParameterID.ToString());
                BindLabResultDropDown(ddlResults, r);
                ddlResults.SelectedValue = dr["ResultID"].ToString();
                testresult.Controls.Add(ddlResults);
                testRow.Cells.Add(testresult);

            }
            else
            {

                TextBox txtResults = new TextBox();
                txtResults.Text = dr["TestResults"].ToString();
                txtResults.ID = strTestName.Replace(" ", "_") + rowIndex;
                txtResults.Attributes.Add("Tag", ParameterID.ToString());
                txtResults.Attributes.Add("data-unit", dr["UnitID"].ToString());
                testresult.Controls.Add(txtResults);
                //testresult.Text = "TestResult";
                testRow.Cells.Add(testresult);
                AddValidation(txtResults, strTestName, dr["MinBoundaryValue"].ToString(), dr["MaxBoundaryValue"].ToString());
            }
            TableCell testUnit = new TableCell();
            testUnit.Text = dr["LabUnit"].ToString();
            testRow.Cells.Add(testUnit);
            return testRow;
        }

        /// <summary>
        /// Handles the Click event of the btnsave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
            //TODO validate first if valid continue
            if (!ValidateForm()) return;

            DataTable theDt = new DataTable();
            theDt.Columns.Add("ParameterID", typeof(int));
            theDt.Columns.Add("TestResult", typeof(Decimal));
            theDt.Columns.Add("TestResult1", typeof(String));
            theDt.Columns.Add("TestResultId", typeof(int));
            theDt.Columns.Add("Units", typeof(int));

            theDt = GetDataToSave(maindiv, theDt);

            IQCareUtils theUtils = new IQCareUtils();
            ILabFunctions LabMaster = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            LabMaster.SaveDynamicLabResults(Convert.ToInt32(ViewState["LabId"]), Convert.ToInt32(Session["AppUserId"]),
               int.Parse(ddlLabReportedbyName.SelectedValue), DateTime.Parse(txtlabReportedbyDate.Text), theDt);
            SuccessRecordsSave();


        }
        /// <summary>
        /// Successes the records save.
        /// </summary>
        private void SuccessRecordsSave()
        {
            string theUrl = string.Format("../ClinicalForms/frmPatient_History.aspx");

            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
			 if (FormMode == "XRAY"){
            script += "window.alert('X-Ray results saved successfully.');\n";
			}
			else{
			script += "window.alert('Lab results saved successfully.');\n";
			}
            script += "window.location.href='" + theUrl + "';\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "success", script);
        }
        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl = string.Format("../ClinicalForms/frmPatient_History.aspx");

            Response.Redirect(theUrl);
        }

        /// <summary>
        /// Binds the dropdown reported by.
        /// </summary>
        /// <param name="EmployeeId">The employee identifier.</param>
        private void BindDropdownReportedBy(String EmployeeId)
        {
            DataSet theDS = new DataSet();
            theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            if (theDS.Tables["Mst_Employee"] != null)
            {
                DataView theDV = new DataView(theDS.Tables["Mst_Employee"]);
                theDV.RowFilter = "DeleteFlag=0";
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

                    BindManager.BindCombo(ddlLabReportedbyName, theDT, "EmployeeName", "EmployeeId");

                }
            }

        }
        /// <summary>
        /// Binds the lab result drop down.
        /// </summary>
        /// <param name="ddlresults">The ddlresults.</param>
        /// <param name="listItems">The list items.</param>
        private void BindLabResultDropDown(DropDownList ddlresults, DataRow[] listItems)
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();


            /*  DataView theDV = orderedLabs.Tables[1].DefaultView;
              theDV.RowFilter = "ParameterID=" + ParameterID;*/

            DataTable theDT = new DataTable();
            theDT = listItems.CopyToDataTable();


            BindManager.BindCombo(ddlresults, theDT, "result", "resultID");
            //theDV.Dispose();
            theDT.Dispose();


        }
        /// <summary>
        /// Adds the validation.
        /// </summary>
        /// <param name="cntrl">The CNTRL.</param>
        /// <param name="itemName">Name of the item.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        private void AddValidation(Control cntrl, String itemName, String min, String max)
        {
            TextBox txtcntrl = cntrl as TextBox;
            if (txtcntrl != null)
            {
                if (max.Trim() != "" && min.Trim() != "")
                {
                    txtcntrl.Attributes.Add("onblur", "isBetween('ctl00_IQCareContentPlaceHolder_" + txtcntrl.ID + "', '" + itemName + "','" + min + "', '" + max + "')");
                    txtcntrl.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + txtcntrl.ID + "')");
                    txtcntrl.Attributes.Add("data-resultType", "num");
                }
                else if (max.Trim() != "")
                {
                    txtcntrl.Attributes.Add("onblur", "checkMax('ctl00_IQCareContentPlaceHolder_" + txtcntrl.ID + "', '" + itemName + "', '" + max + "')");
                    txtcntrl.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + txtcntrl.ID + "')");
                    txtcntrl.Attributes.Add("data-resultType", "num");
                }
                else if (min.Trim() != "")
                {
                    txtcntrl.Attributes.Add("onblur", "checkMin('ctl00_IQCareContentPlaceHolder_" + txtcntrl.ID + "', '" + itemName + "', '" + min + "')");
                    txtcntrl.Attributes.Add("onkeyup", "chkDecimal('ctl00_IQCareContentPlaceHolder_" + txtcntrl.ID + "')");
                    txtcntrl.Attributes.Add("data-resultType", "num");
                }
                else
                {
                    txtcntrl.Attributes.Add("data-resultType", "text");
                    txtcntrl.TextMode = TextBoxMode.MultiLine;
                }

            }
        }
        /// <summary>
        /// Gets the data to save.
        /// </summary>
        /// <param name="containercontrol">The containercontrol.</param>
        /// <param name="theDt">The dt.</param>
        /// <returns></returns>
        private DataTable GetDataToSave(Control containercontrol, DataTable theDt)
        {
            foreach (Control cntrl in containercontrol.Controls)
            {
                if (cntrl.HasControls())
                {
                    theDt = GetDataToSave(cntrl, theDt);
                }
                else if (cntrl.GetType() == typeof(TextBox))
                {
                    TextBox txtcntrl = cntrl as TextBox;
                    if (txtcntrl.Attributes["data-resultType"] == "text")
                        theDt.Rows.Add(txtcntrl.Attributes["Tag"], null, txtcntrl.Text.Trim(), null);
                    else if (txtcntrl.Attributes["data-resultType"] == "num")
                    {
                        int unt = 0;
                        int.TryParse(txtcntrl.Attributes["data-unit"], out unt);
                        theDt.Rows.Add(txtcntrl.Attributes["Tag"], (txtcntrl.Text.Trim().Length == 0 ? null : txtcntrl.Text.Trim()), null, null, unt);
                    }

                }
                else if (cntrl.GetType() == typeof(DropDownList))
                {
                    DropDownList ddlcntrl = cntrl as DropDownList;
                    theDt.Rows.Add(ddlcntrl.Attributes["Tag"], null, null, ddlcntrl.SelectedValue);
                }
            }
            return theDt;
        }
        /// <summary>
        /// Validates the form.
        /// </summary>
        /// <returns></returns>
        private Boolean ValidateForm()
        {

            if (txtlabReportedbyDate.Text == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Lab Reported By Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtlabReportedbyDate.Focus();
                return false;
            }
            else if (txtlabReportedbyDate.Text.ToString() != "")
            {

                if (DateTime.Parse(txtlabReportedbyDate.Text) < (DateTime)ViewState["labOrderDate"])
                {
                    IQCareMsgBox.Show("LabReportedDate", this);
                    txtlabReportedbyDate.Focus();
                    return false;
                }
            }

            if (ddlLabReportedbyName.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Reported by Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                ddlLabReportedbyName.Focus();
                return false;
            }
            return true;


        }

        /// <summary>
        /// Handles the Click event of the lnkLabTest control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void lnkLabTest_Click(object sender, EventArgs e)
        {
            ILabFunctions LabOrderMgr;
            LabOrderMgr = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            DataSet DSLabID = (DataSet)LabOrderMgr.GetPatientLabTestID(Convert.ToString(ViewState["LabId"]));
            Session["LabId"] = ViewState["LabId"];
            Session["LabOther"] = DSLabID.Tables[0];
            Session["LabTestID"] = DSLabID.Tables[2];
            Session["TestDatetxt"] = ViewState["labOrderDate"];

            String theOrdScript;
            if (FormMode == "XRAY")
            {
                theOrdScript = "<script language='javascript' id='XRAYOrderPopup'>\n";
                theOrdScript += "window.location.href = '../ClinicalForms/frmPatient_History.aspx';\n";
                theOrdScript += "window.open('XRayOrderForm.aspx','XRAYSelection','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes');\n";
                theOrdScript += "</script>\n";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "XRAYOrderPopup", theOrdScript);
            }
            else
            {
                theOrdScript = "<script language='javascript' id='LabOrderPopup'>\n";
                theOrdScript += "window.location.href = '../ClinicalForms/frmPatient_History.aspx';\n";
                theOrdScript += "window.open('LabOrderForm.aspx','LabSelection','toolbars=no,location=no,directories=no,dependent=yes,top=100,left=30,maximize=no,resize=no,width=1000,height=800,scrollbars=yes');\n";
                theOrdScript += "</script>\n";
                Page.ClientScript.RegisterStartupScript(this.GetType(), "LabOrderPopup", theOrdScript);
            }


        }
    }
}