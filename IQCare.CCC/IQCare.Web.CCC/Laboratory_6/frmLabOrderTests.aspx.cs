using System;
using System.Data;
using System.Web.UI;
using Application.Common;
using Application.Presentation;
using Interface.Laboratory;

/// <summary>
/// 
/// </summary>
namespace IQCare.Web.Laboratory
{
    /// <summary>
    /// 
    /// </summary>
    public partial class LabOrderTests : System.Web.UI.Page
    {
        /// <summary>
        /// The dv
        /// </summary>
        DataView theDV = new DataView();
        /// <summary>
        /// The d ttest date
        /// </summary>
        DataTable DTtestDate = new DataTable();

        /// <summary>
        /// Binds the chklist labs.
        /// </summary>
        private void BindChklistLabs()
        {
            if (ViewState["LabTestID"] != null)
            {
                for (int i = 0; i < chkOrderLabTests.Items.Count; i++)
                {
                    foreach (DataRow dr in ((DataTable)ViewState["LabTestID"]).Rows)
                    {
                        if (chkOrderLabTests.Items[i].Value == dr[0].ToString())
                        {
                            chkOrderLabTests.Items[i].Selected = true;
                        }

                    }
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
                theDSXML.Clear();
                theDSXML.ReadXml(Server.MapPath("..\\XMLFiles\\LabMasters.con"));
                theDV = new DataView(theDSXML.Tables["LabTestOrder"]);
                theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    theDV.Sort = "SubTestName asc";
                    DataTable theLabDt = theDV.ToTable();
                    theBindManager.BindCheckedList(chkOrderLabTests, theLabDt, "SubTestName", "SubTestId");
                    theDV.Dispose();
                    theDT.Clear();
                }
            }


            if (Convert.ToInt32(ViewState["LabID"]) > 0)
            {

                BindDropdownOrderBy(DTtestDate.Rows[0]["OrderedbyName"].ToString());
                ddlaborderedbyname.SelectedValue = DTtestDate.Rows[0]["OrderedbyName"].ToString();
                txtlaborderedbydate.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(DTtestDate.Rows[0]["OrderedbyDate"]));
                BindChklistLabs();
            }
        }
        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <returns></returns>
        private Boolean FieldValidation()
        {
            int j = 0;
            for (int i = 0; i < chkOrderLabTests.Items.Count; i++)
            {
                if (chkOrderLabTests.Items[i].Selected == false)
                {
                    j = j + 1;
                }
            }

            string LabDate = "";
            if (Request.QueryString["name"] == "Edit")
            {
                if (txtlaborderedbydate.Text != ViewState["TestDatetxt"].ToString())
                {
                    foreach (DataRow DR in ((DataTable)ViewState["TestDate"]).Rows)
                    {
                        LabDate = String.Format("{0:dd-MMM-yyyy}", DR[0]);
                        if (txtlaborderedbydate.Text == LabDate)
                        {
                            IQCareMsgBox.Show("LaborderTestdate", this);
                            return false;
                        }
                    }

                }

            }
            ////else
            ////{
            ////    foreach (DataRow DR in ((DataTable)ViewState["TestDate"]).Rows)
            ////    {
            ////        LabDate = String.Format("{0:dd-MMM-yyyy}", DR[0]);
            ////        if (txtlaborderedbydate.Text == LabDate)
            ////        {
            ////            IQCareMsgBox.Show("LaborderTestdate", this);
            ////            return false;
            ////        }
            ////    }

            ////}


            if (chkOrderLabTests.Items.Count == j)
            {
                IQCareMsgBox.Show("Laborderchklist", this);
                return false;
            }

            else if (ddlaborderedbyname.SelectedIndex == 0)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Ordered By";
                IQCareMsgBox.Show("BlankDropDown", theMsg, this);
                return false;
            }
            else if (txtlaborderedbydate.Text.ToString() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Ordered By Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtlaborderedbydate.Focus();
                return false;
            }

