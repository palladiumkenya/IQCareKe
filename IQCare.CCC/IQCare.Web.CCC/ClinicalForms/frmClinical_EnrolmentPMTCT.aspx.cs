using System;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;
using Interface.Security;
/// /////////////////////////////////////////////////////////////////////
// Code Written By   : Jayanta Kumar Das
// Written Date      :  2009
// Description       : Patient Registration Form - PMTCT
//
/// /////////////////////////////////////////////////////////////////
namespace IQCare.Web.Clinical
{
    public partial class EnrolmentPMTCT : BasePage
    {
        string ObjFactoryParameter = "BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical";
     

        private Boolean FieldValidation()
        {
            IIQCareSystem IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
            IQCareUtils theUtils = new IQCareUtils();

            if (TxtFirstName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "First Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                TxtFirstName.Focus();
                return false;
            }
            if (TxtLastName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Last Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                TxtLastName.Focus();
                return false;
            }

            if (TxtRegistrationDate.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Registration Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                TxtRegistrationDate.Focus();
                return false;
            }
            DateTime theEnrolDate = Convert.ToDateTime(theUtils.MakeDate(TxtRegistrationDate.Text));
            if (theEnrolDate > theCurrentDate)
            {
                IQCareMsgBox.Show("EnrolDate", this);
                TxtRegistrationDate.Focus();
                return false;
            }

            if (DDGender.SelectedValue == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Sex";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                DDGender.Focus();
                return false;
            }

            if (TxtDOB.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "DOB";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                TxtDOB.Focus();
                return false;
            }
            DateTime theDOBDate = Convert.ToDateTime(theUtils.MakeDate(TxtDOB.Text));
            if (theDOBDate > theCurrentDate)
            {
                IQCareMsgBox.Show("DOBDate", this);
                TxtDOB.Focus();
                return false;
            }

            if (theDOBDate > theEnrolDate)
            {
                IQCareMsgBox.Show("DOB_EnrolDate", this);
                return false;
            }
            /////17-09-09
            //if (chkTransferIn.Checked == true && ddReferredFrom.SelectedIndex != 0)
            //{
            //   // //MsgBuilder theBuilder = new MsgBuilder();
            //   // theBuilder.DataElements["Control"] = "RefFrom";
            //   //// IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
            //    return false;
            //  }


            ////17-09-09
            if (chkTransferIn.Checked == false && ddReferredFrom.SelectedIndex != 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Either Tranfer in/Referred From";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                return false;
            }

            if (chkTransferIn.Checked == true && ddReferredFrom.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Either Tranfer in/Referred From";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                return false;
            }

            if (TxtANCNumber.Text == "" && TxtPMTCTNumber.Text == "" && TxtAdmissionNumber.Text == "" && TxtOutPatientNumber.Text == "")
            {
                IQCareMsgBox.Show("PMTCTEnrol_ANC", this);
                return false;
            }

            IPatientRegistration ValMgrPMTCT = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
            string msg = "";
            if (TxtANCNumber.Text != "")
            {
                DataTable theDT = ValMgrPMTCT.Validate(TxtANCNumber.Text, "ANC");

                if (theDT.Rows.Count > 0)
                {
                    //if (Request.QueryString["Name"] == "Edit")
                    if (Convert.ToInt32(Session["PatientId"]) > 0) //i.e. update mode
                    {
                        if (theDT.Rows[0]["IDNumber"].ToString() != Convert.ToString(ViewState["ANC"]))
                        {
                            msg = "ANC" + "' '" + IQCareMsgBox.GetMessage("PMTCTDuplicate", this);
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["MessageText"] = msg;
                            IQCareMsgBox.Show("#C1", theBuilder, this);
                            return false;

                        }
                    }
                    else
                    {
                        msg = "ANC" + "' '" + IQCareMsgBox.GetMessage("PMTCTDuplicate", this);
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["MessageText"] = msg;
                        IQCareMsgBox.Show("#C1", theBuilder, this);
                        return false;
                    }
                }
            }

            if (TxtPMTCTNumber.Text != "")
            {

                DataTable theDT = ValMgrPMTCT.Validate(TxtPMTCTNumber.Text, "PMTCT");
                if (theDT.Rows.Count > 0)
                {
                    //if (Request.QueryString["Name"] == "Edit")
                    //if (Request.QueryString["Name"] != "Add") //i.e. edit mode
                    if (Convert.ToInt32(Session["PatientId"]) > 0)
                    {
                        if (theDT.Rows[0]["IDNumber"].ToString() != ViewState["PMTCT"].ToString())
                        {
                            msg = "PMTCT" + "' '" + IQCareMsgBox.GetMessage("PMTCTDuplicate", this);
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["MessageText"] = msg;
                            IQCareMsgBox.Show("#C1", theBuilder, this);
                            return false;

                        }
                    }
                    else
                    {
                        msg = "PMTCT" + "' '" + IQCareMsgBox.GetMessage("PMTCTDuplicate", this);
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["MessageText"] = msg;
                        IQCareMsgBox.Show("#C1", theBuilder, this);
                        return false;
                    }
                }

            }

            if (TxtAdmissionNumber.Text != "")
            {


                DataTable theDT = ValMgrPMTCT.Validate(TxtAdmissionNumber.Text, "Admission");
                if (theDT.Rows.Count > 0)
                {
                    //if (Request.QueryString["Name"] == "Edit")
                    //if (Request.QueryString["Name"] != "Add") //i.e. edit mode
                    if (Convert.ToInt32(Session["PatientId"]) > 0)
                    {
                        if (theDT.Rows[0]["IDNumber"].ToString() != ViewState["Admission"].ToString())
                        {
                            msg = "Admission" + "' '" + IQCareMsgBox.GetMessage("PMTCTDuplicate", this);
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["MessageText"] = msg;
                            IQCareMsgBox.Show("#C1", theBuilder, this);
                            return false;
                        }
                    }
                    else
                    {
                        msg = "Admission" + "' '" + IQCareMsgBox.GetMessage("PMTCTDuplicate", this);
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["MessageText"] = msg;
                        IQCareMsgBox.Show("#C1", theBuilder, this);
                        return false;
                    }
                }

            }
            if (TxtOutPatientNumber.Text != "")
            {
                DataTable theDT = ValMgrPMTCT.Validate(TxtOutPatientNumber.Text, "Outpatient");
                if (theDT.Rows.Count > 0)
                {
                    //if (Request.QueryString["Name"] == "Edit")
                    //if (Request.QueryString["Name"] != "Add") //i.e. edit mode
                    if (Convert.ToInt32(Session["PatientId"]) > 0)
                    {
                        if (theDT.Rows[0]["IDNumber"].ToString() != ViewState["Outpatient"].ToString())
                        {
                            msg = "OutpatientNumber" + "' '" + IQCareMsgBox.GetMessage("PMTCTDuplicate", this);
                            MsgBuilder theBuilder = new MsgBuilder();
                            theBuilder.DataElements["MessageText"] = msg;
                            IQCareMsgBox.Show("#C1", theBuilder, this);
                            return false;

                        }
                    }
                    else
                    {
                        msg = "OutpatientNumber" + "' '" + IQCareMsgBox.GetMessage("PMTCTDuplicate", this);
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["MessageText"] = msg;
                        IQCareMsgBox.Show("#C1", theBuilder, this);
                        return false;
                    }
                }
            }




            return true;
        }

