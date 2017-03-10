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

namespace IQCare.Web.Laboratory
{
    /// <summary>
    ///
    /// </summary>
    public partial class LabOrderForm : System.Web.UI.Page
    {
        /// <summary>
        /// The d ttest date
        /// </summary>
        private DataTable DTtestDate = new DataTable();

        /// <summary>
        /// The dt lab result
        /// </summary>
        //static DataTable dtLabResult = new DataTable();
        /// <summary>
        /// The dt lab test
        /// </summary>
        //    static DataTable dtLabTest;
        /// <summary>
        /// The dv
        /// </summary>
        private DataView theDV = new DataView();

        // [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        /// <summary>
        /// Saves the specified labtobedone.
        /// </summary>
        /// <param name="labtobedone">The labtobedone.</param>
        /// <param name="laborder">The laborder.</param>
        /// <param name="laborderdate">The laborderdate.</param>
        /// <param name="appcurrdate">The appcurrdate.</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true), ScriptMethod]
        public static string Save(string labtobedone, string laborder, string laborderdate, string appcurrdate)
        {
            string strresult = string.Empty;
            // Page page = HttpContext.Current.Handler as Page;
            if (FieldValidation(labtobedone, laborderdate, laborder, appcurrdate) == false)
            {
                return "false";
            }

            //DataTable LabTestIDs = LabTest();
            DataTable LabTestIDs = new DataTable();
            LabTestIDs.Columns.Add("TestId", System.Type.GetType("System.Int32"));
            LabTestIDs.Columns.Add("IsGroup", System.Type.GetType("System.Boolean"));
            LabTestIDs.Columns.Add("TestNotes", System.Type.GetType("System.String"));

            DataRow drLabTestfinal;
            DataTable dtLabTest = (DataTable)HttpContext.Current.Session["DTLABTEST"];
            for (int i = 0; i < dtLabTest.Rows.Count; i++)
            {
                if (dtLabTest.Rows[i].RowState == DataRowState.Deleted) continue;
                drLabTestfinal = LabTestIDs.NewRow();
                drLabTestfinal["TestId"] = Convert.ToInt32(dtLabTest.Rows[i]["TestId"].ToString());
                bool isGroup = (dtLabTest.Rows[i]["DataType"].ToString().ToLower() == "group");
                drLabTestfinal["IsGroup"] = isGroup;
                LabTestIDs.Rows.Add(drLabTestfinal);
            }

            IQCareUtils theUtils = new IQCareUtils();
            ILabFunctions LabTestsManager;
            if (Convert.ToInt32(HttpContext.Current.Session["Lab_ID"]) == 0)
            {
                int PatientID = Convert.ToInt32(HttpContext.Current.Session["PatientId"]);
                int LocationID = Convert.ToInt32(HttpContext.Current.Session["AppLocationId"]);
                LabTestsManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(PatientID, LocationID, LabTestIDs, Convert.ToInt32(HttpContext.Current.Session["AppUserId"]), Convert.ToInt32(laborder), laborderdate, Convert.ToString(HttpContext.Current.Session["Lab_ID"]), 88, labtobedone, Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]), false);
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
                    DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(PatientID, LocationID, LabTestIDs, Convert.ToInt32(HttpContext.Current.Session["AppUserId"]), Convert.ToInt32(laborder), laborderdate, Convert.ToString(HttpContext.Current.Session["Lab_ID"]), 99, labtobedone, Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]), false);
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
                    DataSet DSsavedLabID = (DataSet)LabTestsManager.SaveLabOrderTests(PatientID, LocationID, LabTestIDs, Convert.ToInt32(HttpContext.Current.Session["AppUserId"]), Convert.ToInt32(laborder), laborderdate, Convert.ToString(HttpContext.Current.Session["Lab_ID"]), 88, labtobedone, Convert.ToInt32(HttpContext.Current.Session["TechnicalAreaId"]), false);
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

            return strresult;
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
            DataTable dtLabTest = (DataTable)HttpContext.Current.Session["DTLABTEST"];
            if (dtLabTest.Rows.Count > 0)
            {
                for (int i = 0; i < dtLabTest.Rows.Count; i++)
                {
                    if (dtLabTest.Rows[i]["TestId"].ToString() == id)
                    {
                        dtLabTest.Rows[i].Delete();
                    }
                }
            }
            rowcount = dtLabTest.Rows.Count.ToString();
            return rowcount;
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
                Panel pnlchildlab = new Panel();
                pnlchildlab.ID = "pnlchild" + r["TestId"].ToString();
                pnlchildlab.Height = 20;
                pnlchildlab.Width = 800;
                pnlchildlab.BorderWidth = 0;

                Label theSpace8 = new Label();
                theSpace8.ID = "theSpace8" + r["TestId"].ToString();
                theSpace8.Width = 10;
                theSpace8.Text = "";
                pnlchildlab.Controls.Add(theSpace8);

                Label thesubheading = new Label();
                thesubheading.ID = "lblheading" + r["TestId"].ToString();
                thesubheading.Text = r["TestName"].ToString();
                thesubheading.Width = 400;
                thesubheading.Font.Bold = true;
                pnlchildlab.Controls.Add(thesubheading);

                Label theSpace9 = new Label();
                theSpace9.ID = "theSpace9" + r["TestId"].ToString();
                theSpace9.Width = 100;
                theSpace9.Text = "";
                pnlchildlab.Controls.Add(theSpace9);

                LinkButton thelnkRemove = new LinkButton();
                thelnkRemove.ID = "lnkrmv%" + pnlchildlab.ID + "^" + r["TestId"].ToString();
                thelnkRemove.Width = 20;
                thelnkRemove.Text = "Remove";

                thelnkRemove.OnClientClick = "return removelink('" + pnlctrl.ClientID + "','" + pnlchildlab.ClientID + "','" + r["TestId"].ToString() + "','" + rowcount + "')";
                pnlchildlab.Controls.Add(thelnkRemove);

                pnlctrl.Controls.Add(pnlchildlab);

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
                string id = ctrl.ClientID;
                Control foundCtrl = FindControlRecursive(ctrl, name);
                if (foundCtrl != null)
                    return foundCtrl;
            }
            return null;
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

                    Label thesubheading = new Label();
                    thesubheading.ID = "lblheading" + r["Department"].ToString();
                    thesubheading.Text = r["Department"].ToString();
                    thesubheading.Font.Bold = true;
                    thesubheading.ForeColor = System.Drawing.Color.Blue;
                    thepnl.Controls.Add(thesubheading);
                    pnlselectlab.Controls.Add(thepnl);

                    ht.Add("lblheading" + r["Department"].ToString(), "lblheading" + r["Department"].ToString());
                }

            }
            BindChildLabTest(dtresult, count);
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
                HttpContext.Current.Session["DTLABTEST"] = null;
                HttpContext.Current.Session["DTLABRESULT"] = null;
                ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
                DataSet theDSPatient = LabResultManager.GetPatientInfo(Session["PatientId"].ToString());
                if (theDSPatient.Tables[1].Rows.Count > 0)
                {
                    ViewState["IEVisitDate"] = theDSPatient.Tables[1].Rows[0]["VisitDate"];
                }
                DataTable dtLabTest = new DataTable();
                dtLabTest.Rows.Clear();
                hdControlExists.Value = "";
                if (dtLabTest.Columns.Count == 0)
                {
                    dtLabTest.Columns.Add("TestId", System.Type.GetType("System.Int32"));
                    dtLabTest.Columns.Add("TestName", System.Type.GetType("System.String"));
                    dtLabTest.Columns.Add("Department", System.Type.GetType("System.String"));
                    dtLabTest.Columns.Add("DataType", System.Type.GetType("System.String"));
                }
                Session["DTLABTEST"] = dtLabTest;
                GetLaborderdate();
                BindControls();
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the txtautoTestName control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void txtautoTestName_TextChanged(object sender, EventArgs e)
        {
            txtautoTestName.Text = "";
            DataTable dtLabResult = (DataTable)HttpContext.Current.Session["DTLABRESULT"];

            DataTable dtLabTest = (DataTable)Session["DTLABTEST"];
            if (hdCustID.Value != "" && dtLabResult.Rows.Count > 0)
            {
                if ((Convert.ToInt32(hdCustID.Value) != 0))
                {
                    DataTable dt = dtLabResult;
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "LabTestID=" + hdCustID.Value + "";
                    DataTable dtfilter = dv.ToTable();

                    var foundlabid = dtLabTest.Select("TestId = '" + hdCustID.Value + "'");
                    if (foundlabid.Length == 0)
                    {
                        hdControlExists.Value = dtfilter.Rows[0]["LabTestID"].ToString();
                        DataRow drLabTest;
                        drLabTest = dtLabTest.NewRow();
                        drLabTest["TestId"] = Convert.ToInt32(dtfilter.Rows[0]["LabTestID"].ToString());
                        drLabTest["TestName"] = dtfilter.Rows[0]["LabName"].ToString();
                        drLabTest["Department"] = dtfilter.Rows[0]["labdepartmentname"].ToString();
                        drLabTest["DataType"] = dtfilter.Rows[0]["DataType"].ToString();
                        dtLabTest.Rows.Add(drLabTest);
                    }

                    LoadLabResult(dtLabTest);
                }
            }
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

        /// <summary>
        /// Fields the validation.
        /// </summary>
        /// <param name="labtobedone">The labtobedone.</param>
        /// <param name="orderbydate">The orderbydate.</param>
        /// <param name="orderby">The orderby.</param>
        /// <param name="appcurrentdate">The appcurrentdate.</param>
        /// <returns></returns>
        private static Boolean FieldValidation(string labtobedone, string orderbydate, string orderby, string appcurrentdate)
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

                Session["DTLABTEST"] = (DataTable)ViewState["LabTestID"];
                LoadLabResult((DataTable)ViewState["LabTestID"]);
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
        /// Labs the test.
        /// </summary>
        /// <returns></returns>
        private DataTable LabTest()
        {
            DataTable dtLabTestFinal = new DataTable();
            dtLabTestFinal.Columns.Add("TestId", System.Type.GetType("System.Int32"));
            dtLabTestFinal.Columns.Add("TestNotes", System.Type.GetType("System.String"));
            DataRow drLabTestfinal;
            DataTable dtLabTest = (DataTable)HttpContext.Current.Session["DTLABTEST"];
            for (int i = 0; i < dtLabTest.Rows.Count; i++)
            {
                drLabTestfinal = dtLabTestFinal.NewRow();
                drLabTestfinal["TestId"] = Convert.ToInt32(dtLabTest.Rows[i]["TestId"].ToString());
                dtLabTestFinal.Rows.Add(drLabTestfinal);
            }
            return dtLabTestFinal;
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

        #region "AutoComplete Method"

        /// <summary>
        /// Gets the lab.
        /// </summary>
        /// <param name="prefixText">The prefix text.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public static List<Labs> GetLab(string prefixText, int count)
        {
            List<Labs> items = new List<Labs>();
            ILabFunctions LabTestsMgrDate;
            LabTestsMgrDate = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            //string query;

            //query = "Exec dbo.Laboratory_GetLabTestID @LabName='" + prefixText + "' ,@ExcludeLabDepartment=8";

            //  DataTable dt = LabTestsMgrDate.ReturnLabQuery(query);
            DataTable dt = new DataTable();
            dt = LabTestsMgrDate.FindLabByName(prefixText, null, 8);

            HttpContext.Current.Session["DTLABRESULT"] = dt;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        Labs item = new Labs();
                        item.SubTestId = (int)row["LabTestID"];
                        item.SubTestName = (string)row["LabName"];
                        item.LabDepartmentId = (int)row["LabDepartmentID"];
                        item.LabDepartmentName = (string)row["labdepartmentname"];
                        item.DataType = row["DataType"].ToString();
                        items.Add(item);
                    }
                    catch
                    {
                    }
                }
            }

            return items;
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
            List<Labs> lstDrugsDetail = GetLab(prefixText, count);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            foreach (Labs c in lstDrugsDetail)
            {
                Labdetail.Add(AutoCompleteExtender.CreateAutoCompleteItem(c.SubTestName, serializer.Serialize(c)));
            }

            return Labdetail;
        }

        #region "Class for Lab"

        /// <summary>
        ///
        /// </summary>
        public class Labs
        {
            protected string _dataType;

            /// <summary>
            /// The _ lab department identifier
            /// </summary>
            protected int _LabDepartmentId;

            /// <summary>
            /// The _ lab department name
            /// </summary>
            protected string _LabDepartmentName;

            /// <summary>
            /// The _ sub test identifier
            /// </summary>
            protected int _SubTestId;

            /// <summary>
            /// The _ sub test name
            /// </summary>
            protected string _SubTestName;

            public string DataType
            {
                get { return _dataType; }
                set { _dataType = value; }
            }

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
        }

        #endregion "Class for Lab"

        #endregion "AutoComplete Method"
    }
}