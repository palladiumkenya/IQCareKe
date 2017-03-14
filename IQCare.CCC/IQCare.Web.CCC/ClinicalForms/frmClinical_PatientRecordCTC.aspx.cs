#region "Namespace
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
using System.Web.Services;
using System.Web.Script.Services;
using System.Web;


#endregion
namespace IQCare.Web.Clinical
{
    public partial class PatientRecordCTC : BasePage
    {
        StringBuilder sbParameter = new StringBuilder();
        StringBuilder sbValues;
        string strmultiselect;
        string TableName = "";
        ArrayList arl = new ArrayList();
        int icount;
        IPatientRecord PatientManager;

        DataSet theMasterDS = new DataSet();
        DataSet theFacilityDS;

        string[] arrIllness = new string[40];
        string[,] arrReferredTo = new string[30, 2];
        string[,] arrAdhReason = new string[30, 2];
        string[,] arrARVReason4 = new string[30, 2];

        private void Show_Hide()
        {
            if (ddlARVStatus.SelectedValue == "2")
            {
                string scriptdisclosure = "<script language = 'javascript' defer ='defer' id = 'onstartARV'>\n";
                scriptdisclosure += "show('divWhyEligible'); \n";
                scriptdisclosure += "</script>\n";
                //ClientScript.RegisterStartupScript(this.GetType(),"onstartARV", scriptdisclosure);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onstartARV", scriptdisclosure);

            }
        }

        private void FillARVReason(DataTable theDT)
        {
            //---Populate ARV Reason checkbox---
            if (theDT != null)
            {
                int col = 0;
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    HtmlTableRow tr = new HtmlTableRow();
                    HtmlInputCheckBox chkbox = new HtmlInputCheckBox();
                    HtmlTableCell tc = new HtmlTableCell();
                    chkbox.ID = Convert.ToString("ARVReason" + theDT.Rows[i]["Id"].ToString());
                    chkbox.Value = theDT.Rows[i]["DisplayName"].ToString();
                    tc.Controls.Add(chkbox);
                    tc.Controls.Add(new LiteralControl(chkbox.Value));
                    tr.Cells.Add(tc);

                    if (chkbox.Value == "123 = Adverse Reaction -- Other")
                    {
                        HtmlTableCell tcOther1 = new HtmlTableCell();
                        tcOther1.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' for='otherARVReason1'>"));
                        tcOther1.Controls.Add(new LiteralControl("<DIV id='divOtherARVReason1' style='DISPLAY:none'>Specify: "));
                        HtmlInputText HTextother1 = new HtmlInputText();
                        HTextother1.ID = "txtotherARVReason1";
                        HTextother1.Size = 10;
                        tcOther1.Controls.Add(HTextother1);
                        tcOther1.Controls.Add(new LiteralControl(HTextother1.Value));
                        tcOther1.Controls.Add(new LiteralControl("</DIV>"));
                        tr.Cells.Add(tcOther1);
                        chkbox.Attributes.Add("onclick", "toggle('divOtherARVReason1'); fnClearTextbox('txtotherARVReason1')");
                    }
                    if (chkbox.Value == "149 = Other Reason -- Other")
                    {
                        HtmlTableCell tcOther2 = new HtmlTableCell();
                        tcOther2.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' for='otherARVReason2'>"));
                        tcOther2.Controls.Add(new LiteralControl("<DIV id='divOtherARVReason2' style='DISPLAY:none'>Specify: "));
                        HtmlInputText HTextother2 = new HtmlInputText();
                        HTextother2.ID = "txtotherARVReason2";
                        HTextother2.Size = 10;
                        tcOther2.Controls.Add(HTextother2);
                        tcOther2.Controls.Add(new LiteralControl(HTextother2.Value));
                        tcOther2.Controls.Add(new LiteralControl("</DIV>"));
                        tr.Cells.Add(tcOther2);
                        chkbox.Attributes.Add("onclick", "toggle('divOtherARVReason2'); fnClearTextbox('txtotherARVReason2')");
                    }
                    col++;
                    tblARVReason2.Rows.Add(tr);
                }

            }
        }