        protected Hashtable AddUpdateData()
        {
            Hashtable theHT = new Hashtable();
            theHT.Clear();
            int iPatientId = (Session["PatientId"] != null) ? System.Convert.ToInt32(Session["PatientId"]) : 0;
            //if (Request.QueryString["Name"] == "Add" && Request.QueryString["PatientID"] == null)
            //if (Request.QueryString["Name"] == "Add" && iPatientId == 0)
            if (Convert.ToInt32(Session["PatientId"]) == 0)
            {
                theHT.Add("PatientID", "");

            }
            //else if (Request.QueryString["Name"] == "Add" && Request.QueryString["PatientID"] != null)
            //else if (Request.QueryString["Name"] == "Add" && iPatientId >0)
            else if (Convert.ToInt32(Session["PatientId"]) > 0)
            {
                ////int patientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                ////theHT.Add("PatientID", patientID);
                theHT.Add("PatientID", iPatientId);

            }

            //if (Request.QueryString["Name"] == "Edit")
            //if (Request.QueryString["Name"] != "Add")
            //////if(Convert.ToInt32(Session["PatientId"])>0)
            //////{
            //////    ////int patientID = Convert.ToInt32(Request.QueryString["PatientId"]);
            //////    ////theHT.Add("PatientID", patientID);
            //////    theHT.Add("PatientID", iPatientId);
            //////}

            if (RbtnDOBPrecEstimated.Checked == true)
            {
                theHT.Add("DOBPrecision", 1);
            }
            else if (RbtnDOBPrecExact.Checked == true)
            {
                theHT.Add("DOBPrecision", 0);
            }
            else
            {
                theHT.Add("DOBPrecision", 2);
            }

            theHT.Add("FName", TxtFirstName.Text);
            theHT.Add("MName", TxtMiddleName.Text);
            theHT.Add("LName", TxtLastName.Text);
            theHT.Add("RegDate", TxtRegistrationDate.Text);
            theHT.Add("Gender", DDGender.SelectedValue);
            theHT.Add("DOB", TxtDOB.Text);
            theHT.Add("Status", 0);
            theHT.Add("MStatus", DDMaritalStatus.SelectedValue);
            int TransferIn = this.chkTransferIn.Checked == true ? 1 : 0;
            theHT.Add("TransferIn", TransferIn);
            theHT.Add("RefFrom", ddReferredFrom.SelectedValue);
            theHT.Add("ANCNumber", TxtANCNumber.Text);
            theHT.Add("PMTCTNumber", TxtPMTCTNumber.Text);// New field is add to PMTCTS
            theHT.Add("Admission", TxtAdmissionNumber.Text);
            theHT.Add("OutpatientNumber", TxtOutPatientNumber.Text);
            theHT.Add("Address", TxtAddress.Text);
            theHT.Add("Village", ddVillageName.SelectedValue);
            theHT.Add("District", ddDistrict.SelectedValue);
            theHT.Add("Phone", TxtPhoneNumber.Text);
            theHT.Add("CountryId", Session["AppCountryId"].ToString());
            theHT.Add("POSId", Session["AppPosID"].ToString());
            theHT.Add("SatelliteId", Session["AppSatelliteId"].ToString());
            theHT.Add("LocationID", Session["AppLocationId"].ToString());
            theHT.Add("UserID", Session["AppUserId"].ToString());
            theHT.Add("VisitType", "11");
            theHT.Add("DataQuality", 0);
            return theHT;

        }

