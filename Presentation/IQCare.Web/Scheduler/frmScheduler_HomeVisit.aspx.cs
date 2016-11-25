using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Scheduler;
using Interface.Security;

namespace IQCare.Web.Scheduler
{
    /// <summary>
    ///
    /// </summary>
    public partial class HomeVisit : System.Web.UI.Page
    {
        /////////////////////////////////////////////////////////////////////
        // Code Written By   : Nisha Nagpal
        // Written Date      : 16th Oct 2006
        // Modified By       : Rakhi Tyagi
        // Modification Date : 3th April 2007
        // Description       : Home Visit
        //
        /////////////////////////////////////////////////////////////////////

        private string enroll, theCreateDate;
        private int Homevisitid = 0;
        private string strCustomField = string.Empty;
        private DataTable theCustomDataDT = new DataTable();
        private int theNumofWeeks, theFlag, theTotVisit = 0, theCount = 0;
        private DateTime theStartDate, theCurrentDate, theIEDate;
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int PatientId = Convert.ToInt32(Session["PatientVisitId"]); //Convert.ToInt32(Request.QueryString["PatientId"]);
            string theUrl;
            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToUInt32(Session["PatientVisitId"]) == 0)
            {
                ////theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", PatientId);
                theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_Home.aspx");
            }
            else if (Request.QueryString["name"] == "Delete")
            {
                ////theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmClinical_DeleteForm.aspx", PatientId, Request.QueryString["sts"].ToString());
                theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_DeleteForm.aspx");
            }
            else ////if (Request.QueryString["name"] == "Edit")
            {
                ////theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmPatient_History.aspx", PatientId, Request.QueryString["sts"].ToString());
                theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_History.aspx");
            }
            Response.Redirect(theUrl);

        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            string msg = DQValidation1();
            if (msg.Length > 56)
            {
                MsgBuilder theBuilder1 = new MsgBuilder();
                theBuilder1.DataElements["MessageText"] = msg;
                IQCareMsgBox.Show("#C1", theBuilder1, this);
                return;
            }
            else
            {
                ViewState["DataQualityFlag"] = "1";
            }
            if (DQValidation() == false)
            {
                return;
            }
            if (VisitValidation() == false)
            {
                return;
            }
            //-----------------------
            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToUInt32(Session["PatientVisitId"]) == 0)
            {
                //if (Request.QueryString["name"] == "Add" && ViewState["HomeVisitId"] == null)
                if (Convert.ToUInt32(Session["PatientVisitId"]) == 0 && ViewState["HomeVisitId"] == null)
                {
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.HomeVisit, (DataSet)ViewState["CustomFieldsDS"]);
                }
                else
                {
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.HomeVisit, (DataSet)ViewState["CustomFieldsDS"]);
                }

                Save();
            }
            //else if(Request.QueryString["name"] != "Add" && Request.QueryString["name"] != "Delete")  ////if (Request.QueryString["name"] == "Edit")
            else if (Convert.ToUInt32(Session["PatientVisitId"]) > 0 && Request.QueryString["name"] != "Delete")
            {
                {
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.HomeVisit, (DataSet)ViewState["CustomFieldsDS"]);
                    RecordUpdate();
                }
            }
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Delete")
            {
                DeleteForm();
            }
            else
            {
                if (SaveValidation() == false)
                {
                    int theNum = Convert.ToInt32(ViewState["WeeksNumber"]);
                    ShowHideData(theNum);
                    return;
                }
                if (VisitValidation() == false)
                {
                    int theNum = Convert.ToInt32(ViewState["WeeksNumber"]);
                    ShowHideData(theNum);
                    return;
                }
                ViewState["DataQualityFlag"] = 0;

                // HomeVisitManager = (IHomeVisit)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BHomeVisit,BusinessProcess.Scheduler");

                //if (Request.QueryString["name"] == "Add")
                if (Convert.ToUInt32(Session["PatientVisitId"]) == 0)
                {
                    //if (Request.QueryString["name"] == "Add" && ViewState["HomeVisitId"] == null)
                    if (Convert.ToUInt32(Session["PatientVisitId"]) == 0 && ViewState["HomeVisitId"] == null)
                    {
                        CustomFieldClinical theCustomManager = new CustomFieldClinical();
                        theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.HomeVisit, (DataSet)ViewState["CustomFieldsDS"]);
                    }
                    else
                    {
                        CustomFieldClinical theCustomManager = new CustomFieldClinical();
                        theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.HomeVisit, (DataSet)ViewState["CustomFieldsDS"]);
                    }

                    Save();
                }
                else ////if (Request.QueryString["name"] == "Edit")
                {
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.HomeVisit, (DataSet)ViewState["CustomFieldsDS"]);
                    RecordUpdate();
                }
            }
        }

        protected void Check_IE()
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]); ////Convert.ToInt32(Request.QueryString["PatientId"]);
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans=true;\n";
            script += "alert('No IE Exists For Patient');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='../ClinicalForms/frmPatient_Home.aspx?PatientId=" + PatientID + "';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        protected void checkifblank()
        {
            if (txtNumofWeeks.Text == "")
            {
                ViewState["numweek"] = 9999;
            }
            else
            {
                ViewState["numweek"] = Convert.ToInt32(txtNumofWeeks.Text);
            }
            if (txtVisitPerWeek.Text == "")
            {
                ViewState["visitsperweek"] = 9999;
            }
            else
            {
                ViewState["visitsperweek"] = Convert.ToInt32(txtVisitPerWeek.Text);
            }
        }

        protected void GetCurrentDate()
        {
            IIQCareSystem SystemManager;
            SystemManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem,BusinessProcess.Security");
            theCurrentDate = SystemManager.SystemDate();
        }

        protected void GetFieldsforAdd(int Patient_ID)
        {
            IHomeVisit HomeVisitManager;
            try
            {
                HomeVisitManager = (IHomeVisit)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BHomeVisit,BusinessProcess.Scheduler");
                DataSet theDS = (DataSet)HomeVisitManager.GetFieldsforAdd(Patient_ID);
                if (theDS.Tables != null)
                {
                    if (theDS.Tables[1].Rows.Count <= 0)
                    {
                        Check_IE();
                    }
                    else
                    {
                        ViewState["IEVisitDate"] = theDS.Tables[1].Rows[0]["VisitDate"];
                    }
                    if (theDS.Tables[0].Rows.Count > 0)
                    {
                        //this.lblpatientname.Text = theDS.Tables[0].Rows[0]["Name"].ToString();
                        //this.lblexisclinicid.Text = theDS.Tables[0].Rows[0]["PatientClinicID"].ToString();
                        enroll = theDS.Tables[0].Rows[0]["PatientEnrollmentID"].ToString();
                        //this.lblpatientenrolment.Text = theDS.Tables[0].Rows[0]["CountryId"].ToString() + "-" + theDS.Tables[0].Rows[0]["PosId"].ToString() + "-" + theDS.Tables[0].Rows[0]["SatelliteId"].ToString() + "-" + enroll;
                        //Session["LocationId"] = theDS.Tables[0].Rows[0]["LocationID"].ToString();
                    }
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
                HomeVisitManager = null;
            }
        }

        protected void GetFieldsforEdit(int Patient_ID, int HomeVisitID, string strCustomField)
        {
            IHomeVisit HomeVisitManager;
            string selectValue1, selectValue2;
            int PatientID = Convert.ToInt32(Session["PatientId"]);  ////Convert.ToInt32(Request.QueryString["PatientId"]);

            try
            {
                HomeVisitManager = (IHomeVisit)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BHomeVisit,BusinessProcess.Scheduler");
                DataSet theDS = (DataSet)HomeVisitManager.GetFieldsforEdit(Patient_ID, HomeVisitID, theCustomDataDT);
                if (theDS.Tables[0].Rows.Count > 0)
                {
                    //this.lblpatientname.Text = theDS.Tables[0].Rows[0]["Name"].ToString();
                    //this.lblexisclinicid.Text = theDS.Tables[0].Rows[0]["PatientClinicID"].ToString();
                    //enroll = theDS.Tables[0].Rows[0]["PatientEnrollmentID"].ToString();
                    //this.lblpatientenrolment.Text = theDS.Tables[0].Rows[0]["CountryId"].ToString() + "-" + theDS.Tables[0].Rows[0]["PosId"].ToString() + "-" + theDS.Tables[0].Rows[0]["SatelliteId"].ToString() + "-" + enroll;
                    if (theDS.Tables[1].Rows[0]["hvPatientCHW"].ToString() == "")
                    {
                        selectValue1 = "0";
                        ddlCHW.SelectedValue = "0";
                    }
                    else
                    {
                        ddlCHW.SelectedValue = theDS.Tables[1].Rows[0]["hvPatientCHW"].ToString();
                        selectValue1 = theDS.Tables[1].Rows[0]["hvPatientCHW"].ToString();
                    }
                    if (theDS.Tables[1].Rows[0]["hvPatientAlternateCHW"].ToString() == "")
                    {
                        selectValue2 = "0";
                        ddlAlternateCHW.SelectedValue = "0";
                    }
                    else
                    {
                        ddlAlternateCHW.SelectedValue = theDS.Tables[1].Rows[0]["hvPatientAlternateCHW"].ToString();
                        selectValue2 = theDS.Tables[1].Rows[0]["hvPatientAlternateCHW"].ToString();
                    }

                    if ((ddlCHW.SelectedValue == "0") || (ddlAlternateCHW.SelectedValue == "0"))
                    {
                        fillDropDownList(Convert.ToInt32(selectValue1), Convert.ToInt32(selectValue2));
                        ddlCHW.SelectedValue = selectValue1;
                        ddlAlternateCHW.SelectedValue = selectValue2;
                    }

                    if (theDS.Tables[1].Rows[0]["hvDuration"].ToString() != "")
                    {
                        int theWeekNo = Convert.ToInt32(theDS.Tables[1].Rows[0]["hvDuration"].ToString());
                        ShowHideData(theWeekNo);
                    }

                    txtVisitPerWeek.Text = theDS.Tables[1].Rows[0]["hvVisitsPerWeek"].ToString();

                    if (theDS.Tables[1].Rows[0]["hvPerWeek1"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[1].Rows[0]["hvPerWeek1"].ToString() != "0")
                        {
                            VisitsPerWeek1.Value = theDS.Tables[1].Rows[0]["hvPerWeek1"].ToString();
                        }
                    }

                    if (theDS.Tables[1].Rows[0]["hvPerWeek2"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[1].Rows[0]["hvPerWeek2"].ToString() != "0")
                        {
                            VisitsPerWeek2.Value = theDS.Tables[1].Rows[0]["hvPerWeek2"].ToString();
                        }
                    }

                    if (theDS.Tables[1].Rows[0]["hvPerWeek3"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[1].Rows[0]["hvPerWeek3"].ToString() != "0")
                        {
                            VisitsPerWeek3.Value = theDS.Tables[1].Rows[0]["hvPerWeek3"].ToString();
                        }
                    }

                    if (theDS.Tables[1].Rows[0]["hvPerWeek4"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[1].Rows[0]["hvPerWeek4"].ToString() != "0")
                        {
                            VisitsPerWeek4.Value = theDS.Tables[1].Rows[0]["hvPerWeek4"].ToString();
                        }
                    }

                    if (theDS.Tables[1].Rows[0].IsNull("hvBeginDate"))
                    {
                        StartDate.Value = "";
                    }
                    else
                    {
                        DateTime theBeginDate = (DateTime)theDS.Tables[1].Rows[0]["hvBeginDate"];
                        StartDate.Value = theBeginDate.ToString(Session["AppDateFormat"].ToString());
                    }

                    if (theDS.Tables[1].Rows[0]["DataQuality"] != System.DBNull.Value && theDS.Tables[1].Rows[0]["DataQuality"].ToString() == "1")
                    {
                        btnComplete.CssClass = "greenbutton";
                    }
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
                HomeVisitManager = null;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //RTyagi..19Feb.07
            /***************** Check For User Access Rights ****************/
            AuthenticationManager Authentiaction = new AuthenticationManager();
            FillDropDowns();
            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToUInt32(Session["PatientVisitId"]) == 0)
            {
                //To Check Access Right..
                if (Authentiaction.HasFunctionRight(ApplicationAccess.HomeVisit, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnSave.Enabled = false;
                    btnComplete.Enabled = false;
                }
            }
            else if (Request.QueryString["name"] == "Delete")
            {
                btnSave.Text = "Delete";
                string strCustomField = string.Empty;
                ////GetFieldsforEdit(Convert.ToInt32(Request.QueryString["patientid"]), Convert.ToInt32(Request.QueryString["OrderId"]), strCustomField);
                GetFieldsforEdit(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]), strCustomField);

                if (Authentiaction.HasFunctionRight(ApplicationAccess.HomeVisit, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    ////int PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                    int PatientID = Convert.ToInt32(Session["PatientId"]);
                    string theUrl = "";
                    ////theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmClinical_DeleteForm.aspx", PatientID, Request.QueryString["sts"].ToString());
                    theUrl = string.Format("{0}", "../ClinicalForms/frmClinical_DeleteForm.aspx");
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.HomeVisit, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                {
                    btnSave.Text = "Delete";
                    btnSave.Enabled = false;
                    btnComplete.Visible = false;
                }
            }
            else ////if (Request.QueryString["name"] == "Edit")
            {
                string strCustomField = string.Empty;
                ////GetFieldsforEdit(Convert.ToInt32(Request.QueryString["patientid"]), Convert.ToInt32(Request.QueryString["OrderId"]), strCustomField);
                GetFieldsforEdit(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["PatientVisitId"]), strCustomField);

                ////To Check Access Right..
                if (Authentiaction.HasFunctionRight(ApplicationAccess.HomeVisit, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    int PatientID = Convert.ToInt32(Session["PatientId"]); ////Convert.ToInt32(Request.QueryString["PatientId"]);
                    string theUrl = "";
                    ////theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmPatient_History.aspx", PatientID, Request.QueryString["sts"].ToString());
                    theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_History.aspx");
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.HomeVisit, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btnSave.Enabled = false;
                    btnComplete.Enabled = false;
                }
            }

            //AddFieldAttributes();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
         
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Home Visit";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Home Visit";

            CareEndedStatus();

            AddFieldAttributes();
            PutCustomControl(); //Added on 16th June 2009 -- Custom Field
            if (!Page.IsPostBack)
            {
                StartDate.Focus();
                IQCareUtils theUtils = new IQCareUtils();

                ViewState["UserID"] = Session["AppUserId"];
                ViewState["Patientid"] = Convert.ToInt32(Session["PatientId"]); ////Convert.ToInt32(Request.QueryString["patientid"]);
                lblH2.Text = Request.QueryString["name"];
                MsgBuilder theBuilder = new MsgBuilder();
                MsgBuilder theDQ = new MsgBuilder();

                if (Convert.ToString(Request.QueryString["name"]) == "Delete")
                {
                    theBuilder.DataElements["FormName"] = "Home Visit Record";
                    IQCareMsgBox.ShowConfirm("DeleteForm", theBuilder, btnSave);
                }

                //if (lblH2.Text == "Add")
                if (Convert.ToUInt32(Session["PatientVisitId"]) == 0)
                {
                    lblH2.Text = "Add Home Visit";
                    GetFieldsforAdd(Convert.ToInt32(ViewState["Patientid"]));
                    Session["PatientVisitId"] = 0;
                    
                }
                else if (lblH2.Text == "Delete")
                {
                    lblH2.Text = "Delete Home Visit";
                    ViewState["theHomeVisitID"] = Convert.ToInt32(Session["PatientVisitId"]);
                    btnSave.Text = "Delete";
                    btnComplete.Visible = false;
                }
                else //if (lblH2.Text == "Edit")
                {
                    ViewState["theHomeVisitID"] = Convert.ToInt32(Session["PatientVisitId"]); 
                    lblH2.Text = "Edit Home Visit";

                   
                }
            }
            else if (txtNumofWeeks.Text != "")
            {
                ViewState["WeeksNumber"] = txtNumofWeeks.Text;

                if (txtNumofWeeks.Text != "")
                {
                    int theWeeks = Convert.ToInt32(txtNumofWeeks.Text);
                    ShowHideData(theWeeks);
                }
            }

            if (Convert.ToUInt32(Session["PatientVisitId"]) > 0 || (Request.QueryString["name"] == "Delete"))
            {
                Int32 PID = Convert.ToInt32(Session["PatientId"]); ////Convert.ToInt32(Request.QueryString["patientid"].ToString());
                FillOldData(PID);
            }

            Form.EnableViewState = true;
        }
        protected void theBtn_Click(object sender, EventArgs e)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]); ////Convert.ToInt32(Request.QueryString["PatientId"]);
            string theUrl = "";

            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToUInt32(Session["PatientVisitId"]) == 0)
            {
                ////theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", PatientID);
                theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_Home.aspx");
            }
            else
            {
                ////theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmPatient_History.aspx", PatientID, Request.QueryString["sts"].ToString());
                theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_History.aspx");
            }
            Response.Redirect(theUrl);
        }

        protected void theBtn1_Click(object sender, EventArgs e)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]); ////Convert.ToInt32(Request.QueryString["PatientId"]);
            string theUrl = "";
            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToUInt32(Session["PatientVisitId"]) == 0)
            {
                ////theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", PatientID);
                theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_Home.aspx");
            }
            else
            {
                ////theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmPatient_History.aspx", PatientID, Request.QueryString["sts"].ToString());
                theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_History.aspx");
            }
            Response.Redirect(theUrl);
        }

        protected void txtNumofWeeks_change(object sender, EventArgs e)
        {
            if (txtNumofWeeks.Text != "")
            {
                ShowHideData(Convert.ToInt32(txtNumofWeeks.Text));
            }
        }

        private void AddFieldAttributes()
        {

            txtNumofWeeks.Attributes.Add("onkeyup", "GetWeeks('" + txtNumofWeeks.ClientID + "')");
            txtNumofWeeks.Attributes.Add("onkeyup", "chkNumber('" + txtNumofWeeks.ClientID + "')");
            txtVisitPerWeek.Attributes.Add("onkeyup", "chkNumber('" + txtVisitPerWeek.ClientID + "')");

            StartDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            StartDate.Attributes.Add("OnKeyUp", "DateFormat(this,this.value,event,false,'3')");
            txtNumofWeeks.Attributes.Add("onblur", "isBetween('" + txtNumofWeeks.ClientID + "', '" + "Number of weeks" + "', '" + 0 + "','" + 4 + "')");
            txtVisitPerWeek.Attributes.Add("onblur", "isBetween('" + txtVisitPerWeek.ClientID + "', '" + "Number of visit per week" + "', '" + 0 + "','" + 21 + "')");
        }

        private void CareEndedStatus()
        {
            if (Session["PatientStatus"] != null)
            {
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Convert.ToString(Session["PatientStatus"]); ////Request.QueryString["sts"].ToString();
            }
            else
            {
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = "0";
            }
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Convert.ToString(Session["PatientStatus"]); ////Request.QueryString["sts"].ToString();
            if ((Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text == "1" && Convert.ToUInt32(Session["PatientVisitId"]) > 0)
            {
                btnSave.Enabled = false;
                btnComplete.Enabled = false;
            }
        }
        /*************Get Current Date ***********/
        /************** Validate Function For Save *************/

        private void DeleteForm()
        {
            IHomeVisit HomeVisitManager;
            int theResultRow, OrderNo;
            string FormName;
            ////OrderNo = Convert.ToInt32(Request.QueryString["OrderId"].ToString());
            OrderNo = Convert.ToInt32(Session["PatientVisitId"]);
            FormName = "Home Visit";

            HomeVisitManager = (IHomeVisit)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BHomeVisit,BusinessProcess.Scheduler");

            ////theResultRow = (int)HomeVisitManager.DeleteHomeVisitForms(FormName, OrderNo, Convert.ToInt32(Request.QueryString["PatientId"].ToString()), Convert.ToInt32(ViewState["UserID"]));
            theResultRow = (int)HomeVisitManager.DeleteHomeVisitForms(FormName, OrderNo, Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(ViewState["UserID"]));

            if (theResultRow == 0)
            {
                IQCareMsgBox.Show("RemoveFormError", this);
                return;
            }
            else
            {
                string theUrl;
                ////theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", Request.QueryString["PatientId"].ToString());
                theUrl = string.Format("{0}", "../ClinicalForms/frmPatient_Home.aspx");
                Response.Redirect(theUrl);
            }
        }

        private void DQCancel()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('DQ Checked complete.\\n Form Marked as DQ Checked.\\n Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            //if (Request.QueryString["Name"] == "Add")
            if (Convert.ToUInt32(Session["PatientVisitId"]) == 0)
            {
                script += "window.location.href='../ClinicalForms/frmPatient_Home.aspx';\n";
                //script += "window.location.href('frmPatient_Home.aspx?PatientId=" + ViewState["PtnID"].ToString() + "');\n";
            }
            else
            {
                script += "window.location.href='../ClinicalForms/frmPatient_History.aspx';\n";
                //script += "window.location.href('frmPatient_History.aspx?PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + "0" + "');\n";
            }
            script += "}\n";
            script += "else \n";
            script += "{\n";
            //script += "window.location.href('frmClinical_EnrolmentPMTCT.aspx?name=Edit&PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + ((Session["fmsts"] == null) ? "0".ToString() : Session["fmsts"].ToString()) + "');\n";
            script += "window.location.href='frmScheduler_HomeVisit.aspx';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        private Boolean DQValidation()
        {
            GetCurrentDate();
            IQCareUtils theUtils = new IQCareUtils();
            if (StartDate.Value != "")
            {
                theStartDate = Convert.ToDateTime(theUtils.MakeDate(StartDate.Value));
                if (theStartDate > theCurrentDate)
                {
                    IQCareMsgBox.Show("HomeVisitCompareDate", this);
                    StartDate.Focus();
                    return false;
                }
            }
            if (ViewState["IEVisitDate"] != null)
            {
                theIEDate = Convert.ToDateTime(ViewState["IEVisitDate"].ToString());

                if (theStartDate < theIEDate)
                {
                    IQCareMsgBox.Show("HomeVisitCompareIEDate", this);
                    StartDate.Focus();
                    return false;
                }
            }
            return true;
        }

        private string DQValidation1()
        {
            // string strmsg = "";
            // string strmsg = "Following values are required to complete the data quality check:\\n\\n";
            if (StartDate.Value == "")
            {
                string strmsg = "Following values are required to complete the data quality check:\\n\\n";
                string scriptHomeVisitDate = "<script language = 'javascript' defer ='defer' id = 'ColorVisitDate'>\n";
                scriptHomeVisitDate += "To_Change_Color('Vdate');\n";
                scriptHomeVisitDate += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ColorVisitDate", scriptHomeVisitDate);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Start Date";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg = strmsg + "\\n";
                return strmsg;
            }
            return "";
        }

        private void fillDropDownList(int idCHW, int idAlternateCHW)
        {
            IAppointment FormManager;
            BindFunctions appBind;
            DataSet theDtSetCHW;
            DataSet theDtSetAlternateCHW;

            //*******Get the patient details on the basis of Patient Enrollment Id and show the details.*******//
            FormManager = (IAppointment)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BAppointment, BusinessProcess.Scheduler");

            theDtSetCHW = FormManager.GetEmployees(idCHW);
            appBind = new BindFunctions();
            appBind.BindCombo(ddlCHW, theDtSetCHW.Tables[0], "EmployeeName", "EmployeeId");

            theDtSetAlternateCHW = FormManager.GetEmployees(idAlternateCHW);
            appBind = new BindFunctions();
            appBind.BindCombo(ddlAlternateCHW, theDtSetAlternateCHW.Tables[0], "EmployeeName", "EmployeeId");
        }

        private void FillDropDowns()
        {
            DataSet DStheXML = new DataSet();
            DStheXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            BindFunctions appBind = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataView theDV = new DataView(DStheXML.Tables["Mst_Employee"]);
            //if (Request.QueryString["Name"] == "Add")
            if (Convert.ToUInt32(Session["PatientVisitId"]) == 0)
            {
                theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    appBind.BindCombo(ddlCHW, theDT, "EmployeeName", "EmployeeId");
                    appBind.BindCombo(ddlAlternateCHW, theDT, "EmployeeName", "EmployeeId");
                    theDV.Dispose();
                    theDT.Clear();
                }
            }
            else ////if (Request.QueryString["name"] == "Edit" || Request.QueryString["name"] == "Delete")
            {
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    appBind.BindCombo(ddlCHW, theDT, "EmployeeName", "EmployeeId");
                    appBind.BindCombo(ddlAlternateCHW, theDT, "EmployeeName", "EmployeeId");
                    theDV.Dispose();
                    theDT.Clear();
                }
            }
        }

        private void FillOldData(Int32 PatID)
        {
            DataSet dsvalues = null;
            ICustomFields CustomFields;
            try
            {
                DataSet theCustomFields = (DataSet)ViewState["CustomFieldsDS"];
                string theTblName = "";
                if (theCustomFields.Tables[0].Rows.Count > 0)
                    theTblName = theCustomFields.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");
                string theColName = "";
                foreach (DataRow theDR in theCustomFields.Tables[0].Rows)
                {
                    if (theDR["ControlId"].ToString() != "9")
                    {
                        if (theColName == "")
                            theColName = theDR["Label"].ToString();
                        else
                            theColName = theColName + "," + theDR["Label"].ToString();
                    }
                }
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
              dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), Convert.ToInt32(Session["PatientVisitId"]), 0, 0, 0, Convert.ToInt32(ApplicationAccess.HomeVisit));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "HomeVisit");
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
                CustomFields = null;
            }
        }

        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.HomeVisit));
                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "HomeVisit");
                    
                }
                //theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "HomeVisit");
                ViewState["CustomFieldsDS"] = theDS;
                pnlCustomList.Visible = true;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
                CustomFields = null;
            }
        }

        private void RecordUpdate()
        {
            //string strCustomField = string.Empty;
            checkifblank();
            int LocationID;
            IHomeVisit HomeVisitManager;

            DataSet theDSResult = new DataSet();
            IQCareUtils theUtils = new IQCareUtils();

            try
            {
                theFlag = 1;
                HomeVisitManager = (IHomeVisit)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BHomeVisit,BusinessProcess.Scheduler");
                DataSet theDSUpdate = new DataSet();
                if (ViewState["theUpdateDS"] != null)
                {
                    theDSUpdate = (DataSet)ViewState["theUpdateDS"];
                }

                LocationID = Convert.ToInt32(Session["ServiceLocationId"]); ////Convert.ToInt32(Request.QueryString["locationid"]);
                theDSResult = HomeVisitManager.SaveHomeVisit(LocationID, Convert.ToInt32(ViewState["Patientid"]), ddlCHW.SelectedValue, ddlAlternateCHW.SelectedValue, Convert.ToInt32(VisitsPerWeek1.Value), Convert.ToInt32(VisitsPerWeek2.Value), Convert.ToInt32(VisitsPerWeek3.Value), Convert.ToInt32(VisitsPerWeek4.Value), Convert.ToInt32(ViewState["visitsperweek"]), Convert.ToInt32(ViewState["numweek"]), Convert.ToDateTime(StartDate.Value), Convert.ToInt32(ViewState["UserID"]), Convert.ToInt32(ViewState["theHomeVisitID"]), Convert.ToInt32(theFlag), Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT);
                //theDSResult = HomeVisitManager.SaveHomeVisit(LocationId, Convert.ToInt32(ViewState["Patientid"]), ddlCHW.SelectedValue, ddlAlternateCHW.SelectedValue, Convert.ToInt32(VisitsPerWeek1.Value), Convert.ToInt32(VisitsPerWeek2.Value), Convert.ToInt32(VisitsPerWeek3.Value), Convert.ToInt32(VisitsPerWeek4.Value), Convert.ToInt32(ViewState["visitsperweek"]), Convert.ToInt32(ViewState["numweek"]), Convert.ToDateTime(StartDate.Value), Convert.ToInt32(ViewState["UserID"]), Convert.ToInt32(ViewState["theHomeVisitID"]), Convert.ToInt32(theFlag), Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT);
                ViewState["theUpdateDS"] = theDSResult;
                if (ViewState["DataQualityFlag"] != null && ViewState["DataQualityFlag"].ToString() == "1")
                {
                    
                    DQCancel();
                    btnComplete.CssClass = "greenbutton";
                    return;
                }
                else if (ViewState["DataQualityFlag"] != null && ViewState["DataQualityFlag"].ToString() == "0")
                {
                    
                    SaveCancel();
                    return;
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
                HomeVisitManager = null;
            }
        }

        private void Save()
        {
            checkifblank();

            IHomeVisit HomeVisitManager;
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDT = new DataSet();

            try
            {
                //--- custom field----

                theCreateDate = theUtils.MakeDate("01-01-1900");
                theFlag = 0;
                HomeVisitManager = (IHomeVisit)ObjectFactory.CreateInstance("BusinessProcess.Scheduler.BHomeVisit,BusinessProcess.Scheduler");
                DataSet theViewDS = new DataSet();
                if (ViewState["theSaveDS"] != null)
                {
                    theViewDS = (DataSet)ViewState["theSaveDS"];
                }
                if ((ViewState["theSaveDS"] == null) || (theViewDS.Tables.Count == 0))
                {
                    //theDT = (DataSet)HomeVisitManager.SaveHomeVisit(Convert.ToInt32(Session["LocationId"].ToString()), Convert.ToInt32(ViewState["Patientid"]), ddlCHW.SelectedValue, ddlAlternateCHW.SelectedValue, Convert.ToInt32(VisitsPerWeek1.Value), Convert.ToInt32(VisitsPerWeek2.Value), Convert.ToInt32(VisitsPerWeek3.Value), Convert.ToInt32(VisitsPerWeek4.Value), Convert.ToInt32(ViewState["visitsperweek"]), Convert.ToInt32(ViewState["numweek"]), Convert.ToDateTime(StartDate.Value), Convert.ToInt32(ViewState["UserID"]), Homevisitid, Convert.ToInt32(theFlag), Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT);
                    theDT = (DataSet)HomeVisitManager.SaveHomeVisit(Convert.ToInt32(Session["AppLocationId"].ToString()), Convert.ToInt32(ViewState["Patientid"]), ddlCHW.SelectedValue, ddlAlternateCHW.SelectedValue, Convert.ToInt32(VisitsPerWeek1.Value), Convert.ToInt32(VisitsPerWeek2.Value), Convert.ToInt32(VisitsPerWeek3.Value), Convert.ToInt32(VisitsPerWeek4.Value), Convert.ToInt32(ViewState["visitsperweek"]), Convert.ToInt32(ViewState["numweek"]), Convert.ToDateTime(StartDate.Value), Convert.ToInt32(ViewState["UserID"]), Homevisitid, Convert.ToInt32(theFlag), Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT);
                    ViewState["theSaveDS"] = theDT;
                    ViewState["HomeVisitId"] = theDT.Tables[1].Rows[0][0].ToString();
                    Session["PatientVisitId"] = ViewState["HomeVisitId"];
                    if (theDT.Tables[0].Rows[0][0].ToString() != "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["MessageText"] = theDT.Tables[0].Rows[0][0].ToString();
                        IQCareMsgBox.Show("#C1", theBuilder, this);
                        return;
                    }
                    if (ViewState["DataQualityFlag"] != null && ViewState["DataQualityFlag"].ToString() == "1")
                    {
                       
                        DQCancel();
                        //btnComplete.ControlStyle.BackColor = System.Drawing.Color.Green;
                        btnComplete.CssClass = "greenbutton";
                        //btnComplete.ControlStyle.BackColor = System.Drawing.Color.Green;
                        return;
                    }
                    else if (ViewState["DataQualityFlag"] != null && ViewState["DataQualityFlag"].ToString() == "0")
                    {
                        SaveCancel();
                        return;
                    }
                }
                else
                {
                    if (theViewDS.Tables.Count == 2)
                    {
                        Homevisitid = Convert.ToInt32(theViewDS.Tables[1].Rows[0][0].ToString());
                    }
                    theFlag = 1;
                    theDT = (DataSet)HomeVisitManager.SaveHomeVisit(Convert.ToInt32(Session["LocationId"].ToString()), Convert.ToInt32(ViewState["Patientid"]), ddlCHW.SelectedValue, ddlAlternateCHW.SelectedValue, Convert.ToInt32(VisitsPerWeek1.Value), Convert.ToInt32(VisitsPerWeek2.Value), Convert.ToInt32(VisitsPerWeek3.Value), Convert.ToInt32(VisitsPerWeek4.Value), Convert.ToInt32(ViewState["visitsperweek"]), Convert.ToInt32(ViewState["numweek"]), Convert.ToDateTime(StartDate.Value), Convert.ToInt32(ViewState["UserID"]), Homevisitid, Convert.ToInt32(theFlag), Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT);
                    if (ViewState["DataQualityFlag"] != null && ViewState["DataQualityFlag"].ToString() == "1")
                    {
                        DQCancel();
                        
                        btnComplete.CssClass = "greenbutton";
                        return;
                    }
                    else if (ViewState["DataQualityFlag"] != null && ViewState["DataQualityFlag"].ToString() == "0")
                    {
                        SaveCancel();
                        return;
                    }
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
                HomeVisitManager = null;
            }
        }

        private void SaveCancel()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Home Visit Form saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            //if (Request.QueryString["Name"] == "Add")
            if (Convert.ToUInt32(Session["PatientVisitId"]) == 0)
            {
                script += "window.location.href='../ClinicalForms/frmPatient_Home.aspx';\n";
             
            }
            else
            {
                script += "window.location.href='../ClinicalForms/frmPatient_History.aspx';\n";
                //script += "window.location.href('frmPatient_History.aspx?PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + "0" + "');\n";
            }
            script += "}\n";
            script += "else \n";
            script += "{\n";
            //script += "window.location.href('frmClinical_EnrolmentPMTCT.aspx?name=Edit&PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + ((Session["fmsts"] == null) ? "0".ToString() : Session["fmsts"].ToString()) + "');\n";
            script += "window.location.href='frmScheduler_HomeVisit.aspx';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        private Boolean SaveValidation()
        {
            IQCareUtils theUtils = new IQCareUtils();
            GetCurrentDate();
            if (StartDate.Value != "")
            {
                theStartDate = Convert.ToDateTime(theUtils.MakeDate(StartDate.Value));
            }
            else if (StartDate.Value == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Start Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                StartDate.Focus();
                return false;
            }
            if (theStartDate > theCurrentDate)
            {
                IQCareMsgBox.Show("HomeVisitCompareDate", this);
                StartDate.Focus();
                return false;
            }
            if (ViewState["IEVisitDate"] != null)
            {
                theIEDate = Convert.ToDateTime(ViewState["IEVisitDate"].ToString());

                if (theStartDate < theIEDate)
                {
                    IQCareMsgBox.Show("HomeVisitCompareIEDate", this);
                    StartDate.Focus();
                    return false;
                }
            }

            return true;
        }

        private void ShowHideData(int theWeeks)
        {
            if (theWeeks.ToString() != "")
            {
                txtNumofWeeks.Text = theWeeks.ToString();

                string script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'ShowWeeks'>\n";
                script += "show('VisitPerWeekShow');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowWeeks", script);
            }
            if (theWeeks.ToString() == "1")
            {
                string script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'ShowWeeks1'>\n";
                script += "show('VisitPerWeekShow1');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowWeeks1", script);
            }
            else if (theWeeks.ToString() == "2")
            {
                string script = "";
                script = "<script language = 'javascript'  defer ='defer' id = 'ShowWeeks1'>\n";
                script += "show('VisitPerWeekShow1');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowWeeks1", script);

                string script1 = "";
                script1 = "<script language = 'javascript' defer ='defer' id = 'ShowWeeks2'>\n";
                script1 += "show('VisitPerWeekShow2');\n";
                script1 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowWeeks2", script1);
            }
            else if (theWeeks.ToString() == "3")
            {
                string script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'ShowWeeks1'>\n";
                script += "show('VisitPerWeekShow1');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowWeeks1", script);

                string script1 = "";
                script1 = "<script language = 'javascript' defer ='defer' id = 'ShowWeeks2'>\n";
                script1 += "show('VisitPerWeekShow2');\n";
                script1 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowWeeks2", script1);

                string script3 = "";
                script3 = "<script language = 'javascript' defer ='defer' id = 'ShowWeeks3'>\n";
                script3 += "show('VisitPerWeekShow3');\n";
                script3 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowWeeks3", script3);
            }
            else if (theWeeks.ToString() == "4")
            {
                string script = "";
                script = "<script language = 'javascript'  defer ='defer' id = 'ShowWeeks1'>\n";
                script += "show('VisitPerWeekShow1');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowWeeks1", script);

                string script1 = "";
                script1 = "<script language = 'javascript' defer ='defer' id = 'ShowWeeks2'>\n";
                script1 += "show('VisitPerWeekShow2');\n";
                script1 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowWeeks2", script1);

                string script3 = "";
                script3 = "<script language = 'javascript' defer ='defer' id = 'ShowWeeks3'>\n";
                script3 += "show('VisitPerWeekShow3');\n";
                script3 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowWeeks3", script3);

                string script4 = "";
                script4 = "<script language = 'javascript' defer ='defer' id = 'ShowWeeks4'>\n";
                script4 += "show('VisitPerWeekShow4');\n";
                script4 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ShowWeeks4", script4);
            }
        }

        private Boolean VisitValidation()
        {
            if (txtNumofWeeks.Text == "")
            {
                if (txtVisitPerWeek.Text != "")
                {
                    IQCareMsgBox.Show("RequireWeeks", this);
                    return false;
                }
            }
            else
            {
                if (txtVisitPerWeek.Text != "")
                {
                    theNumofWeeks = Convert.ToInt32(txtNumofWeeks.Text);
                }

                if (theNumofWeeks == 1)
                {
                    if (VisitsPerWeek1.SelectedIndex != 0)
                    {
                        theCount = Convert.ToInt32(VisitsPerWeek1.Value);
                        VisitsPerWeek2.SelectedIndex = 0;
                        VisitsPerWeek3.SelectedIndex = 0;
                        VisitsPerWeek4.SelectedIndex = 0;
                    }
                }
                else if (theNumofWeeks == 2)
                {
                    if (VisitsPerWeek1.SelectedIndex != 0)
                    {
                        theCount = Convert.ToInt32(VisitsPerWeek1.Value);
                    }
                    if (VisitsPerWeek2.SelectedIndex != 0)
                    {
                        theCount = theCount + Convert.ToInt32(VisitsPerWeek2.Value);
                    }
                    VisitsPerWeek3.SelectedIndex = 0;
                    VisitsPerWeek4.SelectedIndex = 0;
                }
                else if (theNumofWeeks == 3)
                {
                    if (VisitsPerWeek1.SelectedIndex != 0)
                    {
                        theCount = Convert.ToInt32(VisitsPerWeek1.Value);
                    }
                    if (VisitsPerWeek2.SelectedIndex != 0)
                    {
                        theCount = theCount + Convert.ToInt32(VisitsPerWeek2.Value);
                    }
                    if (VisitsPerWeek3.SelectedIndex != 0)
                    {
                        theCount = theCount + Convert.ToInt32(VisitsPerWeek3.Value);
                    }
                    VisitsPerWeek4.SelectedIndex = 0;
                }
                else if (theNumofWeeks == 4)
                {
                    if (VisitsPerWeek1.SelectedIndex != 0)
                    {
                        theCount = Convert.ToInt32(VisitsPerWeek1.Value);
                    }
                    if (VisitsPerWeek2.SelectedIndex != 0)
                    {
                        theCount = theCount + Convert.ToInt32(VisitsPerWeek2.Value);
                    }
                    if (VisitsPerWeek3.SelectedIndex != 0)
                    {
                        theCount = theCount + Convert.ToInt32(VisitsPerWeek3.Value);
                    }
                    if (VisitsPerWeek4.SelectedIndex != 0)
                    {
                        theCount = theCount + Convert.ToInt32(VisitsPerWeek4.Value);
                    }
                }
                if (txtNumofWeeks.Text != "" && txtVisitPerWeek.Text != "")
                {
                    theTotVisit = theNumofWeeks * Convert.ToInt32(txtVisitPerWeek.Text);
                    if (theCount > theTotVisit)
                    {
                        IQCareMsgBox.Show("NotGreaterthanTotDays", this);
                        txtVisitPerWeek.Focus();
                        return false;
                    }
                    else if (theCount <= 0 && theTotVisit > 0)
                    {
                        IQCareMsgBox.Show("SelectWeeklyVisit", this);
                        VisitsPerWeek1.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

    }
}