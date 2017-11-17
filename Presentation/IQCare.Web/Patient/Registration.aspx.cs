using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Text;
using Application.Presentation;
using Interface.Clinical;
using Application.Common;
using System.Web.UI.HtmlControls;
using Interface.Security;
using System.Web.Services;
using System.Web.Script.Services;
using IQCare.Web.UILogic;
using Entities.FormBuilder;

namespace IQCare.Web.Patient
{
    public partial class Register : Page
    {
        DataSet theDSXML = new DataSet();
        string ObjFactoryParameter = "BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical";
        string ObjFactoryParameterBCustom = "BusinessProcess.Clinical.BCustomForm, BusinessProcess.Clinical";
        int FeatureId = 126, patientId = 0, VisitID = 0, LocationId = 0;
        bool theConditional;
        Hashtable htParameters;
         struct RegistrationParameter
        {
            /// <summary>
            /// The name
            /// </summary>
            public string Name;
            /// <summary>
            /// The value
            /// </summary>
            public object Value;
            /// <summary>
            /// The data type
            /// </summary>
            public DbType DataType;
            /// <summary>
            /// The encrypt
            /// </summary>
            public bool Encrypt;
            /// <summary>
            /// The destination
            /// </summary>
            public string Destination;
        }
        bool rdoTrueFalseStatus = true;
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (patientId > 0)
            {
                btncontinue.Enabled = CurrentSession.Current.HasFunctionRight("PATIENT_REGISTRATION", FunctionAccess.Add);
            }
            else
            {
                btncontinue.Enabled = CurrentSession.Current.HasFunctionRight("PATIENT_REGISTRATION", FunctionAccess.Update);
            }
            if (this.IsPostBack == true)
            {
                if (HdnCntrl.Value != "")
                {
                    string[] theCntrl = HdnCntrl.Value.Split('%');
                    CheckControl(PnlDynamicElements, theCntrl);
                    HdnCntrl.Value = "";
                }
                foreach (Control x in PnlDynamicElements.Controls)
                {
                    if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                    {
                        DropDownList theDList = (DropDownList)x;
                        if (theDList.AutoPostBack == true)
                        {
                            EventArgs s = new EventArgs();
                            ddlSelectList_SelectedIndexChanged(x, s);
                        }
                    }
                    else if (x.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                    {
                        CheckBox chklst = (CheckBox)x;
                        if (chklst.AutoPostBack == true)
                        {
                            EventArgs s = new EventArgs();
                            ddlSelectList_SelectedIndexChanged(x, s);
                        }
                    }
                }
            }
            /////HTML Controls PostBack//////
            ConFieldEnableDisable(PnlDynamicElements);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            (Master.FindControl("levelTwoNavigationUserControl1").FindControl("PanelPatiInfo") as Panel).Visible = false;
            // Ajax.Utility.RegisterTypeForAjax(typeof(frmPatientCustomRegistration));
            Attributes();
            Master.ExecutePatientLevel = false;
            theDSXML.ReadXml(MapPath("~/XMLFiles/AllMasters.con"));
            if (!IsPostBack)
            {
                Binddropdown();
                txtRegDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            }
            patientId = Convert.ToInt32(Session["PatientId"]);
            LocationId = Convert.ToInt32(Session["AppLocationId"]);
            VisitID = Convert.ToInt32(ViewState["VisitPk"]);
            LoadPredefinedLabel_Field(FeatureId);

            if (patientId > 0)
            {
                if (!IsPostBack)
                {
                    LoadPatientStaticData(patientId);
                    VisitID = Convert.ToInt32(ViewState["VisitPk"]);
                    BindValue(patientId, VisitID, LocationId, PnlDynamicElements);
                }

            }

        }

        //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        [WebMethod(EnableSession = true), ScriptMethod]
        public static string GetDuplicateRecord(string strfname, string strmname, string strlname, string address, string Phone)
        {
           // IPatientRegistration PatientManager;
            StringBuilder objBilder = new StringBuilder();
           // PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");//ObjFactoryParameter);
           // DataSet dsPatient = new DataSet();
          //  dsPatient = PatientManager.GetDuplicatePatientSearchResults(strlname, strmname, strfname, address, Phone);

            string lName;
                lName = strlname.Trim();
                //  txtlastname.Text = lName.Replace("'", "''").ToString();
                string mName;
                mName = strmname.Trim();

                string fName;
                fName = strfname.Trim();

                if (lName != "" && fName != "")
                {

                    DataTable dt = PatientService.FindPatient(0, lName, mName, fName, "", "", "", null, null, 999, 10);

                    if (dt.Rows.Count > 0)
                    {
                        objBilder.Append("<table border='0'  width='100%'>");
                        objBilder.Append("<tr style='background-color:#e1e1e1'>");
                        //objBilder.Append("<td class='smallerlabel'>PatientID</td>");
                        objBilder.Append("<td class='smallerlabel'>Patient Id</td>");
                        objBilder.Append("<td class='smallerlabel'>F name</td>");
                        objBilder.Append("<td class='smallerlabel'>L name</td>");
                        objBilder.Append("<td class='smallerlabel'>Registration Date</td>");
                        objBilder.Append("<td class='smallerlabel'>Dob</td>");
                        objBilder.Append("<td class='smallerlabel'>Sex</td>");
                        objBilder.Append("<td class='smallerlabel'>Phone</td>");
                        objBilder.Append("<td class='smallerlabel'>Facility</td>");
                        objBilder.Append("</tr>");
                     //   DataTable dt = dsPatient.Tables[0];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objBilder.Append("<tr>");
                            //objBilder.Append("<td class='smallerlabel'>" + dsPatient.Tables[0].Rows[i]["PatientRegistrationID"].ToString() + "</td>");
                            objBilder.Append("<td class='smallerlabel'>" + dt.Rows[i]["PatientFacilityId"].ToString() + "</td>");
                            objBilder.Append("<td class='smallerlabel'>" + dt.Rows[i]["firstname"].ToString() + "</td>");
                            objBilder.Append("<td class='smallerlabel'>" + dt.Rows[i]["lastname"].ToString() + "</td>");
                            objBilder.Append("<td class='smallerlabel'>" + Convert.ToDateTime(dt.Rows[i]["RegistrationDate"]).ToString("dd-MMM-yyyy") + "</td>");
                            objBilder.Append("<td class='smallerlabel'>" + Convert.ToDateTime(dt.Rows[i]["dob"]).ToString("dd-MMM-yyyy") + "</td>");
                            objBilder.Append("<td class='smallerlabel'>" + dt.Rows[i]["sex"].ToString() + "</td>");
                            objBilder.Append("<td class='smallerlabel'>" + dt.Rows[i]["Phone"].ToString() + "</td>");
                            objBilder.Append("<td class='smallerlabel'>" + dt.Rows[i]["FacilityName"].ToString() + "</td>");
                            objBilder.Append("</tr>");
                        }
                        objBilder.Append("</table>");
                    }
                }
            return objBilder.ToString();
        }
        private void ConFieldEnableDisable(Control theControl)
        {
            foreach (Control x in theControl.Controls)
            {
                if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                {
                    ConFieldEnableDisable(x);
                }
                else
                {
                    if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                    {
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                            string[] theId = ((HtmlInputRadioButton)x).ID.Split('-');
                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                            if (theDVConditionalField.Count > 0)
                            {
                                EventArgs s = new EventArgs();
                                this.HtmlRadioButtonSelect(x);
                            }
                        }
                    }
                    if (x.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                    {
                        if (((CheckBox)x).Checked == true)
                        {
                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                            string[] theId = ((CheckBox)x).ID.Split('-');
                            if (theId.Length == 5)
                            {
                                theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                if (theDVConditionalField.Count > 0)
                                {
                                    EventArgs s = new EventArgs();
                                    this.HtmlCheckBoxSelect(x);

                                }
                            }
                        }
                    }
                    if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                    {
                        if (Convert.ToInt32(((DropDownList)x).SelectedIndex) > 0)
                        {
                            DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                            string[] theId = ((DropDownList)x).ID.Split('-');
                            theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                            if (theDVConditionalField.Count > 0)
                            {
                                EventArgs s = new EventArgs();
                                this.ddlSelectList_SelectedIndexChanged(x, s);
                            }
                        }
                    }
                }
            }
        }
        private void CheckControl(Control theCntrl, string[] theId)
        {
            string theCntrlType = theId[0];
            foreach (Control x in theCntrl.Controls)
            {
                if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                    CheckControl(x, theId);
                else if (x.ID == theId[1] && x.GetType().ToString() == theCntrlType && theCntrlType == "System.Web.UI.WebControls.CheckBox")
                {
                    HtmlCheckBoxSelect(x);
                    return;
                }
                else if (x.ID == theId[1] && x.GetType().ToString() == theCntrlType && theCntrlType == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                {
                    HtmlRadioButtonSelect(x);
                    return;
                }
            }
        }
        private void Attributes()
        {

            DateTime theCurrentDate = CurrentSession.SystemDate();
            txtSysDate.Text = theCurrentDate.ToString(CurrentSession.DateFormat);
            //txtlastName.Attributes.Add("onkeyup", "chkString('" + txtlastName.ClientID + "')");
            //txtfirstName.Attributes.Add("onkeyup", "chkString('" + txtfirstName.ClientID + "')");
            TxtDOB.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtRegDate.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
            txtRegDate.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + txtRegDate.ClientID + "', '" + txtRegDate.ClientID + "');");
            TxtDOB.Attributes.Add("onblur", "ValidateAge(); DateFormat(this,this.value,event,true,'3'); CalcualteAge('" + txtageCurrentYears.ClientID + "','" + txtageCurrentMonths.ClientID + "','" + TxtDOB.ClientID + "','" + txtSysDate.ClientID + "'); isCheckValidDate('" + Application["AppCurrentDate"] + "', '" + TxtDOB.ClientID + "', '" + TxtDOB.ClientID + "');");
            txtageCurrentYears.Attributes.Add("onkeyup", "chkNumeric('" + txtageCurrentYears.ClientID + "')");
            txtageCurrentMonths.Attributes.Add("onkeyup", "chkNumeric('" + txtageCurrentMonths.ClientID + "')");

        }
        private void HashTableParameter()
        {
            try
            {
                htParameters = new Hashtable();
                htParameters.Clear();
                htParameters.Add("FirstName", txtfirstName.Text.Trim());
                htParameters.Add("MiddleName", txtmiddleName.Text.Trim());
                htParameters.Add("LastName", txtlastName.Text.Trim());
                htParameters.Add("Gender", ddgender.SelectedValue);
                htParameters.Add("DOB", TxtDOB.Text);
                htParameters.Add("RegistrationDate", txtRegDate.Text);
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

            }
        }
        private void RenderLabel(string labelText, string fieldId,string column, string smallLabel="")
        {
           // PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"col-md-4\" style='text-align:right'>"));
          
            if (SetBusinessrule(fieldId, column) == true)
            {
                PnlDynamicElements.Controls.Add(new LiteralControl("<label class='control-label pull-left required' style='padding-right:0;margin-right:0'  id='lbl" + labelText + "-" + fieldId + "' >" + labelText + " :</label>"));
            }
            else
            {
                PnlDynamicElements.Controls.Add(new LiteralControl("<label class='control-label pull-left' style='padding-right:0;margin-right:0' id='lbl" + labelText + "-" + fieldId + "'>" + labelText + " </label>"));
            }
            if (smallLabel != "")
            {
                PnlDynamicElements.Controls.Add(new LiteralControl("<small class=\"pull-left text-muted\">[" + smallLabel + "]</small>"));
            }
           // PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
        }
        private void LoadFieldTypeControl(string controlId, string column, string fieldId, string codeId, string label, string table, string bindSource, bool theEnable)
        {
            try
            {
                DataTable theBusinessRuleDT = (DataTable)ViewState["BusRule"];
                DataView theBusinessRuleDV = new DataView(theBusinessRuleDT);
                DataView theAutoDV = new DataView();
                theBusinessRuleDV.RowFilter = "BusRuleId=17 and FieldId = " + fieldId.ToString();
                PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"form-group\">"));
                if (controlId == "1") ///SingleLine Text Box
                {
                   // PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%'>"));
                  
                    //PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                 
                   /*PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));                    
                    if (SetBusinessrule(FieldId, Column) == true)
                    {
                        PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "' >" + Label + " :</label>"));
                    }
                    else
                    {
                        PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    }
                    PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                    */
                    this.RenderLabel(label, fieldId, column);

                    //PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                   
                    TextBox theSingleText = new TextBox();
                    theSingleText.ID = "TXT-" + column + "-" + table + "-" + fieldId;

                    if (hdnIds.Value == "")
                    {
                        hdnIds.Value = theSingleText.ID;
                    }
                    else
                    {
                        hdnIds.Value = hdnIds.Value + "," + theSingleText.ID;
                    }

                  //  theSingleText.Width = 180;
                    theSingleText.CssClass = "form-control input-sm";
                    theSingleText.MaxLength = 50;
                    theSingleText.Enabled = theEnable;
                    PnlDynamicElements.Controls.Add(theSingleText);
                    ApplyBusinessRules(theSingleText, controlId, theEnable);
                   // PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                  //  PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
                   

                }
                else if (controlId == "2") ///DecimalTextBox
                {

                   // PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%'>"));
                   // PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                   // PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                   // if (SetBusinessrule(FieldId, Column) == true)
                   // {
                    //    PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                   // }
                   // else
                   // {
                   //     PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                   // }
                   // PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                   // PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                    this.RenderLabel(label, fieldId, column);
                    TextBox theSingleDecimalText = new TextBox();
                    theSingleDecimalText.ID = "TXT-" + column + "-" + table + "-" + fieldId;
                    if (hdnIds.Value == "")
                    {
                        hdnIds.Value = theSingleDecimalText.ID;
                    }
                    else
                    {
                        hdnIds.Value = hdnIds.Value + "," + theSingleDecimalText.ID;
                    }
                    theSingleDecimalText.CssClass = "form-control input-sm";
                   // theSingleDecimalText.Width = 180;
                    theSingleDecimalText.MaxLength = 50;
                    theSingleDecimalText.Enabled = theEnable;
                    PnlDynamicElements.Controls.Add(theSingleDecimalText);
                    ApplyBusinessRules(theSingleDecimalText, controlId, theEnable);
                    theSingleDecimalText.Attributes.Add("onkeyup", "chkNumeric('" + theSingleDecimalText.ClientID + "')");
                    //PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                   // PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                  //  PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));

                }
                else if (controlId == "3")   /// Numeric (Integer)
                {
                    //PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%'>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    //if (SetBusinessrule(FieldId, Column) == true)
                    //{
                    //    PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    //}
                    //else
                    //{
                    //    PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    //}
                    //PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                    this.RenderLabel(label, fieldId, column);
                    TextBox theNumberText = new TextBox();
                    theNumberText.ID = "TXTNUM-" + column + "-" + table + "-" + fieldId;
                    //theNumberText.Width = 100;
                    theNumberText.CssClass = "form-control input-sm";
                    theNumberText.MaxLength = 9;
                    theNumberText.Enabled = theEnable;
                    PnlDynamicElements.Controls.Add(theNumberText);
                    theNumberText.Attributes.Add("onkeyup", "chkInteger('" + theNumberText.ClientID + "')");
                    ApplyBusinessRules(theNumberText, controlId, theEnable);
                    //PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
                }