        private void FillARVAdhReason(DataTable theDT)
        {
            //--- populate ARV Adhrence Reason checkbox ---
            if (theDT != null)
            {
                int col = 0;
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    HtmlTableRow tr = new HtmlTableRow();
                    HtmlInputCheckBox chkbox = new HtmlInputCheckBox();
                    HtmlTableCell tc = new HtmlTableCell();
                    chkbox.ID = Convert.ToString("ARVAdhReason" + theDT.Rows[i]["Id"].ToString());
                    chkbox.Value = theDT.Rows[i]["DisplayName"].ToString();
                    tc.Controls.Add(chkbox);
                    tc.Controls.Add(new LiteralControl(chkbox.Value));
                    tr.Cells.Add(tc);
                    if (chkbox.Value == "13 = Other")
                    {
                        HtmlTableCell tcOther = new HtmlTableCell();
                        tcOther.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' for='otherARVAdh'>"));
                        tcOther.Controls.Add(new LiteralControl("<DIV id='divOtherARVAdh' style='DISPLAY:none'>Specify: "));
                        HtmlInputText HTextother = new HtmlInputText();
                        HTextother.ID = "txtotherARVAdh";
                        HTextother.Size = 10;
                        tcOther.Controls.Add(HTextother);
                        tcOther.Controls.Add(new LiteralControl(HTextother.Value));
                        tcOther.Controls.Add(new LiteralControl("</DIV>"));
                        tr.Cells.Add(tcOther);
                        chkbox.Attributes.Add("onclick", "toggle('divOtherARVAdh'); fnClearTextbox('txtotherARVAdh')");
                    }
                    col++;
                    tbARVAdhReason.Rows.Add(tr);
                }
            }

        }

        private void FillIllnessOld(DataTable theDT)
        {
            //---- populate Illless check boxes of existing patients ----
            if (theDT != null)
            {
                int col = 0;
                for (int i = 0; i < theDT.Rows.Count; i++)
                {
                    HtmlTableRow tr = new HtmlTableRow();
                    HtmlInputCheckBox chkbox = new HtmlInputCheckBox();
                    HtmlTableCell tc = new HtmlTableCell();
                    chkbox.ID = Convert.ToString("Illness" + col);
                    chkbox.Value = theDT.Rows[i]["Name"].ToString();
                    tc.Controls.Add(chkbox);
                    tc.Controls.Add(new LiteralControl(chkbox.Value));
                    tr.Cells.Add(tc);
                    if (chkbox.Value == "Other")
                    {
                        HtmlTableCell tcOther = new HtmlTableCell();
                        tcOther.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' for='otherARVAdh'>"));
                        tcOther.Controls.Add(new LiteralControl("<DIV id='otherARVAdh' style='DISPLAY:none'>Specify: "));
                        HtmlInputText HTextother = new HtmlInputText();
                        HTextother.ID = "txtotherARVAdh";
                        HTextother.Size = 10;
                        tcOther.Controls.Add(HTextother);
                        tcOther.Controls.Add(new LiteralControl(HTextother.Value));
                        tcOther.Controls.Add(new LiteralControl("</DIV>"));
                        tr.Cells.Add(tcOther);
                        chkbox.Attributes.Add("onclick", "toggle('otherARVAdh'); cleartxtbox('ctl00_IQCareContentPlaceHolder_12', '" + HTextother.ClientID + "')");
                    }
                    col++;
                    tbARVAdhReason.Rows.Add(tr);
                }
            }

        }

        private void Fillrefer(DataTable DT)
        {
            //--- populate refer checkbox ---
            if (DT != null)
            {
                int col = 0;
                for (int i = 0; i < DT.Rows.Count; i++)
                {
                    HtmlTableRow tr_refer = new HtmlTableRow();
                    HtmlInputCheckBox chkrefer = new HtmlInputCheckBox();
                    HtmlTableCell tc_refer = new HtmlTableCell();
                    chkrefer.ID = Convert.ToString("ReferTo" + DT.Rows[i]["Id"].ToString());
                    chkrefer.Value = DT.Rows[i]["Name"].ToString();
                    tc_refer.Controls.Add(chkrefer);
                    tc_refer.Controls.Add(new LiteralControl(chkrefer.Value));
                    tr_refer.Cells.Add(tc_refer);

                    if (chkrefer.Value == "Other (specify)")
                    {
                        HtmlTableCell tc_referOther = new HtmlTableCell();
                        tc_referOther.Controls.Add(new LiteralControl("<LABEL style='font-weight:bold' for='otherRefer'>"));
                        tc_referOther.Controls.Add(new LiteralControl("<DIV id='divOtherRefer' style='DISPLAY:none'>Specify: "));
                        HtmlInputText HTextotherrefer = new HtmlInputText();
                        HTextotherrefer.ID = "txtReferOther";
                        HTextotherrefer.Size = 10;
                        tc_referOther.Controls.Add(HTextotherrefer);
                        tc_referOther.Controls.Add(new LiteralControl(HTextotherrefer.Value));
                        tc_referOther.Controls.Add(new LiteralControl("</DIV>"));
                        tr_refer.Cells.Add(tc_referOther);
                        chkrefer.Attributes.Add("onclick", "toggle('divOtherRefer'); cleartxtbox('ctl00_IQCareContentPlaceHolder_ReferTo426', '" + HTextotherrefer.ClientID + "')");
                        chkrefer.Attributes.Add("onclick", "toggle('divOtherRefer'); fnClearTextbox('txtReferOther')");
                    }
                    col++;
                    tblRefer.Rows.Add(tr_refer);
                }

            }
        }

        protected void Page_load(object sender, EventArgs e)
        {
            try
            {

                // Ajax.Utility.RegisterTypeForAjax(typeof(frmClinical_PatientRecordCTC));
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text = Request.QueryString["sts"].ToString();
                //(Master.FindControl("lblRoot") as Label).Text = "Patient Record CTC >>";
                //(Master.FindControl("lblMark") as Label).Visible = false;
                //(Master.FindControl("lblheader") as Label).Text = "Patient Record CTC";
                //(Master.FindControl("lblformname") as Label).Text = "Patient Record"; 
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblRoot") as Label).Text = "Patient Record CTC >> ";
                (Master.FindControl("levelOneNavigationUserControl1").FindControl("lblheader") as Label).Text = "Patient Record CTC";
                (Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblformname") as Label).Text = "Patient Record";
                if ((Master.FindControl("levelTwoNavigationUserControl1").FindControl("lblpntStatus") as Label).Text == "1")
                {
                    btnSave.Enabled = false;
                    btnDQ.Enabled = false;
                }
                PutCustomControl(); //Custom Field 
                #region "Authentication"
                Session["PtnRegCTC"] = "";

                AuthenticationManager Authentiaction = new AuthenticationManager();
                if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientRecord, FunctionAccess.Print, (DataTable)Session["UserRight"]) == false)
                {
                    btnPrint.Enabled = false;

                }
                if (Request.QueryString["name"] == "Delete")
                {
                    //----delete form 26Dec08
                    MsgBuilder theBuilder1 = new MsgBuilder();
                    theBuilder1.DataElements["FormName"] = "Patient Record CTC";
                    IQCareMsgBox.ShowConfirm("DeleteForm", theBuilder1, btnSave);

                    if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientRecord, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                    {
                        int PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                        string theUrl = "";
                        theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmClinical_DeleteForm.aspx", PatientID, Request.QueryString["sts"].ToString());
                        Response.Redirect(theUrl);
                    }
                    else if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientRecord, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                    {
                        btnSave.Text = "Delete";
                        btnSave.Enabled = false;
                        btnDQ.Enabled = false;
                    }

                    btnDQ.Visible = false;
                }
                #endregion


                FillIllness(); // fill illness checkbox
                if (ViewState["theMasterDS"] != null)
                {
                    Fillrefer(((DataSet)ViewState["theMasterDS"]).Tables["ReferredTo"]);
                    FillARVAdhReason(((DataSet)ViewState["theMasterDS"]).Tables["ARVAdhReason_Cont"]);
                    FillARVReason(((DataSet)ViewState["theMasterDS"]).Tables["ARVReason_Stop"]);

                }
                if (Request.QueryString["name"] == "Edit")
                {

                    if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientRecord, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                    {
                        int PatientID = Convert.ToInt32(Request.QueryString["PatientId"].ToString());
                        string theUrl = "";
                        theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmPatient_History.aspx", PatientID, Request.QueryString["sts"].ToString());
                        Response.Redirect(theUrl);
                    }
                    else if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientRecord, FunctionAccess.Update, (DataTable)Session["UserRight"]) == false)
                    {
                        btnSave.Enabled = false;
                        btnDQ.Enabled = false;
                    }


                }
                else if (Request.QueryString["name"] == "Add")
                {
                    if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientRecord, FunctionAccess.Add, (DataTable)Session["UserRight"]) == false)
                    {
                        btnSave.Enabled = false;
                        btnDQ.Enabled = false;
                    }
                }
                else if (Request.QueryString["name"] == "Delete")
                {
                    MsgBuilder theBuilder1 = new MsgBuilder();
                    theBuilder1.DataElements["FormName"] = "PatienT Record CTC";
                    IQCareMsgBox.ShowConfirm("DeleteForm", theBuilder1, btnSave);
                    if (Authentiaction.HasFunctionRight(ApplicationAccess.PatientRecord, FunctionAccess.View, (DataTable)Session["UserRight"]) == false)
                    {
                        int PatientID = Convert.ToInt32(Request.QueryString["PatientId"]);
                        string theUrl = "";
                        theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmClinical_DeleteForm.aspx", PatientID, Request.QueryString["sts"].ToString());
                        Response.Redirect(theUrl);
                    }
                    else if (Authentiaction.HasFunctionRight(ApplicationAccess.Pharmacy, FunctionAccess.Delete, (DataTable)Session["UserRight"]) == false)
                    {
                        btnSave.Text = "Delete";
                        btnSave.Enabled = false;
                        btnDQ.Enabled = false;
                    }
                    btnDQ.Enabled = false;
                }
                if (!IsPostBack)
                {
                    IFacilitySetup FacilityMaster = (IFacilitySetup)ObjectFactory.CreateInstance("BusinessProcess.Administration.BFacility, BusinessProcess.Administration");
                    theFacilityDS = FacilityMaster.GetSystemBasedLabels(Convert.ToInt32(Session["SystemId"]), 73, 0);
                    ViewState["FacilityDS"] = theFacilityDS;
                    SetPageLabels();

                    ViewState["OriginalDate"] = "";
                    ViewState["InitialVisitDate"] = "";
                    Session["htCD4TLC"] = "";
                    if (Request.QueryString["name"] == "Delete")
                    {
                        btnSave.Text = "Delete";
                    }

                    fillDropDown();//populate all dropdowns
                    ShowData(); // show data of existing patients
                    Show_Hide();
                    Add_Attributes();

                    Session["showDivCD4"] = "NO";
                    ViewState["AdhStaus"] = "NotChecked";

                    if (ViewState["InitialVisitDate"].ToString() == ViewState["OriginalDate"].ToString())
                        ViewState["IsInitialVisitForm"] = "TRUE";
                    else
                        ViewState["IsInitialVisitForm"] = "FALSE";

                    ViewState["DQ"] = 0;

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Page_load", "<script language='javascript'>fnGetHeightCD4Regimen();</script>");
                }

                if (Session["showDivCD4"].ToString() == "YES")
                {

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CD4TLC1", "document.getElementById('divARVAdhStatusANDAdhReason').style.display='none';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CD4TLC2", "document.getElementById('divWhyEligible').style.display='inline';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CD4TLC3", "document.getElementById('divARVReason').style.display='none';", true);

                    if (rdoIllness.Checked == true)
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CD4TLC3_Illness1", "document.getElementById('probSymptSideEffect').style.display='inline';", true);
                    if (rdoPregYes.Checked == true)
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CD4TLC3_Pregnant1", "document.getElementById('divPregnant').style.display='inline';", true);
                }
                ClientScripts();

                //--- for Height---
                Session["Ptn_Pk"] = Request.QueryString["PatientId"].ToString();
                if (Request.QueryString["VisitId"] == null)
                    Session["VisitId"] = "0";
                else
                    Session["VisitId"] = Request.QueryString["VisitId"].ToString();
                int patientId = Convert.ToInt32(Request.QueryString["PatientId"]);
                //if (ViewState["ControlCreated"] != null)
                //{
                // FillOldData(pnlCustomList, patientId);
                //}
                if ((Request.QueryString["name"] == "Edit") || (Request.QueryString["name"] == "Delete"))
                {
                    FillOldData(patientId);

                }
                if (Request.QueryString["sts"].ToString() == "1")
                {
                    btnDQ.Enabled = false;
                    btnSave.Enabled = false;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private void ClientScripts()
        {
            //--- show / hide controls on the basis of selections ---
            if (ddlARVStatus.SelectedValue.ToString() == "1")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Cl1_1", "document.getElementById('divARVReason').style.display='inline';", true);
            }
            else if (ddlARVStatus.SelectedValue.ToString() == "2")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CL2_1", "document.getElementById('divWhyEligible').style.display='inline';", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CL2_2", "document.getElementById('divARVReason').style.display='none';", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CL2_3", "document.getElementById('divARVAdhStatusANDAdhReason').style.display='none';", true);
            }
            else if (ddlARVStatus.SelectedValue.ToString() == "3")
            {
                if (rdoAdhStatusGood.Checked == false && rdoAdhStatusNotDoc.Checked == false && rdoAdhStatusPoor.Checked == false)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_1_1", "document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_1_2", "document.getElementById('divARVAdhStatus').style.display = 'inline';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_1_3", "document.getElementById('divLblARVReason2').style.display = 'none';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_1_4", "document.getElementById('divARVReason2').style.display = 'none';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_1_5", "document.getElementById('divARVAdhReason').style.display = 'none';", true);
                }
                else if (rdoAdhStatusGood.Checked == true || rdoAdhStatusNotDoc.Checked == true)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_2_1", "document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_2_2", "document.getElementById('divARVAdhStatus').style.display = 'inline';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_2_3", "document.getElementById('divLblARVReason2').style.display = 'none';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_2_4", "document.getElementById('divARVReason2').style.display = 'none';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_2_5", "document.getElementById('divARVAdhReason').style.display = 'none';", true);
                }
                else if (rdoAdhStatusPoor.Checked == true)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_3_1", "document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_3_2", "document.getElementById('divARVAdhStatus').style.display = 'inline';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_3_3", "document.getElementById('divLblARVReason2').style.display = 'none';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_3_4", "document.getElementById('divARVReason2').style.display = 'none';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_3_5", "document.getElementById('divARVAdhReason').style.display = 'inline';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL3_3_6", "document.getElementById('divChkARVAdhReason').style.display = 'inline';", true);
                }
            }
            else if (ddlARVStatus.SelectedValue.ToString() == "4" || ddlARVStatus.SelectedValue.ToString() == "5")
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CL4/5_1", "document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CL4/5_2", "document.getElementById('divARVAdhStatus').style.display = 'inline';", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CL4/5_3", "document.getElementById('divLblARVReason2').style.display = 'inline';", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CL4/5_4", "document.getElementById('divARVReason2').style.display = 'inline';", true);
                if (rdoAdhStatusPoor.Checked == true)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL4/5_6", "document.getElementById('divARVAdhReason').style.display = 'inline';", true);
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL4/5_5", "document.getElementById('divARVAdhReason').style.display = 'none';", true);
            }

            //---Referred to - Other
            GetChkdReferredTo(); // Get the values of selected - "ReferredTo" checkbox 

            for (int i = 0; i < (arrReferredTo.Length / 2); i++)
            {
                if (arrReferredTo.GetValue(i, 1) != null)
                {
                    if (arrReferredTo.GetValue(i, 1).ToString() != "")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CSReferTo1", "document.getElementById('divOtherRefer').style.display='inline';", true);
                        break;
                    }
                }
            }
            //--- Adh Reason - Other
            GetChkdAdhReason(); // Get the values of selected - "Adhrence Reason" checkbox

            for (int i = 0; i < (arrAdhReason.Length / 2); i++)
            {
                if (arrAdhReason.GetValue(i, 1) != null)
                {
                    if (arrAdhReason.GetValue(i, 1).ToString() != "")
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "CSOtherAdh1", "document.getElementById('divOtherARVAdh').style.display='inline';", true);
                        break;
                    }
                }
            }

            //---ARV Reason - Other
            GetChkdARVReason4(); // get the value of selected - "ARV Reason" checkbox

            for (int i = 0; i < (arrARVReason4.Length / 2); i++)
            {
                if (arrARVReason4.GetValue(i, 1) != null)
                {
                    if (arrARVReason4.GetValue(i, 1).ToString() != "")
                    {
                        if (arrARVReason4.GetValue(i, 0).ToString() == "62")
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CSAdh1", "document.getElementById('divOtherARVReason1').style.display='inline';", true);
                        if (arrARVReason4.GetValue(i, 0).ToString() == "69")
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CSAdh2", "document.getElementById('divOtherARVReason2').style.display='inline';", true);
                    }
                }
            }
            if (rdoPregYes.Checked == true)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "show_Pragant_CS1", "document.getElementById('divPregnantShow').style.display = 'inline';", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "show_Pragant_CS2", "document.getElementById('divPregnant').style.display = 'inline';", true);

                if (chkDelivered.Checked == true)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "show_Deliver_CS1", "document.getElementById('divChkDelivered').style.display = 'inline';", true);
            }
        }

        protected void RefreshCD4TLC(Hashtable theHT)
        {
            //--- initialse hash table---
            theHT.Add("PrevLABID", "");
            theHT.Add("CD4", "");
            theHT.Add("CD4Percent", "");
            theHT.Add("TLC", "");
            theHT.Add("TLCPercent", "");
            theHT.Add("OrderedBy", "");
            theHT.Add("OrderedDate", "");
            theHT.Add("ReportedBy", "");
            theHT.Add("ReportedDate", "");

            Session["htCD4TLC"] = theHT;

        }
        protected void ActionScriptManager_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            string message = e.Exception.Message;
            Master.PageScriptManager.AsyncPostBackErrorMessage = message;
        }
        //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        [WebMethod(EnableSession = true), ScriptMethod]
        public static string GetHeightCD4Regimen(string strVisitDate)
        {
            //--- get height, CD4 and Regimen of the patient, from the database

            string Result;
            string Height;
            string PrevMostRecentCD4;
            string PrevMostRecentCD4Date;
            string RegimenName;
            string RegimenDate;

            string Ptn_Pk = HttpContext.Current.Session["Ptn_Pk"].ToString();
            string VisitId = HttpContext.Current.Session["VisitId"].ToString();
            string LocationID = HttpContext.Current.Session["AppLocationID"].ToString();

            IQCareUtils theUtil = new IQCareUtils();
            IPatientRecord _PatientManager = (IPatientRecord)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRecord, BusinessProcess.Clinical");
            DataSet objDs = new DataSet();
            objDs = _PatientManager.GetHeightCD4Regimen(Ptn_Pk, VisitId, strVisitDate, LocationID);//pr_Clinical_GetHeightCD4RegimenCTC_Constella

            if (objDs.Tables[0].Rows.Count > 0)
                Height = objDs.Tables[0].Rows[0]["Height"].ToString();
            else
                Height = "";

            if (objDs.Tables[1].Rows.Count > 0)
            {
                PrevMostRecentCD4 = objDs.Tables[1].Rows[0]["PrevMostRecentCD4"].ToString();
                PrevMostRecentCD4Date = Convert.ToDateTime(objDs.Tables[1].Rows[0]["PrevMostRecentCD4Date"]).ToString(HttpContext.Current.Session["AppDateFormat"].ToString()).ToString();
            }
            else
            {
                PrevMostRecentCD4 = "";
                PrevMostRecentCD4Date = "";
            }

            if (objDs.Tables[2].Rows.Count > 0)
            {
                RegimenName = objDs.Tables[2].Rows[0]["RegimenName"].ToString();
                RegimenDate = Convert.ToDateTime(objDs.Tables[2].Rows[0]["RegimenDate"]).ToString(HttpContext.Current.Session["AppDateFormat"].ToString()).ToString();
            }
            else
            {
                RegimenName = "";
                RegimenDate = "";
            }
            Result = Height + "*" + PrevMostRecentCD4 + "*" + PrevMostRecentCD4Date + "*" + RegimenName + "*" + RegimenDate;
            return Result;
        }

        protected void FillIllness()
        {
            //--- populate Illness Checkboxes -------
            string s1;

            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));

            //--- Illness Checkbox list-1 : Table no-5 ----
            DataView theDV = new DataView(theDSXML.Tables["Mst_Symptom"]);
            theDV.RowFilter = "DeleteFlag=0 AND SystemId=2 AND Id<=50";
            theDV.Sort = "Name";
            DataTable theDT = theDV.ToTable();
            theDT.TableName = "Symptom1";
            theMasterDS.Tables.Add(theDT.Copy());

            for (int i = 0; i < theMasterDS.Tables["Symptom1"].Rows.Count; i++)
            {
                HtmlTableRow tr = new HtmlTableRow();
                HtmlInputCheckBox chkbox = new HtmlInputCheckBox();
                HtmlTableCell tc = new HtmlTableCell();
                chkbox.ID = Convert.ToString("Illness1" + theMasterDS.Tables["Symptom1"].Rows[i]["Id"].ToString());
                tc.Controls.Add(chkbox);
                s1 = theMasterDS.Tables["Symptom1"].Rows[i]["Name"].ToString();

                tc.Controls.Add(new LiteralControl("<span style='font-weight: bold'>"));

                int j = 0;
                while ((s1.Substring(j, 1) == s1.Substring(j, 1).ToUpper()) && (j < s1.Length - 1))
                {
                    tc.Controls.Add(new LiteralControl(s1.Substring(j, 1)));
                    j++;
                }
                tc.Controls.Add(new LiteralControl("</span>"));

                for (int j2 = j; j2 < s1.Length; j2++)
                    tc.Controls.Add(new LiteralControl(s1.Substring(j2, 1)));


                tr.Cells.Add(tc);
                tblIllness1.Controls.Add(tr);
            }

            //--- Illness Checkbox list-2 : Table no-6 ----
            theDV = new DataView(theDSXML.Tables["Mst_Symptom"]);
            theDV.RowFilter = "DeleteFlag=0 AND SystemId=2 AND Id>50";
            theDV.Sort = "Name";
            theDT = theDV.ToTable();
            theDT.TableName = "Symptom2";
            theMasterDS.Tables.Add(theDT.Copy());

            for (int i = 0; i < theMasterDS.Tables["Symptom2"].Rows.Count; i++)
            {
                HtmlTableRow tr = new HtmlTableRow();
                HtmlInputCheckBox chkbox = new HtmlInputCheckBox();
                HtmlTableCell tc = new HtmlTableCell();
                chkbox.ID = Convert.ToString("Illness2" + theMasterDS.Tables["Symptom2"].Rows[i]["Id"].ToString());

                tc.Controls.Add(chkbox);
                s1 = theMasterDS.Tables["Symptom2"].Rows[i]["Name"].ToString();

                tc.Controls.Add(new LiteralControl("<span style='font-weight: bold'>"));

                int j = 0;
                while ((s1.Substring(j, 1) == s1.Substring(j, 1).ToUpper()) && (j < s1.Length - 1))
                {
                    tc.Controls.Add(new LiteralControl(s1.Substring(j, 1)));
                    j++;
                }
                tc.Controls.Add(new LiteralControl("</span>"));

                for (int j2 = j; j2 < s1.Length; j2++)
                    tc.Controls.Add(new LiteralControl(s1.Substring(j2, 1)));

                tr.Cells.Add(tc);
                tblIllness2.Controls.Add(tr);
            }
        }

        private void PutCustomControl()
        {
            ICustomFields CustomFields;
            CustomFieldClinical theCustomField = new CustomFieldClinical();
            try
            {

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
                DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.PatientRecord));
                if (theDS.Tables[0].Rows.Count != 0)
                {
                    theCustomField.CreateCustomControlsForms(pnlCustomList, theDS, "PRecord");
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
            //ICustomFields CustomFields;
            //string pnlName = "PnlCustomList";
            //CustomFieldClinical theCustomField = new CustomFieldClinical();
            //BindFunctions theBindMgr = new BindFunctions();
            //TableName = string.Empty;
            //Int32 ii = 0;
            //try
            //{

            //    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields,BusinessProcess.Administration");
            //    DataSet theDS = CustomFields.GetCustomFieldListforAForm(Convert.ToInt32(ApplicationAccess.PatientRecord));
            //    if (theDS != null && theDS.Tables[0].Rows.Count > 0)
            //    {
            //        sbParameter = new StringBuilder();
            //        TableName = theDS.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");

            //        pnlCustomList.Visible = true;
            //        pnlCustomList.Controls.Clear();
            //        arl = new ArrayList();
            //        pnlCustomList.Controls.Add(new LiteralControl("<TABLE border='1' cellpadding=6 cellspacing=0 width=100%>"));

            //        foreach (DataRow dr in theDS.Tables[0].Rows)
            //        {
            //            if (ii % 2 == 0)
            //                pnlCustomList.Controls.Add(new LiteralControl("<TR >"));
            //            if (dr[1].ToString() == "1")
            //                pnlCustomList.Controls.Add(new LiteralControl("<TD >"));
            //            else if (dr[1].ToString() == "6")
            //                pnlCustomList.Controls.Add(new LiteralControl("<TD align='left' nowrap='noWrap' >"));
            //            else if ((dr[1].ToString() == "3") || (dr[1].ToString() == "4") || (dr[1].ToString() == "5") || (dr[1].ToString() == "7") || (dr[1].ToString() == "8") || (dr[1].ToString() == "9"))
            //                pnlCustomList.Controls.Add(new LiteralControl("<TD align='left'>"));

            //            //Select List
            //            if (dr[1].ToString() == "4")
            //            {
            //                Label customLabel = new Label();
            //                customLabel.ID = pnlName + "lbl" + ii.ToString();
            //                customLabel.Text = dr[0].ToString().Replace("_", " ");
            //                customLabel.Text = customLabel.Text.Replace("PRecord", "");
            //                sbParameter.Append(",[" + dr[0].ToString() + "]");
            //                customLabel.Width = 200;
            //                customLabel.CssClass = "labelright";
            //                customLabel.Font.Bold = true;

            //                pnlCustomList.Controls.Add(customLabel);

            //                pnlCustomList.Controls.Add(new LiteralControl("&nbsp;&nbsp;"));

            //                DropDownList ddlSelectList = new DropDownList();
            //                ddlSelectList.ID = pnlName + "Selectlist" + dr[0].ToString();
            //                ddlSelectList.Width = 180;

            //                DataSet dsSelectList = CustomFields.GetCustomList(Convert.ToInt32(dr[2].ToString()));
            //                if (dsSelectList != null && dsSelectList.Tables[0].Rows.Count > 0)
            //                {
            //                    if (Request.QueryString["name"] == "Add")
            //                    {
            //                        IQCareUtils theUtilsCF = new IQCareUtils();
            //                        DataView theDVCF = new DataView(dsSelectList.Tables[0]);
            //                        theDVCF.RowFilter = "DeleteFlag=0";
            //                        DataTable theDTCF = (DataTable)theUtilsCF.CreateTableFromDataView(theDVCF);

            //                        theBindMgr.BindCombo(ddlSelectList, theDTCF, "Name", "ID");
            //                        theDVCF.Dispose();
            //                        theDTCF.Clear();
            //                    }
            //                    else
            //                    {
            //                        theBindMgr.BindCombo(ddlSelectList, dsSelectList.Tables[0], "Name", "Id");
            //                    }

            //                }
            //                pnlCustomList.Controls.Add(ddlSelectList);

            //            }
            //            //Multi Select List
            //            else if (dr[1].ToString() == "9")
            //            {
            //                if (arl.Count == 0)
            //                {
            //                    arl.Add("dtl_CustomField_" + TableName.Replace("-", "_").ToString() + "_" + dr[0].ToString());
            //                }
            //                foreach (object obj in arl)
            //                {
            //                    if (obj.ToString() != "dtl_CustomField_" + TableName.Replace("-", "_").ToString() + "_" + dr[0].ToString())
            //                    {
            //                        arl.Add("dtl_CustomField_" + TableName.Replace("-", "_").ToString() + "_" + dr[0].ToString());
            //                        break;
            //                    }
            //                }
            //                Label theMultiSelectlbl = new Label();
            //                theMultiSelectlbl.ID = pnlName + "lbl" + ii.ToString();
            //                theMultiSelectlbl.Text = dr[0].ToString().Replace("_", " ");
            //                theMultiSelectlbl.Text = theMultiSelectlbl.Text.Replace("PRecord", "");
            //                theMultiSelectlbl.Width = 200;
            //                theMultiSelectlbl.CssClass = "labelright";
            //                theMultiSelectlbl.Font.Bold = true;
            //                pnlCustomList.Controls.Add(theMultiSelectlbl);

            //                pnlCustomList.Controls.Add(new LiteralControl("<div class = 'Customdivborder' nowrap='nowrap'>"));

            //                CheckBoxList chkMultiList = new CheckBoxList();
            //                chkMultiList.ID = pnlName + "Multiselectlist" + dr[0].ToString();
            //                chkMultiList.RepeatLayout = RepeatLayout.Flow;
            //                chkMultiList.CssClass = "check";
            //                chkMultiList.Width = 300;

            //                DataSet dsMultiSelectList = CustomFields.GetCustomList(Convert.ToInt32(dr[2].ToString()));
            //                if (dsMultiSelectList != null && dsMultiSelectList.Tables[0].Rows.Count > 0)
            //                {
            //                    if (Request.QueryString["name"] == "Add")
            //                    {
            //                        IQCareUtils theUtilsCF = new IQCareUtils();
            //                        DataView theDVCF = new DataView(dsMultiSelectList.Tables[0]);
            //                        theDVCF.RowFilter = "DeleteFlag=0";
            //                        DataTable theDTCF = (DataTable)theUtilsCF.CreateTableFromDataView(theDVCF);

            //                        theBindMgr.BindCheckedList(chkMultiList, theDTCF, "Name", "Id");

            //                        theDVCF.Dispose();
            //                        theDTCF.Clear();
            //                    }
            //                    else
            //                    {
            //                        theBindMgr.BindCheckedList(chkMultiList, dsMultiSelectList.Tables[0], "Name", "Id");
            //                    }
            //                }
            //                pnlCustomList.Controls.Add(chkMultiList);

            //                pnlCustomList.Controls.Add(new LiteralControl("</div>"));

            //            }

            //            theCustomField.CreateCustomControls(pnlCustomList, pnlName, ref sbParameter, dr, ref TableName, "PRecord", ii);

            //            ii++;
            //        }
            //    }
            //    ViewState["ControlCreated"] = "CC";
            //    pnlCustomList.Controls.Add(new LiteralControl("</TABLE>"));

            //}
            //catch
            //{

            //}
            //finally
            //{
            //    CustomFields = null;
            //}

        }

        // private void FillOldData(Control Cntrl, Int32 PatID)

        private void FillOldData(Int32 PatID)
        {
            DataSet dsvalues = null;
            ICustomFields CustomFields;

            try
            {
                DataSet theCustomFields = (DataSet)ViewState["CustomFieldsDS"];
                string theTblName = "", theColName = "";
                if (theCustomFields.Tables[0].Rows.Count > 0)
                {
                    theTblName = theCustomFields.Tables[0].Rows[0]["FeatureName"].ToString().Replace(" ", "_");
                    theColName = "";
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
                }

                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");

                dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + theTblName.ToString().Replace("-", "_"), theColName, Convert.ToInt32(PatID.ToString()), 0, Convert.ToInt32(Request.QueryString["visitid"]), 0, 0, Convert.ToInt32(ApplicationAccess.PatientRecord));
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomManager.FillCustomFieldData(theCustomFields, dsvalues, pnlCustomList, "PRecord");
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

            //string pnlName = Cntrl.ID;

            //DataSet dsvalues = null;
            //ICustomFields CustomFields;

            //try
            //{
            //    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
            //    dsvalues = CustomFields.GetCustomFieldValues("dtl_CustomField_" + TableName.ToString().Replace("-", "_"), sbParameter.ToString(), PatID, 0, Convert.ToInt32(Request.QueryString["visitid"]), 0, 0, Convert.ToInt32(ApplicationAccess.PatientRecord));
            //}
            //catch
            //{
            //}
            //finally
            //{
            //    CustomFields = null;
            //}
            //try
            //{
            //    Boolean blnflag = false;
            //    foreach (DataTable dt in dsvalues.Tables)
            //    {
            //        blnflag = true;
            //    }

            //    if (dsvalues != null && blnflag && dsvalues.Tables[0].Rows.Count > 0)
            //    {
            //        //if any data exist then set the View State
            //        ViewState["CustomFieldsData"] = 1;
            //        foreach (Control x in Cntrl.Controls)
            //        {

            //            if (x.GetType() == typeof(System.Web.UI.WebControls.DropDownList))
            //            {
            //                foreach (DataColumn dc in dsvalues.Tables[0].Columns)
            //                {
            //                    if (pnlName.ToUpper() + "SELECTLIST" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            //                    {
            //                        if (((DropDownList)x).SelectedValue == "0")
            //                        {
            //                            ((DropDownList)x).SelectedValue = dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString();
            //                        }
            //                    }

            //                }
            //            }
            //            if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputRadioButton))
            //            {
            //                foreach (DataColumn dc in dsvalues.Tables[0].Columns)
            //                {
            //                    if (pnlName.ToUpper() + "RADIO1" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            //                    {
            //                        if (dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString() == "True")
            //                        {
            //                            ((HtmlInputRadioButton)x).Checked = true;
            //                        }
            //                    }
            //                    if (pnlName.ToUpper() + "RADIO2" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            //                    {
            //                        if (dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString() == "False")
            //                        {
            //                            ((HtmlInputRadioButton)x).Checked = true;
            //                        }
            //                    }
            //                }
            //            }
            //            if (x.GetType() == typeof(System.Web.UI.WebControls.TextBox))
            //            {
            //                foreach (DataColumn dc in dsvalues.Tables[0].Columns)
            //                {
            //                    if (pnlName.ToUpper() + "TXT" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            //                    {
            //                        if (((TextBox)x).Text == "")
            //                        {
            //                            ((TextBox)x).Text = dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString();
            //                            break;
            //                        }
            //                    }
            //                    if (pnlName.ToUpper() + "NUM" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            //                    {
            //                        if (((TextBox)x).Text == "")
            //                        {
            //                            ((TextBox)x).Text = dsvalues.Tables[0].Rows[0][dc.ColumnName].ToString();
            //                            break;
            //                        }
            //                    }
            //                    if (pnlName.ToUpper() + "DT" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            //                    {
            //                        if (((TextBox)x).Text == "")
            //                        {
            //                            if (dsvalues.Tables[0].Rows[0][dc.ColumnName] != System.DBNull.Value)
            //                            {
            //                                ((TextBox)x).Text = ((DateTime)dsvalues.Tables[0].Rows[0][dc.ColumnName]).ToString(Session["AppDateFormat"].ToString());
            //                                break;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //            if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
            //            {
            //                DataSet dsmvalues = null;
            //                try
            //                {
            //                    string strfldName = pnlName.ToUpper() + "MULTISELECTLIST";
            //                    Int32 stpos = strfldName.Length;
            //                    Int32 enpos = x.ID.Length - stpos;
            //                    strfldName = x.ID.Substring(stpos, enpos).ToString();
            //                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
            //                    dsmvalues = CustomFields.GetCustomFieldValues("[dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_" + strfldName.ToString() + "]", ",[" + strfldName.ToString() + "]", PatID, 0, Convert.ToInt32(Request.QueryString["visitid"]), 0,  0, Convert.ToInt32(ApplicationAccess.PatientRecord));
            //                    if (dsmvalues != null && dsmvalues.Tables[0].Rows.Count > 0)
            //                        ViewState["CustomFieldsMulti"] = 1;
            //                    foreach (DataRow dr in dsmvalues.Tables[0].Rows)
            //                    {
            //                        foreach (DataColumn dc in dsmvalues.Tables[0].Columns)
            //                        {
            //                            if (pnlName.ToUpper() + "MULTISELECTLIST" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            //                            {
            //                                foreach (ListItem li in ((CheckBoxList)x).Items)
            //                                {
            //                                    if (li.Value == dr[dc.ColumnName].ToString())
            //                                    {
            //                                        li.Selected = true;
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //                catch
            //                {
            //                }
            //                finally
            //                {
            //                    CustomFields = null;
            //                    dsmvalues = null;
            //                }

            //            }
            //        }
            //    }
            //    else
            //    {
            //        foreach (Control x in Cntrl.Controls)
            //            if (x.GetType() == typeof(System.Web.UI.WebControls.CheckBoxList))
            //            {
            //                DataSet dsmvalues = null;
            //                try
            //                {
            //                    string strfldName = pnlName.ToUpper() + "MULTISELECTLIST";
            //                    Int32 stpos = strfldName.Length;
            //                    Int32 enpos = x.ID.Length - stpos;
            //                    strfldName = x.ID.Substring(stpos, enpos).ToString();
            //                    CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
            //                    dsmvalues = CustomFields.GetCustomFieldValues("[dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "_" + strfldName.ToString() + "]", ",[" + strfldName.ToString() + "]", PatID, 0, Convert.ToInt32(Request.QueryString["visitid"]), 0, 0, Convert.ToInt32(ApplicationAccess.PatientRecord));
            //                    if (dsmvalues != null && dsmvalues.Tables[0].Rows.Count > 0)
            //                        ViewState["CustomFieldsMulti"] = 1;


            //                    foreach (DataRow dr in dsmvalues.Tables[0].Rows)
            //                    {
            //                        foreach (DataColumn dc in dsmvalues.Tables[0].Columns)
            //                        {
            //                            if (pnlName.ToUpper() + "MULTISELECTLIST" + dc.ColumnName.ToUpper() == x.ID.ToUpper())
            //                            {
            //                                foreach (ListItem li in ((CheckBoxList)x).Items)
            //                                {
            //                                    if (li.Value == dr[dc.ColumnName].ToString())
            //                                    {
            //                                        li.Selected = true;
            //                                    }
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //                catch
            //                {
            //                }
            //                finally
            //                {
            //                    CustomFields = null;
            //                    dsmvalues = null;
            //                }

            //            }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.Message.ToString();
            //}

        }

        protected void Add_Attributes()
        {
            txtWeight.Attributes.Add("onkeyup", "chkNumeric('" + txtWeight.ClientID + "');funHtWtValidate('" + txtWeight.ClientID + "')");
            txtHeight.Attributes.Add("onkeyup", "chkNumeric('" + txtHeight.ClientID + "')");
            txtHeight.Attributes.Add("onkeyup", "funHtWtValidate('" + txtHeight.ClientID + "')");
            chkDelivered.Attributes.Add("onclick", "DeliverDate();");
            txtEDD.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtDOB.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtvisitDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtvisitDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            txtReadyDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtReadyDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            txtEligibleDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtEligibleDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
            ddlARVStatus.Attributes.Add("onchange", "ShowHideARVStaus('" + theMasterDS.Tables["ARVAdhReason_Cont"].Rows.Count + "', '" + theMasterDS.Tables["ARVReason_Stop"].Rows.Count + "');");
            txtAppDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtAppDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
        }

        protected void ShowData()
        {
            //--- populate data of existing patients ---

            //-- Table 0 - PatientEnrollmentID,PatientName,FileNo --
            //-- Table 1 - PrevMostRecentCD4,PrevMostRecentCD4Date --
            //-- Table 2 - RegimenName,RegimenDate --
            //-- Table 3 - Data of Edit Mode

            string MaxWHOStage;

            IQCareUtils theUtil = new IQCareUtils();
            PatientManager = (IPatientRecord)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRecord, BusinessProcess.Clinical");

            string Mode = Request.QueryString["Name"].ToString();
            int Ptn_Pk = Convert.ToInt32(Request.QueryString["PatientId"]);
            int LocationID = Convert.ToInt32(Session["AppLocationId"]);
            int VisitId = Convert.ToInt32(Request.QueryString["visitid"]);
            if (Mode == "Delete")
                Mode = "Edit";
            DataSet theDS = PatientManager.GetPatientRecord(Mode, Ptn_Pk, LocationID, VisitId);

            //--- WHO Stage - 1 ---

            if (theDS.Tables[11].Rows.Count > 0 && theDS.Tables[11].Rows[0]["MaxWHOStage"] != System.DBNull.Value)
                MaxWHOStage = theDS.Tables[11].Rows[0]["MaxWHOStage"].ToString();
            else
                MaxWHOStage = "0";

            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));

            DataView theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeId=22 AND DeleteFlag=0 AND SystemId=2 and Name>='" + MaxWHOStage + "'";
            DataTable theDT = theDV.ToTable();
            theDT.TableName = "WHOStage";
            theMasterDS.Tables.Add(theDT.Copy());
            BindFunctions theBindMgr = new BindFunctions();
            theBindMgr.BindCombo(ddlWHOStage, theMasterDS.Tables["WHOStage"], "Name", "ID");



            //----- ARV Status starts ------------
            //--- both for ADD/EDIT
            theDV = new DataView(theDSXML.Tables["Mst_ARVStatus"]);
            if (theDS.Tables[14].Rows.Count > 0)
            {
                //Transferred in with records -264
                //Prior Therapy (Transfer in with out records) - 265
                if (theDS.Tables[14].Rows[0]["PriorExposure"].ToString() == "264" ||
                    theDS.Tables[14].Rows[0]["PriorExposure"].ToString() == "265")
                {
                    theDV.RowFilter = "DeleteFlag=0 and ID>=3"; // hide-"No ARV" and "Start ARV"
                }
                else
                {
                    theDV.RowFilter = "DeleteFlag=0";
                }
            }
            else
            {
                theDV.RowFilter = "DeleteFlag=0";
            }

            theDT = theDV.ToTable();
            theDT.TableName = "ARVStatus";
            theMasterDS.Tables.Add(theDT.Copy());
            theBindMgr.BindCombo(ddlARVStatus, theMasterDS.Tables["ARVStatus"], "DisplayName", "ID");
            //----- ARV Status ends ------------



            if (theDS.Tables[0].Rows.Count > 0)
            {
                if (theDS.Tables[0].Rows[0]["PatientName"] != System.DBNull.Value)
                    // lblPatientName.Text = theDS.Tables[0].Rows[0]["PatientName"].ToString();
                    if (theDS.Tables[0].Rows[0]["PatientEnrollmentID"] != System.DBNull.Value)
                        // lblPatEnrolNo.Text = theDS.Tables[0].Rows[0]["PatientEnrollmentID"].ToString();
                        if (theDS.Tables[0].Rows[0]["FileNo"] != System.DBNull.Value)
                            // lblFileNo.Text = theDS.Tables[0].Rows[0]["FileNo"].ToString();
                            if (theDS.Tables[0].Rows[0]["Sex"] != System.DBNull.Value)
                            {
                                if (theDS.Tables[0].Rows[0]["Sex"].ToString() == "17")
                                {
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "show_Pragant", "document.getElementById('divPregnantShow').style.display = 'inline';", true);
                                }
                            }
            }

            if (Mode == "Edit" || Mode == "Delete")
            {
                txtvisitDate.Value = Convert.ToDateTime(theDS.Tables[3].Rows[0]["VisitDate"]).ToString(Session["AppDateFormat"].ToString());
                ddlVisitType.SelectedValue = theDS.Tables[3].Rows[0]["TypeOfVisit"].ToString();
                if (theDS.Tables[3].Rows.Count > 0)
                {
                    txtHeight.Value = theDS.Tables[3].Rows[0]["Height"].ToString();
                    txtWeight.Value = theDS.Tables[3].Rows[0]["Weight"].ToString();
                }
                if (txtHeight.Value != "")
                {
                    if (Convert.ToDecimal(txtHeight.Value) <= 0)
                        txtHeight.Value = "";
                }

                if (txtWeight.Value != "")
                {
                    if (Convert.ToDecimal(txtWeight.Value) <= 0)
                        txtWeight.Value = "";
                }

                ddlWHOStage.SelectedValue = theDS.Tables[3].Rows[0]["WHOStage"].ToString();

                foreach (DataRow theDR in theDS.Tables[4].Rows)
                {
                    if (Convert.ToInt32(theDR["SymptomId"]) == 31)//101)
                        rdoIlnessNone.Checked = true;
                    else if (Convert.ToInt32(theDR["SymptomId"]) == 32)//102)
                        rdoIllnessNotDoc.Checked = true;
                    else
                        PopulateExistIllness(theDR["SymptomId"].ToString());
                }

                txtComplication.Text = theDS.Tables[3].Rows[0]["OtherComplication"].ToString();
                if (theDS.Tables[3].Rows[0]["Pregnant"] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(theDS.Tables[3].Rows[0]["Pregnant"]) == 0)
                        rdoPregNo.Checked = true;
                    else if (Convert.ToInt32(theDS.Tables[3].Rows[0]["Pregnant"]) == 1)
                    {
                        rdoPregYes.Checked = true;
                        if (theDS.Tables[3].Rows[0]["EDD"] != System.DBNull.Value)
                            txtEDD.Value = Convert.ToDateTime(theDS.Tables[3].Rows[0]["EDD"]).ToString(Session["AppDateFormat"].ToString());
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "EDD1", "document.getElementById('divPregnant').style.display='inline';", true);
                        //////string script = "";
                        //////script = "<script language = 'javascript' defer ='defer' id = 'rdopreg'>\n";
                        //////script += "document.getElementById('" + rdoPregYes.ClientID + "').click();\n";
                        //////script += "</script>\n";
                        //////ClientScript.RegisterStartupScript(this.GetType(),"rdopreg", script);

                    }
                }
                ddlFuncStatus.SelectedValue = theDS.Tables[3].Rows[0]["FunctionalStatus"].ToString();
                ddlTBStatus.SelectedValue = theDS.Tables[3].Rows[0]["TBStatus"].ToString();

                if (theDS.Tables[3].Rows[0]["TBID"] != System.DBNull.Value)
                {
                    txtTBID.Text = theDS.Tables[3].Rows[0]["TBID"].ToString();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "TBID", "document.getElementById('divTBStatus').style.display='inline';", true);

                }
                if (theDS.Tables[3].Rows[0]["NutritionalSupport"] != System.DBNull.Value)
                {
                    if (Convert.ToInt32(theDS.Tables[3].Rows[0]["NutritionalSupport"]) == 1)
                        rdoNutSupportNeededYes.Checked = true;
                    else if (Convert.ToInt32(theDS.Tables[3].Rows[0]["NutritionalSupport"]) == 0)
                        rdoNutSupportNeededNo.Checked = true;
                }
                //--- PatientReferredTo
                foreach (DataRow theDR in theDS.Tables[5].Rows)
                {
                    PopulateExistPatRef(theDR["PatientRefID"].ToString(), theDR["PatientRefDesc"].ToString());
                }

                if (theDS.Tables[12].Rows.Count > 0)
                {
                    if (theDS.Tables[12].Rows[0]["AppReason"] != System.DBNull.Value)
                        ddlAppointmentReason.SelectedValue = theDS.Tables[12].Rows[0]["AppReason"].ToString();
                    if (theDS.Tables[12].Rows[0]["AppDate"] != System.DBNull.Value)
                        txtAppDate.Value = Convert.ToDateTime(theDS.Tables[12].Rows[0]["AppDate"]).ToString(Session["AppDateFormat"].ToString());
                    if (theDS.Tables[12].Rows[0]["Signature"] != System.DBNull.Value)
                        ddlSignature.SelectedValue = theDS.Tables[12].Rows[0]["Signature"].ToString();
                    if (theDS.Tables[12].Rows[0]["WeekIndex"] != System.DBNull.Value)
                        ddlNextAppoint.SelectedIndex = Convert.ToInt32(theDS.Tables[12].Rows[0]["WeekIndex"]);
                }

                //--- ARV Details ---

                if (theDS.Tables[8].Rows.Count > 0)
                {
                    if (theDS.Tables[8].Rows[0]["ARVStatus"] != System.DBNull.Value)
                    {
                        ddlARVStatus.SelectedValue = theDS.Tables[8].Rows[0]["ARVStatus"].ToString();
                        if (theDS.Tables[8].Rows[0]["ARVStatus"].ToString() == "1")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "ARVReason1", "document.getElementById('divARVReason').style.display='inline';", true);

                            if (theDS.Tables[8].Rows[0]["ARVReason"] != System.DBNull.Value)
                                ddlARVReason.SelectedValue = theDS.Tables[8].Rows[0]["ARVReason"].ToString();
                        }
                        else if (theDS.Tables[8].Rows[0]["ARVStatus"].ToString() == "2")
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "ARV2", "document.getElementById('divWhyEligible').style.display='inline';", true);

                            if (theDS.Tables[8].Rows[0]["EligibleReason"] != System.DBNull.Value)
                                ddlWhyEligible.SelectedValue = theDS.Tables[8].Rows[0]["EligibleReason"].ToString();

                            if (theDS.Tables[8].Rows[0]["EligibleDate"] != System.DBNull.Value)
                                txtEligibleDate.Value = Convert.ToDateTime(theDS.Tables[8].Rows[0]["EligibleDate"]).ToString(Session["AppDateFormat"].ToString());

                            if (theDS.Tables[8].Rows[0]["ReadyDate"] != System.DBNull.Value)
                                txtReadyDate.Value = Convert.ToDateTime(theDS.Tables[8].Rows[0]["ReadyDate"]).ToString(Session["AppDateFormat"].ToString());
                        }
                        else if (theDS.Tables[8].Rows[0]["ARVStatus"].ToString() == "3")
                        {
                            if (theDS.Tables[6].Rows.Count > 0 && theDS.Tables[6].Rows[0]["AdherenceReason"] != System.DBNull.Value)
                            {
                                if (theDS.Tables[6].Rows[0]["AdherenceReason"].ToString() != "")
                                {
                                    if (theDS.Tables[6].Rows[0]["AdherenceReason"].ToString() == "35")//Good
                                    {
                                        rdoAdhStatusGood.Checked = true;
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-good1", "document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-good2", "document.getElementById('divARVAdhStatus').style.display = 'inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-good3", "document.getElementById('divLblARVReason2').style.display = 'none';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-good4", "document.getElementById('divARVReason2').style.display = 'none';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-good5", "document.getElementById('divARVAdhReason').style.display = 'none';", true);

                                    }
                                    else if (theDS.Tables[6].Rows[0]["AdherenceReason"].ToString() == "36")//Not documented
                                    {
                                        rdoAdhStatusNotDoc.Checked = true;
                                        rdoAdhStatusGood.Checked = true;
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-notdoc1", "document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-notdoc2", "document.getElementById('divARVAdhStatus').style.display = 'inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-notdoc3", "document.getElementById('divLblARVReason2').style.display = 'none';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-notdoc4", "document.getElementById('divARVReason2').style.display = 'none';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-notdoc5", "document.getElementById('divARVAdhReason').style.display = 'none';", true);
                                    }
                                    else
                                    {
                                        rdoAdhStatusPoor.Checked = true;
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-poor1", "document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-poor2", "document.getElementById('divARVAdhStatus').style.display = 'inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-poor3", "document.getElementById('divLblARVReason2').style.display = 'none';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-poor4", "document.getElementById('divARVReason2').style.display = 'none';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-poor5", "document.getElementById('divARVAdhReason').style.display = 'inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "continue-poor6", "document.getElementById('divChkARVAdhReason').style.display = 'inline';", true);

                                        foreach (DataRow theDR in theDS.Tables[6].Rows)
                                        {
                                            PopulateExistAdhReason(theDR["AdherenceReason"].ToString(), theDR["AdherenceReasonOther"].ToString());
                                        }
                                    }
                                }
                            }
                        }
                        else if ((theDS.Tables[8].Rows[0]["ARVStatus"].ToString() == "4") || (theDS.Tables[8].Rows[0]["ARVStatus"].ToString() == "5"))//4-change/5 Stop
                        {
                            foreach (DataRow theDR in theDS.Tables[7].Rows)
                            {
                                if (theDR["ARVReasonChange"] != System.DBNull.Value)
                                {
                                    PopulateExistARVReason(theDR["ARVReasonChange"].ToString(), theDR["ARVReasonChangeOther"].ToString());

                                }
                            }

                            if (theDS.Tables[6].Rows[0]["AdherenceReason"] != System.DBNull.Value)
                            {
                                if (theDS.Tables[6].Rows[0]["AdherenceReason"].ToString() != "")
                                {
                                    if (theDS.Tables[6].Rows[0]["AdherenceReason"].ToString() == "35")//Good
                                    {
                                        rdoAdhStatusGood.Checked = true;
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeStop-good1", "document.getElementById('divARVAdhStatusANDAdhReason').style.display='inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeStop-good2", "document.getElementById('divARVAdhStatus').style.display = 'inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeStop-good5", "document.getElementById('divARVAdhReason').style.display = 'none';", true);

                                    }
                                    else if (theDS.Tables[6].Rows[0]["AdherenceReason"].ToString() == "36")//Not documented
                                    {
                                        rdoAdhStatusNotDoc.Checked = true;
                                        rdoAdhStatusGood.Checked = true;

                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeStop-ND1", "document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeStop-ND2", "document.getElementById('divARVAdhStatus').style.display = 'inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeStop-ND5", "document.getElementById('divARVAdhReason').style.display = 'none';", true);
                                    }
                                    else //poor
                                    {
                                        rdoAdhStatusPoor.Checked = true;

                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "change-stop-poor2", "document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "ChangeStop-ND2", "document.getElementById('divARVAdhStatus').style.display = 'inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "change-stop-poor2", "document.getElementById('divARVAdhReason').style.display = 'inline';", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "change-stop-poor3", "document.getElementById('divChkARVAdhReason').style.display = 'inline';", true);
                                        foreach (DataRow theDR in theDS.Tables[6].Rows)
                                        {
                                            PopulateExistAdhReason(theDR["AdherenceReason"].ToString(), theDR["AdherenceReasonOther"].ToString());
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                if (theDS.Tables[3].Rows[0]["Delivered"] != System.DBNull.Value)
                {
                    if (theDS.Tables[3].Rows[0]["Delivered"].ToString() != "")
                    {
                        if (theDS.Tables[3].Rows[0]["Delivered"].ToString() == "1")
                        {
                            chkDelivered.Checked = true;
                        }
                    }
                }

                if (theDS.Tables[3].Rows[0]["DOB"] != System.DBNull.Value) //2
                {
                    txtDOB.Value = Convert.ToDateTime(theDS.Tables[3].Rows[0]["DOB"]).ToString(Session["AppDateFormat"].ToString());
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showDOB", "document.getElementById('divChkDelivered').style.display='inline';", true);
                    chkDelivered.Checked = true;
                }

                if (theDS.Tables[3].Rows.Count > 0)
                {
                    if (theDS.Tables[3].Rows[0]["VisitDate"] != System.DBNull.Value)
                    {
                        if (theDS.Tables[3].Rows[0]["VisitDate"].ToString() != "")
                            ViewState["OriginalDate"] = theDS.Tables[3].Rows[0]["VisitDate"].ToString();
                    }
                }
            }
            if (theDS.Tables[9].Rows.Count > 0)
            {
                if (theDS.Tables[9].Rows[0]["InitialVisitDate"] != System.DBNull.Value)
                {
                    if (theDS.Tables[9].Rows[0]["InitialVisitDate"].ToString() != "")
                        ViewState["InitialVisitDate"] = theDS.Tables[9].Rows[0]["InitialVisitDate"].ToString();
                }
            }


            if (theDS.Tables[3].Rows.Count > 0)
            {
                if (theDS.Tables[3].Rows[0]["DQ"].ToString() == "1")
                    btnDQ.CssClass = "greenbutton";
            }

            if (theDS.Tables[13].Rows.Count > 0 && theDS.Tables[13].Rows[0]["TransferredDate"] != System.DBNull.Value)
            {
                if (theDS.Tables[13].Rows[0]["TransferredDate"].ToString() != "")
                    ViewState["TransferredDate"] = theDS.Tables[13].Rows[0]["TransferredDate"].ToString();
            }
        }

        protected void PopulateExistARVReason(string ARVReasonChange, string ARVReasonChangeOther)
        {
            // mark the checkboxes of the ARV Reason - for existing patient

            foreach (HtmlTableRow r in tblARVReason2.Rows)
            {
                foreach (HtmlTableCell c in r.Cells)
                {
                    foreach (Control ctrl in c.Controls)
                    {
                        if (ctrl.GetType() == typeof(HtmlInputCheckBox))
                        {
                            if (ctrl.ID == "ARVReason" + ARVReasonChange)
                            {
                                ((HtmlInputCheckBox)ctrl).Checked = true;
                            }

                        }

                        //--- for other -1

                        if ((ARVReasonChange == "62") && (ctrl.GetType() == typeof(HtmlInputText)) && (ctrl.ID == "txtotherARVReason1") && (ARVReasonChangeOther != ""))
                        {
                            ((HtmlInputText)ctrl).Value = ARVReasonChangeOther;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "OtherReason1", "document.getElementById('divOtherARVReason1').style.display='inline';", true);
                        }

                        //--- for other -2
                        if ((ARVReasonChange == "69") && (ARVReasonChangeOther != "") && (ctrl.GetType() == typeof(HtmlInputText)) && (ctrl.ID == "txtotherARVReason2"))
                        {
                            ((HtmlInputText)ctrl).Value = ARVReasonChangeOther;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "OtherReason2", "document.getElementById('divOtherARVReason2').style.display='inline';", true);
                        }
                    }

                }
            }
        }

        protected void PopulateExistAdhReason(string AdherenceReason, string AdherenceReasonOther)
        {

            // mark the checkboxes of the patientreferredto which existing patient suffer from

            foreach (HtmlTableRow r in tbARVAdhReason.Rows)
            {
                foreach (HtmlTableCell c in r.Cells)
                {
                    foreach (Control ctrl in c.Controls)
                    {
                        if (ctrl.GetType() == typeof(HtmlInputCheckBox))
                        {
                            if (ctrl.ID == "ARVAdhReason" + AdherenceReason)
                            {
                                ((HtmlInputCheckBox)ctrl).Checked = true;
                            }

                        }
                        //--- for other
                        if (AdherenceReasonOther != "")
                        {
                            if (ctrl.GetType() == typeof(HtmlInputText))
                            {
                                if (ctrl.ID == "txtotherARVAdh")
                                {
                                    ((HtmlInputText)ctrl).Value = AdherenceReasonOther;
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "OtherAdh1", "document.getElementById('divOtherARVAdh').style.display='inline';", true);
                                }

                            }
                        }
                    }
                }
            }

        }

        protected void PopulateExistPatRef(string PatRefID, string PatRefDesc)
        {
            // mark the checkboxes of the patientreferredto which existing patient suffer from

            foreach (HtmlTableRow r in tblRefer.Rows)
            {
                foreach (HtmlTableCell c in r.Cells)
                {
                    foreach (Control ctrl in c.Controls)
                    {
                        if (ctrl.GetType() == typeof(HtmlInputCheckBox))
                        {
                            if (ctrl.ID == "ReferTo" + PatRefID)
                            {
                                ((HtmlInputCheckBox)ctrl).Checked = true;
                            }
                        }
                        //--- for other
                        if (PatRefDesc != "")
                        {
                            if (ctrl.GetType() == typeof(HtmlInputText))
                            {
                                if (ctrl.ID == "txtReferOther")
                                {
                                    ((HtmlInputText)ctrl).Value = PatRefDesc;
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "ReferTo1", "document.getElementById('divOtherRefer').style.display='inline';", true);

                                }

                            }
                        }
                    }
                }
            }

        }

        protected void PopulateExistIllness(string IllnessID)
        {

            // mark the checkboxes of the illness which existing patient suffer from

            foreach (HtmlTableRow r in tblIllness1.Rows)
            {
                foreach (HtmlTableCell c in r.Cells)
                {
                    foreach (Control ctrl in c.Controls)
                    {
                        if (ctrl.GetType() == typeof(HtmlInputCheckBox))
                        {
                            if (ctrl.ID == "Illness1" + IllnessID)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "document.getElementById('probSymptSideEffect').style.display = 'inline';", true);
                                rdoIllness.Checked = true;
                                ((HtmlInputCheckBox)ctrl).Checked = true;
                            }
                        }
                    }
                }
            }

            foreach (HtmlTableRow r in tblIllness2.Rows)
            {
                foreach (HtmlTableCell c in r.Cells)
                {
                    foreach (Control ctrl in c.Controls)
                    {
                        if (ctrl.GetType() == typeof(HtmlInputCheckBox))
                        {
                            if (ctrl.ID == "Illness2" + IllnessID)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "document.getElementById('probSymptSideEffect').style.display = 'inline';", true);
                                rdoIllness.Checked = true;
                                ((HtmlInputCheckBox)ctrl).Checked = true;
                            }
                        }
                    }
                }
            }

        }

        private void DeleteForm()
        {
            int theResultRow, OrderNo;
            string FormName;
            OrderNo = Convert.ToInt32(Request.QueryString["visitid"].ToString());
            FormName = "Patient Record - Follow Up";


            PatientManager = (IPatientRecord)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRecord, BusinessProcess.Clinical");
            theResultRow = PatientManager.DeletePatientRecord(FormName, OrderNo, Convert.ToInt32(Request.QueryString["PatientId"].ToString()), Convert.ToInt32(Session["AppUserId"]));

            if (theResultRow == 0)
            {
                IQCareMsgBox.Show("RemoveFormError", this);
                return;
            }
            else
            {
                string theUrl;
                theUrl = string.Format("{0}?PatientId={1}", "../ClinicalForms/frmPatient_Home.aspx", Request.QueryString["PatientId"].ToString());
                Response.Redirect(theUrl);
            }

        }

        protected void fillDropDown()
        {
            //--- populate & bind all dropdowns ---

            DataSet theDSXML = new DataSet();
            theDSXML.ReadXml(Server.MapPath("..\\XMLFiles\\AllMasters.con"));

            //---     TypeOf Visit -0 ----
            DataView theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeId=1003 AND DeleteFlag=0";
            DataTable theDT = theDV.ToTable();
            theDT.TableName = "TypeOfVisit";
            theMasterDS.Tables.Add(theDT.Copy());// 0 
            BindFunctions theBindMgr = new BindFunctions();
            theBindMgr.BindCombo(ddlVisitType, theMasterDS.Tables["TypeOfVisit"], "Name", "ID");


            ////--- WHO Stage - 1 : at other place---

            //--- Functional Status - 2 ---- 
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeId=1004 AND DeleteFlag=0";
            theDT = theDV.ToTable();
            theDT.TableName = "FuncStatus";
            theMasterDS.Tables.Add(theDT.Copy());
            theBindMgr.BindCombo(ddlFuncStatus, theMasterDS.Tables["FuncStatus"], "DisplayName", "ID");

            //--- TB Status - 3 ----
            theDV = new DataView(theDSXML.Tables["Mst_TBStatus"]);
            theDV.RowFilter = "DeleteFlag=0";
            theDT = theDV.ToTable();
            theDT.TableName = "TBStatus";
            theMasterDS.Tables.Add(theDT.Copy());
            theBindMgr.BindCombo(ddlTBStatus, theMasterDS.Tables["TBStatus"], "DisplayName", "ID");

            //--- ARV Reason - 5 ----
            theDV = new DataView(theDSXML.Tables["Mst_Reason"]);
            if (Request.QueryString["name"] == "Edit")
                theDV.RowFilter = "CategoryID=4";
            else
                theDV.RowFilter = "DeleteFlag=0 AND CategoryID=4";

            theDT = theDV.ToTable();
            theDT.TableName = "ARVReason";
            theMasterDS.Tables.Add(theDT.Copy());
            theBindMgr.BindCombo(ddlARVReason, theMasterDS.Tables["ARVReason"], "DisplayName", "ID");

            //--- Table no. 6 -Illness1
            //--- Table no. 7 -Illness2

            //--- Why Eligible - 8 ----
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeId=1002 AND DeleteFlag=0";
            theDT = theDV.ToTable();
            theDT.TableName = "WhyEligible";
            theMasterDS.Tables.Add(theDT.Copy());
            theBindMgr.BindCombo(ddlWhyEligible, theMasterDS.Tables["WhyEligible"], "Name", "ID");


            //--- ARVAdhReason_Cont - 9 ----
            theDV = new DataView(theDSXML.Tables["Mst_Reason"]);
            if (Request.QueryString["name"] == "Edit")
                theDV.RowFilter = "CategoryID=5 AND SystemId=2 AND Id>=37 and Id<89";
            else
                theDV.RowFilter = "CategoryID=5 AND DeleteFlag=0 AND SystemId=2 AND Id>=37 and Id<89";

            theDT = theDV.ToTable();
            theDT.TableName = "ARVAdhReason_Cont";
            theMasterDS.Tables.Add(theDT.Copy());
            FillARVAdhReason(theMasterDS.Tables["ARVAdhReason_Cont"]);

            //--- ARVReason_Stop & Change - 9 ----
            theDV = new DataView(theDSXML.Tables["Mst_Reason"]);

            if (Request.QueryString["name"] == "Edit")
                theDV.RowFilter = "CategoryID=3";
            else
                theDV.RowFilter = "CategoryID=3 AND DeleteFlag=0";

            theDT = theDV.ToTable();
            theDT.TableName = "ARVReason_Stop";
            theMasterDS.Tables.Add(theDT.Copy());
            FillARVReason(theMasterDS.Tables["ARVReason_Stop"]);

            //--- Referred To - 10 ----
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeID=1005";
            theDV.Sort = "Code";
            theDT = theDV.ToTable();
            theDT.TableName = "ReferredTo";
            theMasterDS.Tables.Add(theDT.Copy());
            Fillrefer(theMasterDS.Tables["ReferredTo"]);

            //--- Appointment Reason - 12 ----
            theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
            theDV.RowFilter = "CodeId=26 AND DeleteFlag=0";
            theDT = theDV.ToTable();
            theDT.TableName = "AppointmentReason";
            theMasterDS.Tables.Add(theDT.Copy());
            theBindMgr.BindCombo(ddlAppointmentReason, theMasterDS.Tables["AppointmentReason"], "Name", "ID");

            //--- Signature - 9 ----
            theDV = new DataView(theDSXML.Tables["Mst_Employee"]);
            theDT = theDV.ToTable();
            theDT.TableName = "Employee";
            theMasterDS.Tables.Add(theDT.Copy());
            theBindMgr.BindCombo(ddlSignature, theMasterDS.Tables["Employee"], "EmployeeName", "EmployeeId");

            ViewState["theMasterDS"] = theMasterDS;
            Session["Employee"] = theMasterDS.Tables["Employee"];// used only for CD4/TLC popup
        }

        protected Boolean Validation()
        {
            //--- validations before saving ---
            Boolean theValid = true;
            DataSet theDS = new DataSet();
            DateTime theVisitDate;
            DateTime theEnrolDate;

            IIQCareSystem IQCareSecurity;
            IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = (DateTime)IQCareSecurity.SystemDate();


            IQCareUtils theUtils = new IQCareUtils();
            if (txtvisitDate.Value == "")
            {
                MsgBuilder theMsg = new MsgBuilder();
                theMsg.DataElements["Control"] = "Visit Date";
                IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                return false;
            }
            theVisitDate = Convert.ToDateTime(theUtils.MakeDate(txtvisitDate.Value));

            PatientManager = (IPatientRecord)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRecord, BusinessProcess.Clinical");

            int VisitId;
            if (Request.QueryString["visitid"] == null)
                VisitId = 0;
            else
                VisitId = Convert.ToInt32(Request.QueryString["visitid"]);

            //pr_Clinical_CheckVisitDatePatientRecordCTC_Constella
            theDS = PatientManager.CheckVisitDate(Request.QueryString["PatientId"].ToString(), Convert.ToInt32(Session["AppLocationID"]), theVisitDate, VisitId);

            //----- Enrol Date-----
            if (theDS.Tables[1].Rows.Count > 0)
            {
                if (theDS.Tables[1].Rows[0]["EnrolDate"] != System.DBNull.Value)
                {
                    if (theDS.Tables[1].Rows[0]["EnrolDate"].ToString() != "")
                    {
                        theEnrolDate = Convert.ToDateTime(theDS.Tables[1].Rows[0]["EnrolDate"].ToString());
                        if (theEnrolDate > theVisitDate)
                        {
                            IQCareMsgBox.Show("GreaterEnrolDate", this);
                            txtvisitDate.Focus();
                            return false;
                        }
                    }
                }
            }

            //----- future date ----
            if (theVisitDate > theCurrentDate)
            {
                IQCareMsgBox.Show("NoFutureDate", this);
                txtvisitDate.Focus();
                return false;
            }

            /*
            Add - chk date - no duplicate forms
             * - date shud not be less than initial visitdate
             * 
            Edit-
             * ---- form chosed is of Initial visit
             *      --- u can change date to earlier than initial visit date
             *      --- u can change date to future date , but make sure there is no other followup form before that date
             * ---  form chosen is Followup
             *      --- u can change to other date, but make sure no other form exists on that date 
             *      --- date shud not be earlier than initial date
            */

            if (Request.QueryString["name"].ToString() == "Add")
            {
                //----- duplicate form on same date ---
                if (theDS.Tables[0].Rows.Count > 0)
                {
                    if (theDS.Tables[0].Rows[0][0] != System.DBNull.Value)
                    {
                        if (Convert.ToInt32(theDS.Tables[0].Rows[0][0]) == 0)
                        {
                            IQCareMsgBox.Show("PatientRecordExists", this);
                            return false;
                        }
                    }
                }

                //----- Initial Visit (first visited date) ----
                if (ViewState["InitialVisitDate"].ToString() != "")
                {
                    if (theVisitDate <= Convert.ToDateTime(ViewState["InitialVisitDate"].ToString()))
                    {
                        IQCareMsgBox.Show("InitialVisitDate", this);
                        txtvisitDate.Focus();
                        return false;
                    }
                }
                //LatestFormDate
                if (theDS.Tables[4].Rows.Count > 0)
                {
                    if (theDS.Tables[4].Rows[0]["LatestFormDate"] != System.DBNull.Value)
                    {
                        if (theVisitDate <= Convert.ToDateTime((theDS.Tables[4].Rows[0]["LatestFormDate"].ToString())))
                        {
                            IQCareMsgBox.Show("LatestForm", this);
                            return false;
                        }
                    }
                }

                if (ViewState["TransferredDate"] != null)
                {
                    if (ViewState["TransferredDate"].ToString() != "")
                    {
                        if (theVisitDate < Convert.ToDateTime(ViewState["TransferredDate"]))
                        {
                            IQCareMsgBox.Show("TransferPatRec", this);
                            txtvisitDate.Focus();
                            return false;
                        }
                    }
                }
            }
            else if (Request.QueryString["name"].ToString() == "Edit")
            {
                if (ViewState["IsInitialVisitForm"].ToString() == "TRUE")
                {
                    //---- initial visit forms date can be chaged to future date but make sure no
                    //---- other follow up form exists before the "theVisitDate"
                    if (theDS.Tables[3].Rows.Count > 0)
                    {
                        if (theDS.Tables[3].Rows[0]["FirstFollowUpDate"] != System.DBNull.Value)
                        {
                            if (theVisitDate >= Convert.ToDateTime((theDS.Tables[3].Rows[0]["FirstFollowUpDate"].ToString())))
                            {
                                IQCareMsgBox.Show("FollowupFormExists", this);
                                return false;
                            }
                        }
                    }

                }
                else if (ViewState["IsInitialVisitForm"].ToString() == "FALSE")
                {
                    if (ViewState["InitialVisitDate"].ToString() != "")
                    {
                        if (theVisitDate <= Convert.ToDateTime(ViewState["InitialVisitDate"].ToString()))
                        {
                            IQCareMsgBox.Show("InitialVisitDate", this);
                            txtvisitDate.Focus();
                            return false;
                        }
                    }

                    //----- duplicate form on same date ---
                    if (theVisitDate.ToString() != ViewState["OriginalDate"].ToString())
                    {
                        if (theDS.Tables[0].Rows.Count > 0)
                        {
                            if (theDS.Tables[0].Rows[0][0] != System.DBNull.Value)
                            {
                                if (Convert.ToInt32(theDS.Tables[0].Rows[0][0]) == 0)
                                {
                                    IQCareMsgBox.Show("PatientRecordExists", this);
                                    return false;
                                }
                            }
                        }
                    }

                    //--- Editing the Visit Date - Trying change visit date to previous date
                    if (theDS.Tables[5].Rows.Count > 0)
                    {
                        if (theDS.Tables[5].Rows[0]["PrevFormDate"] != System.DBNull.Value)
                        {
                            if (theVisitDate <= Convert.ToDateTime((theDS.Tables[5].Rows[0]["PrevFormDate"].ToString())))
                            {
                                IQCareMsgBox.Show("LatestForm", this);
                                return false;
                            }
                        }
                    }
                }
            }


            //---- ARV Therary

            if (ddlARVStatus.SelectedValue.ToString() == "1")
            {
                if (ddlARVReason.SelectedValue.ToString() == "0")
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "ARV Reason";
                    IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                    return false;
                }
            }

            if (ddlARVStatus.SelectedValue.ToString() == "2")
            {
                if (ddlWhyEligible.SelectedValue.ToString() == "0")
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Why Eligible";
                    IQCareMsgBox.Show("BlankTextBox", theMsg, this);
                    return false;
                }

                if (txtEligibleDate.Value != "" && Convert.ToDateTime(theUtils.MakeDate(txtEligibleDate.Value)) > theCurrentDate)
                {
                    IQCareMsgBox.Show("EligibleDate", this);
                    txtEligibleDate.Focus();
                    return false;
                }

                if (txtReadyDate.Value != "" && Convert.ToDateTime(theUtils.MakeDate(txtReadyDate.Value)) > theCurrentDate)
                {
                    IQCareMsgBox.Show("ReadyDate", this);
                    txtReadyDate.Focus();
                    return false;
                }
            }

            if (ddlARVStatus.SelectedValue.ToString() == "3")
            {
                if (rdoAdhStatusGood.Checked == false && rdoAdhStatusNotDoc.Checked == false && rdoAdhStatusPoor.Checked == false)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "V1", "document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "V2", "document.getElementById('divARVAdhStatus').style.display = 'inline';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "V3", "document.getElementById('divLblARVReason2').style.display = 'none';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "V4", "document.getElementById('divARVReason2').style.display = 'none';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "V5", "document.getElementById('divARVAdhReason').style.display = 'none';", true);

                    IQCareMsgBox.Show("SelectARVAdherenceStatus", this);
                    return false;
                }

                if (rdoAdhStatusPoor.Checked == true)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "V6", "document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "V7", "document.getElementById('divARVAdhStatus').style.display = 'inline';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "V8", "document.getElementById('divLblARVReason2').style.display = 'none';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "V9", "document.getElementById('divARVReason2').style.display = 'none';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "V10", "document.getElementById('divARVAdhReason').style.display = 'inline';", true);
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "V11", "document.getElementById('divChkARVAdhReason').style.display = 'inline';", true);

                    GetChkdAdhReason();
                    if (arrAdhReason.GetValue(0, 0) == null)
                    {
                        IQCareMsgBox.Show("SelectARVAdherenceReason", this);
                        return false;
                    }
                    Array.Clear(arrAdhReason, 0, arrAdhReason.Length);
                }
            }

            if (ddlARVStatus.SelectedValue.ToString() == "4" || ddlARVStatus.SelectedValue.ToString() == "5")
            {

                Page.ClientScript.RegisterStartupScript(this.GetType(), "V11", "document.getElementById('divARVAdhStatusANDAdhReason').style.display = 'inline';", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "V12", "document.getElementById('divARVAdhStatus').style.display = 'inline';", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "V13", "document.getElementById('divLblARVReason2').style.display = 'inline';", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "V14", "document.getElementById('divARVReason2').style.display = 'inline';", true);
                if (rdoAdhStatusPoor.Checked == true)
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL4/5_6", "document.getElementById('divARVAdhReason').style.display = 'inline';", true);
                else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "CL4/5_5", "document.getElementById('divARVAdhReason').style.display = 'none';", true);

                GetChkdARVReason4();

                if (arrARVReason4.GetValue(0, 0) == null)
                {
                    IQCareMsgBox.Show("SelectARVReason", this);
                    return false;
                }
                else if (arrARVReason4.GetValue(0, 0).ToString() == "")
                {
                    Array.Clear(arrARVReason4, 0, arrARVReason4.Length);
                    IQCareMsgBox.Show("SelectARVReason", this);
                    return false;
                }

                if (rdoAdhStatusGood.Checked == false && rdoAdhStatusNotDoc.Checked == false && rdoAdhStatusPoor.Checked == false)
                {
                    IQCareMsgBox.Show("SelectARVAdherenceStatus", this);
                    return false;
                }

                if (rdoAdhStatusPoor.Checked == true)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "V16", "document.getElementById('divARVAdhReason').style.display = 'inline';", true);

                    GetChkdAdhReason();
                    if (arrAdhReason.GetValue(0, 0) == null)
                    {
                        Array.Clear(arrAdhReason, 0, arrAdhReason.Length);
                        IQCareMsgBox.Show("SelectARVAdherenceReason", this);
                        return false;
                    }

                }
            }

            if (rdoIllness.Checked == true)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Illness1", "document.getElementById('probSymptSideEffect').style.display = 'inline';", true);

                GetChkdIllness();

                if (arrIllness.GetValue(0) == null)
                {
                    Array.Clear(arrIllness, 0, arrIllness.Length);
                    IQCareMsgBox.Show("SelectIllness", this);
                    return false;
                }
            }

            if (txtWeight.Value.ToString() != "" && (Convert.ToDecimal(txtWeight.Value) > 225 || Convert.ToDecimal(txtWeight.Value) <= 0))
            {
                IQCareMsgBox.Show("chkWeight", this);
                txtWeight.Focus();
                return false;
            }

            if (txtHeight.Value.ToString() != "" && (Convert.ToDecimal(txtHeight.Value) > 250 || Convert.ToDecimal(txtHeight.Value) <= 0))
            {
                IQCareMsgBox.Show("chkHeight", this);
                txtHeight.Focus();
                return false;
            }

            if (rdoPregYes.Checked == true)
            {
                if (txtEDD.Value != "")
                {
                    if (Convert.ToDateTime(txtEDD.Value) > Convert.ToDateTime(txtvisitDate.Value).AddMonths(10))
                    {
                        IQCareMsgBox.Show("EDDDate", this);
                        txtEDD.Focus();
                        return false;
                    }
                }
                if (txtEDD.Value != "" && txtDOB.Value != "")
                {
                    if (Convert.ToDateTime(txtDOB.Value) < Convert.ToDateTime(txtEDD.Value))
                    {
                        IQCareMsgBox.Show("EDDDOB", this);
                        txtDOB.Focus();
                        return false;
                    }

                    //--- months of birth---
                    TimeSpan months = Convert.ToDateTime(theUtils.MakeDate(txtDOB.Value)).Subtract(Convert.ToDateTime(theUtils.MakeDate(txtEDD.Value)));
                    if (months.Days / 31 > 9)
                    {
                        IQCareMsgBox.Show("BirthDuration", this);
                        txtDOB.Focus();
                        return false;
                    }
                }
            }

            //------ check ARV Status : starts--------------
            //1-No ARV:2-StartARV:3-Continue:4-change:6-Restart 
            Boolean blnARVStatus = true;
            Int32 selARVSatus = Convert.ToInt32(ddlARVStatus.SelectedValue);
            Boolean ARTEnded = false;

            if (theDS.Tables[8].Rows.Count > 0)
            {
                if (theDS.Tables[8].Rows[0]["ARTEnded"].ToString() == "1")
                {
                    ARTEnded = true;
                    if (selARVSatus != 2 && selARVSatus != 6)
                    {
                        IQCareMsgBox.Show("ARTEndCareTrak", this);
                        return false;
                    }
                }
            }

            if (ARTEnded == false)
            {
                //-- both for ADD/EDIT ( prev ARV Status need to be checked )--
                if (theDS.Tables[6].Rows.Count > 0)
                {
                    Int32 PrevARVStatusID = Convert.ToInt32(theDS.Tables[6].Rows[0]["PrevARVStatusID"]);
                    string PrevARVStatusName = theDS.Tables[6].Rows[0]["PrevARVStatusName"].ToString();

                    if (PrevARVStatusID > 0)
                    {
                        if ((PrevARVStatusID == 1) && (selARVSatus != 1 && selARVSatus != 2))
                            blnARVStatus = false;
                        else if ((PrevARVStatusID == 2 || PrevARVStatusID == 3 || PrevARVStatusID == 6) &&
                            (selARVSatus != 3 && selARVSatus != 4))
                            blnARVStatus = false;
                        else if (PrevARVStatusID == 4 && selARVSatus != 3)
                            blnARVStatus = false;
                        if (blnARVStatus == false)
                        {
                            MsgBuilder theMsg = new MsgBuilder();
                            theMsg.DataElements["Control"] = PrevARVStatusName;
                            IQCareMsgBox.Show("ARVStatusPrev", theMsg, this);
                            return false;
                        }
                    }
                }
                else
                {
                    // its initail visit form
                    if (ddlARVStatus.Items.Count > 4) // ART has been set in enrollment form
                    {
                        if (selARVSatus == 3 || selARVSatus == 4 || selARVSatus == 6)
                        {
                            IQCareMsgBox.Show("NewARV", this);
                            return false;
                        }
                    }
                }

                //--- for EDIT next forms's ARVStatus also need to be checked ---
                if (Request.QueryString["name"].ToString() == "Edit")
                {
                    if (theDS.Tables[7].Rows.Count > 0)
                    {
                        Int32 NextARVStatusID = Convert.ToInt32(theDS.Tables[7].Rows[0]["NextARVStatusID"]);
                        string NextARVStatusName = theDS.Tables[7].Rows[0]["NextARVStatusName"].ToString();

                        if (NextARVStatusID > 0)
                        {
                            if ((NextARVStatusID == 1) && (selARVSatus != 1))
                                blnARVStatus = false;
                            else if ((NextARVStatusID == 2) && (selARVSatus != 1))
                                blnARVStatus = false;
                            else if ((NextARVStatusID == 3) && (selARVSatus != 2 && selARVSatus != 3 &&
                                selARVSatus != 4 && selARVSatus != 6))
                                blnARVStatus = false;
                            else if ((NextARVStatusID == 4) && (selARVSatus != 3 && selARVSatus != 6))
                                blnARVStatus = false;
                            if (blnARVStatus == false)
                            {
                                MsgBuilder theMsg = new MsgBuilder();
                                theMsg.DataElements["Control"] = NextARVStatusName;
                                IQCareMsgBox.Show("ARVStatusNext", theMsg, this);
                                return false;
                            }
                        }
                    }
                }
                //------ check ARV Status : ends-------------- 
            }
            return theValid;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["name"] == "Delete")
                {
                    DeleteForm();
                }
                else if (Validation() == true)
                {
                    MakeHTandSave();//Make hash table & save
                    SaveCancel();
                    ClearObjects();
                }
                else
                {
                    ClientScripts();
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private void MakeHTandSave()
        {
            //--- make hash table then pass it at parameter for save function
            string strCustomField = string.Empty;

            int patientId = Convert.ToInt32(Request.QueryString["PatientId"]);

            IQCareUtils theUtils = new IQCareUtils();

            Hashtable ht = new Hashtable();
            ht.Clear();
            ht.Add("Mode", Request.QueryString["Name"]);
            ht.Add("LocationID", Convert.ToInt32(Session["AppLocationID"]));
            ht.Add("UserID", Convert.ToInt32(Session["AppUserId"]));
            ht.Add("Ptn_Pk", Convert.ToInt32(Request.QueryString["PatientId"]));
            ht.Add("VisitId", Convert.ToInt32(Request.QueryString["VisitId"]));
            ht.Add("VisitDate", txtvisitDate.Value);
            if (ddlVisitType.SelectedValue.ToString() == "0")
                ht.Add("TypeOfVisit", "");
            else
                ht.Add("TypeOfVisit", Convert.ToInt32(ddlVisitType.SelectedValue));

            ht.Add("Height", txtHeight.Value);
            ht.Add("Weight", txtWeight.Value);

            if (ddlWHOStage.SelectedValue.ToString() == "0")
                ht.Add("WHOStage", "");
            else
                ht.Add("WHOStage", ddlWHOStage.SelectedValue);

            if (rdoIlnessNone.Checked == true)
                arrIllness[0] = "31";//"101";
            else if (rdoIllnessNotDoc.Checked == true)
                arrIllness[0] = "32";//"102";
            else if (rdoIllness.Checked == true)
                GetChkdIllness();

            ht.Add("OtherComplication", txtComplication.Text);

            if (rdoPregYes.Checked == true)
                ht.Add("Pregnant", 1);
            else if (rdoPregNo.Checked == true)
                ht.Add("Pregnant", 0);
            else
                ht.Add("Pregnant", "");

            ht.Add("EDD", txtEDD.Value);

            if (ddlFuncStatus.SelectedValue.ToString() == "0")
                ht.Add("FuncStatus", "");
            else
                ht.Add("FuncStatus", ddlFuncStatus.SelectedValue);

            if (ddlTBStatus.SelectedValue.ToString() == "0")
                ht.Add("TBStatus", "");
            else
                ht.Add("TBStatus", ddlTBStatus.SelectedValue);

            ht.Add("TBID", txtTBID.Text);

            if (rdoNutSupportNeededYes.Checked == true)
                ht.Add("NutritionalSupport", 1);
            else if (rdoNutSupportNeededNo.Checked == true)
                ht.Add("NutritionalSupport", 0);
            else
                ht.Add("NutritionalSupport", ""); // nothing selected

            GetChkdReferredTo(); // get checkd referred to

            if (ddlAppointmentReason.SelectedValue.ToString() == "0")
                ht.Add("AppReason", "");
            else
                ht.Add("AppReason", ddlAppointmentReason.SelectedValue);

            ht.Add("AppDate", txtAppDate.Value);


            if (ddlSignature.SelectedValue.ToString() == "0")
                ht.Add("Signature", "");
            else
                ht.Add("Signature", ddlSignature.SelectedValue);

            //----------- ARV Details --------------
            ht.Add("ARVStatus", ddlARVStatus.SelectedValue);
            ht.Add("ARVReason", ddlARVReason.SelectedValue);//1 & 4
            ht.Add("EligibleReason", ddlWhyEligible.SelectedValue);//2
            ht.Add("EligibleDate", txtEligibleDate.Value);
            ht.Add("ReadyDate", txtReadyDate.Value);

            if (rdoAdhStatusGood.Checked == true) //3
            {
                ht.Add("AdherenceReason", "37");
                arrAdhReason[0, 0] = "35";
                arrAdhReason[0, 1] = "";
            }
            else if (rdoAdhStatusNotDoc.Checked == true)
            {
                ht.Add("AdherenceReason", "38");
                arrAdhReason[0, 0] = "36";
                arrAdhReason[0, 1] = "";
            }
            else if (rdoAdhStatusPoor.Checked == true)
                GetChkdAdhReason();

            GetChkdARVReason4(); //---4= change---

            //---- CD4 / TLC---
            Hashtable htCD4TLC = new Hashtable();

            if (Session["htCD4TLC"] == null)
                RefreshCD4TLC(htCD4TLC);
            else if (Session["htCD4TLC"].ToString() == "")
                RefreshCD4TLC(htCD4TLC);
            else
                htCD4TLC = (Hashtable)Session["htCD4TLC"];

            ht.Add("PrevLABID", htCD4TLC["LABID"]);
            if (ht["PrevLABID"] == null)
                ht["PrevLABID"] = "";
            ht.Add("CD4", htCD4TLC["CD4"]);

            if (ht["CD4"] == null || ht["EligibleReason"].ToString() != "266")
                ht["CD4"] = "";

            ht.Add("CD4Percent", htCD4TLC["CD4Percent"]);

            if (ht["CD4Percent"] == null || ht["EligibleReason"].ToString() != "266")
                ht["CD4Percent"] = "";


            ht.Add("TLC", htCD4TLC["TLC"]);

            if (ht["TLC"] == null || ht["EligibleReason"].ToString() != "268")
                ht["TLC"] = "";

            ht.Add("TLCPercent", htCD4TLC["TLCPercent"]);

            if (ht["TLCPercent"] == null || ht["EligibleReason"].ToString() != "268")
                ht["TLCPercent"] = "";

            ht.Add("OrderedBy", htCD4TLC["OrderedBy"]);
            if (ht["OrderedBy"] == null)
                ht["OrderedBy"] = "";

            ht.Add("OrderedDate", htCD4TLC["OrderedDate"]);
            if (ht["OrderedDate"] == null)
                ht["OrderedDate"] = "";

            ht.Add("ReportedBy", htCD4TLC["ReportedBy"]);
            if (ht["ReportedBy"] == null)
                ht["ReportedBy"] = "";

            ht.Add("ReportedDate", htCD4TLC["ReportedDate"]); ;
            if (ht["ReportedDate"] == null)
                ht["ReportedDate"] = "";

            if (chkDelivered.Checked == true)
                ht.Add("Delivered", "1");
            else
                ht.Add("Delivered", "");

            ht.Add("DOB", txtDOB.Value);

            //---- 266=CD; 268=TLC
            if (ddlWhyEligible.SelectedValue.ToString() != "266" && ddlWhyEligible.SelectedValue.ToString() != "268")
            {
                ht["OrderedBy"] = "";
                ht["OrderedDate"] = "";
                ht["ReportedBy"] = "";
                ht["ReportedDate"] = "";
            }

            PatientManager = (IPatientRecord)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRecord, BusinessProcess.Clinical");

            if (arrARVReason4.GetValue(0, 0) == null)
            {
                arrARVReason4[0, 0] = "";
                arrARVReason4[0, 1] = "";
            }

            if (Convert.ToInt32(ViewState["DQ"]) == 1)
                ht.Add("DQ", "1");
            else
                ht.Add("DQ", "0");

            //--- custom field----
            DataTable theCustomDataDT = new DataTable();
            if (Request.QueryString["name"] == "Add")
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Insert", ApplicationAccess.PatientRecord, (DataSet)ViewState["CustomFieldsDS"]);

                //if (ViewState["ControlCreated"] != null)
                //{
                //    strCustomField = InsertCustomFieldsValuesString(patientId);
                //    ViewState["CustomFieldsData"] = 1;
                //}
            }
            else if (Request.QueryString["name"] == "Edit")
            {
                CustomFieldClinical theCustomManager = new CustomFieldClinical();
                theCustomDataDT = theCustomManager.GenerateInsertUpdateStatement(pnlCustomList, "Update", ApplicationAccess.PatientRecord, (DataSet)ViewState["CustomFieldsDS"]);
                //if (ViewState["ControlCreated"] != null)
                //{
                //    if (ViewState["CustomFieldsData"] == null && ViewState["CustomFieldsMulti"] == null)
                //    {
                //        strCustomField = InsertCustomFieldsValuesString(patientId);
                //        ViewState["CustomFieldsData"] = 1;
                //    }
                //    else
                //    {
                //        strCustomField = UpdateCustomFieldsValuesString();
                //    }
                //}

            }

            int ret;
            DataTable dt = new DataTable();
            dt = PatientManager.SavePatientRecord(ht, arrIllness, arrReferredTo, arrAdhReason, arrARVReason4, theCustomDataDT);
            ret = Convert.ToInt32(dt.Rows[0][0]);

            ViewState["PtnID"] = Convert.ToInt32(Request.QueryString["PatientId"]);
            ViewState["VisitId"] = ret;

            if (rdoAdhStatusGood.Checked == true || rdoAdhStatusNotDoc.Checked == true || rdoAdhStatusPoor.Checked == false)
                ViewState["AdhStaus"] = "Checked";//used for DQ
            else
                ViewState["AdhStaus"] = "NotChecked";

            Array.Clear(arrAdhReason, 0, arrAdhReason.Length);
            Array.Clear(arrARVReason4, 0, arrARVReason4.Length);
            Array.Clear(arrReferredTo, 0, arrReferredTo.Length);
            Array.Clear(arrIllness, 0, arrIllness.Length);

        }

        private string InsertCustomFieldsValuesString(int patientId)
        {
            string sqlstr = string.Empty;
            string sqlselect;
            try
            {
                GenerateCustomFieldsValues(pnlCustomList);

                Int32 visitID = 0;
                string visitdate = string.Empty;

                visitID = 77777;
                visitdate = "1900-01-01";

                if (sbValues.ToString().Trim() != "")
                {

                    sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
                    sqlstr += " VALUES(" + patientId.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";

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
                                            sqlselect += " VALUES (" + patientId.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";

                                            if (sqlstr == "")
                                            {
                                                sqlstr = sqlselect;
                                            }
                                            else
                                            {
                                                sqlstr = sqlstr + "!" + sqlselect;
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
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
            return sqlstr;
        }

        private void InsertCustomFieldsValues(int patientId)
        {
            GenerateCustomFieldsValues(pnlCustomList);
            string sqlstr = string.Empty;
            string sqlselect;
            Int32 visitID = 0;
            DateTime visitdate = System.DateTime.Now;
            ICustomFields CustomFields;

            try
            {
                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                DataSet dsVisit = CustomFields.GetVisit(Convert.ToInt32(ViewState["VisitId"].ToString()));
                if (dsVisit != null && dsVisit.Tables[0].Rows.Count > 0)
                {
                    visitID = Convert.ToInt32(dsVisit.Tables[0].Rows[0]["Visit_ID"].ToString());
                    visitdate = Convert.ToDateTime(dsVisit.Tables[0].Rows[0]["VisitDate"].ToString());
                }
            }
            catch
            {
            }
            finally
            {
                CustomFields = null;
            }

            if (sbValues.ToString().Trim() != "")
            {

                sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
                sqlstr += " VALUES(" + patientId.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'" + sbValues.ToString() + ")";

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
                                        sqlselect += " VALUES (" + patientId.ToString() + "," + Session["AppLocationID"] + "," + visitID + ",'" + visitdate + "'," + str.ToString() + ")";
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
                                sbValues.Append(",Null");
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

        private string UpdateCustomFieldsValuesString()
        {
            string sqlstr = string.Empty;


            try
            {
                GenerateCustomFieldsValues(pnlCustomList);
                int patientId = Convert.ToInt32(Request.QueryString["PatientId"]);

                DateTime visitdate = System.DateTime.Now;
                string sqlselect;
                string strdelete;
                ICustomFields CustomFields;
                if (sbValues.ToString().Trim() != "")
                {
                    if (ViewState["CustomFieldsData"] != null)
                    {


                        sbValues = sbValues.Remove(0, 1);
                        sqlstr = "UPDATE dtl_CustomField_" + TableName.ToString().Replace("-", "_") + " SET ";
                        sqlstr += sbValues.ToString() + " where Ptn_pk= " + patientId;
                        sqlstr += "and Visit_pk=" + Convert.ToInt32(Request.QueryString["VisitId"]);
                    }
                    else
                    {
                        sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
                        sqlstr += " VALUES(" + ViewState["PtnID"].ToString() + "," + Session["AppLocationID"] + "," + Convert.ToInt32(Request.QueryString["VisitId"]) + ",'" + Convert.ToDateTime(ViewState["VisitDate"].ToString()) + "'" + sbValues.ToString() + ")";
                        ViewState["CustomFieldsData"] = 1;
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

                                CustomFields = (ICustomFields)ObjectFactory.CreateInstance("BusinessProcess.Administration.BCustomFields, BusinessProcess.Administration");
                                strdelete = "DELETE from [" + obj.ToString() + "] where ptn_pk= " + patientId;

                                if (sqlstr == "")
                                {
                                    sqlstr = strdelete;
                                }
                                else
                                {
                                    sqlstr = sqlstr + "!" + strdelete;
                                }

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
                                            sqlselect += " VALUES (" + patientId + "," + Session["AppLocationID"] + "," + Convert.ToInt32(Request.QueryString["VisitId"]) + ",'" + visitdate + "'," + str.ToString() + ")";

                                            if (sqlstr == "")
                                            {
                                                sqlstr = sqlselect;
                                            }
                                            else
                                            {
                                                sqlstr = sqlstr + "!" + sqlselect;
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

            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }

            return sqlstr;
        }

        private void UpdateCustomFieldsValues()
        {
            GenerateCustomFieldsValues(pnlCustomList);
            int patientId = Convert.ToInt32(Request.QueryString["PatientId"]);
            string sqlstr = string.Empty;
            DateTime visitdate = System.DateTime.Now;
            string sqlselect;
            string strdelete;
            ICustomFields CustomFields;
            if (sbValues.ToString().Trim() != "")
            {
                if (ViewState["CustomFieldsData"] != null)
                {
                    sbValues = sbValues.Remove(0, 1);
                    sqlstr = "UPDATE dtl_CustomField_" + TableName.ToString().Replace("-", "_") + " SET ";
                    sqlstr += sbValues.ToString() + " where Ptn_pk= " + patientId;
                    sqlstr += "and Visit_pk=" + Convert.ToInt32(ViewState["VisitId"].ToString());
                }
                else
                {
                    sqlstr = "INSERT INTO dtl_CustomField_" + TableName.ToString().Replace("-", "_") + "(ptn_pk,LocationID,Visit_pk,Visit_Date " + sbParameter.ToString() + " )";
                    sqlstr += " VALUES(" + ViewState["PtnID"].ToString() + "," + Session["AppLocationID"] + "," + Convert.ToInt32(ViewState["VisitId"].ToString()) + ",'" + Convert.ToDateTime(ViewState["VisitDate"].ToString()) + "'" + sbValues.ToString() + ")";
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
                                strdelete = "DELETE from [" + obj.ToString() + "] where ptn_pk= " + patientId;
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

                                            sqlselect = "INSERT INTO [" + obj.ToString() + "](ptn_pk,LocationID,Visit_pk,Visit_Date, [" + obj.ToString().Substring(ispos, iepos) + "])";
                                            sqlselect += " VALUES (" + patientId + "," + Session["AppLocationID"] + "," + ViewState["VisitId"] + ",'" + visitdate + "'," + str.ToString() + ")";

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

        private void SaveCancel()
        {
            //--- runs when Cancel button is clicked, after saving the form ---

            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Patient Record Form saved successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            if (Request.QueryString["name"] == "Add")
                script += "window.location.href='frmPatient_Home.aspx?PatientId=" + ViewState["PtnID"].ToString() + "';\n";
            else
                script += "window.location.href='../ClinicalForms/frmPatient_History.aspx?PatientId=" + Request.QueryString["PatientId"] + "&sts=" + Request.QueryString["sts"] + "';\n";
            script += "}\n";
            script += "else \n";
            script += "{\n";
            script += "window.location.href='frmClinical_PatientRecordCTC.aspx?name=Edit&PatientId=" + ViewState["PtnID"].ToString() + "&sts=" + 0 + "&visitid=" + ViewState["VisitId"].ToString() + "';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        protected void GetChkdARVReason4()
        {
            // --- get the selected ARV Reason checkbox

            try
            {
                //---4= change---
                int i = 0;
                foreach (HtmlTableRow r in tblARVReason2.Rows)
                {
                    foreach (HtmlTableCell c in r.Cells)
                    {
                        foreach (Control ctrl in c.Controls)
                        {
                            if (ctrl.GetType() == typeof(HtmlInputCheckBox))
                            {
                                if (((HtmlInputCheckBox)ctrl).Checked == true)
                                {
                                    arrARVReason4[i, 0] = ctrl.ID.Substring(9, ctrl.ID.Length - 9);
                                    arrARVReason4[i, 1] = "";
                                    i++;
                                }
                            }

                        }
                    }
                }

                //--- for other -1
                foreach (HtmlTableRow r in tblARVReason2.Rows)
                {
                    foreach (HtmlTableCell c in r.Cells)
                    {
                        foreach (Control ctrl in c.Controls)
                        {
                            if ((ctrl.GetType() == typeof(HtmlInputText)) && (ctrl.ID == "txtotherARVReason1") && (((HtmlInputText)ctrl).Value != ""))
                            {
                                for (i = 0; i < arrARVReason4.Length / 2; i++)
                                {
                                    if (arrARVReason4.GetValue(i, 0) != null)
                                    {
                                        if (arrARVReason4.GetValue(i, 0).ToString() == "62")  //ID of adverse reaction-other
                                        {
                                            arrARVReason4[i, 1] = ((HtmlInputText)ctrl).Value;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                //---- for other -2
                foreach (HtmlTableRow r in tblARVReason2.Rows)
                {
                    foreach (HtmlTableCell c in r.Cells)
                    {
                        foreach (Control ctrl in c.Controls)
                        {
                            if ((ctrl.GetType() == typeof(HtmlInputText)) && (ctrl.ID == "txtotherARVReason2") && (((HtmlInputText)ctrl).Value != ""))
                            {
                                for (i = 0; i < arrARVReason4.Length / 2; i++)
                                {
                                    if (arrARVReason4.GetValue(i, 0) != null)
                                    {
                                        if ((arrARVReason4.GetValue(i, 0).ToString() == "69")) //ID of adverse reaction-other
                                        {
                                            arrARVReason4[i, 1] = ((HtmlInputText)ctrl).Value;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        protected void GetChkdAdhReason()
        {
            //--- get selected Adherence Reason checkbox 
            try
            {
                int i = 0;
                foreach (HtmlTableRow r in tbARVAdhReason.Rows)
                {
                    foreach (HtmlTableCell c in r.Cells)
                    {
                        foreach (Control ctrl in c.Controls)
                        {
                            if (ctrl.GetType() == typeof(HtmlInputCheckBox))
                            {
                                if (((HtmlInputCheckBox)ctrl).Checked == true)
                                {
                                    arrAdhReason[i, 0] = ctrl.ID.Substring(12, ctrl.ID.Length - 12);
                                    arrAdhReason[i, 1] = "";
                                    i++;
                                }
                            }
                            //--- for other---
                            if ((ctrl.GetType() == typeof(HtmlInputText)))
                            {
                                if (ctrl.ID == "txtotherARVAdh")
                                {
                                    if (i > 0)
                                        i--;
                                    if (((HtmlInputText)ctrl).Value != "")
                                    {
                                        arrAdhReason[i, 1] = ((HtmlInputText)ctrl).Value;
                                        i++;
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        protected void GetChkdIllness()
        {
            //--- get selected Illness checkbox ---
            try
            {
                int i = 0;
                foreach (HtmlTableRow r in tblIllness1.Rows)
                {
                    foreach (HtmlTableCell c in r.Cells)
                    {
                        foreach (Control ctrl in c.Controls)
                        {
                            if (ctrl.GetType() == typeof(HtmlInputCheckBox))
                            {
                                if (((HtmlInputCheckBox)ctrl).Checked == true)
                                {
                                    arrIllness[i] = ctrl.ID.Substring(8, ctrl.ID.Length - 8);
                                    i++;
                                }
                            }
                        }
                    }
                }

                foreach (HtmlTableRow r in tblIllness2.Rows)
                {
                    foreach (HtmlTableCell c in r.Cells)
                    {
                        foreach (Control ctrl in c.Controls)
                        {
                            if (ctrl.GetType() == typeof(HtmlInputCheckBox))
                            {
                                if (((HtmlInputCheckBox)ctrl).Checked == true)
                                {
                                    arrIllness[i] = ctrl.ID.Substring(8, ctrl.ID.Length - 8);
                                    i++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        protected void GetChkdReferredTo()
        {
            //--- get selected ReferredTo checkbox ---
            try
            {
                int i = 0;
                foreach (HtmlTableRow r in tblRefer.Rows)
                {
                    foreach (HtmlTableCell c in r.Cells)
                    {
                        foreach (Control ctrl in c.Controls)
                        {
                            if (ctrl.GetType() == typeof(HtmlInputCheckBox))
                            {
                                if (((HtmlInputCheckBox)ctrl).Checked == true)
                                {
                                    string str = ((HtmlInputCheckBox)ctrl.FindControl(ctrl.ID)).Value.ToString();
                                    arrReferredTo[i, 0] = ctrl.ID.Substring(7, ctrl.ID.Length - 7);
                                    //if (str == "Other")
                                    if (str == "Other (specify)")
                                        arrReferredTo[i, 1] = ((HtmlInputText)ctrl.FindControl("txtReferOther")).Value;
                                    else
                                        arrReferredTo[i, 1] = "";

                                    i++;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        protected void btnCD4TLC_OnClick(object sender, EventArgs e)
        {
            //--- runs when btnCD4TLC is clicked ---
            try
            {
                if (Request.QueryString["Name"] == "Add" && txtvisitDate.Value == "")
                {
                    IQCareMsgBox.Show("EnterVisitDate", this);
                    return;
                }

                if (ddlWhyEligible.SelectedValue.ToString() == "0")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "WhyEligible1", "funShowWhyEligible();", true);
                    IQCareMsgBox.Show("SelectWhyEligible", this);

                    if (chkDelivered.Checked == true)
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "chkDeliver1", "document.getElementById('divChkDelivered').style.display='inline';", true);

                    return;
                }

                if (Session["htCD4TLC"].ToString() == "")
                {
                    Hashtable htCD4TLC = new Hashtable();
                    htCD4TLC.Add("Mode", Request.QueryString["Name"]);
                    htCD4TLC.Add("LocationID", Convert.ToInt32(Session["AppLocationID"]));
                    htCD4TLC.Add("UserID", Convert.ToInt32(Session["AppUserId"]));
                    htCD4TLC.Add("Ptn_Pk", Convert.ToInt32(Request.QueryString["PatientId"]));
                    htCD4TLC.Add("VisitId", Convert.ToInt32(Request.QueryString["VisitId"]));
                    htCD4TLC.Add("VisitDate", txtvisitDate.Value);
                    PatientManager = (IPatientRecord)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRecord, BusinessProcess.Clinical");
                    DataSet theDS = (DataSet)PatientManager.GetCD4TLC(htCD4TLC);

                    if (theDS.Tables[0].Rows.Count > 0)
                    {
                        if (theDS.Tables[0].Rows[0]["LABID"] != System.DBNull.Value)
                            htCD4TLC["LABID"] = theDS.Tables[0].Rows[0]["LABID"].ToString();

                        if (theDS.Tables[0].Rows[0]["TestResults"] != System.DBNull.Value)
                            htCD4TLC["CD4"] = theDS.Tables[0].Rows[0]["TestResults"].ToString();


                    }
                    if (theDS.Tables[1].Rows.Count > 0)
                    {
                        if (theDS.Tables[1].Rows[0]["TestResults"] != System.DBNull.Value)
                            htCD4TLC["CD4Percent"] = theDS.Tables[1].Rows[0]["TestResults"].ToString();
                    }

                    if (theDS.Tables[2].Rows.Count > 0)
                    {
                        if (theDS.Tables[2].Rows[0]["TLC"] != System.DBNull.Value)
                            htCD4TLC["TLC"] = theDS.Tables[2].Rows[0]["TLC"].ToString();

                        if (theDS.Tables[2].Rows[0]["TLCPercent"] != System.DBNull.Value)
                            htCD4TLC["TLCPercent"] = theDS.Tables[2].Rows[0]["TLCPercent"].ToString();
                    }

                    if (theDS.Tables[0].Rows.Count > 0)
                    {
                        if (theDS.Tables[0].Rows[0]["OrderedBy"] != System.DBNull.Value)
                            htCD4TLC["OrderedBy"] = theDS.Tables[0].Rows[0]["OrderedBy"].ToString();
                        if (theDS.Tables[0].Rows[0]["OrderedDate"] != System.DBNull.Value)
                            htCD4TLC["OrderedDate"] = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["OrderedDate"]);
                        if (theDS.Tables[0].Rows[0]["ReportedBy"] != System.DBNull.Value)
                            htCD4TLC["ReportedBy"] = theDS.Tables[0].Rows[0]["ReportedBy"].ToString();
                        if (theDS.Tables[0].Rows[0]["ReportedDate"] != System.DBNull.Value)
                            htCD4TLC["ReportedDate"] = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[0].Rows[0]["ReportedDate"]);
                    }

                    if (theDS.Tables[1].Rows.Count > 0)
                    {
                        if (theDS.Tables[1].Rows[0]["OrderedBy"] != System.DBNull.Value)
                        {
                            htCD4TLC["OrderedBy"] = theDS.Tables[1].Rows[0]["OrderedBy"].ToString();
                        }
                        if (theDS.Tables[1].Rows[0]["OrderedDate"] != System.DBNull.Value)
                        {
                            htCD4TLC["OrderedDate"] = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[1].Rows[0]["OrderedDate"]);
                        }
                        if (theDS.Tables[1].Rows[0]["ReportedBy"] != System.DBNull.Value)
                        {
                            htCD4TLC["ReportedBy"] = theDS.Tables[1].Rows[0]["ReportedBy"].ToString();
                        }
                        if (theDS.Tables[1].Rows[0]["ReportedDate"] != System.DBNull.Value)
                        {
                            htCD4TLC["ReportedDate"] = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[1].Rows[0]["ReportedDate"]);
                        }
                    }

                    if (theDS.Tables[3].Rows.Count > 0)
                    {
                        if (theDS.Tables[3].Rows[0]["OrderedBy"] != System.DBNull.Value)
                        {
                            htCD4TLC["OrderedBy"] = theDS.Tables[3].Rows[0]["OrderedBy"].ToString();
                        }
                        if (theDS.Tables[3].Rows[0]["OrderedDate"] != System.DBNull.Value)
                        {
                            htCD4TLC["OrderedDate"] = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[3].Rows[0]["OrderedDate"]);
                        }
                        if (theDS.Tables[3].Rows[0]["ReportedBy"] != System.DBNull.Value)
                        {
                            htCD4TLC["ReportedBy"] = theDS.Tables[3].Rows[0]["ReportedBy"].ToString();
                        }
                        if (theDS.Tables[3].Rows[0]["ReportedDate"] != System.DBNull.Value)
                        {
                            htCD4TLC["ReportedDate"] = String.Format("{0:dd-MMM-yyyy}", theDS.Tables[3].Rows[0]["ReportedDate"]);
                        }
                    }

                    Session["htCD4TLC"] = htCD4TLC;
                    Session["VisitDate"] = txtvisitDate.Value;
                }

                Session["showDivCD4"] = "YES";
                Session["WhyEligible"] = ddlWhyEligible.SelectedItem.ToString();
                if (ddlWhyEligible.SelectedItem.ToString() == "CD4 count/%" || ddlWhyEligible.SelectedItem.ToString() == "TLC - Total Lymphocyte Count")
                {
                    string theScript;
                    theScript = "<script language='javascript' id='CD4TLCPopup'>\n";
                    theScript += "window.open('frmClinical_PatientRecordCD4.aspx','','toolbars=no,location=no,directories=no,dependent=yes,top=1,left=1,maximize=no,resize=no,width=770,height=270');\n";
                    theScript += "</script>\n";
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PatRecord", theScript);
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
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
                    else
                    {
                        string theUrl;
                        theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmClinical_DeleteForm.aspx", Request.QueryString["PatientId"].ToString(), Session["Status"].ToString());

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
                    else if (Request.QueryString["name"] == "Edit")
                    {
                        string theUrl;
                        theUrl = string.Format("{0}?PatientId={1}&sts={2}", "frmPatient_History.aspx", Request.QueryString["PatientId"].ToString(), Request.QueryString["sts"].ToString());
                        Response.Redirect(theUrl);
                    }

                    else
                    {
                        string theUrl;
                        theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmClinical_DeleteForm.aspx", Request.QueryString["PatientId"].ToString(), Session["Status"].ToString());

                    }
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private string DQ_Msg()
        {
            //--- Data Quality checks / messages  ---
            string strmsg = "Following values are required to complete the data quality check:\\n\\n";

            if (txtvisitDate.Value == "")
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'VisitDate'>\n";
                script += "To_Change_Color('lblVisitDate');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "VisitDate", script);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -Visit Date";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }

            if (txtWeight.Value == "")
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'Weight'>\n";
                script += "To_Change_Color('lblWeight');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "Weight", script);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -Weight";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }

            if (ddlWHOStage.SelectedValue.ToString() == "0")
            {

                string script = "<script language = 'javascript' defer ='defer' id = 'WHOStage'>\n";
                script += "To_Change_Color('lblWHOStage');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "WHOStage", script);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -WHO Stage";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }

            if (ddlFuncStatus.SelectedValue.ToString() == "0")
            {

                string script = "<script language = 'javascript' defer ='defer' id = 'FuncStatus'>\n";
                script += "To_Change_Color('lblFuncStatus');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "FuncStatus", script);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -Functional Status";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }

            if (ddlTBStatus.SelectedValue.ToString() == "0")
            {

                string script = "<script language = 'javascript' defer ='defer' id = 'TBStatus'>\n";
                script += "To_Change_Color('lblTBStatus');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "TBStatus", script);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -TB Status";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }

            if (ddlARVStatus.SelectedValue.ToString() == "0")
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'ARVStatus'>\n";
                script += "To_Change_Color('lblARVStatus');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ARVStatus", script);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -ARV Status";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }

            if (ddlARVStatus.SelectedValue.ToString() == "1" && ddlARVReason.SelectedValue.ToString() == "0")
            {
                string script = "<script language = 'javascript' defer ='defer' id = 'ARVReason'>\n";
                script += "To_Change_Color('lblARVReason');\n";
                script += "</script>\n";
                ClientScript.RegisterStartupScript(this.GetType(), "ARVReason", script);
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = " -ARV Reason";
                strmsg += IQCareMsgBox.GetMessage("BlankTextBox", theBuilder, this);
                strmsg += "\\n";
            }
            return strmsg;
        }

        protected void btnDQ_Click(object sender, EventArgs e)
        {
            //--- run Data Quality button is clicked ---
            try
            {
                string msg = DQ_Msg(); // used from showing messages for Data Quality
                ClientScripts();
                if (msg.Length > 69)
                {
                    MsgBuilder theBuilder1 = new MsgBuilder();
                    theBuilder1.DataElements["MessageText"] = msg;
                    IQCareMsgBox.Show("#C1", theBuilder1, this);
                    return;
                }
                if (Validation() == true)
                {
                    int patientId = Convert.ToInt32(Request.QueryString["PatientId"]);
                    ViewState["DQ"] = 1;

                    MakeHTandSave();//Make hash table & save
                    SaveCancel();
                }
            }

            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private void ClearObjects()
        {
            Session.Remove("showDivCD4");
            Session.Remove("htCD4TLC");
            Session.Remove("whyEligible");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            // --- close the patient record form and redirect to appropriate page ---
            string theUrl = string.Empty;
            if (Request.QueryString["name"] == "Add")
                theUrl = string.Format("../ClinicalForms/frmPatient_Home.aspx?PatientId=" + Request.QueryString["PatientId"]);
            else
                theUrl = string.Format("{0}?PatientId={1}&sts={2}", "../ClinicalForms/frmPatient_History.aspx", Request.QueryString["PatientId"], Request.QueryString["sts"]);

            Response.Redirect(theUrl);
        }

        private void SetPageLabels()
        {
            DataTable theDT = ((DataSet)ViewState["FacilityDS"]).Tables[0];
            // lblFileRef.InnerHtml = theDT.Rows[0]["Label"].ToString();
        }


    }
}