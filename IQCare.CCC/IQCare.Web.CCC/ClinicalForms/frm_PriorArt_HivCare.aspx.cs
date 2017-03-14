
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
using Interface.Laboratory;
namespace IQCare.Web.Clinical
{
    public partial class PriorArtHivCare : BasePage
    {
        string ObjFactoryParameter = "BusinessProcess.Clinical.BCustomForm, BusinessProcess.Clinical";
        AuthenticationManager Authentiaction = new AuthenticationManager();
        int intflag=0, visitPK;//
        int PId;
        DateTime CreateDate;
        // Visit ID =16 in Mst_VIsitType
        int visitid = 16, chkPepData, chkPmtData, chkArvData, chkHivCarePresumptiveDiagnosisData, chkHivCarePcrInfantData;
        int PriorArt = 9;
        public DataTable theCurrentRegDT;
        public DataTable theRegimen1;
        public DataTable theRegimen2;
        public DataTable thedtDynamicDrugMedical;
        StringBuilder sbValues;
        int PatID;
        string strmultiselect= "", TableName = "";
        int icount;
        ArrayList arl = new ArrayList();
        StringBuilder sbParameter = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {

            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Prior ART/HIV Care";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Prior ART/HIV Care";
            if (Authentiaction.HasFunctionRight(ApplicationAccess.PriorARTHIVCare, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;
            }
            PutCustomControl();
            if (!IsPostBack)
            {
                ViewState["BtnCompClicked"] = "0";
                ViewState["Pregstatus"] = "0";
                Session["PtnRegCTC"] = "";
                Session["SaveFlag"] = "Add";
                Session["SelectedId"] = "";
                Session["SelectedRow"] = -1;
                Session["RemoveFlag"] = "False";
                chkPEP.Disabled = true;
                chkPMTCTOnly.Disabled = true;
                chkEarlierArvNotTransfer.Disabled = true;
                Init_Add_UpdateInitial_Evaluation();
                Add_attributes();
            }
            else
            {
                chkPEP.Disabled = false;
                chkPMTCTOnly.Disabled = false;
                chkEarlierArvNotTransfer.Disabled = false;
                if (chkPEP.Checked)
                {
                    txtPEPWhere.Enabled = true;
                    btnRegimen.Enabled = true;
                    txtPEPARVs.Disabled = false;
                    txtPEPStartDate.Disabled = false;
                }
                if (chkPMTCTOnly.Checked)
                {
                    txtPMTCTWhere.Enabled = true;
                    btnRegimen1.Enabled = true;
                    txtPMTCTARVs.Disabled = false;
                    txtPMTCTStartDate.Disabled = false;
                }
                if (chkEarlierArvNotTransfer.Checked)
                {
                    txtEarlierArvWhere.Enabled = true;
                    btnRegimen2.Enabled = true;
                    txtEarlierArvNotTransferArv.Disabled = false;
                    txtEarlierArvStartDate.Disabled = false;
                }
                ViewState["MasterData"] = ((DataSet)ViewState["OldData"]).Tables[0];

                if ((DataTable)Application["AddRegimen"] != null)
                {
                    ViewState["ARVMasterData"] = (DataTable)Application["MasterData"];
                    Application.Remove("MasterData");
                    DataTable theDT = (DataTable)Application["AddRegimen"];
                    ViewState["SelectedData"] = theDT;
                    string theStr = FillRegimen(theDT);
                    txtPEPARVs.Value = theStr;
                    Application.Remove("AddRegimen");
                    txtPEPWhere.Enabled = true;
                    btnRegimen.Enabled = true;
                    txtPEPARVs.Disabled = false;
                    txtPEPStartDate.Disabled = false;
                }
                else if ((DataTable)Application["AddRegimen1"] != null)
                {
                    ViewState["ARVMasterData1"] = (DataTable)Application["MasterData"];
                    Application.Remove("MasterData");
                    DataTable theDT = (DataTable)Application["AddRegimen1"];
                    ViewState["SelectedData1"] = theDT;
                    string theStr = FillRegimen(theDT);
                    txtPMTCTARVs.Value = theStr;
                    Application.Remove("AddRegimen1");
                    txtPMTCTWhere.Enabled = true;
                    btnRegimen1.Enabled = true;
                    txtPMTCTARVs.Disabled = false;
                    txtPMTCTStartDate.Disabled = false;
                }
                else if ((DataTable)Application["AddRegimen2"] != null)
                {
                    ViewState["ARVMasterData2"] = (DataTable)Application["MasterData"];
                    Application.Remove("MasterData");
                    DataTable theDT = (DataTable)Application["AddRegimen2"];
                    ViewState["SelectedData2"] = theDT;
                    string theStr = FillRegimen(theDT);
                    txtEarlierArvNotTransferArv.Value = theStr;
                    Application.Remove("AddRegimen2");
                    txtEarlierArvWhere.Enabled = true;
                    btnRegimen2.Enabled = true;
                    txtEarlierArvNotTransferArv.Disabled = false;
                    txtEarlierArvStartDate.Disabled = false;
                }

                //if (ViewState["BtnCompClicked"].ToString() == "1")
                //    //DataQuality_Msg();
                if (Session["lblpntstatus"].ToString() == "1")
                {
                    btnAddAllergy.Enabled = false;
                    btnsave.Enabled = false;
                }
                else
                {
                    btnAddAllergy.Enabled = true;
                }
            }
            Int32 ageInYear = (int)Convert.ToDouble(Session["PatientAge"].ToString());
            Int32 ageInMonth = Convert.ToInt32(Session["PatientAge"].ToString().Substring(Session["PatientAge"].ToString().IndexOf(".") + 1));
            if (ageInMonth <= 18)
            {
                ddlHivTestType.Enabled = true;
            }
            else
            {
                ddlHivTestType.Enabled = false;
            }
            Form.EnableViewState = true;
        }

        private void Add_attributes()
        {



            txtPEPStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtPEPStartDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");

            txtPMTCTStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtPMTCTStartDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");

            txtEarlierArvStartDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtEarlierArvStartDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");

            txtHIVConfirmHIVPosDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtHIVConfirmHIVPosDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");

            txtHIVEligibleDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtHIVEligibleDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");

            txtHIVReadyDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtHIVReadyDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");

            txtDateAllergy.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtDateAllergy.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3')");


        }

        //private string DataQuality_Msg()
        //{
        //    string strmsg = "Following values are required to complete the data quality check:\\n\\n";
        //    //Visit Date
        //    if (txtvisitDate.Text.Trim() == "")
        //    {
        //        string scriptVisitDate = "<script language = 'javascript' defer ='defer' id = 'ColorVisitDate'>\n";
        //        scriptVisitDate += "To_Change_Color('Vdate');\n";
        //        scriptVisitDate += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"ColorVisitDate", scriptVisitDate);
        //        MsgBuilder theBuilder = new MsgBuilder();
        //        theBuilder.DataElements["Control"] = "-Visit Date";
        //        strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
        //        strmsg = strmsg + "\\n";
        //    }
        //    //Pregnant Part 
        //    if (rdopregnantYes.Checked == false && rdopregnantNo.Checked == false && Convert.ToInt32(ViewState["Pregstatus"]) == 1)
        //    {
        //        string scriptpregnant = "<script language = 'javascript' defer ='defer' id = 'pregnant_ID'>\n";
        //        scriptpregnant += "To_Change_Color('lblpregnanttmp');\n";
        //        scriptpregnant += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"pregnant_ID", scriptpregnant);
        //        MsgBuilder theBuilder = new MsgBuilder();
        //        theBuilder.DataElements["Control"] = "-Pregnant";
        //        strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
        //        strmsg = strmsg + "\\n";
        //    }

        //    //Previous ARV Exposure
        //    if (rdopreviousARV.Checked == true)
        //    {
        //        if ((txtcurrentART.Value.Trim() == "") && (chkprevSDNVPNVP.Checked == false) && (chkprevARVRegimen.Checked == false))
        //        {
        //            string scriptCurrentART = "<script language = 'javascript' defer ='defer' id = 'CurrentART_ID'>\n";
        //            scriptCurrentART += "To_Change_Color('Cuart');\n";
        //            scriptCurrentART += "To_Change_Color('lblNVP');\n";
        //            scriptCurrentART += "To_Change_Color('lblprevARVRegimen');\n";
        //            scriptCurrentART += "</script>\n";
        //            ClientScript.RegisterStartupScript(this.GetType(),"CurrentART_ID", scriptCurrentART);
        //            MsgBuilder theBuilder = new MsgBuilder();
        //            theBuilder.DataElements["Control"] = "-Select either Current ART or Single Dose NVP or Previous Regimens";
        //            strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
        //            strmsg = strmsg + "\\n";
        //        }

        //        //Drug Allergies
        //        if (rdoAllergynone.Checked == false && rdoAllergynotdocumented.Checked == false && chksulfaAllergy.Checked == false && chkotherAllergy.Checked == false)
        //        {

        //            string scriptDrugAllery = "<script language = 'javascript' defer ='defer' id = 'DrugAllery_ID'>\n";
        //            scriptDrugAllery += "To_Change_Color('lblAllergyNone');\n";
        //            scriptDrugAllery += "To_Change_Color('lblAllergyNotDocumented');\n";
        //            scriptDrugAllery += "To_Change_Color('lblAllergySulfa');\n";
        //            scriptDrugAllery += "To_Change_Color('lblAllergyOther');\n";
        //            scriptDrugAllery += "</script>\n";
        //            ClientScript.RegisterStartupScript(this.GetType(),"DrugAllery_ID", scriptDrugAllery);
        //            MsgBuilder theBuilder = new MsgBuilder();
        //            theBuilder.DataElements["Control"] = "-Please Select either";
        //            strmsg += IQCareMsgBox.GetMessage("DrugAllergy_SulfaDrug", theBuilder, this);
        //            strmsg = strmsg + "\\n";

        //        }
        //        //HIV Associated Conditions
        //        if (rdoHIVassocNone.Checked == false && rdoPrevHIVassocNotDocumented.Checked == false && rdoHIVassociate.Checked == false)
        //        {
        //            string scriptHIVassoc = "<script language = 'javascript' defer ='defer' id = 'HIVassoc_ID'>\n";
        //            scriptHIVassoc += "To_Change_Color('lblHIVassocNone');\n";
        //            scriptHIVassoc += "To_Change_Color('lblHIVassocNotdocumented');\n";
        //            scriptHIVassoc += "To_Change_Color('lblHIVassociate');\n";
        //            scriptHIVassoc += "</script>\n";
        //            ClientScript.RegisterStartupScript(this.GetType(),"HIVassoc_ID", scriptHIVassoc);
        //            MsgBuilder theBuilder = new MsgBuilder();
        //            theBuilder.DataElements["Control"] = "-HIV Associated Conditions radio buttons ";
        //            strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
        //            strmsg = strmsg + "\\n";
        //        }
        //    }
        //    //ARV Exposure 
        //    if (rdoprevARVExposureNone.Checked == false && rdoprevARVExpnotdocumented.Checked == false && rdopreviousARV.Checked == false)
        //    {
        //        string scriptARVExposure = "<script language = 'javascript' defer ='defer' id = 'ARVExposure_ID'>\n";
        //        scriptARVExposure += "To_Change_Color('lblprevARVExposureNone');\n";
        //        scriptARVExposure += "To_Change_Color('lblprevARVExposurenotdocumented');\n";
        //        scriptARVExposure += "To_Change_Color('lblpreviousARV');\n";
        //        scriptARVExposure += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"ARVExposure_ID", scriptARVExposure);
        //        MsgBuilder theBuilder = new MsgBuilder();
        //        theBuilder.DataElements["Control"] = "-ARV Exposure";
        //        strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
        //        strmsg = strmsg + "\\n";
        //    }
        //    //WAB Stage
        //    if (ddlphysWABStage.SelectedValue == "0")
        //    {
        //        string scriptWAB = "<script language = 'javascript' defer ='defer' id = 'WAB_ID'>\n";
        //        scriptWAB += "To_Change_Color('lblWAB');\n";
        //        scriptWAB += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"WAB_ID", scriptWAB);
        //        MsgBuilder theBuilder = new MsgBuilder();
        //        theBuilder.DataElements["Control"] = "-WAB Stage";
        //        strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
        //        strmsg = strmsg + "\\n";
        //    }

        //    //WHO Stage
        //    if (ddlWHOStage.SelectedValue == "0")
        //    {
        //        string scriptWHO = "<script language = 'javascript' defer ='defer' id = 'WHO_ID'>\n";
        //        scriptWHO += "To_Change_Color('lblWHO');\n";
        //        scriptWHO += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"WHO_ID", scriptWHO);
        //        MsgBuilder theBuilder = new MsgBuilder();
        //        theBuilder.DataElements["Control"] = "-WHO Stage";
        //        strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
        //        strmsg = strmsg + "\\n";
        //    }

        //    //Height
        //    if (txtphysHeight.Value.Trim() == "")
        //    {
        //        string scriptHT = "<script language = 'javascript' defer ='defer' id = 'HT_ID'>\n";
        //        scriptHT += "To_Change_Color('lblHT');\n";
        //        scriptHT += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"HT_ID", scriptHT);
        //        MsgBuilder theBuilder = new MsgBuilder();
        //        theBuilder.DataElements["Control"] = "-Height";
        //        strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
        //        strmsg = strmsg + "\\n";

        //    }
        //    //Weight

        //    if (txtphysWeight.Value.Trim() == "")
        //    {
        //        string scriptWT = "<script language = 'javascript' defer ='defer' id = 'WT_ID'>\n";
        //        scriptWT += "To_Change_Color('lblWT');\n";
        //        scriptWT += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"WT_ID", scriptWT);
        //        MsgBuilder theBuilder = new MsgBuilder();
        //        theBuilder.DataElements["Control"] = "-Weight";
        //        strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
        //        strmsg = strmsg + "\\n";
        //    }

        //    //Assessment
        //    if (rdoclinAssessment_Plan_RegimenNone.Checked == false && chkclinAssessmentInitial1.Checked == false && chkclinAssessmentInitial2.Checked == false)
        //    {
        //        string scriptAsessment = "<script language = 'javascript' defer ='defer' id = 'Asessment_ID'>\n";
        //        scriptAsessment += "To_Change_Color('lblAssessNone');\n";
        //        scriptAsessment += "To_Change_Color('lblHIVrelated');\n";
        //        scriptAsessment += "To_Change_Color('lblHIVrelatedNon');\n";
        //        scriptAsessment += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"Asessment_ID", scriptAsessment);
        //        MsgBuilder theBuilder = new MsgBuilder();
        //        theBuilder.DataElements["Control"] = "-Assessment";
        //        strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
        //        // strmsg = strmsg + "\\n";
        //    }

        //    ////if(ddinterviewer.SelectedValue == "0")
        //    //// {
        //    ////     string scriptSign = "<script language = 'javascript' defer ='defer' id = 'IE_signature'>\n";
        //    ////     scriptSign += "To_Change_Color('lblsignature');\n";
        //    ////     scriptSign += "</script>\n";
        //    ////     ClientScript.RegisterStartupScript(this.GetType(),"IE_signature", scriptSign);
        //    ////     MsgBuilder theBuilder = new MsgBuilder();
        //    ////     theBuilder.DataElements["Control"] = "-Signature";
        //    ////     strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);

        //    //// }

        //    return strmsg;
        //}

        private string FillRegimen(DataTable theDT)
        {
            string theRegimen = "";
            DataView theDV = new DataView();
            if (theDT.Rows.Count != 0)
            {
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    if (theDT.Rows[i]["Generic"].ToString() != "0")
                    {
                        theDV = new DataView(((DataSet)ViewState["OldData"]).Tables[2]);
                        theDV.RowFilter = "Generic = " + theDT.Rows[i]["Generic"];
                        if (theDV.Count > 0)
                        {
                            for (int j = 0; j < theDV.Count; j++)
                            {
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[j]["Abbr"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[j]["Abbr"].ToString();
                                }
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                    else
                    {
                        theDV = new DataView(((DataSet)ViewState["OldData"]).Tables[1]);
                        theDV.RowFilter = "DrugId = " + theDT.Rows[i]["DrugId"]; //+ " and DrugTypeID = 37";
                        if (theDV.Count > 0)
                        {
                            for (int j = 0; j < theDV.Count; j++)
                            {
                                if (theRegimen == "")
                                {
                                    theRegimen = theDV[j]["Abbr"].ToString();
                                }
                                else
                                {
                                    theRegimen = theRegimen + "/" + theDV[j]["Abbr"].ToString();
                                }
                            }
                        }
                        theRegimen = theRegimen.Trim();
                    }
                }
            }
            return theRegimen;
        }
        private void Pregnant_LMPCallBack()
        {
            ClientScriptManager LMPPreg = Page.ClientScript;
            string strLMPPreg = LMPPreg.GetCallbackEventReference(this, "args", "RecievePregnantData", "'this is context from server'");
            string strCallbackLMPPreg = "function CallPregnantLMPServer(args){" + strLMPPreg + ";}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallPregnantLMPServer", strCallbackLMPPreg, true);
        }
        //private void Show_Hide()
        //{
        //    if ((String)ViewState["DisclosureYes"] == "1")
        //    {
        //        string scriptdisclosure = "<script language = 'javascript' defer ='defer' id = 'ondisclosure'>\n";
        //        scriptdisclosure += "show('showdisclosureName'); \n";
        //        scriptdisclosure += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"ondisclosure", scriptdisclosure);


        //        if (Convert.ToString(ViewState["DisclosureOther"]) == "1")
        //        {
        //            string scriptdisclosureother = "<script language = 'javascript' defer ='defer' id = 'ondisclosureother'>\n";
        //            scriptdisclosureother += "show('otherdisclosureName'); \n";
        //            scriptdisclosureother += "</script>\n";
        //            ClientScript.RegisterStartupScript(this.GetType(),"ondisclosureother", scriptdisclosureother);

        //        }
        //    }

        //    if (ViewState["presentingComplaintsNone"] == "1")
        //    {
        //        string scriptpresentingComplaints = "<script language = 'javascript' defer ='defer' id = 'onpresentingComplaints'>\n";
        //        scriptpresentingComplaints += "display_chklist('" + chkpresentingComplaintsNone.ClientID + "', '" + presentingComplaintsShow.ClientID + "');\n";
        //        scriptpresentingComplaints += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"onpresentingComplaints", scriptpresentingComplaints);

        //    }
        //    if (ViewState["otherAllergy"] == "1")
        //    {
        //        string scriptotherAllergy = "<script language = 'javascript' defer ='defer' id = 'onotherAllergy'>\n";
        //        scriptotherAllergy += "show('otherAllergyName'); \n";
        //        scriptotherAllergy += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"onotherAllergy", scriptotherAllergy);
        //    }

        //    if (ViewState["longTermMedsSulfa"] == "1")
        //    {
        //        string scriptlongTermMedsSulfa = "<script language = 'javascript' defer ='defer' id = 'onlongTermMedsSulfa'>\n";
        //        scriptlongTermMedsSulfa += "show('longTermMedsSulfaSelected'); \n";
        //        scriptlongTermMedsSulfa += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"onlongTermMedsSulfa", scriptlongTermMedsSulfa);
        //    }

        //    if (ViewState["longTermTBMed"] == "1")
        //    {
        //        string scriptlongTermMedsTB = "<script language = 'javascript' defer ='defer' id = 'onlongTermMedsTB'>\n";
        //        scriptlongTermMedsTB += "show('longTermMedsTBSelected'); \n";
        //        scriptlongTermMedsTB += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"onlongTermMedsTB", scriptlongTermMedsTB);

        //    }

        //    if (ViewState["longTermMedsOther1"] == "1")
        //    {
        //        string scriptlongTermMedsOther1 = "<script language = 'javascript' defer ='defer' id = 'onlongTermMedsOther1'>\n";
        //        scriptlongTermMedsOther1 += "show('longTermMedsOther1Selected'); \n";
        //        scriptlongTermMedsOther1 += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"onlongTermMedsOther1", scriptlongTermMedsOther1);

        //    }

        //    if (ViewState["longTermMedsOther2"] == "1")
        //    {
        //        string scriptlongTermMedsOther2 = "<script language = 'javascript' defer ='defer' id = 'onlongTermMedsOther2'>\n";
        //        scriptlongTermMedsOther2 += "show('longTermMedsOther2Selected'); \n";
        //        scriptlongTermMedsOther2 += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"onlongTermMedsOther2", scriptlongTermMedsOther2);


        //    }
        //    if (ViewState["previousARV"] == "1")
        //    {
        //        string scriptpreviousARV = "<script language = 'javascript' defer ='defer' id = 'onpreviousARV'>\n";
        //        scriptpreviousARV += "show('prevexpdiv'); \n";
        //        scriptpreviousARV += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"onpreviousARV", scriptpreviousARV);

        //        if (ViewState["prevSingleDoseNVP"] == "1")
        //        {
        //            string scriptprevSingleDoseNVP = "<script language = 'javascript' defer ='defer' id = 'onprevSingleDoseNVP'>\n";
        //            scriptprevSingleDoseNVP += "show('prevSingleDoseNVPSelected'); \n";
        //            scriptprevSingleDoseNVP += "</script>\n";
        //            ClientScript.RegisterStartupScript(this.GetType(),"onprevSingleDoseNVP", scriptprevSingleDoseNVP);
        //        }
        //        if (ViewState["prevARVRegimen"] == "1")
        //        {
        //            string scriptprevARVReg = "<script language = 'javascript' defer ='defer' id = 'onprevARVReg'>\n";
        //            scriptprevARVReg += "show('prevARVReg'); \n";
        //            scriptprevARVReg += "</script>\n";
        //            ClientScript.RegisterStartupScript(this.GetType(),"onscriptprevARVReg", scriptprevARVReg);

        //        }
        //    }

        //    if (ViewState["HIVassociate"] == "1")
        //    {
        //        string scriptHIVassociate = "<script language = 'javascript' defer ='defer' id = 'onHIVassociate'>\n";
        //        scriptHIVassociate += "show('assocSelected'); \n";
        //        scriptHIVassociate += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"onHIVassociate", scriptHIVassociate);
        //    }

        //    if (ViewState["ChangeRegimen"] == "1")
        //    {
        //        string scriptChangeRegimen = "<script language = 'javascript' defer ='defer' id = 'onChangeRegimen'>\n";
        //        scriptChangeRegimen += "show('arvTherapyChange'); \n";
        //        scriptChangeRegimen += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"onChangeRegimen", scriptChangeRegimen);

        //        if (ViewState["changeROther"] == "1")
        //        {
        //            string scriptchangeROther = "<script language = 'javascript' defer ='defer' id = 'onchangeROther'>\n";
        //            scriptchangeROther += "show('otherarvTherapyChangeCode'); \n";
        //            scriptchangeROther += "</script>\n";
        //            ClientScript.RegisterStartupScript(this.GetType(),"onchangeROther", scriptchangeROther);

        //        }

        //    }
        //    if (ViewState["StopRegimen"] == "1")
        //    {
        //        string scriptStopRegimen = "<script language = 'javascript' defer ='defer' id = 'onstopRegimen'>\n";
        //        scriptStopRegimen += "show('arvTherapyStop'); \n";
        //        scriptStopRegimen += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"onstopRegimen", scriptStopRegimen);

        //        if (ViewState["stopROther"] == "1")
        //        {
        //            string scriptstopROther = "<script language = 'javascript' defer ='defer' id = 'onstopROther'>\n";
        //            scriptstopROther += "show('otherarvTherapyStopCode'); \n";
        //            scriptstopROther += "</script>\n";
        //            ClientScript.RegisterStartupScript(this.GetType(),"onstopROther", scriptstopROther);
        //        }
        //    }

        //    if (rdoPerformed.Checked == true)
        //    {
        //        string script = "<script language = 'javascript' defer ='defer' id = 'TBSystem'>\n";
        //        script += "show('divTBPerformed');\n";
        //        script += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"TBSystem", script);

        //    }

        //    if (Convert.ToInt32(ViewState["Pregstatus"]) == 1)
        //    {
        //        string script = "";
        //        script = "<script language = 'javascript' defer ='defer' id = 'Pregnant_1'>\n";
        //        script += "show('rdopregnantyesno');\n";
        //        script += "hide('spdelivery');\n";
        //        script += "show('spanEDD');\n";
        //        script += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"Pregnant_1", script);
        //    }

        //    if (Convert.ToInt32(ViewState["Pregstatus"]) == 2)
        //    {
        //        string script = "";
        //        script = "<script language = 'javascript' defer ='defer' id = 'Pregnant_2'>\n";
        //        script += "show('rdopregnantyesno');\n";
        //        script += "hide('spdelivery');\n";
        //        script += "hide('spanEDD');\n";
        //        script += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"Pregnant_2", script);
        //    }

        //    if (Convert.ToInt32(ViewState["Pregstatus"]) == 3)
        //    {
        //        string script = "";
        //        script = "<script language = 'javascript' defer ='defer' id = 'Pregnant_3'>\n";
        //        script += "show('spdelivery');\n";
        //        script += "hide('rdopregnantyesno');\n";
        //        script += "show('spanEDD');\n";
        //        script += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"Pregnant_3", script);
        //    }
        //    if (Convert.ToInt32(ViewState["Pregstatus"]) == 4)
        //    {

        //        string script = "";
        //        script = "<script language = 'javascript' defer ='defer' id = 'Pregnant_4'>\n";
        //        script += "show('spdelivery');\n";
        //        script += "show('spanDelDate');\n";
        //        script += "hide('rdopregnantyesno');\n";
        //        script += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"Pregnant_4", script);
        //    }

        //    if (rdopregnantNo.Checked == true)
        //    {
        //        string script = "<script language = 'javascript' defer ='defer' id = 'rdoPregnantNo'>\n";
        //        script += "hide('spanEDD');\n";
        //        script += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"rdoPregnantNo", script);
        //    }

        //    if (rdopregnantYes.Checked == true)
        //    {
        //        string script = "<script language = 'javascript' defer ='defer' id = 'rdoPregnantYes'>\n";
        //        script += "show('spanEDD');\n";
        //        script += "</script>\n";
        //        ClientScript.RegisterStartupScript(this.GetType(),"rdoPregnantYes", script);
        //    }



        //    if (Convert.ToInt32(Session["PatientVisitId"]) == 0 && txtvisitDate.Text != "")
        //    {
        //        IInitialEval CallBackmgr;
        //        try
        //        {
        //            DataSet theDS = new DataSet();
        //            CallBackmgr = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
        //            theDS = CallBackmgr.GetPregnantStatus(Convert.ToInt32(Session["PatientId"]), txtvisitDate.Text);
        //            if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Pregnant"]) == 1)
        //            {
        //                string script = "<script language = 'javascript' defer ='defer' id = 'Delivery_0'>\n";
        //                script += "show('spdelivery')\n";
        //                script += "hide('rdopregnantyesno')\n";
        //                script += "</script>\n";
        //                ClientScript.RegisterStartupScript(this.GetType(),"Delivery_0", script);

        //                if (rdoDeliveredYes.Checked == true)
        //                {
        //                    script = "";
        //                    script = "<script language = 'javascript' defer ='defer' id = 'Delivery_1'>\n";
        //                    script += "show('spanDelDate')\n";
        //                    script += "</script>\n";
        //                    ClientScript.RegisterStartupScript(this.GetType(),"Delivery_1", script);
        //                }
        //                if (rdoDeliveredNo.Checked == true)
        //                {
        //                    txtEDDDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["EDD"]);
        //                    script = "";
        //                    script = "<script language = 'javascript' defer ='defer' id = 'Delivery_2'>\n";
        //                    script += "hide('spanDelDate')\n";
        //                    script += "</script>\n";
        //                    ClientScript.RegisterStartupScript(this.GetType(),"Delivery_2", script);
        //                }
        //            }

        //        }
        //        catch
        //        {
        //        }
        //        finally
        //        {
        //            CallBackmgr = null;
        //        }

        //    }
        //}
        private Boolean FieldValidation()
        {
            if (txtDrugAllergies.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Drug Allergies";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtDrugAllergies.Focus();
                return false;
            }
            if (txtTypeOfReaction.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Type of Reaction";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtTypeOfReaction.Focus();
                return false;
            }

            return true;
        }
        private void SaveCancel()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Prior Art/HIV Care Form saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            if (Session["Redirect"].ToString() == "0")
            {
                script += "window.location.href='frmPatient_Home.aspx';\n";
            }
            else
            {
                script += "window.location.href='frmPatient_History.aspx?sts=" + 0 + "';\n";
            }
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href='frm_PriorArt_HivCare.aspx';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }
        protected void Init_Add_UpdateInitial_Evaluation()
        {
            IPriorArtHivCare IEManager;
            try
            {
                PId = Convert.ToInt32(Session["PatientId"]);
                IEManager = (IPriorArtHivCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPriorArtHivCare, BusinessProcess.Clinical");
                DataSet theDS = IEManager.GetPatientPriorArtHIVCare(PId);
                ViewState["MasterData"] = theDS.Tables[9];
                ViewState["ARVMasterData"] = theDS.Tables[16];
                ViewState["ARVMasterData1"] = theDS.Tables[16];
                ViewState["ARVMasterData2"] = theDS.Tables[16];
                DataSet theOldDS = new DataSet();
                theOldDS.Tables.Add(theDS.Tables[9].Copy());
                theOldDS.Tables.Add(theDS.Tables[10].Copy());
                theOldDS.Tables.Add(theDS.Tables[16].Copy());
                ViewState["OldData"] = theOldDS;
                ViewState["SelectedData"] = theCurrentRegDT;     //CreateSelectedTable();
                ViewState["SelectedData1"] = theRegimen1;
                ViewState["SelectedData2"] = theRegimen2;
                //For Editing Records
                Session["DrugAndRecentMedicalDataTable"] = null;
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    //from Patient History Page
                    PId = Convert.ToInt32(Session["PatientId"]);
                    visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                    int locationID = Convert.ToInt32(Session["ServiceLocationId"]);
                    ViewState["DataQuality"] = "0";
                    DataView theDV = new DataView(theDS.Tables[9]);
                    IQCareUtils theUtil = new IQCareUtils();
                    DataSet theDS1 = IEManager.GetPriorArtHivCareUpdate(visitPK, PId, locationID);
                    string visitDate = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[0].Rows[0]["VisitDate"]);
                    ViewState["VisitDate"] = visitDate;
                    ViewState["createdate"] = Convert.ToDateTime(theDS1.Tables[0].Rows[0]["CreateDate"].ToString());
                    string pepChk = "0", PmtctChk = "0", chkEarlier = "0";

                    //if (theDS1.Tables[0].Rows.Count > 0)
                    //{
                    //    txtVisitFirstEncounterDate.Value  = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[0].Rows[0]["VisitDate"]);
                    //    hdnVisitData.Value = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[0].Rows[0]["VisitDate"]);
                    //    Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "DataEncounter()", true);
                    //}


                    if (theDS1.Tables[0].Rows[0]["DataQuality"] != System.DBNull.Value && Convert.ToInt32(theDS1.Tables[0].Rows[0]["DataQuality"]) == 1)
                    {
                        btncomplete.CssClass = "greenbutton";
                    }
                    if (theDS1.Tables[1].Rows.Count > 0)
                    {
                        if (theDS1.Tables[1].Rows[0]["PriorArt"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["PriorArt"].ToString()) == 1)
                            {
                                this.rdoDisclosureYes.Checked = true;
                                chkPEP.Disabled = false;
                                chkPMTCTOnly.Disabled = false;
                                chkEarlierArvNotTransfer.Disabled = false;
                            }
                            else if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["PriorArt"].ToString()) == 0)
                            {
                                this.rdoDisclosureNo.Checked = true;
                            }
                        }
                        if (theDS1.Tables[1].Rows[0]["PEP"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["PEP"].ToString()) == 1)
                            {
                                this.chkPEP.Checked = true;
                                txtPEPWhere.Enabled = true;
                                btnRegimen.Enabled = true;
                                txtPEPStartDate.Disabled = false;
                                txtPEPARVs.Disabled = false;
                                pepChk = "1";
                            }
                        }
                        if (theDS1.Tables[1].Rows[0]["PEPWhere"] != System.DBNull.Value)
                        {
                            this.txtPEPWhere.Text = theDS1.Tables[1].Rows[0]["PEPWhere"].ToString();
                        }
                        if (theDS1.Tables[1].Rows[0]["PMTCTOnly"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["PMTCTOnly"].ToString()) == 1)
                            {
                                this.chkPMTCTOnly.Checked = true;
                                txtPMTCTWhere.Enabled = true;
                                btnRegimen1.Enabled = true;
                                txtPMTCTStartDate.Disabled = false;
                                txtPMTCTARVs.Disabled = false;
                                PmtctChk = "1";
                            }
                        }
                        if (theDS1.Tables[1].Rows[0]["PMTCTWhere"] != System.DBNull.Value)
                        {
                            this.txtPMTCTWhere.Text = theDS1.Tables[1].Rows[0]["PMTCTWhere"].ToString();
                        }
                        if (theDS1.Tables[1].Rows[0]["EarlierArvNotTransfer"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["EarlierArvNotTransfer"].ToString()) == 1)
                            {
                                this.chkEarlierArvNotTransfer.Checked = true;
                                txtEarlierArvWhere.Enabled = true;
                                btnRegimen2.Enabled = true;
                                txtEarlierArvNotTransferArv.Disabled = false;
                                txtEarlierArvStartDate.Disabled = false;
                                chkEarlier = "1";
                            }
                        }
                        if (theDS1.Tables[1].Rows[0]["EarlierArvStartDate"] != System.DBNull.Value)
                        {
                            string dt = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[1].Rows[0]["EarlierArvStartDate"]);
                            if (dt != "01-Jan-1900")
                            {
                                this.txtEarlierArvStartDate.Value = dt;
                            }
                            else
                            {
                                this.txtEarlierArvStartDate.Value = "";
                            }
                        }
                        if (theDS1.Tables[1].Rows[0]["EarlierArvWhere"] != System.DBNull.Value)
                        {
                            this.txtEarlierArvWhere.Text = theDS1.Tables[1].Rows[0]["EarlierArvWhere"].ToString();
                        }
                        if (theDS1.Tables[1].Rows[0]["EarlierArvNotTransferArv"] != System.DBNull.Value)
                        {
                            this.txtEarlierArvNotTransferArv.Value = theDS1.Tables[1].Rows[0]["EarlierArvNotTransferArv"].ToString();
                            string[] theStrRegimen = txtEarlierArvNotTransferArv.Value.Split(new Char[] { '/' });
                            DataView theARVDV = new DataView(theDS.Tables[16]);
                            if (txtEarlierArvNotTransferArv.Value != "")
                            {
                                ViewState["SelectedData2"] = OldRegimenList(theStrRegimen, theARVDV);
                            }
                        }
                        if (theDS1.Tables[1].Rows[0]["HivTestType"] != System.DBNull.Value)
                        {
                            this.ddlHivTestType.SelectedValue = theDS1.Tables[1].Rows[0]["HivTestType"].ToString();
                        }
                        if (theDS1.Tables[1].Rows[0]["HIVCareWhere"] != System.DBNull.Value)
                        {
                            this.txtHIVCareWhere.Text = theDS1.Tables[1].Rows[0]["HIVCareWhere"].ToString();
                        }
                        if (theDS1.Tables[1].Rows[0]["HIVPresumptiveDiagnosis"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["HIVPresumptiveDiagnosis"].ToString()) == 1)
                            {
                                this.chkHIVPresumptiveDiagnosis.Checked = true;
                            }
                        }
                        if (theDS1.Tables[1].Rows[0]["HIVPcrInfant"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["HIVPcrInfant"].ToString()) == 1)
                            {
                                this.chkHIVPcrInfant.Checked = true;
                            }
                        }
                    }
                    if (theDS1.Tables[2].Rows.Count > 0)
                    {
                        if (theDS1.Tables[2].Rows[0]["PEPStartDate"] != System.DBNull.Value)
                        {
                            string dt = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[2].Rows[0]["PEPStartDate"]);
                            if (dt != "01-Jan-1900")
                            {
                                this.txtPEPStartDate.Value = dt;
                            }
                            else
                            {
                                this.txtPEPStartDate.Value = "";
                            }
                        }
                        if (theDS1.Tables[2].Rows[0]["PEPARVs"] != System.DBNull.Value)
                        {
                            this.txtPEPARVs.Value = theDS1.Tables[2].Rows[0]["PEPARVs"].ToString();
                            string[] theStrRegimen = txtPEPARVs.Value.Split(new Char[] { '/' });
                            DataView theARVDV = new DataView(theDS.Tables[16]);
                            if (txtPEPARVs.Value != "")
                            {
                                ViewState["SelectedData"] = OldRegimenList(theStrRegimen, theARVDV);
                            }
                        }
                    }
                    if (theDS1.Tables[3].Rows.Count > 0)
                    {
                        if (theDS1.Tables[3].Rows[0]["PMTCTStartDate"] != System.DBNull.Value)
                        {
                            string dt = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[3].Rows[0]["PMTCTStartDate"]);

                            if (dt != "01-Jan-1900")
                            {
                                this.txtPMTCTStartDate.Value = dt;
                            }
                            else
                            {
                                this.txtPMTCTStartDate.Value = "";
                            }
                        }
                        if (theDS1.Tables[3].Rows[0]["PMTCTARVs"] != System.DBNull.Value)
                        {
                            this.txtPMTCTARVs.Value = theDS1.Tables[3].Rows[0]["PMTCTARVs"].ToString();
                            string[] theStrRegimen = txtPMTCTARVs.Value.Split(new Char[] { '/' });
                            DataView theARVDV = new DataView(theDS.Tables[16]);
                            if (txtPMTCTARVs.Value != "")
                            {
                                ViewState["SelectedData1"] = OldRegimenList(theStrRegimen, theARVDV);
                            }
                        }
                    }
                    if (theDS1.Tables[4].Rows.Count > 0)
                    {
                        if (theDS1.Tables[4].Rows[0]["ConfirmHIVPosDate"] != System.DBNull.Value)
                        {
                            string dt = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[4].Rows[0]["ConfirmHIVPosDate"]);

                            if (dt != "01-Jan-1900")
                            {
                                this.txtHIVConfirmHIVPosDate.Value = dt;
                            }
                            else
                            {
                                this.txtHIVConfirmHIVPosDate.Value = "";
                            }
                        }
                    }
                    if (theDS1.Tables[5].Rows.Count > 0)
                    {
                        if (theDS1.Tables[5].Rows[0]["EligibleDate"] != System.DBNull.Value)
                        {
                            string dt = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[5].Rows[0]["EligibleDate"]);

                            if (dt != "01-Jan-1900")
                            {
                                this.txtHIVEligibleDate.Value = dt;
                            }
                            else
                            {
                                this.txtHIVEligibleDate.Value = "";
                            }
                        }
                        if (theDS1.Tables[5].Rows[0]["ReadyDate"] != System.DBNull.Value)
                        {
                            string dt = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[5].Rows[0]["ReadyDate"]);

                            if (dt != "01-Jan-1900")
                            {
                                this.txtHIVReadyDate.Value = dt;
                            }
                            else
                            {
                                this.txtHIVReadyDate.Value = "";
                            }
                        }
                    }
                    if (theDS1.Tables[6].Rows.Count > 0)
                    {
                        if (theDS1.Tables[6].Rows[0]["WHOStage"] != System.DBNull.Value)
                        {
                            this.ddlHIVClincialWHOStage.SelectedValue = theDS1.Tables[6].Rows[0]["WHOStage"].ToString();
                        }
                    }
                    if (theDS1.Tables[7].Rows.Count > 0)
                    {
                        if (theDS1.Tables[7].Rows[0]["PrevARVsCD4"] != System.DBNull.Value)
                        {
                            this.txtHIVPrevARVsCD4.Text = theDS1.Tables[7].Rows[0]["PrevARVsCD4"].ToString();
                        }
                        if (theDS1.Tables[7].Rows[0]["PrevARVsCD4Percent"] != System.DBNull.Value)
                        {
                            this.txtHIVPrevARVsCD4Percent.Text = theDS1.Tables[7].Rows[0]["PrevARVsCD4Percent"].ToString();
                        }
                    }
                    if (theDS1.Tables[8].Rows.Count > 0)
                    {
                        gvDrugAllergiesMedicalCondition.DataSource = null;
                        gvDrugAllergiesMedicalCondition.DataSource = theDS1.Tables[8];
                        gvDrugAllergiesMedicalCondition.DataBind();
                        BindGrid();
                        Session["DrugAndRecentMedicalDataTable"] = theDS1.Tables[8];
                    }
                    if (theDS.Tables[17].Rows.Count > 0)
                    {
                        if (theDS.Tables[17].Rows[0]["TransferIn"] != System.DBNull.Value)
                        {
                            this.ddlarttransferinfrom.SelectedValue = theDS.Tables[17].Rows[0]["TransferIn"].ToString();
                        }
                    }

                    FillOldData(PId);

                    Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "CalEnbleDisble(" + pepChk + "," + PmtctChk + "," + chkEarlier + ")", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(Type.GetType("System.String"), "addScript", "ShowValue()", true);
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
                IEManager = null;
            }
        }
        private DataTable CreateSelectedTable()
        {
            DataTable theDT = new DataTable();
            theDT.Columns.Add("DrugId", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugName", System.Type.GetType("System.String"));
            theDT.Columns.Add("Generic", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("DrugTypeID", System.Type.GetType("System.Int32"));
            theDT.Columns.Add("Abbr", System.Type.GetType("System.String"));
            theDT.PrimaryKey = new DataColumn[] { theDT.Columns[0] };
            return theDT;
        }
        private DataTable OldRegimenList(string[] str, DataView theDV)
        {
            DataTable theDT = CreateSelectedTable();
            foreach (string reg in str)
            {
                theDV.RowFilter = "Abbr Like '" + reg + "%'";
                if (theDV.Count > 0)
                {
                    DataRow theDR = theDT.NewRow();
                    theDR[0] = theDV[0][0];
                    theDR[1] = theDV[0][1];
                    theDR[2] = theDV[0][2];
                    theDR[3] = theDV[0][3];
                    theDR[4] = theDV[0][4];
                    DataRow theTempeDR;
                    theTempeDR = theDT.Rows.Find(theDV[0][0]);
                    if (theTempeDR == null)
                    {
                        theDT.Rows.Add(theDR);
                    }
                }
            }
            return theDT;
        }
        private Hashtable InitialEvalParameters()
        {
            Hashtable htInitialEvalParameters = new Hashtable();
            IQCareUtils theUtils = new IQCareUtils();
            int PatientId = Convert.ToInt32(Session["PatientId"]);
            int locationid = 0;
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                locationid = Convert.ToInt32(Session["AppLocationId"]);
                visitPK = Convert.ToInt32("0");
                CreateDate = Convert.ToDateTime(ViewState["CreateDate"]);
            }
            else
            {
                locationid = Convert.ToInt32(Session["ServiceLocationId"]);
                visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                CreateDate = Convert.ToDateTime(ViewState["createdate"]);
            }
            htInitialEvalParameters.Add("patientid", PatientId);
            htInitialEvalParameters.Add("locationid", locationid);
            htInitialEvalParameters.Add("VisitTypeID", visitid);
            htInitialEvalParameters.Add("VisitPKID", visitPK);
            // check
            //if (hdnVisitData.Value != "")
            //{
            //    txtVisitFirstEncounterDate.Value = hdnVisitData.Value;
            //    htInitialEvalParameters.Add("visitdate", txtVisitFirstEncounterDate.Value);
            //}
            //else
            //{
            //    htInitialEvalParameters.Add("visitdate", txtVisitFirstEncounterDate.Value);
            //}


            htInitialEvalParameters.Add("visitdate", System.DateTime.Now.ToString("dd-MMM-yyyy"));
            PriorArt = this.rdoDisclosureYes.Checked == true ? 1 : (this.rdoDisclosureNo.Checked == true ? 0 : 9);
            htInitialEvalParameters.Add("PriorArt", PriorArt);
            chkPepData = this.chkPEP.Checked == true ? 1 : 0;
            htInitialEvalParameters.Add("PEP", chkPepData);
            htInitialEvalParameters.Add("PEPStartDate", txtPEPStartDate.Value);
            htInitialEvalParameters.Add("PEPWhere", txtPEPWhere.Text);
            htInitialEvalParameters.Add("PEPARVs", txtPEPARVs.Value);
            chkPmtData = this.chkPMTCTOnly.Checked == true ? 1 : 0;
            htInitialEvalParameters.Add("PMTCTOnly", chkPmtData);
            htInitialEvalParameters.Add("PMTCTStartDate", txtPMTCTStartDate.Value);
            htInitialEvalParameters.Add("PMTCTWhere", txtPMTCTWhere.Text);
            htInitialEvalParameters.Add("PMTCTARVs", txtPMTCTARVs.Value);
            chkArvData = this.chkEarlierArvNotTransfer.Checked == true ? 1 : 0;
            htInitialEvalParameters.Add("EarlierArvNotTransfer", chkArvData);
            htInitialEvalParameters.Add("EarlierArvStartDate", txtEarlierArvStartDate.Value);
            htInitialEvalParameters.Add("EarlierArvWhere", txtEarlierArvWhere.Text);
            htInitialEvalParameters.Add("EarlierArvNotTransferArv", txtEarlierArvNotTransferArv.Value);
            htInitialEvalParameters.Add("HIVConfirmHIVPosDate", txtHIVConfirmHIVPosDate.Value);
            htInitialEvalParameters.Add("HivTestType", ddlHivTestType.SelectedItem.Value);
            htInitialEvalParameters.Add("HIVCareWhere", txtHIVCareWhere.Text);
            htInitialEvalParameters.Add("HIVEligibleDate", txtHIVEligibleDate.Value);
            htInitialEvalParameters.Add("HIVClincialWHOStage", ddlHIVClincialWHOStage.SelectedItem.Value);
            htInitialEvalParameters.Add("HIVPrevARVsCD4", txtHIVPrevARVsCD4.Text);
            htInitialEvalParameters.Add("HIVPrevARVsCD4Percent", txtHIVPrevARVsCD4Percent.Text);
            htInitialEvalParameters.Add("HIVReadyDate", txtHIVReadyDate.Value);
            chkHivCarePresumptiveDiagnosisData = this.chkHIVPresumptiveDiagnosis.Checked == true ? 1 : 0;
            htInitialEvalParameters.Add("HIVPresumptiveDiagnosis", chkHivCarePresumptiveDiagnosisData);
            chkHivCarePcrInfantData = this.chkHIVPcrInfant.Checked == true ? 1 : 0;
            htInitialEvalParameters.Add("HIVPcrInfant", chkHivCarePcrInfantData);
            htInitialEvalParameters.Add("UserID", Convert.ToInt32(Session["AppUserId"].ToString()));
            htInitialEvalParameters.Add("CreateDate", CreateDate);
            htInitialEvalParameters.Add("HIVPreTranfrinfrom", ddlarttransferinfrom.SelectedItem.Value);
            if (Convert.ToDateTime(ViewState["VisitDate"]) == Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy")))
            {
                htInitialEvalParameters.Add("Flag", 1);
            }
            else
            {
                htInitialEvalParameters.Add("Flag", 0);
            }
            return htInitialEvalParameters;
        }
        protected void btnAddAllergy_Click(object sender, EventArgs e)
        {

            if (Authentiaction.HasFunctionRight(ApplicationAccess.PriorARTHIVCare, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnsave.Enabled = false;
                btnAddAllergy.Enabled = false;
            }
            if (FieldValidation() == false)
            {
                return;
            }
            CollectDrugAndRecentMedicalData();
        }
        protected void Page_Init(Object sender, EventArgs e)
        {
            if (Convert.ToInt32(Session["TechnicalAreaId"]) != 202)
            {
                btnsave.Enabled = false;
                btncomplete.Enabled = false;
            }
            if (!IsPostBack)
            {
                Init_Form();
                /***************** Check For User Rights ****************/
                Authenticate();
            }
        }
        private void Authenticate()
        {
            /***************** Check For User Rights ****************/
            // AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Authentiaction.HasFunctionRight(ApplicationAccess.PriorARTHIVCare, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;
            }
            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.PriorARTHIVCare, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                    btncomplete.Enabled = false;
                }
            }
            else if (Request.QueryString["name"] == "Delete")
            {
                btnsave.Text = "Delete";
                btncomplete.Visible = false;
                if (Authentiaction.HasFunctionRight(ApplicationAccess.PriorARTHIVCare, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    int PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                    string theUrl = "";
                    theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmClinical_DeleteForm.aspx", PatientID, Session["HIVPatientStatus"].ToString());
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.PriorARTHIVCare, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                {
                    //btnsave.Text = "Delete";
                    btnsave.Enabled = false;
                    //btncomplete.Visible = false;
                }
            }
            else
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.PriorARTHIVCare, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    string theUrl = "";
                    //theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString(), Session["HIVPatientStatus"].ToString());
                    theUrl = string.Format("{0}?sts={1}", "frmPatient_History.aspx", Session["HIVPatientStatus"].ToString());
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.PriorARTHIVCare, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                    btncomplete.Enabled = false;
                }
                if (Convert.ToString(Session["HIVPatientStatus"]) == "1")
                {
                    btnsave.Enabled = false;
                    btncomplete.Enabled = false;
                }
                //Privilages for Care End
                if (Convert.ToString(Session["CareEndFlag"]) == "1")
                {
                    btnsave.Enabled = true;
                    btncomplete.Enabled = true;
                }
            }
            if (Convert.ToString(((DataTable)Session["PtnPrgStatus"]).Rows[0][0]) == "Care Ended")
            {
                btnsave.Enabled = false;
                btncomplete.Enabled = false;
            }
        }
        protected void Init_Form()
        {


            //try
            //{
            BindFunctions BindManager = new BindFunctions();
            IQCareUtils theUtils = new IQCareUtils();
            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
            //if (Request.QueryString["name"] == "Add")
            if (theDSXML.Tables["Mst_LPTF"] != null)
            {
                DataView theDV = new DataView(theDSXML.Tables["Mst_LPTF"]);
                theDV.RowFilter = "DeleteFlag = 0";
                theDV.Sort = "SRNo";
                if (theDV.Table != null)
                {
                    DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlarttransferinfrom, theDT, "Name", "Id");
                }
            }
            if (Session["PatientVisitId"].ToString() == "0")
            {
                //Again Try
                int FeatureID = 0, PatientID = 0;
                FeatureID = 163;// Convert.ToInt32(Session["FeatureID"]);
                PatientID = Convert.ToInt32(Session["PatientId"]);
                ICustomForm CustomFormMgr = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameter);
                DataSet theDS = CustomFormMgr.GetFieldName_and_Label(FeatureID, PatientID);
                if (Convert.ToInt32(theDS.Tables[14].Rows[0]["MultiVisit"]) == 0)
                {
                    if (theDS.Tables[15].Rows.Count > 0)
                    {
                        Session["PatientVisitId"] = theDS.Tables[15].Rows[0]["Visit_Id"];
                        Session["ServiceLocationId"] = theDS.Tables[0].Rows[0]["LocationId"];
                    }
                }
            }

            ILabFunctions LabResultManager = (ILabFunctions)ObjectFactory.CreateInstance("BusinessProcess.Laboratory.BLabFunctions, BusinessProcess.Laboratory");
            DataSet theDSLabs;
            theDSLabs = LabResultManager.GetLabValues();
            DataTable HivTestType = theDSLabs.Tables[2];
            DataView theDV1 = new DataView(HivTestType);
            theDV1.RowFilter = "SubTestId in (53,114)";
            HivTestType = theUtils.CreateTableFromDataView(theDV1);

            if ((Session["PatientId"] == null) || (Convert.ToInt32(Session["PatientId"]) == 0))
            {
                DataView theDV = new DataView();
                DataTable theDT = new DataTable();
                /*******/
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "DeleteFlag=0 and CodeID=22";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlHIVClincialWHOStage, theDT, "Name", "ID");
                    BindManager.BindCombo(ddlHivTestType, HivTestType, "SubTestName", "SubTestId");
                    theDV.Dispose();
                    theDT.Clear();
                }
            }
            //if (Request.QueryString["name"] == "Edit")
            if (Convert.ToInt32(Session["PatientId"]) > 0)
            {
                DataView theDV = new DataView();
                DataTable theDT = new DataTable();
                theDV = new DataView(theDSXML.Tables["Mst_District"]);
                theDV.RowFilter = "SystemID=" + Session["SystemId"] + "";
                theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                theDV.RowFilter = "CodeID=22";
                if (theDV.Table != null)
                {
                    theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddlHIVClincialWHOStage, theDT, "Name", "ID");
                    BindManager.BindCombo(ddlHivTestType, HivTestType, "SubTestName", "SubTestId");
                    theDV.Dispose();
                    theDT.Clear();
                }
            }
            BindEnrollmentDate();
            //}
            //catch (Exception err)
            //{
            //    MsgBuilder theBuilder = new MsgBuilder();
            //    theBuilder.DataElements["MessageText"] = err.Message.ToString();
            //    IQCareMsgBox.Show("#C1", theBuilder, this);
            //    return;
            //}
            //finally
            //{
            //}
        }
        public void BindEnrollmentDate()
        {
            string ObjFactoryParameter = "BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical";
            IPatientRegistration PatRegMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
            DataSet theDS = PatRegMgr.GetFieldNames(202, Convert.ToInt32(Session["PatientId"]));
            ViewState["PatientIdentdata"] = theDS.Tables[1];
            ViewState["ModuleIdentifiers"] = theDS.Tables[0];
            if (theDS.Tables[2].Rows.Count > 0 && txtenrollmentDate.Value == "")
            {
                txtenrollmentDate.Value = ((DateTime)theDS.Tables[2].Rows[0]["StartDate"]).ToString(Session["AppDateFormat"].ToString());

            }

        }
        private void CollectDrugAndRecentMedicalData()
        {
            if (Session["SaveFlag"].ToString() == "Add")
            {
                if (Session["DrugAndRecentMedicalDataTable"] == null)
                {
                    DataTable dTable = new DataTable("Dynamically_Generated");
                    DataColumn auto = new DataColumn("AutoID", typeof(System.Int32));
                    dTable.Columns.Add(auto);
                    // create another column
                    DataColumn drugAllergies = new DataColumn("DrugAllergies", typeof(string));
                    dTable.Columns.Add(drugAllergies);
                    // create one more column
                    DataColumn typeOfReaction = new DataColumn("TypeOfReaction", typeof(string));
                    dTable.Columns.Add(typeOfReaction);
                    DataColumn dateOfAllergy = new DataColumn("DateOfAllergy", typeof(string));
                    dTable.Columns.Add(dateOfAllergy);
                    DataColumn relevantMedicalCondition = new DataColumn("RelevantMedicalCondition", typeof(string));
                    dTable.Columns.Add(relevantMedicalCondition);
                    auto.AutoIncrement = true;
                    auto.AutoIncrementSeed = 1;
                    auto.ReadOnly = true;
                    auto.Unique = true;
                    DataColumn[] pK = new DataColumn[1];
                    pK[0] = auto;
                    dTable.PrimaryKey = pK;
                    DataRow row = null;
                    row = dTable.NewRow();
                    row["AutoID"] = 0;
                    //  ViewState["AutoID"] = 1;
                    row["DrugAllergies"] = txtDrugAllergies.Text.Trim();
                    row["TypeOfReaction"] = txtTypeOfReaction.Text.Trim();

                    if (!string.IsNullOrEmpty(txtDateAllergy.Value.Trim()))
                        row["dateOfAllergy"] = txtDateAllergy.Value.Trim();

                    row["RelevantMedicalCondition"] = txtRelevantMedicalCondition.Text.Trim();
                    dTable.Rows.Add(row);
                    gvDrugAllergiesMedicalCondition.DataSource = dTable;
                    gvDrugAllergiesMedicalCondition.DataBind();
                    if (gvDrugAllergiesMedicalCondition.Columns.Count < 1)
                    {
                        BindGrid();
                    }
                    Session["DrugAndRecentMedicalDataTable"] = dTable;
                }
                else
                {
                    DataTable dt = (DataTable)Session["DrugAndRecentMedicalDataTable"];

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["DrugAllergies"].ToString() == txtDrugAllergies.Text.Trim())
                            {
                                IQCareMsgBox.Show("DrugDBExist", this);
                                txtDrugAllergies.Focus();
                                return;
                            }
                        }

                    }


                    object sumObject;
                    sumObject = dt.Compute("MAX(AutoID)", "");
                    Int32 ComputedMaxID = 0;
                    if (sumObject != null)
                    {
                        ComputedMaxID = Convert.ToInt32(sumObject.ToString());
                    }
                    DataRow row = null;
                    row = dt.NewRow();
                    ComputedMaxID = ComputedMaxID + 1;
                    row["AutoID"] = ComputedMaxID; // dt.Rows.Count + 1;
                    row["DrugAllergies"] = txtDrugAllergies.Text.Trim();
                    row["TypeOfReaction"] = txtTypeOfReaction.Text.Trim();
                    if (txtDateAllergy.Value != "")
                    {
                        row["dateOfAllergy"] = txtDateAllergy.Value.Trim();
                    }
                    row["RelevantMedicalCondition"] = txtRelevantMedicalCondition.Text.Trim();
                    dt.Rows.Add(row);
                    gvDrugAllergiesMedicalCondition.DataSource = dt;
                    gvDrugAllergiesMedicalCondition.DataBind();
                    Session["DrugAndRecentMedicalDataTable"] = dt;
                }
            }
            else if (Session["SaveFlag"].ToString() == "Edit")
            {
                //Edit mode- ie member is selected from grid
                int r = Convert.ToInt32(Session["SelectedRow"]);
                DataTable dt = (DataTable)Session["DrugAndRecentMedicalDataTable"];
                if (dt.Rows.Count > 0)
                {
                    int id = Convert.ToInt32(dt.Rows[r]["AutoID"].ToString());
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dt.Rows[i]["AutoID"]) != id)
                        {
                            if (dt.Rows[i]["DrugAllergies"].ToString() == txtDrugAllergies.Text.Trim())
                            {
                                IQCareMsgBox.Show("DrugDBExist", this);
                                txtDrugAllergies.Focus();
                                return;
                            }
                        }
                    }

                }

                dt.Rows[r]["DrugAllergies"] = txtDrugAllergies.Text.Trim();
                dt.Rows[r]["TypeOfReaction"] = txtTypeOfReaction.Text.Trim();

                if (!string.IsNullOrEmpty(txtDateAllergy.Value.Trim()))
                    dt.Rows[r]["dateOfAllergy"] = txtDateAllergy.Value.Trim();

                dt.Rows[r]["RelevantMedicalCondition"] = txtRelevantMedicalCondition.Text.Trim();
                gvDrugAllergiesMedicalCondition.DataSource = dt;
                gvDrugAllergiesMedicalCondition.DataBind();
                Session["DrugAndRecentMedicalDataTable"] = dt;
            }
            resetDrugData();
        }
        private void resetDrugData()
        {
            btnAddAllergy.Text = "Add Allergy";
            Session["SaveFlag"] = "Add";
            Session["SelectedId"] = "";
            Session["ClinicID"] = "";
            txtDrugAllergies.Text = "";
            txtTypeOfReaction.Text = "";
            txtDateAllergy.Value = "";
            txtRelevantMedicalCondition.Text = "";
        }
        protected void btncomplete_Click(object sender, EventArgs e)
        {


            if (dataQualityCheck())
            {
                // ViewState["dataQuality"]
                ViewState["DataQualityFlag"] = "1";
                btncomplete.CssClass = "greenButton";
                save();
            }
            else
            {
                ViewState["DataQualityFlag"] = "0";
                btncomplete.CssClass = "";
            }
        }

        private void restoreFontColor()
        {

            lblHIVConfirmHIVPosDate.Style.Remove("color");
        }
        private bool dataQualityCheck()
        {
            IQCareUtils iQCareUtils = new IQCareUtils();
            DateTime temp;
            string validateMessage = "Following values are required to complete the data quality check:\\n\\n";
            bool qualityCheck = true;
            restoreFontColor();

            if (txtHIVConfirmHIVPosDate.Value.Trim() == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Data Confirmed HIV Positive Date";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "\\n";
                txtHIVConfirmHIVPosDate.Focus();
                qualityCheck = false;
                lblHIVConfirmHIVPosDate.Style.Add("color", "red");
            }
            else
            {
                if (!DateTime.TryParseExact(txtHIVConfirmHIVPosDate.Value, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Visit Date";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
                    txtHIVConfirmHIVPosDate.Focus();
                    qualityCheck = false;
                    lblHIVConfirmHIVPosDate.Style.Add("color", "red");
                }
                //else if (theCurrentDate < Convert.ToDateTime(iQCareUtils.MakeDate(txtVisitDate.Text)))
                //{
                //    validateMessage += "-" + IQCareMsgBox.GetMessage("CompareDate5", this) + "\\n";
                //    txtVisitDate.Focus();
                //    qualityCheck = false;
                //    lblVisitDate.Style.Add("color", "red");
                //}
            }
            if (!qualityCheck)
            {
                MsgBuilder totalMsgBuilder = new MsgBuilder();
                totalMsgBuilder.DataElements["MessageText"] = validateMessage;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
            }
            return qualityCheck;

        }
        protected void btnback_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Add" && Convert.ToInt32(Session["PatientVisitId"]) > 0)
            {
                string theUrl;
                theUrl = string.Format("frmPatient_Home.aspx");
                Response.Redirect(theUrl);
            }
            else
            {
                string theUrl;
                theUrl = string.Format("frmPatient_History.aspx");
                Response.Redirect(theUrl);
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
        }

        private void save()
        {
            //HashTable Function for SingleSelect Items
            Hashtable htInitialEvalParameters = InitialEvalParameters();
            IPriorArtHivCare IEManager;
            IEManager = (IPriorArtHivCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPriorArtHivCare, BusinessProcess.Clinical");
            PId = Convert.ToInt32(Session["PatientId"]);
            int patientid = Convert.ToInt32(PId);
            thedtDynamicDrugMedical = (DataTable)Session["DrugAndRecentMedicalDataTable"];
            try
            {
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    //  ViewState["DataQualityFlag"] = "0";
                    Session["Redirect"] = "0";

                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);


                    DataSet DS = (DataSet)IEManager.SavePriorArtHivCare(htInitialEvalParameters, intflag, Convert.ToInt32(ViewState["DataQualityFlag"]), thedtDynamicDrugMedical, theCustomDataDT);
                    Session["PatientVisitId"] = Convert.ToInt32(DS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    ViewState["VisitDate"] = Convert.ToDateTime(DS.Tables[0].Rows[0]["VisitDate"].ToString());
                    Session["ServiceLocationId"] = Convert.ToInt32(DS.Tables[0].Rows[0]["LocationID"].ToString());
                    SaveCancel();
                }
                //Editing Existing Records
                else //if (Request.QueryString["name"] == "Edit")
                {
                    IEManager.Update_DataQuality(PId, visitPK, Convert.ToInt32(ViewState["DataQuality"]), Convert.ToInt32(Session["AppLocationId"]));
                    //    ViewState["DataQualityFlag"] = "0";


                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);


                    DataSet DS = (DataSet)IEManager.SavePriorArtHivCare(htInitialEvalParameters, intflag, Convert.ToInt32(ViewState["DataQualityFlag"]), thedtDynamicDrugMedical, theCustomDataDT);
                    Session["PatientVisitId"] = Convert.ToInt32(DS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    ViewState["VisitDate"] = Convert.ToDateTime(DS.Tables[0].Rows[0]["VisitDate"].ToString());
                    Session["ServiceLocationId"] = Convert.ToInt32(DS.Tables[0].Rows[0]["LocationID"].ToString());
                    SaveCancel();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            finally
            {
                IEManager = null;
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (SaveFieldValidation())
            {

                if (Request.QueryString["name"] == "Delete")
                {
                    DeleteForm();
                }

                save();
                // return;
            }
            // rdoDisclosureYes.Focus();


        }

        private void DeleteForm()
        {
            //      Session["ArtEncounterPatientVisitId"] = 0;
            IPriorArtHivCare CareARTEncounter;
            int theResultRow, OrderNo;
            string FormName;
            OrderNo = Convert.ToInt32(Session["PatientVisitId"].ToString());
            FormName = "Prior ART/HIV Care";

            CareARTEncounter = (IPriorArtHivCare)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPriorArtHivCare, BusinessProcess.Clinical");
            theResultRow = (int)CareARTEncounter.DeleteHIVCareEncounterForms(FormName, OrderNo, Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["AppUserId"].ToString()));
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
        private bool SaveFieldValidation()
        {

            string validateMessage = "Following values are required to complete the save check:\\n\\n";
            bool qualityCheck = true;
            if (rdoDisclosureYes.Checked == false && rdoDisclosureNo.Checked == false)
            {

                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Prior Art";
                validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "\\n";
                rdoDisclosureYes.Focus();
                qualityCheck = false;


            }
            if (!qualityCheck)
            {
                MsgBuilder totalMsgBuilder = new MsgBuilder();
                totalMsgBuilder.DataElements["MessageText"] = validateMessage;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
            }
            return qualityCheck;
        }
        protected void btnRegimen_Click(object sender, EventArgs e)
        {
            string theScript;
            //Application.Add("MasterData", ViewState["MasterData"]);
            Application.Add("MasterData", ViewState["ARVMasterData"]);
            Application.Add("SelectedDrug", (DataTable)ViewState["SelectedData"]);
            //ViewState.Remove("MasterData");
            ViewState.Remove("ARVMasterData");
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=" + btnRegimen.ID + "' ,'DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }
        protected void btnRegimen1_Click(object sender, EventArgs e)
        {
            string theScript;
            Application.Add("MasterData", ViewState["ARVMasterData1"]);
            Application.Add("SelectedDrug", (DataTable)ViewState["SelectedData1"]);
            ViewState.Remove("MasterData");
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=" + btnRegimen1.ID + "','DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }
        protected void btnRegimen2_Click(object sender, EventArgs e)
        {
            string theScript;
            Application.Add("MasterData", ViewState["ARVMasterData2"]);
            Application.Add("SelectedDrug", (DataTable)ViewState["SelectedData2"]);
            ViewState.Remove("MasterData");
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=" + btnRegimen2.ID + "','DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }
        protected void gvDrugAllergiesMedicalCondition_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int s = e.RowIndex;
            GridViewRow gr = gvDrugAllergiesMedicalCondition.Rows[e.RowIndex];
            DataTable dt = (DataTable)Session["DrugAndRecentMedicalDataTable"];
            dt.Rows[s].Delete();
            dt.AcceptChanges();
            gvDrugAllergiesMedicalCondition.DataSource = dt;
            gvDrugAllergiesMedicalCondition.DataBind();
            if (gvDrugAllergiesMedicalCondition.Rows.Count == 0)
            {
                Session["DrugAndRecentMedicalDataTable"] = null;
            }
            else
            {
                Session["DrugAndRecentMedicalDataTable"] = dt;
            }
        }
        protected void gvDrugAllergiesMedicalCondition_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (e.Row.Cells[0].Text.ToString() != "0")
                    {
                        if (Authentiaction.HasFunctionRight(ApplicationAccess.PriorARTHIVCare, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                        {
                            e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                            e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                            e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(gvDrugAllergiesMedicalCondition, "Select$" + e.Row.RowIndex.ToString()));
                        }
                    }
                    if (e.Row.Cells[0].Text.ToString() == "0")
                    {
                        //e.Row.Cells[i].BackColor = System.Drawing.Color.LightGray;
                        //e.Row.Cells[0].Visible = false;
                        if (Authentiaction.HasFunctionRight(ApplicationAccess.PriorARTHIVCare, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                        {
                            e.Row.Cells[i].Attributes.Add("onmouseover", "this.style.cursor='hand';this.style.BackColor='#666699';");
                            e.Row.Cells[i].Attributes.Add("onmouseout", "this.style.backgroundColor='';");
                            e.Row.Cells[i].Attributes.Add("onclick", Page.ClientScript.GetPostBackEventReference(gvDrugAllergiesMedicalCondition, "Select$" + e.Row.RowIndex.ToString()));
                        }
                    }
                }
                if (Authentiaction.HasFunctionRight(ApplicationAccess.PriorARTHIVCare, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
                {
                    LinkButton objlink = (LinkButton)e.Row.Cells[5].Controls[0];
                    objlink.OnClientClick = "if(!confirm('Are you sure you want to delete this?')) return false;";
                    e.Row.Cells[5].ID = e.Row.RowIndex.ToString();
                    //btnSubmit.Enabled = false;
                }
            }
        }

        protected void gvDrugAllergiesMedicalCondition_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (Authentiaction.HasFunctionRight(ApplicationAccess.PriorARTHIVCare, FunctionAccess.Update, (DataTable)Session["UserRight"]) == true)
            {
                btnsave.Enabled = true;
                btnAddAllergy.Enabled = true;
            }
            if (Session["lblpntstatus"].ToString() == "1")
            {
                btnAddAllergy.Enabled = false;
                btnsave.Enabled = false;
            }
            else
            {
                btnAddAllergy.Enabled = true;
            }
            int thePage = gvDrugAllergiesMedicalCondition.PageIndex;
            int thePageSize = gvDrugAllergiesMedicalCondition.PageSize;
            GridViewRow theRow = gvDrugAllergiesMedicalCondition.Rows[e.NewSelectedIndex];
            int theIndex = thePageSize * thePage + theRow.RowIndex;
            System.Data.DataTable theDT = new System.Data.DataTable();
            theDT = ((DataTable)Session["DrugAndRecentMedicalDataTable"]);
            int r = theIndex;
            // Fill data in Textboxes from grid
            //Edit the selected row
            if (theDT.Rows.Count > 0)
            {
                txtDrugAllergies.Text = theDT.Rows[r]["DrugAllergies"].ToString();
                txtTypeOfReaction.Text = theDT.Rows[r]["TypeOfReaction"].ToString();
                //txtDateAllergy.Value = String.Format("{0:dd-MMM-yyyy}", theDT.Rows[r]["dateOfAllergy"]);
                if (!string.IsNullOrEmpty(theDT.Rows[r]["dateOfAllergy"].ToString()))
                {
                    if (Convert.ToDateTime(theDT.Rows[r]["dateOfAllergy"]).ToString("dd-MMM-yyyy") != "01-Jan-1900")
                        txtDateAllergy.Value = String.Format("{0:dd-MMM-yyyy}", theDT.Rows[r]["dateOfAllergy"]);
                }
                txtRelevantMedicalCondition.Text = theDT.Rows[r]["RelevantMedicalCondition"].ToString();
                Session["SelectedRow"] = theIndex;
                Session["SaveFlag"] = "Edit";
                btnAddAllergy.Text = "Update Allergy";
            }
        }
        protected void gvDrugAllergiesMedicalCondition_Sorting(object sender, GridViewSortEventArgs e)
        {
            IQCareUtils clsUtil = new IQCareUtils();
            DataView theDV;

            if (Session["SortDirection"].ToString() == "Asc")
            {
                theDV = clsUtil.GridSort((DataTable)Session["GridData"], e.SortExpression, Session["SortDirection"].ToString());
                Session["SortDirection"] = "Desc";
            }
            else
            {
                theDV = clsUtil.GridSort((DataTable)Session["GridData"], e.SortExpression, Session["SortDirection"].ToString());
                Session["SortDirection"] = "Asc";
            }
            gvDrugAllergiesMedicalCondition.Columns.Clear();
            gvDrugAllergiesMedicalCondition.DataSource = theDV;
            BindGrid();
            //if (btnSubmit.Enabled == true)
            //{
            //    btnSubmit.Enabled = true;
            //}
        }
        private void BindGrid()
        {
            BoundField theCol0 = new BoundField();
            theCol0.HeaderText = "AutoID";
            theCol0.HeaderStyle.CssClass = "blue";
            theCol0.DataField = "AutoID";
            theCol0.ItemStyle.CssClass = "textstyle";
            gvDrugAllergiesMedicalCondition.Columns.Add(theCol0);
            BoundField theCol1 = new BoundField();
            theCol1.HeaderText = "Drug Allergies";
            theCol1.HeaderStyle.CssClass = "Blue";
            theCol1.DataField = "DrugAllergies";
            theCol1.ItemStyle.CssClass = "textstyle";
            gvDrugAllergiesMedicalCondition.Columns.Add(theCol1);
            BoundField theCol2 = new BoundField();
            theCol2.HeaderText = "Type Of Reaction";
            theCol2.DataField = "TypeOfReaction";
            theCol2.ItemStyle.CssClass = "textstyle";
            theCol2.ReadOnly = true;
            gvDrugAllergiesMedicalCondition.Columns.Add(theCol2);
            BoundField theCol3 = new BoundField();
            theCol3.HeaderText = "Date Of Allergy";
            theCol3.DataField = "dateOfAllergy";
            theCol3.ItemStyle.CssClass = "textstyle";
            theCol3.DataFormatString = "{0:dd-MMM-yyyy}";
            theCol3.ReadOnly = true;
            gvDrugAllergiesMedicalCondition.Columns.Add(theCol3);
            BoundField theCol6 = new BoundField();
            theCol6.HeaderText = "Relevant Medical Conditions";
            theCol6.ItemStyle.CssClass = "textstyle";
            theCol6.DataField = "RelevantMedicalCondition";
            //    theCol6.SortExpression = "RelevantMedicalCondition";
            theCol6.ReadOnly = true;
            gvDrugAllergiesMedicalCondition.Columns.Add(theCol6);
            if (Authentiaction.HasFunctionRight(ApplicationAccess.PriorARTHIVCare, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == true && Session["lblpntstatus"].ToString() != "1")
            {
                CommandField objfield = new CommandField();
                objfield.ButtonType = ButtonType.Link;
                objfield.DeleteText = "<img src='../Images/del.gif' alt='Delete this' border='0' />";
                objfield.HeaderText = "Delete";
                objfield.ShowDeleteButton = true;
                gvDrugAllergiesMedicalCondition.Columns.Add(objfield);
            }
            gvDrugAllergiesMedicalCondition.DataBind();
            gvDrugAllergiesMedicalCondition.Columns[0].Visible = false;
        }

        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.PriorARTHIVCare));
                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "PAHCare");
                    //ViewState["CustomFieldsDS"] = theDS;
                    //pnlCustomList.Visible = true;
                }
                //theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "PAHCare");
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

        private void UpdateCustomFieldsValues()
        {
            GenerateCustomFieldsValues(pnlCustomList);
            string sqlstr = string.Empty;
            PatID = Convert.ToInt32(Request.QueryString["patientid"]);
            string sqlselect;
            string strdelete;
            Int32 visitID = 0;
            DateTime visitdate = System.DateTime.Now;
            ICustomFields CustomFields;
            //  if (txtvisitDate.Text.ToString() != "")

            visitdate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy"));

            if (ViewState["VisitID_add"] != null)
                visitID = Convert.ToInt32(ViewState["VisitID_add"]);

            if (sbValues.ToString().Trim() != "")
            {
                if (ViewState["CustomFieldsData"] != null)
                {
                    sbValues = sbValues.Remove(0, 1);
                    sqlstr = "UPDATE dtl_CustomField_" + TableName.ToString().Replace("-", "_") + " SET ";
                    sqlstr += sbValues.ToString() + " where Ptn_pk= " + PatID.ToString() + " and Visit_pk=" + visitID.ToString();
                }
                else
                {
                    sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
                    sqlstr += " VALUES(" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";
                    ViewState["CustomFieldsData"] = 1;
                }


                try
                {
                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                    icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
                    if (icount == -1)
                    {
                        return;
                    }
                }
                catch
                {
                }
                finally
                {
                    CustomFields = null;
                }
            }
            if (strmultiselect.ToString() != "")
            {
                string[] FieldValues = strmultiselect.Split(new char[] { '^' });
                if (arl.Count != 0)
                {
                    int p = 0;
                    foreach (object obj in arl)
                    {
                        sqlselect = "";
                        strdelete = "";
                        if (obj.ToString() != "")
                        {
                            try
                            {
                                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                                strdelete = "DELETE from [" + obj.ToString() + "] where ptn_pk= " + PatID.ToString() + " and LocationID=" + Session["AppLocationID"] + " and visit_pk=" + visitID;
                                icount = CustomFields.SaveCustomFieldValues(strdelete.ToString());

                                if (FieldValues[p].ToString() != "")
                                {
                                    string[] mValues = FieldValues[p].Split(new char[] { ',' });

                                    foreach (string str in mValues)
                                    {
                                        if (str.ToString() != "")
                                        {
                                            string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
                                            Int32 ispos = Convert.ToInt32(strtab.Length);
                                            Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;

                                            sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,visit_pk,visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
                                            sqlselect += " VALUES (" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";



                                            icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
                                            if (icount == -1)
                                            {
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            catch
                            {
                            }
                            finally
                            {
                                CustomFields = null;
                            }
                        }
                        p += 1;
                    }
                }
            }
        }

        private void InsertCustomFieldsValues()
        {
            GenerateCustomFieldsValues(pnlCustomList);
            string sqlstr = string.Empty;
            string sqlselect;
            Int32 visitID = 0;
            DateTime visitdate = System.DateTime.Now;
            PatID = Convert.ToInt32(Request.QueryString["patientid"]);
            ICustomFields CustomFields;
            if (ViewState["VisitID_add"] != null)
                visitID = Convert.ToInt32(ViewState["VisitID_add"]);
            //  if (txtvisitDate.Text.ToString() != "")
            visitdate = Convert.ToDateTime(System.DateTime.Now.ToString("dd-MMM-yyyy"));

            if (sbValues.ToString().Trim() != "")
            {
                sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
                sqlstr += " VALUES(" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";

                try
                {
                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                    icount = CustomFields.SaveCustomFieldValues(sqlstr.ToString());
                    if (icount == -1)
                    {
                        return;
                    }
                }
                catch
                {
                }
                finally
                {
                    CustomFields = null;
                }
            }
            if (strmultiselect.ToString() != "")
            {
                string[] FieldValues = strmultiselect.Split(new char[] { '^' });
                if (arl.Count != 0)
                {
                    int p = 0;
                    foreach (object obj in arl)
                    {
                        sqlselect = "";
                        if (obj.ToString() != "")
                        {
                            if (FieldValues[p].ToString() != "")
                            {
                                string[] mValues = FieldValues[p].Split(new char[] { ',' });
                                foreach (string str in mValues)
                                {
                                    if (str.ToString() != "")
                                    {
                                        string strtab = "dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_";
                                        Int32 ispos = Convert.ToInt32(strtab.Length);
                                        Int32 iepos = Convert.ToInt32(obj.ToString().Length) - ispos;
                                        sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,Visit_pk,Visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
                                        sqlselect += " VALUES (" + PatID.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";
                                        try
                                        {
                                            CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                                            icount = CustomFields.SaveCustomFieldValues(sqlselect.ToString());
                                            if (icount == -1)
                                            {
                                                return;
                                            }

                                        }
                                        catch
                                        {
                                        }
                                        finally
                                        {
                                            CustomFields = null;
                                        }
                                    }
                                }
                            }
                        }
                        p += 1;
                    }
                }
            }
        }

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
                dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, visitPK, 0, 0, Convert.ToInt32(ApplicationAccess.PriorARTHIVCare));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "PAHCare");
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


    }
}