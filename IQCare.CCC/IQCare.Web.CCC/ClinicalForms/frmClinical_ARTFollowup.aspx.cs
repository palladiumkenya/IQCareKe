using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Application.Common;
using Application.Presentation;
using Interface.Administration;
using Interface.Clinical;

/////////////////////////////////////////////////////////////////////
// Code Written By   : Jayanta Kumar Das
// Written Date      : 20th Oct 2006
// Modification Date : 
// Description       : Follow Up ART 
//
/// /////////////////////////////////////////////////////////////////
/// 
namespace IQCare.Web.Clinical
{
    public partial class ARTFollowup : BasePage, ICallbackEventHandler
    {
        #region Variable Declaration Area
        DataSet theDS_ARTFU;
        DataTable theDTOIAIDsleft, theDTOIAIDsright, theDTMissedReason;
        int PatientID, ReasonID, LocationID, ARVSideEffectsNone, ARVSideEffectsNotDocumented, OIsAIDsIllnessNone, OIsAIDsIllnessNotDocumented, VisittypeFU = 2, visitPK = 0;
        DateTime createDate;
        Boolean Save = false, Update = false;
        string script, str, strCallback;
        Hashtable htFollowupParameters;
        //Amitava Sinha
        //int icount;
       // StringBuilder sbParameter;
        StringBuilder sbValues;
        string strmultiselect;
        //String TableName;
        //ArrayList arl;
        //Amitava Sinha  
        #endregion
        #region "User Function"

        private Boolean TransferValidation(int PId)
        {
            IPatientTransfer IPTransferMgr;
            IPTransferMgr = (IPatientTransfer)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientTransfer, BusinessProcess.Clinical");
            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                DataSet DS = IPTransferMgr.GetLatestTransferDate(PId, 0);
                if (DS.Tables[0].Rows[0]["NotExistTransferDate"].ToString() != "0")
                {
                    if (Convert.ToDateTime(txtvisitDate.Value) < Convert.ToDateTime(DS.Tables[0].Rows[0]["TransferDate"]))
                    {
                        IQCareMsgBox.Show("TransferDate_4", this);
                        txtvisitDate.Focus();
                        return false;
                    }
                }
            }
            ////else if (Request.QueryString["name"] == "Edit")
            else if ((Convert.ToInt32(Session["PatientVisitId"])) > 0)
            {
                // visitPK = Convert.ToInt32(Request.QueryString["visitid"]);
                visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                DataSet DS = IPTransferMgr.GetLatestTransferDate(PId, visitPK);
                if (DS.Tables[0].Rows[0]["NotExistTransferDate"].ToString() != "0")
                {

                    if (DS.Tables[1].Rows[0]["PrevDate"].ToString() == "0" && DS.Tables[2].Rows[0]["LaterDate"].ToString() != "0")
                    {
                        if (Convert.ToDateTime(txtvisitDate.Value) > Convert.ToDateTime(DS.Tables[2].Rows[0]["LaterDate"]))
                        {
                            IQCareMsgBox.Show("TransferDate_5", this);
                            txtvisitDate.Focus();
                            return false;
                        }
                    }

                }

            }
            return true;
        }

        private Boolean Validate_Save()
        {
            PatientID = Convert.ToInt32(Session["PatientId"]);
            //Visit Date Validations



            if (txtvisitDate.Value.Trim() == "")
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Visit Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtvisitDate.Focus();
                return false;
            }
            if (TransferValidation(PatientID) == false)
            {
                return false;
            }

            IQCareUtils theUtil = new IQCareUtils();
            if (Convert.ToDateTime(Application["AppCurrentDate"].ToString()) < Convert.ToDateTime(theUtil.MakeDate(txtvisitDate.Value)))
            {
                IQCareMsgBox.Show("CompareDate5", this);
                txtvisitDate.Focus();
                return false;
            }

            //Therapy Plan Validations for Non Paperless
            if (lstclinPlanFU.Value.ToString() == "0" && Session["Paperless"].ToString() == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "ARV therapy";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                return false;
            }

            if (lstclinPlanFU.Value.ToString() == "99" && txtARTEndeddate.Value == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "ART EndDate";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                return false;
            }

            if (lstclinPlanFU.Value.ToString() == "99" && ddlArvTherapyStopCode.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Stop Regimen Reason";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                return false;
            }
            if (lstclinPlanFU.Value.ToString() == "99" && ViewState["ARTProgStatus"].ToString() != "ART")
            {
                if (ViewState["ARTProgStatus"].ToString() != "Stopped ART")
                {
                    if (ViewState["ARTProgStatus"].ToString() != "Due for Termination")
                    {
                        IQCareMsgBox.Show("ARVRegimenNotFound", this);
                        return false;
                    }
                }
            }
            if (lstclinPlanFU.Value.ToString() == "98" && ddlArvTherapyChangeCode.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Stop Regimen Reason";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                //validateMessage += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this) + "\\n";
                ddlArvTherapyChangeCode.Focus();
                return false;
            }
            if (lstclinPlanFU.Value.ToString() == "98" && ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
            {
                if (txtarvTherapyChangeCodeOtherName.Value == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Stop Regimen Reason (Other)Specify";
                    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                    //validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "\\n";
                    ddlArvTherapyChangeCode.Focus();
                    return false;
                }
            }

            if (this.lstappPeriod.SelectedIndex != 0 || this.lstappPeriod.SelectedIndex == 0)
            {
                if (txtappDate.Value != "")
                {
                    if (Convert.ToDateTime(theUtil.MakeDate(txtappDate.Value)) < Convert.ToDateTime(theUtil.MakeDate(txtvisitDate.Value)))
                    {
                        IQCareMsgBox.Show("App_Visit", this);
                        txtappDate.Focus();
                        return false;
                    }
                }
            }

            return true;
        }

