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
    public partial class InitialEvaluation : BasePage, ICallbackEventHandler
    {
        public DataTable theARVSideEffectLeft;
        public DataTable theCurrentRegDT;
        public DataSet theDS;
        public DataTable theDTHivAssoConditionLeft, theDTHivAssoConditionRight, DT_disclosure;
        public DataTable theRegimen1;
        public DataTable theRegimen2;
        public DataTable theRegimen3;
        public DataTable theRegimen4;
        private ArrayList arl = new ArrayList();
        private DateTime CreateDate;
        private int FlagSulfa = 0, allergy_Sulfa_ID = 0, allergy_Other_ID = 0;
        private int HIVdiagnosisverified = 9, HIVdisclosure = 9, DisclosureOtherID, Pregnant = 9, intchklongTermMedsSulfa, intlongTermTBMed, intchklongTermMedsOther1, intchklongTermMedsOther2, intPrevSingleDoseNVP, intPrevARVRegimen;

        private int icount;

        private int intflag=0, visitPK;
        private int MedHisnone = 0, MedHisnotDocumented = 0, AssoCondnone = 0, AssoCondnotDocumented = 0;
        private int PatID;
        private int PId;
        private int ReasonID, AssessmentPlanID1, AssessmentPlanID2, AssessmentPlanID3, AssessmentPlanID4, AssessmentPlanID5, AssessmentPlanID6, AssessmentPlanID7, AssessmentPlanID8;
        private StringBuilder sbParameter= new StringBuilder();
        private StringBuilder sbValues;
        private string script, str, strCallback;
        private string strmultiselect;
        private string TableName="";
        private DataSet theDS_IE;
        private int visitid = 1;
        private int VisitIDIE=0;
        public DataSet DataSet_IE()
        {
            //1 Coding for Saving Disclosure

            DataSet theDS = new DataSet();

            DataTable dtDisclosure = new DataTable();

            DataColumn theDisclosureID = new DataColumn("DisclosureID");
            theDisclosureID.DataType = System.Type.GetType("System.Int32");
            dtDisclosure.Columns.Add(theDisclosureID);

            DataColumn theDisclosureOther = new DataColumn("DisclosureOther");
            theDisclosureOther.DataType = System.Type.GetType("System.String");
            dtDisclosure.Columns.Add(theDisclosureOther);

            DataRow drdisclosure;
            int DisclosureOtherID = 0;
            string strDisclosure = "";

            foreach (HtmlTableRow tr in tblHIVdisclosure.Rows)
            {
                drdisclosure = dtDisclosure.NewRow();
                foreach (HtmlTableCell tc in tr.Cells)
                {
                    foreach (Control ct in tc.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Checked == true)
                            {
                                strDisclosure = ((HtmlInputCheckBox)ct).Value;
                                foreach (DataRow dr in DT_disclosure.Rows)
                                {
                                    if (strDisclosure == dr[1].ToString())
                                    {
                                        if (dr[1].ToString() == "Other")
                                        {
                                            DisclosureOtherID = Convert.ToInt32(dr[0].ToString());
                                            strDisclosure = dr[1].ToString();
                                        }
                                        else
                                        {
                                            drdisclosure["DisclosureID"] = Convert.ToInt32(dr[0].ToString());
                                            drdisclosure["DisclosureOther"] = null;
                                            dtDisclosure.Rows.Add(drdisclosure);
                                        }
                                    }
                                }
                            }
                        }
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        {
                            if (strDisclosure == "Other")
                            {
                                drdisclosure["DisclosureID"] = DisclosureOtherID;
                                drdisclosure["DisclosureOther"] = ((HtmlInputText)ct).Value;
                                dtDisclosure.Rows.Add(drdisclosure);
                            }
                        }
                    }
                }
            }
            theDS.Tables.Add(dtDisclosure);

            //2 - Code for saving Presenting Complaints

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

            // Coding for Saving Patients Medical History-3

            DataTable dtMedHistory = new DataTable();

            DataColumn theMedHistoryID = new DataColumn("MedHistoryID");
            theMedHistoryID.DataType = System.Type.GetType("System.Int32");
            dtMedHistory.Columns.Add(theMedHistoryID);

            DataColumn theMediHisDiseasePresent = new DataColumn("MediHisDiseasePresent");
            theMediHisDiseasePresent.DataType = System.Type.GetType("System.Boolean");
            dtMedHistory.Columns.Add(theMediHisDiseasePresent);

            DataColumn theYearDiseasePresent = new DataColumn("YearDiseasePresent");
            theYearDiseasePresent.DataType = System.Type.GetType("System.String");
            dtMedHistory.Columns.Add(theYearDiseasePresent);

            DataColumn theSpecifyDiseasePresent = new DataColumn("SpecifyDiseasePresent");
            theSpecifyDiseasePresent.DataType = System.Type.GetType("System.String");
            dtMedHistory.Columns.Add(theSpecifyDiseasePresent);

            DataRow drMedHistory;

            for (int i = 0; i < GrdMedHist.Rows.Count; i++)
            {
                GridViewRow row = GrdMedHist.Rows[i];
                //if (row.DataItem != null)
                //{
                CheckBox theChk = (CheckBox)row.Cells[1].Controls[0];
                DropDownList theDropDown = (DropDownList)row.Cells[2].Controls[0];
                TextBox theTxtBox = (TextBox)row.Cells[3].Controls[0];

                if (theChk.Checked == true)
                {
                    drMedHistory = dtMedHistory.NewRow();

                    drMedHistory["MedHistoryID"] = Convert.ToInt32(row.Cells[0].Text);
                    drMedHistory["MediHisDiseasePresent"] = true;
                    drMedHistory["YearDiseasePresent"] = theDropDown.Text;
                    drMedHistory["SpecifyDiseasePresent"] = theTxtBox.Text;
                    dtMedHistory.Rows.Add(drMedHistory);
                }
                //}
            }
            theDS.Tables.Add(dtMedHistory);

            //Coding for Saving HIV Associated Conditions
            /* Associate Condition left -4 */
            DataTable dtHIVAssoConditionsleft = new DataTable();

            DataColumn thechkHIVAssoCondid1 = new DataColumn("chkHIVAssoCondid1");
            thechkHIVAssoCondid1.DataType = System.Type.GetType("System.Int32");
            dtHIVAssoConditionsleft.Columns.Add(thechkHIVAssoCondid1);

            DataColumn theHIVAssoDiseasePresent1 = new DataColumn("HIVAssoDiseasePresent1");
            theHIVAssoDiseasePresent1.DataType = System.Type.GetType("System.Boolean");
            dtHIVAssoConditionsleft.Columns.Add(theHIVAssoDiseasePresent1);

            DataColumn theHIVAssocCondYear1 = new DataColumn("HIVAssocCondYear1");
            theHIVAssocCondYear1.DataType = System.Type.GetType("System.String");
            dtHIVAssoConditionsleft.Columns.Add(theHIVAssocCondYear1);

            DataRow drHIVAssoConditionsleft;
            String strHIVAssocvalue;
            String strYear = "";

            foreach (HtmlTableRow tr in tblHIVAIDSleft.Rows)
            {
                drHIVAssoConditionsleft = dtHIVAssoConditionsleft.NewRow();
                foreach (HtmlTableCell tc in tr.Cells)
                {
                    foreach (Control ct in tc.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Checked == true)
                            {
                                strHIVAssocvalue = ((HtmlInputCheckBox)ct).Value;
                                foreach (DataRow dr in theDTHivAssoConditionLeft.Rows)
                                {
                                    if (strHIVAssocvalue == dr[1].ToString())
                                    {
                                        drHIVAssoConditionsleft["chkHIVAssoCondid1"] = dr[0].ToString();
                                        drHIVAssoConditionsleft["HIVAssoDiseasePresent1"] = true;
                                    }
                                }
                            }
                        }

                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                        {
                            if (((HtmlInputRadioButton)ct).Checked == true)
                            {
                                strHIVAssocvalue = ((HtmlInputRadioButton)ct).Value;

                                foreach (DataRow dr in theDTHivAssoConditionLeft.Rows)
                                {
                                    if (strHIVAssocvalue == dr[1].ToString())
                                    {
                                        drHIVAssoConditionsleft["chkHIVAssoCondid1"] = dr[0].ToString();
                                        drHIVAssoConditionsleft["HIVAssoDiseasePresent1"] = true;
                                        drHIVAssoConditionsleft["HIVAssocCondYear1"] = strYear;
                                    }
                                }
                            }
                        }

                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        {
                            drHIVAssoConditionsleft["HIVAssocCondYear1"] = ((HtmlInputText)ct).Value;
                            strYear = ((HtmlInputText)ct).Value;
                        }
                    }
                }
                if (drHIVAssoConditionsleft[0].ToString() != "")
                {
                    dtHIVAssoConditionsleft.Rows.Add(drHIVAssoConditionsleft);
                }
            }
            theDS.Tables.Add(dtHIVAssoConditionsleft);

            /* Associate Condition Right -5*/
            DataTable dtHIVAssoConditionsright = new DataTable();

            DataColumn thechkHIVAssoCondid2 = new DataColumn("chkHIVAssoCondid2");
            thechkHIVAssoCondid2.DataType = System.Type.GetType("System.Int32");
            dtHIVAssoConditionsright.Columns.Add(thechkHIVAssoCondid2);

            DataColumn theHIVAssoDiseasePresent2 = new DataColumn("HIVAssoDiseasePresent2");
            theHIVAssoDiseasePresent2.DataType = System.Type.GetType("System.Boolean");
            dtHIVAssoConditionsright.Columns.Add(theHIVAssoDiseasePresent2);

            DataColumn theHIVAssoDiseaseDesc = new DataColumn("HIVAssoDiseaseDesc");
            theHIVAssoDiseaseDesc.DataType = System.Type.GetType("System.String");
            dtHIVAssoConditionsright.Columns.Add(theHIVAssoDiseaseDesc);

            DataColumn theHIVAssocCondYear2 = new DataColumn("HIVAssocCondYear2");
            theHIVAssocCondYear2.DataType = System.Type.GetType("System.String");
            dtHIVAssoConditionsright.Columns.Add(theHIVAssocCondYear2);

            DataRow drHIVAssoConditionsright;
            String strHIVAssocvalueright;

            string txtBoxrightId = "";
            foreach (HtmlTableRow tr in tblHIVAIDSright.Rows)
            {
                drHIVAssoConditionsright = dtHIVAssoConditionsright.NewRow();
                foreach (HtmlTableCell tc in tr.Cells)
                {
                    foreach (Control ct in tc.Controls)
                    {
                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                        {
                            if (((HtmlInputCheckBox)ct).Checked == true)
                            {
                                strHIVAssocvalueright = ((HtmlInputCheckBox)ct).Value;
                                foreach (DataRow dr in theDTHivAssoConditionRight.Rows)
                                {
                                    if (strHIVAssocvalueright == dr[1].ToString())
                                    {
                                        if (dr[1].ToString() == "Other")
                                        {
                                            drHIVAssoConditionsright["chkHIVAssoCondid2"] = dr[0].ToString();
                                            drHIVAssoConditionsright["HIVAssoDiseaseDesc"] = null;
                                            drHIVAssoConditionsright["HIVAssoDiseasePresent2"] = true;
                                            txtBoxrightId = "txtACondright" + ct.ID.ToString().Substring(12, ct.ID.Length - 12);
                                        }
                                        else
                                        {
                                            drHIVAssoConditionsright["chkHIVAssoCondid2"] = dr[0].ToString();
                                            drHIVAssoConditionsright["HIVAssoDiseaseDesc"] = null;
                                            drHIVAssoConditionsright["HIVAssoDiseasePresent2"] = true;
                                            txtBoxrightId = "txtACondright" + ct.ID.ToString().Substring(12, ct.ID.Length - 12);
                                        }
                                    }
                                }
                            }
                        }

                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        {
                            if (ct.ID.ToString() == txtBoxrightId.ToString())
                            {
                                drHIVAssoConditionsright["HIVAssocCondYear2"] = ((HtmlInputText)ct).Value;
                            }
                            else if (ct.ID.ToString() == "txtACondOther")
                            {
                                drHIVAssoConditionsright["HIVAssoDiseaseDesc"] = ((HtmlInputText)ct).Value;
                            }
                        }
                    }
                }
                if (drHIVAssoConditionsright[0].ToString() != "")
                {
                    dtHIVAssoConditionsright.Rows.Add(drHIVAssoConditionsright);
                }
            }
            theDS.Tables.Add(dtHIVAssoConditionsright);

            /************6*Save TB Screening*********/

            DataTable dtTBScreening = new DataTable();
            DataColumn theTBScreeningID = new DataColumn("TBScreeningID");
            theTBScreeningID.DataType = System.Type.GetType("System.Int32");
            dtTBScreening.Columns.Add(theTBScreeningID);
            DataRow drTBScreening;
            for (int i = 0; i < cblTBScreen.Items.Count; i++)
            {
                if (cblTBScreen.Items[i].Selected)
                {
                    drTBScreening = dtTBScreening.NewRow();
                    drTBScreening["TBScreeningID"] = Convert.ToInt32(cblTBScreen.Items[i].Value);
                    dtTBScreening.Rows.Add(drTBScreening);
                }
            }
            if (rdoPerformed.Checked == true)
            {
                DataRow[] theDR = dtTBScreening.Select("TBScreeningID='70'");
                if (theDR.Length == 0)
                {
                    drTBScreening = dtTBScreening.NewRow();
                    drTBScreening["TBScreeningID"] = "70";
                    dtTBScreening.Rows.Add(drTBScreening);
                }
            }
            else if (rdoNotDocumented.Checked == true)
            {
                DataRow[] theDR = dtTBScreening.Select("TBScreeningID='71'");
                if (theDR.Length == 0)
                {
                    drTBScreening = dtTBScreening.NewRow();
                    drTBScreening["TBScreeningID"] = "71";
                    dtTBScreening.Rows.Add(drTBScreening);
                }
            }

            //else if (rdoNone.Checked == true)
            //{
            //    DataRow[] theDR = dtTBScreening.Select("TBScreeningID='78'");
            //    if (theDR.Length == 0)
            //    {
            //        drTBScreening = dtTBScreening.NewRow();
            //        drTBScreening["TBScreeningID"] = "78";
            //        dtTBScreening.Rows.Add(drTBScreening);
            //    }
            //}

            theDS.Tables.Add(dtTBScreening);

            return theDS;
        }

        public string GetCallbackResult()
        {
            return str;
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            IInitialEval CallBackmgr;
            DataSet theDS = new DataSet();
            try
            {
                CallBackmgr = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
                theDS = CallBackmgr.GetARTStatus(Convert.ToInt32(Session["PatientId"]));
                str = theDS.GetXml();
                str += "ZZZ";
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
                CallBackmgr = null;
            }
        }

        protected void Add_attributes()
        {
            txtvisitDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtvisitDate.ClientID + "', '" + txtvisitDate.ClientID + "'); addDays();isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtprevLowestCD4Date.ClientID + "', '" + "Previous Lowest CD4" + "', '" + txtprevLowestCD4Date.ClientID + "'); isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtpriorARVsCD4Date.ClientID + "', '" + "Prior ARV CD4" + "', '" + txtpriorARVsCD4Date.ClientID + "');isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtmostRecentCD4Date.ClientID + "', '" + "Most Recent CD4" + "', '" + txtmostRecentCD4Date.ClientID + "');isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtmostRecentViralLoadDate.ClientID + "', '" + "Most Recent Viral Load" + "', '" + txtmostRecentViralLoadDate.ClientID + "'); SenderPregnantLMP();");
            txtvisitDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtHIVDiagnosisdate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtHIVDiagnosisdate.ClientID + "', '" + "HIVDiagnosis" + "', '" + txtHIVDiagnosisdate.ClientID + "')");
            txtHIVDiagnosisdate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtLMPdate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'), isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtLMPdate.ClientID + "', '" + "LMP" + "', '" + txtLMPdate.ClientID + "')");
            txtLMPdate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            chkpresentingComplaintsNone.Attributes.Add("OnClick", "display_chklist('" + chkpresentingComplaintsNone.ClientID + "', '" + presentingComplaintsShow.ClientID + "'); disableListItems('" + cblPresentingComplaints.ClientID + "', '" + cblPresentingComplaints.Items.Count + "') ");
            chkpresentingComplaintsNonehidden.Attributes.Add("OnClick", "display_chklist('" + chkpresentingComplaintsNonehidden.ClientID + "', '" + presentingComplaintsShow.ClientID + "')");

            rdoDisclosureNo.Attributes.Add("OnClick", "down(this); hide('showdisclosureName'); hide('otherDisclosure'); disableChkRdoListItems_left('ctl00_IQCareContentPlaceHolder_dis', '" + tblHIVdisclosure.Rows.Count + "');");

            rdoMedHistNone.Attributes.Add("OnClick", "down(this); disableGridItems('" + GrdMedHist.ClientID + "', '" + GrdMedHist.Rows.Count + "')");
            rdoMedHistnotdocumented.Attributes.Add("OnClick", "down(this); disableGridItems('" + GrdMedHist.ClientID + "', '" + GrdMedHist.Rows.Count + "')");

            rdoprevARVExposureNone.Attributes.Add("OnClick", "down(this);hide('prevexpdiv'); hide('prevARVReg'); hide('prevSingleDoseNVPSelected'); disableCheckbox('" + chkprevSDNVPNVP.ClientID + "'); disableCheckbox('" + chkprevARVRegimen.ClientID + "');");
            rdoprevARVExpnotdocumented.Attributes.Add("OnClick", "down(this);hide('prevexpdiv'); hide('prevARVReg'); hide('prevSingleDoseNVPSelected'); disableCheckbox('" + chkprevSDNVPNVP.ClientID + "'); disableCheckbox('" + chkprevARVRegimen.ClientID + "')");

            chksulfaAllergy.Attributes.Add("OnClick", "disableRadioNone('" + rdoAllergynone.ClientID + "'); disableRadioNotDocumented('" + rdoAllergynotdocumented.ClientID + "')");
            chkotherAllergy.Attributes.Add("OnClick", "disableRadioNone('" + rdoAllergynone.ClientID + "'); disableRadioNotDocumented('" + rdoAllergynotdocumented.ClientID + "')");
            rdoHIVassocNone.Attributes.Add("OnClick", "down(this);hide('assocSelected'); hide('pultb1'); hide('pultb2'); hide('otherHIVCondition_1'); hide('otherHIVCondition_2'); hide('otherHIVCondition_3'); disableChkRdoListItems_left_IE('ctl00_IQCareContentPlaceHolder_HIVAssoleft', '" + tblHIVAIDSleft.Rows.Count + "'); disableChkRdoListItems_right('ctl00_IQCareContentPlaceHolder_HIVAssoright', '" + tblHIVAIDSright.Rows.Count + "');");
            rdoPrevHIVassocNotDocumented.Attributes.Add("OnClick", "down(this);hide('assocSelected'); hide('pultb1'); hide('pultb2'); hide('otherHIVCondition_1'); hide('otherHIVCondition_2'); hide('otherHIVCondition_3'); disableChkRdoListItems_left_IE('ctl00_IQCareContentPlaceHolder_HIVAssoleft', '" + tblHIVAIDSleft.Rows.Count + "'); disableChkRdoListItems_right('ctl00_IQCareContentPlaceHolder_HIVAssoright', '" + tblHIVAIDSright.Rows.Count + "');");

            //Lowest CD4
            txtprevLowestCD4.Attributes.Add("OnBlur", "isBetween('" + txtprevLowestCD4.ClientID + "', '" + "CD4" + "', '" + 0 + "', '" + 5000 + "')");
            txtprevLowestCD4Percent.Attributes.Add("OnBlur", "isBetween('" + txtprevLowestCD4Percent.ClientID + "', '" + "CD4 Percent" + "', '" + 0 + "', '" + 100 + "')");
            txtprevLowestCD4Date.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtprevLowestCD4Date.ClientID + "', '" + "CD4" + "', '" + txtprevLowestCD4Date.ClientID + "')");
            txtprevLowestCD4Date.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,3); disableRadioNone('" + rdoprevLowestCD4none.ClientID + "'); disableRadioNotDocumented('" + rdoprevLowestCD4notdocumented.ClientID + "')");
            txtprevLowestCD4Date.Attributes.Add("OnClick", "disableRadioNone('" + rdoprevLowestCD4none.ClientID + "'); disableRadioNotDocumented('" + rdoprevLowestCD4notdocumented.ClientID + "')");

            //Prior ARV CD4
            txtpriorARVsCD4.Attributes.Add("OnBlur", "isBetween('" + txtpriorARVsCD4.ClientID + "', '" + "CD4" + "', '" + 0 + "', '" + 5000 + "')");
            txtpriorARVsCD4Percent.Attributes.Add("OnBlur", "isBetween('" + txtpriorARVsCD4Percent.ClientID + "', '" + "CD4 Percent" + "', '" + 0 + "', '" + 100 + "')");
            txtpriorARVsCD4Date.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtpriorARVsCD4Date.ClientID + "', '" + "CD4" + "', '" + txtpriorARVsCD4Date.ClientID + "')");
            txtpriorARVsCD4Date.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3'); disableRadioNone('" + rdopriorARVsCD4none.ClientID + "'); disableRadioNotDocumented('" + rdopriorARVsCD4notdocumented.ClientID + "')");
            txtpriorARVsCD4Date.Attributes.Add("OnClick", "disableRadioNone('" + rdopriorARVsCD4none.ClientID + "'); disableRadioNotDocumented('" + rdopriorARVsCD4notdocumented.ClientID + "')");

            //Recent CD4
            txtmostRecentCD4.Attributes.Add("OnBlur", "isBetween('" + txtmostRecentCD4.ClientID + "', '" + "CD4" + "', '" + 0 + "', '" + 5000 + "')");
            txtmostRecentCD4Percent.Attributes.Add("OnBlur", "isBetween('" + txtmostRecentCD4Percent.ClientID + "', '" + "CD4 Percent " + "', '" + 0 + "', '" + 100 + "')");
            txtmostRecentCD4Date.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtmostRecentCD4Date.ClientID + "', '" + "CD4" + "', '" + txtmostRecentCD4Date.ClientID + "')");
            txtmostRecentCD4Date.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3'); disableRadioNone('" + rdomostRecentCD4none.ClientID + "'); disableRadioNotDocumented('" + rdomostRecentCD4notdocumented.ClientID + "')");
            txtmostRecentCD4Date.Attributes.Add("OnClick", "disableRadioNone('" + rdomostRecentCD4none.ClientID + "'); disableRadioNotDocumented('" + rdomostRecentCD4notdocumented.ClientID + "')");

            //Recent Viral Load
            txtmostRecentViralLoad.Attributes.Add("OnBlur", "isBetween('" + txtmostRecentViralLoad.ClientID + "', '" + "ViralLoad" + "', '" + 50 + "', '" + 2000000 + "')");
            txtmostRecentViralLoadDate.Attributes.Add("OnBlur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDateHIVrelated('" + txtvisitDate.ClientID + "', '" + txtmostRecentViralLoadDate.ClientID + "', '" + "Viral Load" + "', '" + txtmostRecentViralLoadDate.ClientID + "')");
            txtmostRecentViralLoadDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3'); disableRadioNone('" + rdomostRecentViralLoadnone.ClientID + "'); disableRadioNotDocumented('" + rdomostRecentViralLoadnotdocumented.ClientID + "')");
            txtmostRecentViralLoadDate.Attributes.Add("OnClick", "disableRadioNone('" + rdomostRecentViralLoadnone.ClientID + "'); disableRadioNotDocumented('" + rdomostRecentViralLoadnotdocumented.ClientID + "')");

            txtprevLowestCD4.Attributes.Add("OnClick", "disableRadioNone('" + rdoprevLowestCD4none.ClientID + "'); disableRadioNotDocumented('" + rdoprevLowestCD4notdocumented.ClientID + "')");
            txtprevLowestCD4.Attributes.Add("Onkeyup", "chkNumeric('" + txtprevLowestCD4.ClientID + "'); disableRadioNone('" + rdoprevLowestCD4none.ClientID + "'); disableRadioNotDocumented('" + rdoprevLowestCD4notdocumented.ClientID + "')");

            txtprevLowestCD4Percent.Attributes.Add("OnClick", "disableRadioNone('" + rdoprevLowestCD4none.ClientID + "'); disableRadioNotDocumented('" + rdoprevLowestCD4notdocumented.ClientID + "')");
            txtprevLowestCD4Percent.Attributes.Add("Onkeyup", "chkPostiveInteger('" + txtprevLowestCD4Percent.ClientID + "'); disableRadioNone('" + rdoprevLowestCD4none.ClientID + "'); disableRadioNotDocumented('" + rdoprevLowestCD4notdocumented.ClientID + "')");

            txtpriorARVsCD4.Attributes.Add("OnClick", "disableRadioNone('" + rdopriorARVsCD4none.ClientID + "'); disableRadioNotDocumented('" + rdopriorARVsCD4notdocumented.ClientID + "')");
            txtpriorARVsCD4.Attributes.Add("Onkeyup", "chkNumeric('" + txtpriorARVsCD4.ClientID + "'); disableRadioNone('" + rdopriorARVsCD4none.ClientID + "'); disableRadioNotDocumented('" + rdopriorARVsCD4notdocumented.ClientID + "')");

            txtpriorARVsCD4Percent.Attributes.Add("OnClick", "disableRadioNone('" + rdopriorARVsCD4none.ClientID + "'); disableRadioNotDocumented('" + rdopriorARVsCD4notdocumented.ClientID + "')");
            txtpriorARVsCD4Percent.Attributes.Add("Onkeyup", "chkPostiveInteger('" + txtpriorARVsCD4Percent.ClientID + "'); disableRadioNone('" + rdopriorARVsCD4none.ClientID + "'); disableRadioNotDocumented('" + rdopriorARVsCD4notdocumented.ClientID + "')");

            txtmostRecentCD4.Attributes.Add("OnClick", "disableRadioNone('" + rdomostRecentCD4none.ClientID + "'); disableRadioNotDocumented('" + rdomostRecentCD4notdocumented.ClientID + "')");
            txtmostRecentCD4.Attributes.Add("Onkeyup", "chkNumeric('" + txtmostRecentCD4.ClientID + "'); disableRadioNone('" + rdomostRecentCD4none.ClientID + "'); disableRadioNotDocumented('" + rdomostRecentCD4notdocumented.ClientID + "')");

            txtmostRecentCD4Percent.Attributes.Add("OnClick", "disableRadioNone('" + rdomostRecentCD4none.ClientID + "'); disableRadioNotDocumented('" + rdomostRecentCD4notdocumented.ClientID + "')");
            txtmostRecentCD4Percent.Attributes.Add("Onkeyup", "chkPostiveInteger('" + txtmostRecentCD4Percent.ClientID + "'); disableRadioNone('" + rdomostRecentCD4none.ClientID + "'); disableRadioNotDocumented('" + rdomostRecentCD4notdocumented.ClientID + "')");

            txtmostRecentViralLoad.Attributes.Add("OnClick", "disableRadioNone('" + rdomostRecentViralLoadnone.ClientID + "'); disableRadioNotDocumented('" + rdomostRecentViralLoadnotdocumented.ClientID + "')");
            txtmostRecentViralLoad.Attributes.Add("Onkeyup", "chkNumeric('" + txtmostRecentViralLoad.ClientID + "'); disableRadioNone('" + rdomostRecentViralLoadnone.ClientID + "'); disableRadioNotDocumented('" + rdomostRecentViralLoadnotdocumented.ClientID + "')");

            txtcurrentARTDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtvisitDate.ClientID + "', '" + txtcurrentARTDate.ClientID + "', '" + "Current ART" + "', '" + txtcurrentARTDate.ClientID + "')");
            txtprevSingleDoseNVPDate1.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtvisitDate.ClientID + "', '" + txtprevSingleDoseNVPDate1.ClientID + "', '" + "SingleDose NVP" + "', '" + txtprevSingleDoseNVPDate1.ClientID + "')");
            txtprevSingleDoseNVPDate2.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtvisitDate.ClientID + "', '" + txtprevSingleDoseNVPDate2.ClientID + "', '" + "SingleDose NVP" + "', '" + txtprevSingleDoseNVPDate2.ClientID + "')");
            txtLongTermTBStartDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtvisitDate.ClientID + "', '" + txtLongTermTBStartDate.ClientID + "', '" + "Long Term TB Start Date" + "', '" + txtLongTermTBStartDate.ClientID + "')");

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

            rdoAllergynone.Attributes.Add("OnClick", "down(this); disableCheckbox('" + chksulfaAllergy.ClientID + "'); hide('otherAllergyName'); disableCheckbox('" + chkotherAllergy.ClientID + "')");
            rdoAllergynotdocumented.Attributes.Add("OnClick", "down(this); disableCheckbox('" + chksulfaAllergy.ClientID + "');   hide('otherAllergyName'); disableCheckbox('" + chkotherAllergy.ClientID + "')");
            chksulfaAllergy.Attributes.Add("OnClick", "disableRadioNone('" + rdoAllergynone.ClientID + "'); disableRadioNotDocumented('" + rdoAllergynotdocumented.ClientID + "')");
            chkotherAllergy.Attributes.Add("OnClick", "disableRadioNone('" + rdoAllergynone.ClientID + "'); disableRadioNotDocumented('" + rdoAllergynotdocumented.ClientID + "'); toggle('otherAllergyName')");

            rdoclinAssessment_Plan_RegimenNone.Attributes.Add("OnClick", "down(this); disableCheckbox('" + chkclinAssessmentInitial1.ClientID + "'); hide('otherAllergyName');  disableCheckbox('" + chkclinAssessmentInitial2.ClientID + "')");
            chkclinAssessmentInitial1.Attributes.Add("OnClick", "disableRadioNone('" + rdoclinAssessment_Plan_RegimenNone.ClientID + "')");
            chkclinAssessmentInitial2.Attributes.Add("OnClick", "disableRadioNone('" + rdoclinAssessment_Plan_RegimenNone.ClientID + "')");

            ddlTherapyChange.Attributes.Add("onchange", "Therapy(this.options[this.selectedIndex].text, 1);");
            ddlTherapyStop.Attributes.Add("onchange", "Therapy(this.options[this.selectedIndex].text, 2);");

            txtarvTherapyChangeCodeOtherName.Attributes.Add("onKeyup", "chkString('" + txtarvTherapyChangeCodeOtherName.ClientID + "')");
            txtarvTherapyStopCodeOtherName.Attributes.Add("onKeyup", "chkString('" + txtarvTherapyStopCodeOtherName.ClientID + "')");

            txtappDate.Attributes.Add("onKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtappDate.Attributes.Add("onBlur", "DateFormat(this,this.value,event,true,'3')");

            txtEDDDate.Attributes.Add("onKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtEDDDate.Attributes.Add("onBlur", "DateFormat(this,this.value,event,true,'3')");

            txtDeliDate.Attributes.Add("onKeyup", "DateFormat(this,this.value,event,false,'3')");
            txtDeliDate.Attributes.Add("onBlur", "DateFormat(this,this.value,event,true,'3')");

            rdoprevARVExposureNone.Attributes.Add("onclick", "down(this); getMessage('" + txtcurrentART.ClientID + "', '" + txtcurrentARTDate.ClientID + "', '" + txtprevSingleDoseNVPDate1.ClientID + "', '" + txtprevSingleDoseNVPDate2.ClientID + "', '" + txtprevARVRegimen1Name.ClientID + "', '" + txtprevARVRegimen1Months.ClientID + "', '" + txtprevARVRegimen2Name.ClientID + "', '" + txtprevARVRegimen2Months.ClientID + "', '" + txtprevARVRegimen3Name.ClientID + "', '" + txtprevARVRegimen3Months.ClientID + "', '" + txtprevARVRegimen4Name.ClientID + "', '" + txtprevARVRegimen4Months.ClientID + "', '" + chkprevSDNVPNVP.ClientID + "', '" + chkprevARVRegimen.ClientID + "', '" + rdopreviousARV.ClientID + "');");
            txtprevARVRegimen1Months.Attributes.Add("onkeyup", "chkInteger('" + txtprevARVRegimen1Months.ClientID + "')");
            txtprevARVRegimen2Months.Attributes.Add("onkeyup", "chkInteger('" + txtprevARVRegimen2Months.ClientID + "')");
            txtprevARVRegimen3Months.Attributes.Add("onkeyup", "chkInteger('" + txtprevARVRegimen3Months.ClientID + "')");
            txtprevARVRegimen4Months.Attributes.Add("onkeyup", "chkInteger('" + txtprevARVRegimen4Months.ClientID + "')");
            rdoPerformed.Attributes.Add("onclick", "toggle('divTBPerformed'); down(this); disableChkboxList('" + cblTBScreen.ClientID + "', '" + cblTBScreen.Items.Count + "')");
            rdoNotDocumented.Attributes.Add("onclick", "hide('divTBPerformed'); down(this); disableChkboxList('" + cblTBScreen.ClientID + "', '" + cblTBScreen.Items.Count + "')");
        }

        protected ArrayList AssessmentAL()
        {
            ArrayList AssessmentAL = new ArrayList();
            if (rdoclinAssessment_Plan_RegimenNone.Checked == true)
            {
                AssessmentAL.Add(AssessmentPlanID8);
            }
            if (chkclinAssessmentInitial1.Checked == true)
            {
                AssessmentAL.Add(AssessmentPlanID1);
            }
            if (chkclinAssessmentInitial2.Checked == true)
            {
                AssessmentAL.Add(AssessmentPlanID2);
            }
            if (chkclinPlanInitial.Checked == true)
            {
                AssessmentAL.Add(AssessmentPlanID3);
            }
            if (chkclinPlanInitial2.Checked == true)
            {
                AssessmentAL.Add(AssessmentPlanID4);
            }
            if (chkclinPlanInitial3.Checked == true)
            {
                AssessmentAL.Add(AssessmentPlanID5);
            }
            if (chkclinPlanInitial4.Checked == true)
            {
                AssessmentAL.Add(AssessmentPlanID6);
            }
            return AssessmentAL;
        }

        protected void BMIAttributes()
        {
            txtphysHeight.Attributes.Add("OnBlur", "CalcualteBMI('" + txtanotherbmi.ClientID + "','" + txtphysWeight.ClientID + "','" + txtphysHeight.ClientID + "');");
            txtphysWeight.Attributes.Add("OnBlur", "CalcualteBMI('" + txtanotherbmi.ClientID + "','" + txtphysWeight.ClientID + "','" + txtphysHeight.ClientID + "');");
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

        protected void btncomplete_Click(object sender, EventArgs e)
        {
            ViewState["BtnCompClicked"] = "1";
            if (Validate_Data_Quality() == false)
            {
                Show_Hide();
                return;
            }
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
            //HashTable Function for SingleSelect Items
            Hashtable htInitialEvalParameters = InitialEvalParameters();
            //DataSet function for MultiSelect Items
            theDS_IE = DataSet_IE();
            //ArrayList for Assessment Details
            ArrayList ALAssessment = AssessmentAL();
            IInitialEval IEManager;
            IEManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
            try
            {
                MedHisnone = this.rdoMedHistNone.Checked == true ? 95 : 0;
                MedHisnotDocumented = this.rdoMedHistnotdocumented.Checked == true ? 94 : 0;
                AssoCondnone = this.rdoHIVassocNone.Checked == true ? 97 : 0;
                AssoCondnotDocumented = this.rdoPrevHIVassocNotDocumented.Checked == true ? 96 : 0;
                PId = Convert.ToInt32(Request.QueryString["patientid"]);
                //Adding New Records
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    Session["Redirect"] = "0";
                    //Custom Field Insert
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.InitialEvaluation, (DataSet)ViewState["CustomFieldsDS"]);
                    DataSet DS = (DataSet)IEManager.SaveInitialEvaluation(htInitialEvalParameters, MedHisnone, MedHisnotDocumented, AssoCondnone, AssoCondnotDocumented, theDS_IE, ALAssessment, VisitIDIE, MulttxtclinAssessmentNotes.Value, MulttxtclinPlanNotes.Value, intflag, Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT, txtclinicalNotes.Text);
                    Session["PatientVisitId"] = Convert.ToInt32(DS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    ViewState["VisitDate"] = Convert.ToDateTime(DS.Tables[0].Rows[0]["VisitDate"].ToString());
                    Session["ServiceLocationId"] = Convert.ToInt32(DS.Tables[0].Rows[0]["LocationID"].ToString());
                    DQCancel();
                    btncomplete.CssClass = "greenbutton";
                }
                //Editing Existing Records
                else
                {
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.Enrollment, (DataSet)ViewState["CustomFieldsDS"]);
                    DataSet DS = IEManager.SaveInitialEvaluation(htInitialEvalParameters, MedHisnone, MedHisnotDocumented, AssoCondnone, AssoCondnotDocumented, theDS_IE, ALAssessment, VisitIDIE, MulttxtclinAssessmentNotes.Value, MulttxtclinPlanNotes.Value, intflag, Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT, txtclinicalNotes.Text);
                    Session["PatientVisitId"] = Convert.ToInt32(DS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    ViewState["VisitDate"] = Convert.ToDateTime(DS.Tables[0].Rows[0]["VisitDate"].ToString());
                    Session["ServiceLocationId"] = Convert.ToInt32(DS.Tables[0].Rows[0]["LocationID"].ToString());
                    DQCancel();
                    btncomplete.CssClass = "greenbutton";
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
            Application.Add("MasterData", ViewState["MasterData"]);
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
            Application.Add("MasterData", ViewState["MasterData"]);
            Application.Add("SelectedDrug", (DataTable)ViewState["SelectedData2"]);
            ViewState.Remove("MasterData");
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=" + btnRegimen2.ID + "','DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        protected void btnRegimen3_Click(object sender, EventArgs e)
        {
            string theScript;
            Application.Add("MasterData", ViewState["MasterData"]);
            Application.Add("SelectedDrug", (DataTable)ViewState["SelectedData3"]);
            ViewState.Remove("MasterData");
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=" + btnRegimen3.ID + "','DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        protected void btnRegimen4_Click(object sender, EventArgs e)
        {
            string theScript;
            Application.Add("MasterData", ViewState["MasterData"]);
            Application.Add("SelectedDrug", (DataTable)ViewState["SelectedData4"]);
            ViewState.Remove("MasterData");
            theScript = "<script language='javascript' id='DrgPopup'>\n";
            theScript += "window.open('../Pharmacy/frmDrugSelector.aspx?DrugType=37&btnreg=" + btnRegimen4.ID + "','DrugSelection','toolbars=no,location=no,directories=no,dependent=yes,top=10,left=30,maximize=no,resize=no,width=700,height=350,scrollbars=yes');\n";
            theScript += "</script>\n";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "DrgPopup", theScript);
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (FieldValidation() == false)
            {
                Show_Hide();
                return;
            }
            //HashTable Function for SingleSelect Items
            Hashtable htInitialEvalParameters = InitialEvalParameters();
            //DataSet function for MultiSelect Items
            theDS_IE = DataSet_IE();
            //ArrayList for Assessment Details
            ArrayList ALAssessment = AssessmentAL();
            IInitialEval IEManager;
            IEManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
            int patientid = Convert.ToInt32(PId);
            try
            {
                MedHisnone = this.rdoMedHistNone.Checked == true ? 95 : 0;
                MedHisnotDocumented = this.rdoMedHistnotdocumented.Checked == true ? 94 : 0;
                AssoCondnone = this.rdoHIVassocNone.Checked == true ? 97 : 0;
                AssoCondnotDocumented = this.rdoPrevHIVassocNotDocumented.Checked == true ? 96 : 0;
                //PId = Convert.ToInt32(Request.QueryString["patientid"]);
                //Adding New Records
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    ViewState["DataQualityFlag"] = "0";
                    Session["Redirect"] = "0";
                    //Custom Field Insertion
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.InitialEvaluation, (DataSet)ViewState["CustomFieldsDS"]);
                    //Function for saving new Data
                    DataSet DS = (DataSet)IEManager.SaveInitialEvaluation(htInitialEvalParameters, MedHisnone, MedHisnotDocumented, AssoCondnone, AssoCondnotDocumented, theDS_IE, ALAssessment, VisitIDIE, MulttxtclinAssessmentNotes.Value, MulttxtclinPlanNotes.Value, intflag, Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT, txtclinicalNotes.Text);
                    Session["PatientVisitId"] = Convert.ToInt32(DS.Tables[0].Rows[0]["Visit_Id"].ToString());
                    ViewState["VisitDate"] = Convert.ToDateTime(DS.Tables[0].Rows[0]["VisitDate"].ToString());
                    Session["ServiceLocationId"] = Convert.ToInt32(DS.Tables[0].Rows[0]["LocationID"].ToString());
                    SaveCancel();
                }
                //Editing Existing Records
                else //if (Request.QueryString["name"] == "Edit")
                {
                    IEManager.Update_DataQuality(PId, visitPK, Convert.ToInt32(ViewState["DataQuality"]), Convert.ToInt32(Session["AppLocationId"]));
                    ViewState["DataQualityFlag"] = "0";
                    CustomFieldClinical theCustomManager = new CustomFieldClinical();
                    DataTable theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.InitialEvaluation, (DataSet)ViewState["CustomFieldsDS"]);
                    DataSet DS = (DataSet)IEManager.SaveInitialEvaluation(htInitialEvalParameters, MedHisnone, MedHisnotDocumented, AssoCondnone, AssoCondnotDocumented, theDS_IE, ALAssessment, VisitIDIE, MulttxtclinAssessmentNotes.Value, MulttxtclinPlanNotes.Value, intflag, Convert.ToInt32(ViewState["DataQualityFlag"]), theCustomDataDT, txtclinicalNotes.Text);
                    //ViewState["PtnID"] = Convert.ToInt32(DS.Tables[0].Rows[0]["Ptn_pk"].ToString());
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

        protected void Check_No_of_VisitDate()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Initial Evaluation was done on this visit date. Want to do another...?');\n";
            script += "if (ans==false)\n";
            script += "{\n";
            script += "window.location.href='frmPatient_Home.aspx?PatientId=" + PId + "';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        protected void FillDropDowns()
        {
            IInitialEval IEManager;
            try
            {
                IEManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
                DataSet theDSDB = IEManager.GetAllDropDowns();
                DataTable theDT = new DataTable();
                DataSet theDS = new DataSet();
                theDS.ReadXml(MapPath("..\\XMLFiles\\ALLMasters.con"));
                BindFunctions BindManager = new BindFunctions();

                IIQCareSystem SystemManager;
                SystemManager = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
                DateTime theDate = SystemManager.SystemDate();
                int theYear = theDate.Year;
                SystemManager = null;

                BindFunctions theUtil = new BindFunctions();
                IQCareUtils theUtils = new IQCareUtils();
                DataTable theYearDT = theUtil.GetYears(theYear, "Name", "ID");
                DataView theDV = new DataView(theDS.Tables["Mst_HivDisease"]);
                theDV.RowFilter = "SectionId = 2 and DeleteFlag = 0";
                theDV.Sort = "Name asc";
                if (theDV.Table != null)
                {
                    DataTable theMHistDT = theUtils.CreateTableFromDataView(theDV);
                    GrdMedHist.DataSource = theMHistDT;
                    GrdMedHist.DataBind();
                    Bind_GridMedHistory(theYearDT);
                    theDV.Dispose();
                }

                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    //Jayant 26-12-2007
                    theDTHivAssoConditionLeft = theDSDB.Tables[0];
                    int HIVAssoleft = 0;
                    for (int i = 0; i < theDTHivAssoConditionLeft.Rows.Count; i++)
                    {
                        HtmlTableRow trHIVAssoleft = new HtmlTableRow();
                        HtmlTableCell tcHIVAssoleft1 = new HtmlTableCell();
                        HtmlTableCell tcHIVAssoleft2 = new HtmlTableCell();
                        HtmlTableCell tcHIVAssoleft3 = new HtmlTableCell();
                        if ((i != 1) && (i != 2))
                        {
                            tcHIVAssoleft1.Width = "60%";
                            HtmlInputCheckBox chkHivAssoConditionLeft = new HtmlInputCheckBox();
                            chkHivAssoConditionLeft.ID = Convert.ToString("HIVAssoleft" + i);
                            chkHivAssoConditionLeft.Value = theDTHivAssoConditionLeft.Rows[i][1].ToString();
                            tcHIVAssoleft1.Controls.Add(chkHivAssoConditionLeft);
                            tcHIVAssoleft1.Controls.Add(new LiteralControl(chkHivAssoConditionLeft.Value));
                            trHIVAssoleft.Cells.Add(tcHIVAssoleft1);

                            tcHIVAssoleft2.Width = "20%";
                            tcHIVAssoleft2.Controls.Add(new LiteralControl("<Span class='smallerlabel'>"));
                            tcHIVAssoleft2.Controls.Add(new LiteralControl("MMM-YYYY"));
                            tcHIVAssoleft2.Controls.Add(new LiteralControl("</div>"));
                            trHIVAssoleft.Cells.Add(tcHIVAssoleft2);

                            tcHIVAssoleft3.Width = "20%";
                            HtmlInputText HTextACondleft = new HtmlInputText();
                            HTextACondleft.ID = Convert.ToString("txtACondleft" + i);
                            HTextACondleft.Size = 5;
                            tcHIVAssoleft3.Controls.Add(HTextACondleft);
                            tcHIVAssoleft3.Controls.Add(new LiteralControl(HTextACondleft.Value));
                            trHIVAssoleft.Cells.Add(tcHIVAssoleft3);
                            HTextACondleft.Attributes.Add("Onkeyup", "DateFormat(this,this.value,event,false,'4');");
                            HTextACondleft.Attributes.Add("Onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtvisitDate.ClientID + "', this.id, '" + "Current Row" + "', this.id);");
                            if (chkHivAssoConditionLeft.Value == "Pulmonary TB")
                            {
                                chkHivAssoConditionLeft.Attributes.Add("onclick", "toggle('pultb1'); toggle('pultb2')");
                            }
                            HIVAssoleft++;
                        }
                        if (i == 1)
                        {
                            tcHIVAssoleft2.Controls.Add(new LiteralControl("<div id='pultb1' style='display:none; padding-left:20px'>"));
                            HtmlInputRadioButton rdopultbsmplus = new HtmlInputRadioButton();
                            rdopultbsmplus.ID = Convert.ToString("txtACondleftrdo" + 11);
                            rdopultbsmplus.Value = theDTHivAssoConditionLeft.Rows[1][1].ToString();
                            rdopultbsmplus.Attributes.Add("onfocus", "up(this);");
                            rdopultbsmplus.Attributes.Add("onclick", "down(this);");
                            tcHIVAssoleft2.Controls.Add(rdopultbsmplus);
                            tcHIVAssoleft2.Controls.Add(new LiteralControl(rdopultbsmplus.Value));
                            trHIVAssoleft.Cells.Add(tcHIVAssoleft2);
                            tcHIVAssoleft2.Controls.Add(new LiteralControl("</div>"));
                            HIVAssoleft++;
                        }
                        if (i == 2)
                        {
                            tcHIVAssoleft2.Controls.Add(new LiteralControl("<div id='pultb2' style='display:none; padding-left:20px'>"));
                            HtmlInputRadioButton rdopultbsminus = new HtmlInputRadioButton();
                            rdopultbsminus.ID = Convert.ToString("txtACondleftrdo" + 12);
                            rdopultbsminus.Value = theDTHivAssoConditionLeft.Rows[2][1].ToString();
                            rdopultbsminus.Attributes.Add("onfocus", "up(this);");
                            rdopultbsminus.Attributes.Add("onclick", "down(this);");
                            tcHIVAssoleft2.Controls.Add(rdopultbsminus);
                            tcHIVAssoleft2.Controls.Add(new LiteralControl(rdopultbsminus.Value));
                            trHIVAssoleft.Cells.Add(tcHIVAssoleft2);
                            tcHIVAssoleft2.Controls.Add(new LiteralControl("</div>"));
                            HIVAssoleft++;
                        }
                        HIVAssoleft++;
                        tblHIVAIDSleft.Rows.Add(trHIVAssoleft);
                    }

                    DataView theDVHivAsooConditionRight = new DataView(theDSDB.Tables[1]);
                    theDVHivAsooConditionRight.RowFilter = "DeleteFlag=0";
                    theDVHivAsooConditionRight.RowFilter = "Srno<>27";

                    if (theDV.Table != null)
                    {
                        theDTHivAssoConditionRight = (DataTable)theUtils.CreateTableFromDataView(theDVHivAsooConditionRight);
                        int HIVAssoright = 0;
                        for (int i = 0; i < theDTHivAssoConditionRight.Rows.Count; i++)
                        {
                            HtmlTableRow trHIVAssoright = new HtmlTableRow();
                            HtmlTableCell tcHIVAssoright1 = new HtmlTableCell();
                            HtmlInputCheckBox chkHivAssoConditionright = new HtmlInputCheckBox();
                            chkHivAssoConditionright.ID = Convert.ToString("HIVAssoright" + i);
                            chkHivAssoConditionright.Value = theDTHivAssoConditionRight.Rows[i][1].ToString();
                            tcHIVAssoright1.Controls.Add(chkHivAssoConditionright);
                            tcHIVAssoright1.Controls.Add(new LiteralControl(chkHivAssoConditionright.Value));
                            trHIVAssoright.Cells.Add(tcHIVAssoright1);

                            if (chkHivAssoConditionright.Value == "Other")
                            {
                                HtmlTableCell tcHIVOther = new HtmlTableCell();
                                tcHIVOther.Controls.Add(new LiteralControl("<SPAN id='otherHIVCondition_1' style='DISPLAY:none'>Specify: "));

                                HtmlInputText HTextACondOther = new HtmlInputText();
                                HTextACondOther.ID = "txtACondOther";
                                HTextACondOther.Size = 5;
                                tcHIVOther.Controls.Add(HTextACondOther);
                                tcHIVOther.Controls.Add(new LiteralControl(HTextACondOther.Value));
                                tcHIVOther.Controls.Add(new LiteralControl("</SPAN>"));
                                trHIVAssoright.Cells.Add(tcHIVOther);

                                HtmlTableCell tcHIVAssoright2 = new HtmlTableCell();
                                tcHIVAssoright2.Controls.Add(new LiteralControl("<SPAN id='otherHIVCondition_2' class='smallerlabel' style='DISPLAY:none'>"));
                                tcHIVAssoright2.Controls.Add(new LiteralControl(" MMM-YYYY"));
                                tcHIVAssoright2.Controls.Add(new LiteralControl("</SPAN>"));
                                trHIVAssoright.Cells.Add(tcHIVAssoright2);

                                HtmlTableCell tcHIVAssoright3 = new HtmlTableCell();
                                tcHIVAssoright3.Controls.Add(new LiteralControl("<SPAN id='otherHIVCondition_3' style='DISPLAY:none'>"));
                                HtmlInputText HTextACondright = new HtmlInputText();
                                HTextACondright.ID = Convert.ToString("txtACondright" + i);
                                HTextACondright.Size = 5;
                                tcHIVAssoright3.Controls.Add(HTextACondright);
                                tcHIVAssoright3.Controls.Add(new LiteralControl(HTextACondright.Value));
                                tcHIVAssoright3.Controls.Add(new LiteralControl("</SPAN>"));
                                chkHivAssoConditionright.Attributes.Add("onclick", "toggle('otherHIVCondition_1');toggle('otherHIVCondition_2');toggle('otherHIVCondition_3') ");
                                trHIVAssoright.Cells.Add(tcHIVAssoright3);
                                HTextACondright.Attributes.Add("Onkeyup", "DateFormat(this,this.value,event,false,'4');");
                                HTextACondright.Attributes.Add("Onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtvisitDate.ClientID + "', this.id, '" + "Current Row" + "', this.id);");
                            }
                            else
                            {
                                HtmlTableCell tcHIVOther = new HtmlTableCell();
                                trHIVAssoright.Controls.Add(tcHIVOther);

                                HtmlTableCell tcHIVAssoright2 = new HtmlTableCell();
                                tcHIVAssoright2.Controls.Add(new LiteralControl("<SPAN class='smallerlabel'>"));
                                tcHIVAssoright2.Controls.Add(new LiteralControl("MMM-YYYY"));
                                tcHIVAssoright2.Controls.Add(new LiteralControl("</SPAN>"));
                                trHIVAssoright.Cells.Add(tcHIVAssoright2);

                                HtmlTableCell tcHIVAssoright3 = new HtmlTableCell();
                                HtmlInputText HTextACondright = new HtmlInputText();
                                HTextACondright.ID = Convert.ToString("txtACondright" + i);
                                HTextACondright.Size = 5;
                                tcHIVAssoright3.Controls.Add(HTextACondright);
                                tcHIVAssoright3.Controls.Add(new LiteralControl(HTextACondright.Value));
                                tcHIVAssoright3.Controls.Add(new LiteralControl("</SPAN>"));
                                trHIVAssoright.Cells.Add(tcHIVAssoright3);
                                HTextACondright.Attributes.Add("Onkeyup", "DateFormat(this,this.value,event,false,'4');");
                                HTextACondright.Attributes.Add("Onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtvisitDate.ClientID + "', this.id, '" + "Current Row" + "', this.id);");
                            }
                            HIVAssoright++;
                            tblHIVAIDSright.Rows.Add(trHIVAssoright);
                        }
                    }

                    theDV = new DataView(theDS.Tables["Mst_HivDisclosure"]);
                    theDV.RowFilter = "Deleteflag=0";
                    if (theDV.Table != null)
                    {
                        DT_disclosure = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        int col = 0;
                        for (int i = 0; i < DT_disclosure.Rows.Count; i++)
                        {
                            HtmlTableRow trdisclosure = new HtmlTableRow();
                            HtmlTableCell tcdisclosure = new HtmlTableCell();
                            HtmlInputCheckBox chkdisclosure = new HtmlInputCheckBox();
                            chkdisclosure.ID = Convert.ToString("dis" + col);
                            chkdisclosure.Value = DT_disclosure.Rows[i][1].ToString();
                            tcdisclosure.Controls.Add(chkdisclosure);
                            tcdisclosure.Controls.Add(new LiteralControl(chkdisclosure.Value));
                            trdisclosure.Controls.Add(tcdisclosure);
                            if (chkdisclosure.Value == "Other")
                            {
                                HtmlTableCell tcdisclosureother = new HtmlTableCell();
                                tcdisclosure.Controls.Add(new LiteralControl("<LABEL class=right style='font-weight:bold' for='otherdisclosure'>"));
                                tcdisclosure.Controls.Add(new LiteralControl("<DIV id='otherDisclosure' style='DISPLAY:none'>Specify: "));

                                HtmlInputText txtdisclosureother = new HtmlInputText();
                                txtdisclosureother.ID = "txtdisclosureother";
                                txtdisclosureother.Size = 10;
                                tcdisclosure.Controls.Add(txtdisclosureother);
                                tcdisclosure.Controls.Add(new LiteralControl(txtdisclosureother.Value));
                                tcdisclosure.Controls.Add(new LiteralControl("</DIV>"));
                                trdisclosure.Cells.Add(tcdisclosure);
                                chkdisclosure.Attributes.Add("onclick", "toggle('otherDisclosure');");
                            }
                            col++;
                            tblHIVdisclosure.Rows.Add(trdisclosure);
                        }
                    }

                    theDV = new DataView(theDS.Tables["Mst_Symptom"]);
                    theDV.RowFilter = "CategoryId = '4' and DeleteFlag='0'";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCheckedList(cblPresentingComplaints, theDT, "Name", "ID");
                        theDV.Dispose();
                    }

                    theDV = new DataView(theDS.Tables["Mst_Symptom"]);
                    theDV.RowFilter = "CategoryId = '14' and DeleteFlag='0' and ID not IN('70','71')";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCheckedList(cblTBScreen, theDT, "Name", "ID");
                        theDV.Dispose();
                    }

                    theDV = new DataView(theDS.Tables["Mst_Reason"]);
                    theDV.RowFilter = "CategoryId = '2' and DeleteFlag='0'";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddlTherapyChange, theDT, "Name", "ID");
                        theDV.Dispose();
                    }

                    theDV = new DataView(theDS.Tables["Mst_Reason"]);
                    theDV.RowFilter = "CategoryId = '2' and DeleteFlag='0'";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddlTherapyStop, theDT, "Name", "ID");
                        theDV.Dispose();
                    }

                    theDV = new DataView(theDS.Tables["Mst_Decode"]);
                    theDV.RowFilter = "CodeId = '26' and DeleteFlag='0'";
                    if (theDV.Table != null)
                    {
                        theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddappReason, theDT, "Name", "ID");
                        theDV.Dispose();
                    }

                    DataTable theMaster = theDS.Tables["Mst_Employee"];
                    DataView theDVEmp = new DataView(theMaster);
                    theDVEmp.RowFilter = "DeleteFlag='0'";
                    if (theDV.Table != null)
                    {
                        DataTable theDTEmp = theUtils.CreateTableFromDataView(theDVEmp);
                        if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                        {
                            theDV = new DataView(theDTEmp);
                            theDV.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
                            if (theDV.Count > 0)
                                theDTEmp = theUtils.CreateTableFromDataView(theDV);
                        }
                        BindManager.BindCombo(ddinterviewer, theDTEmp, "EmployeeName", "EmployeeId");
                        theDVEmp.Dispose();
                    }
                }
                else
                {
                    theDV = new DataView(theDS.Tables["Mst_HivDisclosure"]);
                    if (theDV.Table != null)
                    {
                        DT_disclosure = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        int col = 0;
                        for (int i = 0; i < DT_disclosure.Rows.Count; i++)
                        {
                            HtmlTableRow trdisclosure = new HtmlTableRow();
                            HtmlTableCell tcdisclosure = new HtmlTableCell();
                            HtmlInputCheckBox chkdisclosure = new HtmlInputCheckBox();
                            chkdisclosure.ID = Convert.ToString("dis" + col);
                            chkdisclosure.Value = DT_disclosure.Rows[i][1].ToString();
                            tcdisclosure.Controls.Add(chkdisclosure);
                            tcdisclosure.Controls.Add(new LiteralControl(chkdisclosure.Value));
                            trdisclosure.Controls.Add(tcdisclosure);
                            if (chkdisclosure.Value == "Other")
                            {
                                HtmlTableCell tcdisclosureother = new HtmlTableCell();
                                tcdisclosure.Controls.Add(new LiteralControl("<LABEL class=right style='font-weight:bold' for='otherdisclosure'>"));
                                tcdisclosure.Controls.Add(new LiteralControl("<DIV id='otherDisclosure' style='DISPLAY:none'>Specify: "));

                                HtmlInputText txtdisclosureother = new HtmlInputText();
                                txtdisclosureother.ID = "txtdisclosureother";
                                txtdisclosureother.Size = 10;
                                tcdisclosure.Controls.Add(txtdisclosureother);
                                tcdisclosure.Controls.Add(new LiteralControl(txtdisclosureother.Value));
                                tcdisclosure.Controls.Add(new LiteralControl("</DIV>"));
                                trdisclosure.Cells.Add(tcdisclosure);
                                chkdisclosure.Attributes.Add("onclick", "toggle('otherDisclosure');");
                            }
                            col++;
                            tblHIVdisclosure.Rows.Add(trdisclosure);
                        }
                    }

                    theDV = new DataView(theDS.Tables["Mst_Symptom"]);
                    theDV.RowFilter = "CategoryId = '4'";
                    if (theDV.Table != null)
                    {
                        theDT = theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCheckedList(cblPresentingComplaints, theDT, "Name", "ID");
                        theDV.Dispose();
                    }

                    theDV = new DataView(theDS.Tables["Mst_Symptom"]);
                    theDV.RowFilter = "CategoryId = '14' and ID not IN('70','71')";
                    if (theDV.Table != null)
                    {
                        theDT = theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCheckedList(cblTBScreen, theDT, "Name", "ID");
                        theDV.Dispose();
                    }

                    theDV = new DataView(theDS.Tables["Mst_Decode"]);
                    theDV.RowFilter = "CodeId = '26'";
                    if (theDV.Table != null)
                    {
                        theDT = theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddappReason, theDT, "Name", "ID");
                        theDV.Dispose();
                    }

                    theDV = new DataView(theDS.Tables["Mst_Reason"]);
                    theDV.RowFilter = "CategoryId = '2'";
                    if (theDV.Table != null)
                    {
                        theDT = theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddlTherapyChange, theDT, "Name", "ID");
                        theDV.Dispose();
                    }

                    theDV = new DataView(theDS.Tables["Mst_Reason"]);
                    theDV.RowFilter = "CategoryId = '2'";
                    if (theDV.Table != null)
                    {
                        theDT = theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddlTherapyStop, theDT, "Name", "ID");
                        theDV.Dispose();
                    }
                    theDTHivAssoConditionLeft = theDSDB.Tables[0];
                    int HIVAssoleft = 0;
                    for (int i = 0; i < theDTHivAssoConditionLeft.Rows.Count; i++)
                    {
                        HtmlTableRow trHIVAssoleft = new HtmlTableRow();
                        HtmlTableCell tcHIVAssoleft1 = new HtmlTableCell();
                        HtmlTableCell tcHIVAssoleft2 = new HtmlTableCell();
                        HtmlTableCell tcHIVAssoleft3 = new HtmlTableCell();
                        if ((i != 1) && (i != 2))
                        {
                            tcHIVAssoleft1.Width = "60%";
                            HtmlInputCheckBox chkHivAssoConditionLeft = new HtmlInputCheckBox();
                            chkHivAssoConditionLeft.ID = Convert.ToString("HIVAssoleft" + i);
                            chkHivAssoConditionLeft.Value = theDTHivAssoConditionLeft.Rows[i][1].ToString();
                            tcHIVAssoleft1.Controls.Add(chkHivAssoConditionLeft);
                            tcHIVAssoleft1.Controls.Add(new LiteralControl(chkHivAssoConditionLeft.Value));
                            trHIVAssoleft.Cells.Add(tcHIVAssoleft1);

                            tcHIVAssoleft2.Width = "20%";
                            tcHIVAssoleft2.Controls.Add(new LiteralControl("MMM-YYYY"));
                            trHIVAssoleft.Cells.Add(tcHIVAssoleft2);

                            tcHIVAssoleft3.Width = "20%";
                            HtmlInputText HTextACondleft = new HtmlInputText();
                            HTextACondleft.ID = Convert.ToString("txtACondleft" + i);
                            HTextACondleft.Size = 5;
                            tcHIVAssoleft3.Controls.Add(HTextACondleft);
                            tcHIVAssoleft3.Controls.Add(new LiteralControl(HTextACondleft.Value));
                            tcHIVAssoleft3.Controls.Add(new LiteralControl("</SPAN>"));
                            trHIVAssoleft.Cells.Add(tcHIVAssoleft3);
                            HTextACondleft.Attributes.Add("Onkeyup", "DateFormat(this,this.value,event,false,'4');");
                            HTextACondleft.Attributes.Add("Onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtvisitDate.ClientID + "', this.id, '" + "Current Row" + "', this.id);");
                            if (chkHivAssoConditionLeft.Value == "Pulmonary TB")
                            {
                                chkHivAssoConditionLeft.Attributes.Add("onclick", "toggle('pultb1'); toggle('pultb2')");
                            }
                            HIVAssoleft++;
                        }
                        if (i == 1)
                        {
                            tcHIVAssoleft2.Controls.Add(new LiteralControl("<div id='pultb1' style='display:none; padding-left:20px'>"));
                            HtmlInputRadioButton rdopultbsmplus = new HtmlInputRadioButton();
                            rdopultbsmplus.ID = Convert.ToString("txtACondleftrdo" + 11);
                            rdopultbsmplus.Value = theDTHivAssoConditionLeft.Rows[1][1].ToString();
                            rdopultbsmplus.Attributes.Add("onfocus", "up(this);");
                            rdopultbsmplus.Attributes.Add("onclick", "down(this);");
                            tcHIVAssoleft2.Controls.Add(rdopultbsmplus);
                            tcHIVAssoleft2.Controls.Add(new LiteralControl(rdopultbsmplus.Value));
                            trHIVAssoleft.Cells.Add(tcHIVAssoleft2);
                            tcHIVAssoleft2.Controls.Add(new LiteralControl("</div>"));
                            HIVAssoleft++;
                        }
                        if (i == 2)
                        {
                            tcHIVAssoleft2.Controls.Add(new LiteralControl("<div id='pultb2' style='display:none; padding-left:20px'>"));
                            HtmlInputRadioButton rdopultbsminus = new HtmlInputRadioButton();
                            rdopultbsminus.ID = Convert.ToString("txtACondleftrdo" + 12);
                            rdopultbsminus.Value = theDTHivAssoConditionLeft.Rows[2][1].ToString();
                            rdopultbsminus.Attributes.Add("onfocus", "up(this);");
                            rdopultbsminus.Attributes.Add("onclick", "down(this);");
                            tcHIVAssoleft2.Controls.Add(rdopultbsminus);
                            tcHIVAssoleft2.Controls.Add(new LiteralControl(rdopultbsminus.Value));
                            trHIVAssoleft.Cells.Add(tcHIVAssoleft2);
                            tcHIVAssoleft2.Controls.Add(new LiteralControl("</div>"));
                            HIVAssoleft++;
                        }
                        HIVAssoleft++;
                        tblHIVAIDSleft.Rows.Add(trHIVAssoleft);
                    }
                    DataView theDVHivAsooConditionRight = new DataView(theDSDB.Tables[1]);
                    if (theDV.Table != null)
                    {
                        theDTHivAssoConditionRight = (DataTable)theUtils.CreateTableFromDataView(theDVHivAsooConditionRight);
                        int HIVAssoright = 0;
                        for (int i = 0; i < theDTHivAssoConditionRight.Rows.Count; i++)
                        {
                            HtmlTableRow trHIVAssoright = new HtmlTableRow();
                            HtmlTableCell tcHIVAssoright1 = new HtmlTableCell();
                            HtmlInputCheckBox chkHivAssoConditionright = new HtmlInputCheckBox();
                            chkHivAssoConditionright.ID = Convert.ToString("HIVAssoright" + i);
                            chkHivAssoConditionright.Value = theDTHivAssoConditionRight.Rows[i][1].ToString();
                            tcHIVAssoright1.Controls.Add(chkHivAssoConditionright);
                            tcHIVAssoright1.Controls.Add(new LiteralControl(chkHivAssoConditionright.Value));
                            trHIVAssoright.Cells.Add(tcHIVAssoright1);

                            if (chkHivAssoConditionright.Value == "Other")
                            {
                                HtmlTableCell tcHIVOther = new HtmlTableCell();
                                tcHIVOther.Controls.Add(new LiteralControl("<SPAN id='otherHIVCondition_1' style='DISPLAY:none'>Specify: "));

                                HtmlInputText HTextACondOther = new HtmlInputText();
                                HTextACondOther.ID = "txtACondOther";
                                HTextACondOther.Size = 5;
                                tcHIVOther.Controls.Add(HTextACondOther);
                                tcHIVOther.Controls.Add(new LiteralControl(HTextACondOther.Value));
                                tcHIVOther.Controls.Add(new LiteralControl("</SPAN>"));
                                trHIVAssoright.Cells.Add(tcHIVOther);

                                HtmlTableCell tcHIVAssoright2 = new HtmlTableCell();
                                tcHIVAssoright2.Controls.Add(new LiteralControl("<SPAN id='otherHIVCondition_2' style='DISPLAY:none'>"));
                                tcHIVAssoright2.Controls.Add(new LiteralControl(" MM-YYYY"));
                                tcHIVAssoright2.Controls.Add(new LiteralControl("</SPAN>"));
                                trHIVAssoright.Cells.Add(tcHIVAssoright2);

                                HtmlTableCell tcHIVAssoright3 = new HtmlTableCell();
                                tcHIVAssoright3.Controls.Add(new LiteralControl("<SPAN id='otherHIVCondition_3' style='DISPLAY:none'>"));
                                HtmlInputText HTextACondright = new HtmlInputText();
                                HTextACondright.ID = Convert.ToString("txtACondright" + i);
                                HTextACondright.Size = 5;
                                tcHIVAssoright3.Controls.Add(HTextACondright);
                                tcHIVAssoright3.Controls.Add(new LiteralControl(HTextACondright.Value));
                                tcHIVAssoright3.Controls.Add(new LiteralControl("</SPAN>"));
                                chkHivAssoConditionright.Attributes.Add("onclick", "toggle('otherHIVCondition_1');toggle('otherHIVCondition_2');toggle('otherHIVCondition_3') ");
                                trHIVAssoright.Cells.Add(tcHIVAssoright3);
                                HTextACondright.Attributes.Add("Onkeyup", "DateFormat(this,this.value,event,false,'4');");
                                HTextACondright.Attributes.Add("Onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtvisitDate.ClientID + "', this.id, '" + "Current Row" + "', this.id);");
                            }
                            else
                            {
                                HtmlTableCell tcHIVOther = new HtmlTableCell();
                                trHIVAssoright.Controls.Add(tcHIVOther);

                                HtmlTableCell tcHIVAssoright2 = new HtmlTableCell();
                                tcHIVAssoright2.Controls.Add(new LiteralControl("MM-YYYY"));
                                trHIVAssoright.Cells.Add(tcHIVAssoright2);

                                HtmlTableCell tcHIVAssoright3 = new HtmlTableCell();
                                HtmlInputText HTextACondright = new HtmlInputText();
                                HTextACondright.ID = Convert.ToString("txtACondright" + i);
                                HTextACondright.Size = 5;
                                tcHIVAssoright3.Controls.Add(HTextACondright);
                                tcHIVAssoright3.Controls.Add(new LiteralControl(HTextACondright.Value));
                                tcHIVAssoright3.Controls.Add(new LiteralControl("</SPAN>"));
                                trHIVAssoright.Cells.Add(tcHIVAssoright3);
                                HTextACondright.Attributes.Add("Onkeyup", "DateFormat(this,this.value,event,false,'4');");
                                HTextACondright.Attributes.Add("Onblur", "DateFormat(this,this.value,event,true,'4'); isCheckValidDate_MMM_YR('" + txtvisitDate.ClientID + "', this.id, '" + "Current Row" + "', this.id);");
                            }
                            HIVAssoright++;
                            tblHIVAIDSright.Rows.Add(trHIVAssoright);
                        }
                    }

                    DataTable theMaster = theDS.Tables["Mst_Employee"];
                    DataView theDVEmp = new DataView(theMaster);
                    if (theDV.Table != null)
                    {
                        DataTable theDTEmp = theUtils.CreateTableFromDataView(theDVEmp);
                        if (Convert.ToInt32(Session["AppUserEmployeeId"]) > 0)
                        {
                            theDV = new DataView(theDTEmp);
                            theDV.RowFilter = "EmployeeId =" + Session["AppUserEmployeeId"].ToString();
                            if (theDV.Count > 0)
                                theDTEmp = theUtils.CreateTableFromDataView(theDV);
                        }
                        BindManager.BindCombo(ddinterviewer, theDTEmp, "EmployeeName", "EmployeeId");
                        theDVEmp.Dispose();
                    }
                }
                //Disclosure Part
                DisclosureOtherID = Convert.ToInt32(theDSDB.Tables[2].Rows[0]["ID"]);

                theDV = new DataView(theDS.Tables["Mst_Assessment"]);
                theDV.RowFilter = "AssessmentName = 'HIV related illness/OI'";
                AssessmentPlanID1 = Convert.ToInt32(theDV[0]["AssessmentId"]);

                theDV.RowFilter = "AssessmentName = 'Non-HIV-related illness'";
                AssessmentPlanID2 = Convert.ToInt32(theDV[0]["AssessmentId"]);

                theDV.RowFilter = "AssessmentName = 'Lab evaluation/TB Screen'";
                AssessmentPlanID3 = Convert.ToInt32(theDV[0]["AssessmentId"]);

                theDV.RowFilter = "AssessmentName = 'OI propphylaxis/treatment'";
                AssessmentPlanID4 = Convert.ToInt32(theDV[0]["AssessmentId"]);

                theDV.RowFilter = "AssessmentName = 'Treatment Preparation'";
                AssessmentPlanID5 = Convert.ToInt32(theDV[0]["AssessmentId"]);

                theDV.RowFilter = "AssessmentName = 'Other'";
                AssessmentPlanID6 = Convert.ToInt32(theDV[0]["AssessmentId"]);

                theDV.RowFilter = "AssessmentName = 'Not Documented'";
                AssessmentPlanID7 = Convert.ToInt32(theDV[0]["AssessmentId"]);

                theDV.RowFilter = "AssessmentName = 'None'";
                AssessmentPlanID8 = Convert.ToInt32(theDV[0]["AssessmentId"]);

                //Change/Stop Therapy Reason ID
                ReasonID = Convert.ToInt32(theDSDB.Tables[3].Rows[0]["OtherReason"]);
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

        protected void Init_Add_UpdateInitial_Evaluation()
        {
            IInitialEval IEManager;
            try
            {
                PId = Convert.ToInt32(Session["PatientId"]);
                IEManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
                DataSet theDS = IEManager.GetPatientInitialEvaluation(PId);
                ViewState["MasterData"] = theDS.Tables[9];
                ViewState["ARVMasterData"] = theDS.Tables[16];
                DataSet theOldDS = new DataSet();
                theOldDS.Tables.Add(theDS.Tables[9].Copy());
                theOldDS.Tables.Add(theDS.Tables[10].Copy());
                theOldDS.Tables.Add(theDS.Tables[16].Copy());
                ViewState["OldData"] = theOldDS;
                ViewState["SelectedData"] = theCurrentRegDT;     //CreateSelectedTable();
                ViewState["SelectedData1"] = theRegimen1;
                ViewState["SelectedData2"] = theRegimen2;
                ViewState["SelectedData3"] = theRegimen3;
                ViewState["SelectedData4"] = theRegimen4;
                if (Convert.ToInt32(theDS.Tables[0].Rows[0]["Sex"].ToString()) == 16)
                {
                    tdPregnant.Visible = false;
                }
                //Using for Default Values
                rdoMedHistnotdocumented.Checked = true;
                if (theDS.Tables[15].Rows[0]["LMP"] != System.DBNull.Value)
                {
                    txtLMPdate.Value = string.Format("{0:dd-MMM-yyyy}", theDS.Tables[15].Rows[0]["LMP"]);
                }
                //For Editing Records
                if (Convert.ToInt32(Session["PatientVisitId"]) > 0)
                {
                    //from Patient History Page
                    PId = Convert.ToInt32(Session["PatientId"]);
                    visitPK = Convert.ToInt32(Session["PatientVisitId"]);
                    int locationID = Convert.ToInt32(Session["ServiceLocationId"]);
                    ViewState["DataQuality"] = "0";

                    DataView theDV = new DataView(theDS.Tables[9]);

                    IQCareUtils theUtil = new IQCareUtils();
                    DataSet theDS1 = IEManager.GetInitialEvaluationUpdate(visitPK, PId, locationID);

                    this.txtvisitDate.Text = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[0].Rows[0]["VisitDate"]);
                    ViewState["VisitDate"] = this.txtvisitDate.Text;
                    ViewState["createdate"] = Convert.ToDateTime(theDS1.Tables[0].Rows[0]["CreateDate"].ToString());

                    if (theDS1.Tables[0].Rows[0]["DataQuality"] != System.DBNull.Value && Convert.ToInt32(theDS1.Tables[0].Rows[0]["DataQuality"]) == 1)
                    {
                        btncomplete.CssClass = "greenbutton";
                    }

                    if (theDS1.Tables[1].Rows.Count > 0)
                    {
                        if (theDS1.Tables[1].Rows[0]["DateHIVDiagnosis"] != System.DBNull.Value)
                        {
                            this.txtHIVDiagnosisdate.Value = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[1].Rows[0]["DateHIVDiagnosis"]);
                        }

                        if (theDS1.Tables[1].Rows[0]["Disclosure"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["Disclosure"].ToString()) == 1)
                            {
                                this.rdoDisclosureYes.Checked = true;
                            }
                            else if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["Disclosure"].ToString()) == 0)
                            {
                                this.rdoDisclosureNo.Checked = true;
                            }
                        }

                        if (theDS1.Tables[1].Rows[0]["HIVDiagnosisVerified"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["HIVDiagnosisVerified"].ToString()) == 1)
                            {
                                this.rdoHIVDiagnosisVerifiedYes.Checked = true;
                            }
                            else if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["HIVDiagnosisVerified"].ToString()) == 0)
                            {
                                this.rdoHIVDiagnosisVerifiedNo.Checked = true;
                            }
                        }

                        if (theDS1.Tables[1].Rows[0]["Pregnant"] != System.DBNull.Value)
                        {
                            this.txtLMPdate.Value = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[1].Rows[0]["LMP"]);
                            if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["Pregnant"].ToString()) == 1)
                            {
                                if (theDS1.Tables[1].Rows[0]["EDD"] != System.DBNull.Value)
                                {
                                    this.txtEDDDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[1].Rows[0]["EDD"]);
                                }
                                this.rdopregnantYes.Checked = true;
                                script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
                                script += "show('rdopregnantyesno');\n";
                                script += "hide('spdelivery');\n";
                                script += "show('spanEDD');\n";
                                script += "</script>\n";
                                ClientScript.RegisterStartupScript(this.GetType(), "PregnantYes", script);
                                ViewState["Pregstatus"] = "1";
                            }
                            if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["Pregnant"].ToString()) == 0)
                            {
                                this.rdopregnantNo.Checked = true;
                                script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'PregnantYes'>\n";
                                script += "show('rdopregnantyesno');\n";
                                script += "hide('spdelivery');\n";
                                script += "hide('spanEDD');\n";
                                script += "</script>\n";
                                ClientScript.RegisterStartupScript(this.GetType(), "PregnantYes", script);
                                ViewState["Pregstatus"] = "2";
                            }
                        }

                        if (theDS1.Tables[1].Rows[0]["Delivered"] != System.DBNull.Value)
                        {
                            if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["Delivered"]) == 0)
                            {
                                this.rdoDeliveredNo.Checked = true;
                                if (theDS1.Tables[16].Rows[0]["EDD"] != System.DBNull.Value)
                                {
                                    this.txtEDDDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[1].Rows[0]["EDD"]);
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'EDD'>\n";
                                    script += "show('spdelivery');\n";
                                    script += "hide('rdopregnantyesno');\n";
                                    script += "show('spanEDD');\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "EDD", script);
                                    ViewState["Pregstatus"] = "3";
                                }
                            }
                            else if (Convert.ToInt32(theDS1.Tables[1].Rows[0]["Delivered"]) == 1)
                            {
                                this.rdoDeliveredYes.Checked = true;
                                if (theDS1.Tables[1].Rows[0]["DateofDelivery"] != System.DBNull.Value)
                                {
                                    this.txtDeliDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS1.Tables[1].Rows[0]["DateofDelivery"]);
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'EDD'>\n";
                                    script += "show('spdelivery');\n";
                                    script += "show('spanDelDate');\n";
                                    script += "hide('rdopregnantyesno');\n";
                                    script += "hide('spanEDD');\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "EDD", script);
                                    ViewState["Pregstatus"] = "4";
                                }
                            }
                        }
                        if (theDS1.Tables[1].Rows[0]["LMP"] != System.DBNull.Value)
                        {
                            DateTime theTmpDt3 = Convert.ToDateTime(theDS1.Tables[1].Rows[0]["LMP"]);
                            this.txtLMPdate.Value = theTmpDt3.ToString(Session["AppDateFormat"].ToString());
                        }
                    }

                    // Presenting Complaints Part
                    if (theDS1.Tables[9].Rows.Count > 0)
                    {
                        for (int i = 0; i < theDS1.Tables[9].Rows.Count; i++)
                        {
                            for (int j = 0; j < cblPresentingComplaints.Items.Count; j++)
                            {
                                if (cblPresentingComplaints.Items[j].Value == theDS1.Tables[9].Rows[i]["SymptomID"].ToString())
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
                    }

                    if (theDS1.Tables[9].Rows.Count > 0)
                    {
                        for (int i = 0; i < theDS1.Tables[9].Rows.Count; i++)
                        {
                            for (int j = 0; j < cblTBScreen.Items.Count; j++)
                            {
                                if (cblTBScreen.Items[j].Value == theDS1.Tables[9].Rows[i]["SymptomID"].ToString())
                                {
                                    cblTBScreen.Items[j].Selected = true;
                                    rdoPerformed.Checked = true;
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'TBSympton'>\n";
                                    script += "show('divTBPerformed');\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "TBSympton", script);
                                }
                            }
                            if (theDS1.Tables[9].Rows[i]["SymptomID"].ToString() == "70")
                            {
                                rdoPerformed.Checked = true;
                                script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'TBSympton1'>\n";
                                script += "show('divTBPerformed');\n";
                                script += "</script>\n";
                                ClientScript.RegisterStartupScript(this.GetType(), "TBSympton1", script);
                            }
                            else if (theDS1.Tables[9].Rows[i]["SymptomID"].ToString() == "71")
                            {
                                rdoNotDocumented.Checked = true;
                                script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'TBSympton2'>\n";
                                script += "hide('divTBPerformed');\n";
                                script += "</script>\n";
                                ClientScript.RegisterStartupScript(this.GetType(), "TBSympton2", script);
                            }
                            //else if (theDS1.Tables[9].Rows[i]["SymptomID"].ToString() == "78")
                            //{
                            //    rdoNone.Checked = true;
                            //    script = "";
                            //    script = "<script language = 'javascript' defer ='defer' id = 'TBSympton3'>\n";
                            //    script += "hide('divTBPerformed');\n";
                            //    script += "</script>\n";
                            //    ClientScript.RegisterStartupScript(this.GetType(),"TBSympton3", script);
                            //}
                        }
                    }

                    //Allergy Part
                    if (theDS1.Tables[2].Rows.Count > 0)
                    {
                        if (theDS1.Tables[2].Rows[0]["AllergyID"] != System.DBNull.Value)
                        {
                            int SulfaAlleryID = Convert.ToInt32(theDS1.Tables[2].Rows[0]["AllergyID"].ToString());
                            if (SulfaAlleryID == 233)
                            {
                                rdoAllergynone.Checked = true;
                            }
                            else if (SulfaAlleryID == 234)
                            {
                                rdoAllergynotdocumented.Checked = true;
                            }
                            else if (SulfaAlleryID == 82)
                            {
                                this.chksulfaAllergy.Checked = true;
                            }
                        }
                    }
                    if (theDS1.Tables[12].Rows.Count > 0)
                    {
                        if (theDS1.Tables[12].Rows[0]["AllergyID"] != System.DBNull.Value)
                        {
                            if (theDS1.Tables[12].Rows[0]["AllergyID"].ToString() == "83")
                            {
                            }
                        }

                        if (theDS1.Tables[12].Rows[0]["AllergyNameOther"] != System.DBNull.Value)
                        {
                            this.txtotherAllergyName.Value = theDS1.Tables[12].Rows[0]["AllergyNameOther"].ToString();
                        }
                        script = "";
                        script = "<script language = 'javascript' defer ='defer' id = 'otherAllergy'>\n";
                        script += "document.getElementById('" + chkotherAllergy.ClientID + "').click();\n";
                        script += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "otherAllergy", script);
                    }

                    //Current Long Term Medication Part
                    if (theDS1.Tables[3].Rows.Count > 0)
                    {
                        if (theDS1.Tables[3].Rows[0]["longTermMedsSulfa"] != System.DBNull.Value)
                        {
                            int MedsSulfta = 1;
                            if (MedsSulfta == Convert.ToInt32(theDS1.Tables[3].Rows[0]["longTermMedsSulfa"]))
                            {
                                if (theDS1.Tables[3].Rows[0]["longTermMedsSulfaDesc"] != System.DBNull.Value)
                                {
                                    this.txtlongTermMedsSulfaDesc.Value = theDS1.Tables[3].Rows[0]["longTermMedsSulfaDesc"].ToString();
                                }
                                script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'longTermMedsSulfa'>\n";
                                script += "document.getElementById('" + chklongTermMedsSulfa.ClientID + "').click();\n";
                                script += "</script>\n";
                                ClientScript.RegisterStartupScript(this.GetType(), "longTermMedsSulfa", script);
                            }
                        }
                        //Long Term TB Medication

                        if (theDS1.Tables[3].Rows[0]["longTermTBMed"] != System.DBNull.Value)
                        {
                            int TBMed = 1;
                            if (TBMed == Convert.ToInt32(theDS1.Tables[3].Rows[0]["longTermTBMed"]))
                            {
                                if (theDS1.Tables[3].Rows[0]["longTermTBMedDesc"] != System.DBNull.Value)
                                {
                                    this.txtLongTermTBMedDesc.Value = theDS1.Tables[3].Rows[0]["longTermTBMedDesc"].ToString();
                                }

                                if (theDS1.Tables[3].Rows[0]["longTermTBStartDate"] != System.DBNull.Value)
                                {
                                    this.txtLongTermTBStartDate.Value = String.Format("{0:MMM-yyyy}", theDS1.Tables[3].Rows[0]["longTermTBStartDate"]);
                                }
                                script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'LongTermTBMed'>\n";
                                script += "document.getElementById('" + chkLongTermTBMed.ClientID + "').click();\n";
                                script += "</script>\n";
                                ClientScript.RegisterStartupScript(this.GetType(), "LongTermTBMed", script);
                            }
                        }
                        //longTermMedsOther1

                        if (theDS1.Tables[3].Rows[0]["longTermMedsOther1"] != System.DBNull.Value)
                        {
                            int MedsOther1 = 1;
                            if (MedsOther1 == Convert.ToInt32(theDS1.Tables[3].Rows[0]["longTermMedsOther1"]))
                            {
                                if (theDS1.Tables[3].Rows[0]["longTermMedsOther1Desc"] != System.DBNull.Value)
                                {
                                    this.txtlongTermMedsOther1Desc.Value = theDS1.Tables[3].Rows[0]["longTermMedsOther1Desc"].ToString();
                                }
                                script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'longTermMedsOther1'>\n";
                                script += "document.getElementById('" + chklongTermMedsOther1.ClientID + "').click();\n";
                                script += "</script>\n";
                                ClientScript.RegisterStartupScript(this.GetType(), "longTermMedsOther1", script);
                            }
                        }

                        //longTermMedsOther2
                        if (theDS1.Tables[3].Rows[0]["longTermMedsOther2"] != System.DBNull.Value)
                        {
                            int MedsOther2 = 1;
                            if (MedsOther2 == Convert.ToInt32(theDS1.Tables[3].Rows[0]["longTermMedsOther2"]))
                            {
                                if (theDS1.Tables[3].Rows[0]["longTermMedsOther2Desc"] != System.DBNull.Value)
                                {
                                    //chklongTermMedsOther2.Checked = true;
                                    this.txtlongTermMedsOther2Desc.Value = theDS1.Tables[3].Rows[0]["longTermMedsOther2Desc"].ToString();
                                }
                                script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'longTermMedsOther2'>\n";
                                script += "document.getElementById('" + chklongTermMedsOther2.ClientID + "').click();\n";
                                script += "</script>\n";
                                ClientScript.RegisterStartupScript(this.GetType(), "longTermMedsOther2", script);
                            }
                        }

                        //Lowest CD4 Section

                        //Lowest CD4 None
                        if (theDS1.Tables[3].Rows[0]["PrevLowestCD4None"] != System.DBNull.Value)
                        {
                            int CD4 = Convert.ToInt32(theDS1.Tables[3].Rows[0]["PrevLowestCD4None"].ToString());
                            if (CD4 == 1)
                            {
                                rdoprevLowestCD4none.Checked = true;
                            }
                        }
                        //Lowest CD4 NotDocumented
                        if (theDS1.Tables[3].Rows[0]["PrevLowestCD4NotDocumented"] != System.DBNull.Value)
                        {
                            int CD4 = Convert.ToInt32(theDS1.Tables[3].Rows[0]["PrevLowestCD4NotDocumented"].ToString());
                            if (CD4 == 1)
                            {
                                rdoprevLowestCD4notdocumented.Checked = true;
                            }
                        }
                        //Lowest CD4 Value
                        if (theDS1.Tables[3].Rows[0]["PrevLowestCD4"] != System.DBNull.Value)
                        {
                            this.txtprevLowestCD4.Value = theDS1.Tables[3].Rows[0]["PrevLowestCD4"].ToString();
                        }
                        //Lowest CD4 Percent
                        if (theDS1.Tables[3].Rows[0]["PrevLowestCD4Percent"] != System.DBNull.Value)
                        {
                            this.txtprevLowestCD4Percent.Value = theDS1.Tables[3].Rows[0]["PrevLowestCD4Percent"].ToString();
                        }

                        //Lowest CD4 Date
                        if (theDS1.Tables[3].Rows[0]["PrevLowestCD4Date"] != System.DBNull.Value)
                        {
                            DateTime theTmpDt4 = Convert.ToDateTime(theDS1.Tables[3].Rows[0]["PrevLowestCD4Date"]);
                            this.txtprevLowestCD4Date.Value = theTmpDt4.ToString(Session["AppDateFormat"].ToString());
                        }

                        //CD4 Prior to Starting ARVs Section
                        if (theDS1.Tables[3].Rows.Count > 0)
                        {
                            //CD4 Prior to Starting ARVs None

                            if (theDS1.Tables[3].Rows[0]["PrevARVsCD4None"] != System.DBNull.Value)
                            {
                                int PrevARVsCD4 = Convert.ToInt32(theDS1.Tables[3].Rows[0]["PrevARVsCD4None"].ToString());
                                if (PrevARVsCD4 == 1)
                                {
                                    rdopriorARVsCD4none.Checked = true;
                                }
                            }

                            //CD4 Prior to Starting ARVs Not Documented

                            if (theDS1.Tables[3].Rows[0]["PrevARVsCD4NotDocumented"] != System.DBNull.Value)
                            {
                                int PrevARVsCD4 = Convert.ToInt32(theDS1.Tables[3].Rows[0]["PrevARVsCD4NotDocumented"].ToString());
                                if (PrevARVsCD4 == 1)
                                {
                                    rdopriorARVsCD4notdocumented.Checked = true;
                                }
                            }

                            //CD4 Prior to Starting ARVs Value
                            if (theDS1.Tables[3].Rows[0]["PrevARVsCD4"] != System.DBNull.Value)
                            {
                                this.txtpriorARVsCD4.Value = theDS1.Tables[3].Rows[0]["PrevARVsCD4"].ToString();
                            }

                            //CD4 Prior to Starting ARVs Percent
                            if (theDS1.Tables[3].Rows[0]["PrevARVsCD4Percent"] != System.DBNull.Value)
                            {
                                this.txtpriorARVsCD4Percent.Value = theDS1.Tables[3].Rows[0]["PrevARVsCD4Percent"].ToString();
                            }
                            // CD4 Prior to Starting ARVs Value
                            if (theDS1.Tables[3].Rows[0]["PrevARVsCD4Date"] != System.DBNull.Value)
                            {
                                DateTime TmptxtpriorARVsCD4Date = Convert.ToDateTime(theDS1.Tables[3].Rows[0]["PrevARVsCD4Date"]);
                                this.txtpriorARVsCD4Date.Value = TmptxtpriorARVsCD4Date.ToString(Session["AppDateFormat"].ToString());
                            }
                        }

                        //Most Recent CD4 Section
                        //Most Recent CD4 Section None
                        if (theDS1.Tables[3].Rows[0]["PrevMostRecentCD4None"] != System.DBNull.Value)
                        {
                            int PrevMostRecentCD4 = Convert.ToInt32(theDS1.Tables[3].Rows[0]["PrevMostRecentCD4None"].ToString());
                            if (PrevMostRecentCD4 == 1)
                            {
                                rdomostRecentCD4none.Checked = true;
                            }
                        }
                        //Most Recent CD4 Section Not documented
                        if (theDS1.Tables[3].Rows[0]["PrevMostRecentCD4NotDocumented"] != System.DBNull.Value)
                        {
                            int PrevMostRecentCD4 = Convert.ToInt32(theDS1.Tables[3].Rows[0]["PrevMostRecentCD4NotDocumented"].ToString());
                            if (PrevMostRecentCD4 == 1)
                            {
                                rdomostRecentCD4notdocumented.Checked = true;
                            }
                        }
                        //Most Recent CD4 Value
                        if (theDS1.Tables[3].Rows[0]["PrevMostRecentCD4"] != System.DBNull.Value)
                        {
                            this.txtmostRecentCD4.Value = theDS1.Tables[3].Rows[0]["PrevMostRecentCD4"].ToString();
                        }
                        //Most Recent CD4 Percent
                        if (theDS1.Tables[3].Rows[0]["PrevMostRecentCD4Percent"] != System.DBNull.Value)
                        {
                            this.txtmostRecentCD4Percent.Value = theDS1.Tables[3].Rows[0]["PrevMostRecentCD4Percent"].ToString();
                        }
                        //Most Recent CD4 Date
                        if (theDS1.Tables[3].Rows[0]["PrevMostRecentCD4Date"] != System.DBNull.Value)
                        {
                            DateTime theTmpDt6 = Convert.ToDateTime(theDS1.Tables[3].Rows[0]["PrevMostRecentCD4Date"]);
                            this.txtmostRecentCD4Date.Value = theTmpDt6.ToString(Session["AppDateFormat"].ToString());
                        }

                        //Most Recent Viral Load
                        //Most Recent Viral Load None
                        if (theDS1.Tables[3].Rows[0]["PrevMostRecentViralLoadNone"] != System.DBNull.Value)
                        {
                            int PrevMostRecentViralLoad = Convert.ToInt32(theDS1.Tables[3].Rows[0]["PrevMostRecentViralLoadNone"].ToString());
                            if (PrevMostRecentViralLoad == 1)
                            {
                                rdomostRecentViralLoadnone.Checked = true;
                            }
                        }
                        //Most Recent Viral Load Not documented
                        if (theDS1.Tables[3].Rows[0]["PrevMostRecentViralLoadNotDocumented"] != System.DBNull.Value)
                        {
                            int PrevMostRecentViralLoad = Convert.ToInt32(theDS1.Tables[3].Rows[0]["PrevMostRecentViralLoadNotDocumented"].ToString());
                            if (PrevMostRecentViralLoad == 1)
                            {
                                rdomostRecentViralLoadnotdocumented.Checked = true;
                            }
                        }

                        //Most Recent Viral Load Value
                        if (theDS1.Tables[3].Rows[0]["PrevMostRecentViralLoad"] != System.DBNull.Value)
                        {
                            this.txtmostRecentViralLoad.Value = theDS1.Tables[3].Rows[0]["PrevMostRecentViralLoad"].ToString();
                        }
                        //Most Recent Viral Load Date
                        if (theDS1.Tables[3].Rows[0]["PrevMostRecentViralLoadDate"] != System.DBNull.Value)
                        {
                            DateTime theTmpDt7 = Convert.ToDateTime(theDS1.Tables[3].Rows[0]["PrevMostRecentViralLoadDate"]);
                            this.txtmostRecentViralLoadDate.Value = theTmpDt7.ToString(Session["AppDateFormat"].ToString());
                        }

                        //ARV Exposure

                        if (theDS1.Tables[3].Rows[0]["PrevARVExposureNone"] != System.DBNull.Value)
                        {
                            int PrevARVExposureNone = Convert.ToInt32(theDS1.Tables[3].Rows[0]["PrevARVExposureNone"]);
                            if (PrevARVExposureNone == 1)
                            {
                                rdoprevARVExposureNone.Checked = true;
                            }
                        }

                        if (theDS1.Tables[3].Rows[0]["PrevARVExposureNotDocumented"] != System.DBNull.Value)
                        {
                            int PrevARVExposureARTNotdocumented = Convert.ToInt32(theDS1.Tables[3].Rows[0]["PrevARVExposureNotDocumented"]);
                            if (PrevARVExposureARTNotdocumented == 1)
                            {
                                rdoprevARVExpnotdocumented.Checked = true;
                            }
                        }

                        if (theDS1.Tables[3].Rows[0]["PrevARVExposure"] != System.DBNull.Value)
                        {
                            if (theDS1.Tables[3].Rows[0]["CurrentART"] != System.DBNull.Value)
                            {
                                txtcurrentART.Value = theDS1.Tables[3].Rows[0]["CurrentART"].ToString();
                                //string[] theStrRegimen = txtcurrentART.Value.Split(new Char[] { '/' }, 5);
                                string[] theStrRegimen = txtcurrentART.Value.Split(new Char[] { '/' });
                                DataView theARVDV = new DataView(theDS.Tables[16]);
                                if (txtcurrentART.Value != "")
                                {
                                    //ViewState["SelectedData"] = OldRegimenList(theStrRegimen, theDV);
                                    ViewState["SelectedData"] = OldRegimenList(theStrRegimen, theARVDV);
                                }
                            }
                            if (theDS1.Tables[3].Rows[0]["CurrentARTStartDate"] != System.DBNull.Value)
                            {
                                this.txtcurrentARTDate.Value = String.Format("{0:MMM-yyyy}", theDS1.Tables[3].Rows[0]["CurrentARTStartDate"]);
                            }
                            script = "";
                            script = "<script language = 'javascript' defer ='defer' id = 'rdopreviousARV'>\n";
                            script += "document.getElementById('" + rdopreviousARV.ClientID + "').click();\n";
                            script += "</script>\n";
                            ClientScript.RegisterStartupScript(this.GetType(), "rdopreviousARV", script);

                            //ARV and Prev ARV Exposure Part
                            if (theDS1.Tables[3].Rows[0]["PrevSingleDoseNVP"] != System.DBNull.Value)
                            {
                                int SingleDoseNVP = Convert.ToInt32(theDS1.Tables[3].Rows[0]["PrevSingleDoseNVP"]);
                                if (SingleDoseNVP == 1)
                                {
                                    if (theDS1.Tables[3].Rows[0]["PrevSingleDoseNVPDate1"] != System.DBNull.Value)
                                    {
                                        this.txtprevSingleDoseNVPDate1.Value = String.Format("{0:MMM-yyyy}", theDS1.Tables[3].Rows[0]["PrevSingleDoseNVPDate1"]);
                                    }
                                    if (theDS1.Tables[3].Rows[0]["PrevSingleDoseNVPDate2"] != System.DBNull.Value)
                                    {
                                        this.txtprevSingleDoseNVPDate2.Value = String.Format("{0:MMM-yyyy}", theDS1.Tables[3].Rows[0]["PrevSingleDoseNVPDate2"]);
                                    }
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'prevSingleDose'>\n";
                                    script += "document.getElementById('" + chkprevSDNVPNVP.ClientID + "').click();\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "prevSingleDose", script);
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'rdopreviousARV'>\n";
                                    script += "document.getElementById('" + rdopreviousARV.ClientID + "').click();\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "rdopreviousARV", script);
                                }
                            }

                            if (theDS1.Tables[3].Rows[0]["PrevARVRegimen"] != System.DBNull.Value)
                            {
                                int PrevARVRegimen = Convert.ToInt32(theDS1.Tables[3].Rows[0]["PrevARVRegimen"]);
                                if (PrevARVRegimen == 1)
                                {
                                    if (theDS1.Tables[3].Rows[0]["PrevARVRegimen1Name"] != System.DBNull.Value)
                                    {
                                        this.txtprevARVRegimen1Name.Value = theDS1.Tables[3].Rows[0]["PrevARVRegimen1Name"].ToString();
                                        string[] theStrRegimen = txtprevARVRegimen1Name.Value.Split(new Char[] { '/' });
                                        // DataView theDV = new DataView(theDS.Tables[9]);
                                        if (txtprevARVRegimen1Name.Value != "")
                                        {
                                            ViewState["SelectedData1"] = OldRegimenList(theStrRegimen, theDV);
                                        }
                                    }
                                    if (theDS1.Tables[3].Rows[0]["PrevARVRegimen1Months"] != System.DBNull.Value)
                                    {
                                        this.txtprevARVRegimen1Months.Value = theDS1.Tables[3].Rows[0]["PrevARVRegimen1Months"].ToString();
                                    }
                                    if (theDS1.Tables[3].Rows[0]["PrevARVRegimen2Name"] != System.DBNull.Value)
                                    {
                                        this.txtprevARVRegimen2Name.Value = theDS1.Tables[3].Rows[0]["PrevARVRegimen2Name"].ToString();
                                        string[] theStrRegimen = txtprevARVRegimen2Name.Value.Split(new Char[] { '/' });
                                        // DataView theDV = new DataView(theDS.Tables[9]);
                                        if (txtprevARVRegimen2Name.Value != "")
                                        {
                                            ViewState["SelectedData2"] = OldRegimenList(theStrRegimen, theDV);
                                        }
                                    }
                                    if (theDS1.Tables[3].Rows[0]["PrevARVRegimen2Months"] != System.DBNull.Value)
                                    {
                                        this.txtprevARVRegimen2Months.Value = theDS1.Tables[3].Rows[0]["PrevARVRegimen2Months"].ToString();
                                    }
                                    if (theDS1.Tables[3].Rows[0]["PrevARVRegimen3Name"] != System.DBNull.Value)
                                    {
                                        this.txtprevARVRegimen3Name.Value = theDS1.Tables[3].Rows[0]["PrevARVRegimen3Name"].ToString();
                                        string[] theStrRegimen = txtprevARVRegimen3Name.Value.Split(new Char[] { '/' });
                                        //DataView theDV = new DataView(theDS.Tables[9]);
                                        if (txtprevARVRegimen3Name.Value != "")
                                        {
                                            ViewState["SelectedData3"] = OldRegimenList(theStrRegimen, theDV);
                                        }
                                    }
                                    if (theDS1.Tables[3].Rows[0]["PrevARVRegimen3Months"] != System.DBNull.Value)
                                    {
                                        this.txtprevARVRegimen3Months.Value = theDS1.Tables[3].Rows[0]["PrevARVRegimen3Months"].ToString();
                                    }

                                    if (theDS1.Tables[3].Rows[0]["PrevARVRegimen4Name"] != System.DBNull.Value)
                                    {
                                        this.txtprevARVRegimen4Name.Value = theDS1.Tables[3].Rows[0]["PrevARVRegimen4Name"].ToString();
                                        string[] theStrRegimen = txtprevARVRegimen4Name.Value.Split(new Char[] { '/' });
                                        //DataView theDV = new DataView(theDS.Tables[9]);
                                        if (txtprevARVRegimen4Name.Value != "")
                                        {
                                            ViewState["SelectedData4"] = OldRegimenList(theStrRegimen, theDV);
                                        }
                                    }
                                    if (theDS1.Tables[3].Rows[0]["PrevARVRegimen4Months"] != System.DBNull.Value)
                                    {
                                        this.txtprevARVRegimen4Months.Value = theDS1.Tables[3].Rows[0]["PrevARVRegimen4Months"].ToString();
                                        //this.chkprevARVRegimen.Checked = true;
                                    }
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'prevARVRegimen'>\n";
                                    script += "document.getElementById('" + chkprevARVRegimen.ClientID + "').click();\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "prevARVRegimen", script);
                                    //Populating Previous Exposure
                                    script = "";
                                    script = "<script language = 'javascript' defer ='defer' id = 'rdopreviousARV'>\n";
                                    script += "document.getElementById('" + rdopreviousARV.ClientID + "').click();\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "rdopreviousARV", script);
                                }
                            }
                        }
                    }

                    //Physical Exam and Detail Patient Vitals Part[Table: dtl_PatientVitals]
                    if (theDS1.Tables[4].Rows.Count > 0)
                    {
                        if (theDS1.Tables[4].Rows[0]["Temp"] != System.DBNull.Value)
                        {
                            this.txtphysTemp.Value = theDS1.Tables[4].Rows[0]["Temp"].ToString();
                        }
                        if (theDS1.Tables[4].Rows[0]["RR"] != System.DBNull.Value)
                        {
                            this.txtphysRR.Value = theDS1.Tables[4].Rows[0]["RR"].ToString();
                        }
                        if (theDS1.Tables[4].Rows[0]["HR"] != System.DBNull.Value)
                        {
                            this.txtphysHR.Value = theDS1.Tables[4].Rows[0]["HR"].ToString();
                        }
                        if (theDS1.Tables[4].Rows[0]["BPDiastolic"] != System.DBNull.Value)
                        {
                            this.txtphysBPDiastolic.Value = theDS1.Tables[4].Rows[0]["BPDiastolic"].ToString();
                        }
                        if (theDS1.Tables[4].Rows[0]["BPSystolic"] != System.DBNull.Value)
                        {
                            this.txtphysBPSystolic.Value = theDS1.Tables[4].Rows[0]["BPSystolic"].ToString();
                        }
                        if (theDS1.Tables[4].Rows[0]["Height"] != System.DBNull.Value)
                        {
                            this.txtphysHeight.Value = theDS1.Tables[4].Rows[0]["Height"].ToString();
                        }
                        if (theDS1.Tables[4].Rows[0]["Weight"] != System.DBNull.Value)
                        {
                            this.txtphysWeight.Value = theDS1.Tables[4].Rows[0]["Weight"].ToString();
                        }
                        if (((theDS1.Tables[4].Rows[0]["Weight"].ToString() != "") && (theDS1.Tables[4].Rows[0]["Weight"] != System.DBNull.Value)) || ((theDS1.Tables[4].Rows[0]["Height"].ToString() != "") && (theDS1.Tables[4].Rows[0]["Height"] != System.DBNull.Value)))
                        {
                            decimal anotherWeight = Convert.ToDecimal(theDS1.Tables[4].Rows[0]["Weight"].ToString());
                            decimal anotherHeight = Convert.ToDecimal(theDS1.Tables[4].Rows[0]["Height"].ToString());
                            decimal anotherBMI = anotherWeight / ((anotherHeight / 100) * (anotherHeight / 100));
                            anotherBMI = Math.Round(anotherBMI, 2);
                            txtanotherbmi.Value = Convert.ToString(anotherBMI);
                        }

                        if (theDS1.Tables[4].Rows[0]["Pain"] != System.DBNull.Value)
                        {
                            this.ddlPain.Value = theDS1.Tables[4].Rows[0]["Pain"].ToString();
                        }
                    }
                    //Table: dtl_PatientStage
                    if (theDS1.Tables[5].Rows.Count > 0)
                    {
                        if (theDS1.Tables[5].Rows[0]["WABStage"] != System.DBNull.Value)
                        {
                            this.ddlphysWABStage.SelectedValue = theDS1.Tables[5].Rows[0]["WABStage"].ToString();
                        }
                        if (theDS1.Tables[5].Rows[0]["WHOStage"] != System.DBNull.Value)
                        {
                            this.ddlWHOStage.SelectedValue = theDS1.Tables[5].Rows[0]["WHOStage"].ToString();
                        }
                    }
                    //ARV Therapy - Table:dtl_PatientARVTherapy
                    if (theDS1.Tables[6].Rows.Count > 0)
                    {
                        if (theDS1.Tables[6].Rows[0]["THerapyPlan"] != System.DBNull.Value)
                        {
                            this.lstclinPlanIE.Value = theDS1.Tables[6].Rows[0]["THerapyPlan"].ToString();
                        }
                        if (theDS1.Tables[6].Rows[0]["THerapyReasonCOde"] != System.DBNull.Value)
                        {
                            if (this.lstclinPlanIE.Value == "98")
                            {
                                this.ddlTherapyChange.SelectedValue = theDS1.Tables[6].Rows[0]["THerapyReasonCOde"].ToString();
                                this.txtarvTherapyChangeCodeOtherName.Value = theDS1.Tables[6].Rows[0]["TherapyOther"].ToString();

                                if (this.ddlTherapyChange.SelectedValue == "24")
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
                            if (this.lstclinPlanIE.Value == "99")
                            {
                                this.ddlTherapyStop.SelectedValue = theDS1.Tables[6].Rows[0]["THerapyReasonCOde"].ToString();
                                this.txtarvTherapyStopCodeOtherName.Value = theDS1.Tables[6].Rows[0]["TherapyOther"].ToString();

                                if (this.ddlTherapyStop.SelectedValue == "24")
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
                    if (theDS1.Tables[7].Rows.Count > 0)
                    {
                        if (theDS1.Tables[7].Rows[0]["Appdate"] != System.DBNull.Value)
                        {
                            DateTime theTmpDt8 = Convert.ToDateTime(theDS1.Tables[7].Rows[0]["Appdate"]);
                            this.txtappDate.Value = theTmpDt8.ToString(Session["AppDateFormat"].ToString());
                            lstappPeriod.Value = theDS1.Tables[13].Rows[0]["No_of_Days"].ToString();

                            if (this.txtappDate.Value == "01-Jan-1900")
                            {
                                this.txtappDate.Value = "";
                            }
                        }
                        if (theDS1.Tables[7].Rows[0]["AppReason"] != System.DBNull.Value)
                        {
                            this.ddappReason.SelectedValue = theDS1.Tables[7].Rows[0]["AppReason"].ToString();
                        }
                        if (theDS1.Tables[7].Rows[0]["EmployeeID"] != System.DBNull.Value)
                        {
                            BindDropdown(theDS1.Tables[7].Rows[0]["EmployeeID"].ToString());
                            this.ddinterviewer.SelectedValue = theDS1.Tables[7].Rows[0]["EmployeeID"].ToString();
                        }
                        if (theDS1.Tables[7].Rows[0]["CreateDate"] != System.DBNull.Value)
                        {
                            ViewState["createdate"] = Convert.ToDateTime(theDS1.Tables[7].Rows[0]["CreateDate"].ToString());
                        }
                    }
                    else
                    {
                        BindDropdown("");
                    }

                    //Begin
                    string DisclosureOther = "";
                    string DisclosureOtherDesc = "";
                    if (theDS1.Tables[8].Rows.Count >= 1)
                    {
                        for (int i = 0; i < theDS1.Tables[8].Rows.Count; i++)
                        {
                            foreach (HtmlTableRow tr in tblHIVdisclosure.Rows)
                            {
                                foreach (HtmlTableCell tc in tr.Cells)
                                {
                                    foreach (Control ct in tc.Controls)
                                    {
                                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                        {
                                            foreach (DataRow dr in DT_disclosure.Rows)
                                            {
                                                if (((HtmlInputCheckBox)ct).Value == dr[1].ToString())
                                                {
                                                    if (dr[0].ToString() == theDS1.Tables[8].Rows[i]["DisclosureID"].ToString())
                                                    {
                                                        if (dr[1].ToString() == "Other")
                                                        {
                                                            DisclosureOther = dr[1].ToString();
                                                            DisclosureOtherDesc = theDS1.Tables[8].Rows[i]["DisclosureOther"].ToString();
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
                                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                                        {
                                            if (DisclosureOther == "Other")
                                            {
                                                ((HtmlInputText)ct).Value = DisclosureOtherDesc;
                                                string script = "";
                                                script = "<script language = 'javascript' defer ='defer' id = 'OtherDisclosure_0'>\n";
                                                script += "show('otherDisclosure');\n";
                                                script += "</script>\n";
                                                ClientScript.RegisterStartupScript(this.GetType(), "OtherDisclosure_0", script);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        rdoDisclosureYes.Checked = true;
                        string HIVDis = "";
                        HIVDis = "<script language = 'javascript' defer ='defer' id = 'HIVDisclosure'>\n";
                        HIVDis += "show('showdisclosureName');\n";
                        HIVDis += "</script>\n";
                        ClientScript.RegisterStartupScript(this.GetType(), "HIVDisclosure", HIVDis);
                    }

                    if (theDS1.Tables[10].Rows.Count > 0)
                    {
                        for (int i = 0; i < theDS1.Tables[10].Rows.Count; i++)
                        {
                            int Disease_Pk_MedHis = Convert.ToInt32(theDS1.Tables[10].Rows[0]["Disease_Pk"].ToString());
                            if (Disease_Pk_MedHis == 95)
                            {
                                rdoMedHistNone.Checked = true;
                            }
                            else if (Disease_Pk_MedHis == 94)
                            {
                                rdoMedHistnotdocumented.Checked = true;
                            }
                            for (int j = 0; j < GrdMedHist.Rows.Count; j++)
                            {
                                if (GrdMedHist.Rows[j].Cells[0].Text == theDS1.Tables[10].Rows[i]["Disease_Pk"].ToString())
                                {
                                    GridViewRow row = GrdMedHist.Rows[j];
                                    CheckBox theChk = (CheckBox)row.Cells[1].Controls[0];
                                    DropDownList theDropDown = (DropDownList)row.Cells[2].Controls[0];
                                    TextBox theTxtBox = (TextBox)row.Cells[3].Controls[0];

                                    theChk.Checked = Convert.ToBoolean(theDS1.Tables[10].Rows[i]["DiseasePresent"].ToString());
                                    if (theDS1.Tables[10].Rows[i]["DateOfDisease"] != System.DBNull.Value)
                                    {
                                        if (theDS1.Tables[10].Rows[i]["DateOfDisease"].ToString().Length > 4)
                                        {
                                            theDropDown.SelectedValue = theDS1.Tables[10].Rows[i]["DateOfDisease"].ToString().Substring(7, 4);
                                        }
                                        else
                                        {
                                            theDropDown.SelectedValue = theDS1.Tables[10].Rows[i]["DateOfDisease"].ToString();
                                        }
                                    }
                                    theTxtBox.Text = theDS1.Tables[10].Rows[i]["DiseaseDesc"].ToString();
                                }
                            }
                        }
                    }

                    if (theDS1.Tables[10].Rows.Count > 0)
                    {
                        for (int i = 0; i < theDS1.Tables[10].Rows.Count; i++)
                        {
                            int Disease_Pk_AssoCond = Convert.ToInt32(theDS1.Tables[10].Rows[0]["Disease_Pk"]);
                            if (Disease_Pk_AssoCond == 97)
                            {
                                rdoHIVassocNone.Checked = true;
                            }
                            else if (Disease_Pk_AssoCond == 96)
                            {
                                rdoPrevHIVassocNotDocumented.Checked = true;
                            }
                            else
                            {
                                string txtBoxId = "";
                                string AssocondleftDate = "";
                                foreach (HtmlTableRow tbr in tblHIVAIDSleft.Rows)
                                {
                                    foreach (HtmlTableCell tbc in tbr.Cells)
                                    {
                                        foreach (Control ct in tbc.Controls)
                                        {
                                            if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                            {
                                                foreach (DataRow drleft in theDTHivAssoConditionLeft.Rows)
                                                {
                                                    if (((HtmlInputCheckBox)ct).Value == drleft[1].ToString())
                                                    {
                                                        if (drleft[0].ToString() == theDS1.Tables[10].Rows[i]["Disease_Pk"].ToString())
                                                        {
                                                            if (drleft[0].ToString() == "1")
                                                            {
                                                                ((HtmlInputCheckBox)ct).Checked = true;
                                                                txtBoxId = "txtACondleft" + ct.ID.ToString().Substring(11, ct.ID.Length - 11);
                                                                if (theDS1.Tables[10].Rows[i]["DateOfDisease"] != System.DBNull.Value)
                                                                    AssocondleftDate = theDS1.Tables[10].Rows[i]["DateOfDisease"].ToString().Substring(3, 8);
                                                                rdoHIVassociate.Checked = true;
                                                                script = "";
                                                                script = "<script language = 'javascript' defer ='defer' id = 'Assoc_Cond_left'>\n";
                                                                script += "show('assocSelected');\n";
                                                                script += "</script>\n";
                                                                ClientScript.RegisterStartupScript(this.GetType(), "Assoc_Cond_left", script);
                                                                //pulmonary TB+ and pulmonary TB-
                                                                script = "";
                                                                script = "<script language = 'javascript' defer ='defer' id = 'Assoc_Cond_left_rdo1'>\n";
                                                                script += "show('pultb1');\n";
                                                                script += "show('pultb2');\n";
                                                                script += "</script>\n";
                                                                ClientScript.RegisterStartupScript(this.GetType(), "Assoc_Cond_left_rdo1", script);
                                                            }
                                                            else
                                                            {
                                                                ((HtmlInputCheckBox)ct).Checked = true;
                                                                txtBoxId = "txtACondleft" + ct.ID.ToString().Substring(11, ct.ID.Length - 11);
                                                                if (theDS1.Tables[10].Rows[i]["DateOfDisease"] != System.DBNull.Value)
                                                                    AssocondleftDate = theDS1.Tables[10].Rows[i]["DateOfDisease"].ToString().Substring(3, 8);
                                                                rdoHIVassociate.Checked = true;
                                                                script = "";
                                                                script = "<script language = 'javascript' defer ='defer' id = 'Assoc_Cond_left'>\n";
                                                                script += "show('assocSelected');\n";
                                                                script += "</script>\n";
                                                                ClientScript.RegisterStartupScript(this.GetType(), "Assoc_Cond_left", script);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
                                            {
                                                foreach (DataRow drrdo in theDS1.Tables[10].Rows)
                                                {
                                                    if ((drrdo["Disease_Pk"].ToString()) == "2" && (ct.ID == "txtACondleftrdo11"))
                                                    {
                                                        ((HtmlInputRadioButton)ct).Checked = true;
                                                        script = "";
                                                        script = "<script language = 'javascript' defer ='defer' id = 'Assoc_Cond_left_rdo2'>\n";
                                                        script += "show('pultb1');\n";
                                                        script += "show('pultb2');\n";
                                                        script += "</script>\n";
                                                        ClientScript.RegisterStartupScript(this.GetType(), "Assoc_Cond_left_rdo2", script);
                                                    }
                                                    else if ((drrdo["Disease_Pk"].ToString()) == "93" && (ct.ID == "txtACondleftrdo12"))
                                                    {
                                                        ((HtmlInputRadioButton)ct).Checked = true;
                                                        script = "";
                                                        script = "<script language = 'javascript' defer ='defer' id = 'Assoc_Cond_left_rdo3'>\n";
                                                        script += "show('pultb1');\n";
                                                        script += "show('pultb2');\n";
                                                        script += "</script>\n";
                                                        ClientScript.RegisterStartupScript(this.GetType(), "Assoc_Cond_left_rdo3", script);
                                                    }
                                                }
                                            }

                                            if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                                            {
                                                if (ct.ID.ToString() == txtBoxId.ToString())
                                                {
                                                    ((HtmlInputText)ct).Value = AssocondleftDate;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (theDS1.Tables[10].Rows.Count == 0)
                    {
                        rdoHIVassocNone.Checked = true;
                    }
                    #region "20-06-2007 1 jayant"
                    if (theDS1.Tables[10].Rows.Count > 0)
                    {
                        for (int i = 0; i < theDS1.Tables[10].Rows.Count; i++)
                        {
                            int Disease_Pk_AssoCond = Convert.ToInt32(theDS1.Tables[10].Rows[i]["Disease_Pk"]);
                            if (Disease_Pk_AssoCond == 97)
                            {
                                rdoHIVassocNone.Checked = true;
                            }
                            else if (Disease_Pk_AssoCond == 96)
                            {
                                rdoPrevHIVassocNotDocumented.Checked = true;
                            }
                            else if (Disease_Pk_AssoCond == 100)
                            {
                                rdoHIVassociate.Checked = true;
                                script = "";
                                script = "<script language = 'javascript' defer ='defer' id = 'Assoc_Cond_ID'>\n";
                                script += "show('assocSelected');\n";
                                script += "</script>\n";
                                ClientScript.RegisterStartupScript(this.GetType(), "Assoc_Cond_ID", script);
                            }
                            //Jayant 01-01-08
                            string txtBoxId = "";
                            string AssocCondDesc = "";
                            string AssocCond_value_Date1 = "";
                            Boolean AssocCondOther = false;
                            foreach (HtmlTableRow tbr in tblHIVAIDSright.Rows)
                            {
                                foreach (HtmlTableCell tbc in tbr.Cells)
                                {
                                    foreach (Control ct in tbc.Controls)
                                    {
                                        if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputCheckBox))
                                        {
                                            foreach (DataRow drright in theDTHivAssoConditionRight.Rows)
                                            {
                                                if (((HtmlInputCheckBox)ct).Value == drright[1].ToString())
                                                {
                                                    if (drright[0].ToString() == theDS1.Tables[10].Rows[i]["Disease_Pk"].ToString())
                                                    {
                                                        if (drright[1].ToString() == "Other")
                                                        {
                                                            ((HtmlInputCheckBox)ct).Checked = true;
                                                            if (theDS1.Tables[10].Rows[i]["DateOfDisease"] != System.DBNull.Value)
                                                            {
                                                                AssocCond_value_Date1 = theDS1.Tables[10].Rows[i]["DateOfDisease"].ToString().Substring(3, 8);
                                                            }
                                                            AssocCondDesc = theDS1.Tables[10].Rows[i]["DiseaseDesc"].ToString();
                                                            txtBoxId = "txtACondright" + ct.ID.ToString().Substring(12, ct.ID.Length - 12);
                                                            AssocCondOther = true;
                                                            rdoHIVassociate.Checked = true;
                                                            script = "";
                                                            script = "<script language = 'javascript' defer ='defer' id = 'Assoc_Cond_right'>\n";
                                                            script += "show('assocSelected');\n";
                                                            script += "</script>\n";
                                                            ClientScript.RegisterStartupScript(this.GetType(), "Assoc_Cond_right", script);
                                                        }
                                                        else
                                                        {
                                                            ((HtmlInputCheckBox)ct).Checked = true;
                                                            if (theDS1.Tables[10].Rows[i]["DateOfDisease"] != System.DBNull.Value)
                                                            {
                                                                AssocCond_value_Date1 = theDS1.Tables[10].Rows[i]["DateOfDisease"].ToString().Substring(3, 8);
                                                            }
                                                            txtBoxId = "txtACondright" + ct.ID.ToString().Substring(12, ct.ID.Length - 12);
                                                            rdoHIVassociate.Checked = true;
                                                            script = "";
                                                            script = "<script language = 'javascript' defer ='defer' id = 'Assoc_Cond_right'>\n";
                                                            script += "show('assocSelected');\n";
                                                            script += "</script>\n";
                                                            ClientScript.RegisterStartupScript(this.GetType(), "Assoc_Cond_right", script);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (ct.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                                        {
                                            if ((ct.ID.ToString() == "txtACondOther") && (AssocCondOther == true))
                                            {
                                                ((HtmlInputText)ct).Value = AssocCondDesc;
                                                script = "";
                                                script = "<script language = 'javascript' defer ='defer' id = 'Assoc_Cond_Other'>\n";
                                                script += "show('otherHIVCondition_1');\n";
                                                script += "show('otherHIVCondition_2');\n";
                                                script += "show('otherHIVCondition_3');\n";
                                                script += "</script>\n";
                                                ClientScript.RegisterStartupScript(this.GetType(), "Assoc_Cond_Other", script);
                                                AssocCondOther = false;
                                            }

                                            if (ct.ID.ToString() == txtBoxId.ToString())
                                            {
                                                ((HtmlInputText)ct).Value = AssocCond_value_Date1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion "20-06-2007 1 jayant"

                    if (theDS1.Tables[11].Rows.Count > 0)
                    {
                        for (int i = 0; i < theDS1.Tables[11].Rows.Count; i++)
                        {
                            if (Convert.ToInt32(theDS1.Tables[11].Rows[i]["AssessmentID"].ToString()) == AssessmentPlanID8)
                            {
                                this.rdoclinAssessment_Plan_RegimenNone.Checked = true;
                            }

                            if (Convert.ToInt32(theDS1.Tables[11].Rows[i]["AssessmentID"].ToString()) == AssessmentPlanID8)
                            {
                                this.rdoclinAssessment_Plan_RegimenNone.Checked = true;
                            }

                            if (Convert.ToInt32(theDS1.Tables[11].Rows[i]["AssessmentID"].ToString()) == AssessmentPlanID1)
                            {
                                this.chkclinAssessmentInitial1.Checked = true;
                                this.MulttxtclinAssessmentNotes.Value = theDS1.Tables[11].Rows[i]["Description1"].ToString();
                            }
                            if (Convert.ToInt32(theDS1.Tables[11].Rows[i]["AssessmentID"].ToString()) == AssessmentPlanID2)
                            {
                                this.chkclinAssessmentInitial2.Checked = true;
                                this.MulttxtclinAssessmentNotes.Value = theDS1.Tables[11].Rows[i]["Description1"].ToString();
                            }
                            if (Convert.ToInt32(theDS1.Tables[11].Rows[i]["AssessmentID"].ToString()) == AssessmentPlanID3)
                            {
                                this.chkclinPlanInitial.Checked = true;
                                this.MulttxtclinPlanNotes.Value = theDS1.Tables[11].Rows[i]["Description2"].ToString();
                            }
                            if (Convert.ToInt32(theDS1.Tables[11].Rows[i]["AssessmentID"].ToString()) == AssessmentPlanID4)
                            {
                                this.chkclinPlanInitial2.Checked = true;
                                this.MulttxtclinPlanNotes.Value = theDS1.Tables[11].Rows[i]["Description2"].ToString();
                            }
                            if (Convert.ToInt32(theDS1.Tables[11].Rows[i]["AssessmentID"].ToString()) == AssessmentPlanID5)
                            {
                                this.chkclinPlanInitial3.Checked = true;
                                this.MulttxtclinPlanNotes.Value = theDS1.Tables[11].Rows[i]["Description2"].ToString();
                            }
                            if (Convert.ToInt32(theDS1.Tables[11].Rows[i]["AssessmentID"].ToString()) == AssessmentPlanID6)
                            {
                                this.chkclinPlanInitial4.Checked = true;
                                this.MulttxtclinPlanNotes.Value = theDS1.Tables[11].Rows[i]["Description2"].ToString();
                            }
                        }
                    }
                    if (theDS1.Tables[14].Rows.Count > 0)
                    {
                        txtclinicalNotes.Text = theDS1.Tables[14].Rows[0][0].ToString();
                    }
                }
            }
            // }
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

        protected void Page_Init(object sender, EventArgs e)
        {
            //RTyagi.
            if (Convert.ToInt32(Session["TechnicalAreaId"]) != 2)
            {
                btnsave.Enabled = false;
                btncomplete.Enabled = false;
            }
            Authenticate();
            FillDropDowns();
        }

        protected void Page_load(object sender, EventArgs e)
        {
            //Authenticate();
            //FillDropDowns();
            if (Request.QueryString["name"] == "Delete")
            {
                btnsave.Text = "Delete";
            }

            Maintain_ViewState(rdoDisclosureYes.Checked, chkpresentingComplaintsNone.Checked, chkotherAllergy.Checked, chklongTermMedsSulfa.Checked, chkLongTermTBMed.Checked, chklongTermMedsOther1.Checked, chklongTermMedsOther2.Checked, rdopreviousARV.Checked, chkprevSDNVPNVP.Checked, chkprevARVRegimen.Checked, rdoHIVassociate.Checked, Convert.ToInt32(lstclinPlanIE.Value), Convert.ToString(ddlTherapyChange.SelectedItem), Convert.ToString(ddlTherapyStop.SelectedItem));
            //(Master.FindControl("lblRoot") as Label).Text = "Clinical Forms >>";
            //(Master.FindControl("lblMark") as Label).Visible = false;
            //(Master.FindControl("lblheader") as Label).Text = "Initial Evaluation";
            //(Master.FindControl("lblformname") as Label).Text = "Initial Evaluation Form";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Clinical Forms >> ";
            (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Initial Evaluation";
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Initial Evaluation Form";

            //Modified by Jayanta Kr. Das
            PutCustomControl();
            Show_Hide();
            Pregnant_LMPCallBack();
            ShowHideHIVDisclosure();
            HIVAIDS_Assocondition();
            Session["PtnRegCTC"] = "";
            Session["CustomfrmDrug"] = "";
            Session["CustomfrmLab"] = "";
            if (!IsPostBack)
            {
                ViewState["BtnCompClicked"] = "0";
                ViewState["Pregstatus"] = "0";
                Init_Add_UpdateInitial_Evaluation();
                Add_attributes();
                BMIAttributes();
                if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
                {
                    ViewState["VisitID_add"] = null;
                    GrdMedHist.Attributes.Add("OnClick", "disableRadioNone('" + rdoMedHistNone.ClientID + "'); disableRadioNotDocumented('" + rdoMedHistnotdocumented.ClientID + "')");
                }
                else
                {
                    GrdMedHist.Attributes.Add("OnClick", "disableRadioNone('" + rdoMedHistNone.ClientID + "'); disableRadioNotDocumented('" + rdoMedHistnotdocumented.ClientID + "')");
                    //Amitava Sinha
                    //Modified by Jayanta Kr. Das 07-07-2009
                    FillOldData(PId);
                }
            }
            else
            {
                ViewState["MasterData"] = ((DataSet)ViewState["OldData"]).Tables[0];
                #region "Other Drugs"
                if ((DataTable)Application["AddRegimen"] != null)
                {
                    //ViewState["MasterData"] = (DataTable)Application["MasterData"];
                    ViewState["ARVMasterData"] = (DataTable)Application["MasterData"];
                    Application.Remove("MasterData");

                    DataTable theDT = (DataTable)Application["AddRegimen"];
                    ViewState["SelectedData"] = theDT;

                    string theStr = FillRegimen(theDT);
                    txtcurrentART.Value = theStr;
                    Application.Remove("AddRegimen");
                }
                else if ((DataTable)Application["AddRegimen1"] != null)
                {
                    ViewState["MasterData"] = (DataTable)Application["MasterData"];
                    Application.Remove("MasterData");

                    DataTable theDT = (DataTable)Application["AddRegimen1"];
                    ViewState["SelectedData1"] = theDT;

                    string theStr = FillRegimen(theDT);
                    txtprevARVRegimen1Name.Value = theStr;
                    Application.Remove("AddRegimen1");
                }
                else if ((DataTable)Application["AddRegimen2"] != null)
                {
                    ViewState["MasterData"] = (DataTable)Application["MasterData"];
                    Application.Remove("MasterData");

                    DataTable theDT = (DataTable)Application["AddRegimen2"];
                    ViewState["SelectedData2"] = theDT;

                    string theStr = FillRegimen(theDT);
                    txtprevARVRegimen2Name.Value = theStr;
                    Application.Remove("AddRegimen2");
                }
                else if ((DataTable)Application["AddRegimen3"] != null)
                {
                    ViewState["MasterData"] = (DataTable)Application["MasterData"];
                    Application.Remove("MasterData");

                    DataTable theDT = (DataTable)Application["AddRegimen3"];
                    ViewState["SelectedData3"] = theDT;

                    string theStr = FillRegimen(theDT);
                    txtprevARVRegimen3Name.Value = theStr;
                    Application.Remove("AddRegimen3");
                }
                else if ((DataTable)Application["AddRegimen4"] != null)
                {
                    ViewState["MasterData"] = (DataTable)Application["MasterData"];
                    Application.Remove("MasterData");

                    DataTable theDT = (DataTable)Application["AddRegimen4"];
                    ViewState["SelectedData4"] = theDT;

                    string theStr = FillRegimen(theDT);
                    txtprevARVRegimen4Name.Value = theStr;
                    Application.Remove("AddRegimen4");
                }
                #endregion

                if (ViewState["BtnCompClicked"].ToString() == "1")
                    DataQuality_Msg();
            }
            Form.EnableViewState = true;
        }

        protected DataSet PrevARVCD4(int patientID, DateTime VisitDate)
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

        private void Authenticate()
        {
            /***************** Check For User Rights ****************/
            AuthenticationManager Authentiaction = new AuthenticationManager();
            if (Authentiaction.HasFunctionRight(ApplicationAccess.InitialEvaluation, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
            {
                btnPrint.Enabled = false;
            }

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0)
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.InitialEvaluation, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                {
                    btnsave.Enabled = false;
                    btncomplete.Enabled = false;
                }
            }
            else if (Request.QueryString["name"] == "Delete")
            {
                btnsave.Text = "Delete";
                btncomplete.Visible = false;

                if (Authentiaction.HasFunctionRight(ApplicationAccess.InitialEvaluation, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    int PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                    string theUrl = "";
                    theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmClinical_DeleteForm.aspx", PatientID, Session["HIVPatientStatus"].ToString());
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.InitialEvaluation, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                {
                    //btnsave.Text = "Delete";
                    btnsave.Enabled = false;
                    //btncomplete.Visible = false;
                }
            }
            else
            {
                if (Authentiaction.HasFunctionRight(ApplicationAccess.InitialEvaluation, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                {
                    string theUrl = "";
                    //theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString(), Session["HIVPatientStatus"].ToString());
                    theUrl = string.Format("{0}?sts={1}", "frmPatient_History.aspx", Session["HIVPatientStatus"].ToString());
                    Response.Redirect(theUrl);
                }
                else if (Authentiaction.HasFunctionRight(ApplicationAccess.InitialEvaluation, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
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
        }

        private void Bind_GridMedHistory(DataTable Year)
        {
            BoundField theCol0 = new BoundField();
            theCol0.DataField = "ID";
            theCol0.HeaderText = "ID";

            DataTable tmpDT = new DataTable();
            TemplateField theCol1 = new TemplateField();
            DataTable theAttributes = new DataTable();
            theCol1.ItemTemplate = new CreateItemTemplate(CreateItemTemplate.ConType.Checkbox, "Name", tmpDT, theAttributes, 0);

            TemplateField theCol2 = new TemplateField();
            theCol2.ItemTemplate = new CreateItemTemplate(CreateItemTemplate.ConType.Dropdown, "", Year, theAttributes, 0);

            TemplateField theCol3 = new TemplateField();
            theCol3.ItemTemplate = new CreateItemTemplate(CreateItemTemplate.ConType.Textbox, "", tmpDT, theAttributes, 0);

            GrdMedHist.Columns.Add(theCol0);
            GrdMedHist.Columns.Add(theCol1);
            GrdMedHist.Columns.Add(theCol2);
            GrdMedHist.Columns.Add(theCol3);
            GrdMedHist.DataBind();
            theCol0.Visible = false;
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
                theDV.RowFilter = "DeleteFlag = 0";
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
                    BindManager.BindCombo(ddinterviewer, theDT, "EmployeeName", "EmployeeId");
                }
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

        private string DataQuality_Msg()
        {
            string strmsg = "Following values are required to complete the data quality check:\\n\\n";
            //Visit Date
            if (txtvisitDate.Text.Trim() == "")
            {
                string scriptVisitDate = "<script language = 'javascript' defer ='defer' id = 'ColorVisitDate'>\n";
                scriptVisitDate += "To_Change_Color('Vdate');\n";
                scriptVisitDate += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ColorVisitDate", scriptVisitDate);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Visit Date";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg = strmsg + "\\n";
            }
            //Pregnant Part
            if (rdopregnantYes.Checked == false && rdopregnantNo.Checked == false && Convert.ToInt32(ViewState["Pregstatus"]) == 1)
            {
                string scriptpregnant = "<script language = 'javascript' defer ='defer' id = 'pregnant_ID'>\n";
                scriptpregnant += "To_Change_Color('lblpregnanttmp');\n";
                scriptpregnant += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "pregnant_ID", scriptpregnant);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Pregnant";
                strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                strmsg = strmsg + "\\n";
            }

            //Previous ARV Exposure
            if (rdopreviousARV.Checked == true)
            {
                if ((txtcurrentART.Value.Trim() == "") && (chkprevSDNVPNVP.Checked == false) && (chkprevARVRegimen.Checked == false))
                {
                    string scriptCurrentART = "<script language = 'javascript' defer ='defer' id = 'CurrentART_ID'>\n";
                    scriptCurrentART += "To_Change_Color('Cuart');\n";
                    scriptCurrentART += "To_Change_Color('lblNVP');\n";
                    scriptCurrentART += "To_Change_Color('lblprevARVRegimen');\n";
                    scriptCurrentART += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "CurrentART_ID", scriptCurrentART);
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Select either Current ART or Single Dose NVP or Previous Regimens";
                    strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                    strmsg = strmsg + "\\n";
                }

                //Drug Allergies
                if (rdoAllergynone.Checked == false && rdoAllergynotdocumented.Checked == false && chksulfaAllergy.Checked == false && chkotherAllergy.Checked == false)
                {
                    string scriptDrugAllery = "<script language = 'javascript' defer ='defer' id = 'DrugAllery_ID'>\n";
                    scriptDrugAllery += "To_Change_Color('lblAllergyNone');\n";
                    scriptDrugAllery += "To_Change_Color('lblAllergyNotDocumented');\n";
                    scriptDrugAllery += "To_Change_Color('lblAllergySulfa');\n";
                    scriptDrugAllery += "To_Change_Color('lblAllergyOther');\n";
                    scriptDrugAllery += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "DrugAllery_ID", scriptDrugAllery);
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-Please Select either";
                    strmsg += IQCareMsgBox.GetMessage("DrugAllergy_SulfaDrug", theBuilder, this);
                    strmsg = strmsg + "\\n";
                }
                //HIV Associated Conditions
                if (rdoHIVassocNone.Checked == false && rdoPrevHIVassocNotDocumented.Checked == false && rdoHIVassociate.Checked == false)
                {
                    string scriptHIVassoc = "<script language = 'javascript' defer ='defer' id = 'HIVassoc_ID'>\n";
                    scriptHIVassoc += "To_Change_Color('lblHIVassocNone');\n";
                    scriptHIVassoc += "To_Change_Color('lblHIVassocNotdocumented');\n";
                    scriptHIVassoc += "To_Change_Color('lblHIVassociate');\n";
                    scriptHIVassoc += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "HIVassoc_ID", scriptHIVassoc);
                    MsgBuilder theBuilder = new MsgBuilder();
                    theBuilder.DataElements["Control"] = "-HIV Associated Conditions radio buttons ";
                    strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                    strmsg = strmsg + "\\n";
                }
            }
            //ARV Exposure
            if (rdoprevARVExposureNone.Checked == false && rdoprevARVExpnotdocumented.Checked == false && rdopreviousARV.Checked == false)
            {
                string scriptARVExposure = "<script language = 'javascript' defer ='defer' id = 'ARVExposure_ID'>\n";
                scriptARVExposure += "To_Change_Color('lblprevARVExposureNone');\n";
                scriptARVExposure += "To_Change_Color('lblprevARVExposurenotdocumented');\n";
                scriptARVExposure += "To_Change_Color('lblpreviousARV');\n";
                scriptARVExposure += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ARVExposure_ID", scriptARVExposure);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-ARV Exposure";
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

            //Height
            if (txtphysHeight.Value.Trim() == "")
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

            if (txtphysWeight.Value.Trim() == "")
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

            //Assessment
            if (rdoclinAssessment_Plan_RegimenNone.Checked == false && chkclinAssessmentInitial1.Checked == false && chkclinAssessmentInitial2.Checked == false)
            {
                string scriptAsessment = "<script language = 'javascript' defer ='defer' id = 'Asessment_ID'>\n";
                scriptAsessment += "To_Change_Color('lblAssessNone');\n";
                scriptAsessment += "To_Change_Color('lblHIVrelated');\n";
                scriptAsessment += "To_Change_Color('lblHIVrelatedNon');\n";
                scriptAsessment += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "Asessment_ID", scriptAsessment);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "-Assessment";
                strmsg += IQCareMsgBox.GetMessage("BlankDropDown", theBuilder, this);
                // strmsg = strmsg + "\\n";
            }

            return strmsg;
        }

        private void DQCancel()
        {
            ViewState["btcolor"] = '1';
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('DQ Checked complete.\\nForm Marked as DQ Checked.\\n Do you want to close?');\n";
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
            script += "window.location.href='frmClinical_InitialEvaluation.aspx';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        private Boolean FieldValidation()
        {
            if (TransferValidation(PId) == false)
            {
                return false;
            }
            IInitialEval IEManager;
            IEManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");

            //Visit Date Validations
            if (txtvisitDate.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Visit Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtvisitDate.Focus();
                return false;
            }

            IQCareUtils theUtil = new IQCareUtils();
            if (Convert.ToDateTime(Application["AppCurrentDate"]) < Convert.ToDateTime(theUtil.MakeDate(txtvisitDate.Text)))
            {
                IQCareMsgBox.Show("CompareDate5", this);
                txtvisitDate.Focus();
                return false;
            }

            //Previous ARV exposure validations
            if (rdopreviousARV.Checked == true)
            {
                if ((txtcurrentART.Value.Trim() == "") && (chkprevSDNVPNVP.Checked == false) && (chkprevARVRegimen.Checked == false))
                {
                    MsgBuilder theBuilder = new MsgBuilder();
                    //theBuilder.DataElements["Control"] = "Current ART";
                    theBuilder.DataElements["Control"] = "Select Either Current ART or Single Dose NVP or Previous Regimens";
                    IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                    txtcurrentART.Focus();
                    return false;
                }

                if (chkprevARVRegimen.Checked == true)
                {
                    if ((txtprevARVRegimen1Name.Value == "" || txtprevARVRegimen1Months.Value == "") && (txtprevARVRegimen2Name.Value == "" || txtprevARVRegimen2Months.Value == "") && (txtprevARVRegimen3Name.Value == "" || txtprevARVRegimen3Months.Value == "") && (txtprevARVRegimen4Name.Value == "" || txtprevARVRegimen4Months.Value == ""))
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Previous Regimens";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtcurrentART.Focus();
                        return false;
                    }
                }
            }
            //Therapy Plan Validations for Non Paperless clinic
            if (lstclinPlanIE.Value.ToString() == "0" && Session["Paperless"].ToString() == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "ARV therapy";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                txtcurrentART.Focus();
                return false;
            }

            if (this.lstappPeriod.SelectedIndex != 0 || this.lstappPeriod.SelectedIndex == 0)
            {
                if (txtappDate.Value != "")
                {
                }
            }

            //Start new treatment validation
            //int patientid = Convert.ToInt32(PId);
            int patientid = Convert.ToInt32(Session["PatientId"]);
            DataSet theDS = IEManager.GetARTStatus(patientid);
            if ((theDS.Tables[0].Rows[0]["PrevARTflag"].ToString() == "1") && (lstclinPlanIE.Value == "97"))
            {
                IQCareMsgBox.Show("IEStartNewTreatment", this);
                return false;
            }

            if ((theDS.Tables[1].Rows.Count != 0) && (theDS.Tables[1].Rows[0]["EnrollmentDate"].ToString() != "0"))
            {
                if (Convert.ToDateTime(txtvisitDate.Text) < Convert.ToDateTime(theDS.Tables[1].Rows[0]["EnrollmentDate"]))
                {
                    IQCareMsgBox.Show("Enrolment_Exist", this);
                    txtvisitDate.Focus();
                    return false;
                }
            }

            return true;
        }

        // Field Level Validation-Complete
        private Boolean FieldValidation_complete()
        {
            //Visit Date Validations

            if (txtvisitDate.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Visit Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtvisitDate.Focus();
                return false;
            }

            IInitialEval IEManager;
            DataSet dr;
            IEManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
            dr = IEManager.GetCurrentDate();
            DateTime dt = Convert.ToDateTime(dr.Tables[0].Rows[0]["CurrentDay"]);
            dt = Convert.ToDateTime(dt.ToString(Session["AppDateFormat"].ToString()));

            IQCareUtils theUtil = new IQCareUtils();
            if (dt != Convert.ToDateTime(theUtil.MakeDate(txtvisitDate.Text)))
            {
                IQCareMsgBox.Show("CompareDate5", this);
                txtvisitDate.Focus();
                return false;
            }
            //Current ART Validation
            if (txtcurrentART.Value.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Current ART";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtcurrentART.Focus();
                return false;
            }

            //ARV Therapy Plan Validations
            if (lstclinPlanIE.Value.ToString() == "0" && Session["Paperless"].ToString() == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Therapy Plan";
                IQCareMsgBox.Show("BlankDropDown", theBuilder, this);
                txtcurrentART.Focus();
                return false;
            }
            return true;
        }

        //Amitava Sinha
        //Populate Old Data in the Custom Field
        //Modified Jayanta Kr. Das 07-07-09
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
                dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, visitPK, 0, 0, Convert.ToInt32(ApplicationAccess.InitialEvaluation));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "IEval");
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
                        //theDV = new DataView(((DataSet)ViewState["OldData"]).Tables[0]);
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

        private void GetICallBackFunction()
        {
            ClientScriptManager m = Page.ClientScript;
            str = m.GetCallbackEventReference(this, "args", "ReceiveServerData", "'this is context from server'");
            strCallback = "function CallServer(args){" + str + ";}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallServer", strCallback, true);
        }

        private void HIVAIDS_Assocondition()
        {
            //Left DIV Items
            foreach (HtmlTableRow r in tblHIVAIDSleft.Rows)
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
                                    script = "<script language = 'javascript' defer ='defer' id = 'HIVAIDSleft_ID'>\n";
                                    script += "show('pultb1');\n";
                                    script += "show('pultb2');\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "HIVAIDSleft_ID", script);
                                }
                            }
                        }
                    }
                }
            }

            //Right DIV Items
            foreach (HtmlTableRow r in tblHIVAIDSright.Rows)
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
                                    script = "<script language = 'javascript' defer='defer' id='HIVAIDSright_ID'>\n";
                                    script += "show('otherHIVCondition_1');\n";
                                    script += "show('otherHIVCondition_2');\n";
                                    script += "show('otherHIVCondition_3');\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "HIVAIDSright_ID", script);
                                }
                            }
                        }
                    }
                }
            }
        }

        //Passing Parameters
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
            htInitialEvalParameters.Add("visitdate", txtvisitDate.Text);
            htInitialEvalParameters.Add("HIVDiagnosisdate", txtHIVDiagnosisdate.Value);
            HIVdiagnosisverified = this.rdoHIVDiagnosisVerifiedYes.Checked == true ? 1 : (this.rdoHIVDiagnosisVerifiedNo.Checked == true ? 0 : 9);
            htInitialEvalParameters.Add("diagnosisverified", HIVdiagnosisverified);
            HIVdisclosure = this.rdoDisclosureYes.Checked == true ? 1 : (this.rdoDisclosureNo.Checked == true ? 0 : 9);
            htInitialEvalParameters.Add("disclosed", HIVdisclosure);
            htInitialEvalParameters.Add("lmp", txtLMPdate.Value);
            Pregnant = this.rdopregnantYes.Checked == true ? 1 : (this.rdopregnantNo.Checked == true ? 0 : 9);
            int Delivered = this.rdoDeliveredYes.Checked == true ? 1 : (this.rdoDeliveredNo.Checked == true ? 0 : 9);
            htInitialEvalParameters.Add("Delivered", Delivered);
            if (Delivered == 1)
            {
                htInitialEvalParameters.Add("Pregnant", 0);
                htInitialEvalParameters.Add("DelDate", txtDeliDate.Value);
                htInitialEvalParameters.Add("EDDDate", "");
            }
            else if (Delivered == 0)
            {
                htInitialEvalParameters.Add("Pregnant", 1);
                htInitialEvalParameters.Add("EDDDate", txtEDDDate.Value);
                htInitialEvalParameters.Add("DelDate", "");
            }
            else
            {
                htInitialEvalParameters.Add("Pregnant", Pregnant);
                if (Pregnant == 1)
                {
                    htInitialEvalParameters.Add("DelDate", "");
                    htInitialEvalParameters.Add("EDDDate", txtEDDDate.Value);
                }
                else
                {
                    htInitialEvalParameters.Add("DelDate", "");
                    htInitialEvalParameters.Add("EDDDate", "");
                }
            }

            //allergy
            FlagSulfa = this.chksulfaAllergy.Checked == true ? 1 : 0;
            FlagSulfa = this.chkotherAllergy.Checked == true ? 1 : 0;
            htInitialEvalParameters.Add("flagsulfa", FlagSulfa);
            allergy_Sulfa_ID = this.rdoAllergynone.Checked == true ? 233 : (this.rdoAllergynotdocumented.Checked == true ? 234 : (this.chksulfaAllergy.Checked == true ? 82 : 0));
            htInitialEvalParameters.Add("allergy_Sulfa_ID", allergy_Sulfa_ID);

            allergy_Other_ID = this.chkotherAllergy.Checked == true ? 83 : 0;
            htInitialEvalParameters.Add("allergy_Other_ID", allergy_Other_ID);
            txtotherAllergyName.Value = this.chkotherAllergy.Checked == true ? txtotherAllergyName.Value : "";
            htInitialEvalParameters.Add("allergynameother", txtotherAllergyName.Value);
            intchklongTermMedsSulfa = this.chklongTermMedsSulfa.Checked == true ? 1 : 0;
            htInitialEvalParameters.Add("longTermMedsSulfa", intchklongTermMedsSulfa);
            txtlongTermMedsSulfaDesc.Value = this.chklongTermMedsSulfa.Checked == true ? txtlongTermMedsSulfaDesc.Value : "";
            htInitialEvalParameters.Add("longTermMedsSulfaDesc", txtlongTermMedsSulfaDesc.Value);

            //longTermTB
            intlongTermTBMed = this.chkLongTermTBMed.Checked == true ? 1 : 0;
            htInitialEvalParameters.Add("longTermTBMed", intlongTermTBMed);
            txtLongTermTBMedDesc.Value = this.chkLongTermTBMed.Checked == true ? txtLongTermTBMedDesc.Value : "";
            htInitialEvalParameters.Add("longTermTBMedDesc", txtLongTermTBMedDesc.Value);
            txtLongTermTBStartDate.Value = this.chkLongTermTBMed.Checked == true ? txtLongTermTBStartDate.Value : "";
            htInitialEvalParameters.Add("longTermTBStartDate", txtLongTermTBStartDate.Value);

            //Long Term Medication - Other1
            intchklongTermMedsOther1 = this.chklongTermMedsOther1.Checked == true ? 1 : 0;
            htInitialEvalParameters.Add("longTermMedsOther1", intchklongTermMedsOther1);
            txtlongTermMedsOther1Desc.Value = this.chklongTermMedsOther1.Checked == true ? txtlongTermMedsOther1Desc.Value : "";
            htInitialEvalParameters.Add("longTermMedsOther1Desc", txtlongTermMedsOther1Desc.Value);

            //Long Term Medication - Other2
            intchklongTermMedsOther2 = this.chklongTermMedsOther2.Checked == true ? 1 : 0;
            htInitialEvalParameters.Add("longTermMedsOther2", intchklongTermMedsOther2);
            txtlongTermMedsOther2Desc.Value = this.chklongTermMedsOther2.Checked == true ? txtlongTermMedsOther2Desc.Value : "";
            htInitialEvalParameters.Add("longTermMedsOther2Desc", txtlongTermMedsOther2Desc.Value);

            //ARV EXPOSURE
            string strrdoprevARVExposureNone = "", strrdoprevARVExpnotdocumented = "", strrdopreviousARV = "";

            if (rdoprevARVExposureNone.Checked == true)
            {
                strrdoprevARVExposureNone = "1";
                txtcurrentART.Value = "";
                txtcurrentARTDate.Value = "";
                txtprevSingleDoseNVPDate1.Value = "";
                txtprevSingleDoseNVPDate2.Value = "";
                chkprevSDNVPNVP.Value = "";
                txtprevARVRegimen1Name.Value = "";
                txtprevARVRegimen1Months.Value = "";
                txtprevARVRegimen2Name.Value = "";
                txtprevARVRegimen2Months.Value = "";
                txtprevARVRegimen3Name.Value = "";
                txtprevARVRegimen3Months.Value = "";
                txtprevARVRegimen4Name.Value = "";
                txtprevARVRegimen4Months.Value = "";
            }

            if (rdoprevARVExpnotdocumented.Checked == true)
            {
                strrdoprevARVExpnotdocumented = "1";
                txtcurrentART.Value = "";
                txtcurrentARTDate.Value = "";
                txtprevSingleDoseNVPDate1.Value = "";
                txtprevSingleDoseNVPDate2.Value = "";
                chkprevSDNVPNVP.Value = "";
                txtprevARVRegimen1Name.Value = "";
                txtprevARVRegimen1Months.Value = "";
                txtprevARVRegimen2Name.Value = "";
                txtprevARVRegimen2Months.Value = "";
                txtprevARVRegimen3Name.Value = "";
                txtprevARVRegimen3Months.Value = "";
                txtprevARVRegimen4Name.Value = "";
                txtprevARVRegimen4Months.Value = "";
            }
            if (rdopreviousARV.Checked == true)
            {
                strrdopreviousARV = "1";
                intPrevSingleDoseNVP = this.chkprevSDNVPNVP.Checked == true ? 1 : 0;
                txtprevSingleDoseNVPDate1.Value = this.chkprevSDNVPNVP.Checked == true ? txtprevSingleDoseNVPDate1.Value : "";
                txtprevSingleDoseNVPDate2.Value = this.chkprevSDNVPNVP.Checked == true ? txtprevSingleDoseNVPDate2.Value : "";
                intPrevARVRegimen = this.chkprevARVRegimen.Checked == true ? 1 : 0;
                txtprevARVRegimen1Name.Value = this.chkprevARVRegimen.Checked == true ? txtprevARVRegimen1Name.Value : "";
                txtprevARVRegimen1Months.Value = this.chkprevARVRegimen.Checked == true ? txtprevARVRegimen1Months.Value : "";
                txtprevARVRegimen2Name.Value = this.chkprevARVRegimen.Checked == true ? txtprevARVRegimen2Name.Value : "";
                txtprevARVRegimen2Months.Value = this.chkprevARVRegimen.Checked == true ? txtprevARVRegimen2Months.Value : "";
                txtprevARVRegimen3Name.Value = this.chkprevARVRegimen.Checked == true ? txtprevARVRegimen3Name.Value : "";
                txtprevARVRegimen3Months.Value = this.chkprevARVRegimen.Checked == true ? txtprevARVRegimen3Months.Value : "";
                txtprevARVRegimen4Name.Value = this.chkprevARVRegimen.Checked == true ? txtprevARVRegimen4Name.Value : "";
                txtprevARVRegimen4Months.Value = this.chkprevARVRegimen.Checked == true ? txtprevARVRegimen4Months.Value : "";
            }
            if ((rdoprevARVExposureNone.Checked == false) && (rdoprevARVExpnotdocumented.Checked == false) && (rdopreviousARV.Checked == false))
            {
                txtcurrentART.Value = "";
                txtcurrentARTDate.Value = "";
                txtprevSingleDoseNVPDate1.Value = "";
                txtprevSingleDoseNVPDate2.Value = "";
                chkprevSDNVPNVP.Value = "";
                txtprevARVRegimen1Name.Value = "";
                txtprevARVRegimen1Months.Value = "";
                txtprevARVRegimen2Name.Value = "";
                txtprevARVRegimen2Months.Value = "";
                txtprevARVRegimen3Name.Value = "";
                txtprevARVRegimen3Months.Value = "";
                txtprevARVRegimen4Name.Value = "";
                txtprevARVRegimen4Months.Value = "";
            }

            htInitialEvalParameters.Add("PrevARVExposureNone", strrdoprevARVExposureNone);
            htInitialEvalParameters.Add("PrevARVExposureNotDocumented", strrdoprevARVExpnotdocumented);
            htInitialEvalParameters.Add("PrevARVExposure", strrdopreviousARV);

            //Current ART Date Format
            htInitialEvalParameters.Add("CurrentART", txtcurrentART.Value);
            htInitialEvalParameters.Add("currentARTStartDate", txtcurrentARTDate.Value);

            //Single Dose NVP
            htInitialEvalParameters.Add("PrevSingleDoseNVP", intPrevSingleDoseNVP);
            htInitialEvalParameters.Add("txtprevSingleDoseNVPDate1", txtprevSingleDoseNVPDate1.Value);
            htInitialEvalParameters.Add("txtprevSingleDoseNVPDate2", txtprevSingleDoseNVPDate2.Value);

            //Single Dose Regimen
            htInitialEvalParameters.Add("PrevARVRegimen", intPrevARVRegimen);
            htInitialEvalParameters.Add("PrevARVRegimen1Name", txtprevARVRegimen1Name.Value);
            htInitialEvalParameters.Add("PrevARVRegimen1Months", txtprevARVRegimen1Months.Value);
            htInitialEvalParameters.Add("PrevARVRegimen2Name", txtprevARVRegimen2Name.Value);
            htInitialEvalParameters.Add("PrevARVRegimen2Months", txtprevARVRegimen2Months.Value);
            htInitialEvalParameters.Add("PrevARVRegimen3Name", txtprevARVRegimen3Name.Value);
            htInitialEvalParameters.Add("PrevARVRegimen3Months", txtprevARVRegimen3Months.Value);
            htInitialEvalParameters.Add("PrevARVRegimen4Name", txtprevARVRegimen4Name.Value);
            htInitialEvalParameters.Add("PrevARVRegimen4Months", txtprevARVRegimen4Months.Value);

            //Medical History & Physical Temperature validation
            //HIV-Related History
            //None & Not documented Part
            String strrdoprevLowestCD4none = "", strrdoprevLowestCD4notdocumented = "";
            if ((this.rdoprevLowestCD4none.Checked == true) || (this.rdoprevLowestCD4notdocumented.Checked == true))
            {
                strrdoprevLowestCD4none = rdoprevLowestCD4none.Checked == true ? "1" : "";
                strrdoprevLowestCD4notdocumented = rdoprevLowestCD4notdocumented.Checked == true ? "1" : "";
                txtprevLowestCD4.Value = "";
                txtprevLowestCD4Percent.Value = "";
                txtprevLowestCD4Date.Value = "";
            }
            String strrdopriorARVsCD4none = "", strrdopriorARVsCD4notdocumented = "";
            if ((this.rdopriorARVsCD4none.Checked == true) || (this.rdopriorARVsCD4notdocumented.Checked == true))
            {
                strrdopriorARVsCD4none = rdopriorARVsCD4none.Checked == true ? "1" : "";
                strrdopriorARVsCD4notdocumented = rdopriorARVsCD4notdocumented.Checked == true ? "1" : "";
                txtpriorARVsCD4.Value = "";
                txtpriorARVsCD4Percent.Value = "";
                txtpriorARVsCD4Date.Value = "";
            }

            String strrdomostRecentCD4none = "", strrdomostRecentCD4notdocumented = "";
            if ((this.rdomostRecentCD4none.Checked == true) || (this.rdomostRecentCD4notdocumented.Checked == true))
            {
                strrdomostRecentCD4none = rdomostRecentCD4none.Checked == true ? "1" : "";
                strrdomostRecentCD4notdocumented = rdomostRecentCD4notdocumented.Checked == true ? "1" : "";
                txtmostRecentCD4.Value = "";
                txtmostRecentCD4Percent.Value = "";
                txtmostRecentCD4Date.Value = "";
            }
            String strrdomostRecentViralLoadnone = "", strrdomostRecentViralLoadnotdocumented = "";
            if ((this.rdomostRecentViralLoadnone.Checked == true) || (this.rdomostRecentViralLoadnotdocumented.Checked == true))
            {
                strrdomostRecentViralLoadnone = rdomostRecentViralLoadnone.Checked == true ? "1" : "";
                strrdomostRecentViralLoadnotdocumented = rdomostRecentViralLoadnotdocumented.Checked == true ? "1" : "";
                txtmostRecentViralLoad.Value = "";
                txtmostRecentViralLoadDate.Value = "";
            }

            htInitialEvalParameters.Add("PrevLowestCD4None", strrdoprevLowestCD4none);
            htInitialEvalParameters.Add("PrevLowestCD4NotDocumented", strrdoprevLowestCD4notdocumented);
            htInitialEvalParameters.Add("PrevLowestCD4", txtprevLowestCD4.Value);
            htInitialEvalParameters.Add("PrevLowestCD4Percent", txtprevLowestCD4Percent.Value);
            htInitialEvalParameters.Add("PrevLowestCD4Date", txtprevLowestCD4Date.Value);

            htInitialEvalParameters.Add("PrevARVsCD4None", strrdopriorARVsCD4none);
            htInitialEvalParameters.Add("PrevARVsCD4NotDocumented", strrdopriorARVsCD4notdocumented);
            htInitialEvalParameters.Add("PrevARVsCD4", txtpriorARVsCD4.Value);
            htInitialEvalParameters.Add("PrevARVsCD4Percent", txtpriorARVsCD4Percent.Value);
            htInitialEvalParameters.Add("PrevARVsCD4Date", txtpriorARVsCD4Date.Value);

            htInitialEvalParameters.Add("PrevMostRecentCD4None", strrdomostRecentCD4none);
            htInitialEvalParameters.Add("PrevMostRecentCD4NotDocumented", strrdomostRecentCD4notdocumented);
            htInitialEvalParameters.Add("PrevMostRecentCD4", txtmostRecentCD4.Value);
            htInitialEvalParameters.Add("PrevMostRecentCD4Percent", txtmostRecentCD4Percent.Value);
            htInitialEvalParameters.Add("PrevMostRecentCD4Date", txtmostRecentCD4Date.Value);

            htInitialEvalParameters.Add("PrevMostRecentViralLoadNone", strrdomostRecentViralLoadnone);
            htInitialEvalParameters.Add("PrevMostRecentViralLoadNotDocumented", strrdomostRecentViralLoadnotdocumented);
            htInitialEvalParameters.Add("PrevMostRecentViralLoad", txtmostRecentViralLoad.Value);
            htInitialEvalParameters.Add("PrevMostRecentViralLoadDate", txtmostRecentViralLoadDate.Value);

            //Physical Temperature part
            htInitialEvalParameters.Add("Temp", txtphysTemp.Value);
            htInitialEvalParameters.Add("RR", txtphysRR.Value);
            htInitialEvalParameters.Add("HR", txtphysHR.Value);
            htInitialEvalParameters.Add("BPDiastolic", txtphysBPDiastolic.Value);
            htInitialEvalParameters.Add("BPSystolic", txtphysBPSystolic.Value);
            htInitialEvalParameters.Add("Height", txtphysHeight.Value);
            htInitialEvalParameters.Add("Weight", txtphysWeight.Value);
            htInitialEvalParameters.Add("Pain", ddlPain.Value);
            htInitialEvalParameters.Add("WABStage", ddlphysWABStage.SelectedValue);
            htInitialEvalParameters.Add("WHOStage", ddlWHOStage.SelectedValue);
            //Appointment business rule
            if (txtappDate.Value == "")
            {
                if (this.lstappPeriod.Value != "0" && txtvisitDate.Text != "")
                {
                    txtappDate.Value = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(txtvisitDate.Text).AddDays(Convert.ToInt32(this.lstappPeriod.Value)));
                }
                else
                {
                    htInitialEvalParameters.Add("AppExist", 0);
                    htInitialEvalParameters.Add("VisitIDApp", 0);
                }
            }
            //Code section for Appointment Business rules
            if (txtappDate.Value != "")
            {
                IInitialEval IEAppManager;
                IEAppManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
                DataSet theDSApp = IEAppManager.GetAppointment(PatientId, locationid, Convert.ToDateTime(txtappDate.Value), Convert.ToInt32(ddappReason.SelectedValue));
                if (Convert.ToInt32(theDSApp.Tables[0].Rows[0]["ExistFlag"]) == 1)
                {
                    if (theDSApp.Tables[1].Rows.Count > 0)
                    {
                        htInitialEvalParameters.Add("AppExist", 1);
                        htInitialEvalParameters.Add("VisitIDApp", Convert.ToInt32(theDSApp.Tables[1].Rows[0]["Visit_pk"]));
                    }
                    else
                    {
                        htInitialEvalParameters.Add("AppExist", 0);
                        htInitialEvalParameters.Add("VisitIDApp", 0);
                    }
                }
                else
                {
                    htInitialEvalParameters.Add("AppExist", 0);
                    htInitialEvalParameters.Add("VisitIDApp", 0);
                }
            }
            htInitialEvalParameters.Add("appdate", txtappDate.Value);
            htInitialEvalParameters.Add("appreason", ddappReason.SelectedValue);
            htInitialEvalParameters.Add("Signatureid", ddinterviewer.SelectedValue);

            String TherapyValue = "0", OtherTherapyName = "";
            if (this.lstclinPlanIE.Value == "98")
            {
                TherapyValue = ddlTherapyChange.SelectedValue;
                OtherTherapyName = txtarvTherapyChangeCodeOtherName.Value;
            }
            else if (this.lstclinPlanIE.Value == "99")
            {
                TherapyValue = ddlTherapyStop.SelectedValue;
                OtherTherapyName = txtarvTherapyStopCodeOtherName.Value;
            }
            htInitialEvalParameters.Add("ARVtherapyPlan", lstclinPlanIE.Value);
            htInitialEvalParameters.Add("ARVTherapyReasonCode", TherapyValue);
            htInitialEvalParameters.Add("ARVTherapyReasonOther", OtherTherapyName);
            htInitialEvalParameters.Add("UserID", Convert.ToInt32(Session["AppUserId"].ToString()));
            htInitialEvalParameters.Add("CreateDate", CreateDate);
            if (Convert.ToDateTime(ViewState["VisitDate"]) == Convert.ToDateTime(txtvisitDate.Text))
            {
                htInitialEvalParameters.Add("Flag", 1);
            }
            else { htInitialEvalParameters.Add("Flag", 0); }
            return htInitialEvalParameters;
        }

        //Amitava Sinha
        //Generating full DML Statement
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
            if (txtvisitDate.Text.ToString() != "")
                visitdate = Convert.ToDateTime(txtvisitDate.Text.ToString());

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

        //Function to maintain ViewState of Controls
        private void Maintain_ViewState(Boolean DisclosureYes, Boolean presentingComplaintsNone, Boolean otherAllergy, Boolean longTermMedsSulfa, Boolean longTermTBMed, Boolean longTermMedsOther1, Boolean longTermMedsOther2, Boolean previousARV, Boolean prevSingleDoseNVP, Boolean prevARVRegimen, Boolean HIVassociate, int regimen, string changeOther, string stopOther)
        {
            if (DisclosureYes) ViewState["DisclosureYes"] = "1";

            if (!presentingComplaintsNone) ViewState["presentingComplaintsNone"] = "1";

            if (otherAllergy) ViewState["otherAllergy"] = "1";

            if (longTermMedsSulfa) ViewState["longTermMedsSulfa"] = "1";

            if (longTermTBMed) ViewState["longTermTBMed"] = "1";

            if (longTermMedsOther1) ViewState["longTermMedsOther1"] = "1";

            if (longTermMedsOther2) ViewState["longTermMedsOther2"] = "1";

            if (previousARV) ViewState["previousARV"] = "1";

            if (prevSingleDoseNVP) ViewState["prevSingleDoseNVP"] = "1";

            if (prevARVRegimen) ViewState["prevARVRegimen"] = "1";

            if (HIVassociate) ViewState["HIVassociate"] = "1";

            if (regimen == 98) ViewState["ChangeRegimen"] = "1";

            if (regimen == 99) ViewState["StopRegimen"] = "1";

            if (changeOther == "Other") ViewState["changeROther"] = "1";

            if (stopOther == "Other") ViewState["stopROther"] = "1";
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

        private void Pregnant_LMPCallBack()
        {
            ClientScriptManager LMPPreg = Page.ClientScript;
            string strLMPPreg = LMPPreg.GetCallbackEventReference(this, "args", "RecievePregnantData", "'this is context from server'");
            string strCallbackLMPPreg = "function CallPregnantLMPServer(args){" + strLMPPreg + ";}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "CallPregnantLMPServer", strCallbackLMPPreg, true);
        }

        // Create Custom Controls
        // Creation Date : 16-Jan-2007
        // Amitava Sinha
        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.InitialEvaluation));
                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "IEval");
                    //ViewState["CustomFieldsDS"] = theDS;
                    //pnlCustomList.Visible = true;
                }
                //theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "IEval");
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

        private void SaveCancel()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Initial Evaluation Form saved successfully. Do you want to close?');\n";
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
            script += "window.location.href='frmClinical_InitialEvaluation.aspx';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        private void Show_Hide()
        {
            if ((String)ViewState["DisclosureYes"] == "1")
            {
                string scriptdisclosure = "<script language = 'javascript' defer ='defer' id = 'ondisclosure'>\n";
                scriptdisclosure += "show('showdisclosureName'); \n";
                scriptdisclosure += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ondisclosure", scriptdisclosure);

                if (Convert.ToString(ViewState["DisclosureOther"]) == "1")
                {
                    string scriptdisclosureother = "<script language = 'javascript' defer ='defer' id = 'ondisclosureother'>\n";
                    scriptdisclosureother += "show('otherdisclosureName'); \n";
                    scriptdisclosureother += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "ondisclosureother", scriptdisclosureother);
                }
            }

            if (ViewState["presentingComplaintsNone"].ToString() == "1")
            {
                string scriptpresentingComplaints = "<script language = 'javascript' defer ='defer' id = 'onpresentingComplaints'>\n";
                scriptpresentingComplaints += "display_chklist('" + chkpresentingComplaintsNone.ClientID + "', '" + presentingComplaintsShow.ClientID + "');\n";
                scriptpresentingComplaints += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onpresentingComplaints", scriptpresentingComplaints);
            }
            if (ViewState["otherAllergy"].ToString() == "1")
            {
                string scriptotherAllergy = "<script language = 'javascript' defer ='defer' id = 'onotherAllergy'>\n";
                scriptotherAllergy += "show('otherAllergyName'); \n";
                scriptotherAllergy += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onotherAllergy", scriptotherAllergy);
            }

            if (ViewState["longTermMedsSulfa"].ToString() == "1")
            {
                string scriptlongTermMedsSulfa = "<script language = 'javascript' defer ='defer' id = 'onlongTermMedsSulfa'>\n";
                scriptlongTermMedsSulfa += "show('longTermMedsSulfaSelected'); \n";
                scriptlongTermMedsSulfa += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onlongTermMedsSulfa", scriptlongTermMedsSulfa);
            }

            if (ViewState["longTermTBMed"].ToString() == "1")
            {
                string scriptlongTermMedsTB = "<script language = 'javascript' defer ='defer' id = 'onlongTermMedsTB'>\n";
                scriptlongTermMedsTB += "show('longTermMedsTBSelected'); \n";
                scriptlongTermMedsTB += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onlongTermMedsTB", scriptlongTermMedsTB);
            }

            if (ViewState["longTermMedsOther1"].ToString() == "1")
            {
                string scriptlongTermMedsOther1 = "<script language = 'javascript' defer ='defer' id = 'onlongTermMedsOther1'>\n";
                scriptlongTermMedsOther1 += "show('longTermMedsOther1Selected'); \n";
                scriptlongTermMedsOther1 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onlongTermMedsOther1", scriptlongTermMedsOther1);
            }

            if (ViewState["longTermMedsOther2"].ToString() == "1")
            {
                string scriptlongTermMedsOther2 = "<script language = 'javascript' defer ='defer' id = 'onlongTermMedsOther2'>\n";
                scriptlongTermMedsOther2 += "show('longTermMedsOther2Selected'); \n";
                scriptlongTermMedsOther2 += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onlongTermMedsOther2", scriptlongTermMedsOther2);
            }
            if (ViewState["previousARV"].ToString() == "1")
            {
                string scriptpreviousARV = "<script language = 'javascript' defer ='defer' id = 'onpreviousARV'>\n";
                scriptpreviousARV += "show('prevexpdiv'); \n";
                scriptpreviousARV += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onpreviousARV", scriptpreviousARV);

                if (ViewState["prevSingleDoseNVP"].ToString() == "1")
                {
                    string scriptprevSingleDoseNVP = "<script language = 'javascript' defer ='defer' id = 'onprevSingleDoseNVP'>\n";
                    scriptprevSingleDoseNVP += "show('prevSingleDoseNVPSelected'); \n";
                    scriptprevSingleDoseNVP += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "onprevSingleDoseNVP", scriptprevSingleDoseNVP);
                }
                if (ViewState["prevARVRegimen"].ToString() == "1")
                {
                    string scriptprevARVReg = "<script language = 'javascript' defer ='defer' id = 'onprevARVReg'>\n";
                    scriptprevARVReg += "show('prevARVReg'); \n";
                    scriptprevARVReg += "</script>\n";
                    ClientScript.RegisterStartupScript(this.GetType(), "onscriptprevARVReg", scriptprevARVReg);
                }
            }

            if (ViewState["HIVassociate"].ToString() == "1")
            {
                string scriptHIVassociate = "<script language = 'javascript' defer ='defer' id = 'onHIVassociate'>\n";
                scriptHIVassociate += "show('assocSelected'); \n";
                scriptHIVassociate += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "onHIVassociate", scriptHIVassociate);
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

            if (rdoPerformed.Checked == true)
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'TBSystem'>\n";
                script += "show('divTBPerformed');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "TBSystem", script);
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

            if (Convert.ToInt32(Session["PatientVisitId"]) == 0 && txtvisitDate.Text != "")
            {
                IInitialEval CallBackmgr;
                try
                {
                    DataSet theDS = new DataSet();
                    CallBackmgr = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
                    theDS = CallBackmgr.GetPregnantStatus(Convert.ToInt32(Session["PatientId"]), txtvisitDate.Text);
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
                            txtEDDDate.Value = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["EDD"]);
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

        private void ShowHideHIVDisclosure()
        {
            foreach (HtmlTableRow r in tblHIVdisclosure.Rows)
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
                                    script = "<script language = 'javascript' defer='defer' id='HIVDisclosure'>\n";
                                    script += "show('otherDisclosure');\n";
                                    script += "</script>\n";
                                    ClientScript.RegisterStartupScript(this.GetType(), "HIVDisclosure", script);
                                }
                            }
                        }
                    }
                }
            }
        }

        // Field Level Validationis-Save
        private Boolean TransferValidation(int PId)
        {
            IPatientTransfer IPTransferMgr;
            IPTransferMgr = (IPatientTransfer)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientTransfer, BusinessProcess.Clinical");
            if (Request.QueryString["name"] == "Add")
            {
                DataSet DS = IPTransferMgr.GetLatestTransferDate(PId, 0);
                if (DS.Tables[0].Rows[0]["NotExistTransferDate"].ToString() != "0")
                {
                    if (txtvisitDate.Text.Trim() == "")
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Visit Date";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtvisitDate.Focus();
                        return false;
                    }
                    if (txtvisitDate.Text != "")
                    {
                        if (Convert.ToDateTime(txtvisitDate.Text) < Convert.ToDateTime(DS.Tables[0].Rows[0]["TransferDate"]))
                        {
                            IQCareMsgBox.Show("TransferDate_4", this);
                            txtvisitDate.Focus();
                            return false;
                        }
                    }
                }
            }
            else if (Request.QueryString["name"] == "Edit")
            {
                visitPK = Convert.ToInt32(Request.QueryString["visitid"]);
                DataSet DS = IPTransferMgr.GetLatestTransferDate(PId, visitPK);
                if (DS.Tables[0].Rows[0]["NotExistTransferDate"].ToString() != "0")
                {
                    if (DS.Tables[1].Rows[0]["PrevDate"].ToString() == "0" && DS.Tables[2].Rows[0]["LaterDate"].ToString() != "0")
                    {
                        if (Convert.ToDateTime(txtvisitDate.Text) > Convert.ToDateTime(DS.Tables[2].Rows[0]["LaterDate"]))
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
        // Field Level Validation-Data Quality
        #region "Function to meet the DataQuality Business rule"
        #endregion "Function to meet the DataQuality Business rule"

        //Amitava Sinha
        //Generating full DML Statement
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
            if (txtvisitDate.Text.ToString() != "")
                visitdate = Convert.ToDateTime(txtvisitDate.Text.ToString());
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

        private Boolean Validate_Data_Quality()
        {
            if (TransferValidation(PId) == false)
            {
                return false;
            }
            //To Check Validation for Visit Date
            IInitialEval IEManager;
            DataSet dr;
            IEManager = (IInitialEval)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BInitialEval, BusinessProcess.Clinical");
            dr = IEManager.GetCurrentDate();
            DateTime dt = Convert.ToDateTime(dr.Tables[0].Rows[0]["CurrentDay"]);
            dt = Convert.ToDateTime(dt.ToString(Session["AppDateFormat"].ToString()));

            IQCareUtils theUtil = new IQCareUtils();
            if (dt < Convert.ToDateTime(theUtil.MakeDate(txtvisitDate.Text)))
            {
                IQCareMsgBox.Show("CompareDate5", this);
                txtvisitDate.Focus();
                return false;
            }

            //Previous ARV exposure validations
            if (rdopreviousARV.Checked == true)
            {
                if (chkprevARVRegimen.Checked == true)
                {
                    if ((txtprevARVRegimen1Name.Value == "" || txtprevARVRegimen1Months.Value == "") && (txtprevARVRegimen2Name.Value == "" || txtprevARVRegimen2Months.Value == "") && (txtprevARVRegimen3Name.Value == "" || txtprevARVRegimen3Months.Value == "") && (txtprevARVRegimen4Name.Value == "" || txtprevARVRegimen4Months.Value == ""))
                    {
                        MsgBuilder theBuilder = new MsgBuilder();
                        theBuilder.DataElements["Control"] = "Previous Regimens";
                        IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                        txtcurrentART.Focus();
                        return false;
                    }
                }
            }

            if (this.lstappPeriod.SelectedIndex != 0 || this.lstappPeriod.SelectedIndex == 0)
            {
                if (txtappDate.Value != "")
                {
                    if (Convert.ToDateTime(theUtil.MakeDate(txtappDate.Value)) < Convert.ToDateTime(theUtil.MakeDate(txtvisitDate.Text)))
                    {
                        IQCareMsgBox.Show("App_Visit", this);
                        txtappDate.Focus();
                        return false;
                    }
                }
            }

            return true;
        }
    }
}