        protected void AddAttributes()
        {
            TxtRegistrationDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            TxtDOB.Attributes.Add("onkeyup", "DateFormat(this, this.value, event, false, '3')");
            txtSysDate.Text = Application["AppCurrentDate"].ToString();
            TxtDOB.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); CalculateAge('" + TxtAgeEnrollmentYears.ClientID + "', '" + TxtAgeEnrollmentMonths.ClientID + "','" + TxtDOB.ClientID + "','" + TxtRegistrationDate.ClientID + "'); CalculateAge('" + TxtAgeCurrentYears.ClientID + "','" + TxtAgeCurrentMonths.ClientID + "','" + TxtDOB.ClientID + "','" + txtSysDate.ClientID + "')");
        }

        protected void BindDropdown()
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));

            DataView theDV = new DataView();
            DataTable theDT = new DataTable();
            //if (Request.QueryString["Name"] == "Add")
            if (Convert.ToInt32(Session["PatientId"]) == 0)
            {
                //Marital Status
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=12 and SystemID IN(" + Session["SystemId"].ToString() + ",0)";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDMaritalStatus, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Referred From
                theDV = new DataView(theDSXML.Tables["mst_pmtctdecode"]);
                theDV.RowFilter = "CodeID=28 and SystemID IN(" + Session["SystemId"].ToString() + ", 0) and DeleteFlag=0";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddReferredFrom, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                // Village/Town/City
                theDV = new DataView(theDSXML.Tables["mst_Village"]);
                theDV.RowFilter = "SystemID IN(" + Session["SystemId"].ToString() + ", 0) and DeleteFlag=0";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddVillageName, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                //District
                theDV = new DataView(theDSXML.Tables["Mst_District"]);
                theDV.RowFilter = "SystemID IN (" + Session["SystemId"].ToString() + ",0) and DeleteFlag=0";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddDistrict, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Transferring Information from FindAdd /ART Enrolment Form
            }
            else
            {
                //Marital Status
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=12 and SystemID IN(" + Session["SystemId"].ToString() + ",0)";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDMaritalStatus, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Referred From
                theDV = new DataView(theDSXML.Tables["mst_pmtctdecode"]);
                theDV.RowFilter = "CodeID=28 and SystemID IN(" + Session["SystemId"].ToString() + ",0)";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddReferredFrom, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                // Village/Town/City
                theDV = new DataView(theDSXML.Tables["mst_Village"]);
                theDV.RowFilter = "SystemID IN(" + Session["SystemId"].ToString() + ",0)";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddVillageName, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                //District
                theDV = new DataView(theDSXML.Tables["Mst_District"]);
                theDV.RowFilter = "SystemID IN(" + Session["SystemId"].ToString() + ",0)";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddDistrict, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

            }

            if (!IsPostBack)
            {
                //if (Session["PatientId"] == null || Convert.ToString(Session["PatientId"]) == "0")
                //{
                //    Session["PatientId"] = Request.QueryString["PatientId"];  
                //}


                //if (Request.QueryString["PatientID"] == null)
                if (Session["PatientId"] == null || System.Convert.ToInt32(Session["PatientId"]) == 0)
                {
                    Hashtable theHT = (Hashtable)Session["EnrollParams"];
                    TxtFirstName.Text = theHT["FirstName"].ToString();
                    TxtLastName.Text = theHT["LastName"].ToString();
                    //txthospitalID.Text = theHT["ClinicNo"].ToString();
                    TxtDOB.Text = theHT["Date of Birth"].ToString();
                    DDGender.Text = theHT["Sex"].ToString();
                    Session.Remove("EnrollParams");
                }
                else
                {
                    IPatientRegistration MgrPMTCT = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
                    int patientID = System.Convert.ToInt32(Session["PatientId"]); //Convert.ToInt32(Request.QueryString["PatientID"]);
                    ViewState["ptnid"] = patientID;
                    DataTable RecordDT = MgrPMTCT.GetPatientRecord(patientID);
                    this.TxtLastName.Text = RecordDT.Rows[0]["LastName"].ToString();
                    this.TxtFirstName.Text = RecordDT.Rows[0]["FirstName"].ToString();
                    this.DDGender.SelectedValue = RecordDT.Rows[0]["Sex"].ToString();
                    this.TxtDOB.Text = ((DateTime)RecordDT.Rows[0]["DOB"]).ToString(Session["AppDateFormat"].ToString());
                }

                /////////////////////////////////////////////////////////////////////////////////////////////////
            }
        }

        private void SaveCancel()
        {
            Session["status"] = "Add";
            string strPatientID = ViewState["PtnID"].ToString();
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Enrollment Form saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            //if (Request.QueryString["Name"] == "Add")
            if (Convert.ToInt32(Session["Add"]) == 1) //if add mode then go to home page, 0 means update mode
            {
                script += "window.location.href='frmPatient_Home.aspx';\n";
                //script += "window.location.href('frmPatient_Home.aspx?PatientId=" + ViewState["PtnID"].ToString() + "');\n";
            }
            else
            {
                script += "window.location.href='frmPatient_History.aspx';\n";
                //script += "window.location.href('frmPatient_History.aspx?PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + "0" + "');\n";
            }
            script += "}\n";
            script += "else \n";
            script += "{\n";
            //script += "window.location.href('frmClinical_EnrolmentPMTCT.aspx?name=Edit&PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + ((Session["fmsts"] == null) ? "0".ToString() : Session["fmsts"].ToString()) + "');\n";
            script += "window.location.href='frmClinical_EnrolmentPMTCT.aspx';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }
        //private void UpdateCancel()
        //{
        //    string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
        //    script += "var ans;\n";
        //    script += "ans=window.confirm('Enrollment Form updated successfully. Do you want to close?');\n";
        //    script += "if (ans==true)\n";
        //    script += "{\n";
        //    script += "window.location.href('frmPatient_History.aspx?PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + "0" + "');\n";
        //    script += "}\n";
        //    script += "else \n";
        //    script += "{\n";
        //    script += "window.location.href('frmClinical_EnrolmentPMTCT.aspx?name=Edit&PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + "0" + "');\n";
        //    script += "}\n";
        //    script += "</script>\n";
        //    ClientScript.RegisterStartupScript(this.GetType(),"confirm", script);

        //}
        protected void Authenticationright()
        {
            AuthenticationManager AuthenticationMgr = new AuthenticationManager();
            if (AuthenticationMgr.HasFunctionRight(ApplicationAccess.PMTCTEnrollment, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;

            }
            if (AuthenticationMgr.HasFunctionRight(ApplicationAccess.PMTCTEnrollment, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
            }
            if (AuthenticationMgr.HasFunctionRight(ApplicationAccess.PMTCTEnrollment, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
            }

            if (AuthenticationMgr.HasFunctionRight(ApplicationAccess.PMTCTEnrollment, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
            }
        }

        private void LoadPatientDetail()
        {
            //IPatientRegistration PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");

            ////int patientID = Convert.ToInt32(Request.QueryString["PatientId"]);
            int patientID = System.Convert.ToInt32(Session["PatientId"]);

            IPatientRegistration ptnMgrPMTCT = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet theDS = ptnMgrPMTCT.GetPatientRegistrationPMTCT(patientID);
            if (theDS.Tables[0].Rows.Count > 0)
            {
                Session["Add"] = 0;
                ViewState["visitPk"] = theDS.Tables[1].Rows[0]["Visit_ID"].ToString();
                TxtFirstName.Text = theDS.Tables[0].Rows[0]["FirstName"].ToString();
                TxtMiddleName.Text = theDS.Tables[0].Rows[0]["MiddleName"].ToString();
                TxtLastName.Text = theDS.Tables[0].Rows[0]["LastName"].ToString();
                TxtRegistrationDate.Text = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[1].Rows[0]["VisitDate"]);
                DDGender.SelectedValue = theDS.Tables[0].Rows[0]["Sex"].ToString();
                TxtDOB.Text = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["DOB"]);
                if (theDS.Tables[0].Rows[0]["DOBPrecision"].ToString() == "0")
                    RbtnDOBPrecExact.Checked = true;
                else if (theDS.Tables[0].Rows[0]["DOBPrecision"].ToString() == "1")
                    RbtnDOBPrecEstimated.Checked = true;
                else if (theDS.Tables[0].Rows[0]["DOBPrecision"].ToString() == "2")
                {
                    RbtnDOBPrecExact.Checked = false;
                    RbtnDOBPrecEstimated.Checked = false;
                }

                TxtAgeCurrentYears.Text = theDS.Tables[0].Rows[0]["AGE"].ToString();
                TxtAgeCurrentMonths.Text = theDS.Tables[0].Rows[0]["Month"].ToString();
                TxtAgeEnrollmentYears.Text = theDS.Tables[0].Rows[0]["EnrolAge"].ToString();
                TxtAgeEnrollmentMonths.Text = theDS.Tables[0].Rows[0]["EnrolMonth"].ToString();
                DDMaritalStatus.SelectedValue = theDS.Tables[0].Rows[0]["MaritalStatus"].ToString();
                if (theDS.Tables[0].Rows[0]["TransferIn"].ToString() == "1")
                {
                    chkTransferIn.Checked = true;
                }
                else { chkTransferIn.Checked = false; }
                ddReferredFrom.SelectedValue = theDS.Tables[0].Rows[0]["ReferredFrom"].ToString();
                TxtANCNumber.Text = theDS.Tables[0].Rows[0]["ANCNumber"].ToString();
                TxtPMTCTNumber.Text = theDS.Tables[0].Rows[0]["PMTCTNumber"].ToString();
                TxtAdmissionNumber.Text = theDS.Tables[0].Rows[0]["AdmissionNumber"].ToString();
                TxtOutPatientNumber.Text = theDS.Tables[0].Rows[0]["OutpatientNumber"].ToString();
                TxtAddress.Text = theDS.Tables[0].Rows[0]["Address"].ToString();
                ddVillageName.SelectedValue = theDS.Tables[0].Rows[0]["VillageName"].ToString();
                ddDistrict.SelectedValue = theDS.Tables[0].Rows[0]["DistrictName"].ToString();
                TxtPhoneNumber.Text = theDS.Tables[0].Rows[0]["Phone"].ToString();
                ViewState["ANC"] = TxtANCNumber.Text;
                ViewState["PMTCT"] = TxtPMTCTNumber.Text;
                ViewState["Admission"] = TxtAdmissionNumber.Text;
                ViewState["Outpatient"] = TxtOutPatientNumber.Text;
                FillOldData(patientID);
            }
            else
            {
                Session["Add"] = 1;
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {


                //if (Request.QueryString["sts"].ToString() == "1")
                if (Session["PatientStatus"] != null && Session["PatientStatus"].ToString() == "1")
                {
                    btnsave.Enabled = false;
                }
                //(Master.FindControl("PanelPatiInfo") as Panel).Visible = false;
                //(Master.FindControl("lblformname") as Label).Text = "PMTCT Enrollment";
                //(Master.FindControl("lblRoot") as Label).Text = "Clinical >>";
                //(Master.FindControl("lblMark") as Label).Visible = false;
                //(Master.FindControl("lblheader") as Label).Text = "Enrollment";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Enrollment";
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "PMTCT Enrollment";
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;

                AddAttributes();
                PutCustomControl();
                Authenticationright();
                if (!IsPostBack)
                {
                    BindDropdown();
                    //////if (Request.QueryString["Name"] == "Add")
                    //////{
                    //////    theUtil = new Utility();
                    //////    theUtil.SetSession();
                    //////    //if (Session["PatientId"] != null && Session["PatientId"].ToString() != "0" && Session["PatientVisitId"] != null && Session["PatientVisitId"].ToString() != "0")
                    //////    //{
                    //////    //    Session["PatientId"] = 0;
                    //////    //}


                    //////}
                    //////else //if (Request.QueryString["Name"] == "Edit")
                    //////{
                    //////    LoadPatientDetail();
                    //////}

                    if (Convert.ToInt32(Session["PatientId"]) > 0)
                    {
                        LoadPatientDetail();
                    }
                    else
                    {
                        Session["Add"] = 1; //only to open page history or home page after save
                    }


                }
                if (Convert.ToInt32(Session["Add"]) == 1)
                {
                    //CareEnded Privilages
                    if (Convert.ToString(Session["PMTCTPatientStatus"]) == "0")
                    {
                        btnsave.Enabled = true;
                    }
                    else
                    {
                        btnsave.Enabled = false;
                    }

                }
                else if (Convert.ToInt32(Session["Add"]) == 0) //if edit after care end is true only then allow to edit
                {
                    //btnsave.Enabled = true;
                    if (Convert.ToString(Session["PMTCTPatientStatus"]) == "1")
                    {
                        if (Convert.ToString(Session["CareEndFlag"]) == "1")
                        {
                            btnsave.Enabled = true;
                        }
                        else
                        {
                            btnsave.Enabled = false;
                        }
                    }
                    else
                    {
                        btnsave.Enabled = true;
                    }
                }

                Form.EnableViewState = true;
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }


        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.PMTCTEnrollment));
                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "PMTCTEnroll");
                }
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
                dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, Convert.ToInt32(ViewState["visitPk"]), 0, 0, Convert.ToInt32(ApplicationAccess.PMTCTEnrollment));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "PMTCTEnroll");
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

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            { return; }

            Hashtable theHT = AddUpdateData();
            IPatientRegistration ptnMgrPMTCT = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);

            CustomFieldClinical theCustomManager = new CustomFieldClinical();
            DataTable theCustomDataDT = new DataTable();
            //if (Request.QueryString["Name"] == "Add")
            if (Convert.ToInt32(Session["PatientId"]) == 0)
            {
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.PMTCTEnrollment, (DataSet)ViewState["CustomFieldsDS"]);
            }
            else ////if (Request.QueryString["Name"] == "Edit")
            {
                //CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.PMTCTEnrollment, (DataSet)ViewState["CustomFieldsDS"]);
                //DataTable theDS = ptnMgrPMTCT.UpdatePatientRegistrationPMTCT(theHT,theCustomDataDT);
                //ViewState["PtnID"] = theDS.Rows[0]["PatientID"].ToString();
                //UpdateCancel();
            }

            DataTable theDS = ptnMgrPMTCT.SavePatientRegistrationPMTCT(theHT, theCustomDataDT);
            ViewState["PtnID"] = theDS.Rows[0]["PatientID"].ToString();
            Session["PatientId"] = theDS.Rows[0]["PatientID"].ToString();
            SaveCancel();

            IPatientHome PManager;
            PManager = (IPatientHome)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientHome, BusinessProcess.Clinical");
            System.Data.DataSet thePDS = PManager.GetPatientDetails(Convert.ToInt32(Session["PatientId"]), Convert.ToInt32(Session["SystemId"]), Convert.ToInt32(Session["TechnicalAreaId"]));
            Session["PatientInformation"] = thePDS.Tables[0];

            Session["PatientStatus"] = 0;
        }
        //
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string theUrl;

            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = System.Convert.ToString(Session["PatientStatus"]); // Request.QueryString["sts"].ToString();
            //if ((Master.FindControl("lblpntStatus") as Label).Text == "1")
            if (Session["PatientStatus"] != null && Session["PatientStatus"].ToString() == "1")
            {
                theUrl = string.Format("{0}", "frmPatient_Home.aspx");
                Response.Redirect(theUrl);
              
            }
            else
            {
                //if (Request.QueryString["name"] == "Add")
                if (Convert.ToInt32(Session["PatientId"]) == 0)
                {

                    if (ViewState["PtnID"] == null)
                    {
                        Response.Redirect("~/frmFindAddPatient.aspx");
                    }
                    else
                    {
                        Session["PatientId"] = ViewState["PtnID"].ToString();
                        ////string theUrl;
                        ////theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", ViewState["PtnID"].ToString());
                        theUrl = string.Format("{0}", "frmPatient_Home.aspx");
                        Response.Redirect(theUrl);
                    }
                }
                else
                {

                    //theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString(), Request.QueryString["sts"].ToString());
                    theUrl = string.Format("{0}", "frmPatient_History.aspx");
                    Response.Redirect(theUrl);
                }
            }
        }
    }
}