        private int ReasonMissed_Count()
        {
            int count = 0;
            foreach (HtmlTableRow tr in tblReasonMissed.Rows)
            {
                foreach (HtmlTableCell tc in tr.Cells)
                {
                    foreach (Control ct in tc.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Checked == true)
                            {
                                count++;
                            }
                        }
                    }
                }
            }
            return count;
        }

        private string DataQuality_Msg()
        {
            string strmsg = "Following values are required to complete the data quality check:\\n\\n";

            //Visit Date
            if (txtvisitDate.Value.Trim() == "")
            {
                string scriptVdate = "<script language = 'javascript' defer ='defer' id = 'Vdate_ID'>\n";
                scriptVdate += "To_Change_Color('lblvdate');\n";
                scriptVdate += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "Vdate_ID", scriptVdate);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -Visit Date";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }

            //Missed LastWeek
            int count = ReasonMissed_Count();
            //Missed LastWeek && Adherence
            if (chMissedLastWeeknone.Checked == false)
            {
                if (txtMissedLastWeek.Value.Trim() == "" || count == 0)
                {
                    string scriptmisslastdate = "<script language = 'javascript' defer ='defer' id = 'MissLast_ID'>\n";
                    scriptmisslastdate += "To_Change_Color('lbldosmissed');\n";
                    scriptmisslastdate += "To_Change_Color('lblreasonmissed');\n";
                    scriptmisslastdate += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "MissLast_ID", scriptmisslastdate);
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = " -Adherence: doses Missed Last Week and Reason Missed";
                    strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                    strmsg = strmsg + "\\n";
                }
            }
            //Height
            if (txtphysHeight.Text.Trim() == "")
            {
                string scriptHT = "<script language = 'javascript' defer ='defer' id = 'HT_ID'>\n";
                scriptHT += "To_Change_Color('lblHT');\n";
                scriptHT += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "HT_ID", scriptHT);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Height";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg = strmsg + "\\n";
            }

            //Weight

            if (txtphysWeight.Text.Trim() == "")
            {
                string scriptWT = "<script language = 'javascript' defer ='defer' id = 'WT_ID'>\n";
                scriptWT += "To_Change_Color('lblWT');\n";
                scriptWT += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "WT_ID", scriptWT);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Weight";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg = strmsg + "\\n";
            }
            //ARV Side Effects
            if ((rdoARVSideEffectsNone.Checked == false) && (rdoARVSideEffectsNotDocumented.Checked == false) && (rdoARVSideEffectsYes.Checked == false))
            {
                string scriptSideEffect = "<script language = 'javascript' defer ='defer' id = 'ARVSideEffect_ID'>\n";
                scriptSideEffect += "To_Change_Color('lblARVNone');\n";
                scriptSideEffect += "To_Change_Color('lblARVNotdocumented');\n";
                scriptSideEffect += "To_Change_Color('lblARVSideEffect');\n";
                scriptSideEffect += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ARVSideEffect_ID", scriptSideEffect);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-ARV Side Effects";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg = strmsg + "\\n";
            }

            //OIs and AIDS defining Illness
            if ((rdoOIsAIDsIllnessNone.Checked == false) && (rdoOIsAIDsIllnessNotDocumented.Checked == false) && (rdoOIsAIDsIllnessYes.Checked == false))
            {
                string scriptOIsAIDs = "<script language = 'javascript' defer ='defer' id = 'OIsAIDs_ID'>\n";
                scriptOIsAIDs += "To_Change_Color('lblOIAIDNone');\n";
                scriptOIsAIDs += "To_Change_Color('lblOIAIDNotdocumented');\n";
                scriptOIsAIDs += "To_Change_Color('lblOIAIDillness');\n";
                scriptOIsAIDs += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "OIsAIDs_ID", scriptOIsAIDs);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-OIs and AIDS Defining";
                strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                strmsg = strmsg + "\\n";

            }
            //WHO Stage
            if (ddlWHOStage.SelectedValue == "0")
            {
                string scriptWHO = "<script language = 'javascript' defer ='defer' id = 'WHO_ID'>\n";
                scriptWHO += "To_Change_Color('lblWHO');\n";
                scriptWHO += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "WHO_ID", scriptWHO);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-WHO Stage";
                strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                strmsg = strmsg + "\\n";
            }

            //WAB Stage
            if (ddlphysWABStage.SelectedValue == "0")
            {
                string scriptWAB = "<script language = 'javascript' defer ='defer' id = 'WAB_ID'>\n";
                scriptWAB += "To_Change_Color('lblWAB');\n";
                scriptWAB += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "WAB_ID", scriptWAB);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-WAB Stage";
                strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                strmsg = strmsg + "\\n";
            }
            //Assessment
            if (cblAssessment.SelectedValue == "")
            {
                string scriptassessment = "<script language = 'javascript' defer ='defer' id = 'Assessment_ID'>\n";
                scriptassessment += "To_Change_Color('lblassessment');\n";
                scriptassessment += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "Assessment_ID", scriptassessment);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Assessment";
                strmsg += IQCareMsgBox.GetMessage("BlankList", theBuilder, this);
                strmsg = strmsg + "\\n";
            }

            //  Therapy Plan Validations

            if (lstclinPlanFU.Value.ToString() == "0" && Session["Paperless"].ToString() == "0")
            {
                string scriptARVplan = "<script language = 'javascript' defer ='defer' id = 'ARVplan_ID'>\n";
                scriptARVplan += "To_Change_Color('lblarvplan');\n";
                scriptARVplan += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ARVplan_ID", scriptARVplan);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Therapy Plan";
                strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                strmsg = strmsg + "\\n";
            }
            return strmsg;
        }
        private Boolean Validate_Data_Quality()
        {
            //PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
            PatientID = Convert.ToInt32(Session["PatientId"]);
            if (TransferValidation(PatientID) == false)
            {
                return false;
            }
            IInitialEval IEManager;
            DataSet dr;
            IEManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
            dr = IEManager.GetCurrentDate();
            DateTime dt = Convert.ToDateTime(dr.Tables[0].Rows[0]["CurrentDay"]);
            dt = Convert.ToDateTime(dt.ToString(Session["AppDateFormat"].ToString()));

            IQCareUtils theUtil = new IQCareUtils();
            if (dt < Convert.ToDateTime(theUtil.MakeDate(txtvisitDate.Value)))
            {
                IQCareMsgBox.Show("CompareDate5", this);
                txtvisitDate.Focus();
                return false;
            }

            if (this.lstappPeriod.SelectedIndex != 0 || this.lstappPeriod.SelectedIndex == 0)
            {
                if (txtappDate.Value != "")
                {
                    if (Convert.ToDateTime(theUtil.MakeDate(txtappDate.Value)) < Convert.ToDateTime(theUtil.MakeDate(txtvisitDate.Value)))
                    {
                        IQCareMsgBox.Show("App_Visit", this);
                        txtappDate.Focus();
                        return false;
                    }
                }
            }
            return true;
        }

        protected void Check_IE()
        {
            int PatientID = Convert.ToInt32(Session["PatientVisitId"]);
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans=true;\n";
            script += "alert('No IE Exist for this Patient');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='frmPatient_Home.aspx?PatientId=" + PatientID + "'=;\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        private void Load_data()
        {
            IFollowup FUManager;
            visitPK = Convert.ToInt32(Session["PatientVisitId"]);
            LocationID = Convert.ToInt32(Session["ServiceLocationId"]);
            FUManager = null;
            FUManager = (IFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowup, BusinessProcess.Clinical");
            DataSet theDS1 = FUManager.GetFollowUpARTupdate(PatientID, visitPK, LocationID);

            IQCareUtils theUtil = new IQCareUtils();
            if (theDS1.Tables[0].Rows.Count >= 1)
            {
                if (theDS1.Tables[0].Rows[0]["DataQuality"] != System.DBNull.Value && Convert.ToInt32(theDS1.Tables[0].Rows[0]["DataQuality"]) == 1)
                {
                    btndataquality.CssClass = "greenbutton";
                }
            }
            if (theDS1.Tables[0].Rows.Count > 0)
            {
                if (theDS1.Tables[0].Rows[0]["VisitDate"] != System.DBNull.Value)
                {
                    DateTime theTmpDt1 = Convert.ToDateTime(theDS1.Tables[0].Rows[0]["VisitDate"]);
                    this.txtvisitDate.Value = theTmpDt1.ToString(Session["AppDateFormat"].ToString());
                    Session["vdate"] = txtvisitDate.Value;
                }
                if (theDS1.Tables[0].Rows[0]["CreateDate"] != System.DBNull.Value)
                {
                    DateTime theCreateDate = Convert.ToDateTime(theDS1.Tables[0].Rows[0]["CreateDate"]);
                    this.createDate = Convert.ToDateTime(theCreateDate.ToString(Session["AppDateFormat"].ToString()));
                    ViewState["createdate"] = Convert.ToDateTime(theDS1.Tables[0].Rows[0]["CreateDate"].ToString());
                }

                //CD4
                //// DataSet theDS = latestCD4VL(Convert.ToInt32(Request.QueryString["patientID"]), Convert.ToDateTime(txtvisitDate.Value));
                DataSet theDS = latestCD4VL(Convert.ToInt32(Session["PatientId"]), Convert.ToDateTime(txtvisitDate.Value));
                if (theDS.Tables[0].Rows[0]["ExistCD4"].ToString() != "0")
                {
                    this.txtTestCD4Results.Value = theDS.Tables[0].Rows[0]["CD4TestResult"].ToString();

                    DateTime theTmpDt1 = Convert.ToDateTime(theDS.Tables[0].Rows[0]["LastCD4Date"]);
                    this.txtTestResultsDate.Value = theTmpDt1.ToString(Session["AppDateFormat"].ToString());
                }
                //Viral Load
                if (theDS.Tables[1].Rows[0]["ExistViralLoad"].ToString() != "0")
                {

                    this.txtmostRecentViralLoad.Value = theDS.Tables[1].Rows[0]["ViralLoadTestResult"].ToString();

                    DateTime theTmpDt2 = Convert.ToDateTime(theDS.Tables[1].Rows[0]["LastViralLoadDate"]);
                    this.txtmostRecentViralLoadDate.Value = theTmpDt2.ToString(Session["AppDateFormat"].ToString());

                }
                //PrevARV CD4
                if ((theDS.Tables[3].Rows.Count > 0) && (theDS.Tables[3].Rows[0]["Existflag"].ToString() == "1"))
                {
                    if (theDS.Tables[3].Rows[0]["PrevARVsCD4IE"] != System.DBNull.Value)
                    {
                        this.txtpriorARVsCD4.Text = theDS.Tables[3].Rows[0]["PrevARVsCD4IE"].ToString();
                    }
                    if (theDS.Tables[3].Rows[0]["PrevARVsCD4DateIE"] != System.DBNull.Value)
                    {
                        DateTime TmptxtpriorARVsCD4Date = Convert.ToDateTime(theDS.Tables[3].Rows[0]["PrevARVsCD4DateIE"]);
                        this.txtpriorARVsCD4Date.Value = TmptxtpriorARVsCD4Date.ToString(Session["AppDateFormat"].ToString());
                    }
                    if (theDS.Tables[3].Rows[0]["VisitIDIE"] != System.DBNull.Value)
                    {
                        this.hdnVisitIDIE.Value = theDS.Tables[3].Rows[0]["VisitIDIE"].ToString();
                    }

                }


                //REGIMEN
                if (theDS.Tables[2].Rows.Count > 0)
                {
                    if (theDS.Tables[2].Rows[0]["ExistRegimen"] != null)
                    {
                        if (theDS.Tables[2].Rows[0]["ExistRegimen"].ToString() != "0")
                        {

                            this.txtRegimenType.Value = theDS.Tables[2].Rows[0]["RegimenType"].ToString();
                            if ((theDS.Tables[2].Rows[0]["CurrentARTStartDate"] != System.DBNull.Value) || (theDS.Tables[2].Rows[0]["CurrentARTStartDate"].ToString() != ""))
                            {
                                DateTime theTmpDt2 = Convert.ToDateTime(theDS.Tables[2].Rows[0]["CurrentARTStartDate"]);
                                this.txtPrescribedARVStartDate.Value = theTmpDt2.ToString(Session["AppDateFormat"].ToString().Substring(3, 8));
                            }
                        }
                    }
                }

            }

            if (theDS1.Tables[1].Rows.Count > 0)
            {
                if (theDS1.Tables[1].Rows[0]["LMP"] != System.DBNull.Value)
                {
                    DateTime theTmpDt3 = Convert.ToDateTime(theDS1.Tables[1].Rows[0]["LMP"]);
                    this.txtLMPdate.Value = theTmpDt3.ToString(Session["AppDateFormat"].ToString());
                }
                if (theDS1.Tables[1].Rows[0]["Pregnant"] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["Pregnant"].ToString()) == 1)
                    {
                        this.rdopregnantYes.Checked = true;
                        if (theDS1.Tables[1].Rows[0]["EDD"] != System.DBNull.Value)
                        {
                            this.txtEDDDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[1].Rows[0]["EDD"]);
                        }
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
                        script += "show('rdopregnantyesno');\n";
                        script += "hide('spdelivery');\n";
                        script += "show('spanEDD');\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "PregnantYes", script);
                        ViewState["Pregstatus"] = "1";
                    }
                    else if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["Pregnant"].ToString()) == 0)
                    {
                        this.rdopregnantNo.Checked = true;
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'PregnantNo'>\n";
                        script += "show('rdopregnantyesno');\n";
                        script += "hide('spdelivery');\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "PregnantNo", script);
                        ViewState["Pregstatus"] = "2";
                    }
                }

                if (theDS1.Tables[1].Rows[0]["Delivered"] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["Delivered"]) == 0)
                    {
                        this.rdoDeliveredNo.Checked = true;
                        if (theDS1.Tables[1].Rows[0]["EDD"] != System.DBNull.Value)
                        {
                            this.txtEDDDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[1].Rows[0]["EDD"]);
                        }
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'DeliveredNo'>\n";
                        script += "hide('rdopregnantyesno');\n";
                        script += "show('spdelivery');\n";
                        script += "show('spanEDD');\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "DeliveredNo", script);
                        ViewState["Pregstatus"] = "3";
                    }

                    else if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["Delivered"]) == 1)
                    {
                        this.rdoDeliveredYes.Checked = true;
                        if (theDS1.Tables[1].Rows[0]["DateofDelivery"] != System.DBNull.Value)
                        {
                            this.txtDeliDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[1].Rows[0]["DateofDelivery"]);
                        }
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'DeliveredYes'>\n";
                        script += "hide('rdopregnantyesno');\n";
                        script += "show('spdelivery');\n";
                        script += "show('spanDelDate');\n";
                        script += "hide('spanEDD');\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "DeliveredYes", script);
                        ViewState["Pregstatus"] = "4";
                    }
                }
            }
            //For Last Week
            if (theDS1.Tables[2].Rows.Count > 0)
            {
                if (theDS1.Tables[2].Rows[0]["MissedLastWeek"] == System.DBNull.Value)
                {
                    this.txtMissedLastWeek.Value = "";
                    script = "";
                    script = "<script language = 'javascript' defer ='defer' id = 'LastWeek'>\n";
                    script += "document.getElementById('" + chMissedLastWeeknone.ClientID + "').click();\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "LastWeek", script);
                }
                else
                {
                    if (theDS1.Tables[2].Rows[0]["MissedLastWeek"].ToString() == "99999")
                    { this.txtMissedLastWeek.Value = ""; }
                    else
                    {
                        this.txtMissedLastWeek.Value = theDS1.Tables[2].Rows[0]["MissedLastWeek"].ToString();
                    }
                }
                //For Last Month

                if (theDS1.Tables[2].Rows[0]["MissedLastMonth"] == System.DBNull.Value)
                {
                    this.txtMissedLastMonth.Value = "";
                    script = "";
                    script = "<script language = 'javascript' defer ='defer' id = 'LastMonth'>\n";
                    script += "document.getElementById('" + chMissedLastMonthnone.ClientID + "').click();\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "LastMonth", script);
                }
                else
                {
                    if (theDS1.Tables[2].Rows[0]["MissedLastMonth"].ToString() == "99999")
                    { this.txtMissedLastMonth.Value = ""; }
                    else { this.txtMissedLastMonth.Value = theDS1.Tables[2].Rows[0]["MissedLastMonth"].ToString(); }
                }

                if (theDS1.Tables[2].Rows[0]["NumDOTPerWeek"] != System.DBNull.Value)
                {
                    this.txtNumDOTPerWeek.Text = theDS1.Tables[2].Rows[0]["NumDOTPerWeek"].ToString();
                }
                if (theDS1.Tables[2].Rows[0]["NumHomeVisitsPerWeek"] != System.DBNull.Value)
                {
                    this.txtNumHomeVisitsPerWeek.Text = theDS1.Tables[2].Rows[0]["NumHomeVisitsPerWeek"].ToString();
                }
                if (theDS1.Tables[2].Rows[0]["SupportGroup"] != System.DBNull.Value)
                {

                    this.ckSupportGroup.Checked = Convert.ToBoolean(theDS1.Tables[2].Rows[0]["SupportGroup"]);
                }
                // this.rdoInterrupted.Value = 
                if (theDS1.Tables[2].Rows[0]["InterruptedDate"] != System.DBNull.Value)
                {
                    DateTime thetxtInterruptedDate = Convert.ToDateTime(theDS1.Tables[2].Rows[0]["InterruptedDate"]);
                    this.txtInterruptedDate.Value = thetxtInterruptedDate.ToString(Session["AppDateFormat"].ToString());
                    script = "";
                    script = "<script language = 'javascript' defer ='defer' id = 'Interrupted1'>\n";
                    script += "show('interruptedDate');\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "Interrupted1", script);
                    rdoInterrupted.Checked = true;
                }
                if (theDS1.Tables[2].Rows[0]["InterruptedNumDays"] != System.DBNull.Value)
                {
                    this.TxtInterruptedNumDays.Value = theDS1.Tables[2].Rows[0]["InterruptedNumDays"].ToString();
                    script = "";
                    script = "<script language = 'javascript' defer ='defer' id = 'Interrupted2'>\n";
                    script += "show('interruptedDate');\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "Interrupted2", script);
                    rdoInterrupted.Checked = true;
                }
                //this.rdostopped.Value 
                if (theDS1.Tables[2].Rows[0]["StoppedDate"] != System.DBNull.Value)
                {
                    DateTime txtstoppedDate = Convert.ToDateTime(theDS1.Tables[2].Rows[0]["StoppedDate"]);
                    this.txtstoppedDate.Value = txtstoppedDate.ToString(Session["AppDateFormat"].ToString());
                    script = "";
                    script = "<script language = 'javascript' defer ='defer' id = 'Stopped1'>\n";
                    script += "show('stopDate');\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "Stopped1", script);
                    rdostopped.Checked = true;
                }
                if (theDS1.Tables[2].Rows[0]["StoppedNumDays"] != System.DBNull.Value)
                {
                    this.txtstoppedNumDays.Value = theDS1.Tables[2].Rows[0]["StoppedNumDays"].ToString();
                    script = "";
                    script = "<script language = 'javascript' defer ='defer' id = 'Stopped2'>\n";
                    script += "show('stopDate');\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "Stopped2", script);
                    rdostopped.Checked = true;
                }

                if (theDS1.Tables[2].Rows[0]["HerbalMeds"] != System.DBNull.Value)
                {
                    this.ckHerbalMeds.Checked = Convert.ToBoolean(theDS1.Tables[2].Rows[0]["HerbalMeds"]);
                }
            }
            //Physical Exam and Detail Patient Vitals Part[Table: dtl_PatientVitals]

            if (theDS1.Tables[3].Rows.Count > 0)
            {
                if (theDS1.Tables[3].Rows[0]["Temp"] != System.DBNull.Value)
                {
                    this.txtphysTemp.Text = theDS1.Tables[3].Rows[0]["Temp"].ToString();
                }
                if (theDS1.Tables[3].Rows[0]["RR"] != System.DBNull.Value)
                {
                    this.txtphysRR.Text = theDS1.Tables[3].Rows[0]["RR"].ToString();
                }
                if (theDS1.Tables[3].Rows[0]["HR"] != System.DBNull.Value)
                {
                    this.txtphysHR.Text = theDS1.Tables[3].Rows[0]["HR"].ToString();
                }
                if (theDS1.Tables[3].Rows[0]["BPDiastolic"] != System.DBNull.Value)
                {
                    this.txtphysBPDiastolic.Text = theDS1.Tables[3].Rows[0]["BPDiastolic"].ToString();
                }
                if (theDS1.Tables[3].Rows[0]["BPSystolic"] != System.DBNull.Value)
                {
                    this.txtphysBPSystolic.Text = theDS1.Tables[3].Rows[0]["BPSystolic"].ToString();
                }
                if (theDS1.Tables[3].Rows[0]["Height"] != System.DBNull.Value)
                {
                    this.txtphysHeight.Text = theDS1.Tables[3].Rows[0]["Height"].ToString();
                }
                if (theDS1.Tables[3].Rows[0]["Weight"] != System.DBNull.Value)
                {
                    this.txtphysWeight.Text = theDS1.Tables[3].Rows[0]["Weight"].ToString();
                }
                if (((theDS1.Tables[3].Rows[0]["Weight"].ToString() != "") && (theDS1.Tables[3].Rows[0]["Weight"] != System.DBNull.Value)) || ((theDS1.Tables[3].Rows[0]["Height"].ToString() != "") && (theDS1.Tables[3].Rows[0]["Height"] != System.DBNull.Value)))
                {
                    decimal anotherWeight = Convert.ToDecimal(theDS1.Tables[3].Rows[0]["Weight"].ToString());
                    decimal anotherHeight = Convert.ToDecimal(theDS1.Tables[3].Rows[0]["Height"].ToString());
                    decimal anotherBMI = anotherWeight / ((anotherHeight / 100) * (anotherHeight / 100));
                    anotherBMI = Math.Round(anotherBMI, 2);
                    txtanotherbmi.Value = Convert.ToString(anotherBMI);
                }

                if (theDS1.Tables[3].Rows[0]["Pain"] != System.DBNull.Value)
                {
                    this.ddlPain.Value = theDS1.Tables[3].Rows[0]["Pain"].ToString();
                }
            }
            //Table: dtl_PatientStage
            if (theDS1.Tables[4].Rows.Count > 0)
            {
                if (theDS1.Tables[4].Rows[0]["WABStage"] != System.DBNull.Value)
                {
                    this.ddlphysWABStage.SelectedValue = theDS1.Tables[4].Rows[0]["WABStage"].ToString();
                }
                if (theDS1.Tables[4].Rows[0]["WHOStage"] != System.DBNull.Value)
                {
                    this.ddlWHOStage.SelectedValue = theDS1.Tables[4].Rows[0]["WHOStage"].ToString();
                }
            }

            //ARV Therapy - Table:dtl_PatientARVTherapy
            if (theDS1.Tables[5].Rows.Count > 0)
            {
                if (theDS1.Tables[5].Rows[0]["THerapyPlan"] != System.DBNull.Value)
                {
                    this.lstclinPlanFU.Value = theDS1.Tables[5].Rows[0]["THerapyPlan"].ToString();
                }
                if (theDS1.Tables[5].Rows[0]["THerapyReasonCOde"] != System.DBNull.Value)
                {
                    if (this.lstclinPlanFU.Value == "98")
                    {
                        this.ddlArvTherapyChangeCode.SelectedValue = theDS1.Tables[5].Rows[0]["THerapyReasonCOde"].ToString();
                        this.txtarvTherapyChangeCodeOtherName.Value = theDS1.Tables[5].Rows[0]["TherapyOther"].ToString();
                        if (this.ddlArvTherapyChangeCode.SelectedValue == "24")
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TherapyCode10'>\n";
                            script += "show('arvTherapyChange');\n";
                            script += "show('otherarvTherapyChangeCode');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "TherapyCode10", script);
                        }
                        else
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TherapyCode11'>\n";
                            script += "show('arvTherapyChange');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "TherapyCode11", script);
                        }
                    }
                    if (this.lstclinPlanFU.Value == "99")
                    {
                        this.ddlArvTherapyStopCode.SelectedValue = theDS1.Tables[5].Rows[0]["THerapyReasonCOde"].ToString();
                        this.txtarvTherapyStopCodeOtherName.Value = theDS1.Tables[5].Rows[0]["TherapyOther"].ToString();
                        DateTime theTmpDtTherapy = Convert.ToDateTime(theDS1.Tables[13].Rows[0]["ARTEndDate"]);
                        this.txtARTEndeddate.Value = theTmpDtTherapy.ToString(Session["AppDateFormat"].ToString());

                        if (this.ddlArvTherapyStopCode.SelectedValue == "24")
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TherapyCode20'>\n";
                            script += "show('arvTherapyStop');\n";
                            script += "show('otherarvTherapyStopCode');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "TherapyCode20", script);
                        }
                        else
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'TherapyCode21'>\n";
                            script += "show('arvTherapyStop');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "TherapyCode21", script);
                        }
                    }

                }
            }

            //Appointments - Table:dtl_PatientAppointment
            if (theDS1.Tables[6].Rows.Count > 0)
            {
                if (theDS1.Tables[6].Rows[0]["Appdate"] != System.DBNull.Value)
                {
                    DateTime theTmpDt8 = Convert.ToDateTime(theDS1.Tables[6].Rows[0]["Appdate"]);
                    this.txtappDate.Value = theTmpDt8.ToString(Session["AppDateFormat"].ToString());
                    lstappPeriod.Value = theDS1.Tables[11].Rows[0]["No_of_Days"].ToString();
                }
                if (theDS1.Tables[6].Rows[0]["AppReason"] != System.DBNull.Value)
                {
                    this.ddlAppReason.SelectedValue = theDS1.Tables[6].Rows[0]["AppReason"].ToString();
                }
                if (theDS1.Tables[6].Rows[0]["EmployeeID"] != System.DBNull.Value)
                {
                    BindDropdown(theDS1.Tables[6].Rows[0]["EmployeeID"].ToString());
                    this.ddlCounsellorSignature.SelectedValue = theDS1.Tables[6].Rows[0]["EmployeeID"].ToString();
                }
            }
            else
            {
                BindDropdown("");
            }
            //createdate = Convert.ToDateTime(theDS1.Tables[5].Rows[0]["CreateDate"].ToString());

            //Missed Reason
            if (theDS1.Tables[10].Rows.Count > 0)
            {
                string OtherMissedReason_Desc = "";
                string OtherMissedReason = "";
                for (int i = 0; i < theDS1.Tables[10].Rows.Count; i++)
                {
                    foreach (HtmlTableRow r in tblReasonMissed.Rows)
                    {
                        foreach (HtmlTableCell c in r.Cells)
                        {
                            foreach (Control ct in c.Controls)
                            {
                                if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                {
                                    foreach (DataRow drright in theDTMissedReason.Rows)
                                    {
                                        if (((HtmlInputCheckBox)ct).Value == drright[1].ToString())
                                        {
                                            if (drright[0].ToString() == theDS1.Tables[10].Rows[i]["MissedReasonID"].ToString())
                                            {
                                                if (drright[1].ToString() == "Other")
                                                {
                                                    OtherMissedReason_Desc = theDS1.Tables[10].Rows[i]["Other_Desc"].ToString();
                                                    OtherMissedReason = drright[1].ToString();
                                                    ((HtmlInputCheckBox)ct).Checked = true;
                                                }
                                                else
                                                {
                                                    ((HtmlInputCheckBox)ct).Checked = true;

                                                }
                                            }
                                        }
                                    }

                                }
                                else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                                {
                                    if (OtherMissedReason == "Other")
                                    {
                                        ((HtmlInputText)ct).Value = OtherMissedReason_Desc;
                                        string script = "";
                                        script = "<script language = 'javascript' defer ='defer' id = 'ReasonMissed_0'>\n";
                                        script += "show('otherRMissed');\n";
                                        script += "</script>\n";
                                        ClientScript.RegisterStartupScript(this.GetType(), "ReasonMissed_0", script);
                                    }
                                }

                            }
                        }
                    }
                }
            }

            /**********/


            //Presenting Complaints
            if (theDS1.Tables[7].Rows.Count > 0)
            {
                for (int i = 0; i < theDS1.Tables[7].Rows.Count; i++)
                {

                    for (int j = 0; j < cblPresentingComplaints.Items.Count; j++)
                    {
                        if (cblPresentingComplaints.Items[j].Value == theDS1.Tables[7].Rows[i]["SymptomID"].ToString())
                        {
                            cblPresentingComplaints.Items[j].Selected = true;
                            chkpresentingComplaintsNone.Checked = false;
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'presentingComplaints'>\n";
                            script += "document.getElementById('" + chkpresentingComplaintsNonehidden.ClientID + "').click();\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "presentingComplaints", script);
                        }
                    }

                }

                //cblARVSideEffectleft
                if (theDS1.Tables[7].Rows.Count == 0)
                {
                    rdoARVSideEffectsNone.Checked = true;
                }

                for (int i = 0; i < theDS1.Tables[7].Rows.Count; i++)
                {
                    int ARVSideEffect = Convert.ToInt32(theDS1.Tables[7].Rows[i]["SymptomID"].ToString());
                    if (ARVSideEffect == 31)
                    {
                        rdoARVSideEffectsNone.Checked = true;

                    }
                    else if (ARVSideEffect == 32)
                    {
                        rdoARVSideEffectsNotDocumented.Checked = true;
                    }

                    for (int j = 0; j < cblARVSideEffectleft.Items.Count; j++)
                    {
                        if (cblARVSideEffectleft.Items[j].Value == theDS1.Tables[7].Rows[i]["SymptomID"].ToString())
                        {
                            rdoARVSideEffectsYes.Checked = true;
                            cblARVSideEffectleft.Items[j].Selected = true;
                            string script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'cblARVSideEffect_ID'>\n";
                            script += "show('sideEffectsSelected');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "cblARVSideEffect_ID", script);
                        }
                    }
                }
                //cblARVSideEffectright
                for (int i = 0; i < theDS1.Tables[7].Rows.Count; i++)
                {
                    int ARVSideEffect = Convert.ToInt32(theDS1.Tables[7].Rows[i]["SymptomID"].ToString());
                    if (ARVSideEffect == 31)
                    {
                        rdoARVSideEffectsNone.Checked = true;

                    }
                    else if (ARVSideEffect == 32)
                    {
                        rdoARVSideEffectsNotDocumented.Checked = true;
                    }
                    for (int j = 0; j < cblARVSideEffectright.Items.Count; j++)
                    {
                        if (cblARVSideEffectright.Items[j].Value == theDS1.Tables[7].Rows[i]["SymptomID"].ToString())
                        {

                            rdoARVSideEffectsYes.Checked = true;
                            cblARVSideEffectright.Items[j].Selected = true;
                            string script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'cblARVSideEffect_ID'>\n";
                            script += "show('sideEffectsSelected');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "cblARVSideEffect_ID", script);
                        }
                    }
                }
            }


            //TBScreen
            for (int i = 0; i < theDS1.Tables[7].Rows.Count; i++)
            {
                int TBScreen = Convert.ToInt32(theDS1.Tables[7].Rows[i]["SymptomID"].ToString());
                if (TBScreen == 71)
                {
                    rdoTBScreenNotDocumented.Checked = true;

                }

                else if (TBScreen == 70)
                {
                    rdoTBScreenPerformed.Checked = true;
                    string script = "";
                    script = "<script language = 'javascript' defer ='defer' id = 'cblTBScreen_ID'>\n";
                    script += "show('TBSCreenSelected');\n";
                    script += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "cblTBScreen_ID", script);
                }

                for (int j = 0; j < cblTBScreen.Items.Count; j++)
                {
                    if (cblTBScreen.Items[j].Value == theDS1.Tables[7].Rows[i]["SymptomID"].ToString())
                    {
                        rdoTBScreenPerformed.Checked = true;
                        cblTBScreen.Items[j].Selected = true;
                        string script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'cblTBScreen_ID'>\n";
                        script += "show('TBSCreenSelected');\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "cblTBScreen_ID", script);
                    }
                }
            }




            //cblOIsAIDsleft
            if (theDS1.Tables[8].Rows.Count == 0)
            {
                rdoOIsAIDsIllnessNone.Checked = true;
            }

            if (theDS1.Tables[8].Rows.Count > 0)
            {
                for (int i = 0; i < theDS1.Tables[8].Rows.Count; i++)
                {
                    int OIsAIDsIllness = Convert.ToInt32(theDS1.Tables[8].Rows[i]["Disease_Pk"].ToString());
                    if (OIsAIDsIllness == 99)
                    {
                        rdoOIsAIDsIllnessNone.Checked = true;
                        string script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID_None'>\n";
                        script += "hide('assocSelected');\n";
                        script += "hide('pultb');\n";
                        script += "hide('otherOIsAIDs');\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "cblOIsAIDs_ID_None", script);
                    }
                    else if (OIsAIDsIllness == 98)
                    {
                        rdoOIsAIDsIllnessNotDocumented.Checked = true;
                        string script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID_Notdocumented'>\n";
                        script += "hide('assocSelected');\n";
                        script += "hide('pultb');\n";
                        script += "hide('otherOIsAIDs');\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "cblOIsAIDs_ID_Notdocumented", script);
                    }

                    foreach (HtmlTableRow r in tblOIsAIDsleft.Rows)
                    {
                        foreach (HtmlTableCell c in r.Cells)
                        {
                            foreach (Control ct in c.Controls)
                            {
                                if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                {
                                    foreach (DataRow dr in theDTOIAIDsleft.Rows)
                                    {
                                        if (((HtmlInputCheckBox)ct).Value == dr[1].ToString())
                                        {
                                            if (dr[0].ToString() == theDS1.Tables[8].Rows[i]["Disease_Pk"].ToString())
                                            {
                                                if (dr[1].ToString() == "Pulmonary TB")
                                                {
                                                    rdoOIsAIDsIllnessYes.Checked = true;
                                                    ((HtmlInputCheckBox)ct).Checked = true;
                                                    string script = "";
                                                    script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID'>\n";
                                                    script += "show('assocSelected');\n";
                                                    script += "show('pultb');\n";
                                                    script += "</script>\n";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "cblOIsAIDs_ID", script);
                                                }
                                                else
                                                {
                                                    rdoOIsAIDsIllnessYes.Checked = true;
                                                    ((HtmlInputCheckBox)ct).Checked = true;
                                                    string script = "";
                                                    script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID'>\n";
                                                    script += "show('assocSelected');\n";
                                                    script += "</script>\n";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "cblOIsAIDs_ID", script);
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                                {
                                    foreach (DataRow dr in theDTOIAIDsleft.Rows)
                                    {
                                        if (((HtmlInputRadioButton)ct).Value == dr[1].ToString())
                                        {
                                            if (dr[0].ToString() == theDS1.Tables[8].Rows[i]["Disease_Pk"].ToString())
                                            {
                                                rdoOIsAIDsIllnessYes.Checked = true;
                                                ((HtmlInputRadioButton)ct).Checked = true;
                                                string script = "";
                                                script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID1'>\n";
                                                script += "show('assocSelected');\n";
                                                script += "show('pultb');\n";
                                                script += "</script>\n";
                                                ClientScript.RegisterStartupScript(this.GetType(), "cblOIsAIDs_ID1", script);
                                            }

                                        }
                                    }

                                }
                            }
                        }
                    }
                }

                string OI_AIDsOther = "";
                for (int i = 0; i < theDS1.Tables[8].Rows.Count; i++)
                {
                    foreach (HtmlTableRow r in tblOIsAIDsright.Rows)
                    {
                        foreach (HtmlTableCell c in r.Cells)
                        {
                            foreach (Control ct in c.Controls)
                            {
                                if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                {
                                    foreach (DataRow drright in theDTOIAIDsright.Rows)
                                    {
                                        if (((HtmlInputCheckBox)ct).Value == drright[1].ToString())
                                        {
                                            if (drright[0].ToString() == theDS1.Tables[8].Rows[i]["Disease_Pk"].ToString())
                                            {
                                                if (drright[1].ToString() == "Other")
                                                {
                                                    OI_AIDsOther = theDS1.Tables[8].Rows[i]["DiseaseDesc"].ToString();
                                                    rdoOIsAIDsIllnessYes.Checked = true;
                                                    ((HtmlInputCheckBox)ct).Checked = true;
                                                    string script = "";
                                                    script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID2'>\n";
                                                    script += "show('assocSelected');\n";
                                                    script += "show('otherOIsAIDs');\n";
                                                    script += "</script>\n";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "cblOIsAIDs_ID2", script);
                                                }
                                                else
                                                {
                                                    rdoOIsAIDsIllnessYes.Checked = true;
                                                    ((HtmlInputCheckBox)ct).Checked = true;
                                                    string script = "";
                                                    script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID3'>\n";
                                                    script += "show('assocSelected');\n";
                                                    script += "</script>\n";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "cblOIsAIDs_ID3", script);

                                                }
                                            }
                                        }
                                    }

                                }
                                else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                                {
                                    if (OI_AIDsOther != "")
                                    {
                                        ((HtmlInputText)ct).Value = OI_AIDsOther;
                                        string script = "";
                                        script = "<script language = 'javascript' defer ='defer' id = 'cblOIsAIDs_ID4'>\n";
                                        script += "show('assocSelected');\n";
                                        script += "show('otherOIsAIDs');\n";
                                        script += "</script>\n";
                                        ClientScript.RegisterStartupScript(this.GetType(), "cblOIsAIDs_ID4", script);
                                    }
                                }

                            }
                        }
                    }
                }
            }

            //Assessment details  
            if (theDS1.Tables[9].Rows.Count > 0)
            {
                for (int i = 0; i < theDS1.Tables[9].Rows.Count; i++)
                {
                    if (theDS1.Tables[9].Rows[i]["Description1"].ToString() != "")
                    {
                        MulttxtclinPlanNotes.Value = theDS1.Tables[9].Rows[i]["Description1"].ToString();
                    }
                    for (int j = 0; j < cblAssessment.Items.Count; j++)
                    {
                        if (cblAssessment.Items[j].Value == theDS1.Tables[9].Rows[i]["AssessmentID"].ToString())
                        {
                            cblAssessment.Items[j].Selected = true;
                        }
                    }
                }
            }

            //Ajay-06-Jan-09-  Fill Clinical Notes - Begin
            if (theDS1.Tables[12].Rows.Count > 0)
            {
                if (theDS1.Tables[12].Rows[0]["ClinicalNotes"] != System.DBNull.Value)
                {
                    this.txtClinicalNotes.Text = theDS1.Tables[12].Rows[0]["ClinicalNotes"].ToString();
                }
            }
            //Ajay-06-Jan-09-  Fill Clinical Notes - End

            FillOldData(PatientID);

        }

        private void DeleteForm()
        {
            IFollowup ARTManager;
            int theResultRow, OrderNo;
            string FormName;
            OrderNo = Convert.ToInt32(Session["PatientVisitId"].ToString());
            FormName = "ART Follow-Up";
            ARTManager = (IFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowup, BusinessProcess.Clinical");
            theResultRow = (int)ARTManager.DeleteARTForms(FormName, OrderNo, Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["AppUserId"].ToString()));
            if (theResultRow == 0)
            {
                IQCareMsgBox.Show("RemoveFormError", this);
                return;
            }
            else
            {
                string theUrl;
                theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Convert.ToInt32(Session["PatientVisitId"]));
                Response.Redirect(theUrl);
            }

        }

        private void BindDropdown(String EmployeeId)
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
                        if (EmployeeId == "")
                        {
                            theDV.RowFilter = "EmployeeId IN(" + Session["AppUserEmployeeId"].ToString() + ")";
                        }
                        else
                        {
                            theDV.RowFilter = "EmployeeId IN(" + Session["AppUserEmployeeId"].ToString() + "," + EmployeeId + ")";
                        }
                        if (theDV.Count > 0)
                            theDT = theUtils.CreateTableFromDataView(theDV);
                    }
                    BindManager.BindCombo(ddlCounsellorSignature, theDT, "EmployeeName", "EmployeeId");
                }
            }
        }

        //Function to Bind DropDowns
        private void FillDropDowns()
        {
            this.PatientID = Convert.ToInt32(Session["PatientId"]);
            IFollowup FUManager;
            FUManager = (IFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowup, BusinessProcess.Clinical");
            DataSet theDSDropDown = FUManager.GetAllDropDownsART(this.PatientID);
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataSet DStheXML = new DataSet();
            DStheXML.ReadXml(MapPath("..\\XMLFiles\\AllMasters.con"));
            DataView theDV = new DataView();
            DataTable theDT = new DataTable();
            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {

                /*******/
                theDV = new DataView(DStheXML.Tables["Mst_Reason"]);
                theDV.RowFilter = "DeleteFlag=0 and CategoryID = 1";
                if (theDV.Table != null)
                {
                    theDTMissedReason = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    int RMissed = 0;
                    for (int i = 0; i < theDTMissedReason.Rows.Count; i++)
                    {
                        HtmlTableRow tr = new HtmlTableRow();
                        HtmlTableCell tc = new HtmlTableCell();
                        HtmlInputCheckBox chkReasonMissed = new HtmlInputCheckBox();
                        chkReasonMissed.ID = Convert.ToString("RMissed" + i);
                        chkReasonMissed.Value = theDTMissedReason.Rows[i][1].ToString();
                        tc.Controls.Add(chkReasonMissed);
                        tc.Controls.Add(new LiteralControl(chkReasonMissed.Value));
                        tr.Cells.Add(tc);
                        if (chkReasonMissed.Value == "Other")
                        {
                            HtmlTableCell tc1 = new HtmlTableCell();
                            //tc1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' >"));
                            tc1.Controls.Add(new LiteralControl("<SPAN id='otherRMissed' style='DISPLAY:none'>Specify: "));
                            HtmlInputText HTextRMissed = new HtmlInputText();
                            HTextRMissed.ID = "txtotherRMissed";
                            HTextRMissed.Size = 5;
                            tc1.Controls.Add(HTextRMissed);
                            tc1.Controls.Add(new LiteralControl(HTextRMissed.Value));
                            tc1.Controls.Add(new LiteralControl("</SPAN>"));
                            tr.Cells.Add(tc1);
                            chkReasonMissed.Attributes.Add("onclick", "toggle('otherRMissed');");
                        }
                        RMissed++;
                        tblReasonMissed.Rows.Add(tr);

                    }
                    theDV.Dispose();
                    // theDT.Clear();
                }
                /*******/
                //TB Screening
                theDV = new DataView(DStheXML.Tables["Mst_Symptom"]);
                theDV.RowFilter = "DeleteFlag=0 and CategoryID=14 and ID not IN('70','71')";
                theDV.Sort = "SRNO";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCheckedList(cblTBScreen, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                /*******/

                theDV = new DataView(DStheXML.Tables["Mst_Symptom"]);
                theDV.RowFilter = "DeleteFlag='0' and CategoryID='4'";
                theDV.Sort = "SRNO";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCheckedList(cblPresentingComplaints, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }

                /*******/
                //ARV Side Effect left
                theDV = new DataView(theDSDropDown.Tables[0]);
                theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCheckedList(cblARVSideEffectleft, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                /*******/
                //ARV Side Effect right
                theDV = new DataView(theDSDropDown.Tables[1]);
                theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCheckedList(cblARVSideEffectright, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                /*******/
                //HIV Associated Condition left
                theDV = new DataView(theDSDropDown.Tables[2]);
                theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    theDTOIAIDsleft = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    int rcol_left = 0;
                    for (int i = 0; i < theDTOIAIDsleft.Rows.Count; i++)
                    {
                        HtmlTableRow r = new HtmlTableRow();
                        if ((i != 1) && (i != 2))
                        {
                            HtmlTableCell c = new HtmlTableCell();
                            HtmlInputCheckBox chkOIsDsleft = new HtmlInputCheckBox();
                            chkOIsDsleft.ID = Convert.ToString(rcol_left);
                            rdoOIsAIDsIllnessNotDocumented.Attributes.Add("Onclick", "disableCheckbox('" + chkOIsDsleft.ClientID + "')");
                            chkOIsDsleft.Value = theDTOIAIDsleft.Rows[i][1].ToString();
                            c.Controls.Add(chkOIsDsleft);
                            c.Controls.Add(new LiteralControl(chkOIsDsleft.Value));
                            r.Cells.Add(c);
                            if (chkOIsDsleft.Value == "Pulmonary TB")
                            {
                                chkOIsDsleft.Attributes.Add("onclick", "toggle('pultb');");
                            }
                            rcol_left++;
                        }
                        if (i == 0)
                        {
                            HtmlTableCell d = new HtmlTableCell();
                            d.Controls.Add(new LiteralControl("<div id='pultb' style='display:none'>"));
                            HtmlInputRadioButton rdopultbsmplus = new HtmlInputRadioButton();
                            rdopultbsmplus.ID = Convert.ToString(11);
                            rdopultbsmplus.Value = theDTOIAIDsleft.Rows[1][1].ToString();
                            rdopultbsmplus.Attributes.Add("onfocus", "up(this);");
                            rdopultbsmplus.Attributes.Add("onclick", "down(this);");
                            d.Controls.Add(rdopultbsmplus);
                            d.Controls.Add(new LiteralControl(rdopultbsmplus.Value));

                            d.Controls.Add(new LiteralControl("</br>"));

                            HtmlInputRadioButton rdopultbsminus = new HtmlInputRadioButton();
                            rdopultbsminus.ID = Convert.ToString(12);
                            rdopultbsminus.Value = theDTOIAIDsleft.Rows[2][1].ToString();
                            rdopultbsminus.Attributes.Add("onfocus", "up(this);");
                            rdopultbsminus.Attributes.Add("onclick", "down(this);");
                            d.Controls.Add(rdopultbsminus);
                            d.Controls.Add(new LiteralControl(rdopultbsminus.Value));
                            d.Controls.Add(new LiteralControl("</div>"));
                            r.Cells.Add(d);
                        }
                        tblOIsAIDsleft.Rows.Add(r);
                    }
                    theDV.Dispose();
                    //theDTOIAIDsleft.Clear();
                }
                /*******/
                //HIV Associated Condition Right
                theDV = new DataView(theDSDropDown.Tables[3]);
                theDV.RowFilter = "DeleteFlag=0";
                if (theDV.Table != null)
                {
                    theDTOIAIDsright = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    int rcol_right = 0;
                    for (int i = 0; i < theDTOIAIDsright.Rows.Count; i++)
                    {
                        HtmlTableRow trright = new HtmlTableRow();
                        HtmlTableCell tc2_0 = new HtmlTableCell();
                        HtmlInputCheckBox chkOIsDsright = new HtmlInputCheckBox();
                        chkOIsDsright.ID = Convert.ToString("right" + rcol_right);
                        chkOIsDsright.Value = theDTOIAIDsright.Rows[i][1].ToString();
                        tc2_0.Controls.Add(chkOIsDsright);
                        tc2_0.Controls.Add(new LiteralControl(chkOIsDsright.Value));
                        trright.Cells.Add(tc2_0);

                        if (chkOIsDsright.Value == "Other")
                        {
                            HtmlTableCell tc2_1 = new HtmlTableCell();
                            tc2_1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' for='otherOIsAIDs'>"));
                            tc2_1.Controls.Add(new LiteralControl("<SPAN id='otherOIsAIDs' style='DISPLAY:none'>Specify: "));
                            HtmlInputText HTextOIsAIDsillness = new HtmlInputText();
                            HTextOIsAIDsillness.ID = "txtotherOIsAIDsillness";
                            HTextOIsAIDsillness.Size = 10;
                            tc2_1.Controls.Add(HTextOIsAIDsillness);
                            tc2_1.Controls.Add(new LiteralControl(HTextOIsAIDsillness.Value));
                            tc2_1.Controls.Add(new LiteralControl("</SPAN>"));
                            trright.Cells.Add(tc2_1);
                            chkOIsDsright.Attributes.Add("onclick", "toggle('otherOIsAIDs');");
                        }
                        rcol_right++;
                        tblOIsAIDsright.Rows.Add(trright);
                    }
                    theDV.Dispose();
                    // theDT.Clear();
                }
                /*********/
                theDV = new DataView(DStheXML.Tables["Mst_Assessment"]);
                theDV.RowFilter = "AssessmentCategoryID = 2";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCheckedList(cblAssessment, theDT, "AssessmentName", "AssessmentID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Therapy Change Codes
                theDV = new DataView(DStheXML.Tables["mst_Reason"]);
                theDV.RowFilter = "DeleteFlag='0' and CategoryID='2'";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlArvTherapyChangeCode, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Therapy Stop Codes
                theDV = new DataView(DStheXML.Tables["mst_Reason"]);
                theDV.RowFilter = "DeleteFlag='0' and CategoryID='2'";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlArvTherapyStopCode, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Employee Code

                if (DStheXML.Tables["Mst_Employee"] != null)
                {
                    theDV = new DataView(DStheXML.Tables["Mst_Employee"]);

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
                        BindManager.BindCombo(ddlCounsellorSignature, theDT, "EmployeeName", "EmployeeId");
                        theDV.Dispose();
                        theDT.Clear();
                    }
                }

                theDV = new DataView(DStheXML.Tables["mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=26";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlAppReason, theDT, "Name", "ID");
                }
                int WHOStage = 0;
                if (theDSDropDown.Tables[7].Rows.Count > 0)
                    WHOStage = Convert.ToInt32(theDSDropDown.Tables[7].Rows[0]["WHOStage"]);

                int WhoStageSRNO = 0;
                theDV = new DataView(DStheXML.Tables["mst_Decode"]);
                theDV.RowFilter = "Id=" + WHOStage.ToString();
                if (theDV.Count > 0)
                    WhoStageSRNO = Convert.ToInt32(theDV[0]["SRNO"]);
                theDV = null;

                theDV = new DataView(DStheXML.Tables["mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=22 and SystemId = 1 and SRNO>=" + WhoStageSRNO.ToString();
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlWHOStage, theDT, "Name", "ID");
                }

            }

            //else if (Request.QueryString["name"] == "Edit" || Request.QueryString["name"] == "Delete")
            else if ((Convert.ToInt32(Session["PatientVisitId"])) > 1 || Request.QueryString["name"] == "Delete")
            {
                //Adherance Reason Missed
                theDV = new DataView(DStheXML.Tables["Mst_Reason"]);
                theDV.RowFilter = "CategoryID = 1";
                if (theDV.Table != null)
                {
                    theDTMissedReason = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    int RMissed = 0;
                    for (int i = 0; i < theDTMissedReason.Rows.Count; i++)
                    {
                        HtmlTableRow tr = new HtmlTableRow();
                        HtmlTableCell tc = new HtmlTableCell();
                        HtmlInputCheckBox chkReasonMissed = new HtmlInputCheckBox();
                        chkReasonMissed.ID = Convert.ToString("RMissed" + i);
                        chkReasonMissed.Value = theDTMissedReason.Rows[i][1].ToString();
                        tc.Controls.Add(chkReasonMissed);
                        tc.Controls.Add(new LiteralControl(chkReasonMissed.Value));
                        tr.Cells.Add(tc);

                        if (chkReasonMissed.Value == "Other")
                        {
                            HtmlTableCell tc1 = new HtmlTableCell();
                            tc1.Controls.Add(new LiteralControl("<SPAN id='otherRMissed' style='DISPLAY:none'>Specify: "));
                            HtmlInputText HTextRMissed = new HtmlInputText();
                            HTextRMissed.ID = "txtotherRMissed";
                            HTextRMissed.Size = 5;
                            tc1.Controls.Add(HTextRMissed);
                            tc1.Controls.Add(new LiteralControl(HTextRMissed.Value));
                            tc1.Controls.Add(new LiteralControl("</SPAN>"));
                            tr.Cells.Add(tc1);
                            chkReasonMissed.Attributes.Add("onclick", "toggle('otherRMissed');");
                        }
                        RMissed++;
                        tblReasonMissed.Rows.Add(tr);
                    }
                }
                //Complaints
                theDV = new DataView(DStheXML.Tables["Mst_Symptom"]);
                theDV.RowFilter = "CategoryID=4";
                if (theDV.Table != null)
                {
                    theDT = theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCheckedList(cblPresentingComplaints, theDT, "Name", "ID");
                }
                //ARV Side Effects
                if (theDSDropDown.Tables[0] != null)
                {
                    BindManager.BindCheckedList(cblARVSideEffectleft, theDSDropDown.Tables[0], "Name", "ID");
                }
                if (theDSDropDown.Tables[0] != null)
                {
                    BindManager.BindCheckedList(cblARVSideEffectright, theDSDropDown.Tables[1], "Name", "ID");
                }

                if (theDSDropDown.Tables[8] != null)
                {
                    BindManager.BindCheckedList(cblTBScreen, theDSDropDown.Tables[8], "Name", "ID");
                }

                //Coding for Saving HIV Associated Conditions - left
                theDTOIAIDsleft = theDSDropDown.Tables[2];
                if (theDTOIAIDsleft != null)
                {
                    int rcol_left = 0;
                    for (int j = 0; j < theDTOIAIDsleft.Rows.Count; j++)
                    {

                        HtmlTableRow r = new HtmlTableRow();
                        if ((j != 1) && (j != 2))
                        {
                            HtmlTableCell c = new HtmlTableCell();
                            HtmlInputCheckBox chkOIsDsleft = new HtmlInputCheckBox();
                            chkOIsDsleft.ID = Convert.ToString(rcol_left);
                            chkOIsDsleft.Value = theDSDropDown.Tables[2].Rows[j][1].ToString();
                            c.Controls.Add(chkOIsDsleft);
                            c.Controls.Add(new LiteralControl(chkOIsDsleft.Value));
                            r.Cells.Add(c);
                            if (chkOIsDsleft.Value == "Pulmonary TB")
                            {
                                chkOIsDsleft.Attributes.Add("onclick", "toggle('pultb');");
                            }
                            rcol_left++;
                        }
                        if (j == 0)
                        {
                            HtmlTableCell d = new HtmlTableCell();
                            d.Controls.Add(new LiteralControl("<div id='pultb' style='display:none'>"));
                            HtmlInputRadioButton rdopultbsmplus = new HtmlInputRadioButton();
                            rdopultbsmplus.ID = Convert.ToString(11); ;
                            rdopultbsmplus.Value = theDSDropDown.Tables[2].Rows[1][1].ToString();
                            d.Controls.Add(rdopultbsmplus);
                            d.Controls.Add(new LiteralControl(rdopultbsmplus.Value));
                            d.Controls.Add(new LiteralControl("</br>"));
                            HtmlInputRadioButton rdopultbsminus = new HtmlInputRadioButton();
                            rdopultbsminus.ID = Convert.ToString(12); ;
                            rdopultbsminus.Value = theDSDropDown.Tables[2].Rows[2][1].ToString();
                            d.Controls.Add(rdopultbsminus);
                            d.Controls.Add(new LiteralControl(rdopultbsminus.Value));
                            d.Controls.Add(new LiteralControl("</div>"));
                            r.Cells.Add(d);
                        }
                        tblOIsAIDsleft.Rows.Add(r);
                    }
                }
                //Coding for Saving HIV Associated Conditions - right
                theDTOIAIDsright = theDSDropDown.Tables[3];
                if (theDTOIAIDsright != null)
                {
                    int rcol_right = 0;
                    for (int i = 0; i < theDTOIAIDsright.Rows.Count; i++)
                    {
                        HtmlTableRow trright = new HtmlTableRow();
                        HtmlTableCell tc2_0 = new HtmlTableCell();
                        HtmlInputCheckBox chkOIsDsright = new HtmlInputCheckBox();
                        chkOIsDsright.ID = Convert.ToString("right" + rcol_right);
                        chkOIsDsright.Value = theDTOIAIDsright.Rows[i][1].ToString();
                        tc2_0.Controls.Add(chkOIsDsright);
                        tc2_0.Controls.Add(new LiteralControl(chkOIsDsright.Value));
                        trright.Cells.Add(tc2_0);

                        if (chkOIsDsright.Value == "Other")
                        {
                            HtmlTableCell tc2_1 = new HtmlTableCell();
                            tc2_1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' for='otherOIsAIDs'>"));
                            tc2_1.Controls.Add(new LiteralControl("<SPAN id='otherOIsAIDs' style='DISPLAY:none'>Specify: "));
                            HtmlInputText HTextOIsAIDsillness = new HtmlInputText();
                            HTextOIsAIDsillness.ID = "txtotherOIsAIDsillness";
                            HTextOIsAIDsillness.Size = 10;
                            tc2_1.Controls.Add(HTextOIsAIDsillness);
                            tc2_1.Controls.Add(new LiteralControl(HTextOIsAIDsillness.Value));
                            tc2_1.Controls.Add(new LiteralControl("</SPAN>"));
                            trright.Cells.Add(tc2_1);
                            chkOIsDsright.Attributes.Add("onclick", "toggle('otherOIsAIDs');");
                        }
                        rcol_right++;
                        tblOIsAIDsright.Rows.Add(trright);
                    }
                }
                //Assessment 
                theDV = new DataView(DStheXML.Tables["Mst_Assessment"]);
                theDV.RowFilter = "AssessmentCategoryID=2";
                if (theDV.Table != null)
                {
                    theDT = theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCheckedList(cblAssessment, theDT, "AssessmentName", "AssessmentID");
                }
                //Therapy Change Codes
                theDV = new DataView(DStheXML.Tables["Mst_Reason"]);
                theDV.RowFilter = "CategoryID=2";
                if (theDV.Table != null)
                {
                    theDT = theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlArvTherapyChangeCode, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Therapy Stop Codes
                theDV = new DataView(DStheXML.Tables["Mst_Reason"]);
                theDV.RowFilter = "CategoryID=2 and DeleteFlag=0";
                //theDV = new DataView(DStheXML.Tables["Mst_Decode"]);
                //theDV.RowFilter = "DeleteFlag=0 and CodeID=34";
                if (theDV.Table != null)
                {
                    theDT = theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlArvTherapyStopCode, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //Appointment Reason
                theDV = new DataView(DStheXML.Tables["mst_Decode"]);
                theDV.RowFilter = "CodeID=26";
                if (theDV.Table != null)
                {
                    theDT = theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlAppReason, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                //WHO Stage
                int WHOStage = 0;
                if (theDSDropDown.Tables[7].Rows.Count > 0)
                    WHOStage = Convert.ToInt32(theDSDropDown.Tables[7].Rows[0]["WHOStage"]);

                int WhoStageSRNO = 0;
                theDV = new DataView(DStheXML.Tables["mst_Decode"]);
                theDV.RowFilter = "Id=" + WHOStage.ToString();
                if (theDV.Count > 0)
                    WhoStageSRNO = Convert.ToInt32(theDV[0]["SRNO"]);
                theDV = null;

                theDV = new DataView(DStheXML.Tables["mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=22 and SystemId = 1 and SRNO>=" + WhoStageSRNO.ToString();
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlWHOStage, theDT, "Name", "ID");
                }

                //////if (WHOStage == 87 || WHOStage == 0)
                //////{
                //////    ddlWHOStage.Items.Add("Select");
                //////    ddlWHOStage.Items.Add("I");
                //////    ddlWHOStage.Items.Add("II");
                //////    ddlWHOStage.Items.Add("III");
                //////    ddlWHOStage.Items.Add("IV");
                //////    ddlWHOStage.Items[0].Value = "0";
                //////    ddlWHOStage.Items[1].Value = "87";
                //////    ddlWHOStage.Items[2].Value = "88";
                //////    ddlWHOStage.Items[3].Value = "89";
                //////    ddlWHOStage.Items[4].Value = "90";
                //////}
                //////else if (WHOStage == 88)
                //////{
                //////    ddlWHOStage.Items.Add("Select");
                //////    ddlWHOStage.Items.Add("II");
                //////    ddlWHOStage.Items.Add("III");
                //////    ddlWHOStage.Items.Add("IV");
                //////    ddlWHOStage.Items[0].Value = "0";
                //////    ddlWHOStage.Items[1].Value = "88";
                //////    ddlWHOStage.Items[2].Value = "89";
                //////    ddlWHOStage.Items[3].Value = "90";
                //////}
                //////else if (WHOStage == 89)
                //////{
                //////    ddlWHOStage.Items.Add("Select");
                //////    ddlWHOStage.Items.Add("III");
                //////    ddlWHOStage.Items.Add("IV");
                //////    ddlWHOStage.Items[0].Value = "0";
                //////    ddlWHOStage.Items[1].Value = "89";
                //////    ddlWHOStage.Items[2].Value = "90";
                //////}
                //////else if (WHOStage == 90)
                //////{
                //////    ddlWHOStage.Items.Add("Select");
                //////    ddlWHOStage.Items.Add("IV");
                //////    ddlWHOStage.Items[0].Value = "0";
                //////    ddlWHOStage.Items[1].Value = "90";
                //////}

                ///}

                //Change/Stop Therapy Reason ID
                ReasonID = Convert.ToInt32(theDSDropDown.Tables[4].Rows[0]["OtherReason"]);
                //(Master.FindControl("lblpntStatus") as Label).Text = Request.QueryString["sts"].ToString();
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["HIVPatientStatus"].ToString();
            }
        }

        public DataSet DataSet_ARTFollowup()
        {
            // Table 0
            DataSet theDS = new DataSet();
            DataTable dtMissedReason = new DataTable();
            DataColumn theMissedReasonID = new DataColumn("MissedReasonID");
            theMissedReasonID.DataType = System.Type.GetType("System.Int32");
            dtMissedReason.Columns.Add(theMissedReasonID);

            DataColumn theMissedReasonID_Other = new DataColumn("MissedReasonID_Other");
            theMissedReasonID_Other.DataType = System.Type.GetType("System.String");
            dtMissedReason.Columns.Add(theMissedReasonID_Other);

            DataRow drMissedReason;
            string chktrueother = "";
            int chktrueothervalue = 0;
            foreach (HtmlTableRow tr in tblReasonMissed.Rows)
            {
                drMissedReason = dtMissedReason.NewRow();
                foreach (HtmlTableCell tc in tr.Cells)
                {
                    foreach (Control ct in tc.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Checked == true)
                            {
                                foreach (DataRow dr in theDTMissedReason.Rows)
                                {
                                    if (((HtmlInputCheckBox)ct).Value == dr[1].ToString())
                                    {
                                        if (dr[1].ToString() == "Other")
                                        {
                                            chktrueothervalue = Convert.ToInt32(dr[0]);
                                            chktrueother = dr[1].ToString();
                                        }
                                        else
                                        {
                                            drMissedReason["MissedReasonID"] = dr[0].ToString();
                                            drMissedReason["MissedReasonID_Other"] = null;
                                            dtMissedReason.Rows.Add(drMissedReason);
                                        }
                                    }
                                }
                            }
                        }

                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        {
                            if (chktrueother == "Other")
                            {
                                if (ct.ID.ToString() == "txtotherRMissed")
                                {
                                    drMissedReason["MissedReasonID"] = chktrueothervalue;
                                    drMissedReason["MissedReasonID_Other"] = ((HtmlInputText)ct).Value;
                                    dtMissedReason.Rows.Add(drMissedReason);
                                }
                            }
                        }
                    }
                }
            }
            theDS.Tables.Add(dtMissedReason);

            // Coding for saving Presenting Complaints // Table 1
            DataTable dtPresentComplaints = new DataTable();

            DataColumn thePresentComplaintsID = new DataColumn("PresentComplaintsID");
            thePresentComplaintsID.DataType = System.Type.GetType("System.Int32");
            dtPresentComplaints.Columns.Add(thePresentComplaintsID);

            DataRow drPresentComplaints;


            for (int i = 0; i < cblPresentingComplaints.Items.Count; i++)
            {
                if (cblPresentingComplaints.Items[i].Selected)
                {
                    drPresentComplaints = dtPresentComplaints.NewRow();
                    drPresentComplaints["PresentComplaintsID"] = Convert.ToInt32(cblPresentingComplaints.Items[i].Value);
                    dtPresentComplaints.Rows.Add(drPresentComplaints);
                }

            }
            theDS.Tables.Add(dtPresentComplaints);

            ////Code for Saving ARV Side Effects[left]-//dtl_PatientSymptoms// Table 2

            DataTable dtARVSideEffectsleft = new DataTable();

            DataColumn theARVSideEffectsID1 = new DataColumn("ARVSideEffectsID1");
            theARVSideEffectsID1.DataType = System.Type.GetType("System.Int32");
            dtARVSideEffectsleft.Columns.Add(theARVSideEffectsID1);

            DataRow drARVSideEffectsleft;


            for (int i = 0; i < cblARVSideEffectleft.Items.Count; i++)
            {
                if (cblARVSideEffectleft.Items[i].Selected)
                {
                    drARVSideEffectsleft = dtARVSideEffectsleft.NewRow();
                    drARVSideEffectsleft["ARVSideEffectsID1"] = Convert.ToInt32(cblARVSideEffectleft.Items[i].Value);
                    dtARVSideEffectsleft.Rows.Add(drARVSideEffectsleft);
                }

            }
            theDS.Tables.Add(dtARVSideEffectsleft);

            ////Code for Saving ARV Side Effects[right] - //dtl_PatientSymptoms // Table 3

            DataTable dtARVSideEffectsright = new DataTable();

            DataColumn theARVSideEffectsID2 = new DataColumn("ARVSideEffectsID2");
            theARVSideEffectsID2.DataType = System.Type.GetType("System.Int32");
            dtARVSideEffectsright.Columns.Add(theARVSideEffectsID2);

            DataRow drARVSideEffectsright;


            for (int i = 0; i < cblARVSideEffectright.Items.Count; i++)
            {
                if (cblARVSideEffectright.Items[i].Selected)
                {
                    drARVSideEffectsright = dtARVSideEffectsright.NewRow();
                    drARVSideEffectsright["ARVSideEffectsID2"] = Convert.ToInt32(cblARVSideEffectright.Items[i].Value);
                    dtARVSideEffectsright.Rows.Add(drARVSideEffectsright);
                }

            }
            theDS.Tables.Add(dtARVSideEffectsright);

            ////Code for Saving TB Screen - //dtl_PatientSymptoms // Table 4

            DataTable dtTBScreen = new DataTable();
            DataColumn theTBScreen = new DataColumn("TBScreenID");
            theTBScreen.DataType = System.Type.GetType("System.Int32");
            dtTBScreen.Columns.Add(theTBScreen);

            DataRow drTBScreen;
            for (int i = 0; i < cblTBScreen.Items.Count; i++)
            {
                if (cblTBScreen.Items[i].Selected)
                {
                    drTBScreen = dtTBScreen.NewRow();
                    drTBScreen["TBScreenID"] = Convert.ToInt32(cblTBScreen.Items[i].Value);
                    dtTBScreen.Rows.Add(drTBScreen);
                }

            }
            if (rdoTBScreenPerformed.Checked == true)
            {
                DataRow[] theDR = dtTBScreen.Select("TBScreenId='70'");
                if (theDR.Length == 0)
                {
                    drTBScreen = dtTBScreen.NewRow();
                    drTBScreen["TBScreenID"] = "70";
                    dtTBScreen.Rows.Add(drTBScreen);
                }
            }
            else if (rdoTBScreenNotDocumented.Checked == true)
            {
                DataRow[] theDR = dtTBScreen.Select("TBScreenId='71'");
                if (theDR.Length == 0)
                {
                    drTBScreen = dtTBScreen.NewRow();
                    drTBScreen["TBScreenID"] = "71";
                    dtTBScreen.Rows.Add(drTBScreen);
                }
            }

            theDS.Tables.Add(dtTBScreen);

            //Code for Saving HIV Associated Conditions[left]-//dtl_PatientDisease // Table 5

            DataTable dtOI_AIDSleft = new DataTable();

            DataColumn theOI_AIDS_ID1 = new DataColumn("OI_AIDS_ID1");
            theOI_AIDS_ID1.DataType = System.Type.GetType("System.Int32");
            dtOI_AIDSleft.Columns.Add(theOI_AIDS_ID1);

            DataColumn theOI_AIDS_Desc = new DataColumn("OI_AIDS_Desc");
            theOI_AIDS_Desc.DataType = System.Type.GetType("System.String");
            dtOI_AIDSleft.Columns.Add(theOI_AIDS_Desc);


            DataRow drOI_AIDSleft;

            string strOI_AIDSValue;
            HtmlInputCheckBox chkpulTB = new HtmlInputCheckBox();
            foreach (HtmlTableRow r in tblOIsAIDsleft.Rows)
            {
                foreach (HtmlTableCell c in r.Cells)
                {
                    foreach (Control ct in c.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Checked == true)
                            {
                                strOI_AIDSValue = ((HtmlInputCheckBox)ct).Value;
                                foreach (DataRow dr in theDTOIAIDsleft.Rows)
                                {
                                    if (dr[1].ToString() == strOI_AIDSValue)
                                    {
                                        if (strOI_AIDSValue == "Pulmonary TB")
                                        {
                                            chkpulTB.Checked = true;
                                            drOI_AIDSleft = dtOI_AIDSleft.NewRow();
                                            drOI_AIDSleft["OI_AIDS_ID1"] = dr[0].ToString();
                                            drOI_AIDSleft["OI_AIDS_Desc"] = "Blank";
                                            dtOI_AIDSleft.Rows.Add(drOI_AIDSleft);
                                        }
                                        else
                                        {
                                            drOI_AIDSleft = dtOI_AIDSleft.NewRow();
                                            drOI_AIDSleft["OI_AIDS_ID1"] = dr[0].ToString();
                                            drOI_AIDSleft["OI_AIDS_Desc"] = "Blank";
                                            dtOI_AIDSleft.Rows.Add(drOI_AIDSleft);
                                        }
                                    }
                                }
                            }

                        }
                        else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                        {
                            if (chkpulTB.Checked == true)
                            {
                                if (((HtmlInputRadioButton)ct).Checked == true)
                                {
                                    strOI_AIDSValue = ((HtmlInputRadioButton)ct).Value;
                                    foreach (DataRow dr in theDTOIAIDsleft.Rows)
                                    {
                                        if (dr[1].ToString() == strOI_AIDSValue)
                                        {
                                            drOI_AIDSleft = dtOI_AIDSleft.NewRow();
                                            drOI_AIDSleft["OI_AIDS_ID1"] = dr[0].ToString();
                                            drOI_AIDSleft["OI_AIDS_Desc"] = "Blank";
                                            dtOI_AIDSleft.Rows.Add(drOI_AIDSleft);

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            theDS.Tables.Add(dtOI_AIDSleft);

            //Code for Saving HIV Associated Conditions[right]-//dtl_PatientDisease // Table 6

            int OI_AIDS_Desc_OtherID = 0;
            DataTable dtOI_AIDSright = new DataTable();

            DataColumn theOI_AIDS_ID2 = new DataColumn("OI_AIDS_ID2");
            theOI_AIDS_ID2.DataType = System.Type.GetType("System.Int32");
            dtOI_AIDSright.Columns.Add(theOI_AIDS_ID2);

            DataColumn theOI_AIDS_Desc_Other = new DataColumn("OI_AIDS_Desc_other");
            theOI_AIDS_Desc_Other.DataType = System.Type.GetType("System.String");
            dtOI_AIDSright.Columns.Add(theOI_AIDS_Desc_Other);

            DataRow drOI_AIDSright;

            foreach (HtmlTableRow r in tblOIsAIDsright.Rows)
            {
                foreach (HtmlTableCell c in r.Cells)
                {
                    foreach (Control ct in c.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Checked == true)
                            {
                                strOI_AIDSValue = ((HtmlInputCheckBox)ct).Value;
                                foreach (DataRow dr in theDTOIAIDsright.Rows)
                                {
                                    if (dr[1].ToString() == strOI_AIDSValue)
                                    {
                                        if (dr[1].ToString() == "Other")
                                        {
                                            OI_AIDS_Desc_OtherID = Convert.ToInt32(dr[0].ToString());
                                        }
                                        else
                                        {
                                            drOI_AIDSright = dtOI_AIDSright.NewRow();
                                            drOI_AIDSright["OI_AIDS_ID2"] = dr[0].ToString();
                                            drOI_AIDSright["OI_AIDS_Desc_other"] = "Blank";
                                            dtOI_AIDSright.Rows.Add(drOI_AIDSright);
                                        }
                                    }
                                }
                            }
                        }
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        {
                            if (((HtmlInputText)ct).Value == "")
                            {
                                drOI_AIDSright = dtOI_AIDSright.NewRow();
                                drOI_AIDSright["OI_AIDS_ID2"] = OI_AIDS_Desc_OtherID.ToString();
                                drOI_AIDSright["OI_AIDS_Desc_other"] = "Blank";
                                dtOI_AIDSright.Rows.Add(drOI_AIDSright);
                            }
                            else
                            {
                                drOI_AIDSright = dtOI_AIDSright.NewRow();
                                drOI_AIDSright["OI_AIDS_ID2"] = OI_AIDS_Desc_OtherID.ToString();
                                drOI_AIDSright["OI_AIDS_Desc_other"] = ((HtmlInputText)ct).Value;
                                dtOI_AIDSright.Rows.Add(drOI_AIDSright);
                            }
                        }
                    }
                }
            }

            theDS.Tables.Add(dtOI_AIDSright);

            //Code for Saving AssessmentID //-dtl_PatientAssessment // Table 7

            DataTable dtAssessment = new DataTable();

            DataColumn theAssessmentID = new DataColumn("AssessmentID");
            theAssessmentID.DataType = System.Type.GetType("System.Int32");
            dtAssessment.Columns.Add(theAssessmentID);

            DataRow drAssessment;

            for (int i = 0; i < cblAssessment.Items.Count; i++)
            {
                if (cblAssessment.Items[i].Selected)
                {
                    drAssessment = dtAssessment.NewRow();
                    drAssessment["AssessmentID"] = Convert.ToInt32(cblAssessment.Items[i].Value);
                    dtAssessment.Rows.Add(drAssessment);
                }

            }
            theDS.Tables.Add(dtAssessment);
            return theDS;
        }
        // Function to Pass HashTable Parameters
        private void FollowUpParameters()
        {
            htFollowupParameters = new Hashtable();
            IQCareUtils theUtils = new IQCareUtils();
            int locationID = 0;
            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                //visitPK = Convert.ToInt32(ViewState["VisitID_add"]);
                locationID = Convert.ToInt32(Session["AppLocationId"]);
                createDate = Convert.ToDateTime(ViewState["CreateDate"]);
            }
            ////else if (Request.QueryString["name"] == "Edit")
            else if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
            {
                //visitPK = Convert.ToInt32(Request.QueryString["visitid"]);
                visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                //locationID = Convert.ToInt32(Request.QueryString["locationid"]);
                locationID = Convert.ToInt32(Session["ServiceLocationId"]);
                createDate = Convert.ToDateTime(ViewState["createdate"]);
            }
            htFollowupParameters.Add("Visitdate", txtvisitDate.Value);
            htFollowupParameters.Add("txtpriorARVsFU", txtpriorARVsCD4.Text);
            htFollowupParameters.Add("txtpriorARVsCD4DateFU", txtpriorARVsCD4Date.Value);
            htFollowupParameters.Add("VisittypeIDFU", VisittypeFU);
            htFollowupParameters.Add("LMPdate", txtLMPdate.Value);
            int Pregnant = this.rdopregnantYes.Checked == true ? 1 : (this.rdopregnantNo.Checked == true ? 0 : 9);
            int Delivered = this.rdoDeliveredYes.Checked == true ? 1 : (this.rdoDeliveredNo.Checked == true ? 0 : 9);
            htFollowupParameters.Add("Delivered", Delivered);
            if (Delivered == 1)
            {
                htFollowupParameters.Add("Pregnant", 1);
                htFollowupParameters.Add("DelDate", txtDeliDate.Value);
                htFollowupParameters.Add("EDDDate", "");
            }
            else if (Delivered == 0)
            {
                htFollowupParameters.Add("Pregnant", 1);
                htFollowupParameters.Add("EDDDate", txtEDDDate.Value);
                htFollowupParameters.Add("DelDate", "");
            }
            else
            {
                htFollowupParameters.Add("Pregnant", Pregnant);
                if (Pregnant == 1)
                {
                    htFollowupParameters.Add("DelDate", "");
                    htFollowupParameters.Add("EDDDate", txtEDDDate.Value);
                }
                else
                {
                    htFollowupParameters.Add("DelDate", "");
                    htFollowupParameters.Add("EDDDate", "");
                }
            }
            string strmissedlastweek = this.chMissedLastWeeknone.Checked == true ? "11111" : txtMissedLastWeek.Value;
            htFollowupParameters.Add("DosesMissedLastWeek", strmissedlastweek);
            string strmissedlastmonth = this.chMissedLastMonthnone.Checked == true ? "11111" : txtMissedLastMonth.Value;
            htFollowupParameters.Add("DosesMissedLastMonth", strmissedlastmonth);
            htFollowupParameters.Add("NumDOTPerWeek", txtNumDOTPerWeek.Text);
            htFollowupParameters.Add("NumHomeVisitsPerWeek", txtNumHomeVisitsPerWeek.Text);
            htFollowupParameters.Add("SupportGroup", ckSupportGroup.Checked);
            if (rdoInterrupted.Checked == false)
            {
                txtInterruptedDate.Value = "";
                TxtInterruptedNumDays.Value = "";
            }
            htFollowupParameters.Add("InterruptedDate", txtInterruptedDate.Value);
            htFollowupParameters.Add("InterruptedNumDays", TxtInterruptedNumDays.Value);
            if (rdostopped.Checked == false)
            {
                txtstoppedDate.Value = "";
                txtstoppedNumDays.Value = "";
            }
            htFollowupParameters.Add("stoppedDate", txtstoppedDate.Value);
            htFollowupParameters.Add("stoppedNumDays", txtstoppedNumDays.Value);
            htFollowupParameters.Add("HerbalMeds", ckHerbalMeds.Checked);
            htFollowupParameters.Add("physTemp", txtphysTemp.Text);
            htFollowupParameters.Add("physRR", txtphysRR.Text);
            htFollowupParameters.Add("physHR", txtphysHR.Text);
            htFollowupParameters.Add("physBPDiastolic", txtphysBPDiastolic.Text);
            htFollowupParameters.Add("physBPSystolic", txtphysBPSystolic.Text);
            htFollowupParameters.Add("physHeight", txtphysHeight.Text);
            htFollowupParameters.Add("physWeight", txtphysWeight.Text);
            htFollowupParameters.Add("physExamPain", ddlPain.Value);
            htFollowupParameters.Add("phyWHOstage", ddlWHOStage.SelectedValue);
            htFollowupParameters.Add("physWABStage", ddlphysWABStage.SelectedValue);
            string ARVTherapyCode = "0";
            if (lstclinPlanFU.Value == "98")
            {
                ARVTherapyCode = ddlArvTherapyChangeCode.SelectedValue;
            }
            else if (lstclinPlanFU.Value == "99")
            {

                ARVTherapyCode = ddlArvTherapyStopCode.SelectedValue;
            }
            htFollowupParameters.Add("ARTEndDate", txtARTEndeddate.Value);
            htFollowupParameters.Add("ARVtherapyplan", lstclinPlanFU.Value);
            if (lstclinPlanFU.Value.ToString() == "96")
            {
                Session["ARTEndedStatus"] = "";
            }
            else if (lstclinPlanFU.Value.ToString() == "99")
            {
                Session["ARTEndedStatus"] = "ART Stopped";
            }
            htFollowupParameters.Add("ArvTherapyReasonCode", ARVTherapyCode);

            string OtherReason = "";
            if (ReasonID == Convert.ToInt32(ddlArvTherapyChangeCode.SelectedValue))
            {
                OtherReason = txtarvTherapyChangeCodeOtherName.Value;
            }
            else if (ReasonID == Convert.ToInt32(ddlArvTherapyStopCode.SelectedValue))
            {
                OtherReason = txtarvTherapyStopCodeOtherName.Value;
            }
            htFollowupParameters.Add("ARVTherapyReasonOther", OtherReason);

            //Code section for Appointment Business rules

            //Appointment Dates
            if (txtappDate.Value == "")
            {
                if (this.lstappPeriod.Value != "0" && txtvisitDate.Value != "")
                {
                    txtappDate.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtvisitDate.Value).AddDays(Convert.ToInt32(this.lstappPeriod.Value)));
                }
                else
                {
                    htFollowupParameters.Add("AppExist", 0);
                    htFollowupParameters.Add("VisitIDApp", 0);
                }
            }

            //Code section for Appointment Business rules
            if (txtappDate.Value != "")
            {
                IInitialEval IEAppManager;
                IEAppManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
                DataSet theDSApp = IEAppManager.GetAppointment(Convert.ToInt32(Session["PatientId"]), locationID, Convert.ToDateTime(txtappDate.Value), Convert.ToInt32(ddlAppReason.SelectedValue));
                if (Convert.ToInt32(theDSApp.Tables[0].Rows[0]["ExistFlag"]) == 1)
                {
                    if (theDSApp.Tables[1].Rows.Count > 0)
                    {
                        htFollowupParameters.Add("AppExist", 1);
                        htFollowupParameters.Add("VisitIDApp", Convert.ToInt32(theDSApp.Tables[1].Rows[0]["Visit_pk"]));
                    }
                    else
                    {
                        htFollowupParameters.Add("AppExist", 0);
                        htFollowupParameters.Add("VisitIDApp", 0);
                    }
                }
                else
                {
                    htFollowupParameters.Add("AppExist", 0);
                    htFollowupParameters.Add("VisitIDApp", 0);
                }
            }
            htFollowupParameters.Add("appdate", txtappDate.Value);
            htFollowupParameters.Add("appreason", ddlAppReason.SelectedValue);
            htFollowupParameters.Add("Signatureid", ddlCounsellorSignature.SelectedValue);
            htFollowupParameters.Add("UserID", Convert.ToInt32(Session["AppUserId"].ToString()));
            htFollowupParameters.Add("PlanNote", MulttxtclinPlanNotes.Value);

            //Ajay Kumar-05-Jan-2010 Begin
            //Clinical note added
            htFollowupParameters.Add("ClinicalNotes", txtClinicalNotes.Text.Trim());
            //Ajay Kumar-05-Jan-2010 End
            if ((Convert.ToInt32(Session["PatientVisitId"]) > 0))
            {
                htFollowupParameters.Add("Flag", 1);
            }
            else { htFollowupParameters.Add("Flag", 0); }
        }


        private void Maintain_ViewState(Boolean MissedLastWeeknone, Boolean MissedLastMonthnone, Boolean Interrupted, Boolean Stopped, Boolean presentingComplaintsNone, Boolean ARVSideEffectsYes, Boolean OIsAIDsIllnessYes, Boolean TBScreenYes, int clinPlanFU, string ArvTherapyChangeCode, string ArvTherapyStopCode)
        {
            if (MissedLastWeeknone) ViewState["MissedLastWeeknone"] = "1";

            if (MissedLastMonthnone) ViewState["MissedLastMonthnone"] = "1";

            if (Interrupted) ViewState["Interrupted"] = "1";

            if (Stopped) ViewState["Stopped"] = "1";

            if (!presentingComplaintsNone) ViewState["presentingComplaintsNone"] = "1";

            if (ARVSideEffectsYes) ViewState["ARVSideEffectsYes"] = "1";

            if (OIsAIDsIllnessYes) ViewState["OIsAIDsIllnessYes"] = "1";

            if (clinPlanFU == 98) ViewState["ChangeRegimen"] = "1";

            if (clinPlanFU == 99) ViewState["StopRegimen"] = "1";

            if (ArvTherapyChangeCode == "Other") ViewState["changeROther"] = "1";

            if (ArvTherapyStopCode == "Other") ViewState["stopROther"] = "1";

            if (TBScreenYes) ViewState["TBScreenYes"] = "1";
        }

        /*************************Maintain OI & AIDS status in case of field validation *******************/
        private void ShowOIAIDS_ReasonMissed()
        {
            //Left Reason missed
            foreach (HtmlTableRow r in tblReasonMissed.Rows)
            {
                foreach (HtmlTableCell c in r.Cells)
                {
                    foreach (Control ct in c.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Value == "Other")
                            {
                                if (((HtmlInputCheckBox)ct).Checked == true)
                                {
                                    string script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'OIsAIDs_ID_2'>\n";
                                    script += "show('otherRMissed');\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "OIsAIDs_ID_2", script);
                                }
                            }
                        }
                    }
                }
            }


            //Left DIV Items
            foreach (HtmlTableRow r in tblOIsAIDsleft.Rows)
            {
                foreach (HtmlTableCell c in r.Cells)
                {
                    foreach (Control ct in c.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Value == "Pulmonary TB")
                            {
                                if (((HtmlInputCheckBox)ct).Checked == true)
                                {
                                    string script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'OIsAIDs_ID'>\n";
                                    script += "show('pultb');\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "OIsAIDs_ID", script);
                                }
                            }
                        }
                    }
                }
            }

            //Right DIV Items
            foreach (HtmlTableRow r in tblOIsAIDsright.Rows)
            {
                foreach (HtmlTableCell c in r.Cells)
                {
                    foreach (Control ct in c.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Value == "Other")
                            {
                                if (((HtmlInputCheckBox)ct).Checked == true)
                                {
                                    string script = "";
                                    script = "<script language = 'javascript' defer='defer' id='OIsAIDs_ID_1'>\n";
                                    script += "show('otherOIsAIDs');\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "OIsAIDs_ID_1", script);
                                }
                            }
                        }
                    }
                }
            }

        }

        private void Show_Hide()
        {
            if (ViewState["MissedLastWeeknone"].ToString() == "1")
            {
                string scriptMissedLastWeeknone = "<script language = 'javascript' defer ='defer' id = 'onMissedLastWeeknone'>\n";
                scriptMissedLastWeeknone += "show('MissedLastWeek'); \n";
                scriptMissedLastWeeknone += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onMissedLastWeeknone", scriptMissedLastWeeknone);
            }

            if (ViewState["MissedLastMonthnone"].ToString() == "1")
            {
                string scriptMissedLastMonthnone = "<script language = 'javascript' defer ='defer' id = 'onMissedLastMonthnone'>\n";
                scriptMissedLastMonthnone += "show('MissedLastMonth'); \n";
                scriptMissedLastMonthnone += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onMissedLastMonthnone", scriptMissedLastMonthnone);
            }

            if (ViewState["Interrupted"].ToString() == "1")
            {
                string scriptInterrupted = "<script language = 'javascript' defer ='defer' id = 'onInterrupted'>\n";
                scriptInterrupted += "show('interruptedDate'); \n";
                scriptInterrupted += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onInterrupted", scriptInterrupted);

            }

            if (ViewState["Stopped"].ToString() == "1")
            {
                string scriptStopped = "<script language = 'javascript' defer ='defer' id = 'onStopped'>\n";
                scriptStopped += "show('stopDate'); \n";
                scriptStopped += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onStopped", scriptStopped);

            }

            if (ViewState["presentingComplaintsNone"].ToString() == "1")
            {
                string scriptpresentingComplaints = "<script language = 'javascript' defer ='defer' id = 'onpresentingComplaints'>\n";
                scriptpresentingComplaints += "display_chklist('" + chkpresentingComplaintsNone.ClientID + "', '" + presentingComplaintsShow.ClientID + "');\n";
                scriptpresentingComplaints += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onpresentingComplaints", scriptpresentingComplaints);

            }

            if (ViewState["ARVSideEffectsYes"].ToString() == "1")
            {
                string scriptARVSideEffectsYes = "<script language = 'javascript' defer ='defer' id = 'onARVSideEffectsYes'>\n";
                scriptARVSideEffectsYes += "show('sideEffectsSelected'); \n";
                scriptARVSideEffectsYes += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onARVSideEffectsYes", scriptARVSideEffectsYes);
            }

            if (ViewState["OIsAIDsIllnessYes"].ToString() == "1")
            {
                string scriptOIsAIDsIllnessYes = "<script language = 'javascript' defer ='defer' id = 'onOIsAIDsIllnessYes'>\n";
                scriptOIsAIDsIllnessYes += "show('assocSelected'); \n";
                scriptOIsAIDsIllnessYes += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onOIsAIDsIllnessYes", scriptOIsAIDsIllnessYes);
            }
            if (ViewState["TBScreenYes"].ToString() == "1")
            {
                string scriptTBScreenYes = "<script language = 'javascript' defer ='defer' id = 'TBScreenYes'>\n";
                scriptTBScreenYes += "show('TBScreenSelected'); \n";
                scriptTBScreenYes += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "TBScreenYes", scriptTBScreenYes);
            }

            if (ViewState["ChangeRegimen"].ToString() == "1")
            {
                string scriptChangeRegimen = "<script language = 'javascript' defer ='defer' id = 'onChangeRegimen'>\n";
                scriptChangeRegimen += "show('arvTherapyChange'); \n";
                scriptChangeRegimen += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onChangeRegimen", scriptChangeRegimen);

                if (ViewState["changeROther"].ToString() == "1")
                {
                    string scriptchangeROther = "<script language = 'javascript' defer ='defer' id = 'onchangeROther'>\n";
                    scriptchangeROther += "show('otherarvTherapyChangeCode'); \n";
                    scriptchangeROther += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "onchangeROther", scriptchangeROther);
                }

            }
            if (ViewState["StopRegimen"].ToString() == "1")
            {
                string scriptStopRegimen = "<script language = 'javascript' defer ='defer' id = 'onstopRegimen'>\n";
                scriptStopRegimen += "show('arvTherapyStop'); \n";
                scriptStopRegimen += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onstopRegimen", scriptStopRegimen);

                if (ViewState["stopROther"].ToString() == "1")
                {
                    string scriptstopROther = "<script language = 'javascript' defer ='defer' id = 'onstopROther'>\n";
                    scriptstopROther += "show('otherarvTherapyStopCode'); \n";
                    scriptstopROther += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "onstopROther", scriptstopROther);
                }
            }

            if (Convert.ToInt32(ViewState["Pregstatus"]) == 1)
            {
                string script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'Pregnant_1'>\n";
                script += "show('rdopregnantyesno');\n";
                script += "hide('spdelivery');\n";
                script += "show('spanEDD');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "Pregnant_1", script);
            }

            if (Convert.ToInt32(ViewState["Pregstatus"]) == 2)
            {
                string script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'Pregnant_2'>\n";
                script += "show('rdopregnantyesno');\n";
                script += "hide('spdelivery');\n";
                script += "hide('spanEDD');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "Pregnant_2", script);
            }

            if (Convert.ToInt32(ViewState["Pregstatus"]) == 3)
            {
                string script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'Pregnant_3'>\n";
                script += "show('spdelivery');\n";
                script += "hide('rdopregnantyesno');\n";
                script += "show('spanEDD');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "Pregnant_3", script);
            }
            if (Convert.ToInt32(ViewState["Pregstatus"]) == 4)
            {

                string script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'Pregnant_4'>\n";
                script += "show('spdelivery');\n";
                script += "show('spanDelDate');\n";
                script += "hide('rdopregnantyesno');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "Pregnant_4", script);
            }

            if (rdopregnantNo.Checked == true)
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'rdoPregnantNo'>\n";
                script += "hide('spanEDD');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "rdoPregnantNo", script);
            }

            if (rdopregnantYes.Checked == true)
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'rdoPregnantYes'>\n";
                script += "show('spanEDD');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "rdoPregnantYes", script);
            }



            if (Convert.ToInt32(Session["PatientVisitId"]) == 0 && txtvisitDate.Value != "")
            {
                IInitialEval CallBackmgr;
                try
                {
                    DataSet theDS = new DataSet();
                    CallBackmgr = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
                    theDS = CallBackmgr.GetPregnantStatus(Convert.ToInt32(Session["PatientId"]), txtvisitDate.Value);
                    if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Pregnant"]) == 1)
                    {
                        string script = "<script language = 'javascript' defer ='defer' id = 'Delivery_0'>\n";
                        script += "show('spdelivery')\n";
                        script += "hide('rdopregnantyesno')\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "Delivery_0", script);

                        if (rdoDeliveredYes.Checked == true)
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'Delivery_1'>\n";
                            script += "show('spanDelDate')\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "Delivery_1", script);
                        }
                        if (rdoDeliveredNo.Checked == true)
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'Delivery_2'>\n";
                            script += "hide('spanDelDate')\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "Delivery_2", script);
                        }
                    }

                }
                catch
                {
                }
                finally
                {
                    CallBackmgr = null;
                }

            }


        }

        protected void Init_Add_Update_ART_Followup()
        {
            IFollowup FUManager;
            try
            {
                this.PatientID = Convert.ToInt32(Session["PatientId"]);
                FUManager = (IFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowup, BusinessProcess.Clinical");
                DataSet theDS = FUManager.GetPatientFollowUpART(PatientID);
                //Images
                ImgCD4.Visible = false;
                ImgMostRecentViralLoad.Visible = false;
                if (theDS.Tables[12].Rows.Count > 0)
                {
                    ViewState["ARTProgStatus"] = theDS.Tables[12].Rows[0]["ART/PalliativeCare"].ToString();
                }
                else
                    ViewState["ARTProgStatus"] = "";
                //Begin -- Code Section to check Whether IE form Already Exist
                //Jayant 31-01-2008 start
                if ((Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text == "0")
                {
                    DataSet DSInitialE = new DataSet();
                    DSInitialE = FUManager.GetClinicalDate(PatientID, 1);
                    if (Convert.ToInt32(DSInitialE.Tables[0].Rows[0]["Existflag"]) == 0)
                    {
                        Check_IE();
                        return;
                    }
                }
                //Jayant 31-01-2008 end
                //Pregnancy  and LMPpart
                if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Sex"].ToString()) == 16)
                {
                    tdPregnant.Visible = false;
                }


                if (theDS.Tables[15].Rows[0]["LMP"] != System.DBNull.Value)
                {
                    txtLMPdate.Value = string.Format("{0:dd-MMM-yyyy}", theDS.Tables[15].Rows[0]["LMP"]);
                }

                if (((Convert.ToInt32(Session["PatientVisitId"])) > 0) || (Request.QueryString["name"] == "Delete"))
                {
                    if (theDS.Tables[0].Rows[0]["Sex"] != System.DBNull.Value)
                    {
                        if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Sex"].ToString()) == 16)
                        {
                            tdPregnant.Visible = false;
                            txtLMPdate.Visible = false;
                        }
                    }
                }

                if (Request.QueryString["name"] == "Delete")
                {
                    if (theDS.Tables[0].Rows[0]["Sex"] != System.DBNull.Value)
                    {
                        if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Sex"].ToString()) == 16)
                        {
                            tdPregnant.Visible = false;
                            txtLMPdate.Visible = false;
                        }
                    }
                    Load_data();
                    btndataquality.Visible = false;
                    btnsave.Text = "Delete";
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
                FUManager = null;
            }

        }

        protected void Add_Attributes()
        {
            //txtvisitDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'); CalculateAgeHeight('" + Height + "','" + txtphysHeight.ClientID + "','" + hdnDOB.ClientID + "','" + txtvisitDate.ClientID + "'), addDays(), SendCodeName()");
            txtvisitDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'); addDays(), SendCodeName(),isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtvisitDate.ClientID + "', '" + txtvisitDate.ClientID + "');");
            txtvisitDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            chkpresentingComplaintsNone.Attributes.Add("OnClick", "display_chklist('" + chkpresentingComplaintsNone.ClientID + "', '" + presentingComplaintsShow.ClientID + "'); disableListItems('" + cblPresentingComplaints.ClientID + "', '" + cblPresentingComplaints.Items.Count + "') ");
            chkpresentingComplaintsNonehidden.Attributes.Add("OnClick", "display_chklist('" + chkpresentingComplaintsNonehidden.ClientID + "', '" + presentingComplaintsShow.ClientID + "')");

            chMissedLastWeeknone.Attributes.Add("OnClick", "toggle('MissedLastWeek');");
            chMissedLastMonthnone.Attributes.Add("OnClick", "toggle('MissedLastMonth');");
            txtLMPdate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtLMPdate.ClientID + "', '" + "LMP" + "', '" + txtLMPdate.ClientID + "')");
            txtLMPdate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3');");

            txtpriorARVsCD4Date.Attributes.Add("onBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtpriorARVsCD4Date.ClientID + "', '" + "prior ARVsCD4 Date" + "', '" + txtpriorARVsCD4Date.ClientID + "')");
            txtpriorARVsCD4Date.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3');");

            txtMissedLastWeek.Attributes.Add("onkeyup", "chkPostiveInteger('" + txtMissedLastWeek.ClientID + "')");
            txtMissedLastMonth.Attributes.Add("onkeyup", "chkPostiveInteger('" + txtMissedLastMonth.ClientID + "')");
            txtNumDOTPerWeek.Attributes.Add("onkeyup", "chkInteger('" + txtNumDOTPerWeek.ClientID + "')");
            txtNumHomeVisitsPerWeek.Attributes.Add("onkeyup", "chkInteger('" + txtNumHomeVisitsPerWeek.ClientID + "')");
            TxtInterruptedNumDays.Attributes.Add("onkeyup", "chkInteger('" + TxtInterruptedNumDays.ClientID + "')");
            txtstoppedNumDays.Attributes.Add("onkeyup", "chkInteger('" + txtstoppedNumDays.ClientID + "')");

            txtInterruptedDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtInterruptedDate.ClientID + "', '" + "Interrupted" + "', '" + txtInterruptedDate.ClientID + "')");
            txtInterruptedDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtstoppedDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtstoppedDate.ClientID + "', '" + "Stopped" + "', '" + txtstoppedDate.ClientID + "')");
            txtstoppedDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");


            rdoARVSideEffectsNone.Attributes.Add("OnClick", "down(this);hide('sideEffectsSelected'); disableChkboxList('" + cblARVSideEffectleft.ClientID + "', '" + cblARVSideEffectleft.Items.Count + "')");
            rdoARVSideEffectsNone.Attributes.Add("OnChange", "disableChkboxList('" + cblARVSideEffectright.ClientID + "', '" + cblARVSideEffectright.Items.Count + "')");

            //cblOIsAIDsleft.Attributes.Add("OnClick", "SetTB('" + cblOIsAIDsleft.ClientID + "', '" + cblOIsAIDsleft.Items.Count + "')");

            rdoARVSideEffectsNotDocumented.Attributes.Add("OnClick", "down(this);hide('sideEffectsSelected'); disableChkboxList('" + cblARVSideEffectleft.ClientID + "', '" + cblARVSideEffectleft.Items.Count + "')");
            rdoARVSideEffectsNotDocumented.Attributes.Add("OnChange", "disableChkboxList('" + cblARVSideEffectright.ClientID + "', '" + cblARVSideEffectright.Items.Count + "')");


            rdoOIsAIDsIllnessNone.Attributes.Add("OnClick", "down(this); hide('pultb'); hide('assocSelected'); hide('otherOIsAIDs'); disableChkRdoListItems_left('ctl00_IQCareContentPlaceHolder_', '" + tblOIsAIDsleft.Rows.Count + "'); disableChkRdoListItems_right('ctl00_IQCareContentPlaceHolder_right', '" + tblOIsAIDsright.Rows.Count + "');");
            rdoOIsAIDsIllnessNotDocumented.Attributes.Add("OnClick", "down(this); hide('pultb'); hide('assocSelected'); hide('otherOIsAIDs'); disableChkRdoListItems_left('ctl00_IQCareContentPlaceHolder_', '" + tblOIsAIDsleft.Rows.Count + "'); disableChkRdoListItems_right('ctl00_IQCareContentPlaceHolder_right', '" + tblOIsAIDsright.Rows.Count + "');");


            rdoTBScreenNotDocumented.Attributes.Add("OnClick", "down(this); hide('TBSCreenSelected'); disableChkboxList('" + cblTBScreen.ClientID + "', '" + cblTBScreen.Items.Count + "')");
            rdoTBScreenNotDocumented.Attributes.Add("OnChange", "disableChkboxList('" + cblTBScreen.ClientID + "', '" + cblTBScreen.Items.Count + "')");

            txtphysTemp.Attributes.Add("onkeyup", "chkDecimal('" + txtphysTemp.ClientID + "'); AddBoundary('" + txtphysTemp.ClientID + "','" + 30 + "','" + 50 + "')");
            txtphysTemp.Attributes.Add("onBlur", "isBetween('" + txtphysTemp.ClientID + "', '" + "Temperature" + "', '" + 30 + "', '" + 50 + "')");

            txtphysRR.Attributes.Add("onkeyup", "chkInteger('" + txtphysRR.ClientID + "')");
            txtphysRR.Attributes.Add("onBlur", "isBetween('" + txtphysRR.ClientID + "', '" + "RR" + "', '" + 4 + "', '" + 100 + "')");

            txtphysHR.Attributes.Add("onkeyup", "chkInteger('" + txtphysHR.ClientID + "')");
            txtphysHR.Attributes.Add("onBlur", "isBetween('" + txtphysHR.ClientID + "', '" + "HR" + "', '" + 30 + "', '" + 200 + "')");

            txtphysBPDiastolic.Attributes.Add("onkeyup", "chkInteger('" + txtphysBPDiastolic.ClientID + "')");
            txtphysBPDiastolic.Attributes.Add("onBlur", "isBetween('" + txtphysBPDiastolic.ClientID + "', '" + "physBPDiastolic" + "', '" + 30 + "', '" + 150 + "')");

            txtphysBPSystolic.Attributes.Add("onkeyup", "chkInteger('" + txtphysBPSystolic.ClientID + "')");
            txtphysBPSystolic.Attributes.Add("onBlur", "isBetween('" + txtphysBPSystolic.ClientID + "', '" + "physBPSystolic" + "', '" + 50 + "', '" + 250 + "')");

            txtphysHeight.Attributes.Add("onkeyup", "chkInteger('" + txtphysHeight.ClientID + "')");
            txtphysHeight.Attributes.Add("onBlur", "isBetween('" + txtphysHeight.ClientID + "', '" + "physHeight" + "', '" + 0 + "', '" + 250 + "')");

            txtphysWeight.Attributes.Add("onkeyup", "chkDecimal('" + txtphysWeight.ClientID + "')");
            txtphysWeight.Attributes.Add("onBlur", "isBetween('" + txtphysWeight.ClientID + "', '" + "physWeight" + "', '" + 0 + "', '" + 225 + "')");

            ddlArvTherapyChangeCode.Attributes.Add("onchange", "Therapy(this.options[this.selectedIndex].text, 1);");
            ddlArvTherapyStopCode.Attributes.Add("onchange", "Therapy(this.options[this.selectedIndex].text, 2);");
            //   ddlArvTherapyChangeCode.Attributes.Add("onclick", "Therapy(this.options[this.selectedIndex].text, 1);");
            //   ddlArvTherapyStopCode.Attributes.Add("onclick", "Therapy(this.options[this.selectedIndex].text, 2);");

            txtarvTherapyChangeCodeOtherName.Attributes.Add("onKeyup", "chkString('" + txtarvTherapyChangeCodeOtherName.ClientID + "')");
            txtarvTherapyStopCodeOtherName.Attributes.Add("onKeyup", "chkString('" + txtarvTherapyStopCodeOtherName.ClientID + "')");

            txtappDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            txtappDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtEDDDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            txtEDDDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtDeliDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            txtDeliDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtARTEndeddate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");
            txtARTEndeddate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
        }

        private Boolean Check_patient_status()
        {

            //(Master.FindControl("lblpntStatus") as Label).Text = Request.QueryString["sts"].ToString();
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["HIVPatientStatus"].ToString();
            if ((Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text == "0")
            {
                IQCareMsgBox.Show("SelectPatient", this);
                return false;
            }
            return true;
        }

        protected DataSet GetHeight(int PatientID, DateTime VisitDate)
        {
            IFollowup IMgrHeight;
            DataSet theDS = new DataSet();
            try
            {
                IMgrHeight = (IFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowup, BusinessProcess.Clinical");
                theDS = IMgrHeight.GetLatestHeight(PatientID, VisitDate);
            }
            catch
            {
            }
            finally
            {
                IMgrHeight = null;
            }
            return theDS;
        }

        protected DataSet latestCD4VL(int patientID, DateTime VisitDate)
        {
            IFollowup CallBackmgr;
            DataSet theDS = new DataSet();
            try
            {
                CallBackmgr = (IFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowup, BusinessProcess.Clinical");
                theDS = CallBackmgr.GetLatestCD4ViralLoad(patientID, VisitDate);
            }
            catch
            {
            }
            finally
            {
                CallBackmgr = null;
            }
            return theDS;
        }
        #endregion
        # region "Amitava"
        // Create Custom Controls 
        // Creation Date : 16-Jan-2007 
        // Amitava Sinha
        //Modified by Jayanta Kr. Das 08-07-2007
        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.ARTFollowup));
                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "ART");
                    //ViewState["CustomFieldsDS"] = theDS;
                    //pnlCustomList.Visible = true;
                }
                //theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "ART");
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


        //Amitava Sinha
        //Generate a string builder for Insert Query Values 
        //and Update Query Values 
        private void GenerateCustomFieldsValues(Control Cntrl)
        {
            string pnlName = Cntrl.ID;
            sbValues = new StringBuilder();
            strmultiselect = string.Empty;
            string strfName = string.Empty;
            Boolean radioflag = false;

            Int32 stpos = 0;
            Int32 enpos = 0;
            if (ViewState["CustomFieldsData"] != null)
            {
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {
                        if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                        {
                            strfName = pnlName.ToUpper() + "TXT";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",[" + strfName + "] = '" + ((TextBox)x).Text.ToString() + "'");
                            }
                            else
                            {
                                sbValues.Append(",[" + strfName + "] = ' " + "'");
                            }
                        }
                        else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                        {
                            strfName = pnlName.ToUpper() + "NUM";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",[" + strfName + "]=" + ((TextBox)x).Text.ToString());
                            }
                            else
                            {
                                sbValues.Append("," + strfName + "=Null");
                            }

                        }
                        else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                        {
                            strfName = pnlName.ToUpper() + "DT";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",[" + strfName + "]='" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
                            }
                            else
                            {
                                sbValues.Append(",[" + strfName + "]=" + "Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                    {
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                        {
                            radioflag = false;
                            strfName = pnlName.ToUpper() + "RADIO1";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",[" + strfName + "]=" + "1");
                                radioflag = true;
                            }
                        }
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                        {
                            strfName = pnlName.ToUpper() + "RADIO2";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",[" + strfName + "]=" + "0");
                                radioflag = true;
                            }
                            if (radioflag == false)
                            {
                                sbValues.Append(",[" + strfName + "]=" + "Null");
                            }
                        }

                    }
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                        {
                            strfName = pnlName.ToUpper() + "SELECTLIST";
                            stpos = strfName.Length;
                            enpos = x.ID.Length - stpos;
                            strfName = x.ID.Substring(stpos, enpos).ToString();
                            if (((DropDownList)x).SelectedValue != "0")
                            {
                                sbValues.Append(",[" + strfName + "] = " + ((DropDownList)x).SelectedValue.ToString() + " ");
                            }
                            else
                            {
                                sbValues.Append(",[" + strfName + "] =  " + "0");
                            }

                        }
                    }

                }
            }

            if (ViewState["CustomFieldsData"] == null)
            {
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {
                        if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "TXT")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",'" + ((TextBox)x).Text.ToString() + "'");
                            }
                            else
                            {
                                sbValues.Append(",' " + "'");
                            }
                        }
                        else if (x.ID.Substring(0, 16).ToString().ToUpper() == pnlName.ToUpper() + "NUM")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append("," + ((TextBox)x).Text.ToString());
                            }
                            else
                            {
                                sbValues.Append(",0");
                            }

                        }
                        else if (x.ID.Substring(0, 15).ToString().ToUpper() == pnlName.ToUpper() + "DT")
                        {
                            if (((TextBox)x).Text != "")
                            {
                                sbValues.Append(",'" + Convert.ToDateTime(((TextBox)x).Text.ToString()) + "'");
                            }
                            else
                            {
                                sbValues.Append(",Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                    {
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO1")
                        {
                            radioflag = false;
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",1");
                                radioflag = true;
                            }
                        }
                        if (x.ID.Substring(0, 19).ToString().ToUpper() == pnlName.ToUpper() + "RADIO2")
                        {
                            if (((HtmlInputRadioButton)x).Checked == true)
                            {
                                sbValues.Append(",0");
                                radioflag = true;
                            }
                            if (radioflag == false)
                            {
                                sbValues.Append(",Null");
                            }
                        }
                    }
                    if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
                    {
                        if (x.ID.Substring(0, 23).ToString().ToUpper() == pnlName.ToUpper() + "SELECTLIST")
                        {
                            if (((DropDownList)x).SelectedValue != "0")
                            {
                                sbValues.Append("," + ((DropDownList)x).SelectedValue.ToString() + " ");
                            }
                            else
                            {
                                sbValues.Append(", " + "0");
                            }

                        }
                    }
                }
            }
            if (ViewState["CustomFieldsMulti"] != null || ViewState["CustomFieldsMulti"] == null)
            {
                foreach (Control x in Cntrl.Controls)
                {
                    if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
                    {

                        if (x.ID.Substring(0, 28).ToString().ToUpper() == pnlName.ToUpper() + "MULTISELECTLIST")
                        {

                            foreach (ListItem li in ((CheckBoxList)x).Items)
                            {
                                if (Convert.ToInt32(li.Selected) == 1)
                                {
                                    strmultiselect += " " + li.Value.ToString() + ",";
                                }
                            }
                            strmultiselect += "^";
                        }
                    }
                }
            }
        }

        //private void Pregnant_LMPCallBack()
        //{

        //    ClientScriptManager LMPPreg = Page.ClientScript;
        //    string strLMPPreg = LMPPreg.GetCallbackEventReference(this, "args", "RecievePregnantData", "'this is context from server'");
        //    string strCallbackLMPPreg = "function CallPregnantLMPServer(args){" + strLMPPreg + ";}";
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallPregnantLMPServer", strCallbackLMPPreg, true);
        //}
        //Amitava Sinha 
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
                dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, visitPK, 0, 0, Convert.ToInt32(ApplicationAccess.ARTFollowup));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "ART");
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
        //Function to Get CD4 and Viral Load DataBased on Visit Date
        private void GetICallBackFunction()
        {
            ClientScriptManager m = Page.ClientScript;
            str = m.GetCallbackEventReference(this, "args", "ReceiveServerData", "'this is context from server'");
            // strHeight = m.GetCallbackEventReference(this, "message", "ReceiveHeight", "'context'");
            strCallback = "function CallServer(args,context){" + str + "; }";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", strCallback, true);
        }
        #region "Function to Handle Height Data Dynamically"
        //private void GetHeight()
        //{

        //    ClientScriptManager Ht = Page.ClientScript;
        //    strHeight = Ht.GetCallbackEventReference(this, "message", "ReceiveServerData", "context");
        //    strHtCallback = "function CallServer(message,context){" + strHeight + ";}";
        //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", strHtCallback, true);
        //}

        #endregion

        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            IFollowup FUManager;
            FUManager = (IFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowup, BusinessProcess.Clinical");
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["HIVPatientStatus"].ToString();
            //(Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Session["HIVPatientStatus"].ToString();
            Maintain_ViewState(chMissedLastWeeknone.Checked, chMissedLastMonthnone.Checked, rdoInterrupted.Checked, rdostopped.Checked, chkpresentingComplaintsNone.Checked, rdoARVSideEffectsYes.Checked, rdoOIsAIDsIllnessYes.Checked, rdoTBScreenPerformed.Checked, Convert.ToInt32(lstclinPlanFU.Value), Convert.ToString(ddlArvTherapyChangeCode.SelectedItem), Convert.ToString(ddlArvTherapyStopCode.SelectedItem));
            //(Master.FindControl("lblformname") as Label).Text = "ART Follow-up Form";
            //(Master.FindControl("lblRoot") as Label).Text = "Clinical Forms >>";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "ART Follow-up";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "ART Follow-up";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "ART Follow-up Form";

            //Amitava Sinha
            //CreateControls();
            PutCustomControl();
            ShowOIAIDS_ReasonMissed();
            Show_Hide();
            Add_Attributes();
            GetICallBackFunction();
            if (!IsPostBack)
            {

                BMIAttributes();
                if (Session["Paperless"].ToString() == "0")
                {
                    pnlPanelNotes.Visible = false;
                }
                ViewState["Pregstatus"] = "0";
                Init_Add_Update_ART_Followup();
                MsgBuilder theBuilder = new MsgBuilder();
                if (Request.QueryString["name"] == "Delete")
                {
                    theBuilder.DataElements["FormName"] = "ART FollowUp";
                    IQCareMsgBox.ShowConfirm("DeleteForm", theBuilder, btnsave);
                }

                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {

                    ViewState["VisitIDIE"] = null;
                    MsgBuilder theDQ = new MsgBuilder();
                    theDQ.DataElements["Name"] = "DQ Checked complete.\\n Form Marked as DQ Checked.\\n Do you want to close";
                    IQCareMsgBox.ShowConfirm("FollowupART", theDQ, theBtnDQ);

                    MsgBuilder theSave = new MsgBuilder();
                    #region "14-Jun-2007 -1"
                    theSave.DataElements["Name"] = "Form saved successfully.Do you want to close";
                    #endregion
                    IQCareMsgBox.ShowConfirm("FollowupART", theSave, btnOk);

                }
        #endregion

                if ((Convert.ToInt32(Session["PatientVisitId"]) > 0) || (Request.QueryString["name"] == ""))
                {
                    ViewState["VisitIDIE"] = null;
                    Load_data();
                    MsgBuilder theDQ = new MsgBuilder();
                    theDQ.DataElements["Name"] = "DQ Checked complete.\\n Form Marked as DQ Checked.\\n Do you want to close";
                    IQCareMsgBox.ShowConfirm("FollowupART", theDQ, theBtnDQ);
                    MsgBuilder theSave = new MsgBuilder();
                    #region "14-Jun-2007 -2"
                    theSave.DataElements["Name"] = "Form updated successfully.Do you want to close";
                    #endregion
                    IQCareMsgBox.ShowConfirm("FollowupART", theSave, btnOk);
                }

            }
            //Amitava Sinha
            //Modified by Jayanta Kr. Das 08-07-2009

        }

        protected void BMIAttributes()
        {
            txtphysHeight.Attributes.Add("OnBlur", "CalcualteBMI('" + txtanotherbmi.ClientID + "','" + txtphysWeight.ClientID + "','" + txtphysHeight.ClientID + "');");
            txtphysWeight.Attributes.Add("OnBlur", "CalcualteBMI('" + txtanotherbmi.ClientID + "','" + txtphysWeight.ClientID + "','" + txtphysHeight.ClientID + "');");
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["TechnicalAreaId"]) != 2)
            {
                btnsave.Enabled = false;
                btndataquality.Enabled = false;
            }
            FillDropDowns();
            //Session["visitPK"] = null;
            //RTyagi..19Feb.07
            /***************** Check For User Rights ****************/
            AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Authentiaction.HasFunctionRight(ApplicationAccess.ARTFollowup, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;

            }
            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                Utility utils = new Utility();
                utils.SetSession();
                if (Authentiaction.HasFunctionRight(ApplicationAccess.ARTFollowup, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                    btndataquality.Enabled = false;
                }
            }

            else if (Request.QueryString["name"] == "Delete")
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.ARTFollowup, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    //int PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                    int PatientID = Convert.ToInt32(Session["PatientId"]);
                    string theUrl = "";
                    //theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmClinical_DeleteForm.aspx", PatientID, Request.QueryString["sts"].ToString());
                    theUrl = string.Format("{0}", "frmClinical_DeleteForm.aspx");
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.ARTFollowup, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Text = "Delete";
                    btnsave.Enabled = false;
                    btndataquality.Visible = false;
                }
            }
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 1)
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.ARTFollowup, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    string theUrl = "";
                    //theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString(), Request.QueryString["sts"].ToString());
                    theUrl = string.Format("{0}", "frmPatient_History.aspx");
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.ARTFollowup, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                    btndataquality.Enabled = false;
                }
                if (Session["HIVPatientStatus"].ToString() == "1")
                {
                    btnsave.Enabled = false;
                    btndataquality.Enabled = false;
                }
                //Privilages for Care End
                if (Session["CareEndFlag"].ToString() == "1")
                {
                    btnsave.Enabled = true;
                    btndataquality.Enabled = true;
                }
            }


        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                FollowUpParameters();
                IFollowup FUManager;
                if (Request.QueryString["name"] == "Delete")
                {
                    //PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                    PatientID = Convert.ToInt32(Session["PatientId"]);
                    DeleteForm();
                }

                //DataSet function for MultiSelect Items
                theDS_ARTFU = DataSet_ARTFollowup();
                FUManager = (IFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowup, BusinessProcess.Clinical");
                DataSet theDSAdd = FUManager.GetARTFollowUPVisitDate(PatientID);

                if (rdoARVSideEffectsNone.Checked == true)
                {
                    ARVSideEffectsNone = 31;
                }
                else if (rdoARVSideEffectsNotDocumented.Checked == true)
                {
                    ARVSideEffectsNotDocumented = 32;
                }

                if (rdoOIsAIDsIllnessNone.Checked == true)
                {
                    OIsAIDsIllnessNone = 99;
                }
                else if (rdoOIsAIDsIllnessNotDocumented.Checked == true)
                {
                    OIsAIDsIllnessNotDocumented = 98;
                }

                if (rdoTBScreenNotDocumented.Checked == true)
                {
                    // TBScreenNotDocumented = 71;

                }
                else if (rdoTBScreenPerformed.Checked == true)
                {
                    //  TBScreenPerformed = 70;
                }

                //this.PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                PatientID = Convert.ToInt32(Session["PatientId"]);
                if (Validate_Save() == false)
                {
                    Show_Hide();
                    return;
                }
                //DataSet to Check IE forms         
                DataSet DSInitialE = new DataSet();
                DSInitialE = FUManager.GetClinicalDate(PatientID, 1);
                #region Naveen Added On 25-Aug-2010

                int LocationID = Convert.ToInt32(Session["AppLocationId"]);

                this.Save = true;

                if (Convert.ToInt32(DSInitialE.Tables[0].Rows[0]["Existflag"]) == 1)
                {
                    DateTime IEVisitDate = Convert.ToDateTime(DSInitialE.Tables[1].Rows[0]["VisitDate"]);
                    IEVisitDate = Convert.ToDateTime(IEVisitDate.ToString(Session["AppDateFormat"].ToString()));
                    if (IEVisitDate.ToString() != "" && txtvisitDate.Value != "")
                    {
                        if (Convert.ToDateTime(txtvisitDate.Value) < IEVisitDate)
                        {
                            IQCareMsgBox.Show("InitialE_Date", this);
                            return;
                        }
                    }
                    else
                    {
                        IQCareMsgBox.Show("InitialE_Exist", this);
                        return;
                    }

                }
                if (Validate_Data_Quality() == false)
                {
                    Show_Hide();
                    return;
                }
                if (Convert.ToInt32(Session["PatientVisitId"]) < 1)
                {
                    this.Update = false;
                    Session["Redirect"] = "0";
                    //Custom Field Insertion
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.ARTFollowup, (DataSet)ViewState["CustomFieldsDS"]);
                    //Saving New Data
                    ViewState["DataQualityFlag"] = "0";
                    FUManager.Save_Update_FollowUP(PatientID, visitPK, LocationID, htFollowupParameters, theDS_ARTFU, Convert.ToInt32(ViewState["VisitIDIE"]), ARVSideEffectsNone, ARVSideEffectsNotDocumented, OIsAIDsIllnessNone, OIsAIDsIllnessNotDocumented, Convert.ToInt32(Session["AppUserId"].ToString()), Save, Update, createDate, Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT);

                    //DataSet theDSAdd1 = FUManager.GetARTFollowUPVisitDate(PatientID);

                    DataSet theDSAdd1 = FUManager.GetPatient_No_Of_VisitDate(PatientID, Convert.ToDateTime(txtvisitDate.Value), 2);
                    Session["PatientVisitId"] = Convert.ToInt32(theDSAdd1.Tables[2].Rows[0]["Visit_Id"].ToString());
                    //ViewState["DataQualityFlag"] = Convert.ToInt32(theDSAdd1.Tables[2].Rows[0]["DataQuality"].ToString());


                }
                else
                {
                    this.Update = true;
                    Save = false;
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.ARTFollowup, (DataSet)ViewState["CustomFieldsDS"]);
                    //Functions for Updating Existing Data
                    ViewState["DataQualityFlag"] = "0";
                    FUManager.Save_Update_FollowUP(PatientID, visitPK, LocationID, htFollowupParameters, theDS_ARTFU, Convert.ToInt32(ViewState["VisitIDIE"]), ARVSideEffectsNone, ARVSideEffectsNotDocumented, OIsAIDsIllnessNone, OIsAIDsIllnessNotDocumented, Convert.ToInt32(Session["AppUserId"].ToString()), Save, Update, createDate, Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT);


                }
                SaveCancel();
                #endregion

            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }


        }
        protected void btnclose_Click(object sender, EventArgs e)
        {
            string theUrl = "";
            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                //string theUrl;
                theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Session["PatientId"].ToString());
                //Response.Redirect(theUrl);
            }
            ////else if (Request.QueryString["name"] == "Edit")
            else if (Convert.ToInt32(Session["PatientVisitId"]) > 1)
            {
                //string theUrl;
                //////theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", Session["patientId"].ToString(), Session["PatientStatus"].ToString());
                theUrl = string.Format("{0}", "frmPatient_History.aspx");
                //Response.Redirect(theUrl);
            }
            else
            {
                theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmClinical_DeleteForm.aspx", Session["patientId"].ToString(), Session["HIVPatientStatus"].ToString());
            }
            Response.Redirect(theUrl);
        }
        protected void btndataquality_Click(object sender, EventArgs e)
        {
            try
            {
                FollowUpParameters();
                //DataSet function for MultiSelect Items
                theDS_ARTFU = DataSet_ARTFollowup();
                this.PatientID = Convert.ToInt32(Session["PatientId"]);
                IFollowup FUManager;
                FUManager = (IFollowup)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BFollowup, BusinessProcess.Clinical");
                DataSet theDSAdd = new DataSet();
                theDSAdd = FUManager.GetARTFollowUPVisitDate(PatientID);
                DataSet DSInitialE = new DataSet();
                DSInitialE = FUManager.GetClinicalDate(PatientID, 1);

                if (rdoARVSideEffectsNone.Checked == true)
                {
                    ARVSideEffectsNone = 31;
                }
                else if (rdoARVSideEffectsNotDocumented.Checked == true)
                {
                    ARVSideEffectsNotDocumented = 32;
                }

                if (rdoOIsAIDsIllnessNone.Checked == true)
                {
                    OIsAIDsIllnessNone = 99;
                }
                else if (rdoOIsAIDsIllnessNotDocumented.Checked == true)
                {
                    OIsAIDsIllnessNotDocumented = 98;
                }

                if (rdoTBScreenNotDocumented.Checked == true)
                {
                    // TBScreenNotDocumented = 71;

                }
                else if (rdoTBScreenPerformed.Checked == true)
                {
                    //  TBScreenPerformed = 70;
                }

                #region Naveen Added On 25-Aug-2010

                int LocationID = Convert.ToInt32(Session["AppLocationId"]);
                string msg = DataQuality_Msg();
                if (msg.Length > 69)
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



                if (Convert.ToInt32(DSInitialE.Tables[0].Rows[0]["Existflag"]) == 1)
                {
                    DateTime IEVisitDate = Convert.ToDateTime(DSInitialE.Tables[1].Rows[0]["VisitDate"]);
                    IEVisitDate = Convert.ToDateTime(IEVisitDate.ToString(Session["AppDateFormat"].ToString()));
                    if (IEVisitDate.ToString() != "" && txtvisitDate.Value != "")
                    {
                        if (Convert.ToDateTime(txtvisitDate.Value) < IEVisitDate)
                        {
                            IQCareMsgBox.Show("InitialE_Date", this);
                            return;
                        }
                    }
                    else
                    {
                        IQCareMsgBox.Show("InitialE_Exist", this);
                        return;
                    }

                }
                //if (Validate_Data_Quality() == false)
                if (Validate_Save() == false)
                {

                    Show_Hide();
                    return;
                }
                if (Convert.ToInt32(Session["PatientVisitId"]) < 1)
                {
                    this.Update = false;
                    Save = true;
                    Session["Redirect"] = "0";
                    //Custom Field Insertion
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.ARTFollowup, (DataSet)ViewState["CustomFieldsDS"]);
                    //Saving New Data
                    FUManager.Save_Update_FollowUP(PatientID, visitPK, LocationID, htFollowupParameters, theDS_ARTFU, Convert.ToInt32(ViewState["VisitIDIE"]), ARVSideEffectsNone, ARVSideEffectsNotDocumented, OIsAIDsIllnessNone, OIsAIDsIllnessNotDocumented, Convert.ToInt32(Session["AppUserId"].ToString()), Save, Update, createDate, Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT);

                    DataSet theDSAdd1 = FUManager.GetPatient_No_Of_VisitDate(PatientID, Convert.ToDateTime(txtvisitDate.Value), 2);
                    Session["PatientVisitId"] = Convert.ToInt32(theDSAdd1.Tables[2].Rows[0]["Visit_Id"].ToString());



                    btndataquality.ControlStyle.BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    this.Update = true;
                    Save = false;
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.ARTFollowup, (DataSet)ViewState["CustomFieldsDS"]);
                    //Functions for Updating Existing Data
                    FUManager.Save_Update_FollowUP(PatientID, visitPK, LocationID, htFollowupParameters, theDS_ARTFU, Convert.ToInt32(ViewState["VisitIDIE"]), ARVSideEffectsNone, ARVSideEffectsNotDocumented, OIsAIDsIllnessNone, OIsAIDsIllnessNotDocumented, Convert.ToInt32(Session["AppUserId"].ToString()), Save, Update, createDate, Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT);
                    //Session["visitPK"] = visitPK;

                    btndataquality.ControlStyle.BackColor = System.Drawing.Color.LightGreen;

                }
                SaveCancel();
                #endregion

            }
            catch (Exception err)
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theMsg, this);
            }

        }
        #region-Added by Naveen 19-Aug-2010
        private void SaveCancel()
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);


            //string strPatientID = ViewState["PtnID"].ToString();
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('ART FollowUp Form saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            //if (Session["Redirect"].ToString() == "0")
            //{
                script += "window.location.href='frmPatient_Home.aspx?PatientId=" + PatientID + "';\n";
            //}
            //else
            //{
            //    script += "window.location.href='frmPatient_History.aspx?PatientId=" + PatientID + "';\n";
            //}
            script += "}\n";

            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        #endregion
        protected void theBtn_Click(object sender, EventArgs e)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            string theUrl = "";
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", PatientID);
            }
            else if ((Convert.ToInt32(Session["PatientVisitId"]) > 1))
            {
                theUrl = string.Format("{0}", "frmPatient_History.aspx");
            }
            else
            {
                DeleteForm();
            }
            Response.Redirect(theUrl);
        }
        protected void theBtnDQ_Click(object sender, EventArgs e)
        {
            int PatientID = Convert.ToInt32(Session["PatientId"]);
            string theUrl;
            //if (Request.QueryString["name"] == "Add")
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", PatientID);
            }
            else
            {
                theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", PatientID, Session["HIVPatientStatus"].ToString());
            }
            Response.Redirect(theUrl);
        }

        #region ICallbackEventHandler Members

        public string GetCallbackResult()
        {
            string thestr = str;
            return thestr;
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            try
            {
                IInitialEval CallBackmgr = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
                DataSet theDS = latestCD4VL(Convert.ToInt32(Session["patientID"]), Convert.ToDateTime(eventArgument.Trim()));
                DataSet theDSHeight = GetHeight(Convert.ToInt32(Session["patientID"]), Convert.ToDateTime(eventArgument.Trim()));
                if (theDS != null && theDS.Tables[0].Rows.Count > 0)
                {
                    str = theDS.GetXml();

                }
                str += "zzzz";
                if (theDSHeight != null && theDSHeight.Tables[0].Rows.Count > 0)
                {
                    str += theDSHeight.GetXml();
                }

                str += "zzzz";
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    theDS.Clear();
                    theDS = CallBackmgr.GetPregnantStatus(Convert.ToInt32(Session["PatientId"]), eventArgument);
                    str += theDS.GetXml();
                }
                else
                {
                    theDS.Clear();
                    str += theDS.GetXml();
                }

            }
            catch
            {
            }
            finally
            {
                // CallBackmgr = null;
            }
        }

        #endregion

    }
}