            else if (Convert.ToDateTime(txtlaborderedbydate.Text) > Convert.ToDateTime(Application["AppCurrentDate"]))
            {
                IQCareMsgBox.Show("OrderedToDate", this);
                return false;
            }
            return true;
        }
        /// <summary>
        /// Saves the cancel.
        /// </summary>
        private void SaveCancel()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Lab tests saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.close();\n";
            script += "}\n";
            //script += "else \n";
            //script += "{\n";
            //script += "window.location.href('../Laboratory/frmLabOrderTests.aspx'  );\n";
            //script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);

        }

        /// <summary>
        /// Saves the cancel exist.
        /// </summary>
        private void SaveCancelExist()
        {

            //string strSession = Session["Ptn_pk"].ToString();
            string script = "<script language = 'javascript' id = 'confirm1'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Lab tests saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.opener.GetControl();window.close();\n";
            script += "}\n";
            //script += "else \n";
            //script += "{\n";
            //script += "window.location.href('../Laboratory/frmLabOrderTests.aspx?Mode=Edit&patientid=" + Session["Ptn_Pk"].ToString() + "&LabID=" + ViewState["LabID"] + "'  );\n";
            //script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm1", script);


        }

        /// <summary>
        /// Gets the laborderdate.
        /// </summary>
        private void GetLaborderdate()
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            int LocationID = Convert.ToInt32(Session["AppLocationId"]);
            ILabFunctions LabTestsMgrDate;
            if (ViewState["LabID"] != null)
            {
                LabTestsMgrDate = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                DTtestDate = (DataTable)LabTestsMgrDate.GetLaborderdate(PatientID, LocationID, Convert.ToInt32(ViewState["LabID"]));
                ViewState["TestDate"] = DTtestDate;
            }
        }

        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            txtlaborderedbydate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtlaborderedbydate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            AuthenticationManager Authentication = new AuthenticationManager();
            if (Session["LabId"] != null)
            {
                ViewState["LabID"] = Session["LabId"];
                ViewState["LabTestID"] = Session["LabTestID"];
                Session["LabId"] = null;
                Session["LabTestID"] = null;
            }
            if (Convert.ToString(Request.QueryString["sts"]) == "1")
                btnsave.Enabled = false;

            if (IsPostBack != true)
            {
                //Authentication Right
                if (Authentication.HasFunctionRight(ApplicationAccess.OrderLabTest, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                }
                GetLaborderdate();
                BindControls();
            }
        }
        /// <summary>
        /// Labs the test.
        /// </summary>
        /// <returns></returns>
        private DataTable LabTest()
        {
            DataTable dtLabTest = new DataTable();
            dtLabTest.Columns.Add("TestId", System.Type.GetType("System.Int32"));

            DataRow drLabTest;

            for (int i = 0; i < chkOrderLabTests.Items.Count; i++)

                if (chkOrderLabTests.Items[i].Selected == true)
                {
                    drLabTest = dtLabTest.NewRow();
                    drLabTest["TestId"] = Convert.ToInt32(chkOrderLabTests.Items[i].Value);
                    dtLabTest.Rows.Add(drLabTest);

                }

            return dtLabTest;

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
        /// Handles the Click event of the btnsave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                return;
            }


            DataTable LabTestIDs = LabTest();
            IQCareUtils theUtils = new IQCareUtils();
            ILabFunctions LabTestsManager;
            if (Convert.ToInt32(ViewState["LabID"]) == 0)
            {
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                int LocationID = Convert.ToInt32(Session["AppLocationId"]);
                LabTestsManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(PatientID, LocationID, LabTestIDs, Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(ddlaborderedbyname.SelectedValue), txtlaborderedbydate.Text, Convert.ToString(ViewState["LabID"]), 88, "");
                ViewState["LabID"] = DSsavedLabID.Tables[0].Rows[0]["LabID"].ToString();
                ViewState["LabOther"] = DSsavedLabID.Tables[0];
                ViewState["LabTestID"] = DSsavedLabID.Tables[1];
                ViewState["TestDatetxt"] = txtlaborderedbydate.Text;
                SaveCancel();
            }
            else
            {
                int PatientID = Convert.ToInt32(Session["PatientId"]);
                int LocationID = Convert.ToInt32(Session["ServiceLocationId"]);

                if (Convert.ToInt32(ViewState["LabID"]) > 0)
                {
                    LabTestsManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                    DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(PatientID, LocationID, LabTestIDs, Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(ddlaborderedbyname.SelectedValue), txtlaborderedbydate.Text, Convert.ToString(ViewState["LabID"]), 99, "");
                    ViewState["LabID"] = DSsavedLabID.Tables[0].Rows[0]["LabID"].ToString();
                    ViewState["LabOther"] = DSsavedLabID.Tables[0];
                    ViewState["LabTestID"] = DSsavedLabID.Tables[1];
                    ViewState["TestDatetxt"] = txtlaborderedbydate.Text;
                    SaveCancelExist();
                }
                else
                {
                    LocationID = Convert.ToInt32(Session["AppLocationId"]);
                    LabTestsManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                    DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(PatientID, LocationID, LabTestIDs, Convert.ToInt32(Session["AppUserId"]), Convert.ToInt32(ddlaborderedbyname.SelectedValue), txtlaborderedbydate.Text, Convert.ToString(ViewState["LabID"]), 88, "");
                    ViewState["LabID"] = DSsavedLabID.Tables[0].Rows[0]["LabID"].ToString();
                    ViewState["LabOther"] = DSsavedLabID.Tables[0];
                    ViewState["LabTestID"] = DSsavedLabID.Tables[1];
                    ViewState["TestDatetxt"] = txtlaborderedbydate.Text;
                    SaveCancel();
                }

            }

        }
        /// <summary>
        /// Handles the Click event of the btnclose control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnclose_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(),"onclick", "<script language='javascript' type='text/javascript'>window.close();</script>");
            Session.Remove("TestDatetxt");
        }
    }

}