                else if (controlId == "4") /// Dropdown
                {
                    bool theCntrlPresent = false;
                    if (theCntrlPresent != true)
                    {

                       // PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%'>"));
                       // PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                       // PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                       //DropDownList ddlSelectListAuto = new DropDownList();
                       // if (SetBusinessrule(FieldId, Column) == true)
                       // {
                       //     PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                       // }
                       // else
                       // {
                       //     PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                       // }
                       // PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                       // PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                        this.RenderLabel(label, fieldId, column);
                        DropDownList ddlSelectList = new DropDownList();
                        ddlSelectList.ID = "SELECTLIST-" + column + "-" + table + "-" + fieldId;
                        if (codeId == "")
                        {
                            codeId = "0";
                        }
                        DataView theDV = new DataView(theDSXML.Tables[bindSource]);
                        if (VisitID == 0)
                        {
                            if (bindSource.ToUpper() == "MST_DISTRICT" || bindSource.ToUpper() == "MST_VILLAGE" || bindSource.ToUpper() == "MST_TOWN" || bindSource.ToUpper() == "MST_COUNTRY" || bindSource.ToUpper() == "MST_PROVINCE" || bindSource.ToUpper() == "MST_WARD")
                            {
                                theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
                            }
                            else if (bindSource.ToUpper() == "MST_DECODE")
                            {
                                DataView theDV4 = new DataView(((DataSet)Session["AllData"]).Tables[1]);
                                theDV4.RowFilter = "FieldId=" + fieldId + "";
                                DataTable theDT4 = theDV4.ToTable();
                                if (theDT4.Rows.Count > 0)
                                {
                                    theDV.RowFilter = "(ModuleId IN(0, " + theDT4.Rows[0]["ModuleId"] + ") or ModuleId Is null) and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + codeId + "";
                                }
                            }
                            else if (bindSource.ToUpper() == "MST_PMTCTDECODE" || bindSource.ToUpper() == "MST_MODDECODE")
                            { theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + codeId + ""; }
                            else
                            {
                                theDV.RowFilter = "DeleteFlag=0";
                            }
                        }
                        else
                        {
                            if (bindSource.ToUpper() == "MST_DISTRICT" || bindSource.ToUpper() == "MST_VILLAGE" || bindSource.ToUpper() == "MST_TOWN" || bindSource.ToUpper() == "MST_COUNTRY" || bindSource.ToUpper() == "MST_PROVINCE" || bindSource.ToUpper() == "MST_WARD")
                            {
                                theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ")";
                            }
                            else if (bindSource.ToUpper() == "MST_DECODE")
                            {
                                DataView theDV4 = new DataView(((DataSet)Session["AllData"]).Tables[1]);
                                theDV4.RowFilter = "FieldId=" + fieldId + "";
                                DataTable theDT4 = theDV4.ToTable();
                                if (theDT4.Rows.Count > 0)
                                {
                                    theDV.RowFilter = "(ModuleId IN(0, " + theDT4.Rows[0]["ModuleId"] + ") or ModuleId Is null) and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + codeId + "";
                                }
                            }
                            else if (bindSource.ToUpper() == "MST_PMTCTDECODE" || bindSource.ToUpper() == "MST_MODDECODE")
                            {
                                theDV.RowFilter = "SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and CodeID=" + codeId + "";
                            }
                        }

                        //if (theDV.Table != null)
                        //{
                        IQCareUtils theUtils = new IQCareUtils();
                        BindFunctions BindManager = new BindFunctions();
                        DataTable theDT = (DataTable)theUtils.CreateTableFromDataView(theDV);
                        //DataView theDVCon = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                        //theDVCon.RowFilter = "FieldId=" + FieldId + "";
                        //DataTable theDTFilter = theDVCon.ToTable();
                        //DataView theDVSelect = new DataView(theDT);
                        //if (BindSource.ToUpper() == "MST_DECODE")
                        //{
                        //    theDVSelect.RowFilter = "(ModuleId IN(" + theDTFilter.Rows[0]["ModuleId"] + ") and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + "))";
                        //}
                        ////DataTable theDTCon = theDVSelect.ToTable();
                        DataTable theDTCon = theDT.Copy();
                        if (theDTCon.Rows.Count > 0)
                        {
                           // BindManager.BindCombo(ddlSelectListAuto, theDTCon, "Name", "ID");
                            BindManager.BindCombo(ddlSelectList, theDTCon, "Name", "ID");
                        }
                        if (theDTCon.Rows.Count == 0)
                        {
                            ListItem theItem = new ListItem();
                            theItem.Text = "Select";
                            theItem.Value = "0";
                            ddlSelectList.Items.Add(theItem);
                        }
                        //}
                        else
                        {
                            /* ListItem theItem1 = new ListItem();
                             theItem1.Text = "Select";
                             theItem1.Value = "0";
                             ddlSelectList.Items.Add(theItem1);*/
                        }
                        //ddlSelectList.Width = 180;
                        ddlSelectList.CssClass = "form-control input-sm";
                        ddlSelectList.AutoPostBack = true;
                        ddlSelectList.Enabled = theEnable;
                        if (theConditional == true && theEnable == true)
                        {
                            ddlSelectList.AutoPostBack = true;
                            ddlSelectList.SelectedIndexChanged += new EventHandler(ddlSelectList_SelectedIndexChanged);
                        }

                        PnlDynamicElements.Controls.Add(ddlSelectList);
                        ApplyBusinessRules(ddlSelectList, controlId, theEnable);

                        //PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                        //PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                        //PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
                    }

                }
                else if (controlId == "5") ///Date
                {
                    //PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%'>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    //if (SetBusinessrule(FieldId, Column) == true)
                    //{
                    //    PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    //}
                    //else
                    //{
                    //    PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                    //}
                    //PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                    this.RenderLabel(label, fieldId, column,"DD-MMM-YYYY");
                    TextBox theDateText = new TextBox();
                    theDateText.ID = "TXTDT-" + column + "-" + table + "-" + fieldId;
                    if (hdnIds.Value == "")
                    {
                        hdnIds.Value = theDateText.ID;
                    }
                    else
                    {
                        hdnIds.Value = hdnIds.Value + "," + theDateText.ID;
                    }
                    Control ctl = (TextBox)theDateText;
                  
                    theDateText.CssClass = "form-control input-sm col-md-7";
                    theDateText.Style.Add("margin-right","0px");
                    theDateText.MaxLength = 11;
                    theDateText.Enabled = theEnable;
                    PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"col-md-11\" style=\"padding-right: 0px; padding-left: 0px\">"));
                    PnlDynamicElements.Controls.Add(theDateText);
                   // PnlDynamicElements.Controls.Add(new LiteralControl("<span class=\"help-block\">DD-MMM-YYYY</span>")); 
                    PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));

                    theDateText.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                    theDateText.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                    ApplyBusinessRules(theDateText, controlId, theEnable);

                    Image theDateImage = new Image();
                      theDateImage.ID = "img" + theDateText.ID;
                      theDateImage.Height = 22;
                      theDateImage.Width = 20;
                      theDateImage.Visible = theEnable;
                      theDateImage.ToolTip = "Date Helper";
                      theDateImage.ImageUrl = "~/images/cal_icon.gif";

                      theDateImage.Attributes.Add("onClick", "w_displayDatePicker('" + ((TextBox)ctl).ClientID + "');");
                      PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"col-md-1\" style=\"padding-left: 0px\">"));
                      PnlDynamicElements.Controls.Add(theDateImage);
                      PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
                      ApplyBusinessRules(theDateImage, controlId, theEnable);
                   
                     // PnlDynamicElements.Controls.Add(new LiteralControl("<span class='help-block smallerlabel' style='margin-left:0px'>(DD-MMM-YYYY)</span>"));
                   
                }
                else if (controlId == "6")  /// Radio Button
                {

                   
                    this.RenderLabel(label, fieldId, column);
                    PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"radio col-md-12 pull-left\" style=\"padding: 0px;text-align:left;vertical-align:middle;margin:0\">"));
                    HtmlInputRadioButton theYesNoRadio1 = new HtmlInputRadioButton();
                    theYesNoRadio1.ID = "RADIO1-" + column + "-" + table + "-" + fieldId;
                    theYesNoRadio1.Value = "Yes";
                    theYesNoRadio1.Name = "" + column + "";
                   // theYesNoRadio1.Attributes.Add("class", "form-control input-sm");
                    if (theConditional == true && theEnable == true)
                        theYesNoRadio1.Attributes.Add("onclick", "down(this);SetValue('HdnCntrl','System.Web.UI.HtmlControls.HtmlInputRadioButton%" + theYesNoRadio1.ClientID + "');");
                    else
                        theYesNoRadio1.Attributes.Add("onclick", "down(this);");
                    theYesNoRadio1.Attributes.Add("onfocus", "up(this)");

                     PnlDynamicElements.Controls.Add(new LiteralControl("<label class=\"radio-inline\">"));
                    PnlDynamicElements.Controls.Add(theYesNoRadio1);
                    PnlDynamicElements.Controls.Add(new LiteralControl("Yes "));
                    PnlDynamicElements.Controls.Add(new LiteralControl("</label>"));
                    theYesNoRadio1.Visible = theEnable;
                    ApplyBusinessRules(theYesNoRadio1, controlId, theEnable);

                    if (rdoTrueFalseStatus == false)
                    {
                        theEnable = false;
                    }


                    theYesNoRadio1.Visible = theEnable;
                   // PnlDynamicElements.Controls.Add(new LiteralControl("<label align='labelright' id='lblYes-" + FieldId + "'>Yes</label>"));

                    HtmlInputRadioButton theYesNoRadio2 = new HtmlInputRadioButton();
                    theYesNoRadio2.ID = "RADIO2-" + column + "-" + table + "-" + fieldId;
                    theYesNoRadio2.Value = "No";
                    theYesNoRadio2.Name = "" + column + "";
                   // theYesNoRadio2.Attributes.Add("class", "form-control input-sm");
                    if (theConditional == true && theEnable == true)
                        theYesNoRadio2.Attributes.Add("onclick", "down(this);SetValue('HdnCntrl','System.Web.UI.HtmlControls.HtmlInputRadioButton%" + theYesNoRadio2.ClientID + "');");
                    else
                        theYesNoRadio2.Attributes.Add("onclick", "down(this);");
                    theYesNoRadio2.Attributes.Add("onchange", "up(this)");
                    PnlDynamicElements.Controls.Add(new LiteralControl("<label class=\"radio-inline\">")); 
                    PnlDynamicElements.Controls.Add(theYesNoRadio2);
                    PnlDynamicElements.Controls.Add(new LiteralControl("No ")); 
                    PnlDynamicElements.Controls.Add(new LiteralControl("</label>"));
                    ApplyBusinessRules(theYesNoRadio2, controlId, theEnable);
                    theYesNoRadio2.Visible = theEnable;
                    PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
                   // PnlDynamicElements.Controls.Add(new LiteralControl("<label align='labelright' id='lblNo-" + FieldId + "'>No</label>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));

                }
                else if (controlId == "7") //Checkbox
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"col-md-12 pull-left\"style=\"padding:0\">"));
                    this.RenderLabel(label, fieldId, column);
                    PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
                    HtmlInputCheckBox theChk = new HtmlInputCheckBox();
                    theChk.ID = "Chk-" + column + "-" + table + "-" + fieldId;
                    theChk.Attributes.Add("class", "form-control input-sm");
                    
                    theChk.Visible = theEnable;
                    PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"col-md-1\" style=\"padding: 0;margin:0\">"));                 
                    PnlDynamicElements.Controls.Add(theChk);   
                    PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
              

                }
                else if (controlId == "8")  /// MultiLine TextBox
                {
                   
                    this.RenderLabel(label, fieldId, column);
                    TextBox theMultiText = new TextBox();
                    theMultiText.ID = "TXTMulti-" + column + "-" + table + "-" + fieldId;
                    //theMultiText.Width = 200;
                    theMultiText.CssClass = "form-control input-sm";
                    theMultiText.Rows = 5;
                    theMultiText.TextMode = TextBoxMode.MultiLine;
                    theMultiText.MaxLength = 200;
                    theMultiText.Enabled = theEnable;
                    PnlDynamicElements.Controls.Add(theMultiText);
                    ApplyBusinessRules(theMultiText, controlId, theEnable);
                }
                else if (controlId == "9") ///  MultiSelect List 
                {

                    PnlDynamicElements.Controls.Add(new LiteralControl("<table>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                    if (SetBusinessrule(fieldId, column) == true)
                    {
                        PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + label + "-" + fieldId + "'>" + label + " :</label>"));
                    }
                    else
                    {
                        PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + label + "-" + fieldId + "'>" + label + " :</label>"));
                    }
                    PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));

                    //WithPanel
                    Panel PnlMulti = new Panel();
                    PnlMulti.ID = "Pnl_" + fieldId;
                    PnlMulti.ToolTip = label;
                    PnlMulti.Enabled = theEnable;
                    PnlMulti.Controls.Add(new LiteralControl("<DIV class = 'customdivbordermultiselect'  runat='server' nowrap='nowrap'>"));

                    if (codeId == "")
                    {
                        codeId = "0";
                    }
                    string theCodeFldName = "";
                    DataTable theBindTbl = theDSXML.Tables[bindSource];
                    if (theBindTbl.Columns.Contains("CategoryId") == true)
                        theCodeFldName = "CategoryId";
                    else if (theBindTbl.Columns.Contains("SectionId") == true)
                        theCodeFldName = "SectionId";
                    else
                        theCodeFldName = "CodeId";
                    DataView theDV = new DataView(theDSXML.Tables[bindSource]);
                    theDV.RowFilter = "DeleteFlag=0 and SystemID IN(0," + Convert.ToString(Session["SystemId"]) + ") and " + theCodeFldName + "=" + codeId + "";
                    IQCareUtils theUtils = new IQCareUtils();
                    BindFunctions BindManager = new BindFunctions();
                    DataTable theDT = theUtils.CreateTableFromDataView(theDV);
                    if (theDT != null)
                    {
                        for (int i = 0; i < theDT.Rows.Count; i++)
                        {
                            CheckBox chkbox = new CheckBox();
                            chkbox.ID = Convert.ToString("CHKMULTI-" + theDT.Rows[i][0] + "-" + column + "-" + table + "-" + fieldId);
                            chkbox.Text = Convert.ToString(theDT.Rows[i]["Name"]);
                            if (chkbox.Text == "Other")
                            {
                                PnlMulti.Controls.Add(chkbox);
                                PnlMulti.Controls.Add(new LiteralControl("<DIV  class='pad10' id='" + column + "' style='DISPLAY:none'>Specify: "));
                                HtmlInputText HTextother = new HtmlInputText();
                                HTextother.ID = "TXTMULTI-" + theDT.Rows[i][0] + "-" + column + "-" + table + "-" + fieldId;
                                HTextother.Size = 10;
                                PnlMulti.Controls.Add(HTextother);
                                PnlMulti.Controls.Add(new LiteralControl(HTextother.Value));
                                PnlMulti.Controls.Add(new LiteralControl("</DIV>"));
                                if (theConditional == true && theEnable == true)
                                    chkbox.Attributes.Add("onclick", "toggle('" + column + "');SetValue('HdnCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");
                                else
                                    chkbox.Attributes.Add("onclick", "toggle('" + column + "');");

                            }
                            else
                            {
                                if (theConditional == true && theEnable == true)
                                    chkbox.Attributes.Add("onclick", "SetValue('HdnCntrl','System.Web.UI.WebControls.CheckBox%" + chkbox.ClientID + "');");

                                PnlMulti.Controls.Add(chkbox);
                                chkbox.Width = 300;
                                PnlMulti.Controls.Add(new LiteralControl("<br>"));

                            }
                        }
                    }
                    PnlMulti.Controls.Add(new LiteralControl("</DIV>"));
                    PnlDynamicElements.Controls.Add(PnlMulti);
                    ApplyBusinessRules(PnlMulti, controlId, theEnable);
                    PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
                }
                else if (controlId == "13")  /// Placeholder
                {
                    //PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%'>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:100%;Height:25px' align='right'>"));
                    HtmlGenericControl div1 = new HtmlGenericControl("div");
                    div1.ID = "DIVPLC-" + column + "-" + fieldId;
                    PlaceHolder thePlchlderText = new PlaceHolder();
                    thePlchlderText.ID = "plchlder-" + column + "-" + fieldId;
                    thePlchlderText.Controls.Add(div1);
                    PnlDynamicElements.Controls.Add(thePlchlderText);
                    //PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                    //PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
                }

                else if (controlId == "14")
                {
                    bool theCntrlPresent = false;
                    if (theCntrlPresent != true)
                    {

                        //PnlDynamicElements.Controls.Add(new LiteralControl("<table width='100%'>"));
                        //PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                        //PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:40%' align='right'>"));
                        //DropDownList ddlSelectListAuto = new DropDownList();
                        //if (SetBusinessrule(FieldId, Column) == true)
                        //{
                        //    PnlDynamicElements.Controls.Add(new LiteralControl("<label class='required' align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                        //}
                        //else
                        //{
                        //    PnlDynamicElements.Controls.Add(new LiteralControl("<label align='center' id='lbl" + Label + "-" + FieldId + "'>" + Label + " :</label>"));
                        //}
                        //PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                        //PnlDynamicElements.Controls.Add(new LiteralControl("<td style='width:60%' align='left'>"));
                        this.RenderLabel(label, fieldId, column);
                        DropDownList ddlSelectList = new DropDownList();
                        ddlSelectList.ID = "SELECTLIST-" + column + "-" + table + "-" + fieldId;
                        IQCareUtils theUtil = new IQCareUtils();
                        DataTable theDT = theUtil.CreateTimeTable(15);
                        DataRow theDR = theDT.NewRow();
                        theDR[0] = "0";
                        theDR[1] = "Select";
                        theDT.Rows.InsertAt(theDR, 0);
                        ddlSelectList.DataSource = theDT;
                        ddlSelectList.DataTextField = "Time";
                        ddlSelectList.DataValueField = "Id";
                        ddlSelectList.DataBind();
                        ddlSelectList.SelectedValue = "Select";
                        ddlSelectList.CssClass = "form-control input-sm";
                        //ddlSelectList.Width = 180;
                       
                        ddlSelectList.Enabled = theEnable;
                        if (theConditional == true && theEnable == true)
                        {
                            //ddlSelectList.AutoPostBack = true;
                            //ddlSelectList.SelectedIndexChanged += new EventHandler(ddlSelectList_SelectedIndexChanged);
                        }
                        PnlDynamicElements.Controls.Add(ddlSelectList);
                        ApplyBusinessRules(ddlSelectList, controlId, theEnable);
                        //PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                        //PnlDynamicElements.Controls.Add(new LiteralControl("</tr>"));
                        //PnlDynamicElements.Controls.Add(new LiteralControl("</table>"));
                    }
                }
                PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }

        private void ApplyBusinessRules(object theControl, string controlId, bool theConField)
        {
            try
            {
                DataTable theDT = (DataTable)ViewState["BusRule"];
                string Max = "", Min = "", Column = "";
                bool theEnable = theConField;
                string[] Field;
                if (controlId == "9")
                {
                    Field = ((Control)theControl).ID.Split('_');
                }
                else
                {
                    Field = ((Control)theControl).ID.Split('-');
                }
                foreach (DataRow DR in theDT.Rows)
                {
                    if (Field[0] == "Pnl")
                    {

                        if (Field[1] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "14"
                            && Session["PatientSex"].ToString() != "Male")
                            theEnable = false;

                        if (Field[1] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "15"
                            && Session["PatientSex"].ToString() != "Female")
                            theEnable = false;

                        if (Field[1] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "16")
                        {
                            if ((DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                                {
                                }
                                else
                                    theEnable = false;
                            }
                        }

                    }
                    else
                    {
                        if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2].ToLower() == Convert.ToString(DR["TableName"]).ToLower() && Field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "2")
                        {
                            Max = Convert.ToString(DR["Value"]);
                            Column = Convert.ToString(DR["FieldLabel"]);
                        }
                        if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2].ToLower() == Convert.ToString(DR["TableName"]).ToLower() && Field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "3")
                        {
                            Min = Convert.ToString(DR["Value"]);

                        }
                        if (Field[1] == Convert.ToString(DR["FieldName"]) && Field[2].ToLower() == Convert.ToString(DR["TableName"]).ToLower() && Field[3] == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "16")
                        {
                            if ((DR["Value"] != System.DBNull.Value) && (DR["Value1"] != System.DBNull.Value))
                            {
                                if (Convert.ToDecimal(Session["PatientAge"]) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(Session["PatientAge"]) <= Convert.ToDecimal(DR["Value1"]))
                                {
                                }
                                else
                                    theEnable = false;
                            }
                        }
                    }
                }

                if (theControl.GetType().ToString() == "System.Web.UI.WebControls.TextBox")
                {
                    Field = ((Control)theControl).ID.Split('-');
                    TextBox tbox = (TextBox)theControl;
                    tbox.Enabled = theEnable;
                    if (controlId == "1")
                    { }
                    else if (controlId == "2" && Field[0] == "TXT")
                    {
                        tbox.Attributes.Add("onkeyup", "chkDecimal('" + tbox.ClientID + "')");
                    }
                    else if (controlId == "3" && Field[0] == "TXTNUM")
                    {
                        tbox.Attributes.Add("onkeyup", "chkNumeric('" + tbox.ClientID + "')");
                    }
                    else if (controlId == "5" && Field[0] == "TXTDT")
                    {
                        tbox.Attributes.Add("onkeyup", "DateFormat(this,this.value,event,false,'3')");
                        tbox.Attributes.Add("onblur", "DateFormat(this,this.value,event,true,'3')");
                    }
                    if (Max != "" && Min != "")
                    {
                        tbox.Attributes.Add("onblur", "isBetween('" + tbox.ClientID + "', '" + Column + "', '" + Min + "', '" + Max + "')");
                    }
                    else if (Max != "")
                    {
                        tbox.Attributes.Add("onblur", "checkMax('" + tbox.ClientID + "', '" + Column + "', '" + Max + "')");
                    }
                    else if (Min != "")
                    {
                        tbox.Attributes.Add("onblur", "checkMin('" + tbox.ClientID + "', '" + Column + "', '" + Min + "')");
                    }

                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.DropDownList")
                {
                    DropDownList ddList = (DropDownList)theControl;
                    ddList.Enabled = theEnable;

                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.CheckBox")
                {
                    CheckBox Multichk = (CheckBox)theControl;
                    Multichk.Enabled = theEnable;
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton")
                {
                    HtmlInputRadioButton Rdobtn = (HtmlInputRadioButton)theControl;
                    Rdobtn.Visible = theEnable;
                    rdoTrueFalseStatus = true;
                    if (theEnable == false)
                    {
                        rdoTrueFalseStatus = false;
                    }
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.Image")
                {
                    Image img = (Image)theControl;
                    img.Visible = theEnable;
                }
                else if (theControl.GetType().ToString() == "System.Web.UI.WebControls.Panel")
                {
                    Panel pnl = (Panel)theControl;
                    pnl.Enabled = theEnable;
                }
            }
            catch (Exception err)
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["MessageText"] = err.Message.ToString();
                IQCareMsgBox.Show("#C1", theBuilder, this);
            }
        }
        void ddlSelectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList theDList = ((DropDownList)sender);
            DataSet theDS = (DataSet)Session["AllData"];
            string[] theCntrl = theDList.ID.Split('-');
            DataView theDVConFieldEnable = new DataView(theDS.Tables[6]);
            theDVConFieldEnable.RowFilter = "ConditionalFieldSectionId=" + theDList.SelectedValue.ToString() + "";
            DataTable Dtcon = new DataTable();
            IQCareUtils theUtils = new IQCareUtils();
            Dtcon = theUtils.CreateTableFromDataView(theDVConFieldEnable);
            foreach (DataRow theDR in Dtcon.Rows)
            {
                foreach (Control x in PnlDynamicElements.Controls)
                {
                    if (x.ID != null)
                    {
                        string[] theIdent = x.ID.Split('-');
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                            {
                                ((TextBox)x).Enabled = true;
                                if (theDR["controlid"].ToString() == "1")
                                {
                                    ApplyBusinessRules(x, "1", true);
                                }
                                if (theDR["controlid"].ToString() == "2")
                                {
                                    ApplyBusinessRules(x, "2", true);
                                }
                                if (theDR["controlid"].ToString() == "3")
                                {
                                    ApplyBusinessRules(x, "3", true);
                                }
                            }
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            {
                                ((TextBox)x).Enabled = false;
                                ((TextBox)x).Text = "";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                            {
                                if (x.ID.ToString() == theIdent[0].ToString() + "-" + theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                                {
                                    ((DropDownList)x).Enabled = true;
                                    ApplyBusinessRules(x, "4", true);
                                }
                                else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                {
                                    ((DropDownList)x).Enabled = false;
                                    ((DropDownList)x).SelectedValue = "0";

                                }
                            }
                        }
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl_" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                            {
                                ((Panel)x).Enabled = true;
                                ApplyBusinessRules(x, "9", true);
                            }
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                            {
                                ((Panel)x).Enabled = false;
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                            {
                                ((Image)x).Visible = true;
                                ApplyBusinessRules(x, "5", true);
                            }
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                ((Image)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                            {
                                ((HtmlInputRadioButton)x).Visible = true;
                                //ApplyBusinessRules(x, "6", true);
                            }
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                ((HtmlInputRadioButton)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theDList.SelectedValue.ToString())
                                ((HtmlInputCheckBox)x).Visible = true;
                            else if (theCntrl[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theDList.SelectedValue.ToString())
                                ((HtmlInputCheckBox)x).Visible = false;
                        }
                    }
                }
            }
        }

        private StringBuilder InsertMultiSelectList(string patientId, string fieldName, int featureId, string Multi_SelectTable, int theControlId, int theFieldId)
        {
            StringBuilder Insertcbl = new StringBuilder();
            foreach (Control y in PnlDynamicElements.Controls)
            {
                if (y.GetType() == typeof(Panel))
                {
                    if (((Panel)y).ID == "Pnl_" + theControlId.ToString() && ((Panel)y).Enabled == false)
                        return Insertcbl;
                    foreach (Control x in y.Controls)
                    {

                        if (x.GetType() == typeof(CheckBox))
                        {
                            string[] TableName = ((CheckBox)x).ID.Split('-');
                            if (TableName.Length == 5)
                            {
                                string Table = TableName[3];

                                if (Table == Multi_SelectTable)
                                {
                                    if (Table == "dtl_CustomField")
                                    {
                                        Table = "dtl_FB_" + TableName[2] + "";
                                        Table = Table.Replace(' ', '_');
                                    }
                                    if (Table != "dtl_CustomField" && Convert.ToInt32(TableName[4]) == theFieldId)
                                    {

                                        if (((CheckBox)x).Checked == true && ((CheckBox)x).Text != "Other")
                                        {
                                            Insertcbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                            Insertcbl.Append("values (" + patientId + ",  @visit_pk ," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                            Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                        }

                                        else if (((CheckBox)x).Checked == true && ((CheckBox)x).Text == "Other")
                                        {
                                            ViewState["OtherNote"] = ((CheckBox)x).Text;
                                        }
                                    }

                                    else if (Convert.ToInt32(TableName[4]) == theFieldId)
                                    {
                                        if (((CheckBox)x).Checked == true)
                                        {
                                            Insertcbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                            Insertcbl.Append("values (" + patientId + ",  @visit_pk ," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                            Insertcbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                        }

                                    }
                                }
                            }
                        }

                        if (x.GetType() == typeof(System.Web.UI.HtmlControls.HtmlInputText))
                        {
                            string[] TableName = ((HtmlInputText)x).ID.Split('-');
                            string Table = TableName[3];
                            string Other = "";
                            if (Table == Multi_SelectTable)
                            {
                                if (Table == "dtl_CustomField")
                                {
                                    Table = "dtl_FB_" + TableName[2] + "";
                                    Table = Table.Replace(' ', '_');
                                }
                                if (Table != "dtl_CustomField")
                                {
                                    string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                                    DataSet dsMultiSelectList = new DataSet();
                                    dsMultiSelectList.ReadXml(filePath);
                                    DataTable DT = dsMultiSelectList.Tables[0];
                                    foreach (DataRow DR in DT.Rows)
                                    {
                                        if (DR[0].ToString() == Table)
                                        {
                                            Other = DR[2].ToString();
                                        }
                                    }

                                    if (Convert.ToString(ViewState["OtherNote"]) != "" && ((HtmlInputText)x).Value != "")
                                    {
                                        Insertcbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "],[" + Other + "], [UserID], [CreateDate])");
                                        Insertcbl.Append("values (" + patientId + ", @visit_pk ," + Session["AppLocationId"].ToString() + "," + TableName[1] + ",");
                                        Insertcbl.Append("'" + ((HtmlInputText)x).Value + "', " + Session["AppUserId"].ToString() + ", Getdate())");
                                    }
                                }
                            }
                        }
                    }
                }

            }
            return Insertcbl;
        }

        private StringBuilder UpdateMultiSelectList(int patientId, int featureId, int visitId, int locationId, string Multi_SelectTable, string columnName, int deleteFlag, int theControlId)
        {
            StringBuilder Updatecbl = new StringBuilder();

            if (deleteFlag == 0)
            {
                if (Multi_SelectTable == "dtl_CustomField")
                {
                    Multi_SelectTable = "dtl_FB_" + columnName + "";
                    Multi_SelectTable = Multi_SelectTable.Replace(' ', '_');
                }
                if (Updatecbl.ToString().Contains(Multi_SelectTable.ToString()) == false)
                    Updatecbl.Append("Delete from [" + Multi_SelectTable + "] where [ptn_pk]=" + patientId + " and [Visit_Pk]=" + visitId + " and [LocationID]=" + locationId + "");
                return Updatecbl;
            }
            else
            {
                foreach (Control y in PnlDynamicElements.Controls)
                {
                    if (y.GetType() == typeof(Panel))
                    {
                        foreach (Control x in y.Controls)
                        {
                            if (((Panel)y).ID == "Pnl_" + theControlId.ToString() && ((Panel)y).Enabled == false)
                                return Updatecbl;
                            if (x.GetType() == typeof(CheckBox))
                            {
                                string[] TableName = ((CheckBox)x).ID.Split('-');
                                if (TableName.Length == 5)
                                {
                                    string Table = TableName[3];
                                    if (Table == Multi_SelectTable)
                                    {
                                        if (Table == "dtl_CustomField")
                                        {
                                            Table = "dtl_FB_" + TableName[2] + "";
                                            Table = Table.Replace(' ', '_');
                                        }
                                        if (Table != "dtl_CustomField")
                                        {


                                            if (((CheckBox)x).Checked == true && ((CheckBox)x).Text != "Other")
                                            {
                                                Updatecbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                                Updatecbl.Append("values (" + patientId + ",  " + visitId + ", " + locationId + ", " + TableName[1] + ",");
                                                Updatecbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                            }

                                            else if (((CheckBox)x).Checked == true && ((CheckBox)x).Text == "Other")
                                            {
                                                ViewState["OtherNote"] = ((CheckBox)x).Text;
                                            }
                                        }

                                        else
                                        {
                                            if (((CheckBox)x).Checked == true)
                                            {
                                                Updatecbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "], [UserID], [CreateDate])");
                                                Updatecbl.Append("values (" + patientId + ",  " + visitId + ", " + locationId + "," + TableName[1] + ",");
                                                Updatecbl.Append("" + Session["AppUserId"].ToString() + ", Getdate())");
                                            }

                                        }
                                    }
                                }
                            }

                            if (x.GetType() == typeof(HtmlInputText))
                            {
                                string[] TableName = ((HtmlInputText)x).ID.Split('-');
                                string Table = TableName[3];
                                string Other = "";
                                if (Table == Multi_SelectTable)
                                {
                                    if (Table == "dtl_CustomField")
                                    {
                                        Table = "dtl_FB_" + TableName[2] + "";
                                        Table = Table.Replace(' ', '_');
                                    }
                                    if (Table != "dtl_CustomField")
                                    {
                                        string filePath = Server.MapPath("~/XMLFiles/MultiSelectCustomForm.xml");
                                        DataSet dsMultiSelectList = new DataSet();
                                        dsMultiSelectList.ReadXml(filePath);
                                        DataTable DT = dsMultiSelectList.Tables[0];
                                        foreach (DataRow DR in DT.Rows)
                                        {
                                            if (DR[0].ToString() == Table)
                                            {
                                                Other = DR[2].ToString();
                                            }
                                        }

                                        if (Convert.ToString(ViewState["OtherNote"]) != "" && ((HtmlInputText)x).Value != "")
                                        {
                                            Updatecbl.Append("Insert into [" + Table + "]([ptn_pk], [Visit_Pk], [LocationID], [" + TableName[2] + "],[" + Other + "], [UserID], [CreateDate])");
                                            Updatecbl.Append("values (" + patientId + ", " + visitId + ", " + locationId + "," + TableName[1] + ",");
                                            Updatecbl.Append("'" + ((HtmlInputText)x).Value + "', " + Session["AppUserId"].ToString() + ", Getdate())");
                                            ViewState["OtherNote"] = null;
                                        }
                                    }
                                }
                            }

                        }

                    }
                }
                return Updatecbl;
            }
        }

        private bool SetBusinessrule(string fieldId, string fieldLabel)
        {

            DataTable theDT = (DataTable)ViewState["BusRule"];
            foreach (DataRow DR in theDT.Rows)
            {
                if (Convert.ToString(DR["FieldID"]) == fieldId && Convert.ToString(DR["FieldName"]) == fieldLabel && Convert.ToString(DR["BusRuleId"]) == "1")
                {
                    return true;
                }
            }
            return false;
        }
        private void SectionHeading(string H2)
        {
          //  PnlDynamicElements.Controls.Add(new LiteralControl("<br>"));
           // PnlDynamicElements.Controls.Add(new LiteralControl("<h2 class='forms' align='left'>" + H2 + "</h2>"));
            PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"panel panel-heading\"><i class='glyphicon glyphicon-list-alt fa-1x pull-left' aria-hidden=\"true\"><strong>" + H2 + "</strong></i><br /></div>"));

        }

        private void LoadPredefinedLabel_Field(int featureId)
        {
            CustomFormService cfS = new CustomFormService();
           // IPatientRegistration IPatientCustomFormMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
            List<FormSection> sections = cfS.GetRegistrationFormSections(featureId);
            DataSet theDS = cfS.FormFieldLabels;
                //IPatientCustomFormMgr.GetFieldName_and_Label(FeatureID, PatientID);
            Session["AllData"] = theDS;
            if (theDS.Tables.Count > 0)
            {
                if (theDS.Tables[10].Rows.Count == 0)
                {
                    return;
                }
            }
            Session["AllData"] = theDS;
            ViewState["LnkTable"] = theDS.Tables[1];
            ViewState["LnkConTable"] = theDS.Tables[6];
            ViewState["BusRule"] = theDS.Tables[2];
            Session["SessionBusRule"] = theDS.Tables[2];
            DataTable DT = theDS.Tables[1].DefaultView.ToTable(true, "SectionID", "SectionName").Copy();
            int Numtds = 4, td = 1;
            PnlDynamicElements.Controls.Clear();
            DataTable theConditionalFields = theDS.Tables[6].Copy();
            theConditionalFields.Columns.Add("Load", typeof(string));
            theConditionalFields.Columns["Load"].DefaultValue = "T";
            foreach (DataRow theMDR in theDS.Tables[6].Rows)
            {
                int theFieldId = Convert.ToInt32(theMDR["FieldId"]);
                bool theRecFnd = false;
                foreach (DataRow theDR in theConditionalFields.Rows)
                {
                    if (Convert.ToInt32(theDR["FieldId"]) == theFieldId && theRecFnd == true)
                        theDR["Load"] = "F";
                    else if (Convert.ToInt32(theDR["FieldId"]) == theFieldId)
                    {
                        theDR["Load"] = "T";
                        theRecFnd = true;
                    }
                }
                theRecFnd = false;
            }
            theConditionalFields.AcceptChanges();
            PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"panel panel-default\">"));
            foreach (FormSection section in sections)
            {
                this.SectionHeading(section.Name);
                PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"panel-body\" style=\"padding-top:0; padding-bottom:0\">"));
                foreach (FormField field in section.FieldSet)
                {
                    
                    DataView theDVConditionalField = new DataView(theConditionalFields);
                    theDVConditionalField.RowFilter = "ConFieldId=" + field.FieldId.ToString() + " and ConFieldPredefined=" +(field.Field.Predefined? "1" : "0") + " and Load = 'T'";
                    theDVConditionalField.Sort = "ConditionalFieldSectionId, Seq asc";
                    if (theDVConditionalField.Count > 0)
                        theConditional = true;
                    else
                        theConditional = false;
                    if (td <= Numtds)
                    {
                        if (td == 1)
                        {
                            //PnlDynamicElements.Controls.Add(new LiteralControl("<tr>"));
                            PnlDynamicElements.Controls.Add(new LiteralControl(" <div class=\"row well well-sm\">"));
                        }
                        if (td < Numtds)
                        {
                            PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"col-md-3\">"));
                            // PnlDynamicElements.Controls.Add(new LiteralControl("<td class='border center pad5 whitebg' style='width: 50%'>"));
                            LoadFieldTypeControl(field.Field.FieldType.Id.ToString(),
                                field.Field.Name,
                                field.FieldId.ToString(),
                               field.Field.CategoryId.ToString(),
                               field.FieldLabel,
                               
                               field.PersistTable,
                                field.Field.BindTable,
                                true);
                            // PnlDynamicElements.Controls.Add(new LiteralControl("</td>"));
                            PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
                            td++;
                        }
                        else
                        {
                            PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"col-md-3\">"));
                            LoadFieldTypeControl(field.Field.FieldType.Id.ToString(),
                               field.Field.Name,
                               field.FieldId.ToString(),
                              field.Field.CategoryId.ToString(),
                              field.FieldLabel,
                              field.PersistTable,
                               field.Field.BindTable,
                               true);
                            PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
                            PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
                            td = 1;
                        }
                    }
                    if (theConditional == true)
                    {
                        for (int i = 0; i < theDVConditionalField.Count; i++)
                        {
                            if (td <= Numtds)
                            {
                                if (td == 1)
                                {
                                  
                                    PnlDynamicElements.Controls.Add(new LiteralControl(" <div class=\"row well well-sm\">"));
                                }
                                if (td < Numtds)
                                {
               
                                    PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"col-md-3\">"));
                                    LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                    theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(),
                                    theDVConditionalField[i]["BindSource"].ToString(), false);
                                    PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));                                
                                    td++;
                                }
                                else
                                {
                              
                                    PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"col-md-3\">"));
                                    LoadFieldTypeControl(theDVConditionalField[i]["Controlid"].ToString(), theDVConditionalField[i]["FieldName"].ToString(), theDVConditionalField[i]["FieldID"].ToString(),
                                    theDVConditionalField[i]["CodeID"].ToString(), theDVConditionalField[i]["FieldLabel"].ToString(), theDVConditionalField[i]["PDFTableName"].ToString(),
                                    theDVConditionalField[i]["BindSource"].ToString(), false);
                                    PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
                                    PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
                                    td = 1;
                                }
                            }

                        }
                 
                    }


                }
                if (td > 1 && td <= Numtds)
                {
                    PnlDynamicElements.Controls.Add(new LiteralControl("<div class=\"col-md-3\">"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
                    PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
                    
                }
                td = 1;
                PnlDynamicElements.Controls.Add(new LiteralControl("</div>")); //close panel-body
            }
           
            PnlDynamicElements.Controls.Add(new LiteralControl("</div>"));
            ViewState["NoMulti"] = theDS.Tables[3];
        }

        private void LoadPatientStaticData(int Ptn_Pk)
        {
            string moduleId;
            if (Session["CEModule"] != null)
                moduleId = Session["CEModule"].ToString();
            IQCareUtils theUtil = new IQCareUtils();
            try
            {
                IPatientRegistration PatientManager = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
                DataSet theDS = PatientManager.GetPatientRegistration(Ptn_Pk, 12);
                ViewState["themstpatient"] = theDS.Tables[0];
                ViewState["VisitPk"] = theDS.Tables[4].Rows[0]["VisitId"].ToString();
                this.txtIQCareRef.Text = theDS.Tables[0].Rows[0]["IQNumber"].ToString();
                ViewState["IQNumber"] = txtIQCareRef.Text;
                this.ddgender.SelectedValue = theDS.Tables[0].Rows[0]["RegSex"].ToString();
                this.txtRegDate.Text = string.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(theDS.Tables[0].Rows[0]["RegDate"]));
                this.txtageCurrentYears.Text = theDS.Tables[0].Rows[0]["Age"].ToString();
                this.txtageCurrentMonths.Text = theDS.Tables[0].Rows[0]["Age1"].ToString();
                this.txtlastName.Text = theDS.Tables[0].Rows[0]["LastName"].ToString();
                this.txtmiddleName.Text = theDS.Tables[0].Rows[0]["MiddleName"].ToString();
                this.txtfirstName.Text = theDS.Tables[0].Rows[0]["FirstName"].ToString();
                this.TxtDOB.Text = ((DateTime)theDS.Tables[0].Rows[0]["RegDOB"]).ToString(Session["AppDateFormat"].ToString());
                if (Convert.ToInt32(theDS.Tables[0].Rows[0]["DobPrecision"]) == 1)
                {
                    this.rbtndobPrecEstimated.Checked = true;
                }
                else if (Convert.ToInt32(theDS.Tables[0].Rows[0]["DobPrecision"]) == 0)
                {
                    this.rbtndobPrecExact.Checked = true;
                }
                this.ddmaritalStatus.SelectedValue = theDS.Tables[0].Rows[0]["MaritalStatus"].ToString();
            }
            catch (Exception err)
            {
                IQCareMsgBox.NotifyAction(err.Message, "Error Loading Page", true, this, "");
                //MsgBuilder theBuilder = new MsgBuilder();
                //theBuilder.DataElements["MessageText"] = err.Message.ToString();
                //IQCareMsgBox.Show("#C1", theBuilder, this);
                return;
            }
            finally
            {

            }
        }

        private void BindValue(int patientId, int visitId, int locationId, Control theControl)
        {
            ICustomForm MgrBindValue = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameterBCustom);
            DataTable theDT = SetControlIDs(PnlDynamicElements);
            DataTable TempDT = theDT.DefaultView.ToTable(true, "TableName").Copy();
            StringBuilder GetStaticData = new StringBuilder();
            GetStaticData.Append("Select VisitDate, Signature,DataQuality from ord_visit where Ptn_Pk=" + patientId + " and Visit_Id=" + visitId + " and LocationID=" + locationId + "");
            DataSet theDS = new DataSet();
            DataSet TmpDS = MgrBindValue.Common_GetSaveUpdate(GetStaticData.ToString());
            DataTable theMstPatientDT = new DataTable();
            try
            {
                foreach (DataRow TempDR in TempDT.Rows)
                {
                    string GetValue = "";
                    if (Convert.ToString(TempDR["TableName"]).ToUpper() == "DTL_CUSTOMFIELD")
                    {
                        theMstPatientDT = new DataTable();
                        string TableName = "DTL_FBCUSTOMFIELD_" + "Patient Registration".Replace(' ', '_');
                        GetValue = "Select * from [" + TableName + "] where Ptn_pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationId=" + locationId + "";
                    }
                    else
                    {
                        if (Convert.ToString(TempDR["TableName"]).ToUpper() == "DTL_PATIENTCONTACTS")
                        {
                            theMstPatientDT = new DataTable();
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + patientId + " and Visitid=" + visitId + " and LocationId=" + locationId + "";
                        }
                        else if (Convert.ToString(TempDR["TableName"]).ToUpper() == "MST_PATIENT")
                        {
                            theMstPatientDT = new DataTable();
                            theMstPatientDT = (DataTable)ViewState["themstpatient"];
                            GetValue = "";
                        }
                        else
                        {
                            theMstPatientDT = new DataTable();
                            GetValue = "Select * from [" + TempDR["TableName"] + "] where Ptn_pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationId=" + locationId + "";
                        }
                    }
                    DataSet TempDS = new DataSet();
                    if (GetValue != "")
                    {
                        TempDS = MgrBindValue.Common_GetSaveUpdate(GetValue);
                        theMstPatientDT = TempDS.Tables[0].Copy();
                    }

                    for (int i = 0; i <= theMstPatientDT.Columns.Count - 1; i++)
                    {

                        foreach (Control x in theControl.Controls)
                        {
                            if (x.GetType() == typeof(TextBox))
                            {

                                if ("TXTMulti-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                {
                                    if (theMstPatientDT.Rows.Count > 0)
                                    {
                                        ((TextBox)x).Text = Convert.ToString(theMstPatientDT.Rows[0][i]);
                                    }
                                }
                                if ("TXTSingle-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                {
                                    if (theMstPatientDT.Rows.Count > 0)
                                    {
                                        ((TextBox)x).Text = Convert.ToString(theMstPatientDT.Rows[0][i]);
                                    }
                                }

                                if ("TXT-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                {
                                    if (theMstPatientDT.Rows.Count > 0)
                                    {
                                        ((TextBox)x).Text = Convert.ToString(theMstPatientDT.Rows[0][i]);
                                    }
                                }
                                if ("TXTNUM-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                {
                                    if (theMstPatientDT.Rows.Count > 0)
                                    {
                                        ((TextBox)x).Text = Convert.ToString(theMstPatientDT.Rows[0][i]);
                                    }
                                }

                                if ("TXTDT-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((TextBox)x).ID.Substring(0, ((TextBox)x).ID.LastIndexOf('-')))
                                {
                                    if (theMstPatientDT.Rows.Count > 0)
                                    {
                                        ((TextBox)x).Text = string.Format("{0:dd-MMM-yyyy}", theMstPatientDT.Rows[0][i]);
                                    }
                                }

                            }

                            else if (x.GetType() == typeof(DropDownList))
                            {
                                if (Convert.ToString("SELECTLIST-" + theMstPatientDT.Columns[i] + "-" + TempDR["TableName"]).ToUpper() == ((DropDownList)x).ID.Substring(0, ((DropDownList)x).ID.LastIndexOf('-')).ToUpper())
                                {
                                    if (theMstPatientDT.Rows.Count > 0)
                                    {
                                        ((DropDownList)x).SelectedValue = Convert.ToString(theMstPatientDT.Rows[0][i]);

                                        //DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                        //string[] theId = ((DropDownList)x).ID.Split('-');
                                        //theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                        //if (theDVConditionalField.Count > 0)
                                        //{
                                        //EventArgs s = new EventArgs();
                                        //ddlSelectList_SelectedIndexChanged((DropDownList)x, s);
                                        //}

                                    }
                                }

                            }
                            else if (x.GetType() == typeof(HtmlInputRadioButton))
                            {
                                if (theMstPatientDT.Columns[i].ToString() == ((HtmlInputRadioButton)x).Name)
                                {
                                    for (int k = 0; k < theMstPatientDT.Rows.Count; k++)
                                    {
                                        if (theMstPatientDT.Rows[k][theMstPatientDT.Columns[i]].ToString() == "True" || theMstPatientDT.Rows[k][theMstPatientDT.Columns[i]].ToString() == "1")
                                        {
                                            if ("RADIO1-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((HtmlInputRadioButton)x).ID.Substring(0, ((HtmlInputRadioButton)x).ID.LastIndexOf('-')))
                                            {
                                                ((HtmlInputRadioButton)x).Checked = true;
                                                DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                                                string[] theId = ((HtmlInputRadioButton)x).ID.Split('-');
                                                theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                                if (theDVConditionalField.Count > 0)
                                                {
                                                    EventArgs s = new EventArgs();
                                                    //this.HtmlRadioButtonSelect(x);
                                                }

                                            }

                                        }
                                        else if (theMstPatientDT.Rows[k][theMstPatientDT.Columns[i]].ToString() == "False" || theMstPatientDT.Rows[k][theMstPatientDT.Columns[i]].ToString() == "0")
                                        {
                                            if ("RADIO2-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((HtmlInputRadioButton)x).ID.Substring(0, ((HtmlInputRadioButton)x).ID.LastIndexOf('-')))
                                            {
                                                ((HtmlInputRadioButton)x).Checked = true;
                                                DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                                                string[] theId = ((HtmlInputRadioButton)x).ID.Split('-');
                                                theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(3);
                                                if (theDVConditionalField.Count > 0)
                                                {
                                                    EventArgs s = new EventArgs();
                                                    //this.HtmlRadioButtonSelect(x);
                                                }

                                            }

                                        }
                                    }
                                }
                            }

                            else if (x.GetType() == typeof(HtmlInputCheckBox))
                            {
                                if ("Chk-" + theMstPatientDT.Columns[i].ToString() + "-" + TempDR["TableName"] == ((HtmlInputCheckBox)x).ID.Substring(0, ((HtmlInputCheckBox)x).ID.LastIndexOf('-')))
                                {
                                    for (int k = 0; k < theMstPatientDT.Rows.Count; k++)
                                    {

                                        if (theMstPatientDT.Rows[k][theMstPatientDT.Columns[i]].ToString() == "True")
                                        {
                                            ((HtmlInputCheckBox)x).Checked = true;

                                        }
                                        else { ((HtmlInputCheckBox)x).Checked = false; }

                                    }
                                }
                            }

                        }
                    }
                }
                foreach (Control y in PnlDynamicElements.Controls)
                {
                    if (y.GetType() == typeof(Panel))
                    {
                        foreach (Control z in y.Controls)
                        {
                            if (z.GetType() == typeof(CheckBox))
                            {
                                string[] Table = ((CheckBox)z).ID.Split('-');
                                if (Table.Length == 5)
                                {
                                    string TableName = Table[3];
                                    string GetValue = "";
                                    string Id = Table[1];
                                    if (TableName == "dtl_CustomField")
                                    {
                                        TableName = "dtl_FB_" + Table[2] + "";
                                        TableName = TableName.Replace(' ', '_');
                                        GetValue = "Select * from " + TableName + " where Ptn_pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationId=" + locationId + "";
                                    }
                                    else
                                    {
                                        GetValue = "Select * from " + TableName + " where Ptn_pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationId=" + locationId + "";
                                    }

                                    DataSet TmpDSMulti = MgrBindValue.Common_GetSaveUpdate(GetValue);
                                    if (Table[3] == "dtl_CustomField")
                                    {
                                        foreach (DataRow theDR in TmpDSMulti.Tables[0].Rows)
                                        {
                                            if (Id == theDR[Table[2]].ToString())
                                            {
                                                ((CheckBox)z).Checked = true;
                                                DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                                                string[] theId = ((CheckBox)z).ID.Split('-');
                                                theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                                if (theDVConditionalField.Count > 0)
                                                {
                                                    EventArgs s = new EventArgs();
                                                    this.HtmlCheckBoxSelect(z);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        foreach (DataRow theDR in TmpDSMulti.Tables[0].Rows)
                                        {
                                            if (Id == theDR[3].ToString())
                                            {
                                                if (((CheckBox)z).Text == "Other")
                                                {
                                                    ((CheckBox)z).Checked = true;
                                                    DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[6]);
                                                    string[] theId = ((CheckBox)z).ID.Split('-');
                                                    theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                                    if (theDVConditionalField.Count > 0)
                                                    {
                                                        EventArgs s = new EventArgs();
                                                        this.HtmlCheckBoxSelect(z);
                                                    }

                                                    string script = "";
                                                    script = "<script language = 'javascript' defer ='defer' id = 'Other'" + Id + ">\n";
                                                    script += "show('" + Table[2] + "');\n";
                                                    script += "</script>\n";
                                                    ClientScript.RegisterStartupScript(this.GetType(), "Other" + Id + "", script);
                                                    ViewState["Otherchk"] = ((CheckBox)z).Text;
                                                    ViewState["Othertxt"] = theDR[4];
                                                }
                                                else
                                                {
                                                    ((CheckBox)z).Checked = true;
                                                    DataView theDVConditionalField = new DataView(((DataSet)Session["AllData"]).Tables[17]);
                                                    string[] theId = ((CheckBox)z).ID.Split('-');
                                                    theDVConditionalField.RowFilter = "ConFieldId=" + theId.GetValue(4);
                                                    if (theDVConditionalField.Count > 0)
                                                    {
                                                        EventArgs s = new EventArgs();
                                                        this.HtmlCheckBoxSelect(z);
                                                    }
                                                }

                                            }
                                        }

                                    }
                                }


                                if (z.GetType() == typeof(HtmlInputText))
                                {
                                    if (Convert.ToString(ViewState["Otherchk"]) == "Other")
                                    {
                                        ((HtmlInputText)z).Value = Convert.ToString(ViewState["Othertxt"]);
                                    }

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception err)
            {

                IQCareMsgBox.NotifyAction(err.Message, "Error Loading Page", true, this, "");
            }

        }

        private StringBuilder UpdateCustomRegistrationData(int patientId, int visitId, int locationId)
        {
            DataTable LnkDTUnique = new DataTable();
            StringBuilder sbUpdate = new StringBuilder();
            DataTable theDTNoMulti = new DataTable();
            DataTable theDTMulti = new DataTable();
            DataTable theDTConMulti = new DataTable();
            ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameterBCustom);
            DataTable theDT = SetControlIDs(PnlDynamicElements);

            if (ViewState["NoMulti"] != null)
            {
                theDTNoMulti = ((DataTable)ViewState["NoMulti"]);
                theDTMulti = ((DataTable)ViewState["LnkTable"]);
                theDTConMulti = ((DataTable)ViewState["LnkConTable"]);
                LnkDTUnique = theDTNoMulti.DefaultView.ToTable(true, "PDFTableName", "FeatureName").Copy();

            }
            int DOBPrecision = 0;
            if (rbtndobPrecEstimated.Checked == true)
            {
                DOBPrecision = 1;
            }
            else if (rbtndobPrecExact.Checked == true)
            {
                DOBPrecision = 0;
            }
            else
            {
                DOBPrecision = 2;
            }

            bool HasContacts = false;
            bool HasHouseHold = false;
            bool HasRural = false;
            bool HasUrban = false;
            bool HasHivPrev = false;
            bool HasGurantor = false;
            bool HasDeposit = false;
            bool HasInterview = false;
            bool HasCustom = false;

            /////////////////////////////////////////////////
            StringBuilder SbmstpatColumns = new StringBuilder();
            StringBuilder SbmstpatValues = new StringBuilder();
            SbmstpatColumns.Append("Update [MST_PATIENT]Set ");
            SbmstpatColumns.Append("FirstName=encryptbykey(key_guid('Key_CTC'),'" + this.SanitizeQoute(txtfirstName.Text) + "'), MiddleName=encryptbykey(key_guid('Key_CTC'),'" + this.SanitizeQoute(txtmiddleName.Text) + "'),");
            SbmstpatColumns.Append("LastName=encryptbykey(key_guid('Key_CTC'),'" + this.SanitizeQoute(txtlastName.Text) + "'), LocationID='" + Session["AppLocationId"] + "', RegistrationDate='" + txtRegDate.Text + "',");
            SbmstpatColumns.Append("Sex='" + ddgender.SelectedValue + "',DOB='" + TxtDOB.Text + "',DobPrecision='" + DOBPrecision + "',MaritalStatus='" + ddmaritalStatus.SelectedValue + "',");
            SbmstpatColumns.Append("CountryId='" + Session["AppCountryId"] + "', PosId='" + Session["AppPosID"] + "', SatelliteId='" + Session["AppSatelliteId"] + "', ");

            StringBuilder sbDeletion = new StringBuilder();
            sbDeletion.Append(" Delete  from [DTL_PATIENTCONTACTS] where Ptn_Pk=" + patientId + " and Visitid=" + visitId + " and LocationID=" + locationId + " ; ");
            sbDeletion.Append(" Delete  from [DTL_PATIENTHOUSEHOLDINFO] where Ptn_Pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationID=" + locationId + " ; ");
            sbDeletion.Append(" Delete  from [DTL_RURALRESIDENCE] where Ptn_Pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationID=" + locationId + " ; ");
            sbDeletion.Append(" Delete  from [DTL_URBANRESIDENCE] where Ptn_Pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationID=" + locationId + " ; ");
            sbDeletion.Append(" Delete  from [DTL_PATIENTHIVPREVCAREENROLLMENT] where Ptn_Pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationID=" + locationId + " ; ");
            sbDeletion.Append(" Delete  from [DTL_PATIENTGUARANTOR] where Ptn_Pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationID=" + locationId + " ; ");
            sbDeletion.Append(" Delete  from [DTL_PATIENTDEPOSITS] where Ptn_Pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationID=" + locationId + " ; ");
            sbDeletion.Append(" Delete  from [DTL_PATIENTINTERVIEWER] where Ptn_Pk=" + patientId + " and Visit_Pk=" + visitId + " and LocationID=" + locationId + " ; ");


            StringBuilder SbContColumns = new StringBuilder();
            StringBuilder SbContValues = new StringBuilder();
            //  SbContColumns.Append(" Delete  from [DTL_PATIENTCONTACTS] where Ptn_Pk=" + PatientID + " and Visitid=" + VisitID + " and LocationID=" + LocationID + " ");      
            SbContColumns.Append(" Insert into [DTL_PATIENTCONTACTS](Ptn_pk,Visitid,LocationId,UserID,UpdateDate,");
            SbContValues.Append("Values(" + patientId + "," + visitId + ", " + locationId + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SbHouseHoldColumns = new StringBuilder();
            StringBuilder SbHouseHoldValues = new StringBuilder();
            //  SbHouseHoldColumns.Append(" Delete  from [DTL_PATIENTHOUSEHOLDINFO] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
            SbHouseHoldColumns.Append("Insert into [DTL_PATIENTHOUSEHOLDINFO](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbHouseHoldValues.Append("Values(" + patientId + "," + visitId + ", " + locationId + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SbruralResidenceColumns = new StringBuilder();
            StringBuilder SbruralResidenceValues = new StringBuilder();
            //  SbruralResidenceColumns.Append(" Delete  from [DTL_RURALRESIDENCE] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
            SbruralResidenceColumns.Append("Insert into [DTL_RURALRESIDENCE](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbruralResidenceValues.Append("Values(" + patientId + "," + visitId + ", " + locationId + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SburbanresidenceColumns = new StringBuilder();
            StringBuilder SburbanresidenceValues = new StringBuilder();
            //   SburbanresidenceColumns.Append(" Delete  from [DTL_URBANRESIDENCE] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
            SburbanresidenceColumns.Append("Insert into [DTL_URBANRESIDENCE](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SburbanresidenceValues.Append("Values(" + patientId + "," + visitId + ", " + locationId + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SbpatienthivprevcareenrollmentColumns = new StringBuilder();
            StringBuilder SbpatienthivprevcareenrollmentValues = new StringBuilder();
            // SbpatienthivprevcareenrollmentColumns.Append(" Delete  from [DTL_PATIENTHIVPREVCAREENROLLMENT] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
            SbpatienthivprevcareenrollmentColumns.Append("Insert into [DTL_PATIENTHIVPREVCAREENROLLMENT](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbpatienthivprevcareenrollmentValues.Append("Values(" + patientId + "," + visitId + ", " + locationId + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SbpatientguarantorColumns = new StringBuilder();
            StringBuilder SbpatientguarantorValues = new StringBuilder();
            //SbpatientguarantorColumns.Append(" Delete  from [DTL_PATIENTGUARANTOR] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
            SbpatientguarantorColumns.Append("Insert into [DTL_PATIENTGUARANTOR](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbpatientguarantorValues.Append("Values(" + patientId + "," + visitId + ", " + locationId + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SbpatientDepositsColumns = new StringBuilder();
            StringBuilder SbpatientDepositsValues = new StringBuilder();
            //  SbpatientDepositsColumns.Append(" Delete  from [DTL_PATIENTDEPOSITS] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
            SbpatientDepositsColumns.Append("Insert into [DTL_PATIENTDEPOSITS](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbpatientDepositsValues.Append("Values(" + patientId + "," + visitId + ", " + locationId + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SbpatientInterviewerColumns = new StringBuilder();
            StringBuilder SbpatientInterviewerValues = new StringBuilder();
            //  SbpatientInterviewerColumns.Append(" Delete  from [DTL_PATIENTINTERVIEWER] where Ptn_Pk=" + PatientID + " and Visit_Pk=" + VisitID + " and LocationID=" + LocationID + " ");
            SbpatientInterviewerColumns.Append("Insert into [DTL_PATIENTINTERVIEWER](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbpatientInterviewerValues.Append("Values(" + patientId + "," + visitId + ", " + locationId + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SbCustColumns = new StringBuilder();
            StringBuilder SbCustValues = new StringBuilder();
            string TableName = "DTL_FBCUSTOMFIELD_" + "Patient Registration".Replace(' ', '_');
            SbCustColumns.Append("if exists(select name from sysobjects where type = 'u' and name ='" + TableName + "') begin ");
            //  SbCustColumns.Append(" Delete  from [" + TableName + "] where Ptn_Pk=" + PatientID + " and Visit_pk=" + VisitID + " and LocationID=" + LocationID + " ");
            sbDeletion.Append(" Delete  from [" + TableName + "] where Ptn_Pk=" + patientId + " and Visit_pk=" + visitId + " and LocationID=" + locationId + " ; ");
            SbCustColumns.Append(" Insert into [" + TableName + "](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbCustValues.Append("Values(" + patientId + "," + visitId + "," + locationId + "," + Session["AppUserId"] + ", GetDate(),");
            //////////////////////////////////////////////////
            //For Controls Other than Multiselect
            string ColValue = "";

            foreach (DataRow theMainDR in LnkDTUnique.Rows)
            {
                StringBuilder SbColumns = new StringBuilder();
                StringBuilder SbValues = new StringBuilder();
                if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "MST_PATIENT")
                {
                }
                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTCONTACTS")
                {
                }
                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTHOUSEHOLDINFO")
                { }

                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_RURALRESIDENCE")
                {
                }

                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_URBANRESIDENCE")
                {

                }

                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTHIVPREVCAREENROLLMENT")
                { }

                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTGUARANTOR")
                {
                }


                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTDEPOSITS")
                {

                }

                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTINTERVIEWER")
                {

                }

                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_CUSTOMFIELD")
                {

                }

                else
                {
                    //SbColumns.Append(" Delete  from [" + theMainDR["PDFTableName"] + "] where Ptn_Pk=" + PatientID + " and Visit_pk=" + VisitID + " and LocationID=" + LocationID + " ");
                    sbDeletion.Append(" Delete  from [" + theMainDR["PDFTableName"] + "] where Ptn_Pk=" + patientId + " and Visit_pk=" + visitId + " and LocationID=" + locationId + " ; ");
                    SbColumns.Append(" Insert into [" + theMainDR["PDFTableName"] + "](Ptn_pk,Visit_pk,LocationId,UserID,Updatedate,");
                    SbValues.Append("Values(" + patientId + "," + visitId + ", " + locationId + "," + Session["AppUserId"] + ", GetDate(),");
                }
                foreach (DataRow theSub1DR in theDT.Rows)
                {
                    if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == Convert.ToString(theSub1DR["TableName"]).ToUpper())
                    {
                        if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "MST_PATIENT")
                        {
                            if (Convert.ToString(theSub1DR["Value"]) != "")
                            {
                                ColValue = this.SanitizeQoute(Convert.ToString(theSub1DR["Value"]).Trim());
                                SbmstpatColumns.Append("[" + theSub1DR["Column"] + "]=");
                                if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "ADDRESS")
                                {
                                    SbmstpatColumns.Append("encryptbykey(key_guid('Key_CTC'),'" + ColValue + "'),");
                                }
                                else if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "PHONE")
                                {
                                    SbmstpatColumns.Append("encryptbykey(key_guid('Key_CTC'),'" + ColValue + "'),");
                                }
                                else
                                {
                                    SbmstpatColumns.Append("'" + ColValue + "',");
                                }
                            }
                        }

                        else
                        {

                            if (Convert.ToString(theSub1DR["Value"]) != "")
                            {
                                ColValue = this.SanitizeQoute(Convert.ToString(theSub1DR["Value"]).Trim());
                                if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTCONTACTS")
                                {
                                    HasContacts = true;
                                    SbContColumns.Append("[" + theSub1DR["Column"] + "],");
                                    SbContValues.Append("'" + ColValue + "',");
                                }
                                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTHOUSEHOLDINFO")
                                {
                                    HasHouseHold = true;
                                    SbHouseHoldColumns.Append("[" + theSub1DR["Column"] + "],");
                                    SbHouseHoldValues.Append("'" + ColValue + "',");
                                }

                                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_RURALRESIDENCE")
                                {
                                    HasRural = true;
                                    SbruralResidenceColumns.Append("[" + theSub1DR["Column"] + "],");
                                    SbruralResidenceValues.Append("'" + ColValue + "',");
                                }

                                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_URBANRESIDENCE")
                                {
                                    HasUrban = true;
                                    SburbanresidenceColumns.Append("[" + theSub1DR["Column"] + "],");
                                    SburbanresidenceValues.Append("'" + ColValue + "',");
                                }

                                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTHIVPREVCAREENROLLMENT")
                                {
                                    HasHivPrev = true;
                                    SbpatienthivprevcareenrollmentColumns.Append("[" + theSub1DR["Column"] + "],");
                                    SbpatienthivprevcareenrollmentValues.Append("'" + ColValue + "',");
                                }

                                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTGUARANTOR")
                                {
                                    HasGurantor = true;
                                    SbpatientguarantorColumns.Append("[" + theSub1DR["Column"] + "],");
                                    SbpatientguarantorValues.Append("'" + ColValue + "',");
                                }

                                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTDEPOSITS")
                                {
                                    HasDeposit = true;
                                    SbpatientDepositsColumns.Append("[" + theSub1DR["Column"] + "],");
                                    SbpatientDepositsValues.Append("'" + ColValue + "',");
                                }

                                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTINTERVIEWER")
                                {
                                    HasInterview = true;
                                    SbpatientInterviewerColumns.Append("[" + theSub1DR["Column"] + "],");
                                    SbpatientInterviewerValues.Append("'" + ColValue + "',");

                                }
                                else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_CUSTOMFIELD")
                                {
                                    HasCustom = true;
                                    SbCustColumns.Append("[" + theSub1DR["Column"] + "],");
                                    SbCustValues.Append("'" + ColValue + "',");
                                }
                                else
                                {
                                    SbColumns.Append("[" + theSub1DR["Column"] + "],");
                                    SbValues.Append("'" + ColValue + "',");
                                }
                            }
                        }
                    }
                }
                if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "MST_PATIENT")
                {
                }
                else
                {
                    if (SbColumns.Length > 0)
                    {
                        SbColumns.Remove(SbColumns.Length - 1, 1);
                        SbValues.Remove(SbValues.Length - 1, 1);
                        sbUpdate.Append(SbColumns.Append(")"));
                        sbUpdate.Append(SbValues.Append(")"));
                    }
                }
            }

            SbmstpatColumns.Append("UserID='" + Session["AppUserId"] + "', UpdateDate=getdate() where ptn_pk='" + patientId + "' and LocationID='" + Session["AppLocationId"] + "' ");
            sbUpdate.Append(SbmstpatColumns);
            SbmstpatColumns = new StringBuilder();
            SbmstpatColumns.Append("Update [ord_Visit] Set VisitDate='" + txtRegDate.Text + "', UserID='" + Session["AppUserId"] + "', UpdateDate=getdate() where Visit_Id='" + visitId + "' and Visittype=12");
            sbUpdate.Append(SbmstpatColumns);

            sbUpdate.Append(sbDeletion);

            if (SbContColumns.Length > 0 && HasContacts)
            {
                SbContColumns.Remove(SbContColumns.Length - 1, 1);
                SbContValues.Remove(SbContValues.Length - 1, 1);
                sbUpdate.Append(SbContColumns.Append(")"));
                sbUpdate.Append(SbContValues.Append(")"));
            }

            if (SbHouseHoldColumns.Length > 0 && HasHouseHold)
            {
                SbHouseHoldColumns.Remove(SbHouseHoldColumns.Length - 1, 1);
                SbHouseHoldValues.Remove(SbHouseHoldValues.Length - 1, 1);
                sbUpdate.Append(SbHouseHoldColumns.Append(")"));
                sbUpdate.Append(SbHouseHoldValues.Append(")"));
            }

            if (SbruralResidenceColumns.Length > 0 && HasRural)
            {
                SbruralResidenceColumns.Remove(SbruralResidenceColumns.Length - 1, 1);
                SbruralResidenceValues.Remove(SbruralResidenceValues.Length - 1, 1);
                sbUpdate.Append(SbruralResidenceColumns.Append(")"));
                sbUpdate.Append(SbruralResidenceValues.Append(")"));
            }

            if (SburbanresidenceColumns.Length > 0 && HasUrban)
            {
                SburbanresidenceColumns.Remove(SburbanresidenceColumns.Length - 1, 1);
                SburbanresidenceValues.Remove(SburbanresidenceValues.Length - 1, 1);
                sbUpdate.Append(SburbanresidenceColumns.Append(")"));
                sbUpdate.Append(SburbanresidenceValues.Append(")"));
            }

            if (SbpatienthivprevcareenrollmentColumns.Length > 0 && HasHivPrev)
            {
                SbpatienthivprevcareenrollmentColumns.Remove(SbpatienthivprevcareenrollmentColumns.Length - 1, 1);
                SbpatienthivprevcareenrollmentValues.Remove(SbpatienthivprevcareenrollmentValues.Length - 1, 1);
                sbUpdate.Append(SbpatienthivprevcareenrollmentColumns.Append(")"));
                sbUpdate.Append(SbpatienthivprevcareenrollmentValues.Append(")"));
            }

            if (SbpatientguarantorColumns.Length > 0 && HasGurantor)
            {
                SbpatientguarantorColumns.Remove(SbpatientguarantorColumns.Length - 1, 1);
                SbpatientguarantorValues.Remove(SbpatientguarantorValues.Length - 1, 1);
                sbUpdate.Append(SbpatientguarantorColumns.Append(")"));
                sbUpdate.Append(SbpatientguarantorValues.Append(")"));
            }

            if (SbpatientDepositsColumns.Length > 0 && HasDeposit)
            {
                SbpatientDepositsColumns.Remove(SbpatientDepositsColumns.Length - 1, 1);
                SbpatientDepositsValues.Remove(SbpatientDepositsValues.Length - 1, 1);
                sbUpdate.Append(SbpatientDepositsColumns.Append(")"));
                sbUpdate.Append(SbpatientDepositsValues.Append(")"));
            }

            if (SbpatientInterviewerColumns.Length > 0 && HasInterview)
            {
                SbpatientInterviewerColumns.Remove(SbpatientInterviewerColumns.Length - 1, 1);
                SbpatientInterviewerValues.Remove(SbpatientInterviewerValues.Length - 1, 1);
                sbUpdate.Append(SbpatientInterviewerColumns.Append(")"));
                sbUpdate.Append(SbpatientInterviewerValues.Append(")"));
            }
            if (SbCustColumns.Length > 0 && HasCustom)
            {
                SbCustColumns.Remove(SbCustColumns.Length - 1, 1);
                SbCustValues.Remove(SbCustValues.Length - 1, 1);
                sbUpdate.Append(SbCustColumns.Append(")"));
                sbUpdate.Append(SbCustValues.Append(") end "));
            }
            //For MultiSelect control
            if (theDTMulti != null)
            {
                foreach (DataRow DRMultiSelect in theDTMulti.Rows)
                {
                    if (DRMultiSelect["ControlID"].ToString() == "9")
                    {
                        StringBuilder DeleteMultiselect = UpdateMultiSelectList(patientId, FeatureId, visitId, locationId, DRMultiSelect["PDFTableName"].ToString(),
                            DRMultiSelect["FieldName"].ToString(), 0, Convert.ToInt32(DRMultiSelect["ControlID"]));
                        sbUpdate.Append(DeleteMultiselect);
                        StringBuilder InsertMultiselect = UpdateMultiSelectList(patientId, FeatureId, visitId, locationId, DRMultiSelect["PDFTableName"].ToString(),
                            DRMultiSelect["FieldName"].ToString(), 1, Convert.ToInt32(DRMultiSelect["ControlID"]));
                        sbUpdate.Append(InsertMultiselect);
                    }
                }
            }


            //Generating Query for CondMultiSelect 
            if (theDTConMulti != null)
            {
                foreach (DataRow DRMultiSelect in theDTConMulti.Rows)
                {
                    if (DRMultiSelect["ControlID"].ToString() == "9")
                    {
                        StringBuilder DeleteMultiselect = UpdateMultiSelectList(patientId, FeatureId, visitId, locationId, DRMultiSelect["PDFTableName"].ToString(),
                            DRMultiSelect["FieldName"].ToString(), 0, Convert.ToInt32(DRMultiSelect["ControlID"]));
                        sbUpdate.Append(DeleteMultiselect);
                        StringBuilder InsertMultiselect = UpdateMultiSelectList(patientId, FeatureId, visitId, locationId, DRMultiSelect["PDFTableName"].ToString(),
                            DRMultiSelect["FieldName"].ToString(), 1, Convert.ToInt32(DRMultiSelect["ControlID"]));
                        sbUpdate.Append(InsertMultiselect);
                    }
                }
            }
            //  

            sbUpdate.Append("Select 1[Saved]");
            return sbUpdate;
        }
        /// <summary>
        /// Sanitizes the qoute.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns></returns>
        string SanitizeQoute(string inputString)
        {
            return System.Text.RegularExpressions.Regex.Replace(inputString, "[']", "''");

        }
        private DataTable PatientInfo()
        {

            DataTable theDT = new DataTable();
            theDT.Columns.Add("FielName", typeof(string));
            theDT.Columns.Add("FieldValue", typeof(string));
            theDT.Columns.Add("TableName", typeof(string));
            theDT.Columns.Add("DataType", typeof(string));
            return theDT;
        }
        private StringBuilder SaveCustomRegistrationData()
        {
            //ICustomForm MgrSaveUpdate = (ICustomForm)ObjectFactory.CreateInstance(ObjFactoryParameterBCustom);

            DataTable LnkDTUnique = new DataTable();
            StringBuilder sbUpdate = new StringBuilder();
            DataTable theDTNoMulti = new DataTable();
            DataTable theDTMulti = new DataTable();
            DataTable theDTConMulti = new DataTable();

            DataTable theDT = SetControlIDs(PnlDynamicElements);
            theDTNoMulti = ((DataTable)ViewState["NoMulti"]);
            theDTMulti = ((DataTable)ViewState["LnkTable"]);
            theDTConMulti = ((DataTable)ViewState["LnkConTable"]);

            StringBuilder SbInsert = new StringBuilder();
            if (ViewState["NoMulti"] != null)
            {
                LnkDTUnique = theDTNoMulti.DefaultView.ToTable(true, "PDFTableName", "FeatureName").Copy();
            }
            int DOBPrecision = 0;
            if (rbtndobPrecEstimated.Checked == true)
            {
                DOBPrecision = 1;
            }
            else if (rbtndobPrecExact.Checked == true)
            {
                DOBPrecision = 0;
            }
            else
            {
                DOBPrecision = 2;
            }
            ///////////////Added by Naveen///////////////////
            StringBuilder SbmstpatColumns = new StringBuilder();
            StringBuilder SbmstpatValues = new StringBuilder();

            bool HasContacts = false;
            bool HasHouseHold = false;
            bool HasRural = false;
            bool HasUrban = false;
            bool HasHivPrev = false;
            bool HasGurantor = false;
            bool HasDeposit = false;
            bool HasInterview = false;
            bool HasCustom = false;
            SbmstpatColumns.Append("Insert into [MST_PATIENT](");
            SbmstpatValues.Append("Values(");
            SbmstpatColumns.Append("Status, FirstName, MiddleName, LastName, LocationID, RegistrationDate,Sex,DOB,DobPrecision,MaritalStatus, CountryId, PosId, SatelliteId, UserID, CreateDate,");
            SbmstpatValues.Append("'0', encryptbykey(key_guid('Key_CTC'),'" + this.SanitizeQoute(txtfirstName.Text) + "'), encryptbykey(key_guid('Key_CTC'),'" + this.SanitizeQoute(txtmiddleName.Text) + "'), encryptbykey(key_guid('Key_CTC'),'" + this.SanitizeQoute(txtlastName.Text) + "')");
            SbmstpatValues.Append(", '" + Session["AppLocationId"] + "', '" + txtRegDate.Text + "', '" + ddgender.SelectedValue + "', '" + TxtDOB.Text + "', '" + DOBPrecision + "', '" + ddmaritalStatus.SelectedValue + "',");
            SbmstpatValues.Append("'" + Session["AppCountryId"] + "', '" + Session["AppPosID"] + "', '" + Session["AppSatelliteId"] + "', '" + Session["AppUserId"] + "', getdate(),");


            StringBuilder SbContColumns = new StringBuilder();
            StringBuilder SbContValues = new StringBuilder();
            SbContColumns.Append("Insert into [DTL_PATIENTCONTACTS](Ptn_pk,Visitid,LocationId,UserID,CreateDate,");
            SbContValues.Append("Values(@patient_pk,@visit_pk ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");


            StringBuilder SbHouseHoldColumns = new StringBuilder();
            StringBuilder SbHouseHoldValues = new StringBuilder();
            SbHouseHoldColumns.Append("Insert into [DTL_PATIENTHOUSEHOLDINFO](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbHouseHoldValues.Append("Values(@patient_pk,@visit_pk ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SbruralResidenceColumns = new StringBuilder();
            StringBuilder SbruralResidenceValues = new StringBuilder();
            SbruralResidenceColumns.Append("Insert into [DTL_RURALRESIDENCE](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbruralResidenceValues.Append("Values(@patient_pk,@visit_pk ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SburbanresidenceColumns = new StringBuilder();
            StringBuilder SburbanresidenceValues = new StringBuilder();
            SburbanresidenceColumns.Append("Insert into [DTL_URBANRESIDENCE](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SburbanresidenceValues.Append("Values(@patient_pk,@visit_pk ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SbpatienthivprevcareenrollmentColumns = new StringBuilder();
            StringBuilder SbpatienthivprevcareenrollmentValues = new StringBuilder();
            SbpatienthivprevcareenrollmentColumns.Append("Insert into [DTL_PATIENTHIVPREVCAREENROLLMENT](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbpatienthivprevcareenrollmentValues.Append("Values(@patient_pk,@visit_pk ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SbpatientguarantorColumns = new StringBuilder();
            StringBuilder SbpatientguarantorValues = new StringBuilder();
            SbpatientguarantorColumns.Append("Insert into [DTL_PATIENTGUARANTOR](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbpatientguarantorValues.Append("Values(@patient_pk,@visit_pk ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SbpatientDepositsColumns = new StringBuilder();
            StringBuilder SbpatientDepositsValues = new StringBuilder();
            SbpatientDepositsColumns.Append("Insert into [DTL_PATIENTDEPOSITS](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbpatientDepositsValues.Append("Values(@patient_pk,@visit_pk ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SbpatientInterviewerColumns = new StringBuilder();
            StringBuilder SbpatientInterviewerValues = new StringBuilder();
            SbpatientInterviewerColumns.Append("Insert into [DTL_PATIENTINTERVIEWER](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbpatientInterviewerValues.Append("Values(@patient_pk,@visit_pk ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

            StringBuilder SbCustColumns = new StringBuilder();
            StringBuilder SbCustValues = new StringBuilder();
            string TableName = "DTL_FBCUSTOMFIELD_" + "Patient Registration".Replace(' ', '_');
            SbCustColumns.Append("if exists(select name from sysobjects where name = '" + TableName + "') begin ");
            SbCustColumns.Append("Insert into [" + TableName + "](Ptn_pk,Visit_Pk,LocationId,UserID,CreateDate,");
            SbCustValues.Append("Values(@patient_pk,@visit_pk ," + Session["AppLocationId"] + "," + Session["AppUserId"] + ", GetDate(),");

            List<RegistrationParameter> rgParam = new List<RegistrationParameter>();
            string ColValue = "";
            /////////////////////////////////////////////////////
            foreach (DataRow theMainDR in LnkDTUnique.Rows)
            {
                StringBuilder SbColumns = new StringBuilder();
                StringBuilder SbValues = new StringBuilder();

                foreach (DataRow theSub1DR in theDT.Rows)
                {
                    if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == Convert.ToString(theSub1DR["TableName"]).ToUpper())
                    {
                        if (Convert.ToString(theSub1DR["Value"]) != "")
                        {
                            ColValue = this.SanitizeQoute(Convert.ToString(theSub1DR["Value"]));
                            if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "MST_PATIENT")
                            {
                                SbmstpatColumns.Append("[" + theSub1DR["Column"] + "],");

                                if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "ADDRESS")
                                {
                                    SbmstpatValues.Append("encryptbykey(key_guid('Key_CTC'),'" + ColValue + "'),");
                                }
                                else if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "PHONE")
                                {
                                    SbmstpatValues.Append("encryptbykey(key_guid('Key_CTC'),'" + ColValue + "'),");
                                }
                                else
                                {
                                    SbmstpatValues.Append("'" + ColValue + "',");
                                }
                            }
                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTCONTACTS")
                            {
                                HasContacts = true;
                                SbContColumns.Append("[" + theSub1DR["Column"] + "],");
                                if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "ADDRESS")
                                {
                                    SbContValues.Append("encryptbykey(key_guid('Key_CTC'),'" + ColValue + "'),");
                                }
                                else if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "PHONE")
                                {
                                    SbContValues.Append("encryptbykey(key_guid('Key_CTC'),'" + ColValue + "'),");
                                }
                                else
                                {
                                    SbContValues.Append("'" + ColValue + "',");
                                }
                            }

                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTHOUSEHOLDINFO")
                            {
                                HasHouseHold = true;
                                SbHouseHoldColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbHouseHoldValues.Append("'" + ColValue + "',");
                            }

                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_RURALRESIDENCE")
                            {
                                HasRural = true;
                                SbruralResidenceColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbruralResidenceValues.Append("'" + ColValue + "',");
                            }

                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_URBANRESIDENCE")
                            {
                                HasUrban = true;
                                SburbanresidenceColumns.Append("[" + theSub1DR["Column"] + "],");
                                SburbanresidenceValues.Append("'" + ColValue + "',");
                            }

                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTHIVPREVCAREENROLLMENT")
                            {
                                HasHivPrev = true;
                                SbpatienthivprevcareenrollmentColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbpatienthivprevcareenrollmentValues.Append("'" + ColValue + "',");
                            }

                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTGUARANTOR")
                            {
                                HasGurantor = true;
                                SbpatientguarantorColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbpatientguarantorValues.Append("'" + ColValue + "',");
                            }

                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTDEPOSITS")
                            {
                                HasDeposit = true;
                                SbpatientDepositsColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbpatientDepositsValues.Append("'" + ColValue + "',");
                            }

                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_PATIENTINTERVIEWER")
                            {
                                HasInterview = true;
                                SbpatientInterviewerColumns.Append("[" + theSub1DR["Column"] + "],");
                                SbpatientInterviewerValues.Append("'" + ColValue + "',");

                            }
                            else if (Convert.ToString(theMainDR["PDFTableName"]).ToUpper() == "DTL_CUSTOMFIELD")
                            {
                                HasCustom = true;
                                SbCustColumns.Append("[" + theSub1DR["Column"] + "],");
                                if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "ADDRESS")
                                {
                                    SbCustValues.Append("encryptbykey(key_guid('Key_CTC'),'" + ColValue + "'),");
                                }
                                else if (Convert.ToString(theSub1DR["Column"]).ToUpper() == "PHONE")
                                {
                                    SbCustValues.Append("encryptbykey(key_guid('Key_CTC'),'" + ColValue + "'),");
                                }
                                else
                                {
                                    SbCustValues.Append("'" + theSub1DR["Value"] + "',");
                                }
                            }

                            string columnName = theSub1DR["Column"].ToString();
                            string columnValue = theSub1DR["Value"].ToString();
                            string tablename = Convert.ToString(theMainDR["PDFTableName"]);

                            rgParam.Add(new RegistrationParameter()
                            {
                                Name = columnName,
                                Value = columnValue,
                                Destination = tablename,
                                DataType = DbType.String,
                                Encrypt = (columnName.ToUpper() == "ADDRESS" || columnName.ToUpper() == "PHONE") ? true : false
                            }
                            );
                        }
                    }
                }
            }
            if (SbmstpatColumns.Length > 0)
            {
                SbmstpatColumns.Remove(SbmstpatColumns.Length - 1, 1);
                SbmstpatValues.Remove(SbmstpatValues.Length - 1, 1);
                SbInsert.Append(SbmstpatColumns.Append(")"));
                SbInsert.Append(SbmstpatValues.Append(")"));
                SbmstpatColumns = new StringBuilder();
                SbmstpatValues = new StringBuilder();
                SbmstpatColumns.Append("declare @patient_pk int; declare @visit_pk  int; Select @patient_pk = SCOPE_IDENTITY(); Insert into ord_Visit(Ptn_Pk,LocationID,VisitDate,VisitType,UserID,CreateDate");
                SbmstpatValues.Append("values (@patient_pk,'" + Session["AppLocationId"] + "', '" + txtRegDate.Text + "', 12, '" + Session["AppUserId"] + "', getdate()");
                SbInsert.Append(SbmstpatColumns.Append(")"));
                SbInsert.Append(SbmstpatValues.Append("); Select @visit_pk = SCOPE_IDENTITY()"));
            }

            if (SbContColumns.Length > 0 && HasContacts)
            {
                SbContColumns.Remove(SbContColumns.Length - 1, 1);
                SbContValues.Remove(SbContValues.Length - 1, 1);
                SbInsert.Append(SbContColumns.Append(")"));
                SbInsert.Append(SbContValues.Append(")"));
            }

            if (SbHouseHoldColumns.Length > 0 && HasHouseHold)
            {
                SbHouseHoldColumns.Remove(SbHouseHoldColumns.Length - 1, 1);
                SbHouseHoldValues.Remove(SbHouseHoldValues.Length - 1, 1);
                SbInsert.Append(SbHouseHoldColumns.Append(")"));
                SbInsert.Append(SbHouseHoldValues.Append(")"));
            }

            if (SbruralResidenceColumns.Length > 0 && HasRural)
            {
                SbruralResidenceColumns.Remove(SbruralResidenceColumns.Length - 1, 1);
                SbruralResidenceValues.Remove(SbruralResidenceValues.Length - 1, 1);
                SbInsert.Append(SbruralResidenceColumns.Append(")"));
                SbInsert.Append(SbruralResidenceValues.Append(")"));
            }

            if (SburbanresidenceColumns.Length > 0 && HasUrban)
            {
                SburbanresidenceColumns.Remove(SburbanresidenceColumns.Length - 1, 1);
                SburbanresidenceValues.Remove(SburbanresidenceValues.Length - 1, 1);
                SbInsert.Append(SburbanresidenceColumns.Append(")"));
                SbInsert.Append(SburbanresidenceValues.Append(")"));
            }

            if (SbpatienthivprevcareenrollmentColumns.Length > 0 && HasHivPrev)
            {
                SbpatienthivprevcareenrollmentColumns.Remove(SbpatienthivprevcareenrollmentColumns.Length - 1, 1);
                SbpatienthivprevcareenrollmentValues.Remove(SbpatienthivprevcareenrollmentValues.Length - 1, 1);
                SbInsert.Append(SbpatienthivprevcareenrollmentColumns.Append(")"));
                SbInsert.Append(SbpatienthivprevcareenrollmentValues.Append(")"));
            }

            if (SbpatientguarantorColumns.Length > 0 && HasGurantor)
            {
                SbpatientguarantorColumns.Remove(SbpatientguarantorColumns.Length - 1, 1);
                SbpatientguarantorValues.Remove(SbpatientguarantorValues.Length - 1, 1);
                SbInsert.Append(SbpatientguarantorColumns.Append(")"));
                SbInsert.Append(SbpatientguarantorValues.Append(")"));
            }

            if (SbpatientDepositsColumns.Length > 0 && HasDeposit)
            {
                SbpatientDepositsColumns.Remove(SbpatientDepositsColumns.Length - 1, 1);
                SbpatientDepositsValues.Remove(SbpatientDepositsValues.Length - 1, 1);
                SbInsert.Append(SbpatientDepositsColumns.Append(")"));
                SbInsert.Append(SbpatientDepositsValues.Append(")"));
            }

            if (SbpatientInterviewerColumns.Length > 0 && HasInterview)
            {
                SbpatientInterviewerColumns.Remove(SbpatientInterviewerColumns.Length - 1, 1);
                SbpatientInterviewerValues.Remove(SbpatientInterviewerValues.Length - 1, 1);
                SbInsert.Append(SbpatientInterviewerColumns.Append(")"));
                SbInsert.Append(SbpatientInterviewerValues.Append(")"));
            }


            if (SbCustColumns.Length > 0 && HasCustom)
            {
                SbCustColumns.Remove(SbCustColumns.Length - 1, 1);
                SbCustValues.Remove(SbCustValues.Length - 1, 1);
                SbInsert.Append(SbCustColumns.Append(")"));
                SbInsert.Append(SbCustValues.Append(") end "));
            }

            SbInsert.Append("update mst_patient set IQNumber = 'IQ-'+convert(varchar,Replicate('0',20-len(x.[ptnIdentifier]))) +x.[ptnIdentifier]  from ");
            SbInsert.Append("(select UPPER(substring(convert(varchar(50),decryptbykey(firstname)),1,1))+UPPER(substring(convert(varchar(50),decryptbykey(lastname)),1,1))+");
            SbInsert.Append("convert(varchar,dob,112)+convert(varchar,locationid)+Convert(varchar(10),ptn_pk) [ptnIdentifier] from mst_patient ");
            SbInsert.Append("where ptn_pk = @patient_pk)x where ptn_pk= @patient_pk ");
            SbInsert.Append("Select @patient_pk[ptn_pk], a.IQNumber, b.Visit_ID from mst_patient a inner join Ord_visit b on a.ptn_pk=b.ptn_pk where a.Ptn_Pk=@patient_pk and b.visittype=12");
            //Generating Query for MultiSelect 
            if (theDTMulti != null)
            {
                foreach (DataRow DRMultiSelect in theDTMulti.Rows)
                {
                    if (DRMultiSelect["ControlID"].ToString() == "9")
                    {
                        StringBuilder InsertMultiselect = InsertMultiSelectList("@patient_pk", DRMultiSelect["FieldName"].ToString(), Convert.ToInt32(DRMultiSelect["FeatureID"].ToString()),
                            DRMultiSelect["PDFTableName"].ToString(), Convert.ToInt32(DRMultiSelect["ControlID"]), Convert.ToInt32(DRMultiSelect["FieldId"]));
                        if (SbInsert[0].ToString().Contains(DRMultiSelect["PDFTableName"].ToString()) == false)
                            SbInsert.Append(InsertMultiselect);
                    }

                }
            }
            //  

            //Generating Query for CondMultiSelect 
            if (theDTConMulti != null)
            {
                foreach (DataRow DRMultiSelect in theDTConMulti.Rows)
                {
                    if (DRMultiSelect["ControlID"].ToString() == "9")
                    {
                        StringBuilder InsertMultiselect = InsertMultiSelectList("@patient_pk", DRMultiSelect["FieldName"].ToString(), Convert.ToInt32(DRMultiSelect["FeatureID"].ToString()),
                            DRMultiSelect["PDFTableName"].ToString(), Convert.ToInt32(DRMultiSelect["ControlID"]), Convert.ToInt32(DRMultiSelect["FieldId"]));
                        if (SbInsert[0].ToString().Contains(DRMultiSelect["PDFTableName"].ToString()) == false)
                            SbInsert.Append(InsertMultiselect);
                    }

                }
            }
            //  
            return SbInsert;

        }
        private void Binddropdown()
        {
            try
            {
                BindFunctions BindManager = new BindFunctions();
                IQCareUtils theUtils = new IQCareUtils();
                if ((Session["PatientId"] == null) || (Convert.ToInt32(Session["PatientId"]) == 0))
                {
                    DataView theDV = new DataView();
                    DataTable theDT = new DataTable();

                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    theDV.RowFilter = "CodeID=4";
                    if (theDV.Table != null)
                    {
                        theDT = theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddgender, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }

                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    Session["SystemId"] = "1";
                    theDV.RowFilter = "CodeID=12 and SystemID=1 and DeleteFlag=0";
                    theDT = theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddmaritalStatus, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
                }
                else
                {
                    DataView theDV = new DataView();
                    DataTable theDT = new DataTable();

                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    theDV.RowFilter = "CodeID=4";
                    if (theDV.Table != null)
                    {
                        theDT = theUtils.CreateTableFromDataView(theDV);
                        BindManager.BindCombo(ddgender, theDT, "Name", "ID");
                        theDV.Dispose();
                        theDT.Clear();
                    }

                    theDV = new DataView(theDSXML.Tables["Mst_Decode"]);
                    Session["SystemId"] = "1";

                    theDV.RowFilter = "CodeID=12 and SystemID=" + Session["SystemId"] + "";
                    theDT = theUtils.CreateTableFromDataView(theDV);
                    BindManager.BindCombo(ddmaritalStatus, theDT, "Name", "ID");
                    theDV.Dispose();
                    theDT.Clear();
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

            }

        }

        private DataTable SetControlIDs(Control theControl)
        {
            DataTable TempDT = new DataTable();

            DataColumn Column = new DataColumn("Column");
            Column.DataType = Type.GetType("System.String");
            TempDT.Columns.Add(Column);

            DataColumn Control = new DataColumn("FieldID");
            Control.DataType = Type.GetType("System.String");
            TempDT.Columns.Add(Control);

            DataColumn Value = new DataColumn("Value");
            Value.DataType = Type.GetType("System.String");
            TempDT.Columns.Add(Value);

            DataColumn TableName = new DataColumn("TableName");
            TableName.DataType = Type.GetType("System.String");
            TempDT.Columns.Add(TableName);

            DataRow DRTemp;
            DRTemp = TempDT.NewRow();
            ///////////////Sanjay////////////////////////
            ConFieldEnableDisable(PnlDynamicElements);
            ////////////////////////////////////////////
            foreach (Control x in theControl.Controls)
            {
                if (x.GetType() == typeof(TextBox))
                {

                    DRTemp = TempDT.NewRow();
                    string[] str = ((TextBox)x).ID.Split('-');
                    DRTemp["Column"] = str[1];
                    if (((TextBox)x).Enabled == true)
                        DRTemp["Value"] = ((TextBox)x).Text;
                    else
                        DRTemp["Value"] = "";
                    DRTemp["TableName"] = str[2];
                    DRTemp["FieldID"] = str[3];
                    TempDT.Rows.Add(DRTemp);

                }
                if (x.GetType() == typeof(HtmlInputRadioButton))
                {

                    DRTemp = TempDT.NewRow();
                    string[] str = ((HtmlInputRadioButton)x).ID.Split('-');
                    if (((HtmlInputRadioButton)x).ID == "RADIO1-" + str[1] + "-" + str[2] + "-" + str[3])
                    {
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            DRTemp["Column"] = str[1];
                            if (((HtmlInputRadioButton)x).Visible == true)
                                DRTemp["Value"] = "1";
                            else
                                DRTemp["Value"] = "";
                        }
                    }
                    else if (((HtmlInputRadioButton)x).ID == "RADIO2-" + str[1] + "-" + str[2] + "-" + str[3])
                    {
                        if (((HtmlInputRadioButton)x).Checked == true)
                        {
                            DRTemp["Column"] = str[1];
                            if (((HtmlInputRadioButton)x).Visible == true)
                                DRTemp["Value"] = "0";
                            else
                                DRTemp["Value"] = "";
                        }

                    }

                    DRTemp["TableName"] = str[2];
                    DRTemp["FieldID"] = str[3];
                    TempDT.Rows.Add(DRTemp);
                }
                if (x.GetType() == typeof(DropDownList))
                {
                    DRTemp = TempDT.NewRow();
                    string[] str = ((DropDownList)x).ID.Split('-');
                    DRTemp["Column"] = str[1];
                    if (((DropDownList)x).Enabled == true)
                        DRTemp["Value"] = ((DropDownList)x).SelectedValue;
                    else
                        //DRTemp["Value"] = "0";
                        DRTemp["Value"] = "";
                    DRTemp["TableName"] = str[2];
                    DRTemp["FieldID"] = str[3];
                    TempDT.Rows.Add(DRTemp);
                }

                if (x.GetType() == typeof(HtmlInputCheckBox))
                {
                    DRTemp = TempDT.NewRow();
                    string[] str = ((HtmlInputCheckBox)x).ID.Split('-');
                    DRTemp["Column"] = str[1];
                    if (((HtmlInputCheckBox)x).Visible == true)
                    {
                        if (((HtmlInputCheckBox)x).Checked == true)
                        {
                            DRTemp["Value"] = 1;
                        }
                        else
                        {
                            DRTemp["Value"] = 0;
                        }
                    }
                    else
                    {
                        DRTemp["Value"] = "";
                    }
                    DRTemp["TableName"] = str[2];
                    DRTemp["FieldID"] = str[3];
                    TempDT.Rows.Add(DRTemp);
                }
            }
            return TempDT;
        }

        private bool FieldValidation()
        {
            IIQCareSystem IQCareSecurity;
            IQCareSecurity = (IIQCareSystem)ObjectFactory.CreateInstance("BusinessProcess.Security.BIQCareSystem, BusinessProcess.Security");
            DateTime theCurrentDate = IQCareSecurity.SystemDate();
            IQCareUtils theUtils = new IQCareUtils();
            if (txtfirstName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "First Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtfirstName.Focus();
                return false;
            }
            else if (txtlastName.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Last Name";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtlastName.Focus();
                return false;
            }
            else if (txtRegDate.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Registration Date";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                txtlastName.Focus();
                return false;
            }
            DateTime theEnrolDate = Convert.ToDateTime(theUtils.MakeDate(txtRegDate.Text));
            if (theEnrolDate > theCurrentDate)
            {
                IQCareMsgBox.Show("EnrolDate", this);
                return false;
            }
            if (ddgender.SelectedValue.Trim() == "0")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Sex";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);
                ddgender.Focus();
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
            if (Convert.ToInt32(Session["PatientId"]) > 0 && ViewState["ARTStartDate"] != null)
            {
                DateTime theARTRegDate = Convert.ToDateTime(ViewState["ARTStartDate"].ToString());
                if (theEnrolDate > theARTRegDate)
                {
                    IQCareMsgBox.Show("ARTRegDate", this);
                    return false;
                }
            }
            return true;
        }
        private string ValidationMessage()
        {
            
            DateTime theCurrentDate = CurrentSession.SystemDate();
            string strmsg = "Following values are required to complete this:\\n\\n";
            DataTable theDT = (DataTable)ViewState["BusRule"];
            string Radio1 = "", Radio2 = "", MultiSelectName = "", MultiSelectLabel = "";
            int TotCount = 0, FalseCount = 0;
            try
            {
                foreach (Control x in PnlDynamicElements.Controls)
                {
                    if (x.GetType() == typeof(TextBox))
                    {
                        string[] Field = ((TextBox)x).ID.Split('-');
                        foreach (DataRow theDR in theDT.Rows)
                        {
                            if ((((TextBox)x).ID.Contains("=") == true) && (((TextBox)x).Enabled == true))
                            {
                                string[] Field10 = ((TextBox)x).ID.Replace('=', '-').Split('-');
                                if (Field10[1] == Convert.ToString(theDR["FieldName"]) && Field10[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field10[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                                {
                                    if (((TextBox)x).Text == "")
                                    {
                                        strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                        strmsg = strmsg + "\\n";
                                    }
                                }

                            }

                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                            {
                                if ((((TextBox)x).Text == "") && (((TextBox)x).Enabled == true))
                                {
                                    strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                    strmsg = strmsg + "\\n";
                                }
                            }
                            //Date Greater than Today's Date
                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "7")
                            {
                                if (((TextBox)x).Text != "")
                                {
                                    DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);
                                    if (GetDate <= theCurrentDate)
                                    {
                                        strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                        strmsg = strmsg + "\\n";
                                    }
                                }
                            }
                            //Date Less than Today's Date
                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "8")
                            {
                                if (((TextBox)x).Text != "")
                                {
                                    DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);

                                    if (GetDate >= theCurrentDate)
                                    {
                                        strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                        strmsg = strmsg + "\\n";
                                    }
                                }
                            }
                            //Date greater than Date of Birth
                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "9")
                            {
                                DateTime GetDOB = Convert.ToDateTime(TxtDOB.Text);
                                if (((TextBox)x).Text != "")
                                {
                                    DateTime GetDate = Convert.ToDateTime(((TextBox)x).Text);
                                    if (GetDate <= GetDOB)
                                    {
                                        strmsg += theDR["Name"] + " for " + theDR["FieldLabel"];
                                        strmsg = strmsg + "\\n";
                                    }
                                }
                            }
                        }
                    }
                    if (x.GetType() == typeof(HtmlInputRadioButton))
                    {
                        string[] Field = ((HtmlInputRadioButton)x).ID.Split('-');
                        if (Field[0] == "RADIO1" && ((HtmlInputRadioButton)x).Checked == false)
                        {
                            Radio1 = Field[3];
                        }
                        if (Field[0] == "RADIO2" && ((HtmlInputRadioButton)x).Checked == false)
                        {
                            Radio2 = Field[3];
                        }

                        foreach (DataRow theDR in theDT.Rows)
                        {

                            if (Radio1 == Field[3] && Radio2 == Field[3])
                            {
                                if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                                {
                                    strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                    strmsg = strmsg + "\\n";
                                }

                            }

                        }
                    }
                    if (x.GetType() == typeof(DropDownList))
                    {
                        string[] Field = ((DropDownList)x).ID.Split('-');
                        foreach (DataRow theDR in theDT.Rows)
                        {
                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1" && Field[0].ToString() != "SELECTLISTAuto")
                            {
                                if ((((DropDownList)x).SelectedValue == "0") && (Field[0].ToString() != "SELECTLISTAuto") && ((DropDownList)x).Enabled == true)
                                {
                                    strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                    strmsg = strmsg + "\\n";
                                }
                            }
                        }
                    }

                    if (x.GetType() == typeof(HtmlInputCheckBox))
                    {
                        string[] Field = ((HtmlInputCheckBox)x).ID.Split('-');
                        foreach (DataRow theDR in theDT.Rows)
                        {
                            if (Field[1] == Convert.ToString(theDR["FieldName"]) && Field[2].ToLower() == Convert.ToString(theDR["TableName"].ToString().ToLower()) && Field[3] == Convert.ToString(theDR["FieldId"]) && Convert.ToString(theDR["BusRuleId"]) == "1")
                            {
                                if (((HtmlInputCheckBox)x).Checked == false)
                                {
                                    strmsg += theDR["FieldLabel"] + " is " + theDR["Name"];
                                    strmsg = strmsg + "\\n";
                                }
                            }
                        }

                    }

                }

                //MultiSelect Validation

                foreach (Control y in PnlDynamicElements.Controls)
                {
                    if (y.GetType() == typeof(Panel))
                    {
                        foreach (Control z in y.Controls)
                        {

                            if (z.GetType() == typeof(CheckBox))
                            {
                                TotCount++;
                                if (((CheckBox)z).Checked == false)
                                {
                                    FalseCount++;

                                }
                            }
                        }
                        foreach (DataRow theDR in theDT.Rows)
                        {
                            if (Convert.ToString(theDR["ControlId"]) == "9" && ((Panel)y).ID.Substring(4, (((Panel)y).ID.Length - 4)) == Convert.ToString(theDR["FieldID"]) && Convert.ToInt32(theDR["BusRuleId"]) < 13)
                            {
                                MultiSelectName = Convert.ToString(theDR["Name"]);
                                MultiSelectLabel = Convert.ToString(theDR["FieldLabel"]);
                                if (TotCount == FalseCount)
                                {
                                    strmsg += MultiSelectLabel + " is " + MultiSelectName;
                                    strmsg = strmsg + "\\n";
                                }
                            }
                        }

                        TotCount = 0; FalseCount = 0;
                        MultiSelectName = ""; MultiSelectLabel = "";
                    }
                }

            }

            catch (Exception err)
            {

                IQCareMsgBox.NotifyAction(err.Message, "Validation Message", true, this, "");
            }
            finally { }

            return strmsg;
        }
        protected void btncontinue_Click(object sender, EventArgs e)
        {

            if (FieldValidation() == false)
            {
                return;
            }
            string msg = ValidationMessage();
            if (msg.Length > 51)
            {
                IQCareMsgBox.NotifyAction(msg, "Validation failed", true, this, false);
                //MsgBuilder theBuilder1 = new MsgBuilder();
                //theBuilder1.DataElements["MessageText"] = msg;
                //IQCareMsgBox.Show("#C1", theBuilder1, this);
                return;
            }

           
            if (patientId == 0)
            {
                HashTableParameter();
                Session["htPtnRegParameter"] = htParameters;
                StringBuilder Add = SaveCustomRegistrationData();
                Session["CustomRegistration"] = Add;
                SaveCancel();
            }
            else if (patientId > 0)
            {
                if (PatientService.ValidatePatientDate(Convert.ToDateTime(TxtDOB.Text), Convert.ToDateTime(txtRegDate.Text), patientId))
                {
                    StringBuilder Edit = UpdateCustomRegistrationData(patientId, VisitID, LocationId);
                    IPatientRegistration IPatientFormMgr = (IPatientRegistration)ObjectFactory.CreateInstance(ObjFactoryParameter);
                    DataSet Update = IPatientFormMgr.Common_GetSaveUpdateforCustomRegistrion(Edit.ToString());
                    if (Update.Tables[0].Rows.Count > 0)
                    {
                        try
                        {
                         
                            IPatientRegistration pReg;
                            pReg = (IPatientRegistration)ObjectFactory.CreateInstance("BusinessProcess.Clinical.BPatientRegistration, BusinessProcess.Clinical");
                            pReg.BlueCardToGreenCardSyncronise(patientId);
                            pReg = null;
                        }
                        catch (Exception ex)
                        {
                            Exception lastError = ex;
                            lastError.Data.Add("Domain", "Syncing to greencard");
                            Application.Logger.EventLogger logger = new Application.Logger.EventLogger();
                            logger.LogError(ex);
                        }
                        UpdateCancel();
                    }
                }
                else
                {
                    IQCareMsgBox.NotifyAction("The patient has visits before the specified date of birth or registration date", "Invalid Dates", true, this, false);
                }
            }

        }

        private void SaveCancel()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('This Registration will be redirected to Service. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='./AddTechnicalArea.aspx?mod=" + Session["TechnicalAreaId"] + "';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm", script);
        }

        private void UpdateCancel()
        {
            string script = "<script language = 'javascript' defer ='defer' id = 'confirm2'>\n";
            script += "var ans;\n";
            script += "ans=window.confirm('Registration Form Update Successfully. Do you want to close?');\n";
            script += "if (ans==true)\n";
            script += "{\n";
            script += "window.location.href='../ClinicalForms/frmPatient_Home.aspx';\n";
           // script += "window.location.href='./AddTechnicalArea.aspx?mod=" + Session["TechnicalAreaId"] + "';\n";
            script += "}\n";
            script += "</script>\n";
            ClientScript.RegisterStartupScript(this.GetType(), "confirm2", script);
        }

        public void HtmlRadioButtonSelect(object sender)
        {
            HtmlInputRadioButton theButton = ((HtmlInputRadioButton)sender);
            string[] theControlId = theButton.ID.Split('-');
            DataSet theDS = (DataSet)Session["AllData"];
            int theValue = 0;
            if (theButton.Value == "Yes" && theButton.Checked == true)
                theValue = 1;
            else if (theButton.Value == "Yes" && theButton.Checked == false)
                theValue = 0;

            if (theButton.Value == "No" && theButton.Checked == true)
                theValue = 2;
            else if (theButton.Value == "No" && theButton.Checked == false)
                theValue = 0;

            foreach (DataRow theDR in theDS.Tables[6].Rows)
            {
                foreach (Control x in PnlDynamicElements.Controls)
                {
                    if (x.ID != null)
                    {
                        string[] theIdent = x.ID.Split('-');
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                            {
                                ((TextBox)x).Enabled = true;
                                //ApplyBusinessRules(x, "1", true);
                                //ApplyBusinessRules(x, "2", true);
                                //ApplyBusinessRules(x, "3", true);
                            }
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                            {
                                ((TextBox)x).Enabled = false;
                                ((TextBox)x).Text = "";
                            }
                            if ((theIdent[0] == "TXTDTAuto") || (theIdent[0] == "TXTMultiAuto") || (theIdent[0] == "TXTAuto") || (theIdent[0] == "TXTNUMAuto"))
                            {
                                ((TextBox)x).Enabled = false;
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                            {
                                ((DropDownList)x).Enabled = true;
                                //ApplyBusinessRules(x, "4", true);
                            }
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                            {
                                ((DropDownList)x).Enabled = false;
                                ((DropDownList)x).SelectedValue = "0";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl_" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                            {
                                ((Panel)x).Enabled = true;
                                //ApplyBusinessRules(x, "9", true);
                            }
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                            {
                                ((Panel)x).Enabled = false;
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                            {
                                ((Image)x).Visible = true;
                                //ApplyBusinessRules(x, "5", true);
                            }
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                ((Image)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                            {
                                ((HtmlInputRadioButton)x).Visible = true;
                                //ApplyBusinessRules(x, "6", true);
                            }
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                ((HtmlInputRadioButton)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theValue.ToString())
                                ((HtmlInputCheckBox)x).Visible = true;
                            else if (theControlId[3] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() != theValue.ToString())
                                ((HtmlInputCheckBox)x).Visible = false;
                        }
                    }
                }
            }


        }

        public void HtmlCheckBoxSelect(object theObj)
        {
            CheckBox theButton = ((CheckBox)theObj);
            string[] theControlId = theButton.ID.ToString().Split('-');
            DataSet theDS = (DataSet)Session["AllData"];
            int theValue = 0;
            if (theButton.Checked == true)
                theValue = 1;
            else
                theValue = 0;
            foreach (DataRow theDR in theDS.Tables[6].Rows)
            {
                foreach (Control x in PnlDynamicElements.Controls)
                {
                    if (x.ID != null)
                    {
                        string[] theIdent = x.ID.Split('-');
                        if (x.GetType().ToString() == "System.Web.UI.WebControls.TextBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                            {
                                ((TextBox)x).Enabled = true;
                                //ApplyBusinessRules(x, "1", true);
                                //ApplyBusinessRules(x, "2", true);
                                //ApplyBusinessRules(x, "3", true);

                            }
                            else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                            {
                                ((TextBox)x).Enabled = false;
                                ((TextBox)x).Text = "";
                            }
                            if ((theIdent[0] == "TXTDTAuto") || (theIdent[0] == "TXTMultiAuto") || (theIdent[0] == "TXTAuto") || (theIdent[0] == "TXTNUMAuto"))
                            {
                                ((TextBox)x).Enabled = false;
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.DropDownList" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                            {
                                ((DropDownList)x).Enabled = true;
                                //ApplyBusinessRules(x, "4", true);

                            }
                            else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                            {
                                ((DropDownList)x).Enabled = false;
                                ((DropDownList)x).SelectedValue = "0";
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Panel" && theIdent[0] == "Pnl_" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                            {
                                ((Panel)x).Enabled = true;
                                //ApplyBusinessRules(x, "9", true);

                            }
                            else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                            {
                                ((Panel)x).Enabled = false;
                            }
                        }

                        if (x.GetType().ToString() == "System.Web.UI.WebControls.Image" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                            {
                                ((Image)x).Visible = true;
                                //ApplyBusinessRules(x, "5", true);

                            }
                            else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                ((Image)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputRadioButton" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                            {
                                ((HtmlInputRadioButton)x).Visible = true;
                                //ApplyBusinessRules(x, "6", true);

                            }
                            else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                ((HtmlInputRadioButton)x).Visible = false;
                        }

                        if (x.GetType().ToString() == "System.Web.UI.HtmlControls.HtmlInputCheckBox" && theIdent[1] + "-" + theIdent[2] + "-" + theIdent[3] == theDR["FieldName"].ToString() + "-" + theDR["PdfTableName"].ToString() + "-" + theDR["FieldId"].ToString())
                        {
                            if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "1")
                                ((HtmlInputCheckBox)x).Visible = true;
                            else if (theControlId[4] == theDR["ConFieldId"].ToString() && theDR["ConditionalFieldSectionId"].ToString() == theControlId[1].ToString() && theValue.ToString() == "0")
                                ((HtmlInputCheckBox)x).Visible = false;
                        }
                    }
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //VY changed 2014-10-07 to go to facility home when closed
            Response.Redirect("~/frmFacilityHome.aspx");

            /*  if (txtIQCareRef.Text == "")
              {
                  Response.Redirect("~/frmFindAddPatient.aspx");
              }
              else
                  Response.Redirect("frmAddTechnicalArea.aspx");*/
        }

        //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        [WebMethod(EnableSession = true), ScriptMethod]
        public static string EnableControlAge(string strhidden, string age)
        {
            string strreturn = string.Empty;
            try
            {
                string[] ArrCtlId = strhidden.Split(',');
                DataTable theDT = (DataTable)HttpContext.Current.Session["SessionBusRule"];
                foreach (DataRow DR in theDT.Rows)
                {
                    for (int i = 0; i < ArrCtlId.Length; i++)
                    {
                        string[] a = ArrCtlId[i].Split('-');
                        if (a[3].ToString() == Convert.ToString(DR["FieldId"]) && Convert.ToString(DR["BusRuleId"]) == "16")
                        {
                            if ((DR["Value"] != DBNull.Value) && (DR["Value1"] != DBNull.Value))
                            {
                                if (Convert.ToDecimal(age) >= Convert.ToDecimal(DR["Value"]) && Convert.ToDecimal(age) <= Convert.ToDecimal(DR["Value1"]))
                                {
                                    strreturn = ArrCtlId[i].ToString();
                                }

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
            }
            return strreturn;
        }
        //[Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.ReadWrite)]
        // [WebMethod(EnableSession = true), ScriptMethod]
        protected void btncalculate_DOB_Click(object sender, EventArgs e)
        {

            if (txtageCurrentYears.Text.Trim() == "")
            {
                MsgBuilder theBuilder = new MsgBuilder();
                theBuilder.DataElements["Control"] = "Age (Years)";
                IQCareMsgBox.Show("BlankTextBox", theBuilder, this);//,this);
                txtfirstName.Focus();

                return;
            }
            if (txtageCurrentMonths.Text != "")
            {
                if ((Convert.ToInt32(txtageCurrentMonths.Text) < 0) || (Convert.ToInt32(txtageCurrentMonths.Text) > 11))
                {
                    MsgBuilder theMsg = new MsgBuilder();
                    theMsg.DataElements["Control"] = "Age (Month)";
                    IQCareMsgBox.Show("AgeMonthRange", theMsg, this);//this);
                    return;
                }
            }

            int age = 0;
            int months = 0;
            DateTime currentdate;
            age = Convert.ToInt32(txtageCurrentYears.Text);

            if (txtageCurrentMonths.Text != "")
            {
                currentdate = Convert.ToDateTime(Convert.ToDateTime(txtSysDate.Text).Month + "-01-" + Convert.ToDateTime(txtSysDate.Text).Year);
            }
            else
            {
                currentdate = Convert.ToDateTime("06-15-" + Convert.ToDateTime(txtSysDate.Text).Year);
            }
            DateTime birthdate = currentdate.AddYears(age * -1);
            if (txtageCurrentMonths.Text != "")
            {
                months = Convert.ToInt32(txtageCurrentMonths.Text);
                birthdate = birthdate.AddMonths(months * -1);
            }

            TxtDOB.Text = ((DateTime)birthdate).ToString(Session["AppDateFormat"].ToString());

            if (TxtDOB.Text != "")
            {
                rbtndobPrecEstimated.Checked = true;

            }
        }
    }
    
}