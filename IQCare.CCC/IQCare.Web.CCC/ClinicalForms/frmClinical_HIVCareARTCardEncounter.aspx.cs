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
using Interface.Security;
namespace IQCare.Web.Clinical
{
    public partial class HIVCareARTCardEncounter : System.Web.UI.Page
    {
        DataTable DTCheckedIds;
        AuthenticationManager Authentication;
        IHivCareARTEncounter HivCareARTEncounterInterface;
        DataSet tempDataSet;
        DataTable tempDataTable;
        DataView tempDataView;
        Hashtable hashTable;
        DataSet dataSetForSaving;
        DataTable theDTNewOisProblem, theDTpotentialSideEffect, theDTReferTo, theDTFamilyPlanning;
        int patientID;
        int locationID;
        string gender;
        int ageInYear;
        int ageInMonth;
        int visitID;
        int oedema = 0;
        int pregnant = -1;
        int PMTCT;
        string DOBofpatient;

        bool isUpdate;

        int ArtStartinMonth = 0;
        int ArtRegimeninMonth = 0;
        StringBuilder sbValues;
        int PatID;
        string strmultiselect;
        string TableName ="";
        int icount;
        ArrayList arl = new ArrayList();
        StringBuilder sbParameter = new StringBuilder();
        int PId;
        private void AddUIAttributes()
        {

            txtFollowUpDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtFollowUpDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3');");

            txtPhysHeight.Attributes.Add("onkeyup", "chkInteger('" + txtPhysHeight.ClientID + "')");
            txtPhysHeight.Attributes.Add("onBlur", "isBetween('" + txtPhysHeight.ClientID + "', '" + "physHeight" + "', '" + 0 + "', '" + 250 + "')");

            txtPhysWeight.Attributes.Add("onkeyup", "chkDecimal('" + txtPhysWeight.ClientID + "')");
            txtPhysWeight.Attributes.Add("onBlur", "isBetween('" + txtPhysWeight.ClientID + "', '" + "physWeight" + "', '" + 0 + "', '" + 225 + "')");

            txtEDD.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');");
            txtEDD.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3')");

            txtGestation.Attributes.Add("onkeyup", "chkInteger('" + txtGestation.ClientID + "')");

            txtDeliveryDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');HIVcareArtPostPartumBRule(this.value,'" + cbPostPartum.ClientID + "')");
            txtDeliveryDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3');HIVcareArtPostPartumBRule(this.value,'" + cbPostPartum.ClientID + "')");
            txtDeliveryDate.Attributes.Add("onfocus", "DateFormat(this,this.value,event,false,'3');HIVcareArtPostPartumBRule(this.value,'" + cbPostPartum.ClientID + "')");

            txtMUAC.Attributes.Add("OnKeyup", "chkDecimal('" + txtMUAC.ClientID + "')");
            txtNumOfDaysHospitalized.Attributes.Add("onkeyup", "chkInteger('" + txtNumOfDaysHospitalized.ClientID + "')");

            ddlArvTherapyChangeCode.Attributes.Add("onchange", "TherapyBlueCard(this.options[this.selectedIndex].text, 1);");
            ddlArvTherapyStopCode.Attributes.Add("onchange", "TherapyBlueCard(this.options[this.selectedIndex].text, 2);");

            ddlTBStatus.Attributes.Add("onchange", "TBstatusBRule(this.options[this.selectedIndex].text);");
            ddlARVDrugsAdhere.Attributes.Add("onchange", "ARVDrugsPoorFairBRule(this.options[this.selectedIndex].text,'" + ddlReasonARVDrugsPoorFair.ClientID + "');HIVcareArtPoorFairOtherBRule('" + ddlReasonARVDrugsPoorFair.ClientID + "','Other','divReasonARVDrugsother');");
            ddlReasonARVDrugsPoorFair.Attributes.Add("onchange", "HIVcareArtOtherBRule(this.options[this.selectedIndex].text,'Other','divReasonARVDrugsother');");
            // ddlReferredTo.Attributes.Add("onchange", "HIVcareArtOtherBRule(this.options[this.selectedIndex].text,'Other (specify)','divotherReferredTo');");
            ddlFamilyPanningStatus.Attributes.Add("onchange", "HIVcareArtOtherBRule(this.options[this.selectedIndex].text,'ONFP=on Family Planning','divFamilyPlanningMethod');");
            lstclinPlanFU.Attributes.Add("onchange", "specifyChangeStop('" + lstclinPlanFU.ClientID + "');");
            // rdoPMTCT.Attributes.Add("onclick", "HIVcareArtPMTCTBRule('" + rdoPMTCT.ClientID + "','spanpmctcancno');");
        }
        private void restoreFontColor()
        {
            lblVisitDate.Style.Remove("color");
            lblFollowUpDate.Style.Remove("color");
            lblHeight.Style.Remove("color");
            lblWeight.Style.Remove("color");
            lblPregnant.Style.Remove("color");
            lblTBStatus.Style.Remove("color");
            lblWABStage.Style.Remove("color");
            lblWHOStage.Style.Remove("color");
            lblARVDrugsAdhere.Style.Remove("color");
        }
        private Boolean fieldValidation()
        {
            IIQCareSystem IQCareSystemInterface = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSystemInterface.SystemDate();
            IQCareUtils iQCareUtils = new IQCareUtils();

            string validateMessage = "Following values are required to complete the data quality check:\\n\\n";
            bool validationCheck = true;
            DateTime temp;

            #region Check Visit Date
            if (txtVisitDate.Text.Trim() == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Visit Date";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "\\n";
                txtVisitDate.Focus();
                validationCheck = false;
            }
            else
            {
                if (!DateTime.TryParseExact(txtVisitDate.Text, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Visit Date";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
                    txtVisitDate.Focus();
                    validationCheck = false;

                }
                else if (theCurrentDate < Convert.ToDateTime(iQCareUtils.MakeDate(txtVisitDate.Text)))
                {
                    validateMessage += "-" + IQCareMsgBox.GetMessage("CompareDate5", this) + "\\n";
                    txtVisitDate.Focus();
                    validationCheck = false;
                }
            }
            #endregion

            #region Check Follow Up Date
            //Check FollowUp Date
            if (txtFollowUpDate.Text.Trim() != "")
                if (!DateTime.TryParseExact(txtFollowUpDate.Text, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Follow Up Date";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
                    txtFollowUpDate.Focus();
                    validationCheck = false;
                }
                else if (validationCheck && temp < Convert.ToDateTime(iQCareUtils.MakeDate(txtVisitDate.Text)))
                {
                    validateMessage += "-" + IQCareMsgBox.GetMessage("App_Visit", this) + "\\n";
                    txtFollowUpDate.Focus();
                    validationCheck = false;
                }
            #endregion
            oedema = rdoOedemaPlus.Checked == true ? 1 : (rdoOedemaMinus.Checked == true ? 2 : 0);

            //Pregnant
            pregnant = rdoPregnantYes.Checked == true ? 1 : (rdoPregnantNo.Checked == true ? 0 : -1);
            if (pregnant != -1)
            {
                if (pregnant == 1)
                    txtDeliveryDate.Text = "";
                else
                {
                    // txtEDD.Text = "";
                    txtGestation.Text = "";
                    // cbPMTCT.Checked = false;
                    txtPMTCTANCNumber.Text = "";
                }
            }

            #region Check EDD
            ////if (txtEDD.Text.Trim() != "")
            ////    if (!DateTime.TryParseExact(txtEDD.Text, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
            ////    {
            ////        MsgBuilder msgBuilder = new MsgBuilder();
            ////        msgBuilder.DataElements["Control"] = " -EDD";
            ////        validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
            ////        txtEDD.Focus();
            ////        validationCheck = false;
            ////    }            
            #endregion
            #region Check Delivery Date

            if (txtDeliveryDate.Text.Trim() != "")
                if (!DateTime.TryParseExact(txtDeliveryDate.Text, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -DeliveryDate";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
                    txtDeliveryDate.Focus();
                    validationCheck = false;
                }
            #endregion
            PMTCT = this.rdoPMTCT.Checked == true ? 1 : 0;

            #region Check TR Rx
            //////if (ddlTBStatus.SelectedItem.Text != "TB Rx")
            //////{
            //////    txtTBRxStart.Text = "";
            //////    txtTBRxStop.Text = "";
            //////    txtTBRegNumber.Text = "";
            //////}
            //////else
            //////{
            //////    if (txtTBRxStart.Text.Trim() != "")
            //////        if (!DateTime.TryParseExact(txtTBRxStart.Text, "MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
            //////        {
            //////            MsgBuilder msgBuilder = new MsgBuilder();
            //////            msgBuilder.DataElements["Control"] = " -TBRxStart";
            //////            validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
            //////            txtTBRxStart.Focus();
            //////            validationCheck = false;
            //////        }
            //////    if (txtTBRxStop.Text.Trim() != "")
            //////        if (!DateTime.TryParseExact(txtTBRxStop.Text, "MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
            //////        {
            //////            MsgBuilder msgBuilder = new MsgBuilder();
            //////            msgBuilder.DataElements["Control"] = " -TBRxStop";
            //////            validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
            //////            txtTBRxStop.Focus();
            //////            validationCheck = false;
            //////        }
            //////}
            #endregion



            //Check Other Reason of Why ARV Drugs are Poor of Fair
            if (ddlARVDrugsAdhere.SelectedIndex == 0 || ddlARVDrugsAdhere.SelectedItem.Text == "G=Good")
                ddlReasonARVDrugsPoorFair.SelectedIndex = 0;

            if (ddlReasonARVDrugsPoorFair.SelectedItem.Text.Contains("Other") == false)
                txtReasonARVDrugsPoorFairOther.Text = "";


            //  Subsitutions/Interruption
            #region
            if (lstclinPlanFU.SelectedValue.ToString() == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Subsitutions/Interruption";
                //  IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                validateMessage += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this) + "\\n";
                lstclinPlanFU.Focus();
                validationCheck = false;

            }

            if (lstclinPlanFU.SelectedValue.ToString() == "99" && ddlArvTherapyStopCode.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Stop Regimen Reason";
                // IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                validateMessage += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this) + "\\n";
                ddlArvTherapyStopCode.Focus();
                validationCheck = false;

            }
            if (lstclinPlanFU.SelectedValue.ToString() == "99" && txtARTEndeddate.Value == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Date ART Ended";
                // IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "\\n";
                txtARTEndeddate.Focus();
                validationCheck = false;
            }
            if (lstclinPlanFU.SelectedValue.ToString() == "99" && ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other") && txtarvTherapyStopCodeOtherName.Value == "")
            {

                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "- Stop Regimen Reason Other(Specify)";
                // IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "\\n";
                ddlArvTherapyStopCode.Focus();
                validationCheck = false;

            }



            if (lstclinPlanFU.SelectedValue.ToString() == "98" && ddlArvTherapyChangeCode.SelectedIndex == 0)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Stop Regimen Reason";
                // IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                validateMessage += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this) + "\\n";
                ddlArvTherapyChangeCode.Focus();
                validationCheck = false;
            }
            if (lstclinPlanFU.SelectedValue.ToString() == "98" && ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
            {
                if (txtarvTherapyChangeCodeOtherName.Value == "")
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Stop Regimen Reason (Other)Specify";
                    // IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this) + "\\n";
                    ddlArvTherapyChangeCode.Focus();
                    validationCheck = false;
                }
            }

            IQCareUtils theUtils = new IQCareUtils();
            HivCareARTEncounterInterface = (IHivCareARTEncounter)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BHivCareARTEncounter, BusinessProcess.Clinical");
            DataSet dsValidate = HivCareARTEncounterInterface.GetExistHIVArtCareEncounterbydate(patientID, Convert.ToDateTime(theUtils.MakeDate(txtVisitDate.Text)), locationID);

            if (dsValidate.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ViewState["VisitID"]) != Convert.ToInt32(dsValidate.Tables[0].Rows[0][0]))
                {
                    IQCareMsgBox.Show("ARTHIVCareenCounterExists", this);
                    validationCheck = false;
                    return validationCheck; ;
                }
            }
            #endregion


            if (!validationCheck)
            {
                MsgBuilder totalMsgBuilder = new MsgBuilder();
                totalMsgBuilder.DataElements["MessageText"] = validateMessage;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
            }
            return validationCheck;
        }
        private Boolean dataQualityCheck()
        {
            IIQCareSystem IQCareSystemInterface = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSystemInterface.SystemDate();
            IQCareUtils iQCareUtils = new IQCareUtils();
            DateTime temp;
            string validateMessage = "Following values are required to complete the data quality check:\\n\\n";
            bool qualityCheck = true;

            restoreFontColor();
            #region Check Visit Date
            if (txtVisitDate.Text.Trim() == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Visit Date";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "\\n";
                txtVisitDate.Focus();
                qualityCheck = false;
                lblVisitDate.Style.Add("color", "red");
            }
            else
            {
                if (!DateTime.TryParseExact(txtVisitDate.Text, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Visit Date";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
                    txtVisitDate.Focus();
                    qualityCheck = false;
                    lblVisitDate.Style.Add("color", "red");
                }
                else if (theCurrentDate < Convert.ToDateTime(iQCareUtils.MakeDate(txtVisitDate.Text)))
                {
                    validateMessage += "-" + IQCareMsgBox.GetMessage("CompareDate5", this) + "\\n";
                    txtVisitDate.Focus();
                    qualityCheck = false;
                    lblVisitDate.Style.Add("color", "red");
                }
            }
            #endregion
            #region Check Follow Up Date
            if (txtFollowUpDate.Text.Trim() != "")
            {
                if (!DateTime.TryParseExact(txtFollowUpDate.Text, "dd-MMM-yyyy", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out temp))
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -Follow Up Date";
                    validateMessage += IQCareMsgBox.GetMessage("WrongDateFormat", msgBuilder, this) + "\\n";
                    txtFollowUpDate.Focus();
                    qualityCheck = false;
                    lblFollowUpDate.Style.Add("color", "red");
                }
                else if (qualityCheck && temp < Convert.ToDateTime(iQCareUtils.MakeDate(txtVisitDate.Text)))
                {
                    validateMessage += "-" + IQCareMsgBox.GetMessage("App_Visit", this) + "\\n";
                    txtFollowUpDate.Focus();
                    qualityCheck = false;
                    lblFollowUpDate.Style.Add("color", "red");
                }
            }
            else
            {
                if (txtFollowUpDate.Text.Trim() == "")
                {
                    MsgBuilder msgBuilder = new MsgBuilder();
                    msgBuilder.DataElements["Control"] = " -FollowUp Date";
                    validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "\\n";
                    txtFollowUpDate.Focus();
                    qualityCheck = false;
                    lblFollowUpDate.Style.Add("color", "red");
                }
            }
            #endregion
            //Weight
            if (txtPhysWeight.Text.Trim() == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Weight";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "\\n";
                txtPhysWeight.Focus();
                qualityCheck = false;
                lblWeight.Style.Add("color", "red");
            }
            //Height
            if (txtPhysHeight.Text.Trim() == "")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Height";
                validateMessage += IQCareMsgBox.GetMessage("BlankTextBox", msgBuilder, this) + "\\n";
                txtPhysHeight.Focus();
                qualityCheck = false;
                lblHeight.Style.Add("color", "red");
            }
            //Pregnant
            if (!rdoPregnantYes.Checked && !rdoPregnantNo.Checked && gender != "Male")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -Pregnant";
                validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "\\n";
                rdoPregnantYes.Focus();
                qualityCheck = false;
                lblPregnant.Style.Add("color", "red");
            }
            //TBStatus
            ////if (ddlTBStatus.SelectedItem.Text == "Select")
            ////{
            ////    MsgBuilder msgBuilder = new MsgBuilder();
            ////    msgBuilder.DataElements["Control"] = " -TB Status";
            ////    validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "\\n";
            ////    ddlTBStatus.Focus();
            ////    qualityCheck = false;
            ////    lblTBStatus.Style.Add("color", "red");
            ////}
            //WABStage
            if (ddlWABStage.SelectedItem.Text == "Select")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -WAB Stage";
                validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "\\n";
                ddlWABStage.Focus();
                qualityCheck = false;
                lblWABStage.Style.Add("color", "red");
            }
            //WHOStage
            if (ddlWHOStage.SelectedItem.Text == "Select")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -WHO Stage";
                validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "\\n";
                ddlWHOStage.Focus();
                qualityCheck = false;
                lblWHOStage.Style.Add("color", "red");
            }
            //ARVDrugsAdhere
            if (ddlARVDrugsAdhere.SelectedItem.Text == "Select")
            {
                MsgBuilder msgBuilder = new MsgBuilder();
                msgBuilder.DataElements["Control"] = " -ARV Drugs Adhere";
                validateMessage += IQCareMsgBox.GetMessage("BlankList", msgBuilder, this) + "\\n";
                ddlARVDrugsAdhere.Focus();
                qualityCheck = false;
                lblARVDrugsAdhere.Style.Add("color", "red");
            }

            if (!qualityCheck)
            {
                MsgBuilder totalMsgBuilder = new MsgBuilder();
                totalMsgBuilder.DataElements["MessageText"] = validateMessage;
                IQCareMsgBox.Show("#C1", totalMsgBuilder, this);
            }
            return qualityCheck;
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            MsgBuilder theBuilder = new MsgBuilder();
            if (Request.QueryString["name"] == "Delete")
            {
                btnDataQualityCheck.Visible = false;
                btnSave.Text = "Delete";
                theBuilder.DataElements["FormName"] = "HIV Care/ART Encounter";
                IQCareMsgBox.ShowConfirm("DeleteForm", theBuilder, btnSave);
            }


            //GetValuefromHT = (Hashtable)Session["htPtnRegParameter"];
            Authentication = new AuthenticationManager();
            if (Authentication.HasFunctionRight(ApplicationAccess.HIVCareARTEncounter, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;

            }

            // Utility utils = new Utility();

            if (Authentication.HasFunctionRight(ApplicationAccess.HIVCareARTEncounter, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
            {
                btnSave.Enabled = false;
                btnDataQualityCheck.Enabled = false;
            }
            else if (Request.QueryString["name"] == "Delete")
            {
                if (Authentication.HasFunctionRight(ApplicationAccess.HIVCareARTEncounter, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    //int PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                    int PatientID = Convert.ToInt32(Session["PatientId"]);
                    string theUrl = "";
                    //theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmClinical_DeleteForm.aspx", PatientID, Request.QueryString["sts"].ToString());
                    theUrl = string.Format("{0}", "frmClinical_DeleteForm.aspx");
                    Response.Redirect(theUrl);
                }
                else if (Authentication.HasFunctionRight(ApplicationAccess.HIVCareARTEncounter, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                {
                    btnSave.Text = "Delete";
                    btnSave.Enabled = false;
                    btnDataQualityCheck.Visible = false;
                }
            }
            if (Session["CEndedStatus"] != null)
            {
                if (((DataTable)Session["CEndedStatus"]).Rows.Count > 0)
                {
                    if (((DataTable)Session["CEndedStatus"]).Rows[0]["CareEnded"].ToString() == "1")
                    {
                        btnSave.Enabled = false;
                        btnDataQualityCheck.Enabled = false;
                    }
                }
            }

            IQCareUtils iQCareUtils = new IQCareUtils();
            BindFunctions bindFunctions = new BindFunctions();

            //Get Sessions
            patientID = Convert.ToInt32(Session["PatientId"].ToString());
            locationID = Convert.ToInt32(Session["AppLocationId"].ToString());
            gender = Session["PatientSex"].ToString();
            ageInYear = (int)Convert.ToDouble(Session["PatientAge"].ToString());
            ageInMonth = Convert.ToInt32(Session["PatientAge"].ToString().Substring(Session["PatientAge"].ToString().IndexOf(".") + 1));


            //Header Texts
            //(Master.FindControl("lblformname") as Label).Text = "HIV Care/ART Encounter Form";
            //(Master.FindControl("lblRoot") as Label).Text = "Clinical Forms >>";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "HIV Care/ART Encounter";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "HIV Care/ART Encounter";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "HIV Care/ART Encounter Form";

            //UI Decision
            if (ageInYear <= 14)
            {
                //isAdult = false;

                divAdultPharmacy.Visible = true;
            }
            else
            {

                divAdultPharmacy.Visible = true;
            }
            if (Session["Paperless"].ToString() == "1")
            {
                //isAdult = false;
                divLabOrderTest.Visible = true;
                divLaboratory.Visible = false;
            }
            else
            {
                divLabOrderTest.Visible = false;
                divLaboratory.Visible = true;
            }

            if (ageInYear < 5)
            {
                tdOedema.Style.Remove("display");
                tdMUACAge.Style.Remove("display");
                txtAge.Text = ageInMonth.ToString();
                lblNutritionalProblems.Style.Remove("visibility");
                ddlNutritionalProblems.Style.Remove("visibility");
            }
            if (gender == "Male")
                tdPregnant.Style.Add("display", "none");

            if (gender == "Female" && ageInYear < 12)
            {
                tdPregnant.Style.Add("display", "none");
            }
            if (ageInYear < 2)
            {
                tdInfantFeedingPractice.Style.Remove("display");
            }
            if (gender == "Female" && ageInYear > 12)
            {
                tdInfantFeedingPractice.Style.Remove("display");
            }

            if (ageInYear < 12)
            {
                tdFamilyPlanning.Style.Add("display", "none");
            }

            AddUIAttributes();
            //  Show_Hide();
            //Auto Populate Fields
            HivCareARTEncounterInterface = (IHivCareARTEncounter)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BHivCareARTEncounter, BusinessProcess.Clinical");
            tempDataSet = HivCareARTEncounterInterface.GetHIVCareARTPatientFormData(patientID, locationID);
            tempDataTable = tempDataSet.Tables[0];




            if (tempDataTable.Rows.Count != 0)
            {
                if (tempDataTable.Rows[0]["ART"] != DBNull.Value)
                {
                    //txtARTStart.Text = (((DateTime.Now.Year - Convert.ToDateTime(tempDataTable.Rows[0]["ART"]).Year) * 12) +
                    //    (DateTime.Now.Month - Convert.ToDateTime(tempDataTable.Rows[0]["ART"]).Month)).ToString();

                    ArtStartinMonth = Convert.ToDateTime(tempDataTable.Rows[0]["ART"]).Year * 12 + Convert.ToDateTime(tempDataTable.Rows[0]["ART"]).Month;
                }
                else
                {
                    //txtARTStart.Text= "0";
                    ArtStartinMonth = 0;
                }
                if (tempDataTable.Rows[0]["REGIMEN"] != DBNull.Value)
                {
                    ArtRegimeninMonth = Convert.ToDateTime(tempDataTable.Rows[0]["REGIMEN"]).Year * 12 + Convert.ToDateTime(tempDataTable.Rows[0]["REGIMEN"]).Month;
                    //txtStartingCurrentRegimen.Text = (((DateTime.Now.Year - Convert.ToDateTime(tempDataTable.Rows[0]["REGIMEN"]).Year) * 12) +
                    //    (DateTime.Now.Month - Convert.ToDateTime(tempDataTable.Rows[0]["REGIMEN"]).Month)).ToString();
                }
                else
                {
                    ArtRegimeninMonth = 0;
                }
            }
            //this,'" + ArtRegimeninMonth +"','"+txtARTStart.ClientID+"');");

            txtVisitDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3');HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "');HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "');isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtVisitDate.ClientID + "', '" + txtVisitDate.ClientID + "');");
            txtVisitDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3');HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "');HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "')");
            // txtVisitDate.Attributes.Add("OnKeyup", "DateFormat(this,this.value,event,false,'3');HIVCareEncounterARTStartBrule(this,'" + ArtRegimeninMonth + "','" + txtARTStart.ClientID + "');");

            if ((Convert.ToInt32(ViewState["VisitID"]) > 1) && (!IsPostBack))
            {
                //    HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "');HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "')");

                string ArtinfoScript = "<script language = 'javascript' defer ='defer' id = 'BRuleArtInfo'>\n";
                ArtinfoScript += "HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "'); \n";
                ArtinfoScript += "HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "'); \n";
                ArtinfoScript += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "BRuleArtInfo", ArtinfoScript);
            }



            //Family Planning Status
            tempDataView = new DataView(tempDataSet.Tables[1]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                bindFunctions.BindCombo(ddlFamilyPanningStatus, tempDataTable, "Name", "ID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }

            //Family Planning Methods
            tempDataView = new DataView(tempDataSet.Tables[2]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                //  bindFunctions.BindCheckedList(cblFamilyPlanningMethod, tempDataTable, "Name", "ID");
                //cblFamilyPlanningMethod.DataBind = tempDataTable;
                //tempDataView.Dispose();
                //tempDataTable.Clear();

                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                theDTFamilyPlanning = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                BindFunctions theBindFunction = new BindFunctions();
                theBindFunction.CreateBlueCheckedList(PnlFamilyPlanningMethod, theDTFamilyPlanning, "", "");
                //int countFamilyPlanning = 0;
                //for (int i = 0; i < tempDataTable.Rows.Count; i++)
                //{
                //    HtmlTableRow tr = new HtmlTableRow();
                //    HtmlTableCell tc = new HtmlTableCell();
                //    HtmlInputCheckBox chkFamilyPlanning = new HtmlInputCheckBox();
                //    chkFamilyPlanning.ID = Convert.ToString("FamilyPlanning" + i);
                //    chkFamilyPlanning.Value = tempDataTable.Rows[i][1].ToString();
                //    tc.Controls.Add(chkFamilyPlanning);
                //    tc.Controls.Add(new LiteralControl(chkFamilyPlanning.Value));
                //    tr.Cells.Add(tc);
                //    //if (chkPotentialSideEffect.Value == "Other")
                //    //{
                //    //    HtmlTableCell tc1 = new HtmlTableCell();
                //    //    //tc1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' >"));
                //    //    tc1.Controls.Add(new LiteralControl("<SPAN id='otherPotentialSideEffect' style='DISPLAY:none'>Other (specify): "));
                //    //    HtmlInputText HTextRMissed = new HtmlInputText();
                //    //    HTextRMissed.ID = "txtotherPotential";
                //    //    HTextRMissed.Size = 5;
                //    //    tc1.Controls.Add(HTextRMissed);
                //    //    tc1.Controls.Add(new LiteralControl(HTextRMissed.Value));
                //    //    tc1.Controls.Add(new LiteralControl("</SPAN>"));
                //    //    tr.Cells.Add(tc1);
                //    //    chkPotentialSideEffect.Attributes.Add("onclick", "toggle('otherPotentialSideEffect');");
                //    //}
                //    countFamilyPlanning++;
                //    tbFamilyPlanningMethod.Rows.Add(tr);

                //}
                tempDataView.Dispose();
                tempDataTable.Clear();
            }




            //TB Status
            tempDataView = new DataView(tempDataSet.Tables[3]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                bindFunctions.BindCombo(ddlTBStatus, tempDataTable, "Name", "ID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }

            //Potential Side Effects
            tempDataView = new DataView(tempDataSet.Tables[4]);

            if (tempDataView.Table != null)
            {
                ////tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                ////bindFunctions.BindCheckedList(cblPotentialSideEffect, tempDataTable, "Name", "ID");
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                theDTpotentialSideEffect = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                BindFunctions theBindFunction = new BindFunctions();
                theBindFunction.CreateBlueCheckedList(PnlPotentialSideEffect, theDTpotentialSideEffect, "", "");

                tempDataView.Dispose();
                tempDataTable.Clear();
            }

            //New Ois
            tempDataView = new DataView(tempDataSet.Tables[5]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                theDTNewOisProblem = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);

                BindFunctions theBindFunction = new BindFunctions();
                theBindFunction.CreateBlueCheckedList(PnlNewOIsProblemsOther, theDTNewOisProblem, "", "");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }


            //Nutritional Problems
            tempDataView = new DataView(tempDataSet.Tables[6]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                bindFunctions.BindCombo(ddlNutritionalProblems, tempDataTable, "Name", "ID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }

            //WAB Stage
            tempDataView = new DataView(tempDataSet.Tables[7]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                bindFunctions.BindCombo(ddlWABStage, tempDataTable, "Name", "ID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }

            //WHO Stage
            tempDataView = new DataView(tempDataSet.Tables[8]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                bindFunctions.BindCombo(ddlWHOStage, tempDataTable, "Name", "ID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }

            //CPT Adhere
            tempDataView = new DataView(tempDataSet.Tables[9]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                bindFunctions.BindCombo(ddlCPTAdhere, tempDataTable, "Name", "ID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }

            //ARV Drugs Adhere
            tempDataView = new DataView(tempDataSet.Tables[10]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                bindFunctions.BindCombo(ddlARVDrugsAdhere, tempDataTable, "Name", "ID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }

            //Reason why ARV Drugs Poor of Fair
            tempDataView = new DataView(tempDataSet.Tables[11]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                bindFunctions.BindCombo(ddlReasonARVDrugsPoorFair, tempDataTable, "Name", "ID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }

            //Referred To
            tempDataView = new DataView(tempDataSet.Tables[12]);

            if (tempDataView.Table != null)
            {

                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                theDTReferTo = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);

                theDTReferTo = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                BindFunctions theBindFunction = new BindFunctions();
                theBindFunction.CreateBlueCheckedList(PnlReferredTo, theDTReferTo, "", "");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }


            //Nutritional Support
            tempDataView = new DataView(tempDataSet.Tables[13]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                bindFunctions.BindCombo(ddlNutritionalSupport, tempDataTable, "Name", "ID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }



            //Infant Feeding Option
            tempDataView = new DataView(tempDataSet.Tables[14]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                bindFunctions.BindCombo(ddlInfantFeedingPractice, tempDataTable, "Name", "ID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }

            //Attending Clinician
            tempDataView = new DataView(tempDataSet.Tables[15]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                DataColumn dataColumnFullName = new DataColumn("Name");
                dataColumnFullName.DataType = System.Type.GetType("System.String");
                tempDataTable.Columns.Add(dataColumnFullName);

                for (int i = 0; i < tempDataTable.Rows.Count; i++)
                    tempDataTable.Rows[i]["Name"] = tempDataTable.Rows[i]["FirstName"].ToString() + " " + tempDataTable.Rows[i]["LastName"].ToString();

                bindFunctions.BindCombo(ddlAttendingClinician, tempDataTable, "Name", "EmployeeID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }

            //Height
            tempDataView = new DataView(tempDataSet.Tables[16]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                if (tempDataTable.Rows.Count > 0 && ageInYear >= 15)
                    txtPhysHeight.Text = tempDataTable.Rows[0]["height"].ToString();
            }


            // tempDataView = new DataView(tempDataSet.Tables[17]);
            if (tempDataSet.Tables[17].Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(Convert.ToString(tempDataSet.Tables[17].Rows[0]["EmergContactName"])))
                    txtTreatmentSupporterName.Text = tempDataSet.Tables[17].Rows[0]["EmergContactName"].ToString();

                if (!String.IsNullOrEmpty(Convert.ToString(tempDataSet.Tables[17].Rows[0]["EmergContactPhone"])))
                    txtTreatmentSupporterContact.Text = tempDataSet.Tables[17].Rows[0]["EmergContactPhone"].ToString();
            }
            //Subsitution /Interruption
            tempDataView = new DataView(tempDataSet.Tables[18]);
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                bindFunctions.BindCombo(lstclinPlanFU, tempDataTable, "Name", "ID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }

            //PMTCT/ANC No.
            tempDataView = new DataView(tempDataSet.Tables[19]);
            if (tempDataView.Table != null)
            {
                if (!String.IsNullOrEmpty(Convert.ToString(tempDataSet.Tables[19].Rows[0]["ANCNumber"])))
                {
                    txtPMTCTANCNumber.Text = tempDataSet.Tables[19].Rows[0]["ANCNumber"].ToString();
                    tempDataView.Dispose();
                    tempDataTable.Clear();
                    txtPMTCTANCNumber.ReadOnly = true;
                }
                else
                {
                    txtPMTCTANCNumber.ReadOnly = false;
                }
                if (!String.IsNullOrEmpty(Convert.ToString(tempDataSet.Tables[19].Rows[0]["DOB"])))
                {
                    DOBofpatient = String.Format("{0:dd-MMM-yyyy}", tempDataSet.Tables[19].Rows[0]["DOB"]);
                    // DOBofpatient = Convert.ToString(Convert.ToString(tempDataSet.Tables[19].Rows[0]["DOB"]));
                    txtTBRxStart.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtVisitDate.ClientID + "', '" + txtTBRxStart.ClientID + "', '" + "TB Rx Start" + "', '" + txtTBRxStart.ClientID + "'); isCheckValidDate_MMM_YR_TBRxStart('" + DOBofpatient + "', '" + txtTBRxStart.ClientID + "', '" + "TB Rx Start" + "', '" + txtTBRxStart.ClientID + "')");
                    txtTBRxStop.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtVisitDate.ClientID + "', '" + txtTBRxStop.ClientID + "', '" + "TB Rx Stop" + "', '" + txtTBRxStop.ClientID + "');  isCheckValidDate_MMM_YR_TBRxSTOP('" + txtTBRxStart.ClientID + "', '" + txtTBRxStop.ClientID + "', '" + "TB Rx Stop" + "', '" + txtTBRxStop.ClientID + "')");
                }

            }

            //Therapy Change Codes
            tempDataView = new DataView(tempDataSet.Tables[20]);
            tempDataView.RowFilter = "CodeID='6' and ModuleId=" + Convert.ToInt32(Session["TechnicalAreaId"]);
            tempDataView.Sort = "ID ASC";
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                bindFunctions.BindCombo(ddlArvTherapyChangeCode, tempDataTable, "Name", "ID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }
            //Therapy Stop Codes
            tempDataView = new DataView(tempDataSet.Tables[20]);
            tempDataView.RowFilter = "CodeID='5' and ModuleId=" + Convert.ToInt32(Session["TechnicalAreaId"]);
            tempDataView.Sort = "ID ASC";
            if (tempDataView.Table != null)
            {
                tempDataTable = (DataTable)iQCareUtils.CreateTableFromDataView(tempDataView);
                bindFunctions.BindCombo(ddlArvTherapyStopCode, tempDataTable, "Name", "ID");
                tempDataView.Dispose();
                tempDataTable.Clear();
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["PatientVisitId"] != null)

            Maintain_ViewState(Convert.ToString(ddlArvTherapyChangeCode.SelectedItem), Convert.ToString(ddlArvTherapyStopCode.SelectedItem), Convert.ToInt32(lstclinPlanFU.SelectedValue), Convert.ToString(ddlARVDrugsAdhere.SelectedItem), Convert.ToString(ddlReasonARVDrugsPoorFair.SelectedItem), Convert.ToString(ddlFamilyPanningStatus.SelectedItem), Convert.ToString(ddlTBStatus.SelectedItem));
            Show_Hide();
            PutCustomControl();
            if (Convert.ToInt32(ViewState["VisitID"]) > 0)
            {
                isUpdate = true;
            }
            else
            {
                if (Convert.ToInt32(Session["PatientVisitIdhiv"]) > 0)
                {
                    Session["ArtEncounterPatientVisitId"] = Session["PatientVisitIdhiv"];
                    visitID = Convert.ToInt32(Session["PatientVisitIdhiv"]);

                    isUpdate = true;
                }
                else
                {
                    visitID = 0;
                    Session["ArtEncounterPatientVisitId"] = 0;
                }
                ViewState["VisitID"] = visitID;
            }



            if (!IsPostBack && Convert.ToInt32(ViewState["VisitID"]) > 0)
            {
                tempDataSet = HivCareARTEncounterInterface.GetHIVCareARTPatientVisitInfo(patientID, locationID, Convert.ToInt32(ViewState["VisitID"]));

                //VisitDate & Attending Clinican & dataQuality
                tempDataTable = tempDataSet.Tables[0];
                if (tempDataTable.Rows.Count != 0)
                {
                    if (tempDataTable.Rows[0]["visitDate"] != DBNull.Value)
                    {
                        txtVisitDate.Text = Convert.ToDateTime(tempDataTable.Rows[0]["visitDate"].ToString()).ToString(Session["AppDateFormat"].ToString());
                        string ArtinfoScript = "<script language = 'javascript' defer ='defer' id = 'BRuleArtInfo'>\n";
                        ArtinfoScript += "HIVCareEncounterARTStartBrule('" + txtVisitDate.ClientID + "','" + ArtStartinMonth + "','" + txtARTStart.ClientID + "'); \n";
                        ArtinfoScript += "HIVCareEncounterARTRegimeBrule('" + txtVisitDate.ClientID + "','" + ArtRegimeninMonth + "','" + txtStartingCurrentRegimen.ClientID + "'); \n";
                        ArtinfoScript += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "BRuleArtInfo", ArtinfoScript);

                    }

                    ViewState["createDate"] = tempDataTable.Rows[0]["createDate"].ToString();
                    if (tempDataTable.Rows[0]["dataQuality"] != System.DBNull.Value)
                    {
                        //ViewState["dataQuality"] = tempDataTable.Rows[0]["dataQuality"].ToString();
                        if (Convert.ToInt32(tempDataTable.Rows[0]["dataQuality"]) == 1)
                            btnDataQualityCheck.CssClass = "greenbutton";
                    }
                }

                //TreatmentSupporter Name & Contact
                tempDataTable = tempDataSet.Tables[1];
                if (tempDataTable.Rows.Count != 0)
                {
                    if (tempDataTable.Rows[0]["treatmentSupporterName"] != DBNull.Value)
                        txtTreatmentSupporterName.Text = tempDataTable.Rows[0]["treatmentSupporterName"].ToString();
                    if (tempDataTable.Rows[0]["treatmentSupporterContact"] != DBNull.Value)
                        txtTreatmentSupporterContact.Text = tempDataTable.Rows[0]["treatmentSupporterContact"].ToString();



                    if (tempDataTable.Rows[0]["TBRegNumber"] != DBNull.Value)
                        txtTBRegNumber.Value = tempDataTable.Rows[0]["TBRegNumber"].ToString();
                    if (tempDataTable.Rows[0]["nutritionalProblem"] != DBNull.Value)
                        ddlNutritionalProblems.SelectedValue = tempDataTable.Rows[0]["nutritionalProblem"].ToString();

                    if (tempDataTable.Rows[0]["attendingClinician"] != DBNull.Value)
                        ddlAttendingClinician.SelectedValue = tempDataTable.Rows[0]["attendingClinician"].ToString();


                    if (tempDataTable.Rows[0]["Scheduled"] != DBNull.Value)
                        chkifschedule.Checked = tempDataTable.Rows[0]["Scheduled"].ToString() == "1" ? true : false;


                }

                //Follow Up Date
                tempDataTable = tempDataSet.Tables[2];
                if (tempDataTable.Rows.Count != 0)
                    if (tempDataTable.Rows[0]["followUpDate"] != DBNull.Value)
                        txtFollowUpDate.Text = Convert.ToDateTime(tempDataTable.Rows[0]["followUpDate"].ToString()).ToString(Session["AppDateFormat"].ToString());

                //Height Weight Oedema
                tempDataTable = tempDataSet.Tables[3];
                if (tempDataTable.Rows.Count != 0)
                {
                    if (tempDataTable.Rows[0]["height"] != DBNull.Value)
                        txtPhysHeight.Text = tempDataTable.Rows[0]["height"].ToString();
                    if (tempDataTable.Rows[0]["weight"] != DBNull.Value)
                        txtPhysWeight.Text = tempDataTable.Rows[0]["weight"].ToString();
                    if (tempDataTable.Rows[0]["oedema"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(tempDataTable.Rows[0]["oedema"].ToString()) == 1)
                            rdoOedemaPlus.Checked = true;
                        else if (Convert.ToInt32(tempDataTable.Rows[0]["oedema"].ToString()) == 2)
                            rdoOedemaMinus.Checked = true;
                    }
                }
                string script;
                //	Pregnancy & EDD & DateOfDelivery & PMTCT & MUAC	
                tempDataTable = tempDataSet.Tables[4];
                if (tempDataTable.Rows.Count != 0)
                {
                    if (tempDataTable.Rows[0]["pregnant"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(tempDataTable.Rows[0]["pregnant"].ToString()) == 1)
                        {
                            rdoPregnantYes.Checked = true;
                            if (tempDataTable.Rows[0]["EDD"] != System.DBNull.Value)
                            {
                                this.txtEDD.Value = String.Format("{0:dd-MMM-yyyy}", tempDataTable.Rows[0]["EDD"]);
                            }
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
                            script += "show('rdopregnantyesno');\n";
                            script += "hide('spanEDDNopregnant');\n";
                            script += "show('spanEDD');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "PregnantYes", script);
                            ViewState["Pregstatus"] = "1";
                        }
                        else if (Convert.ToInt32(tempDataTable.Rows[0]["Pregnant"].ToString()) == 0)
                        {
                            this.rdoPregnantNo.Checked = true;
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'PregnantNo'>\n";
                            script += "show('rdopregnantyesno');\n";
                            script += "show('spanEDDNopregnant');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "PregnantNo", script);
                            ViewState["Pregstatus"] = "2";


                            if (tempDataTable.Rows[0]["deliveryDate"] != System.DBNull.Value)
                            {
                                this.txtDeliveryDate.Text = String.Format("{0:dd-MMM-yyyy}", tempDataTable.Rows[0]["deliveryDate"]);
                                cbPostPartum.Checked = true;
                            }
                            else
                            {
                                cbPostPartum.Checked = false;
                            }
                        }

                    }


                    if (tempDataTable.Rows[0]["PMTCT"] != DBNull.Value)
                    {
                        if (Convert.ToInt32(tempDataTable.Rows[0]["PMTCT"].ToString()) == 1)
                        {
                            this.rdoPMTCT.Checked = true;
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'PregnantPMTCT_0'>\n";
                            script += "show('spanpmctcancno');\n";

                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "PregnantPMTCT_0", script);
                        }
                        else
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'PregnantPMTCT_0'>\n";
                            script += "hide('spanpmctcancno');\n"; ;
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "PregnantPMTCT_0", script);
                        }
                    }



                    if (tempDataTable.Rows[0]["MUAC"] != DBNull.Value)
                        txtMUAC.Text = tempDataTable.Rows[0]["MUAC"].ToString();
                }

                //Gestation
                tempDataTable = tempDataSet.Tables[5];
                if (tempDataTable.Rows.Count != 0)
                    if (tempDataTable.Rows[0]["gestation"] != DBNull.Value)
                        txtGestation.Text = tempDataTable.Rows[0]["gestation"].ToString();

                //PMTCTANCNumber
                tempDataTable = tempDataSet.Tables[6];
                if (tempDataTable.Rows.Count != 0)
                    if (tempDataTable.Columns.Contains("PMTCTANCNumber"))
                        if (tempDataTable.Rows[0]["PMTCTANCNumber"] != DBNull.Value)
                            txtPMTCTANCNumber.Text = tempDataTable.Rows[0]["PMTCTANCNumber"].ToString();

                // txtMUAC.Text = tempDataTable.Rows[0]["PMTCTAMCNumber"].ToString();

                //Family Planning & NumOfDaysHospitalized & NutritionalSupport
                tempDataTable = tempDataSet.Tables[7];
                if (tempDataTable.Rows.Count != 0)
                {
                    if (tempDataTable.Rows[0]["familyPlanningStatus"] != DBNull.Value)
                        ddlFamilyPanningStatus.SelectedValue = tempDataTable.Rows[0]["familyPlanningStatus"].ToString();
                    if (tempDataTable.Rows[0]["numOfDaysHospitalized"] != DBNull.Value)
                        txtNumOfDaysHospitalized.Text = tempDataTable.Rows[0]["numOfDaysHospitalized"].ToString();

                    if (tempDataTable.Rows[0]["nutritionalSupport"] != DBNull.Value)
                        ddlNutritionalSupport.SelectedValue = tempDataTable.Rows[0]["nutritionalSupport"].ToString();


                    if (ddlFamilyPanningStatus.SelectedItem.Text.ToUpper() == "ONFP=ON FAMILY PLANNING")
                    {
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'FamilyPlanning_0'>\n";
                        script += "show('divFamilyPlanningMethod');\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "FamilyPlanning_0", script);
                        tempDataTable = tempDataSet.Tables[17];


                        if (tempDataTable.Rows.Count > 0)
                        {
                            FillCheckBoxListData(tempDataTable, PnlFamilyPlanningMethod, "familyPlanningMethodID", "");
                        }
                        else
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'FamilyPlanning_1'>\n";
                            script += "hide('divFamilyPlanningMethod');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "FamilyPlanning_1", script);
                        }
                    }
                }
                //TB Status		
                tempDataTable = tempDataSet.Tables[8];
                if (tempDataTable.Rows.Count != 0)
                {
                    if (tempDataTable.Rows[0]["TBStatus"] != DBNull.Value)
                    {
                        ddlTBStatus.SelectedValue = tempDataTable.Rows[0]["TBStatus"].ToString();

                        if (tempDataTable.Rows[0]["TBStatus"] != DBNull.Value)
                        {
                            ddlTBStatus.SelectedValue = tempDataTable.Rows[0]["TBStatus"].ToString();
                            if (ddlTBStatus.SelectedItem.Text.Trim() == "TB Rx")
                            {
                                if (tempDataTable.Rows[0]["TBRxStart"] != DBNull.Value)
                                    this.txtTBRxStart.Value = Convert.ToDateTime(tempDataTable.Rows[0]["TBRxStart"].ToString()).ToString("MMM-yyyy");

                                if (tempDataTable.Rows[0]["TBRxStop"] != DBNull.Value)
                                    txtTBRxStop.Value = Convert.ToDateTime(tempDataTable.Rows[0]["TBRxStop"].ToString()).ToString("MMM-yyyy");

                                script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'TBStatusRX_0'>\n";
                                script += "show('divTBStatusTBRX');\n";
                                script += "</script>\n";
                                ClientScript.RegisterStartupScript(this.GetType(), "TBStatusRX_0", script);
                            }
                            else
                            {
                                script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'TBStatusRX_0'>\n";
                                script += "hide('divTBStatusTBRX');\n";
                                script += "</script>\n";
                                ClientScript.RegisterStartupScript(this.GetType(), "TBStatusRX_0", script);
                            }

                        }
                    }

                }

                //  Subsitutions/Interruption
                tempDataTable = tempDataSet.Tables[16];
                if (tempDataTable.Rows.Count != 0)
                {
                    if (tempDataTable.Rows[0]["TherapyPlan"] != DBNull.Value)
                        lstclinPlanFU.SelectedValue = tempDataTable.Rows[0]["TherapyPlan"].ToString();
                    if (tempDataTable.Rows[0]["PrescribedARVStartDate"] != DBNull.Value)
                    {
                        // this.txt.Value = Convert.ToDateTime(tempDataTable.Rows[0]["PrescribedARVStartDate"].ToString()).ToString("MMM-yyyy");
                    }
                    if (tempDataTable.Rows[0]["TherapyReasonCode"] != System.DBNull.Value)
                    {

                        if (this.lstclinPlanFU.SelectedValue == "98")
                        {
                            this.ddlArvTherapyChangeCode.SelectedValue = tempDataTable.Rows[0]["TherapyReasonCode"].ToString();
                            this.txtarvTherapyChangeCodeOtherName.Value = tempDataTable.Rows[0]["TherapyOther"].ToString();
                            if (this.ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
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
                        if (this.lstclinPlanFU.SelectedValue == "99")
                        {
                            this.ddlArvTherapyStopCode.SelectedValue = tempDataTable.Rows[0]["THerapyReasonCOde"].ToString();
                            this.txtarvTherapyStopCodeOtherName.Value = tempDataTable.Rows[0]["TherapyOther"].ToString();
                            DateTime theTmpDtTherapy = Convert.ToDateTime(tempDataTable.Rows[0]["PrescribedARVStartDate"]);
                            this.txtARTEndeddate.Value = theTmpDtTherapy.ToString(Session["AppDateFormat"].ToString());

                            if (this.ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other"))
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
                tempDataTable = tempDataSet.Tables[9];
                if (tempDataTable.Rows.Count != 0)
                {

                }
                if (tempDataTable.Rows.Count > 0)
                {
                    FillCheckBoxListData(tempDataTable, PnlNewOIsProblemsOther, "newOIsProblemID", "newOIsProblemOther");

                }

                tempDataTable = tempDataSet.Tables[10];

                if (tempDataTable.Rows.Count > 0)
                {
                    FillCheckBoxListData(tempDataTable, PnlPotentialSideEffect, "potentialSideEffectID", "potentialSideEffectOther");

                }


                //  cblPotentialSideEffect_SelectedIndexChanged(null, null);
                //11 WAB Stage & WHO Stage
                tempDataTable = tempDataSet.Tables[11];
                if (tempDataTable.Rows.Count != 0)
                {
                    if (tempDataTable.Rows[0]["WABStage"] != DBNull.Value)
                        ddlWABStage.SelectedValue = tempDataTable.Rows[0]["WABStage"].ToString();
                    if (tempDataTable.Rows[0]["WHOStage"] != DBNull.Value)
                        ddlWHOStage.SelectedValue = tempDataTable.Rows[0]["WHOStage"].ToString();
                }

                //12 CPTAdhere
                tempDataTable = tempDataSet.Tables[12];
                if (tempDataTable.Rows.Count != 0)
                    if (tempDataTable.Rows[0]["CPTAdhere"] != DBNull.Value)
                        ddlCPTAdhere.SelectedValue = tempDataTable.Rows[0]["CPTAdhere"].ToString();

                //13 ARV Drugs Adhere + Reason
                tempDataTable = tempDataSet.Tables[13];
                if (tempDataTable.Rows.Count != 0)
                {
                    if (tempDataTable.Rows[0]["ARVDrugsAdhere"] != DBNull.Value)
                    {
                        ddlARVDrugsAdhere.SelectedValue = tempDataTable.Rows[0]["ARVDrugsAdhere"].ToString();
                        // ddlARVDrugsAdhere.Attributes.Add("onchange", "ARVDrugsPoorFairBRule(this.options[this.selectedIndex].text,'" + ddlReasonARVDrugsPoorFair.ClientID + "');HIVcareArtPoorFairOtherBRule('" + ddlReasonARVDrugsPoorFair.ClientID + "','Other','divReasonARVDrugsother');");
                        if (ddlARVDrugsAdhere.SelectedItem.Text == "F=Fair" || ddlARVDrugsAdhere.SelectedItem.Text == "P=Poor")
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsAdhere_0'>\n";
                            script += "HIVcareArtEnableARVDrugsPoor('" + ddlReasonARVDrugsPoorFair.ClientID + "','0');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "ARVDrugsAdhere_0", script);
                        }
                        else
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsAdhere_1'>\n";
                            // script += "DisableDIVallControl('divARVDrugsPoorFair');\n";
                            script += "HIVcareArtDisableARVDrugsPoor('" + ddlReasonARVDrugsPoorFair.ClientID + "''0');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "ARVDrugsAdhere_1", script);
                        }
                    }
                    if (tempDataTable.Rows[0]["reasonARVDrugsPoorFair"] != DBNull.Value)
                    {
                        ddlReasonARVDrugsPoorFair.SelectedValue = tempDataTable.Rows[0]["reasonARVDrugsPoorFair"].ToString();
                        if (ddlReasonARVDrugsPoorFair.SelectedItem.Text.Contains("Other"))
                        {
                            //tdReasonARVDrugsPoorFairOther
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsPoorFairOther_0'>\n";
                            script += "show('divReasonARVDrugsother');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "ARVDrugsPoorFairOther_0", script);
                        }
                        else
                        {
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsPoorFairOther_1'>\n";
                            script += "hide('divReasonARVDrugsother');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "ARVDrugsPoorFairOther_1", script);
                        }
                    }
                    if (tempDataTable.Rows[0]["reasonARVDrugsPoorFairOther"] != DBNull.Value)
                        txtReasonARVDrugsPoorFairOther.Text = tempDataTable.Rows[0]["reasonARVDrugsPoorFairOther"].ToString();
                }

                tempDataTable = tempDataSet.Tables[14];

                if (tempDataTable.Rows.Count > 0)
                {
                    FillCheckBoxListData(tempDataTable, PnlReferredTo, "referredTo", "referredToOther");

                }

                tempDataTable = tempDataSet.Tables[15];
                if (tempDataTable.Rows.Count != 0)
                    if (tempDataTable.Rows[0]["infantFeedingOption"] != DBNull.Value)
                        ddlInfantFeedingPractice.SelectedValue = tempDataTable.Rows[0]["infantFeedingOption"].ToString();

                PId = Convert.ToInt32(Session["PatientId"]);

                FillOldData(PId);

            }
        }
        #region "Added New Functions-07-June-2012"



        private void FillCheckBoxListData(DataTable theDT, Panel thePnl, string FieldName, string theFieldName)
        {

            foreach (DataRow DR in theDT.Rows)
            {
                foreach (Control y in thePnl.Controls)
                {

                    if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                    {
                        if (y.ID.StartsWith("Pnl"))
                        {
                            FillCheckBoxListData(theDT, (System.Web.UI.WebControls.Panel)y, FieldName, theFieldName);
                        }
                    }

                    else
                    {
                        if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                        {
                            string[] theControlId = ((CheckBox)y).ID.ToString().Split('-');
                            if (((CheckBox)y).ID == "Chk-" + DR[FieldName].ToString() + "-" + theControlId[2].ToString())
                                ((CheckBox)y).Checked = true;
                        }
                        if (y.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                        {
                            if (theFieldName != "")
                            {
                                string[] theControlId;
                                if (((System.Web.UI.WebControls.TextBox)y).ID.Contains("OtherTXT") == true)
                                {
                                    theControlId = ((TextBox)y).ID.ToString().Split('-');
                                    ((TextBox)y).Text = DR[theFieldName].ToString();

                                }
                                string script = "";
                                script = "<script language = 'javascript' defer ='defer' id = " + ((TextBox)y).ID + ">\n";
                                script += "show('txt" + (((TextBox)y).ID.ToString().Split('-')[1]).ToString() + "');\n";
                                script += "</script>\n";
                                ClientScript.RegisterStartupScript(this.GetType(), "" + ((TextBox)y).ID + "", script);
                            }



                        }
                    }

                }

            }
        }

        private void ShowhideTextbox(Panel thePnl)
        {
            foreach (Control y in thePnl.Controls)
            {

                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                {
                    if (y.ID.StartsWith("Pnl"))
                    {
                        ShowhideTextbox((System.Web.UI.WebControls.Panel)y);
                    }
                }

                else
                {
                    if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                    {
                        string[] theControlId = ((CheckBox)y).ID.ToString().Split('-');
                        if (((CheckBox)y).Text.Contains("Other") && ((CheckBox)y).Checked == true)
                        {
                            //((CheckBox)y).Checked = true;
                            string script = "";
                            script = "<script language = 'javascript' defer ='defer' id = " + ((CheckBox)y).ID + ">\n";
                            script += "show('txt" + (((CheckBox)y).ID.ToString().Split('-')[1]).ToString() + "');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "" + ((CheckBox)y).ID + "", script);
                        }
                    }

                }

            }


        }

        private DataTable GetCheckBoxListcheckedIDs(Panel thePnl, string FieldName, string thetxtFieldName, int Flag)
        {

            string chktrueother = "";
            int chktrueothervalue = 0;
            if (Flag == 0)
            {
                DTCheckedIds = new DataTable();

                if (DTCheckedIds.Columns.Contains(FieldName) == false && DTCheckedIds.Columns.Contains(FieldName) == false)
                {
                    DataColumn dataColumnPotentialSideEffect = new DataColumn(FieldName);
                    dataColumnPotentialSideEffect.DataType = System.Type.GetType("System.Int32");
                    DTCheckedIds.Columns.Add(dataColumnPotentialSideEffect);
                    if (thetxtFieldName != "")
                    {
                        DataColumn thepotentialSideEffect_Other = new DataColumn(thetxtFieldName);
                        thepotentialSideEffect_Other.DataType = System.Type.GetType("System.String");
                        DTCheckedIds.Columns.Add(thepotentialSideEffect_Other);
                    }

                }

            }
            DataRow theDR;
            foreach (Control y in thePnl.Controls)
            {
                if (y.GetType() == typeof(System.Web.UI.WebControls.Panel))
                    GetCheckBoxListcheckedIDs((System.Web.UI.WebControls.Panel)y, FieldName, thetxtFieldName, 1);

                else
                {

                    if (y.GetType() == typeof(System.Web.UI.WebControls.CheckBox))
                    {
                        if (((CheckBox)y).Checked == true)
                        {

                            string[] theControlId = ((CheckBox)y).ID.ToString().Split('-');
                            //theDR[FieldName] = theControlId[1].ToString();
                            if (theControlId[2].ToString().Contains("Other") == true)
                            {
                                chktrueother = theControlId[2].ToString();
                                chktrueothervalue = Convert.ToInt32(theControlId[1].ToString());
                            }
                            else
                            {
                                theDR = DTCheckedIds.NewRow();
                                theDR[FieldName] = theControlId[1].ToString();
                                DTCheckedIds.Rows.Add(theDR);
                            }

                        }

                    }
                    if (y.GetType() == typeof(System.Web.UI.WebControls.TextBox))
                    {
                        if (thetxtFieldName != "")
                        {
                            if (((System.Web.UI.WebControls.TextBox)y).ID.Contains("OtherTXT") == true)
                            {
                                theDR = DTCheckedIds.NewRow();
                                string[] theControlId = ((TextBox)y).ID.ToString().Split('-');
                                theDR[FieldName] = chktrueothervalue.ToString();
                                theDR[thetxtFieldName] = ((TextBox)y).Text;
                                DTCheckedIds.Rows.Add(theDR);

                            }
                            string script = "";
                            script = "<script language = 'javascript' defer ='defer' id = " + ((TextBox)y).ID + ">\n";
                            script += "show('txt" + chktrueothervalue.ToString() + "');\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "" + ((TextBox)y).ID + "", script);
                        }

                    }
                    //DTCheckedIds.Rows.Add(theDR);
                }
            }
            return DTCheckedIds;

        }
        #endregion
        private void save(bool isDataQuailtyChecked)
        {
            if (fieldValidation() == false)
            { return; }

            hashTable = new Hashtable();
            dataSetForSaving = new DataSet();
            HivCareARTEncounterInterface = (IHivCareARTEncounter)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BHivCareARTEncounter, BusinessProcess.Clinical");
            //Appointment Scheduling
            hashTable.Add("patientID", patientID.ToString());
            if (!isDataQuailtyChecked)
                hashTable.Add("dataQuality", "0");
            else
                hashTable.Add("dataQuality", "1");
            hashTable.Add("locationID", locationID.ToString());
            hashTable.Add("visitDate", txtVisitDate.Text);
            hashTable.Add("UserID", Convert.ToInt32(Session["AppUserId"].ToString()));

            if (chkifschedule.Checked == true)
            {
                hashTable.Add("Scheduled", 1);
            }
            else
            {
                hashTable.Add("Scheduled", 0);
            }

            hashTable.Add("treatmentSupporterName", txtTreatmentSupporterName.Text);
            hashTable.Add("treatmentSupporterContact", txtTreatmentSupporterContact.Text);
            hashTable.Add("followUpDate", txtFollowUpDate.Text);

            //Clinical Status
            hashTable.Add("height", txtPhysHeight.Text);
            hashTable.Add("weight", txtPhysWeight.Text);
            hashTable.Add("oedema", oedema.ToString());

            hashTable.Add("pregnant", pregnant.ToString());
            hashTable.Add("EDD", txtEDD.Value);
            hashTable.Add("gestation", txtGestation.Text);
            hashTable.Add("PMTCT", PMTCT.ToString());
            hashTable.Add("PMTCTANCNumber", txtPMTCTANCNumber.Text);
            hashTable.Add("deliveryDate", txtDeliveryDate.Text);

            //if (pregnant == 1)
            //{
            //    hashTable.Add("PostPartum", "");
            //}
            //else
            //{
            //    hashTable.Add("PostPartum", (cbPostPartum.Checked==true)?1:0);
            //}



            //Family Planning
            hashTable.Add("familyPlanningStatus", ddlFamilyPanningStatus.SelectedValue);
            //Table 0-Family Planning Methods
            DataTable dataTableFamilyPlanningMethods = new DataTable();
            DataColumn dataColumnFamilyPlanningMethods = new DataColumn("familyPlanningMethodID");
            dataColumnFamilyPlanningMethods.DataType = System.Type.GetType("System.Int32");
            dataTableFamilyPlanningMethods.Columns.Add(dataColumnFamilyPlanningMethods);
            /// DataRow dataRowFamilyPlanningMethods;

            if (ddlFamilyPanningStatus.SelectedItem.Text.ToUpper() == "ONFP=ON FAMILY PLANNING")
            {
                dataTableFamilyPlanningMethods = GetCheckBoxListcheckedIDs(PnlFamilyPlanningMethod, "familyPlanningMethodID", "", 0);

            }
            dataSetForSaving.Tables.Add(dataTableFamilyPlanningMethods);

            //MUAC
            hashTable.Add("MUAC", txtMUAC.Text);

            //TB Rx
            hashTable.Add("TBStatus", ddlTBStatus.SelectedValue);
            hashTable.Add("TBRxStart", txtTBRxStart.Value);
            hashTable.Add("TBRxStop", txtTBRxStop.Value);
            hashTable.Add("TBRegNumber", txtTBRegNumber.Value);

            //Subsitutions/Interruption
            hashTable.Add("TherapyPlan", lstclinPlanFU.SelectedValue);


            string OtherReason = "";
            // int ReasonID;

            string ARVTherapyCode = "0";
            string artendeddate = "";
            if (lstclinPlanFU.SelectedValue == "98")
            {
                ARVTherapyCode = ddlArvTherapyChangeCode.SelectedValue;
                if (ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
                {
                    OtherReason = txtarvTherapyChangeCodeOtherName.Value;
                }
            }
            else if (lstclinPlanFU.SelectedValue == "99")
            {

                ARVTherapyCode = ddlArvTherapyStopCode.SelectedValue;
                artendeddate = txtARTEndeddate.Value;
                if (ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other"))
                {
                    OtherReason = txtarvTherapyStopCodeOtherName.Value;
                }

            }
            if (lstclinPlanFU.SelectedValue.ToString() == "96")
            {
                Session["ARTEndedStatus"] = "";
            }
            else if (lstclinPlanFU.SelectedValue.ToString() == "99")
            {
                Session["ARTEndedStatus"] = "ART Stopped";
            }
            hashTable.Add("PrescribedARVStartDate", artendeddate);
            hashTable.Add("TherapyReasonCode", ARVTherapyCode);
            hashTable.Add("TherapyOther", OtherReason);


            DataTable dataTablePotentialSideEffect = new DataTable();
            dataTablePotentialSideEffect = GetCheckBoxListcheckedIDs(PnlPotentialSideEffect, "potentialSideEffectID", "potentialSideEffect_Other", 0);
            dataSetForSaving.Tables.Add(dataTablePotentialSideEffect);



            DataTable dataTableNewOIsProblems = new DataTable();
            dataTableNewOIsProblems = GetCheckBoxListcheckedIDs(PnlNewOIsProblemsOther, "newOIsProblemID", "newOIsProblemID_Other", 0);
            dataSetForSaving.Tables.Add(dataTableNewOIsProblems);

            //Nutritional Problems
            hashTable.Add("nutritionalProblem", ddlNutritionalProblems.SelectedValue);
            //WAB Stage
            hashTable.Add("WABStage", ddlWABStage.SelectedValue);
            //WHO Stage
            hashTable.Add("WHOStage", ddlWHOStage.SelectedValue);
            //CPT Adhere
            hashTable.Add("CPTAdhere", ddlCPTAdhere.SelectedValue);
            //ARV Drugs Adhere
            hashTable.Add("ARVDrugsAdhere", ddlARVDrugsAdhere.SelectedValue);
            //Reason why ARV Drugs are poor or fair
            hashTable.Add("reasonARVDrugsPoorFair", ddlReasonARVDrugsPoorFair.SelectedValue);
            //Other Reason why ARV Drugs are poor or fair
            hashTable.Add("reasonARVDrugsPoorFairOther", txtReasonARVDrugsPoorFairOther.Text);

            DataTable dataTableReferredTo = new DataTable();
            dataTableReferredTo = GetCheckBoxListcheckedIDs(PnlReferredTo, "referredToID", "referredToOtherID_Other", 0);
            dataSetForSaving.Tables.Add(dataTableReferredTo);
            //If Hospitalized # of Days
            hashTable.Add("numOfDaysHospitalized", txtNumOfDaysHospitalized.Text);
            //Nutritional Support
            hashTable.Add("nutritionalSupport", ddlNutritionalSupport.SelectedValue);
            //Infant Feeding Option
            hashTable.Add("infantFeedingOption", ddlInfantFeedingPractice.SelectedValue);
            //Name of attending clinician
            hashTable.Add("attendingClinician", ddlAttendingClinician.SelectedValue);

            //CreateDate
            if (isUpdate)
            {
                hashTable.Add("createDate", Session["visitdatenewhiv"].ToString());
                hashTable.Add("visitID", Convert.ToInt32(Session["PatientVisitIdhiv"]));
            }


            DataTable theCustomDataDT = null;
            if ((Convert.ToInt32(Session["PatientVisitIdhiv"]) > 0))
            {
                //   visitPK = Convert.ToInt32(Session["PatientVisitId"]);

                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);

            }
            else
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);

            }




            DataSet dsreturn = HivCareARTEncounterInterface.SaveUpdateHIVCareARTPatientVisit(hashTable, dataSetForSaving, isUpdate, theCustomDataDT);
            Session["PatientVisitIdhiv"] = Convert.ToInt32(dsreturn.Tables[0].Rows[0]["visitID"].ToString());
            ViewState["VisitID"] = Session["PatientVisitIdhiv"];
            Session["visitdatenewhiv"] = Convert.ToDateTime(dsreturn.Tables[0].Rows[0]["VisitDate"].ToString());
            ViewState["VisitDate"] = Convert.ToDateTime(dsreturn.Tables[0].Rows[0]["VisitDate"].ToString());
            Session["ServiceLocationId"] = Convert.ToInt32(dsreturn.Tables[0].Rows[0]["LocationID"].ToString());

            hashTable.Clear();
            closeWindow();
        }
        private void save(bool isDataQuailtyChecked, string button)
        {
            if (fieldValidation() == false)
            { return; }

            hashTable = new Hashtable();
            dataSetForSaving = new DataSet();
            HivCareARTEncounterInterface = (IHivCareARTEncounter)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BHivCareARTEncounter, BusinessProcess.Clinical");
            //Appointment Scheduling
            hashTable.Add("patientID", patientID.ToString());
            if (!isDataQuailtyChecked)
                hashTable.Add("dataQuality", "0");
            else
                hashTable.Add("dataQuality", "1");
            hashTable.Add("locationID", locationID.ToString());
            hashTable.Add("visitDate", txtVisitDate.Text);
            hashTable.Add("UserID", Convert.ToInt32(Session["AppUserId"].ToString()));

            if (chkifschedule.Checked == true)
            {
                hashTable.Add("Scheduled", 1);
            }
            else
            {
                hashTable.Add("Scheduled", 0);
            }

            hashTable.Add("treatmentSupporterName", txtTreatmentSupporterName.Text);
            hashTable.Add("treatmentSupporterContact", txtTreatmentSupporterContact.Text);
            hashTable.Add("followUpDate", txtFollowUpDate.Text);

            //Clinical Status
            hashTable.Add("height", txtPhysHeight.Text);
            hashTable.Add("weight", txtPhysWeight.Text);
            hashTable.Add("oedema", oedema.ToString());

            hashTable.Add("pregnant", pregnant.ToString());
            hashTable.Add("EDD", txtEDD.Value);
            hashTable.Add("gestation", txtGestation.Text);
            hashTable.Add("PMTCT", PMTCT.ToString());
            hashTable.Add("PMTCTANCNumber", txtPMTCTANCNumber.Text);
            hashTable.Add("deliveryDate", txtDeliveryDate.Text);




            //Family Planning
            hashTable.Add("familyPlanningStatus", ddlFamilyPanningStatus.SelectedValue);
            //Table 0-Family Planning Methods
            DataTable dataTableFamilyPlanningMethods = new DataTable();
            DataColumn dataColumnFamilyPlanningMethods = new DataColumn("familyPlanningMethodID");
            dataColumnFamilyPlanningMethods.DataType = System.Type.GetType("System.Int32");
            dataTableFamilyPlanningMethods.Columns.Add(dataColumnFamilyPlanningMethods);


            if (ddlFamilyPanningStatus.SelectedItem.Text.ToUpper() == "ONFP=ON FAMILY PLANNING")
            {
                dataTableFamilyPlanningMethods = GetCheckBoxListcheckedIDs(PnlFamilyPlanningMethod, "familyPlanningMethodID", "", 0);

            }
            dataSetForSaving.Tables.Add(dataTableFamilyPlanningMethods);

            //MUAC
            hashTable.Add("MUAC", txtMUAC.Text);

            //TB Rx
            hashTable.Add("TBStatus", ddlTBStatus.SelectedValue);
            hashTable.Add("TBRxStart", txtTBRxStart.Value);
            hashTable.Add("TBRxStop", txtTBRxStop.Value);
            hashTable.Add("TBRegNumber", txtTBRegNumber.Value);

            //Subsitutions/Interruption
            hashTable.Add("TherapyPlan", lstclinPlanFU.SelectedValue);


            string OtherReason = "";
            // int ReasonID;

            string ARVTherapyCode = "0";
            string artendeddate = "";
            if (lstclinPlanFU.SelectedValue == "98")
            {
                ARVTherapyCode = ddlArvTherapyChangeCode.SelectedValue;
                if (ddlArvTherapyChangeCode.SelectedItem.Text.Contains("Other"))
                {
                    OtherReason = txtarvTherapyChangeCodeOtherName.Value;
                }
            }
            else if (lstclinPlanFU.SelectedValue == "99")
            {

                ARVTherapyCode = ddlArvTherapyStopCode.SelectedValue;
                artendeddate = txtARTEndeddate.Value;
                if (ddlArvTherapyStopCode.SelectedItem.Text.Contains("Other"))
                {
                    OtherReason = txtarvTherapyStopCodeOtherName.Value;
                }

            }
            if (lstclinPlanFU.SelectedValue.ToString() == "96")
            {
                Session["ARTEndedStatus"] = "";
            }
            else if (lstclinPlanFU.SelectedValue.ToString() == "99")
            {
                Session["ARTEndedStatus"] = "ART Stopped";
            }
            hashTable.Add("PrescribedARVStartDate", artendeddate);
            hashTable.Add("TherapyReasonCode", ARVTherapyCode);
            hashTable.Add("TherapyOther", OtherReason);


            DataTable dataTablePotentialSideEffect = new DataTable();
            dataTablePotentialSideEffect = GetCheckBoxListcheckedIDs(PnlPotentialSideEffect, "potentialSideEffectID", "potentialSideEffect_Other", 0);
            dataSetForSaving.Tables.Add(dataTablePotentialSideEffect);



            DataTable dataTableNewOIsProblems = new DataTable();
            dataTableNewOIsProblems = GetCheckBoxListcheckedIDs(PnlNewOIsProblemsOther, "newOIsProblemID", "newOIsProblemID_Other", 0);
            dataSetForSaving.Tables.Add(dataTableNewOIsProblems);

            //Nutritional Problems
            hashTable.Add("nutritionalProblem", ddlNutritionalProblems.SelectedValue);
            //WAB Stage
            hashTable.Add("WABStage", ddlWABStage.SelectedValue);
            //WHO Stage
            hashTable.Add("WHOStage", ddlWHOStage.SelectedValue);
            //CPT Adhere
            hashTable.Add("CPTAdhere", ddlCPTAdhere.SelectedValue);
            //ARV Drugs Adhere
            hashTable.Add("ARVDrugsAdhere", ddlARVDrugsAdhere.SelectedValue);
            //Reason why ARV Drugs are poor or fair
            hashTable.Add("reasonARVDrugsPoorFair", ddlReasonARVDrugsPoorFair.SelectedValue);
            //Other Reason why ARV Drugs are poor or fair
            hashTable.Add("reasonARVDrugsPoorFairOther", txtReasonARVDrugsPoorFairOther.Text);
            //Referred To
            DataTable dataTableReferredTo = new DataTable();
            dataTableReferredTo = GetCheckBoxListcheckedIDs(PnlReferredTo, "referredToID", "referredToOtherID_Other", 0);
            dataSetForSaving.Tables.Add(dataTableReferredTo);


            //If Hospitalized # of Days
            hashTable.Add("numOfDaysHospitalized", txtNumOfDaysHospitalized.Text);
            //Nutritional Support
            hashTable.Add("nutritionalSupport", ddlNutritionalSupport.SelectedValue);
            //Infant Feeding Option
            hashTable.Add("infantFeedingOption", ddlInfantFeedingPractice.SelectedValue);
            //Name of attending clinician
            hashTable.Add("attendingClinician", ddlAttendingClinician.SelectedValue);

            //CreateDate
            if (isUpdate)
            {
                hashTable.Add("createDate", Session["visitdatenewhiv"].ToString());
                hashTable.Add("visitID", Convert.ToInt32(Session["PatientVisitIdhiv"]));
            }


            DataTable theCustomDataDT = null;
            if ((Convert.ToInt32(Session["PatientVisitIdhiv"]) > 0))
            {
                //   visitPK = Convert.ToInt32(Session["PatientVisitId"]);

                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);

            }
            else
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.PriorARTHIVCare, (DataSet)ViewState["CustomFieldsDS"]);

            }




            DataSet dsreturn = HivCareARTEncounterInterface.SaveUpdateHIVCareARTPatientVisit(hashTable, dataSetForSaving, isUpdate, theCustomDataDT);
            Session["PatientVisitIdhiv"] = Convert.ToInt32(dsreturn.Tables[0].Rows[0]["visitID"].ToString());
            ViewState["VisitID"] = Session["PatientVisitIdhiv"];
            Session["visitdatenewhiv"] = Convert.ToDateTime(dsreturn.Tables[0].Rows[0]["VisitDate"].ToString());
            ViewState["VisitDate"] = Convert.ToDateTime(dsreturn.Tables[0].Rows[0]["VisitDate"].ToString());
            Session["ServiceLocationId"] = Convert.ToInt32(dsreturn.Tables[0].Rows[0]["LocationID"].ToString());

            hashTable.Clear();
            string script = string.Empty;

            if (button == "Pharmacy")
            {
                script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
                script += "fnPageOpen('Pharmacy');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "pharmacy", script);
            }
            else if (button == "Labratory")
            {
                script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
                script += "fnPageOpen('Labratory');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "pharmacy", script);
            }
            else if (button == "LabTest")
            {
                script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
                script += "fnPageOpen('LabTest');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "pharmacy", script);
            }
        }
        #region UI Control Event Handlers


        private void closeWindow()
        {
            Session["ArtEncounterPatientVisitId"] = 0;
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Form saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='frmPatient_Home.aspx?PatientId=" + Session["PatientId"].ToString() + "';\n";
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href='frmClinical_HIVCareARTCardEncounter.aspx';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }
        protected void btnPrescribeDrugs_Click(Object sender, EventArgs e)
        {
        }

        protected void btnSave_Click(Object sender, EventArgs e)
        {
            if (Request.QueryString["name"] == "Delete")
            {
                DeleteForm();
            }

            save(false);
        }
        protected void btnDataQualityCheck_Click(Object sender, EventArgs e)
        {
            if (dataQualityCheck())
            {
                //ViewState["dataQuality"] = "1";
                btnDataQualityCheck.CssClass = "greenButton";
                save(true);
            }
            else
            {
                //ViewState["dataQuality"] = "0";
                btnDataQualityCheck.CssClass = "";
            }
        }
        private void DeleteForm()
        {
            Session["ArtEncounterPatientVisitId"] = 0;
            IHivCareARTEncounter CareARTEncounter;
            int theResultRow, OrderNo;
            string FormName;
            OrderNo = Convert.ToInt32(Session["PatientVisitIdhiv"].ToString());
            FormName = "HIV Care/ART Encounter";

            CareARTEncounter = (IHivCareARTEncounter)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BHivCareARTEncounter, BusinessProcess.Clinical");
            theResultRow = (int)CareARTEncounter.DeleteHIVCareEncounterForms(FormName, OrderNo, Convert.ToInt32(Session["PatientId"].ToString()), Convert.ToInt32(Session["AppUserId"].ToString()));
            if (theResultRow == 0)
            {
                IQCareMsgBox.Show("RemoveFormError", this);
                return;
            }
            else
            {
                string theUrl;
                theUrl = string.Format("{0}?PatientId={1}", "frmPatient_Home.aspx", Convert.ToInt32(Session["PatientVisitIdhiv"]));
                Response.Redirect(theUrl);
            }

        }
        protected void btnClose_Click(Object sender, EventArgs e)
        {
            //Session["PatientVisitId"]
            if (Request.QueryString["name"] == "Add" && Convert.ToInt32(ViewState["VisitID"]) > 0)
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
            Session["ArtEncounterPatientVisitId"] = 0;
        }
        //protected void btnPrint_Click(Object sender, EventArgs e)
        //{
        //}

        private void Maintain_ViewState(string ArvTherapyChangeCode, string ArvTherapyStopCode, int clinPlanFU, string ARVDrugsAdhereFairPoor, string ReasonARVDrugsPoorFair, string FamilyPanningStatus, string TBStatus)
        {
            if (clinPlanFU == 98) ViewState["ChangeRegimen"] = "1";

            if (clinPlanFU == 99) ViewState["StopRegimen"] = "1";

            if (ArvTherapyChangeCode == "Other") ViewState["changeROther"] = "1";

            if (ArvTherapyStopCode == "Other") ViewState["stopROther"] = "1";

            if ((ARVDrugsAdhereFairPoor == "F=Fair") || (ARVDrugsAdhereFairPoor == "P=Poor")) ViewState["ARVDrugsAdhereFairPoor"] = "1";

            if (ReasonARVDrugsPoorFair == "Other") ViewState["ReasonARVDrugsPoorFair"] = "1";

            if (FamilyPanningStatus.ToUpper() == "ONFP=ON FAMILY PLANNING") ViewState["FamilyPanningStatus"] = "1";

            if (TBStatus == "TB Rx") ViewState["TBStatusTBRx"] = "1";

        }
        private void Show_Hide()
        {
            string script;
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
            if (ViewState["ARVDrugsAdhereFairPoor"].ToString() == "1")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsAdhere_00'>\n";
                script += "HIVcareArtEnableARVDrugsPoor('" + ddlReasonARVDrugsPoorFair.ClientID + "','0');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ARVDrugsAdhere_00", script);
            }
            else
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsAdhere_11'>\n";
                // script += "DisableDIVallControl('divARVDrugsPoorFair');\n";
                script += "HIVcareArtDisableARVDrugsPoor('" + ddlReasonARVDrugsPoorFair.ClientID + "''0');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ARVDrugsAdhere_11", script);
            }

            if (ViewState["ReasonARVDrugsPoorFair"].ToString() == "1")
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'ARVDrugsPoorFairOther_00'>\n";
                script += "show('divReasonARVDrugsother');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ARVDrugsPoorFairOther_00", script);
            }

            if (Convert.ToInt32(ViewState["Pregstatus"]) == 1)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes_11'>\n";
                script += "show('rdopregnantyesno');\n";
                script += "hide('spanEDDNopregnant');\n";
                script += "show('spanEDD');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "PregnantYes_11", script);
            }

            if (Convert.ToInt32(ViewState["Pregstatus"]) == 2)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'PregnantNo_00'>\n";
                script += "show('rdopregnantyesno');\n";
                script += "show('spanEDDNopregnant');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "PregnantNo_00", script);
            }
            if (rdoPregnantYes.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes_11'>\n";
                script += "show('rdopregnantyesno');\n";
                script += "hide('spanEDDNopregnant');\n";
                script += "show('spanEDD');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "PregnantYes_11", script);
            }

            if (rdoPregnantNo.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'PregnantNo_00'>\n";
                script += "show('rdopregnantyesno');\n";
                script += "show('spanEDDNopregnant');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "PregnantNo_00", script);
            }
            if (rdoPMTCT.Checked)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'PregnantPMTCT_00'>\n";
                script += "show('spanpmctcancno');\n";

                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "PregnantPMTCT_00", script);
            }

            if (Convert.ToInt32(ViewState["FamilyPanningStatus"]) == 1)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'FamilyPlanning_00'>\n";
                script += "show('divFamilyPlanningMethod');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "FamilyPlanning_00", script);
            }
            if (Convert.ToInt32(ViewState["TBStatusTBRx"]) == 1)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'TBStatusRX_00'>\n";
                script += "show('divTBStatusTBRX');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "TBStatusRX_00", script);
            }
            if (Convert.ToInt32(ViewState["ReferredToOther"]) == 1)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'ReferredToOther_00'>\n";
                script += "show('spanotherReferTo');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ReferredToOther_00", script);

            }
            if (Convert.ToInt32(ViewState["potentialSideEffecOther"]) == 1)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'potentialSideEffect_00'>\n";
                script += "show('otherPotentialSideEffect');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "potentialSideEffect_00", script);

            }
            if (Convert.ToInt32(ViewState["OIsProblemOther"]) == 1)
            {
                script = "";
                script = "<script language = 'javascript' defer ='defer' id = 'OIsProblemOther_00'>\n";
                script += "show('otherchkNewOIsProblems');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "OIsProblemOther_00", script);
            }

        }

        #endregion


        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.HIVCareARTEncounter));
                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "HCACounter");

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
                dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, visitID, 0, 0, Convert.ToInt32(ApplicationAccess.HIVCareARTEncounter));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "HCACounter");
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

        protected void btnpharmacy_Click(object sender, EventArgs e)
        {
            try
            {
                save(false, "Pharmacy");
            }
            finally { }
        }
        protected void btnLabratory_Click(object sender, EventArgs e)
        {
            try
            {
                save(false, "Labratory");
            }
            finally { }
        }
        protected void btnOrderLabTest_Click(Object sender, EventArgs e)
        {
            try
            {
                save(false, "LabTest");
            }
            finally { }
        }
    }
}