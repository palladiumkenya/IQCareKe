using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;
using Interface.Security;

/// /////////////////////////////////////////////////////////////////////
// Code Written By   : Jayanta Kumar Das
// Written Date      : 03th Oct 2008
// Description       : Patient Registration Form
//
/// /////////////////////////////////////////////////////////////////
///
namespace IQCare.Web.Clinical
{
    public partial class PatientRegistrationCTC : BasePage
    {
        //Variable Declaration Section

        #region "Local Variables"

        private Hashtable htParameters;

  
        private string ObjFactoryParameter = "BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical";

        #endregion "Local Variables"

        private Boolean FieldValidation()
        {
            IIQCareSystem IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();
            IQCareUtils theUtils = new IQCareUtils();

            if (TxtPtnEnrollment.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Patient Registration ID";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                TxtPtnEnrollment.Focus();
                return false;
            }
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

            if (DDGender.SelectedValue.Trim() == "0")
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
            if (TxtDtPosHivTest.Text != "")
            {
                DateTime theDTPosHivTest = Convert.ToDateTime(theUtils.MakeDate(TxtDtPosHivTest.Text));
                if (theDTPosHivTest > theCurrentDate)
                {
                    IQCareMsgBox.Show("PostHivTest", this);
                    return false;
                }
            }
            if (TxtConfirmHivPositive.Text != "")
            {
                DateTime theDTConfirmPos = Convert.ToDateTime(theUtils.MakeDate(TxtConfirmHivPositive.Text));
                if (theDTConfirmPos > theCurrentDate)
                {
                    IQCareMsgBox.Show("ConfirmHIV", this);
                    return false;
                }
            }
            if (DDPriorExposure.SelectedValue == "265")
            {
                if (TxtArtStartDate.Text != "")
                {
                    DateTime theArtStartDate = Convert.ToDateTime(theUtils.MakeDate(TxtArtStartDate.Text));

                    if (theArtStartDate > theCurrentDate)
                    {
                        IQCareMsgBox.Show("CompART_CurrentDate", this);
                        return false;
                    }
                    if (theArtStartDate > theEnrolDate)
                    {
                        IQCareMsgBox.Show("CompART_RegDate", this);
                        return false;
                    }
                }
            }
            if (DDPriorExposure.SelectedValue == "265")
            {
                if (ddregimen.SelectedValue.Trim() == "0" && TxtInitialRegimen.Text == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "Initial Regimen";
                    IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                    ddregimen.Focus();
                    return false;
                }
            }

            if ((Request.QueryString["name"] == "Add"))
            {
                IPatientRegistration PtnMgrCTC = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                DataSet theDS = PtnMgrCTC.GetPatientDetailsCTC(TxtPtnEnrollment.Text, TxtRegion.Text, TxtDistrict.Text, TxtFacility.Text, TxtFreference.Text, 1, 0);

                if (theDS.Tables[1].Rows[0]["Exist"].ToString() == "1")
                {
                    IQCareMsgBox.Show("EnrolmentExists", this);
                    return false;
                }
                if (theDS.Tables[0].Rows[0]["Exist"].ToString() == "2")
                {
                    IQCareMsgBox.Show("FileReferenceExists", this);
                    return false;
                }
            }
            else if (Request.QueryString["name"] == "Edit")
            {
                String Enrollment = TxtRegion.Text + "-" + TxtDistrict.Text + "-" + TxtFacility.Text + "-" + TxtPtnEnrollment.Text;
                IPatientRegistration PtnMgrCTC = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                DataSet theDS = PtnMgrCTC.GetPatientDetailsCTC(TxtPtnEnrollment.Text, TxtRegion.Text, TxtDistrict.Text, TxtFacility.Text, TxtFreference.Text, 1, 0);
                if (ViewState["EnrolID"].ToString() != Enrollment.ToString())
                {
                    if (theDS.Tables[1].Rows[0]["Exist"].ToString() == "1")
                    {
                        IQCareMsgBox.Show("EnrolmentExists", this);
                        return false;
                    }
                }

                if (ViewState["FileRefID"].ToString() != TxtFreference.Text)
                {
                    if (theDS.Tables[0].Rows[0]["Exist"].ToString() == "2")
                    {
                        IQCareMsgBox.Show("FileReferenceExists", this);
                        return false;
                    }
                }
            }

            //Patient Registration Number Cannot be Zero's
            int EnrolmentNo = 0;
            string EnrolmentID = TxtPtnEnrollment.Text;

            for (int i = 0; i <= EnrolmentID.Length - 1; i++)
            {
                int j = 1;
                if (EnrolmentID.Substring(i, j) != "0")
                {
                    EnrolmentNo++;
                }
            }
            if (EnrolmentNo == 0)
            {
                IQCareMsgBox.Show("NoZero", this);
                return false;
            }
            return true;
        }

        private string DataQuality_Msg()
        {
            string strmsg = "Following values are required to complete the data quality check:\\n\\n";
            if (TxtPtnEnrollment.Text.Trim() == "")
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'PtnID'>\n";
                script += "To_Change_Color('EnrolmentID');\n";
                script += "To_Change_Color('ptnID');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "PtnID", script);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -Patient ID";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }
            if (TxtFirstName.Text.Trim() == "")
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'Fname'>\n";
                script += "To_Change_Color('lblPName');\n";
                script += "To_Change_Color('FName');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "Fname", script);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -First Name";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }

            if (TxtLastName.Text.Trim() == "")
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'Lname'>\n";
                script += "To_Change_Color('lblPName');\n";
                script += "To_Change_Color('LName');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "Lname", script);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -Last Name";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }

            if (TxtRegistrationDate.Text.Trim() == "")
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'RegDate'>\n";
                script += "To_Change_Color('lblregistrationdate');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "RegDate", script);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -Registration Date";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }

            if (DDGender.SelectedValue.Trim() == "0")
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'ddgender'>\n";
                script += "To_Change_Color('lblgender');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ddgender", script);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -Sex";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }

            if (TxtDOB.Text.Trim() == "")
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'DOB'>\n";
                script += "To_Change_Color('lblDOB');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "DOB", script);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -Date of Birth";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }
            if (DDPriorExposure.SelectedValue.Trim() == "265")
            {
                if (ddregimen.SelectedValue.Trim() == "0" && TxtInitialRegimen.Text == "")
                {
                    string script = "<script language = 'javascript' defer ='defer' id = 'ddregimen'>\n";
                    script += "To_Change_Color('lbliregimen');\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "ddregimen", script);
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = " -Initial Regimen";
                    strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                    strmsg += "\\n";
                }
            }
            return strmsg;
        }

        private void Save_Update()
        {
            string theUrl;
            if (Request.QueryString["name"] == "Add" && ViewState["PtnID"] == null)
            {
                theUrl = string.Format("{0}", "../frmFindAddPatient.aspx");
                Response.Redirect(theUrl);
            }
            else if (Request.QueryString["name"] == "Edit" && ViewState["PtnID"] != null)
            {
                theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmPatient_History.aspx", Request.QueryString["PatientId"].ToString(), Request.QueryString["sts"].ToString());
                Response.Redirect(theUrl);
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
            script += "Redirect(" + strPatientID + ");\n";
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href='frmClinical_PatientRegistrationCTC.aspx?name=Edit&PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + Session["fmsts"] + "';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        private void UpdateCancel()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Enrollment Form updated successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='frmPatient_History.aspx?PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + Session["fmsts"] + "';\n";
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href='frmClinical_PatientRegistrationCTC.aspx?name=Edit&PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + Session["fmsts"] + "';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        private void DQCancel()
        {
            Session["status"] = "Add";
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Enrollment DQ Checked complete.\\nForm Marked as DQ Checked.\\n Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='frmPatient_Home.aspx?PatientId=" + ViewState["PtnID"].ToString() + "';\n";
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href='frmClinical_PatientRegistrationCTC.aspx?name=Edit&PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + 0 + "';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        private void DQUpdateCancel()
        {
            ViewState["btcolor"] = '1';
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Enrollment DQ Checked complete.\\nForm Marked as DQ Checked.\\n Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='frmPatient_History.aspx?PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + Session["fmsts"] + "';\n";
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href='frmClinical_PatientRegistrationCTC.aspx?name=Edit&PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + Session["fmsts"] + "';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        private void InitialRegimen(DataTable theDT)
        {
            ddregimen.DataSource = theDT;
            ddregimen.DataValueField = "Id";
            ddregimen.DataTextField = "Name";
            ddregimen.DataBind();
        }

        ////private string InsertCustomFieldsValuesString(string PatientId)
        ////{
        ////    string sqlstr = string.Empty;
        ////    try
        ////    {
        ////        GenerateCustomFieldsValues(pnlCustomList);

        ////        string sqlselect;
        ////        Int32 visitID = 0;
        ////        string visitdate = string.Empty;
        ////        ICustomFields CustomFields;
        ////        visitID = 77777;
        ////        visitdate = "1900-01-01";
        ////        CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");

        ////        ////DataSet dsVisit = CustomFields.GetPatientVisit(Convert.ToInt32(PatientId), Convert.ToInt32(Session["AppLocationID"]), 0);
        ////        //if (dsVisit != null && dsVisit.Tables[1].Rows.Count > 0)
        ////        //{
        ////        //    visitID = Convert.ToInt32(dsVisit.Tables[1].Rows[0]["Visit_ID"].ToString());
        ////        //    visitdate = Convert.ToDateTime(dsVisit.Tables[1].Rows[0]["VisitDate"].ToString());
        ////        //}

        ////        if (sbValues.ToString().Trim() != "")
        ////        {
        ////            sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
        ////            sqlstr += " VALUES(" + PatientId.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";

        ////        }
        ////        if (strmultiselect.ToString() != "")
        ////        {
        ////            string[] FieldValues = strmultiselect.Split(new char[] { '^' });
        ////            if (arl.Count != 0)
        ////            {
        ////                int p = 0;
        ////                foreach (object obj in arl)
        ////                {
        ////                    sqlselect = "";
        ////                    if (obj.ToString() != "")
        ////                    {
        ////                        if (FieldValues[p].ToString() != "")
        ////                        {
        ////                            string[] mValues = FieldValues[p].Split(new char[] { ',' });
        ////                            foreach (string str in mValues)
        ////                            {
        ////                                if (str.ToString() != "")
        ////                                {
        ////                                    string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
        ////                                    Int32 ispos = Convert.ToInt32(strtab.Length);
        ////                                    Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;
        ////                                    sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,Visit_pk,Visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
        ////                                    sqlselect += " VALUES (" + PatientId.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";

        ////                                    if (sqlstr == "")
        ////                                    {
        ////                                        sqlstr = sqlselect;
        ////                                    }
        ////                                    else
        ////                                    {
        ////                                        sqlstr = sqlstr + "!" + sqlselect;
        ////                                    }
        ////                                    //CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
        ////                                    //icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
        ////                                    //if (icount == -1)
        ////                                    //{
        ////                                    //    return;
        ////                                    //}

        ////                                }
        ////                            }
        ////                        }
        ////                    }
        ////                    p += 1;
        ////                }
        ////            }
        ////        }
        ////    }
        ////    catch (Exception err)
        ////    {
        ////        MsgBuilder theBuilder = new MsgBuilder();
        ////        theBuilder.DataElements["MessageText"] = err.Message.ToString();
        ////        IQCareMsgBox.Show("#C1", theBuilder, this);
        ////    }
        ////    return sqlstr;
        ////}
        ////private void InsertCustomFieldsValues(string PatientId)
        ////{
        ////    GenerateCustomFieldsValues(pnlCustomList);
        ////    string sqlstr = string.Empty;
        ////    string sqlselect;
        ////    Int32 visitID = 0;
        ////    DateTime visitdate = System.DateTime.Now;
        ////    ICustomFields CustomFields;
        ////    try
        ////    {
        ////        CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
        ////        DataSet dsVisit = CustomFields.GetPatientVisit(Convert.ToInt32(PatientId), Convert.ToInt32(Session["AppLocationID"]), 0);
        ////        if (dsVisit != null && dsVisit.Tables[1].Rows.Count > 0)
        ////        {
        ////            visitID = Convert.ToInt32(dsVisit.Tables[1].Rows[0]["Visit_ID"].ToString());
        ////            visitdate = Convert.ToDateTime(dsVisit.Tables[1].Rows[0]["VisitDate"].ToString());
        ////        }

        ////    }
        ////    catch
        ////    {
        ////    }
        ////    finally
        ////    {
        ////        CustomFields = null;
        ////    }

        ////    if (sbValues.ToString().Trim() != "")
        ////    {
        ////        sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
        ////        sqlstr += " VALUES(" + PatientId.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";

        ////        try
        ////        {
        ////            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
        ////            icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
        ////            if (icount == -1)
        ////            {
        ////                return;
        ////            }
        ////        }
        ////        catch
        ////        {
        ////        }
        ////        finally
        ////        {
        ////            CustomFields = null;
        ////        }
        ////    }
        ////    if (strmultiselect.ToString() != "")
        ////    {
        ////        string[] FieldValues = strmultiselect.Split(new char[] { '^' });
        ////        if (arl.Count != 0)
        ////        {
        ////            int p = 0;
        ////            foreach (object obj in arl)
        ////            {
        ////                sqlselect = "";
        ////                if (obj.ToString() != "")
        ////                {
        ////                    if (FieldValues[p].ToString() != "")
        ////                    {
        ////                        string[] mValues = FieldValues[p].Split(new char[] { ',' });
        ////                        foreach (string str in mValues)
        ////                        {
        ////                            if (str.ToString() != "")
        ////                            {
        ////                                string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
        ////                                Int32 ispos = Convert.ToInt32(strtab.Length);
        ////                                Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;
        ////                                sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,Visit_pk,Visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
        ////                                sqlselect += " VALUES (" + PatientId.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";
        ////                                try
        ////                                {
        ////                                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
        ////                                    icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
        ////                                    if (icount == -1)
        ////                                    {
        ////                                        return;
        ////                                    }

        ////                                }
        ////                                catch
        ////                                {
        ////                                }
        ////                                finally
        ////                                {
        ////                                    CustomFields = null;
        ////                                }
        ////                            }
        ////                        }
        ////                    }
        ////                }
        ////                p += 1;
        ////            }
        ////        }
        ////    }
        ////}

        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();

            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.Enrollment));
                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "Enroll");
                }
                ViewState["CustomFieldsDS"] = theDS;
                pnlCustomList.Visible = true;
            }

        ////    ICustomFields CustomFields;
            ////    string pnlName = "PnlCustomList";
            ////    CustomFieldClinical theCustomField = new CustomFieldClinical();
            ////    BindFunctions theBindMgr = new BindFunctions();
            ////    TableName = string.Empty;
            ////    Int32 ii = 0;
            ////    try
            ////    {
        ////        CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
            ////        DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.Enrollment));
            ////        if (theDS != null && theDS.Tables[0].Rows.Count > 0)
            ////        {
            ////            sbParameter = new StringBuilder();
            ////            TableName = theDS.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");

        ////            pnlCustomList.Visible = true;
            ////            pnlCustomList.Controls.Clear();
            ////            arl = new ArrayList();
            ////            pnlCustomList.Controls.Add(new LiteralControl("<TABLE border='1' cellpadding=6 cellspacing=0 width=100%>"));

        ////            foreach (DataRow dr in theDS.Tables[0].Rows)
            ////            {
            ////                if (ii % 2 == 0)
            ////                    pnlCustomList.Controls.Add(new LiteralControl("<TR >"));
            ////                if (dr[1].ToString() == "1")
            ////                    pnlCustomList.Controls.Add(new LiteralControl("<TD >"));
            ////                else if (dr[1].ToString() == "6")
            ////                    pnlCustomList.Controls.Add(new LiteralControl("<TD align='left' nowrap='noWrap' >"));
            ////                else if ((dr[1].ToString() == "3") || (dr[1].ToString() == "4") || (dr[1].ToString() == "5") || (dr[1].ToString() == "7") || (dr[1].ToString() == "8") || (dr[1].ToString() == "9"))
            ////                    pnlCustomList.Controls.Add(new LiteralControl("<TD align='left'>"));

        ////                //Select List
            ////                if (dr[1].ToString() == "4")
            ////                {
            ////                    Label customLabel = new Label();
            ////                    customLabel.ID = pnlName + "lbl" + ii.ToString();
            ////                    customLabel.Text = dr[0].ToString().Replace("_", " ");
            ////                    customLabel.Text = customLabel.Text.Replace("Enroll", "");
            ////                    sbParameter.Append(",[" + dr[0].ToString() + "]");
            ////                    customLabel.Width = 200;
            ////                    customLabel.CssClass = "labelright";
            ////                    customLabel.Font.Bold = true;

        ////                    pnlCustomList.Controls.Add(customLabel);

        ////                    pnlCustomList.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

        ////                    DropDownList ddlSelectList = new DropDownList();
            ////                    ddlSelectList.ID = pnlName + "Selectlist" + dr[0].ToString();
            ////                    ddlSelectList.Width = 180;

        ////                    DataSet dsSelectList = CustomFields.GetCustomList(Convert.ToInt32(dr[2].ToString()));
            ////                    if (dsSelectList != null && dsSelectList.Tables[0].Rows.Count > 0)
            ////                    {
            ////                        if (Request.QueryString["name"] == "Add")
            ////                        {
            ////                            IQCareUtils theUtilsCF = new IQCareUtils();
            ////                            DataView theDVCF = new DataView(dsSelectList.Tables[0]);
            ////                            theDVCF.RowFilter = "DeleteFlag=0";
            ////                            DataTable theDTCF = (DataTable)theUtilsCF.CreateTableFromDataView(theDVCF);

        ////                            theBindMgr.BindCombo(ddlSelectList, theDTCF, "Name", "ID");
            ////                            theDVCF.Dispose();
            ////                            theDTCF.Clear();
            ////                        }
            ////                        else
            ////                        {
            ////                            theBindMgr.BindCombo(ddlSelectList, dsSelectList.Tables[0], "Name", "Id");
            ////                        }

        ////                    }
            ////                    pnlCustomList.Controls.Add(ddlSelectList);

        ////                }
            ////                //Multi Select List
            ////                else if (dr[1].ToString() == "9")
            ////                {
            ////                    if (arl.Count == 0)
            ////                    {
            ////                        arl.Add("dtl_CustomField_" + TableName.Replace("-", "_").ToString() + "_" + dr[0].ToString());
            ////                    }
            ////                    foreach (object obj in arl)
            ////                    {
            ////                        if (obj.ToString() != "dtl_CustomField_" + TableName.Replace("-", "_").ToString() + "_" + dr[0].ToString())
            ////                        {
            ////                            arl.Add("dtl_CustomField_" + TableName.Replace("-", "_").ToString() + "_" + dr[0].ToString());
            ////                            break;
            ////                        }
            ////                    }
            ////                    Label theMultiSelectlbl = new Label();
            ////                    theMultiSelectlbl.ID = pnlName + "lbl" + ii.ToString();
            ////                    theMultiSelectlbl.Text = dr[0].ToString().Replace("_", " ");
            ////                    theMultiSelectlbl.Text = theMultiSelectlbl.Text.Replace("Enroll", "");
            ////                    theMultiSelectlbl.Width = 200;
            ////                    theMultiSelectlbl.CssClass = "labelright";
            ////                    theMultiSelectlbl.Font.Bold = true;
            ////                    pnlCustomList.Controls.Add(theMultiSelectlbl);

        ////                    pnlCustomList.Controls.Add(new LiteralControl("<div class = 'Customdivborder' nowrap='nowrap'>"));

        ////                    CheckBoxList chkMultiList = new CheckBoxList();
            ////                    chkMultiList.ID = pnlName + "Multiselectlist" + dr[0].ToString();
            ////                    chkMultiList.RepeatLayout = RepeatLayout.Flow;
            ////                    chkMultiList.CssClass = "check";
            ////                    chkMultiList.Width = 300;

        ////                    DataSet dsMultiSelectList = CustomFields.GetCustomList(Convert.ToInt32(dr[2].ToString()));
            ////                    if (dsMultiSelectList != null && dsMultiSelectList.Tables[0].Rows.Count > 0)
            ////                    {
            ////                        if (Request.QueryString["name"] == "Add")
            ////                        {
            ////                            IQCareUtils theUtilsCF = new IQCareUtils();
            ////                            DataView theDVCF = new DataView(dsMultiSelectList.Tables[0]);
            ////                            theDVCF.RowFilter = "DeleteFlag=0";
            ////                            DataTable theDTCF = (DataTable)theUtilsCF.CreateTableFromDataView(theDVCF);

        ////                            theBindMgr.BindCheckedList(chkMultiList, theDTCF, "Name", "Id");

        ////                            theDVCF.Dispose();
            ////                            theDTCF.Clear();
            ////                        }
            ////                        else
            ////                        {
            ////                            theBindMgr.BindCheckedList(chkMultiList, dsMultiSelectList.Tables[0], "Name", "Id");
            ////                        }
            ////                    }
            ////                    pnlCustomList.Controls.Add(chkMultiList);

        ////                    pnlCustomList.Controls.Add(new LiteralControl("</div>"));

        ////                }

        ////                theCustomField.CreateCustomControls(pnlCustomList, pnlName, ref sbParameter, dr, ref TableName, "Enroll", ii);

        ////                ii++;
            ////            }
            ////        }
            ////        ViewState["ControlCreated"] = "CC";
            ////        pnlCustomList.Controls.Add(new LiteralControl("</TABLE>"));

        ////    }
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

        //Populate Old Data in the Custom Field
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
                dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, Convert.ToInt32(ViewState["visitPk"]), 0, 0, Convert.ToInt32(ApplicationAccess.Enrollment));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "Enroll");
            }

        ////    string pnlName = Cntrl.ID;

        ////    DataSet dsvalues = null;
            ////    ICustomFields CustomFields;

        ////    try
            ////    {
            ////        CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
            ////        dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + TableName.ToString().Replace("-", "_"), sbParameter.ToString(), Convert.ToInt32(PatID.ToString()),0, Convert.ToInt32(ViewState["visitPk"]), 0, 0, Convert.ToInt32(ApplicationAccess.Enrollment));
            ////    }
            ////    catch
            ////    {
            ////    }
            ////    finally
            ////    {
            ////        CustomFields = null;
            ////    }
            ////    try
            ////    {
            ////        Boolean blnflag = false;
            ////        foreach (DataTable dt in dsvalues.Tables)
            ////        {
            ////            blnflag = true;
            ////        }

        ////        if (dsvalues != null && blnflag && dsvalues.Tables[0].Rows.Count > 0)
            ////        {
            ////            //if any data exist then set the View State
            ////            ViewState["CustomFieldsData"] = 1;
            ////            foreach (Control x in Cntrl.Controls)
            ////            {
        ////                if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
            ////                {
            ////                    foreach (DataColumn dc in dsvalues.Tables[0].Columns)
            ////                    {
            ////                        if (pnlName.ToUpper() + "SELECTLIST" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            ////                        {
            ////                            if (((DropDownList)x).SelectedValue == "0")
            ////                            {
            ////                                ((DropDownList)x).SelectedValue = dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString();
            ////                            }
            ////                        }

        ////                    }
            ////                }
            ////                if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
            ////                {
            ////                    foreach (DataColumn dc in dsvalues.Tables[0].Columns)
            ////                    {
            ////                        if (pnlName.ToUpper() + "RADIO1" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            ////                        {
            ////                            if (dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString() == "True")
            ////                            {
            ////                                ((HtmlInputRadioButton)x).Checked = true;
            ////                            }
            ////                        }
            ////                        if (pnlName.ToUpper() + "RADIO2" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            ////                        {
            ////                            if (dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString() == "False")
            ////                            {
            ////                                ((HtmlInputRadioButton)x).Checked = true;
            ////                            }
            ////                        }
            ////                    }
            ////                }
            ////                if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
            ////                {
            ////                    foreach (DataColumn dc in dsvalues.Tables[0].Columns)
            ////                    {
            ////                        if (pnlName.ToUpper() + "TXT" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            ////                        {
            ////                            if (((TextBox)x).Text == "")
            ////                            {
            ////                                ((TextBox)x).Text = dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString();
            ////                                break;
            ////                            }
            ////                        }
            ////                        if (pnlName.ToUpper() + "NUM" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            ////                        {
            ////                            if (((TextBox)x).Text == "")
            ////                            {
            ////                                ((TextBox)x).Text = dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString();
            ////                                break;
            ////                            }
            ////                        }
            ////                        if (pnlName.ToUpper() + "DT" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            ////                        {
            ////                            if (((TextBox)x).Text == "")
            ////                            {
            ////                                if (dsvalues.Tables[0].Rows[0][dc.ColumnName] != System.DBNull.Value)
            ////                                {
            ////                                    ((TextBox)x).Text = ((DateTime)dsvalues.Tables[0].Rows[0][dc.ColumnName]).ToString(Session["AppDateFormat"].ToString());
            ////                                    break;
            ////                                }
            ////                            }
            ////                        }
            ////                    }
            ////                }
            ////                if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
            ////                {
            ////                    DataSet dsmvalues = null;
            ////                    try
            ////                    {
            ////                        string strfldName = pnlName.ToUpper() + "MULTISELECTLIST";
            ////                        Int32 stpos = strfldName.Length;
            ////                        Int32 enpos = x.ID.Length - stpos;
            ////                        strfldName = x.ID.Substring(stpos, enpos).ToString();
            ////                        CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
            ////                        dsmvalues = CustomFields.GetCustomFieldValues("[dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_" + strfldName.ToString() + "]", ",[" + strfldName.ToString() + "]", Convert.ToInt32(PatID.ToString()),0, Convert.ToInt32(ViewState["visitPk"]), 0, 0, Convert.ToInt32(ApplicationAccess.Enrollment));
            ////                        if (dsmvalues != null && dsmvalues.Tables[0].Rows.Count > 0)
            ////                            ViewState["CustomFieldsMulti"] = 1;
            ////                        foreach (DataRow dr in dsmvalues.Tables[0].Rows)
            ////                        {
            ////                            foreach (DataColumn dc in dsmvalues.Tables[0].Columns)
            ////                            {
            ////                                if (pnlName.ToUpper() + "MULTISELECTLIST" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            ////                                {
            ////                                    foreach (ListItem li in ((CheckBoxList)x).Items)
            ////                                    {
            ////                                        if (li.Value == dr[dc.ColumnName].ToString())
            ////                                        {
            ////                                            li.Selected = true;
            ////                                        }
            ////                                    }
            ////                                }
            ////                            }
            ////                        }
            ////                    }
            ////                    catch
            ////                    {
            ////                    }
            ////                    finally
            ////                    {
            ////                        CustomFields = null;
            ////                        dsmvalues = null;
            ////                    }

        ////                }
            ////            }
            ////        }
            ////        else
            ////        {
            ////            foreach (Control x in Cntrl.Controls)
            ////                if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
            ////                {
            ////                    DataSet dsmvalues = null;
            ////                    try
            ////                    {
            ////                        string strfldName = pnlName.ToUpper() + "MULTISELECTLIST";
            ////                        Int32 stpos = strfldName.Length;
            ////                        Int32 enpos = x.ID.Length - stpos;
            ////                        strfldName = x.ID.Substring(stpos, enpos).ToString();
            ////                        CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
            ////                        dsmvalues = CustomFields.GetCustomFieldValues("[dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_" + strfldName.ToString() + "]", ",[" + strfldName.ToString() + "]", Convert.ToInt32(PatID.ToString()),0, Convert.ToInt32(ViewState["visitPk"]), 0, 0, Convert.ToInt32(ApplicationAccess.Enrollment));
            ////                        if (dsmvalues != null && dsmvalues.Tables[0].Rows.Count > 0)
            ////                            ViewState["CustomFieldsMulti"] = 1;

        ////                        foreach (DataRow dr in dsmvalues.Tables[0].Rows)
            ////                        {
            ////                            foreach (DataColumn dc in dsmvalues.Tables[0].Columns)
            ////                            {
            ////                                if (pnlName.ToUpper() + "MULTISELECTLIST" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            ////                                {
            ////                                    foreach (ListItem li in ((CheckBoxList)x).Items)
            ////                                    {
            ////                                        if (li.Value == dr[dc.ColumnName].ToString())
            ////                                        {
            ////                                            li.Selected = true;
            ////                                        }
            ////                                    }
            ////                                }
            ////                            }
            ////                        }
            ////                    }
            ////                    catch
            ////                    {
            ////                    }
            ////                    finally
            ////                    {
            ////                        CustomFields = null;
            ////                        dsmvalues = null;
            ////                    }

        ////                }
            ////        }
            ////    }
            ////    catch (Exception ex)
            ////    {
            ////        ex.Message.ToString();
            ////    }
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

        //Jayant Kr. Das
        //Generate a string builder for Insert Query Values
        //and Update Query Values
        ////private void GenerateCustomFieldsValues(Control Cntrl)
        ////{
        ////    string pnlName = Cntrl.ID;
        ////    sbValues  = new StringBuilder();
        ////    strmultiselect  = string.Empty;
        ////    string strfName = string.Empty;
        ////    Boolean radioflag = false;

        ////    Int32 stpos = 0;
        ////    Int32 enpos = 0;
        ////    if (ViewState["CustomFieldsData"] != null)
        ////    {
        ////        foreach (Control x in Cntrl.Controls)
        ////        {
        ////            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
        ////            {
        ////                if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
        ////                {
        ////                    strfName = pnlName.ToUpper() + "TXT";
        ////                    stpos = strfName.Length;
        ////                    enpos = x.ID.Length - stpos;
        ////                    strfName = x.ID.Substring(stpos, enpos).ToString();
        ////                    if (((TextBox)x).Text != "")
        ////                    {
        ////                        sbValues.Append (",[" + strfName + "] = '" + ((TextBox)x).Text.ToString() + "'");
        ////                    }
        ////                    else
        ////                    {
        ////                        sbValues.Append(",[" + strfName + "] = ' " + "'");
        ////                    }
        ////                }
        ////                else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
        ////                {
        ////                    strfName = pnlName.ToUpper() + "NUM";
        ////                    stpos = strfName.Length;
        ////                    enpos = x.ID.Length - stpos;
        ////                    strfName = x.ID.Substring(stpos, enpos).ToString();
        ////                    if (((TextBox)x).Text != "")
        ////                    {
        ////                        sbValues.Append(",[" + strfName + "]=" + ((TextBox)x).Text.ToString());
        ////                    }
        ////                    else
        ////                    {
        ////                        sbValues.Append("," + strfName + "=Null");
        ////                    }

        ////                }
        ////                else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
        ////                {
        ////                    strfName = pnlName.ToUpper() + "DT";
        ////                    stpos = strfName.Length;
        ////                    enpos = x.ID.Length - stpos;
        ////                    strfName = x.ID.Substring(stpos, enpos).ToString();
        ////                    if (((TextBox)x).Text != "")
        ////                    {
        ////                        sbValues.Append(",[" + strfName + "]='" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
        ////                    }
        ////                    else
        ////                    {
        ////                        sbValues.Append(",[" + strfName + "]=" + "Null");
        ////                    }
        ////                }
        ////            }
        ////            if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
        ////            {
        ////                if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
        ////                {
        ////                    radioflag = false;
        ////                    strfName = pnlName.ToUpper() + "RADIO1";
        ////                    stpos = strfName.Length;
        ////                    enpos = x.ID.Length - stpos;
        ////                    strfName = x.ID.Substring(stpos, enpos).ToString();
        ////                    if (((HtmlInputRadioButton)x).Checked == true)
        ////                    {
        ////                        sbValues.Append(",[" + strfName + "]=" + "1");
        ////                        radioflag = true;
        ////                    }
        ////                }
        ////                if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
        ////                {
        ////                    strfName = pnlName.ToUpper() + "RADIO2";
        ////                    stpos = strfName.Length;
        ////                    enpos = x.ID.Length - stpos;
        ////                    strfName = x.ID.Substring(stpos, enpos).ToString();
        ////                    if (((HtmlInputRadioButton)x).Checked == true)
        ////                    {
        ////                        sbValues.Append(",[" + strfName + "]=" + "0");
        ////                        radioflag = true;
        ////                    }
        ////                    if (radioflag == false)
        ////                    {
        ////                        sbValues.Append(",[" + strfName + "]=" + "Null");
        ////                    }
        ////                }

        ////            }
        ////            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
        ////            {
        ////                if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
        ////                {
        ////                    strfName = pnlName.ToUpper() + "SELECTLIST";
        ////                    stpos = strfName.Length;
        ////                    enpos = x.ID.Length - stpos;
        ////                    strfName = x.ID.Substring(stpos, enpos).ToString();
        ////                    if (((DropDownList)x).SelectedValue != "0")
        ////                    {
        ////                        sbValues.Append(",[" + strfName + "] = " + ((DropDownList)x).SelectedValue.ToString() + " ");
        ////                    }
        ////                    else
        ////                    {
        ////                        sbValues.Append(",[" + strfName + "] =  " + "0");
        ////                    }

        ////                }
        ////            }

        ////        }
        ////    }

        ////    if (ViewState["CustomFieldsData"] == null)
        ////    {
        ////        foreach (Control x in Cntrl.Controls)
        ////        {
        ////            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
        ////            {
        ////                if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
        ////                {
        ////                    if (((TextBox)x).Text != "")
        ////                    {
        ////                        sbValues.Append(",'" + ((TextBox)x).Text.ToString() + "'");
        ////                    }
        ////                    else
        ////                    {
        ////                        sbValues.Append(",' " + "'");
        ////                    }
        ////                }
        ////                else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
        ////                {
        ////                    if (((TextBox)x).Text != "")
        ////                    {
        ////                        sbValues.Append("," + ((TextBox)x).Text.ToString());
        ////                    }
        ////                    else
        ////                    {
        ////                        sbValues.Append(",Null");
        ////                    }

        ////                }
        ////                else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
        ////                {
        ////                    if (((TextBox)x).Text != "")
        ////                    {
        ////                        sbValues.Append(",'" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
        ////                    }
        ////                    else
        ////                    {
        ////                        sbValues.Append(",Null");
        ////                    }
        ////                }
        ////            }
        ////            if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
        ////            {
        ////                if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
        ////                {
        ////                    radioflag = false;
        ////                    if (((HtmlInputRadioButton)x).Checked == true)
        ////                    {
        ////                        sbValues.Append(",1");
        ////                        radioflag = true;
        ////                    }
        ////                }
        ////                if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
        ////                {
        ////                    if (((HtmlInputRadioButton)x).Checked == true)
        ////                    {
        ////                        sbValues.Append(",0");
        ////                        radioflag = true;
        ////                    }
        ////                    if (radioflag == false)
        ////                    {
        ////                        sbValues.Append(",Null");
        ////                    }
        ////                }
        ////            }
        ////            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
        ////            {
        ////                if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
        ////                {
        ////                    if (((DropDownList)x).SelectedValue != "0")
        ////                    {
        ////                        sbValues.Append("," + ((DropDownList)x).SelectedValue.ToString() + " ");
        ////                    }
        ////                    else
        ////                    {
        ////                        sbValues.Append(", " + "0");
        ////                    }

        ////                }
        ////            }
        ////        }
        ////    }
        ////    if (ViewState["CustomFieldsMulti"] != null || ViewState["CustomFieldsMulti"] == null)
        ////    {
        ////        foreach (Control x in Cntrl.Controls)
        ////        {
        ////            if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
        ////            {
        ////                if (x.ID.Substring(0, 28).ToString().ToUpper() == pnlName.ToUpper() + "MULTISELECTLIST")
        ////                {
        ////                    foreach (ListItem li in ((CheckBoxList)x).Items)
        ////                    {
        ////                        if (Convert.ToInt32(li.Selected) == 1)
        ////                        {
        ////                            strmultiselect  += " " + li.Value.ToString() + ",";
        ////                        }
        ////                    }
        ////                    strmultiselect  += "^";
        ////                }
        ////            }
        ////        }
        ////    }
        ////}

        ////private string UpdateCustomFieldsValuesString()
        ////{
        ////    string sqlstr = string.Empty;
        ////    try
        ////    {
        ////    GenerateCustomFieldsValues(pnlCustomList);

        ////    DateTime visitdate = System.DateTime.Now;
        ////    string sqlselect;
        ////    string strdelete;
        ////    ICustomFields CustomFields;
        ////    if (sbValues.ToString().Trim() != "")
        ////    {
        ////        if (ViewState["CustomFieldsData"] != null)
        ////        {
        ////            sbValues = sbValues.Remove(0, 1);
        ////            sqlstr = "UPDATE dtl_CustomField_" + TableName.ToString().Replace("-", "_") + " SET ";
        ////            sqlstr += sbValues.ToString() + " where Ptn_pk= " + ViewState["PtnID"].ToString();
        ////            sqlstr += "and Visit_pk=" + Convert.ToInt32(ViewState["visitPk"].ToString());
        ////        }
        ////        else
        ////        {
        ////            sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
        ////            sqlstr += " VALUES(" + ViewState["PtnID"].ToString() + "," + Session["AppLocationID"] + "," + Convert.ToInt32(ViewState["visitPk"].ToString()) + ",'" + Convert.ToDateTime(ViewState["VisitDate"].ToString()) + "'" + sbValues.ToString() + ")";

        ////            ViewState["CustomFieldsData"] = 1;
        ////        }

        ////    }
        ////    if (strmultiselect.ToString() != "")
        ////    {
        ////        string[] FieldValues = strmultiselect.Split(new char[] { '^' });
        ////        if (arl.Count != 0)
        ////        {
        ////            int p = 0;
        ////            foreach (object obj in arl)
        ////            {
        ////                sqlselect = "";
        ////                strdelete = "";
        ////                if (obj.ToString() != "")
        ////                {
        ////                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
        ////                    strdelete = "DELETE from [" + obj.ToString() + "] where ptn_pk= " + ViewState["PtnID"].ToString() + "";// and LocationID=" + Session["AppLocationID"] +" ";
        ////                    if (sqlstr == "")
        ////                    {
        ////                        sqlstr = strdelete;
        ////                    }
        ////                    else
        ////                    {
        ////                        sqlstr = sqlstr + "!" + strdelete;
        ////                    }
        ////                    //icount = CustomFields.SaveCustomFieldValues(strdelete.ToString());

        ////                    if (FieldValues[p].ToString() != "")
        ////                    {
        ////                        string[] mValues = FieldValues[p].Split(new char[] { ',' });

        ////                        foreach (string str in mValues)
        ////                        {
        ////                            if (str.ToString() != "")
        ////                            {
        ////                                string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
        ////                                Int32 ispos = Convert.ToInt32(strtab.Length);
        ////                                Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;

        ////                                sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,Visit_pk,Visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
        ////                                sqlselect += " VALUES (" + ViewState["PtnID"].ToString() + "," + Session["AppLocationID"] + "," + ViewState["visitPk"] + ",'" + visitdate + "'," + str.ToString() + ")";
        ////                                if (sqlstr == "")
        ////                                {
        ////                                    sqlstr = sqlselect;
        ////                                }
        ////                                else
        ////                                {
        ////                                    sqlstr = sqlstr + "!" + sqlselect;
        ////                                }
        ////                                //icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
        ////                                //if (icount == -1)
        ////                                //{
        ////                                //    return;
        ////                                //}
        ////                            }
        ////                        }
        ////                    }

        ////                    p += 1;
        ////                }
        ////            }

        ////        }
        ////    }
        ////    }
        ////         catch (Exception err)
        ////    {
        ////        MsgBuilder theBuilder = new MsgBuilder();
        ////        theBuilder.DataElements["MessageText"] = err.Message.ToString();
        ////        IQCareMsgBox.Show("#C1", theBuilder, this);
        ////    }
        ////    return sqlstr;
        ////}
        //private void UpdateCustomFieldsValues()
        //{
        //    GenerateCustomFieldsValues(pnlCustomList);
        //    string sqlstr = string.Empty;
        //    DateTime visitdate = System.DateTime.Now;
        //    string sqlselect;
        //    string strdelete;
        //    ICustomFields CustomFields;
        //    if (sbValues.ToString().Trim() != "")
        //    {
        //        if (ViewState["CustomFieldsData"] != null)
        //        {
        //            sbValues = sbValues.Remove(0, 1);
        //            sqlstr = "UPDATE dtl_CustomField_" + TableName.ToString().Replace("-", "_") + " SET ";
        //            sqlstr += sbValues.ToString() + " where Ptn_pk= " + ViewState["PtnID"].ToString();
        //            sqlstr += "and Visit_pk=" + Convert.ToInt32(ViewState["visitPk"].ToString());
        //        }
        //        else
        //        {
        //            sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
        //            sqlstr += " VALUES(" + ViewState["PtnID"].ToString() + "," + Session["AppLocationID"] + "," + Convert.ToInt32(ViewState["visitPk"].ToString()) + ",'" + Convert.ToDateTime(ViewState["VisitDate"].ToString()) + "'" + sbValues.ToString() + ")";

        //            ViewState["CustomFieldsData"] = 1;
        //        }
        //        try
        //        {
        //            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
        //            icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
        //            if (icount == -1)
        //            {
        //                return;
        //            }
        //        }
        //        catch
        //        {
        //        }
        //        finally
        //        {
        //            CustomFields = null;
        //        }

        //    }
        //    if (strmultiselect.ToString() != "")
        //    {
        //        string[] FieldValues = strmultiselect.Split(new char[] { '^' });
        //        if (arl.Count != 0)
        //        {
        //            int p = 0;
        //            foreach (object obj in arl)
        //            {
        //                sqlselect = "";
        //                strdelete = "";
        //                if (obj.ToString() != "")
        //                {
        //                    try
        //                    {
        //                        CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
        //                        strdelete = "DELETE from [" + obj.ToString() + "] where ptn_pk= " + ViewState["PtnID"].ToString() + "";// and LocationID=" + Session["AppLocationID"] +" ";

        //                        icount = CustomFields.SaveCustomFieldValues(strdelete.ToString());

        //                        if (FieldValues[p].ToString() != "")
        //                        {
        //                            string[] mValues = FieldValues[p].Split(new char[] { ',' });

        //                            foreach (string str in mValues)
        //                            {
        //                                if (str.ToString() != "")
        //                                {
        //                                    string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
        //                                    Int32 ispos = Convert.ToInt32(strtab.Length);
        //                                    Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;

        //                                    sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,Visit_pk,Visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
        //                                    sqlselect += " VALUES (" + ViewState["PtnID"].ToString() + "," + Session["AppLocationID"] + "," + ViewState["visitPk"] + ",'" + visitdate + "'," + str.ToString() + ")";
        //                                    icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
        //                                    if (icount == -1)
        //                                    {
        //                                        return;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                    catch
        //                    {
        //                    }
        //                    finally
        //                    {
        //                        CustomFields = null;
        //                    }
        //                }
        //                p += 1;
        //            }
        //        }
        //    }
        //}

        private void Attributes()
        {
            TxtRegistrationDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            TxtRegistrationDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            TxtDtPosHivTest.Attributes.Add("onkeyup", "DateFormat(this, this.value, event, false, '3')");
            TxtDtPosHivTest.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            TxtConfirmHivPositive.Attributes.Add("onkeyup", "DateFormat(this, this.value, event, false, '3')");
            TxtConfirmHivPositive.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            TxtArtStartDate.Attributes.Add("onkeyup", "DateFormat(this, this.value, event, false, '3')");
            TxtArtStartDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            DDPriorExposure.Attributes.Add("onchange", "ShowHidePriorExposure();");
            DDReferredFrom.Attributes.Add("onchange", "othertextbox();");
            txtSysDate.Text = Application["AppCurrentDate"].ToString();
            TxtDOB.Attributes.Add("onkeyup", "DateFormat(this, this.value, event, false, '3')");
            TxtDOB.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); CalculateAge('" + TxtAgeEnrollmentYears.ClientID + "', '" + TxtAgeEnrollmentMonths.ClientID + "','" + TxtDOB.ClientID + "','" + TxtRegistrationDate.ClientID + "'); CalculateAge('" + TxtAgeCurrentYears.ClientID + "','" + TxtAgeCurrentMonths.ClientID + "','" + TxtDOB.ClientID + "','" + txtSysDate.ClientID + "')");
            TxtPtnEnrollment.Attributes.Add("onkeyup", "chkInteger('" + TxtPtnEnrollment.ClientID + "')");
            TxtCD4.Attributes.Add("onblur", "isBetween('" + TxtCD4.ClientID + "', '" + "CD4" + "', '" + 0 + "', '" + 5000 + "')");
            TxtCD4Percent.Attributes.Add("onblur", "isBetween('" + TxtCD4Percent.ClientID + "', '" + "CD4 Percent" + "', '" + 0 + "', '" + 100 + "')");
            TxtTLC.Attributes.Add("onblur", "isBetween('" + TxtTLC.ClientID + "', '" + "TLC" + "', '" + 0 + "', '" + 60 + "');");
            TxtTLC.Attributes.Add("onkeyup", "chkPostiveInteger('" + TxtTLC.ClientID + "');");
            TxtTLCPercent.Attributes.Add("onblur", "isBetween('" + TxtTLCPercent.ClientID + "', '" + "TLC Percent" + "', '" + 0 + "', '" + 100 + "')");
            TxtWeight.Attributes.Add("onkeyup", "chkDecimal('" + TxtWeight.ClientID + "')");
            TxtWeight.Attributes.Add("onBlur", "isBetween('" + TxtWeight.ClientID + "', '" + "Weight" + "', '" + 0 + "', '" + 225 + "')");
        }

        private DataTable TempDT(DataSet tempDS, int CodeID, int SystemID)
        {
            DataTable thetempDT = new DataTable();
            IQCareUtils theUtils = new IQCareUtils();
            DataView thetempDV = new DataView(tempDS.Tables["Mst_Decode"]);
            thetempDV.RowFilter = "CodeID=" + CodeID + " and SystemId=" + SystemID + "";
            thetempDT = (DataTable)theUtils.CreateTableFromDataView(thetempDV);
            return thetempDT;
        }

        private void FillDropDown()
        {
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            ViewState["theDSXML"] = theDSXML;
            DataView theDV = new DataView();
            DataTable theDT = new DataTable();
            if (Request.QueryString["name"] == "Add")
            {
                //Gender
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=4";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDGender, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                //Province
                theDV = new DataView(theDSXML.Tables["Mst_Province"]);
                theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDRegion, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Marital Status
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=12 and SystemId=2";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDMaritalStatus, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                //Referred From
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=17 and SystemId=2";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDReferredFrom, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Prior Exposure
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=1001 and SystemId=2";
                if (theDV.Table != null)
                {
                    //ViewState["theDTPExposure"] = theDV.ToTable();
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    DataRow[] theDR = theDT.Select("CodeID=1001 and Code=4");
                    //if(theDR.Length>1)
                    theDT.Rows.Remove(theDR[0]);
                    theDR = theDT.Select("CodeID=1001 and Code=5");
                    //if (theDR.Length > 1)
                    theDT.Rows.Remove(theDR[0]);
                    theDR = theDT.Select("CodeID=1001 and Code=6");
                    //if (theDR.Length > 1)
                    theDT.Rows.Remove(theDR[0]);
                    ViewState["theDTPExposure"] = theDT;
                    BindManager.BindCombo(DDPriorExposure, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Dispose();
                }
                //Why Eligible
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=1002 and SystemId=2";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDWhyEligible, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Initial Regimen

                theDV = new DataView(theDSXML.Tables["Mst_regimen"]);
                theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    DataRow theDR = theDT.NewRow();
                    theDR[0] = "0";
                    theDR[1] = "Select";
                    theDT.Rows.InsertAt(theDR, 0);
                    ViewState["mst_regimen"] = theDT;
                    InitialRegimen((DataTable)ViewState["mst_regimen"]);
                }
                //WHO Status
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=22 and SystemId=2";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDWHOStage, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                //Functional Status
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=1004 and SystemId=2";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDFunctionalStatus, theDT, "DisplayName", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Initial Regimen
                IPatientRegistration MgrDrugCTC = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                DataSet theDS = MgrDrugCTC.GetDrugGenericCTC();
                ViewState["DrugGeneric"] = theDS.Tables[0];

                //Transferring Information from FindAdd /ART Enrolment Form
                if (!IsPostBack)
                {
                    if (Request.QueryString["PatientID"] == null)
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
                        int patientID = Convert.ToInt32(Request.QueryString["PatientID"]);
                        ViewState["PtnID"] = patientID;
                        DataTable RecordDT = MgrPMTCT.GetPatientRecord(patientID);
                        this.TxtLastName.Text = RecordDT.Rows[0]["LastName"].ToString();
                        this.TxtFirstName.Text = RecordDT.Rows[0]["FirstName"].ToString();
                        this.DDGender.SelectedValue = RecordDT.Rows[0]["Sex"].ToString();
                        this.TxtDOB.Text = ((DateTime)RecordDT.Rows[0]["DOB"]).ToString(Session["AppDateFormat"].ToString());
                    }

                    /////////////////////////////////////////////////////////////////////////////////////////////////
                }
            }
            else if (Request.QueryString["name"] == "Edit")
            {
                //Gender
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=4";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDGender, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                //Province
                theDV = new DataView(theDSXML.Tables["Mst_Province"]);
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDRegion, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Marital Status
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=12 and SystemId=2";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDMaritalStatus, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Referred From
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=17 and SystemId=2";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDReferredFrom, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Prior Exposure
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=1001 and SystemId=2";
                if (theDV.Table != null)
                {
                    ViewState["theDTPExposure"] = theDV.ToTable();
                }
                //Why Eligible
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=1002 and SystemId=2";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDWhyEligible, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Initial Regimen

                theDV = new DataView(theDSXML.Tables["Mst_regimen"]);
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    DataRow theDR = theDT.NewRow();
                    theDR[0] = "0";
                    theDR[1] = "Select";
                    theDT.Rows.InsertAt(theDR, 0);
                    ViewState["mst_regimen"] = theDT;
                    InitialRegimen((DataTable)ViewState["mst_regimen"]);
                }
                //WHO Status
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=22 and SystemId=2";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDWHOStage, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                //Functional Status
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=1004 and SystemId=2";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(DDFunctionalStatus, theDT, "DisplayName", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Initial Regimen
                IPatientRegistration MgrDrugCTC = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                DataSet theDS = MgrDrugCTC.GetDrugGenericCTC();
                ViewState["DrugGeneric"] = theDS.Tables[0];
            }
        }

        private void BindDistrict()
        {
            IPatientRegistration MgrDropDown = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet theDS = MgrDropDown.theDropdown(DDRegion.SelectedValue, "0");
            BindFunctions BindManager = new BindFunctions();
            BindManager.BindCombo(DDDistrict, theDS.Tables[0], "District", "ID");
            BindDivision_Ward();
            BindVillage();
            BindVillageChairperson();
        }

        private void BindDivision_Ward()
        {
            IPatientRegistration MgrDropDown = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet theDS = MgrDropDown.theDropdown(DDDistrict.SelectedValue, "1");
            BindFunctions BindManager = new BindFunctions();
            BindManager.BindCombo(DDDivision, theDS.Tables[0], "Division", "ID");
            BindManager.BindCombo(DDWard, theDS.Tables[1], "Ward", "ID");
            BindVillage();
            BindVillageChairperson();
        }

        private void BindVillage()
        {
            IPatientRegistration MgrDropDown = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet theDS = MgrDropDown.theDropdown(DDWard.SelectedValue, "2");
            BindFunctions BindManager = new BindFunctions();
            BindManager.BindCombo(DDVillage, theDS.Tables[0], "Village", "ID");
            BindVillageChairperson();
        }

        private void ShowpriorExposure()
        {
            DataTable DT = TempDT((DataSet)ViewState["theDSXML"], 1001, 2);
            if (DDPriorExposure.SelectedItem.Text == "ART Transfer in (with records)")
            {
                string script = "";
                script = "<script language = 'javascript' defer='defer' id='PriorExposure'>\n";
                script += "show('divPriorExposure');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "PriorExposure", script);
            }
            else if (DDPriorExposure.SelectedItem.Text == "Non-ART Transfer in with records")
            {
                string script = "";
                script = "<script language = 'javascript' defer='defer' id='PriorExposure'>\n";
                script += "show('divPriorExposure');\n";
                script += "hide('TrART_I');\n";
                script += "hide('TrART_II');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "PriorExposure", script);
            }
        }

        private void BindVillageChairperson()
        {
            IPatientRegistration MgrDropDown = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet theDS = MgrDropDown.theDropdown(DDVillage.SelectedValue, "3");
            if (theDS.Tables[0].Rows.Count != 0)
            {
                TxtChairPerson.Text = theDS.Tables[0].Rows[0]["ChairperName"].ToString();
            }
            else { TxtChairPerson.Text = ""; }
        }

        protected void AddUpdatePatient()
        {
            htParameters = new Hashtable();
            htParameters.Clear();
            if (Request.QueryString["Name"] == "Add" && Request.QueryString["PatientID"] == null)
            {
                htParameters.Add("ptn_pk", "");
            }
            else if (Request.QueryString["Name"] == "Add" && Request.QueryString["PatientID"] != null)
            {
                int patientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                htParameters.Add("ptn_pk", patientID);
            }

            if (Request.QueryString["name"] == "Edit")
            {
                htParameters.Add("Update", "1");
                htParameters.Add("ptn_pk", ViewState["PtnID"].ToString());
                htParameters.Add("CountryID", TxtRegion.Text);
                htParameters.Add("PosID", TxtDistrict.Text);
                htParameters.Add("SatelliteID", TxtFacility.Text);
            }
            else
            {
                htParameters.Add("Update", "0");
                htParameters.Add("CountryID", Session["AppCountryId"].ToString());
                htParameters.Add("PosID", Session["AppPosID"].ToString());
                htParameters.Add("SatelliteID", Session["AppSatelliteId"].ToString());
            }
            htParameters.Add("LocationID", Session["AppLocationId"].ToString());
            htParameters.Add("EnrolmentID", TxtPtnEnrollment.Text);
            htParameters.Add("FileReferenceID", TxtFreference.Text);
            htParameters.Add("FirstName", TxtFirstName.Text.Trim());
            htParameters.Add("MiddleName", TxtMiddleName.Text.Trim());
            htParameters.Add("LastName", TxtLastName.Text.Trim());
            htParameters.Add("RegDate", TxtRegistrationDate.Text.Trim());
            htParameters.Add("Gender", DDGender.SelectedValue);
            htParameters.Add("DOB", TxtDOB.Text);
            int rbtnDOBPrecision = this.RbtnDOBPrecEstimated.Checked == true ? 0 : (this.RbtnDOBPrecExact.Checked == true ? 1 : 2);
            htParameters.Add("DOBPrecision", rbtnDOBPrecision);
            htParameters.Add("Maristatus", DDMaritalStatus.SelectedValue);
            htParameters.Add("Phone", TxtPhone.Text.Trim());
            htParameters.Add("ConDetails", TxtContactDetails.Text.Trim());
            htParameters.Add("Region", DDRegion.SelectedValue);
            htParameters.Add("District", DDDistrict.SelectedValue);
            htParameters.Add("Division", DDDivision.SelectedValue);
            htParameters.Add("Ward", DDWard.SelectedValue);
            htParameters.Add("Village", DDVillage.SelectedValue);
            htParameters.Add("ChairPerson", TxtChairPerson.Text);
            htParameters.Add("TCLLeader", TxtTCLeader.Text);
            htParameters.Add("TCLContact", TxtTCLContact.Text);
            htParameters.Add("HHead", TxtHHHead.Text);
            htParameters.Add("Hcontact", TxtHHHcontact.Text);
            htParameters.Add("SupportName", TxtTreatSupporterName.Text);
            htParameters.Add("DrugAllery", txtdrugallergies.Text);
            htParameters.Add("TsAddress", TxtTsAddress.Text);
            htParameters.Add("TsPhone", TxtTSPhone.Text);
            htParameters.Add("ComSOrganisation", TxtComsOrganization.Text.Trim());
            htParameters.Add("PosHivTest", TxtDtPosHivTest.Text);
            htParameters.Add("ConfirmHivPositive", TxtConfirmHivPositive.Text);
            if (DDReferredFrom.SelectedItem.Text == "Other")
            {
                htParameters.Add("ReferredFrom", DDReferredFrom.SelectedValue);
                htParameters.Add("ReferredFromOther", TxtReferredFromOther.Text);
            }
            else
            {
                htParameters.Add("ReferredFrom", DDReferredFrom.SelectedValue);
                htParameters.Add("ReferredFromOther", "");
            }

            if (DDPriorExposure.SelectedItem.Text == "ART Transfer in (with records)")
            {
                htParameters.Add("PriorExposure", DDPriorExposure.SelectedValue);
                htParameters.Add("ArtStartDate", TxtArtStartDate.Text);
                htParameters.Add("WhyEligible", DDWhyEligible.SelectedValue);
                htParameters.Add("WHOStage", DDWHOStage.SelectedValue);
                htParameters.Add("FunStatus", DDFunctionalStatus.SelectedValue);
                htParameters.Add("Weight", TxtWeight.Text);
                htParameters.Add("InitialRegimenCode", ddregimen.SelectedValue);
                htParameters.Add("InitialRegimenAbb", TxtInitialRegimen.Text);
                htParameters.Add("CD4", TxtCD4.Text);
                htParameters.Add("CD4Percent", TxtCD4Percent.Text);
                htParameters.Add("TLC", TxtTLC.Text);
                htParameters.Add("TLCPercent", TxtTLCPercent.Text);
            }
            else if (DDPriorExposure.SelectedItem.Text == "Non-ART Transfer in with records")
            {
                htParameters.Add("PriorExposure", DDPriorExposure.SelectedValue);
                htParameters.Add("ArtStartDate", "");
                htParameters.Add("WhyEligible", "");
                htParameters.Add("InitialRegimenCode", "");
                htParameters.Add("InitialRegimenAbb", "");
                htParameters.Add("WHOStage", DDWHOStage.SelectedValue);
                htParameters.Add("FunStatus", DDFunctionalStatus.SelectedValue);
                htParameters.Add("Weight", TxtWeight.Text);
                htParameters.Add("CD4", TxtCD4.Text);
                htParameters.Add("CD4Percent", TxtCD4Percent.Text);
                htParameters.Add("TLC", TxtTLC.Text);
                htParameters.Add("TLCPercent", TxtTLCPercent.Text);
            }
            else
            {
                htParameters.Add("PriorExposure", DDPriorExposure.SelectedValue);
                htParameters.Add("ArtStartDate", "");
                htParameters.Add("WhyEligible", "");
                htParameters.Add("WHOStage", "");
                htParameters.Add("FunStatus", "");
                htParameters.Add("Weight", "");
                htParameters.Add("InitialRegimenCode", "");
                htParameters.Add("InitialRegimenAbb", "");
                htParameters.Add("CD4", "");
                htParameters.Add("CD4Percent", "");
                htParameters.Add("TLC", "");
                htParameters.Add("TLCPercent", "");
            }
            htParameters.Add("UserID", Session["AppUserId"]);
        }

        protected void BindDDExposure(string value)
        {
            DDGender.SelectedValue = value;
            BindFunctions BindManager = new BindFunctions();

            DataView theDVPExposure = new DataView((DataTable)ViewState["theDTPExposure"]);
            if (DDGender.SelectedValue == "16")
            {
                theDVPExposure.RowFilter = "ID<>262 and ID<>263";
                BindManager.BindCombo(DDPriorExposure, theDVPExposure.ToTable(), "Name", "ID");
            }
            else { BindManager.BindCombo(DDPriorExposure, theDVPExposure.ToTable(), "Name", "ID"); }
        }

        protected void ScriptPriorExposure()
        {
            DataTable theDTPE = (DataTable)ViewState["theDTPExposure"];
            String priorExp1 = "", priorExp2 = "";
            if (theDTPE.Rows.Count > 0)
            {
                if (theDTPE.Rows[4]["ID"] != System.DBNull.Value)
                {
                    priorExp1 = theDTPE.Rows[4]["ID"].ToString();
                }
                if (theDTPE.Rows[5]["ID"] != System.DBNull.Value)
                {
                    priorExp2 = theDTPE.Rows[5]["ID"].ToString();
                }
            }

            string strPExp = "<script language = 'javascript' id= 'ConPExposure'>\n";
            strPExp += "function ShowHidePriorExposure()\n";
            strPExp += "{\n";
            strPExp += "if(document.getElementById('ctl00_IQCareContentPlaceHolder_DDPriorExposure').value==" + priorExp1 + ")\n";
            strPExp += "{document.getElementById('ctl00_IQCareContentPlaceHolder_DDWHOStage').value=0;\n";
            strPExp += "document.getElementById('ctl00_IQCareContentPlaceHolder_DDFunctionalStatus').value=0;\n";
            strPExp += "document.getElementById('ctl00_IQCareContentPlaceHolder_TxtWeight').value=''\n";
            strPExp += "document.getElementById('ctl00_IQCareContentPlaceHolder_TxtCD4').value='';\n";
            strPExp += "document.getElementById('ctl00_IQCareContentPlaceHolder_TxtCD4Percent').value='';\n";
            strPExp += "document.getElementById('ctl00_IQCareContentPlaceHolder_TxtTLC').value='';\n";
            strPExp += "document.getElementById('ctl00_IQCareContentPlaceHolder_TxtTLCPercent').value='';\n";
            strPExp += "document.getElementById('divPriorExposure').style.display='inline'\n";
            strPExp += "document.getElementById('TrART_I').style.display='inline'\n";
            strPExp += "document.getElementById('TrART_II').style.display='inline'}\n";
            strPExp += "else if (document.getElementById('ctl00_IQCareContentPlaceHolder_DDPriorExposure').value==" + priorExp2 + "){\n";
            strPExp += "document.getElementById('ctl00_IQCareContentPlaceHolder_TxtArtStartDate').value='';\n";
            strPExp += "document.getElementById('ctl00_IQCareContentPlaceHolder_DDWhyEligible').value=0;\n";
            strPExp += "document.getElementById('ctl00_IQCareContentPlaceHolder_ddregimen').value=0;\n";
            strPExp += "document.getElementById('ctl00_IQCareContentPlaceHolder_TxtInitialRegimen').value='';\n";
            strPExp += "document.getElementById('divPriorExposure').style.display='inline'\n";
            strPExp += "document.getElementById('TrART_I').style.display='none'\n";
            strPExp += "document.getElementById('TrART_II').style.display='none'}\n";
            strPExp += "else\n";
            strPExp += "document.getElementById('divPriorExposure').style.display='none'\n";
            strPExp += "}\n";
            strPExp += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "ConPExposure", strPExp);
        }

        protected void LoadPatientData()
        {
            if (Request.QueryString["name"] == "Edit")
            {
                Session.Add("EnrollParams", "");
                string LocId1 = Session["AppCountryId"] + "-" + Session["AppPosID"] + "-" + Session["AppSatelliteId"];
                IPatientRegistration PtnMgrCTC = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                DataSet theDS = PtnMgrCTC.GetPatientDetailsCTC(Request.QueryString["PatientId"].ToString(), "", "", "", "", 0, 0);
                ViewState["PtnID"] = Request.QueryString["PatientId"].ToString();
                ViewState["EnrolID"] = theDS.Tables[0].Rows[0]["EnrolmentID"].ToString();
                ViewState["visitPk"] = theDS.Tables[0].Rows[0]["VisitPk"].ToString();
                ViewState["FileRefID"] = theDS.Tables[0].Rows[0]["PatientClinicID"].ToString();
                ViewState["Transferred"] = theDS.Tables[7].Rows[0]["Tranfer"].ToString();
                TxtRegion.Text = theDS.Tables[0].Rows[0]["CountryID"].ToString();
                TxtDistrict.Text = theDS.Tables[0].Rows[0]["PosID"].ToString();
                TxtFacility.Text = theDS.Tables[0].Rows[0]["SatelliteID"].ToString();
                TxtPtnEnrollment.Text = theDS.Tables[0].Rows[0]["PatientEnrollmentID"].ToString();
                string LocId2 = TxtRegion.Text + "-" + TxtDistrict.Text + "-" + TxtFacility.Text;

                DataTable theDTPE = (DataTable)ViewState["theDTPExposure"];

                BindFunctions BindManager = new BindFunctions();
                ScriptPriorExposure();
                if (Convert.ToInt32(ViewState["Transferred"].ToString()) > 0)
                {
                    BindManager.BindCombo(DDPriorExposure, theDTPE, "Name", "ID");
                }
                else if (LocId1 != LocId2)
                {
                    BindManager.BindCombo(DDPriorExposure, theDTPE, "Name", "ID");
                }
                else
                {
                    DataRow[] theDR = theDTPE.Select("CodeID=1001 and Code=4");
                    theDTPE.Rows.Remove(theDR[0]);
                    theDR = theDTPE.Select("CodeID=1001 and Code=5");
                    theDTPE.Rows.Remove(theDR[0]);
                    theDR = theDTPE.Select("CodeID=1001 and Code=6");
                    theDTPE.Rows.Remove(theDR[0]);
                    BindManager.BindCombo(DDPriorExposure, theDTPE, "Name", "ID");
                }

                if (Session["EnrollFlag"].ToString() == "1")
                {
                    TxtPtnEnrollment.ReadOnly = false;
                }
                else { TxtPtnEnrollment.ReadOnly = true; }
                if (theDS.Tables[0].Rows[0]["DQ"].ToString() == "1")
                { btnDQ.CssClass = "greenbutton"; }
                if (theDS.Tables[0].Rows[0]["CareEndsts"].ToString() == "1")
                {
                    btnsave.Enabled = false; btnDQ.Enabled = false;
                }
                TxtFreference.Text = theDS.Tables[0].Rows[0]["PatientClinicID"].ToString();
                TxtFirstName.Text = theDS.Tables[0].Rows[0]["FirstName"].ToString();
                TxtMiddleName.Text = theDS.Tables[0].Rows[0]["MiddleName"].ToString();
                TxtLastName.Text = theDS.Tables[0].Rows[0]["LastName"].ToString();
                TxtRegistrationDate.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDS.Tables[0].Rows[0]["VisitDate"].ToString()));
                BindDDExposure(theDS.Tables[0].Rows[0]["Sex"].ToString());
                TxtDOB.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDS.Tables[0].Rows[0]["DOB"].ToString()));
                if (theDS.Tables[0].Rows[0]["DobPrecision"].ToString() == "1")
                {
                    RbtnDOBPrecExact.Checked = true;
                }
                else if (theDS.Tables[0].Rows[0]["DobPrecision"].ToString() == "0")
                {
                    RbtnDOBPrecEstimated.Checked = true;
                }
                TxtAgeCurrentYears.Text = theDS.Tables[0].Rows[0]["AGE"].ToString();
                TxtAgeCurrentMonths.Text = theDS.Tables[0].Rows[0]["Month"].ToString();
                TxtAgeEnrollmentYears.Text = theDS.Tables[0].Rows[0]["EnrolAge"].ToString();
                TxtAgeEnrollmentMonths.Text = theDS.Tables[0].Rows[0]["EnrolMonth"].ToString();
                DDMaritalStatus.SelectedValue = theDS.Tables[0].Rows[0]["MaritalStatus"].ToString();
                TxtPhone.Text = theDS.Tables[0].Rows[0]["Phone"].ToString();
                TxtContactDetails.Text = theDS.Tables[0].Rows[0]["Address"].ToString();
                DDRegion.SelectedValue = theDS.Tables[0].Rows[0]["Province"].ToString();
                BindDistrict();
                DDDistrict.SelectedValue = theDS.Tables[0].Rows[0]["DistrictName"].ToString();
                BindDivision_Ward();
                DDDivision.SelectedValue = theDS.Tables[0].Rows[0]["Division"].ToString();
                DDWard.SelectedValue = theDS.Tables[0].Rows[0]["Ward"].ToString();
                BindVillage();
                DDVillage.SelectedValue = theDS.Tables[0].Rows[0]["VillageName"].ToString();
                BindVillageChairperson();
                TxtTCLeader.Text = theDS.Tables[1].Rows[0]["TenCellLeader"].ToString();
                TxtTCLContact.Text = theDS.Tables[1].Rows[0]["TenCellLeaderAddress"].ToString();
                TxtHHHead.Text = theDS.Tables[2].Rows[0]["HouseHoldHeadName"].ToString();
                TxtHHHcontact.Text = theDS.Tables[2].Rows[0]["HouseHoldHeadAdd"].ToString();
                TxtTreatSupporterName.Text = theDS.Tables[1].Rows[0]["TreatmentSupportName"].ToString();
                TxtTsAddress.Text = theDS.Tables[1].Rows[0]["TreatmentSupportAddress"].ToString();
                TxtTSPhone.Text = theDS.Tables[1].Rows[0]["TreatmentSupportPhone"].ToString();
                TxtComsOrganization.Text = theDS.Tables[1].Rows[0]["CommunitySupportGroup"].ToString();
                if (theDS.Tables[3].Rows.Count > 0)
                {
                    if (theDS.Tables[3].Rows[0]["FirstHIVPosTestDate"] != System.DBNull.Value)
                    {
                        TxtDtPosHivTest.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDS.Tables[3].Rows[0]["FirstHIVPosTestDate"].ToString()));
                    }
                }
                if (theDS.Tables[3].Rows.Count > 0)
                {
                    if (theDS.Tables[3].Rows[0]["ConfirmHIVPosDate"] != System.DBNull.Value)
                    {
                        TxtConfirmHivPositive.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDS.Tables[3].Rows[0]["ConfirmHIVPosDate"].ToString()));
                    }
                }
                DDReferredFrom.SelectedValue = theDS.Tables[0].Rows[0]["ReferredFrom"].ToString();
                if (DDReferredFrom.SelectedItem.Text == "Other")
                {
                    string script = "";
                    script = "<script language = 'javascript' defer='defer' id='ReferredFrom'>\n";
                    script += "show('divreferredfromOther');\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "ReferredFrom", script);
                }
                TxtReferredFromOther.Text = theDS.Tables[0].Rows[0]["ReferredFromSpecify"].ToString();
                txtdrugallergies.Text = theDS.Tables[3].Rows[0]["DrugAllergySpecify"].ToString();
                DDPriorExposure.SelectedValue = theDS.Tables[3].Rows[0]["PrevHIVCare"].ToString();
                ShowpriorExposure();
                if (theDS.Tables[3].Rows[0]["ARTStartDate"] != System.DBNull.Value)
                {
                    TxtArtStartDate.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDS.Tables[3].Rows[0]["ARTStartDate"].ToString()));
                }
                DDWhyEligible.SelectedValue = theDS.Tables[3].Rows[0]["WhyEligible"].ToString();
                if (theDS.Tables[4].Rows.Count > 0)
                {
                    DDWHOStage.SelectedValue = theDS.Tables[4].Rows[0]["WHOStage"].ToString();
                    DDFunctionalStatus.SelectedValue = theDS.Tables[4].Rows[0]["WABStage"].ToString();
                }
                if (theDS.Tables[5].Rows.Count > 0)
                {
                    TxtWeight.Text = theDS.Tables[5].Rows[0]["Weight"].ToString();
                    TxtTLC.Text = theDS.Tables[5].Rows[0]["TLC"].ToString();
                    TxtTLCPercent.Text = theDS.Tables[5].Rows[0]["TLCPercent"].ToString();
                }
                if (theDS.Tables[6].Rows.Count > 0)
                {
                    TxtInitialRegimen.Text = theDS.Tables[6].Rows[0]["PrevARVRegimen"].ToString();
                    TxtCD4.Text = theDS.Tables[6].Rows[0]["PrevARVsCD4"].ToString();
                    TxtCD4Percent.Text = theDS.Tables[6].Rows[0]["PrevARVsCD4Percent"].ToString();
                    ddregimen.SelectedValue = theDS.Tables[6].Rows[0]["InitialRegimenCode"].ToString();
                }
                DataTable theDTSavedReg = (DataTable)ViewState["DrugGeneric"];
                string[] theStrRegimen = TxtInitialRegimen.Text.Split(new Char[] { '-' });

                if (TxtInitialRegimen.Text != "")
                {
                    DataTable theDT = FillDataHistRegimen(theStrRegimen, theDTSavedReg);
                    ViewState["SelectedDrug"] = theDT;
                }

                //Custom Field
                ////if (ViewState["ControlCreated"] != null)
                ////{ FillOldData(pnlCustomList, Convert.ToInt32(ViewState["PtnID"].ToString())); }
                FillOldData(Convert.ToInt32(ViewState["PtnID"].ToString()));
            }
        }

        protected string FillHistRegimen(DataTable theDT)
        {
            string strRegimen = "";
            if (theDT != null)
            {
                foreach (DataRow dr in theDT.Rows)
                {
                    if (strRegimen == "")
                    {
                        strRegimen = dr["DrugAbbr"].ToString();
                    }
                    else
                    {
                        strRegimen = strRegimen + "-" + dr["DrugAbbr"].ToString();
                    }
                }
            }
            return strRegimen;
        }

        protected void Authenticationright()
        {
            AuthenticationManager AuthenticationMgr = new AuthenticationManager();
            if (AuthenticationMgr.HasFunctionRight(ApplicationAccess.Enrollment, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;
            }
            if (AuthenticationMgr.HasFunctionRight(ApplicationAccess.Enrollment, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
                btnDQ.Enabled = false;
            }
            if (AuthenticationMgr.HasFunctionRight(ApplicationAccess.Enrollment, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
                btnDQ.Enabled = false;
            }

            if (AuthenticationMgr.HasFunctionRight(ApplicationAccess.Enrollment, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
                btnDQ.Enabled = false;
            }
        }

        private void FindAddEnrollData()
        {
            if (Session["EnrollParams"].ToString() != "")
            {
                Hashtable theHT = (Hashtable)Session["EnrollParams"];
                TxtFirstName.Text = theHT["FirstName"].ToString();
                TxtLastName.Text = theHT["LastName"].ToString();
                TxtMiddleName.Text = theHT["MiddleName"].ToString();
                TxtPtnEnrollment.Text = theHT["EnrollmentNo"].ToString();
                TxtFreference.Text = theHT["ClinicNo"].ToString();
                TxtDOB.Text = theHT["Date of Birth"].ToString();
                if (theHT["Sex"].ToString() == "")
                {
                    DDGender.SelectedValue = "0";
                }
                else { DDGender.SelectedValue = theHT["Sex"].ToString(); }
                Session.Remove("EnrollParams");
            }
        }

        private DataTable PtnRegCTCselectedDataTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theDT.Columns.Add("DrugAbbr", System.Type.GetType("System.String"));
            theDT.PrimaryKey = new DataColumn[] { theDT.Columns[0] };
            return theDT;
        }

        private DataTable FillDataHistRegimen(string[] str, DataTable theDTSavedReg)
        {
            DataTable theDT = PtnRegCTCselectedDataTable();
            DataView theDV = new DataView(theDTSavedReg);
            foreach (string reg in str)
            {
                theDV.RowFilter = "Abbr Like '" + reg + "%'";
                if (theDV.Count > 0)
                {
                    DataRow theDR = theDT.NewRow();
                    theDR[0] = theDV[0][0];
                    theDR[1] = theDV[0][1];
                    theDR[2] = theDV[0][2];

                    DataRow theTempDR;
                    theTempDR = theDT.Rows.Find(theDV[0][0]);
                    if (theTempDR == null)
                    {
                        theDT.Rows.Add(theDR);
                    }
                    DataRow[] theDR1 = theDTSavedReg.Select("DrugId=" + theDV[0][0]);

                    theDTSavedReg.Rows.Remove(theDR1[0]);
                    ViewState["DrugGeneric"] = theDTSavedReg;
                }
            }
            return theDT;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //(Master.FindControl("lblRoot") as Label).Text = "Clinical >>";
                //(Master.FindControl("lblMark") as Label).Visible = false;
                //(Master.FindControl("lblheader") as Label).Text = "Enrollment";
                //(Master.FindControl("lblformname") as Label).Text = "Enrollment";
                //(Master.FindControl("PanelPatiInfo") as Panel).Visible = false;
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Enrollment";
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Enrollment";
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
                //(Master.FindControl("PanelPatiInfo") as Label).Visible = false;
                if (Request.QueryString["sts"].ToString() == "1")
                {
                    btnsave.Enabled = false;
                }
                //(Master.FindControl("lblpntStatus") as Label).Text = Request.QueryString["sts"].ToString();
                //   Ajax.Utility.RegisterTypeForAjax(typeof(ClinicalForms_frmClinical_PatientRegistrationCTC));
                PutCustomControl();
                Authenticationright();
                if (!IsPostBack)
                {
                    TxtRegion.Text = Session["AppCountryId"].ToString();
                    TxtDistrict.Text = Session["AppPosID"].ToString();
                    TxtFacility.Text = Session["AppSatelliteId"].ToString();

                    Attributes(); FillDropDown();
                    LoadPatientData();
                    //FindAddEnrollData();
                    if (Request.QueryString["locationid"] != null)
                        Session["fmLocationID"] = Request.QueryString["locationid"].ToString();
                    if (Request.QueryString["sts"] != null)
                        Session["fmsts"] = Request.QueryString["sts"].ToString();
                }
                else
                {
                    if ((DataTable)Session["DrugDataPtnReg"] != null)
                    {
                        ViewState["DrugGeneric"] = (DataTable)Session["DrugDataPtnReg"];
                        DataTable theDT = (DataTable)Session["SelectedData"];
                        ViewState["SelectedDrug"] = theDT;
                        Session.Remove("DrugDataPtnReg");
                        string Regimen = FillHistRegimen(theDT);
                        TxtInitialRegimen.Text = Regimen;
                        InitialRegimen((DataTable)ViewState["mst_regimen"]);
                        ShowpriorExposure();
                    }
                }

                ShowpriorExposure();
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        protected void ddregion_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDistrict();
        }

        protected void dddistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDivision_Ward();
        }

        protected void ddWard_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillage();
        }

        /// <summary>
        /// Handles the AsyncPostBackError event of the ActionScriptManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AsyncPostBackErrorEventArgs"/> instance containing the event data.</param>
        protected void ActionScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            string message = e.Exception.Message;
            Master.PageScriptManager.AsyncPostBackErrorMessage = message;
        }

        // [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        [WebMethod(EnableSession = true), ScriptMethod]
        public static string GetDuplicateRecord(string strfname, string strmname, string strlname, string address, string Phone)
        {
            IPatientRegistration PatientManager;
            StringBuilder objBilder = new StringBuilder();
            PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            DataSet dsPatient = new DataSet();
            dsPatient = PatientManager.GetDuplicatePatientSearchResults(strlname, strmname, strfname, address, Phone);

            if (dsPatient.Tables[0].Rows.Count > 0)
            {
                objBilder.Append("<table border='0'  width='100%'>");
                objBilder.Append("<tr style='background-color:#e1e1e1'>");
                objBilder.Append("<td class='smallerlabel'>PatientID</td>");
                objBilder.Append("<td class='smallerlabel'>F name</td>");
                objBilder.Append("<td class='smallerlabel'>M name</td>");
                objBilder.Append("<td class='smallerlabel'>L name</td>");
                objBilder.Append("<td class='smallerlabel'>RegistrationDate</td>");
                objBilder.Append("<td class='smallerlabel'>File Reference</td>");
                objBilder.Append("<td class='smallerlabel'>Address</td>");
                objBilder.Append("<td class='smallerlabel'>Phone</td>");
                objBilder.Append("<td class='smallerlabel'>Facility</td>");
                objBilder.Append("</tr>");
                for (int i = 0; i < dsPatient.Tables[0].Rows.Count; i++)
                {
                    objBilder.Append("<tr>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["PatientEnrollmentID"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["firstname"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["middlename"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["lastname"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["RegistrationDate"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["PatientClinicID"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["Address"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["Phone"].ToString() + "</td>");
                    objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["FacilityName"].ToString() + "</td>");
                    objBilder.Append("</tr>");
                }
                objBilder.Append("</table>");
            }
            return objBilder.ToString();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            //string strCustomField = string.Empty;
            if (FieldValidation() == false)
            { return; }
            AddUpdatePatient();
            IPatientRegistration PtnMgrCTC = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            if (Request.QueryString["name"] == "Add")
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.Enrollment, (DataSet)ViewState["CustomFieldsDS"]);
                ////if (ViewState["ControlCreated"] != null)
                ////{
                ////    strCustomField=InsertCustomFieldsValuesString("99999");
                ////    ViewState["CustomFieldsData"] = 1;
                ////}
                DataTable theDTsave = PtnMgrCTC.SavePatientRegistrationCTC(htParameters, 0, theCustomDataDT);
                ViewState["PtnID"] = theDTsave.Rows[0]["PtnID"].ToString();
                SaveCancel();
            }
            else
            {
                if (Request.QueryString["name"] == "Edit" && ViewState["PtnID"] != null)
                {
                    if (Session["status"].ToString() == "Add")
                    {
                        SaveCancel();
                    }
                    else
                    {
                        UpdateCancel();
                    }

                    ////if (ViewState["ControlCreated"] != null)
                    ////{
                    ////    if (ViewState["CustomFieldsData"] == null && ViewState["CustomFieldsMulti"] == null)
                    ////    {
                    ////        strCustomField=InsertCustomFieldsValuesString("99999");
                    ////        ViewState["CustomFieldsData"] = 1;
                    ////    }
                    ////    else
                    ////    {
                    ////        strCustomField=UpdateCustomFieldsValuesString();
                    ////    }
                    ////}
                }
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.Enrollment, (DataSet)ViewState["CustomFieldsDS"]);

                DataTable theDTsave = PtnMgrCTC.SavePatientRegistrationCTC(htParameters, 0, theCustomDataDT);
                ViewState["PtnID"] = theDTsave.Rows[0]["PtnID"].ToString();
            }
            PtnMgrCTC = null;
        }

        protected void DDVillage_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindVillageChairperson();
        }

        protected void btnDQ_Click(object sender, EventArgs e)
        {
            string msg = DataQuality_Msg();
            ////string strCustomField = string.Empty;
            if (msg.Length > 69)
            {
                MsgBuilder theBuilder1 = new MsgBuilder();
                theBuilder1.DataElements["MessageText"] = msg;
                IQCareMsgBox.Show("#C1", theBuilder1, this);
                return;
            }

            if (FieldValidation() == false)
            { return; }
            AddUpdatePatient();
            IPatientRegistration PtnMgrCTC = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
            if (ViewState["PtnID"] == null)
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.Enrollment, (DataSet)ViewState["CustomFieldsDS"]);
                ////if (ViewState["ControlCreated"] != null)
                ////{
                ////    strCustomField=InsertCustomFieldsValuesString("99999");
                ////    ViewState["CustomFieldsData"] = 1;
                ////}
                DataTable theDTsave = PtnMgrCTC.SavePatientRegistrationCTC(htParameters, 1, theCustomDataDT);
                btnDQ.CssClass = "greenbutton";
                ViewState["PtnID"] = theDTsave.Rows[0]["PtnID"].ToString();
                DQCancel();
            }
            else
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.Enrollment, (DataSet)ViewState["CustomFieldsDS"]);

                //if (ViewState["ControlCreated"] != null)
                //{
                //    if (ViewState["CustomFieldsData"] == null && ViewState["CustomFieldsMulti"] == null)
                //    {
                //        strCustomField=InsertCustomFieldsValuesString("99999");
                //        ViewState["CustomFieldsData"] = 1;
                //    }
                //    else
                //    {
                //        strCustomField=UpdateCustomFieldsValuesString();
                //    }
                //}
                DataTable theDTsave = PtnMgrCTC.SavePatientRegistrationCTC(htParameters, 1, theCustomDataDT);
                //btnDQ.CssClass = "greenbutton";
                ViewState["PtnID"] = theDTsave.Rows[0]["PtnID"].ToString();
                if (Request.QueryString["name"] == "Edit" && ViewState["PtnID"] != null)
                {
                    if (Session["status"].ToString() == "Add") { DQCancel(); }
                    else { DQUpdateCancel(); }
                }
            }
            //Save_Update();
            PtnMgrCTC = null;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Request.QueryString["sts"].ToString();
            if ((Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text == "1")
            {
                if (Request.QueryString["name"] == "Add")
                {
                    string theUrl;
                    theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Request.QueryString["PatientId"].ToString());
                    Response.Redirect(theUrl);
                }
                else if (Request.QueryString["name"] == "Edit")
                {
                    string theUrl;
                    theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString(), Request.QueryString["sts"].ToString());
                    Response.Redirect(theUrl);
                }
            }
            else
            {
                if (Request.QueryString["name"] == "Add")
                {
                    if (ViewState["PtnID"] == null)
                    {
                        Response.Redirect("~/frmFindAddPatient.aspx");
                    }
                    else
                    {
                        string theUrl;
                        theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", ViewState["PtnID"].ToString());
                        Response.Redirect(theUrl);
                    }
                }
                else
                {
                    string theUrl;
                    theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString(), Request.QueryString["sts"].ToString());
                    Response.Redirect(theUrl);
                }
            }
        }

        protected void DDGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDDExposure(DDGender.SelectedValue);
        }

        protected void btnRegimen_Click(object sender, EventArgs e)
        {
            string theScript;
            Session["MasterData"] = ViewState["DrugGeneric"];
            Session["SelectedData"] = ViewState["SelectedDrug"];
            ViewState.Remove("DrugGeneric");
            ViewState.Remove("SelectedDrug");
            Session["PtnRegCTC"] = "PtnRegCTC";
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=" + Session["PtnRegCTC"] + "' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        protected void ddregimen_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtInitialRegimen.Text = "";
            ShowpriorExposure();
        }
    }
}